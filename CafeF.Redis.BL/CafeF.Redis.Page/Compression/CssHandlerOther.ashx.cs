using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Configuration;
using System.IO;
using System.IO.Compression;

namespace CafeF.Redis.Page.Compression
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class CssHandlerOther : IHttpHandler
    {
        private const bool DO_GZIP = true;
        private readonly static TimeSpan CACHE_DURATION = TimeSpan.FromDays(30);
        private static readonly List<string> _files = new List<string>();
        private static bool allowCacheDependency = false;

        static CssHandlerOther()
        {
            Hashtable settings = ConfigurationManager.GetSection("CssSettingsOther") as Hashtable;

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
            context.Response.ContentType = "text/css";

            //string themeName = context.Request["t"];
            //string themeFileNames = context.Request["f"];
            string version = context.Request["v"];

            bool isCompressed = DO_GZIP && this.CanGZip(context.Request);

            UTF8Encoding encoding = new UTF8Encoding(false);

            if (!this.WriteFromCache(context, version, isCompressed))
            {
                using (MemoryStream memoryStream = new MemoryStream(5000))
                {
                    using (Stream writer = isCompressed ? (Stream)(new GZipStream(memoryStream, CompressionMode.Compress)) : memoryStream)
                    {
                        // First deliver the common CSS
                        foreach (string fileName in _files)
                        {
                            byte[] fileBytes = this.GetCss(context, fileName, version, encoding);
                            writer.Write(fileBytes, 0, fileBytes.Length);
                        }
                        writer.Close();
                    }
                    // Cache and generate response
                    byte[] responseBytes = memoryStream.ToArray();

                    if (allowCacheDependency)
                        context.Cache.Insert(GetCacheKey(version, isCompressed), responseBytes, null, System.Web.Caching.Cache.NoAbsoluteExpiration, CACHE_DURATION);
                    this.WriteBytes(responseBytes, context, isCompressed);
                }
            }
            //context.Response.Flush();
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
            //return false; //bodoan nay khi release
            if (!allowCacheDependency) return false;

            byte[] responseBytes = context.Cache[GetCacheKey(version, isCompressed)] as byte[];

            if (null == responseBytes) return false;

            this.WriteBytes(responseBytes, context, isCompressed);
            return true;
        }

        private void WriteBytes(byte[] bytes, HttpContext context, bool isCompressed)
        {
            HttpResponse response = context.Response;

            response.AppendHeader("Content-Length", bytes.Length.ToString());
            response.ContentType = "text/css";
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
            return "CssSettingsOther." + version + "." + isCompressed;
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
