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
    public partial class InSameCategory : System.Web.UI.UserControl
    {
        public List<StockShortInfo> GetSameCategory
        {
            get
            {
                try
                {
                    return ((Stock)((CafeF.Redis.Page.MasterPage.StockMain)(((ContentPlaceHolder)this.Parent).Page).Master).GetStock).SameCategory;
                }
                catch
                {
                    return new List<StockShortInfo>();
                }
            }
        }
        public Stock GetStock
        {
            get
            {
                try
                {
                    return ((Stock)((CafeF.Redis.Page.MasterPage.StockMain)(((ContentPlaceHolder)this.Parent).Page).Master).GetStock);
                }
                catch
                {
                    return null;
                }
            }
        }
        public string StockSymbol
        {
            get
            {
                if (Request.QueryString["Symbol"] != null)
                {
                    return Request.QueryString["Symbol"];
                }
                else
                {
                    return "";
                }
            }
        }

        public int TotalPage
        {
            get
            {
                var ls = GetSameCategory;
                int totalPage = (ls.Count % pageCount == 0) ? ((int)ls.Count / pageCount) : ((int)ls.Count / pageCount + 1);
                return totalPage;
            }
        }
        private int pageCount = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getData(1);
                getGenPaging(1);
            }
        }

        private void getData(int indx)
        {
            var stock = GetStock;
            if (stock == null) return;
            try
            {
                ltrNganh.Text = stock.CompanyProfile.basicInfos.category.Name;
            }
            catch (Exception) { }
            var dt = GetSameCategory.GetPaging(indx, pageCount);
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
                ret.Add(new MyStock() { Name = data.Name, Symbol = data.Symbol, TradeCenterId = data.TradeCenterId, EPS = data.EPS, Price = p.Price, CeilingPrice = p.CeilingPrice, FloorPrice = p.FloorPrice, RefPrice = p.RefPrice });
            }
            rptSameCategory.DataSource = ret;
            rptSameCategory.DataBind();
        }

        private void getGenPaging(int idx)
        {
            paging.InnerHtml = @"<a style='padding-left=5px' href='javascript:void(0);' rel='prev'>&lt;</a>&nbsp;";
            for (int i = 1; i <= TotalPage; i++)
            {
                //if (i % 11 == 0)
                //    paging.InnerHtml += "<br />";
                if (i == idx)
                    paging.InnerHtml += "<a href='javascript:void(0);' rel='"+i+"' class='current'>" + i.ToString() + "</a>&nbsp;";
                else
                    paging.InnerHtml += "<a href='javascript:void(0);' rel='" + i + "'>" + i.ToString() + "</a>&nbsp;";
            }
            paging.InnerHtml += "<a href='javascript:void(0);' rel='next'>&gt;</a>&nbsp;";
            if (TotalPage > 0)
                ltrGhiChu.Text = "Trang " + idx.ToString() + "/" + TotalPage.ToString();
            else
            {
                this.Visible = false;
            }
        }

        protected void rptSameCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Literal ltrPrice = (Literal)e.Item.FindControl("ltrPrice");
                Literal ltrPercent = (Literal)e.Item.FindControl("ltrPercent");
                Literal ltrEPS = (Literal)e.Item.FindControl("ltrEPS");
                Literal ltrPE = (Literal)e.Item.FindControl("ltrPE");
                Literal ltrSan = (Literal)e.Item.FindControl("ltrSan");
                MyStock si = (MyStock)e.Item.DataItem;
                ltrSan.Text = Utils.GetCenterName(si.TradeCenterId.ToString());
                ltrEPS.Text = String.Format("{0:#,##0.0}", si.EPS);
                string color = "orange";
                HtmlGenericControl divColor = (HtmlGenericControl)e.Item.FindControl("divColor");

                //StockPrice sp = StockBL.getStockPriceBySymbol(si.Symbol);
                ltrPrice.Text = "-";
                ltrPercent.Text = "-";
                ltrPE.Text = "-";
                if (si.Price > 0)
                {
                    ltrPrice.Text = String.Format("{0:#,##0.0}", ConvertUtility.ToDouble(si.Price));
                    ltrPE.Text = ConvertUtility.ToDouble(si.EPS) != 0 ? String.Format("{0:#,##0.0}", Math.Round(ConvertUtility.ToDouble(si.Price) / ConvertUtility.ToDouble(si.EPS), 2)) : "-";
                    double chgIndex = si.Price - si.RefPrice;
                    double pctIndex = 0;
                    if (si.RefPrice > 0) pctIndex = (chgIndex / si.RefPrice) * 100;

                    ltrPercent.Text = string.Format("({0}{1}{2})", pctIndex >= 0 ? "+" : "", String.Format("{0:#,##0.0}", pctIndex), "%");

                    if (pctIndex < 0)
                    {
                        color = "red";
                    }
                    if (pctIndex > 0)
                    {
                        color = "green";
                    }

                    if (si.CeilingPrice > 0 && Math.Round(si.Price, 1) == Math.Round(si.CeilingPrice, 1))
                    {
                        color = "pink";
                    }
                    else
                        if (si.FloorPrice > 0 && Math.Round(si.Price, 1) == Math.Round(si.FloorPrice, 1))
                        {
                            color = "blue";
                        }

                    divColor.Attributes.Add("class", color);
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
        }
    }
}