using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading;
using CafeF.Redis.Data;
using CafeF.Redis.Entity;
using CafeF.Redis.TestUpdate;
using ServiceStack.Redis;
using CafeF.Redis.BL;

namespace CafeF.Redis.UpdateService
{
    public class PriceData
    {
        #region Properties
        public StockCenter Center { get; set; }
        public bool ServiceStarted { get; set; }
        private LogUtils log;
        private LogType logType = (ConfigurationManager.AppSettings["LogType"] ?? "text") == "text" ? LogType.TextLog : LogType.EventLog;
        private bool logEnable = (ConfigurationManager.AppSettings["LogEnable"] ?? "true").ToLower() == "true";
        private string logPath = String.IsNullOrEmpty(ConfigurationManager.AppSettings["LogPath"]) ? AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"log\" : ConfigurationManager.AppSettings["LogPath"].Replace(@"~\", AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
        private EventLog EventLog1 { get; set; }
        #endregion

        private readonly int interval = int.Parse(ConfigurationManager.AppSettings["LoopInterval"] ?? "1000");
        private readonly int longInterval = int.Parse(ConfigurationManager.AppSettings["NoTradeInterval"] ?? "60000");
        private readonly int crawlerInterval = int.Parse(ConfigurationManager.AppSettings["CrawlerInterval"] ?? "300000");
        private int index = 0;

