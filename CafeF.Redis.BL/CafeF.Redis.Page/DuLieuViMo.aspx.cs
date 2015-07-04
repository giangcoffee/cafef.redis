using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace CafeF.Redis.Page
{
    public partial class DuLieuViMo : System.Web.UI.Page
    {
        private Control ctl = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            
                string type = Request.QueryString["dlvmtype"] != null ? Request.QueryString["dlvmtype"] : "";
                switch (type)
                {
                    case "gdp":
                        {
                            ctl = LoadControl("~/UserControl/DuLieuViMo/GDPChart.ascx");
                            a_gdp.Attributes.Add("style", "color:#C00000");
                            break;
                        }
                    case "slcn":
                        {
                            ctl = LoadControl("~/UserControl/DuLieuViMo/SLCNChart.ascx");
                            a_slcn.Attributes.Add("style", "color:#C00000");
                            break;
                        }
                    case "tmbl":
                        {
                            ctl = LoadControl("~/UserControl/DuLieuViMo/TMBLChart.ascx");
                            a_tmbl.Attributes.Add("style", "color:#C00000");
                            break;
                        }
                    case "cpisau":
                        {
                            ctl = LoadControl("~/UserControl/DuLieuViMo/CPIAfterChart.ascx");
                            a_cpisau.Attributes.Add("style", "color:#C00000");
                            break;
                        }
                    case "cpitruoc":
                        {
                            ctl = LoadControl("~/UserControl/DuLieuViMo/CPIBeforeChart.ascx");
                            a_cpitruoc.Attributes.Add("style", "color:#C00000");
                            break;
                        }
                    case "xnk":
                        {
                            ctl = LoadControl("~/UserControl/DuLieuViMo/XNKChart.ascx");
                            a_xnk.Attributes.Add("style", "color:#C00000");
                            break;
                        }
                    case "fdi":
                        {
                            ctl = LoadControl("~/UserControl/DuLieuViMo/FDIChart.ascx");
                            a_fdi.Attributes.Add("style", "color:#C00000");
                            break;
                        }
                    case "dttnsnn":
                        {
                            ctl = LoadControl("~/UserControl/DuLieuViMo/BCNSNN.ascx");
                            a_nsnn.Attributes.Add("style", "color:#C00000");
                            break;
                        }
                    default:
                        ctl = LoadControl("~/UserControl/DuLieuViMo/GDPChart.ascx");
                        a_gdp.Attributes.Add("style", "color:#C00000");
                        break;
                }
                pldContent.Controls.Clear();
                pldContent.Controls.Add(ctl);
           
        }
    }
}
