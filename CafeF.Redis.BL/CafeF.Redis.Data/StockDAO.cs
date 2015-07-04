using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Entity;
namespace CafeF.Redis.Data
{
    public class StockDAO
    {
        /// <summary>
     /// Với 1 object ~ 1 table có bao nhiêu thuộc tính ~ Field sẽ tạo ra bấy nhiêu sortedset tương ứng (max ~ 100 item)
     /// Khi có thay đổi thêm thì sẽ đẩy ID đó vào và đẩy 1 item ra
     /// </summary>
     /// <param name="symbol"></param>
     /// <returns></returns>

        public static Stock getStockBySymbol(string symbol)
        {
            symbol = symbol.ToUpper();
            //return (Stock)Utility.Deserialize<Stock>(BLFACTORY.RedisClient.GetString(String.Format(key, symbol))); 
            return BLFACTORY.RedisClient.Get<Stock>(String.Format(RedisKey.Key, symbol)); 
        }
        /// <summary>
        /// Lấy giá của mã (trước khi cập nhật từ Commetd)
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public static StockPrice getStockPriceBySymbol(string symbol)
        {
            symbol = symbol.ToUpper();
            //return (Stock)Utility.Deserialize<Stock>(BLFACTORY.RedisClient.GetString(String.Format(key, symbol))); 
            return BLFACTORY.RedisClient.Get<StockPrice>(String.Format(RedisKey.PriceKey, symbol));
        }
        public static IDictionary<string,StockPrice> GetStockPriceMultiple(List<string> symbols)
        { 
            if(symbols.Count==0) return new Dictionary<string, StockPrice>();
            var ss = new List<string>();
            ss.AddRange(symbols);
            for(var i=0; i<symbols.Count; i++)
            {
                ss[i] = string.Format(RedisKey.PriceKey, symbols[i]);
            }
            var tmp = BLFACTORY.RedisClient.GetAll<StockPrice>(ss);
            var ret = new Dictionary<string, StockPrice>();
            foreach(var symbol in symbols)
            {
                ret.Add(symbol, tmp[string.Format(RedisKey.PriceKey, symbol)]);
            }
            return ret;
        }

        public static AgreementHistory getAgreementHistoryBySymbolAndDate(string symbol, string date)
        {
            symbol = symbol.ToUpper();
            return BLFACTORY.RedisClient.Get<AgreementHistory>(String.Format(RedisKey.KeyAgreementHistory, symbol, date));
           
        }

        public static string getFinanceReport(string symbol)
        {
            symbol = symbol.ToUpper();
            return BLFACTORY.RedisClient.Get<string>(String.Format(RedisKey.KeyFinanceReport, symbol));

        }
        public static string GetKbyFolder()
        {
            return BLFACTORY.RedisClient.Get<string>(RedisKey.KeyKby);
        }
        public static StockCompactInfo GetStockCompactInfo(string symbol)
        {
            return BLFACTORY.RedisClient.Get<StockCompactInfo>(string.Format(RedisKey.KeyCompactStock, symbol.ToUpper()));
        }
        public static Dictionary<string, StockCompactInfo> GetStockCompactInfoMultiple(List<string> symbols)
        {
            if (symbols.Count == 0) return new Dictionary<string, StockCompactInfo>();
            var ss = new List<string>();
            ss.AddRange(symbols);
            for (var i = 0; i < symbols.Count; i++)
            {
                ss[i] = string.Format(RedisKey.KeyCompactStock, symbols[i]);
            }
            var tmp = BLFACTORY.RedisClient.GetAll<StockCompactInfo>(ss);
            var ret = new Dictionary<string, StockCompactInfo>();
            foreach (var symbol in symbols)
            {
                ret.Add(symbol, tmp[string.Format(RedisKey.KeyCompactStock, symbol)]);
            }
            return ret;
        }
        public static List<StockCompact> GetStockList(int tradeCenterId)
        {
            var key = tradeCenterId > 0 ? string.Format(RedisKey.KeyStockListByCenter, tradeCenterId) : RedisKey.KeyStockList;
            return BLFACTORY.RedisClient.Get<List<StockCompact>>(key);
        }
    }
}
