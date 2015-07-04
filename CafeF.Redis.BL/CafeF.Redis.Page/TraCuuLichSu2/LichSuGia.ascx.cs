using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CafeF.Redis.Entity;
using CafeF.Redis.BL;
using CafeF.Redis.Page;
namespace CafeF.Redis.Page.TraCuuLichSu2
{
    public partial class LichSuGia : System.Web.UI.UserControl
    {
        public String san;
        public String date;
        public String dateTextBox;
        protected void Page_Load(object sender, EventArgs e)
        {
            san = String.IsNullOrEmpty(Request.QueryString["san"]) ? String.Empty : Request.QueryString["san"];
            date = String.IsNullOrEmpty(Request.QueryString["date"]) ? String.Empty : Request.QueryString["date"];

            san = String.IsNullOrEmpty(san) ? "HOSE" : san;

            DateTime tradeDate;// = ConvertDateTime(date);
            if (!(DateTime.TryParseExact(date, "dd/MM/yyyy/", CultureInfo.InvariantCulture, DateTimeStyles.None, out tradeDate)))
            {
                var dtHoIndex = TradeCenterBL.getByTradeCenter((int)(san == "HOSE" ? TradeCenter.HoSE : TradeCenter.HaSTC));
                tradeDate = dtHoIndex.CurrentDate;
            }
            //// Thoi gian giao dich
            //if (tradeDate == DateTime.Today && DateTime.Now < (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 15, 0)))
            //{
            //    if (tradeDate.DayOfWeek == DayOfWeek.Monday)
            //    {
            //        tradeDate = tradeDate.AddDays(-3);
            //    }
            //    else
            //    {
            //        tradeDate = tradeDate.AddDays(-1);
            //    }
            //}
            //// CN
            //else if (tradeDate.DayOfWeek == DayOfWeek.Sunday)
            //{
            //    tradeDate = tradeDate.AddDays(-2);

            //}
            //// Thu 7
            //else if (tradeDate.DayOfWeek == DayOfWeek.Saturday)
            //{
            //    tradeDate = tradeDate.AddDays(-1);
            //}

            date = tradeDate.ToString("dd/MM/yyyy");

            dateTextBox = tradeDate.ToString("dd/MM/yyyy");
            List<StockHistory> data = StockHistoryBL.get_StockHistoryByCenterAndDate((san == "HOSE" ? 1 : (san == "HASTC" ? 2 : 9)), tradeDate);
            data.Sort(delegate(StockHistory s2, StockHistory s1)
                        {
                            return (s1.BasicPrice == 0 ? 100 : ((s1.ClosePrice - s1.BasicPrice) / s1.BasicPrice)).CompareTo(s2.BasicPrice == 0 ? 100 : ((s2.ClosePrice - s2.BasicPrice) / s2.BasicPrice));
                        });
            rptData.DataSource = data;
            rptData.DataBind();

            string centerLabel = string.Empty;
            string index = string.Empty;
            if (san == "HOSE")
            {
                index = "VNINDEX";
                centerLabel = "VNINDEX: ";
            }
            if (san == "HASTC")
            {
                index = "HNX-INDEX";
                centerLabel = "HNX-INDEX: ";
            }
            if (san == "UPCOM")
            {
                index = "UPCOM-INDEX";
                centerLabel = "UPCOM-INDEX: ";
            }
            StockHistory sh = StockHistoryBL.get_StockHistoryByKey(index, tradeDate);
            double closePrice = sh.ClosePrice;
            double basicPrice = sh.BasicPrice;
            double chgIndex = closePrice - basicPrice;
            double pctIndex = (closePrice + basicPrice) > 0 ? (chgIndex / (basicPrice)) * 100 : 0;
            lblIndexPoint.InnerHtml = centerLabel + Math.Round(sh.ClosePrice, 1) + "&nbsp;điểm" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src='" + specificWhatIconToShow(chgIndex) + "'/>" + Math.Round(chgIndex, 2) + "&nbsp;điểm, tương đương&nbsp;<img src='" + specificWhatIconToShow(chgIndex) + "'>" + Math.Round(pctIndex, 2) + "&nbsp;%";

            div1.InnerHtml = "<span style='font-weight:bold;'>KLGD khớp lệnh:</span> " + String.Format("{0:#,##0}", sh.Volume) + " cổ phiếu;&nbsp;&nbsp;&nbsp; <span style='font-weight:bold;'>GTGD khớp lệnh:</span> " + String.Format("{0:#,##0.0}", sh.TotalValue / 1000000000) + " tỉ đồng";
            div2.InnerHtml = "<span style='font-weight:bold;'>KLGD thỏa thuận:</span> " + String.Format("{0:#,##0}", sh.AgreedVolume) + " cổ phiếu;&nbsp;&nbsp;&nbsp; <span style='font-weight:bold;'>GTGD thỏa thuận:</span> " + String.Format("{0:#,##0.0}", sh.AgreedValue / 1000000000) + " tỉ đồng";
            divTotalDown.InnerHtml = "<img src='http://cafef3.vcmedia.vn/images/btdown.gif'> " + this.totalStockDown.ToString() + "cp&nbsp;";
            divToTalUp.InnerHtml = "<img src='http://cafef3.vcmedia.vn/images/btup.gif'> " + this.totalStockUp.ToString() + "cp&nbsp;";
            divTotalNochange.InnerHtml = "<img src='http://cafef3.vcmedia.vn/images/no_change.jpg'> " + this.totalStockNochange.ToString() + "cp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

            spanKichSan.InnerHtml = "(<img src='http://cafef3.vcmedia.vn/images/Floor_.gif'> " + this.totalKichsan.ToString() + "cp)";
            spanKichTran.InnerHtml = "(<img src='http://cafef3.vcmedia.vn/images/Ceiling_.gif'> " + this.totalKichtran.ToString() + "cp)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

            switch (san)
            {
                case "HASTC":
                    sSan.SelectedIndex = 0;
                    break;
                case "HOSE":
                    sSan.SelectedIndex = 1;
                    avgPriceHeader.Style.Value = "display:none;";
                    avgHeadTD.Style.Value = "display:none;";
                    foreach (RepeaterItem item in rptData.Items)
                    {
                        HtmlTableCell avgPriceItem = item.FindControl("avgPriceItem") as HtmlTableCell;
                        avgPriceItem.Style.Value = "display:none;";
                    }
                    avgFootTD.Style.Value = "display:none;";
                    gtgdSort.Style.Value = "width:122px;";
                    gtgdHead.Style.Value = "width:126px;";
                    klgdThoaThuanHead.Style.Value = "width:111px;";
                    klgdThoaThuanSort.Style.Value = "width:107px;";
                    gtgdThoaThuanHead.Style.Value = "width:117px;";
                    gtgdThoaThuanSort.Style.Value = "width:130px;";
                    break;
                case "UPCOM":
                    sSan.SelectedIndex = 2;
                    break;
                default:
                    sSan.SelectedIndex = 0;
                    break;
            }
        }
        private string specificWhatIconToShow(double value)
        {
            if (value < 0) return "http://cafef3.vcmedia.vn/images/Scontrols/Images/LSG/down_.gif";
            if (value > 0) return "http://cafef3.vcmedia.vn/images/Scontrols/Images/LSG/up_.gif";
            return "http://cafef3.vcmedia.vn/images/Scontrols/Images/LSG/nochange_.jpg";
        }
        private DateTime ConvertDateTime(String date)
        {
            try
            {
                String[] d = date.Split('/');
                return Convert.ToDateTime(d[1] + "/" + d[0] + "/" + d[2]);
            }
            catch
            {
                return DateTime.Today;
            }
        }
        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Literal ltrAveragePrice = e.Item.FindControl("ltrAveragePrice") as Literal;
                Literal ltrChange = e.Item.FindControl("ltrChange") as Literal;
                Literal ltrImage = e.Item.FindControl("ltrImage") as Literal;
                StockHistory __dr = (StockHistory)e.Item.DataItem;
                HtmlTableCell __cell = e.Item.FindControl("avgPriceItem") as HtmlTableCell;
                HtmlTableCell __basicPriceItem = e.Item.FindControl("BasicPriceColItem") as HtmlTableCell;

