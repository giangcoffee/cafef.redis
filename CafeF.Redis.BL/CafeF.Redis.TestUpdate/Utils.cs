using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace CafeF.Redis.TestUpdate
{
    public class Lib
    {
        public static bool InTradingTime(int centerId)
        {
            string tradingDate = ConfigurationManager.AppSettings["TradeDayInWeek"] ?? "1,2,3,4,5";
            string holiday = ConfigurationManager.AppSettings["Holiday"] ?? "02/09";
            DateTime startTime, endTime;
            if (!DateTime.TryParseExact(ConfigurationManager.AppSettings["StartTradeTime_" + centerId] ?? "081500", "HHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out startTime)) startTime = new DateTime(0, 0, 0, 8, 0, 0);
            if (!DateTime.TryParseExact(ConfigurationManager.AppSettings["EndTradeTime_" + centerId] ?? "120000", "HHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out endTime)) endTime = new DateTime(0, 0, 0, 12, 0, 0);

            if (("," + holiday + ",").Contains("," + DateTime.Now.ToString("dd/MM") + ",")) return false;
            if (!("," + tradingDate + ",").Contains("," + (int)DateTime.Now.DayOfWeek + ",")) return false;
            if (int.Parse(DateTime.Now.ToString("HHmmss")) < int.Parse(startTime.ToString("HHmmss"))) return false;
            if (int.Parse(DateTime.Now.ToString("HHmmss")) > int.Parse(endTime.ToString("HHmmss"))) return false;
            return true;
        }
        public static string Serialize(DataTable dt)
        {
            var ds = new DataSet("DS");
            ds.Tables.Add(dt);
            string result;
            using (var sw = new StringWriter())
            {
                ds.WriteXml(sw);
                result = sw.ToString();
            }
            return result;
        }
        public static DataTable Deserialize(string s)
        {
            var reader = new StringReader(s);
            var dt = new DataSet();
            dt.ReadXml(reader);
            return dt.Tables[0];
        }

    }
}
