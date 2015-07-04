using System;
using System.Collections;
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
using CafeF.TA;
using CafeF.Redis.Data;
using CafeF.Redis.Entity;
using CafeF.Redis.BL;
namespace CafeF.Redis.Page
{
    public partial class CafeFTA : System.Web.UI.Page
    {
        protected string symbol = string.Empty;
        private int pcount = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            symbol = Request.QueryString["Symbol"] != null ? Request.QueryString["Symbol"] : "";
            symbol = symbol.ToUpper();
            string date = Request.QueryString["date"] != null ? Request.QueryString["date"] : "";
            DateTime dateview = DateTime.Now;
            try
            {
                
                dateview = DateTime.ParseExact(date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch
            {
            }
            int page = 1;
            try
            {               
                page = Convert.ToInt32(Request.QueryString["page"] != null ? Request.QueryString["page"] : "1");
                pcount = Convert.ToInt32(Request.QueryString["pcount"] != null ? Request.QueryString["pcount"] : "10");
            }
            catch
            {
            }
            if (symbol != "")
            {
                getData(symbol, dateview, page);
            }
        }
        private void getData(string symbol, DateTime dateview, int page)
        {
            DataTable dt = IndexBO.IndexCacheSql.TA_GetTopTenSignal(symbol, dateview, page, pcount);
            rptLichSuGD.DataSource = dt;
            rptLichSuGD.DataBind();
            if (dt.Rows.Count == pcount)
                anext.HRef = Request.RawUrl.Substring(0, Request.RawUrl.LastIndexOf(".aspx")) + ".aspx?pcount=" + pcount.ToString()  + "&Symbol=" + symbol + "&date=" + dateview.ToString("yyyyMMdd") + "&page=" + (page + 1).ToString();
            if (page > 1)
                apre.HRef = Request.RawUrl.Substring(0, Request.RawUrl.LastIndexOf(".aspx")) + ".aspx?pcount=" + pcount.ToString() + "&Symbol=" + symbol + "&date=" + dateview.ToString("yyyyMMdd") + "&page=" + (page - 1).ToString();
        }
        protected long ConvertToLong(object Value)
        {
            try
            {
                return Int64.Parse(Value.ToString());
            }
            catch
            {
                return 0;
            }
        }
        private double dlCeiling = 0, dlFloor = 0;
        protected void rptLichSuGD_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;
                Literal ltrChange = e.Item.FindControl("ltrChange") as Literal;
                Literal ltrTotal = e.Item.FindControl("ltrTotal") as Literal;
                Literal ltrPrice = e.Item.FindControl("ltrPrice") as Literal;
                Literal ltrVolume = e.Item.FindControl("ltrVolume") as Literal;
                StockHistory sh = StockHistoryDAO.get_StockHistoryByKey(symbol,(DateTime) dr["TransDate"]);
                dlCeiling = ConvertUtility.ToDouble(sh.Ceiling);
                dlFloor = ConvertUtility.ToDouble(sh.Floor);
                double basicPrice = ConvertUtility.ToDouble(sh.BasicPrice);
                double closePrice = ConvertUtility.ToDouble(sh.ClosePrice);
                ltrVolume.Text = String.Format("{0:#,##0}", ConvertUtility.ToDouble(sh.Volume));
                if (closePrice == 0) basicPrice = 0;

                ltrPrice.Text = String.Format("{0:#,##0.0}", closePrice);

                double chgIndex = closePrice - basicPrice;
                double pctIndex = 0;
                if (basicPrice > 0)
                    pctIndex = (closePrice + basicPrice) > 0 ? (chgIndex / (basicPrice)) * 100 : 0;

                string strChgIndex = String.Format("{0:#,##0.0}", chgIndex) + " (" + String.Format("{0:#,##0.0}", Math.Round(pctIndex, 1)) + "%" + ")";


                string icon = "nochange";
                string color = "orange";
                if (pctIndex < 0)
                {
                    icon = "down";
                    color = "red";
                }
                if (pctIndex > 0)
                {
                    color = "green";
                    icon = "up";
                }

                if (dlCeiling > 0 && Math.Round(closePrice, 1) == Math.Round(dlCeiling, 1))
                {
                    color = "pink";
                }
                else
                    if (dlFloor > 0 && Math.Round(closePrice, 1) == Math.Round(dlFloor, 1))
                    {
                        color = "blue";
                    }

                ltrChange.Text = "<div class='r " + icon + " " + color + "'>" + strChgIndex + "</div>";
                //AgreementHistory ah = StockBL.getAgreementHistoryBySymbolAndDate(symbol, sh.TradeDate.ToString("yyyyMMdd"));
                //if (ah != null)
                //    ltrTotal.Text = String.Format("{0:#,##0}", ConvertUtility.ToDouble(ah.Trans_Value) + ConvertUtility.ToDouble(sh.TotalValue));
                //else
                    ltrTotal.Text = String.Format("{0:#,##0}", ConvertUtility.ToDouble(sh.TotalValue + sh.AgreedValue));

            }
        }

    }
}
