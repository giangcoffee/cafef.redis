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

namespace CafeF.Redis.Page
{
    public partial class TinDoanhNghiep : System.Web.UI.Page
    {
        protected string __symbol = string.Empty;
        int NumNews = 20;
        private int page = 1;
        private int configId = 0;
        public List<StockNews> GetNews
        {
            get
            {
                try
                {
                    //var symbol = (Request["symbol"] ?? "").ToUpper();
                    //return !string.IsNullOrEmpty(symbol) ? StockBL.getStockBySymbol(symbol).StockNews.FindAll(sn => (sn.DateDeploy.CompareTo(DateTime.Now) <= 0)) : StockHistoryBL.get_TopLatestNews(NumNews);
                    var symbol = (Request["symbol"] ?? "").ToUpper();
                    if(string.IsNullOrEmpty(symbol))
                    {
                        return StockHistoryBL.GetAllNews(configId, page, 20);
                    }else
                    {
                        return StockHistoryBL.GetNewsByStock(symbol, configId, page, 20);
                    }
                }
                catch
                {
                    return new List<StockNews>();
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            __symbol = (Request["symbol"] ?? "").ToUpper();
            if (!int.TryParse(Request["page"] ?? "1", out page)) page = 1;
            if (!int.TryParse(Request["configID"] ?? "0", out configId)) configId = 0;
            if (IsPostBack) return;
            Page.Title = (!string.IsNullOrEmpty(__symbol) ? (__symbol + " : ") : "") + GetConfigName(configId) + (page > 1 ? (" | Trang " + page) : "") + " | CafeF.vn";
            var tbNews = GetNews.Take(NumNews).ToList();
            rptTopEvents.DataSource = tbNews;
            rptTopEvents.DataBind();
        }
        private string GetConfigName(int configId)
        {
            if(configId==1)
            {
                return "Tình hình SXKD - Phân tích khác";
            }
            else if (configId == 2)
            {
                return "Trả cổ tức - Chốt quyền";
            }
            else if (configId == 3)
            {
                return "Thay đổi nhân sự";
            }
            else if (configId == 4)
            {
                return "Tăng vốn - Cổ phiếu quỹ";
            }
            else if (configId == 5)
            {
                return "GD cổ đông lớn - Cổ đông nội bộ";
            }
            else
            {
                return "Tin tức doanh nghiệp niêm yết";
            }
            
        }
        protected void rptTopEvents_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                StockNews __r = (StockNews)e.Item.DataItem;
                if (__r == null) return;
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Literal _ltr = e.Item.FindControl("ltrContent") as Literal;
                    if (_ltr == null) return;
                    string NewsId = __r.ID.ToString();
                    string title = __r.Title.ToString();
                    string __d = String.Format("{0:dd/MM/yyyy HH:mm}", __r.DateDeploy);
                    string s = "";
                    try
                    {
                        s = (__symbol != "" ? __symbol : Hepler.getSymbol(ConvertUtility.ToString(__r.Symbol)));
                    }
                    catch
                    {
                    }
                    if (s == "")
                        s =  __r.Title.ToString().Substring(0, __r.Title.ToString().IndexOf(":")>=0?__r.Title.ToString().IndexOf(":"):0);
                    _ltr.Text = Hepler.Event_BuildLink(NewsId, s, title, __d);
                    if (__r.DateDeploy.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
                        _ltr.Text = _ltr.Text + " <img src='http://cafef3.vcmedia.vn/images/new.gif' alt='' />";
                }
            }
            catch
            { }
        }
    }
}
