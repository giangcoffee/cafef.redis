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
using CafeF.Redis.BL;
using CafeF.Redis.Entity;

namespace CafeF.Redis.Page.UserControl.Ceo
{
    public partial class CeoProfile : System.Web.UI.UserControl
    {
        private CafeF.Redis.Entity.Ceo ceo;

        protected string ceoCode = "";
        protected string image = "";
        protected string name = "";
        protected string birthday = "";
        protected string idNo = "";
        protected string homeTown = "";
        protected string achievements = "";
        protected string profileShort = "";
        protected string schoolDegree = "";
        protected string Symbol = "SSI";
        protected string San = "hose";
        protected string StorageServer = ConfigurationManager.AppSettings["StorageServer"] ?? "http://testcafef.vcmedia.vn/";
        protected string CeoStoragePath = ConfigurationManager.AppSettings["CeoStoragePath"] ?? "Common/CEO/";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ceoCode = (Request["ceocode"] ?? "").Trim().ToUpper();
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
        protected Stock GetCeoStock
        {
            get
            {
                try
                {
                    return ((CafeF.Redis.Page.MasterPage.StockMain)(((ContentPlaceHolder)this.Parent).Page).Master).GetStockForCeo;
                }
                catch
                {
                    return new Stock();
                }
            }
        }
        private void FillData(CafeF.Redis.Entity.Ceo c)
        {
            if (c == null || string.IsNullOrEmpty(c.CeoCode)) return;
            //List<CafeF.Redis.Entity.CeoSchool> tblResult = c.CeoSchool;
            //rpData.DataSource = tblResult;
            //rpData.DataBind();
            if (CeoPhotos.ContainsKey(c.CeoCode) && !string.IsNullOrEmpty(CeoPhotos[c.CeoCode]))
            {
                image = CeoPhotos[c.CeoCode];
            }
            else
            {
                image = "noimage.jpg";
            }
            //if (image == null || image == "")
            //{
            //    image = "http://images1.cafef.vn/batdongsan/Images/media/noimage.jpg";
            //}
            name = c.CeoName;
            birthday = c.CeoBirthday;
            idNo = c.CeoIdNo;
            homeTown = c.CeoHomeTown;
            achievements = c.CeoAchievements;
            profileShort = c.CeoProfileShort;
            schoolDegree = c.CeoSchoolDegree;
            //if (ceoCode.Contains("_"))
            //{
            //    Symbol = ceoCode.Substring(0, ceoCode.IndexOf("_"));
            //    var stock = StockBL.GetStockCompactInfo(Symbol);
            //    if (stock != null)
            //    {
            //        San = Utils.GetCenterFolder(stock.TradeCenterId.ToString());
            //    }
            //}
            var stock = GetCeoStock;
            if (stock != null)
            {
                Symbol = stock.Symbol;
                San = Utils.GetCenterFolder(stock.TradeCenterId.ToString());
            }
        }
    }
}