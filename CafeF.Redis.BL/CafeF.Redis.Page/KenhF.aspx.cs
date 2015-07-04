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
using CafeF.Redis.BO;
using KenhF.Common;
using System.Data.SqlClient;
using CafeF.Redis.Entity;
using CafeF.Redis.BL;
using CafeF.Redis.Page;
namespace CafeF.Redis.Page
{
    public partial class KenhF : System.Web.UI.Page
    {
        private string chartUpdatedDate;
        public string ChartUpdatedDate { get { return chartUpdatedDate; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            //chartUpdatedDate = MarketHelper.GetChartFolder("INDEX");
            TradeCenterStats dtHoIndex = TradeCenterBL.getByTradeCenter((int)TradeCenter.HoSE);
            TradeCenterStats dtHaIndex = TradeCenterBL.getByTradeCenter((int)TradeCenter.HaSTC);

            if (IsPostBack) return;
            this.Page.Title = "Dữ liệu tài chính, doanh nghiệp, thị trường, công cụ khai thác | CafeF.vn";
            Utils.AddMetaTag("Keywords", Const.TTNY_KEYWORD, Page);
            Utils.AddMetaTag("Description", Const.TTNY_DESCRIPTION, Page);

            //NewsHepler_Update.Set_Page_Header(Page, "Dữ liệu tài chính, doanh nghiệp, thị trường, công cụ khai thác | CafeF.vn", string.Format(Const.TTNY_DESCRIPTION, lastUpdate), Const.TTNY_KEYWORD);

            //DataTable dtHaIndex, dtHoIndex;
            //lay tu memcache

            if (dtHaIndex != null)
            {
                chartUpdatedDate = dtHaIndex.ChartFolder;
                ltrLastUpdate.Text = NewsHelper.GetDateVN1(dtHaIndex.CurrentDate);
                var chgIndex = ConvertUtility.ToDouble(dtHaIndex.CurrentIndex) - ConvertUtility.ToDouble(dtHaIndex.PrevIndex);
                var chgPercent = chgIndex / dtHaIndex.PrevIndex * 100;
                var img = "no_change";
                var cls = "NoChange";
                var sign = "";
                if (chgIndex > 0)
                {
                    img = "btup";
                    cls = "Up";
                    sign = "+";
                }
                if (chgIndex < 0)
                {
                    img = "btdown";
                    cls = "Down";
                }
                ltrHnxIndex.Text = string.Format("<span style='color: #666; font-weight: bold;' class='idx_{6}'>{0}</span><img align='absmiddle' style='margin: 0 3px;' border='0' src='http://cafef3.vcmedia.vn/images/{1}.gif' class='img_{6}' /><span class='Index_{2}' id='idxd_{6}'><span class='idc_{6}'>{3}{4}</span> (<span class='idp_{6}'>{3}{5}%</span>)</span>", dtHaIndex.CurrentIndex.ToString("#,##0.00"), img, cls, sign, chgIndex.ToString("#,##0.00"), chgPercent.ToString("#,##0.00"), 2);
                ltrHnxVolume.Text = ConvertUtility.ToDouble(dtHaIndex.CurrentVolume).ToString("#,##0");
                ltrHnxValue.Text = ConvertUtility.ToDouble(dtHaIndex.CurrentValue).ToString("#,##0.0");
                ltrHnxForeignVol.Text = dtHaIndex.ForeignNetVolume.ToString("#,##0");
                ltrHnxUp.Text = dtHaIndex.Up.ToString("#,##0");
                ltrHnxCeiling.Text = dtHaIndex.Ceiling.ToString("#,##0");
                ltrHnxNormal.Text = dtHaIndex.Normal.ToString("#,##0");
                ltrHnxDown.Text = dtHaIndex.Down.ToString("#,##0");
                ltrHnxFloor.Text = dtHaIndex.Floor.ToString("#,##0");
            }

            if (dtHoIndex != null)
            {
                chartUpdatedDate = dtHoIndex.ChartFolder;
                ltrLastUpdate.Text = NewsHelper.GetDateVN1(dtHoIndex.CurrentDate);
                var chgIndex = ConvertUtility.ToDouble(dtHoIndex.CurrentIndex) - ConvertUtility.ToDouble(dtHoIndex.PrevIndex);
                var chgPercent = chgIndex / dtHoIndex.PrevIndex * 100;
                var img = "no_change";
                var cls = "NoChange";
                var sign = "";
                if (chgIndex > 0)
                {
                    img = "btup";
                    cls = "Up";
                    sign = "+";
                }
                if (chgIndex < 0)
                {
                    img = "btdown";
                    cls = "Down";
                }
                ltrVnIndex.Text = string.Format("<span style='color: #666; font-weight: bold;' class='idx_{6}'>{0}</span><img align='absmiddle' style='margin: 0 3px;' border='0' src='http://cafef3.vcmedia.vn/images/{1}.gif' class='img_{6}' /><span class='Index_{2}' id='idxd_{6}'><span class='idc_{6}'>{3}{4}</span> (<span class='idp_{6}'>{3}{5}%</span>)</span>", dtHoIndex.CurrentIndex.ToString("#,##0.00"), img, cls, sign, chgIndex.ToString("#,##0.00"), chgPercent.ToString("#,##0.00"), 1);
                ltrVnVol.Text = ConvertUtility.ToDouble(dtHoIndex.CurrentVolume).ToString("#,##0");
                ltrVnVal.Text = ConvertUtility.ToDouble(dtHoIndex.CurrentValue).ToString("#,##0.0");
                ltrVnForeignVol.Text = dtHoIndex.ForeignNetVolume.ToString("#,##0");
                ltrVnUp.Text = dtHoIndex.Up.ToString("#,##0");
                ltrVnCeiling.Text = dtHoIndex.Ceiling.ToString("#,##0");
                ltrVnNormal.Text = dtHoIndex.Normal.ToString("#,##0");
                ltrVnDown.Text = dtHoIndex.Down.ToString("#,##0");
                ltrVnFloor.Text = dtHoIndex.Floor.ToString("#,##0");
            }


            if (Utils.InTradingTime((int)TradeCenter.HoSE))
            {
                lblHoSE_State.Text = "Đang giao dịch";
                lblHoSE_State.CssClass = "";
            }
            else
            {
                lblHoSE_State.Text = "Đóng cửa";
                lblHoSE_State.CssClass = "NoteText";
            }
            if (Utils.InTradingTime((int)TradeCenter.HaSTC))
            {
                lblHaSTC_State.Text = "Đang giao dịch";
                lblHaSTC_State.CssClass = "";
            }
            else
            {
                lblHaSTC_State.Text = "Đóng cửa";
                lblHaSTC_State.CssClass = "NoteText";
            }
            GetVNIndex();
        }

        public enum TradeCenter
        {
            HoSE = 1,
            HaSTC = 2
        }

        private void GetVNIndex()
        {
            //using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["VincomscPriceBoardConnectionString"].ConnectionString))
            //{
            //    using (SqlCommand cmd = new SqlCommand("tblHCMIndex_GetIndex", cnn))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;

            //        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            //        {
            //            DataTable dtVNIndex = new DataTable();

            //            adapter.Fill(dtVNIndex);
            //DataTable dtVNIndex = new DataTable();
            var dtVNIndex = TradeCenterBL.getByTradeCenter((int)TradeCenter.HoSE);
            if (dtVNIndex == null) return;
            if (ConvertUtility.ToDouble(dtVNIndex.Index1) > 0)
            {
                lblVNIndex1.Text = ConvertUtility.ToDouble(dtVNIndex.Index1).ToString("#,##0.0#");
                double change = ConvertUtility.ToDouble(dtVNIndex.Index1) - ConvertUtility.ToDouble(dtVNIndex.PrevIndex);
                double changePercent = change * 100 / ConvertUtility.ToDouble(dtVNIndex.PrevIndex);

                if (change > 0)
                {
                    imgVNIndex1.ImageUrl = "http://cafef3.vcmedia.vn/images/btup.gif";
                    lblVNIndexChange1.CssClass = "Index_Up";
                    lblVNIndexChange1.Text = "+" + change.ToString("#,##0.0#") + " (+" + changePercent.ToString("#,##0.0#") + "%)";
                }
                else if (change < 0)
                {
                    imgVNIndex1.ImageUrl = "http://cafef3.vcmedia.vn/images/btdown.gif";
                    lblVNIndexChange1.CssClass = "Index_Down";
                    lblVNIndexChange1.Text = change.ToString("#,##0.0#") + " (" + changePercent.ToString("#,##0.0#") + "%)";
                }
                else
                {
                    imgVNIndex1.ImageUrl = "http://cafef3.vcmedia.vn/images/no_change.jpg";
                    lblVNIndexChange1.CssClass = "Index_NoChange";
                    lblVNIndexChange1.Text = "0 (0%)";
                }
                lblHoKLGD1.Text = ConvertUtility.ToDouble(dtVNIndex.Volume1).ToString("#,##0");
                lblHoGTGD1.Text = ConvertUtility.ToDouble(dtVNIndex.Value1).ToString("#,##0.0#");

                tblVNIndex1.Visible = true;
            }

            if (ConvertUtility.ToDouble(dtVNIndex.Index2) > 0)
            {
                lblVNIndex2.Text = ConvertUtility.ToDouble(dtVNIndex.Index2).ToString("#,##0.0#");
                double change = ConvertUtility.ToDouble(dtVNIndex.Index2) - ConvertUtility.ToDouble(dtVNIndex.PrevIndex);
                double changePercent = change * 100 / ConvertUtility.ToDouble(dtVNIndex.PrevIndex);

                if (change > 0)
                {
                    imgVNIndex2.ImageUrl = "http://cafef3.vcmedia.vn/images/btup.gif";
                    lblVNIndexChange2.CssClass = "Index_Up";
                    lblVNIndexChange2.Text = "+" + change.ToString("#,##0.0#") + " (+" + changePercent.ToString("#,##0.0#") + "%)";
                }
                else if (change < 0)
                {
                    imgVNIndex2.ImageUrl = "http://cafef3.vcmedia.vn/images/btdown.gif";
                    lblVNIndexChange2.CssClass = "Index_Down";
                    lblVNIndexChange2.Text = change.ToString("#,##0.0#") + " (" + changePercent.ToString("#,##0.0#") + "%)";
                }
                else
                {
                    imgVNIndex2.ImageUrl = "http://cafef3.vcmedia.vn/images/no_change.jpg";
                    lblVNIndexChange2.CssClass = "Index_NoChange";
                    lblVNIndexChange2.Text = "0 (0%)";
                }
                lblHoKLGD2.Text = ConvertUtility.ToDouble(dtVNIndex.Volume2).ToString("#,##0");
                lblHoGTGD2.Text = ConvertUtility.ToDouble(dtVNIndex.Value2).ToString("#,##0.0#");

                tblVNIndex2.Visible = true;
            }

            if (ConvertUtility.ToDouble(dtVNIndex.Index3) > 0)
            {
                lblVNIndex3.Text = ConvertUtility.ToDouble(dtVNIndex.Index3).ToString("#,##0.0#");
                double change = ConvertUtility.ToDouble(dtVNIndex.Index3) - ConvertUtility.ToDouble(dtVNIndex.PrevIndex);
                double changePercent = change * 100 / ConvertUtility.ToDouble(dtVNIndex.PrevIndex);

                if (change > 0)
                {
                    imgVNIndex3.ImageUrl = "http://cafef3.vcmedia.vn/images/btup.gif";
                    lblVNIndexChange3.CssClass = "Index_Up";
                    lblVNIndexChange3.Text = "+" + change.ToString("#,##0.0#") + " (+" + changePercent.ToString("#,##0.0#") + "%)";
                }
                else if (change < 0)
                {
                    imgVNIndex3.ImageUrl = "http://cafef3.vcmedia.vn/images/btdown.gif";
                    lblVNIndexChange3.CssClass = "Index_Down";
                    lblVNIndexChange3.Text = change.ToString("#,##0.0#") + " (" + changePercent.ToString("#,##0.0#") + "%)";
                }
                else
                {
                    imgVNIndex3.ImageUrl = "http://cafef3.vcmedia.vn/images/no_change.jpg";
                    lblVNIndexChange3.CssClass = "Index_NoChange";
                    lblVNIndexChange3.Text = "0 (0%)";
                }
                lblHoKLGD3.Text = ConvertUtility.ToDouble(dtVNIndex.Volume3).ToString("#,##0");
                lblHoGTGD3.Text = ConvertUtility.ToDouble(dtVNIndex.Value3).ToString("#,##0.0#");

                tblVNIndex3.Visible = true;
            }

            //            dtVNIndex.Dispose();
            //        }
            //    }
            //}
        }
    }
}
