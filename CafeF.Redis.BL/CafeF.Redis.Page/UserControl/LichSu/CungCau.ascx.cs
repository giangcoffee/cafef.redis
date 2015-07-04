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
    public partial class CungCau : System.Web.UI.UserControl
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

            var order = OrderHistoryBL.get_OrderHistoryBySymbolAndDate(__symbol.ToUpper(), d1, d2, idx, pager1.PageSize, out nTotalItems);
            int count;
            var price = StockHistoryBL.get_StockHistoryBySymbolAndDate(__symbol.ToUpper(), order[order.Count - 1].TradeDate, order[0].TradeDate, 1, order.Count, out count);
            for (var i = 0; i < order.Count; i++)
            {
                var o = order[i];
                var da = o.TradeDate.ToString("ddMMyyyy");
                var p = price.Find(s => s.TradeDate.ToString("ddMMyyyy") == da);
                if (p == null) continue;
                o.BasicPrice = p.BasicPrice;
                o.Price = p.AdjustPrice > 0 ? p.AdjustPrice : (p.AveragePrice>0 ? p.AveragePrice : p.ClosePrice);
                o.Ceiling = p.Ceiling;
                o.Floor = p.Floor;
                o.BasicPrice = p.BasicPrice;
                o.Volume = p.Volume;
                order[i] = o;
            }
            rptData.DataSource = order;
            rptData.DataBind();
            pager1.ItemCount = nTotalItems;
        }

        protected long ConvertToLong(object Value)
        {
            try
            {
                return Int64.Parse(Value.ToString());
            }
            catch
            {
                return 0;
            }
        }

        private void FormatColor(Literal ltr, double basicPrice, double closePrice, Literal ltrImage)
        {
            double chgIndex = closePrice - basicPrice;
            double pctIndex = (closePrice + basicPrice) > 0 ? (chgIndex / (basicPrice)) * 100 : 0;

            string ImgUrl = "<img src='http://cafef3.vcmedia.vn/images/Scontrols/Images/LSG/{0}' align='absmiddle'> &nbsp;&nbsp;";
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

        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Literal ltrChange = e.Item.FindControl("ltrChange") as Literal;
                OrderHistory oh = (OrderHistory)e.Item.DataItem;
                long BID_VOLUME = (long)oh.BuyVolume;
                long OFFER_VOLUME = (long)oh.SellVolume;
                ltrChange.Text = String.Format("{0:#,##0}", (BID_VOLUME - OFFER_VOLUME));

                Literal ltrPrice = e.Item.FindControl("ltrPrice") as Literal;
                closePrice = oh.Price;
                basicPrice = oh.BasicPrice;
                if (closePrice == 0) basicPrice = 0;
                dlCeiling = oh.Ceiling;
                dlFloor = oh.Floor;

                ltrPrice.Text = FormatPrice(basicPrice, closePrice);
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

        private string FormatPrice(double basicPrice, double closePrice)
        {
            string strResult = string.Empty;
            double chgIndex = closePrice - basicPrice;
            double pctIndex = (closePrice + basicPrice) > 0 ? (chgIndex / (basicPrice)) * 100 : 0;
            string ImgUrl = " <img src='/Images/LSG/{0}' align='absmiddle' alt='' /> &nbsp;&nbsp;";
            ImgUrl = Math.Round(pctIndex, 1) == 0 ? String.Format(ImgUrl, "nochange_.jpg") : (Math.Round(pctIndex, 1) > 0) ? String.Format(ImgUrl, "up_.gif") : String.Format(ImgUrl, "down_.gif");
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
        
    }
}