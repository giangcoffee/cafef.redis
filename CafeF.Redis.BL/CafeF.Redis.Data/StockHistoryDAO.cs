using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Entity;
namespace CafeF.Redis.Data
{
    public class StockHistoryDAO
    {
        public static List<StockHistory> get_StockHistoryBySymbolAndDate(string symbol, DateTime fromDate, DateTime toDate, int PageIndex, int PageCount, out int totalItem)
        {
            var redis = BLFACTORY.RedisClient;
            symbol = symbol.ToUpper();
            List<StockHistory> ret = new List<StockHistory>();
            var keylist = string.Format(RedisKey.PriceHistoryKeys, symbol);
            var ls = redis.ContainsKey(keylist) ? redis.Get<List<String>>(keylist) : new List<string>();
            ls = ls.FindAll(delegate(string sKey) { return ((Convert.ToInt32(sKey.Split(':')[4]) >= Convert.ToInt32(fromDate.ToString("yyyyMMdd"))) && (Convert.ToInt32(sKey.Split(':')[4]) <= Convert.ToInt32(toDate.ToString("yyyyMMdd")))); });
            totalItem = ls.Count;
            ls = ls.GetPaging(PageIndex, PageCount);
            var keys = new List<string>();
            foreach (string hKey in ls)
            {
                //if (BLFACTORY.RedisClient.ContainsKey(hKey))
                //    ret.Add(BLFACTORY.RedisClient.Get<StockHistory>(hKey));
                if (!keys.Contains(hKey)) keys.Add(hKey);
            }
            if (keys.Count > 0) ret = redis.GetAll<StockHistory>(keys).Values.ToList();
            ret.RemoveAll(s => s == null);
            return ret;
        }


        public static StockHistory get_StockHistoryByKey(string symbol, DateTime dt)
        {
            symbol = symbol.ToUpper();
            StockHistory ret = new StockHistory();
            string hKey = String.Format(RedisKey.PriceHistory, symbol, dt.ToString("yyyyMMdd"));
            if (BLFACTORY.RedisClient.ContainsKey(hKey))
                ret = BLFACTORY.RedisClient.Get<StockHistory>(hKey);

            return ret;
        }

        public static List<FundHistory> get_FundHistoryBySymbolAndDate(string symbol, DateTime fromDate, DateTime toDate, int PageIndex, int PageCount, out int totalItem)
        {
            var redis = BLFACTORY.RedisClient;
            symbol = symbol.ToUpper();
            List<FundHistory> ret = new List<FundHistory>();
            var keylist = string.Format(RedisKey.FundHistoryKeys, symbol);
            var ls = redis.ContainsKey(keylist) ? redis.Get<List<String>>(keylist) : new List<string>();
            ls = ls.FindAll(delegate(string sKey) { return ((Convert.ToInt32(sKey.Split(':')[4]) >= Convert.ToInt32(fromDate.ToString("yyyyMMdd"))) && (Convert.ToInt32(sKey.Split(':')[4]) <= Convert.ToInt32(toDate.ToString("yyyyMMdd")))); });
            totalItem = ls.Count;
            ls = ls.GetPaging(PageIndex, PageCount);
            var keys = new List<string>();
            foreach (string hKey in ls)
            {
                //if (BLFACTORY.RedisClient.ContainsKey(hKey))
                //    ret.Add(BLFACTORY.RedisClient.Get<FundHistory>(hKey));
                if (!keys.Contains(hKey)) keys.Add(hKey);
            }
            if(keys.Count>0)ret = redis.GetAll<FundHistory>(keys).Values.ToList();
            ret.RemoveAll(s => s == null);
            return ret;
        }

