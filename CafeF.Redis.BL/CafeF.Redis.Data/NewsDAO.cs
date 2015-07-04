using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Entity;

namespace CafeF.Redis.Data
{
    public class NewsDAO
    {
        private static readonly string key = "newsid:{0}:{1}:{2}:Object";//~ Symbol:TypeID:ID
    }
}
