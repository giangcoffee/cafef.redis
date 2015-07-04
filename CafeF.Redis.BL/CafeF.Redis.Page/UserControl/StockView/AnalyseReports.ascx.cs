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
    public partial class AnalyseReports : System.Web.UI.UserControl
    {
        public List<Reports> GetReports
        {
       
            get 
            {      
                try
                {
                    return ((Stock)((CafeF.Redis.Page.MasterPage.StockMain)(((ContentPlaceHolder)this.Parent).Page).Master).GetStock).Reports3; 
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
            object objSymbol = Request.QueryString["symbol"];
            if (objSymbol == null || objSymbol.ToString().Trim() == "")
                return;
            
            List<Reports> tblResult = GetReports;// StockBL.get_NTopReportsStockBySymbol(objSymbol.ToString(), Top);
            if (tblResult != null && tblResult.Count > 0)
            {
                if (tblResult.Count > 6)
                {
                    rpData.DataSource = tblResult.GetRange(0, 5);
                }
                else
                {
                    rpData.DataSource = tblResult;
                }
                rpData.DataBind();
            }
            else
                divCompany.Visible = false;
        }
        protected string GetSource(object value)
        {
            string strResult = string.Empty;
            if (value == null || value == DBNull.Value || value.ToString().Trim() == "")
                strResult = "";
            strResult = (value!=null?" - " + value.ToString():"");
            return strResult;
        }
    }
}