using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CafeF.Redis.BL;
using CafeF.Redis.Entity;

namespace CafeF.Redis.Page.UserControl.StockView
{
    public partial class ucCongTyCon : System.Web.UI.UserControl
    {
        private Stock stock;
        protected string Symbol { get; set; }
        protected string CenterName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;  
        }

        public void LoadData(Stock mystock)
        {
            if(mystock==null || mystock.CompanyProfile==null) return;
            stock = mystock;
            Symbol = stock.Symbol;
            CenterName = Utils.GetCenterName(stock.TradeCenterId.ToString());
            rptData.DataSource = stock.CompanyProfile.Subsidiaries;
            rptData.DataBind();
            rptCtyLienKet.DataSource = stock.CompanyProfile.AssociatedCompanies;
            rptCtyLienKet.DataBind();
        }

        private string generateChart(double value)
        {
            var maxValue = 100;
            var minValue = 0;
            var maxWidth = 45;
            var chart = new StringBuilder();
            var color = "#8395ad";
            if (value < 51) color = "#8395ad";
            if (value > 0)
            {
                var __width = (int)((value / maxValue) * maxWidth);
                chart.Append("<div style='overflow: hidden;height:15px; background-color:#e3e8ed;width:" + maxWidth.ToString() + "px;' >");
                chart.Append("<div style='overflow: hidden;height:15px; background-color: " + color + ";width:" + __width.ToString() + "px;'><img alt='' style='width: " + __width.ToString() + "px;height: 15px' src='http://cafef3.vcmedia.vn/images/images/spacer.gif' /></div></div>");
            }
            return chart.ToString();
        }

        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                if (e.Item.DataItem != null)
                {
                    OtherCompany oc = (OtherCompany)e.Item.DataItem;
                    double value = ConvertUtility.ToDouble(oc.OwnershipRate);
                    Literal ltrChart = e.Item.FindControl("ltrChart") as Literal;
                    if (ltrChart != null) ltrChart.Text = generateChart(value);
                }
            }
        }

        protected void rptCtyLienKet_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                if (e.Item.DataItem != null)
                {
                    OtherCompany oc = (OtherCompany)e.Item.DataItem;
                    double value = ConvertUtility.ToDouble(oc.OwnershipRate);
                    Literal ltrChart = e.Item.FindControl("ltrChart") as Literal;
                    if (ltrChart != null) ltrChart.Text = generateChart(value);
                }
            }
        }
    }
}