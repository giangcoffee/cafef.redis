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
using CafeF.Redis.BL;

namespace CafeF.Redis.Page.TraCuuLichSu2
{
    public partial class TraCuu : System.Web.UI.Page
    {

        public String date = String.Empty;
        public String tab = String.Empty;
        public String san = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            san = String.IsNullOrEmpty(Request.QueryString["san"]) ? String.Empty : Request.QueryString["san"];
            date = String.IsNullOrEmpty(Request.QueryString["date"]) ? String.Empty : Request.QueryString["date"];
            tab = String.IsNullOrEmpty(Request.QueryString["tab"]) ? String.Empty : Request.QueryString["tab"];

            san = String.IsNullOrEmpty(san) ? "HOSE" : san.ToUpper();
            var tradeDate = DateTime.Now;// = ConvertDateTime(date);
            if (!(DateTime.TryParseExact(date, "dd/MM/yyyy/", CultureInfo.InvariantCulture, DateTimeStyles.None, out tradeDate)))
            {
                //var dtHoIndex = TradeCenterBL.getByTradeCenter((int)(san == "HASTC" ? TradeCenter.HaSTC : TradeCenter.HoSE));
                //tradeDate = dtHoIndex.CurrentDate;
                int total;
                var dtHoIndex = StockHistoryBL.get_StockHistoryBySymbolAndDate(san == "HASTC" ? "HNX-INDEX" : "VNINDEX", DateTime.MinValue, DateTime.MaxValue, 1, 1, out total);
                if(dtHoIndex.Count>0)
                {
                    tradeDate = dtHoIndex[0].TradeDate;
                }
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

            tab = String.IsNullOrEmpty(tab) ? "1" : tab;

            String path = String.Empty;

            switch (tab)
            { 
                case "1":
                    this.Title = "Thống kê lịch sử giao dịch khớp lệnh trên sàn " + (san == "HASTC" ? "HNX" : "HSX") + " ngày " + tradeDate.ToString("dd/MM/yyyy");
                    ltrTitle.Text = "Lịch sử giá";
                    path = "~/TraCuuLichSu2/LichSuGia.ascx";
                    break;
                case "2":
                    this.Title = "Thống kê lịch sử đặt lệnh trên sàn " + (san == "HASTC" ? "HNX" : "HSX") + " ngày " + tradeDate.ToString("dd/MM/yyyy");
                    ltrTitle.Text = "Thống kê đặt lệnh";
                    path = "~/TraCuuLichSu2/ThongKeDatLenh.ascx";
                    break;
                case "3":
                    this.Title = "Thống kê lịch sử giao dịch nhà đầu tư nước ngoài trên sàn " + (san == "HASTC" ? "HNX" : "HSX") + " ngày " + tradeDate.ToString("dd/MM/yyyy");
                    path = "~/TraCuuLichSu2/GiaoDichNuocNgoai.ascx";
                    ltrTitle.Text = "Giao dịch NĐT Nước ngoài";
                    break;
                case "4":
                    this.Title = "Thống kê lịch sử giao dịch cổ đông lớn - cổ đông nội bộ trên sàn " + (san == "HASTC" ? "HNX" : "HSX") + " ngày " + tradeDate.ToString("dd/MM/yyyy");
                    path = "~/TraCuuLichSu2/GiaoDichNoiBo.ascx";
                    ltrTitle.Text = "Giao dịch cổ đông lớn & cổ đông nội bộ";
                    break;
            }
            ltrTitle.Text += " / ";
            ltrText.Text = "Toàn bộ cổ phiếu GD tại " + san + " - ngày " + date;

            //try
            //{
                Control control = LoadControl(path);
                pldContent.Controls.Add(control);
            //}
            //catch (Exception ex)
            //{
            //    Response.Clear();
            //    Response.End();
            //    Response.Flush();
            //}
            
        }

        public string CurentDomain
        {
            get { return Request.Url.Authority; }
        }

        private bool IsValidDate(String date)
        {
            return true;
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
    }
}
