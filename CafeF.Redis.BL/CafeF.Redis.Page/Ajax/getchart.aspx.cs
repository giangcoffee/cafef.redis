using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CafeF.Redis.BL;
using CafeF.Redis.Data;
using CafeF.Redis.Entity;

namespace CafeF.Redis.Page.Ajax
{
    public partial class getchart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           LoadData();
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
                ltrDateTime.Text = "Cập nhật lúc " + String.Format("{0:HH:mm}", (inTrading ? hsx.CurrentDate : Utils.GetCloseTime(0))) + " " + Hepler.GetDateVN(hsx.CurrentDate);

                ltrVnIndex.Text = string.Format("<b>{0}</b> {1}{2} ({1}{3}%)", hsx.CurrentIndex.ToString("#,##0.00"), hsx.CurrentIndex > hsx.PrevIndex ? "+" : "", (hsx.CurrentIndex - hsx.PrevIndex).ToString("#,##0.00"), hsx.PrevIndex > 0 ? ((hsx.CurrentIndex - hsx.PrevIndex) / hsx.PrevIndex * 100).ToString("#0.00") : "");
                ltrHnxIndex.Text = string.Format("<b>{0}</b> {1}{2} ({1}{3}%)", hnx.CurrentIndex.ToString("#,##0.00"), hnx.CurrentIndex > hnx.PrevIndex ? "+" : "", (hnx.CurrentIndex - hnx.PrevIndex).ToString("#,##0.00"), hnx.PrevIndex > 0 ? ((hnx.CurrentIndex - hnx.PrevIndex) / hnx.PrevIndex * 100).ToString("#0.00") : "");
                divVnIndex.Attributes["class"] = "bd-vni " + (hsx.CurrentIndex < hsx.PrevIndex ? "down" : "up");
                divHnxIndex.Attributes["class"] = "bd-vni " + (hnx.CurrentIndex < hnx.PrevIndex ? "down" : "up");

                ltrVnIndexValue.Text = hsx.CurrentValue.ToString("#,##0.0");
                ltrHnxIndexValue.Text = hnx.CurrentValue.ToString("#,##0.0");
            }
            catch (Exception) { }
        }
    }
}
