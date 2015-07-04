using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Caching;
using VCCorp.FinanceChannel.Core.DataUpd;

namespace CafeF.Redis.BO
{
    public static class NewsHelper_NoCached
    {
        //#region Cache name format
        //public class CacheNameFormat
        //{
        //    /// <summary>
        //    /// ca_HostNewsInHomPage_{top}
        //    /// </summary>
        //    /// 

        //    public static string ListAllCrawlerNewsPaging(int PageNum, int PageSize, string lstSymbol)
        //    {
        //        return "ListAllCrawlerNewsPaging_" + PageNum + "_" + PageSize + "_" + lstSymbol;
        //    }

        //    public static string NV_Get_Newsest(int PageSize, int PageIndex)
        //    {
        //        return "NV_Get_Newsest_" + PageSize + "_" + PageIndex;
        //    }

        //    public static string HostNewsInHomPage(int top)
        //    {
        //        return string.Format("ca_HostNewsInHomPage_{0}", top);
        //    }

        //    /// <summary>
        //    /// _TOPNEW_CATEGORY_{Cat_ID}_{Top_Record}_{mode}
        //    /// </summary>
        //    public static string GetTopNewByCategory(int Cat_ID, int Top_Record, int mode)
        //    {
        //        return string.Format("_TOPNEW_CATEGORY_{0}_{1}_{2}", Cat_ID, Top_Record, mode);
        //    }

        //    /// <summary>
        //    /// ListNewsByLinkHomePage_{NewsId}_{Cat_ID}_{top}
        //    /// </summary>
        //    public static string ListNewsByLinkHomePage(long NewsId, int Cat_ID, int top)
        //    {
        //        return string.Format("ListNewsByLinkHomePage_{0}_{1}_{2}", NewsId, Cat_ID, top);
        //    }

        //    /// <summary>
        //    /// ListNewsByCategoryEdit_{Cat_ID}_{StartIndex}_{PageSize}
        //    /// </summary>
        //    public static string ListNewsByCategoryEdit(int Cat_ID, int StartIndex, int PageSize)
        //    {
        //        return string.Format("ListNewsByCategoryEdit_{0}_{1}_{2}", Cat_ID, StartIndex, PageSize);
        //    }

        //    /// <summary>
        //    /// ListNewsByCategoryEdit_1_{Cat_ID}_{StartIndex}_{PageSize}
        //    /// /// </summary>
        //    public static string ListNewsByCategoryEdit_1(int Cat_ID, int StartIndex, int PageSize)
        //    {
        //        return string.Format("ListNewsByCategoryEdit_1_{0}_{1}_{2}", Cat_ID, StartIndex, PageSize);
        //    }

        //    /// <summary>
        //    /// RelatedNewsByNews_Id_{originalNew_Id}
        //    /// </summary>
        //    public static string RelatedNewsByNews_Id(long originalNew_Id)
        //    {
        //        return string.Format("RelatedNewsByNews_Id_{0}", originalNew_Id);
        //    }

        //    /// <summary>
        //    /// _GET_FOCUS_NEW_BY_CATEGORY_{__catID}_{__topRecord}
        //    /// </summary>
        //    public static string GetFocusNewsByCategory(int __catID, int __topRecord)
        //    {
        //        return string.Format("_GET_FOCUS_NEW_BY_CATEGORY_{0}_{1}", __catID, __topRecord);
        //    }

        //    /// <summary>
        //    /// __NEWDETAIL_{News_Id}
        //    /// </summary>
        //    public static string NewsDetail(Int64 News_Id)
        //    {
        //        return string.Format("__NEWDETAIL_{0}", News_Id);
        //    }

        //    /// <summary>
        //    /// _LIST_OLD_ARTICLE_BY_CATEGORY_{__newsID}_{__catID}_{__topRecord}
        //    /// </summary>
        //    public static string GetListOldArticleByCategory(long __newsID, int __catID, int __topRecord)
        //    {
        //        return string.Format("_LIST_OLD_ARTICLE_BY_CATEGORY_{0}_{1}_{2}", __newsID, __catID, __topRecord);
        //    }

        //    /// <summary>
        //    /// _LIST_NEW_ARTICLE_BY_CATEGORY_{__newsID}_{__catID}_{__topRecord}
        //    /// </summary>
        //    public static string GetListNewArticleByCategory(long __newsID, int __catID, int __topRecord)
        //    {
        //        return string.Format("_LIST_NEW_ARTICLE_BY_CATEGORY_{0}_{1}_{2}", __newsID, __catID, __topRecord);
        //    }
        //}
        //#endregion

        #region Cache methods
        public static void SaveToCacheDependency(string database, string tableName, string cacheName, object data)
        {
            SqlCacheDependency sqlDep = new SqlCacheDependency(database, tableName);
            HttpContext.Current.Cache.Insert(cacheName, data, sqlDep);
        }
        public static void SaveToCacheDependency(string database, string[] tableName, string cacheName, object data)
        {
            AggregateCacheDependency aggCacheDep = new AggregateCacheDependency();
            SqlCacheDependency[] sqlDepGroup = new SqlCacheDependency[tableName.Length] ;
            for (int i = 0; i < tableName.Length; i++)
            {
                sqlDepGroup[i] = new SqlCacheDependency(database, tableName[i]);
                
            }
            aggCacheDep.Add(sqlDepGroup);
            HttpContext.Current.Cache.Insert(cacheName, data, aggCacheDep);
        }
        public static T GetFromCacheDependency<T>(string cacheName)
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
        public static T GetDataPortfolioFromDistributedCache<T>(string cacheName)
        {
            if (Channelvn.Cached.CacheController.IsAllowDistributedCached)
            {
                return Channelvn.Cached.CacheController.Get<T>(Channelvn.Cached.Common.Constants.PORTFOLIO_FOR_DATA_CACHE,
                                            cacheName);
            }
            else
            {
                return default(T);
            }
        }
        //public static T GetDataThongKeFromDistributedCache<T>(string cacheName)
        //{
        //    if (Channelvn.Cached.CacheController.IsAllowDistributedCached)
        //    {
        //        return Channelvn.Cached.CacheController.Get<T>(Channelvn.Cached.Common.Constants.THONGKE_FOR_DATA_CACHE,
        //                                    cacheName);
        //    }
        //    else
        //    {
        //        return default(T);
        //    }
        //}
        public static void CloseConnection(ref SqlAccessLayerUpd __sqlDal)
        {
            if (__sqlDal != null)
            {
                __sqlDal.CurrentConnection.Close();
                __sqlDal.CurrentConnection.Dispose();
            }
        }
        #endregion

        
    }
}
