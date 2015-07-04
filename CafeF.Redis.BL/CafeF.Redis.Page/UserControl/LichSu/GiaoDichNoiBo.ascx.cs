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
namespace CafeF.Redis.Page.UserControl.LichSu
{
    public partial class GiaoDichNoiBo : System.Web.UI.UserControl
    {
        private Stock stock;
        private double slcplh = 0;
        private double tyle = 0;
        public event SendMessageToThePageHandler sendMessageToThePage;
        string __symbol;
        DateTime d1 = DateTime.MinValue;
        DateTime d2 = DateTime.MinValue;
        string Symbol
        {
            get
            {
                if (Request.QueryString["Symbol"] != null)
                {
                    return Request.QueryString["Symbol"];
                }
                return "";
            }
        }
        string __CompanyName = string.Empty;
        public string CompanyName
        {
            set { __CompanyName = value; }
            get { return __CompanyName; }
        }
        public const string __tit_Symbol = "Gõ mã CK hoặc Tên công ty";
        public const string __tit_ToChuc = "Gõ tên tổ chức/cá nhân";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["Symbol"] != null)
                    txtKeyword.Text = ConvertUtility.ToString(Request.Params["Symbol"]).ToUpper();
                if (txtKeyword.Text.Trim() == "ALL") txtKeyword.Text = "";
                //dpkTradeDate2.SelectedDate = DateTime.Now;
                //dpkTradeDate1.SelectedDate = DateTime.Now.AddMonths(-1);
                Searching(1);
            }
        }

        protected void pager1_Command(object sender, CommandEventArgs e)
        {
            int currnetPageIndx = Convert.ToInt32(e.CommandArgument);
            pager1.CurrentIndex = currnetPageIndx;
            Searching(currnetPageIndx);
        }

        protected void Searching(int pageNo)
        {
            __symbol = txtKeyword.Text.Trim().ToUpper();
            if (txtKeyword2.Text == "") txtKeyword2.Text = Request["ShareHolderId"]??"";
            LoadData(pageNo);
        }

        private void LoadData(int idx)
        {
            if (sendMessageToThePage != null)
            {
                sendMessageToThePage(__symbol);
            }
            int nTotalItems = 0;
            List<InternalHistory> histories;
            if (txtKeyword2.Text == "")
            {
                d1 = dpkTradeDate1.SelectedDate != DateTime.MinValue ? dpkTradeDate1.SelectedDate : DateTime.MinValue;
                d2 = dpkTradeDate2.SelectedDate != DateTime.MinValue ? dpkTradeDate2.SelectedDate : DateTime.MaxValue;
                #region test
                //InternalHistory h = new InternalHistory();
                //h.HolderID = "1";
                //h.PlanBeginDate = DateTime.Now;
                //h.PlanBuyVolume = 2;
                //h.PlanEndDate = DateTime.Now;
                //h.PlanSellVolume = 2;
                //h.PublishedDate = DateTime.Now;
                //h.RealBuyVolume = 3;
                //h.RealEndDate = DateTime.Now;
                //h.RealSellVolume = 4;
                //h.RelatedMan = "sdfsdf";
                //h.RelatedManPosition = "sdfsdf";
                //h.Stock = "AAA";
                //h.TransactionMan = "dsfds";
                //h.TransactionManPosition = "sdfds";
                //h.TransactionNote = "23ropijdsjf.";
                //h.VolumeAfterTransaction = 33333;
                //h.VolumeBeforeTransaction = 444;

                //List<InternalHistory> his = new List<InternalHistory>();
                //his.Add(h);
                #endregion

                histories = StockHistoryBL.get_InternalHistoryBySymbolAndDate(__symbol.ToUpper(), d1, d2, idx, pager1.PageSize, out nTotalItems);
            }
            else
            {
                histories = StockHistoryBL.get_InternalHistoryByHolder(txtKeyword2.Text, idx, pager1.PageSize, out nTotalItems);
            }
            var infos = new Dictionary<string, double>();
            for (var i = 0; i < histories.Count; i++ )
            {
                var h = histories[i];
                if(!infos.ContainsKey(h.Stock))
                {
                    var s = StockBL.getStockBySymbol(h.Stock);
                    infos.Add(h.Stock, s==null?0:s.CompanyProfile.commonInfos.OutstandingVolume);
                }
                h.TyLeSoHuu = infos[h.Stock] > 0 ? ((double)h.VolumeAfterTransaction / infos[h.Stock] * 100) : 0;
                histories[i] = h;
            }
            rptData.DataSource = histories;
            rptData.DataBind();
            pager1.ItemCount = nTotalItems;
        }

        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                HtmlTableRow __tr;
                HtmlTableCell __td1;
                HtmlTableCell __td2;
                HtmlTableCell __td3;
                HtmlTableCell __td4;
                HtmlTableCell __td5;
                HtmlTableCell __td6;
                HtmlTableCell __td7;
                if (e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    __tr = e.Item.FindControl("altitemTR") as HtmlTableRow;
                    __td1 = e.Item.FindControl("a_td1") as HtmlTableCell;
                    __td2 = e.Item.FindControl("a_td2") as HtmlTableCell;
                    __td3 = e.Item.FindControl("a_td3") as HtmlTableCell;
                    __td4 = e.Item.FindControl("a_td4") as HtmlTableCell;
                    __td5 = e.Item.FindControl("a_td5") as HtmlTableCell;
                    __td6 = e.Item.FindControl("a_td6") as HtmlTableCell;
                    __td7 = e.Item.FindControl("a_td7") as HtmlTableCell;

                }
                else
                {
                    __tr = e.Item.FindControl("itemTR") as HtmlTableRow;
                    __td1 = e.Item.FindControl("e_td1") as HtmlTableCell;
                    __td2 = e.Item.FindControl("e_td2") as HtmlTableCell;
                    __td3 = e.Item.FindControl("e_td3") as HtmlTableCell;
                    __td4 = e.Item.FindControl("e_td4") as HtmlTableCell;
                    __td5 = e.Item.FindControl("e_td5") as HtmlTableCell;
                    __td6 = e.Item.FindControl("e_td6") as HtmlTableCell;
                    __td7 = e.Item.FindControl("e_td7") as HtmlTableCell;
                }
                InternalHistory __dr = (InternalHistory)e.Item.DataItem;
                if (__tr != null && __dr != null)
                {
                    if (__dr.RealEndDate == null)
                    {
                        __tr.Attributes.Add("style", "border-right: solid 1px #e6e6e6;vertical-align: top; border-bottom: solid 1px #e6e6e6;" + ConfigurationManager.AppSettings["Mau"].ToString());
                        __td1.Attributes.Add("style", "border-right: solid 1px #e6e6e6;vertical-align: top; border-bottom: solid 1px #e6e6e6;" + ConfigurationManager.AppSettings["Mau"].ToString());
                        __td2.Attributes.Add("style", "border-right: solid 1px #e6e6e6;vertical-align: top; border-bottom: solid 1px #e6e6e6;" + ConfigurationManager.AppSettings["Mau"].ToString());
                        __td3.Attributes.Add("style", "border-right: solid 1px #e6e6e6;vertical-align: top; border-bottom: solid 1px #e6e6e6;" + ConfigurationManager.AppSettings["Mau"].ToString());
                        __td4.Attributes.Add("style", "border-right: solid 1px #e6e6e6;vertical-align: top; border-bottom: solid 1px #e6e6e6;" + ConfigurationManager.AppSettings["Mau"].ToString());
                        __td5.Attributes.Add("style", "border-right: solid 1px #e6e6e6;vertical-align: top; border-bottom: solid 1px #e6e6e6;" + ConfigurationManager.AppSettings["Mau"].ToString());
                        __td6.Attributes.Add("style", "border-right: solid 1px #e6e6e6;vertical-align: top; border-bottom: solid 1px #e6e6e6;" + ConfigurationManager.AppSettings["Mau"].ToString());
                        __td7.Attributes.Add("style", "border-right: solid 1px #e6e6e6;vertical-align: top; border-bottom: solid 1px #e6e6e6;" + ConfigurationManager.AppSettings["Mau"].ToString());
                    }
                }

                Literal ltrTyLe = e.Item.FindControl("ltrTyLe") as Literal;
                ltrTyLe.Text = __dr.TyLeSoHuu.ToString("#0.00");
                //try
                //{
                //    stock = StockBL.getStockBySymbol(__dr.Stock);
                //    slcplh = stock.CompanyProfile.basicInfos.basicCommon.OutstandingVolume;
                //}
                //catch (Exception) { }
                //if (slcplh != 0)
                //{
                //    try
                //    {
                //        tyle = (__dr.VolumeAfterTransaction / slcplh) * 100;
                //    }
                //    catch (Exception) { }

                //    ltrTyLe.Text = String.Format("{0:0.##}", tyle);
                //}
            }
        }

        protected void ibtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            txtType.Value = "1";
            Searching(1);
        }

        protected void ibtnSearch2_Click(object sender, ImageClickEventArgs e)
        {
            txtType.Value = "2";
            Searching(1);
        }

        protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ViewHolder")
            {
                LinkButton lkbutton = (LinkButton)e.CommandSource;
                txtType.Value = "2";
                txtKeyword2.Text = e.CommandArgument.ToString();
                Searching(1);
                if (sendMessageToThePage != null)
                {
                    sendMessageToThePage(lkbutton.Text + " / Mã CK " + __symbol);
                }
            }
        }
    }
}