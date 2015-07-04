using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Mail;
using System.Web.UI;
using System.Web.UI.HtmlControls;
//using Portal.Core.DAL;
using VCCorp.FinanceChannel.Core.DataUpd;

namespace CafeF.Redis.BO
{
    /// <summary>
    /// _RELATEED_CATEGORY_+CategoryId+_NEWS_+News_Id: ten cache doi voi cac tin cung category (CategoryId) va NewsId 
    /// _TOPNEW_CATEGORY_+Cat_Id
    /// </summary>
    public static class NewsHepler_Update
    {
        public const string __PAGE_KEYWORD =
            "cafef,tài chính,chứng khoán,vn-index,hastc-index,P/E,EPS,bất động sản,nhà đất,địa ốc,ngân hàng,đầu tư,cổ phiếu,cổ phần, trái phiếu, cổ tức,danh mục đầu tư,quản lý lãi lỗ,thị trường niêm yết,otc, blog tài chính, chính sách, tiền tệ, lạm phát, dữ liệu doanh nghiệp, blue-chips, vàng, Đô la, USD, IPO, đấu giá, bán khống, phái sinh,vốn điều lệ, lợi nhuận, tỷ giá, kinh tế, chuyên gia kinh tế, khủng hoảng kinh tế, suy thoái kinh tế, tài chính quốc tế, kinh doanh, tín dụng, quỹ đầu tư,  Việt Nam";

        public const string __PAGE_TITLE = "  CafeF.vn - Thông tin và dữ liệu tài chính, chứng khoán Việt Nam";
        public const string __PAGE_TITLE1 = "  Thông tin và dữ liệu tài chính, chứng khoán Việt Nam";
        public static string __Site_Url = ConfigurationSettings.AppSettings.Get("SITE_URL");

        public static string __strConn =
            ConfigurationManager.ConnectionStrings["FinanceChannelConnectionString"].ToString();

        public static string __strConn_CMS = ConfigurationSettings.AppSettings.Get("k14path");
        //connectiong string den CSDL chinh
        public static string __strConn_MasterDB = ConfigurationManager.ConnectionStrings["MasterDB_CafeF_Core"].ToString();
        public const string KoDauChars =
            "aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIOOOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU";

        public const string uniChars =
            "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";

