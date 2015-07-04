using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CafeF.Redis.BO;

namespace CafeF.Redis.Page.UserControl.LichSuKien
{
    public partial class LichSuKien_TomTat : System.Web.UI.UserControl
    {
        private bool m_IsFilterBySymbol;
        public bool IsFilterBySymbol
        {
            get
            {
                return this.m_IsFilterBySymbol;
            }
            set
            {
                this.m_IsFilterBySymbol = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string symbol = "";
                if (!string.IsNullOrEmpty(Request.QueryString["Symbol"]) && this.m_IsFilterBySymbol)
                {
                    symbol = Request.QueryString["Symbol"];
                }
                ltrMonth.Text = DateTime.Now.Month.ToString();
                int topEvents = Convert.ToInt32(ConfigurationManager.AppSettings["TopEvents_Small"]);

                using (DataTable dtEventCalendar = CompanyHelper_Update.SearchEventCalendar(string.Format("{0}",symbol.ToUpper()), topEvents,DateTime.Now.Month,DateTime.Now.Year))
                {
                    rptLichSukien.DataSource = dtEventCalendar;
                    rptLichSukien.DataBind();
                }
            }
        }

        protected void rptLichSukien_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                string urlFormat = "/{0}-{1}/{2}.chn";

                Literal ltrMaCK = e.Item.FindControl("ltrMaCK") as Literal;
                HyperLink lnkNoiDung = e.Item.FindControl("lnkNoiDung") as HyperLink;
                var ltrNgay = e.Item.FindControl("ltrNgay") as Literal;
                DataRowView drvEvent = e.Item.DataItem as DataRowView;
                string symbol = "";

                if (null != drvEvent["StockSymbols"] && DBNull.Value != drvEvent["StockSymbols"])
                {
                    symbol = drvEvent["StockSymbols"].ToString().Replace("&", "");
                }
                ltrMaCK.Text = symbol;
                lnkNoiDung.Text = drvEvent["TomTat"].ToString();
                lnkNoiDung.ToolTip = drvEvent["TomTat"].ToString();
                lnkNoiDung.NavigateUrl = string.Format(urlFormat, symbol, drvEvent["News_ID"], NewsHepler_Update.UnicodeToKoDauAndGach(drvEvent["Title"].ToString()));
                if (null != drvEvent["NgayBatDau"] && DBNull.Value != drvEvent["NgayBatDau"])
                {
                    ltrNgay.Text = Convert.ToDateTime(drvEvent["NgayBatDau"]).ToString("dd/MM");
                }
            }
        }
    }
}