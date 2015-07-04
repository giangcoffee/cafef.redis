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
using CafeF.Redis.Data;

namespace CafeF.Redis.Page.Ajax.CungNganh
{
    public partial class SamePE : System.Web.UI.Page
    {
        public List<StockShortInfo> GetSamePE
        {
            get
            {
                try
                {
                    return StockBL.getStockBySymbol((Request["symbol"]??"").ToUpper()).SamePE;
                }
                catch
                {
                    return new List<StockShortInfo>();
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int PageIndex = Request.QueryString["PageIndex"] != null ? Convert.ToInt32(Request.QueryString["PageIndex"]) : 1;
                int PageSize = Request.QueryString["PageSize"] != null ? Convert.ToInt32(Request.QueryString["PageSize"]) : Int32.Parse(ConfigurationManager.AppSettings["SoLuongTinDoanhNghiep"].ToString());

                getData(PageIndex, PageSize);
            }
        }
        private void getData(int indx, int size)
        {
            var dt = GetSamePE.GetPaging(indx, size);
            var ret = new List<MyStock>();
            var ss = new List<string>();
            foreach (var data in dt)
            {
                var symbol = data.Symbol;
                while (symbol.StartsWith("&")) symbol = symbol.Remove(0, 1);
                if (symbol.Contains("&")) symbol = symbol.Substring(0, symbol.IndexOf("&"));
                if (!ss.Contains(symbol)) ss.Add(symbol);
            }
            var ps = StockBL.GetStockPriceMultiple(ss);
            foreach (var data in dt)
            {
                var symbol = data.Symbol;
                while (symbol.StartsWith("&")) symbol = symbol.Remove(0, 1);
                if (symbol.Contains("&")) symbol = symbol.Substring(0, symbol.IndexOf("&"));
                var p = ps[symbol] ?? new StockPrice();
                ret.Add(new MyStock() { Name = data.Name, Symbol = data.Symbol, TradeCenterId = data.TradeCenterId, EPS = data.EPS, Price = p.Price, CeilingPrice = p.CeilingPrice, FloorPrice = p.FloorPrice, RefPrice = p.RefPrice, MarketValue = data.MarketValue });
            }
            rptPE.DataSource = ret;
            rptPE.DataBind();
        }

        protected void rptPE_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Literal ltrEPS = (Literal)e.Item.FindControl("ltrEPS");
                Literal ltrSan = (Literal)e.Item.FindControl("ltrSan");
                Literal ltrPrice = (Literal)e.Item.FindControl("ltrPrice");
                Literal ltrPE = (Literal)e.Item.FindControl("ltrPE");
                Literal ltrVonHoa = (Literal)e.Item.FindControl("ltrVonHoa");
                MyStock si = (MyStock)e.Item.DataItem;
                ltrSan.Text = Utils.GetCenterName(si.TradeCenterId.ToString());
                ltrEPS.Text = String.Format("{0:#,##0.0}", si.EPS);
                ltrVonHoa.Text = String.Format("{0:#,##0.0}", si.MarketValue);
                ltrPrice.Text = "-";
                ltrPE.Text = "-";
                //StockPrice sp = StockBL.getStockPriceBySymbol(si.Symbol);
                if (si.Price > 0)
                {
                    ltrPrice.Text = String.Format("{0:#,##0.0}", ConvertUtility.ToDouble(si.Price));
                    ltrPE.Text = ConvertUtility.ToDouble(si.EPS) != 0 ? String.Format("{0:#,##0.0}", Math.Round(ConvertUtility.ToDouble(si.Price) / ConvertUtility.ToDouble(si.EPS), 2)) : "";
                }
            }
        }

        internal class MyStock
        {
            public string Symbol { get; set; }
            public string Name { get; set; }
            public int TradeCenterId { get; set; }
            public double EPS { get; set; }
            public double Price { get; set; }
            public double RefPrice { get; set; }
            public double FloorPrice { get; set; }
            public double CeilingPrice { get; set; }
            public double MarketValue { get; set; }
        }
    }
}
