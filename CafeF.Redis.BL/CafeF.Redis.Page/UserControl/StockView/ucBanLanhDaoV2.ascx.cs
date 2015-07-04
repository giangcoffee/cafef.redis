using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
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

namespace CafeF.Redis.Page.UserControl.StockView
{
    public partial class ucBanLanhDaoV2 : System.Web.UI.UserControl
    {
        protected bool TabBLD = true;
        private Stock stock;
        protected string Symbol { get; set; }
        protected string CenterName { get; set; }
        protected string style1 = "";
        protected string style2 = "";
        protected string style3 = "";
        protected string StorageServer = ConfigurationManager.AppSettings["StorageServer"] ?? "http://testcafef.vcmedia.vn/";
        protected string CeoStoragePath = ConfigurationManager.AppSettings["CeoStoragePath"] ?? "Common/CEO/";
        protected IDictionary<string, string> CeoPhotos;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Request["Tab"] ?? "") == "co-dong-lon") TabBLD = false;
            if (IsPostBack) return;
        }

        public void LoadData(Stock myStock)
        {
            if (myStock == null || myStock.CompanyProfile == null) return;
            stock = myStock;
            Symbol = stock.Symbol;
            CenterName = Utils.GetCenterFolder(stock.TradeCenterId.ToString());
            Load_CoCauSohuu();
            KhoiLuongCPNiemYetHienTai();
            Load_BanLanhdao();

        }
        private void KhoiLuongCPNiemYetHienTai()
        {
            ltrVonDieuLe.Text = String.Format("{0:#,###}", ConvertUtility.ToDouble(stock.CompanyProfile.commonInfos.TotalVolume));// totalCapital.ToString("#,###");
        }

        public void Load_CoCauSohuu()
        {
            List<MajorOwner> listShareHolder = stock.CompanyProfile.MajorOwners;
            rptBanLanhDao.DataSource = listShareHolder;
            rptBanLanhDao.DataBind();
        }
        public void Load_BanLanhdao()
        {
            KhoiLuongCPNiemYetHienTai();

            List<StockCeo> dtHoidongQuantri = stock.CompanyProfile.AssociatedCeo.FindAll(l => l.GroupID == 1);
            List<StockCeo> dtBanGiamdoc = stock.CompanyProfile.AssociatedCeo.FindAll(l => l.GroupID == 2);
            List<StockCeo> dtBanKiemsoat = stock.CompanyProfile.AssociatedCeo.FindAll(l => l.GroupID == 3);
            var ls = new List<string>();
            foreach (var ceo in stock.CompanyProfile.AssociatedCeo)
            {
                if (!ls.Contains(ceo.CeoCode)) ls.Add(ceo.CeoCode);
            }
            CeoPhotos = CeoBL.GetCeoImage(ls);
            if (dtHoidongQuantri.Count == 0)
            {
                style1 = "display:none";
            }
            else
            {
                style1 = "";
                rpHDQT.DataSource = dtHoidongQuantri;
                rpHDQT.DataBind();
            }
            if (dtBanGiamdoc.Count == 0)
            {
                style2 = "display:none";
            }
            else
            {
                style2 = "";
                rptBGD.DataSource = dtBanGiamdoc;
                rptBGD.DataBind();
            }
            if (dtBanKiemsoat.Count == 0)
            {
                style3 = "display:none";
            }
            else
            {
                style3 = "";
                rptBKS.DataSource = dtBanKiemsoat;
                rptBKS.DataBind();
            }

        }

        protected String makeLink(string code, string name)
        {
            if (name.StartsWith("Ông ")) name = name.Substring(4);
            else if (name.StartsWith("Bà ")) name = name.Substring(3);
            string value = "/ceo/" + code + "/" + CafeF.Redis.BO.UnicodeUtility.UnicodeToKoDauAndGach(name) + ".chn?cs=" + Symbol;
            return value;
        }

        protected String convertAge(string age)
        {
            string strReturn = "";
            if (!age.Equals("0"))
            {
                try
                {
                    int i = DateTime.Now.Year - int.Parse(age);
                    strReturn = i.ToString();
                }
                catch (Exception) { }
            }
            return strReturn;
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