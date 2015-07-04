using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using KenhF.Common;
using KenhF.Engine;
using System.Text;
using System.Collections;
using KenhF.Common;
using System.Collections.Generic;
using VCCorp.FinanceChannel.Core.DataUpd;
namespace CafeF.Redis.BO
{
    /// <summary>
    /// Summary description for GO
    /// </summary>
    public class GO
    {
        public static iDataAccessLayerUpd _dal;
        public static iDataAccessLayerUpd _dalPortfolio;
        private static iCEOEngine _ceoEngine;
        private static iCompanyProfileEngine _companyProfileEngine;
        private static iCompanyPublishInfoEngine _companyPublishInfoEngine;
        private static iFinanceStatementEngine _financeStatementEngine;
        private static iShareHolderEngine _shareHolderEngine;
        private static iStockEngine _stockEngine;
        private static iTradeCenterEngine _tradeCenterEngine;
        private static iCategoryEngine _CategoryEngine;
        private static iPortfolioEngine _PortfolioEngine;
        private static iRemainTransactionEngine _RemainTransactionEngine;
        private static iTradeTransactionEngine _TradeTransactionEngine;
        private static iWatchListEngine _WatchListEngine;
        private static iEventCalendarEngine _EventCalendarEngine;
        private static iPublishInfoEngine _PublishInfoEngine;
        private static KenhF.Engine.PortfolioEngine __PortfolioEngine;
        private static Guid _demoUserID;
        private static iCashTransactionEngine _cashTransactionEngine;
        private static iUserEngine _userEngine;
        private static iNewsEngine _newsEngine;
        private static iOTCEngine _otcEngine;
        private static iForeignTradingEngine _foreignTradingEngine;
        private static iExchangeRateEngine _exchangeRate;
        public static void Finalize()
        {
            string strConn = ConfigurationManager.AppSettings["DBConnectionString"].ToString();
            if (_dal != null && _dal.CurrentConnection.State == ConnectionState.Open)
            {
                _dal.CurrentConnection.Close();
                _dal.CurrentConnection.Dispose();
                _dal = new SqlAccessLayerUpd(strConn);
                Init();
            }
        }
        static GO()
        {
            Init();

        }
        static void Init()
        {
            //string strConn = ConfigurationManager.AppSettings["DBConnectionString"].ToString();
            string strConn = ConfigurationManager.ConnectionStrings["FinanceChannelConnectionString"].ToString();
            _dal = new SqlAccessLayerUpd(strConn);

            string strConn_Portfolio = ConfigurationManager.ConnectionStrings["MasterDB_FinanceChannel"].ToString();
            _dalPortfolio = new SqlAccessLayerUpd(strConn_Portfolio);

            _ceoEngine = new CEOEngine(_dal);
            _companyProfileEngine = new CompanyProfileEngine(_dal);
            _financeStatementEngine = new FinanceStatementEngine(_dal);
            _shareHolderEngine = new ShareHolderEngine(_dal);
            _stockEngine = new StockEngine(_dal, _financeStatementEngine);
            _tradeCenterEngine = new TradeCenterEngine(_dal);
            _CategoryEngine = new CategoryEngine(_dal);


            _PortfolioEngine = new PortfolioEngine(_dalPortfolio, _cashTransactionEngine, _stockEngine);
            _RemainTransactionEngine = new RemainTransactionEngine(_dalPortfolio);
            _TradeTransactionEngine = new TradeTransactionEngine(_dalPortfolio);
            //_WatchListEngine = new WatchListEngine(_dal);
            //_PublishInfoEngine = new PublishInfoEngine(_dal);
            //_EventCalendarEngine = new EventCalendarEngine(_dal);
            __PortfolioEngine = new PortfolioEngine(_dalPortfolio, _cashTransactionEngine, _stockEngine);
            //_demoUserID = new Guid("29b25ab1-ac5c-4d2b-86a3-e317dc11c0fc");
            //_cashTransactionEngine = new CashTransactionEngine(_dal);
            _userEngine = new UserEngine(_dalPortfolio);
            //_newsEngine = new NewsEngine(_dal);
            //_otcEngine = new OTCEngine(_dal);
            //_foreignTradingEngine = new ForeignTradingEngine(_dal);
            //_exchangeRate = new ExchangeRateEngine(_dal);
        }

        public static iExchangeRateEngine ExchangeRateEngine
        {
            get
            {
                return _exchangeRate;
            }
        }

        public static iOTCEngine OTCEngine
        {
            get
            {
                return _otcEngine;
            }
        }

        public static iForeignTradingEngine ForeignTradingEngine
        {
            get
            {
                return _foreignTradingEngine;
            }
        }

        public static iNewsEngine NewsEngine
        {
            get
            {
                return _newsEngine;
            }
        }

