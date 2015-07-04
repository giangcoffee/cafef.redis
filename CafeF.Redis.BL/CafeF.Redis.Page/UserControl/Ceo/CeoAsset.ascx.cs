using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CafeF.Redis.BL;
using CafeF.Redis.Entity;

namespace CafeF.Redis.Page.UserControl.Ceo
{
    public partial class CeoAsset : System.Web.UI.UserControl
    {
        private IDictionary<string, StockPrice> price = null;
        private IDictionary<string, StockCompactInfo> info = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private List<CafeF.Redis.Entity.CeoAsset> GetCeoAsset
        {

            get
            {
                try
                {
                    return ((CafeF.Redis.Entity.Ceo)((CafeF.Redis.Page.MasterPage.StockMain)(((ContentPlaceHolder)this.Parent).Page).Master).GetCeo).CeoAsset;
                }
                catch
                {
                    return new List<CafeF.Redis.Entity.CeoAsset>();
                }
            }
        }
        private IDictionary<string, StockCompactInfo> CeoStocks
        {
            get{
                try
                {
                    return ((CafeF.Redis.Page.MasterPage.StockMain)(((ContentPlaceHolder)this.Parent).Page).Master).CeoStocks;
                }
                catch (Exception)
                {
                    return new Dictionary<string, StockCompactInfo>();
                }}
        }
        private IDictionary<string, StockPrice> CeoStockPrices
        {
            get
            {
                try
                {
                    return ((CafeF.Redis.Page.MasterPage.StockMain)(((ContentPlaceHolder)this.Parent).Page).Master).CeoStockPrices;
                }
                catch (Exception)
                {
                    return new Dictionary<string, StockPrice>();
                }
            }
        }
        protected void BindData()
        {
            object objSymbol = Request.QueryString["ceocode"];
            if (objSymbol == null || objSymbol.ToString().Trim() == "")
                return;

            List<CafeF.Redis.Entity.CeoAsset> tblResult = GetCeoAsset;
            if (tblResult != null && tblResult.Count > 0)
            {
                this.Visible = true;

                price = CeoStockPrices;
                info = CeoStocks;
                rpData.DataSource = tblResult;
                rpData.DataBind();
            }
            else
            {
                this.Visible = false;
            }
        }

        protected string GetValue(double volume, string symbol)
        {
            if (price == null) return "";
            StockPrice p;
            if (!price.TryGetValue(symbol, out p)) return "";
            return ((double)Math.Round(p.Price * volume / Math.Pow(10, 6), 1)).ToString("#,##0.0");
        }
        protected string GetLink(string symbol)
        {
            if(info==null) return symbol;
            StockCompactInfo i;
            if (!info.TryGetValue(symbol, out i)) return symbol;
            return string.Format("<a href='{0}' title='{1}'>{2}</a>", Utils.GetSymbolLink(i.Symbol, i.CompanyName, i.TradeCenterId.ToString()), HttpUtility.HtmlEncode(i.CompanyName).Replace("'",""), i.Symbol);
        }
    }
}