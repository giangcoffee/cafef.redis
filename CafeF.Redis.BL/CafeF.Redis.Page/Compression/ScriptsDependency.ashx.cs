using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Configuration;
namespace CafeF.Redis.Page.Compression
{
    
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ScriptsDependency : IHttpHandler
    {

        private const bool DO_GZIP = true;
        private readonly static TimeSpan CACHE_DURATION = TimeSpan.FromDays(30);
        private static readonly List<string> _files = new List<string>();
        private static bool allowCacheDependency = false;

        static ScriptsDependency()
        {
            Hashtable settings = ConfigurationManager.GetSection("ScriptsDependency") as Hashtable;

            if (settings != null)
            {
                allowCacheDependency = Convert.ToBoolean(settings["allowCacheDependency"].ToString());
                string fileList = settings["files"].ToString();
                if (!string.IsNullOrEmpty(fileList))
                {
                    char[] _ch = { ';', ',' };
                    string[] files = fileList.Split(_ch);

                    if (files.Length > 0)
                    {
                        HttpContext context = HttpContext.Current;

                        if (context != null)
                        {
                            foreach (string file in files)
                            {
                                _files.Add(file);
                            }
                        }
                    }
                }
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/javascript";
            string version = context.Request["v"];
            bool isCompressed = DO_GZIP && this.CanGZip(context.Request);
            UTF8Encoding encoding = new UTF8Encoding(false);
            if (!this.WriteFromCache(context, version, isCompressed))
            {
                using (MemoryStream memoryStream = new MemoryStream(5000))
                {
                    using (Stream writer = isCompressed ? (Stream)(new GZipStream(memoryStream, CompressionMode.Compress)) : memoryStream)
                    {
                        foreach (string fileName in _files)
                        {
                            byte[] fileBytes = this.GetCss(context, fileName, version, encoding);
                            writer.Write(fileBytes, 0, fileBytes.Length);
                        }
                        writer.Close();
                    }
                    byte[] responseBytes = memoryStream.ToArray();

                    if (allowCacheDependency)
                    {
                        CreateCacheDependency(version, isCompressed,context, responseBytes);
                        //context.Cache.Insert(GetCacheKey(version, isCompressed), responseBytes, null, System.Web.Caching.Cache.NoAbsoluteExpiration, CACHE_DURATION);
                    }
                    this.WriteBytes(responseBytes, context, isCompressed);
                }
            }
            //context.Response.Flush();
        }

        private void CreateCacheDependency(string version, bool isCompressed, HttpContext context, byte[]responseBytes)
        {
            try
            {
                string CacheName = GetCacheKey(version, isCompressed);
                System.Web.Caching.AggregateCacheDependency __agg = new System.Web.Caching.AggregateCacheDependency();
                System.Web.Caching.CacheDependency[] __c1 = new System.Web.Caching.CacheDependency[2];
                int i = 0;
                foreach (string fileName in _files)
                {

                    string physicalPath = context.Server.MapPath(fileName);
                    __c1[i] = new System.Web.Caching.CacheDependency(physicalPath);
                }
                __agg.Add(__c1);
                context.Cache.Insert(CacheName, responseBytes, __agg, System.Web.Caching.Cache.NoAbsoluteExpiration, CACHE_DURATION);
            }
            catch (Exception ex)
            {
            }

        }
        private byte[] GetCss(HttpContext context, string virtualPath, string version, Encoding encoding)
        {
            string physicalPath = context.Server.MapPath(virtualPath);
            string fileContent = File.ReadAllText(physicalPath, encoding);
            string imagePrefixUrl = VirtualPathUtility.GetDirectory(virtualPath).TrimStart('~').TrimStart('/');
            return encoding.GetBytes(fileContent);
        }

        private bool WriteFromCache(HttpContext context, string version, bool isCompressed)
        {
            //if (!allowCacheDependency) return false;

            byte[] responseBytes = context.Cache[GetCacheKey(version, isCompressed)] as byte[];

            if (null == responseBytes) return false;

            this.WriteBytes(responseBytes, context, isCompressed);
            return true;
        }

        private void WriteBytes(byte[] bytes, HttpContext context, bool isCompressed)
        {
            HttpResponse response = context.Response;

            response.AppendHeader("Content-Length", bytes.Length.ToString());
            response.ContentType = "text/javascript";
            if (isCompressed)
                response.AppendHeader("Content-Encoding", "gzip");

            context.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.Response.Cache.SetExpires(DateTime.Now.Add(CACHE_DURATION));
            context.Response.Cache.SetMaxAge(CACHE_DURATION);
            context.Response.Cache.AppendCacheExtension("must-revalidate, proxy-revalidate");
            response.OutputStream.Write(bytes, 0, bytes.Length);
            //response.Flush();
        }

        private bool CanGZip(HttpRequest request)
        {
            string acceptEncoding = request.Headers["Accept-Encoding"];
            if (!string.IsNullOrEmpty(acceptEncoding) && (acceptEncoding.Contains("gzip") || acceptEncoding.Contains("deflate")))
                return true;
            return false;
        }

        private string GetCacheKey(string version, bool isCompressed)
        {
            return "ScriptsDependency.header." + version + "." + isCompressed;
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}

