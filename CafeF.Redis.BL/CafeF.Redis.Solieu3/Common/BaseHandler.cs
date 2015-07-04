using System;
using System.Collections;
using System.Diagnostics;
using System.Web;
using System.Web.Script.Serialization;

namespace CafeF_EmbedData.Common
{
    public abstract class BaseHandler : IDisposable
    {
        private HttpContext m_Context;
        private object m_Output = null;
        private string m_Method;
        private string[] m_Parameters;
        private int m_CacheExpiration;

        protected HttpRequest CurrentRequest
        {
            get
            {
                return m_Context.Request;
            }
        }

        protected HttpContext CurrentContext
        {
            get
            {
                return m_Context;
            }
        }

        protected int CacheExpiration
        {
            get
            {
                return m_CacheExpiration;
            }
        }

        protected string[] Parameters
        {
            get
            {
                return m_Parameters;
            }
        }

        protected void UpdateOutput(object ouput)
        {
            m_Output = ouput;
        }

        protected bool CacheExists(string cacheName)
        {
            return (CurrentContext.Cache[cacheName] != null);
        }

        protected void UpdateOutputInCache(string cacheName)
        {
            m_Output = CurrentContext.Cache[cacheName];
        }

        protected void UpdateCache(string cacheName, int cacheExpiration, string value)
        {
            CurrentContext.Cache.Add(cacheName, value, null, DateTime.Now.AddSeconds(cacheExpiration), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            //Lib.WriteLog("Updated cache " + cacheName + "|" + cacheExpiration.ToString());
        }

        public string MethodName
        {
            get
            {
                return this.m_Method;
            }
        }

        public override string ToString()
        {
            try
            {
                return m_Output.ToString();
            }
            catch
            {
                return "";
            }
        }

        protected string ToJson()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(m_Output);
        }

        public object ToObject()
        {
            return m_Output;
        }

        protected BaseHandler(HttpContext context, string method, params string[] parameters)
        {
            m_Context = context;
            this.m_Method = method;
            m_Parameters = parameters;
            m_CacheExpiration = 0;
        }

        protected BaseHandler(HttpContext context, string method, int cacheExpiration, params string[] parameters)
        {
            m_Context = context;
            this.m_Method = method;
            m_Parameters = parameters;
            m_CacheExpiration = cacheExpiration;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
        }

        ~BaseHandler()
        {
            Dispose(false);
        }

        public void Execute()
        {
            try
            {
                this.GetType().InvokeMember(this.MethodName, System.Reflection.BindingFlags.InvokeMethod, null, this, null);
                //Lib.WriteLog(this.MethodName + "-" + CacheExpiration.ToString());
            }
            catch (Exception ex)
            {
                Lib.WriteLog(ex);

                m_Output = null;
            }
        }
    }
}