        public static iUserEngine UserEngine
        {
            get
            {
                return _userEngine;
            }
        }
        public static iCashTransactionEngine CashTransactionEngine
        {
            get
            {
                return _cashTransactionEngine;
            }
        }
        public static Guid DemoUserID
        {
            get
            {
                return _demoUserID;
            }
        }
        public static iCompanyProfileEngine CompanyProfileEngine
        {
            get
            {
                return _companyProfileEngine;
            }
        }

        public static iStockEngine StockEngine
        {
            get
            {
                return _stockEngine;
            }
        }

        public static iTradeCenterEngine TradeCenterEngine
        {
            get
            {
                return _tradeCenterEngine;
            }
        }

        public static iFinanceStatementEngine FinanceStatementEngine
        {
            get
            {
                return _financeStatementEngine;
            }
        }

        public static iCEOEngine CEOEngine
        {
            get
            {
                return _ceoEngine;
            }
        }

        public static iShareHolderEngine ShareHolderEngine
        {
            get
            {
                return _shareHolderEngine;
            }
        }
        public static iCategoryEngine CategoryEngine
        {
            get
            {
                return _CategoryEngine;
            }
        }
        public static iPortfolioEngine PortfolioEngine
        {
            get
            {
                return _PortfolioEngine;
            }
        }
        public static iRemainTransactionEngine RemainTransactionEngine
        {
            get
            {
                return _RemainTransactionEngine;
            }
        }
        public static iTradeTransactionEngine TradeTransactionEngine
        {
            get
            {
                return _TradeTransactionEngine;
            }
        }
        public static iWatchListEngine WatchListEngine
        {
            get
            {
                return _WatchListEngine;
            }
        }

        public static iEventCalendarEngine EventCalendarEngine
        {
            get
            {
                return _EventCalendarEngine;
            }
        }
        public static iPublishInfoEngine PublishInfoEngine
        {
            get
            {
                return _PublishInfoEngine;
            }
        }
        public static KenhF.Engine.PortfolioEngine PortfolioEngine_Q
        {
            get
            {
                return __PortfolioEngine;
            }
        }
        
        public static bool IsInTradingTime
        {
            get
            {
                bool __inTradeDay = false;
                bool __inTradeTime = false;

                DateTime __currentDateTime = DateTime.Now;
                string[] __listTradeDayInWeek = ConfigurationManager.AppSettings["TradeDayInWeek"].Split(new char[] { ',' });
                string[] __startTime = ConfigurationManager.AppSettings["StartTradeTime"].Split(new char[] { ':' });
                string[] __endTime = ConfigurationManager.AppSettings["EndTradeTime"].Split(new char[] { ':' });
                DayOfWeek __dayOfWeek;
                foreach (string __day in __listTradeDayInWeek)
                {
                    __dayOfWeek = (DayOfWeek)int.Parse(__day);
                    if (__currentDateTime.DayOfWeek == __dayOfWeek)
                        __inTradeDay = true;
                }

                DateTime __startTradeTime = new DateTime(__currentDateTime.Year, __currentDateTime.Month, __currentDateTime.Day, int.Parse(__startTime[0]), int.Parse(__startTime[1]), int.Parse(__startTime[2]));
                DateTime __endTradeTime = new DateTime(__currentDateTime.Year, __currentDateTime.Month, __currentDateTime.Day, int.Parse(__endTime[0]), int.Parse(__endTime[1]), int.Parse(__endTime[2]));

                if (__currentDateTime >= __startTradeTime & __currentDateTime <= __endTradeTime)
                    __inTradeTime = true;
                else
                    __inTradeTime = false;

                return (__inTradeDay & __inTradeTime);
            }
        }

        public static bool IsInHOTradeCenter(string __StockSymbol)
        {
            string __SQL = "SELECT TradeCenterID FROM tblStock WHERE Symbol=@StockSymbol";
            string[] __pName = new string[] { "@StockSymbol" };
            object[] __pValue = new object[] { __StockSymbol };

            DataTable __result = _dal.Execute(__SQL, __pName, __pValue);
            if (__result != null || __result.Rows.Count != 0)
            {
                if ("1".Equals(__result.Rows[0]["TradeCenterID"].ToString()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public class AppConfig
        {
            public static string HaSTCDataFile
            {
                get
                {
                    return ConfigurationManager.AppSettings["HaSTCDataFile"];
                }
            }

            public static string HaSTCMarketDataFile
            {
                get
                {
                    return ConfigurationManager.AppSettings["HaSTCMarketDataFile"];
                }
            }

            public static string HoSTCDataFile
            {
                get
                {
                    return ConfigurationManager.AppSettings["HoSTCDataFile"];
                }
            }

            public static string HoSTCMarketDataFile
            {
                get
                {
                    return ConfigurationManager.AppSettings["HoSTCMarketDataFile"];
                }
            }
        }
    }
}