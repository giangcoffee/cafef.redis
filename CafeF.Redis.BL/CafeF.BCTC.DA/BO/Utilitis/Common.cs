using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
namespace CafeF.BCTC.BO.Utilitis
{
    public class Const
    {
        #region Cache SQL
        public const string DATABASE_NAME = "BCTC";
        public const string TABLE_MAPPING = "tblMapping";
        public const string TABLE_CHITIEU = "tblChiTieu";
        public const string TABLE_VALUE = "tblValue";
        public const string TABLE_SYMBOL = "tblSymbol";
        public const string TABLE_SYMBOLTYPE = "tblSymbolType";
        #endregion
    }
    public class Common
    {
        /// <summary>
        ///Resize ảnh theo chiều rộng x chiều cao 
        /// </summary>
        /// <param name="originalUrl">Ảnh gốc</param>
        /// <param name="width">Độ rộng ảnh Thumb</param>
        /// <param name="height">Chiều cao ảnh Thumb</param>
        /// <returns></returns>
        /// 

        public static void FillDataToDropDownList(DropDownList list, DataTable dt, string label, string name, string id)
        {
            list.Items.Clear();
            list.Items.Add(new ListItem(label, "0"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Items.Add(new ListItem(dt.Rows[i][name].ToString().Trim(),
                    dt.Rows[i][id].ToString().Trim()));
            }
        }

        public static string parseValue(string input)
        {
            try
            {
                string value = "";

                string temp = HtmlRemoval.StripTagsRegex(input);
                temp = temp.Replace("\r", "").Trim();
                string[] arr = temp.Split(" ".ToCharArray());
                if (arr.Length > 40)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        value += arr[i] + " ";
                    }
                    value += "...";
                    return value;
                }
                else
                {
                    return temp;
                }
            }
            catch (Exception)
            {
                return input;
            }

        }

        public static string VietNamDateNoDate(DateTime dt)
        {
            string t = dt.DayOfWeek.ToString();
            string ngay = "";

            return dt.Day.ToString() + "/" + dt.Month.ToString() + "/" + dt.Year.ToString() + "  " + dt.Hour.ToString() + ":" + dt.Minute.ToString();// +"(GMT+7)";
        }

        public static string VietNamDateNoTime(DateTime dt)
        {
            string t = dt.DayOfWeek.ToString();
            return "Ngày " + dt.Day.ToString() + " tháng " + dt.Month.ToString() + " năm " + dt.Year.ToString();
        }

