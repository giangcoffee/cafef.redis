using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CafeF.Redis.BL;

namespace CafeF.Redis.Page.UserControl.StockView
{
    public partial class ucBaoCaoTaiChinh : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack)return;
        }
        public void LoadData(string symbol)
        {
            ltrDownload.Text = StockBL.getFinanceReport(symbol);
        }
    }
}