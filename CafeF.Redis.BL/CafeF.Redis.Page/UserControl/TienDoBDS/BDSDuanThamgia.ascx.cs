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

namespace CafeF.Redis.Page.UserControl.TienDoBDS
{
    public partial class BDSDuanThamgia : System.Web.UI.UserControl
    {
        protected string style = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private List<CafeF.Redis.Entity.TienDoBDS> GetStockTienDoBDS
        {

            get
            {
                try
                {
                    return ((CafeF.Redis.Page.MasterPage.StockMain)(((ContentPlaceHolder)this.Parent).Page).Master).GetStockTienDoBDS;
                }
                catch
                {
                    return new List<CafeF.Redis.Entity.TienDoBDS>();
                }
            }
        }
        protected int getPageIdx(int idx, int pagecount)
        {
            int count = (idx % pagecount == 0) ? (idx / pagecount) : (idx / pagecount + 1);
            return count;
        }

        protected void BindData()
        {
            object objSymbol = Request.QueryString["symbol"];
            if (objSymbol == null || objSymbol.ToString().Trim() == "")
            {
                this.Visible = false;
                return;
            }
            List<CafeF.Redis.Entity.TienDoBDS> tblResult = GetStockTienDoBDS;
            if (tblResult != null && tblResult.Count > 0)
            {
                if (tblResult.Count > 0)
                {
                    style = "";
                    rpData.DataSource = tblResult;
                    rpData.DataBind();
                }
                else
                {
                    this.Visible = false;
                    style = "display:none";
                }
            }
            else
            {
                this.Visible = false;
            }
        }

        protected void rpData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CafeF.Redis.Entity.TienDoBDS data = (CafeF.Redis.Entity.TienDoBDS)e.Item.DataItem;
                Literal ltrGhichu = (Literal)e.Item.FindControl("ltrGhichu");
                Literal ltDate = (Literal)e.Item.FindControl("ltDate");
               
                if ((data.ViewDate != null) && (data.ViewDate != DateTime.MinValue))
                    ltDate.Text = "(Tính đến " + data.ViewDate.ToString("dd/MM/yyyy") + ")";

                ltrGhichu.Text = CafeF.Redis.BL.Utils.SubStringSpace(data.GhiChu, 50, "..."); 
            }
        }

       
    }
}