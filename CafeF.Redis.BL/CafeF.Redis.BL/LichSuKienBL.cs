using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Data;
using CafeF.Redis.Entity;

namespace CafeF.Redis.BL
{
    public class LichSuKienBL
    {
        public static List<LichSuKien> get_LichSuKienSearching(bool status, string symbol, int type, DateTime fromDate, DateTime toDate, int PageIndex, int PageCount, out int totalItem)
        {
            return LichSuKienDAO.get_LichSuKienSearching(status, symbol, type, fromDate, toDate, PageIndex, PageCount, out totalItem);
        }

        public static List<LichSuKien> get_LichSuKienByTop(int top)
        {
            return LichSuKienDAO.get_LichSuKienTop(top);
        }
    }
}
