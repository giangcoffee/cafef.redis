using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using CafeF.Redis.Entity;
using CafeF.Redis.BL;
using CafeF.Redis.Page;
namespace CafeF.Redis.Page.UserControl.StockView
{
    public partial class FinanceStatement : System.Web.UI.UserControl
    {
        private Stock stock;
        private const int pagecount = 4;
        private int invert = 0;
        private int _year = 0, _quarter = 0;
        protected string CenterName { get; set; }
        protected string Symbol { get; set; }        

        public int TotalItemInvert
        {
            get
            {
                if (stock == null || stock.CompanyProfile == null || stock.CompanyProfile.FinancePeriods == null) return 0;
                return stock.CompanyProfile.FinancePeriods.Count - TotalItem;
            }
        }


        public int TotalItem
        {
            get
            {
                if (stock == null || stock.CompanyProfile == null || stock.CompanyProfile.FinancePeriods==null) return 0;
                if (ConvertUtility.ToInt32(txtType.Value) == 2)
                {
                    return stock.CompanyProfile.FinancePeriods.FindAll(pf => pf.Quarter == 5).Count;
                }
                return stock.CompanyProfile.FinancePeriods.FindAll(pf => pf.Quarter != 5).Count;
            }
        }

        public string StockSymbol
        {
            get
            {
                return stock == null ? "" : stock.Symbol;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }

        public void LoadData(Stock mystock)
        {
            if (mystock == null || mystock.CompanyProfile == null || mystock.CompanyProfile.FinancePeriods == null || mystock.CompanyProfile.financeInfos == null || mystock.CompanyProfile.FinancePeriods.Count == 0) return;
            stock = mystock;
            CenterName = Utils.GetCenterFolder(stock.TradeCenterId.ToString());
            Symbol = stock.Symbol;
            txtType.Value = stock.CompanyProfile.FinancePeriods[0].Quarter == 5 ? "2" : "1";
            BindData();
            BindRepeater();
        }
        private void BindData()
        {
            int idx = ConvertUtility.ToInt32(txtIdx.Value);
            List<FinancePeriod> ListItem = new List<FinancePeriod>();
            
            if (ConvertUtility.ToInt32(txtType.Value) == 2)
                ListItem = stock.CompanyProfile.FinancePeriods.FindAll(pf => pf.Quarter == 5);
            else
            {
                ListItem = stock.CompanyProfile.FinancePeriods.FindAll(pf => pf.Quarter != 5);
                //if (ListItem.Count == 0) { ListItem = stock.CompanyProfile.FinancePeriods.FindAll(delegate(FinancePeriod pf) { return pf.Quarter == 5; }); }
            }
            _year = ListItem[0].Year;
            _quarter = ListItem[0].Quarter;
            ListItem.Sort("Year desc, Quarter desc");

            if (pagecount * idx <= ListItem.Count - 1)
                ltrCol1.Text = ListItem[pagecount * idx].Title;
            else
                ltrCol1.Text = "&nbsp;";
            if (pagecount * idx + 1 <= ListItem.Count - 1)
                ltrCol2.Text = ListItem[pagecount * idx + 1].Title;
            else
                ltrCol2.Text = "&nbsp;";
            if (pagecount * idx + 2 <= ListItem.Count - 1)
                ltrCol3.Text = ListItem[pagecount * idx + 2].Title;
            else
                ltrCol3.Text = "&nbsp;";
            if (pagecount * idx + 3 <= ListItem.Count - 1)
                ltrCol4.Text = ListItem[pagecount * idx + 3].Title;
            else
                ltrCol4.Text = "&nbsp;";

            BindRepeater();
            SetImage();
        }

        private void SetImage()
        {
            imgNext.Src = "http://cafef3.vcmedia.vn/images/Scontrols/Images/LSG/Next_Red.gif";
            imgPre.Src = "http://cafef3.vcmedia.vn/images/Scontrols/Images/LSG/Previous_Red.gif";
            if (TotalItem > 0)
            {
                if ((ConvertUtility.ToInt32(txtIdx.Value) + 1) * 4 > TotalItem)
                {
                    imgPre.Src = "http://cafef3.vcmedia.vn/images/Scontrols/Images/LSG/Previous_Black.gif";
                }
                if (ConvertUtility.ToInt32(txtIdx.Value) == 0)
                {
                    imgNext.Src = "http://cafef3.vcmedia.vn/images/Scontrols/Images/LSG/Next_Black.gif";
                }
            }
            else
            {
                imgNext.Src = "http://cafef3.vcmedia.vn/images/Scontrols/Images/LSG/Next_Black.gif";
                imgPre.Src = "http://cafef3.vcmedia.vn/images/Scontrols/Images/LSG/Previous_Black.gif";
            }
        }

        private void BindRepeater()
        {
            rptNhomChiTieu.DataSource = stock.CompanyProfile.financeInfos;
            rptNhomChiTieu.DataBind();

        }

        //protected void btnAjax_Click(object sender, EventArgs e)
        //{
        //    BindData();
        //    if (ConvertUtility.ToInt32(txtType.Value) == 2)
        //    {
        //        idTabTaiChinhNam.Attributes.Add("class", "active");
        //        idTabTaiChinhQuy.Attributes.Remove("class");
        //    }
        //    else
        //    {
        //        idTabTaiChinhQuy.Attributes.Add("class", "active");
        //        idTabTaiChinhNam.Attributes.Remove("class");
        //    }
        //}

        protected void rptNhomChiTieu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Literal ltrCol4 = e.Item.FindControl("ltrCol4") as Literal;
                Literal ltrCol3 = e.Item.FindControl("ltrCol3") as Literal;
                Literal ltrCol2 = e.Item.FindControl("ltrCol2") as Literal;
                Literal ltrCol1 = e.Item.FindControl("ltrCol1") as Literal;
                Repeater rptData = e.Item.FindControl("rptData") as Repeater;
                FinanceInfo info = (FinanceInfo)e.Item.DataItem;

                rptData.ItemDataBound += new RepeaterItemEventHandler(rptData_ItemDataBound);
                rptData.DataSource = info.ChiTieus;
                rptData.DataBind();

                var i = 0;
                foreach (RepeaterItem item in rptData.Items)
                {
                    if (!item.Visible) continue;
                    var tr = item.FindControl("TrData") as HtmlControl;
                    if (tr == null) continue;
                    if (i % 2 == 1) { tr.Attributes["style"] += "background-color: #F6F6F6;"; }
                    i++;
                }
            }
        }

