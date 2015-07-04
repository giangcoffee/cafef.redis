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
using CafeF.Redis.BL;
using CafeF.Redis.Data;
using CafeF.Redis.Entity;
using ServiceStack.Redis;

namespace CafeF.Redis.Page.UserControl.Header
{
    public partial class ucChart : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (IsPostBack) return;
            //LoadData();
        }

        public string GetDateVN(DateTime _Date)
        {
            if (_Date == null) return "";
            string[] ArrayDay = new string[] { "Chủ nhật", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7" };
            int currDay = (int)_Date.DayOfWeek;

            return ArrayDay[currDay];
        }

        //private void LoadData()
        //{
        //    try
        //    {
        //        var inTrading = Utils.InTradingTime(0);
        //        var redis = BLFACTORY.RedisClient;
        //        var key = string.Format(RedisKey.KeyCenterIndex, "1");
        //        var hsx = redis.ContainsKey(key) ? redis.Get<TradeCenterStats>(key) : new TradeCenterStats() { TradeCenterId = 1 };
        //        key = string.Format(RedisKey.KeyCenterIndex, "2");
        //        var hnx = redis.ContainsKey(key) ? redis.Get<TradeCenterStats>(key) : new TradeCenterStats() { TradeCenterId = 2 };
        //        ltrDateTime.Text = "Cập nhật lúc " + String.Format("{0:HH:mm}", (inTrading ? hsx.CurrentDate : Utils.GetCloseTime(0))) + " " + Hepler.GetDateVN(hsx.CurrentDate);

        //        ltrVnIndex.Text = string.Format("<b>{0}</b> {1}{2} ({1}{3}%)",hsx.CurrentIndex.ToString("#,##0.00"), hsx.CurrentIndex>hsx.PrevIndex?"+":"", (hsx.CurrentIndex-hsx.PrevIndex).ToString("#,##0.00"), hsx.PrevIndex>0?((hsx.CurrentIndex-hsx.PrevIndex)/hsx.PrevIndex*100).ToString("#0.00"): "");
        //        ltrHnxIndex.Text = string.Format("<b>{0}</b> {1}{2} ({1}{3}%)", hnx.CurrentIndex.ToString("#,##0.00"), hnx.CurrentIndex > hnx.PrevIndex ? "+" : "", (hnx.CurrentIndex - hnx.PrevIndex).ToString("#,##0.00"), hnx.PrevIndex > 0 ? ((hnx.CurrentIndex - hnx.PrevIndex) / hnx.PrevIndex * 100).ToString("#0.00") : "");
        //        divVnIndex.Attributes["class"] = "bd-vni " + (hsx.CurrentIndex < hsx.PrevIndex ? "down" : "up");
        //        divHnxIndex.Attributes["class"] = "bd-vni " + (hnx.CurrentIndex < hnx.PrevIndex ? "down" : "up");

        //        ltrVnIndexValue.Text = hsx.CurrentValue.ToString("#,##0.0");
        //        ltrHnxIndexValue.Text = hnx.CurrentValue.ToString("#,##0.0");
        //    }
        //    catch (Exception) { }
        //}
    }
}