using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using CafeF.Redis.BL;
using CafeF.Redis.Data;
using CafeF.Redis.Entity;
using CafeF.Redis.UpdateService;
using ServiceStack.Redis;

namespace CafeF.Redis.KhopLenh
{
    public class PriceData
    {
        #region Properties
        public bool ServiceStarted { get; set; }
        private LogUtils log;
        private LogType logType = (ConfigurationManager.AppSettings["LogType"] ?? "text") == "text" ? LogType.TextLog : LogType.EventLog;
        private bool logEnable = (ConfigurationManager.AppSettings["LogEnable"] ?? "true").ToLower() == "true";
        private string logPath = String.IsNullOrEmpty(ConfigurationManager.AppSettings["LogPath"]) ? AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"log\" : ConfigurationManager.AppSettings["LogPath"].Replace(@"~\", AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
        private EventLog EventLog1 { get; set; }
        private readonly int tradeInterval = int.Parse(ConfigurationManager.AppSettings["LoopInterval"] ?? "1000");
        private readonly int noTradeInterval = int.Parse(ConfigurationManager.AppSettings["NoTradeInterval"] ?? "600000");
        private int index = 0;
        public int TradeCenterId { get; set; }
        #endregion

        public PriceData(EventLog mylog)
        {
            EventLog1 = mylog;
            log = logType == LogType.TextLog ? new LogUtils(logType, logEnable, logPath) : new LogUtils(logType, logEnable, EventLog1);
            log.Enabled = logEnable;
            log.LogPath = logPath;
        }