        public static string GetDateVN(DateTime _Date)
        {
            if (_Date == null) return "";
            string[] ArrayDay = new string[] { "Chủ nhật", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7" };
            int currDay = (int)_Date.DayOfWeek;

            string CurrDate = String.Format("{0:dd/MM/yyyy}", _Date);
            int currMonth = _Date.Month;
            return ArrayDay[currDay] + ", " + CurrDate + ", " + String.Format("{0:HH:mm}", _Date);
            //return String.Format("{0:dd/MM/yyyy}", _Date);
        }


        public static string SubString(string str)
        {
            string strOutput = "";
            string[] arr = str.Split(" ".ToCharArray());
            int count = arr.Length;
            if (count > 10)
            {
                count = 10;

                for (int i = 0; i < count; i++)
                {
                    strOutput += arr[i] + " ";
                }
                strOutput += "...";
            }
            else
                strOutput = str;

            return strOutput;
        }

        public static string SubString(string str, int length)
        {
            string strOuput = "";
            int strLen = str.Length;
            if (strLen <= length)
            {
                strOuput = str;
            }
            else
            {
                strOuput = str.Substring(0, length);
                strOuput = string.Format("{0}...", strOuput.Substring(0, strOuput.LastIndexOf(" ") + 1));
            }
            return strOuput;
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


        public static bool CheckInt(string Value)
        {
            if (Value == String.Empty)
                return false;
            if (Value == null)
                return false;
            foreach (char c in Value)
            {
                int i;

                i = Convert.ToInt16(c);

                if ((i > 57) || (i < 48))
                    return false;
            }
            return true;
        }

        public static string ProjectUrl(string code, string name)
        {
            string value = "";
            value = "/du-an/" + code + "/" + UnicodeUtility.UnicodeUtility.UnicodeToKoDauAndGach(name) + ".chn";

            return value;
        }

        public static string GenerateChart(double value, double maxValue, double minValue)
        {
            int maxHeight = 24;
            int width = 4;
            double totalValue = 0;
            if (maxValue > 0 && minValue > 0)
            {
                totalValue = maxValue;
            }
            else if (maxValue < 0 && minValue < 0)
            {
                totalValue = -minValue;
            }
            else
            {
                totalValue = (maxValue < 0 ? -maxValue : maxValue) + (minValue < 0 ? -minValue : minValue);
            }

            int max_height_up = (int)(((maxValue > 0 ? maxValue : 0) / totalValue) * maxHeight);
            int max_height_down = (int)(((minValue < 0 ? -minValue : 0) / totalValue) * maxHeight);
            int height = 0;

            StringBuilder chart = new StringBuilder();

            if (value > 0)
            {
                height = (int)((value / maxValue) * max_height_up);
                chart.Append("<td valign='bottom'>");
                chart.Append("<div style='overflow: hidden; background-color: #a4ccf0'><img alt='' style='width: " + width.ToString() + "px;height: " + height.ToString() + "px' src='http://cafef3.vcmedia.vn/images/images/spacer.gif' /></div>");
                chart.Append("<div style='overflow: hidden; background-color: #5493be'><img alt='' style='width: " + width.ToString() + "px;height: 1px' src='http://cafef3.vcmedia.vn/images/images/spacer.gif' /></div>");
                if (max_height_down > 0)
                {
                    chart.Append("<div style='overflow: hidden;'><img alt='' style='width: " + width.ToString() + "px;height: " + max_height_down.ToString() + "px' src='http://cafef3.vcmedia.vn/images/images/spacer.gif' /></div>");
                }
                chart.Append("</td>");
            }
            else if (value < 0)
            {
                height = (int)((value / minValue) * max_height_down);
                chart.Append("<td valign='top'>");
                if (max_height_up > 0)
                {
                    chart.Append("<div style='overflow: hidden;'><img alt='' style='width: " + width.ToString() + "px;height: " + max_height_up.ToString() + "px' src='http://cafef3.vcmedia.vn/images/images/spacer.gif' /></div>");
                }
                chart.Append("<div style='overflow: hidden; background-color: #7f0102'><img alt='' style='width: " + width.ToString() + "px;height: 1px' src='http://cafef3.vcmedia.vn/images/images/spacer.gif' /></div>");
                chart.Append("<div style='overflow: hidden; background-color: #cc0001'><img alt='' style='width: " + width.ToString() + "px;height: " + height.ToString() + "px' src='http://cafef3.vcmedia.vn/images/images/spacer.gif' /></div>");
                chart.Append("</td>");
            }
            else
            {
                chart.Append("<td>");
                if (max_height_up > 0)
                {
                    chart.Append("<div style='overflow: hidden;'><img alt='' style='width: " + width.ToString() + "px;height: " + max_height_up.ToString() + "px' src='http://cafef3.vcmedia.vn/images/images/spacer.gif' /></div>");
                }
                chart.Append("<div style='overflow: hidden; background-color: #5493be'><img alt='' style='width: " + width.ToString() + "px;height: 1px' src='http://cafef3.vcmedia.vn/images/images/spacer.gif' /></div>");
                if (max_height_down > 0)
                {
                    chart.Append("<div style='overflow: hidden;'><img alt='' style='width: " + width.ToString() + "px;height: " + max_height_down.ToString() + "px' src='http://cafef3.vcmedia.vn/images/images/spacer.gif' /></div>");
                }
                chart.Append("</td>");
            }
            return chart.ToString();
        }

        #region Tiếng Việt ko dấu
        private const string KoDauChars ="aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIOOOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU";

        private const string UniChars =           "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";

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
                pos = UniChars.IndexOf(s[i].ToString());
                if (pos >= 0)
                    retVal += KoDauChars[pos];
                else
                    retVal += s[i];
            }
            return retVal;
        }

