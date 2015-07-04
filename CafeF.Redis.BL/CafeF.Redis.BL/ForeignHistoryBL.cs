using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Data;
using CafeF.Redis.Entity;

namespace CafeF.Redis.BL
{
    public class ForeignHistoryBL
    {
        public static List<ForeignHistory> get_ForeignHistoryBySymbolAndDate(string symbol, DateTime fromDate, DateTime toDate, int PageIndex, int PageCount, out int totalItem)
        {
            return ForeignHistoryDAO.get_ForeignHistoryBySymbolAndDate(symbol, fromDate, toDate, PageIndex, PageCount, out totalItem);
        }

        public static List<ForeignHistory> get_OneForeignHistoryBySymbolAndDate(string symbol, DateTime date)
        {
            return ForeignHistoryDAO.get_OneForeignHistoryBySymbolAndDate(symbol, date);
        }


        public static List<ForeignHistory> get_ForeignHistoryByCenterAndDate(int centerid, DateTime date)
        {
            return ForeignHistoryDAO.get_ForeignHistoryByCenterAndDate(centerid, date);
        }
    }
}
