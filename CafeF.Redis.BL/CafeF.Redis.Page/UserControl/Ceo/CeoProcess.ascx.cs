using System;
using System.Collections;
using System.Collections.Generic;
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

namespace CafeF.Redis.Page.UserControl.Ceo
{
    public partial class CeoProcess : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private List<CafeF.Redis.Entity.CeoProcess> GetCeoProcess
        {

            get
            {
                try
                {
                    return ((CafeF.Redis.Entity.Ceo)((CafeF.Redis.Page.MasterPage.StockMain)(((ContentPlaceHolder)this.Parent).Page).Master).GetCeo).CeoProcess;
                }
                catch
                {
                    return new List<CafeF.Redis.Entity.CeoProcess>();
                }
            }
        }
        private string GetSymbolForCEO
        {

            get
            {
                try
                {
                    return ((CafeF.Redis.Page.MasterPage.StockMain)(((ContentPlaceHolder)this.Parent).Page).Master).SymbolForCEO;
                }
                catch
                {
                    return "";
                }
            }
        }

        protected void BindData()
        {
            object objSymbol = Request.QueryString["ceocode"];
            if (objSymbol == null || objSymbol.ToString().Trim() == "")
                return;
            var sym = GetSymbolForCEO;
            var processes = GetCeoProcess;
            List<CafeF.Redis.Entity.CeoProcess> tblResult = processes.FindAll(s => (s.Symbol ?? "") == "" || s.Symbol == sym);
            if (tblResult.Count == 0)
            {
                var ss = processes.Find(s => (s.Symbol ?? "") != "");
                if (ss != null && !string.IsNullOrEmpty(ss.Symbol))
                {
                    tblResult = processes.FindAll(s => s.Symbol == ss.Symbol);
                }
            }
            if (tblResult != null && tblResult.Count > 0)
            {
                this.Visible = true;
                rpData.DataSource = tblResult;
                rpData.DataBind();
            }
            else
            {
                this.Visible = false;
            }
        }
    }
}