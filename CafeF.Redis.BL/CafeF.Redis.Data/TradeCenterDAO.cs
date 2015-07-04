using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Entity;
namespace CafeF.Redis.Data
{
    public class TradeCenterDAO
    {
        public static TradeCenterStats getByTradeCenter(int centerid)
        {
            return BLFACTORY.RedisClient.Get<TradeCenterStats>(String.Format(RedisKey.KeyCenterIndex, centerid)); 
        }
    }
}
