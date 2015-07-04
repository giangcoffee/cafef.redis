using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Entity;

namespace CafeF.Redis.Data
{
    public class LichSuKienDAO
    {
        public static List<LichSuKien> get_LichSuKienSearching(bool status, string symbol, int type, DateTime fromDate, DateTime toDate, int PageIndex, int PageCount, out int totalItem)
        {

            var redis = BLFACTORY.RedisClient;
            var ret = new List<LichSuKien>();
            var keylist = redis.ContainsKey(RedisKey.KeyLichSuKien) ? redis.Get<List<string>>(RedisKey.KeyLichSuKien) : new List<string>();
            if(status && int.Parse(fromDate.ToString("yyyyMMdd")) < int.Parse(DateTime.Now.ToString("yyyyMMdd")))
            {
                fromDate = DateTime.Now;
            }
            if (!status && int.Parse(toDate.ToString("yyyyMMdd")) > int.Parse(DateTime.Now.ToString("yyyyMMdd")))
            {
                toDate = DateTime.Now;
            }
            keylist = keylist.FindAll(sKey => ((Convert.ToInt32(fromDate.ToString("yyyyMMdd")) < int.Parse("20000101") || Convert.ToInt32(sKey.Substring(0, 8)) >= Convert.ToInt32(fromDate.ToString("yyyyMMdd")))) && (Convert.ToInt32(toDate.ToString("yyyyMMdd")) < int.Parse("20000101") || Convert.ToInt32(sKey.Substring(0, 8)) <= Convert.ToInt32(toDate.ToString("yyyyMMdd"))) && (type == 0 || ("," + sKey.Substring(9, sKey.LastIndexOf(":") - 9) + ",").Contains("," + type.ToString() + ",")));
            var keys = new List<string>();
            foreach (var hKey in keylist)
            {
                var key = string.Format(RedisKey.KeyLichSuKienObject, hKey.Substring(hKey.LastIndexOf(":") + 1));
                if (!keys.Contains(key)) keys.Add(key);
            }
            var objs = keys.Count > 0 ? redis.GetAll<LichSuKien>(keys) : new Dictionary<string, LichSuKien>();
            ret = objs.Values.ToList().FindAll(s => s != null && (symbol == "" || stringContainSymbol(s.MaCK, symbol)) && double.Parse(s.PostDate.ToString("yyyyMMddHHmm")) <= double.Parse(DateTime.Now.ToString("yyyyMMddHHmm")));
            if (status)
                ret.Sort("EventDate asc");
            else
                ret.Sort("EventDate desc");
            totalItem = ret.Count;
            ret = ret.GetPaging(PageIndex, PageCount);
            return ret;
        }

        public static List<LichSuKien> get_LichSuKienTop(int top)
        {
            var redis = BLFACTORY.RedisClient;
            if (top <= 10) return get_LichSuKienTomTat(top);

            var keylist = redis.ContainsKey(RedisKey.KeyLichSuKien) ? redis.Get<List<string>>(RedisKey.KeyLichSuKien) : new List<string>();
            var keys = new List<string>();
            foreach (string hKey in keylist)
            {
                var key = string.Format(RedisKey.KeyLichSuKienObject, hKey.Substring(hKey.LastIndexOf(":") + 1));
                if (!keys.Contains(key)) keys.Add(key);
                if (keys.Count > top * 2) break;
            }
            var ret = keys.Count > 0 ? redis.GetAll<LichSuKien>(keys).Values.ToList() : new List<LichSuKien>();
            ret.RemoveAll(s => s == null || double.Parse(s.PostDate.ToString("yyyyMMddHHmm")) > double.Parse(DateTime.Now.ToString("yyyyMMddHHmm")));
            if (ret.Count > top) { ret = ret.GetRange(0, top); }
            return ret;
        }

        private static bool stringContainSymbol(string list, string sym)
        {
            //bool ret = false;
            //if (sym == "") return true;
            //string[] sList = list.Split(',');
            //for (int i = 0; i < sList.Length; i++)
            //    if (sList[i].ToString().Trim().ToLower() == sym.Trim().ToLower())
            //        return true;

            //return ret;
            return ("&" + list.ToUpper() + "&").Contains("&" + sym.ToUpper() + "&");
        }
        public static List<LichSuKien> get_LichSuKienTomTat(int top)
        {
            var redis = BLFACTORY.RedisClient;
            var key = RedisKey.KeyLichSuKienTomTat;
            var ret = (redis.ContainsKey(key) ? redis.Get<List<LichSuKien>>(key) : new List<LichSuKien>());
            ret.RemoveAll(s => double.Parse(s.PostDate.ToString("yyyyMMddHHmm")) > double.Parse(DateTime.Now.ToString("yyyyMMddHHmm")));
            if (ret.Count > top) return ret.GetRange(0, top);
            return ret;
        }
    }
}
