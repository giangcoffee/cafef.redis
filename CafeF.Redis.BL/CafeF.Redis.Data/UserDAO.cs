using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Entity;

namespace CafeF.Redis.Data
{
    public class UserDAO
    {
        private static readonly string key = "userid:{0}:Object";
    }
}