                string color = string.Empty;

                double basicPrice = ConvertUtility.ToDouble(__dr.BasicPrice);
                double closePrice = ConvertUtility.ToDouble(__dr.ClosePrice);
                double averagePrice = ConvertUtility.ToDouble(__dr.AveragePrice);
                if (closePrice == 0) basicPrice = 0;
                double chgIndex;
                if (san == "HASTC" || san == "UPCOM")
                {
                    chgIndex = averagePrice - basicPrice;
                }
                else
                {
                    chgIndex = closePrice - basicPrice;
                }
                if (chgIndex == 0)
                {
                    totalStockNochange++;
                    color = "#ff9900";
                }
                if (chgIndex > 0)
                {
                    totalStockUp++;
                    color = "green";
                }
                if (chgIndex < 0)
                {
                    totalStockDown++;
                    color = "red";
                }
                if (san == "HASTC" || san == "UPCOM") //san ha noi 
                {
                    try
                    {
                        if (ConvertUtility.ToDouble(__dr.AveragePrice) == ConvertUtility.ToDouble(__dr.Floor))
                        {
                            totalKichsan++;
                            color = "#3987ae";
                        }
                        if (ConvertUtility.ToDouble(__dr.AveragePrice) == ConvertUtility.ToDouble(__dr.Ceiling))
                        {
                            totalKichtran++;
                            color = "#FF00FF";
                        }
                    }
                    catch (Exception)
                    {
                    }

                    ltrAveragePrice.Text = String.Format("{0:#,###.0}", __dr.AveragePrice);
                }
                else//Hose
                {
                    try
                    {

                        if (ConvertUtility.ToDouble(__dr.ClosePrice) == ConvertUtility.ToDouble(__dr.Floor))
                        {
                            totalKichsan++;
                            color = "#3987ae";
                        }
                        if (ConvertUtility.ToDouble(__dr.ClosePrice) == ConvertUtility.ToDouble(__dr.Ceiling))
                        {
                            totalKichtran++;
                            color = "#FF00FF";
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
                double pctIndex = (closePrice + basicPrice) > 0 ? (chgIndex / (basicPrice)) * 100 : 0;
                HtmlTableCell percentTd = e.Item.FindControl("percentTd") as HtmlTableCell;
                percentTd.InnerHtml = String.Format("{0:#,##0.0}", pctIndex);

                string ImgUrl = " <img src='http://cafef3.vcmedia.vn/images/Scontrols/Images/LSG/{0}' style='vertical-align:middle'> &nbsp;&nbsp;";
                ltrImage.Text = Math.Round(pctIndex, 1) == 0 ? String.Format(ImgUrl, "nochange_.jpg") : (Math.Round(pctIndex, 1) > 0) ? String.Format(ImgUrl, "up_.gif") : String.Format(ImgUrl, "down_.gif");
                string strChgIndex = String.Format("{0:#,##0.0}", chgIndex) + " (" + String.Format("{0:#,##0.0}", pctIndex) + " %" + ")";

                ltrChange.Text = "<div style='color:" + color + "'>" + strChgIndex + "</div>";
            }
        }
        private int totalStockUp = 0, totalStockDown = 0, totalStockNochange = 0, totalKichtran = 0, totalKichsan = 0;

    }
}