        public PriceData(int ind, EventLog mylog)
        {
            index = ind;
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
                //log.WriteEntry("Start Price : " + index, EventLogEntryType.Information);
                bool bfirstTime = true;
                if ((ConfigurationManager.AppSettings["PriceAllowance"] ?? "") != "TRUE")
                {
                    return;
                }
                //log.WriteEntry("Start Price 2 : " + index, EventLogEntryType.Information);
                bool bHo = false, bHa = false, bLastHo = true, bLastHa = true;
                var sql = new SqlDb();
                while (ServiceStarted)
                {
                    var itv = interval;
                    bHo = Utils.InTradingTime(1);
                    bHa = Utils.InTradingTime(2);
                    if (!bHo && !bHa) itv = longInterval;
                    try
                    {
                        try
                        {
                            if (bLastHa && !bHa)
                            {

                                //update index to FC
                                var hnx = sql.GetHnxIndex();
                                if (hnx.Rows.Count > 0)
                                {
                                    sql.UpdateHnxIndex((DateTime)hnx.Rows[0]["TradeDate"], double.Parse(hnx.Rows[0]["PrevIndex"].ToString()), double.Parse(hnx.Rows[0]["CurrentIndex"].ToString()), double.Parse(hnx.Rows[0]["CurrentCount"].ToString()), double.Parse(hnx.Rows[0]["CurrentVolume"].ToString()), double.Parse(hnx.Rows[0]["CurrentValue"].ToString()));
                                }

                            }
                            bLastHa = bHa;
                        }
                        catch (Exception ex)
                        {
                            bLastHa = true;
                            log.WriteEntry("UpdateHNX : " + ex.ToString(), EventLogEntryType.Error);
                        }
                        try
                        {
                            if (bLastHo && !bHo)
                            {
                                //update index to FC
                                var hsx = sql.GetHsxIndex();
                                if (hsx.Rows.Count > 0)
                                {
                                    sql.UpdateHCMIndex((DateTime)hsx.Rows[0]["Date"], double.Parse(hsx.Rows[0]["PrevIndex"].ToString()), double.Parse(hsx.Rows[0]["Index1"].ToString()), double.Parse(hsx.Rows[0]["Quantity1"].ToString()), double.Parse(hsx.Rows[0]["Vol1"].ToString()), double.Parse(hsx.Rows[0]["Value1"].ToString()), double.Parse(hsx.Rows[0]["Index2"].ToString()), double.Parse(hsx.Rows[0]["Quantity2"].ToString()), double.Parse(hsx.Rows[0]["Vol2"].ToString()), double.Parse(hsx.Rows[0]["Value2"].ToString()), double.Parse(hsx.Rows[0]["Index3"].ToString()), double.Parse(hsx.Rows[0]["Quantity3"].ToString()), double.Parse(hsx.Rows[0]["Vol3"].ToString()), double.Parse(hsx.Rows[0]["Value3"].ToString()));
                                }
                            }

                            bLastHo = bHo;
                        }
                        catch (Exception ex)
                        {
                            bLastHa = true;
                            log.WriteEntry("UpdateHNX : " + ex.ToString(), EventLogEntryType.Error);
                        }
                        //log.WriteEntry("Start Price 3 : " + index, EventLogEntryType.Information);

                        UpdatePrice(1, bfirstTime);
                        UpdatePrice(2, bfirstTime);
                        UpdatePrice(9, bfirstTime);
                        UpdateKby();
                        bfirstTime = false;
                        //log.WriteEntry("Start Price 4 : " + index, EventLogEntryType.Information);

                    }
                    catch (Exception ex)
                    {
                        log.WriteEntry(ex.ToString(), EventLogEntryType.Error);
                    }
                    Thread.Sleep(itv);
                }
            }
            catch (Exception ex)
            {
                log.WriteEntry(ex.ToString(), EventLogEntryType.Error);
            }
            finally
            {
                Thread.CurrentThread.Abort();
            }
        }
        private void UpdatePrice(int centerId, bool bFirst)
        {
            try
            {
                var redisPrice = new RedisClient(ConfigRedis.PriceHost, ConfigRedis.PricePort);
                var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
                //var pdt = bFirst ? sql.GetAllPrice(centerId) : sql.GetChangedPriceSymbols(centerId);
                var sql = new SqlDb();
                var isInTrading = Utils.InTradingTime(centerId);
                var bRealtime = isInTrading;
                if (!isInTrading)
                {
                    var rlddt = sql.GetLastRealtimePriceDate(centerId);
                    var impdt = sql.GetLastImportPriceDate(centerId);
                    if (impdt.Rows.Count == 0)
                    {
                        bRealtime = rlddt.Rows.Count > 0;
                    }
                    else
                    {
                        if (rlddt.Rows.Count == 0) bRealtime = false;
                        else
                        {
                            bRealtime = int.Parse(rlddt.Rows[0][0].ToString().Replace(".", "")) > int.Parse(impdt.Rows[0][0].ToString().Replace(".", ""));
                        }
                    }
                }
                //log.WriteEntry("UpdatePrice : " + centerId + " : " + bRealtime, EventLogEntryType.Information);
                var pdt = bRealtime ? sql.GetRealtimePrice(centerId) : sql.GetPriceData(centerId);
                var allkey = string.Format(RedisKey.KeyRealTimePrice, centerId);
                if (redis.ContainsKey(allkey))
                    redis.Set(allkey, Utils.Serialize(pdt));
                else
                    redis.Add(allkey, Utils.Serialize(pdt));
                var stats = new List<int> { 0, 0, 0, 0, 0 };
                var rows = pdt.Select("Symbol<>'CENTER'");
                //log.WriteEntry("UpdatePrice : " + centerId + " : " + bRealtime + " : " + rows[0]["TradeDate"], EventLogEntryType.Information);
                foreach (var pdr in rows)
                {
                    var symbol = pdr["Symbol"].ToString();

                    try
                    {
                        #region Get Stock Price Data

                        //var cdt = sql.GetCenterId(symbol);
                        //if (cdt.Rows.Count == 0) continue;
                        //var centerId = int.Parse(pdr["centerId"].ToString());
                        //var pdt = sql.GetRealtimePriceData(symbol, centerId);
                        //if (pdt.Rows.Count == 0) continue;
                        //var pdr = pdt.Rows[0];
                        var bAvg = centerId != 1 && !isInTrading;
                        var price = redis.Get<StockPrice>(String.Format(RedisKey.PriceKey, symbol)) ?? new StockPrice();
                        price.Symbol = symbol;
                        price.LastTradeDate = (DateTime)pdr["TradeDate"];

                        price.Price = double.Parse(pdr[bAvg ? "AveragePrice" : "TradingPrice"].ToString()); price.RefPrice = double.Parse(pdr["Ref"].ToString()); price.CeilingPrice = double.Parse(pdr["Ceiling"].ToString()); price.FloorPrice = double.Parse(pdr["Floor"].ToString()); price.Volume = double.Parse(pdr["TradingVol"].ToString()); price.Value = double.Parse(pdr["TotalTradingValue"].ToString()); price.HighPrice = double.Parse(pdr["TradingPriceMax"].ToString()); price.LowPrice = double.Parse(pdr["TradingPriceMin"].ToString()); price.OpenPrice = double.Parse(pdr["OpenPrice"].ToString()); price.ClosePrice = double.Parse(pdr["TradingPrice"].ToString());
                        try
                        {
                            price.AvgPrice = double.Parse(pdr["AveragePrice"].ToString());
                            price.ClosePrice = double.Parse(pdr["TradingPrice"].ToString());
                        }
                        catch (Exception) { }
                        price.BidTotalOrder = double.Parse(pdr["TotalBidOrder"].ToString());
                        price.BidTotalVolume = double.Parse(pdr["TotalBidVolume"].ToString());
                        price.AskTotalOrder = double.Parse(pdr["TotalAskOrder"].ToString());
                        price.AskTotalVolume = double.Parse(pdr["TotalAskVolume"].ToString());

                        if (double.Parse(pdr["HasForeign"].ToString()) >= 0)
                        {
                            price.ForeignCurrentRoom = double.Parse(pdr["RemainFrRoom"].ToString());
                            price.ForeignTotalRoom = double.Parse(pdr["FrTotalRoom"].ToString());
                            price.ForeignBuyValue = double.Parse(pdr["BuyFrValue"].ToString()); price.ForeignBuyVolume = double.Parse(pdr["BuyFrVolume"].ToString()); price.ForeignSellValue = double.Parse(pdr["SellFrValue"].ToString()); price.ForeignSellVolume = double.Parse(pdr["SellFrVolume"].ToString());
                        }
                        if (bRealtime)
                        {
                            price.AskPrice01 = double.Parse(pdr["SellPrice1"].ToString());
                            price.AskPrice02 = double.Parse(pdr["SellPrice2"].ToString());
                            price.AskPrice03 = double.Parse(pdr["SellPrice3"].ToString());
                            price.AskVolume01 = double.Parse(pdr["SellVol1"].ToString());
                            price.AskVolume02 = double.Parse(pdr["SellVol2"].ToString());
                            price.AskVolume03 = double.Parse(pdr["SellVol3"].ToString());
                            price.BidPrice01 = double.Parse(pdr["BidPrice1"].ToString());
                            price.BidPrice02 = double.Parse(pdr["BidPrice2"].ToString());
                            price.BidPrice03 = double.Parse(pdr["BidPrice3"].ToString());
                            price.BidVolume01 = double.Parse(pdr["BidVol1"].ToString());
                            price.BidVolume02 = double.Parse(pdr["BidVol2"].ToString());
                            price.BidVolume03 = double.Parse(pdr["BidVol3"].ToString());
                        }

                        #endregion

                        #region Update Center Stats
                        var key = string.Format(RedisKey.PriceKey, symbol);
                        if (redis.ContainsKey(key))
                            redis.Set<StockPrice>(key, price);
                        else
                            redis.Add<StockPrice>(key, price);
                        if (redisPrice.ContainsKey(key))
                            redisPrice.Set<StockPrice>(key, price);
                        else
                            redisPrice.Add<StockPrice>(key, price);

                        if (price.Price <= 0) continue;
                        if (Math.Round(price.Price, 2) <= Math.Round(price.FloorPrice, 2)) stats[0]++;
                        else if (Math.Round(price.Price, 2) < Math.Round(price.RefPrice, 2)) stats[1]++;
                        else if (Math.Round(price.Price, 2) >= Math.Round(price.CeilingPrice, 2)) stats[4]++;
                        else if (Math.Round(price.Price, 2) > Math.Round(price.RefPrice, 2)) stats[3]++;
                        else stats[2]++;
                        #endregion

                    }
                    catch (Exception ex)
                    {
                        log.WriteEntry("Price " + symbol + ":" + ex.ToString(), EventLogEntryType.Information);
                    }


                }

                #region Center stats
                rows = pdt.Select("Symbol='CENTER'");
                if (rows.Length == 0) return;
                //update center stats
                var centerkey = string.Format(RedisKey.KeyCenterIndex, centerId);
                var center = redis.ContainsKey(centerkey) ? redis.Get<TradeCenterStats>(centerkey) : new TradeCenterStats() { TradeCenterId = centerId };
                center.Ceiling = stats[4];
                center.Up = stats[3];
                center.Normal = stats[2];
                center.Down = stats[1];
                center.Floor = stats[0];

                center.CurrentDate = (DateTime)rows[0]["TradeDate"];
                //log.WriteEntry("UpdatePrice : " + centerId + " : Center : " + rows[0]["TradeDate"], EventLogEntryType.Information);
                center.CurrentIndex = double.Parse(rows[0]["TradingPrice"].ToString());
                center.PrevIndex = double.Parse(rows[0]["Ref"].ToString());
                center.CurrentVolume = double.Parse(rows[0]["TotalTradingVolume"].ToString());
                center.CurrentValue = double.Parse(rows[0]["TotalTradingValue"].ToString());

                center.ForeignBuyVolume = double.Parse(rows[0]["BuyFrVolume"].ToString());
                center.ForeignBuyValue = double.Parse(rows[0]["BuyFrValue"].ToString());
                center.ForeignSellVolume = double.Parse(rows[0]["SellFrVolume"].ToString());
                center.ForeignSellValue = double.Parse(rows[0]["SellFrValue"].ToString());
                //chỉ số theo đợt
                if (centerId == 1)
                {
                    center.Index1 = double.Parse(rows[0]["BidPrice1"].ToString());
                    center.Volume1 = double.Parse(rows[0]["BidVol1"].ToString());
                    center.Value1 = double.Parse(rows[0]["BidPrice2"].ToString());
                    center.Index2 = double.Parse(rows[0]["BidVol2"].ToString());
                    center.Volume2 = double.Parse(rows[0]["BidPrice3"].ToString());
                    center.Value2 = double.Parse(rows[0]["BidVol3"].ToString());
                    center.Index3 = double.Parse(rows[0]["SellPrice1"].ToString());
                    center.Volume3 = double.Parse(rows[0]["SellVol1"].ToString());
                    center.Value3 = double.Parse(rows[0]["SellPrice2"].ToString());
                }
                if (!bRealtime) { center.ChartFolder = rows[0]["SecName"].ToString(); }
                if (redis.ContainsKey(centerkey))
                    redis.Set(centerkey, center);
                else
                    redis.Add(centerkey, center);
                #endregion
            }
            catch (Exception ex)
            {
                log.WriteEntry("PriceData - " + centerId + ": " + ex.ToString(), EventLogEntryType.Error);
            }
        }
        private void UpdateKby()
        {
            try
            {
                var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
                //var pdt = bFirst ? sql.GetAllPrice(centerId) : sql.GetChangedPriceSymbols(centerId);
                var sql = new SqlDb();
                var dt = sql.GetKbyFolder();
                if (redis.ContainsKey(RedisKey.KeyKby))
                    redis.Set(RedisKey.KeyKby, dt);
                else
                    redis.Add(RedisKey.KeyKby, dt);
            }
            catch (Exception ex)
            {
                log.WriteEntry("UpdateKby : " + ex.ToString(), EventLogEntryType.Error);
            }
        }
        public void UpdateTopPrice()
        {
            try
            {
                var bfirstTime = true;
                if ((ConfigurationManager.AppSettings["PriceAllowance"] ?? "") != "TRUE")
                {
                    //Thread.CurrentThread.Abort();
                    return;
                }
                var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
                while (ServiceStarted)
                {
                    try
                    {
                        if (!bfirstTime)
                        {
                            var itv = interval;
                            if (!Utils.InTradingTime(1) && !Utils.InTradingTime(2)) itv = longInterval;
                            Thread.Sleep(itv);
                        }
                        bfirstTime = false;
                        /*----- hnx -----*/
                        var hnxkey = string.Format(RedisKey.KeyRealTimePrice, 2);
                        var hnx = ConvertPrice(Utils.Deserialize(redis.Get<string>(hnxkey))) ?? new DataTable();
                        var hnxpu = new List<TopStock>();
                        var hnxpd = new List<TopStock>();
                        var hnxvd = new List<TopStock>();
                        if (hnx.Rows.Count > 0)
                        {
                            var bAvg = !Utils.InTradingTime(2);
                            var hrs = hnx.Select("Symbol <> 'CENTER'", (bAvg ? "DoubleAvgChange" : "DoublePriceChange") + " DESC");
                            var key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.Ha, RedisKey.KeyTopStockType.PriceUp);
                            //var hnxpu = new List<TopStock>();
                            for (var i = 0; i < 10; i++)
                            {
                                if (hrs.Length <= i || double.Parse(hrs[i][(bAvg ? "DoubleAvgChange" : "DoublePriceChange")].ToString()) <= 0) break;
                                hnxpu.Add(new TopStock() { Symbol = hrs[i]["Symbol"].ToString(), BasicPrice = double.Parse(hrs[i]["Ref"].ToString()), Price = double.Parse(hrs[i][(bAvg ? "AveragePrice" : "TradingPrice")].ToString()), Volume = double.Parse(hrs[i]["TradingVol"].ToString()) });
                            }
                            if (redis.ContainsKey(key))
                                redis.Set(key, hnxpu);
                            else
                                redis.Add(key, hnxpu);
                            /****************/
                            hrs = hnx.Select("Symbol <> 'CENTER'", (bAvg ? "DoubleAvgChange" : "DoublePriceChange") + " ASC, Symbol");
                            key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.Ha, RedisKey.KeyTopStockType.PriceDown);
                            //var hnxpd = new List<TopStock>();
                            for (var i = 0; i < 10; i++)
                            {
                                if (hrs.Length <= i || double.Parse(hrs[i][(bAvg ? "DoubleAvgChange" : "DoublePriceChange")].ToString()) >= 0) break;
                                hnxpd.Add(new TopStock() { Symbol = hrs[i]["Symbol"].ToString(), BasicPrice = double.Parse(hrs[i]["Ref"].ToString()), Price = double.Parse(hrs[i][(bAvg ? "AveragePrice" : "TradingPrice")].ToString()), Volume = double.Parse(hrs[i]["TradingVol"].ToString()) });
                            }
                            if (redis.ContainsKey(key))
                                redis.Set(key, hnxpd);
                            else
                                redis.Add(key, hnxpd);
                            /****************/
                            hrs = hnx.Select("Symbol <> 'CENTER'", "DoubleVolume DESC, Symbol");
                            key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.Ha, RedisKey.KeyTopStockType.VolumeDown);
                            //var hnxvd = new List<TopStock>();
                            for (var i = 0; i < 10; i++)
                            {
                                if (hrs.Length <= i || double.Parse(hrs[i]["TradingVol"].ToString()) <= 0) break;
                                hnxvd.Add(new TopStock() { Symbol = hrs[i]["Symbol"].ToString(), BasicPrice = double.Parse(hrs[i]["Ref"].ToString()), Price = double.Parse(hrs[i][(bAvg ? "AveragePrice" : "TradingPrice")].ToString()), Volume = double.Parse(hrs[i]["TradingVol"].ToString()) });
                            }
                            if (redis.ContainsKey(key))
                                redis.Set(key, hnxvd);
                            else
                                redis.Add(key, hnxvd);
                        }
                        /*--------------*/
                        /*----- hsx ----*/
                        var hsxkey = string.Format(RedisKey.KeyRealTimePrice, 1);
                        var hsx = ConvertPrice(Utils.Deserialize(redis.Get<string>(hsxkey))) ?? new DataTable();
                        var hsxpu = new List<TopStock>();
                        var hsxpd = new List<TopStock>();
                        var hsxvd = new List<TopStock>();
                        if (hsx.Rows.Count > 0)
                        {
                            var hrs = hsx.Select("Symbol <> 'CENTER'", "DoublePriceChange DESC, Symbol");
                            var key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.Ho, RedisKey.KeyTopStockType.PriceUp);
                            //var hsxpu = new List<TopStock>();
                            for (var i = 0; i < 10; i++)
                            {
                                if (hrs.Length <= i || double.Parse(hrs[i]["DoublePriceChange"].ToString()) <= 0) break;
                                hsxpu.Add(new TopStock() { Symbol = hrs[i]["Symbol"].ToString(), BasicPrice = double.Parse(hrs[i]["Ref"].ToString()), Price = double.Parse(hrs[i]["TradingPrice"].ToString()), Volume = double.Parse(hrs[i]["TradingVol"].ToString()) });
                            }
                            if (redis.ContainsKey(key))
                                redis.Set(key, hsxpu);
                            else
                                redis.Add(key, hsxpu);
                            /****************/
                            hrs = hsx.Select("Symbol <> 'CENTER'", "DoublePriceChange, Symbol");
                            key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.Ho, RedisKey.KeyTopStockType.PriceDown);
                            //var hsxpd = new List<TopStock>();
                            for (var i = 0; i < 10; i++)
                            {
                                if (hrs.Length <= i || double.Parse(hrs[i]["DoublePriceChange"].ToString()) >= 0) break;
                                hsxpd.Add(new TopStock() { Symbol = hrs[i]["Symbol"].ToString(), BasicPrice = double.Parse(hrs[i]["Ref"].ToString()), Price = double.Parse(hrs[i]["TradingPrice"].ToString()), Volume = double.Parse(hrs[i]["TradingVol"].ToString()) });
                            }
                            if (redis.ContainsKey(key))
                                redis.Set(key, hsxpd);
                            else
                                redis.Add(key, hsxpd);
                            /****************/
                            hrs = hsx.Select("Symbol <> 'CENTER'", "DoubleVolume DESC, Symbol");
                            key = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.Ho, RedisKey.KeyTopStockType.VolumeDown);
                            //var hsxvd = new List<TopStock>();
                            for (var i = 0; i < 10; i++)
                            {
                                if (hrs.Length <= i || double.Parse(hrs[i]["DoubleVolume"].ToString()) <= 0) break;
                                hsxvd.Add(new TopStock() { Symbol = hrs[i]["Symbol"].ToString(), BasicPrice = double.Parse(hrs[i]["Ref"].ToString()), Price = double.Parse(hrs[i]["TradingPrice"].ToString()), Volume = double.Parse(hrs[i]["TradingVol"].ToString()) });
                            }
                            if (redis.ContainsKey(key))
                                redis.Set(key, hsxvd);
                            else
                                redis.Add(key, hsxvd);
                        }
                        /*--------------*/
                        /*----- all ----*/
                        var all = MergeList(hnxpu, hsxpu, RedisKey.KeyTopStockType.PriceUp);
                        var allkey = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.All, RedisKey.KeyTopStockType.PriceUp);
                        if (redis.ContainsKey(allkey))
                            redis.Set(allkey, all);
                        else
                            redis.Add(allkey, all);
                        all = MergeList(hnxpd, hsxpd, RedisKey.KeyTopStockType.PriceDown);
                        allkey = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.All, RedisKey.KeyTopStockType.PriceDown);
                        if (redis.ContainsKey(allkey))
                            redis.Set(allkey, all);
                        else
                            redis.Add(allkey, all);
                        all = MergeList(hnxvd, hsxvd, RedisKey.KeyTopStockType.VolumeDown);
                        allkey = string.Format(RedisKey.KeyTopStock, RedisKey.KeyTopStockCenter.All, RedisKey.KeyTopStockType.VolumeDown);
                        if (redis.ContainsKey(allkey))
                            redis.Set(allkey, all);
                        else
                            redis.Add(allkey, all);
                        /*--------------*/
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
            finally
            {
                //Thread.CurrentThread.Abort();
            }
        }
        private static DataTable ConvertPrice(DataTable hnx)
        {
            hnx.Columns.Add(new DataColumn("DoubleAvgChange", typeof(double), "Convert(AverageChange, 'System.Double')"));
            hnx.Columns.Add(new DataColumn("DoublePriceChange", typeof(double), "Convert(PriceChange, 'System.Double')"));
            hnx.Columns.Add(new DataColumn("DoubleVolume", typeof(double), "Convert(TradingVol, 'System.Double')"));
            hnx.AcceptChanges();
            return hnx;
        }
        private static List<TopStock> MergeList(List<TopStock> hnx, List<TopStock> hsx, string type)
        {
            var ret = hnx;
            ret.InsertRange(0, hsx);
            switch (type)
            {
                case RedisKey.KeyTopStockType.PriceUp:
                    ret.Sort("ChangePrice DESC"); break;
                case RedisKey.KeyTopStockType.PriceDown:
                    ret.Sort("ChangePrice ASC"); break;
                case RedisKey.KeyTopStockType.VolumeDown:
                    ret.Sort("Volume DESC"); break;
            }
            while (ret.Count > 10) ret.RemoveAt(10);
            return ret;
        }
        public void UpdateBoxHangHoa()
        {
            try
            {
                if ((ConfigurationManager.AppSettings["ProductBoxAllowance"] ?? "") != "TRUE")
                {
                    return;
                }
                var tabVN = new List<string>() { ChiTieuCrawler.TabVietnam.VangTheGioi, ChiTieuCrawler.TabVietnam.VangSJC, ChiTieuCrawler.TabVietnam.USDSIN, ChiTieuCrawler.TabVietnam.USDHKD, ChiTieuCrawler.TabVietnam.CNY, ChiTieuCrawler.TabVietnam.BangAnh, ChiTieuCrawler.TabVietnam.USDVCB, ChiTieuCrawler.TabVietnam.EURVCB }; /*, ChiTieuCrawler.TabVietnam.USDtudo, ChiTieuCrawler.TabVietnam.EURtudo*/
                var tabTG = new List<string>() { ChiTieuCrawler.TabTheGioi.USDIndex, ChiTieuCrawler.TabTheGioi.DowJones, ChiTieuCrawler.TabTheGioi.Nasdaq, ChiTieuCrawler.TabTheGioi.SP500, ChiTieuCrawler.TabTheGioi.FTSE100, ChiTieuCrawler.TabTheGioi.DAX, ChiTieuCrawler.TabTheGioi.Nikkei225, ChiTieuCrawler.TabTheGioi.HangSeng, ChiTieuCrawler.TabTheGioi.StraitTimes };
                var tabHH = new List<string>() { ChiTieuCrawler.TabHangHoa.CrudeOil, ChiTieuCrawler.TabHangHoa.NaturalGas, ChiTieuCrawler.TabHangHoa.Gold, ChiTieuCrawler.TabHangHoa.Copper, ChiTieuCrawler.TabHangHoa.Silver, ChiTieuCrawler.TabHangHoa.Corn, ChiTieuCrawler.TabHangHoa.Sugar, ChiTieuCrawler.TabHangHoa.Coffee, ChiTieuCrawler.TabHangHoa.Cotton, ChiTieuCrawler.TabHangHoa.RoughRice, ChiTieuCrawler.TabHangHoa.Wheat, ChiTieuCrawler.TabHangHoa.Soybean, ChiTieuCrawler.TabHangHoa.Ethanol };
                var tabMobile = new List<string>() { ChiTieuCrawler.TabMobile.VangSJC, ChiTieuCrawler.TabMobile.USDVCB, ChiTieuCrawler.TabMobile.VangTD, ChiTieuCrawler.TabMobile.USDTD };
                var bFirstTime = true;
                while (ServiceStarted)
                {
                    try
                    {
                        var dt = SqlDb.GetCrawlerData();
                        var manual = SqlDb.GetManualProductData();
                        var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);

                        #region Tab Viet Nam
                        //tab vietnam
                        var key = string.Format(RedisKey.KeyProductBox, 1);
                        var ls = redis.ContainsKey(key) ? redis.Get<List<ProductBox>>(key) : new List<ProductBox>();
                        var data = new List<ProductBox>();
                        ProductBox box, newbox, otherbox;
                        foreach (var item in tabVN)
                        {
                            if (int.Parse(item) < 0)
                            {
                                newbox = otherbox = null;
                                box = FindBox(item, ls);
                                otherbox = null;
                                if (manual.Rows.Count > 0)
                                {
                                    switch (item)
                                    {
                                        case ChiTieuCrawler.TabVietnam.EURtudo:
                                            if (double.Parse(manual.Rows[0]["Price_EURO"].ToString()) == 0) continue;
                                            otherbox = new ProductBox() { ProductName = "EUR (tự do)", CurrentPrice = double.Parse(manual.Rows[0]["Price_EURO"].ToString()), OtherPrice = double.Parse(manual.Rows[0]["Price_Euro_Sale"].ToString()), PrevPrice = 0, UpdateDate = DateTime.Now, DbId = "-2" };
                                            break;
                                        case ChiTieuCrawler.TabVietnam.USDtudo:
                                            if (double.Parse(manual.Rows[0]["Price_USD"].ToString()) == 0) continue;
                                            otherbox = new ProductBox() { ProductName = "USD (tự do)", CurrentPrice = double.Parse(manual.Rows[0]["Price_USD"].ToString()), OtherPrice = double.Parse(manual.Rows[0]["Price_USD_Sale"].ToString()), PrevPrice = 0, UpdateDate = DateTime.Now, DbId = "-1" };
                                            break;
                                    }
                                }
                                if (otherbox != null)
                                {
                                    otherbox.UpdatePrevPrice(bFirstTime ? null : box);
                                    data.Add(otherbox);
                                }
                                continue;
                            }
                            var drs = dt.Select("ID=" + item);
                            if (drs.Length == 0) continue;
                            var dr = drs[0];
                            try
                            {
                                newbox = otherbox = null;
                                box = FindBox(dr["ID"].ToString(), ls);
                                switch (dr["ID"].ToString())
                                {
                                    case ChiTieuCrawler.TabVietnam.VangTheGioi:
                                        newbox = new ProductBox() { ProductName = "Vàng TG(USD)", CurrentPrice = double.Parse(dr["MuaVao"].ToString()), OtherPrice = 0, PrevPrice = double.Parse(dr["MuaVao"].ToString()) - double.Parse(dr["ThayDoi"].ToString()), UpdateDate = DateTime.Now, DbId = box.DbId };
                                        break;
                                    case ChiTieuCrawler.TabVietnam.VangSJC:
                                        newbox = new ProductBox() { ProductName = "Vàng SJC", CurrentPrice = double.Parse(dr["MuaVao"].ToString().Replace(",", "")), OtherPrice = double.Parse(dr["BanRa"].ToString().Replace(",", "")), PrevPrice = 0, UpdateDate = DateTime.Now, DbId = box.DbId };
                                        break;
                                    case ChiTieuCrawler.TabVietnam.USDVCB:
                                        newbox = new ProductBox() { ProductName = "USD (VCB)", CurrentPrice = double.Parse(dr["MuaVao"].ToString().Replace(",", "")), OtherPrice = double.Parse(dr["BanRa"].ToString().Replace(",", "")), PrevPrice = 0, UpdateDate = DateTime.Now, DbId = box.DbId };
                                        break;
                                    case ChiTieuCrawler.TabVietnam.EURVCB:
                                        newbox = new ProductBox() { ProductName = "EUR (VCB)", CurrentPrice = double.Parse(dr["MuaVao"].ToString().Replace(",", "")), OtherPrice = double.Parse(dr["BanRa"].ToString().Replace(",", "")), PrevPrice = 0, UpdateDate = DateTime.Now, DbId = box.DbId };
                                        break;
                                    case ChiTieuCrawler.TabVietnam.CNY:
                                        newbox = new ProductBox() { ProductName = "CNY", CurrentPrice = double.Parse(dr["MuaVao"].ToString().Replace(",", "")), OtherPrice = double.Parse(dr["BanRa"].ToString().Replace(",", "")), PrevPrice = 0, UpdateDate = DateTime.Now, DbId = box.DbId };
                                        break;
                                    case ChiTieuCrawler.TabVietnam.USDSIN:
                                        newbox = new ProductBox() { ProductName = "SGD", CurrentPrice = double.Parse(dr["MuaVao"].ToString().Replace(",", "")), OtherPrice = double.Parse(dr["BanRa"].ToString().Replace(",", "")), PrevPrice = 0, UpdateDate = DateTime.Now, DbId = box.DbId };
                                        break;
                                    case ChiTieuCrawler.TabVietnam.USDHKD:
                                        newbox = new ProductBox() { ProductName = "HKD", CurrentPrice = double.Parse(dr["MuaVao"].ToString().Replace(",", "")), OtherPrice = double.Parse(dr["BanRa"].ToString().Replace(",", "")), PrevPrice = 0, UpdateDate = DateTime.Now, DbId = box.DbId };
                                        break;
                                    case ChiTieuCrawler.TabVietnam.BangAnh:
                                        newbox = new ProductBox() { ProductName = "Bảng Anh", CurrentPrice = double.Parse(dr["MuaVao"].ToString().Replace(",", "")), OtherPrice = double.Parse(dr["BanRa"].ToString().Replace(",", "")), PrevPrice = 0, UpdateDate = DateTime.Now, DbId = box.DbId };
                                        break;
                                    default:
                                        newbox = null;
                                        break;
                                }
                                if (newbox != null)
                                {
                                    if (newbox.DbId != ChiTieuCrawler.TabVietnam.VangTheGioi) newbox.UpdatePrevPrice(bFirstTime ? null : box);
                                    data.Add(newbox);
                                }

                            }
                            catch (Exception ex) { log.WriteEntry("GetBox : " + dr["ID"] + ":" + ex.ToString(), EventLogEntryType.Error); }
                        }
                        if (data.Count > 0)
                        {
                            if (redis.ContainsKey(key))
                                redis.Set(key, data);
                            else
                                redis.Add(key, data);
                        }
                        #endregion

                        #region Tab The gioi
                        key = string.Format(RedisKey.KeyProductBox, 2);
                        data = new List<ProductBox>();
                        double d;
                        foreach (var item in tabTG)
                        {
                            var drs = dt.Select("ID=" + item);
                            if (drs.Length == 0) continue;
                            var dr = drs[0];
                            try
                            {
                                box = FindBox(dr["ID"].ToString(), ls);
                                switch (dr["ID"].ToString())
                                {
                                    case ChiTieuCrawler.TabTheGioi.USDIndex:
                                        newbox = new ProductBox() { ProductName = "US Dollar Index", CurrentPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")) - double.Parse(dr["ThayDoi"].ToString().Replace(",", "")), UpdateDate = DateTime.Now, DbId = box.DbId };
                                        break;
                                    case ChiTieuCrawler.TabTheGioi.DowJones:
                                        newbox = new ProductBox() { ProductName = "DowJones", CurrentPrice = double.Parse(dr["Index"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), UpdateDate = DateTime.Now, DbId = box.DbId };
                                        if (newbox.CurrentPrice == newbox.PrevPrice)
                                        {
                                            newbox.PrevPrice = double.Parse(dr["ThayDoi"].ToString());
                                        }
                                        break;
                                    case ChiTieuCrawler.TabTheGioi.Nasdaq:
                                        newbox = new ProductBox() { ProductName = "Nasdaq", CurrentPrice = double.Parse(dr["Index"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), UpdateDate = DateTime.Now, DbId = box.DbId };
                                        if (newbox.CurrentPrice == newbox.PrevPrice && double.TryParse(dr["ThayDoi"].ToString(), out d))
                                        {
                                            newbox.PrevPrice = double.Parse(dr["ThayDoi"].ToString());
                                        }
                                        break;
                                    case ChiTieuCrawler.TabTheGioi.SP500:
                                        newbox = new ProductBox() { ProductName = "S&P 500", CurrentPrice = double.Parse(dr["Index"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), UpdateDate = DateTime.Now, DbId = box.DbId };
                                        if (newbox.CurrentPrice == newbox.PrevPrice)
                                        {
                                            newbox.PrevPrice = double.Parse(dr["ThayDoi"].ToString());
                                        }
                                        break;
                                    case ChiTieuCrawler.TabTheGioi.FTSE100:
                                        newbox = new ProductBox() { ProductName = "FTSE 100", CurrentPrice = double.Parse(dr["Index"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), UpdateDate = DateTime.Now, DbId = box.DbId };
                                        if (newbox.CurrentPrice == newbox.PrevPrice)
                                        {
                                            newbox.PrevPrice = double.Parse(dr["ThayDoi"].ToString());
                                        }
                                        break;
                                    case ChiTieuCrawler.TabTheGioi.DAX:
                                        newbox = new ProductBox() { ProductName = "DAX", CurrentPrice = double.Parse(dr["Index"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), UpdateDate = DateTime.Now, DbId = box.DbId };
                                        if (newbox.CurrentPrice == newbox.PrevPrice)
                                        {
                                            newbox.PrevPrice = double.Parse(dr["ThayDoi"].ToString());
                                        }
                                        break;
                                    case ChiTieuCrawler.TabTheGioi.Nikkei225:
                                        newbox = new ProductBox() { ProductName = "Nikkei 225", CurrentPrice = double.Parse(dr["Index"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), UpdateDate = DateTime.Now, DbId = box.DbId };
                                        if (newbox.CurrentPrice == newbox.PrevPrice)
                                        {
                                            newbox.PrevPrice = double.Parse(dr["ThayDoi"].ToString());
                                        }
                                        break;
                                    case ChiTieuCrawler.TabTheGioi.HangSeng:
                                        newbox = new ProductBox() { ProductName = "Hang Seng", CurrentPrice = double.Parse(dr["Index"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), UpdateDate = DateTime.Now, DbId = box.DbId };
                                        if (newbox.CurrentPrice == newbox.PrevPrice)
                                        {
                                            newbox.PrevPrice = double.Parse(dr["ThayDoi"].ToString());
                                        }
                                        break;
                                    case ChiTieuCrawler.TabTheGioi.StraitTimes:
                                        newbox = new ProductBox() { ProductName = "Strait Times", CurrentPrice = double.Parse(dr["Index"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), UpdateDate = DateTime.Now, DbId = box.DbId };
                                        if (newbox.CurrentPrice == newbox.PrevPrice)
                                        {
                                            newbox.PrevPrice = double.Parse(dr["ThayDoi"].ToString());
                                        }
                                        break;
                                    default:
                                        newbox = null;
                                        break;
                                }
                                if (newbox != null)
                                {
                                    data.Add(newbox);
                                }
                            }
                            catch (Exception ex) { log.WriteEntry("GetBox : " + dr["ID"] + ":" + ex.ToString(), EventLogEntryType.Error); }
                        }
                        if (data.Count > 0)
                        {
                            if (redis.ContainsKey(key))
                                redis.Set(key, data);
                            else
                                redis.Add(key, data);
                        }
                        #endregion

                        #region Tab Hàng hóa
                        key = string.Format(RedisKey.KeyProductBox, 3);
                        data = new List<ProductBox>();
                        foreach (var item in tabHH)
                        {
                            if (item == "-1")
                            {
                                switch (item)
                                {

                                }
                                continue;
                            }
                            var drs = dt.Select("ID=" + item);
                            if (drs.Length == 0) continue;
                            var dr = drs[0];
                            try
                            {
                                box = FindBox(dr["ID"].ToString(), ls);
                                switch (dr["ID"].ToString())
                                {
                                    case ChiTieuCrawler.TabHangHoa.CrudeOil:
                                        newbox = new ProductBox() { ProductName = "Crude Oil", CurrentPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")) - double.Parse(dr["ThayDoi"].ToString()), UpdateDate = DateTime.Now, DbId = box.DbId };
                                        break;
                                    case ChiTieuCrawler.TabHangHoa.NaturalGas:
                                        newbox = new ProductBox() { ProductName = "Natural Gas", CurrentPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")) - double.Parse(dr["ThayDoi"].ToString()), UpdateDate = DateTime.Now, DbId = box.DbId };
                                        break;
                                    case ChiTieuCrawler.TabHangHoa.Gold:
                                        newbox = new ProductBox() { ProductName = "Gold", CurrentPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")) - double.Parse(dr["ThayDoi"].ToString()), UpdateDate = DateTime.Now, DbId = box.DbId };
                                        break;
                                    case ChiTieuCrawler.TabHangHoa.Copper:
                                        newbox = new ProductBox() { ProductName = "Copper", CurrentPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")) - double.Parse(dr["ThayDoi"].ToString()), UpdateDate = DateTime.Now, DbId = box.DbId };
                                        break;
                                    case ChiTieuCrawler.TabHangHoa.Silver:
                                        newbox = new ProductBox() { ProductName = "Silver", CurrentPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")) - double.Parse(dr["ThayDoi"].ToString()), UpdateDate = DateTime.Now, DbId = box.DbId };
                                        break;
                                    case ChiTieuCrawler.TabHangHoa.Corn:
                                        newbox = new ProductBox() { ProductName = "Corn", CurrentPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")) - double.Parse(dr["ThayDoi"].ToString()), UpdateDate = DateTime.Now, DbId = box.DbId };
                                        break;
                                    case ChiTieuCrawler.TabHangHoa.Sugar:
                                        newbox = new ProductBox() { ProductName = "Sugar", CurrentPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")) - double.Parse(dr["ThayDoi"].ToString()), UpdateDate = DateTime.Now, DbId = box.DbId };
                                        break;
                                    case ChiTieuCrawler.TabHangHoa.Coffee:
                                        newbox = new ProductBox() { ProductName = "Coffee", CurrentPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")) - double.Parse(dr["ThayDoi"].ToString()), UpdateDate = DateTime.Now, DbId = box.DbId };
                                        break;
                                    case ChiTieuCrawler.TabHangHoa.Cotton:
                                        newbox = new ProductBox() { ProductName = "Cotton", CurrentPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")) - double.Parse(dr["ThayDoi"].ToString()), UpdateDate = DateTime.Now, DbId = box.DbId };
                                        break;
                                    case ChiTieuCrawler.TabHangHoa.RoughRice:
                                        newbox = new ProductBox() { ProductName = "Rough rice", CurrentPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")) - double.Parse(dr["ThayDoi"].ToString()), UpdateDate = DateTime.Now, DbId = box.DbId };
                                        break;
                                    case ChiTieuCrawler.TabHangHoa.Wheat:
                                        newbox = new ProductBox() { ProductName = "Wheat", CurrentPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")) - double.Parse(dr["ThayDoi"].ToString()), UpdateDate = DateTime.Now, DbId = box.DbId };
                                        break;
                                    case ChiTieuCrawler.TabHangHoa.Soybean:
                                        newbox = new ProductBox() { ProductName = "Soybean", CurrentPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")) - double.Parse(dr["ThayDoi"].ToString()), UpdateDate = DateTime.Now, DbId = box.DbId };
                                        break;
                                    case ChiTieuCrawler.TabHangHoa.Ethanol:
                                        newbox = new ProductBox() { ProductName = "Ethanol", CurrentPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")), OtherPrice = 0, PrevPrice = double.Parse(dr["Gia"].ToString().Replace(",", "")) - double.Parse(dr["ThayDoi"].ToString()), UpdateDate = DateTime.Now, DbId = box.DbId };
                                        break;
                                    default:
                                        newbox = null;
                                        break;
                                }
                                if (newbox != null)
                                {
                                    data.Add(newbox);
                                }
                            }
                            catch (Exception ex) { log.WriteEntry("GetBox : " + dr["ID"] + ":" + ex.ToString(), EventLogEntryType.Error); }
                        }
                        if (data.Count > 0)
                        {
                            if (redis.ContainsKey(key))
                                redis.Set(key, data);
                            else
                                redis.Add(key, data);
                        }
                        #endregion

                        #region Tab Mobile
                        key = string.Format(RedisKey.KeyProductBox, 4);
                        data = new List<ProductBox>();
                        foreach (var item in tabMobile)
                        {
                            newbox = null;
                            if (double.Parse(item) < 0)
                            {
                                double price;
                                //manual
                                switch (item)
                                {
                                    case ChiTieuCrawler.TabMobile.USDTD:
                                        price = manual.Rows.Count > 0 ? double.Parse(manual.Rows[0]["Price_USD"].ToString()) : 0;
                                        newbox = new ProductBox() { ProductName = "USDTD", CurrentPrice = price };
                                        break;
                                    case ChiTieuCrawler.TabMobile.VangTD:
                                        price = manual.Rows.Count > 0 ? double.Parse(manual.Rows[0]["Price_Gold"].ToString()) : 0;
                                        newbox = new ProductBox() { ProductName = "VangTD", CurrentPrice = price };
                                        break;
                                }
                                if (newbox != null)
                                {
                                    data.Add(newbox);
                                }
                                continue;
                            }
                            var drs = dt.Select("ID=" + item);
                            if (drs.Length == 0) continue;
                            var dr = drs[0];
                            try
                            {
                                box = FindBox(dr["ID"].ToString(), ls);
                                switch (dr["ID"].ToString())
                                {
                                    case ChiTieuCrawler.TabMobile.VangSJC:
                                        newbox = new ProductBox() { ProductName = "Vàng SJC", CurrentPrice = double.Parse(dr["MuaVao"].ToString().Replace(",", "")), OtherPrice = double.Parse(dr["BanRa"].ToString().Replace(",", "")), PrevPrice = 0, UpdateDate = DateTime.Now, DbId = box.DbId };
                                        break;
                                    case ChiTieuCrawler.TabMobile.USDVCB:
                                        newbox = new ProductBox() { ProductName = "USD (VCB)", CurrentPrice = double.Parse(dr["MuaVao"].ToString().Replace(",", "")), OtherPrice = double.Parse(dr["BanRa"].ToString().Replace(",", "")), PrevPrice = 0, UpdateDate = DateTime.Now, DbId = box.DbId };
                                        break;
                                }
                                if (newbox != null)
                                {
                                    data.Add(newbox);
                                }
                            }
                            catch (Exception ex) { log.WriteEntry("GetBox : " + dr["ID"] + ":" + ex.ToString(), EventLogEntryType.Error); }
                        }
                        if (data.Count > 0)
                        {
                            if (redis.ContainsKey(key))
                                redis.Set(key, data);
                            else
                                redis.Add(key, data);
                        }
                        #endregion

                        bFirstTime = false;
                    }
                    catch (Exception ex)
                    {
                        log.WriteEntry(ex.ToString(), EventLogEntryType.Error);
                    }
                    Thread.Sleep(crawlerInterval);
                }
            }
            catch (Exception ex)
            {
                log.WriteEntry(ex.ToString(), EventLogEntryType.Error);
            }
        }
        internal class ChiTieuCrawler
        {
            public class TabVietnam
            {
                public const string VangTheGioi = "18";
                public const string VangSJC = "30";
                public const string USDVCB = "24";
                public const string EURVCB = "29";
                public const string CNY = "22";
                public const string USDSIN = "1";
                public const string USDHKD = "2";
                public const string BangAnh = "20";
                public const string USDtudo = "-1";
                public const string EURtudo = "-2";
            }
            public class TabTheGioi
            {
                public const string DowJones = "16";
                public const string Nasdaq = "14";
                public const string SP500 = "10";
                public const string FTSE100 = "12";
                public const string DAX = "3";
                public const string Nikkei225 = "5";
                public const string HangSeng = "6";
                public const string StraitTimes = "9";
                public const string USDIndex = "28";
            }
            public class TabHangHoa
            {
                public const string CrudeOil = "7";
                public const string NaturalGas = "8";
                public const string Gold = "4";
                public const string Copper = "11";
                public const string Silver = "13";
                public const string Corn = "15";
                public const string Sugar = "17";
                public const string Coffee = "19";
                public const string Cotton = "21";
                public const string RoughRice = "23";
                public const string Wheat = "25";
                public const string Soybean = "26";
                public const string Ethanol = "27";
            }
            public class TabMobile
            {
                public const string VangSJC = "30";
                public const string USDVCB = "24";
                public const string VangTD = "-1";
                public const string USDTD = "-2";
            }
        }
        private static ProductBox FindBox(string id, List<ProductBox> ls)
        {
            foreach (var box in ls)
            {
                if (box.DbId == id) return box;
            }
            return new ProductBox() { DbId = id };
        }
        public void UpdateFundTransaction()
        {
            try
            {
                var bfirstTime = true;
                if ((ConfigurationManager.AppSettings["PriceAllowance"] ?? "") != "TRUE")
                {
                    //Thread.CurrentThread.Abort();
                    return;
                }
                var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
                while (ServiceStarted)
                {
                    try
                    {
                        #region "Fund Transaction"
                        var fndt = SqlDb.GetFundUpdate();

                        var fndkeylist = "";
                        var symbol = "";
                        var fndls = new List<string>();
                        foreach (DataRow fndr in fndt.Rows)
                        {
                            if (symbol != fndr["Symbol"].ToString().ToUpper())
                            {
                                if (fndls.Count > 0)
                                {
                                    fndls.Sort();
                                    fndls.Reverse();
                                    if (redis.ContainsKey(fndkeylist))
                                        redis.Set<List<string>>(fndkeylist, fndls);
                                    else
                                        redis.Add<List<string>>(fndkeylist, fndls);
                                }
                                symbol = fndr["Symbol"].ToString().ToUpper();
                                fndkeylist = string.Format(RedisKey.FundHistoryKeys, symbol);
                                fndls = new List<string>();
                            }
                            if (symbol == "") continue;
                            var key = string.Format(RedisKey.FundHistory, symbol, ((DateTime)fndr["TradingDate"]).ToString("yyyyMMdd"));
                            var order = new FundHistory() { Symbol = symbol, TradeDate = (DateTime)fndr["TradingDate"], TransactionType = fndr["Buy_Sale"].ToString() == "s" ? "Bán" : "Mua", PlanVolume = double.Parse(fndr["RegisteredVol"].ToString()), TodayVolume = double.Parse(fndr["TodayTradingVol"].ToString()), AccumulateVolume = double.Parse(fndr["AccumVol"].ToString()), ExpiredDate = (DateTime)fndr["ExpireDate"] };
                            if (redis.ContainsKey(key))
                                redis.Set<FundHistory>(key, order);
                            else
                                redis.Add<FundHistory>(key, order);

                            if (!fndls.Contains(key)) fndls.Add(key);
                        }
                        if (fndls.Count > 0)
                        {
                            fndls.Sort();
                            fndls.Reverse();
                            if (redis.ContainsKey(fndkeylist))
                                redis.Set<List<string>>(fndkeylist, fndls);
                            else
                                redis.Add<List<string>>(fndkeylist, fndls);
                        }
                        #endregion


                    }
                    catch (Exception ex)
                    {
                        log.WriteEntry(ex.ToString(), EventLogEntryType.Error);
                    }
                    Thread.Sleep(300000); // 5 mins
                }
            }
            catch (Exception ex)
            {
                log.WriteEntry(ex.ToString(), EventLogEntryType.Error);
            }
            finally
            {
                //Thread.CurrentThread.Abort();
            }
        }
        #endregion
    }
}
