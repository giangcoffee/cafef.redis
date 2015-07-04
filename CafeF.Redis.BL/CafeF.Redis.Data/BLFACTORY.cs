using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ServiceStack.Redis;
namespace CafeF.Redis.Data
{
    public class BLFACTORY
    {
        //private static IRedisClient redisClient = new RedisClient(ConfigRedis.Host, ConfigRedis.Port);
        private static readonly string redisSlotName = "Redis";
        private static readonly string redisSlotPrice = "RedisPrice";
        //public static Redis RedisClient
        //{
        //    get
        //    {
        //        if (!HttpContext.Current.Items.Contains(redisSlotName))
        //            HttpContext.Current.Items.Add(redisSlotName, new Redis());

        //        return (Redis)HttpContext.Current.Items[redisSlotName];
        //    }
        //}

        public static IRedisClient RedisClient
        {
            get
            {
                if (!HttpContext.Current.Items.Contains(redisSlotName))
                    HttpContext.Current.Items.Add(redisSlotName, new RedisClient(ConfigRedis.Host, ConfigRedis.Port));

                return (IRedisClient)HttpContext.Current.Items[redisSlotName];
            }
        }

        public static IRedisClient RedisPriceClient
        {
            get
            {
                if (!HttpContext.Current.Items.Contains(redisSlotPrice))
                    HttpContext.Current.Items.Add(redisSlotPrice, new RedisClient(ConfigRedis.PriceHost, ConfigRedis.PricePort));

                return (IRedisClient)HttpContext.Current.Items[redisSlotPrice];
            }
        }


    }
   
}
