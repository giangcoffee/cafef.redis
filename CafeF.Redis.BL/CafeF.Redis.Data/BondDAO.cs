using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Entity;

namespace CafeF.Redis.Data
{
    public class BondDAO
    {
        public BondDAO(){}

        public static Bond GetBond(string country, string type)
        {
            var redis = BLFACTORY.RedisClient;
            return redis.Get<Bond>(string.Format(RedisKey.BondKey, country, type));
        }
    }
}