        public static string DisplayDate(object date)
        {
            try
            {
                var d = (DateTime) date;
                return d.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd") ? d.ToString("HH:mm") : d.ToString("dd/MM");
            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion
    }

    public class CreateLogFiles
    {
        private string sLogFormat;
        private string sErrorTime;

        public CreateLogFiles()
        {
            //sLogFormat used to create log files format :

            // dd/mm/yyyy hh:mm:ss AM/PM ==> Log Message

            sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";

            //this variable used to create log filename format "

            //for example filename : ErrorLogYYYYMMDD

            string sYear = DateTime.Now.Year.ToString();
            string sMonth = DateTime.Now.Month.ToString();
            string sDay = DateTime.Now.Day.ToString();
            sErrorTime = sYear + sMonth + sDay;
        }
        public void ErrorLog(string sPathName, string sErrMsg)
        {
            StreamWriter sw = new StreamWriter(sPathName + "\\Error_" + sErrorTime + ".txt", true);
            sw.WriteLine(sLogFormat + sErrMsg);
            sw.Flush();
            sw.Close();
        }
    }

    public class MetaKeyWord
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="__Page"></param>
        /// <param name="addTitle1">Tieu de bai viet</param>
        /// <param name="addTitle2">Tieu de bai viet khong dau</param>
        /// <param name="addChapo">Chapo cua bai de dua vao the meta des</param>
        /// 

        public static void Set_Page_Header(System.Web.UI.Page __Page, string addTitle, string addDesc, string addKeyword)
        {
            Control __ctlTitle = __Page.Header.FindControl("Title");
            if (__ctlTitle != null)
            {
                //string __newTitle = addTitle1 != "" ? (addTitle1 + (addTitle2 != "" ? " - " + addTitle2 + " - " : " - ") + ConfigurationSettings.AppSettings.Get("AutoTitle").ToString()) : ConfigurationSettings.AppSettings.Get("AutoTitle").ToString();
                string __newTitle = addTitle != "" ? addTitle : "";
                __ctlTitle.Controls.Add(new LiteralControl(__newTitle));
            }
            System.Web.UI.HtmlControls.HtmlMeta __ctlMetaDesc = __Page.Header.FindControl("description") as System.Web.UI.HtmlControls.HtmlMeta;
            if (__ctlMetaDesc != null)
            {
                __ctlMetaDesc.Attributes["content"] = addDesc != "" ? addDesc : "";
            }

            System.Web.UI.HtmlControls.HtmlMeta __ctlKeywords = __Page.Header.FindControl("metakeywords") as System.Web.UI.HtmlControls.HtmlMeta;
            if (__ctlKeywords != null)
            {
                //__ctlKeywords.Attributes["content"] = addKeyword != "" ? addKeyword : "";
                if (addKeyword != null && addKeyword != "")
                    __ctlKeywords.Attributes["content"] = addKeyword;
                else
                    __ctlKeywords.Visible = false;
            }
        }
    }

    public class MetaRobots
    {
        public static void Set_Page_Robots(System.Web.UI.Page __Page, string addTitle)
        {
            System.Web.UI.HtmlControls.HtmlMeta __ctlRobots = __Page.Header.FindControl("idRobots") as System.Web.UI.HtmlControls.HtmlMeta;

            if (addTitle != null)
            {
                //__ctlKeywords.Attributes["content"] = addKeyword != "" ? addKeyword : "";
                if (addTitle != null && addTitle != "")
                    __ctlRobots.Attributes["content"] = addTitle;
                else
                    __ctlRobots.Attributes["content"] = "noindex,nofollow";
            }
        }
    }

    public static class HtmlRemoval
    {
        /// <summary>
        /// Remove HTML from string with Regex.
        /// </summary>
        public static string StripTagsRegex(string source)
        {
            string value = "";
            Regex regex = new Regex(@"</?(\w+)(\s+\w+=(\w+|""[^""]*""|'[^']*'))*>");
            Regex regex2 = new Regex(@"<!--[\d\D]*?-->");
            value = regex2.Replace(regex.Replace(source, ""), "").Replace("\t", "").Replace("\n", " ").Replace((char)10, " "[0]).Replace("  ", " ").Trim();
            //value = Regex.Replace(value, "<.*?>", string.Empty).Replace("&nbsp;", "");
            value = StripTagsCharArray(value);
            return value;
            //return Regex.Replace(source, "<(.|\n)*?>", string.Empty);
            //return Regex.Replace(source, "<.*?>", "<br/>");
        }

        /// <summary>
        /// Compiled regular expression for performance.
        /// </summary>
        static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        /// <summary>
        /// Remove HTML from string with compiled Regex.
        /// </summary>
        public static string StripTagsRegexCompiled(string source)
        {
            return _htmlRegex.Replace(source, string.Empty);
        }

        /// <summary>
        /// Remove HTML tags from string using char array.
        /// </summary>
        public static string StripTagsCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex).Replace("&nbsp;", "").Replace("\r\n", "");
        }
    }
}