        public static List<InternalHistory> get_InternalHistoryBySymbolAndDate(string symbol, DateTime fromDate, DateTime toDate, int PageIndex, int PageCount, out int totalItem)
        {
            var redis = BLFACTORY.RedisClient;
            symbol = symbol.ToUpper();
            List<InternalHistory> ret = new List<InternalHistory>();
            var keylist = string.Format(RedisKey.InternalHistoryKeys, symbol);
            var ls = redis.ContainsKey(keylist) ? redis.Get<List<String>>(keylist) : new List<string>();
            ls = ls.FindAll(sKey => ((fromDate == DateTime.MinValue || Convert.ToInt32(sKey.Split(':')[4]) >= Convert.ToInt32(fromDate.ToString("yyyyMMdd"))) && (toDate == DateTime.MaxValue || Convert.ToInt32(sKey.Split(':')[4]) <= Convert.ToInt32(toDate.ToString("yyyyMMdd")))));
            totalItem = ls.Count;
            ls = ls.GetPaging(PageIndex, PageCount);
            var keys = new List<string>();
            foreach (string hKey in ls)
            {
                //if (BLFACTORY.RedisClient.ContainsKey(hKey))
                //{
                //    InternalHistory ih = BLFACTORY.RedisClient.Get<InternalHistory>(hKey);
                //    ih.HolderID = hKey.Substring(hKey.LastIndexOf(":") + 1);
                //    ret.Add(ih);
                //}
                if (!keys.Contains(hKey)) keys.Add(hKey);
            }
            var items = (keys.Count > 0) ? redis.GetAll<InternalHistory>(keys) : new Dictionary<string, InternalHistory>();
            foreach (var item in items)
            {
                if (item.Value != null)
                {
                    var t = item.Value;
                    //t.HolderID = item.Key.Substring(item.Key.LastIndexOf(":") + 1);
                    ret.Add(t);
                }
            }
            return ret;
        }

        public static List<InternalHistory> get_InternalHistoryByHolder(string holderid, int PageIndex, int PageCount, out int totalItem)
        {
            var redis = BLFACTORY.RedisClient;
            List<InternalHistory> ret = new List<InternalHistory>();
            var keylist = redis.SearchKeys(string.Format(RedisKey.InternalHistory, "*", "*", "*", holderid + "*"));
            var keys = new List<string>();
            foreach (string hKey in keylist)
            {
                //if (BLFACTORY.RedisClient.ContainsKey(hKey))
                //{
                //    InternalHistory ih = BLFACTORY.RedisClient.Get<InternalHistory>(hKey);
                //    ih.HolderID = hKey.Substring(hKey.LastIndexOf(":") + 1);
                //    ret.Add(ih);
                //}
                if (!keys.Contains(hKey)) keys.Add(hKey);
            }
            var items = (keys.Count > 0) ? redis.GetAll<InternalHistory>(keys) : new Dictionary<string, InternalHistory>();
            foreach (var item in items)
            {
                if (item.Value != null)
                {
                    var t = item.Value;
                    //t.HolderID = item.Key.Substring(item.Key.LastIndexOf(":") + 1);
                    ret.Add(t);
                }
            }
            totalItem = ret.Count;
            ret.Sort("Stock asc,PlanBeginDate desc ");
            ret = ret.GetPaging(PageIndex, PageCount);
            return ret;
        }

