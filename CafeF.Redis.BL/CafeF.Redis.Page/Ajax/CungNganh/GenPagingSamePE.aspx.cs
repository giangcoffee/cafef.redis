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

using CafeF.Redis.Entity;
using CafeF.Redis.BL;
using CafeF.Redis.Page;
using CafeF.Redis.Data;
namespace CafeF.Redis.Page.Ajax.CungNganh
{
    public partial class GenPagingSamePE : System.Web.UI.Page
    {
        private List<StockShortInfo> samepe;
        public List<StockShortInfo> GetSamePE
        {
            get
            {
                try
                {
                    return StockBL.getStockBySymbol((Request["Symbol"]??"").ToUpper()).SamePE;
                }
                catch
                {
                    return new List<StockShortInfo>();
                }
            }
        }
        public int TotalPage
        {
            get
            {
                int totalPage = (samepe.Count % pageCount == 0) ? ((int)samepe.Count / pageCount) : ((int)samepe.Count / pageCount + 1);
                return totalPage;
            }
        }

        public int TotalItem
        {
            get
            {
                return samepe.Count;
            }
        }

        private int pageCount = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            samepe = GetSamePE;
            if (!IsPostBack)
            {
                getGenPaging();
            }
        }
        private void getGenPaging()
        {
            paging.InnerHtml = @"<a style='padding-left=5px' href='javascript:ViewPageNextPreviousPE(-1)'>&lt;</a>&nbsp;";
            for (int i = 1; i <= TotalPage; i++)
            {
                //if (i % 11 == 0)
                //    paging.InnerHtml += "<br />";

                if (i == 1)
                    paging.InnerHtml += "<a class='current' id='aSamePE" + i.ToString() + "' href='javascript:ViewPageSamePEByIndex(" + i.ToString() + ");'>" + i.ToString() + "</a>&nbsp;";
                else
                    paging.InnerHtml += "<a id='aSamePE" + i.ToString() + "' href='javascript:ViewPageSamePEByIndex(" + i.ToString() + ");'>" + i.ToString() + "</a>&nbsp;";
            }
            paging.InnerHtml += "<a href='javascript:ViewPageNextPreviousPE(1)'>&gt;</a>&nbsp;";
        }
    }
}
