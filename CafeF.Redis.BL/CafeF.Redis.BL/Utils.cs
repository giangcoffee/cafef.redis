using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace CafeF.Redis.BL
{
    public class Utils
    {
        /// <summary>
        /// Kiểm tra giờ giao dịch theo từng sàn
        /// </summary>
        /// <param name="centerId"></param>
        /// <returns></returns>
        public static bool InTradingTime(int centerId)
        {
            var tradingDate = ConfigurationManager.AppSettings["TradeDayInWeek"] ?? "1,2,3,4,5";
            var holiday = ConfigurationManager.AppSettings["Holiday"] ?? "02/09";
            DateTime startTime, endTime;
            if (!DateTime.TryParseExact(ConfigurationManager.AppSettings["StartTradeTime_" + centerId] ?? "083000", "HHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out startTime)) startTime = new DateTime(0, 0, 0, 8, 30, 0);
            if (!DateTime.TryParseExact(ConfigurationManager.AppSettings["EndTradeTime_" + centerId] ?? "110000", "HHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out endTime)) endTime = new DateTime(0, 0, 0, 11, 0, 0);

            if (("," + holiday + ",").Contains("," + DateTime.Now.ToString("dd/MM") + ",")) return false;
            if (!("," + tradingDate + ",").Contains("," + (int)DateTime.Now.DayOfWeek + ",")) return false;
            if (int.Parse(DateTime.Now.ToString("HHmmss")) < int.Parse(startTime.ToString("HHmmss"))) return false;
            if (int.Parse(DateTime.Now.ToString("HHmmss")) > int.Parse(endTime.ToString("HHmmss"))) return false;
            return true;
        }
        /// <summary>
        /// Lấy giờ đóng cửa của từng sàn
        /// </summary>
        /// <param name="centerId"></param>
        /// <returns></returns>
        public static DateTime GetCloseTime(int centerId)
        {
            DateTime endTime;
            if (!DateTime.TryParseExact(ConfigurationManager.AppSettings["EndTradeTime_" + centerId] ?? "110000", "HHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out endTime)) endTime = new DateTime(0, 0, 0, 11, 0, 0);
            return endTime;
        }
        /// <summary>
        /// Lấy folder rewrite của từng sàn
        /// </summary>
        /// <param name="centerId"></param>
        /// <returns></returns>
        public static string GetCenterFolder(string centerId)
        {
            switch (centerId)
            {
                case "9":
                    return "upcom";
                case "8":
                    return "otc";
                case "2":
                    return "hastc";
                case "1":
                default:
                    return "hose";
            }
        }
        /// <summary>
        /// Lấy mã sàn theo ID
        /// </summary>
        /// <param name="centerId"></param>
        /// <returns></returns>
        public static string GetCenterName(string centerId)
        {
            switch (centerId)
            {
                case "9":
                    return "Upcom";
                case "8":
                    return "OTC";
                case "2":
                    return "HNX";
                case "1":
                    return "HSX";
                default:
                    return "";
            }
        }
        /// <summary>
        /// Lấy link trang doanh nghiệp
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="name"></param>
        /// <param name="centerId"></param>
        /// <returns></returns>
        public static string GetSymbolLink(string symbol, string name, string centerId)
        {
            return string.Format("/{0}/{1}-{2}.chn", GetCenterFolder(centerId), symbol, Hepler.UnicodeToKoDauAndGach(name));
        }
        /// <summary>
        /// Add or alter meta tags on page header
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="page">Current page</param>
        public static void AddMetaTag(string name, string value, Page page)
        {
            if(page==null) return;
            var head = page.Header;
            foreach (Control ctrl in head.Controls)
            {
                if (ctrl.GetType() != typeof(HtmlMeta)) continue;
                if (((HtmlMeta)ctrl).Name.ToUpper() != name.ToUpper()) continue;
                ((HtmlMeta)ctrl).Content = value;
                return;
            }
            head.Controls.Add(new HtmlMeta { Name = name, Content = value });
        }
        /// <summary>
        /// Get kby.js folder on ftp server
        /// </summary>
        /// <returns></returns>
        public static string GetKbyFolder()
        {
            var folder = StockBL.GetKbyFolder();
            return string.IsNullOrEmpty(folder) ? "" : (folder + "/");
        }
        /// <summary>
        /// Get merged image link (chart on header)
        /// </summary>
        /// <returns></returns>
        public static string GetHeaderChartLink()
        {
            var t = double.Parse(DateTime.Now.ToString("HHmm"));
            var rd = new Random();
            return (t >= 830 && t <= 1130) ? ("/chart4.aspx?rd=" + rd.Next()) : ("/chartindex/merge/cafefchart3.png?d=" + (t>1100?DateTime.Now:DateTime.Now.AddDays(-1)).ToString("yyyyMMdd") );
        }
        public static string GetDuLieuChartLink()
        {
            var t = double.Parse(DateTime.Now.ToString("HHmm"));
            var rd = new Random();
            return (t >= 830 && t <= 1130) ? ("/chartdulieu.aspx?rd=" + rd.Next()) : ("/chartindex/merge/cafefchart.dulieu.gif?d=" + (t > 1100 ? DateTime.Now : DateTime.Now.AddDays(-1)).ToString("yyyyMdd"));
        }
        public static string Serialize(DataTable dt)
        {
            var ds = new DataSet("DS");
            ds.Tables.Add(dt);
            string result;
            using (var sw = new StringWriter())
            {
                ds.WriteXml(sw);
                result = sw.ToString();
            }
            return result;
        }
        public static DataTable Deserialize(string s)
        {
            var reader = new StringReader(s);
            var dt = new DataSet();
            dt.ReadXml(reader);
            return dt.Tables[0];
        }
        public static string GetCategoryName(string catid)
        {
            string cat_name = "Trang chủ";
            switch (catid)
            {
                case "1001":
                    cat_name = "Danh mục đầu tư";
                    break;
                case "1006":
                    cat_name = "Đọc nhanh";
                    break;
                case "1003":
                    cat_name = "Ý kiến đọc giả";
                    break;
                case "1004":
                    cat_name = "Luồng sự kiện";
                    break;
                case "0":
                    cat_name = "Trang chủ";
                    break;
                case "1005":
                    cat_name = "Thị trường niêm yết";
                    break;
                case "2010":
                    cat_name = "Thống kê";
                    break;
                case "1115":
                    cat_name = "Lịch sử giao dịch";
                    break;
                case "1117":
                    cat_name = "BCTCFull";
                    break;
                case "31":
                    cat_name = "Thị trường chứng khoán";
                    break;
                case "32":
                    cat_name = "Tài chính quốc tế";
                    break;
                case "33":
                    cat_name = "Kinh tế vĩ mô - Đầu tư ";
                    break;
                case "34":
                    cat_name = "Tài chính - ngân hàng";
                    break;
                case "35":
                    cat_name = "Bất động sản";
                    break;
                case "36":
                    cat_name = "Doanh nghiệp";
                    break;
                case "39":
                    cat_name = "Hàng hóa - Nguyên liệu";
                    break;
                case "40":
                    cat_name = "Kinh doanh - Doanh nhân";
                    break;
                case "41":
                    cat_name = "Doanh nghiệp giới thiệu";
                    break;
                case "42":
                    cat_name = "Hội nghị - Hội thảo";
                    break;
                case "43":
                    cat_name = "Thị trường đầu tư (BĐS)";
                    break;
                case "44":
                    cat_name = "Chính sách quy hoạch (BDS)";
                    break;
                case "45":
                    cat_name = "Tin tức dự án (BĐS)";
                    break;
                case "46":
                    cat_name = "Phong cách (BĐS)";
                    break;


            }
            return cat_name;
        }

        public static string UnicodeToKoDauAndGach(string s)
        {
            return Hepler.UnicodeToKoDauAndGach(s);
        }
        public static string UnicodeToKoDau(string s)
        {
            return Hepler.UnicodeToKoDau(s);
        }
        public static string SubStringSpace(string str, int length, string extend)
        {
            String[] Content_Array = str.Split(' ');
            int count_spacer = Content_Array.Length;
            string temp = "";
            string value = "";
            if (count_spacer > length)
            {
                temp = String.Empty;
                for (int j = 0; j <= length; j++)
                    temp += Content_Array[j] + " ";
                value = temp + extend;
            }
            else
            {
                value = str;
            }

            return value;
        }
    }
    class Hepler
    {
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
    }
}
