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

namespace CafeF.Redis.Page.UserControl.StockView
{
    public partial class CompanyInfo : System.Web.UI.UserControl
    {
        private Stock stock;
        public String Symbol = "";
        protected string CenterName = "";
        protected bool HasCeo { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            GetParams();
            if(IsPostBack) return;
            LoadData();
        }

        private void GetParams()
        {
            Symbol = (Request["Symbol"] ?? "").Trim().ToUpper();
            try
            {
                stock = ((Stock)((CafeF.Redis.Page.MasterPage.StockMain)(((ContentPlaceHolder)this.Parent.Parent).Page).Master).GetStock);
            }
            catch
            {
                stock = StockBL.getStockBySymbol(Symbol) ?? null;
            }
            if(stock ==null) return;
            CenterName = Utils.GetCenterFolder(stock.TradeCenterId.ToString());
            var allowV2 = (ConfigurationManager.AppSettings["AllowCeoV2"] ?? "") == "TRUE";
            HasCeo = (allowV2 && stock.CompanyProfile.AssociatedCeo != null && stock.CompanyProfile.AssociatedCeo.Count > 0);
        }
        private void LoadData()
        {
            if (stock == null || stock.CompanyProfile==null) return;
            var currentTab = Request["tabid"] ?? "1";
            FinanceStatement1.LoadData(stock);
            ucThongTinChung1.Visible = false;
            //FinanceStatement1.Visible = false;
            ucCongTyCon1.Visible = false;
            ucBanLanhDao1.Visible = false;
            ucBanLanhDao2.Visible = false;
            ucBaoCaoTaiChinh1.Visible = false;
             
            
            switch (currentTab)
            {
                case "2":
                    ucThongTinChung1.Load_ThongTinChung(stock);
                    ucThongTinChung1.Visible = true;
                    break;
                case "3":
                    if(!HasCeo)
                    {
                        ucBanLanhDao1.LoadData(stock);
                        ucBanLanhDao1.Visible = true;
                    }
                    else
                    {
                        ucBanLanhDao2.LoadData(stock);
                        ucBanLanhDao2.Visible = true;
                    }
                    break;
                case "4":
                    ucCongTyCon1.LoadData(stock);
                    ucCongTyCon1.Visible = true;
                    break;
                case "5":
                    ucBaoCaoTaiChinh1.LoadData(stock.Symbol);
                    ucBaoCaoTaiChinh1.Visible = true;
                    break;
                default:
                    
                   // FinanceStatement1.Visible = true;
                    break;
            }
        }
    }
}