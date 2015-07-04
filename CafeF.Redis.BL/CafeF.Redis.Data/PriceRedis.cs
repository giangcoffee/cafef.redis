using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Entity;

namespace CafeF.Redis.Data
{
    public class PriceRedisDAO
    {
        /// <summary>
        /// Lấy giá của mã (trước khi cập nhật từ Commetd)
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public static StockPrice getStockPriceBySymbol(string symbol)
        {
            symbol = symbol.ToUpper();
            //return (Stock)Utility.Deserialize<Stock>(BLFACTORY.RedisClient.GetString(String.Format(key, symbol))); 
            return BLFACTORY.RedisPriceClient.Get<StockPrice>(String.Format(RedisKey.PriceKey, symbol));
        }
        public static IDictionary<string, StockPrice> GetStockPriceMultiple(List<string> symbols)
        {
            if (symbols.Count == 0) return new Dictionary<string, StockPrice>();
            var ss = new List<string>();
            ss.AddRange(symbols);
            for (var i = 0; i < symbols.Count; i++)
            {
                ss[i] = string.Format(RedisKey.PriceKey, symbols[i]);
            }
            var tmp = BLFACTORY.RedisPriceClient.GetAll<StockPrice>(ss);
            var ret = new Dictionary<string, StockPrice>();
            foreach (var symbol in symbols)
            {
                ret.Add(symbol, tmp[string.Format(RedisKey.PriceKey, symbol)]);
            }
            return ret;
        }
    }
}
