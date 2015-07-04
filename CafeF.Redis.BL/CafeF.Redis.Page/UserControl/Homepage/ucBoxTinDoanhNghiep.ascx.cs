using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CafeF.Redis.BL;
using CafeF.Redis.Entity;

namespace CafeF.Redis.Page.UserControl.Homepage
{
    public partial class ucBoxTinDoanhNghiep : System.Web.UI.UserControl
    {
        #region Properties

        private int pageSize = 18;
        private int pageIndex = 1;
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            GetParams();
            if (IsPostBack) return;
            LoadNews();
        }
        #endregion

        #region Business
        private void GetParams()
        {
            if (!int.TryParse(Request["page"] ?? "1", out pageIndex)) pageIndex = 1;
        }
        private void LoadNews()
        {

            int itemcount;
            List<StockNews> newsdata;
            newsdata = StockHistoryBL.get_StockNewsByList(pageIndex, pageSize, out itemcount);
            var news = new List<News>();
            int i = 0;
            var ss = new List<string>();
            foreach (var data in newsdata)
            {
                var symbol = data.Symbol;
                while (symbol.StartsWith("&")) symbol = symbol.Remove(0, 1);
                if (symbol.Contains("&")) symbol = symbol.Substring(0, symbol.IndexOf("&"));
                if (!ss.Contains(symbol)) ss.Add(symbol);
            }
            var ps = StockBL.GetStockPriceMultiple(ss);
            foreach (var data in newsdata)
            {
                var tmp = new News() { NewsId = data.ID, NewsDate = data.DateDeploy, NewsTitle = data.Title, RowIndex = i };
                var symbol = data.Symbol;
                while (symbol.StartsWith("&")) symbol = symbol.Remove(0, 1);
                if (symbol.Contains("&")) symbol = symbol.Substring(0, symbol.IndexOf("&"));
                tmp.Symbol = symbol;
                //var pr = StockBL.getStockPriceBySymbol(symbol);
                var pr = ps[symbol];
                if (pr != null)
                {
                    tmp.Price = pr.Price;
                    tmp.Change = pr.Price - pr.RefPrice;
                }
                news.Add(tmp);
                i++;
            }
            repNews.DataSource = news;
            repNews.DataBind();

            //var pageCount = 10000; // (int)Math.Round((double)itemcount / pageSize);
            //var pages = new List<NewsPage>();
            //if (pageIndex > 1)
            //{
            //    pages.Add(new NewsPage() { PageIndex = pageIndex - 1, Current = 0, PageTitle = "&lt;" });
            //    pages.Add(new NewsPage() { PageIndex = pageIndex - 1, Current = 0, PageTitle = (pageIndex - 1).ToString() });
            //}
            //pages.Add(new NewsPage() { PageIndex = pageIndex, Current = 1, PageTitle = pageIndex.ToString() });
            //if (pageIndex < pageCount)
            //{
            //    var add = 1;
            //    if (pages.Count == 1)
            //    {
            //        pages.Add(new NewsPage() { PageIndex = pageIndex + 1, Current = 0, PageTitle = (pageIndex + 1).ToString() });
            //        add++;
            //    }
            //    pages.Add(new NewsPage() { PageIndex = pageIndex + add, Current = 0, PageTitle = (pageIndex + add).ToString() });
            //    pages.Add(new NewsPage() { PageIndex = pageIndex + 1, Current = 0, PageTitle = "&gt;" });
            //}
            //repPage.DataSource = pages;
            //repPage.DataBind();
        }
        protected string DisplayDate(object date)
        {
            DateTime d;
            if (!(DateTime.TryParse(date.ToString(), out d))) return "";
            if (d.ToString("ddMMyyyy") == DateTime.Now.ToString("ddMMyyyy")) return d.ToString("HH:mm");
            return d.ToString("dd/MM");
        }
        protected string DisplayLink(string symbol, string id, string title)
        {
            return string.Format("/{0}-{1}/{2}.chn", symbol, id, Hepler.UnicodeToKoDauAndGach(title));
        }
        #endregion
    }
    class News
    {
        public string Symbol { get; set; }
        public int NewsId { get; set; }
        public string NewsTitle { get; set; }
        public DateTime NewsDate { get; set; }
        public double Price { get; set; }
        public double Change { get; set; }
        public int RowIndex { get; set; }
    }
    class NewsPage
    {
        public int PageIndex { get; set; }
        public string PageTitle { get; set; }
        public int Current { get; set; } //is current --> = 1
    }
}