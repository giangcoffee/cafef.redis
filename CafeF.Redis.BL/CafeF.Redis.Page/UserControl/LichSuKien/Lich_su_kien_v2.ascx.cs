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
using System.Data.SqlClient;
using CafeF.Redis.BL;
using CafeF.Redis.BO;
using CafeF.Redis.Entity;

namespace CafeF.Redis.Page.UserControl.LichSuKien
{
    public partial class Lich_su_kien_v2 : System.Web.UI.UserControl
    {
        string __danhsachma = "<script>var __arrma=new Array(";
        bool __firstItem = true;
        int __tblLength = 0;
        int PageSize = 20;
        int totalPage = 0;
        int pageIndex;
        DateTime d1 = DateTime.MinValue;
        DateTime d2 = DateTime.MinValue;
        string Symbol = "";
        bool blStatus = true; /* true : tim theo tu ngay hien tai den cac ngay tiep theo, false : tim theo ngay hien tai den ngay truoc do */
        //private Dictionary<string, StockPrice> ps;
        //private Dictionary<string, StockHistory> phs;
        
        private string ImgUrl = string.Empty;
        private string strChgIndex = string.Empty;
        //private double pctIndex = 0, chgIndex = 0;
        private const string __CompanyLinkFormat = "<a style=\"font-weight:bold; color: #003466\" target=\"_parent\" href=\"{0}\"  title=\"{1}\" >{2}</a>";
        private Dictionary<string, StockPrice> ps;
        private Dictionary<string, StockHistory> phs;


        protected void Page_Load(object sender, EventArgs e)
        {
            pageIndex = 1;
            try
            {
                pageIndex = Convert.ToInt32(Request.QueryString["page"]);
            }
            catch
            {
                pageIndex = 1;
            }
            if (pageIndex == 0) pageIndex = 1;
            //
            if (!string.IsNullOrEmpty(Request.QueryString["Symbol"]))
            {
                Symbol = Request.QueryString["Symbol"];
            }

            if (ViewState["LSKStatus"] == null)
                ViewState["LSKStatus"] = blStatus;

            if (!IsPostBack)
            {
                hdfSymbol.Value = Symbol;
                ltrScript.Text = __danhsachma;
                ltrMonth.Text = " THÁNG " + DateTime.Now.Month.ToString();
                hdfDate1.Value = dpkTradeDate1.SelectedDate.ToString();
                hdfDate2.Value = dpkTradeDate2.SelectedDate.ToString();
                BindData(blStatus);
            }
        }

