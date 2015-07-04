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
namespace CafeF.Redis.Page.UserControl.StockView
{
    public partial class SymbolNews : System.Web.UI.UserControl
    {
        protected string __symbol = string.Empty;

        public List<StockNews> GetNews
        {
            get
            {
                try
                {
                    return ((Stock)((CafeF.Redis.Page.MasterPage.StockMain)(((ContentPlaceHolder)this.Parent).Page).Master).GetStock).StockNews.FindAll(sn => (sn.DateDeploy.CompareTo(DateTime.Now) <= 0));
                }
                catch
                {
                    return new List<StockNews>();
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            __symbol = Request.QueryString["symbol"] != null ? Request.QueryString["symbol"].ToString().ToLower() : "";
            if (!IsPostBack)
            {
                List<StockNews> tbNews = new List<StockNews>();

                if (Request.QueryString["symbol"] != null)
                {
                    int NumNews = Int32.Parse(ConfigurationManager.AppSettings["SoLuongTinDoanhNghiep"].ToString());
                    tbNews = GetNews;
                    tbNews = tbNews.Take(NumNews).ToList(); 
                    rptTopEvents.DataSource = tbNews;
                    rptTopEvents.DataBind();
                }
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
                    _ltr.Text = Hepler.Event_BuildLink(NewsId, __symbol, title, __d) + (__r.DateDeploy.ToString("yyyyMMdd")==DateTime.Now.ToString("yyyyMMdd")?" <img alt='' src='http://cafef3.vcmedia.vn/images/new.gif' />":"");
                }
            }
            catch
            { }
        }
    }
}