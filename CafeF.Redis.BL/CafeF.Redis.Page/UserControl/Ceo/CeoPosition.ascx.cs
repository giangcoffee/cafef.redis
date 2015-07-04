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
    public partial class CeoPosition : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private List<CafeF.Redis.Entity.CeoPosition> GetCeoPosition
        {

            get
            {
                try
                {
                    return ((CafeF.Redis.Entity.Ceo)((CafeF.Redis.Page.MasterPage.StockMain)(((ContentPlaceHolder)this.Parent).Page).Master).GetCeo).CeoPosition;
                }
                catch
                {
                    return new List<CafeF.Redis.Entity.CeoPosition>();
                }
            }
        }

        protected void BindData()
        {
            object objSymbol = Request.QueryString["ceocode"];
            if (objSymbol == null || objSymbol.ToString().Trim() == "")
                return;

            List<CafeF.Redis.Entity.CeoPosition> tblResult = GetCeoPosition;
            if (tblResult != null && tblResult.Count > 0)
            {
                divPosition.Visible = true;
                rpData.DataSource = tblResult;
                rpData.DataBind();
            }
            else
            {
                divPosition.Visible = false;
            }
        }
    }
}