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
using System.Collections.Generic;

using CafeF.Redis.Entity;
using CafeF.Redis.BL;
using CafeF.Redis.Page;
using CafeF.Redis.Data;

namespace CafeF.Redis.Page.Ajax
{
    public partial class TKDL : System.Web.UI.Page
    {
        public string symbol = "";
        public StockCompactHistory GetHistory
        {
            get
            {
                try
                {
                    return StockBL.getStockBySymbol(Request.QueryString["sym"] != null ? Request.QueryString["sym"].ToString().ToUpper() : "").StockPriceHistory;
                }
                catch
                {
                    return new StockCompactHistory();
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["sym"] != null && Request.QueryString["sym"].ToString() != "")
            {
                symbol = Request.QueryString["sym"].ToString();
            }

            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            //List<OrderCompactHistory> result = GetHistory.Orders;
            //List<OrderHistory> fResult = new List<OrderHistory>();
            //foreach (StockHistory sh in result)
            //{
            //    if ((sh != null) && (sh.Orders != null))
            //        fResult.Add(sh.Orders);
            //}
            var history = GetHistory;
            var order = history.Orders;
            if (order.Count > 0) order = order.GetRange(0, Math.Min(order.Count, 10));
            var price = history.Price;
            for (var i = 0; i < order.Count; i++)
            {
                var o = order[i];
                var da = o.TradeDate.ToString("ddMMyyyy");
                var p = price.Find(s => s.TradeDate.ToString("ddMMyyyy") == da);
                if (p == null) continue;
                o.BidLeft = o.BidVolume - p.Volume;
                o.AskLeft = o.AskVolume - p.Volume;
                order[i] = o;
            }
            rptTKDatlenh.DataSource = order;
            rptTKDatlenh.DataBind();
        }

        //protected string Div(object value1, object value2)
        //{
        //    string strResult = "0";
        //    long lgvalue1 = ConvertUtility.ToLong (value1.ToString());
        //    long lgvalue2 = ConvertUtility.ToLong(value2.ToString());
        //    if (lgvalue2 > 0)
        //        strResult = String.Format("{0:#,##0}", lgvalue1 / lgvalue2);
        //    return strResult;
        //}

        //protected void rptTKDatlenh_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        //    {
        //        Literal ltrChange = e.Item.FindControl("ltrChange") as Literal;
        //        OrderHistory sh = (OrderHistory)e.Item.DataItem;
        //        long BID_VOLUME = ConvertUtility.ToLong(sh.BuyVolume);
        //        long OFFER_VOLUME = ConvertUtility.ToLong(sh.SellVolume);
        //        ltrChange.Text = String.Format("{0:#,##0}", BID_VOLUME - OFFER_VOLUME) + "&nbsp;";
        //    }
        //}
    }
}
