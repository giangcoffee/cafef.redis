using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Entity;

namespace CafeF.Redis.Data
{
    public class BCTCDAO
    {
        public BCTCDAO() {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol">Mã</param>
        /// <param name="type">Loại báo cáo : IncSta, BSheet, CashFlow, CashFlowDirect</param>
        /// <param name="fromYear">từ năm</param>
        /// <param name="fromQuarter">từ quý : = 0 ~ báo cáo năm</param>
        /// <param name="count">Số kỳ muốn lấy</param>
        /// <returns></returns>
        public static List<BCTC> GetTopValues(string symbol, string type, int fromYear, int fromQuarter, int count)
        {
            var redis = BLFACTORY.RedisClient;
            var keys = redis.SearchKeys(string.Format(RedisKey.BCTCKey, symbol.Trim().ToUpper(), type.Trim().ToUpper(), fromQuarter > 0 ? 1 : 0, "*", "*"));
            keys.Sort();
            keys.Reverse();
            var i = 0;
            while (i < keys.Count)
            {
                if (i >= count) { keys.RemoveAt(i); continue; }
                var tmp = keys[i].Substring(keys[i].Length - 6);
                var year = int.Parse(tmp.Substring(0, 4));
                var quarter = int.Parse(tmp.Substring(5));
                if (year > fromYear) { keys.RemoveAt(i); continue; }
                if (year == fromYear && quarter > fromQuarter) { keys.RemoveAt(i); continue; }
                i++;
            }
            return keys.Count==0 ? new List<BCTC>() : redis.GetAll<BCTC>(keys).Values.ToList();
        }
    }
}
