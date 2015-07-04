using System;
using System.Data;
using System.Configuration;
using System.Web;

using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Mail;
using Channelvn.Cached;
using Channelvn.Cached.Common;
namespace CafeF.Redis.BO
{
    public static class KenhFHelper
    {

        public static string __strConn = ConfigurationManager.ConnectionStrings["FinanceChannelConnectionString"].ToString();
        public static string __strConn_MasterDB = ConfigurationManager.ConnectionStrings["MasterDB_FinanceChannel"].ToString();
        public static DataTable GetCompanyProfile(string StockSymbol)
        {
            string cacheName = ConstCachePortfolio.GetCompanyProfile(StockSymbol);
            DataTable __result = NewsHelper_NoCached.GetDataPortfolioFromDistributedCache<DataTable>(cacheName);
            if (__result == null)
            {
                object[] __pValue = new object[] { StockSymbol };
                string[] __pPara = new string[] { "@StockSymbol" };
                VCCorp.FinanceChannel.Core.DataUpd.SqlAccessLayerUpd __sqlDal = new VCCorp.FinanceChannel.Core.DataUpd.SqlAccessLayerUpd(__strConn);
                __result = __sqlDal.ExecuteStoreProcedure(SQL.CompanyProfile.__GetCompanyProfile, __pPara, __pValue);
                __sqlDal.CurrentConnection.Close();
                __sqlDal.CurrentConnection.Dispose();

                CacheController.AddPortfolio(Constants.PORTFOLIO_FOR_DATA_CACHE, cacheName, __result, Constants.PORTFOLIO_TIME_FOR_DATA_CACHE);
            }

            return __result;
        }
       
    }
    internal sealed class SQL
    {
        internal sealed class Portfolio
        {
            internal const string __GetPortfolioListByUser = "FC_GetStockSymbolListByUser";
        }
        internal sealed class News
        {
            internal const string __GetSumarryNewsByStockSymbol = "FC_tblNews_GetSumarryNewsByStockSymbol";
            internal const string __GetNewsDetail = "FC_tblNews_ViewDetails";
            internal const string __GetOtherNews = "FC_tblNews_GetOtherNews";

        }
        internal sealed class CompanyProfile
        {
            internal const string __GetCompanyProfile = "FC_tblCompanyProfile_GetCompanyProfile";
            internal const string __GetCompanyListByIndustryId = "FC_tblCompanyProfile_ListCompanyByIndustry";
            internal const string __CompanyListSearchBy = "FC_tblCompanyProfile_SearchBy";
        }
        internal sealed class Events
        {
            internal const string __GetTopEvent = "FC_tblEventCalendar_TopEvent";
            //internal const string __GetEventByDate = "select top 5 from (select * from tblEventCalendar where EventDate=@Date and Id<>@ID) as a order by Title";
            internal const string __GetEventsBySymbol = "Select top 5   * from tblEventCalendar where StockSymbol=@StockSymbol  order by EventTime desc";
        }
    }
}
