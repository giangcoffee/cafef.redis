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
using CafeF.Redis.Entity;
using CafeF.Redis.BL;
using CafeF.Redis.Page;
namespace CafeF.Redis.Page.UserControl.StockView
{
    public partial class TradeInfoHeader : System.Web.UI.UserControl
    {
        public string __Symbol = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            __Symbol = (Request.QueryString["symbol"] ?? "").ToUpper();
            if (__Symbol == "") return;
            __Symbol = Server.UrlDecode(__Symbol);
            __Symbol = __Symbol.Replace(",", "");
            if (IsPostBack) return;
            LoadData();
        }

        private void LoadData()
        {
            __Symbol = __Symbol.Trim().ToUpper();
            string name = "";
            var center = "";
            var bShowTradeCenter = false;
            try
            {
                var stock = (((CafeF.Redis.Page.MasterPage.StockMain)(((ContentPlaceHolder)this.Parent).Page).Master).GetStock);
                name = stock.CompanyProfile.basicInfos.Name;
                center = stock.CompanyProfile.basicInfos.TradeCenter;
                bShowTradeCenter = stock.ShowTradeCenter;
            }
            catch (Exception)
            {
                var compact = StockBL.GetStockCompactInfo(__Symbol);
                if (compact != null)
                {
                    name = compact.CompanyName;
                    center = compact.TradeCenterId.ToString();
                    bShowTradeCenter = compact.ShowTradeCenter;
                }
            }

            //if (stock == null) return;
            //var bs = stock.CompanyProfile.basicInfos;
            //if (bs.Symbol == null)
            //    try
            //    {
            //        bs = StockBL.getStockBySymbol(__Symbol).CompanyProfile.basicInfos;
            //    }
            //    catch { }
            ltrComapanyName.Text = name;
            ltrSymbol.Text = __Symbol;
            ltrTradeCenter.Text = " (" + GetNameCenter(center) + ")";
            ltrTradeCenter.Visible = bShowTradeCenter;
            
            return;
        }
        
        public string GetNameCenter(string value)
        {
            string strResult = string.Empty;
            switch (value)
            {
                case "1":
                    strResult = "HOSE";
                    break;
                case "2":
                    strResult = "HNX";
                    break;
                case "8":
                    strResult = "OTC";
                    break;
                case "9":
                    strResult = "UpCOM";
                    break;
            }
            return strResult;
        }
    }
}