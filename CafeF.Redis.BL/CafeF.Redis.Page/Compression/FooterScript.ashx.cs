using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Reflection;
using System.IO;
using System.IO.Compression;
using System.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace CafeF.Redis.Page.Compression
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class FooterScript : IHttpHandler
    {
        private static string _versionNo;
        private static bool _compress;
        private static int _cacheDurationInDays;
        private static string _fileName = string.Empty;
        private static string _time = string.Empty;
        public static string _cacheKey = "FooterScript1.v";
        private static readonly List<string> _files = new List<string>();
         static FooterScript()
        {
            HttpContext context = HttpContext.Current;
            string queryString = HttpUtility.UrlDecode(context.Request.QueryString.ToString());
            string[] urlSplit = queryString.Split('&');
            #region Get cache name
            if (urlSplit.Length > 0)
            {
                string[] versionTokens = urlSplit[0].Split('=');
                _versionNo = (versionTokens.Length > 0) ? versionTokens[1].ToString() : "1.0";
                _cacheKey += _versionNo;
            }
            #endregion

            #region Checking cache existed 
            if ((context.Cache[_cacheKey] == null))
            {
                Hashtable settings = ConfigurationManager.GetSection("FooterScript") as Hashtable;
                if (settings != null)
                {
                    _versionNo = settings["versionNo"].ToString();
                    _compress = Convert.ToBoolean(settings["compress"], CultureInfo.InvariantCulture);
                    //_cacheKey += _compress.ToString();
                    _cacheDurationInDays = Convert.ToInt32(settings["cacheDurationInDays"], CultureInfo.InvariantCulture);

                    string fileList = settings["files"].ToString();

                    if (!string.IsNullOrEmpty(fileList))
                    {
                        char[] _ch = { ';', ',' };
                        string[] files = fileList.Split(_ch);

                        if (files.Length > 0)
                        {
                            if (context != null)
                            {
                                foreach (string file in files)
                                {
                                    _files.Add(context.Server.MapPath(file));
                                }
                            }
                        }
                    }
                }
            }
            #endregion
        }

        public void ProcessRequest(HttpContext context)
        {
            HttpResponse response = context.Response;
            if (_files.Count == 0)
            {
                response.StatusCode = 500;
                response.StatusDescription = "Unable to find any script file.";
                response.End();
                return;
            }

            response.ContentType = "application/x-javascript";
            Stream output = response.OutputStream;
            if (_compress)
            {
                string acceptEncoding = context.Request.Headers["Accept-Encoding"];

                if (!string.IsNullOrEmpty(acceptEncoding))
                {
                    acceptEncoding = acceptEncoding.ToUpperInvariant();

                    if (acceptEncoding.Contains("GZIP"))
                    {
                        response.AddHeader("Content-encoding", "gzip");
                        output = new GZipStream(output, CompressionMode.Compress);
                    }
                    else if (acceptEncoding.Contains("DEFLATE"))
                    {
                        response.AddHeader("Content-encoding", "deflate");
                        output = new DeflateStream(output, CompressionMode.Compress);
                    }
                }
            }
            //context.Cache.Remove(_cacheKey);// = null; //bo doan nay khi release
            if (context.Cache[_cacheKey] == null)
            {
               
                //Combine
                using (StreamWriter sw = new StreamWriter(output))
                {
                    //Write each files in the response

                    StringBuilder sb = new StringBuilder();

                    try
                    {
                        foreach (string file in _files)
                        {
                            string content = File.ReadAllText(file);
                            sb.Append(content);
                        }

                        //string _content = RemoveRedundanceSymbols(sb.ToString());
                        string _content = StripWhitespace(sb.ToString());

                        context.Cache.Add(_cacheKey, _content, null, DateTime.MaxValue, TimeSpan.FromDays(_cacheDurationInDays), System.Web.Caching.CacheItemPriority.Normal, null);
                        sw.WriteLine(_content);

                        //sw.Write("if(typeof(Sys)!='undefined'){Sys.Application.notifyScriptLoaded();}");
                    }
                    catch (Exception ex)
                    {
                        context.Response.Write(ex.Message.ToString());
                    }
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(output))
                {
                    sw.WriteLine(context.Cache[_cacheKey] as string);
                }
            }

            //Cache
            if (_cacheDurationInDays > 0)
            {
                TimeSpan duration = TimeSpan.FromDays(_cacheDurationInDays);
                HttpCachePolicy cache = response.Cache;
                cache.SetCacheability(HttpCacheability.Public);
                cache.SetExpires(DateTime.Now.Add(duration));
                cache.SetMaxAge(duration);
                cache.AppendCacheExtension("must-revalidate, proxy-revalidate");

                FieldInfo maxAgeField = cache.GetType().GetField("_maxAge", BindingFlags.Instance | BindingFlags.NonPublic);
                maxAgeField.SetValue(cache, duration);
            }
            //response.Flush();
        }

        private string RemoveRedundanceSymbols(string Content)
        {
            Content = Content.Replace(Environment.NewLine, String.Empty);
            Content = Content.Replace("  ", String.Empty);
            Content = Content.Replace("\t", String.Empty);
            Content = Content.Replace(" {", "{");
            Content = Content.Replace(" :", ":");
            Content = Content.Replace(": ", ":");
            Content = Content.Replace(", ", ",");
            Content = Content.Replace("; ", ";");
            Content = Content.Replace(";}", "}");
            Content = Content.Replace("    ", string.Empty);

            return Content;
        }
        private static string StripWhitespace(string body)
        {
            string[] lines = body.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
            foreach (string line in lines)
            {
                string s = line.Trim();
                if (s.Length > 0 && !s.StartsWith("//"))
                    sb.AppendLine(s.Trim());
            }

            body = sb.ToString();
            body = Regex.Replace(body, @"^[\s]+|[ \f\r\t\v]+$", String.Empty);
            body = Regex.Replace(body, @"([+-])\n\1", "$1 $1");
            body = Regex.Replace(body, @"([^+-][+-])\n", "$1");
            body = Regex.Replace(body, @"([^+]) ?(\+)", "$1$2");
            body = Regex.Replace(body, @"(\+) ?([^+])", "$1$2");
            body = Regex.Replace(body, @"([^-]) ?(\-)", "$1$2");
            body = Regex.Replace(body, @"(\-) ?([^-])", "$1$2");
            body = Regex.Replace(body, @"\n([{}()[\],<>/*%&|^!~?:=.;+-])", "$1");
            body = Regex.Replace(body, @"(\W(if|while|for)\([^{]*?\))\n", "$1");
            body = Regex.Replace(body, @"(\W(if|while|for)\([^{]*?\))((if|while|for)\([^{]*?\))\n", "$1$3");
            body = Regex.Replace(body, @"([;}]else)\n", "$1 ");
            body = Regex.Replace(body, @"(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,}(?=&nbsp;)|(?<=&ndsp;)\s{2,}(?=[<])", String.Empty);

            return body;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
