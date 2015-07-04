using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Entity;

namespace CafeF.Redis.Data
{
    public class TienDoBDSDAO
    {
        /// <summary>
        /// Với 1 object ~ 1 table có bao nhiêu thuộc tính ~ Field sẽ tạo ra bấy nhiêu sortedset tương ứng (max ~ 100 item)
        /// Khi có thay đổi thêm thì sẽ đẩy ID đó vào và đẩy 1 item ra
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>

        public static TienDoBDS getByCode(string symbol, string code)
        {
            return BLFACTORY.RedisClient.Get<TienDoBDS>(String.Format(RedisKey.BDSKey, symbol, code));
        }

        public static List<TienDoBDS> get_TienDoBDSBySymbol(string symbol)
        {
            var redis = BLFACTORY.RedisClient;
            symbol = symbol.ToUpper();
            List<TienDoBDS> ret = new List<TienDoBDS>();
            var keylist = redis.SearchKeys(string.Format(RedisKey.BDSKey, symbol.ToUpper(),"*"));

            var keys = new List<string>();
            foreach (string hKey in keylist)
            {
                //if (BLFACTORY.RedisClient.ContainsKey(hKey))
                //{
                //    try
                //    {
                //        TienDoBDS td = BLFACTORY.RedisClient.Get<TienDoBDS>(hKey);
                //        if (td.MaCK.ToUpper() == symbol)
                //            ret.Add(td);
                //    }
                //    catch { }
                //}
                if(!keys.Contains(hKey)) keys.Add(hKey);
            }
            if (keys.Count > 0) ret = redis.GetAll<TienDoBDS>(keys).Values.ToList();
            ret.RemoveAll(s => s == null || s.MaCK != symbol);
            ret.Sort("MaTienDo asc,TenDuAn asc ");          
            return ret;
        }
    }
}
