using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Data;

namespace CafeF.Redis.BL
{
    public class KeywordBL
    {
        public static List<string> GetGoogleKeyword(int top)
        {
            return KeywordDAO.GetGoogleKeyword(top);
        }
    }
}
