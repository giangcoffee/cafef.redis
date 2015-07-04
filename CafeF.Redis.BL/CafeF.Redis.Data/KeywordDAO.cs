using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeF.Redis.Data
{
    public class KeywordDAO
    {
        public static List<string> GetGoogleKeyword(int top)
        {
            var ls = new List<string>();
            if (BLFACTORY.RedisClient.ContainsKey(RedisKey.GoogleTag))
                ls = BLFACTORY.RedisClient.Get<List<string>>(RedisKey.GoogleTag) ?? new List<string>();
            var ret = new List<string>();
            var rd = new Random();
            while (ret.Count < top)
            {
                if(ls.Count==0) break;
                var s = ls[rd.Next(ls.Count)];
                if(!ret.Contains(s)) ret.Add(s);
                ls.Remove(s);
            }
            return ret;
        }

    }
}
