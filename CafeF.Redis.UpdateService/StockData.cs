using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using CafeF.Redis.BL;
using CafeF.Redis.Data;
using CafeF.Redis.Entity;
using CafeF.Redis.TestUpdate;
using ServiceStack.Redis;
using CafeF.Redis.BO;

namespace CafeF.Redis.UpdateService
{
    public class StockData
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

        private readonly int interval = int.Parse(ConfigurationManager.AppSettings["StockUpdateInterval"] ?? "1000");
        private readonly int reportInterval = int.Parse(ConfigurationManager.AppSettings["BCTCUpdateInterval"] ?? "600000");
        private int index = 0;

        public StockData(int ind, EventLog mylog)
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
                if ((ConfigurationManager.AppSettings["StockDataAllowance"] ?? "") != "TRUE") return;
                //log.WriteEntry("Start Stock 1 : " + index, EventLogEntryType.Information);
                var bfirstTime = true;
                Thread.Sleep((index - 1) * 1000);
                var sql = new SqlDb();
                while (ServiceStarted)
                {
                    try
                    {
                        //log.WriteEntry(index + "-start-", EventLogEntryType.Information);
                        if (!bfirstTime) Thread.Sleep(interval);
                        bfirstTime = false;
                        //log.WriteEntry("Start Stock 2 : " + index, EventLogEntryType.Information);
                        var sdt = sql.GetStockUpdate();
                        //log.WriteEntry("Start Stock 2 : " + index + " : " + sdt.Rows.Count, EventLogEntryType.Information);
                        if (sdt.Rows.Count == 0) continue;
                        var symbols = new List<string>();
                        foreach (DataRow sdr in sdt.Rows)
                        {
                            var ts = sdr["Symbol"].ToString().ToUpper();
                            if (symbols.Contains(ts)) continue;
                            symbols.Add(ts);
                        }
                        foreach (var symbol in symbols)
                        {
                            //var symbol = sdt.Rows[0]["Symbol"].ToString().ToUpper();
                            var relobj = new List<string>();
                            var rellst = new List<string>();
                            var relutp = new List<string>();

                            var ids = "";
                            var success = true;
                            //log.WriteEntry(symbol + Environment.NewLine, EventLogEntryType.Information);
                            foreach (DataRow sdr in sdt.Rows)
                            {
                                if (symbol != sdr["Symbol"].ToString().ToUpper()) continue;
                                var related = sdr["RelatedObject"].ToString().ToUpper().Split(',');
                                var updateType = sdr["UpdateType"].ToString().ToUpper();
                                foreach (var s in related)
                                {
                                    if (!relobj.Contains(s)) relobj.Add(s);
                                    if (!s.StartsWith("S"))  //nếu là danh sách
                                    {
                                        if (!rellst.Contains(s)) { rellst.Add(s); relutp.Add(updateType); }
                                        //if (!UpdateList(symbol, s, updateType)) success = false;
                                    }
                                }
                                ids += sdr["UpdateId"].ToString() + ",";
                            }
                            //log.WriteEntry(ids + "-" + success + "-" + relobj.Count, EventLogEntryType.Information);
                            sql.OpenDb();
                            if (relobj.Count > 0)  //update mã
                            {
                                //log.WriteEntry("Update stock - " + symbol + "-" + relobj[0], EventLogEntryType.Information);
                                if (!UpdateStock(symbol, relobj, ref sql)) success = false;
                            }
                            for (var i = 0; i < rellst.Count; i++)
                            {
                                if (!UpdateList(symbol, rellst[i], relutp[i], ref sql)) success = false;
                            }
                            //log.WriteEntry("Start Stock 3 : " + index + " : " + symbol + " : " + ids, EventLogEntryType.Information);
                            if (success && !string.IsNullOrEmpty(ids))
                            {
                                sql.UpdateStockMonitor(ids);
                            }
                            sql.CloseDb();
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
            finally
            {
                Thread.CurrentThread.Abort();
            }
        }
        private bool UpdateList(string updateKey, string related, string updateType, ref SqlDb sql)
        {
            var ret = true;
            try
            {

                var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);

                var symbol = updateKey;
                var date = "";
                if (updateKey.Contains("."))
                {
                    symbol = updateKey.Substring(0, updateKey.IndexOf("."));
                    date = updateKey.Substring(updateKey.IndexOf(".") + 1);
                }
                switch (related)
                {
                    case "FP":
                        #region FP
                        try
                        {
                            var keylist = string.Format(RedisKey.PriceHistoryKeys, symbol);
                            var ls = redis.ContainsKey(keylist) ? redis.Get<List<String>>(keylist) : new List<string>();
                            //log.WriteEntry(symbol + "-price-" + DateTime.Now, EventLogEntryType.Information);
                            var pdt = (date == "" || ls.Count == 0) ? sql.GetPriceHistory(symbol, -1) : sql.GetPriceHistory(symbol, date);
                            //log.WriteEntry(symbol + "-price-" + DateTime.Now, EventLogEntryType.Information);
                            foreach (DataRow pdr in pdt.Rows)
                            {
                                var key = string.Format(RedisKey.PriceHistory, symbol, ((DateTime)pdr["TradeDate"]).ToString("yyyyMMdd"));

                                var price = new StockHistory() { TradeDate = (DateTime)pdr["TradeDate"], ClosePrice = double.Parse(pdr["ClosePrice"].ToString()), AveragePrice = double.Parse(pdr["AveragePrice"].ToString()), BasicPrice = double.Parse(pdr["BasicPrice"].ToString()), Ceiling = double.Parse(pdr["Ceiling"].ToString()), Floor = double.Parse(pdr["Floor"].ToString()), Volume = double.Parse(pdr["Volume"].ToString()), TotalValue = double.Parse(pdr["TotalValue"].ToString()), AgreedValue = double.Parse(pdr["AgreedValue"].ToString()), AgreedVolume = double.Parse(pdr["AgreedVolume"].ToString()), Symbol = symbol, KLGDDot1 = double.Parse(pdr["VolumePhase1"].ToString()), KLGDDot2 = double.Parse(pdr["VolumePhase2"].ToString()), KLGDDot3 = double.Parse(pdr["VolumePhase3"].ToString()), OpenPrice = double.Parse(pdr["OpenPrice"].ToString()), HighPrice = double.Parse(pdr["HighPrice"].ToString()), LowPrice = double.Parse(pdr["LowPrice"].ToString()) };
                                if (redis.ContainsKey(key))
                                    redis.Set<StockHistory>(key, price);
                                else
                                    redis.Add<StockHistory>(key, price);

                                if (!ls.Contains(key)) ls.Add(key);
                            }
                            ls.Sort();
                            ls.Reverse();
                            if (redis.ContainsKey(keylist))
                                redis.Set<List<string>>(keylist, ls);
                            else
                                redis.Add<List<string>>(keylist, ls);
                        }
                        catch (Exception ex)
                        {
                            log.WriteEntry(symbol + " : FP : " + ex.ToString(), EventLogEntryType.Error);
                            ret = false;
                        }
                        //var test = redis.Get<List<string>>(keylist);
                        #endregion
                        break;
                    case "FO":
                        #region FO
                        try
                        {
                            var keylist = string.Format(RedisKey.OrderHistoryKeys, symbol);
                            var ls = redis.ContainsKey(keylist) ? redis.Get<List<String>>(keylist) : new List<string>();

                            var pdt = (date == "" || ls.Count == 0) ? sql.GetOrderHistory(symbol, 1000) : sql.GetOrderHistory(symbol, date);
                            foreach (DataRow pdr in pdt.Rows)
                            {
                                var key = string.Format(RedisKey.OrderHistory, symbol, ((DateTime)pdr["Trading_Date"]).ToString("yyyyMMdd"));

                                var order = new OrderHistory() { TradeDate = (DateTime)pdr["Trading_Date"], BuyOrderCount = double.Parse(pdr["Bid_Order"].ToString()), BuyVolume = double.Parse(pdr["Bid_Volume"].ToString()), SellOrderCount = double.Parse(pdr["Offer_Order"].ToString()), SellVolume = double.Parse(pdr["Offer_Volume"].ToString()), Symbol = symbol }; //Volume = double.Parse(pdr["Volume"].ToString()), Price = double.Parse(pdr["Price"].ToString()), BasicPrice = double.Parse(pdr["BasicPrice"].ToString()), Ceiling = double.Parse(pdr["Ceiling"].ToString()), Floor = double.Parse(pdr["Floor"].ToString()), 
                                if (redis.ContainsKey(key))
                                    redis.Set<OrderHistory>(key, order);
                                else
                                    redis.Add<OrderHistory>(key, order);

                                if (!ls.Contains(key)) ls.Add(key);
                            }
                            ls.Sort();
                            ls.Reverse();
                            if (redis.ContainsKey(keylist))
                                redis.Set<List<string>>(keylist, ls);
                            else
                                redis.Add<List<string>>(keylist, ls);
                        }
                        catch (Exception ex)
                        {
                            log.WriteEntry(symbol + " : FO : " + ex.ToString(), EventLogEntryType.Error);
                            ret = false;
                        }
                        #endregion
                        break;
                    case "FIT":
                        #region FIT
                        try
                        {
                            var keylist = string.Format(RedisKey.InternalHistoryKeys, symbol);
                            var ls = new List<string>(); //redis.ContainsKey(keylist) ? redis.Get<List<String>>(keylist) : new List<string>();
                            
                            var pdt = sql.GetInternalHistory(symbol, -1);
                            
                            foreach (DataRow pdr in pdt.Rows)
                            {
                                var key = string.Format(RedisKey.InternalHistory, symbol, (((DateTime?)pdr["NgayThongBao"]) ?? DateTime.Now).ToString("yyyyMMdd"), pdr["ID"], pdr["ShareHolder_ID"] + ":" + pdr["ShareHolderCode"]);
                                var order = new InternalHistory() { Stock = symbol, RelatedMan = pdr["NguoiLienQuan"].ToString(), RelatedManPosition = pdr["ChucVuNguoiLienQuan"].ToString(), VolumeBeforeTransaction = double.Parse(pdr["SLCPTruocGD"].ToString()), PlanBuyVolume = double.Parse(pdr["DangKy_Mua"].ToString()), PlanSellVolume = double.Parse(pdr["DangKy_Ban"].ToString()), PlanBeginDate = pdr["DangKy_TuNgay"].Equals(DBNull.Value) ? null : (DateTime?)pdr["DangKy_TuNgay"], PlanEndDate = pdr["DangKy_DenNgay"].Equals(DBNull.Value) ? null : (DateTime?)pdr["DangKy_DenNgay"], RealBuyVolume = double.Parse(pdr["ThucHien_Mua"].ToString()), RealSellVolume = double.Parse(pdr["ThucHien_Ban"].ToString()), RealEndDate = pdr["ThucHien_NgayKetThuc"].Equals(DBNull.Value) ? null : (DateTime?)pdr["ThucHien_NgayKetThuc"], PublishedDate = pdr["NgayThongBao"].Equals(DBNull.Value) ? null : (DateTime?)pdr["NgayThongBao"], VolumeAfterTransaction = double.Parse(pdr["SLCPSauGD"].ToString()), TransactionNote = pdr["GhiChu"].ToString(), HolderID = pdr["ShareHolder_ID"].ToString(), ShareHolderCode = pdr["ShareHolderCode"].ToString() }; //TransactionMan = pdr["FullName"].ToString(), TransactionManPosition = pdr["ChucVu"].ToString(), 
                                var tochuc = pdr["ToChuc"].ToString();
                                order.TransactionMan = tochuc.IndexOf("--") > 0 ? tochuc.Substring(0, tochuc.IndexOf("--")) : tochuc;
                                order.TransactionManPosition = tochuc.IndexOf("--") > 0 && tochuc.IndexOf("--") < tochuc.Length - 2 ? tochuc.Substring(tochuc.IndexOf("--") + 2) : "";

                                if (redis.ContainsKey(key))
                                    redis.Set<InternalHistory>(key, order);
                                else
                                    redis.Add<InternalHistory>(key, order);

                                if (!ls.Contains(key)) ls.Add(key);
                            }
                            ls.Sort();
                            ls.Reverse();
                            if (redis.ContainsKey(keylist))
                                redis.Set<List<string>>(keylist, ls);
                            else
                                redis.Add<List<string>>(keylist, ls);
                            var internalkeys = redis.SearchKeys(string.Format(RedisKey.InternalHistory, symbol, "*", "*", "*")) ?? new List<string>();
                            foreach (var internalkey in internalkeys)
                            {
                                if(!ls.Contains(internalkey)) redis.Remove(internalkey);
                            }
                        }
                        catch (Exception ex)
                        {
                            log.WriteEntry(symbol + " : FIT : " + ex.ToString(), EventLogEntryType.Error);
                            ret = false;
                        }
                        #endregion
                        break;
                    case "FFT":
                        #region FFT
                        try
                        {
                            var keylist = string.Format(RedisKey.ForeignHistoryKeys, symbol);
                            var ls = redis.ContainsKey(keylist) ? redis.Get<List<String>>(keylist) : new List<string>();

                            var pdt = (date == "" || ls.Count == 0) ? sql.GetForeignHistory(symbol, -1) : sql.GetForeignHistory(symbol, date);
                            foreach (DataRow pdr in pdt.Rows)
                            {
                                var key = string.Format(RedisKey.ForeignHistory, symbol, ((DateTime)pdr["Trading_Date"]).ToString("yyyyMMdd"));

                                var order = new ForeignHistory() { TradeDate = (DateTime)pdr["Trading_Date"], BuyVolume = double.Parse(pdr["Buying_Volume"].ToString()), BuyValue = double.Parse(pdr["Buying_Value"].ToString()), SellVolume = double.Parse(pdr["Selling_Volume"].ToString()), SellValue = double.Parse(pdr["Selling_Value"].ToString()), Room = double.Parse(pdr["CurrentRoom"].ToString()), TotalRoom = double.Parse(pdr["TotalRoom"].ToString()), Percent = double.Parse(pdr["SoHuu"].ToString()), Symbol = symbol }; //, BasicPrice = double.Parse(pdr["BasicPrice"].ToString()), ClosePrice = double.Parse(pdr["ClosePrice"].ToString()), AveragePrice = double.Parse(pdr["AveragePrice"].ToString()) 
                                if (redis.ContainsKey(key))
                                    redis.Set<ForeignHistory>(key, order);
                                else
                                    redis.Add<ForeignHistory>(key, order);

                                if (!ls.Contains(key)) ls.Add(key);
                            }
                            ls.Sort();
                            ls.Reverse();
                            if (redis.ContainsKey(keylist))
                                redis.Set<List<string>>(keylist, ls);
                            else
                                redis.Add<List<string>>(keylist, ls);
                        }
                        catch (Exception ex)
                        {
                            log.WriteEntry(symbol + " : FFT : " + ex.ToString(), EventLogEntryType.Error);
                            ret = false;
                        }
                        #endregion
                        break;
                    case "FS":
                        #region FS
                        try
                        {
                            var skey = RedisKey.KeyStockList;
                            var sdt = sql.GetSymbolList(-1);
                            var sls = new List<StockCompact>();
                            var hnx = new List<StockCompact>();
                            var hsx = new List<StockCompact>();
                            var upc = new List<StockCompact>();
                            foreach (DataRow sdr in sdt.Rows)
                            {
                                var stock = new StockCompact() { Symbol = sdr["Symbol"].ToString(), CategoryId = int.Parse(sdr["StockIndustryId"].ToString()), TradeCenterId = int.Parse(sdr["TradeCenterId"].ToString()) };
                                sls.Add(stock);
                                switch (stock.TradeCenterId)
                                {
                                    case 1: hsx.Add(stock); break;
                                    case 2: hnx.Add(stock); break;
                                    case 9: upc.Add(stock); break;
                                }
                            }
                            if (redis.ContainsKey(skey))
                                redis.Set(skey, sls);
                            else
                                redis.Add(skey, sls);
                            skey = string.Format(RedisKey.KeyStockListByCenter, "1");
                            if (redis.ContainsKey(skey))
                                redis.Set(skey, hsx);
                            else
                                redis.Add(skey, hsx);
                            skey = string.Format(RedisKey.KeyStockListByCenter, "2");
                            if (redis.ContainsKey(skey))
                                redis.Set(skey, hnx);
                            else
                                redis.Add(skey, hnx);
                            skey = string.Format(RedisKey.KeyStockListByCenter, "9");
                            if (redis.ContainsKey(skey))
                                redis.Set(skey, upc);
                            else
                                redis.Add(skey, upc);
                        }
                        catch (Exception ex)
                        {
                            log.WriteEntry(symbol + " : FS : " + ex.ToString(), EventLogEntryType.Error);
                            ret = false;
                        }
                        #endregion
                        break;
                    case "FA":
                        #region FA
                        try
                        {
                            var keylist = RedisKey.KeyAnalysisReport;
                            var ls = (redis.ContainsKey(keylist)) ? redis.Get<List<string>>(keylist) : new List<string>();
                            var pdt = (date == "" || ls.Count == 0) ? sql.GetAnalysisReports("A", 2000) : sql.GetAnalysisReports("A", date);
                            var rs = new List<string>();
                            if (ls.Count > 0 && date != "")
                            {
                                //remove reports by date
                                var d = date.Replace(".", "");
                                rs = ls.FindAll(s => s.Substring(0, 8) == d);
                                foreach (var r in rs)
                                {
                                    ls.Remove(r);

                                }
                            }
                            foreach (DataRow rdr in pdt.Rows)
                            {

                                var key = string.Format(RedisKey.KeyAnalysisReportDetail, rdr["ID"]);
                                var obj = new Reports() { ID = int.Parse(rdr["ID"].ToString()), Body = rdr["Des"].ToString(), DateDeploy = (DateTime)rdr["PublishDate"], file = new FileObject() { FileName = rdr["FileName"].ToString(), FileUrl = "http://images1.cafef.vn/Images/Uploaded/DuLieuDownload/PhanTichBaoCao/" + rdr["FileName"] }, IsHot = (rdr["IsHot"].ToString().ToLower() == "true"), ResourceCode = rdr["Source"].ToString(), Symbol = rdr["Symbol"].ToString(), Title = rdr["title"].ToString(), SourceID = int.Parse(rdr["SourceId"].ToString()), ResourceName = rdr["SourceFullName"].ToString(), ResourceLink = rdr["SourceUrl"].ToString() };
                                var id = obj.DateDeploy.ToString("yyyyMMdd") + obj.ID;
                                if (redis.ContainsKey(key))
                                    redis.Set(key, obj);
                                else
                                    redis.Add(key, obj);
                                if (!ls.Contains(id)) ls.Add(id);
                            }
                            foreach (var r in rs)
                            {
                                if (ls.Contains(r)) continue;
                                var id = r.Substring(8);
                                var key = string.Format(RedisKey.KeyAnalysisReportDetail, id);
                                if (redis.ContainsKey(key))
                                    redis.Remove(key);
                            }
                            ls.Sort();
                            ls.Reverse();
                            if (redis.ContainsKey(keylist))
                                redis.Set(keylist, ls);
                            else
                                redis.Add(keylist, ls);

                        }
                        catch (Exception ex)
                        {
                            log.WriteEntry(symbol + " : FA : " + ex.ToString(), EventLogEntryType.Error);
                            ret = false;
                        }
                        #endregion
                        break;
                    case "FT":
                        #region FT
                        try
                        {
                            var fdt = sql.GetTopStock();

                            UpdateTopStock(RedisKey.KeyTopStockCenter.All, RedisKey.KeyTopStockType.EPS, ref redis, ref fdt);
                            UpdateTopStock(RedisKey.KeyTopStockCenter.Ho, RedisKey.KeyTopStockType.EPS, ref redis, ref fdt);
                            UpdateTopStock(RedisKey.KeyTopStockCenter.Ha, RedisKey.KeyTopStockType.EPS, ref redis, ref fdt);
                            UpdateTopStock(RedisKey.KeyTopStockCenter.All, RedisKey.KeyTopStockType.MarketCap, ref redis, ref fdt);
                            UpdateTopStock(RedisKey.KeyTopStockCenter.Ho, RedisKey.KeyTopStockType.MarketCap, ref redis, ref fdt);
                            UpdateTopStock(RedisKey.KeyTopStockCenter.Ha, RedisKey.KeyTopStockType.MarketCap, ref redis, ref fdt);
                            UpdateTopStock(RedisKey.KeyTopStockCenter.All, RedisKey.KeyTopStockType.PE, ref redis, ref fdt);
                            UpdateTopStock(RedisKey.KeyTopStockCenter.Ho, RedisKey.KeyTopStockType.PE, ref redis, ref fdt);
                            UpdateTopStock(RedisKey.KeyTopStockCenter.Ha, RedisKey.KeyTopStockType.PE, ref redis, ref fdt);
                        }
                        catch (Exception ex)
                        {
                            log.WriteEntry(symbol + " : FT : " + ex.ToString(), EventLogEntryType.Error);
                            ret = false;
                        }
                        #endregion
                        break;
                    case "FN":
                        #region FN
                        try
                        {
                            var keylist = string.Format(RedisKey.KeyCompanyNewsByCate, 0); //Tất cả
                            var ls = (redis.ContainsKey(keylist)) ? redis.Get<List<string>>(keylist) : new List<string>();
                            var pdt = (date == "" || ls.Count == 0) ? sql.GetCompanyNews("A", 1000) : sql.GetCompanyNews("A", date);

                            var key1 = string.Format(RedisKey.KeyCompanyNewsByCate, 1); //Tình hình SXKD & Phân tích khác
                            var key2 = string.Format(RedisKey.KeyCompanyNewsByCate, 2); // Cổ tức - Chốt quyền
                            var key3 = string.Format(RedisKey.KeyCompanyNewsByCate, 3); // Thay đổi nhân sự
                            var key4 = string.Format(RedisKey.KeyCompanyNewsByCate, 4); // Tăng vốn - Cổ phiếu quỹ
                            var key5 = string.Format(RedisKey.KeyCompanyNewsByCate, 5); // GD cđ lớn & cđ nội bộ
                            var cate1 = (redis.ContainsKey(key1)) ? redis.Get<List<string>>(key1) : new List<string>();

                            var cate2 = (redis.ContainsKey(key2)) ? redis.Get<List<string>>(key2) : new List<string>();
                            var cate3 = (redis.ContainsKey(key3)) ? redis.Get<List<string>>(key3) : new List<string>();

                            var cate4 = (redis.ContainsKey(key4)) ? redis.Get<List<string>>(key4) : new List<string>();
                            var cate5 = (redis.ContainsKey(key5)) ? redis.Get<List<string>>(key5) : new List<string>();

                            //remove bài trong ngày (dùng để xóa các tin thừa)
                            var rm = (date != "") ? ls.FindAll(key => key.StartsWith((date.Replace(".", "")))) : new List<string>();
                            foreach (var item in rm)
                            {
                                if (ls.Contains(item)) ls.Remove(item);
                                if (cate1.Contains(item)) cate1.Remove(item);
                                if (cate2.Contains(item)) cate2.Remove(item);
                                if (cate3.Contains(item)) cate3.Remove(item);
                                if (cate4.Contains(item)) cate4.Remove(item);
                                if (cate5.Contains(item)) cate5.Remove(item);
                            }

                            foreach (DataRow rdr in pdt.Rows)
                            {
                                var compact = new StockNews() { ID = int.Parse(rdr["ID"].ToString()), Body = "", DateDeploy = (DateTime)rdr["PostTime"], Image = "", Title = rdr["title"].ToString(), Sapo = "", TypeID = rdr["ConfigId"].ToString(), Symbol = rdr["StockSymbols"].ToString() };
                                var obj = new StockNews() { ID = int.Parse(rdr["ID"].ToString()), Body = rdr["Content"].ToString(), DateDeploy = (DateTime)rdr["PostTime"], Image = rdr["ImagePath"].ToString(), Title = rdr["title"].ToString(), Sapo = rdr["SubContent"].ToString(), TypeID = rdr["ConfigId"].ToString(), Symbol = rdr["StockSymbols"].ToString() };
                                for (int i = 0xD800; i <= 0xDFFF; i++)
                                {
                                    obj.Body = obj.Body.Replace((char)i, ' ');
                                }
                                var key = obj.DateDeploy.ToString("yyyyMMddHHmm") + obj.ID;
                                var compactkey = string.Format(RedisKey.KeyCompanyNewsCompact, obj.ID);
                                if (redis.ContainsKey(compactkey))
                                    redis.Set(compactkey, compact);
                                else
                                    redis.Add(compactkey, compact);
                                var detailkey = string.Format(RedisKey.KeyCompanyNewsDetail, obj.ID);
                                if (redis.ContainsKey(detailkey))
                                    redis.Set(detailkey, obj);
                                else
                                    redis.Add(detailkey, obj);

                                if (!ls.Contains(key)) ls.Add(key);
                                #region Update category list
                                if (obj.TypeID.Contains("1"))
                                {
                                    if (!cate1.Contains(key)) cate1.Add(key);
                                }
                                else
                                {
                                    if (cate1.Contains(key)) cate1.Remove(key);
                                }
                                if (obj.TypeID.Contains("2"))
                                {
                                    if (!cate2.Contains(key)) cate2.Add(key);
                                }
                                else
                                {
                                    if (cate2.Contains(key)) cate2.Remove(key);
                                }
                                if (obj.TypeID.Contains("3"))
                                {
                                    if (!cate3.Contains(key)) cate3.Add(key);
                                }
                                else
                                {
                                    if (cate3.Contains(key)) cate3.Remove(key);
                                }
                                if (obj.TypeID.Contains("4"))
                                {
                                    if (!cate4.Contains(key)) cate4.Add(key);
                                }
                                else
                                {
                                    if (cate4.Contains(key)) cate4.Remove(key);
                                }
                                if (obj.TypeID.Contains("5"))
                                {
                                    if (!cate5.Contains(key)) cate5.Add(key);
                                }
                                else
                                {
                                    if (cate5.Contains(key)) cate5.Remove(key);
                                }
                                #endregion
                            }

                            //if (date != "")
                            //{
                            //remove bài trong ngày (dùng để xóa các tin thừa)
                            foreach (var item in rm)
                            {
                                if (ls.Contains(item)) continue;
                                var id = item.Substring(12);
                                var compactkey = string.Format(RedisKey.KeyCompanyNewsCompact, id);
                                if (redis.ContainsKey(compactkey))
                                    redis.Remove(compactkey);
                                var detailkey = string.Format(RedisKey.KeyCompanyNewsDetail, id);
                                if (redis.ContainsKey(detailkey))
                                    redis.Remove(detailkey);
                            }
                            //}

                            ls.Sort();
                            ls.Reverse();
                            if (redis.ContainsKey(keylist))
                                redis.Set(keylist, ls);
                            else
                                redis.Add(keylist, ls);
                            #region Update category list
                            cate1.Sort();
                            cate1.Reverse();
                            if (redis.ContainsKey(key1))
                                redis.Set(key1, cate1);
                            else
                                redis.Add(key1, cate1);
                            cate2.Sort();
                            cate2.Reverse();
                            if (redis.ContainsKey(key2))
                                redis.Set(key2, cate2);
                            else
                                redis.Add(key2, cate2);
                            cate3.Sort();
                            cate3.Reverse();
                            if (redis.ContainsKey(key3))
                                redis.Set(key3, cate3);
                            else
                                redis.Add(key3, cate3);
                            cate4.Sort();
                            cate4.Reverse();
                            if (redis.ContainsKey(key4))
                                redis.Set(key4, cate4);
                            else
                                redis.Add(key4, cate4);
                            cate5.Sort();
                            cate5.Reverse();
                            if (redis.ContainsKey(key5))
                                redis.Set(key5, cate5);
                            else
                                redis.Add(key5, cate5);
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            log.WriteEntry(symbol + " : FN : " + ex.ToString(), EventLogEntryType.Error);
                            ret = false;
                        }
                        #endregion
                        #region Update top 100 news
                        var topkey = RedisKey.KeyTop20News;
                        var ndt = sql.GetCompanyNews("A", 100);
                        var topls = new List<StockNews>();
                        foreach (DataRow ndr in ndt.Rows)
                        {
                            var da = (DateTime)ndr["PostTime"];
                            topls.Add(new StockNews() { ID = int.Parse(ndr["ID"].ToString()), Body = "", DateDeploy = (DateTime)ndr["PostTime"], Image = "", Title = ndr["title"].ToString(), Sapo = "", TypeID = ndr["ConfigId"].ToString(), Symbol = ndr["StockSymbols"].ToString() });
                        }
                        if (redis.ContainsKey(topkey))
                            redis.Set(topkey, topls);
                        else
                            redis.Add(topkey, topls);
                        #endregion
                        break;
                    case "FND":
                        #region "Fund Transaction"
                        var fndkeylist = string.Format(RedisKey.FundHistoryKeys, symbol);
                        //var fndls = redis.ContainsKey(fndkeylist) ? redis.Get<List<String>>(fndkeylist) : new List<string>();
                        var fndls = new List<string>();
                        var fndt = sql.GetFundHistory(symbol, -1);
                        foreach (DataRow fndr in fndt.Rows)
                        {
                            var key = string.Format(RedisKey.FundHistory, symbol, ((DateTime)fndr["TradingDate"]).ToString("yyyyMMdd"));

                            var order = new FundHistory() { Symbol = symbol, TradeDate = (DateTime)fndr["TradingDate"], TransactionType = fndr["Buy_Sale"].ToString() == "s" ? "Bán" : "Mua", PlanVolume = double.Parse(fndr["RegisteredVol"].ToString()), TodayVolume = double.Parse(fndr["TodayTradingVol"].ToString()), AccumulateVolume = double.Parse(fndr["AccumVol"].ToString()), ExpiredDate = (DateTime)fndr["ExpireDate"] };
                            if (redis.ContainsKey(key))
                                redis.Set<FundHistory>(key, order);
                            else
                                redis.Add<FundHistory>(key, order);

                            if (!fndls.Contains(key)) fndls.Add(key);
                        }
                        fndls.Sort();
                        fndls.Reverse();
                        if (redis.ContainsKey(fndkeylist))
                            redis.Set<List<string>>(fndkeylist, fndls);
                        else
                            redis.Add<List<string>>(fndkeylist, fndls);
                        break;
                        #endregion
                    case "CI":

                        break;
                    case "CN":
                        #region CEO

                        var ceokey = string.Format(RedisKey.CeoKey, symbol);
                        var cdt = sql.GetCeosNew_Profile(symbol);
                        if (cdt.Rows.Count == 0)
                        {
                            if (redis.ContainsKey(ceokey)) redis.Remove(ceokey);
                        }
                        else
                        {
                            var ceo = new Ceo();
                            if (redis.ContainsKey(ceokey))
                            {
                                ceo = redis.Get<Ceo>(ceokey) ?? new Ceo();
                            }
                            ceo.CeoName = cdt.Rows[0]["CeoName"].ToString(); ceo.CeoBirthday = cdt.Rows[0]["CeoBirthday"].ToString(); ceo.CeoIdNo = cdt.Rows[0]["CeoIdNo"].ToString(); ceo.CeoAchievements = cdt.Rows[0]["CeoAchievements"].ToString(); ceo.CeoHomeTown = cdt.Rows[0]["CeoHomeTown"].ToString(); ceo.CeoSchoolDegree = cdt.Rows[0]["CeoLevel"].ToString();
                            ceo.CeoCode = cdt.Rows[0]["CeoCode"].ToString();

                            if (ceo.CeoBirthday.Contains("/"))
                            {
                                ceo.CeoBirthday = ceo.CeoBirthday.Substring(ceo.CeoBirthday.LastIndexOf("/") + 1);
                            }

                            try
                            {
                                //school title
                                var css = new List<CeoSchool>();
                                var sdt = sql.GetCeosNew_School(symbol);
                                foreach (DataRow sdr in sdt.Rows)
                                {
                                    css.Add(new CeoSchool() { CeoTitle = sdr["CeoTitle"].ToString(), SchoolTitle = sdr["SchoolTitle"].ToString(), SchoolYear = sdr["SchoolYear"].ToString() });
                                }
                                ceo.CeoSchool = css;
                            }
                            catch (Exception ex)
                            {
                                log.WriteEntry(symbol + " : CEO School : " + ex.ToString(), EventLogEntryType.Error);
                                ret = false;
                            }
                            try
                            {
                                //ceo position
                                var cps = new List<CeoPosition>();
                                var pdt = sql.GetCeosNew_Position(symbol);
                                foreach (DataRow pdr in pdt.Rows)
                                {
                                    var cp = new CeoPosition() { PositionTitle = pdr["PositionTitle"].ToString(), PositionCompany = pdr["PositionCompany"].ToString(), CeoSymbol = pdr["Symbol"].ToString(), CeoSymbolCenterId = int.Parse(pdr["TradeCenterId"].ToString()) };
                                    if (string.IsNullOrEmpty(cp.PositionTitle)) cp.PositionTitle = pdr["PositionName"].ToString();
                                    if (string.IsNullOrEmpty(cp.PositionCompany)) cp.PositionCompany = pdr["FullName"].ToString();

                                    //__/01/2007
                                    string cpd = pdr["CeoPosDate"].ToString();
                                    if (cpd.Contains("/"))
                                    {
                                        int day, month, year;
                                        if (!int.TryParse(cpd.Substring(0, cpd.IndexOf("/")), out day)) day = 0;
                                        cpd = cpd.Substring(cpd.IndexOf("/") + 1);
                                        if (!int.TryParse(cpd.Substring(0, cpd.IndexOf("/")), out month)) month = 0;
                                        cpd = cpd.Substring(cpd.IndexOf("/") + 1);
                                        if (!int.TryParse(cpd, out year)) year = 0;
                                        if (year == 0)
                                        {
                                            cp.CeoPosDate = "";
                                        }
                                        else if (day == 0 && month == 0)
                                        {
                                            cp.CeoPosDate = "" + year;
                                        }
                                        else if (month > 0 && day == 0)
                                        {
                                            cp.CeoPosDate = month + "/" + year;
                                        }
                                        else if (day > 0 && month > 0)
                                        {
                                            cp.CeoPosDate = day + "/" + month + "/" + year;
                                        }
                                        else
                                        {
                                            cp.CeoPosDate = "";
                                        }
                                    }

                                    cps.Add(cp);
                                }
                                ceo.CeoPosition = cps;
                            }
                            catch (Exception ex)
                            {
                                log.WriteEntry(symbol + " : CEO Position : " + ex.ToString(), EventLogEntryType.Error);
                                ret = false;
                            }
                            try
                            {
                                //asset
                                var cas = new List<CeoAsset>();
                                var adt = sql.GetCeosNew_Asset(symbol);
                                foreach (DataRow adr in adt.Rows)
                                {
                                    cas.Add(new CeoAsset() { Symbol = adr["Symbol"].ToString(), AssetVolume = double.Parse(adr["AssetVolume"].ToString()).ToString("#,##0"), UpdatedDate = ((DateTime)adr["UpdatedDate"]).ToString("dd/MM/yyyy") });
                                }
                                ceo.CeoAsset = cas;
                            }
                            catch (Exception ex)
                            {
                                log.WriteEntry(symbol + " : CEO Asset : " + ex.ToString(), EventLogEntryType.Error);
                                ret = false;
                            }
                            try
                            {
                                //relation
                                var crs = new List<CeoRelation>();
                                var rdt = sql.GetCeosNew_Relation(symbol);
                                foreach (DataRow rdr in rdt.Rows)
                                {
                                    crs.Add(new CeoRelation() { Symbol = rdr["Symbol"].ToString(), AssetVolume = double.Parse(rdr["AssetVolume"].ToString()).ToString("#,##0"), UpdatedDate = ((DateTime)rdr["UpdatedDate"]).ToString("dd/MM/yyyy"), Name = rdr["CeoName"].ToString(), CeoCode = rdr["CeoCode"].ToString(), RelationTitle = rdr["RelationTitle"].ToString() });
                                }
                                ceo.CeoRelation = crs;
                            }
                            catch (Exception ex)
                            {
                                log.WriteEntry(symbol + " : CEO Relation : " + ex.ToString(), EventLogEntryType.Error);
                                ret = false;
                            }
                            try
                            {
                                //process
                                var cos = new List<CeoProcess>();
                                var odt = sql.GetCeosNew_Process(symbol);
                                foreach (DataRow odr in odt.Rows)
                                {
                                    var begin = GetCeoDate(odr["ProcessBegin"].ToString());
                                    var end = GetCeoDate(odr["ProcessEnd"].ToString());
                                    var process = (begin != "" ? ("Từ " + begin + " ") : "") + (end != "" ? ((begin != "" ? "đến" : "Đến") + " " + end + "") : "");
                                    cos.Add(new CeoProcess() { ProcessBegin = odr["ProcessBegin"].ToString(), ProcessEnd = odr["ProcessEnd"].ToString(), ProcessDesc = process + (process == "" ? "" : " : ") + odr["ProcessDesc"].ToString(), Symbol = odr["Symbol"].ToString()});
                                }
                                ceo.CeoProcess = cos;
                            }
                            catch (Exception ex)
                            {
                                log.WriteEntry(symbol + " : CEO Process : " + ex.ToString(), EventLogEntryType.Error);
                                ret = false;
                            }
                            try
                            {
                                //news
                                var cns = new List<CeoNews>();
                                var wdt = sql.GetCeosNew_News(symbol);
                                var ids = "";
                                foreach (DataRow ndr in wdt.Rows)
                                {
                                    ids += "," + ndr["NewsId"];
                                }
                                if (ids != "")
                                {
                                    var wdt2 = sql.GetCeosNew_NewsDetail(ids);
                                    foreach (DataRow ndr in wdt2.Rows)
                                    {
                                        cns.Add(new CeoNews() { Title = ndr["News_Title"].ToString(), PublishDate = (DateTime)ndr["News_PublishDate"], NewsLink = string.Format("/{0}CA{1}/{2}.chn", ndr["News_Id"], ndr["Cat_ID"], CafeF.Redis.BL.Utils.UnicodeToKoDauAndGach(ndr["News_Title"].ToString())) });
                                    }
                                }
                                ceo.CeoNews = cns;
                            }
                            catch (Exception ex)
                            {
                                log.WriteEntry(symbol + " : CEO News : " + ex.ToString(), EventLogEntryType.Error);
                                ret = false;
                            }
                            if (redis.ContainsKey(ceokey))
                            {
                                redis.Set(ceokey, ceo);
                            }
                            else
                            {
                                redis.Add(ceokey, ceo);
                            }
                        }
                        #endregion
                        break;
                    case "FC":
                        #region Lịch sự kiện 
                        var lskdt = sql.GetLichSuKien(date);
                        var keys = redis.ContainsKey(RedisKey.KeyLichSuKien) ? redis.Get<List<string>>(RedisKey.KeyLichSuKien) : new List<string>();
                        var removals = keys.FindAll(s => s.Substring(0,8) == date.Replace(".", ""));
                        foreach(var removal in removals)
                        {
                            var key = string.Format(RedisKey.KeyLichSuKienObject, removal.Substring(removal.LastIndexOf(":") + 1));
                            if (redis.ContainsKey(key)) redis.Remove(key);
                            keys.Remove(removal);
                        }
                        foreach (DataRow ldr in lskdt.Rows)
                        {
                            var o = new LichSuKien() { ID = int.Parse(ldr["ID"].ToString()), LoaiSuKien = ldr["EventType_List"].ToString(), MaCK = ldr["StockSymbols"].ToString(), MaSan = 0, News_ID = ldr["News_ID"].ToString(), Title = ldr["EventTitle"].ToString(), NgayBatDau = ldr["NgayBatDau"].ToString(), NgayKetThuc = ldr["NgayKetThuc"].ToString(), NgayThucHien = ldr["NgayThucHien"].ToString(), TenCty = "", TomTat = "", PostDate = (DateTime)ldr["PostDate"] };
                            try
                            {
                                o.EventDate = (DateTime)ldr["EventDate"];
                            }
                            catch (Exception)
                            {
                                o.EventDate = DateTime.Parse("2000-01-01");
                            }
                            var key = string.Format(RedisKey.KeyLichSuKienObject, o.ID);
                            if (redis.ContainsKey(key))
                                redis.Set(key, o);
                            else
                                redis.Add(key, o);
                            key = string.Format(RedisKey.KeyLichSuKienObjectInList, o.EventDate.ToString("yyyyMMdd"), string.IsNullOrEmpty(o.LoaiSuKien.Trim()) ? "_" : o.LoaiSuKien, o.ID);
                            if (!keys.Contains(key)) keys.Add(key);
                        }
                        if (redis.ContainsKey(RedisKey.KeyLichSuKien))
                            redis.Set(RedisKey.KeyLichSuKien, keys);
                        else
                            redis.Add(RedisKey.KeyLichSuKien, keys);
                        #endregion
                        #region Lịch sự kiện tóm tắt
                        lskdt = sql.GetLichSuKienTomTat();
                        var lls = new List<LichSuKien>();
                        foreach (DataRow ldr in lskdt.Rows)
                        {
                            var o = new LichSuKien() { ID = int.Parse(ldr["ID"].ToString()), LoaiSuKien = ldr["EventType_List"].ToString(), MaCK = ldr["StockSymbols"].ToString(), MaSan = 0, News_ID = ldr["News_ID"].ToString(), Title = ldr["EventTitle"].ToString(), NgayBatDau = ldr["NgayBatDau"].ToString(), NgayKetThuc = ldr["NgayKetThuc"].ToString(), NgayThucHien = ldr["NgayThucHien"].ToString(), TenCty = "", TomTat = "", PostDate = (DateTime)ldr["PostDate"] };
                            try
                            {
                                o.EventDate = (DateTime)ldr["EventDate"];
                            }
                            catch (Exception)
                            {
                                o.EventDate = DateTime.Parse("2000-01-01");
                            }
                            lls.Add(o);
                        }
                        if (redis.ContainsKey(RedisKey.KeyLichSuKienTomTat))
                            redis.Set(RedisKey.KeyLichSuKienTomTat, lls);
                        else
                            redis.Add(RedisKey.KeyLichSuKienTomTat, lls);
                        #endregion
                        break;
                    case "FB":
                        #region Bond
                        var countries = SqlDb.GetBondCountry(symbol);
                        var types = new List<string>() { "1", "3", "5", "10" };
                        var bondkeys = new List<string>();
                        var cs = new List<string>();
                        foreach (DataRow dr in countries.Rows)
                        {
                            var country = dr["CountryName"].ToString();
                            if (!cs.Contains(country)) cs.Add(country);
                            foreach (var type in types)
                            {
                                var dt = SqlDb.GetBondValue(country, type);
                                if (dt.Rows.Count == 0) continue;
                                var o = new Bond() { BondCode = dt.Rows[0]["BondCode"].ToString(), BondCountry = dt.Rows[0]["CountryName"].ToString(), BondType = dt.Rows[0]["BondType"].ToString(), BondEnName = dt.Rows[0]["ENName"].ToString(), BondVnName = dt.Rows[0]["VNName"].ToString() };
                                var values = new List<BondValue>();
                                foreach (DataRow value in dt.Rows)
                                {
                                    values.Add(new BondValue() { TradeDate = (DateTime)value["TradeDate"], ClosePrice = double.Parse(value["ClosePrice"].ToString()) });
                                }
                                o.BondValues = values;
                                var key = string.Format(RedisKey.BondKey, country, type);
                                if (!bondkeys.Contains(key)) bondkeys.Add(key);
                                if (redis.ContainsKey(key))
                                    redis.Set(key, o);
                                else
                                    redis.Add(key, o);
                            }
                        }
                        foreach(var c in cs)
                        {
                            var existed = redis.SearchKeys(string.Format(RedisKey.BondKey, c, "*"));
                            foreach(var key in existed)
                            {
                                if (bondkeys.Contains(key)) continue;
                                if (redis.ContainsKey(key)) redis.Remove(key);
                            }
                        }
                        

                        #endregion
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                log.WriteEntry(updateKey + " : UpdateList : " + ex.ToString(), EventLogEntryType.Error);
                ret = false;
            }
            return ret;
        }
        private bool UpdateStock(string symbol, List<string> related, ref SqlDb sql)
        {
            var ret = true;
            //log.WriteEntry(symbol + "-1-" + ret, EventLogEntryType.Information);
            try
            {
                var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
                var key = string.Format(RedisKey.Key, symbol.ToUpper());
                var bExisted = (redis.ContainsKey(key));
                var stock = bExisted ? redis.Get<Stock>(key) : new Stock() { Symbol = symbol };
                #region Update stock from sql
                //stock.Symbol = "AAA";
                //load stock data 

                //profile
                var profile = bExisted ? stock.CompanyProfile : new CompanyProfile { Symbol = stock.Symbol };
                var basicInfo = bExisted ? profile.basicInfos : new BasicInfo() { Symbol = stock.Symbol };
                var basicCommon = bExisted ? profile.basicInfos.basicCommon : new BasicCommon() { Symbol = stock.Symbol };
                var category = bExisted ? profile.basicInfos.category : new CategoryObject();
                var firstInfo = bExisted ? profile.basicInfos.firstInfo : new FirstInfo() { Symbol = stock.Symbol };
                var commonInfo = bExisted ? profile.commonInfos : new CommonInfo() { Symbol = stock.Symbol };
                //basic information
                #region SB
                if (!bExisted || related.Contains("SB"))
                {
                    try
                    {
                        var dt = sql.GetSymbolData(symbol);
                        if (dt.Rows.Count <= 0) return true;

                        var row = dt.Rows[0];

                        #region StockCompactInfo

                        var compactkey = string.Format(RedisKey.KeyCompactStock, symbol.ToUpper());
                        var compact = new StockCompactInfo() { Symbol = symbol.ToUpper(), TradeCenterId = int.Parse(row["TradeCenterId"].ToString()), CompanyName = row["CompanyName"].ToString(), EPS = double.Parse(row["EPS"].ToString()), FolderChart = row["FolderChart"].ToString(), ShowTradeCenter = row["ShowTradeCenter"].ToString().ToUpper() == "TRUE", IsBank = row["IsBank"].ToString().ToUpper() == "TRUE", IsCCQ = row["IsCCQ"].ToString().ToUpper() == "TRUE" };
                        if (redis.ContainsKey(compactkey))
                            redis.Set(compactkey, compact);
                        else
                            redis.Add(compactkey, compact);
                        #endregion

                        stock.Symbol = row["Symbol"].ToString();
                        stock.TradeCenterId = int.Parse(row["TradeCenterId"].ToString());
                        stock.IsDisabled = row["IsDisabled"].ToString() == "TRUE";
                        stock.StatusText = row["StatusText"].ToString();
                        stock.ShowTradeCenter = row["ShowTradeCenter"].ToString().ToUpper() == "TRUE";
                        stock.FolderImage = row["FolderChart"].ToString();
                        stock.IsBank = row["IsBank"].ToString() == "TRUE";
                        stock.IsCCQ = row["IsCCQ"].ToString().ToUpper() == "TRUE";

                        //profile - basicInfo
                        basicInfo.Name = row["CompanyName"].ToString();
                        basicInfo.TradeCenter = stock.TradeCenterId.ToString();

                        //profile - basicInfo - basicCommon
                        /*PE = double.Parse(row["PE"].ToString()),*/
                        basicCommon.AverageVolume = double.Parse(row["AVG10SS"].ToString());
                        basicCommon.Beta = double.Parse(row["Beta"].ToString());
                        basicCommon.EPS = double.Parse(row["EPS"].ToString());
                        basicCommon.TotalValue = double.Parse(row["MarketCap"].ToString());
                        basicCommon.ValuePerStock = double.Parse(row["BookValue"].ToString());
                        basicCommon.VolumeTotal = double.Parse(row["SLCPNY"].ToString());
                        basicCommon.OutstandingVolume = double.Parse(row["TotalShare"].ToString());
                        basicCommon.PE = basicCommon.EPS != 0 ? (double.Parse(row["LastPrice"].ToString()) / basicCommon.EPS) : 0;
                        basicCommon.EPSDate = row["EPSDate"].ToString();
                        basicCommon.CCQv3 = double.Parse(row["CCQv3"].ToString());
                        basicCommon.CCQv6 = double.Parse(row["CCQv6"].ToString());
                        basicCommon.CCQdate = DateTime.ParseExact(row["CCQdate"].ToString(), "yyyy.MM.dd", CultureInfo.InvariantCulture, DateTimeStyles.None);
                        basicInfo.basicCommon = basicCommon;

                        //profile - basicInfo - category
                        category.ID = int.Parse(row["CategoryId"].ToString());
                        category.Name = row["CategoryName"].ToString();
                        basicInfo.category = category;

                        //profile - basicInfo - firstInfo
                        firstInfo.FirstPrice = double.Parse(row["FirstPrice"].ToString());
                        firstInfo.FirstTrade = row["FirstTrade"].Equals(DBNull.Value) ? null : ((DateTime?)row["FirstTrade"]);
                        firstInfo.FirstVolume = double.Parse(row["FirstVolume"].ToString());
                        basicInfo.firstInfo = firstInfo;

                        profile.basicInfos = basicInfo;

                        //profile - commonInfo
                        commonInfo.Capital = double.Parse(row["VonDieuLe"].ToString());
                        commonInfo.Category = row["CategoryName"].ToString();
                        commonInfo.Content = row["About"].ToString();
                        commonInfo.OutstandingVolume = double.Parse(row["TotalShare"].ToString());
                        commonInfo.TotalVolume = double.Parse(row["SLCPNY"].ToString());
                        commonInfo.Content += "<p><b>Địa chỉ:</b> " + row["Address"].ToString() + "</p>";
                        commonInfo.Content += "<p><b>Điện thoại:</b> " + row["Phone"].ToString() + "</p>";
                        commonInfo.Content += "<p><b>Người phát ngôn:</b> " + row["Spokenman"].ToString() + "</p>";
                        if (!string.IsNullOrEmpty(row["Email"].ToString())) commonInfo.Content += "<p><b>Email:</b> <a href='mailto:" + row["Email"] + "'>" + row["Email"] + "</a></p>";
                        if (!string.IsNullOrEmpty(row["Website"].ToString())) commonInfo.Content += "<p><b>Website:</b> <a href='" + row["Website"] + "' target='_blank'>" + row["Website"] + "</a></p>";
                        commonInfo.AuditFirmName = row["AuditName"].ToString();
                        commonInfo.AuditFirmSite = row["AuditSite"].ToString().Trim();
                        commonInfo.ConsultantName = row["ConsultantName"].ToString();
                        commonInfo.ConsultantSite = row["ConsultantSite"].ToString();
                        commonInfo.BusinessLicense = row["BusinessLicense"].ToString();
                        commonInfo.ConsultantSymbol = row["ConsultantSymbol"].ToString().Trim().ToUpper();

                        profile.commonInfos = commonInfo;

                        //business plans
                        var plans = new List<BusinessPlan>();
                        if (row["HasPlan"].ToString() == "1")
                        {
                            plans.Add(new BusinessPlan() { Body = row["PlanNote"].ToString(), Date = (DateTime)row["PlanDate"], DividendsMoney = double.Parse(row["Dividend"].ToString()), DividendsStock = double.Parse(row["DivStock"].ToString()), ID = int.Parse(row["PlanId"].ToString()), IncreaseExpected = double.Parse(row["CapitalRaising"].ToString()), ProfitATax = double.Parse(row["NetIncome"].ToString()), ProfitBTax = double.Parse(row["TotalProfit"].ToString()), Revenue = double.Parse(row["TotalIncome"].ToString()), Symbol = stock.Symbol, Year = int.Parse(row["KYear"].ToString()) });
                        }
                        stock.BusinessPlans1 = plans;

                        var fdt = sql.GetPrevTradeInfo(symbol);
                        if (fdt.Rows.Count == 0)
                        {
                            stock.PrevTradeInfo = new List<StockFirstInfo>();
                        }
                        else
                        {
                            var tret = new List<StockFirstInfo>();
                            foreach (DataRow fdr in fdt.Rows)
                            {
                                tret.Add(new StockFirstInfo() { Floor = fdr["Floor"].ToString().ToUpper() == "HASTC" ? "HNX" : fdr["Floor"].ToString(), FirstDate = ((DateTime)fdr["FirstDate"]), FirstVolume = double.Parse(fdr["FirstVolume"].ToString()), FirstPrice = double.Parse(fdr["FirstPrice"].ToString()), EndDate = ((DateTime)fdr["EndDate"]) });
                            }
                            stock.PrevTradeInfo = tret;
                            //log.WriteEntry(symbol + " : BasicInfo : " + fdt.Rows.Count, EventLogEntryType.Error);

                        }
                    }
                    catch (Exception ex)
                    {
                        log.WriteEntry(symbol + " : BasicInfo : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }
                #endregion

                #region SS
                //profile - subsidiaries
                if (!bExisted || related.Contains("SS"))
                {
                    try
                    {


                        var subsidiaries = new List<OtherCompany>();
                        var associates = new List<OtherCompany>();
                        var cdt = sql.GetChildrenCompany(stock.Symbol);
                        var i = 0;
                        foreach (DataRow cdr in cdt.Rows)
                        {
                            i++;
                            var child = new OtherCompany() { Name = cdr["CompanyName"].ToString(), Note = cdr["NoteInfo"].ToString(), OwnershipRate = double.Parse(cdr["Rate"].ToString()), Order = i, SharedCapital = double.Parse(cdr["TotalShareValue"].ToString()), Symbol = stock.Symbol, TotalCapital = double.Parse(cdr["CharterCapital"].ToString()) };
                            if (cdr["isCongTyCon"].ToString() == "1") subsidiaries.Add(child);
                            else associates.Add(child);
                        }
                        profile.Subsidiaries = subsidiaries;
                        profile.AssociatedCompanies = associates;
                    }
                    catch (Exception ex)
                    {
                        log.WriteEntry(symbol + " : Cty con : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }
                #endregion

                #region SF
                //profile - financePeriod
                if (!bExisted || related.Contains("SF"))
                {
                    try
                    {
                        var periods = new List<FinancePeriod>();
                        var financeInfo = new List<FinanceInfo>();

                        var fyt = sql.GetFinancePeriod(stock.Symbol);
                        FinancePeriod period = null;
                        var tmp = 0;
                        foreach (DataRow fyr in fyt.Rows)
                        {
                            if (period == null || tmp != int.Parse(fyr["Year"].ToString()) * 10 + int.Parse(fyr["QuarterType"].ToString()))
                            {
                                if (period != null) { period.UpdateTitle(); periods.Add(period); }
                                period = new FinancePeriod() { Quarter = int.Parse(fyr["QuarterType"].ToString()), Year = int.Parse(fyr["Year"].ToString()) };
                            }
                            tmp = int.Parse(fyr["Year"].ToString()) * 10 + int.Parse(fyr["QuarterType"].ToString());
                            switch (fyr["MaChiTieu"].ToString())
                            {
                                case "Audited":
                                    period.SubTitle = fyr["TieuDeNhom"].ToString();
                                    break;
                                case "QuarterModify":
                                    var qrt = fyr["TieuDeNhom"].ToString();
                                    var qrti = 0;
                                    if (qrt.EndsWith("T")) { period.QuarterTitle = qrt.Remove(qrt.Length - 1) + " tháng"; }
                                    else if (int.TryParse(qrt, out qrti) && qrti >= 1 && qrti < 5)
                                    {
                                        period.QuarterTitle = "Quý " + qrti;
                                    }
                                    else period.QuarterTitle = "";
                                    break;
                                case "YearModify":
                                    if (int.TryParse(fyr["TieuDeNhom"].ToString(), out qrti))
                                    {
                                        period.YearTitle = "Năm " + qrti;
                                    }
                                    else period.YearTitle = "";
                                    break;
                                case "FromDate":
                                    qrt = fyr["TieuDeNhom"].ToString();
                                    if (qrt.Contains("/"))
                                    {
                                        var begin = qrt.Substring(0, qrt.LastIndexOf("/"));
                                        if (begin.Contains("/"))
                                        {
                                            begin = begin.Substring(begin.IndexOf("/") + 1) + "/" + begin.Substring(0, begin.IndexOf("/"));
                                        }
                                        period.BeginTitle = begin;
                                    }
                                    break;
                                case "ToDate":
                                    qrt = fyr["TieuDeNhom"].ToString();
                                    if (qrt.Contains("/"))
                                    {
                                        var end = qrt.Substring(0, qrt.LastIndexOf("/"));
                                        if (end.Contains("/"))
                                        {
                                            end = end.Substring(end.IndexOf("/") + 1) + "/" + end.Substring(0, end.IndexOf("/"));
                                        }
                                        period.EndTitle = end;
                                    }

                                    break;
                                default:
                                    break;
                            }
                        }
                        if (period != null) { period.UpdateTitle(); periods.Add(period); }
                        profile.FinancePeriods = periods;

                        //profile - financeInfo

                        var fit = sql.GetChiTieuFinance(stock.Symbol);
                        var fvt = sql.GetFinanceData(stock.Symbol);
                        var groupId = 0;
                        FinanceInfo info = null;
                        foreach (DataRow fir in fit.Rows)
                        {
                            if (info == null || groupId != int.Parse(fir["LoaiChiTieu"].ToString()))
                            {
                                if (info != null) financeInfo.Add(info);
                                info = new FinanceInfo() { NhomChiTieuId = groupId, Symbol = stock.Symbol, TenNhomChiTieu = fir["TenLoaiChiTieu"].ToString() };
                            }
                            groupId = int.Parse(fir["LoaiChiTieu"].ToString());
                            var chiTieu = new FinanceChiTieu() { ChiTieuId = fir["MaChiTieu"].ToString(), TenChiTieu = fir["TieuDeKhac"].ToString() };
                            if (fir["MaChiTieu"].ToString() == "ROA")
                            {
                                int b = 0;
                            }
                            foreach (var financePeriod in periods)
                            {
                                var fvrs = fvt.Select("MaChiTieu = '" + fir["MaChiTieu"] + "' AND Year = " + financePeriod.Year + " AND QuarterType = " + financePeriod.Quarter);
                                if (fvrs.Length > 0)
                                {
                                    chiTieu.Values.Add(new FinanceValue() { Quarter = financePeriod.Quarter, Year = financePeriod.Year, Value = double.Parse(fvrs[0]["FinanceValue"].ToString()) });
                                }
                                else
                                {
                                    chiTieu.Values.Add(new FinanceValue() { Quarter = financePeriod.Quarter, Year = financePeriod.Year, Value = 0 });
                                }
                            }

                            info.ChiTieus.Add(chiTieu);
                        }
                        if (info != null) financeInfo.Add(info);
                        profile.financeInfos = financeInfo;
                    }
                    catch (Exception ex)
                    {
                        log.WriteEntry(symbol + " : FinanceInfo : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }
                #endregion

                #region SC
                //profile - leader
                if (!bExisted || related.Contains("SC"))
                {
                    try
                    {
                        var leaders = new List<Leader>();
                        var ldt = sql.GetCeos(stock.Symbol);
                        foreach (DataRow ldr in ldt.Rows)
                        {
                            leaders.Add(new Leader() { GroupID = ldr["ParentId"].ToString(), Name = ldr["FullName"].ToString(), Positions = ldr["TenNhom"].ToString() });
                        }
                        profile.Leaders = leaders;
                    }
                    catch (Exception ex)
                    {
                        log.WriteEntry(symbol + " : Leaders : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }
                #endregion

                #region SH
                //profile - owner
                if (!bExisted || related.Contains("SH"))
                {
                    try
                    {
                        var owners = new List<MajorOwner>();
                        var odt = sql.GetShareHolders(stock.Symbol);
                        foreach (DataRow odr in odt.Rows)
                        {
                            owners.Add(new MajorOwner() { Name = odr["FullName"].ToString(), Rate = double.Parse(odr["ShareHoldPct"].ToString()), ToDate = (DateTime)odr["DenNgay"], Volume = double.Parse(odr["SoCoPhieu"].ToString()) });
                        }
                        profile.MajorOwners = owners;
                    }
                    catch (Exception ex)
                    {
                        log.WriteEntry(symbol + " : Owners : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }
                #endregion

                #region SCN
                //profile - CEO
                if (!bExisted || related.Contains("SCN"))
                {
                    try
                    {
                        var ceos = new List<StockCeo>();
                        var cdt = sql.GetCeosNew(stock.Symbol);
                        foreach (DataRow cdr in cdt.Rows)
                        {
                            var ceo = new StockCeo() { CeoId = int.Parse(cdr["CeoId"].ToString()), CeoCode = cdr["CeoCode"].ToString(), GroupID = int.Parse(cdr["PositionType"].ToString()), Name = (cdr["CeoGender"].ToString().ToUpper() == "M" ? "Ông " : (cdr["CeoGender"].ToString().ToUpper() == "F" ? "Bà " : "")) + cdr["CeoName"].ToString(), Positions = cdr["PositionName"].ToString() };
                            ceo.Process = "";
                            //process
                            var process = cdr["CeoProfileShort"].ToString();
                            if (process != "")
                            {
                                var begin = GetCeoDate(process.Substring(0, process.IndexOf("---")));
                                process = process.Substring(process.IndexOf("---") + 3);
                                var end = GetCeoDate(process.Substring(0, process.IndexOf("---")));
                                process = process.Substring(process.IndexOf("---") + 3);
                                var desc = process;
                                process = (begin != "" ? ("Từ " + begin + " ") : "") + (end != "" ? ((begin != "" ? "đến" : "Đến") + " " + end + "") : "");
                                ceo.Process = process + (process == "" ? "" : " : ") + desc;
                                if (ceo.Process.Length > 80)
                                {
                                    ceo.Process = ceo.Process.Substring(0, 80) + "...";
                                }
                            }


                            var birthday = cdr["CeoBirthday"].ToString();
                            if (birthday.Contains("/"))
                            {
                                int year;
                                if (!int.TryParse(birthday.Substring(birthday.LastIndexOf("/") + 1), out year)) year = 0;
                                if (year > 0)
                                {
                                    if (year < 100) year = 1900 + year;
                                    ceo.Age = year;
                                }
                            }
                            ceos.Add(ceo);
                        }
                        profile.AssociatedCeo = ceos;
                    }
                    catch (Exception ex)
                    {
                        log.WriteEntry(symbol + " : CEO New : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }
                #endregion

                stock.CompanyProfile = profile;
                /*================================*/

                #region SD
                //dividend histories
                if (!bExisted || related.Contains("SD"))
                {
                    try
                    {
                        var divs = new List<DividendHistory>();
                        var ddt = sql.GetDividendHistory(stock.Symbol);
                        foreach (DataRow ddr in ddt.Rows)
                        {
                            divs.Add(new DividendHistory() { DonViDoiTuong = ddr["DonViDoiTuong"].ToString(), NgayGDKHQ = (DateTime)ddr["NgayGDKHQ"], GhiChu = ddr["GhiChu"].ToString(), SuKien = ddr["SuKien"].ToString(), Symbol = stock.Symbol, TiLe = ddr["TiLe"].ToString() });
                        }
                        stock.DividendHistorys = divs;
                    }
                    catch (Exception ex)
                    {
                        log.WriteEntry(symbol + " : Dividend : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }
                #endregion

                #region SA
                //báo cáo phân tích
                if (!bExisted || related.Contains("SA"))
                {
                    try
                    {
                        var reports = new List<Reports>();
                        var rdt = sql.GetAnalysisReports(stock.Symbol);
                        foreach (DataRow rdr in rdt.Rows)
                        {
                            reports.Add(new Reports() { ID = int.Parse(rdr["ID"].ToString()), Title = rdr["title"].ToString(), DateDeploy = (DateTime)rdr["PublishDate"], ResourceCode = rdr["Source"].ToString(), IsHot = rdr["IsHot"].ToString().ToLower() == "true" });
                        }
                        stock.Reports3 = reports;
                    }
                    catch (Exception ex)
                    {
                        log.WriteEntry(symbol + " : AnalyseReport : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }
                #endregion

                #region SCA
                //công ty cùng ngành
                if (!bExisted || related.Contains("SCA"))
                {
                    try
                    {
                        var samecateCompanies = new List<StockShortInfo>();
                        var scdt = sql.GetSameCateCompanies(stock.Symbol);
                        foreach (DataRow scdr in scdt.Rows)
                        {
                            samecateCompanies.Add(new StockShortInfo() { Symbol = scdr["StockSymbol"].ToString(), TradeCenterId = int.Parse(scdr["TradeCenterId"].ToString()), Name = scdr["FullName"].ToString(), EPS = double.Parse(scdr["EPS"].ToString()) });
                        }
                        stock.SameCategory = samecateCompanies;
                    }
                    catch (Exception ex)
                    {
                        log.WriteEntry(symbol + " : SameCate : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }
                #endregion

                #region SEPS
                //eps tương đương
                if (!bExisted || related.Contains("SEPS"))
                {
                    try
                    {
                        var sameEPSCompanies = new List<StockShortInfo>();
                        var sedt = sql.GetSameEPSCompanies(stock.Symbol);
                        foreach (DataRow sedr in sedt.Rows)
                        {
                            sameEPSCompanies.Add(new StockShortInfo() { Symbol = sedr["StockSymbol"].ToString(), TradeCenterId = int.Parse(sedr["TradeCenterId"].ToString()), Name = sedr["FullName"].ToString(), EPS = double.Parse(sedr["EPS"].ToString()), MarketValue = double.Parse(sedr["MarketCap"].ToString()) });
                        }
                        stock.SameEPS = sameEPSCompanies;
                    }
                    catch (Exception ex)
                    {
                        log.WriteEntry(symbol + " : SameEPS : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }
                #endregion

                #region SPE
                //pe tương đương
                if (!bExisted || related.Contains("SPE"))
                {
                    try
                    {
                        var samePECompanies = new List<StockShortInfo>();
                        var spdt = sql.GetSamePECompanies(stock.Symbol);
                        foreach (DataRow spdr in spdt.Rows)
                        {
                            samePECompanies.Add(new StockShortInfo() { Symbol = spdr["StockSymbol"].ToString(), TradeCenterId = int.Parse(spdr["TradeCenterId"].ToString()), Name = spdr["FullName"].ToString(), EPS = double.Parse(spdr["EPS"].ToString()), MarketValue = double.Parse(spdr["MarketCap"].ToString()) });
                        }
                        stock.SamePE = samePECompanies;
                    }
                    catch (Exception ex)
                    {
                        log.WriteEntry(symbol + " : SamePE : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }
                #endregion

                //stock history
                var history = bExisted ? stock.StockPriceHistory : new StockCompactHistory();

                #region SP
                //stock history - price
                if (!bExisted || related.Contains("SP"))
                {
                    try
                    {
                        var price = new List<PriceCompactHistory>();
                        var pdt = sql.GetPriceHistory(stock.Symbol, 11);
                        foreach (DataRow pdr in pdt.Rows)
                        {
                            price.Add(new PriceCompactHistory() { ClosePrice = double.Parse(pdr["Price"].ToString()), BasicPrice = double.Parse(pdr["BasicPrice"].ToString()), Ceiling = double.Parse(pdr["Ceiling"].ToString()), Floor = double.Parse(pdr["Floor"].ToString()), Volume = double.Parse(pdr["Volume"].ToString()), TotalValue = double.Parse(pdr["TotalValue"].ToString()), TradeDate = (DateTime)pdr["TradeDate"], AgreedVolume = double.Parse(pdr["AgreedVolume"].ToString()), AgreedValue = double.Parse(pdr["AgreedValue"].ToString()) });
                        }
                        history.Price = price;
                    }
                    catch (Exception ex)
                    {
                        log.WriteEntry(symbol + " : Price : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }
                #endregion

                #region SO
                //stock history - order
                if (!bExisted || related.Contains("SO"))
                {
                    try
                    {
                        var order = new List<OrderCompactHistory>();
                        var hodt = sql.GetOrderHistory(stock.Symbol, 11);
                        foreach (DataRow hodr in hodt.Rows)
                        {
                            order.Add(new OrderCompactHistory() { AskAverageVolume = double.Parse(hodr["AskAverage"].ToString()), AskVolume = double.Parse(hodr["Offer_Volume"].ToString()), BidAverageVolume = double.Parse(hodr["BidAverage"].ToString()), BidVolume = double.Parse(hodr["Bid_Volume"].ToString()), TradeDate = (DateTime)hodr["Trading_Date"] });
                        }
                        history.Orders = order;
                    }
                    catch (Exception ex)
                    {
                        log.WriteEntry(symbol + " : Order : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }
                #endregion

                #region SFT
                //stock history - foreign
                if (!bExisted || related.Contains("SFT"))
                {
                    try
                    {
                        var foreign = new List<ForeignCompactHistory>();
                        var fdt = sql.GetForeignHistory(stock.Symbol, 11);
                        foreach (DataRow fdr in fdt.Rows)
                        {
                            foreign.Add(new ForeignCompactHistory() { BuyValue = double.Parse(fdr["Buying_Value"].ToString()), SellValue = double.Parse(fdr["Selling_Value"].ToString()), NetVolume = double.Parse(fdr["FNetVolume"].ToString()), NetValue = double.Parse(fdr["FNetValue"].ToString()), TradeDate = (DateTime)fdr["Trading_Date"] });
                        }
                        history.Foreign = foreign;
                    }
                    catch (Exception ex)
                    {
                        log.WriteEntry(symbol + " : Foreign : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }
                #endregion

                stock.StockPriceHistory = history;
                /*====================*/

                #region SN
                //tin tức và sự kiện
                if (!bExisted || related.Contains("SN"))
                {
                    try
                    {
                        //var news = new List<StockNews>();
                        //var ndt = sql.GetCompanyNews(stock.Symbol, -1);
                        //foreach (DataRow ndr in ndt.Rows)
                        //{
                        //    news.Add(new StockNews() { DateDeploy = (DateTime)ndr["PostTime"], ID = int.Parse(ndr["Id"].ToString()), Title = ndr["Title"].ToString(), TypeID = ndr["ConfigId"].ToString() });
                        //}
                        //stock.StockNews = news;
                        var keylist = string.Format(RedisKey.KeyCompanyNewsByStock, stock.Symbol, 0); //Tất cả
                        var ls = new List<string>();
                        var pdt = sql.GetCompanyNews(stock.Symbol, -1);

                        var key1 = string.Format(RedisKey.KeyCompanyNewsByStock, stock.Symbol, 1); //Tình hình SXKD & Phân tích khác
                        var key2 = string.Format(RedisKey.KeyCompanyNewsByStock, stock.Symbol, 2); // Cổ tức - Chốt quyền
                        var key3 = string.Format(RedisKey.KeyCompanyNewsByStock, stock.Symbol, 3); // Thay đổi nhân sự
                        var key4 = string.Format(RedisKey.KeyCompanyNewsByStock, stock.Symbol, 4); // Tăng vốn - Cổ phiếu quỹ
                        var key5 = string.Format(RedisKey.KeyCompanyNewsByStock, stock.Symbol, 5); // GD cđ lớn & cđ nội bộ
                        var cate1 = new List<string>();
                        var cate2 = new List<string>();
                        var cate3 = new List<string>();

                        var cate4 = new List<string>();
                        var cate5 = new List<string>();
                        var stocknews = new List<StockNews>();
                        foreach (DataRow rdr in pdt.Rows)
                        {
                            var compact = new StockNews() { ID = int.Parse(rdr["ID"].ToString()), Body = "", DateDeploy = (DateTime)rdr["PostTime"], Image = "", Title = rdr["title"].ToString(), Sapo = "", TypeID = rdr["ConfigId"].ToString(), Symbol = rdr["StockSymbols"].ToString() };
                            var obj = new StockNews() { ID = int.Parse(rdr["ID"].ToString()), Body = rdr["Content"].ToString(), DateDeploy = (DateTime)rdr["PostTime"], Image = rdr["ImagePath"].ToString(), Title = rdr["title"].ToString(), Sapo = rdr["SubContent"].ToString(), TypeID = rdr["ConfigId"].ToString(), Symbol = rdr["StockSymbols"].ToString() };
                            for (int i = 0xD800; i <= 0xDFFF; i++)
                            {
                                obj.Body = obj.Body.Replace((char)i, ' ');
                            }

                            if (stocknews.Count < 6) { stocknews.Add(compact); }
                            var newskey = obj.DateDeploy.ToString("yyyyMMddHHmm") + obj.ID;
                            var compactkey = string.Format(RedisKey.KeyCompanyNewsCompact, obj.ID);
                            var detailkey = string.Format(RedisKey.KeyCompanyNewsDetail, obj.ID);
                            if (redis.ContainsKey(compactkey))
                                redis.Set(compactkey, compact);
                            else
                                redis.Add(compactkey, compact);
                            if (redis.ContainsKey(detailkey))
                                redis.Set(detailkey, obj);
                            else
                                redis.Add(detailkey, obj);

                            if (!ls.Contains(newskey)) ls.Add(newskey);
                            #region Update category list
                            if (obj.TypeID.Contains("1"))
                            {
                                if (!cate1.Contains(newskey)) cate1.Add(newskey);
                            }
                            else
                            {
                                if (cate1.Contains(newskey)) cate1.Remove(newskey);
                            }
                            if (obj.TypeID.Contains("2"))
                            {
                                if (!cate2.Contains(newskey)) cate2.Add(newskey);
                            }
                            else
                            {
                                if (cate2.Contains(newskey)) cate2.Remove(newskey);
                            }
                            if (obj.TypeID.Contains("3"))
                            {
                                if (!cate3.Contains(newskey)) cate3.Add(newskey);
                            }
                            else
                            {
                                if (cate3.Contains(newskey)) cate3.Remove(newskey);
                            }
                            if (obj.TypeID.Contains("4"))
                            {
                                if (!cate4.Contains(newskey)) cate4.Add(newskey);
                            }
                            else
                            {
                                if (cate4.Contains(newskey)) cate4.Remove(newskey);
                            }
                            if (obj.TypeID.Contains("5"))
                            {
                                if (!cate5.Contains(newskey)) cate5.Add(newskey);
                            }
                            else
                            {
                                if (cate5.Contains(newskey)) cate5.Remove(newskey);
                            }
                            #endregion
                        }
                        stock.StockNews = stocknews;

                        ls.Sort();
                        ls.Reverse();
                        if (redis.ContainsKey(keylist))
                            redis.Set(keylist, ls);
                        else
                            redis.Add(keylist, ls);
                        #region Update category list
                        cate1.Sort();
                        cate1.Reverse();
                        if (redis.ContainsKey(key1))
                            redis.Set(key1, cate1);
                        else
                            redis.Add(key1, cate1);
                        cate2.Sort();
                        cate2.Reverse();
                        if (redis.ContainsKey(key2))
                            redis.Set(key2, cate2);
                        else
                            redis.Add(key2, cate2);
                        cate3.Sort();
                        cate3.Reverse();
                        if (redis.ContainsKey(key3))
                            redis.Set(key3, cate3);
                        else
                            redis.Add(key3, cate3);
                        cate4.Sort();
                        cate4.Reverse();
                        if (redis.ContainsKey(key4))
                            redis.Set(key4, cate4);
                        else
                            redis.Add(key4, cate4);
                        cate5.Sort();
                        cate5.Reverse();
                        if (redis.ContainsKey(key5))
                            redis.Set(key5, cate5);
                        else
                            redis.Add(key5, cate5);
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        log.WriteEntry(symbol + " : News : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }
                #endregion

                #region SL
                //if (false)
                if (!bExisted || related.Contains("SL"))
                {
                    try
                    {
                        var lldt = sql.GetLandProject(symbol);
                        var adt = sql.GetLandProject_Area(symbol);
                        var pdt = sql.GetLandProject_Profit(symbol);
                        foreach (DataRow ldr in lldt.Rows)
                        {
                            var bdskey = string.Format(RedisKey.BDSKey, ldr["MaCK"].ToString(),  ldr["MaTienDo"].ToString());
                            TienDoBDS o;
                            try
                            {
                                o = redis.Get<TienDoBDS>(bdskey) ?? new TienDoBDS();
                            }
                            catch (Exception) { o = new TienDoBDS(); }
                            o.MaCK = ldr["MaCK"].ToString(); o.MaTienDo = ldr["MaTienDo"].ToString(); o.TenDuAn = ldr["TenDuAn"].ToString(); o.HinhThucKinhDoanh = ldr["HinhThucKinhDoanh"].ToString(); o.DiaDiem = ldr["DiaDiem"].ToString(); o.ThanhPho = ldr["ThanhPho"].ToString(); o.TongVon = decimal.Parse(ldr["TongVon"].ToString()); o.Donvi = ldr["Donvi"].ToString(); o.TyLeGhopVon = ldr["TyLeGhopVon"].ToString(); o.TyLeDenBu = ldr["TyLeDenBu"].ToString(); o.GhiChu = ldr["GhiChu"].ToString(); o.MoTa = ldr["Mota"].ToString(); o.URL = ldr["URL"].ToString();
                            o.ID = int.Parse(ldr["ID"].ToString());
                            DateTime d;
                            if (DateTime.TryParse(ldr["ViewDate"].ToString(), out d))
                            {
                                o.ViewDate = d;
                            }
                            var adrs = adt.Select("MaTienDo='" + o.MaTienDo + "'");
                            var als = new List<TienDoBDSDienTich>();
                            foreach (var adr in adrs)
                            {
                                als.Add(new TienDoBDSDienTich() { MaTienDo = o.MaTienDo, DienTich = decimal.Parse(adr["DienTich"].ToString()), LoaiDienTich = adr["LoaiDienTich"].ToString() });
                            }
                            o.DienTichs = als;

                            var pdrs = pdt.Select("MaTienDo='" + o.MaTienDo + "'");
                            var pls = new List<TienDoBDSLoiNhuan>();
                            foreach (var pdr in pdrs)
                            {
                                pls.Add(new TienDoBDSLoiNhuan() { MaTienDo = o.MaTienDo, LoiNhuanDoanhThu = decimal.Parse(pdr["LoiNhuanDoanhThu"].ToString()), LoaiLoiNhuan = pdr["LoaiLoiNhuan"].ToString() });
                            }
                            o.LoiNhuans = pls;

                            if (redis.ContainsKey(bdskey))
                                redis.Set(bdskey, o);
                            else
                                redis.Add(bdskey, o);
                        }
                     
                    }
                    catch (Exception ex)
                    {
                        log.WriteEntry(symbol + " : TienDoBDS : " + ex.ToString(), EventLogEntryType.Error);
                        ret = false;
                    }
                }
                #endregion

                #endregion

                if (redis.ContainsKey(key))
                    redis.Set<Stock>(key, stock);
                else
                    redis.Add<Stock>(key, stock);
            }
            catch (Exception ex)
            {
                log.WriteEntry(symbol + ": " + ex.ToString(), EventLogEntryType.Error);
                ret = false;
            }

            return ret;
        }

        private void UpdateTopStock(string center, string type, ref RedisClient redis, ref DataTable pdt)
        {
            var key = string.Format(RedisKey.KeyTopStock, center, type);
            var pdrs = pdt.Select("Center='" + center + "' AND SType = '" + type + "'");
            var ls = new List<TopStock>();
            foreach (var pdr in pdrs)
            {
                ls.Add(new TopStock() { Symbol = pdr["Symbol"].ToString(), Price = double.Parse(pdr["Price"].ToString()), BasicPrice = double.Parse(pdr["BasicPrice"].ToString()), EPS = double.Parse(pdr["EPS"].ToString()), MarketCap = double.Parse(pdr["MarketCap"].ToString()), PE = double.Parse(pdr["PE"].ToString()), Volume = double.Parse(pdr["Volume"].ToString()) });
            }
            if (redis.ContainsKey(key))
                redis.Set(key, ls);
            else
                redis.Add(key, ls);
        }
        public void GetBCTC()
        {
            try
            {
                if ((ConfigurationManager.AppSettings["BCTCAllowance"] ?? "") != "TRUE") return;

                var bfirstTime = true;
                Thread.Sleep(index * 1000);
                var sql = new SqlDb();
                while (ServiceStarted)
                {
                    try
                    {
                        if (!bfirstTime) Thread.Sleep(reportInterval);
                        bfirstTime = false;
                        //log.WriteEntry("BCTC : Started", EventLogEntryType.Information);
                        //string user = ConfigurationManager.AppSettings["user"]??"";
                        //string pass = ConfigurationManager.AppSettings["pass"]??"";
                        //string domain = ConfigurationManager.AppSettings["domain"]??"";
                        //log.WriteEntry("BCTC : " + user + "-" + pass + "-" + domain + "-" + (Update_DanhSach_BCTC.impersonateValidUser(user, domain, pass)), EventLogEntryType.Information);
                        var full = Update_DanhSach_BCTC.GetAllData();

                        if (full.Rows.Count == 0) continue;
                        //log.WriteEntry("BCTC : " + full.Rows[0]["Symbol"], EventLogEntryType.Information);
                        //sql.OpenDb();
                        //var sdt = sql.GetSymbolList(-1);
                        //sql.CloseDb();
                        var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
                        var sls = redis.Get<List<StockCompact>>(RedisKey.KeyStockList) ?? new List<StockCompact>();
                        foreach (var stock in sls)
                        {
                            var symbol = stock.Symbol;
                            //var obj = new FinanceReport() { Symbol = symbol, HtmlContent = Update_DanhSach_BCTC.ReturnHTML_BCTC(full.Select("Symbol = '" + symbol + "'")) };
                            var obj = Update_DanhSach_BCTC.ReturnHTML_BCTC(full.Select("Symbol = '" + symbol + "'"));
                            var key = String.Format(RedisKey.KeyFinanceReport, symbol);
                            if (redis.ContainsKey(key))
                                redis.Set(key, obj);
                            else
                                redis.Add(key, obj);
                        }
                        //log.WriteEntry("BCTC : Done", EventLogEntryType.Information);
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
        public void UpdateCeoImage()
        {
            try
            {
                if ((ConfigurationManager.AppSettings["CeoImageAllowance"] ?? "") != "TRUE") return;

                var bfirstTime = true;
                //Thread.Sleep(index * 1000);
                var sql = new SqlDb();
                var redis = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
                while (ServiceStarted)
                {
                    try
                    {
                        if (!bfirstTime) Thread.Sleep(reportInterval);
                        bfirstTime = false;

                        sql.OpenDb();
                        var ldt = sql.GetAllLandProjects();
                        //var cdt = sql.GetAllCeos();
                        sql.CloseDb();

                        landImages = GetAllLandImages();
                        var keys = redis.SearchKeys(string.Format(RedisKey.BDSKey, "*", "*"));
                        foreach(var key in keys)
                        {
                            var t = key.Substring(key.IndexOf(":tiendocode:"), key.IndexOf(":Object") - key.IndexOf(":tiendocode:")).Replace(":tiendocode:", "");
                            if (ldt.Select("MaTienDo = '" + t + "'").Length == 0)
                            {
                                redis.Remove(key);
                                continue;
                            }
                            var o = redis.Get<TienDoBDS>(key);
                            if(o==null) continue;
                            var s = o.MaTienDo;
                            if (string.IsNullOrEmpty(s)) continue;
                            o.BDSImages = landImages.FindAll(i => i.ToUpper().StartsWith(s));
                            redis.Set(key, o);
                        }

                        ceoPhotos = GetCeoPhotos();
                        foreach (var photo in ceoPhotos)
                        {
                            if (!photo.Contains(".")) continue;
                            var code = photo.Substring(0, photo.IndexOf("."));
                            if (redis.ContainsKey(string.Format(RedisKey.CeoImage, code)))
                            {
                                redis.Set(string.Format(RedisKey.CeoImage, code), photo);
                            }
                            else
                            {
                                redis.Add(string.Format(RedisKey.CeoImage, code), photo);
                            }
                            //var o = redis.Get<Ceo>(string.Format(RedisKey.CeoKey, code));
                            //if (o == null) continue;
                            //o.CeoImage = photo;
                            //redis.Set(string.Format(RedisKey.CeoKey, code), o);
                        }
                        var keywords = SqlDb.GetGoogleTag(-1);
                        var ls = new List<string>();
                        foreach (DataRow keyword in keywords.Rows)
                        {
                            if(!ls.Contains(keyword["keyword"].ToString())) ls.Add(keyword["keyword"].ToString());
                        }
                        //if (redis.ContainsKey(RedisKey.GoogleTag))
                        //    redis.Set(RedisKey.GoogleTag, ls);
                        //else
                        //    redis.Add(RedisKey.GoogleTag, ls);
                    }
                    catch (Exception ex)
                    {
                        log.WriteEntry("UpdateCeoImage - " + ex.ToString(), EventLogEntryType.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                log.WriteEntry("UpdateCeoImage - " + ex.ToString(), EventLogEntryType.Error);
            }
        }
        private string GetCeoDate(string date)
        {
            if (date.ToLower() == "trước đó" || date.ToLower() == "vào thời điểm" || date.ToLower() == "không rõ") return "";
            if (date.ToLower() == "nay" || !date.ToLower().Contains("_")) return date;
            if (date.Substring(6) == "0000") return "";
            if (date.Substring(3, 2) == "00") return "năm " + date.Substring(6);
            if (date.Substring(0, 2) == "00") return "tháng " + date.Substring(3, 2) + " năm " + date.Substring(6);
            return "ngày " + date.Substring(0, 2) + " tháng " + date.Substring(3, 2) + " năm " + date.Substring(6);
        }
        #endregion

        #region FTP
        private bool useFtp = (ConfigurationManager.AppSettings["UseFtp"] ?? "").ToUpper() == "TRUE";
        private string ftpAddress = ConfigurationManager.AppSettings["FTPAddressCeo"] ?? "";
        private string ftpUser = ConfigurationManager.AppSettings["FTPUser"] ?? "";
        private string ftpPass = ConfigurationManager.AppSettings["FTPPassword"] ?? "";
        private List<string> ceoPhotos = new List<string>();
        private List<string> landImages = new List<string>();
        private void UploadCeoPhoto(string srcFile, string desName)
        {
            if (!useFtp) return;

            //Create FTP request
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpAddress + "/" + desName);

            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(ftpUser, ftpPass);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            //Load the file
            FileStream stream = File.OpenRead(srcFile);
            byte[] buffer = new byte[stream.Length];

            stream.Read(buffer, 0, buffer.Length);
            stream.Close();

            //Upload file
            Stream reqStream = request.GetRequestStream();
            reqStream.Write(buffer, 0, buffer.Length);
            reqStream.Close();
        }
        private List<string> GetCeoPhotos()
        {
            try
            {
                bool useFtp = true; // (ConfigurationManager.AppSettings["UseFtp"] ?? "").ToUpper() == "TRUE";
                string ftpAddress = ConfigurationManager.AppSettings["FTPAddressCeo"] ?? "ftp://222.255.27.100/Images/Uploaded/DuLieuDownload/Ceo";
                string ftpUser = ConfigurationManager.AppSettings["FTPUser"] ?? "";
                string ftpPass = ConfigurationManager.AppSettings["FTPPassword"] ?? "";
                string storageFolder = ConfigurationManager.AppSettings["StorageCeo"] ?? "Common/Ceo";
                string webAddress = ConfigurationManager.AppSettings["WebAddressCeo"] ?? "http://images1.cafef.vn/Images/Uploaded/DuLieuDownload/CEO/";

                var files = StorageUtils.Utils.GetFileList(ftpAddress + "/", ftpUser, ftpPass);
                var ret = new List<string>();
                foreach (var file in files)
                {
                    //if (file.StartsWith(MaTienDo, true, CultureInfo.InvariantCulture))
                    //{
                    try
                    {
                        if (StorageUtils.Utils.checkImageExtension(file))
                        {
                            //StorageUtils.Utils.UploadFile(file, storageFolder, ftpAddress, ftpUser, ftpPass);
                            StorageUtils.Utils.UploadFile(webAddress, file, storageFolder);
                            ret.Add(file);
                        }
                    }
                    catch (Exception ex)
                    {
                        log.WriteEntry("GetCeoPhotos - " + file + " - " + ex.ToString(), EventLogEntryType.Error);
                    }
                    //}
                }
                return ret;
            }
            catch (Exception ex)
            {
                log.WriteEntry("GetCeoPhotos - " + ex.ToString(), EventLogEntryType.Error);
                return new List<string>();
            }
        }
        private string GetCeoPhotos(string CeoCode)
        {

            {
                try
                {
                    if (ceoPhotos.Count == 0) ceoPhotos = GetCeoPhotos();
                    var ret = new List<string>();
                    foreach (var file in ceoPhotos)
                    {
                        if (file.StartsWith(CeoCode, true, CultureInfo.InvariantCulture))
                        {
                            try
                            {
                                //if (StorageUtils.Utils.checkImageExtension(file))
                                //{
                                //    //StorageUtils.Utils.UploadFile(file, storageFolder, ftpAddress, ftpUser, ftpPass);
                                //    StorageUtils.Utils.UploadFile(webAddress, file, storageFolder);
                                return file;
                                //}
                            }
                            catch (Exception ex)
                            {
                                log.WriteEntry("GetCeoPhotos - " + CeoCode + " - " + file + " - " + ex.ToString(), EventLogEntryType.Error);
                            }
                        }
                    }
                    return "";
                }
                catch (Exception ex)
                {
                    log.WriteEntry("GetCeoPhotos - " + CeoCode + " - " + ex.ToString(), EventLogEntryType.Error);
                    return "";
                }
            }
        }
        private List<string> GetAllLandImages()
        {
            try
            {
                bool useFtp = true; // (ConfigurationManager.AppSettings["UseFtp"] ?? "").ToUpper() == "TRUE";
                string ftpAddress = ConfigurationManager.AppSettings["FTPAddressTienDoBDS"] ?? "ftp://222.255.27.100/Images/Uploaded/DuLieuDownload/RealEstate/";
                string ftpUser = ConfigurationManager.AppSettings["FTPUser"] ?? "";
                string ftpPass = ConfigurationManager.AppSettings["FTPPassword"] ?? "";
                string storageFolder = ConfigurationManager.AppSettings["StorageTienDoBDS"] ?? "Common/TienDoBDS";
                string webAddress = ConfigurationManager.AppSettings["WebAddressTienDoBDS"] ?? "http://images1.cafef.vn/Images/Uploaded/DuLieuDownload/RealEstate/";

                var files = StorageUtils.Utils.GetFileList(ftpAddress + "/", ftpUser, ftpPass);
                var ret = new List<string>();
                foreach (var file in files)
                {
                    //if (file.StartsWith(MaTienDo, true, CultureInfo.InvariantCulture))
                    //{
                    try
                    {
                        if (StorageUtils.Utils.checkImageExtension(file))
                        {
                            //StorageUtils.Utils.UploadFile(file, storageFolder, ftpAddress, ftpUser, ftpPass);
                            StorageUtils.Utils.UploadFile(webAddress, file, storageFolder);
                            ret.Add(file);
                        }
                    }
                    catch (Exception ex)
                    {
                        log.WriteEntry("GetAllLandImages - " + file + " - " + ex.ToString(), EventLogEntryType.Error);
                    }
                    //}
                }
                return ret;
            }
            catch (Exception ex)
            {
                log.WriteEntry("GetAllLandImages - " + ex.ToString(), EventLogEntryType.Error);
                return new List<string>();
            }
        }
        private List<string> GetLandImages(string MaTienDo)
        {
            try
            {
                if (landImages.Count == 0) landImages = GetAllLandImages();
                var ret = new List<string>();
                foreach (var file in landImages)
                {
                    if (file.StartsWith(MaTienDo, true, CultureInfo.InvariantCulture))
                    {
                        try
                        {
                            //if (StorageUtils.Utils.checkImageExtension(file))
                            //{
                            //    //StorageUtils.Utils.UploadFile(file, storageFolder, ftpAddress, ftpUser, ftpPass);
                            //    StorageUtils.Utils.UploadFile(webAddress, file, storageFolder);
                            ret.Add(file);
                            //}
                        }
                        catch (Exception ex)
                        {
                            log.WriteEntry("GetLandImages - " + MaTienDo + " - " + file + " - " + ex.ToString(), EventLogEntryType.Error);
                        }
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                log.WriteEntry("GetLandImages - " + MaTienDo + " - " + ex.ToString(), EventLogEntryType.Error);
                return new List<string>();
            }
        }
        #endregion
    }
}