        #region Business
        public void ExecuteTask()
        {
            try
            {
                var isTrading = false;
                var bfirstTime = true;
                var isBeginTrading = false;
                var isEndTrading = false;
                var ss = new List<StockCompact>();
                var keys = new List<string>();
                var priceKeys = new List<string>();
                var syms = new List<string>();
                var objs = new Dictionary<string, SessionPriceData>();
                var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
                var redisPrice = new RedisClient(ConfigRedis.PriceHost, ConfigRedis.PricePort);
                while (ServiceStarted)
                {
                    try
                    {
                        var b = Utils.InTradingTime(TradeCenterId);
                        isBeginTrading = (!isTrading && b);
                        isEndTrading = (isTrading && !b);
                        isTrading = b;
                        if (!bfirstTime) Thread.Sleep(isTrading ? tradeInterval : noTradeInterval);
                        if (bfirstTime || isBeginTrading)
                        {
                            var key = string.Format(RedisKey.KeyStockListByCenter, TradeCenterId);
                            ss = redis.Get<List<StockCompact>>(key);
                            keys = new List<string>();
                            syms = new List<string>();
                            objs = new Dictionary<string, SessionPriceData>();
                            foreach (var s in ss)
                            {
                                keys.Add(string.Format(RedisKey.RealtimeSessionPrice, s.Symbol));
                                priceKeys.Add(string.Format(RedisKey.PriceKey, s.Symbol));
                                syms.Add(s.Symbol);
                                objs.Add(string.Format(RedisKey.RealtimeSessionPrice, s.Symbol), new SessionPriceData() { Symbol = s.Symbol, Price = -1, TotalValue = 0, TotalVolume = 0, TradeDate = DateTime.Now, Volume = 0 });
                            }
                            //init objects
                            if (isBeginTrading)
                            {
                                redis.SetAll(objs);
                            }
                            else
                            {
                                objs = (Dictionary<string, SessionPriceData>)redis.GetAll<SessionPriceData>(keys);
                            }
                        }
                        bfirstTime = false;
                        if (isTrading)
                        {
                            //detect change
                            var prices = redisPrice.GetAll<StockPrice>(priceKeys);
                            foreach (var sym in syms)
                            {
                                var price = prices[string.Format(RedisKey.PriceKey, sym)];
                                var obj = objs[string.Format(RedisKey.RealtimeSessionPrice, sym)];
                                if (price == null || obj == null) continue;
                                if (obj.Price != price.Price || obj.TotalVolume != price.Volume)
                                {
                                    var vol = price.Volume - obj.TotalVolume;
                                    //trade time --> save
                                    if (obj.Price > -1) //don't save the initial value
                                    {
                                        var key = string.Format(RedisKey.SessionPrice, obj.Symbol, DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HHmmss"));
                                        var o = new SessionPriceData() { Symbol = obj.Symbol, Price = price.Price, TotalValue = price.Value, TotalVolume = price.Volume, TradeDate = DateTime.Now, Volume = vol };
                                        if (redis.ContainsKey(key))
                                            redis.Set(key, o);
                                        else
                                            redis.Add(key, o);
                                    }
                                    //save realtime object
                                    obj.Price = price.Price;
                                    obj.Volume = vol;
                                    obj.TotalVolume = price.Volume;
                                    obj.TotalValue = price.Value;
                                    objs[string.Format(RedisKey.RealtimeSessionPrice, sym)] = obj;
                                }
                            }
                            redis.SetAll(objs);
                        }
                    }
                    catch (Exception ex)
                    {
                        log.WriteEntry(ex.ToString(), EventLogEntryType.Error);
                    }

                }
            }
            catch (Exception ex)
            {
                log.WriteEntry(ex.ToString(), EventLogEntryType.Error);
            }
        }
        public void SaveToDb()
        {
            try
            {
                var done = new List<string>();
                var lastDate = DateTime.Now.AddDays(-1);
                var bfirstTime = true;
                var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
                while (ServiceStarted)
                {
                    try
                    {
                        if (lastDate.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd"))
                        {
                            //reset time
                            done = new List<string>();
                            lastDate = DateTime.Now;
                        }
                        if (!bfirstTime) Thread.Sleep(noTradeInterval);
                        bfirstTime = false;
                        var keys = redis.SearchKeys(string.Format(RedisKey.SessionPrice, "*", DateTime.Now.ToString("yyyyMMdd"), "*"));
                        var count = 0;
                        if (keys != null)
                        {
                            var xml = "<root>";
                            var tmp = new List<string>();
                            foreach (var key in keys)
                            {
                                if (done.Contains(key)) continue;
                                var o = redis.Get<SessionPriceData>(key);
                                if (o == null) continue;
                                count++;
                                xml += string.Format("<info><sym>{0}</sym><price>{1}</price><vol>{2}</vol><tvol>{3}</tvol><tval>{4}</tval><time>{5}</time></info>", o.Symbol, o.Price, o.Volume, o.TotalVolume, o.TotalValue, o.TradeDate.ToString("yyyy-MM-dd HH:mm:ss"));
                                tmp.Add(key);
                                if (count >= 1000)
                                {
                                    try
                                    {
                                        xml += "</root>";
                                        SqlDb.UpdateData(xml);
                                        Thread.Sleep(tradeInterval);
                                        foreach (var item in tmp)
                                        {
                                            done.Add(item);
                                        }
                                        tmp = new List<string>();
                                        xml = "<root>";
                                        count = 0;
                                    }
                                    catch (Exception ex)
                                    {
                                        log.WriteEntry("SaveToDb : " + ex.ToString(), EventLogEntryType.Error);
                                    }
                                }
                            }
                            if (tmp.Count > 0)
                            {
                                xml += "</root>";
                                try
                                {
                                    SqlDb.UpdateData(xml);
                                    foreach (var item in tmp)
                                    {
                                        done.Add(item);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    log.WriteEntry("SaveToDb : " + ex.ToString(), EventLogEntryType.Error);
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        log.WriteEntry("SaveToDb : " + ex.ToString(), EventLogEntryType.Error);
                    }

                }
            }
            catch (Exception ex)
            {
                log.WriteEntry("SaveToDb : " + ex.ToString(), EventLogEntryType.Error);
            }
        }
        #endregion
    }
}
