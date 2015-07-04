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

using System.Collections.Generic;
using CafeF.Redis.Entity;
using CafeF.Redis.BL;
using CafeF.Redis.Page;
namespace CafeF.Redis.Page.TraCuuLichSu2
{
    public partial class GiaoDichNuocNgoai : System.Web.UI.UserControl
    {
        public String dateTextBox;
        String date = string.Empty;
        public string san = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            TradeCenter eSan = TradeCenter.AllCenter;
            try
            {
                san = string.Empty;
                san = Request.QueryString["san"] == null ? "ALL" : Request.QueryString["san"];
                date = Request.QueryString["date"] == null ? String.Empty : Request.QueryString["date"];
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
                dateTextBox = tradeDate.ToString("dd/MM/yyyy");
                loadData(eSan, tradeDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void loadData(TradeCenter tradeCenter, DateTime tradeDate)
        {
            List<ForeignHistory> data = ForeignHistoryBL.get_ForeignHistoryByCenterAndDate((int)tradeCenter, tradeDate);
            data.Sort("NetVolume desc");
            rptData.DataSource = data;
            rptData.DataBind();
            string index = "VNINDEX";
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
            var foreign = ForeignHistoryBL.get_OneForeignHistoryBySymbolAndDate(index, tradeDate);
            int tmp;
            var trade = StockHistoryBL.get_StockHistoryBySymbolAndDate(index, tradeDate, tradeDate, 1, 1, out tmp);
            if (foreign.Count == 0 || trade.Count == 0) return;
            var center = new List<CenterStats> {new CenterStats() {Symbol = index, BuyVolume = foreign[0].BuyVolume, BuyValue = foreign[0].BuyValue, SellValue = foreign[0].SellValue, SellVolume = foreign[0].SellVolume, TradeValue = trade[0].TotalValue + trade[0].AgreedValue, TradeVolume = trade[0].Volume + trade[0].AgreedVolume}};
            rptHead.DataSource = center;
            rptHead.DataBind();
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
                ForeignHistory fh = (ForeignHistory)e.Item.DataItem;
                Literal ltrROOM_CONLAI = e.Item.FindControl("ltrROOM_CONLAI") as Literal;
                Literal ltrSOHUU = e.Item.FindControl("ltrSOHUU") as Literal;

                ltrROOM_CONLAI.Text = String.Format("{0:#,###}", fh.Room);
                if (fh.Percent > 0)
                    ltrSOHUU.Text = String.Format("{0:#,##0.00}", fh.Percent) + "%";
            }
        }
        protected void rptHead_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                {
                }
            }
            catch (Exception ex)
            {

            }
        }

    }

    class CenterStats
    {
        public string Symbol { get; set; }
        public double BuyVolume { get; set; }
        public double SellVolume { get; set; }
        public double TradeVolume { get; set; }
        public double BuyValue { get; set; }
        public double SellValue { get; set; }
        public double TradeValue { get; set; }
        public double DiffVolume { get { return BuyVolume - SellVolume; } }
        public double DiffValue { get { return BuyValue - SellValue; } }
        public double BuyVolumePercent { get { return BuyVolume / TradeVolume * 100; } }
        public double SellVolumePercent { get { return SellVolume / TradeVolume * 100; } }
        public double BuyValuePercent { get { return BuyValue / TradeValue * 100; } }
        public double SellValuePercent { get { return SellValue / TradeValue * 100; } }
    }
}
