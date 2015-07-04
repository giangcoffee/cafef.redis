using System;
using System.Data;
using System.Web;
using System.Configuration;
using CafeF.TA.DAL;
using CafeF.TA.BO.Cache;
using CafeF.TA.BO.Utilitis;

namespace CafeF.TA
{
    public class IndexBO
    {
        public class IndexNoCacheSql
        {
            public static DataTable TA_GetTopTenSignal(string Symbol, DateTime dt, int idx, int pcount)
            {
                DataTable __result = new DataTable();
                using (MainDB db = new MainDB())
                {
                    __result = db.StoredProcedures.TA_GetTopTenSignal(Symbol, dt, idx ,pcount);
                    db.Close();
                }

                return __result;
            }
        }

        public class IndexCacheSql
        {
            public static DataTable TA_GetTopTenSignal(string Symbol, DateTime dt, int idx, int pcount)
            {
                string cacheName = CacheSqlHelper.CacheNameFormat.TA_GetTopTenSignal(Symbol, dt, idx, pcount);
                DataTable __result = CacheSqlHelper.GetFromCacheDependency<DataTable>(cacheName);
                if (__result == null)
                {
                    __result = IndexNoCacheSql.TA_GetTopTenSignal(Symbol, dt, idx, pcount);
                    CacheSqlHelper.SaveToCacheDependency(Const.DATABASE_NAME, new string[] { Const.TABLE_CANDLETREND, Const.TABLE_CANDLELIST, Const.TABLE_TATREND, Const .TABLE_TALIST}, cacheName, __result);
                }

                return __result;
            }
        }
    }
}
