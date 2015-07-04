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
    public delegate void SendMessageToThePageHandler(string messageToThePage);
    public partial class GiaoDichNuocNgoai : System.Web.UI.UserControl
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
        private double dlCeiling = 0, dlFloor = 0;
        private double basicPrice = 0, closePrice = 0;
        private int Floor_Code = 0;

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

        private void LoadData(int idx)
        {
            if (__symbol == "") return;
            int nTotalItems = 0;

            d1 = dpkTradeDate1.SelectedDate != DateTime.MinValue ? dpkTradeDate1.SelectedDate : DateTime.MinValue;
            d2 = dpkTradeDate2.SelectedDate != DateTime.MinValue ? dpkTradeDate2.SelectedDate : DateTime.MaxValue;

            var foreign = ForeignHistoryBL.get_ForeignHistoryBySymbolAndDate(__symbol.ToUpper(), d1, d2, idx, pager1.PageSize, out nTotalItems);
            int count;
            var price = StockHistoryBL.get_StockHistoryBySymbolAndDate(__symbol.ToUpper(), foreign[foreign.Count - 1].TradeDate, foreign[0].TradeDate, 1, foreign.Count, out count);
            for (var i = 0; i < foreign.Count; i++ )
            {
                var f = foreign[i];
                var da = f.TradeDate.ToString("ddMMyyyy");
                var p = price.Find(s => s.TradeDate.ToString("ddMMyyyy") == da);
                if (p == null) continue;
                f.BasicPrice = p.BasicPrice;
                f.ClosePrice = p.ClosePrice;
                f.AveragePrice = p.AveragePrice;
                foreign[i] = f;
            }
            rptData.DataSource = foreign;
            rptData.DataBind();
            pager1.ItemCount = nTotalItems;
        }

        private void FormatColor(Literal ltr, double basicPrice, double closePrice, Literal ltrImage)
        {
            double chgIndex = closePrice - basicPrice;
            double pctIndex = (closePrice + basicPrice) > 0 ? (chgIndex / (basicPrice)) * 100 : 0;

            string ImgUrl = " <img src='http://cafef3.vcmedia.vn/images/Scontrols/Images/LSG/{0}' align='absmiddle'> &nbsp;&nbsp;";
            ltrImage.Text = Math.Round(pctIndex, 1) == 0 ? String.Format(ImgUrl, "no_change.jpg") : (Math.Round(pctIndex, 1) > 0) ? String.Format(ImgUrl, "btup.gif") : String.Format(ImgUrl, "btdown.gif");
            string strChgIndex = String.Format("{0:#,##0.0}", chgIndex) + " (" + String.Format("{0:#,##0.0}", pctIndex) + " %" + ")";

            ltr.Text = Math.Round(pctIndex, 1) == 0 ? "<span class=Index_NoChange>" : (Math.Round(pctIndex, 1) > 0) ? "<span class=Index_Up>" : "<span class=Index_Down>";
            ltr.Text = strChgIndex + "</span>";
        }

        private string FormatPrice(double basicPrice, double closePrice)
        {
            string strResult = string.Empty;
            double chgIndex = closePrice - basicPrice;
            double pctIndex = (closePrice + basicPrice) > 0 ? (chgIndex / (basicPrice)) * 100 : 0;

            string ImgUrl = " <img src='http://cafef3.vcmedia.vn/images/Scontrols/Images/LSG/{0}' align='absmiddle'> &nbsp;&nbsp;";
            ImgUrl = Math.Round(pctIndex, 1) == 0 ? String.Format(ImgUrl, "nochange_.jpg") : (Math.Round(pctIndex, 1) > 0) ? String.Format(ImgUrl, "up_.gif") : String.Format(ImgUrl, "down_.gif");

            //string strChgIndex = String.Format("{0:#,##0.0}", chgIndex) + " (" + String.Format("{0:#,##0.0}", pctIndex) + " %" + ")";
            string strChgIndex = " (" + String.Format("{0:#,##0.0}", pctIndex) + " %" + ")";
            string styleColor = Math.Round(pctIndex, 1) == 0 ? "<span class=Index_NoChange>" : (Math.Round(pctIndex, 1) > 0) ? "<span class=Index_Up>" : "<span class=Index_Down>";
            if (Math.Round(closePrice, 1) == Math.Round(dlCeiling, 1))
                styleColor = "<span class=Index_Ceiling>";
            else
                if (Math.Round(closePrice, 1) == Math.Round(dlFloor, 1))
                    styleColor = "<span class=Index_Floor>";
            strResult = closePrice + styleColor + strChgIndex + "</span>" + ImgUrl;

            return strResult;
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
            try
            {
                switch (__symbol)
                {
                    case "VNINDEX":
                        Floor_Code = 1; break;
                    case "HNX-INDEX":
                        Floor_Code = 2; break;
                    case "UPCOM-INDEX":
                        Floor_Code = 9; break;
                    default:
                        Floor_Code = StockBL.GetStockCompactInfo(__symbol).TradeCenterId;
                        break;
                }
            }
            catch
            {
                Floor_Code = 0;
            }
            LoadData(pageNo);
            if (sendMessageToThePage != null)
            {
                sendMessageToThePage(__symbol);
            }
        }

        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                ForeignHistory fh = (ForeignHistory)e.Item.DataItem;
                Literal ltrROOM_CONLAI = e.Item.FindControl("ltrROOM_CONLAI") as Literal;
                Literal ltrSOHUU = e.Item.FindControl("ltrSOHUU") as Literal;

                ltrROOM_CONLAI.Text = String.Format("{0:#,###}", fh.Room);
                if (fh.Percent > 0)
                    ltrSOHUU.Text = String.Format("{0:#,##0.00}", fh.Percent) + "%";
                Literal ltrPrice = e.Item.FindControl("ltrPrice") as Literal;
                //StockHistory sh = (StockHistory)StockHistoryBL.get_StockHistoryByKey(fh.Symbol.ToUpper(), fh.TradeDate);
                closePrice = fh.ClosePrice; //sh.ClosePrice;
                basicPrice = fh.BasicPrice; // sh.BasicPrice;

                if ((Floor_Code != 1 && (__symbol.ToLower() != "vnindex" && __symbol.ToLower() != "hnx-index")) || Floor_Code == 11) //san ha noi and upcom 
                {
                    closePrice = fh.AveragePrice;// sh.AveragePrice;
                    if (closePrice <= 0)
                        closePrice = fh.ClosePrice;// sh.ClosePrice;
                }
                ltrPrice.Text = FormatPrice(basicPrice, closePrice);

                if (__symbol.ToLower() == "vnindex" || __symbol.ToLower() == "hnx-index" || __symbol.ToLower() == "upcom-index")
                    ltrROOM_CONLAI.Visible = false;
                else
                    ltrROOM_CONLAI.Visible = true;
            }
        }
    }
}