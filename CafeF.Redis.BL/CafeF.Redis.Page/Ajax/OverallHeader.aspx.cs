using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CafeF.Redis.BL;
using CafeF.Redis.Data;
using CafeF.Redis.Entity;

namespace CafeF.Redis.Page.Ajax
{
    public partial class OverallHeader : System.Web.UI.Page
    {
        #region Properties
        private int pageindex = 1, pagesize = 5;
#endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            GetParams();
            if(IsPostBack) return;
            BindTinMoi();
            LoadData();
        }
        #endregion

        #region Business
        private void GetParams()
        {
            if (!int.TryParse(Request["pageindex"] ?? "1", out pageindex)) pageindex = 1;
        }
        private void BindTinMoi()
        {
            var dt = CafeF.BO.NewsPublished.NewsHome.NewsHomeCacheSql.CafeF_News_Select_News_New_with_Page(pageindex, pagesize);
            repListNews.DataSource = dt;
            repListNews.DataBind();
        }
        private void LoadData()
        {
            try
            {
                var inTrading = Utils.InTradingTime(0);
                var redis = BLFACTORY.RedisClient;
                var key = string.Format(RedisKey.KeyCenterIndex, "1");
                var hsx = redis.ContainsKey(key) ? redis.Get<TradeCenterStats>(key) : new TradeCenterStats() { TradeCenterId = 1 };
                key = string.Format(RedisKey.KeyCenterIndex, "2");
                var hnx = redis.ContainsKey(key) ? redis.Get<TradeCenterStats>(key) : new TradeCenterStats() { TradeCenterId = 2 };
                //ltrDateTime.Text = "Cập nhật lúc <span class='idt_1'>" + String.Format("{0:HH:mm}", (inTrading ? hsx.CurrentDate : Utils.GetCloseTime(0))) + "</span> " + Hepler.GetDateVN(hsx.CurrentDate);

                //ltrVnIndex.Text = string.Format("<b>{0}</b> {1}{2} ({1}{3}%)", hsx.CurrentIndex.ToString("#,##0.00"), hsx.CurrentIndex > hsx.PrevIndex ? "+" : "", (hsx.CurrentIndex - hsx.PrevIndex).ToString("#,##0.00"), hsx.PrevIndex > 0 ? ((hsx.CurrentIndex - hsx.PrevIndex) / hsx.PrevIndex * 100).ToString("#0.00") : "");
                //ltrHnxIndex.Text = string.Format("<b>{0}</b> {1}{2} ({1}{3}%)", hnx.CurrentIndex.ToString("#,##0.00"), hnx.CurrentIndex > hnx.PrevIndex ? "+" : "", (hnx.CurrentIndex - hnx.PrevIndex).ToString("#,##0.00"), hnx.PrevIndex > 0 ? ((hnx.CurrentIndex - hnx.PrevIndex) / hnx.PrevIndex * 100).ToString("#0.00") : "");
                divVnIndex.Attributes["class"] = "bd-vni " + (hsx.CurrentIndex < hsx.PrevIndex ? "down" : "up");
                divHnxIndex.Attributes["class"] = "bd-vni " + (hnx.CurrentIndex < hnx.PrevIndex ? "down" : "up");

                ltrVnIndex.Text = hsx.CurrentIndex.ToString("#,##0.00");
                ltrVnIndexChange.Text = (hsx.CurrentIndex > hsx.PrevIndex ? "+" : "") + (hsx.CurrentIndex - hsx.PrevIndex).ToString("#,##0.00");
                ltrVnIndexPercent.Text = (hsx.CurrentIndex > hsx.PrevIndex ? "+" : "") + (hsx.PrevIndex <= 0 ? 0 : ((hsx.CurrentIndex - hsx.PrevIndex) / hsx.PrevIndex * 100)).ToString("#,##0.00") + "%";
                ltrVnIndexValue.Text = hsx.CurrentValue.ToString("#,##0.0");

                ltrHnxIndex.Text = hnx.CurrentIndex.ToString("#,##0.00");
                ltrHnxIndexChange.Text = (hnx.CurrentIndex > hnx.PrevIndex ? "+" : "") + (hnx.CurrentIndex - hnx.PrevIndex).ToString("#,##0.00");
                ltrHnxIndexPercent.Text = (hnx.CurrentIndex > hnx.PrevIndex ? "+" : "") + (hnx.PrevIndex <= 0 ? 0 : ((hnx.CurrentIndex - hnx.PrevIndex) / hnx.PrevIndex * 100)).ToString("#,##0.00") + "%";
                ltrHnxIndexValue.Text = hnx.CurrentValue.ToString("#,##0.0");
            }
            catch (Exception ex) {
                ltrError.Text = ex.ToString(); }
        }
        #endregion
    }
}
