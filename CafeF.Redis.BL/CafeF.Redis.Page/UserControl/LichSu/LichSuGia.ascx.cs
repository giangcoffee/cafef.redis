using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;

using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CafeF.Redis.Entity;
using CafeF.Redis.BL;
using CafeF.Redis.Page;


using ServiceStack.Redis.Generic;
using ServiceStack.Redis;
namespace CafeF.Redis.Page.UserControl.LichSu
{
    public partial class LichSuGia : System.Web.UI.UserControl
    {
        public event SendMessageToThePageHandler sendMessageToThePage;
        string __symbol;
        DateTime d1 = DateTime.MinValue;
        DateTime d2 = DateTime.MinValue;
        string Symbol
        {
            get
            {
                return __symbol;
            }
            set { __symbol = value; }
        }
        int Floor_Code = 1;
        private double dlCeiling = 0, dlFloor = 0;

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
            //if (sendMessageToThePage != null)
            //{
            //    sendMessageToThePage(txtKeyword.Text.Trim().ToUpper());
            //}
        }

        private string[] getKeysParts(DateTime d1, DateTime d2)
        {
            List<string> keysParts = new List<string>();
            while (d1 <= d2)
            {
                keysParts.Add(string.Format("{0:MMyyyy}", d1));
                d1 = d1.AddMonths(1);
            }
            return keysParts.ToArray();
        }

        private void FormatColor(Literal ltr, double basicPrice, double closePrice, Literal ltrImage)
        {
            double chgIndex = closePrice - basicPrice;
            double pctIndex = (closePrice + basicPrice) > 0 ? (chgIndex / (basicPrice)) * 100 : 0;

            string ImgUrl = " <img src='http://cafef3.vcmedia.vn/images/Scontrols/Images/LSG/{0}' align='absmiddle'> &nbsp;&nbsp;";
            ltrImage.Text = Math.Round(pctIndex, 1) == 0 ? String.Format(ImgUrl, "nochange_.jpg") : (Math.Round(pctIndex, 1) > 0) ? String.Format(ImgUrl, "up_.gif") : String.Format(ImgUrl, "down_.gif");

            string strChgIndex = String.Format("{0:#,##0.0}", chgIndex) + " (" + String.Format("{0:#,##0.0}", pctIndex) + " %" + ")";
            string styleColor = Math.Round(pctIndex, 1) == 0 ? "<span class=Index_NoChange>" : (Math.Round(pctIndex, 1) > 0) ? "<span class=Index_Up>" : "<span class=Index_Down>";
            if (Math.Round(closePrice, 1) == Math.Round(dlCeiling, 1))
                styleColor = "<span class=Index_Ceiling>";
            else
                if (Math.Round(closePrice, 1) == Math.Round(dlFloor, 1))
                    styleColor = "<span class=Index_Floor>";
            ltr.Text = styleColor + strChgIndex + "</span>";
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
            LoadData(pageNo);
            if (sendMessageToThePage != null)
            {
                sendMessageToThePage(__symbol);
            }
        }

        protected void pager2_Command(object sender, CommandEventArgs e)
        {
            int currnetPageIndx = Convert.ToInt32(e.CommandArgument);
            pager2.CurrentIndex = currnetPageIndx;
            Searching(currnetPageIndx);
        }

        protected void btSearch_Click(object sender, ImageClickEventArgs e)
        {
            Searching(1);
        }

