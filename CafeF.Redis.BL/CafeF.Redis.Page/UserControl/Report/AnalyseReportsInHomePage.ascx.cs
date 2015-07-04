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
    public partial class AnalyseReportsInHomePage : System.Web.UI.UserControl
    {
        public List<Reports> GetReports
        {
       
            get 
            {      
                try
                {
                    return ReportsBL.get_ReportsByTop(5);
                }
                catch
                {
                    return new List<Reports>();
                }
           }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }
        protected void BindData()
        {
            List<Reports> tblResult = GetReports;// StockBL.get_NTopReportsStockBySymbol(objSymbol.ToString(), Top);
            if (tblResult != null && tblResult.Count > 0)
            {
                rpData.DataSource = tblResult;
                rpData.DataBind();
            }
            else
                divCompany.Visible = false;
        }
        protected string GetSource(object value)
        {
            if (value == null || value == DBNull.Value || value.ToString().Trim() == "")
                return "";
            return " - " + value.ToString();;
        }
    }
}