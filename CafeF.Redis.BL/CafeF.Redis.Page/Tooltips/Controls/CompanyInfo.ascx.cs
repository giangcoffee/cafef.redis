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
using CafeF.BO;
using CafeF.Redis.BL;
using CafeF.Redis.BO;
using NewsHelper=CafeF.Redis.BO.NewsHelper;

namespace Portal.ToolTips.Controls
{
    public partial class CompanyInfo : System.Web.UI.UserControl
    {
        public string Folder { get; set; }
        private string __StockSymbol = "";
        private string ImgUrl = " <img src='images/{0}' align='absmiddle'> &nbsp;&nbsp;";
        double chgIndex = 0;
        double pctIndex = 0;
        double closePrice = 0;
        double currentIndex = 0;
        double currentPrice = 0;
        double currentQtty = 0;
        double openPrice = 0;
        double highestPrice = 0;
        double lowestPrice = 0;
        double matchPrice = 0;
        double totalTradingQtty = 0;
        DateTime tradingDate = DateTime.Now.Date;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                __StockSymbol = (Request["symbol"]?? "").Trim().ToUpper();
                if (__StockSymbol == "") return;
                __StockSymbol = Server.UrlDecode(__StockSymbol);
                __StockSymbol = __StockSymbol.Replace(",", "");
                LoadData(__StockSymbol);

