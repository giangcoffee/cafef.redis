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
using System.Text;
using CafeF.Redis.BL;
using CafeF.Redis.Page;
using CafeF.Redis.Data;
using CafeF.Redis.Entity;
namespace CafeF.Redis.Page.Ajax.CongTy
{
    public partial class BanLanhDao : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            LoadData();
        }

        private void LoadData()
        {
            var stock = StockBL.getStockBySymbol(Request["sym"] ?? "");
            if (stock == null || stock.CompanyProfile==null) return;
            ucBanLanhDao1.Visible = false;
            ucBanLanhDao2.Visible = false;
            var allowV2 = (ConfigurationManager.AppSettings["AllowCeoV2"] ?? "") == "TRUE";
            if(!allowV2 || stock.CompanyProfile.AssociatedCeo==null || stock.CompanyProfile.AssociatedCeo.Count==0){
                ucBanLanhDao1.LoadData(stock);
                ucBanLanhDao1.Visible = true;
            }else
            {
                ucBanLanhDao2.LoadData(stock);
                ucBanLanhDao2.Visible = true;
            }
        }
    }
}
