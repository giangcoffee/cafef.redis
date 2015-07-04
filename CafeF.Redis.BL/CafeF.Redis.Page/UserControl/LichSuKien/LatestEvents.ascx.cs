using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CafeF.Redis.Entity;
using CafeF.Redis.BL;
using CafeF.Redis.Page;

namespace CafeF.Redis.Page.UserControl.LichSuKien
{
    public partial class LatestEvents : System.Web.UI.UserControl
    {
        protected string __symbol = string.Empty;
        int NumNews = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            __symbol = Request.QueryString["symbol"] != null ? Request.QueryString["symbol"].ToString().ToLower() : "";
            List<StockNews> tbNews = new List<StockNews>();
            tbNews = StockHistoryBL.get_TopLatestNews(NumNews);
            rptTopEvents.DataSource = tbNews;
            rptTopEvents.DataBind();
        }

        protected void rptTopEvents_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            StockNews __r = (StockNews)e.Item.DataItem;
            if (__r == null) return;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal _ltr = e.Item.FindControl("ltrContent1") as Literal;
                if (_ltr == null) return;
                string NewsId = __r.ID.ToString();
                string symbol = ConvertUtility.ToString(__r.Symbol);
                //while (symbol.StartsWith("&")) symbol = symbol.Remove(0, 1);
                //if (symbol.Contains("&")) symbol = symbol.Substring(0, symbol.IndexOf("&")+1);

                symbol = Hepler.getSymbol(symbol);

                string title = __r.Title.ToString();
                string __d = String.Format("{0:dd/MM}", __r.DateDeploy);
                _ltr.Text = Hepler.Event_BuildLink(NewsId, symbol.ToLower(), title, __d);
            }
        }
    }
}