        public static List<StockHistory> get_StockHistoryByCenterAndDate(int centerid, DateTime date)
        {
            var redis = BLFACTORY.RedisClient;
            List<StockHistory> ret = new List<StockHistory>();
            var keylist = string.Format(RedisKey.KeyStockListByCenter, centerid);
            var ls = redis.ContainsKey(keylist) ? redis.Get<List<StockCompact>>(keylist) : new List<StockCompact>();
            var keys = new List<string>();
            foreach (StockCompact sc in ls)
            {
                if (sc.Symbol.ToUpper() == "VNINDEX" || sc.Symbol.ToUpper() == "HNX-INDEX" || sc.Symbol.ToUpper() == "UPCOM-INDEX")
                    continue;
                var keylistprice = string.Format(RedisKey.PriceHistoryKeys, sc.Symbol.ToUpper());
                var lsprice = BLFACTORY.RedisClient.ContainsKey(keylistprice) ? BLFACTORY.RedisClient.Get<List<String>>(keylistprice) : new List<string>();
                lsprice = lsprice.FindAll(delegate(string sKey) { return ((Convert.ToInt32(sKey.Split(':')[4]) == Convert.ToInt32(date.ToString("yyyyMMdd")))); });
                var key = lsprice.Count > 0 ? lsprice[0] : "";
                //if (BLFACTORY.RedisClient.ContainsKey(key))
                //{
                //    StockHistory sh = BLFACTORY.RedisClient.Get<StockHistory>(key);
                //    ret.Add(sh);
                //}
                if(!keys.Contains(key)) keys.Add(key);
            }
            if(keys.Count > 0)ret = redis.GetAll<StockHistory>(keys).Values.ToList();
            ret.RemoveAll(s => s == null);
            return ret;
        }
        public static List<StockNews> GetNewsByStock(string symbol, int configId, int pageIndex, int pageSize)
        {
            var ret = new List<StockNews>();
            var key = string.Format(RedisKey.KeyCompanyNewsByStock, symbol, configId);
            var ls = BLFACTORY.RedisClient.ContainsKey(key) ? BLFACTORY.RedisClient.Get<List<string>>(key) : new List<string>();
            ls = ls.FindAll(sn => (double.Parse(Utility.getDateTime(sn)) <= (double.Parse(DateTime.Now.ToString("yyyyMMddHHmm")))));
            var lspaging = Utility.GetPaging<string>(ls, pageIndex, pageSize);
            for (int i = 0; i < lspaging.Count; i++)
            {
                lspaging[i] = string.Format(RedisKey.KeyCompanyNewsCompact, Utility.getNewsID(lspaging[i]));
            }
            if (lspaging.Count == 0) return new List<StockNews>();
            var tmp = BLFACTORY.RedisClient.GetAll<StockNews>(lspaging);

            foreach (StockNews sn in tmp.Values)
            {
                ret.Add(sn);
            }
            return ret;
        }
        public static List<StockNews> GetNewsByStockList(List<string> symbols, int configId, int pageIndex, int pageSize)
        {
            var ret = new List<StockNews>();
            var keys = new List<string>();
            foreach (var symbol in symbols)
            {
                if (string.IsNullOrEmpty(symbol)) continue;
                var key = string.Format(RedisKey.KeyCompanyNewsByStock, symbol, configId);
                if (!keys.Contains(key)) keys.Add(key);
            }
            var ls = BLFACTORY.RedisClient.GetAll<List<string>>(keys);
            var nkeys = new List<string>();
            foreach (var lsn in ls.Values)
            {
                if (lsn == null || lsn.Count == 0) continue;
                for (var i = 0; i < lsn.Count; i++)
                {
                    if (double.Parse(Utility.getDateTime(lsn[i])) <= (double.Parse(DateTime.Now.ToString("yyyyMMddHHmm")))) { nkeys.Add(lsn[i]); }
                }
            }
            nkeys.Sort();
            nkeys.Reverse();
            nkeys = nkeys.GetPaging(pageIndex, pageSize);
            for (var i = 0; i < nkeys.Count; i++)
            {
                nkeys[i] = string.Format(RedisKey.KeyCompanyNewsCompact, Utility.getNewsID(nkeys[i]));
            }
            if (nkeys.Count == 0) return new List<StockNews>();
            var tmp = BLFACTORY.RedisClient.GetAll<StockNews>(nkeys);

            foreach (var sn in tmp.Values)
            {
                ret.Add(sn);
            }
            return ret;
        }
        public static int GetNewsByStockCount(string symbol, int configId, int pageIndex, int pageSize)
        {
            var key = string.Format(RedisKey.KeyCompanyNewsByStock, symbol, configId);
            var ls = BLFACTORY.RedisClient.ContainsKey(key) ? BLFACTORY.RedisClient.Get<List<string>>(key) : new List<string>();
            ls = ls.FindAll(sn => (double.Parse(Utility.getDateTime(sn)) <= (double.Parse(DateTime.Now.ToString("yyyyMMddHHmm")))));
            return ls.Count;
        }
        public static List<StockNews> GetAllNews(int configId, int pageIndex, int pageSize)
        {
            var ret = new List<StockNews>();
            var key = string.Format(RedisKey.KeyCompanyNewsByCate, configId);
            var keylist = BLFACTORY.RedisClient.ContainsKey(key) ? BLFACTORY.RedisClient.Get<List<string>>(key) : new List<string>();
            keylist = keylist.FindAll(sn => (double.Parse(Utility.getDateTime(sn)) <= (double.Parse(DateTime.Now.ToString("yyyyMMddHHmm")))));
            var lspaging = Utility.GetPaging<string>(keylist, pageIndex, pageSize);
            if (lspaging.Count == 0) lspaging = Utility.GetPaging<string>(keylist, 1, pageSize);
            for (int i = 0; i < lspaging.Count; i++)
            {
                lspaging[i] = string.Format(RedisKey.KeyCompanyNewsCompact, Utility.getNewsID(lspaging[i]));
            }
            if (lspaging.Count == 0) return new List<StockNews>();
            var tmp = BLFACTORY.RedisClient.GetAll<StockNews>(lspaging);
            foreach (StockNews sn in tmp.Values)
            {
                ret.Add(sn);
            }
            return ret;
        }
        public static List<StockNews> GetNewsForPortfolio(List<string> symbols)
        {
            var ret = new List<StockNews>();
            var keys = new List<string>();
            foreach (var symbol in symbols)
            {
                if (string.IsNullOrEmpty(symbol)) continue;
                var key = string.Format(RedisKey.KeyCompanyNewsByStock, symbol, 0); // get all news - configId = 0
                if (!keys.Contains(key)) keys.Add(key);
            }
            var ls = BLFACTORY.RedisClient.GetAll<List<string>>(keys);
            var nkeys = new List<string>();
            foreach (var lsn in ls.Values)
            {
                if (lsn == null || lsn.Count == 0) continue;
                for (var i = 0; i < lsn.Count; i++)
                {
                    if (i >= 10) break; // top 10 news
                    if (double.Parse(Utility.getDateTime(lsn[i])) <= (double.Parse(DateTime.Now.ToString("yyyyMMddHHmm")))) { nkeys.Add(lsn[i]); }
                }
            }
            //nkeys.Sort();
            //nkeys.Reverse();
            //nkeys = nkeys.GetPaging(pageIndex, pageSize);
            for (var i = 0; i < nkeys.Count; i++)
            {
                nkeys[i] = string.Format(RedisKey.KeyCompanyNewsCompact, Utility.getNewsID(nkeys[i]));
            }
            if (nkeys.Count == 0) return new List<StockNews>();
            var tmp = BLFACTORY.RedisClient.GetAll<StockNews>(nkeys);

            foreach (var sn in tmp.Values)
            {
                ret.Add(sn);
            }
            return ret;
        }
        public static List<StockNews> get_StockNewsByList(int pageIndex, int pageSize, out int itemCount)
        {
            itemCount = 0;
            var ret = new List<StockNews>();
            var list = new List<StockNews>();
            if (pageIndex * pageSize < 100)
            {
                var tmp = BLFACTORY.RedisClient.ContainsKey(RedisKey.KeyTop20News) ? BLFACTORY.RedisClient.Get<List<StockNews>>(RedisKey.KeyTop20News) : new List<StockNews>();
                tmp = tmp.FindAll(sn => double.Parse(sn.DateDeploy.ToString("yyyyMMddHHmm")) <= double.Parse(DateTime.Now.ToString("yyyyMMddHHmm")));
                if (tmp.Count > pageSize * pageIndex)
                {
                    for (var i = 0; i < 100; i++)
                    {
                        if (i < (pageIndex - 1) * pageSize) continue;
                        if (i >= tmp.Count || i >= pageIndex * pageSize) break;
                        list.Add(tmp[i]);
                    }
                }
            }
            if (list.Count != 0)
            {
                return list;
            }
            var keylist = BLFACTORY.RedisClient.ContainsKey(string.Format(RedisKey.KeyCompanyNewsByCate, "0")) ? BLFACTORY.RedisClient.Get<List<string>>(string.Format(RedisKey.KeyCompanyNewsByCate, "0")) : new List<string>();
            keylist = keylist.FindAll(sn => (double.Parse(Utility.getDateTime(sn)) <= (double.Parse(DateTime.Now.ToString("yyyyMMddHHmm")))));
            var lspaging = Utility.GetPaging<string>(keylist, pageIndex + 1, pageSize);
            if (lspaging.Count == 0) lspaging = Utility.GetPaging<string>(keylist, 1, pageSize);
            for (int i = 0; i < lspaging.Count; i++)
            {
                lspaging[i] = string.Format(RedisKey.KeyCompanyNewsCompact, Utility.getNewsID(lspaging[i]));
            }
            if (lspaging.Count == 0) return new List<StockNews>();
            var tmp2 = BLFACTORY.RedisClient.GetAll<StockNews>(lspaging);
            foreach (var sn in tmp2.Values)
            {
                ret.Add(sn);
            }
            return ret;
        }
        public static List<StockNews> get_StockNewsByList()
        {
            var redis = BLFACTORY.RedisClient;
            List<StockNews> ret = new List<StockNews>();
            var keylist = redis.ContainsKey(string.Format(RedisKey.KeyCompanyNewsByCate, "0")) ? redis.Get<List<string>>(string.Format(RedisKey.KeyCompanyNewsByCate, "0")) : new List<string>();
            var keys = new List<string>();
            foreach (string hKey in keylist)
            {
                //if (BLFACTORY.RedisClient.ContainsKey(hKey))
                //{
                //    StockNews sn = BLFACTORY.RedisClient.Get<StockNews>(hKey);
                //    if (sn.DateDeploy.CompareTo(DateTime.Now) <= 0)
                //        ret.Add(sn);
                //}
                if(!keys.Contains(hKey)) keys.Add(hKey);
            }
            if (keys.Count > 0) ret = redis.GetAll<StockNews>(keys).Values.ToList();
            ret.RemoveAll(s => s == null);
            //ret.Sort("DateDeploy desc,Symbol asc, Title asc ");
            return ret;
        }

