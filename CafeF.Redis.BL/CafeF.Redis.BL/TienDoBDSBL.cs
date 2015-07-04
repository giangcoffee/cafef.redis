using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Entity;
using CafeF.Redis.Data;

namespace CafeF.Redis.BL
{
    public class TienDoBDSBL
    {
        public static TienDoBDS getByCode(string symbol, string code)
        {
            return TienDoBDSDAO.getByCode(symbol, code);
        }

        public static List<TienDoBDS> get_TienDoBDSBySymbol(string symbol)
        {
           return TienDoBDSDAO.get_TienDoBDSBySymbol(symbol);
           
        }
    }
}
