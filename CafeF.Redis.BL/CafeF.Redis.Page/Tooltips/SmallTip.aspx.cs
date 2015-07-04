using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CafeF.BO;
using System.Text.RegularExpressions;
using System.IO.Compression;
using CafeF.Redis.BL;
using CafeF.Redis.BO;

namespace Portal.ToolTips
{
    public partial class SmallTip : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //CafefCommonHelper.PageComppressed(HttpContext.Current);
            if (!IsPostBack)
            {
                
                //string __StockSymbol = Request.QueryString["symbol"].ToString();
                //DataTable __tbl = KenhFHelper.GetCompanyProfile(__StockSymbol);
                //if (__tbl != null && __tbl.Rows.Count > 0)
                //{
                //    txtFullname.Text ="<span style=\"font-weight:bold;\">"+__StockSymbol+"</span> - "+ __tbl.Rows[0]["Fullname"].ToString();
                //}
                var symbol = (Request["symbol"] ?? "").ToUpper();
                var stock = StockBL.GetStockCompactInfo(symbol);
                if(stock==null) return;
                txtFullname.Text ="<span style=\"font-weight:bold;\">"+symbol+"</span> - "+ stock.CompanyName;
                //CompanyInfo1.Folder = stock.FolderChart;
            }
        }
        public bool IsValidEmail(string strIn)
        {
            
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
    }
}