        public void BindData(bool status)
        {
            //CafeF.Redis.Entity.LichSuKien l = new CafeF.Redis.Entity.LichSuKien();
            //l.ID = 1;
            //l.LoaiSuKien = 22;
            //l.MaCK = "FPT";
            //l.MaSan = 1;
            //l.News_ID = "16544";
            //l.NgayBatDau = DateTime.Now;
            //l.NgayKetThuc = "";
            //l.NgayThucHien = "";
            //l.TenCty = "O yea";
            //l.Title = "Thu phat";
            //l.TomTat = "thu phat nua";
            //List<CafeF.Redis.Entity.LichSuKien> lis = new List<CafeF.Redis.Entity.LichSuKien>();
            //lis.Add(l);

            grvLichSuKien.Columns[grvLichSuKien.Columns.Count - 1].HeaderText = (status ? "Giá hiện tại" : "Giá tại ngày GD không hưởng quyền");
            string symbol = "";
            symbol = txtKeyword.Text.Trim();
            int type = 0;
            type = int.Parse(dlType.SelectedValue);
            ltrScript.Text = __danhsachma; __firstItem = true;
            int nTotalItems = 0;
            List<CafeF.Redis.Entity.LichSuKien> tblResult = LichSuKienBL.get_LichSuKienSearching(status, symbol, type, d1, d2, pageIndex, PageSize, out nTotalItems);
            totalPage = (int) Math.Ceiling((double)nTotalItems/PageSize);
            __tblLength = tblResult.Count - 1;

            //bind info and price 
            var ss = new List<string>();
            foreach (var item in tblResult)
            {
                var s = item.MaCK.Trim().ToUpper();
                //while (s.StartsWith("&")) { s = s.Remove(0, 1); }
                //if (s.Contains("&")) s = s.Substring(0, s.IndexOf("&"));
                s = s.Replace("&", "");
                if (!ss.Contains(s)) ss.Add(s);
            }
            var ph = StockBL.GetStockCompactInfoMultiple(ss);
            for (var i = 0; i < tblResult.Count; i++)
            {
                var o = tblResult[i];
                var s = o.MaCK.Trim().ToUpper().Replace("&", "");
                if (ph.ContainsKey(s) && ph[s] != null)
                {
                    o.MaSan = ph[s].TradeCenterId;
                    o.TenCty = ph[s].CompanyName;
                    tblResult[i] = o;
                }
            }
            if (status)
            {
                ps = (Dictionary<string, StockPrice>)StockBL.GetStockPriceMultiple(ss);
            }
            else
            {
                var dates = new List<string>();
                foreach (var item in tblResult)
                {
                    var s = item.MaCK.ToString().Trim().ToUpper();
                    s = s.Replace("&", "");
                    var d = item.EventDate.ToString("yyyyMMdd");
                    if (!dates.Contains(s + "-" + d)) dates.Add(s + "-" + d);
                }
                phs = (Dictionary<string, StockHistory>)StockHistoryBL.GetStockPriceMultiple(dates);
            }

            grvLichSuKien.DataSource = tblResult;
            if (pageIndex > 0) grvLichSuKien.PageIndex = pageIndex;
            grvLichSuKien.DataBind();
            if (ltrScript.Text != "")
            {
                ltrScript.Text += ");</script>";
            }
            if (totalPage > 0)//(tblResult != null && tblResult.Count > 0)
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
            else
            {
                rptPage.DataSource = null;
                rptPage.DataBind();
            }


        }

        protected void grvLichSuKien_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grvLichSuKien_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string img = " <img src='http://cafef3.vcmedia.vn/images/{0}' align='absmiddle'>";
                string strTable = "<table style=\"border-bottom:0px;border-right:0px;border-left:0px;border-top:0px;\" border=\"0\" width=\"100%\"><tr><td style=\"border-bottom:0px;border-right:0px;border-left:0px;border-top:0px;\" border=\"0\" width=\"30px\">{0}</td><td style=\"border-bottom:0px;border-right:0px;border-left:0px;border-top:0px;\" border=\"0\" width=\"110px\">{1}</td></tr></table>";
                string urlFormat = "/{0}-{1}/{2}.chn";

                Literal ltrMaCK = e.Row.FindControl("ltrMaCK") as Literal;
                Literal ltrNgayBatDau = e.Row.FindControl("ltrNgayBatDau") as Literal;
                Literal ltrNgayKetThuc = e.Row.FindControl("ltrNgayKetThuc") as Literal;
                Literal ltrNgayThucHien = e.Row.FindControl("ltrNgayThucHien") as Literal;
                Literal ltrSuKien = e.Row.FindControl("ltrSuKien") as Literal;
                Literal ltrSan = e.Row.FindControl("ltrSan") as Literal;
                Literal ltrChange = e.Row.FindControl("ltrChange") as Literal;

                HyperLink lnkNoiDung = e.Row.FindControl("lnkNoiDung") as HyperLink;

                CafeF.Redis.Entity.LichSuKien __dr = (CafeF.Redis.Entity.LichSuKien)e.Row.DataItem;
                //DataRowView drvEvent = e.Row.DataItem as DataRowView;
                string symbol = "";

                if (null != __dr.MaCK)
                {
                    symbol = __dr.MaCK.Replace("&", "");
                }
                ltrSan.Text = "<div id=\"sp_" + symbol + "\"></div>";
                if (null != __dr.NgayBatDau)
                {
                    ltrNgayBatDau.Text = __dr.NgayBatDau;
                }
                if (null != __dr.NgayKetThuc)
                {
                    ltrNgayKetThuc.Text = __dr.NgayKetThuc;//Convert.ToDateTime(drvEvent["NgayKetThuc"]).ToString("dd/MM/yyyy");
                }
                if (null != __dr.NgayThucHien)
                {
                    ltrNgayThucHien.Text = __dr.NgayThucHien;//Convert.ToDateTime(drvEvent["NgayThucHien"]).ToString("dd/MM/yyyy");
                }

