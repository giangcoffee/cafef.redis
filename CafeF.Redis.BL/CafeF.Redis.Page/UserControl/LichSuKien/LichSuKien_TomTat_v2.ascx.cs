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

namespace CafeF.Redis.Page.UserControl.LichSuKien
{
    public partial class LichSuKien_TomTat_v2 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ltrMonth.Text = DateTime.Now.Month.ToString();
                this.BindData();
            }
        }

        private void BindData()
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
            List<Entity.LichSuKien> ls;
            try
            {
                ls = LichSuKienBL.get_LichSuKienByTop(10);
                var ss = new List<string>();
                foreach (var item in ls)
                {
                    var s = item.MaCK.Trim().ToUpper();
                    //while (s.StartsWith("&")) { s = s.Remove(0, 1); }
                    //if (s.Contains("&")) s = s.Substring(0, s.IndexOf("&"));
                    s = s.Replace("&", "");
                    if (!ss.Contains(s)) ss.Add(s);
                }
                var ph = StockBL.GetStockCompactInfoMultiple(ss);
                for(var i = 0; i<ls.Count; i++)
                {
                    var o = ls[i];
                    var s = o.MaCK.Trim().ToUpper().Replace("&", "");
                    if(ph.ContainsKey(s) && ph[s]!=null)
                    {
                        o.MaSan = ph[s].TradeCenterId;
                        o.TenCty = ph[s].CompanyName;
                        ls[i] = o;
                    }
                }
            }
            catch
            {
                ls = new List<CafeF.Redis.Entity.LichSuKien>();
            }

            List<CafeF.Redis.Entity.LichSuKien> tblResult = ls;
            if (tblResult != null && tblResult.Count > 0)
            {
                ltrMonth.Text = tblResult[0].EventDate.Month.ToString("#0");
                rptLichSukien.DataSource = tblResult;
                rptLichSukien.DataBind();
            }
        }

        protected void rptLichSukien_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                string urlFormat = "/{0}-{1}/{2}.chn";

                Literal ltrMaCK = e.Item.FindControl("ltrMaCK") as Literal;
                HyperLink lnkNoiDung = e.Item.FindControl("lnkNoiDung") as HyperLink;
                var ltrNgay = e.Item.FindControl("ltrNgay") as Literal;
                CafeF.Redis.Entity.LichSuKien __dr = (CafeF.Redis.Entity.LichSuKien)e.Item.DataItem;
                string symbol = "";

                if (null != __dr.MaCK)
                {
                    symbol = __dr.MaCK.Replace("&", "");
                }
                if(__dr.MaSan==0)
                {
                    ltrMaCK.Text = symbol;
                }else{
                    ltrMaCK.Text = string.Format("<a href='{0}' title='{1}' style='color:#003466'>{2}</a>", Utils.GetSymbolLink(symbol, __dr.TenCty, __dr.MaSan.ToString()), Utils.UnicodeToKoDau(__dr.TenCty).Replace("'", ""), symbol);
                }
                lnkNoiDung.Text = __dr.Title;
                lnkNoiDung.ToolTip = string.IsNullOrEmpty(__dr.TomTat)?__dr.Title:__dr.TomTat;
                lnkNoiDung.NavigateUrl = string.Format(urlFormat, symbol, __dr.News_ID, UnicodeToKoDauAndGach(__dr.Title));
                //if (null != __dr.NgayBatDau)
                //{
                //    ltrNgay.Text = __dr.NgayBatDau.Length > 5 ? __dr.NgayBatDau.Substring(0, 5) : "";
                //}
                ltrNgay.Text = __dr.EventDate.ToString("dd/MM");
            }
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

        public const string KoDauChars =
            "aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIOOOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU";

        public const string uniChars =
            "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";
    }
}