using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Entity;

namespace CafeF.Redis.Data
{
    public class KhopLenhDAO
    {
        public static List<SessionPriceData> GetBySymbolDate(string symbol, DateTime date)
        {
            var redis = BLFACTORY.RedisClient;
            var keys = redis.SearchKeys(string.Format(RedisKey.SessionPrice, symbol.ToUpper(), date.ToString("yyyyMMdd"), "*"));
            if (keys == null || keys.Count == 0) return new List<SessionPriceData>();
            var ret = new List<SessionPriceData>();
            var objs = redis.GetAll<SessionPriceData>(keys);
            foreach (var o in objs.Values)
            {
                if(o!=null) ret.Add(o);
            }
            ret.Sort("TradeDate DESC");
            return ret;
        }
    }
}
