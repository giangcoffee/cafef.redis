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
    public partial class TradeHistory : System.Web.UI.UserControl
    {
        #region Public variables
        public String san;
        public String Symbol = "";
        public int Top = 10;
        #endregion

        public int TradeCenter
        {
            get 
            {  
                try
                {
                    return ConvertUtility.ToInt32(((Stock)((CafeF.Redis.Page.MasterPage.StockMain)(((ContentPlaceHolder)this.Parent).Page).Master).GetStock).CompanyProfile.basicInfos.TradeCenter); 
                }
                catch
                {
                    return 1;
                }
            }
        }

        public StockCompactHistory GetHistory
        {
            get
            {
                try
                {
                    return ((Stock)((CafeF.Redis.Page.MasterPage.StockMain)(((ContentPlaceHolder)this.Parent).Page).Master).GetStock).StockPriceHistory;
                }
                catch
                {
                    return new StockCompactHistory();
                }
               
            }
        }

        #region Event handlers
        protected void Page_Load(object sender, EventArgs e)
        {
            san = String.IsNullOrEmpty(Request.QueryString["san"]) ? String.Empty : Request.QueryString["san"];
            string symbol = Request.QueryString["Symbol"] ?? "";
            symbol = symbol.ToUpper();
            if (symbol != "")
            {
                this.Symbol = symbol;
                getData(symbol);
            }
        }
        private double dlCeiling = 0, dlFloor = 0;
        protected void rptLichSuGD_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {

                PriceCompactHistory sh = (PriceCompactHistory)e.Item.DataItem;
                Literal ltrChange = e.Item.FindControl("ltrChange") as Literal;
                Literal ltrTotal = e.Item.FindControl("ltrTotal") as Literal;
                Literal ltrPrice = e.Item.FindControl("ltrPrice") as Literal;
                int Floor_Code = TradeCenter;
                dlCeiling = ConvertUtility.ToDouble(sh.Ceiling);
                dlFloor = ConvertUtility.ToDouble(sh.Floor);
                double basicPrice = ConvertUtility.ToDouble(sh.BasicPrice);
                double closePrice = ConvertUtility.ToDouble(sh.ClosePrice); 
                
                if (closePrice == 0) basicPrice = 0;

                ltrPrice.Text = String.Format("{0:#,##0.0}", closePrice);

                double chgIndex = closePrice - basicPrice;
                double pctIndex = 0;
                if (basicPrice > 0)
                    pctIndex = (closePrice + basicPrice) > 0 ? (chgIndex / (basicPrice)) * 100 : 0;

                string strChgIndex = String.Format("{0:#,##0.0}", chgIndex) + " (" + String.Format("{0:#,##0.0}", Math.Round(pctIndex,1)) + "%" + ")";

                //string icon = "<img src='http://cafef3.vcmedia.vn/images/Scontrols/Images/LSG/nochange_.jpg'>";
                //string color = "color:#FF9900;";
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
                //AgreementHistory ah = StockBL.getAgreementHistoryBySymbolAndDate(Symbol, sh.TradeDate.ToString("yyyyMMdd"));
                //if (ah != null)
                //    ltrTotal.Text = String.Format("{0:#,##0}", ConvertUtility.ToDouble(ah.Trans_Value) + ConvertUtility.ToDouble(sh.TotalValue));
                //else
                
                    ltrTotal.Text = String.Format("{0:#,##0}", ConvertUtility.ToDouble(sh.TotalValue + sh.AgreedValue));

            }
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
        //protected string Div(object value1, object value2)
        //{
        //    string strResult = "0";
        //    long lgvalue1 = ConvertToLong(value1);
        //    long lgvalue2 = ConvertToLong(value2);
        //    if (lgvalue2 > 0)
        //        strResult = String.Format("{0:#,##0}", lgvalue1 / lgvalue2);
        //    return strResult;
        //}

        #endregion

        #region Private Methods
        private void getData(string symbol)
        {
            //Stock st = StockBL.getStockBySymbol(symbol);
            //try
            //{
            //    TradeCenter = ConvertUtility.ToInt32(st.companyProfile.basicInfos.TradeCenter);
            //}
            //catch { }
            //List<StockHistory> result = StockBL.get_NTopHistoryStockBySymbol(symbol, Top);
            var histories = GetHistory.Price;
            if (histories.Count > 0) histories = histories.GetRange(0, Math.Min(histories.Count, 10));
            rptLichSuGD.DataSource = histories;
            rptLichSuGD.DataBind();
        }

        #endregion
    }
}