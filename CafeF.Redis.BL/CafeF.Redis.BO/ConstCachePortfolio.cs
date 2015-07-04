using System;
using System.Collections.Generic;
using System.Text;

namespace CafeF.Redis.BO
{
    public class ConstCachePortfolio
    {
        public static string GetPortfolioByUserID(string UserID)
        {           
            return "GetPortfolioByUserID_" + UserID;
        }

        public static string GainLossByPortfolioId(long PortfolioId)
        {            
            return "GainLossByPortfolioId_" + PortfolioId.ToString();
        }

        public static string GetCompanyProfile(string StockSymbol)
        {            
            return "GetCompanyProfile_" + StockSymbol;
        }

        public static string Performance(string userid)
        {
            return "Performance_" + userid;
        }

        public static string Portfolio_User_Alert_GetSetting(string userid)
        {
            return "Portfolio_User_Alert_GetSetting_" + userid;
        }
    }
}
