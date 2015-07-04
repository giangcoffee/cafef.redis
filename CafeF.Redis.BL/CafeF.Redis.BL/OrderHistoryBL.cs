using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Data;
using CafeF.Redis.Entity;

namespace CafeF.Redis.BL
{
    public class OrderHistoryBL
    {
        public static List<OrderHistory> get_OrderHistoryBySymbolAndDate(string symbol, DateTime fromDate, DateTime toDate, int PageIndex, int PageCount, out int totalItem)
        {
            return OrderHistoryDAO.get_OrderHistoryBySymbolAndDate(symbol, fromDate, toDate, PageIndex, PageCount, out totalItem);
        }

        public static List<OrderHistory> get_TwoOrderHistoryBySymbolAndDate(string symbol, DateTime date)
        {
            return OrderHistoryDAO.get_TwoOrderHistoryBySymbolAndDate(symbol, date);
        }

        public static List<OrderHistory> get_OrderHistoryByCenterAndDate(int centerid, DateTime date)
        {
            return OrderHistoryDAO.get_OrderHistoryByCenterAndDate(centerid, date);
        }
    }
}
