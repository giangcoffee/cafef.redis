using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Channelvn.Cached.Common;
using KenhF.Common;
using Cafef_DAL;
using VCCorp.FinanceChannel.Core.DataUpd;
using System.Web.Caching;
using System.Data.SqlClient;
using Channelvn.Cached;
using System.Collections.Generic;

namespace CafeF.Redis.BO
{
    public class CompanyHelper_Update
    {
        public static Int32 PageSize = 15;
        public static string __strConnNapShop = ConfigurationManager.ConnectionStrings["Cache_FinanceChannel"].ToString();
        public static string __strConn = System.Configuration.ConfigurationManager.ConnectionStrings["FinanceChannelConnectionString"].ToString();
        public static DataTable GetListEventTypeName(string EventList)
        {
            DataTable dtResult = null;
            using (Cafef_DAL.MainDB_Finance db = new MainDB_Finance())
            {
                dtResult = db.FinanceStoredProcedures.GetListEventTypeName(EventList);
            }
            return dtResult;
        }
        public static string GetCompanyInfoLink(string symbol)
        {
            string format_HaSTC = "/hastc/{0}-{1}.chn";
            string format_HoSE = "/hose/{0}-{1}.chn";
            string format_Upcom = "/upcom/{0}-{1}.chn";

            string link = "";

            DataTable __tblCompany = KenhFHelper.GetCompanyProfile(symbol);
            if (__tblCompany != null)
            {
                if (__tblCompany.Rows.Count > 0)
                {
                    if (__tblCompany.Rows[0]["Symbol"].ToString().ToLower() == "hose" || __tblCompany.Rows[0]["Symbol"].ToString() == "hsx")
                    {
                        link = string.Format(format_HoSE, __tblCompany.Rows[0]["StockSymbol"].ToString(), NewsHepler_Update.UnicodeToKoDauAndGach(__tblCompany.Rows[0]["FullName"].ToString()));
                    }
                    else if (__tblCompany.Rows[0]["Symbol"].ToString().ToLower() == "hastc" || __tblCompany.Rows[0]["Symbol"].ToString().ToLower() == "hnx")
                    {
                        link = string.Format(format_HaSTC, __tblCompany.Rows[0]["StockSymbol"].ToString(), NewsHepler_Update.UnicodeToKoDauAndGach(__tblCompany.Rows[0]["FullName"].ToString()));
                    }
                    else
                    {
                        link = string.Format(format_Upcom, __tblCompany.Rows[0]["StockSymbol"].ToString(), NewsHepler_Update.UnicodeToKoDauAndGach(__tblCompany.Rows[0]["FullName"].ToString()));
                    }
                }
            }

            return link;
        }
        public static string FormatColor(double chgIndex, double pctIndex, double basicPrice, double closePrice, double dlCeiling, double dlFloor, bool show)
        {

            string ImgUrl = " <img src='http://cafef3.vcmedia.vn/images/{0}' align='absmiddle' style=\"margin-top:-5px\">&nbsp;";

            string img = Math.Round(pctIndex, 1) == 0 ? String.Format(ImgUrl, "no_change.jpg") : (Math.Round(pctIndex, 1) > 0) ? String.Format(ImgUrl, "btup.gif") : String.Format(ImgUrl, "btdown.gif");

            string strChgIndex = "";
            if (show)
            {
                strChgIndex = String.Format("{0:#,##0.0}", chgIndex) + " (" + String.Format("{0:#,##0.0}", pctIndex) + " %" + ")";
            }
            else
            {
                strChgIndex = String.Format("{0:#,##0.0}", pctIndex) + " %";
            }
            string styleColor = Math.Round(pctIndex, 1) == 0 ? "<span class='Index_NoChange'>" : (Math.Round(pctIndex, 1) > 0) ? "<span class='Index_Up'>" : "<span class='Index_Down'>";

            //if (Math.Round(closePrice, 1) == Math.Round(dlCeiling, 1))
            if (dlCeiling > 0 && Math.Round(closePrice, 1) == Math.Round(dlCeiling, 1))
                styleColor = "<span class='Index_Ceiling'>";
            else
                if (Math.Round(closePrice, 1) == Math.Round(dlFloor, 1))
                    styleColor = "<span class='Index_Floor'>";
            string __return = img + styleColor + strChgIndex + "</span>";

            return __return;

        }
        public static int V2_FC_tblLichSuKien_Search_Old_Total(string symbol, DateTime d1, DateTime d2, int pagesize, int type)
        {
            string __strConn = System.Configuration.ConfigurationManager.ConnectionStrings["MasterDB_FinanceChannel"].ToString();
            object val1 = d1 == DateTime.MinValue ? "" : (object)d1;
            object val2 = d2 == DateTime.MinValue ? "" : (object)d2;
            SqlAccessLayerUpd sqlDal = new SqlAccessLayerUpd(__strConn);
            string[] pPara = new string[] { "@StockSymbol", "@FromDate", "@EndDate", "@Type" };
            object[] pValue = new object[] { symbol, val1, val2, type };
            DataTable __tbl = sqlDal.ExecuteStoreProcedure("V2_FC_tblLichSuKien_Search_Old_Total", pPara, pValue);
            if (__tbl != null && __tbl.Rows.Count > 0)
            {
                int totalR = int.Parse(__tbl.Rows[0][0].ToString());
                int totalP = 0;
                int CountPaging = 0;
                totalP = totalR / pagesize;
                if (totalP % pagesize > 0) totalP++;

                if (totalP == 0) totalP = 1;
                CountPaging = totalP;
                return CountPaging;
            }
            return 0;
        }
        public static int V2_FC_tblLichSuKien_Search_New_Total(string symbol, DateTime d1, DateTime d2, int pagesize, int type)
        {
            string __strConn = System.Configuration.ConfigurationManager.ConnectionStrings["MasterDB_FinanceChannel"].ToString();
            object val1 = d1 == DateTime.MinValue ? "" : (object)d1;
            object val2 = d2 == DateTime.MinValue ? "" : (object)d2;
            SqlAccessLayerUpd sqlDal = new SqlAccessLayerUpd(__strConn);
            string[] pPara = new string[] { "@StockSymbol", "@FromDate", "@EndDate", "@Type" };
            object[] pValue = new object[] { symbol, val1, val2, type };
            DataTable __tbl = sqlDal.ExecuteStoreProcedure("V2_FC_tblLichSuKien_Search_New_Total", pPara, pValue);
            if (__tbl != null && __tbl.Rows.Count > 0)
            {
                int totalR = int.Parse(__tbl.Rows[0][0].ToString());
                int totalP = 0;
                int CountPaging = 0;
                /*
                totalP = totalR / pagesize;
                if (totalP % pagesize > 0) totalP++;

                if (totalP == 0) totalP = 1;
                CountPaging = totalP;
                */

                totalP = (totalR - 1) / pagesize + 1;


                return totalP;
            }
            return 0;
        }
        public static DataTable V2_FC_tblLichSuKien_Search_New_Paging(string symbol, DateTime d1, DateTime d2, int PageIndex, int PageSize, int type)
        {
            string __strConn = System.Configuration.ConfigurationManager.ConnectionStrings["MasterDB_FinanceChannel"].ToString();
            object val1 = d1 == DateTime.MinValue ? "" : (object)d1;
            object val2 = d2 == DateTime.MinValue ? "" : (object)d2;
            SqlAccessLayerUpd sqlDal = new SqlAccessLayerUpd(__strConn);
            string[] pPara = new string[] { "@StartIndex", "@PageSize", "@StockSymbol", "@FromDate", "@EndDate", "@Type" };
            object[] pValue = new object[] { PageIndex, PageSize, symbol, val1, val2, type };
            DataTable tbl = sqlDal.ExecuteStoreProcedure("V2_FC_tblLichSuKien_Search_New_Paging", pPara, pValue);
            return tbl;
        }
        public static DataTable V2_FC_tblLichSuKien_Search_Old_Paging(string symbol, DateTime d1, DateTime d2, int PageIndex, int PageSize, int type)
        {
            string __strConn = System.Configuration.ConfigurationManager.ConnectionStrings["MasterDB_FinanceChannel"].ToString();
            object val1 = d1 == DateTime.MinValue ? "" : (object)d1;
            object val2 = d2 == DateTime.MinValue ? "" : (object)d2;
            SqlAccessLayerUpd sqlDal = new SqlAccessLayerUpd(__strConn);
            string[] pPara = new string[] { "@StartIndex", "@PageSize", "@StockSymbol", "@FromDate", "@EndDate", "@Type" };
            object[] pValue = new object[] { PageIndex, PageSize, symbol, val1, val2,type };
            DataTable tbl = sqlDal.ExecuteStoreProcedure("V2_FC_tblLichSuKien_Search_Old_Paging", pPara, pValue);
            return tbl;
        }
        public static DataTable V2_FC_tblLichSuKien_Search_Paging(string symbol, DateTime d1, DateTime d2, int PageIndex, int PageSize, int type)
        {
            string __strConn = System.Configuration.ConfigurationManager.ConnectionStrings["MasterDB_FinanceChannel"].ToString();
            object val1 = d1 == DateTime.MinValue ? "" : (object)d1;
            object val2 = d2 == DateTime.MinValue ? "" : (object)d2;
            SqlAccessLayerUpd sqlDal = new SqlAccessLayerUpd(__strConn);
            string[] pPara = new string[] { "@StartIndex", "@PageSize", "@StockSymbol", "@FromDate", "@EndDate", "@Type" };
            object[] pValue = new object[] { PageIndex, PageSize, symbol, val1, val2,type };
            DataTable tbl = sqlDal.ExecuteStoreProcedure("V2_FC_tblLichSuKien_Search_Paging", pPara, pValue);
            return tbl;
        }
        public static int V2_FC_tblLichSuKien_Search_Total(string symbol, DateTime d1, DateTime d2,int type)
        {
            string __strConn = System.Configuration.ConfigurationManager.ConnectionStrings["MasterDB_FinanceChannel"].ToString();
            object val1 = d1 == DateTime.MinValue ? "" : (object)d1;
            object val2 = d2 == DateTime.MinValue ? "" : (object)d2;
            SqlAccessLayerUpd sqlDal = new SqlAccessLayerUpd(__strConn);
            string[] pPara = new string[] { "@StockSymbol", "@FromDate", "@EndDate","@Type" };
            object[] pValue = new object[] { symbol, val1, val2,type };
            DataTable __tbl = sqlDal.ExecuteStoreProcedure("V2_FC_tblLichSuKien_Search_Total", pPara, pValue);
            if (__tbl != null && __tbl.Rows.Count > 0)
            {
                int totalR = int.Parse(__tbl.Rows[0][0].ToString());
                int totalP = 0;
                int CountPaging = 0;
                totalP = totalR / PageSize;
                if (totalP % PageSize > 0) totalP++;

                if (totalP == 0) totalP = 1;
                CountPaging = totalP;
                return CountPaging;
            }
            return 0;
        }
        public static DataTable SearchEventCalendar(string symbol, int topEvents, int month, int year)
        {
            //DataTable dtResult = null;
            //using (Cafef_DAL.MainDB_Finance db = new MainDB_Finance())
            //{
            //    dtResult = db.FinanceStoredProcedures.SearchEventCalendar(topEvents, symbol, month, year);
            //}
            //return dtResult;

            string __strConn = System.Configuration.ConfigurationManager.ConnectionStrings["MasterDB_FinanceChannel"].ToString();
            SqlAccessLayerUpd sqlDal = new SqlAccessLayerUpd(__strConn);
            string[] pPara = new string[] { "@top", "@symbol", "@month", "@year" };
            object[] pValue = new object[] { topEvents, symbol, month, year };
            DataTable tbl = sqlDal.ExecuteStoreProcedure("V2_FC_tblLichSuKien_Search", pPara, pValue);
            return tbl;
        }

