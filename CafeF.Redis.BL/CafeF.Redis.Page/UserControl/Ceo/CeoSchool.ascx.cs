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

namespace CafeF.Redis.Page.UserControl.Ceo
{
    public partial class CeoSchool : System.Web.UI.UserControl
    {
        private CafeF.Redis.Entity.Ceo ceo;

        private string ceoCode = "";
        protected string profileShort = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ceoCode = Request.QueryString["ceocode"] != null ? Request.QueryString["ceocode"] : "";
                ceo = GetCeo;
                if (ceo != null)
                {
                    FillData(ceo);
                }
            }
        }

        private CafeF.Redis.Entity.Ceo GetCeo
        {

            get
            {
                try
                {
                    return ((CafeF.Redis.Entity.Ceo)((CafeF.Redis.Page.MasterPage.StockMain)(((ContentPlaceHolder)this.Parent).Page).Master).GetCeo);
                }
                catch
                {
                    return new CafeF.Redis.Entity.Ceo();
                }
            }
        }

        private void FillData(CafeF.Redis.Entity.Ceo c)
        {
            profileShort = c.CeoProfileShort;
            if (profileShort != null && !profileShort.Trim().Equals(""))
            {
                divSchool.Visible = true;
            }
            else
            {
                divSchool.Visible = false;
            }
        }

        #region Remove
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        BindData();
        //    }
        //}

        //private List<CafeF.Redis.Entity.CeoSchool> GetCeoSchool
        //{

        //    get
        //    {
        //        try
        //        {
        //            return ((CafeF.Redis.Entity.Ceo)((CafeF.Redis.Page.MasterPage.StockMain)(((ContentPlaceHolder)this.Parent).Page).Master).GetCeo).CeoSchool;
        //        }
        //        catch
        //        {
        //            return new List<CafeF.Redis.Entity.CeoSchool>();
        //        }
        //    }
        //}

        //protected void BindData()
        //{
        //    object objSymbol = Request.QueryString["ceocode"];
        //    if (objSymbol == null || objSymbol.ToString().Trim() == "")
        //        return;

        //    List<CafeF.Redis.Entity.CeoSchool> tblResult = GetCeoSchool;
        //    if (tblResult != null && tblResult.Count > 0)
        //    {
        //        rpData.DataSource = tblResult;
        //        rpData.DataBind();
        //    }
        //}
        #endregion
    }
}