using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CafeF.Redis.BL;
using CafeF.Redis.Entity;

namespace CafeF.Redis.Page.UserControl.StockView
{
    public partial class ucThongTinChung : System.Web.UI.UserControl
    {
        private Stock stock;
        protected string Symbol { get; set; }
        protected string CenterName { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }
        public void Load_ThongTinChung(Stock mystock)
        {
            stock = mystock;
            if (stock == null || stock.CompanyProfile.commonInfos == null) return;
            Symbol = stock.Symbol;
            CenterName = Utils.GetCenterName(stock.TradeCenterId.ToString());
            var common = stock.CompanyProfile.commonInfos;
            ltrContent_Nhomnganh.Text = common.Category;
            ltrContent_VonDieule.Text = String.Format("{0:#,###}", common.Capital);
            ltrKLCPNiemYet.Text = String.Format("{0:#,###}", common.TotalVolume);
            ltrKLCPLuuHanh.Text = String.Format("{0:#,###}", common.OutstandingVolume);
            ltrContent_Gioithieu.Text = common.Content;
            var auditName = common.AuditFirmName??"";
            var auditSite = (common.AuditFirmSite??"").Trim();
            var consultantName = common.ConsultantName??"";
            var consultantSite = (common.ConsultantSite??"").Trim();
            var businessLicense = common.BusinessLicense??"";
            var consultantSymbol = (common.ConsultantSymbol ?? "").Trim();
            if (!string.IsNullOrEmpty(auditSite) && !auditSite.StartsWith("http://") && !auditSite.StartsWith("https://"))
            {
                auditSite = "http://" + auditSite; }
            ltrAudit.Text = string.IsNullOrEmpty(auditSite)?auditName:string.Format("<a href='{0}' target='_blank'>{1}</a>", auditSite, auditName);
            pnAudit.Visible = !String.IsNullOrEmpty(auditName);
            if (!string.IsNullOrEmpty(consultantSite) && !consultantSite.StartsWith("http://") && !consultantSite.StartsWith("https://")){
                consultantSite = "http://" + consultantSite;
            }
            ltrConsultant.Text = string.IsNullOrEmpty(consultantSite) ? consultantName : string.Format("<a href='{0}' target='_blank'>{1}</a>", consultantSite, consultantName);
            pnConsultant.Visible = !string.IsNullOrEmpty(consultantName);
            ltrBusinessLicense.Text = businessLicense;
            pnBusiness.Visible = !string.IsNullOrEmpty(businessLicense);
            pnAuditConsultant.Visible = !String.IsNullOrEmpty(auditName) || !string.IsNullOrEmpty(consultantName) || !string.IsNullOrEmpty(businessLicense);
            pnConsultantStock.Visible = false;
            if(!string.IsNullOrEmpty(consultantSymbol))
            {
                var info = StockBL.GetStockCompactInfo(consultantSymbol);
                if(info!=null)
                {
                    ltrConsultantStock.Text = string.Format("<a href='{0}' target='_blank'>{1}</a>", Utils.GetSymbolLink(info.Symbol, info.CompanyName, info.TradeCenterId.ToString()), info.Symbol);
                    pnConsultantStock.Visible = true;
                }

            }
        }

    }
}