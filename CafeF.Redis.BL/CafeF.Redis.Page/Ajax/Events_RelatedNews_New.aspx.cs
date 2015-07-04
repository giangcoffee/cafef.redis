using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using CafeF.Redis.Entity;
using CafeF.Redis.BL;
using CafeF.Redis.Page;
using CafeF.Redis.Data;
namespace CafeF.Redis.Page.Ajax
{
    public partial class Events_RelatedNews_New : System.Web.UI.Page
    {
        private string __symbol = string.Empty;
        private int PageIndex = 1;
        private int PageSize = 6;
        private int ItemCount = 0;
        private string configID = "";
        private string type = "1";
        public List<StockNews> GetNews
        {
            get
            {
                try
                {
                    var symbol = (Request["symbol"] ?? "").ToUpper();
                    if (!string.IsNullOrEmpty(symbol)) return StockHistoryBL.GetNewsByStock(symbol, int.Parse(configID), PageIndex, PageSize);
                    else if (type == "2") return StockHistoryBL.GetAllNews(int.Parse(configID), PageIndex, PageSize);
                    else return new List<StockNews>();

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
            configID = Request.QueryString["ConfigID"] != null ? Request.QueryString["ConfigID"].ToString() : "0";
            PageIndex = Request.QueryString["PageIndex"] != null ? Convert.ToInt32(Request.QueryString["PageIndex"]) : 1;
            PageSize = Request.QueryString["PageSize"] != null ? Convert.ToInt32(Request.QueryString["PageSize"]) : Int32.Parse(ConfigurationManager.AppSettings["SoLuongTinDoanhNghiep"].ToString());
            type = Request.QueryString["Type"] != null ? Request.QueryString["Type"] : "1";

            if (!IsPostBack)
            {
                //if (Request.QueryString["symbol"] != null)
                //{
                //List<StockNews> __tblNews = new List<StockNews>();

                //__tblNews = GetNews.FindAll(delegate(StockNews sn)
                //                                {
                //                                    return ((("," + sn.TypeID.ToString() + ",").IndexOf("," + configID + ",") >= 0) || (configID == "0"));
                //                                }
                //);
                //rptTopEvents.DataSource = Utility.GetPaging(__tblNews,PageIndex,PageSize);
                rptTopEvents.DataSource = GetNews;
                rptTopEvents.DataBind();

                //}
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
                        s = __r.Title.ToString().Substring(0, __r.Title.ToString().IndexOf(":") >= 0 ? __r.Title.ToString().IndexOf(":") : 0);
                    _ltr.Text = Hepler.Event_BuildLink(NewsId, s, title, __d);
                    if (__r.DateDeploy.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
                        _ltr.Text = _ltr.Text + " <img src='http://cafef3.vcmedia.vn/images/new.gif' alt='' />";
                }
            }
            catch
            { }
        }

        protected override void Render(HtmlTextWriter output)
        {
            output.Write(RenderControlToString(divEvents));
        }
        public string RenderControlToString(Control ctr)
        {
            StringBuilder sb = new StringBuilder();
            System.IO.StringWriter sw = new System.IO.StringWriter(sb);
            HtmlTextWriter htmlWriter = new HtmlTextWriter(sw);
            ctr.RenderControl(htmlWriter);
            return sb.ToString();
        }
    }
}
