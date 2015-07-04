using System;
using System.Data;
using System.Configuration;
using System.Web;

using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
namespace CafeF.Redis.BO
{
    public class TextUtil
    {
        public static string KeyImgRemove = ConfigurationSettings.AppSettings["KeyImgRemove"].ToString();
        public static string KeyImgInsert = ConfigurationSettings.AppSettings["KeyImgInsert"].ToString();

        public static List<string> parseValue(string input, string pattern)
        {
            List<string> returnValue = new List<string>();
            MatchCollection MatchList = Regex.Matches(input, pattern, RegexOptions.IgnoreCase);
            foreach (Match match in MatchList) returnValue.Add(match.Value);
            return returnValue;
        }

        public static bool CheckSrc(string input, string pattern)
        {
            //string returnValue ;
            Match match = Regex.Match(input, pattern, RegexOptions.IgnoreCase);
            //foreach (Match match in MatchList) returnValue.Add(match.Value);
            if (match.Success)
            {
                return true;
            }
            else
                return false;
        }


        public static string getResponseString(string url)
        {
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = httpRequest.GetResponse();
            if (response == null) return "";
            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    sb.Append(line);
                }
            }
            response.Close();
            return sb.ToString();
        }

        public static string RemoveCData(string str)
        {
            return str.Replace("<![CDATA[", "").Replace("]]>", "");
        }

        public static string GetStringBetween(string Str, string Seq, string SeqEnd)
        {
            string Orgi = Str;
            try
            {
                Str = Str.ToLower();
                Seq = Seq.ToLower();
                SeqEnd = SeqEnd.ToLower();

                int i = Str.IndexOf(Seq);

                if (i < 0)
                    return "";

                i = i + Seq.Length;

                int j = Str.IndexOf(SeqEnd, i);
                int end;

                if (j > 0) end = j - i;
                else end = Str.Length - i;

                return Orgi.Substring(i, end);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static List<string> GetListStringBetween(string input, string start, string end)
        {
            string orgInput = input;
            input = input.ToLower();
            start = start.ToLower();
            end = end.ToLower();
            List<string> list = new List<string>();
            try
            {
                int index = 0;
                while (index >= 0)
                {
                    index = input.IndexOf(start);
                    if (index < 0)
                        break;

                    index += start.Length;
                    int length;

                    int j = input.IndexOf(end, index);
                    if (j > 0)
                    {
                        length = j - index;
                        list.Add(orgInput.Substring(index, length));
                        if (j + end.Length < input.Length)
                        {
                            input = input.Substring(j + end.Length);
                            orgInput = orgInput.Substring(j + end.Length);
                        }
                        else break;
                    }
                    else
                        break;
                }
                return list;
            }
            catch
            {
                return null;
            }
        }

        public static string Md5Hash(string strURL)
        {
            byte[] buffer = System.Security.Cryptography.MD5.Create().ComputeHash(Encoding.Default.GetBytes(strURL.ToLower()));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.AppendFormat("{0:x2}", buffer[i]);
            }
            return builder.ToString();
        }

        public static string GetDomainFromUrl(string url)
        {
            url = url.ToLower();
            int ind = url.IndexOf("//");
            if (ind <= 0)
                return url;//.Replace("www.", "");
            int index = url.IndexOf("/", ind + 2);
            if (index <= 0)
                return url;//.Replace("www.", "");
            else
                return url.Substring(0, index + 1);//.Replace("www.", "");
        }

        public static string UTF8Convert(string input)
        {
            string[] strUTF8 = {    "à", "á", "ả", "ã", "ạ", 
                                    "ă", "ằ", "ắ", "ẳ", "ẵ", "ặ", 
                                    "â", "ầ", "ấ", "ẩ", "ẫ", "ậ", 

                                    "đ", 
                                    
                                    "è", "é", "ẻ", "ẽ", "ẹ",
                                    "ê", "ề", "ế", "ể", "ễ", "ệ",
                                    
                                    "ò", "ó", "ỏ", "õ", "ọ",
                                    "ô", "ồ", "ố", "ổ", "ỗ", "ộ",
                                    "ơ", "ờ", "ớ", "ở", "ỡ", "ợ",
                                    
                                    "ù", "ú", "ủ", "ũ", "ụ",
                                    "ư", "ừ", "ứ", "ử", "ữ", "ự",
                                    
                                    "À", "Á", "Ả", "Ã", "Ạ",
                                    "Â", "Ầ", "Ấ", "Ẩ", "Ẫ", "Ậ",
                                    "Ă", "Ằ", "Ắ", "Ẳ", "Ẵ", "Ặ",
                                    
                                    "Đ",

                                    "È", "É", "Ẻ", "Ẽ", "Ẹ",
                                    "Ê", "Ề", "Ế", "Ể", "Ễ", "Ệ",

                                    "Ò", "Ó", "Ỏ", "Õ", "Ọ",
                                    "Ô", "Ồ", "Ố", "Ổ", "Ỗ", "Ộ",
                                    "Ơ", "Ờ", "Ớ", "Ở", "Ỡ", "Ợ",

                                    "Ù", "Ú", "Ủ", "Ũ", "Ụ",
                                    "Ư", "Ừ", "Ứ", "Ử", "Ữ", "Ự",

                                    "ì", "í", "ỉ", "ĩ", "ị",
                                    "Ì", "Í", "Ỉ", "Ĩ", "Ị",

                                    "ỳ", "ý", "ỷ", "ỹ", "ỵ",
                                    "Ỳ", "Ý", "Ỷ", "Ỹ", "Ỵ" };

            string[] strPI = {      "\\u00e0", "\\u00e1", "\\u1ea3", "\\u00e3", "\\u1ea1",
                                    "\\u0103", "\\u1eb1", "\\u1eaf", "\\u1eb3", "\\u1eb5", "\\u1eb7",
                                    "\\u00e2", "\\u1ea7", "\\u1ea5", "\\u1ea9", "\\u1eab", "\\u1ead",

                                    "\\u0111",

                                    "\\u00e8", "\\u00e9",  "\\u1ebb",  "\\u1ebd", "\\u1eb9",
                                    "\\u00ea", "\\u1ec1", "\\u1ebf", "\\u1ec3", "\\u1ec5", "\\u1ec7",
                                        
                                    "\\u00f2", "\\u00f3", "\\u1ecf", "\\u00f5", "\\u1ecd",
                                    "\\u00f4", "\\u1ed3", "\\u1ed1", "\\u1ed5", "\\u1ed7", "\\u1ed9",
                                    "\\u01a1", "\\u1edd", "\\u1edb", "\\u1edf", "\\u1ee1", "\\u1ee3",

                                    "\\u00f9", "\\u00fa", "\\u1ee7", "\\u0169", "\\u1ee5",
                                    "\\u01b0", "\\u1eeb", "\\u1ee9", "\\u1eed", "\\u1eef", "\\u1ef1",

                                    "\\u00c0", "\\u00c1", "\\u1ea2", "\\u00c3", "\\u1ea0",
                                    "\\u0102", "\\u1eb0", "\\u1eae", "\\u1eb2", "\\u1eb4", "\\u1eb6",
                                    "\\u00c2", "\\u1ea6", "\\u1ea4", "\\u1ea8", "\\u1eaa", "\\u1eac",

                                    "\\u0110",

                                    "\\u00c8", "\\u00c9", "\\u1eba", "\\u1ebc", "\\u1eb8",
                                    "\\u00ca", "\\u1ec0", "\\u1ebe", "\\u1ec2", "\\u1ec4", "\\u1ec6",

                                    "\\u00d2", "\\u00d3", "\\u1ece", "\\u00d5", "\\u1ec6",
                                    "\\u00d4", "\\u1ed2", "\\u1ed0", "\\u1ed4", "\\u1ed6", "\\u1ed8",
                                    "\\u01a0", "\\u1edc", "\\u1eda", "\\u1ede", "\\u1ee0", "\\u1ee2",

                                    "\\u00d9", "\\u00da", "\\u1ee6", "\\u0168", "\\u1ee4",
                                    "\\u01af", "\\u1eea", "\\u1ee8", "\\u1eec", "\\u1eee", "\\u1ef0",

                                    "\\u00ec", "\\u00ed", "\\u1ec9", "\\u0129", "\\u1ecb",
                                    "\\u00cc", "\\u00cd", "\\u1ec8", "\\u0128", "\\u1eca",

                                    "\\u1ef3", "\\u00fd", "\\u1ef7", "\\u1ef9", "\\u1ef5",
                                    "\\u1ef2", "\\u00dd Ỷ", "\\u1ef6", "\\u1ef8", "\\u1ef4" };
            string strResult = input;
            int total = strUTF8.Length;
            for (int i = 0; i < total; i++)
            {
                strResult = strResult.Replace(strPI[i], strUTF8[i]);
            }
            return strResult;
        }

        public static string UTF81252Convert(string input, bool flow)
        {
            string strUTF8Literal, strDecimal, strResult;
            string[] arrUTF8Literal, arrDecimal;
            int intTotal, intCount;

            strUTF8Literal = "" +
             "à á ả ã ạ À Á Ả Ã Ạ â ầ ấ ẩ ẫ ậ Â Ầ Ấ Ẩ Ẫ Ậ ă ằ ắ ẳ ẵ ặ Ă Ằ Ắ Ẳ Ẵ Ặ " +
             "ò ó ỏ õ ọ Ò Ó Ỏ Õ Ọ ô ồ ố ổ ỗ ộ Ô Ồ Ố Ổ Ỗ Ộ ơ ờ ớ ở ỡ ợ Ơ Ờ Ớ Ở Ỡ Ợ " +
             "è é ẻ ẽ ẹ È É Ẻ Ẽ Ẹ ê ề ế ể ễ ệ Ê Ề Ế Ể Ễ Ệ " +
             "ù ú ủ ũ ụ Ù Ú Ủ Ũ Ụ ư ừ ứ ử ữ ự Ư Ừ Ứ Ử Ữ Ự " +
             "ì í ỉ ĩ ị Ì Í Ỉ Ĩ Ị ỳ ý ỷ ỹ ỵ Ỳ Ý Ỷ Ỹ Ỵ đ Đ " +
             "Đ –";

            arrUTF8Literal = strUTF8Literal.Split(' ');

            strDecimal = "" +
            "&#224; &#225; &#7843; &#227; &#7841; &#192; &#193; &#7842; &#195; &#7840; &#226; &#7847; &#7845; &#7849; &#7851; &#7853; &#194; &#7846; &#7844; &#7848; &#7850; &#7852; &#259; &#7857; &#7855; &#7859; &#7861; &#7863; &#258; &#7856; &#7854; &#7858; &#7860; &#7862; " +
            "&#242; &#243; &#7887; &#245; &#7885; &#210; &#211; &#7886; &#213; &#7884; &#244; &#7891; &#7889; &#7893; &#7895; &#7897; &#212; &#7890; &#7888; &#7892; &#7894; &#7896; &#417; &#7901; &#7899; &#7903; &#7905; &#7907; &#416; &#7900; &#7898; &#7902; &#7904; &#7906; " +
            "&#232; &#233; &#7867; &#7869; &#7865; &#200; &#201; &#7866; &#7868; &#7864; &#234; &#7873; &#7871; &#7875; &#7877; &#7879; &#202; &#7872; &#7870; &#7874; &#7876; &#7878; " +
            "&#249; &#250; &#7911; &#361; &#7909; &#217; &#218; &#7910; &#360; &#7908; &#432; &#7915; &#7913; &#7917; &#7919; &#7921; &#431; &#7914; &#7912; &#7916; &#7918; &#7920; " +
            "&#236; &#237; &#7881; &#297; &#7883; &#204; &#205; &#7880; &#296; &#7882; &#7923; &#253; &#7927; &#7929; &#7925; &#7922; &#221; &#7926; &#7928; &#7924; &#273; &#272; " +
            "&#208; &#45;";

            arrDecimal = strDecimal.Split(' ');

            strResult = input;
            intTotal = arrDecimal.Length;

            if (flow)
            {
                //UTF8 Literal --> HTML Decimal
                for (intCount = 0; intCount < intTotal; intCount++)
                    strResult = strResult.Replace(arrUTF8Literal[intCount], arrDecimal[intCount]);
            }
            else
            {
                //HTML Decimal --> UTF8 Literal
                for (intCount = 0; intCount < intTotal; intCount++)
                    strResult = strResult.Replace(arrDecimal[intCount], arrUTF8Literal[intCount]);
            }

            return strResult;
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
