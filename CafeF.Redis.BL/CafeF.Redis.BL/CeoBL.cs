using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Entity;
using CafeF.Redis.Data;

namespace CafeF.Redis.BL
{
    public class CeoBL
    {
        public static Ceo getCeoByCode(string code)
        {
            return CeoDAO.getCeoByCode(code);
        }
        public static IDictionary<string, string> GetCeoImage(List<string> codes)
        {
            return CeoDAO.GetCeoImage(codes);
        }
    }
}
