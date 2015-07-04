using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using MemcachedProviders.Cache;
using System.IO.Compression;
namespace CafeF.Redis.BO
{
    public class CafefCommonHelper
    {
        public static string __url = ConfigurationManager.AppSettings["SITE_URL"].ToString();
        public static void CreateSQLDependency(string dbName,string tblName,string CacheName,object Data)
        {
            try
            {
                System.Web.Caching.SqlCacheDependency __sqlDep = new System.Web.Caching.SqlCacheDependency(dbName, tblName);
                if (Data == null) return;
                HttpContext.Current.Cache.Insert(CacheName, Data, __sqlDep);
                
            }
            catch (Exception ex)
            {
            }
        }
        public static DateTime GetDateValue(string d)
        {
            try
            {
                return Convert.ToDateTime(d);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
        public static string News_BuildUrl(string News_ID, string Cat_Id, string News_Title)
        {
            string __urlFormart = "/{0}CA{1}/{2}.chn";
            int __Cat_ID = 0;
            if (Convert.ToInt32(Cat_Id) == 0)
            {
                __Cat_ID = MapCategory(NewsHepler_Update.UnicodeToKoDauAndGach(Cat_Id).ToLower());
            }
            else
            {
                __Cat_ID = int.Parse(Cat_Id);
            }
            return String.Format(__urlFormart, News_ID, __Cat_ID.ToString(), NewsHepler_Update.UnicodeToKoDauAndGach(News_Title));
        }
        public static int MapCategory(string str)
        {
            if (Convert.ToInt32(str) != 0) return Convert.ToInt32(str);
            if (str == "" || str == "0") return 0;
            int Cat_ID = NewsHelper.ConvertToInt(HttpContext.Current.Cache[str]);
            if (Cat_ID <= 0)
            {
                //Map chuỗi rewite path với catID lấy trong web.config
                Cat_ID = Convert.ToInt32(ConfigurationManager.AppSettings[str]);
                HttpContext.Current.Cache.Insert(str, Cat_ID, null, DateTime.Now.AddDays(365), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
            return Cat_ID;
        }
        public static int CompareDate(DateTime d1, DateTime d2)
        {
            DateTime t1 = new DateTime(d1.Year, d1.Month, d1.Day);
            DateTime t2 = new DateTime(d2.Year, d2.Month, d2.Day);
            if (t1 > t2) return 1;
            if (t1 == t2) return 0;
            return -1;
        }
        public static void PageComppressed(HttpContext c)
        {
            string AllowCompressedPage = ConfigurationManager.AppSettings["AllowCompressedPage"].ToString();
            if (AllowCompressedPage == "0")
            {
                if (!c.Request.UserAgent.ToLower().Contains("konqueror"))
                {
                    if (c.Request.Headers["Accept-encoding"] != null && c.Request.Headers["Accept-encoding"].Contains("gzip"))
                    {
                        c.Response.Filter = new GZipStream(c.Response.Filter, CompressionMode.Compress, true);

                        c.Response.AppendHeader("Content-encoding", "gzip");
                    }

                    else if (c.Request.Headers["Accept-encoding"] != null &&
                             c.Request.Headers["Accept-encoding"].Contains("deflate"))
                    {
                        c.Response.Filter = new DeflateStream(c.Response.Filter, CompressionMode.Compress, true);

                        c.Response.AppendHeader("Content-encoding", "deflate");
                    }
                }
            }
        }
    }
    public class CryptoUtil
    {
        private static Byte[] KEY_64 = new Byte[] { 42, 16, 93, 156, 78, 4, 218, 32 };
        private static Byte[] IV_64 = new Byte[] { 55, 103, 246, 79, 36, 99, 167, 3 };

        //'24 byte or 192 bit key and IV for TripleDES
        private static Byte[] KEY_192 = new Byte[]{42, 16, 93, 156, 78, 4, 218, 32,
            15, 167, 44, 80, 26, 250, 155, 112,
            2, 94, 11, 204, 119, 35, 184, 197};
        private static Byte[] IV_192 = new Byte[] {55, 103, 246, 79, 36, 99, 167, 3,
            42, 5, 62, 83, 184, 7, 209, 13,
            145, 23, 200, 58, 173, 10, 121, 222};

        //Standard DES encryption
        public static string Encrypt(string value)
        {
            if (value != "")
            {
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(KEY_64, IV_64), CryptoStreamMode.Write);
                StreamWriter sw = new StreamWriter(cs);
                sw.Write(value);
                sw.Flush();
                cs.FlushFinalBlock();
                ms.Flush();
                //'convert back to a string
                return Convert.ToBase64String(ms.GetBuffer());
            }
            return "";

        }

        //'Standard DES decryption
        public static string Decrypt(string value)
        {
            if (value != "")
            {
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                Byte[] buffer = Convert.FromBase64String(value);
                MemoryStream ms = new MemoryStream(buffer);
                CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(KEY_64, IV_64),CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cs);
                return sr.ReadToEnd();

            }
            return "";
        }

        //'TRIPLE DES encryption
        public static string EncryptTripleDES(string value)
        {
            if (value != "")
            {
                TripleDESCryptoServiceProvider cryptoProvider = new TripleDESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(KEY_192, IV_192), CryptoStreamMode.Write);
                StreamWriter sw = new StreamWriter(cs);
                sw.Write(value);
                sw.Flush();
                cs.FlushFinalBlock();
                ms.Flush();

                //'convert back to a string
                return Convert.ToBase64String(ms.GetBuffer());
            }
            return "";
        }

        //'TRIPLE DES decryption
        public static string DecryptTripleDES(string value)
        {
            TripleDESCryptoServiceProvider cryptoProvider = new TripleDESCryptoServiceProvider();
            Byte[] buffer = Convert.FromBase64String(value);
            MemoryStream ms = new MemoryStream(buffer);
            CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(KEY_192, IV_192), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }

}
