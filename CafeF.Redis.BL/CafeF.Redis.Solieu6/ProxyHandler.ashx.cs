using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;

using CafeF_EmbedData.Common;
using CafeF_EmbedData.Handlers;

namespace CafeF_EmbedData
{
    /// <summary>
    /// Request Sample: http://localhost:58221/ProxyHandler.ashx?RequestName=CurrentMarket&CallBack=OnLoaded&param1=1&param2=2&param3=3
    /// Cliend CallBack Method: OnLoaded(data, methodName);
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ProxyHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            using (BaseHandler handler = HandlerFactory.CreateHandler(context))
            {
                if (handler != null)
                {
                    // Log domain
                    //string host = (context.Request.UrlReferrer != null ? context.Request.UrlReferrer.Host : "");
                    //host = host.ToLower();
                    //host = host.Replace("www.", "");
                    //if (context.Cache[host] == null)
                    //{
                    //    Lib.WriteLog("[ " + host + " - " + DateTime.Now.ToString("dd/MM/yyyy, HH:mm:ss") + " - " + handler.MethodName + " ]");
                    //    context.Cache.Add(host, 1, null, DateTime.Now.AddMonths(6), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
                    //}
                    // -----------

                    handler.Execute();

                    string callbackMethodName = context.Request.QueryString["CallBack"];
                    string requestType = context.Request.QueryString["RequestType"];
                   
                    string ouput = "";

                    if (string.IsNullOrEmpty(callbackMethodName))
                    {
                        if (requestType == "json")
                        {
                            ouput = string.Format("var {0} = ({1});", context.Request.QueryString["RequestName"], handler.ToString());
                        }
                        else
                        {
                            ouput = handler.ToString();
                        }
                    }
                    else
                    {
                        if (requestType == "json")
                        {
                            ouput = string.Format("{0}(({1}), \"{2}\");", callbackMethodName, handler.ToString(), context.Request.QueryString["RequestName"]);
                        }
                        else
                        {
                            ouput = string.Format("{0}('{1}', \"{2}\");", callbackMethodName, handler.ToString(), context.Request.QueryString["RequestName"]);
                        }
                    }

                    context.Response.ContentType = "application/x-javascript";
                    context.Response.Write(ouput);
                }
                else
                {
                    context.Response.Write("Handler could not be created");
                }
            }
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
