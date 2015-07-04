using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CafeF.Redis.Entity;

namespace CafeF.Redis.Data
{
    public class CeoDAO
    {
        /// <summary>
        /// Với 1 object ~ 1 table có bao nhiêu thuộc tính ~ Field sẽ tạo ra bấy nhiêu sortedset tương ứng (max ~ 100 item)
        /// Khi có thay đổi thêm thì sẽ đẩy ID đó vào và đẩy 1 item ra
        /// </summary>
        /// <param name="ceocode"></param>
        /// <returns></returns>

        public static Ceo getCeoByCode(string code)
        {
            code = code.ToUpper();
            return BLFACTORY.RedisClient.Get<Ceo>(String.Format(RedisKey.CeoKey, code));
        }
        public static IDictionary<string,string> GetCeoImage(List<string> codes)
        {
            if (codes.Count == 0) return new Dictionary<string, string>();
            var ss = new List<string>();
            ss.AddRange(codes);
            for (var i = 0; i < codes.Count; i++)
            {
                ss[i] = string.Format(RedisKey.CeoImage, codes[i]);
            }
            var tmp = BLFACTORY.RedisClient.GetAll<string>(ss);
            var ret = new Dictionary<string, string>();
            foreach (var code in codes)
            {
                ret.Add(code, tmp[string.Format(RedisKey.CeoImage, code)]);
            }
            return ret;
        }
    }
}
