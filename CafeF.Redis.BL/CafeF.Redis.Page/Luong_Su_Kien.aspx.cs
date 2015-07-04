using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CafeF.Redis.BO;
namespace CafeF.Redis.Page
{
    public partial class Luong_Su_Kien : System.Web.UI.Page
    {
        int PageSize = 15;
        int totalPage = 0;
        string urlFormat = "/dong-su-kien-sk{0}/{1}/Trang-{2}.chn";
        string News_Title = "";
        public int ThreadId
        {
            get
            {
                if (Request.QueryString["ThreadId"] != null)
                {
                    return ConvertUtility.ToInt32(Request.QueryString["ThreadId"].ToString());
                }
                return 0;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            int pageIndex = 1;
            try
            {
                pageIndex = Convert.ToInt32(Request.QueryString["page"]);
            }
            catch
            {
                pageIndex = 1;
            }
            if (pageIndex == 0) pageIndex = 1;
            DataTable tbl = new DataTable();
            tbl = NewsHepler_Update.vc_GetAllNewsByThreadId_Paging(ThreadId,pageIndex,PageSize);
            rptFavoriteNews.DataSource = tbl;
            rptFavoriteNews.DataBind();
            NewsHepler_Update.Set_Page_Header(Page, Const.KTTC_TITLE_DES, Const.KTTC_DESCRIPTION, Const.KTTC_KEYWORD);
            totalPage = NewsHepler_Update.vc_GetAllNewsByThreadIdTotalPage(ThreadId);
            if (tbl.Rows.Count > 0)
            {
                rptFavoriteNews.DataSource = tbl;
                rptFavoriteNews.DataBind();
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
                if (pageIndex < totalPage )
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
        }

        protected void rptFavoriteNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Literal ltrNews_Title = e.Item.FindControl("ltrNews_Title") as Literal;
                HyperLink lnkAuthour = e.Item.FindControl("lnkAuthour") as HyperLink;
                Image imgImage = e.Item.FindControl("imgImage") as Image;
                Label lblPublishDate = e.Item.FindControl("lblPublishDate") as Label;
                Literal ltrDescription = e.Item.FindControl("ltrDescription") as Literal;

                DataRowView drvFavoriteNews = e.Item.DataItem as DataRowView;
                News_Title = NewsHepler_Update.UnicodeToKoDauAndGach(drvFavoriteNews["Title"].ToString());
                ltrNews_Title.Text = "<a href=\"" + NewsHepler_Update.BuildLink("News", drvFavoriteNews["News_ID"].ToString(), drvFavoriteNews["News_Title"].ToString(), drvFavoriteNews["Cat_ID"].ToString(), "") + "\" title='" + drvFavoriteNews["News_Title"].ToString() + "' class='titleHotNew'>" + drvFavoriteNews["News_Title"].ToString() + "</a>";
                DateTime pDate = Convert.ToDateTime(drvFavoriteNews["News_PublishDate"]);
                
                lblPublishDate.Text = Convert.ToDateTime(drvFavoriteNews["News_PublishDate"]).ToString("dd/MM/yyyy HH:mm:ss");
                ltrDescription.Text = drvFavoriteNews["News_InitialContent"].ToString();
                if (ltrThreadName.Text == "") ltrThreadName.Text = drvFavoriteNews["Title"].ToString();
                if (CafefCommonHelper.CompareDate(pDate, DateTime.Now) == 0)
                {
                    lblPublishDate.Text += " <img alt=\"\" src=\"/Images/new.gif\" align=\"absmiddle\">";
                }
            }
        }
        protected void rptPage_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Literal ltrPage = e.Item.FindControl("ltrPage") as Literal;

                ListItem page = e.Item.DataItem as ListItem;

                if (page.Selected)
                {
                    ltrPage.Text = page.Text;
                }
                else
                {
                    if (page.Value != "-1")
                    {
                        ltrPage.Text = "<a href=\"" + string.Format(this.urlFormat, ThreadId.ToString(),News_Title, page.Value) + "\">" + page.Text + "</a>";
                    }
                    else
                    {
                    }
                }
            }
        }
    }
}
