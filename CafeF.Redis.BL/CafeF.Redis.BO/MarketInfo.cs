using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Channelvn.Cached.Common;
using KenhF.Common;
using MemcachedProviders.Cache;
using Channelvn.Cached;
namespace CafeF.Redis.BO
{
    /// <summary>
    /// 
    /// </summary>
    public class MarketHelper
    {
        public static string Symbol = "n/a";
        public static string __reserveHaCatcheName = "__HaSTC";
        public static string __reserveHoCatcheName = "__HoSTC";
        public static string __reserveHaMarketCatcheName = "__HaSTCMarket";
        public static string __reserveHoMarketCatcheName = "__HoSTCMarket";
        //sharemem cache
        public static string __HaMarketMemCache = "HaMarket";
        public static string __HaStockMemCache = "HaStock";
        public static string __HoMarketMemCache = "HoMarket";
        public static string __HoStockMemCache = "HoStock";

        public static DataTable GetStockPriceByMarket(string Market)
        {
            DataTable __resTable = null;
            if (Market.ToLower() == "hastc")
            {
                __resTable = DistCache.Get<DataTable>("MemCached_HaSTC_PriceData");
            }
            else if (Market.ToLower() == "hose")
            {
                __resTable = DistCache.Get<DataTable>("MemCached_HoSE_PriceData");
            }
            else
            {
                __resTable = DistCache.Get<DataTable>("MemCached_UpCom_PriceData");
            }
            return __resTable;
        }
        public static bool isTradingTime()
        {
            DateTime now = DateTime.Now;

            if (ConfigurationManager.AppSettings["TradeDayInWeek"].IndexOf(Convert.ToInt32(now.DayOfWeek).ToString()) < 0)
            {
                return false;
            }
            else
            {
                string[] __arrHoliday = ConfigurationManager.AppSettings["Holiday"].Split(new char[] { ',' });
                for (int i = 0; i < __arrHoliday.Length; i++)
                {
                    if (now.ToString("dd/MM") == __arrHoliday[i])
                    {
                        return false;
                    }
                }

                string[] start = ConfigurationManager.AppSettings["StartTradeTime"].Split(':');
                string[] end = ConfigurationManager.AppSettings["EndTradeTime"].Split(':');

                DateTime startTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, Convert.ToInt32(start[0]), Convert.ToInt32(start[1]), Convert.ToInt32(start[2]));
                DateTime endTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, Convert.ToInt32(end[0]), Convert.ToInt32(end[1]), Convert.ToInt32(end[2]));

