using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CafeF.Redis.BL;
using CafeF.Redis.Entity;

namespace CafeF.Redis.Page.UserControl.KhopLenh
{
    public partial class test : System.Web.UI.Page
    {
        #region Properties

        private StockPrice price;
        private StockHistory history;
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            txtSymbol.Text = "SSI";
            var center = TradeCenterBL.getByTradeCenter(1);
            dpkTradeDate1.SelectedDate = center.CurrentDate;
            price = PriceRedisBL.getStockPriceBySymbol(txtSymbol.Text);
            LoadData();
        }
        protected void btnXem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSymbol.Text)) return;
            LoadData();
        }
        #endregion

        #region Business
        private void LoadData()
        {
            if (string.IsNullOrEmpty(txtSymbol.Text)) return;
            int count;
            var prices = dpkTradeDate1.SelectedDate.ToString("yyyyMMdd")==DateTime.Now.ToString("yyyyMMdd") ? null : StockHistoryBL.get_StockHistoryBySymbolAndDate(txtSymbol.Text.Trim().ToUpper(), dpkTradeDate1.SelectedDate, dpkTradeDate1.SelectedDate, 1, 1, out count);
            if (prices != null && prices.Count > 0)
            {
                history = prices[0];
                price = null;
            }
            else
            {
                history = null;
                price = PriceRedisBL.getStockPriceBySymbol(txtSymbol.Text.Trim().ToUpper());
            }
            repResult.DataSource = KhopLenhBL.GetBySymbolDate(txtSymbol.Text.Trim().ToUpper(), dpkTradeDate1.SelectedDate);
            repResult.DataBind();
        }
        protected string DisplayPrice(object p)
        {

            if (p == null) return "";
            double cp;
            if (!double.TryParse(p.ToString(), out cp)) cp = 0;
            if (cp == 0) return "";
            double open, ceiling, floor;
            if(price==null && history!=null)
            {
                open = history.BasicPrice;
                ceiling = history.Ceiling;
                floor = history.Floor;
            }else if(price!=null)
            {
                open = price.RefPrice;
                ceiling = price.CeilingPrice;
                floor = price.FloorPrice;
            }else
            {
                return "";
            }
            var change = cp - open;
            var changePercent = open > 0 ? (change / open * 100) : 0;
            var color = "#D0AD08";
            if (cp >= ceiling) color = "#E200FF";
            else if (cp > open) color = "green";
            else if (cp < open) color = "red";
            else if (cp <= floor) color = "#3987AE";
            return string.Format("{0} <span style='color:{1}'>{2}{3}</span> (<span style='color:{1}'>{2}{4}%</span>)", cp.ToString("#.0"), color, change > 0 ? "+" : "", change.ToString("#0.0"), changePercent.ToString("##0.0"));
        }
        protected string DisplayTime(object t)
        {
            DateTime d;
            if (!DateTime.TryParse(t.ToString(), out d)) return "";
            return d.ToString("HH:mm:ss");
        }
        protected string DisplayVolume(object v)
        {
            double d;
            if (!double.TryParse(v.ToString(), out d)) return "";
            return d.ToString("#,##0");
        }
        #endregion
    }
}
