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
using CafeF.Redis.Page.UserControl.StockView;

namespace CafeF.Redis.Page.Ajax.CungNganh
{
    public partial class SameCategory : System.Web.UI.Page
    {
        public List<StockShortInfo> GetSameCategory
        {
            get
            {
                try
                {
                    return StockBL.getStockBySymbol(Request.QueryString["symbol"] != null ? Request.QueryString["symbol"].ToString().ToUpper() : "").SameCategory;
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
            var dt = GetSameCategory.GetPaging(indx, size);
            var ret = new List<InSameCategory.MyStock>();
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
                ret.Add(new InSameCategory.MyStock() { Name = data.Name, Symbol = data.Symbol, TradeCenterId = data.TradeCenterId, EPS = data.EPS, Price = p.Price, CeilingPrice = p.CeilingPrice, FloorPrice = p.FloorPrice, RefPrice = p.RefPrice });
            }
            rptSameCategory.DataSource = ret;
            rptSameCategory.DataBind();
        }

        protected void rptSameCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Literal ltrPrice = (Literal)e.Item.FindControl("ltrPrice");
                Literal ltrPercent = (Literal)e.Item.FindControl("ltrPercent");
                Literal ltrEPS = (Literal)e.Item.FindControl("ltrEPS");
                Literal ltrPE = (Literal)e.Item.FindControl("ltrPE");
                Literal ltrSan = (Literal)e.Item.FindControl("ltrSan");
                InSameCategory.MyStock si = (InSameCategory.MyStock)e.Item.DataItem;
                ltrSan.Text = Utils.GetCenterName(si.TradeCenterId.ToString());
                ltrEPS.Text = String.Format("{0:#,##0.0}", si.EPS);
                string color = "orange";
                HtmlGenericControl divColor = (HtmlGenericControl)e.Item.FindControl("divColor");

                //StockPrice sp = StockBL.getStockPriceBySymbol(si.Symbol);
                ltrPrice.Text = "-";
                ltrPercent.Text = "-";
                ltrPE.Text = "-";
                if (si.Price > 0)
                {
                    ltrPrice.Text = String.Format("{0:#,##0.0}", ConvertUtility.ToDouble(si.Price));
                    ltrPE.Text = ConvertUtility.ToDouble(si.EPS) != 0 ? String.Format("{0:#,##0.0}", Math.Round(ConvertUtility.ToDouble(si.Price) / ConvertUtility.ToDouble(si.EPS), 2)) : "-";
                    double chgIndex = si.Price - si.RefPrice;
                    double pctIndex = 0;
                    if (si.RefPrice > 0) pctIndex = (chgIndex / si.RefPrice) * 100;

                    ltrPercent.Text = string.Format("({0}{1}{2})", pctIndex >= 0 ? "+" : "", String.Format("{0:#,##0.0}", pctIndex), "%");

                    if (pctIndex < 0)
                    {
                        color = "red";
                    }
                    if (pctIndex > 0)
                    {
                        color = "green";
                    }

                    if (si.CeilingPrice > 0 && Math.Round(si.Price, 1) == Math.Round(si.CeilingPrice, 1))
                    {
                        color = "pink";
                    }
                    else
                        if (si.FloorPrice > 0 && Math.Round(si.Price, 1) == Math.Round(si.FloorPrice, 1))
                        {
                            color = "blue";
                        }

                    divColor.Attributes.Add("class", color);
                }
            }
        }
    }
}
