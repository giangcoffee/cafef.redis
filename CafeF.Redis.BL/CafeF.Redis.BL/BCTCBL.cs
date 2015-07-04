using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Data;
using CafeF.Redis.Entity;

namespace CafeF.Redis.BL
{
    public class BCTCBL
    {
        public static List<BCTC> GetTopBCTC(string symbol, string type, int year, int quarter, int count)
        {
            return BCTCDAO.GetTopValues(symbol, type, year, quarter, count);
        }
    }
}
