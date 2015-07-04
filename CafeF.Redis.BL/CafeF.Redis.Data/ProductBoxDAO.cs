using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Entity;

namespace CafeF.Redis.Data
{
    public class ProductBoxDAO
    {
        public static List<ProductBox> GetByTab(int tabId)
        {
            return BLFACTORY.RedisClient.Get<List<ProductBox>>(string.Format(RedisKey.KeyProductBox, tabId));
        }
    }
}
