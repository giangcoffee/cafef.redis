using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CafeF.Redis.BL;

namespace CafeF.Redis.Page
{
    public partial class FilesDownload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ltrDownload.Text = StockBL.getFinanceReport((Request["symbol"] ?? "").ToUpper());
            }
        }
    }
}
