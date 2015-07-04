using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Script.Serialization;

using System.Xml;
using System.Collections;

namespace CafeF_EmbedData.Common.Chart
{
    public class AllowChartRequest
    {
        public AllowChartRequest()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(System.Web.HttpContext.Current.Server.MapPath("/HandlerMapping/AllowChartRequest.config"));

            this.Domains = new DomainCollection();

            if (xmlDoc != null && xmlDoc.DocumentElement.ChildNodes.Count > 0)
            {
                XmlNodeList nodeDomains = xmlDoc.DocumentElement.SelectNodes("//AllowDomains/AllowDomain");

                for (int domainIndex = 0; domainIndex < nodeDomains.Count; domainIndex++)
                {
                    string domainName = nodeDomains[domainIndex].Attributes["domain"].Value;
                    Domain newDomain = new Domain(domainName);

                    XmlNodeList nodeCharts = nodeDomains[domainIndex].SelectNodes("//chart");

                    for (int chartIndex = 0; chartIndex < nodeCharts.Count; chartIndex++)
                    {
                        string file = nodeCharts[chartIndex].Attributes["file"].Value;
                        string parameters = nodeCharts[chartIndex].Attributes["params"].Value;
                        int cacheExpiration = Lib.Object2Integer(nodeCharts[chartIndex].Attributes["cache"].Value);
                        
                        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                        List<string> listOfParams = new List<string>();
                        if (parameters != "")
                        {
                            listOfParams = jsSerializer.Deserialize<List<string>>("[" + parameters + "]");
                        }

                        newDomain.Charts.Add(new Chart(file, cacheExpiration, listOfParams.ToArray()));
                    }

                    this.Domains.Add(newDomain);
                }
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

        public Chart IsAllowRequest(string host, string file, bool isDebugMode)
        {
            host = host.ToLower();

            file = file.ToLower();
            if (file.IndexOf("/") >= 0) file = file.Substring(file.LastIndexOf("/") + 1);
            for (int i = 0; i < this.m_Domains.Count; i++)
            {
                if (this.m_Domains[i].DomainName == "*" || host == this.m_Domains[i].DomainName)
                {
                    for (int j = 0; j < this.m_Domains[i].Charts.Count; j++)
                    {
                        if (file == this.m_Domains[i].Charts[j].File)
                        {
                            return this.m_Domains[i].Charts[j];
                        }
                    }
                }
            }

            return null;
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
        private ChartCollection m_Charts;

        public Domain(string domainName)
        {
            this.m_DomainName = domainName;
            this.m_Charts = new ChartCollection();
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

        public ChartCollection Charts
        {
            get
            {
                return this.m_Charts;
            }
            set
            {
                this.m_Charts = value;
            }
        }
    }

    public class ChartCollection : CollectionBase
    {
        public void Add(Chart item)
        {
            this.List.Add(item);
        }

        public Chart this[int index]
        {
            get
            {
                return (Chart)this.List[index];
            }
            set
            {
                this.List[index] = (Chart)value;
            }
        }
    }

    public class Chart
    {
        private string m_File;
        private string[] m_Parameters;
        private int m_CacheExpiration;

        public Chart(string file, int cacheExpiration, string[] parameters)
        {
            this.m_File = file;
            this.m_Parameters = parameters;
            this.m_CacheExpiration = cacheExpiration;
        }

        public string File
        {
            get
            {
                return this.m_File;
            }
            set
            {
                this.m_File = value;
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
    }
}
