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

namespace CafeF_EmbedData.Common
{
    public static class HandlerFactory
    {
        private static readonly Assembly m_currentAssembly = Assembly.GetExecutingAssembly();

        public static BaseHandler CreateHandler(HttpContext context)
        {
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

            //string strValidateIdenity = GetIdentityHeader(context);
            if (isDebugMode || (!isDebugMode && host != "") || !string.IsNullOrEmpty(allowRequestKey))
            {
                try
                {
                    foreach (Domain domain in mapping.Domains)
                    {
                        //if (domain.DomainName == "*" || host.ToLower() == domain.DomainName.ToLower())
                        //if (host.ToLower() == domain.DomainName.ToLower() && strValidateIdenity == domain.Idenity.ToLower())
                       //
                        if (host.ToLower() == domain.DomainName.ToLower())
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
        public static string GetIdentityHeader(HttpContext context)
        {
            string strResult = string.Empty;
            try
            {
                string strValidateIdenity = context.Request.Cookies["__utma"].Value;
                string[] values = strValidateIdenity.Split('.');
                if (values != null && values.Length > 1)
                    strResult = values[0];
            }
            catch { }
            return strResult.ToLower();
        }
    }
}
