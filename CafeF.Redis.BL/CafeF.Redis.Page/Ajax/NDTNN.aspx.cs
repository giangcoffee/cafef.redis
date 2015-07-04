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
    public partial class NDTNN : System.Web.UI.Page
    {
        public string symbol = "";

        public StockCompactHistory GetHistory
        {
            get
            {
                try
                {
                    return StockBL.getStockBySymbol( Request.QueryString["sym"] != null ? Request.QueryString["sym"].ToString().ToUpper() : "").StockPriceHistory;
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
            //List<ForeignCompactHistory> result = GetHistory.Foreign;
            //List<ForeignHistory> fResult = new List<ForeignHistory>();
            //foreach (StockHistory sh in result)
            //{
            //    if ((sh != null) && (sh.ForeignOrders != null))
            //        fResult.Add(sh.ForeignOrders);
            //}
            var history = GetHistory;
            var foreign = history.Foreign;
            if (foreign.Count > 0) foreign = foreign.GetRange(0, Math.Min(foreign.Count, 10));
            var price = history.Price;
            for(var i = 0; i < foreign.Count; i++)
            {
                var f = foreign[i];
                var da = f.TradeDate.ToString("ddMMyyyy");
                var p = price.Find(s => s.TradeDate.ToString("ddMMyyyy")==da);
                if (p == null) continue;
                var value = p.TotalValue + p.AgreedValue;
                f.BuyPercent = value > 0 ? ((double) f.BuyValue/value*100) : 0;
                f.SellPercent = value > 0 ? ((double) f.SellValue/value*100) : 0;
                foreign[i] = f;
            }
            rptTDTNN.DataSource = foreign;
            rptTDTNN.DataBind();
        }

        //protected void rptTDTNN_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        //    {
        //        Literal ltrGTGDRong = e.Item.FindControl("ltrGTGDRong") as Literal;
        //        ForeignHistory fh = (ForeignHistory)e.Item.DataItem;
        //        double BUYING_VALUE = ConvertUtility.ToDouble(fh.BuyValue);
        //        double SELLING_VALUE = ConvertUtility.ToDouble(fh.SellValue);
        //        double BUYING_VOL = ConvertUtility.ToDouble(fh.BuyVolume);
        //        double SELLING_VOL = ConvertUtility.ToDouble(fh.SellVolume);
        //        ltrGTGDRong.Text = String.Format("{0:#,###}", (BUYING_VALUE - SELLING_VALUE));


        //    }
        //}
    }

}
