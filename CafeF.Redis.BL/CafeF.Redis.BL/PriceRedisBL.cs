using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Data;
using CafeF.Redis.Entity;

namespace CafeF.Redis.BL
{
    public class PriceRedisBL
    {
        public static StockPrice getStockPriceBySymbol(string symbol)
        {
            return PriceRedisDAO.getStockPriceBySymbol(symbol);
        }
        public static IDictionary<string, StockPrice> GetStockPriceMultiple(List<string> symbols)
        {
            return PriceRedisDAO.GetStockPriceMultiple(symbols);
        }
    }
}
