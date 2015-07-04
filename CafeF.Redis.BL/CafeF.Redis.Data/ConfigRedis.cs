using System.Configuration;
namespace CafeF.Redis.Data
{
	public static class ConfigRedis
	{
        static ConfigRedis()
		{
		}

		public static string Host  = ConfigurationManager.AppSettings["ServerRedis"];
        public static int Port =int.Parse(ConfigurationManager.AppSettings["Port"]);

	    public static string PriceHost = ConfigurationManager.AppSettings["ServerRedisPrice"] ?? "192.168.74.26";
	    public static int PricePort = int.Parse(ConfigurationManager.AppSettings["ServerRedisPortPrice"] ?? "6379");
	}
}