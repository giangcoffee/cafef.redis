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
    public partial class CeoNews : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private List<CafeF.Redis.Entity.CeoNews> GetCeoNews
        {

            get
            {
                try
                {
                    return ((CafeF.Redis.Entity.Ceo)((CafeF.Redis.Page.MasterPage.StockMain)(((ContentPlaceHolder)this.Parent).Page).Master).GetCeo).CeoNews;
                }
                catch
                {
                    return new List<CafeF.Redis.Entity.CeoNews>();
                }
            }
        }

        protected void BindData()
        {
            object objSymbol = Request.QueryString["ceocode"];
            if (objSymbol == null || objSymbol.ToString().Trim() == "")
                return;

            List<CafeF.Redis.Entity.CeoNews> tblResult = GetCeoNews;
            if (tblResult != null && tblResult.Count > 0)
            {
                divNews.Visible = true;
                rpData.DataSource = tblResult;
                rpData.DataBind();
            }
            else
            {
                divNews.Visible = false;
            }
        }
        protected string ProcessDate(object d)
        {
            try
            {
                return ((DateTime) d).ToString("dd/MM HH:mm");
            }
            catch (Exception)
            {
                return "";
                throw;
            }
        }
    }
}