                lnkNoiDung.Text = __dr.Title;
                lnkNoiDung.ToolTip = __dr.TomTat;
                lnkNoiDung.NavigateUrl = string.Format(urlFormat, symbol, __dr.News_ID, UnicodeToKoDauAndGach(__dr.Title));
                if(__dr.MaSan==0)
                {
                    ltrMaCK.Text = symbol;
                }else{
                ltrMaCK.Text = String.Format(__CompanyLinkFormat, Utils.GetSymbolLink(symbol, __dr.TenCty, __dr.MaSan.ToString()), symbol, symbol);
                }
                ltrSan.Text = Utils.GetCenterName(__dr.MaSan.ToString());

                //StockPrice pr = StockBL.getStockPriceBySymbol(symbol);
                //try
                //{
                //    double dChange = Math.Round(pr.Price - pr.RefPrice, 2);

                //    string strChgIndex = String.Format("&nbsp; {0} ({1}%)", String.Format("{0:#,##0.0}", dChange), pr.RefPrice != 0 ? String.Format("{0:#,##0.0}", Math.Round(dChange * (double)100.0 / pr.RefPrice, 1)) : "0.0");

                //    string icon = "nochange";
                //    string color = "orange";
                //    if (dChange < 0)
                //    {
                //        icon = "down";
                //        color = "red";
                //    }
                //    if (dChange > 0)
                //    {
                //        color = "green";
                //        icon = "up";
                //    }

                //    if (pr.CeilingPrice > 0 && Math.Round(pr.Price, 1) >= Math.Round(pr.CeilingPrice, 1))
                //    {
                //        color = "pink";
                //    }
                //    else
                //        if (pr.FloorPrice > 0 && Math.Round(pr.Price, 1) <= Math.Round(pr.FloorPrice, 1))
                //        {
                //            color = "blue";
                //        }

                //    ltrChange.Text = "<span class='lichsu'><div class='col2'><div class='l'>" + String.Format("{0:#,##0.0}", ConvertUtility.ToDouble(pr.Price)) + "</div><div class='r " + icon + " " + color + "' style='float:left'>" + strChgIndex + "</div></div></span>";
                //}
                //catch (Exception){}
                var s = symbol;
                var d = __dr.EventDate.ToString("yyyyMMmdd");
                double chgIndex, pctIndex;
                if (ps!=null)
                {
                    if (ps.ContainsKey(s.ToUpper()) && ps[s.ToUpper()] != null)
                    {
                        var val = (StockPrice)ps[s.ToUpper()];
                        chgIndex = Convert.ToDouble(val.Price - val.RefPrice);
                        pctIndex = val.RefPrice > 0 ? Math.Round(100 * chgIndex / val.RefPrice, 1) : 0;
                        ImgUrl = Math.Round(pctIndex, 1) == 0 ? String.Format(img, "no_change.jpg") : (Math.Round(pctIndex, 1) > 0) ? String.Format(ImgUrl, "btup.gif") : String.Format(ImgUrl, "btdown.gif");
                        ltrChange.Text = string.Format(strTable, val.Price, CompanyHelper_Update.FormatColor(chgIndex, pctIndex, val.RefPrice, val.Price, val.CeilingPrice, val.FloorPrice, true));
                    }
                }
                else
                {
                    if (phs.ContainsKey(s + "-" + d) && phs[s + "-" + d] != null)
                    {
                        var val = (StockHistory)phs[s + "-" + d];
                        var hasAvg = val.AveragePrice > 0;
                        chgIndex = Convert.ToDouble((hasAvg ? val.AveragePrice : val.ClosePrice) - val.BasicPrice);
                        pctIndex = val.BasicPrice > 0 ? Math.Round(100 * chgIndex / val.BasicPrice, 1) : 0;
                        ImgUrl = Math.Round(pctIndex, 1) == 0 ? String.Format(img, "no_change.jpg") : (Math.Round(pctIndex, 1) > 0) ? String.Format(ImgUrl, "btup.gif") : String.Format(ImgUrl, "btdown.gif");
                        ltrChange.Text = string.Format(strTable, hasAvg ? val.AveragePrice : val.ClosePrice, CompanyHelper_Update.FormatColor(chgIndex, pctIndex, val.BasicPrice, hasAvg ? val.AveragePrice : val.ClosePrice, val.Ceiling, val.Floor, true));
                    }
                }

