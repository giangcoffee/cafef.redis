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
    public partial class Lich_su_kien : System.Web.UI.UserControl
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
        private Dictionary<string, StockPrice> ps;
        private Dictionary<string, StockHistory> phs;

        private DataTable realTimeDataHoSTC, realTimeDataHoSE, realTimeDataUpcom;
        private DataRow[] rows = null;
        private DataRow[] rowsHa = null;
        private DataRow[] rowsHo = null;
        private DataRow[] rowsUp = null;
        private string ImgUrl = string.Empty;
        private string strChgIndex = string.Empty;
        private double pctIndex = 0, chgIndex = 0;
        private const string __CompanyLinkFormat = "<a style=\"font-weight:bold;\" target=\"_parent\" href=\"{0}\"  title=\"{1}\" >{2}</a>";

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
                int topEvents = Convert.ToInt32(ConfigurationManager.AppSettings["TopEvents"]);
                BindData(blStatus);
                //realTimeDataHoSTC = MarketHelper.GetStockPriceByMarket("HaSTC");
                //realTimeDataHoSE = MarketHelper.GetStockPriceByMarket("HoSE");
            }
        }

        public void BindData(bool status)
        {
            grvLichSuKien.Columns[grvLichSuKien.Columns.Count - 1].HeaderText = (status ? "Giá hiện tại" : "Giá tại ngày GD không hưởng quyền");
            string symbol = "";
            symbol = txtKeyword.Text.Trim();
            int type = 0;
            type = int.Parse(dlType.SelectedValue);
            ltrScript.Text = __danhsachma; __firstItem = true;
            int topEvents = Convert.ToInt32(ConfigurationManager.AppSettings["TopEvents"]);
            DataTable dtEventCalendar = new DataTable();
            if (status)
            {
                dtEventCalendar = CompanyHelper_Update.V2_FC_tblLichSuKien_Search_New_Paging(symbol, d1, d2, pageIndex, PageSize, type);
                totalPage = CompanyHelper_Update.V2_FC_tblLichSuKien_Search_New_Total(symbol, dpkTradeDate1.SelectedDate, dpkTradeDate2.SelectedDate, PageSize, type);
            }
            else
            {
                dtEventCalendar = CompanyHelper_Update.V2_FC_tblLichSuKien_Search_Old_Paging(symbol, d1, d2, pageIndex, PageSize, type);
                totalPage = CompanyHelper_Update.V2_FC_tblLichSuKien_Search_Old_Total(symbol, dpkTradeDate1.SelectedDate, dpkTradeDate2.SelectedDate, PageSize, type);
            }

            #region Price
            if (!dtEventCalendar.Columns.Contains("PriceString"))
            {
                dtEventCalendar.Columns.Add("CenterName", "".GetType());
                dtEventCalendar.Columns.Add("PriceString", "".GetType());
                dtEventCalendar.Columns.Add("StockLink", "".GetType());
            }
            ps = new Dictionary<string, StockPrice>();
            phs = new Dictionary<string, StockHistory>();
            var ls = new List<string>();
            foreach (DataRow row in dtEventCalendar.Rows)
            {
                var s = row["StockSymbols"].ToString().Trim().ToUpper();
                s = s.Replace("&", "");
                if (!ls.Contains(s)) ls.Add(s);
            }
            if (ls.Count > 0)
            {
                if (status)
                {
                    ps = (Dictionary<string, StockPrice>)StockBL.GetStockPriceMultiple(ls);
                }
                else
                {
                    var dates = new List<string>();
                    foreach (DataRow row in dtEventCalendar.Rows)
                    {
                        var s = row["StockSymbols"].ToString().Trim().ToUpper();
                        s = s.Replace("&", "");
                        var d = "";
                        if (null != row["NgayBatDau"] && DBNull.Value != row["NgayBatDau"])
                        {
                            try
                            {
                                d = Convert.ToDateTime(row["NgayBatDau"]).ToString("yyyyMMdd");
                            }catch(Exception)
                            {
                                d = DateTime.Now.AddDays(1).ToString("yyyyMMdd");
                            }
                        }
                        if (!dates.Contains(s + "-" + d)) dates.Add(s + "-" + d);
                    }
                    phs = (Dictionary<string, StockHistory>)StockHistoryBL.GetStockPriceMultiple(dates);
                }
                var ss = StockBL.GetStockCompactInfoMultiple(ls);

                string img = " <img src='http://cafef3.vcmedia.vn/images/{0}' align='absmiddle'>";
                string strTable = "<table style=\"border-bottom:0px;border-right:0px;border-left:0px;border-top:0px;\" border=\"0\" width=\"100%\"><tr><td style=\"border-bottom:0px;border-right:0px;border-left:0px;border-top:0px;\" border=\"0\" width=\"30px\">{0}</td><td style=\"border-bottom:0px;border-right:0px;border-left:0px;border-top:0px;\" border=\"0\" width=\"110px\">{1}</td></tr></table>";

                foreach (DataRow row in dtEventCalendar.Rows)
                {
                    row["StockLink"] = "";
                    row["PriceString"] = "";
                    row["Centername"] = "";
                    var s = row["StockSymbols"].ToString().Trim().ToUpper();
                    string d = "";
                    if (null != row["NgayBatDau"] && DBNull.Value != row["NgayBatDau"])
                    {
                        try
                        {
                            d = Convert.ToDateTime(row["NgayBatDau"]).ToString("yyyyMMdd");
                        }
                        catch (Exception)
                        {
                            d = DateTime.Now.AddDays(1).ToString("yyyyMMdd");
                        }
                    }
                    s = s.Replace("&", "");
                    if (ss.ContainsKey(s.ToUpper()) && ss[s.ToUpper()] != null)
                    {
                        var os = ss[s.ToUpper()];
                        row["StockLink"] = String.Format(__CompanyLinkFormat, Utils.GetSymbolLink(os.Symbol, os.CompanyName, os.TradeCenterId.ToString()), os.Symbol, os.Symbol);
                        row["Centername"] = Utils.GetCenterName(os.TradeCenterId.ToString());
                    }
                    if (status)
                    {
                        if (ps.ContainsKey(s.ToUpper()) && ps[s.ToUpper()] != null)
                        {
                            var val = (StockPrice)ps[s.ToUpper()];
                            chgIndex = Convert.ToDouble(val.Price - val.RefPrice);
                            pctIndex = val.RefPrice > 0 ? Math.Round(100 * chgIndex / val.RefPrice, 1) : 0;
                            ImgUrl = Math.Round(pctIndex, 1) == 0 ? String.Format(img, "no_change.jpg") : (Math.Round(pctIndex, 1) > 0) ? String.Format(ImgUrl, "btup.gif") : String.Format(ImgUrl, "btdown.gif");
                            row["PriceString"] = string.Format(strTable, val.Price, CompanyHelper_Update.FormatColor(chgIndex, pctIndex, val.RefPrice, val.Price, val.CeilingPrice, val.FloorPrice, true));
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
                            row["PriceString"] = string.Format(strTable, hasAvg ? val.AveragePrice : val.ClosePrice, CompanyHelper_Update.FormatColor(chgIndex, pctIndex, val.BasicPrice, hasAvg ? val.AveragePrice : val.ClosePrice, val.Ceiling, val.Floor, true));
                        }
                    }
                }
            }
            #endregion

            __tblLength = dtEventCalendar.Rows.Count - 1;
            dtEventCalendar.AcceptChanges();
            grvLichSuKien.DataSource = dtEventCalendar;
            if (pageIndex > 0) grvLichSuKien.PageIndex = pageIndex;
            grvLichSuKien.DataBind();
            if (ltrScript.Text != "")
            {
                ltrScript.Text += ");</script>";
            }
            if (dtEventCalendar != null && dtEventCalendar.Rows.Count > 0)
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
                string urlFormat = "/{0}-{1}/{2}.chn";

                Literal ltrMaCK = e.Row.FindControl("ltrMaCK") as Literal;
                Literal ltrNgayBatDau = e.Row.FindControl("ltrNgayBatDau") as Literal;
                Literal ltrNgayKetThuc = e.Row.FindControl("ltrNgayKetThuc") as Literal;
                Literal ltrNgayThucHien = e.Row.FindControl("ltrNgayThucHien") as Literal;
                Literal ltrSuKien = e.Row.FindControl("ltrSuKien") as Literal;
                Literal ltrSan = e.Row.FindControl("ltrSan") as Literal;
                Literal ltrChange = e.Row.FindControl("ltrChange") as Literal;

                HyperLink lnkNoiDung = e.Row.FindControl("lnkNoiDung") as HyperLink;

                DataRowView drvEvent = e.Row.DataItem as DataRowView;
                string symbol = "";

                if (null != drvEvent["StockSymbols"] && DBNull.Value != drvEvent["StockSymbols"])
                {
                    symbol = drvEvent["StockSymbols"].ToString().Replace("&", "");
                }
                ltrSan.Text = "<div id=\"sp_" + symbol + "\"></div>";
                if (null != drvEvent["NgayBatDau"] && DBNull.Value != drvEvent["NgayBatDau"])
                {
                    ltrNgayBatDau.Text = Convert.ToDateTime(drvEvent["NgayBatDau"]).ToString("dd/MM/yyyy");
                }
                if (null != drvEvent["NgayKetThuc"] && DBNull.Value != drvEvent["NgayKetThuc"])
                {
                    ltrNgayKetThuc.Text = Convert.ToDateTime(drvEvent["NgayKetThuc"]).ToString("dd/MM/yyyy");
                }
                if (null != drvEvent["NgayThucHien"] && DBNull.Value != drvEvent["NgayThucHien"])
                {
                    ltrNgayThucHien.Text = Convert.ToDateTime(drvEvent["NgayThucHien"]).ToString("dd/MM/yyyy");
                }
                //if (string.IsNullOrEmpty(ltrNgayThucHien.Text.Trim())) ltrNgayThucHien.Text = ltrNgayBatDau.Text;
                ltrSuKien.Text = GetListEventTypeName(drvEvent["EventType_List"].ToString());
                lnkNoiDung.Text = drvEvent["TomTat"].ToString();
                lnkNoiDung.ToolTip = drvEvent["TomTat"].ToString();
                lnkNoiDung.NavigateUrl = string.Format(urlFormat, symbol, drvEvent["News_ID"], NewsHepler_Update.UnicodeToKoDauAndGach(drvEvent["Title"].ToString()));
                ltrMaCK.Text = drvEvent["StockLink"].ToString();
                ltrChange.Text = drvEvent["PriceString"].ToString();
                //ltrMaCK.Text = String.Format(__CompanyLinkFormat, CompanyHelper_Update.GetCompanyInfoLink(symbol), symbol, symbol); //"<a href=\"http://cafef.vn/Thi-truong-niem-yet/Thong-tin-cong-ty/" + __dr["StockSymbol"].ToString() + ".chn\" target='_blank' title=\"" + __dr["StockSymbol"].ToString() + "\">" + __dr["StockSymbol"].ToString() + "</a>";

                //if (realTimeDataHoSTC == null || realTimeDataHoSTC.Rows.Count < 1)
                //    realTimeDataHoSTC = MarketHelper.GetStockPriceByMarket("HaSTC");
                //if (realTimeDataHoSE == null || realTimeDataHoSE.Rows.Count < 1)
                //    realTimeDataHoSE = MarketHelper.GetStockPriceByMarket("HoSE");
                //if (realTimeDataUpcom == null || realTimeDataUpcom.Rows.Count < 1)
                //    realTimeDataUpcom = MarketHelper.GetStockPriceByMarket("UpCom");

                //rows = null;
                //if (realTimeDataHoSTC != null && realTimeDataHoSTC.Rows.Count > 0)
                //    rows = realTimeDataHoSTC.Select("code='" + symbol + "'");
                //if (!(rows != null && rows.Length > 0))
                //{
                //    if (realTimeDataHoSE != null && realTimeDataHoSE.Rows.Count > 0)
                //        rows = realTimeDataHoSE.Select("code='" + symbol + "'");
                //}

                //if (!(rows != null && rows.Length > 0))
                //{
                //    if (realTimeDataUpcom != null && realTimeDataUpcom.Rows.Count > 0)
                //        rows = realTimeDataUpcom.Select("code='" + symbol + "'");
                //}

                //if ((rows != null && rows.Length > 0))
                //{
                //if (ps.ContainsKey(symbol.ToUpper()) && ps[symbol.ToUpper()] != null)
                //{

                //    string img = " <img src='http://cafef3.vcmedia.vn/images/{0}' align='absmiddle'>";
                //    string strTable = "<table style=\"border-bottom:0px;border-right:0px;border-left:0px;border-top:0px;\" border=\"0\" width=\"100%\"><tr><td style=\"border-bottom:0px;border-right:0px;border-left:0px;border-top:0px;\" border=\"0\" width=\"30px\">{0}</td><td style=\"border-bottom:0px;border-right:0px;border-left:0px;border-top:0px;\" border=\"0\" width=\"110px\">{1}</td></tr></table>";
                //   if(Convert.ToBoolean(ViewState["LSKStatus"]))
                //   {
                //    var val = (StockPrice)ps[symbol.ToUpper()]; 
                //       chgIndex = Convert.ToDouble(val.Price-val.RefPrice);
                //       pctIndex = val.RefPrice > 0 ? Math.Round(chgIndex / val.RefPrice, 1) : 0;
                //       ImgUrl = Math.Round(pctIndex, 1) == 0 ? String.Format(img, "no_change.jpg") : (Math.Round(pctIndex, 1) > 0) ? String.Format(ImgUrl, "btup.gif") : String.Format(ImgUrl, "btdown.gif");
                //       ltrChange.Text = string.Format(strTable, val.Price, CompanyHelper_Update.FormatColor(chgIndex, pctIndex, val.RefPrice, val.Price, val.CeilingPrice, val.FloorPrice, true));
                //   }else
                //   {
                //      var val = (StockHistory) phs[symbol.ToUpper()];
                //       var hasAvg = val.AveragePrice > 0;
                //       chgIndex = Convert.ToDouble((hasAvg?val.AveragePrice:val.ClosePrice) - val.BasicPrice);
                //       pctIndex = val.BasicPrice > 0 ? Math.Round(chgIndex / val.BasicPrice, 1) : 0;
                //      ImgUrl = Math.Round(pctIndex, 1) == 0 ? String.Format(img, "no_change.jpg") : (Math.Round(pctIndex, 1) > 0) ? String.Format(ImgUrl, "btup.gif") : String.Format(ImgUrl, "btdown.gif");
                //      ltrChange.Text = string.Format(strTable, hasAvg ? val.AveragePrice : val.ClosePrice, CompanyHelper_Update.FormatColor(chgIndex, pctIndex, val.BasicPrice, hasAvg ? val.AveragePrice : val.ClosePrice, val.Ceiling, val.Floor, true));
                //   }
                //}
                //}
                //ltrSan.Text = "<div id=\"sp_" + symbol + "\">&nbsp;</div>";
                ltrSan.Text = drvEvent["CenterName"].ToString();
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
                d1 = CafefCommonHelper.GetDateValue(hdfDate1.Value);
                d2 = CafefCommonHelper.GetDateValue(hdfDate2.Value);
                hdfPageIndex.Value = e.CommandArgument.ToString();
                pageIndex = ConvertUtility.ToInt32(hdfPageIndex.Value);
                Symbol = hdfSymbol.Value;

                if (!Convert.ToBoolean(ViewState["LSKStatusSearch"]))
                {
                    blStatus = Convert.ToBoolean(ViewState["LSKStatus"]);
                    BindData(blStatus);
                }
                else
                {
                    SearchEvent();
                }
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
            SearchEvent();
        }

        public string GetListEventTypeName(string value)
        {
            string strResult = string.Empty;
            if (string.IsNullOrEmpty(value))
                return "";
            using (DataTable dtEventType = CompanyHelper_Update.GetListEventTypeName(value))
            {
                foreach (DataRow row in dtEventType.Rows)
                {
                    strResult += string.Format("   + {0}<br />", row["TypeName"]);
                }
            }
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

        public void SearchEvent()
        {
            ViewState["LSKStatusSearch"] = true;
            string symbol = "";
            symbol = txtKeyword.Text.Trim();
            int type = 0;
            type = int.Parse(dlType.SelectedValue);
            ltrScript.Text = __danhsachma; __firstItem = true;
            int topEvents = Convert.ToInt32(ConfigurationManager.AppSettings["TopEvents"]);
            DataTable dtEventCalendar = CompanyHelper_Update.V2_FC_tblLichSuKien_Search_Paging(symbol, d1, d2, pageIndex, PageSize,type);
            totalPage = CompanyHelper_Update.V2_FC_tblLichSuKien_Search_Total(symbol, dpkTradeDate1.SelectedDate, dpkTradeDate2.SelectedDate,type);

            #region Price
            if (!dtEventCalendar.Columns.Contains("PriceString"))
            {
                dtEventCalendar.Columns.Add("CenterName", "".GetType());
                dtEventCalendar.Columns.Add("PriceString", "".GetType());
                dtEventCalendar.Columns.Add("StockLink", "".GetType());
            }
            ps = new Dictionary<string, StockPrice>();
            phs = new Dictionary<string, StockHistory>();
            var ls = new List<string>();
            foreach (DataRow row in dtEventCalendar.Rows)
            {
                var s = row["StockSymbols"].ToString().Trim().ToUpper();
                s = s.Replace("&", "");
                if (!ls.Contains(s)) ls.Add(s);
            }
            if (ls.Count > 0)
            {
                var dates = new List<string>();
                foreach (DataRow row in dtEventCalendar.Rows)
                {
                    var s = row["StockSymbols"].ToString().Trim().ToUpper();
                    s = s.Replace("&", "");
                    var d = "";
                    if (null != row["NgayBatDau"] && DBNull.Value != row["NgayBatDau"])
                    {
                        try
                        {
                            d = Convert.ToDateTime(row["NgayBatDau"]).ToString("yyyyMMdd");
                        }
                        catch (Exception)
                        {
                            d = DateTime.Now.AddDays(1).ToString("yyyyMMdd");
                        }
                    }
                    if (!dates.Contains(s + "-" + d)) dates.Add(s + "-" + d);
                }
                phs = (Dictionary<string, StockHistory>)StockHistoryBL.GetStockPriceMultiple(dates);

                var ss = StockBL.GetStockCompactInfoMultiple(ls);
                ps = (Dictionary<string, StockPrice>)StockBL.GetStockPriceMultiple(ls);

                string img = " <img src='http://cafef3.vcmedia.vn/images/{0}' align='absmiddle'>";
                string strTable = "<table style=\"border-bottom:0px;border-right:0px;border-left:0px;border-top:0px;\" border=\"0\" width=\"100%\"><tr><td style=\"border-bottom:0px;border-right:0px;border-left:0px;border-top:0px;\" border=\"0\" width=\"30px\">{0}</td><td style=\"border-bottom:0px;border-right:0px;border-left:0px;border-top:0px;\" border=\"0\" width=\"110px\">{1}</td></tr></table>";

                foreach (DataRow row in dtEventCalendar.Rows)
                {
                    row["StockLink"] = "";
                    row["PriceString"] = "";
                    var s = row["StockSymbols"].ToString().Trim().ToUpper().Replace("&", "");
                    string d = "";
                    if (null != row["NgayBatDau"] && DBNull.Value != row["NgayBatDau"])
                    {
                        try
                        {
                            d = Convert.ToDateTime(row["NgayBatDau"]).ToString("yyyyMMdd");
                        }
                        catch (Exception)
                        {
                            d = DateTime.Now.AddDays(1).ToString("yyyyMMdd");
                        }
                    }
                    if (ss.ContainsKey(s.ToUpper()) && ss[s.ToUpper()] != null)
                    {
                        var os = ss[s.ToUpper()];
                        row["StockLink"] = String.Format(__CompanyLinkFormat, Utils.GetSymbolLink(os.Symbol, os.CompanyName, os.TradeCenterId.ToString()), os.Symbol, os.Symbol);
                        row["CenterName"] = Utils.GetCenterName(os.TradeCenterId.ToString());
                    }
                    if (phs.ContainsKey(s + "-" + d) && phs[s + "-" + d] != null)
                    {
                        var val = (StockHistory)phs[s + "-" + d];
                        var hasAvg = val.AveragePrice > 0;
                        chgIndex = Convert.ToDouble((hasAvg ? val.AveragePrice : val.ClosePrice) - val.BasicPrice);
                        pctIndex = val.BasicPrice > 0 ? Math.Round(100 * chgIndex / val.BasicPrice, 1) : 0;
                        ImgUrl = Math.Round(pctIndex, 1) == 0 ? String.Format(img, "no_change.jpg") : (Math.Round(pctIndex, 1) > 0) ? String.Format(ImgUrl, "btup.gif") : String.Format(ImgUrl, "btdown.gif");
                        row["PriceString"] = string.Format(strTable, hasAvg ? val.AveragePrice : val.ClosePrice, CompanyHelper_Update.FormatColor(chgIndex, pctIndex, val.BasicPrice, hasAvg ? val.AveragePrice : val.ClosePrice, val.Ceiling, val.Floor, true));
                    }
                    else
                    {
                        if (d != "" && double.Parse(d) >= double.Parse(DateTime.Now.ToString("yyyyMMdd")))
                        {
                            if (ps.ContainsKey(s.ToUpper()) && ps[s.ToUpper()] != null)
                            {
                                var val = (StockPrice)ps[s.ToUpper()];
                                chgIndex = Convert.ToDouble(val.Price - val.RefPrice);
                                pctIndex = val.RefPrice > 0 ? Math.Round(100 * chgIndex / val.RefPrice, 1) : 0;
                                ImgUrl = Math.Round(pctIndex, 1) == 0 ? String.Format(img, "no_change.jpg") : (Math.Round(pctIndex, 1) > 0) ? String.Format(ImgUrl, "btup.gif") : String.Format(ImgUrl, "btdown.gif");
                                row["PriceString"] = string.Format(strTable, val.Price, CompanyHelper_Update.FormatColor(chgIndex, pctIndex, val.RefPrice, val.Price, val.CeilingPrice, val.FloorPrice, true));
                            }
                        }
                    }
                }
            }
            #endregion

            __tblLength = dtEventCalendar.Rows.Count - 1;
            grvLichSuKien.DataSource = dtEventCalendar;
            if (pageIndex > 0) grvLichSuKien.PageIndex = pageIndex;
            grvLichSuKien.DataBind();
            if (ltrScript.Text != "")
            {
                ltrScript.Text += ");</script>";
            }
            if (dtEventCalendar != null && dtEventCalendar.Rows.Count > 0)
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
                //if (totalPage > PageSize)
                //{
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

        /* tiennv : 27-05-2010 */
        //private DataTable CreatePriceDataTable(string tableName)
        //{
        //    DataTable dtPriceTable = new DataTable(tableName);
        //    dtPriceTable.Columns.Add("code", typeof(string));
        //    dtPriceTable.Columns.Add("currentPrice", typeof(double));
        //    dtPriceTable.Columns.Add("chgIndex", typeof(double));
        //    dtPriceTable.Columns.Add("matchQtty", typeof(double));
        //    dtPriceTable.Columns.Add("totalTradingQtty", typeof(double));
        //    dtPriceTable.Columns.Add("Stock", typeof(int));
        //    dtPriceTable.Columns.Add("pctIndex", typeof(double));
        //    dtPriceTable.Columns.Add("currentIndex", typeof(double));
        //    dtPriceTable.Columns.Add("averagePrice", typeof(double));
        //    dtPriceTable.Columns.Add("openPrice", typeof(double));
        //    dtPriceTable.Columns.Add("basicPrice", typeof(double));

        //    dtPriceTable.Columns.Add("adjustQtty", typeof(double));//GDNN
        //    dtPriceTable.Columns.Add("adjustRate", typeof(double));//RoomNN
        //    //thanhbv :4-12-2008
        //    dtPriceTable.Columns.Add("highestPrice", typeof(double));
        //    dtPriceTable.Columns.Add("lowestPrice", typeof(double));

        //    dtPriceTable.Columns.Add("extentValue", typeof(string));
        //    // SonPC
        //    dtPriceTable.Columns.Add("closePrice", typeof(double));
        //    //hungnd
        //    dtPriceTable.Columns.Add("Ceiling", typeof(double));
        //    dtPriceTable.Columns.Add("Floor", typeof(double));
        //    dtPriceTable.Columns.Add("TotalShare", typeof(long));

        //    return dtPriceTable;
        //}
        //public static DataTable GetTotalShareBySymbol()
        //{
        //    DataTable dtProfile = new DataTable("h_profile");
        //    using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["FinanceChannelConnectionString"].ConnectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("H_tblCompanyProfile_GetCompanyProfile", cnn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
        //            {
        //                adapter.Fill(dtProfile);
        //            }
        //        }
        //    }
        //    return dtProfile;
        //}
        //private DataTable ReturnTableUpCom()
        //{
        //    DataTable tblProfileCom;
        //    DataRow[] rowProfile;
        //    tblProfileCom = GetTotalShareBySymbol();
        //    DataTable dtPriceTable = CreatePriceDataTable("PriceData");
        //    DataTable dtData = new DataTable();

        //    string connectionString = ConfigurationManager.ConnectionStrings["VincomscPriceBoardConnectionString"].ConnectionString;
        //    using (SqlConnection cnn = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("tblUpComPriceBoard_GetData", cnn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
        //            {

        //                adapter.Fill(dtData);
        //            }
        //        }
        //    }

        //    if (dtData != null && dtData.Rows.Count > 0)
        //    {

        //        for (int i = 0; i < dtData.Rows.Count; i++)
        //        {
        //            DataRow drNewRow = dtPriceTable.NewRow();

        //            #region Chuan hoa du lieu
        //            drNewRow["code"] = dtData.Rows[i]["Symbol"].ToString();
        //            drNewRow["Stock"] = (int)TraCuuLichSu2.TradeCenter.UpCom;

        //            drNewRow["matchQtty"] = ConvertUtility.ToDouble(dtData.Rows[i]["TradingVol"]) / 10; // Don vi 10cp
        //            drNewRow["totalTradingQtty"] = ConvertUtility.ToDouble(dtData.Rows[i]["TotalTradingVolume"]) / 10; // Don vi 10cp

        //            double currentPrice = ConvertUtility.ToDouble(dtData.Rows[i]["TradingPrice"]);
        //            double basicPrice = ConvertUtility.ToDouble(dtData.Rows[i]["Ref"]);
        //            double averagePrice = ConvertUtility.ToDouble(dtData.Rows[i]["averagePrice"]);
        //            if (currentPrice > 0)
        //            {
        //                drNewRow["averagePrice"] = averagePrice;
        //                drNewRow["currentPrice"] = currentPrice;
        //                drNewRow["currentIndex"] = currentPrice;
        //            }
        //            else
        //            {
        //                drNewRow["averagePrice"] = basicPrice;
        //                drNewRow["currentPrice"] = basicPrice;
        //                drNewRow["currentIndex"] = basicPrice;
        //            }

        //            if (currentPrice > 0 && basicPrice > 0)
        //            {
        //                drNewRow["chgIndex"] = currentPrice - basicPrice;
        //                drNewRow["pctIndex"] = ((currentPrice - basicPrice) / basicPrice) * 100;
        //            }
        //            else
        //            {
        //                drNewRow["chgIndex"] = 0;
        //                drNewRow["pctIndex"] = 0;
        //            }

        //            drNewRow["openPrice"] = basicPrice;
        //            drNewRow["basicPrice"] = basicPrice;
        //            try
        //            {
        //                drNewRow["closePrice"] = drNewRow["averagePrice"];
        //            }
        //            catch
        //            {

        //            }

        //            drNewRow["adjustQtty"] = ConvertUtility.ToDouble(dtData.Rows[i]["FITradingVol"]) / 10;// 10; // GDNN (Don vi 10cp)
        //            drNewRow["adjustRate"] = ConvertUtility.ToDouble(dtData.Rows[i]["FIRoom"]) / 10; // RoomNN (Don vi 10cp)
        //            drNewRow["highestPrice"] = ConvertUtility.ToDouble(dtData.Rows[i]["TradingPriceMax"]); // 
        //            drNewRow["lowestPrice"] = ConvertUtility.ToDouble(dtData.Rows[i]["TradingPriceMin"]); //
        //            try
        //            {
        //                if (tblProfileCom != null && tblProfileCom.Rows.Count > 0)
        //                {
        //                    rowProfile = tblProfileCom.Select("Stocksymbol IN ('" + dtData.Rows[i]["Symbol"].ToString().Trim() + "')");
        //                    if (rowProfile != null && rowProfile.Length > 0)
        //                    {
        //                        drNewRow["TotalShare"] = rowProfile[0]["TotalShare"]; // 
        //                    }
        //                }
        //                drNewRow["Ceiling"] = ConvertUtility.ToDouble(dtData.Rows[i]["Ceiling"]); // 
        //                drNewRow["Floor"] = ConvertUtility.ToDouble(dtData.Rows[i]["Floor"]); // 
        //            }
        //            catch { }
        //            #endregion

        //            dtPriceTable.Rows.Add(drNewRow);
        //        }

        //    }
        //    return dtPriceTable;
        //}
    }
}