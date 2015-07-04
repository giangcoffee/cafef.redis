using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CafeF.Redis.BO;
using CafeF.Redis.Entity;
using CafeF.Redis.BL;
using CafeF.Redis.Page;
namespace CafeF.Redis.Page.UserControl.LichSuKien
{
    public partial class NewsContent : System.Web.UI.UserControl
    {
        private long __NewsId = 0;
        private string __CurrentStockSymbol = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            __NewsId = Request.QueryString["NewsId"] == null ? 0 : ConvertUtility.ToLong(Request.QueryString["NewsId"].ToString());
            __CurrentStockSymbol = Request.QueryString["symbol"] != null ? Request.QueryString["symbol"] : "";
            if (__NewsId == 0) return;
            if (!IsPostBack)
            {
                LoadData();
            }
        }
        private void LoadData()
        {
            img.Visible = false;
            StockNews __tbl = StockHistoryBL.get_StockNewsByID(__NewsId);
            if (__tbl != null && double.Parse(__tbl.DateDeploy.ToString("yyyyMMddHHmmss")) <= double.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")))
            {
                string __symbol = __tbl.Symbol;
                if (__CurrentStockSymbol != "")
                {
                    __symbol = __CurrentStockSymbol;
                }
                else
                {
                    __symbol = __symbol.Replace("&&", "&");
                    if ((__symbol.Split('&')).Length > 3) __symbol = "";
                }
                //ThongTinCongTy1.StockSymbol = __row["StockSymbols"].ToString().Replace("&", "");
                //OtherCrawlerNews1.StockSymbol = __symbol.Replace("&",""); 
                lblNgay.Text = NewsHelper.GetDateVN(Convert.ToDateTime(__tbl.DateDeploy));
                lblTitle.Text = __tbl.Title == null ? "" : __tbl.Title.ToString();
                if (__tbl.Body != null)
                {
                    LiteralControl ltr;
                    ltr = new LiteralControl(__tbl.Body);
                    pldContent.Controls.Clear();
                    pldContent.Controls.Add(ltr);
                }
                initContent.Text = __tbl.Sapo != null ? __tbl.Sapo.ToString() : "";
                if (__tbl.Image != null)
                {
                    if (__tbl.Image.ToString() != "")
                    {
                        string img1 = ConfigurationManager.AppSettings["imageUploaded"].ToString();
                        img.ImageUrl = img1 + __tbl.Image.ToString();
                        img.Visible = true;
                    }
                }
                else
                    img.Visible = false;

                string symbol = __symbol.Replace("&", "");

                // Lấy dữ liệu giá realtime, neu khong phai la tin lien quan den nhieu ma
                string companyName = "";
                __CurrentStockSymbol = __CurrentStockSymbol.ToUpper();
                if (__CurrentStockSymbol != "")
                {
                    StockPrice __price = GetStockPrice(__CurrentStockSymbol);
                    if (__price != null)
                    {
                        if (Convert.ToDouble(__price.Price) == 0)
                        {
                            lblPrice.Text = Convert.ToDouble(__price.OpenPrice).ToString("#,##0.0#");
                        }
                        else
                        {
                            lblPrice.Text = Convert.ToDouble(__price.Price).ToString("#,##0.0#");
                        }
                        double dChange = ConvertUtility.ToDouble(__price.Price) - ConvertUtility.ToDouble(__price.RefPrice);
                        if (dChange > 0)
                        {
                            imgChange.ImageUrl = "http://cafef3.vcmedia.vn/images/btup.gif";
                            lblChange.CssClass = "Index_Up";
                            lblChange.Text = "+" + Convert.ToDouble(dChange).ToString("#,##0.0#") + "(+" + Convert.ToDouble(100 * (__price.RefPrice == 0 ? 1 : dChange / __price.RefPrice)).ToString("#,##0.0#") + "%)";
                        }
                        else if (dChange < 0)
                        {
                            imgChange.ImageUrl = "http://cafef3.vcmedia.vn/images/btdown.gif";
                            lblChange.CssClass = "Index_Down";
                            lblChange.Text = Convert.ToDouble(dChange).ToString("#,##0.0#") + "(" + Convert.ToDouble(100 * (__price.RefPrice == 0 ? 1 : dChange / __price.RefPrice)).ToString("#,##0.0#") + "%)";
                        }
                        else
                        {
                            imgChange.ImageUrl = "http://cafef3.vcmedia.vn/images/no_change.jpg";
                            lblChange.CssClass = "Index_NoChange";
                            lblChange.Text = "0(0)";
                        }
                        divThiTruong.Visible = true;
                    }
                    else
                    {
                        divThiTruong.Visible = false;
                    }
                    lblSymbol.Text = __CurrentStockSymbol;
                    //DataTable __tblCompany = KenhFHelper.GetCompanyProfile(__CurrentStockSymbol);
                    //if (__tblCompany != null)
                    //{
                    //    if (__tblCompany.Rows.Count > 0)
                    //    {
                    //        companyName = __tblCompany.Rows[0]["Fullname"].ToString();

                    //        lnkCompany.NavigateUrl = "/" + __tblCompany.Rows[0]["Symbol"].ToString().ToLower() + "/" + __tblCompany.Rows[0]["StockSymbol"].ToString()+"-"+NewsHepler_Update.UnicodeToKoDauAndGach(companyName) + ".chn";
                    //        lnkTangtruong.NavigateUrl = "/" + __tblCompany.Rows[0]["Symbol"].ToString().ToLower() + "/" + __tblCompany.Rows[0]["StockSymbol"].ToString() + "/thong-tin-tai-chinh.chn";
                    //        lnkBieudo.NavigateUrl = CompanyHelper_Update.GetCompanyInfoLink(__tblCompany.Rows[0]["StockSymbol"].ToString()) + "#Hoso";
                    //    }
                    //}
                    //__tblCompany.Dispose();
                }
                else
                {
                    companyName = "";
                }

                //imgChart.ImageUrl = "http://cafef.vn/FinanceStatementData/" + __CurrentStockSymbol + "/1month.png?upd=" + DateTime.Now.ToString("ddMMyyyyHHmmss");
                var stock = StockBL.GetStockCompactInfo(__CurrentStockSymbol);
                if (stock != null)
                {
                    imgChart.ImageUrl = string.Format("http://cafef4.vcmedia.vn/{0}/{1}/6months.png", stock.FolderChart, stock.Symbol);
                    lnkCompany.NavigateUrl = Utils.GetSymbolLink(stock.Symbol, stock.CompanyName, stock.TradeCenterId.ToString());
                }
                this.Page.Title = lblTitle.Text + " | " + GetConfigName(__tbl.TypeID ?? "0") + "CafeF.vn";
                //AddMetaTag("Keywords", string.Format("{0}, {1}, hồ sơ công ty, thông tin giao dịch", symbol, companyName));
                AddMetaTag("Description", "Tin tức-sự kiện liên quan đến công ty " + companyName + " - " + lblTitle.Text);


                //NewsHepler_Update.Set_Page_Header(Page, __tbl.Title.ToString() + " | CafeF.vn", "Tin tức-sự kiện liên quan đến công ty " + companyName + " - " + __tbl.Title.ToString(), "");
                ltrCounter.Text = @"<input type='hidden' id='Log_AssignValue_NewsID' value='" + __NewsId.ToString() + @"' />
                                        <input type='hidden' id='Log_AssignValue_NewsTitle' value='" + lblTitle.Text + @"' />
                                        <input type='hidden' id='Log_AssignValue_CatID' value='1004' />
                                        <input type='hidden' id='Log_AssignValue_CatTitle' value='ChiTietCongTy' />";

            }
        }
        private string GetConfigName(string configId)
        {
            var ret = "";
            if (configId.Contains("1"))
            {
                ret = "Tình hình SXKD - Phân tích khác";
            }
            else if (configId.Contains("2"))
            {
                ret = "Trả cổ tức - Chốt quyền";
            }
            else if (configId.Contains("3"))
            {
                ret = "Thay đổi nhân sự";
            }
            else if (configId.Contains("4"))
            {
                ret = "Tăng vốn - Cổ phiếu quỹ";
            }
            else if (configId.Contains("5"))
            {
                ret = "GD cổ đông lớn - Cổ đông nội bộ";
            }
            else
            {
                ret = "Tin tức doanh nghiệp niêm yết";
            }
            if (ret != "") ret = ret + " | ";
            return ret;

        }
        private void AddMetaTag(string name, string value)
        {
            var head = Page.Header;
            foreach (Control ctrl in head.Controls)
            {
                if (ctrl.GetType() != typeof(HtmlMeta)) continue;
                if (((HtmlMeta)ctrl).Name.ToUpper() != name.ToUpper()) continue;
                ((HtmlMeta)ctrl).Content = value;
                return;
            }
            head.Controls.Add(new HtmlMeta { Name = name, Content = value });
        }
        private StockPrice GetStockPrice(string symbol)
        {
            StockPrice pr = StockBL.getStockPriceBySymbol(symbol);
            return pr;
        }
    }
}