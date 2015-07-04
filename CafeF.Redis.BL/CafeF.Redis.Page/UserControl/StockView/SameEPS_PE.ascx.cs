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
namespace CafeF.Redis.Page.UserControl.StockView
{
    public partial class SameEPS_PE : System.Web.UI.UserControl
    {
        public List<StockShortInfo> GetSameEPS
        {
            get
            {
                try
                {
                    return ((Stock)((CafeF.Redis.Page.MasterPage.StockMain)(((ContentPlaceHolder)this.Parent).Page).Master).GetStock).SameEPS;
                }
                catch
                {
                    return new List<StockShortInfo>();
                }
            }
        }
        public int TotalPage
        {
            get
            {
                var ls = GetSameEPS;
                int totalPage = (ls.Count % pageCount == 0) ? ((int)ls.Count / pageCount) : ((int)ls.Count / pageCount + 1);
                return totalPage;
            }
        }

        public int TotalItem
        {
            get
            {
                return GetSameEPS.Count;
            }
        }

        private int pageCount = 10;
        public String Symbol = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Symbol = (Request["symbol"] ?? "").ToUpper();
            if (Symbol != "")
            {
                getData(1, 10);
                getGenPaging(1);
            }
        }
        private void getData(int indx, int size)
        {
            var dt = GetSameEPS.GetPaging(indx, size);
            var ret = new List<MyStock>();
            var ss = new List<string>();
            foreach (var data in dt)
            {
                var symbol = data.Symbol;
                while (symbol.StartsWith("&")) symbol = symbol.Remove(0, 1);
                if (symbol.Contains("&")) symbol = symbol.Substring(0, symbol.IndexOf("&"));
                if (!ss.Contains(symbol)) ss.Add(symbol);
            }
            var ps = StockBL.GetStockPriceMultiple(ss);
            foreach (var data in dt)
            {
                var symbol = data.Symbol;
                while (symbol.StartsWith("&")) symbol = symbol.Remove(0, 1);
                if (symbol.Contains("&")) symbol = symbol.Substring(0, symbol.IndexOf("&"));
                var p = ps[symbol] ?? new StockPrice();
                ret.Add(new MyStock() { Name = data.Name, Symbol = data.Symbol, TradeCenterId = data.TradeCenterId, EPS = data.EPS, Price = p.Price, CeilingPrice = p.CeilingPrice, FloorPrice = p.FloorPrice, RefPrice = p.RefPrice, MarketValue = data.MarketValue });
            }
            rptEPS.DataSource = ret;
            rptEPS.DataBind();
        }

        protected void rptEPS_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Literal ltrEPS = (Literal)e.Item.FindControl("ltrEPS");
                Literal ltrSan = (Literal)e.Item.FindControl("ltrSan");
                Literal ltrPrice = (Literal)e.Item.FindControl("ltrPrice");
                Literal ltrPE = (Literal)e.Item.FindControl("ltrPE");
                Literal ltrVonHoa = (Literal)e.Item.FindControl("ltrVonHoa");
                MyStock si = (MyStock)e.Item.DataItem;
                ltrSan.Text = Utils.GetCenterName(si.TradeCenterId.ToString());
                ltrEPS.Text = String.Format("{0:#,##0.0}", si.EPS);
                ltrVonHoa.Text = String.Format("{0:#,##0.0}", si.MarketValue);
                ltrPrice.Text = "-";
                ltrPE.Text = "-";
                //StockPrice sp = StockBL.getStockPriceBySymbol(si.Symbol);
                if (si.Price > 0)
                {
                    ltrPrice.Text = String.Format("{0:#,##0.0}", ConvertUtility.ToDouble(si.Price));
                    ltrPE.Text = ConvertUtility.ToDouble(si.EPS) != 0 ? String.Format("{0:#,##0.0}", Math.Round(ConvertUtility.ToDouble(si.Price) / ConvertUtility.ToDouble(si.EPS), 2)) : "";
                }
            }
        }

        internal class MyStock
        {
            public string Symbol { get; set; }
            public string Name { get; set; }
            public int TradeCenterId { get; set; }
            public double EPS { get; set; }
            public double Price { get; set; }
            public double RefPrice { get; set; }
            public double FloorPrice { get; set; }
            public double CeilingPrice { get; set; }
            public double MarketValue { get; set; }
        }

        private void getGenPaging(int idx)
        {
            paging.InnerHtml = @"<a style='padding-left=5px' href='javascript:ViewPageNextPreviousEPS(-1)'>&lt;</a>&nbsp;";
            for (int i = 1; i <= TotalPage; i++)
            {
                //if (i % 11 == 0)
                //    paging.InnerHtml += "<br />";

                if (i == idx)
                    paging.InnerHtml += "<a id='aSameEPS" + i.ToString() + "' href='javascript:ViewPageSameEPSByIndex(" + i.ToString() + ");' class='current'>" + i.ToString() + "</a>&nbsp;";
                else
                    paging.InnerHtml += "<a id='aSameEPS" + i.ToString() + "' href='javascript:ViewPageSameEPSByIndex(" + i.ToString() + ");'>" + i.ToString() + "</a>&nbsp;";
                if (i == 9) paging.InnerHtml += "<br />";
            }
            paging.InnerHtml += "<a href='javascript:ViewPageNextPreviousEPS(1)'>&gt;</a>&nbsp;";
            if (TotalPage > 0)
                ltrGhiChu.Text = "Trang " + idx.ToString() + "/" + TotalPage.ToString() + string.Format(" {0} {1} {2}", "(Tổng số ", GetSameEPS.Count.ToString(), "công ty)");
        }

    }
}