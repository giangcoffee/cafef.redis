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
namespace CafeF.Redis.Page.UserControl.StockNewsControl
{
    public partial class StockNewsInHomePage : System.Web.UI.UserControl
    {
        private int PageIndex = 1;
        private int PageSize = 6;
        private int TotalCount = 0;
        public int TotalPage
        {
            get
            {
                int totalPage = (TotalCount % PageSize == 0) ? ((int)TotalCount / PageSize) : ((int)TotalCount / PageSize + 1);
                return totalPage;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                getGenPaging(1);
            }
        }

        protected void BindData()
        {
            PageIndex = ConvertUtility.ToInt32(txtIdx.Value);
            List<StockNews> tblResult = StockHistoryBL.get_StockNewsByList(PageIndex, PageSize, out TotalCount);
            if (tblResult != null && tblResult.Count > 0)
            {
                var data = new List<NewsList>();
                var ss = new List<String>();
                foreach (var result in tblResult)
                {
                    var s = result.Symbol;
                    while (s.StartsWith("&")) s = s.Remove(0, 1);
                    if (s.Contains("&")) s = s.Substring(0, s.IndexOf("&"));
                    if (!ss.Contains(s)) ss.Add(s);
                }
                var ps = StockBL.GetStockPriceMultiple(ss);
                foreach (var result in tblResult)
                {
                    var s = result.Symbol;
                    while (s.StartsWith("&")) s = s.Remove(0, 1);
                    if (s.Contains("&")) s = s.Substring(0, s.IndexOf("&"));
                    var p = ps[s] ?? new StockPrice();
                    data.Add(new NewsList(){Id = result.ID, PostTime = result.DateDeploy, Symbol = s, Title = result.Title, Price = p.Price, RefPrice = p.RefPrice});
                }
                rptStockNews.DataSource = data;
                rptStockNews.DataBind();
            }
            else
                divCompany.Visible = false;
        }
        protected void rptStockNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Literal ltrSymbol = e.Item.FindControl("ltrSymbol") as Literal;
                Literal ltrPrice = e.Item.FindControl("ltrPrice") as Literal;
                Literal ltrChange = e.Item.FindControl("ltrChange") as Literal;
                HtmlAnchor idLink = e.Item.FindControl("idLink") as HtmlAnchor;
                StockNews __r = (StockNews)e.Item.DataItem;
                string symbol =  Hepler.getSymbol(ConvertUtility.ToString(__r.Symbol));
                ltrSymbol.Text = symbol ;
                StockPrice sp = StockBL.getStockPriceBySymbol(symbol);
                if (sp != null)
                {
                     ltrPrice.Text = String.Format("{0:#,##0.0}", ConvertUtility.ToDouble(sp.Price));
                     double cg = ConvertUtility.ToDouble(sp.Price) - ConvertUtility.ToDouble(sp.RefPrice);
                     ltrChange.Text = "<div class='" + (cg>0?"up":"down") + "'>" + String.Format("{0:#,##0.0}", cg) + "</div>";
                }
                idLink.HRef = String.Format("/{0}-{1}/{2}.chn",symbol,__r.ID,Hepler.UnicodeToKoDauAndGach(__r.Title));
                idLink.Title = HttpUtility.HtmlEncode(__r.Title).Replace("'", "&#39;");
            }
        }

        private void getGenPaging(int idx)
        {
            int start = 1;
            int end = 1;
            if (idx > 1) start = idx - 1;
            if (idx < TotalCount) end = idx + 1;
            paging.InnerHtml = @"<a style='padding-left=5px' href='javascript:ViewPageStockNews(" + (idx-1).ToString() + ")'>&lt;</a>&nbsp;";
            for (int i = start; i <= end; i++)
            {
                if (i == idx)
                    paging.InnerHtml += "<a id='aStockNews" + i.ToString() + "' href='javascript:ViewPageStockNews(" + i.ToString() + ")' class='current'>" + i.ToString() + "</a>&nbsp;";
                else
                    paging.InnerHtml += "<a id='aStockNews" + i.ToString() + "' href='javascript:ViewPageStockNews(" + i.ToString() + ")'>" + i.ToString() + "</a>&nbsp;";
            }
            paging.InnerHtml += "<a href='javascript:ViewPageStockNews(" + (idx + 1).ToString() + ")'>&gt;</a>&nbsp;";
        }

        protected void btnAjax_Click(object sender, EventArgs e)
        {
            BindData();
            getGenPaging(ConvertUtility.ToInt32(txtIdx.Value));
            panelAjaxStockNews.Update();
        }

        internal class NewsList
        {
            public string Symbol { get; set; }
            public string Title { get; set; }
            public int Id { get; set; }
            public DateTime PostTime { get; set; }
            public double Price { get; set; }
            public double RefPrice { get; set; }
            public string ChangeString {get
            {
                var cg = Price - RefPrice;
                return "<div class='" + (cg > 0 ? "up" : "down") + "'>" + String.Format("{0:#,##0.0}", cg) + "</div>";
            }}
            public string Link {get
            {
                return String.Format("/{0}-{1}/{2}.chn", Symbol, Id, Hepler.UnicodeToKoDauAndGach(Title));
            }}
            public string LinkTitle {get
            {
                return HttpUtility.HtmlEncode(Title).Replace("'", "&#39;");
            }}
        }
    }
}