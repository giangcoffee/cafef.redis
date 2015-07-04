using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CafeF.Redis.BL;
using CafeF.Redis.Entity;

namespace CafeF.Redis.Page.UserControl.StockView
{
    public partial class ucBanLanhDao : System.Web.UI.UserControl
    {
        protected bool TabBLD = true;
        private Stock stock;
        protected string Symbol { get; set; }
        protected string CenterName { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Request["Tab"] ?? "") == "co-dong-lon") TabBLD = false;
            if (IsPostBack) return;
        }

        public void LoadData(Stock myStock)
        {
            if(myStock==null || myStock.CompanyProfile==null) return;
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

            var strBanLanhdao = new StringBuilder();
            strBanLanhdao.Append("<table border='0' cellpadding='3' cellspacing='1' class='CafeF_BanLanhDao' style='width: 100%;font-weight:normal'>");
            strBanLanhdao.Append("<tr><td colspan='2' style='width: 50%' class='BlockTitle'>HỘI ĐỒNG QUẢN TRỊ</td>");
            strBanLanhdao.Append("<td colspan='2' style='width: 50%' class='BlockTitle'>BAN GIÁM ĐỐC / KẾ TOÁN TRƯỞNG</td></tr>");
            strBanLanhdao.Append("<tr><td class='FieldTitle' align='left'>Họ và tên</td><td class='FieldTitle'>Chức vụ</td><td class='FieldTitle'>Họ và tên</td><td class='FieldTitle'>Chức vụ</td></tr>");

            // Group: 1:Hoi dong quan tri; 2:Ban GD/Ke toan; 3:Ban kiem soat
            if (stock.CompanyProfile.Leaders == null) return;
            List<Leader> dtHoidongQuantri = stock.CompanyProfile.Leaders.FindAll(l => l.GroupID == "1");
            List<Leader> dtBanGiamdoc = stock.CompanyProfile.Leaders.FindAll(l => l.GroupID == "2");
            if (dtBanGiamdoc.Count % 2 == 0)
            {
                dtBanGiamdoc.Add(new Leader() { GroupID = "2", Name = "", Positions = "" });
            }

            List<Leader> dtBanKiemsoat = stock.CompanyProfile.Leaders.FindAll(l => l.GroupID == "3");

            bool isAlternate = false;
            int BanKiemsoat_GenerateStep = 0;

            int count = dtHoidongQuantri.Count;

            if (count < dtBanGiamdoc.Count + dtBanKiemsoat.Count + 2)
            {
                count = dtBanGiamdoc.Count + dtBanKiemsoat.Count + 2;
            }

            // (dtBanGiamdoc.Rows.Count + dtBanKiemsoat.Rows.Count + 2) la 1 row la title cua block BAN KIEM SOAT va 1 row cho FieldTitle cua block nay
            for (int i = 0; i < count; i++)
            {
                strBanLanhdao.Append("<tr" + (isAlternate ? " class='alt'" : "") + ">");
                // Ben trai
                if (i < dtHoidongQuantri.Count)
                {
                    if (dtHoidongQuantri.Count > 0)
                    {
                        strBanLanhdao.Append("<td>" + dtHoidongQuantri[i].Name.ToString() + "</td>");
                        strBanLanhdao.Append("<td>" + dtHoidongQuantri[i].Positions.ToString() + "</td>");
                    }
                }
                else
                {
                    strBanLanhdao.Append("<td>&nbsp;</td>");
                    strBanLanhdao.Append("<td>&nbsp;</td>");
                }
                // Ben phai
                if (i < dtBanGiamdoc.Count)
                {
                    if (dtBanGiamdoc.Count > 0)
                    {
                        strBanLanhdao.Append("<td>" + dtBanGiamdoc[i].Name.ToString() + "</td>");
                        strBanLanhdao.Append("<td>" + dtBanGiamdoc[i].Positions.ToString() + "</td>");
                    }
                    else
                    {
                        strBanLanhdao.Append("<td>&nbsp;</td>");
                        strBanLanhdao.Append("<td>&nbsp;</td>");
                    }
                }
                else
                {
                    if (BanKiemsoat_GenerateStep == 0) // Gen Title block Ban kiem soat
                    {
                        strBanLanhdao.Append("<td colspan='2' class='BlockTitle'>BAN KIỂM SOÁT</td>");
                        BanKiemsoat_GenerateStep = 1;
                    }
                    else if (BanKiemsoat_GenerateStep == 1) // Gen FieldTitle cua block Ban kiem soat
                    {
                        strBanLanhdao.Append("<td class='FieldTitle'>Họ và tên</td><td class='FieldTitle'>Chức vụ</td>");
                        BanKiemsoat_GenerateStep = 2;
                    }
                    else // Gen noi dung block Ban kiem soat
                    {
                        if (dtBanKiemsoat.Count > 0 && i - dtBanGiamdoc.Count - 2 < dtBanKiemsoat.Count)
                        {
                            strBanLanhdao.Append("<td>" + dtBanKiemsoat[i - dtBanGiamdoc.Count - 2].Name.ToString() + "</td>");
                            strBanLanhdao.Append("<td>" + dtBanKiemsoat[i - dtBanGiamdoc.Count - 2].Positions.ToString() + "</td>");
                        }
                        else
                        {
                            strBanLanhdao.Append("<td>&nbsp;</td>");
                            strBanLanhdao.Append("<td>&nbsp;</td>");
                        }
                    }
                }
                strBanLanhdao.Append("</tr>");

                isAlternate = !isAlternate;
            }

            strBanLanhdao.Append("</table>");

            ltrBanLanhdao.Text = strBanLanhdao.ToString();
        }
    }
}