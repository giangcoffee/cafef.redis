using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CafeF.Redis.BO;

namespace CafeF.Redis.Page.UserControl.NewsTitleHot
{
    public partial class ucNewsTitleHot : System.Web.UI.UserControl
    {
        private string StrNewsTitleHot = "<a href=\"{0}\" class=\"dangky\" {1}" +
                                    "<img src=\"http://cafef3.vcmedia.vn/images/images/muiten1.gif\" border=\"0\">{2}{3}</a>&nbsp;&nbsp;&nbsp;";
        private string strNewIcon = "<img align=\"AbsMiddle\" src=\"http://cafef3.vcmedia.vn/images/new.gif\" border=\"0\" />";

        private string strFormat = "style=\"font: Times New Roman; font-size: 13px;\">";

        private string Cat_ID = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["cat_id"] != null && Request.QueryString["cat_id"].ToString() != "")
                Cat_ID = Request.QueryString["cat_id"].ToString();

            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            int cat = 0;
            if (!int.TryParse(Cat_ID, out cat)) cat = 0;
            ltrNewsTitleHot.Text = NewsTitleHotHelper.ReturnNewsTitleHot(cat);
        }
    }
}