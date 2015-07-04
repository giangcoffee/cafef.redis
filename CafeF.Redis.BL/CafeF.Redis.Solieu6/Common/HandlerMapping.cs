using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Script.Serialization;

using System.Xml;
using System.Collections;

namespace CafeF_EmbedData.Common
{
    public class HandlerMapping
    {
        private const string CACHE_NAME = "CacheName_HandlerMapping";
        private const long CACHE_EXPIRED = 31104000; // tính theo giây

        public HandlerMapping()
        {
            if (null != HttpContext.Current.Cache[CACHE_NAME])
            {
                try
                {
                    this.Domains = (DomainCollection)HttpContext.Current.Cache[CACHE_NAME];
                    return;
                }
                catch
                {
                }
            }

            string configFilePath = System.Web.HttpContext.Current.Server.MapPath("/HandlerMapping/HandlerMapping.config");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(configFilePath);

            this.Domains = new DomainCollection();

            if (xmlDoc != null && xmlDoc.DocumentElement.ChildNodes.Count > 0)
            {
                XmlNodeList nodeDomains = xmlDoc.DocumentElement.SelectNodes("//AllowDomains/AllowDomain");

                for (int domainIndex = 0; domainIndex < nodeDomains.Count; domainIndex++)
                {
                    string domainName = nodeDomains[domainIndex].Attributes["domain"].Value;
                    string idenity = string.Empty;
                    if (nodeDomains[domainIndex].Attributes["idenity"] != null)
                        idenity = nodeDomains[domainIndex].Attributes["idenity"].Value;
                    Domain newDomain = new Domain(domainName, idenity);

                    XmlNodeList nodeHandlers = nodeDomains[domainIndex].SelectNodes("handler");

                    for (int handlerIndex = 0; handlerIndex < nodeHandlers.Count; handlerIndex++)
                    {
                        string key = nodeHandlers[handlerIndex].Attributes["key"].Value;
                        string assembly = nodeHandlers[handlerIndex].Attributes["assembly"].Value;
                        string method = nodeHandlers[handlerIndex].Attributes["method"].Value;
                        string parameters = nodeHandlers[handlerIndex].Attributes["params"].Value;
                        int cacheExpiration = Lib.Object2Integer(nodeHandlers[handlerIndex].Attributes["cache"].Value);
                        string allowRequestKey = nodeHandlers[handlerIndex].Attributes["AllowRequestKey"].Value;
                        
                        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                        List<string> listOfParams = new List<string>();
                        if (parameters != "")
                        {
                            listOfParams = jsSerializer.Deserialize<List<string>>("[" + parameters + "]");
                        }

                        newDomain.Handlers.Add(new Handler(key, assembly, method, cacheExpiration, allowRequestKey, listOfParams.ToArray()));
                    }

                    this.Domains.Add(newDomain);
                }

                CacheDependency fileDependency = new CacheDependency(configFilePath);
                HttpContext.Current.Cache.Insert(CACHE_NAME, this.Domains, fileDependency, DateTime.Now.AddSeconds(CACHE_EXPIRED), TimeSpan.Zero, CacheItemPriority.Normal, null);

            }
        }

        private DomainCollection m_Domains;

        public DomainCollection Domains
        {
            get
            {
                return this.m_Domains;
            }
            set
            {
                this.m_Domains = value;
            }
        }
    }

    public class DomainCollection : CollectionBase
    {
        public void Add(Domain item)
        {
            this.List.Add(item);
        }

        public Domain this[int index]
        {
            get
            {
                return (Domain)this.List[index];
            }
            set
            {
                this.List[index] = (Domain)value;
            }
        }
    }

    public class Domain
    {
        private string m_DomainName;
        private string m_Idenity;
        private HandlerCollection m_Handlers;

        public Domain(string domainName, string idenity)
        {
            this.m_DomainName = domainName;
            this.m_Idenity = idenity;
            this.m_Handlers = new HandlerCollection();
        }

        public string DomainName
        {
            get
            {
                return this.m_DomainName;
            }
            set
            {
                this.m_DomainName = value;
            }
        }
        public string Idenity
        {
            get
            {
                return this.m_Idenity;
            }
            set
            {
                this.m_Idenity = value;
            }
        }
        public HandlerCollection Handlers
        {
            get
            {
                return this.m_Handlers;
            }
            set
            {
                this.m_Handlers = value;
            }
        }
    }

    public class HandlerCollection : CollectionBase
    {
        public void Add(Handler item)
        {
            this.List.Add(item);
        }

        public Handler this[int index]
        {
            get
            {
                return (Handler)this.List[index];
            }
            set
            {
                this.List[index] = (Handler)value;
            }
        }
    }

    public class Handler
    {
        private string m_Key;
        private string m_Assembly;
        private string m_Method;
        private string[] m_Parameters;
        private int m_CacheExpiration;
        private string m_AllowRequestKey;

        public Handler(string key, string assembly, string method, int cacheExpiration, string allowRequestKey, string[] parameters)
        {
            this.m_Key = key;
            this.m_Assembly = assembly;
            this.m_Method = method;
            this.m_Parameters = parameters;
            this.m_CacheExpiration = cacheExpiration;
            this.AllowRequestKey = allowRequestKey;
        }

        public string Key
        {
            get
            {
                return this.m_Key;
            }
            set
            {
                this.m_Key = value;
            }
        }
        public string Assembly
        {
            get
            {
                return this.m_Assembly;
            }
            set
            {
                this.m_Assembly = value;
            }
        }
        public string Method
        {
            get
            {
                return this.m_Method;
            }
            set
            {
                this.m_Method = value;
            }
        }
        public int CacheExpiration
        {
            get
            {
                try
                {
                    return this.m_CacheExpiration;
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                this.m_CacheExpiration = value;
            }
        }
        public string[] Parameters
        {
            get
            {
                return this.m_Parameters;
            }
            set
            {
                this.m_Parameters = value;
            }
        }
        public string AllowRequestKey
        {
            get
            {
                try
                {
                    return this.m_AllowRequestKey;
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                this.m_AllowRequestKey = value;
            }
        }
    }
}