                ltrCurentIndex.Text = String.Format("{0:#,##0.0}", currentPrice);
                string ImgUrl = " <img src='http://cafef3.vcmedia.vn/images/{0}' align='absmiddle'> &nbsp;&nbsp;";//btup.gif, btdown.gif, no_change.gif                    
                string strChgIndex = String.Format("{0:#,##0.0}", chgIndex) + " (" + pctIndex.ToString("#,##0.0") + " %" + ")";
                ImgUrl = ImgUrl + strChgIndex;
                strChgIndex = Math.Round(pctIndex, 1) == 0 ? String.Format(ImgUrl, "no_change.jpg") : (Math.Round(pctIndex, 1) > 0) ? String.Format(ImgUrl, "btup.gif") : String.Format(ImgUrl, "btdown.gif");
                lblChange.CssClass = Math.Round(pctIndex, 1) == 0 ? "stc_nochange" : (Math.Round(pctIndex, 1) > 0) ? "stc_asc" : "stc_desc";
                this.lblChange.Text = strChgIndex;
                string imgPath = "http://cafef4.vcmedia.vn/{0}/{1}/newsdetail_{2}.png";
                if (Math.Round(pctIndex, 1) == 0)
                {
                    imgPath = String.Format(imgPath, Folder, __StockSymbol, "nochange");
                }
                if (Math.Round(pctIndex, 1) > 0)
                {
                    imgPath = String.Format(imgPath, Folder, __StockSymbol, "up");
                }
                if (Math.Round(pctIndex, 1) < 0)
                {
                    imgPath = String.Format(imgPath, Folder, __StockSymbol, "down");
                }
                ltrimgChart.Text = "<script>DrawImage('"+imgPath+"');</script>";
            }
        }        
        private void LoadData(string __StockSymbol)
        {

            var price = StockBL.getStockPriceBySymbol(__StockSymbol.ToUpper());
            if (price == null) return;
            chgIndex = Convert.ToDouble(price.Price - price.RefPrice);
            pctIndex = price.RefPrice > 0 ? Math.Round(chgIndex / price.RefPrice * 100, 1) : 0;
            //closePrice = Convert.ToDouble(__RowItem["closePrice"]);
            currentIndex = price.Price;
            currentPrice = price.Price;
            //currentQtty = Convert.ToDouble(__RowItem["currentQtty"]);
            openPrice = price.OpenPrice;//tham chieu 
            highestPrice = price.HighPrice;
            lowestPrice = price.LowPrice;
            //matchPrice = Convert.ToDouble(__RowItem["matchPrice"]);
            totalTradingQtty = price.Volume;
            //tradingDate = Convert.ToDateTime(__RowItem["tradingDate"]);
            //try
            //{
            //__StockSymbol = __StockSymbol.Trim();
            //DataTable __tbl = KenhFHelper.GetCompanyProfile(__StockSymbol);
            //if (__tbl != null)
            //{
            //    if (__tbl.Rows.Count > 0)
            //    {
            //        DataRow __row = __tbl.Rows[0];
            //        string __TradeSymbol = __row["Symbol"].ToString();
            //        if (__TradeSymbol.ToLower() == "hose") __TradeSymbol = "HoSTC"; //dong bo ten

                   

            //        //DataTable __realTimeData = (DataTable)Cache[__TradeSymbol];

            //        //DataTable __realTimeData = MarketHelper.GetSymbolDataByCache(__StockSymbol, __TradeSymbol);

            //        //if (__realTimeData != null)
            //        //{
            //        //    if (__realTimeData.Rows.Count > 0)
            //        //    {

            //        //        DataRow __RowItem = __realTimeData.Rows[0];
            //        //        chgIndex = Convert.ToDouble(__RowItem["chgIndex"]);
            //        //        pctIndex = Math.Round(Convert.ToDouble(__RowItem["pctIndex"]), 1);
            //        //        //closePrice = Convert.ToDouble(__RowItem["closePrice"]);
            //        //        currentIndex = Convert.ToDouble(__RowItem["currentIndex"]);
            //        //        currentPrice = Convert.ToDouble(__RowItem["currentPrice"]);
            //        //        //currentQtty = Convert.ToDouble(__RowItem["currentQtty"]);
            //        //        openPrice = Convert.ToDouble(__RowItem["basicPrice"]);//tham chieu 
            //        //        highestPrice = Convert.ToDouble(__RowItem["highestPrice"]);
            //        //        lowestPrice = Convert.ToDouble(__RowItem["lowestPrice"]);
            //        //        //matchPrice = Convert.ToDouble(__RowItem["matchPrice"]);
            //        //        totalTradingQtty = Convert.ToDouble(__RowItem["totalTradingQtty"]);
            //        //        //tradingDate = Convert.ToDateTime(__RowItem["tradingDate"]);
            //        //    }
            //        //}
            //    }
            //}
            //}
            //catch
            //{
            //}


        }
        public string UpdateTimer()
        {
            string __strReturn = "";
            DateTime openTime = Convert.ToDateTime("08:30:00 AM");
            DateTime closeTime = Convert.ToDateTime("11:00:00 AM");
            DateTime sysTime = DateTime.Now;
            DateTime preTime = new DateTime(sysTime.Year, sysTime.Month, sysTime.Day);
            int currDay = (int)DateTime.Now.Date.DayOfWeek;//ngay hien tai
            if (currDay == 0 || currDay == 6) //ngay nghi
            {
                int __date = currDay == 0 ?  - 2 : - 1;
                __strReturn = " Cập nhật &nbsp;11:00&nbsp; ";
                preTime =sysTime.AddDays(__date) ;//new DateTime(sysTime.Year, sysTime.Month, __date);
                __strReturn += NewsHelper.GetDateVN1(preTime) ;

            }
            else
            {
                if (DateTime.Compare(sysTime, openTime) >= 0 && DateTime.Compare(sysTime, closeTime) <= 0) //trong gio
                {
                    __strReturn = " Cập nhật&nbsp;" + String.Format("{0:HH:mm}", sysTime) + "&nbsp;" + NewsHelper.GetDateVN1(sysTime) +"&nbsp; ";
                }
                else
                {
                    if (DateTime.Compare(sysTime, openTime) < 0) //truoc h mo cua
                    {
                        preTime = sysTime.AddDays(-1);//new DateTime(sysTime.Year, sysTime.Month, sysTime.Day - 1);
                        int preDay = (int)preTime.DayOfWeek;
                        int __ShowDay = 0;
                        __ShowDay = preDay == 0 ? - 2 :  - 1;
                        __strReturn = " Cập nhật&nbsp; 11:00";
                        preTime = sysTime.AddDays(__ShowDay);//new DateTime(sysTime.Year, sysTime.Month, __ShowDay);
                        __strReturn += NewsHelper.GetDateVN1(preTime) + "&nbsp; ";
                    }
                    if (DateTime.Compare(sysTime, closeTime) > 0) //sau h  dong cua
                    {
                        __strReturn = " Cập nhật 11:00&nbsp;";
                        __strReturn += NewsHelper.GetDateVN1(sysTime) + "&nbsp; ";
                    }
                }
            }
            return __strReturn;
        }
    }
}