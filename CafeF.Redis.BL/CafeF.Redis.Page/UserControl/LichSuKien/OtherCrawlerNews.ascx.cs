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

using CafeF.Redis.Entity;
using CafeF.Redis.BL;
using CafeF.Redis.Page;
namespace CafeF.Redis.Page.UserControl.LichSuKien
{
    public partial class OtherCrawlerNews : System.Web.UI.UserControl
    {
        private string __StockSymbol;
        public string StockSymbol
        {
            get
            {
                return string.IsNullOrEmpty(this.__StockSymbol) ? "" : this.__StockSymbol;
            }
            set
            {
                this.__StockSymbol = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                __StockSymbol = Request.QueryString["Symbol"] != null ? Request.QueryString["Symbol"].ToString() : "";
                __StockSymbol = __StockSymbol.Replace(",", "");
                int __NewsId = Request.QueryString["NewsID"] != null ? ConvertUtility.ToInt32(Request.QueryString["NewsID"].ToString()) : 0;
                if (__NewsId == 0) return;
                DataList1.DataSource = StockHistoryBL.get_TopOtherStockNewsRelateStock(__NewsId, __StockSymbol, 10);
                DataList1.DataBind();
                if (StockSymbol == "")
                {
                    hplMore.NavigateUrl = "/Tin-Doanh-Nghiep.chn";
                }
                else
                {
                    hplMore.NavigateUrl = "/Tin-Doanh-Nghiep/"+StockSymbol+"/Event.chn";
                }
            }
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            StockNews __r = (StockNews)e.Item.DataItem;
            if (__r == null) return;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal _ltr = e.Item.FindControl("ltrContent1") as Literal;
                if (_ltr == null) return;
                string NewsId = __r.ID.ToString();
                string symbol = __StockSymbol != "" ? __StockSymbol : ConvertUtility.ToString(__r.Symbol);
                string title = __r.Title.ToString();
                string __d = String.Format("{0:dd/MM}", __r.DateDeploy);
                _ltr.Text = Hepler.Event_BuildLink(NewsId, symbol, title, __d);
            }
        }
    }
}