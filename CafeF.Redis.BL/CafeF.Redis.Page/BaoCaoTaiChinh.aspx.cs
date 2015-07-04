using System;
using System.Collections;
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
using CafeF.BCTC.DA.BO.BaoCaoTaiChinh;
using System.Text;
using CafeF.Redis.Entity;
using CafeF.Redis.BL;
using CafeF.BCTC.BO.Utilitis;

namespace CafeF.Redis.Page
{
    public partial class BaoCaoTaiChinh : System.Web.UI.Page
    {
        protected string symbol = "";
        private string type = "";
        private int year = 0;
        private int quarter = 0;
        private string showtype = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                symbol = Request["symbol"] ?? "";
                symbol = Server.UrlDecode(symbol);
                symbol = symbol.Replace(",", "");
                type = Request["type"] ?? "BSheet";
                if (string.IsNullOrEmpty(symbol)) return;
                var stock = StockBL.getStockBySymbol(symbol);
                if (stock == null) return;
                if (!int.TryParse(Request["year"] ?? "", out year) && stock.CompanyProfile.FinancePeriods.Count>0) year = stock.CompanyProfile.FinancePeriods[0].Year;
                if (!int.TryParse(Request["quarter"] ?? "", out quarter) && stock.CompanyProfile.FinancePeriods.Count>0) quarter = stock.CompanyProfile.FinancePeriods[0].Quarter;
                ltrTitle.Text = GetNameType(type) + " / " + stock.CompanyProfile.basicInfos.Name + " (" + GetNameCenter(stock.TradeCenterId.ToString()) + ")";
                showtype = Request["showtype"] ?? "0";
                txtKeyword.Text = symbol;
                //LoadData();
                this.BindData(symbol, type, year, quarter, showtype);
            }
        }

        private void BindData(string _symbol, string _type, int _year, int _quarter, string _showtype)
        {
            if (_quarter == 0)
            {
                DataTable dt = this.GenTable(_symbol, _type, _year, _quarter);//BCTCBO.BCTCCacheSql.sp_CafeF_DataCrawler_GetFinanceValue_Recent4year(_symbol, _type, _year, _showtype);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ltrLabel1.Text = dt.Columns[4].ColumnName;
                    ltrLabel2.Text = dt.Columns[5].ColumnName;
                    ltrLabel3.Text = dt.Columns[6].ColumnName;
                    ltrLabel4.Text = dt.Columns[7].ColumnName;
                    string paTemp = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        paTemp += "'" + dt.Rows[i]["MappingCode"].ToString() + "',";
                    }
                    ltrScriptParam.Text = "<script type='text/javascript'>var list = [" + paTemp.TrimEnd(',') + "]</script>";
                    rpData.DataSource = dt;
                    rpData.DataBind();
                }
            }
            else
            {
                DataTable dt = this.GenTable(_symbol, _type, _year, _quarter);//BCTCBO.BCTCCacheSql.sp_CafeF_DataCrawler_GetFinanceValue_Recent4part(_symbol, _type, _year, _quarter, _showtype);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ltrLabel1.Text = "Quý " + dt.Columns[4].ColumnName;
                    ltrLabel2.Text = "Quý " + dt.Columns[5].ColumnName;
                    ltrLabel3.Text = "Quý " + dt.Columns[6].ColumnName;
                    ltrLabel4.Text = "Quý " + dt.Columns[7].ColumnName;
                    string paTemp = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        paTemp += "'" + dt.Rows[i]["MappingCode"].ToString() + "',";
                    }
                    ltrScriptParam.Text = "<script type='text/javascript'>var list = [" + paTemp.TrimEnd(',') + "]</script>";
                    rpData.DataSource = dt;
                    rpData.DataBind();
                }
            }
        }

        private void LoadData()
        {
            symbol = symbol.Trim().ToUpper();
            string name = "";
            var center = "";
            var bShowTradeCenter = false;
            try
            {
                Stock stock = StockBL.getStockBySymbol(symbol);
                name = stock.CompanyProfile.basicInfos.Name;
                center = stock.CompanyProfile.basicInfos.TradeCenter;
                bShowTradeCenter = stock.ShowTradeCenter;
            }
            catch (Exception)
            {
                var compact = StockBL.GetStockCompactInfo(symbol);
                if (compact != null)
                {
                    name = compact.CompanyName;
                    center = compact.TradeCenterId.ToString();
                    bShowTradeCenter = compact.ShowTradeCenter;
                }
            }

            ltrTitle.Text = GetNameType(type) + " / " + name + " (" + GetNameCenter(center) + ")";

            return;
        }

        protected string GetNameCenter(string value)
        {
            string strResult = string.Empty;
            switch (value)
            {
                case "1":
                    strResult = "HOSE";
                    break;
                case "2":
                    strResult = "HNX";
                    break;
                case "8":
                    strResult = "OTC";
                    break;
                case "9":
                    strResult = "UpCOM";
                    break;
            }
            return strResult;
        }

        protected string GetNameType(string value)
        {
            string strResult = string.Empty;
            switch (value)
            {
                case "BSheet":
                    strResult = "Báo cáo tài chính";
                    break;
                case "IncSta":
                    strResult = "Kết quả hoạt động kinh doanh";
                    break;
                case "CashFlow":
                    strResult = "Lưu chuyển tiền tệ gián tiếp";
                    break;
                case "CashFlowDirect":
                    strResult = "Lưu chuyển tiền tệ trực tiếp";
                    break;
            }
            return strResult;
        }

        protected String ToDis(string par)
        {
            string toret = "";
            double d = double.Parse(par);
            if (d != -1 && d != 0 && d != -2)
            {
                toret = String.Format("{0:0,0}", d);
            }
            return toret;

        }

        private DataTable GenTable(string symbol, string type, int year, int quarter)
        {
            var bctc = BCTCBL.GetTopBCTC(symbol, type, year, quarter, 4);
            DataTable dtData = new DataTable();
            if (bctc.Count == 4)
            {
                int mapcode = 0;
                string _style = "";
                string _exco = "";
                string _imgexco = "";
                double maxValue = 0;
                double minValue = 0;
                string mc = "";
                dtData.Columns.Add("Symbol");
                dtData.Columns.Add("MappingName");
                dtData.Columns.Add("MappingCode");
                dtData.Columns.Add("OrderCode");
                if (quarter == 0)
                {
                    for (int i = 0; i < bctc.Count; i++)
                        dtData.Columns.Add(bctc[i].Year.ToString());
                }
                else
                {
                    for (int i = 0; i < bctc.Count; i++)
                        dtData.Columns.Add(bctc[i].Quarter.ToString() + '-' + bctc[i].Year.ToString());
                }
                dtData.Columns.Add("Chart");
                dtData.Columns.Add("SStyle");
                dtData.Columns.Add("ForEC");
                dtData.Columns.Add("ImgEC");
                for (int j = 0; j < bctc[0].Values.Count; j++)
                {
                    DataRow row = dtData.NewRow();
                    row["Symbol"] = bctc[0].Symbol;
                    row["MappingName"] = bctc[0].Values[j].Name;
                    row["MappingCode"] = bctc[0].Values[j].Code;
                    row["OrderCode"] = bctc[0].Values[j].Code;
                    row[4] = bctc[0].Values[j].Value;
                    row[5] = bctc[1].Values[j].Value;
                    row[6] = bctc[2].Values[j].Value;
                    row[7] = bctc[3].Values[j].Value;
                    StringBuilder chart = new StringBuilder();
                    maxValue = bctc[0].Values[j].Value;
                    if (bctc[1].Values[j].Value > maxValue) maxValue = bctc[1].Values[j].Value;
                    if (bctc[2].Values[j].Value > maxValue) maxValue = bctc[2].Values[j].Value;
                    if (bctc[3].Values[j].Value > maxValue) maxValue = bctc[3].Values[j].Value;
                    minValue = bctc[0].Values[j].Value;
                    if (bctc[1].Values[j].Value < minValue) minValue = bctc[1].Values[j].Value;
                    if (bctc[2].Values[j].Value < minValue) minValue = bctc[2].Values[j].Value;
                    if (bctc[3].Values[j].Value < minValue) minValue = bctc[3].Values[j].Value;
                    if (maxValue == -1 || maxValue == -2) { maxValue = 0; }
                    if (minValue == -1 || minValue == -2) { minValue = 0; }

                    chart.Append("<center><table class='BaoCaoTaiChinh_Chart' border='0' cellpadding='0' cellspacing='0' style='margin-top:3px;'><tr>");
                    if (bctc[0].Values[j].Value != -1 && bctc[0].Values[j].Value != -2)
                    {
                        chart.Append(Common.GenerateChart( bctc[0].Values[j].Value, maxValue, minValue));
                    }
                    chart.Append("<td><img alt='' style='width: 3px;' src='http://cafef3.vcmedia.vn/images/images/spacer.gif'/></td>");
                    if (bctc[1].Values[j].Value != -1 &&  bctc[1].Values[j].Value != -2)
                    {
                        chart.Append(Common.GenerateChart(bctc[1].Values[j].Value, maxValue, minValue));
                    }
                    chart.Append("<td><img alt='' style='width: 3px;' src='http://cafef3.vcmedia.vn/images/images/spacer.gif'/></td>");
                    if (bctc[2].Values[j].Value != -1 && bctc[2].Values[j].Value != -2)
                    {
                        chart.Append(Common.GenerateChart(bctc[2].Values[j].Value, maxValue, minValue));
                    }
                    chart.Append("<td><img alt='' style='width: 3px;' src='http://cafef3.vcmedia.vn/images/images/spacer.gif'/></td>");
                    if (bctc[3].Values[j].Value != -1 && bctc[3].Values[j].Value != -2)
                    {
                        chart.Append(Common.GenerateChart(bctc[3].Values[j].Value, maxValue, minValue));
                    }
                    chart.Append("<tr></table></center>");
                    row["Chart"] = chart;
                    try
                    {
                        mapcode = int.Parse(row["MappingCode"].ToString());
                    }
                    catch (Exception) { }
                    if (type.Equals("BSheet"))
                    {
                        if (mapcode % 100 == 0)
                        {
                            _style = "color:#333333;font-weight:bold;font-size:13px";
                            _exco = "";
                            _imgexco = "&nbsp;&nbsp;&nbsp;&nbsp;";
                        }
                        else if (mapcode % 10 == 0 && mapcode % 100 != 0)
                        {
                            _style = "color:#014377;font-weight:bold;";
                            _exco = "style='cursor:pointer' onclick='expandcollapse(this);'";
                            _imgexco = "<img id='img_" + mapcode.ToString() + "' border='0' src='http://images1.cafef.vn/batdongsan/Images/media/plusi.gif' />";
                        }
                        else if (mapcode < 10)
                        {
                            _style = "color:black;font-weight:bold;font-size:13px;text-align:center;";
                            _exco = "";
                            _imgexco = "&nbsp;&nbsp;&nbsp;&nbsp;";
                        }
                        else
                        {
                            _style = "color:#014377;";
                            if (showtype.Equals("1"))
                            {
                                _exco = "";
                            }
                            else
                            {
                                _exco = "style='display:none'";
                            }
                            _imgexco = "&nbsp;&nbsp;&nbsp;&nbsp;";
                        }
                    }
                    else if (type.Equals("IncSta"))
                    {
                        mc = row["MappingCode"].ToString();
                        if (row["MappingName"].ToString().Contains("-"))
                        {
                            //_exco = "style='display:none'";
                            _style = "color:#014377;font-style:italic;";
                            //_imgexco = "&nbsp;&nbsp;&nbsp;&nbsp;";
                        }
                        else if (mc.Equals("01") || mc.Equals("02") || mc.Equals("10") || mc.Equals("20") || mc.Equals("30") || mc.Equals("40") || mc.Equals("50") || mc.Equals("60") || mc.Equals("62"))
                        {
                            _style = "color:#014377;font-weight:bold;";
                            //_exco = "style='cursor:pointer' onclick='expandcollapse(this);'";
                            //_imgexco = "<img id='img_"+mc+"' border='0' src='http://images1.cafef.vn/batdongsan/Images/media/plusi.gif' />";
                        }
                        else
                        {
                            //_exco = "style='display:none'";
                            _style = "color:#014377;";
                            //_imgexco = "&nbsp;&nbsp;&nbsp;&nbsp;";
                        }
                    }
                    else if (type.Equals("CashFlow") || type.Equals("CashFlowDirect"))
                    {
                        mc = row["MappingCode"].ToString();
                        if (mc.Equals("001") || mc.Equals("002") || mc.Equals("003") || mc.Equals("010") || mc.Equals("110") || mc.Equals("210") || mc.Equals("310"))
                        {
                            _style = "color:black;font-weight:bold;font-size:13px";
                        }
                        else
                        {
                            _style = "color:#014377;";
                        }
                    }
                    row["SStyle"] = _style;
                    row["ForEC"] = _exco;
                    row["ImgEC"] = _imgexco;
                    dtData.Rows.Add(row);
                }
                
            }
            return dtData;
        }
    }
}
