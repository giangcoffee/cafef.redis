using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Entity;

namespace CafeF.Redis.Data
{
    public class OrderHistoryDAO
    {
        public static List<OrderHistory> get_OrderHistoryBySymbolAndDate(string symbol, DateTime fromDate, DateTime toDate, int PageIndex, int PageCount, out int totalItem)
        {
            symbol = symbol.ToUpper();
            List<OrderHistory> ret = new List<OrderHistory>();
            var keylist = string.Format(RedisKey.OrderHistoryKeys, symbol);
            var ls = BLFACTORY.RedisClient.ContainsKey(keylist) ? BLFACTORY.RedisClient.Get<List<String>>(keylist) : new List<string>();
            ls = ls.FindAll(delegate(string sKey) { return ((Convert.ToInt32(sKey.Split(':')[4]) >= Convert.ToInt32(fromDate.ToString("yyyyMMdd"))) && (Convert.ToInt32(sKey.Split(':')[4]) <= Convert.ToInt32(toDate.ToString("yyyyMMdd")))); });
            totalItem = ls.Count;
            ls = ls.GetPaging(PageIndex, PageCount);
            foreach (string hKey in ls)
            {
                if (BLFACTORY.RedisClient.ContainsKey(hKey))
                    ret.Add(BLFACTORY.RedisClient.Get<OrderHistory>(hKey));
            }

            return ret;
        }

        public static List<OrderHistory> get_TwoOrderHistoryBySymbolAndDate(string symbol, DateTime date)
        {
            symbol = symbol.ToUpper();
            List<OrderHistory> ret = new List<OrderHistory>();

            var keylist = string.Format(RedisKey.OrderHistoryKeys, symbol);
            var ls = BLFACTORY.RedisClient.ContainsKey(keylist) ? BLFACTORY.RedisClient.Get<List<String>>(keylist) : new List<string>();
            int count = 0;
            foreach (string hKey in ls)
            {
                if (count == 1)
                {
                    if (BLFACTORY.RedisClient.ContainsKey(hKey))
                        ret.Add(BLFACTORY.RedisClient.Get<OrderHistory>(hKey));
                    break;
                }
                if (hKey == String.Format(RedisKey.OrderHistory, symbol, date.ToString("yyyyMMdd")))
                {
                    if (BLFACTORY.RedisClient.ContainsKey(hKey))
                        ret.Add(BLFACTORY.RedisClient.Get<OrderHistory>(hKey));
                    count = 1;
                }
            }

            return ret;
        }


        public static List<OrderHistory> get_OrderHistoryByCenterAndDate(int centerid, DateTime date)
        {
            List<OrderHistory> ret = new List<OrderHistory>();
            var keylist =(centerid != 8?string.Format(RedisKey.KeyStockListByCenter, centerid): RedisKey.KeyStockList);

            var ls = BLFACTORY.RedisClient.ContainsKey(keylist) ? BLFACTORY.RedisClient.Get<List<StockCompact>>(keylist) : new List<StockCompact>();
            foreach (StockCompact sc in ls)
            {
                if (sc.Symbol.ToUpper() == "VNINDEX" || sc.Symbol.ToUpper() == "HNX-INDEX" || sc.Symbol.ToUpper() == "UPCOM-INDEX")
                    continue;
                var keylistorder = string.Format(RedisKey.OrderHistoryKeys, sc.Symbol.ToUpper());
                var lsorder = BLFACTORY.RedisClient.ContainsKey(keylistorder) ? BLFACTORY.RedisClient.Get<List<String>>(keylistorder) : new List<string>();
                lsorder = lsorder.FindAll(delegate(string sKey) { return ((Convert.ToInt32(sKey.Split(':')[4]) == Convert.ToInt32(date.ToString("yyyyMMdd")))); });
                var key = lsorder.Count > 0 ? lsorder[0] : "";
                if (BLFACTORY.RedisClient.ContainsKey(key))
                {
                    OrderHistory sh = BLFACTORY.RedisClient.Get<OrderHistory>(key);
                    ret.Add(sh);
                }
            }
            return ret;
        }
    }
}
