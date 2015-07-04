using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Channelvn.Cached;
using Channelvn.Cached.Common;
using Channelvn.MemcachedProviders.Cache;

namespace CafeF.DLVM.BO.Cache
{
    public class CacheUtils
    {
        public static T GetChartDataFromDistributedCache<T>(string cacheName)
        {
            return CacheController.IsAllowDistributedCached ? CacheController.Get<T>(Constants.SCREENER_CACHE, cacheName) : default(T);
        }

        public static bool AddChartDataToDistributedCache(string cacheName, object value, long expire)
        {
            long timeExpire = expire;
            string parentCategory = ConfigurationManager.AppSettings["CacheCategory"] ?? "8888";
            if (CacheController.IsAllowDistributedCached)
            {
                string lastUpdateKey = string.Format(Constants.CACHE_NAME_LAST_UPDATE, cacheName);
                DistCached.GetInstance(parentCategory).Add(lastUpdateKey, DateTime.Now.ToString(), timeExpire * 1000);
                return DistCached.GetInstance(parentCategory).Add(cacheName, value, timeExpire * 1000);
            }
            return false;
        }
    }
}