        public static DataTable H_AnalyzeReportSource_All()
        {
            string key = "H_AnalyzeReportSource_All";
            DataTable tbl = HttpContext.Current.Cache[key] as DataTable;
            if (tbl != null)
                return tbl;
            SqlAccessLayerUpd sqlDal = new SqlAccessLayerUpd(__strConnNapShop);
            tbl = sqlDal.ExecuteStoreProcedure("H_AnalyzeReportSource_All");
            if (tbl != null)
            {
                HttpContext.Current.Cache.Insert(key, tbl, new SqlCacheDependency("FinanceChannel", "AnalyzeReportSource"));
            }
            return tbl;
        }
        public static void H_AnalyseReport_Update_DownloadNum(int ID)
        {
            var __strConn = ConfigurationManager.ConnectionStrings["MasterDB_FinanceChannel"].ToString();
            var __sqlDal = new SqlAccessLayerUpd(__strConn);
            //SqlCacheDependency __sqlDep = new SqlCacheDependency(Const.DATABASE_NAME, "NewsPublished");

            string[] __param = new string[] { "@ID" };
            object[] __value = new object[] { ID };
            __sqlDal.ExecuteStoreProcedure("AnalyzeReport_Update_DownloadNum", __param, __value);
        }
        public static DataTable H_Category_SelectParent()
        {
            string key = "H_tblCategory_SelectParent";
            DataTable tbl = HttpContext.Current.Cache[key] as DataTable;
            if (tbl != null)
                return tbl;
            SqlAccessLayerUpd sqlDal = new SqlAccessLayerUpd(__strConnNapShop);
            tbl = sqlDal.ExecuteStoreProcedure("H_tblCategory_SelectParent");
            if (tbl != null)
            {
                HttpContext.Current.Cache.Insert(key, tbl, new SqlCacheDependency("FinanceChannel", "tblCategory"));
            }
            return tbl;
        }
    }
    internal sealed class CompanyInfo_CacheName
    {
        internal sealed class CompanyProfile
        {
            internal const string CacheName_CompanyProfileInfo = "CACHE_COMPANYPROFILE_{0}"; //theo StockSymbol
            internal const string CacheName_CompanyShareHolder = "CACHE_CompanyShareHolder_{0}"; //theo StockSymbol
            internal const string CacheName_CompanyCEO = "CACHE_CompanyCEO_{0}_{1}"; //theo StockSymbol,groupid
            internal const string CacheName_MemberCompany = "CACHE_CompanyMember_{0}"; //theo StockSymbol,groupid
            internal const string CacheName_FC_StockDataHistory_Paging_no_DateTime = "CACHE_FC_StockDataHistory_Paging_no_DateTime_{0}_{1}_{2}"; //theo StockSymbol,groupid
            internal const string CacheName_tblCompanyFirstInfo_GetBy_Symbol = "CACHE_tblCompanyFirstInfo_GetBy_Symbol_{0}";
            internal const string CacheName_FC_tblCompanyProfile_KLGDRONG = "CACHE_FC_tblCompanyProfile_KLGDRONG_{0}";
            internal const string CacheName_FC_StockDataHistory_Paging = "CACHE_FC_StockDataHistory_Paging_{0}_{1}_{2}_{3}_{4}";
            internal const string CacheName_FC_tblCungCau_Paging = "CACHE_FC_tblCungCau_Paging_{0}_{1}_{2}_{3}_{4}";
            internal const string CacheName_TraCoTuc_GetBySymbol = "CACHE_TraCoTuc_GetBySymbol_{0}";

        }
        internal sealed class FinancialReport
        {
            internal const string CacheName_CompanyTargetGroup = "CACHE_COMPANYTargetGroup";
            internal const string CacheName_CompanyFinanceStatementTop4LastestQuarterType = "CACHE_COMPANYFinanceStatementTop4LastestQuarterType_{0}_{1}"; //stock, viewbyyear
            internal const string CacheName_CompanyFinanceStatementTop4LastestQuarterTypeIndex = "CACHE_COMPANYFinanceStatementTop4LastestQuarterTypeIndex_{0}_{1}_{2}"; //stock, viewbyyear, index
            internal const string CacheName_CompanyFinanceStatementViewByYear = "CACHE_COMPANYFinanceStatementViewByYear_{0}_{1}"; //stock, TargetType
            internal const string CacheName_CompanyFinanceStatementViewByYearIndex = "CACHE_COMPANYFinanceStatementViewByYearIndex_{0}_{1}_{2}"; //stock, TargetType, index
            internal const string CacheName_CompanyFinanceStatementViewByQuarter = "CACHE_COMPANYFinanceStatementViewByQuarter_{0}_{1}"; //stock, TargetType
            internal const string CacheName_CompanyFinanceStatementViewByQuarterIndex = "CACHE_COMPANYFinanceStatementViewByQuarterIndex_{0}_{1}_{2}"; //stock, TargetType, index
            
        }
    }
    internal sealed class CompanyInfo_CodeDescription
    {
        internal sealed class StockCodeDescription
        {
            internal const string SX = "Soát xét";
            internal const string KT = "Kiểm toán";
            internal const string HN = "Hợp nhất";
            internal const string M = "Công ty mẹ";
            internal const string HNSX = "Hợp nhất soát xét";
            internal const string HNKT = "Hợp nhất kiểm toán";
            internal const string MSX = "Soát xét Cty mẹ";
            internal const string MKT = "Kiểm toán Cty mẹ";
        }
    }

}