        void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Literal ltrChart = e.Item.FindControl("ltrChart") as Literal;
                Literal ltrCol1 = e.Item.FindControl("ltrCol1") as Literal;
                Literal ltrCol2 = e.Item.FindControl("ltrCol2") as Literal;
                Literal ltrCol3 = e.Item.FindControl("ltrCol3") as Literal;
                Literal ltrCol4 = e.Item.FindControl("ltrCol4") as Literal;

                double col1 = 0;
                double col2 = 0;
                double col3 = 0;
                double col4 = 0;

                FinanceChiTieu chitieu = (FinanceChiTieu)e.Item.DataItem;

                int idx = ConvertUtility.ToInt32(txtIdx.Value);
                List<FinanceValue> ListItem = chitieu.Values;
                if (ConvertUtility.ToInt32(txtType.Value) == 2)
                    ListItem = ListItem.FindAll(delegate(FinanceValue pf) { return pf.Quarter == 5; });
                else
                {
                    ListItem = ListItem.FindAll(delegate(FinanceValue pf) { return pf.Quarter != 5; });
                    if (ListItem.Count == 0) ListItem = chitieu.Values;
                }
                ListItem.Sort("Year desc, Quarter desc");
                string strFormat = "{0:#,###}";
                if (chitieu.TenChiTieu.ToString().ToLower() == "gtmotdvquy")
                {
                    strFormat = "{0:#,##0.#0}";
                }

                if (chitieu.TenChiTieu.ToString().ToLower() == "roa" || chitieu.TenChiTieu.ToString().ToLower() == "roe")
                {
                    strFormat = "{0:#,##0.#0}%";
                }
                if (pagecount * idx <= ListItem.Count - 1)
                {
                    ltrCol1.Text = String.Format(strFormat, ListItem[pagecount * idx].Value);
                    col1 = ListItem[pagecount * idx].Value;
                }
                else
                    ltrCol1.Text = "&nbsp;";
                if (pagecount * idx + 1 <= ListItem.Count - 1)
                {
                    ltrCol2.Text = String.Format(strFormat, ListItem[pagecount * idx + 1].Value);
                    col2 = ListItem[pagecount * idx + 1].Value;
                }
                else
                    ltrCol2.Text = "&nbsp;";
                if (pagecount * idx + 2 <= ListItem.Count - 1)
                {
                    ltrCol3.Text = String.Format(strFormat, ListItem[pagecount * idx + 2].Value);
                    col3 = ListItem[pagecount * idx + 2].Value;
                }
                else
                    ltrCol3.Text = "&nbsp;";
                if (pagecount * idx + 3 <= ListItem.Count - 1)
                {
                    ltrCol4.Text = String.Format(strFormat, ListItem[pagecount * idx + 3].Value);
                    col4 = ListItem[pagecount * idx + 3].Value;
                }
                else
                    ltrCol4.Text = "&nbsp;";

                if (col1 == 0 && col2 == 0 && col3 == 0 && col4 == 0)
                {
                    e.Item.Visible = false; return;
                }

                double maxValue = col1;
                if (col2 > maxValue) maxValue = col2;
                if (col3 > maxValue) maxValue = col3;
                if (col4 > maxValue) maxValue = col4;

                double minValue = col1;
                if (col2 < minValue) minValue = col2;
                if (col3 < minValue) minValue = col3;
                if (col4 < minValue) minValue = col4;

                StringBuilder chart = new StringBuilder();