        private void LoadData(int idx)
        {
            if (__symbol == "") return;
            int nTotalItems = 0;
            int isHo = 1;
            try
            {
                switch (__symbol)
                {
                    case "VNINDEX":
                        Floor_Code = 1; break;
                    case "HNX-INDEX":
                        Floor_Code = 2; break;
                    case "UPCOM-INDEX":
                        Floor_Code = 9; break;
                    default:
                        Floor_Code = StockBL.GetStockCompactInfo(__symbol).TradeCenterId;
                        break;
                }
            }
            catch
            {
                Floor_Code = 0;
            }
            if (Floor_Code == 0) return;

            if (Floor_Code == 1 || __symbol.ToUpper().Equals("VNINDEX"))
            {
                notHO.Visible = false;
                divHO.Visible = true;
                isHo = 1;
            }
            else
            {
                notHO.Visible = true;
                divHO.Visible = false;
                isHo = 2;
            }
            d1 = dpkTradeDate1.SelectedDate != DateTime.MinValue ? dpkTradeDate1.SelectedDate : DateTime.MinValue;
            d2 = dpkTradeDate2.SelectedDate != DateTime.MinValue ? dpkTradeDate2.SelectedDate : DateTime.MaxValue;
            if (isHo == 1)
            {
                pager1.CurrentIndex = 0;
                rptData2.DataSource = StockHistoryBL.get_StockHistoryBySymbolAndDate(__symbol.ToUpper(), d1, d2, idx, pager1.PageSize, out nTotalItems);
                rptData2.DataBind();
                pager1.ItemCount = nTotalItems;
                pager2.ItemCount = nTotalItems;
            }
            else if (isHo == 2)
            {
                pager2.CurrentIndex = 0;
                rptData.DataSource = StockHistoryBL.get_StockHistoryBySymbolAndDate(__symbol.ToUpper(), d1, d2, idx, pager2.PageSize, out nTotalItems);
                rptData.DataBind();
                pager2.ItemCount = nTotalItems;
                pager1.ItemCount = nTotalItems;
            }
        }

        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Literal ltrAveragePrice = e.Item.FindControl("ltrAveragePrice") as Literal;
                Literal ltrChange = e.Item.FindControl("ltrChange") as Literal;
                Literal ltrImage = e.Item.FindControl("ltrImage") as Literal;
                StockHistory __dr = (StockHistory)e.Item.DataItem;
                HtmlTableCell __cell = e.Item.FindControl("avgPriceItem") as HtmlTableCell;
                HtmlTableCell __basicPriceItem = e.Item.FindControl("BasicPriceColItem") as HtmlTableCell;

                if ((Floor_Code != 1) && (__symbol.ToLower() != "vnindex" && __symbol.ToLower() != "hnx-index" && __symbol.ToLower() != "upcom-index")) //san ha noi va upcom
                {
                    avgPriceHeader.Attributes.Add("style", "");
                    ltrAveragePrice.Text = String.Format("{0:#,###.0}", __dr.AveragePrice);
                    __cell.Attributes.Add("style", "");

                }
                else//Hose
                {
                    __cell.Attributes.Add("style", "display:none");
                    avgPriceHeader.Attributes.Add("style", "display:none");
                }

                double basicPrice = ConvertUtility.ToDouble(__dr.BasicPrice);
                if (__symbol.ToLower() == "vnindex" || __symbol.ToLower() == "hnx-index" || __symbol.ToLower() == "upcom-index")
                {
                    __basicPriceItem.Attributes.Add("style", "display:none");
                    BasicPriceColHeader.Attributes.Add("style", "display:none");
                }
                else
                {
                    __basicPriceItem.Attributes.Add("style", "");
                    BasicPriceColHeader.Attributes.Add("style", "");
                }
                double closePrice = ConvertUtility.ToDouble(__dr.ClosePrice);

                if ((Floor_Code != 1 && (__symbol.ToLower() != "vnindex" && __symbol.ToLower() != "hnx-index")) || Floor_Code == 11) //san ha noi and upcom 
                {
                    closePrice = ConvertUtility.ToDouble(__dr.AveragePrice);
                    if (closePrice <= 0)
                        closePrice = ConvertUtility.ToDouble(__dr.ClosePrice);
                }
                if (closePrice == 0) basicPrice = 0;
                double.TryParse(__dr.Ceiling.ToString(), out dlCeiling);
                double.TryParse(__dr.Floor.ToString(), out dlFloor);
                FormatColor(ltrChange, basicPrice, closePrice, ltrImage);
            }
        }

        protected void rptData2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Literal ltrChange = e.Item.FindControl("ltrChange1") as Literal;
                Literal ltrImage = e.Item.FindControl("ltrImage1") as Literal;
                StockHistory __dr = (StockHistory)e.Item.DataItem;

                double basicPrice = ConvertUtility.ToDouble(__dr.BasicPrice);
                double closePrice = ConvertUtility.ToDouble(__dr.ClosePrice);

                if (closePrice == 0) basicPrice = 0;
                double.TryParse(__dr.Ceiling.ToString(), out dlCeiling);
                double.TryParse(__dr.Floor.ToString(), out dlFloor);
                FormatColor(ltrChange, basicPrice, closePrice, ltrImage);
            }
        }

    }
}