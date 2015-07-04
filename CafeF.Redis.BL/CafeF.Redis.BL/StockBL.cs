using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Entity;
using CafeF.Redis.Data;

namespace CafeF.Redis.BL
{
    public class StockBL
    {
        public static StockCompactInfo GetStockCompactInfo(string symbol)
        {
            return StockDAO.GetStockCompactInfo(symbol);
        }
        public static Dictionary<string,StockCompactInfo> GetStockCompactInfoMultiple(List<string> symbols)
        {
            return StockDAO.GetStockCompactInfoMultiple(symbols);
        }
        public static Stock getStockBySymbol(string symbol)
        {
            try
            {
                return StockDAO.getStockBySymbol(symbol);
            }catch(Exception){
                return new Stock();}
        }

        public static StockPrice getStockPriceBySymbol(string symbol)
        {
            return StockDAO.getStockPriceBySymbol(symbol);
        }
        public static IDictionary<string, StockPrice> GetStockPriceMultiple(List<string> symbols)
        {
            if(symbols.Count==0) return new Dictionary<string, StockPrice>();
            return StockDAO.GetStockPriceMultiple(symbols);
        }
        public static AgreementHistory getAgreementHistoryBySymbolAndDate(string symbol, string date)
        {
            return StockDAO.getAgreementHistoryBySymbolAndDate(symbol, date);
        }

        public static string getFinanceReport(string symbol)
        {
            return StockDAO.getFinanceReport(symbol);
        }
        public static string GetKbyFolder()
        {
            return StockDAO.GetKbyFolder();
        }
        public static List<StockCompact> GetStockList(int tradeCenterId)
        {
            return StockDAO.GetStockList(tradeCenterId);
        }

    }
}