                if (__firstItem && __tblLength > 1)
                {
                    ltrScript.Text += "'" + symbol + "'";
                    __firstItem = false;
                }
                else
                {
                    ltrScript.Text += ",'" + symbol + "'";
                }
                __tblLength--;
            }
        }

        protected void rptPage_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "paging")
            {
                d1 = GetDateValue(hdfDate1.Value);
                d2 = GetDateValue(hdfDate2.Value);
                hdfPageIndex.Value = e.CommandArgument.ToString();
                pageIndex = ConvertUtility.ToInt32(hdfPageIndex.Value);
                Symbol = hdfSymbol.Value;
                
                blStatus = Convert.ToBoolean(ViewState["LSKStatus"]);
                BindData(blStatus);               
            }
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

        protected void btSearch_Click(object sender, ImageClickEventArgs e)
        {
            pageIndex = 1;
            hdfPageIndex.Value = "1";
            hdfDate1.Value = dpkTradeDate1.SelectedDate.ToString();
            hdfDate2.Value = dpkTradeDate2.SelectedDate.ToString();
            hdfSymbol.Value = txtKeyword.Text;
            Symbol = txtKeyword.Text;
            d1 = dpkTradeDate1.SelectedDate;
            d2 = dpkTradeDate2.SelectedDate;
            BindData(blStatus);
        }

        public string GetListEventTypeName(string value)
        {
            string strResult = string.Empty;
            //if (string.IsNullOrEmpty(value))
            //    return "";
            //using (DataTable dtEventType = CompanyHelper_Update.GetListEventTypeName(value))
            //{
            //    foreach (DataRow row in dtEventType.Rows)
            //    {
            //        strResult += string.Format("   + {0}<br />", row["TypeName"]);
            //    }
            //}
            return strResult;
        }

        protected void lbkStatusTrue_Click(object sender, EventArgs e)
        {
            ViewState["LSKStatusSearch"] = false;
            blStatus = true;
            ViewState["LSKStatus"] = blStatus;
            BindData(blStatus);
            lbkStatusTrue.Visible = false;
            lbkStatusFalse.Visible = true;
        }

        protected void lbkStatusFalse_Click(object sender, EventArgs e)
        {
            ViewState["LSKStatusSearch"] = false;
            blStatus = false;
            ViewState["LSKStatus"] = blStatus;
            BindData(blStatus);
            lbkStatusTrue.Visible = true;
            lbkStatusFalse.Visible = false;
        }

        private string UnicodeToKoDauAndGach(string s)
        {
            string strChar = "abcdefghiklmnopqrstxyzuvxw0123456789 ";
            s = s.Replace("–", "");
            s = s.Replace("  ", " ");
            s = UnicodeToKoDau(s.ToLower().Trim());
            string sReturn = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (strChar.IndexOf(s[i]) > -1)
                {
                    if (s[i] != ' ')
                        sReturn += s[i];
                    else if (i > 0 && s[i - 1] != ' ' && s[i - 1] != '-')
                        sReturn += "-";
                }
            }

            return sReturn;
        }

        private string UnicodeToKoDau(string s)
        {
            string retVal = String.Empty;
            s = s.Trim();
            int pos;
            for (int i = 0; i < s.Length; i++)
            {
                pos = uniChars.IndexOf(s[i].ToString());
                if (pos >= 0)
                    retVal += KoDauChars[pos];
                else
                    retVal += s[i];
            }
            return retVal;
        }

        private DateTime GetDateValue(string d)
        {
            try
            {
                return Convert.ToDateTime(d);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        public const string KoDauChars =
            "aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIOOOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU";

        public const string uniChars =
            "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";
    }
}