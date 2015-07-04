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
using CafeF.Redis.Page;

namespace CafeF.Redis.Page.UserControl.LichSu
{
    public partial class CoPhieuQuy : System.Web.UI.UserControl
    {
        public event SendMessageToThePageHandler sendMessageToThePage;
        string __symbol;
        DateTime d1 = DateTime.MinValue;
        DateTime d2 = DateTime.MinValue;
        string Symbol
        {
            get
            {
                return __symbol;
            }
            set { __symbol = value; }
        }
        string __CompanyName = string.Empty;
        public string CompanyName
        {
            set { __CompanyName = value; }
            get { return __CompanyName; }
        }
        public const string __tit_Symbol = "Gõ mã CK hoặc Tên công ty";
        public const string __tit_ToChuc = "Gõ tên tổ chức/cá nhân";
        private string[] getKeysParts(DateTime d1, DateTime d2)
        {
            List<string> keysParts = new List<string>();
            while (d1.Month <= d2.Month)
            {
                keysParts.Add(string.Format("{0:MMyyyy}", d1));
                d1 = d1.AddMonths(1);
            }
            return keysParts.ToArray();
        }

        private void LoadData(int idx)
        {
            if (__symbol == "") return;

            int nTotalItems = 0;

            d1 = dpkTradeDate1.SelectedDate != DateTime.MinValue ? dpkTradeDate1.SelectedDate : DateTime.MinValue;
            d2 = dpkTradeDate2.SelectedDate != DateTime.MinValue ? dpkTradeDate2.SelectedDate : DateTime.MaxValue;

            rptData.DataSource = StockHistoryBL.get_FundHistoryBySymbolAndDate(__symbol.ToUpper(), d1, d2, idx, pager1.PageSize, out nTotalItems);
            rptData.DataBind();
            pager1.ItemCount = nTotalItems;
        }

        protected float ConvertToFloat(object Value)
        {
            try
            {
                return float.Parse(Value.ToString());
            }
            catch
            {
                return 0;
            }
        }

        public static string returnBS(string value)
        {
            string re = "";
            if (value.Trim().Equals("1/1/1999 12:00:00 AM"))
            {
                //return "";
            }
            else
            {
                re = DateTime.Parse(value).ToString("dd/MM/yyyy");
            }
            return re;
        }

        public static string returnEmpty(string value)
        {
            if (value.Trim().Equals("0"))
            {
                return "";
            }
            else
            {
                return String.Format("{0:f}", float.Parse(value)) + "%";
            }
        }

        private void FormatColor(Literal ltr, double basicPrice, double closePrice, Literal ltrImage)
        {
            double chgIndex = closePrice - basicPrice;
            double pctIndex = (closePrice + basicPrice) > 0 ? (chgIndex / (basicPrice)) * 100 : 0;

            string ImgUrl = " <img src='http://cafef.vn/images/{0}' align='absmiddle'> &nbsp;&nbsp;";
            ltrImage.Text = Math.Round(pctIndex, 1) == 0 ? String.Format(ImgUrl, "no_change.jpg") : (Math.Round(pctIndex, 1) > 0) ? String.Format(ImgUrl, "btup.gif") : String.Format(ImgUrl, "btdown.gif");
            string strChgIndex = String.Format("{0:#,##0.0}", chgIndex) + " (" + String.Format("{0:#,##0.0}", pctIndex) + " %" + ")";

            ltr.Text = Math.Round(pctIndex, 1) == 0 ? "<span class=Index_NoChange>" : (Math.Round(pctIndex, 1) > 0) ? "<span class=Index_Up>" : "<span class=Index_Down>";
            ltr.Text = strChgIndex + "</span>";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["Symbol"] != null)
                    txtKeyword.Text = ConvertUtility.ToString(Request.Params["Symbol"]).ToUpper();
                if (txtKeyword.Text.Trim() == "ALL") txtKeyword.Text = "";
                //dpkTradeDate2.SelectedDate = DateTime.Now;
                //dpkTradeDate1.SelectedDate = DateTime.Now.AddMonths(-1);
                Searching(1);
            }
        }
     
        protected void btSearch_Click(object sender, ImageClickEventArgs e)
        {
            Searching(1);
           
        }

        protected void pager1_Command(object sender, CommandEventArgs e)
        {
            int currnetPageIndx = Convert.ToInt32(e.CommandArgument);
            pager1.CurrentIndex = currnetPageIndx;
            Searching(currnetPageIndx);
        }

        protected void Searching(int pageNo)
        {
            __symbol = txtKeyword.Text.Trim().ToUpper();
            LoadData(pageNo);
            if (sendMessageToThePage != null)
            {
                sendMessageToThePage(__symbol);
            }
        }
    }
}