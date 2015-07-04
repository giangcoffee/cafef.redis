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

namespace CafeF.Redis.Page.UserControl.Ceo
{
    public partial class CeoIn : System.Web.UI.UserControl
    {
        private int type = 1;
        private string name = "";
        protected string style = "";
        protected string StorageServer = ConfigurationManager.AppSettings["StorageServer"] ?? "http://testcafef.vcmedia.vn/";
        protected string CeoStoragePath = ConfigurationManager.AppSettings["CeoStoragePath"] ?? "Common/CEO/";
        public IDictionary<string, string> CeoPhotos
        {
            get
            {
                try
                {
                    return ((CafeF.Redis.Page.MasterPage.StockMain)(((ContentPlaceHolder)this.Parent).Page).Master).CeoPhotos;
                }
                catch
                {
                    return new Dictionary<string, string>();
                }
            }
        }
        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private List<CafeF.Redis.Entity.StockCeo> GetStockCeo
        {

            get
            {
                try
                {
                    return GetStock.CompanyProfile.AssociatedCeo;
                }
                catch
                {
                    return new List<CafeF.Redis.Entity.StockCeo>();
                }
            }
        }
        public Stock GetStock
        {
            get
            {
                try
                {
                    return (CafeF.Redis.Entity.Stock)((CafeF.Redis.Page.MasterPage.StockMain)(((ContentPlaceHolder)this.Parent).Page).Master).GetStockForCeo;
                }
                catch
                {
                    return new Stock();
                }
            }
        }

        protected String makeLink(string code, string name)
        {
            if (name.StartsWith("Ông ")) name = name.Substring(4);
            else if (name.StartsWith("Bà ")) name = name.Substring(3);
            string value = "/ceo/" + code + "/" + CafeF.Redis.BO.UnicodeUtility.UnicodeToKoDauAndGach(name) + ".chn?cs=" + GetStock.Symbol;
            return value;
        }

        protected void BindData()
        {
            object objSymbol = Request.QueryString["ceocode"];
            if (objSymbol == null || objSymbol.ToString().Trim() == "")
                return;

            List<CafeF.Redis.Entity.StockCeo> tblResult = GetStockCeo;
            if (tblResult != null && tblResult.Count > 0)
            {
                tblResult = tblResult.FindAll(pf => pf.GroupID == type);
                if (tblResult.Count > 0)
                {
                    style = "";
                    rpData.DataSource = tblResult;
                    rpData.DataBind();
                }
                else
                {
                    style = "display:none";
                }
            }
        }

        protected string GetImages(string code)
        {
            if (CeoPhotos.ContainsKey(code) && !string.IsNullOrEmpty(CeoPhotos[code]))
            {
                return CeoPhotos[code];
            }
            return "noimage.jpg";
        }
    }
}