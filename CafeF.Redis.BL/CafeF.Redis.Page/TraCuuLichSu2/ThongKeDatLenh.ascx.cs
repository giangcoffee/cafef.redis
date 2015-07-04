using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Collections.Generic;
using CafeF.Redis.Entity;
using CafeF.Redis.BL;
using CafeF.Redis.Page;
namespace CafeF.Redis.Page.TraCuuLichSu2
{
    public enum TradeCenter
    {
        HaSTC = 2, HoSE = 1, AllCenter = 8, UpCom = 9
    }
    
    public partial class ThongKeDatLenh : System.Web.UI.UserControl
    {
        public string san = string.Empty;
        public String dateTextBox;
        String date = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            TradeCenter eSan = TradeCenter.AllCenter;
            try
            {
                san = Request.QueryString["san"] == null ? "HOSE" : Request.QueryString["san"];
                date = Request.QueryString["date"] == null ? String.Empty : Request.QueryString["date"];
                DateTime tradeDate;// = ConvertDateTime(date);
                if (!(DateTime.TryParseExact(date, "dd/MM/yyyy/", CultureInfo.InvariantCulture, DateTimeStyles.None, out tradeDate)))
                {
                    var dtHoIndex = TradeCenterBL.getByTradeCenter((int)(san == "HASTC" ? TradeCenter.HaSTC : TradeCenter.HoSE));
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

                
                switch (san)
                {
                    case "HASTC":
                        eSan = TradeCenter.HaSTC;
                        sSan.SelectedIndex = 0;
                        break;
                    case "HOSE":
                        eSan = TradeCenter.HoSE;
                        sSan.SelectedIndex = 1;
                        break;
                    case "UPCOM":
                        eSan = TradeCenter.UpCom;
                        sSan.SelectedIndex = 2;
                        break;
                    default:
                        eSan = TradeCenter.HoSE;
                        sSan.SelectedIndex = 1;
                        break;
                }
                dateTextBox = tradeDate.ToString("dd/MM/yyyy");
                loadData(eSan, tradeDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DateTime ConvertDateTime(String date)
        {
            try
            {
                String[] d = date.Split('/');
                //return Convert.ToDateTime(d[1] + "/" + d[0] + "/" + d[2]);
                return Convert.ToDateTime(d[2] + "-" + d[1] + "-" + d[0]);
            }
            catch
            {
                return DateTime.Today;
            }
        }

        private void loadData(TradeCenter tradeCenter, DateTime tradeDate)
        {
            List<OrderHistory> data = OrderHistoryBL.get_OrderHistoryByCenterAndDate((int)tradeCenter, tradeDate);
            data.Sort("BuyLeft desc");
            rptData.DataSource = data; 
            rptData.DataBind();
            string index =  "VNINDEX";
            if (tradeCenter == TradeCenter.HoSE)
            {
                index = "VNINDEX";
            }
            if (tradeCenter == TradeCenter.HaSTC)
            {
                index = "HNX-INDEX";
            }
            if (tradeCenter == TradeCenter.UpCom)
            {
                index = "UPCOM-INDEX";
            }

            List<OrderHistory> CompareOrder = OrderHistoryBL.get_TwoOrderHistoryBySymbolAndDate(index, tradeDate);
            if (CompareOrder.Count==2)
            {
                string[] comparedResults = compareCurrent_PreviousTrading(CompareOrder[0], CompareOrder[1]);
                div1.InnerHtml = "<span style='font-weight:bold;'>Tổng số lệnh đặt mua:&nbsp;</span>" + String.Format("{0:#,##0}", CompareOrder[0].BuyOrderCount) + "&nbsp;cổ phiếu&nbsp;" + comparedResults[0];
                div2.InnerHtml = "<span style='font-weight:bold;'>Tổng khối lượng đặt mua:&nbsp;</span>" + String.Format("{0:#,##0}", CompareOrder[0].BuyVolume) + "&nbsp;cổ phiếu&nbsp;" + comparedResults[1];
                try
                {
                    div5.InnerHtml = "<span style='font-weight:bold;'>KLTB 1 lệnh mua:&nbsp;</span>" + String.Format("{0:#,##0}", CompareOrder[0].BuyVolume /( CompareOrder[0].BuyOrderCount == 0 ? 1 : CompareOrder[0].BuyOrderCount)) + "&nbsp;cổ phiếu";
                }
                catch { }

                div3.InnerHtml = "<span style='font-weight:bold;'>Tổng số lệnh đặt bán:&nbsp;</span>" + String.Format("{0:#,##0}", CompareOrder[0].SellOrderCount) + "&nbsp;cổ phiếu&nbsp;" + comparedResults[2];
                div4.InnerHtml = "<span style='font-weight:bold;'>Tổng khối lượng đặt bán:&nbsp;</span>" + String.Format("{0:#,##0}", CompareOrder[0].SellVolume) + "&nbsp;cổ phiếu&nbsp;" + comparedResults[3];
                try
                {
                    div6.InnerHtml = "<span style='font-weight:bold;'>KLTB 1 lệnh bán:&nbsp;</span>" + String.Format("{0:#,##0}", CompareOrder[0].SellVolume / (CompareOrder[0].SellOrderCount == 0 ? 1 : CompareOrder[0].SellOrderCount)) + "&nbsp;cổ phiếu";
                }
                catch { }
                divDumua.InnerHtml = "&nbsp;&nbsp;&nbsp;Dư mua:&nbsp;" + String.Format("{0:#,##0}", Math.Abs(CompareOrder[0].BuyVolume - CompareOrder[0].Volume)) + "&nbsp;cổ phiếu";
                divDuban.InnerHtml = "&nbsp;&nbsp;&nbsp;Dư bán:&nbsp;" + String.Format("{0:#,##0}", Math.Abs(CompareOrder[0].SellVolume - CompareOrder[0].Volume)) + "&nbsp;cổ phiếu";
            }
        }

        private string[] compareCurrent_PreviousTrading(OrderHistory currentRow, OrderHistory previousRow)
        {
            string[] comparedResults = new string[4];
            double changePercent;
            // BID_ORDER
            changePercent =(previousRow.BuyOrderCount == 0? 100: 100 * ((currentRow.BuyOrderCount - previousRow.BuyOrderCount) /previousRow.BuyOrderCount));
            changePercent = Math.Round(changePercent, 2);
            if (changePercent > 0) comparedResults[0] = "(<img src='http://cafef3.vcmedia.vn/images/btup.gif'>" + changePercent + "% so với phiên trước)";
            if (changePercent == 0) comparedResults[0] = "(<img src='http://cafef3.vcmedia.vn/images/no_change.jpg'>" + changePercent + "% so với phiên trước)";
            if (changePercent < 0) comparedResults[0] = "(<img src='http://cafef3.vcmedia.vn/images/btdown.gif'>" + changePercent + "% so với phiên trước)";
            // BID_VOLUME
            changePercent = (previousRow.BuyVolume ==0? 100: 100 * ((currentRow.BuyVolume - previousRow.BuyVolume) / previousRow.BuyVolume));
            changePercent = Math.Round(changePercent, 2);
            if (changePercent > 0) comparedResults[1] = "(<img src='http://cafef3.vcmedia.vn/images/btup.gif'>" + changePercent + "% so với phiên trước)";
            if (changePercent == 0) comparedResults[1] = "(<img src='http://cafef3.vcmedia.vn/images/no_change.jpg'>" + changePercent + "% so với phiên trước)";
            if (changePercent < 0) comparedResults[1] = "(<img src='http://cafef3.vcmedia.vn/images/btdown.gif'>" + changePercent + "% so với phiên trước)";
            // OFFER_ORDER
            changePercent = (previousRow.SellOrderCount == 0? 100: 100 * ((currentRow.SellOrderCount - previousRow.SellOrderCount) / previousRow.SellOrderCount));
            changePercent = Math.Round(changePercent, 2);
            if (changePercent > 0) comparedResults[2] = "(<img src='http://cafef3.vcmedia.vn/images/btup.gif'>" + changePercent + "% so với phiên trước)";
            if (changePercent == 0) comparedResults[2] = "(<img src='http://cafef3.vcmedia.vn/images/no_change.jpg'>" + changePercent + "% so với phiên trước)";
            if (changePercent < 0) comparedResults[2] = "(<img src='http://cafef3.vcmedia.vn/images/btdown.gif'>" + changePercent + "% so với phiên trước)";
            // OFFER_VOLUME
            changePercent = (previousRow.SellVolume ==0?100: 100 * ((currentRow.SellVolume - previousRow.SellVolume) / previousRow.SellVolume));
            changePercent = Math.Round(changePercent, 2);
            if (changePercent > 0) comparedResults[3] = "(<img src='http://cafef3.vcmedia.vn/images/btup.gif'>" + changePercent + "% so với phiên trước)";
            if (changePercent == 0) comparedResults[3] = "(<img src='http://cafef3.vcmedia.vn/images/no_change.jpg'>" + changePercent + "% so với phiên trước)";
            if (changePercent < 0) comparedResults[3] = "(<img src='http://cafef3.vcmedia.vn/images/btdown.gif'>" + changePercent + "% so với phiên trước)";
            return comparedResults;
        }

        private string FormatPrice(double basicPrice, double closePrice)
        {
            string strResult = string.Empty;
            double chgIndex = closePrice - basicPrice;
            double pctIndex = (closePrice + basicPrice) > 0 ? (chgIndex / (basicPrice)) * 100 : 0;

            string ImgUrl = " <img src='http://cafef3.vcmedia.vn/images/Scontrols/Images/LSG/{0}' align='absmiddle'> &nbsp;&nbsp;";
            ImgUrl = Math.Round(pctIndex, 1) == 0 ? String.Format(ImgUrl, "nochange_.jpg") : (Math.Round(pctIndex, 1) > 0) ? String.Format(ImgUrl, "up_.gif") : String.Format(ImgUrl, "down_.gif");

            //string strChgIndex = String.Format("{0:#,##0.0}", chgIndex) + " (" + String.Format("{0:#,##0.0}", pctIndex) + " %" + ")";
            string strChgIndex = String.Format("{0:#,##0.0}", chgIndex) + " (" + String.Format("{0:#,##0.0}", pctIndex) + " %" + ")";
            string styleColor = Math.Round(pctIndex, 1) == 0 ? "<span class=Index_NoChange>" : (Math.Round(pctIndex, 1) > 0) ? "<span class=Index_Up>" : "<span class=Index_Down>";
            if (Math.Round(closePrice, 1) == Math.Round(dlCeiling, 1))
                styleColor = "<span class=Index_Ceiling>";
            else
                if (Math.Round(closePrice, 1) == Math.Round(dlFloor, 1))
                    styleColor = "<span class=Index_Floor>";
            strResult = styleColor + strChgIndex + "</span>" + ImgUrl;

            return strResult;
        }
        private double dlCeiling = 0, dlFloor = 0;
        private double basicPrice = 0, closePrice = 0;
        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Literal ltrChange = e.Item.FindControl("ltrChange") as Literal;
                OrderHistory oh = (OrderHistory)e.Item.DataItem;
                long BID_VOLUME = (long)oh.BuyVolume;
                long OFFER_VOLUME = (long)oh.SellVolume;
                ltrChange.Text = String.Format("{0:#,##0}", (BID_VOLUME - OFFER_VOLUME));

                Literal ltrChangePrice = e.Item.FindControl("ltrChangePrice") as Literal;
                closePrice = oh.Price;
                basicPrice = oh.BasicPrice;
                if (closePrice == 0) basicPrice = 0;
                dlCeiling = oh.Ceiling;
                dlFloor = oh.Floor;

                ltrChangePrice.Text = FormatPrice(basicPrice, closePrice);
            }
        }
    }
}