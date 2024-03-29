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
using System.Collections.Generic;
using CafeF.Redis.BO;
using CafeF.Redis.Entity;
using CafeF.Redis.BL;
using CafeF.Redis.Data;
using CafeF.Redis.Page;
namespace CafeF.Redis.Page.UserControl.Report
{
    public partial class AnalyzeReportList : System.Web.UI.UserControl
    {
        private int PageSize = 20;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDetail();
                LoadCommon();
                BindData();
                
            }
        }

        protected void BindData()
        {
            string Symbol = txtSymbol.Text.Trim();
            int SourceID = ConvertUtility.ToInt32(cboSource.SelectedValue);
            int CategoryID = ConvertUtility.ToInt32(cboCategory.SelectedValue);
            int pageIndex = (hdPageIndex.Value == null || hdPageIndex.Value == "") ? 1 : ConvertUtility.ToInt32(hdPageIndex.Value);
            if(tblDetail.Visible == false)
            {
                this.Page.Title = "Báo cáo phân tích trang " + pageIndex + " | CafeF.vn";
            }
            int totalPage = 0;
            List<Reports> tblResult = ReportsBL.get_ReportsSearching(Symbol, SourceID, CategoryID, pageIndex, PageSize, out totalPage);
            if (tblResult != null)
            {
                rptData.DataSource = tblResult;
                rptData.DataBind();
            }
            LoadPage(pageIndex, totalPage);
        }

        protected void LoadCommon()
        {
            DataTable tblSource = CompanyHelper_Update.H_AnalyzeReportSource_All();
            DataTable tblCategory = CompanyHelper_Update.H_Category_SelectParent();
            BindComboBox(cboSource, tblSource, "Source", "ID", "-1");
            BindComboBox(cboCategory, tblCategory, "Name", "ID", "");
        }
        protected void BindComboBox(DropDownList Source, DataTable tblValue, string TextField, string ValueField, string Default)
        {
            if (tblValue != null && tblValue.Rows.Count > 0)
            {
                Source.DataSource = tblValue;
                Source.DataTextField = TextField;
                Source.DataValueField = ValueField;
                Source.DataBind();
                Source.Items.Insert(0, new ListItem("Tất cả", Default));
            }
        }

        private string specificWhatIconToShow(double value)
        {
            if (value < 0) return " <img src=\"/images/LSG/down_.gif\" /><span style=\"color:#CC0000\"> " + String.Format("{0:#,##0.0}", value) + " %</span>";
            if (value > 0) return " <img src=\"/images/LSG/up_.gif\" /><span style=\"color:#00CC00\"> " + String.Format("{0:#,##0.0}", value) + " %</span>";
            return " <img src=\"/images/LSG/nochange_.jpg\" /><span style=\"color:#D0AD08\"> " + String.Format("{0:#,##0.0}", value) + " %</span>";
        }

        protected void LoadDetail()
        {
            if (Request.QueryString["id"] == null || Request.QueryString["id"].Trim() == "")
            {
                tblDetail.Visible = false;
                return;
            }
            PageSize = 10;
            int ID = 0; int.TryParse(Request.QueryString["id"], out ID);
            if (ID > 0)
            {
                Reports tblResult = ReportsBL.get_ReportsByKey(ID);
                if (tblResult != null)
                {
                    ltrTitle.Text = tblResult.Title;
                    if (tblResult.ResourceName != null)
                        ltrSource.Text = string.Format("<a href=\"{0}>{1}</a>", GetUrlSource(tblResult.ResourceLink),
                            tblResult.ResourceName.ToString() + " - " + tblResult.ResourceCode.ToString());
                    this.Page.Title = ltrTitle.Text + " | Báo cáo phân tích | CafeF.vn";
                    ltrDes.Text = tblResult.Body;
                    if (tblResult.file != null)
                    {
                        ltrDownload.Text = string.Format("<a style=\"cursor:pointer\" id=\"aBCPT1_" + ID + "\" onclick=\"DownloadBaoCao({1},'{0}',1);\" target=\"_blank\"><img alt=\"\" border=\"0\" height=\"30\" src=\"http://cafef3.vcmedia.vn/images/btnDownload.jpg\" /></a>", tblResult.file.FileName, ID);
                        ltrDetail.Text = "Dạng tệp: " + GetDownload(tblResult.file, ID) + "&nbsp;&nbsp; Ngày phát hành: " + Convert.ToDateTime(tblResult.DateDeploy).ToString("dd/MM/yyyy");
                    }

                    string symbol = "";


                    symbol = CafeF.Redis.Data.Utility.getSymbolFromString(tblResult.Symbol??"");
                    if(string.IsNullOrEmpty(symbol)) return;
                    var sk = StockBL.GetStockCompactInfo(symbol);
                    if(sk == null) return;
                    var sp = StockBL.getStockPriceBySymbol(symbol);
                    double pc = 0;
                    if (sp != null)
                        pc = ConvertUtility.ToDouble(sp.RefPrice) == 0 ? 100 : (ConvertUtility.ToDouble(sp.Price) - ConvertUtility.ToDouble(sp.RefPrice)) / ConvertUtility.ToDouble(sp.RefPrice) * 100;

                    if (sk.CompanyName != "")
                        ltrCompanyName.Text = sk.CompanyName + " - Mã CK: " + symbol + "(" + String.Format("{0:#,##0.0}", (sp==null?0:sp.Price)) + specificWhatIconToShow(pc) + ")";
                    else
                        tblTrCompany.Visible = false;

                    
                }
            }
            else
                tblDetail.Visible = false;
        }
        protected string GetUrlSource(object value)
        {
            string strResult = string.Empty;
            if (value == null || value.ToString().Trim() == "")
                strResult = "javascript:void(0)\"";
            else
                strResult = "http://" + HttpUtility.HtmlEncode(value.ToString()) + "\" target=\"_blank\" ";
            return strResult;
        }
        protected string GetDownload(object File, object ID)
        {
            FileObject fo = (FileObject)File;
            string image = string.Empty;
            if (fo == null) return "";
            string strFileName = HttpUtility.HtmlEncode(ConvertUtility.ToString(fo.FileName));
            if (strFileName.ToLower().Contains(".pdf"))
                image = "iconAdobe.jpg";
            else if (strFileName.ToLower().Contains(".doc"))
                image = "iconWord.jpg";
            else if (strFileName.ToLower().Contains(".xls"))
                image = "iconExel.jpg";
            else if (strFileName.ToLower().Contains(".ppt"))
                image = "iconPP.jpg";
            else if (strFileName.ToLower().Contains(".rar") || strFileName.ToLower().Contains(".zip"))
                image = "iconRar.jpg";
            if (string.IsNullOrEmpty(image)) return "";
            image = string.Format("<a style=\"cursor:pointer\" id=\"aBCPT_" + ID + "\" onclick=\"DownloadBaoCao({2},'{1}',0);\" target=\"_blank\"><img alt=\"\" border=\"0\" width=\"16\" src=\"http://cafef3.vcmedia.vn/images/{0}\" /></a>", image, strFileName, ID);
            return image;
        }
        protected void LoadPage(int pageIndex, int totalPage)
        {
            #region Page

            if (pageIndex <= 0) pageIndex = 1;
            if (pageIndex > totalPage) pageIndex = totalPage;
            List<ListItem> pages = new List<ListItem>();
            if (pageIndex > 3)
            {
                pages.Add(new ListItem("Đầu", "1"));
            }
            if (pageIndex > 3)
            {
                pages.Add(new ListItem("...", (pageIndex - 3).ToString()));
            }
            if (pageIndex > 2)
            {
                pages.Add(new ListItem((pageIndex - 2).ToString(), (pageIndex - 2).ToString()));
            }
            if (pageIndex > 1)
            {
                pages.Add(new ListItem((pageIndex - 1).ToString(), (pageIndex - 1).ToString()));
            }
            ListItem currentPage = new ListItem(pageIndex.ToString(), pageIndex.ToString());
            currentPage.Selected = true;
            pages.Add(currentPage);
            if (pageIndex < totalPage)
            {
                pages.Add(new ListItem((pageIndex + 1).ToString(), (pageIndex + 1).ToString()));
            }
            if (pageIndex < (totalPage - 1))
            {
                pages.Add(new ListItem((pageIndex + 2).ToString(), (pageIndex + 2).ToString()));
            }
            if (pageIndex < totalPage - 2)
            {
                pages.Add(new ListItem("...", (pageIndex + 3).ToString()));
            }

            if (pageIndex < totalPage - 2)
            {
                pages.Add(new ListItem("Cuối", totalPage.ToString()));
            }
            rptPage.DataSource = pages;
            rptPage.DataBind();

            #endregion
        }
        protected void rptPage_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Button btnPage = e.Item.FindControl("btnPage") as Button;
                ListItem page = e.Item.DataItem as ListItem;
                btnPage.CommandArgument = page.Value;
                if (page.Selected)
                {
                    btnPage.Text = page.Text;
                    hdPageIndex.Value = page.Value;
                    btnPage.CssClass = "btn_Search_Selected";
                }
                else
                {
                    btnPage.CssClass = "btn_Search_Normal";
                    if (page.Value != "-1")
                    {
                        btnPage.Text = page.Text;
                    }
                }
            }
        }

        protected void rptPage_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "paging")
            {
                hdPageIndex.Value = e.CommandArgument.ToString();
                BindData();
            }
        }

        protected void ibtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            hdPageIndex.Value = "1";
            BindData();
        }


    }
}