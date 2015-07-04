using System;
using System.Data;
using System.Web;
using System.Configuration;
using CafeF.DLVM.DAL;
using CafeF.DLVM.BO.Cache;
using CafeF.DLVM.BO.Utilitis;

namespace CafeF.DLVM.DA.BO.DuLieuViMo
{
    public class IndexBO
    {
        public class IndexNoCacheSql
        {
            public static DataTable pr_Chart_Month_CPI(Int64 parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
            {
                DataTable __result = new DataTable();
                using (MainDB db = new MainDB())
                {
                    __result = db.StoredProcedures.pr_Chart_Month_CPI(parentid, time1, time2, time3, time4, year1, year2, year3);
                    db.Close();
                }

                return __result;
            }

            public static DataTable pr_Chart_Quarter(Int64 parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
            {
                DataTable __result = new DataTable();
                using (MainDB db = new MainDB())
                {
                    __result = db.StoredProcedures.pr_Chart_Quarter(parentid, time1, time2, time3, time4, year1, year2, year3);
                    db.Close();
                }

                return __result;
            }

            public static DataTable pr_Chart_Year(Int64 parentid, int time1, int time2)
            {
                DataTable __result = new DataTable();
                using (MainDB db = new MainDB())
                {
                    __result = db.StoredProcedures.pr_Chart_Year(parentid, time1, time2);
                    db.Close();
                }

                return __result;
            }

            public static DataTable pr_IndexQuarter_SelectByIDAndTime(Int64 id, int time, int year)
            {
                DataTable __result = new DataTable();
                using (MainDB db = new MainDB())
                {
                    __result = db.StoredProcedures.pr_IndexQuarter_SelectByIDAndTime(id, time, year);
                    db.Close();
                }

                return __result;
            }

            public static DataTable pr_IndexYear_SelectByIDAndTime(Int64 id, int time)
            {
                DataTable __result = new DataTable();
                using (MainDB db = new MainDB())
                {
                    __result = db.StoredProcedures.pr_IndexYear_SelectByIDAndTime(id, time);
                    db.Close();
                }

                return __result;
            }

            public static DataTable pr_Chart_Month(Int64 parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
            {
                DataTable __result = new DataTable();
                using (MainDB db = new MainDB())
                {
                    __result = db.StoredProcedures.pr_Chart_Month(parentid, time1, time2, time3, time4, year1, year2, year3);
                    db.Close();
                }

                return __result;
            }

            public static DataTable pr_IndexMonth_SelectByIDAndTime(Int64 id, int time, int year)
            {
                DataTable __result = new DataTable();
                using (MainDB db = new MainDB())
                {
                    __result = db.StoredProcedures.pr_IndexMonth_SelectByIDAndTime(id, time, year);
                    db.Close();
                }

                return __result;
            }

            public static DataTable pr_Chart_MonthCompare(Int64 parentid, int time1, int time2, int year1, int year2, string code)
            {
                DataTable __result = new DataTable();
                using (MainDB db = new MainDB())
                {
                    __result = db.StoredProcedures.pr_Chart_MonthCompare(parentid, time1, time2, year1, year2, code);
                    db.Close();
                }

                return __result;
            }

            public static DataTable pr_IndexMonth_SelectByIDAndTimeForQuy(int id, int time1, int time2, int year)
            {
                DataTable __result = new DataTable();
                using (MainDB db = new MainDB())
                {
                    __result = db.StoredProcedures.pr_IndexMonth_SelectByIDAndTimeForQuy(id, time1, time2, year);
                    db.Close();
                }

                return __result;
            }
        }

        public class IndexCacheSql
        {
            public static DataTable pr_Chart_Month_CPI(Int64 parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
            {
                string cacheName = CacheSqlHelper.CacheNameFormat.pr_Chart_Month_CPI(parentid, time1, time2, time3, time4, year1, year2, year3);
                DataTable __result = CacheSqlHelper.GetFromCacheDependency<DataTable>(cacheName);
                if (__result == null)
                {
                    __result = IndexNoCacheSql.pr_Chart_Month_CPI(parentid, time1, time2, time3, time4, year1, year2, year3);
                    CacheSqlHelper.SaveToCacheDependency(Const.DATABASE_NAME, new string[] { Const.TABLE_INDEXMONTH, Const.TABLE_CATEGORY }, cacheName, __result);
                }

                return __result;
            }

            public static DataTable pr_Chart_Quarter(Int64 parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
            {
                string cacheName = CacheSqlHelper.CacheNameFormat.pr_Chart_Quarter(parentid, time1, time2, time3, time4, year1, year2, year3);
                DataTable __result = CacheSqlHelper.GetFromCacheDependency<DataTable>(cacheName);
                if (__result == null)
                {
                    __result = IndexNoCacheSql.pr_Chart_Quarter(parentid, time1, time2, time3, time4, year1, year2, year3);
                    CacheSqlHelper.SaveToCacheDependency(Const.DATABASE_NAME, new string[] { Const.TABLE_INDEXQUARTER, Const.TABLE_CATEGORY }, cacheName, __result);
                }

                return __result;
            }

            public static DataTable pr_Chart_Year(Int64 parentid, int time1, int time2)
            {
                string cacheName = CacheSqlHelper.CacheNameFormat.pr_Chart_Year(parentid, time1, time2);
                DataTable __result = CacheSqlHelper.GetFromCacheDependency<DataTable>(cacheName);
                if (__result == null)
                {
                    __result = IndexNoCacheSql.pr_Chart_Year(parentid, time1, time2);
                    CacheSqlHelper.SaveToCacheDependency(Const.DATABASE_NAME, new string[] { Const.TABLE_INDEXYEAR, Const.TABLE_CATEGORY }, cacheName, __result);
                }

                return __result;
            }

            public static DataTable pr_IndexQuarter_SelectByIDAndTime(Int64 id, int time, int year)
            {
                string cacheName = CacheSqlHelper.CacheNameFormat.pr_IndexQuarter_SelectByIDAndTime(id, time, year);
                DataTable __result = CacheSqlHelper.GetFromCacheDependency<DataTable>(cacheName);
                if (__result == null)
                {
                    __result = IndexNoCacheSql.pr_IndexQuarter_SelectByIDAndTime(id, time, year);
                    CacheSqlHelper.SaveToCacheDependency(Const.DATABASE_NAME, Const.TABLE_INDEXQUARTER, cacheName, __result);
                }

                return __result;
            }

            public static DataTable pr_IndexYear_SelectByIDAndTime(Int64 id, int time)
            {
                string cacheName = CacheSqlHelper.CacheNameFormat.pr_IndexYear_SelectByIDAndTime(id, time);
                DataTable __result = CacheSqlHelper.GetFromCacheDependency<DataTable>(cacheName);
                if (__result == null)
                {
                    __result = IndexNoCacheSql.pr_IndexYear_SelectByIDAndTime(id, time);
                    CacheSqlHelper.SaveToCacheDependency(Const.DATABASE_NAME, Const.TABLE_INDEXYEAR, cacheName, __result);
                }

                return __result;
            }

            public static DataTable pr_Chart_Month(Int64 parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
            {
                string cacheName = CacheSqlHelper.CacheNameFormat.pr_Chart_Month(parentid, time1, time2, time3, time4, year1, year2, year3);
                DataTable __result = CacheSqlHelper.GetFromCacheDependency<DataTable>(cacheName);
                if (__result == null)
                {
                    __result = IndexNoCacheSql.pr_Chart_Month(parentid, time1, time2, time3, time4, year1, year2, year3);
                    CacheSqlHelper.SaveToCacheDependency(Const.DATABASE_NAME, new string[] { Const.TABLE_INDEXMONTH, Const.TABLE_CATEGORY }, cacheName, __result);
                }

                return __result;
            }

            public static DataTable pr_IndexMonth_SelectByIDAndTime(Int64 id, int time, int year)
            {
                string cacheName = CacheSqlHelper.CacheNameFormat.pr_IndexMonth_SelectByIDAndTime(id, time, year);
                DataTable __result = CacheSqlHelper.GetFromCacheDependency<DataTable>(cacheName);
                if (__result == null)
                {
                    __result = IndexNoCacheSql.pr_IndexMonth_SelectByIDAndTime(id, time, year);
                    CacheSqlHelper.SaveToCacheDependency(Const.DATABASE_NAME, Const.TABLE_INDEXMONTH, cacheName, __result);
                }

                return __result;
            }

            public static DataTable pr_Chart_MonthCompare(Int64 parentid, int time1, int time2, int year1, int year2, string code)
            {
                string cacheName = CacheSqlHelper.CacheNameFormat.pr_Chart_MonthCompare(parentid, time1, time2, year1, year2, code);
                DataTable __result = CacheSqlHelper.GetFromCacheDependency<DataTable>(cacheName);
                if (__result == null)
                {
                    __result = IndexNoCacheSql.pr_Chart_MonthCompare(parentid, time1, time2, year1, year2, code);
                    CacheSqlHelper.SaveToCacheDependency(Const.DATABASE_NAME, new string[] { Const.TABLE_INDEXMONTH, Const.TABLE_CATEGORY }, cacheName, __result);
                }

                return __result;
            }

            public static DataTable pr_IndexMonth_SelectByIDAndTimeForQuy(int id, int time1, int time2, int year)
            {
                string cacheName = CacheSqlHelper.CacheNameFormat.pr_IndexMonth_SelectByIDAndTimeForQuy(id, time1, time2, year);
                DataTable __result = CacheSqlHelper.GetFromCacheDependency<DataTable>(cacheName);
                if (__result == null)
                {
                    __result = IndexNoCacheSql.pr_IndexMonth_SelectByIDAndTimeForQuy(id, time1, time2, year);
                    CacheSqlHelper.SaveToCacheDependency(Const.DATABASE_NAME, Const.TABLE_INDEXMONTH, cacheName, __result);
                }

                return __result;
            }
        }
    }
}