        public static List<StockNews> get_TopOtherStockNewsRelateStock(int currNewsId, string Symbol, int Top)
        {
            Symbol = Symbol.ToUpper();
            //List<StockNews> ret = new List<StockNews>();
            //ret = BLFACTORY.RedisClient.Get<Stock>(String.Format(RedisKey.Key, Symbol)).StockNews.FindAll(delegate(StockNews sn)
            //{
            //    return (sn.DateDeploy.CompareTo(DateTime.Now) <= 0);
            //});
            //StockNews curr = ret.Find(delegate(StockNews sN) { return (sN.ID == currNewsId); });
            //ret.Remove(curr);
            //return ret.Take(Top).ToList();
            var keyls = string.Format(RedisKey.KeyCompanyNewsByStock, Symbol, 0);
            var ls = BLFACTORY.RedisClient.ContainsKey(keyls) ? BLFACTORY.RedisClient.Get<List<string>>(keyls) : new List<string>();
            if (ls.Count == 0) return new List<StockNews>();
            ls = ls.FindAll(sn => double.Parse(Utility.getDateTime(sn)) <= double.Parse(DateTime.Now.ToString("yyyyMMddHHmm")) && int.Parse(Utility.getNewsID(sn)) < currNewsId);
            var keys = new List<string>();
            for (var i = 0; i < Top; i++)
            {
                if (i >= ls.Count) break;
                keys.Add(string.Format(RedisKey.KeyCompanyNewsCompact, Utility.getNewsID(ls[i])));
            }
            if (keys.Count == 0) return new List<StockNews>();
            var tmp = BLFACTORY.RedisClient.GetAll<StockNews>(keys);
            var ret = new List<StockNews>();
            foreach (var t in tmp.Values)
            {
                if (t != null) ret.Add(t);
            }
            return ret;
        }

