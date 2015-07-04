using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CafeF.Redis.BO;

namespace CafeF.Redis.Page
{
    public partial class ARDownload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int ID;
            if (!int.TryParse(Request.QueryString["IDID"] ?? "0", out ID)) ID = 0;
            if (ID <= 0) { Response.Redirect("http://cafef.vn"); }
            if (Session["DownloadReport"] == null) Session["DownloadReport"] = ",";
            if (!("," + Session["DownloadReport"] + ",").Contains("," + ID + ","))
            {
                Session["DownloadReport"] = Session["DownloadReport"].ToString() + ID + ",";
                CompanyHelper_Update.H_AnalyseReport_Update_DownloadNum(ID);
                //string fileName = Request["filename"] ?? "";
                //if (string.IsNullOrEmpty(fileName)) { Response.Redirect("http://cafef.vn"); }
                //Response.Redirect("http://images1.cafef.vn/Images/Uploaded/DuLieuDownload/PhanTichBaoCao/" + fileName);
            }
        }
    }
}
