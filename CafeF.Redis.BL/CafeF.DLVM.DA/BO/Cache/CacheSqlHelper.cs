using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace CafeF.DLVM.BO.Cache
{
    public class CacheSqlHelper
    {
        public class CacheNameFormat
        {
            #region Dulieuvimo
            public static string pr_Chart_Month_CPI(Int64 parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
            {
                return "pr_Chart_Month_CPI_" + parentid + "_" + time1 + "_" + time2 + "_" + time3 + "_" + time4 + "_" + year1 + "_" + year2 + "_" + year3;
            }

            public static string pr_Chart_Quarter(Int64 parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
            {
                return "pr_Chart_Quarter_" + parentid + "_" + time1 + "_" + time2 + "_" + time3 + "_" + time4 + "_" + year1 + "_" + year2 + "_" + year3;
            }

            public static string pr_Chart_Year(Int64 parentid, int time1, int time2)
            {
                return "pr_Chart_Year_" + parentid + "_" + time1 + "_" + time2;
            }

            public static string pr_IndexQuarter_SelectByIDAndTime(Int64 id, int time, int year)
            {
                return "pr_IndexQuarter_SelectByIDAndTime_" + id + "_" + time + "_" + year;
            }

            public static string pr_IndexYear_SelectByIDAndTime(Int64 id, int time)
            {
                return "pr_IndexYear_SelectByIDAndTime_" + id + "_" + time;
            }

            public static string pr_Chart_Month(Int64 parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
            {
                return "pr_Chart_Month_" + parentid + "_" + time1 + "_" + time2 + "_" + time3 + "_" + time4 + "_" + year1 + "_" + year2 + "_" + year3;
            }

            public static string pr_IndexMonth_SelectByIDAndTime(Int64 id, int time, int year)
            {
                return "pr_IndexMonth_SelectByIDAndTime_" + id + "_" + time + "_" + year;
            }

            public static string pr_Chart_MonthCompare(Int64 parentid, int time1, int time2, int year1, int year2, string code)
            {
                return "pr_Chart_MonthCompare_" + parentid + "_" + time1 + "_" + time2 + "_" + year1 + "_" + year2 + "_" + code;
            }

            public static string pr_IndexMonth_SelectByIDAndTimeForQuy(int id, int time1, int time2, int year)
            {
                return "pr_IndexMonth_SelectByIDAndTimeForQuy_" + id + "_" + time1 + "_" + time2 + "_" + year;
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
