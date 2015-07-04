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
namespace CafeF.Redis.Page.UserControl.StockView
{
    public partial class TradeInfo : System.Web.UI.UserControl
    {
        private int tradeCenterId = 1;
        private Stock stock;
        public string __Symbol = "";
        public string CurrrentTrade = "2";

        DateTime tradingDate = DateTime.Now.Date;
        private string symbolFolder = "";
        public string SymbolFolder { get { return symbolFolder; } set { symbolFolder = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            __Symbol = (Request.QueryString["symbol"] ?? "").Trim().ToUpper();
            if (__Symbol == "") return;
            __Symbol = Server.UrlDecode(__Symbol);
            __Symbol = __Symbol.Replace(",", "");
            try
            {
                stock = ((Stock)((CafeF.Redis.Page.MasterPage.StockMain)(((ContentPlaceHolder)this.Parent).Page).Master).GetStock);
            }
            catch (Exception)
            {
                stock = StockBL.getStockBySymbol(__Symbol);
            }
            if (stock == null) return;
            if (IsPostBack) return;
            lblStatus.Text = stock.StatusText;
            lblStatus.Visible = !string.IsNullOrEmpty(stock.StatusText);
            symbolFolder = stock.FolderImage;
            if(stock.PrevTradeInfo==null || stock.PrevTradeInfo.Count==0)
            {
                divFirstInfo.Visible = false;
            }else
            {
                var temp = "<tr><td style=\"color:#343434; background: url(http://cafef3.vcmedia.vn/images/images/top_6px_dot.gif) no-repeat transparent top left; padding-left: 10px;\">Giao dịch đầu tiên tại <b>{0}</b>:</td><td style=\"font-weight: bold; color: #004370; text-align:right;\">{1}</td></tr><tr><td style=\"color:#343434;\">Với Khối lượng (cp):</td><td style=\"font-weight: bold; text-align:right;\">{2}</td></tr><tr><td style=\"color:#343434;\">Giá đóng cửa trong ngày (nghìn đồng):</td><td style=\"font-weight: bold; text-align:right;\">{3}</td></tr><tr><td style=\"color:#343434;\">Ngày giao dịch cuối cùng:</td><td style=\"font-weight: bold; color: #004370; text-align:right;\">{4}</td></tr>";
                var tret = "<table width='100%' border='0' cellspacing='0' cellpadding='0'>";
                foreach (var info in stock.PrevTradeInfo)
                {
                    tret += string.Format(temp, info.Floor, info.FirstDate.ToString("dd/MM/yyyy"), info.FirstVolume.ToString("#,##0"), info.FirstPrice.ToString("#,##0.0"), info.EndDate.ToString("dd/MM/yyyy"));
                }
                tret += "</table>";
                ltrFirstInfo.Text = tret;
                divFirstInfo.Visible = true;
            }
            GenData(stock.CompanyProfile.basicInfos);
            GenDataPrice(__Symbol);

        }
        private void GenDataPrice(string symbol)
        {

            StockPrice pr = StockBL.getStockPriceBySymbol(symbol);
            if (pr == null) return;
            ltrBasicPrice.Text = ConvertUtility.ToDouble(pr.RefPrice) != 0 ? String.Format("{0:#,##0.0}", ConvertUtility.ToDouble(pr.RefPrice)) : "0.0";
            ltrOpenPrice.Text = ConvertUtility.ToDouble(pr.OpenPrice) != 0 ? String.Format("{0:#,##0.0}", ConvertUtility.ToDouble(pr.OpenPrice)) : "0.0";
            ltrHighestPrice.Text = ConvertUtility.ToDouble(pr.HighPrice) != 0 ? String.Format("{0:#,##0.0}", ConvertUtility.ToDouble(pr.HighPrice)) : "0.0";
            ltrLowerPrice.Text = ConvertUtility.ToDouble(pr.LowPrice) != 0 ? String.Format("{0:#,##0.0}", ConvertUtility.ToDouble(pr.LowPrice)) : "0.0";
            ltrClosePrice.Text = ConvertUtility.ToDouble(pr.ClosePrice) != 0 ? String.Format("{0:#,##0.0}", ConvertUtility.ToDouble(pr.ClosePrice)) : "0.0";
            ltrKhoiLuong.Text = ConvertUtility.ToDouble(pr.Volume) != 0 ? String.Format("{0:#,##}", ConvertUtility.ToDouble(pr.Volume)) : "0.0";

            //ltrRoomNNConlai.Text = ConvertUtility.ToDouble(pr.ForeignCurrentRoom / 10000) != 0 ? String.Format("{0:#,##}", ConvertUtility.ToDouble(pr.ForeignCurrentRoom / 10000)) : "0.0";
            var totalroom = pr.ForeignTotalRoom > 0 ? pr.ForeignTotalRoom : ((stock.IsBank ? 0.3 : 0.49) * stock.CompanyProfile.basicInfos.basicCommon.VolumeTotal);
            if (totalroom > 0)
                ltrRoomNNConlai.Text = pr.ForeignCurrentRoom > totalroom ? "100" : ((pr.ForeignCurrentRoom / totalroom) * 100).ToString("#,##0.#");
            else
                ltrRoomNNConlai.Text = "0";

            try
            {
                var inTrading = Utils.InTradingTime(tradeCenterId);
                ltrGDNNTitle.Text = inTrading ? "GDNN (KL Mua)" : "GD ròng NĐTNN";
                var foreign = Utils.InTradingTime(tradeCenterId) ? pr.ForeignBuyVolume : pr.ForeignNetVolume;
                ltrGDNN.Text = ConvertUtility.ToDouble(foreign) != 0 ? String.Format("{0:#,##}", ConvertUtility.ToDouble(foreign)) : "0";
                //ltrDateTime.Text = "Cập nhật lúc " + String.Format("{0:HH:mm}", DateTime.Now) + " " + Hepler.GetDateVN(DateTime.Now);
                var last = pr.LastTradeDate;
                ltrDateTime.Text = "Cập nhật lúc " + String.Format("{0:HH:mm}", ((inTrading && double.Parse(last.ToString("yyyyMMdd")) < double.Parse(DateTime.Now.ToString("yyyyMMdd"))) ? last : Utils.GetCloseTime(tradeCenterId))) + " " + Hepler.GetDateVN(last);

                ltrCurentIndex.Text = String.Format("{0:#,##0.0}", ConvertUtility.ToDouble(pr.Price));
                //lblChange.Text = String.Format("{0}({1}%)", String.Format("{0:#,##0.0}", dChange),ConvertUtility.ToDouble(pr.Price)!=0? String.Format("{0:#,##0.0}",dChange*(double) 100.0 / ConvertUtility.ToDouble(pr.Price)):"0.0");

                //double price = (tradeCenterId == 1 || inTrading) ? pr.ClosePrice : pr.AvgPrice;
                double dChange = Math.Round(pr.Price - pr.RefPrice, 2); //ConvertUtility.ToDouble(price) - ConvertUtility.ToDouble(pr.RefPrice);
                //double chgIndex = price - pr.RefPrice;

                string strChgIndex = String.Format("&nbsp; {0} ({1}%)", String.Format("{0:#,##0.0}", dChange), pr.RefPrice != 0 ? String.Format("{0:#,##0.0}", Math.Round(dChange * (double)100.0 / pr.RefPrice, 1)) : "0.0");

                string icon = "dltlu-nochange";
                string color = "orange";
                if (dChange < 0)
                {
                    icon = "dltlu-down";
                    color = "red";
                }
                if (dChange > 0)
                {
                    color = "green";
                    icon = "dltlu-up";
                }

                if (pr.CeilingPrice > 0 && Math.Round(pr.Price, 1) >= Math.Round(pr.CeilingPrice, 1))
                {
                    color = "pink";
                }
                else
                    if (pr.FloorPrice > 0 && Math.Round(pr.Price, 1) <= Math.Round(pr.FloorPrice, 1))
                    {
                        color = "blue";
                    }

                lblChange.Text = "<div class='" + icon + " " + color + "'>" + strChgIndex + "</div>";
            }
            catch (Exception ex)
            {

                Response.Write(ex);
            }
        }

        private void GenData(BasicInfo basic)
        {
            if (!int.TryParse(basic.TradeCenter, out tradeCenterId)) tradeCenterId = 1;
            FirstInfo first = ((basic == null) ? (new FirstInfo()) : basic.firstInfo);
            BasicCommon common = ((basic == null) ? (new BasicCommon()) : basic.basicCommon);
            GenDataFirstInfo(first);
            GenDataCommonInfo(common);
            ltrTraCoTuc.Text = GenTraCoTuc();

        }

        private void GenDataFirstInfo(FirstInfo first)
        {
            if (first == null) return;
            ltrNgayGiaoDich.Text = first.FirstTrade == null ? "" : first.FirstTrade.Value.ToString("dd/MM/yyyy");
            ltrFirstPrice.Text = String.Format("{0:#,##0.0}", ConvertUtility.ToDouble(first.FirstPrice));
            ltrFirstVolume.Text = String.Format("{0:#,##}", ConvertUtility.ToDouble(first.FirstVolume));
        }

        private void GenDataCommonInfo(BasicCommon common)
        {
            ltrEPS.Text = "N/A";
            ltrPE.Text = "N/A";
            ltrPB.Text = "N/A";
            ltrCovarian.Text = "N/A";
            ltrKLTrungBinh20Ngay.Text = "N/A";
            ltrTotalCPLuuHanh.Text = "N/A";
            ltrMarketcap.Text = "N/A";
            ltrEPSDate.Text = "Tổng LNST 4Q âm hoặc chưa đủ số liệu tính";

            if (common == null) return;
            ltrEPS.Text = common.EPS <= 0 ? "N/A" : common.EPS.ToString("N2");
            ltrPE.Text = common.PE <= 0 ? "N/A" : common.PE.ToString("N2");
            ltrPB.Text = common.ValuePerStock == 0 ? "N/A" : common.ValuePerStock.ToString("N2");
            ltrCovarian.Text = common.Beta == 0 ? "N/A" : common.Beta.ToString("N2");
            ltrKLTrungBinh20Ngay.Text = common.AverageVolume == 0 ? "N/A" : common.AverageVolume.ToString("N0");
            ltrTotalCPLuuHanh.Text = common.OutstandingVolume == 0 ? "N/A" : common.OutstandingVolume.ToString("N0");
            ltrMarketcap.Text = common.TotalValue == 0 ? "N/A" : common.TotalValue.ToString("N2");
            ltrEPSDate.Text = common.EPS > 0 ? string.Format("Số liệu EPS tính tới {0}", common.EPSFullDate) : "Tổng LNST 4Q âm hoặc chưa đủ số liệu tính";
            pnEPSNote.Visible = true;
            pnEPS.Visible = true;
            try
            {
                if (stock.IsCCQ)
                {
                    pnEPSNote.Visible = false;
                    pnEPS.Visible = false;
                    pnCCQ.Visible = true;
                    pnCCQDate.Visible = true;

                    ltrCCQv3.Text = common.CCQv3.ToString("#,##0.00");
                    ltrCCQv6.Text = common.CCQv6.ToString("#,##0.00");
                    ltrCCQDate.Text = common.CCQdate.ToString("dd/MM/yyyy");

                }
            }
            catch (Exception) { }
        }

        private string GenTraCoTuc()
        {

            string strTraCoTuc = "";
            List<DividendHistory> ltTraCoTuc = stock.DividendHistorys;
            if (ltTraCoTuc.Count == 0) return "";
            string truoc = "";
            string sau = "";
            for (int i = 0; i < ltTraCoTuc.Count; i++)
            {
                DividendHistory dh = (DividendHistory)ltTraCoTuc[i];
                truoc = String.Format("{0:dd/MM/yyyy}", dh.NgayGDKHQ);
                if (truoc.Equals(sau))
                {
                    strTraCoTuc += "&thinsp;&ensp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
                else
                {
                    strTraCoTuc += "- ";
                    strTraCoTuc += "<b>" + String.Format("{0:dd/MM/yyyy}", dh.NgayGDKHQ) + "</b>" + ": ";
                }
                strTraCoTuc += dh.SuKien.ToString().Trim();
                //string test = dtTraCoTuc.Rows[i]["SuKien"].ToString().Trim().ToLower();
                if (!dh.SuKien.ToString().Trim().ToLower().Equals("bán ưu đãi"))
                {
                    strTraCoTuc += " bằng " + dh.DonViDoiTuong.ToString();
                }
                strTraCoTuc += ", tỷ lệ " + dh.TiLe.ToString() + "";
                if (!dh.GhiChu.ToString().Trim().Equals(""))
                {
                    strTraCoTuc += ", giá " + dh.GhiChu.ToString().Trim();
                }
                strTraCoTuc += "<br />";
                sau = String.Format("{0:dd/MM/yyyy}", dh.NgayGDKHQ);
            }
            strTraCoTuc += "";
            return strTraCoTuc;
        }
    }
}