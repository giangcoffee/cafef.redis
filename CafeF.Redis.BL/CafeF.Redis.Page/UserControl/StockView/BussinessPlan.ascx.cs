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
using CafeF.Redis.Entity;
using CafeF.Redis.BL;
namespace CafeF.Redis.Page.UserControl.StockView
{
    public partial class BussinessPlan : System.Web.UI.UserControl
    {
        public List<CafeF.Redis.Entity.BussinessPlan> GetBussinessPlan
        {
            get 
            {
                try
                {
                    return ((Stock)((CafeF.Redis.Page.MasterPage.Main)(((ContentPlaceHolder)this.Parent).Page).Master).GetStock).BussinessPlans1;
                }
                catch
                {
                    return new List<CafeF.Redis.Entity.BussinessPlan>();
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }
        protected void BindData()
        {
            object objSymbol = Request.QueryString["symbol"];
            if (objSymbol == null || objSymbol.ToString().Trim() == "")
                return;
            List<CafeF.Redis.Entity.BussinessPlan> tblResult = GetBussinessPlan;

            if (tblResult != null && tblResult.Count > 0)
            {
                CafeF.Redis.Entity.BussinessPlan bp = tblResult[0];
                ltDoanhthu.Text = bp.Revenue == null ? "N/A" : bp.Revenue;
                ltLoinhuanTruocthue.Text = bp.ProfitBTax == null ? "N/A" : bp.ProfitBTax;
                ltLoinhuanSauthue.Text = bp.ProfitATax == null ? "N/A" : bp.ProfitATax;
                ltCotucTienmat.Text = bp.DividendsMoney == null ? "N/A" : bp.DividendsMoney;
                ltCotucCophieu.Text = bp.DividendsStock == null ? "N/A" : bp.DividendsStock;
                ltTangVon.Text = bp.IncreaseExpected == null ? "N/A" : bp.IncreaseExpected;
                ltrLink.Text = "<a href=\"javascript:void(0);\" class=\"tt\">Xem chi tiết >><span class=\"tooltip\"><span class=\"top\"></span><span class=\"middle\"><strong>Chi tiết phương án kinh doanh năm 2010</strong><br />" + bp.Body + "</span><span class=\"bottom\"></span></span></a>";
            }
            //else
                //divCompany.Visible = false;
        }
    }
}