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
    public partial class BusinessPlan : System.Web.UI.UserControl
    {
        public List<CafeF.Redis.Entity.BusinessPlan> GetBusinessPlan
        {
            get 
            {
                try
                {
                    return ((Stock)((CafeF.Redis.Page.MasterPage.StockMain)(((ContentPlaceHolder)this.Parent).Page).Master).GetStock).BusinessPlans1;
                }
                catch
                {
                    return new List<CafeF.Redis.Entity.BusinessPlan>();
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
            List<CafeF.Redis.Entity.BusinessPlan> tblResult = GetBusinessPlan;

            if (tblResult != null && tblResult.Count > 0)
            {
                CafeF.Redis.Entity.BusinessPlan bp = tblResult[0];
                ltrNam.Text = bp.Year.ToString().ToUpper();
                ltDoanhthu.Text = ConvertUtility.ToDouble( bp.Revenue ) == 0 ? "N/A" : String.Format("{0:#,##0.##}", bp.Revenue) + " tỷ"; 
                ltLoinhuanTruocthue.Text = ConvertUtility.ToDouble(bp.ProfitBTax) == 0? "N/A" : String.Format("{0:#,##0.##}", bp.ProfitBTax) + " tỷ"; 
                ltLoinhuanSauthue.Text = ConvertUtility.ToDouble(bp.ProfitATax) == 0? "N/A" : String.Format("{0:#,##0.##}", bp.ProfitATax) + " tỷ";
                ltCotucTienmat.Text = ConvertUtility.ToDouble(bp.DividendsMoney) == 0? "N/A" : String.Format("{0:#,##0.##}", bp.DividendsMoney) + " %";
                ltCotucCophieu.Text = ConvertUtility.ToDouble(bp.DividendsStock) == 0? "N/A" : String.Format("{0:#,##0.##}", bp.DividendsStock) + " %";
                ltTangVon.Text = ConvertUtility.ToDouble(bp.IncreaseExpected )== 0? "N/A" : String.Format("{0:#,##0.##}", bp.IncreaseExpected) + " tỷ";
                ltrLink.Text = "<a href=\"javascript:void(0);\" class=\"tt\" style=\"color:#003366\">Xem chi tiết<span class=\"tooltip\"><span class=\"top\"></span><span class=\"middle\"><strong>Chi tiết phương án kinh doanh năm "+bp.Year+"</strong><br />" + bp.Body + "</span><span class=\"bottom\"></span></span></a>";
            }
            else
                this.Visible = false;
        }
    }
}