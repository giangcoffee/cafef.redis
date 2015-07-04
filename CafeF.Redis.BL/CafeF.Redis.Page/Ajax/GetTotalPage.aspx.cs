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

using CafeF.Redis.Entity;
using CafeF.Redis.BL;
using CafeF.Redis.Data;
namespace CafeF.Redis.Page.Ajax
{
    public partial class GetTotalPage : System.Web.UI.Page
    {
        private string __symbol = string.Empty;
        private int PageIndex = 1;
        private int PageSize = 7;
        private string configID = "";
        protected int intTotalPage = 1;
        private int total = 1;
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
            __symbol = Request.QueryString["symbol"] != null ? Request.QueryString["symbol"].ToString() : "";
            configID = Request.QueryString["ConfigID"] != null ? Request.QueryString["ConfigID"].ToString() : "0";
            PageIndex = Request.QueryString["PageIndex"] != null ? Convert.ToInt32(Request.QueryString["PageIndex"]) : 1;
            PageSize = Request.QueryString["PageSize"] != null ? Convert.ToInt32(Request.QueryString["PageSize"]) : Int32.Parse(ConfigurationManager.AppSettings["SoLuongTinDoanhNghiep"].ToString());
            type = Request.QueryString["Type"] != null ? Request.QueryString["Type"] : "1";
            if (!IsPostBack)
            {
                    List<StockNews> __tblNews = new List<StockNews>();
                    __tblNews = GetNews;
                    total = __tblNews.Count;
                    intTotalPage = (total - 1) / PageSize + 1;
                    ltrTotalPage.Text = intTotalPage.ToString();
            }

        }  
                
    }
}