                chart.Append("<center><table class='BaoCaoTaiChinh_Chart' border='0' cellpadding='0' cellspacing='0' style='margin-top:3px;'><tr>");
                chart.Append(generateChart(col4, maxValue, minValue));
                chart.Append("<td><img alt='' style='width: 3px;' src='http://cafef3.vcmedia.vn/images/images/spacer.gif'/></td>");
                chart.Append(generateChart(col3, maxValue, minValue));
                chart.Append("<td><img alt='' style='width: 3px;' src='http://cafef3.vcmedia.vn/images/images/spacer.gif'/></td>");
                chart.Append(generateChart(col2, maxValue, minValue));
                chart.Append("<td><img alt='' style='width: 3px;' src='http://cafef3.vcmedia.vn/images/images/spacer.gif'/></td>");
                chart.Append(generateChart(col1, maxValue, minValue));
                chart.Append("<tr></table></center>");

                ltrChart.Text = chart.ToString();
            }
        }

        private string generateChart(double value, double maxValue, double minValue)
        {
            int maxHeight = 24;
            int width = 4;
            double totalValue = 0;
            if (maxValue > 0 && minValue > 0)
            {
                totalValue = maxValue;
            }
            else if (maxValue < 0 && minValue < 0)
            {
                totalValue = -minValue;
            }
            else
            {
                totalValue = (maxValue < 0 ? -maxValue : maxValue) + (minValue < 0 ? -minValue : minValue);
            }

            int max_height_up = (int)(((maxValue > 0 ? maxValue : 0) / totalValue) * maxHeight);
            int max_height_down = (int)(((minValue < 0 ? -minValue : 0) / totalValue) * maxHeight);
            int height = 0;

            StringBuilder chart = new StringBuilder();

            if (value > 0)
            {
                height = (int)((value / maxValue) * max_height_up);
                chart.Append("<td valign='bottom'>");
                chart.Append("<div style='overflow: hidden; background-color: #a4ccf0'><img alt='' style='width: " + width.ToString() + "px;height: " + height.ToString() + "px' src='http://cafef3.vcmedia.vn/images/images/spacer.gif' /></div>");
                chart.Append("<div style='overflow: hidden; background-color: #5493be'><img alt='' style='width: " + width.ToString() + "px;height: 1px' src='http://cafef3.vcmedia.vn/images/images/spacer.gif' /></div>");
                if (max_height_down > 0)
                {
                    chart.Append("<div style='overflow: hidden;'><img alt='' style='width: " + width.ToString() + "px;height: " + max_height_down.ToString() + "px' src='http://cafef3.vcmedia.vn/images/images/spacer.gif' /></div>");
                }
                chart.Append("</td>");
            }
            else if (value < 0)
            {
                height = (int)((value / minValue) * max_height_down);
                chart.Append("<td valign='top'>");
                if (max_height_up > 0)
                {
                    chart.Append("<div style='overflow: hidden;'><img alt='' style='width: " + width.ToString() + "px;height: " + max_height_up.ToString() + "px' src='http://cafef3.vcmedia.vn/images/images/spacer.gif' /></div>");
                }
                chart.Append("<div style='overflow: hidden; background-color: #7f0102'><img alt='' style='width: " + width.ToString() + "px;height: 1px' src='http://cafef3.vcmedia.vn/images/images/spacer.gif' /></div>");
                chart.Append("<div style='overflow: hidden; background-color: #cc0001'><img alt='' style='width: " + width.ToString() + "px;height: " + height.ToString() + "px' src='http://cafef3.vcmedia.vn/images/images/spacer.gif' /></div>");
                chart.Append("</td>");
            }
            else
            {
                chart.Append("<td>");
                if (max_height_up > 0)
                {
                    chart.Append("<div style='overflow: hidden;'><img alt='' style='width: " + width.ToString() + "px;height: " + max_height_up.ToString() + "px' src='http://cafef3.vcmedia.vn/images/images/spacer.gif' /></div>");
                }
                chart.Append("<div style='overflow: hidden; background-color: #5493be'><img alt='' style='width: " + width.ToString() + "px;height: 1px' src='http://cafef3.vcmedia.vn/images/images/spacer.gif' /></div>");
                if (max_height_down > 0)
                {
                    chart.Append("<div style='overflow: hidden;'><img alt='' style='width: " + width.ToString() + "px;height: " + max_height_down.ToString() + "px' src='http://cafef3.vcmedia.vn/images/images/spacer.gif' /></div>");
                }
                chart.Append("</td>");
            }
            return chart.ToString();
        }

        protected String ReturnHref(string nhomchitieu)
        {
            string href = "";
            string type = "BSheet";
            nhomchitieu = (nhomchitieu == "0" ? "2" : nhomchitieu);
            if (nhomchitieu.Equals("2"))
            {
                type = "IncSta";
            }
            var allowV2 = (ConfigurationManager.AppSettings["AllowBCTCV2"] ?? "") == "TRUE";
            if (allowV2)
            {
                href = "/BaoCaoTaiChinh.aspx?symbol=" + Symbol + "&type="+type+"&year=" + _year + "&quarter=" + _quarter;
            }
            else
            {
                href = "/BCTCFull/BCTCFull.aspx?symbol="+Symbol+"&nhom="+nhomchitieu+"&type=1&quy=0";
            }
            return href;
        }
    }
}