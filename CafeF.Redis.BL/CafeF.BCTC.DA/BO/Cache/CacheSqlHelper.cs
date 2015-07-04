using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace CafeF.BCTC.BO.Cache
{
    public class CacheSqlHelper
    {
        public class CacheNameFormat
        {
            #region BCTC
            public static string sp_CafeF_DataCrawler_GetFinanceValue(string symbol, string type, int year, int quarter)
            {
                return "sp_CafeF_DataCrawler_GetFinanceValue_" + symbol + "_" + type + "_" + year + "_" + quarter;
            }

            public static string sp_CafeF_DataCrawler_GetFinanceValue_Recent4part(string symbol, string type, int year, int quarter, string showtype)
            {
                return "sp_CafeF_DataCrawler_GetFinanceValue_Recent4part_" + symbol + "_" + type + "_" + year + "_" + quarter + "_" + showtype;
            }

            public static string sp_CafeF_DataCrawler_GetFinanceValue_Recent4year(string symbol, string type, int year, string showtype)
            {
                return "sp_CafeF_DataCrawler_GetFinanceValue_Recent4part_" + symbol + "_" + type + "_" + year + "_" + showtype;
            }
            #endregion
        }

        #region Cache methods
        public static void SaveToCacheDependency(string database, string tableName, string cacheName, object data)
        {
            SqlCacheDependency sqlDep = new SqlCacheDependency(database, tableName);
            HttpContext.Current.Cache.Insert(cacheName, data, sqlDep);
        }
        public static void SaveToCacheDependency(string database, string[] tableName, string cacheName, object data)
        {
            AggregateCacheDependency aggCacheDep = new AggregateCacheDependency();
            SqlCacheDependency[] sqlDepGroup = new SqlCacheDependency[tableName.Length];
            for (int i = 0; i < tableName.Length; i++)
            {
                sqlDepGroup[i] = new SqlCacheDependency(database, tableName[i]);

            }
            aggCacheDep.Add(sqlDepGroup);
            HttpContext.Current.Cache.Insert(cacheName, data, aggCacheDep);
        }
        public static T GetFromCacheDependency<T>(string cacheName)
        {
            if (DAL.GetKeyConfig.AllowSqlCache == "1")
            {
                object data = HttpContext.Current.Cache[cacheName];
                if (null != data)
                {
                    try
                    {
                        return (T)data;
                    }
                    catch
                    {
                        return default(T);
                    }
                }
                else
                {
                    return default(T);
                }
            }
            else
                return default(T);
        }

        public static T GetFromCacheDependencySqlCache<T>(string cacheName)
        {
            object data = HttpContext.Current.Cache[cacheName];
            if (null != data)
            {
                try
                {
                    return (T)data;
                }
                catch
                {
                    return default(T);
                }
            }
            else
            {
                return default(T);
            }
        }

        public static T GetFromDistributedCache<T>(string cacheName)
        {
            if (Channelvn.Cached.CacheController.IsAllowDistributedCached)
            {
                return Channelvn.Cached.CacheController.Get<T>(Channelvn.Cached.Common.Constants.PARENT_CATEGORY_FOR_DATA_CACHE,
                                            cacheName);
            }
            else
            {
                return default(T);
            }
        }
        
        #endregion
    }
}