        public static string UnicodeToKoDauAndGach(string s)
        {
            string strChar = "abcdefghiklmnopqrstxyzuvxw0123456789 ";
            //string retVal = UnicodeToKoDau(s);
            //s = s.Replace("-", " ");
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
        public static string UnicodeToKoDau(string s)
        {
            //return s;
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
        public static void Set_Page_Header(Page __Page, string addTitle1, string addTitle2, string addChapo, int Cat_ID)
        {
            Control __ctlTitle = __Page.Header.FindControl("Title");
            string __keyword = __PAGE_KEYWORD;
            if (__ctlTitle != null)
            {
                //string __newTitle = addTitle1 != ""
                //                        ? ( addTitle1 +
                //                           (addTitle2 != "" ? " - " + addTitle2 + " - " : " - ") + "  CafeF.vn - " + __PAGE_TITLE1)
                //                        : __PAGE_TITLE;
                string __newTitle = addTitle1 != ""
                                        ? (addTitle1 +
                                           (addTitle2 != "" ? " | " + addTitle2 : "") + " | CafeF.vn")
                                        : __PAGE_TITLE;
                __ctlTitle.Controls.Add(new LiteralControl(__newTitle));
            }
            HtmlMeta __ctlMetaKeyword = __Page.Header.FindControl("KEYWORDS") as HtmlMeta;
            if (Cat_ID > 0)
            {
                if (__ctlMetaKeyword != null)
                {
                    if (Cat_ID == 31) __keyword = Const.TCCK_KEYWORD; //chung khoan
                    if (Cat_ID == 35) __keyword = Const.BDS_KEYWORD;
                    if (Cat_ID == 34) __keyword = Const.TCNH_KEYWORD;
                    if (Cat_ID == 32) __keyword = Const.TCQT_KEYWORD;
                    if (Cat_ID == 36) __keyword = Const.DN_KEYWORD;
                    if (Cat_ID == 33) __keyword = Const.KTDT_KEYWORD;
                    if (__keyword == "") __keyword = __PAGE_KEYWORD;
                    __ctlMetaKeyword.Attributes["content"] = __keyword;
                }
            }
            else
            {
                __ctlMetaKeyword.Visible = false;
            }
            HtmlMeta __ctlMetaDesc = __Page.Header.FindControl("description") as HtmlMeta;
            //if (__ctlMetaDesc != null)
            //{
            //    __ctlMetaDesc.Attributes["content"] = addChapo != "" ? addChapo + "," + __keyword : __keyword;
            //}

            //description mà có keywords nữa là thừa, vì trên thẻ meta keywords đã có rồi mà lại để ở description nữa là trùng lặp
            //SonPC Modified
            if (__ctlMetaDesc != null)
            {
                __ctlMetaDesc.Attributes["content"] = addChapo != "" ? addChapo : "";
            }
            //SonPC Modifed

        }

        public static void Set_Page_Header(Page __Page, string addTitle1, int Cat_ID)
        {
            Control __ctlTitle = __Page.Header.FindControl("Title");
            if (__ctlTitle != null)
            {
                string _title_des = "";

                switch (Cat_ID)
                {
                    case 31:
                        _title_des = Const.TCCK_TITLE_DES;
                        break;
                    case 35:
                        _title_des = Const.BDS_TITLE_DES;
                        break;
                    case 34:
                        _title_des = Const.TCNH_TITLE_DES;
                        break;
                    case 32:
                        _title_des = Const.TCQT_TITLE_DES;
                        break;
                    case 36:
                        _title_des = Const.DN_TITLE_DES;
                        break;
                    case 33:
                        _title_des = Const.KTDT_TITLE_DES;
                        break;
                }

                //if (Cat_ID == 31) _title_des = Const.TCCK_TITLE_DES;
                //else
                //    if (Cat_ID == 35) _title_des = Const.BDS_TITLE_DES;
                //    else
                //        if (Cat_ID == 34) _title_des = Const.TCNH_TITLE_DES;
                //        else
                //            if (Cat_ID == 32) _title_des = Const.TCQT_TITLE_DES;
                //            else
                //                if (Cat_ID == 36) _title_des = Const.DN_TITLE_DES;
                //                else
                //                    if (Cat_ID == 33) _title_des = Const.KTDT_TITLE_DES;

                string __newTitle = _title_des;
                //if (_title_des != "")
                //    __newTitle =  addTitle1 + " - CafeF.vn - "  + _title_des;
                //else
                //    __newTitle = addTitle1 + " - CafeF.vn ";

                __ctlTitle.Controls.Add(new LiteralControl(__newTitle));
            }
            HtmlMeta __ctlMetaDesc = __Page.Header.FindControl("description") as HtmlMeta;
            if (__ctlMetaDesc != null)
            {
                string __description = "";

                switch (Cat_ID)
                {
                    case 31:
                        __description = Const.TCCK_DESCRIPTION;
                        break;
                    case 35:
                        __description = Const.BDS_DESCRIPTION;
                        break;
                    case 34:
                        __description = Const.TCNH_DESCRIPTION;
                        break;
                    case 32:
                        __description = Const.TCQT_DESCRIPTION;
                        break;
                    case 36:
                        __description = Const.DN_DESCRIPTION;
                        break;
                    case 33:
                        __description = Const.KTDT_DESCRIPTION;
                        break;
                }

                //if (Cat_ID == 31) __description = Const.TCCK_DESCRIPTION;
                //else
                //    if (Cat_ID == 35) __description = Const.BDS_DESCRIPTION;
                //    else
                //        if (Cat_ID == 34) __description = Const.TCNH_DESCRIPTION;
                //        else
                //            if (Cat_ID == 32) __description = Const.TCQT_DESCRIPTION;
                //            else
                //                if (Cat_ID == 36) __description = Const.DN_DESCRIPTION;
                //                else
                //                    if (Cat_ID == 33) __description = Const.KTDT_DESCRIPTION;
                if (__description == "") __description = __PAGE_KEYWORD;
                __ctlMetaDesc.Attributes["content"] = __description;
            }
            HtmlMeta __ctlMetaKeyword = __Page.Header.FindControl("KEYWORDS") as HtmlMeta;
            if (__ctlMetaKeyword != null)
            {
                string __keyword = "";

                switch (Cat_ID)
                {
                    case 31:
                        __keyword = Const.TCCK_KEYWORD;
                        break;
                    case 35:
                        __keyword = Const.BDS_KEYWORD;
                        break;
                    case 34:
                        __keyword = Const.TCNH_KEYWORD;
                        break;
                    case 32:
                        __keyword = Const.TCQT_KEYWORD;
                        break;
                    case 36:
                        __keyword = Const.DN_KEYWORD;
                        break;
                    case 33:
                        __keyword = Const.KTDT_KEYWORD;
                        break;
                }

                //if (Cat_ID == 31) __keyword = Const.TCCK_KEYWORD;
                //else
                //    if (Cat_ID == 35) __keyword = Const.BDS_KEYWORD;
                //    else
                //        if (Cat_ID == 34) __keyword = Const.TCNH_KEYWORD;
                //        else
                //            if (Cat_ID == 32) __keyword = Const.TCQT_KEYWORD;
                //            else
                //                if (Cat_ID == 36) __keyword = Const.DN_KEYWORD;
                //                else
                //                    if (Cat_ID == 33) __keyword = Const.KTDT_KEYWORD;
                if (__keyword == "") __keyword = __PAGE_KEYWORD;
                __ctlMetaKeyword.Attributes["content"] = __keyword;
            }
        }

        public static void Set_Page_Header(Page __Page, int pageIndex, int Cat_ID, DateTime startDate, DateTime endDate, bool showKeyword)
        {
            string viewDate = "";
            if (startDate != DateTime.MaxValue && endDate != DateTime.MaxValue)
            {
                if (startDate != endDate)
                {
                    viewDate = " | " + startDate.ToString("dd/MM/yyyy") + " - " + endDate.ToString("dd/MM/yyyy");
                }
                else
                {
                    viewDate = " | " + startDate.ToString("dd/MM/yyyy");
                }
            }

            Control __ctlTitle = __Page.Header.FindControl("Title");
            if (__ctlTitle != null)
            {
                string _title_des = "";

                switch (Cat_ID)
                {
                    case 31:
                        _title_des = string.Format(Const.TCCK_TITLE_DES, viewDate + (pageIndex > 1 ? " | Trang " + pageIndex.ToString() : ""));
                        break;
                    case 35:
                        _title_des = string.Format(Const.BDS_TITLE_DES, viewDate + (pageIndex > 1 ? " | Trang " + pageIndex.ToString() : ""));
                        break;
                    case 34:
                        _title_des = string.Format(Const.TCNH_TITLE_DES, viewDate + (pageIndex > 1 ? " | Trang " + pageIndex.ToString() : ""));
                        break;
                    case 32:
                        _title_des = string.Format(Const.TCQT_TITLE_DES, viewDate + (pageIndex > 1 ? " | Trang " + pageIndex.ToString() : ""));
                        break;
                    case 36:
                        _title_des = string.Format(Const.DN_TITLE_DES, viewDate + (pageIndex > 1 ? " | Trang " + pageIndex.ToString() : ""));
                        break;
                    case 33:
                        _title_des = string.Format(Const.KTDT_TITLE_DES, viewDate + (pageIndex > 1 ? " | Trang " + pageIndex.ToString() : ""));
                        break;
                    case 39:
                        _title_des = string.Format(Const.TTHH_TITLE_DES, viewDate + (pageIndex > 1 ? " | Trang " + pageIndex.ToString() : ""));
                        break;
                }

                //if (Cat_ID == 31) _title_des = Const.TCCK_TITLE_DES;
                //else
                //    if (Cat_ID == 35) _title_des = Const.BDS_TITLE_DES;
                //    else
                //        if (Cat_ID == 34) _title_des = Const.TCNH_TITLE_DES;
                //        else
                //            if (Cat_ID == 32) _title_des = Const.TCQT_TITLE_DES;
                //            else
                //                if (Cat_ID == 36) _title_des = Const.DN_TITLE_DES;
                //                else
                //                    if (Cat_ID == 33) _title_des = Const.KTDT_TITLE_DES;

                string __newTitle = _title_des;
                //if (_title_des != "")
                //    __newTitle =  addTitle1 + " - CafeF.vn - "  + _title_des;
                //else
                //    __newTitle = addTitle1 + " - CafeF.vn ";

                __ctlTitle.Controls.Add(new LiteralControl(__newTitle));
            }
            HtmlMeta __ctlMetaDesc = __Page.Header.FindControl("description") as HtmlMeta;
            if (__ctlMetaDesc != null)
            {
                string __description = "";

                switch (Cat_ID)
                {
                    case 31:
                        __description = string.Format(Const.TCCK_DESCRIPTION, viewDate + (pageIndex > 1 ? " | Trang " + pageIndex.ToString() : ""));
                        break;
                    case 35:
                        __description = string.Format(Const.BDS_DESCRIPTION, viewDate + (pageIndex > 1 ? " | Trang " + pageIndex.ToString() : ""));
                        break;
                    case 34:
                        __description = string.Format(Const.TCNH_DESCRIPTION, viewDate + (pageIndex > 1 ? " | Trang " + pageIndex.ToString() : ""));
                        break;
                    case 32:
                        __description = string.Format(Const.TCQT_DESCRIPTION, viewDate + (pageIndex > 1 ? " | Trang " + pageIndex.ToString() : ""));
                        break;
                    case 36:
                        __description = string.Format(Const.DN_DESCRIPTION, viewDate + (pageIndex > 1 ? " | Trang " + pageIndex.ToString() : ""));
                        break;
                    case 33:
                        __description = string.Format(Const.KTDT_DESCRIPTION, viewDate + (pageIndex > 1 ? " | Trang " + pageIndex.ToString() : ""));
                        break;
                    case 39:
                        __description = string.Format(Const.TTHH_DESCRIPTION, viewDate + (pageIndex > 1 ? " | Trang " + pageIndex.ToString() : ""));
                        break;
                }

                //if (Cat_ID == 31) __description = Const.TCCK_DESCRIPTION;
                //else
                //    if (Cat_ID == 35) __description = Const.BDS_DESCRIPTION;
                //    else
                //        if (Cat_ID == 34) __description = Const.TCNH_DESCRIPTION;
                //        else
                //            if (Cat_ID == 32) __description = Const.TCQT_DESCRIPTION;
                //            else
                //                if (Cat_ID == 36) __description = Const.DN_DESCRIPTION;
                //                else
                //                    if (Cat_ID == 33) __description = Const.KTDT_DESCRIPTION;
                if (__description == "") __description = __PAGE_KEYWORD;
                __ctlMetaDesc.Attributes["content"] = __description;
            }
            HtmlMeta __ctlMetaKeyword = __Page.Header.FindControl("KEYWORDS") as HtmlMeta;
            if (showKeyword && __ctlMetaKeyword != null)
            {
                string __keyword = "";

                switch (Cat_ID)
                {
                    case 31:
                        __keyword = Const.TCCK_KEYWORD;
                        break;
                    case 35:
                        __keyword = Const.BDS_KEYWORD;
                        break;
                    case 34:
                        __keyword = Const.TCNH_KEYWORD;
                        break;
                    case 32:
                        __keyword = Const.TCQT_KEYWORD;
                        break;
                    case 36:
                        __keyword = Const.DN_KEYWORD;
                        break;
                    case 33:
                        __keyword = Const.KTDT_KEYWORD;
                        break;
                    case 39:
                        __keyword = Const.TTHH_KEYWORD;
                        break;
                }

                //if (Cat_ID == 31) __keyword = Const.TCCK_KEYWORD;
                //else
                //    if (Cat_ID == 35) __keyword = Const.BDS_KEYWORD;
                //    else
                //        if (Cat_ID == 34) __keyword = Const.TCNH_KEYWORD;
                //        else
                //            if (Cat_ID == 32) __keyword = Const.TCQT_KEYWORD;
                //            else
                //                if (Cat_ID == 36) __keyword = Const.DN_KEYWORD;
                //                else
                //                    if (Cat_ID == 33) __keyword = Const.KTDT_KEYWORD;
                if (__keyword == "") __keyword = __PAGE_KEYWORD;
                __ctlMetaKeyword.Attributes["content"] = __keyword;
            }
            __ctlMetaKeyword.Visible = showKeyword;
        }

        public static void Set_Page_Header(Page __Page, string addTitle1, string des, string key)
        {
            Control __ctlTitle = __Page.Header.FindControl("Title");
            if (__ctlTitle != null)
            {
                __ctlTitle.Controls.Add(new LiteralControl(addTitle1));
            }
            HtmlMeta __ctlMetaDesc = __Page.Header.FindControl("description") as HtmlMeta;
            if (__ctlMetaDesc != null)
            {
                __ctlMetaDesc.Attributes["content"] = des;
            }
            HtmlMeta __ctlMetaKeyword = __Page.Header.FindControl("KEYWORDS") as HtmlMeta;
            if (__ctlMetaKeyword != null)
            {
                if (key == "")
                {
                    __ctlMetaKeyword.Visible = false;
                }
                else
                {
                    __ctlMetaKeyword.Visible = true;
                    __ctlMetaKeyword.Attributes["content"] = key;
                }
            }
        }

        public static void Set_Page_Header(Page __Page, string addTitle1, string des, string key, bool showKeyword)
        {
            Control __ctlTitle = __Page.Header.FindControl("Title");
            if (__ctlTitle != null)
            {
                __ctlTitle.Controls.Add(new LiteralControl(addTitle1));
            }
            HtmlMeta __ctlMetaDesc = __Page.Header.FindControl("description") as HtmlMeta;
            if (__ctlMetaDesc != null)
            {
                __ctlMetaDesc.Attributes["content"] = des;
            }
            HtmlMeta __ctlMetaKeyword = __Page.Header.FindControl("KEYWORDS") as HtmlMeta;
            if (showKeyword && __ctlMetaKeyword != null)
            {
                __ctlMetaKeyword.Attributes["content"] = key;
            }
            __ctlMetaKeyword.Visible = showKeyword;
        }
        
        public static DataTable vc_GetAllNewsByThreadId_Paging(int ThreadID, int StartIndex, int PageSize)
        {
            string __CacheName = "vc_GetAllNewsByThreadId_" + ThreadID.ToString();
            DataTable __result = (DataTable)HttpContext.Current.Cache[__CacheName]; ;
            if (__result == null)
            {
                using (Cafef_DAL.MainDB __db = new Cafef_DAL.MainDB())
                {
                    __result = __db.StoredProcedures.vc_GetAllNewsByThreadId_Paging(ThreadID, StartIndex, PageSize);
                }
            }
            return __result;
        }
        public static int vc_GetAllNewsByThreadIdTotalPage(int ThreadID)
        {
            string __CacheName = "vc_GetAllNewsByThreadId_" + ThreadID.ToString();
            DataTable __result = (DataTable)HttpContext.Current.Cache[__CacheName]; ;
            if (__result == null)
            {
                using (Cafef_DAL.MainDB __db = new Cafef_DAL.MainDB())
                {
                    __result = __db.StoredProcedures.vc_GetAllNewsByThreadIdTotalPage(ThreadID);
                    if (__result != null && __result.Rows.Count > 0)
                    {
                        int totalR = int.Parse(__result.Rows[0][0].ToString());
                        return (Int32)Math.Ceiling((Decimal)totalR / 15);
                    }
                }
            }
            return 0;
        }
        public static string BuildLink(string TabRef, string News_ID, string News_Title, string Cat_ID, string Cat_Title)
        {
            if (Cat_ID == "0")
            {
                return CafefCommonHelper.News_BuildUrl(News_ID, Cat_Title, News_Title);
            }
            else
            {
                return CafefCommonHelper.News_BuildUrl(News_ID, Cat_ID, News_Title);
            }
        }

    }

    internal sealed class SQL_News
    {
        #region Nested type: Category

        internal sealed class Category
        {
            internal const string __GetCategoryPath = " ";
        }

        #endregion

        #region Nested type: News

        internal sealed class News
        {
            //Get new,old,other article
            internal const string __EmailIsExit = "EmailIsExit";
            internal const string __GetLinkRelated_Category = "KenhF_GetLinkRelated_Category";
            internal const string __GetLinkRelated_NewsIdList = "KenhF__GetLinkRelated_NewsIdList";

            internal const string __GetListArticleOnPublishDateByCategoryPaging =
                "KenhF__GetArticle_OnPublishDate_ByCategoryEdit";

            internal const string __GetListArticleOnRangeOfPublishDateByCategoryPaging =
                "KenhF__GetArticle_OnRangeOfPublishDate_ByCategoryEdit";

            internal const string __GetNewsByKeywordPaging = "KenhF_GetNewsByKeywordPaging";
            internal const string __GetTopNewByCategory = "KenhF_GetTopNewByCategory";
            //hoanv Update
            //By Categoty
            internal const string __GetTotalPageByCategory = "KenhF_GetTotalPageByCategory";
            //By Categoty And DateTime Public
            internal const string __GetTotalPageByCategoryPublishDate = "KenhF_GetTotalPageByCategory_PublishDate";
            //By Categoty And Range Of DateTime Public
            internal const string __GetTotalPageByCategoryRangeOfPublishDate = "KenhF_GetTotalPageByCategory_RangeOfPublishDate";
            //By strKeyword
            internal const string __GetTotalPageByKeyword = "KenhF_GetTotalPageByKeyword";

            internal const string __IMAGE_FORMAT =
                "<img src='images/Uploaded/Share/{0}/{1}' class='ImgCategory'width='140px' />";

            internal const string __IMAGE_ROOT_PATH = "Images/Uploaded/Share/";
            internal const string __KenhF__GetLinkRelated_NewsIdList_Home = "KenhF__GetLinkRelated_NewsIdList_Home";

            //Conpany Helper
            internal const string __KenhF_CategorỵGetAll = "KenhF_CategorỵGetAll";
            internal const string __KenhF_CountTradeCenter = "KenhF_CountTradeCenter";
            internal const string __KenhF_TradeCenter_All = "KenhF_TradeCenter_All";
            internal const string __KenhF_TradeCenterPagging = "KenhF_TradeCenterPagging";
            internal const string __LatestNews = "KenhF_LatestNews";
            internal const string __ListFocusNews = "KenhF_ListFocusNews";
            internal const string __ListNewsByCategory = "KenhF_GetTopNewByCategory";
            internal const string __ListNewsByCategoryPaging = "KenhF_GetNewByCategor_Paging";
            internal const string __TinIPO = "KenhF_TinIPO";
            internal const string __TopView = "KenhF_TopView";
            internal const string GetFocusNewsByCategory = @"KenhF__GetFocusNewsByCategory";
            internal const string GetListArticleOnPublishDateByCategory = @"KenhF__GetArticle_OnPublishDate_ByCategory";
            internal const string GetListNewArticleByCategory = @"KenhF__GetNewArticleByCategory";
            internal const string GetListOldArticleByCategory = @"KenhF__GetOldArticleByCategory";

            //Register
        }

        #endregion

        #region Nested type: Thead

        internal sealed class Thead
        {
            internal const string __GetNewsListByThread = "KenhF_GetNewsListByThread"; //input: @ThreadId
        }

        #endregion
    }
}