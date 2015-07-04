using System;
using System.Data;
using System.Reflection;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using log4net;
using System.Text;

namespace CafeF_EmbedData.Common
{
    public static class HandlerFactory
    {
        private static readonly Assembly m_currentAssembly = Assembly.GetExecutingAssembly();

        public static bool HandlerIsAllow(HttpContext context)
        {
            bool blResult = false;
            string host = (context.Request.UrlReferrer != null ? context.Request.UrlReferrer.Host : "");
            host = host.ToLower();
            host = host.Replace("www.", "");
            bool isDebugMode = (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["DEBUG_MODE"]) &&
                                  ConfigurationManager.AppSettings["DEBUG_MODE"] == "1");
            HandlerMapping mapping = new HandlerMapping();

            //string strValidateIdenity = GetIdentityHeader(context);
            if (isDebugMode || (!isDebugMode && host != ""))
            {
                try
                {
                    foreach (Domain domain in mapping.Domains)
                    {
                        //try
                        //{
                        //    if (host.ToLower() == domain.DomainName.ToLower())
                        //    {
                        //        bool bl1 = (host.ToLower() == domain.DomainName.ToLower());
                        //        bool bl2 = (strValidateIdenity.Trim().ToLower() == domain.Idenity.Trim().ToLower());
                        //        Lib.WriteErrorOnly(bl1.ToString() + "::" + bl2.ToString() + "::" + strValidateIdenity + "::" + domain.Idenity.ToLower() + "::" + host + "::utma:" + context.Request.Cookies["__utma"].Value);
                        //    }
                        //}
                        //catch(Exception ex) {
                        //}
                        //if (host.ToLower() == domain.DomainName.ToLower() && strValidateIdenity == domain.Idenity.ToLower())
                        if (host.ToLower() == domain.DomainName.ToLower())
                        {
                            blResult = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Lib.WriteLog(ex);
                }
            }
            return blResult;
        }
      
        public static string GetHeaders1(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            string host = (context.Request.UrlReferrer != null ? context.Request.UrlReferrer.Host : "");
            host = host.ToLower();
            host = host.Replace("www.", "");
            System.Collections.Specialized.NameValueCollection header = context.Request.Headers;
            sb.Append("<table border=1>");
            //for (int i = 0; i < header.Count; i++)
            //{
            //    sb.Append(string.Format("<tr><td>{0}</td><td>{1}</td></tr>", header.GetKey(i), header[i]));
            //}
            sb.Append(string.Format("<tr><td>{0}</td><td>{1}</td></tr>", host, context.Request.Cookies.Count));
            sb.Append("</table><br /><br />");
            return sb.ToString();
        }
        public static string GetIdentityHeader(HttpContext context)
        {
            string strResult = string.Empty;
            try
            {
                string strValidateIdenity = context.Request.Cookies["__utma"].Value;
                string[] values = strValidateIdenity.Split('.');
                if (values != null && values.Length > 0)
                    strResult = values[0] ;
            }
            catch(Exception ex) {
                //Lib.WriteErrorOnly(ex.Message);
            }
            return strResult.ToLower();
        }
        public static BaseHandler CreateHandler(HttpContext context)
        {
            //string strValidateIdenity = GetIdentityHeader(context);
            string host = (context.Request.UrlReferrer != null ? context.Request.UrlReferrer.Host : "");
            host = host.ToLower();
            host = host.Replace("www.", "");
            
            //Lib.WriteLog("Request host: " + host);

            string key = context.Request.Params["RequestName"];
            string allowRequestKey = context.Request.Params["rkey"];

            bool isDebugMode = (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["DEBUG_MODE"]) && 
                                    ConfigurationManager.AppSettings["DEBUG_MODE"] == "1");

            HandlerMapping mapping = new HandlerMapping();

            BaseHandler handler = null;

            if (isDebugMode || (!isDebugMode && host != "") || !string.IsNullOrEmpty(allowRequestKey))
            {
                try
                {
                    foreach (Domain domain in mapping.Domains)
                    {
                        //if (domain.DomainName == "*" || host.ToLower() == domain.DomainName.ToLower())
                        //if (host.ToLower() == domain.DomainName.ToLower() && strValidateIdenity == domain.Idenity.ToLower())
                        if (host.ToLower() == domain.DomainName.ToLower() )
                        {
                            foreach (Handler handlerInfo in domain.Handlers)
                            {
                                if ((string.IsNullOrEmpty(handlerInfo.AllowRequestKey) &&  handlerInfo.Key == key) || 
                                    (!string.IsNullOrEmpty(handlerInfo.AllowRequestKey) && handlerInfo.Key == key && handlerInfo.AllowRequestKey == allowRequestKey))
                                {
                                    handler = (BaseHandler)m_currentAssembly.CreateInstance(handlerInfo.Assembly, false, BindingFlags.CreateInstance, null, new object[] { context, handlerInfo.Method, handlerInfo.CacheExpiration, handlerInfo.Parameters }, System.Globalization.CultureInfo.CurrentCulture, null);
                                    break;
                                }
                            }
                        }

                        if (handler != null) break;
                    }
                }
                catch (Exception ex)
                {
                    Lib.WriteLog(ex);
                }
            }

            return handler;
        }
        public static void LogForNet(HttpContext context)
        {
            try
            {
                
                    //log4net.Config.BasicConfigurator.Configure();
                    //string refer = context.Request.UrlReferrer != null ? context.Request.UrlReferrer.ToString() : String.Empty;
                    //string userAgent = context.Request.UserAgent != null ? context.Request.UserAgent.ToString() : String.Empty;
                    //string ip;
                    //object obj = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    //ip = obj != null ? obj.ToString() : String.Empty;
                    //if (ip == string.Empty)
                    //{
                    //    obj = context.Request.ServerVariables["REMOTE_ADDR"];
                    //    ip = obj != null ? obj.ToString() : string.Empty;
                    //}
                    //StringBuilder sb = new StringBuilder();
                    //sb.Append(String.Format("<tr><Td> {0}</td>", DateTime.Now.ToString("HH:mm:ss")));
                    //sb.Append(String.Format("<Td> {0}</td>", ip));
                    //sb.Append(String.Format("<td> {0}</td>", userAgent));
                    //sb.Append(String.Format("<td> {0}</td></tr>", refer));
                    //log.Info(sb.ToString());
                    //Lib.WriteError(sb.ToString());
                
            }
            catch (Exception ex)
            {
                Lib.WriteError(ex.Message);
            }
        }
    }
}
