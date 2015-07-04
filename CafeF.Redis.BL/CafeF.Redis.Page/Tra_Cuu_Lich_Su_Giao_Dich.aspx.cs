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
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
namespace CafeF.Redis.Page
{
    public partial class Tra_Cuu_Lich_Su_Giao_Dich : System.Web.UI.Page
    {
        public String Symbol = "All";
        protected void Page_Load(object sender, EventArgs e)
        {
            form1.Action = Request.RawUrl; 
            Control ctl = null;
           
            int TabID = 1;
            if (Request.Params["Tab"] != null)
                TabID = ConvertUtility.ToInt32(Request.Params["Tab"]);
            string symbol = Request.QueryString["Symbol"] != null ? Request.QueryString["Symbol"] : "";
            if (symbol != "")
            {
                this.Symbol = symbol;
            }
            switch (TabID)
            {
                case 1:
                    {
                        this.Title = symbol + " : Lịch sử giao dịch | CafeF.vn";
                        this.Meta1.Content = "Lịch sử biến động " + (symbol.ToLower().Contains("index") ? ("chỉ số và khối lượng giao dịch trên sàn " + symbol) : ("giá và khối lượng giao dịch cổ phiếu " + symbol)) + " theo thời gian";
                        ltrTitle.Text = "LỊCH SỬ GIÁ";
                        divDataHistory.Attributes.Add("class", "cf_ResearchDataHistory_Tab1_Sel");
                        ctl = LoadControl("~/UserControl/LichSu/LichSuGia.ascx");
                        ((CafeF.Redis.Page.UserControl.LichSu.LichSuGia)ctl).sendMessageToThePage += delegate(string message)
                        {
                            ltrTitle.Text = "LỊCH SỬ GIÁ / Mã CK " + message;
                            aLSG.HRef = "/Lich-su-giao-dich-" + message + "-1.chn";
                            aTKL.HRef = "/Lich-su-giao-dich-" + message + "-2.chn";
                            aNDTNG.HRef = "/Lich-su-giao-dich-" + message + "-3.chn";
                            aCDL.HRef = "/Lich-su-giao-dich-" + message + "-4.chn";
                            aCPQ.HRef = "/Lich-su-giao-dich-" + message + "-5.chn";
                        };
                        LabelUpdatePanel.Update();
                        LinkUpdatePanel.Update();
                        break;
                    }
                case 2:
                    {
                        this.Title = symbol + " : Thống kê đặt lệnh | CafeF.vn";
                        this.Meta1.Content = "Thống kê khối lượng lệnh đặt mua, đặt bán " + (symbol.ToLower().Contains("index") ? ("trên sàn " + symbol) : ("của cổ phiếu " + symbol)) + " theo thời gian";
                        ltrTitle.Text = "THỐNG KÊ ĐẶT LỆNH";
                        divDataHistory.Attributes.Add("class", "cf_ResearchDataHistory_Tab1");
                        divCungCau.Attributes.Add("class", "cf_ThongKeDatLenh_Selected");
                        ctl = LoadControl("~/UserControl/LichSu/CungCau.ascx");
                        ((CafeF.Redis.Page.UserControl.LichSu.CungCau)ctl).sendMessageToThePage += delegate(string message)
                        {
                            ltrTitle.Text = "THỐNG KÊ ĐẶT LỆNH / Mã CK " + message;
                            aLSG.HRef = "/Lich-su-giao-dich-" + message + "-1.chn";
                            aTKL.HRef = "/Lich-su-giao-dich-" + message + "-2.chn";
                            aNDTNG.HRef = "/Lich-su-giao-dich-" + message + "-3.chn";
                            aCDL.HRef = "/Lich-su-giao-dich-" + message + "-4.chn";
                            aCPQ.HRef = "/Lich-su-giao-dich-" + message + "-5.chn";
                        };
                        LabelUpdatePanel.Update();
                        LinkUpdatePanel.Update();
                        break;
                    }
                case 3:
                    {
                        this.Title = symbol + " : Giao dịch Nhà đầu tư nước ngoài | CafeF.vn";
                        this.Meta1.Content = "Thống kê khối lượng và giá trị giao dịch nhà đầu tư nước ngoài " + (symbol.ToLower().Contains("index") ? ("trên sàn " + symbol) : ("của cổ phiếu " + symbol)) + " theo thời gian";
                        ltrTitle.Text = "GDNDT NƯỚC NGOÀI";
                        divDataHistory.Attributes.Add("class", "cf_ResearchDataHistory_Tab1");
                        divGDNN.Attributes.Add("class", "cf_ResearchDataHistory_Selected");
                        ctl = LoadControl("~/UserControl/LichSu/GiaoDichNuocNgoai.ascx");
                        ((CafeF.Redis.Page.UserControl.LichSu.GiaoDichNuocNgoai)ctl).sendMessageToThePage += delegate(string message)
                        {
                            ltrTitle.Text = "GDNDT NƯỚC NGOÀI / Mã CK " + message;
                            aLSG.HRef = "/Lich-su-giao-dich-" + message + "-1.chn";
                            aTKL.HRef = "/Lich-su-giao-dich-" + message + "-2.chn";
                            aNDTNG.HRef = "/Lich-su-giao-dich-" + message + "-3.chn";
                            aCDL.HRef = "/Lich-su-giao-dich-" + message + "-4.chn";
                            aCPQ.HRef = "/Lich-su-giao-dich-" + message + "-5.chn";
                        };
                        LabelUpdatePanel.Update();
                        LinkUpdatePanel.Update();
                        break;
                    }
                case 4:
                    {
                        this.Title = symbol + " : Giao dịch Cổ đông lớn và Cổ đông nội bộ | CafeF.vn";
                        this.Meta1.Content = "Thống kê giao dịch của các Cổ đông lớn và Cổ đông nội bộ " + "của cổ phiếu " + symbol + " theo thời gian";
                        ltrTitle.Text = "GD CỔ ĐÔNG LỚN & CỔ ĐÔNG NỘI BỘ";
                        divDataHistory.Attributes.Add("class", "cf_ResearchDataHistory_Tab1");
                        divLocalTrans.Attributes.Add("class", "cf_Codonglon_Selected");
                        ctl = LoadControl("~/UserControl/LichSu/GiaoDichNoiBo.ascx");
                        ((CafeF.Redis.Page.UserControl.LichSu.GiaoDichNoiBo)ctl).sendMessageToThePage += delegate(string message)
                        {
                            ltrTitle.Text = "GD CỔ ĐÔNG LỚN & CỔ ĐÔNG NỘI BỘ / " + message;
                            string s = message;
                            if (s.LastIndexOf("/") >= 0)
                                s = message.Substring(message.LastIndexOf("/") + 8).Trim();
                            aLSG.HRef = "/Lich-su-giao-dich-" + s  + "-1.chn";
                            aTKL.HRef = "/Lich-su-giao-dich-" + s + "-2.chn";
                            aNDTNG.HRef = "/Lich-su-giao-dich-" + s + "-3.chn";
                            aCDL.HRef = "/Lich-su-giao-dich-" + s + "-4.chn";
                            aCPQ.HRef = "/Lich-su-giao-dich-" + s + "-5.chn";
                        };
                        LabelUpdatePanel.Update();
                        LinkUpdatePanel.Update();
                        break;
                    }
                case 5:
                    {
                        this.Title = symbol + " : Giao dịch Cổ phiếu quỹ | CafeF.vn";
                        this.Meta1.Content = "Thống kê giao dịch cổ phiếu quỹ đối với của cổ phiếu " + symbol + " theo thời gian";
                        ltrTitle.Text = "GD CỔ PHIẾU QUỸ";
                        divDataHistory.Attributes.Add("class", "cf_ResearchDataHistory_Tab1");
                        divMBLFinal.Attributes.Add("class", "cf_ResearchDataHistory_Selected");
                        ctl = LoadControl("~/UserControl/LichSu/CoPhieuQuy.ascx");
                        ((CafeF.Redis.Page.UserControl.LichSu.CoPhieuQuy)ctl).sendMessageToThePage += delegate(string message)
                        {
                            ltrTitle.Text = "GD CỔ PHIẾU QUỸ / Mã CK " + message;
                            aLSG.HRef = "/Lich-su-giao-dich-" + message + "-1.chn";
                            aTKL.HRef = "/Lich-su-giao-dich-" + message + "-2.chn";
                            aNDTNG.HRef = "/Lich-su-giao-dich-" + message + "-3.chn";
                            aCDL.HRef = "/Lich-su-giao-dich-" + message + "-4.chn";
                            aCPQ.HRef = "/Lich-su-giao-dich-" + message + "-5.chn";
                        };
                        LabelUpdatePanel.Update();
                        LinkUpdatePanel.Update();
                        break;
                    }
                default:
                    this.Title = symbol + " : Lịch sử giao dịch | CafeF.vn";
                    this.Meta1.Content = "Lịch sử biến động " + (symbol.ToLower().Contains("index") ? ("chỉ số và khối lượng giao dịch trên sàn " + symbol) : ("giá và khối lượng giao dịch cổ phiếu " + symbol)) + " theo thời gian";
                    divDataHistory.Attributes.Add("class", "cf_ResearchDataHistory_Tab1_Sel");
                    ctl = LoadControl("~/UserControl/LichSu/LichSuGia.ascx");
                    break;
            }
            pldContent.Controls.Clear();
            pldContent.Controls.Add(ctl);
           
        }

        //protected override void Render(HtmlTextWriter writer)
        //{
        //    if (this.Request.Headers["X-MicrosoftAjax"] != "Delta=true")
        //    {
        //        Regex reg = new Regex(@"<script[^>]*>[\w|\t|\r|\W]*?</script>");
        //        StringBuilder sb = new StringBuilder();
        //        StringWriter sw = new StringWriter(sb);
        //        HtmlTextWriter hw = new HtmlTextWriter(sw);
        //        base.Render(hw);
        //        string html = sb.ToString();
        //        MatchCollection mymatch = reg.Matches(html);
        //        html = reg.Replace(html, string.Empty);
        //        reg = new Regex(@"(?<=[^])\t{2,}|(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,11}(?=[<])|(?=[\n])\s{2,}|(?=[\r])\s{2,}");
        //        html = reg.Replace(html, string.Empty);
        //        reg = new Regex(@"</body>");
        //        string str = string.Empty;
        //        foreach (Match match in mymatch)
        //        {
        //            str += match.ToString();
        //        }
        //        html = reg.Replace(html, str + "</body>");
        //        writer.Write(html);
        //    }
        //    else
        //        base.Render(writer);
        //}
    }
}
