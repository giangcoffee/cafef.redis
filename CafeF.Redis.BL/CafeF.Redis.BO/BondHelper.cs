using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VCCorp.FinanceChannel.Core.DataUpd;

namespace CafeF.Redis.BO
{
    public class BondHelper
    {
        public static DataTable GetBondByTimeAndCountryAndType(DateTime start, DateTime end, String country, int type)
        {
            string __strConn = System.Configuration.ConfigurationManager.ConnectionStrings["FinanceChannelConnSnap"].ToString();
            object val1 = start == DateTime.MinValue ? "" : (object)start;
            object val2 = end == DateTime.MinValue ? "" : (object)end;
            SqlAccessLayerUpd sqlDal = new SqlAccessLayerUpd(__strConn);
            string[] pPara = new string[] { "@Start", "@End", "@Country", "@Type" };
            object[] pValue = new object[] { start, end, country, type };
            DataTable tbl = sqlDal.ExecuteStoreProcedure("pr_Bond_SelectByTimeAndCountryAndType", pPara, pValue);
            return tbl;
        }
    }
}
