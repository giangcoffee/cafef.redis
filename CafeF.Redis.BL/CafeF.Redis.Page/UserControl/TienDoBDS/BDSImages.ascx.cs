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


namespace CafeF.Redis.Page.UserControl.TienDoBDS
{
    public partial class BDSImages : System.Web.UI.UserControl
    {
        protected string imageid="",mainSrc = "";
        private List<string> lsImages = new List<string>();
        private string sHostTienDoBDSPath = ConfigurationManager.AppSettings["HostTienDoBDSPath"] ?? "";
        private string sTienDoBDSPath = ConfigurationManager.AppSettings["TienDoBDSPath"] ?? "";
        protected string sZoomTienDoBDSPath = ConfigurationManager.AppSettings["ZoomTienDoBDSPath"] ?? "";
        protected string sZoomMain = "/zoom/290_200";
        protected string sNoImage = "http://testcafef.vcmedia.vn/zoom/290_200/Common/TienDoBDSno_images.jpg";
        public List<string> BDSImagesList
        {
            set { lsImages = value; }
            get { return lsImages; }

        }
        public string ImageID
        {
            set { imageid = value; }
            get { return imageid; }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               this.BindData();
            }
        }

        private void BindData()
        {
            if (lsImages != null && lsImages.Count > 0)
            {
                mainSrc = sHostTienDoBDSPath + sZoomMain + sTienDoBDSPath + lsImages[0].ToString();
                List<string> databind = this.SelectTopFrom(lsImages, 5);
                rptImages.DataSource = databind;
                rptImages.DataBind();
                if (databind.Count <= 1)
                    rptImages.Visible = false;
            }
            else
            {
                mainSrc = sNoImage;
                rptImages.Visible = false;
            }
        }

        private List<string> SelectTopFrom(List<string> dt, int rowCount)
        {
            List<string> dtn = new List<string>();
            for (int i = 0; i < rowCount; i++)
            {
                if (i < dt.Count)
                    dtn.Add(sHostTienDoBDSPath + sZoomTienDoBDSPath + sTienDoBDSPath + dt[i].ToString());
            }
            return dtn;
        }

        protected string ToUrl(string code)
        {
            return "";
        }
    }
}