        public static StockNews get_StockNewsByID(long newsID)
        {
            StockNews sn = new StockNews();
            if (BLFACTORY.RedisClient.ContainsKey(string.Format(RedisKey.KeyCompanyNewsDetail, newsID)))
            {
                sn = BLFACTORY.RedisClient.Get<StockNews>(string.Format(RedisKey.KeyCompanyNewsDetail, newsID));
            }
            return sn;
        }

        public static List<StockNews> get_TopLatestNews(int Top)
        {
            List<StockNews> ret = new List<StockNews>();
            int item = 0;
            ret = get_StockNewsByList(1, Top, out item);
            return ret.Take(Top).ToList();
        }

        public static Dictionary<string, StockHistory> GetStockPriceMultiple(List<string> dates)
        {
            if (dates.Count == 0) return new Dictionary<string, StockHistory>();
            var keys = new List<string>();
            foreach (var sd in dates)
            {
                var s = string.Format(RedisKey.PriceHistoryKeys, sd.Substring(0, sd.IndexOf("-")));
                if (!keys.Contains(s)) keys.Add(s);
            }
            if (keys.Count == 0) return new Dictionary<string, StockHistory>();
            var ls = BLFACTORY.RedisClient.GetAll<List<string>>(keys);
            var mykeys = new List<string>();
            for (var i = 0; i < dates.Count - 1; i++)
            {
                var sym = dates[i].Substring(0, dates[i].IndexOf("-"));
                var date = dates[i].Substring(dates[i].IndexOf("-") + 1);
                var key = string.Format(RedisKey.PriceHistoryKeys, sym);
                if (ls[key] == null) continue;
                var kpls = ls[key];
                var pk = "";
                foreach (var kp in kpls)
                {
                    if (double.Parse(kp.Substring(kp.Length - 8)) > double.Parse(date)) continue;
                    pk = kp;
                    break;
                }
                if (!mykeys.Contains(pk)) mykeys.Add(pk);
            }
            if (mykeys.Count == 0) return new Dictionary<string, StockHistory>();
            var pls = BLFACTORY.RedisClient.GetAll<StockHistory>(mykeys);
            var ret = new Dictionary<string, StockHistory>();
            foreach (var pl in pls)
            {
                if (pl.Value == null) continue;
                var key = pl.Key.Substring(0, pl.Key.IndexOf(":PriceHistory:")).Substring(("stock:stockid:").Length)
                          + "-" + pl.Key.Substring(pl.Key.Length - 8);
                ret.Add(key, pl.Value);
            }
            return ret;
        }
    }
}
