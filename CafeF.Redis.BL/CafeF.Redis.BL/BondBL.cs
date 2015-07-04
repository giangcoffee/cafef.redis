using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Data;
using CafeF.Redis.Entity;

namespace CafeF.Redis.BL
{
    public class BondBL
    {
        public BondBL(){}
        public static Bond GetBond(string country, string type)
        {
            return BondDAO.GetBond(country, type);
        }
    }
}
