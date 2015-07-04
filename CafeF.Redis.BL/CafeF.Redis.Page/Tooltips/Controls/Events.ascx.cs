using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CafeF.BO;
using CafeF.BO.Utilitis;
using CafeF.Redis.BL;
using CafeF.Redis.Entity;
using CafeF.Redis.Page;

namespace Portal.ToolTips.Controls
{
    [PartialCaching(60, "symbol",null,null)]
    public partial class Events : System.Web.UI.UserControl
    {
        string __symbol = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            __symbol = (Request["symbol"] ?? "").ToUpper();
            if (!IsPostBack)
            {
                var stock = StockBL.getStockBySymbol(__symbol);
                if (stock == null || stock.StockNews.Count==0) return;
                var numNews = Int32.Parse(ConfigurationManager.AppSettings["SoLuongTinDoanhNghiep"].ToString());
                rptTopEvents.DataSource = stock.StockNews.Take(numNews).ToList();
                rptTopEvents.DataBind();
            }
        }

        protected void rptTopEvents_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                StockNews __r = (StockNews)e.Item.DataItem;
                if (__r == null) return;
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Literal _ltr = e.Item.FindControl("ltrContent") as Literal;
                    if (_ltr == null) return;
                    string NewsId = __r.ID.ToString();
                    string title = __r.Title.ToString();
                    string __d = String.Format("{0:dd/MM/yyyy HH:mm}", __r.DateDeploy);
                    _ltr.Text = string.Format("<a href=\"/{0}-{2}/{1}.chn\" target=\"_blank\" title=\"{3}\">{5}</a> ({6})", __symbol, Hepler.UnicodeToKoDauAndGach(title), NewsId, HttpUtility.HtmlEncode(title).Replace("'", "&#39;"), "", title, __d);
                }
            }
            catch
            { }
        }

    }
}