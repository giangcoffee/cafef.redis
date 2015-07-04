using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Data;
using CafeF.Redis.Entity;

namespace CafeF.Redis.BL
{
    public class ReportsBL
    {
        public static List<Reports> get_ReportsSearching(string Symbol, int SourceID, int CategoryID, int pageIndex, int PageSize, out int Total)
        {
            return ReportsDAO.get_ReportsSearching( Symbol,  SourceID,  CategoryID,  pageIndex,  PageSize, out Total);
        }

        public static Reports get_ReportsByKey(int ID)
        {
            return ReportsDAO.get_ReportsByKey(ID);
        }

        public static List<Reports> get_ReportsByTop(int top)
        {
            return ReportsDAO.get_ReportsByTop(top);
        }
    }
}
