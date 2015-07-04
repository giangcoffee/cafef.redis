using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Data;
using CafeF.Redis.Entity;

namespace CafeF.Redis.BL
{
    public class StockHistoryBL
    {
        public static List<StockHistory> get_StockHistoryBySymbolAndDate(string symbol, DateTime fromDate, DateTime toDate, int PageIndex, int PageCount, out int totalItem)
        {
            return StockHistoryDAO.get_StockHistoryBySymbolAndDate(symbol, fromDate, toDate, PageIndex, PageCount, out totalItem);
        }

        public static StockHistory get_StockHistoryByKey(string symbol, DateTime dt)
        {
            return StockHistoryDAO.get_StockHistoryByKey(symbol, dt);
        }

        public static List<FundHistory> get_FundHistoryBySymbolAndDate(string symbol, DateTime fromDate, DateTime toDate, int PageIndex, int PageCount, out int totalItem)
        {
            return StockHistoryDAO.get_FundHistoryBySymbolAndDate(symbol, fromDate, toDate, PageIndex, PageCount, out totalItem);
        }

        public static List<InternalHistory> get_InternalHistoryBySymbolAndDate(string symbol, DateTime fromDate, DateTime toDate, int PageIndex, int PageCount, out int totalItem)
        {
            return StockHistoryDAO.get_InternalHistoryBySymbolAndDate(symbol, fromDate, toDate, PageIndex, PageCount, out totalItem);
        }

        public static List<InternalHistory> get_InternalHistoryByHolder(string holderid,int PageIndex, int PageCount, out int totalItem)
        {
            return StockHistoryDAO.get_InternalHistoryByHolder(holderid, PageIndex,  PageCount, out  totalItem);
        }
        public static List<StockHistory> get_StockHistoryByCenterAndDate(int centerid, DateTime date)
        {
            return StockHistoryDAO.get_StockHistoryByCenterAndDate(centerid, date);
        }

        public static List<StockNews> get_StockNewsByList()
        {
            return StockHistoryDAO.get_StockNewsByList();
        }
        public static List<StockNews> get_StockNewsByList(int pageIndex, int pageSize, out int itemCount)
        {
            return StockHistoryDAO.get_StockNewsByList(pageIndex,pageSize, out itemCount);
        }

        public static List<StockNews> get_TopOtherStockNewsRelateStock(int currNewsId, string Symbol, int Top)
        {
            return StockHistoryDAO.get_TopOtherStockNewsRelateStock(currNewsId, Symbol, Top);
        }

        public static StockNews get_StockNewsByID(long newsID)
        {
            return StockHistoryDAO.get_StockNewsByID( newsID);
        }

        public static List<StockNews> get_TopLatestNews(int Top)
        {
            return StockHistoryDAO.get_TopLatestNews(Top);
        }

        public static List<StockNews> GetAllNews(int configId, int pageIndex, int pageSize)
        {
            return StockHistoryDAO.GetAllNews(configId, pageIndex, pageSize);
        }
        public static List<StockNews> GetNewsByStock(string symbol, int configId, int pageIndex, int pageSize)
        {
            return StockHistoryDAO.GetNewsByStock(symbol, configId, pageIndex, pageSize);
        }
        public static List<StockNews> GetNewsByStockList(List<string> symbols, int configId, int pageIndex, int pageSize)
        {
            return StockHistoryDAO.GetNewsByStockList(symbols, configId, pageIndex, pageSize);
        }
        public static List<StockNews> GetNewsForPortfolio(List<string> symbols)
        {
            return StockHistoryDAO.GetNewsForPortfolio(symbols);
        }
        public static IDictionary<string, StockHistory> GetStockPriceMultiple(List<string>dates)
        {
            if (dates.Count == 0) return new Dictionary<string, StockHistory>();
            return StockHistoryDAO.GetStockPriceMultiple(dates);
        }
    }
}
