using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Entity;

namespace CafeF.Redis.Data
{
    public class ForeignHistoryDAO
    {
        public static List<ForeignHistory> get_ForeignHistoryBySymbolAndDate(string symbol, DateTime fromDate, DateTime toDate, int PageIndex, int PageCount, out int totalItem)
        {
            var redis = BLFACTORY.RedisClient;
            symbol = symbol.ToUpper();
            List<ForeignHistory> ret = new List<ForeignHistory>();
            var keylist = string.Format(RedisKey.ForeignHistoryKeys, symbol);
            var ls = redis.ContainsKey(keylist) ? redis.Get<List<String>>(keylist) : new List<string>();
            ls = ls.FindAll(delegate(string sKey) { return ((Convert.ToInt32(sKey.Split(':')[4]) >= Convert.ToInt32(fromDate.ToString("yyyyMMdd"))) && (Convert.ToInt32(sKey.Split(':')[4]) <= Convert.ToInt32(toDate.ToString("yyyyMMdd")))); });
            totalItem = ls.Count;
            ls = ls.GetPaging(PageIndex, PageCount);
            var keys = new List<string>();
            foreach (string hKey in ls)
            {
                //if (BLFACTORY.RedisClient.ContainsKey(hKey))
                //    ret.Add(BLFACTORY.RedisClient.Get<ForeignHistory>(hKey));
                if(!keys.Contains(hKey)) keys.Add(hKey);
            }
            if (keys.Count > 0) ret = redis.GetAll<ForeignHistory>(keys).Values.ToList();
            ret.RemoveAll(s => s == null);
            return ret;
        }

        public static List<ForeignHistory> get_OneForeignHistoryBySymbolAndDate(string symbol, DateTime date)
        {
            symbol = symbol.ToUpper();
            List<ForeignHistory> ret = new List<ForeignHistory>();
            string hKey = String.Format(RedisKey.ForeignHistory, symbol, date.ToString("yyyyMMdd"));
            if (BLFACTORY.RedisClient.ContainsKey(hKey))
                ret.Add(BLFACTORY.RedisClient.Get<ForeignHistory>(hKey));

            return ret;
        }

      


        public static List<ForeignHistory> get_ForeignHistoryByCenterAndDate(int centerid, DateTime date)
        {
            var redis = BLFACTORY.RedisClient;
            List<ForeignHistory> ret = new List<ForeignHistory>();
            var keylist = (centerid != 8 ? string.Format(RedisKey.KeyStockListByCenter, centerid) : RedisKey.KeyStockList);

            var ls = redis.ContainsKey(keylist) ? redis.Get<List<StockCompact>>(keylist) : new List<StockCompact>();
            var keys = new List<string>();
            foreach (StockCompact sc in ls)
            {
                if (sc.Symbol.ToUpper() == "VNINDEX" || sc.Symbol.ToUpper() == "HNX-INDEX" || sc.Symbol.ToUpper() == "UPCOM-INDEX")
                    continue;
                var keylistforeign = string.Format(RedisKey.ForeignHistoryKeys, sc.Symbol.ToUpper());
                var lsforeign = BLFACTORY.RedisClient.ContainsKey(keylistforeign) ? BLFACTORY.RedisClient.Get<List<String>>(keylistforeign) : new List<string>();
                lsforeign = lsforeign.FindAll(delegate(string sKey) { return ((Convert.ToInt32(sKey.Split(':')[4]) == Convert.ToInt32(date.ToString("yyyyMMdd")))); });
                var key = lsforeign.Count > 0 ? lsforeign[0] : "";
                //if (BLFACTORY.RedisClient.ContainsKey(key))
                //{
                //    ForeignHistory fh = BLFACTORY.RedisClient.Get<ForeignHistory>(key);
                //    ret.Add(fh);
                //}
                if(!keys.Contains(key)) keys.Add(key);
            }
            if (keys.Count > 0) ret = redis.GetAll<ForeignHistory>(keys).Values.ToList();
            ret.RemoveAll(s => s == null);
            return ret;
        }
    }
}