                return (now >= startTime && now <= endTime);
            }
        }
        public static DateTime isTradeInTime()
        {
            DateTime __dResult = DateTime.MaxValue;
            bool isMatched = false;
            try
            {
                string __now = String.Format("{0:dd/MM}", DateTime.Now.Date);
                string[] __arrHoliday = ConfigurationManager.AppSettings["Holiday"].ToString().Split(new char[] { ',' });
                string[] __arrReplaceDate = ConfigurationManager.AppSettings["ReplateDate"].ToString().Split(new char[] { ',' });
                string __ReplaceText = "";
                for (int i = 0; i < __arrHoliday.Length; i++)
                {
                    if (__now == __arrHoliday[i])
                    {
                        isMatched = true;
                        __ReplaceText = __arrReplaceDate[i];
                        break;
                    }
                }
                if (isMatched && __ReplaceText != string.Empty)
                {
                    string[] __tmp = __ReplaceText.Split(new char[] { '/' });
                    int __dd = 0;
                    int __mm = 0;
                    int __yy = int.Parse(ConfigurationManager.AppSettings["CurrentYear"]);
                    if (__tmp.Length >= 1) __dd = int.Parse(__tmp[0]);
                    if (__tmp.Length > 1) __mm = int.Parse(__tmp[1]);
                    if (__dd != 0 && __mm != 0 && __yy != 0)
                    {
                        __dResult = new DateTime(__yy, __mm, __dd);
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return __dResult;
        }
        public static DateTime isTradeInTime(DateTime inDay)
        {
            DateTime __dResult = DateTime.MaxValue;
            bool isMatched = false;
            try
            {
                string __now = String.Format("{0:dd/MM}", inDay);
                string[] __arrHoliday = ConfigurationManager.AppSettings["Holiday"].ToString().Split(new char[] { ',' });
                string[] __arrReplaceDate = ConfigurationManager.AppSettings["ReplateDate"].ToString().Split(new char[] { ',' });
                string __ReplaceText = "";
                for (int i = 0; i < __arrHoliday.Length; i++)
                {
                    if (__now == __arrHoliday[i])
                    {
                        isMatched = true;
                        __ReplaceText = __arrReplaceDate[i];
                        break;
                    }
                }
                if (isMatched && __ReplaceText != string.Empty)
                {
                    string[] __tmp = __ReplaceText.Split(new char[] { '/' });
                    int __dd = 0;
                    int __mm = 0;
                    int __yy = int.Parse(ConfigurationManager.AppSettings["CurrentYear"]);
                    if (__tmp.Length >= 1) __dd = int.Parse(__tmp[0]);
                    if (__tmp.Length > 1) __mm = int.Parse(__tmp[1]);
                    if (__dd != 0 && __mm != 0 && __yy != 0)
                    {
                        __dResult = new DateTime(__yy, __mm, __dd);
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return __dResult;
        }
        public static DataTable ReadMemCache(string CacheName)
        {

            DataTable __resTable = null;
            if (CacheName == Const.SIMPLE_HA_STOCKSDATA)
            {
                __resTable = DistCache.Get<DataTable>("MemCached_HaSTC_PriceData");
            }
            else
            {
                __resTable = DistCache.Get<DataTable>("MemCached_HoSE_PriceData");
            }
            return __resTable;
        }
        public static void ReadStockXML(ref FinanceChannelDB.StockTradeInfoDataTable __StockTable, string xmlpath, string whatxml)
        {
            string __cacheName = "ReadStockXML_" + whatxml;
            try
            {
                switch (whatxml)
                {
                    case "Ha":
                        __cacheName = __reserveHaCatcheName;
                        break;
                    case "Ho":
                        __cacheName = __reserveHoCatcheName;
                        break;
                }
                __StockTable = HttpContext.Current.Cache[__cacheName] as FinanceChannelDB.StockTradeInfoDataTable;
                if (__StockTable == null)
                {
                    __StockTable = new FinanceChannelDB.StockTradeInfoDataTable();
                    __StockTable.ReadXml(xmlpath);
                    HttpContext.Current.Cache.Insert(__cacheName, __StockTable, new System.Web.Caching.CacheDependency(xmlpath));
                }
            }
            catch (Exception ex)
            {

            }
        }
        public static void GetMarketTradeInfo(out DataTable Ha, out DataTable Ho)
        {
            Ha = DistCache.Get<DataTable>("MemCached_HaSTCIndex");
            Ho = DistCache.Get<DataTable>("MemCached_VNIndex");
        }

        public static string UpdateTimer_ReturnDateOnly()
        {
            string __strRes = "";
            //string __cacheName = String.Format("{0:dd/MM/yyy hh}", DateTime.Now);
            DateTime __d = isTradeInTime();
            if (__d != DateTime.MaxValue)
            {
                __strRes += NewsHelper.GetDateVN1(__d, false);
            }
            else
            {
                try
                {

                    DateTime openTime = Convert.ToDateTime("08:00:00 AM");
                    DateTime closeTime = Convert.ToDateTime("11:00:00 AM");
                    DateTime sysTime = DateTime.Now;
                    DateTime preTime = new DateTime(sysTime.Year, sysTime.Month, sysTime.Day);
                    int currDay = (int)DateTime.Now.Date.DayOfWeek;//ngay hien tai
                    if (currDay == 0 || currDay == 6) //ngay nghi
                    {

                        int __date = currDay == 0 ? -2 : -1;
                        __strRes = "";

                        preTime = sysTime.AddDays(__date);//new DateTime(sysTime.Year, sysTime.Month, __date);
                        //kiem tra xem co phai la ngay nghi bat thuong khong 
                        __d = isTradeInTime(preTime);
                        if (__d != DateTime.MaxValue)
                        {
                            __strRes += NewsHelper.GetDateVN1(__d, false);
                        }
                        else
                        {
                            __strRes += NewsHelper.GetDateVN1(preTime, false);
                        }
                    }
                    else
                    {
                        if (DateTime.Compare(sysTime, openTime) >= 0 && DateTime.Compare(sysTime, closeTime) <= 0) //trong gio
                        {
                            __strRes += NewsHelper.GetDateVN1(sysTime, false);
                        }
                        else
                        {
                            if (DateTime.Compare(sysTime, openTime) < 0) //truoc h mo cua
                            {
                                preTime = sysTime.AddDays(-1);// new DateTime(sysTime.Year, sysTime.Month, sysTime.Day - 1);
                                int preDay = (int)preTime.DayOfWeek;
                                int __ShowDay = 0;
                                __ShowDay = preDay == 0 ? -2 : -1;
                                preTime = sysTime.AddDays(__ShowDay);//new DateTime(sysTime.Year, sysTime.Month, __ShowDay);
                                //kiem tra xem co phai ngay nghi bat thuong khong 
                                __d = isTradeInTime(preTime);
                                if (__d != DateTime.MaxValue)
                                {
                                    __strRes += NewsHelper.GetDateVN1(__d, false);
                                }
                                else
                                {
                                    __strRes += NewsHelper.GetDateVN1(preTime, false);
                                }
                            }
                            if (DateTime.Compare(sysTime, closeTime) > 0) //sau h  dong cua
                            {
                                __strRes += NewsHelper.GetDateVN1(sysTime, false);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
            return __strRes;

        }

        public static string GetChartFolder(string symbol)
        {
            string cacheName = "CafeF.ChartLastUpdated";
            string category = ConfigurationManager.AppSettings["PortSoLieu"] ?? "22";
            DataTable dt = (DataTable)CacheController.GetCacheSolieu(cacheName);
            if (dt == null || dt.Rows.Count == 0)
            {
                dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FinanceChannelConnectionString"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT * FROM tblStock_Extend ORDER BY Symbol";
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataAdapter dap = new SqlDataAdapter(cmd))
                        {
                            dap.Fill(dt);
                            CacheController.Add(category, cacheName, dt, 864000);
                        }
                    }
                    conn.Close();
                }
            }
            DataRow[] rows = dt.Select("Symbol='" + symbol + "'");
            if (rows.Length == 0) return DateTime.Now.ToString("yyyyMMdd");
            return rows[0]["CurrentFolder"].ToString();
        }
        
    }

}
