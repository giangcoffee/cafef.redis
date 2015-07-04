using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Data;
using CafeF.Redis.Entity;

namespace CafeF.Redis.BL
{
    public class KhopLenhBL
    {
        public static List<SessionPriceData> GetBySymbolDate(string symbol, DateTime date)
        {
            return KhopLenhDAO.GetBySymbolDate(symbol, date);
        }
    }
}
