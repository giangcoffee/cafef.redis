using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Entity;

namespace CafeF.Redis.Data
{
    public class ReportsDAO
    {
        private static readonly string key = "stockid:{0}:reports:List"; //~Symbol
        public static List<Reports> get_ReportsSearching(string Symbol, int SourceID, int CategoryID, int pageIndex, int PageSize, out int Total)
        {
            var redis = BLFACTORY.RedisClient;
            List<Reports> ret = new List<Reports>();
            var keylist = redis.ContainsKey(RedisKey.KeyAnalysisReport) ? BLFACTORY.RedisClient.Get<List<string>>(RedisKey.KeyAnalysisReport) : new List<string>();
            var keys = new List<string>();
            foreach (var id in keylist)
            {
                var hKey = string.Format(RedisKey.KeyAnalysisReportDetail, id.Substring(8));
                //if (BLFACTORY.RedisClient.ContainsKey(hKey))
                //{
                    //var sn = BLFACTORY.RedisClient.Get<Reports>(hKey);
                    //if ((sn.SourceID == SourceID || SourceID == -1) && (sn.CategoryID == CategoryID || CategoryID == 0) && (stringContainSymbol(sn.Symbol, Symbol)))
                    //    ret.Add(sn);
                //}
                if(!keys.Contains(hKey))keys.Add(hKey);
            }
            var objs = keys.Count>0 ? redis.GetAll<Reports>(keys) : new Dictionary<string, Reports>();
            ret = objs.Values.ToList().FindAll(s=>s!=null && (SourceID==-1 || s.SourceID==SourceID) && (CategoryID==0 || s.CategoryID==CategoryID) && (Symbol=="" || stringContainSymbol(s.Symbol, Symbol)));
            Total = ret.Count;
            ret = ret.GetPaging(pageIndex, PageSize);
            return ret;
        }

        public static Reports get_ReportsByKey(int ID)
        {
            Reports ret = new Reports();
            string hKey = String.Format(RedisKey.KeyAnalysisReportDetail, ID);
            if (BLFACTORY.RedisClient.ContainsKey(hKey))
                ret = BLFACTORY.RedisClient.Get<Reports>(hKey);

            return ret;
        }

        public static List<Reports> get_ReportsByTop(int top)
        {
            var redis = BLFACTORY.RedisClient;
            //List<Reports> ret = new List<Reports>();
            var keylist = redis.ContainsKey(RedisKey.KeyAnalysisReport) ? redis.Get<List<string>>(RedisKey.KeyAnalysisReport) : new List<string>();
            var keys = new List<string>();
            foreach (string id in keylist)
            {
                var hKey = string.Format(RedisKey.KeyAnalysisReportDetail, id.Substring(8));
                //if (BLFACTORY.RedisClient.ContainsKey(hKey))
                //{
                //    Reports sn = BLFACTORY.RedisClient.Get<Reports>(hKey);
                //    ret.Add(sn);
                //    if (ret.Count > top)break;
                //}
                if(!keys.Contains(hKey)) keys.Add(hKey);
                if(keys.Count>top) break;
            }
            var ret = keys.Count>0 ? redis.GetAll<Reports>(keys).Values.ToList(): new List<Reports>(); //ret.Take(top).ToList();
            ret.RemoveAll(s => s == null);
            return ret;
        }

        public static bool stringContainSymbol(string list, string sym)
        {
            if(sym=="") return true;
            return ("," + list.Trim().ToUpper() + ",").Contains("," + sym.Trim().ToUpper() + ",");
            //bool ret = false;
            //if (sym == "") return true;
            //string[] sList = list.Split(',');
            //for (int i = 0; i < sList.Length; i++)
            //    if (sList[i].ToString().Trim().ToLower() == sym.Trim().ToLower())
            //        return true;

            //return ret;
        }

    }
}
