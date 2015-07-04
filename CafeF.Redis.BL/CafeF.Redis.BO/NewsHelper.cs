using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//using Portal.Core.DAL;
using System.Xml;
using System.Collections;
using KenhF.Common;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
namespace CafeF.Redis.BO
{
    //update lai viec lay tin
    public static class NewsHelper
    {
        #region "Common functions"
        public static int ConvertToInt(string Value)
        {
            try
            {
                if (Value == string.Empty) return 0;
                return Convert.ToInt32(Value);
            }
            catch
            {
                return 0;
            }
        }
        public static int ConvertToInt(object Value)
        {
            try
            {
                if (Value == null) return 0;
                if (Value.GetType() != typeof(int)) return 0;
                return Convert.ToInt32(Value);
            }
            catch
            {
                return 0;
            }
        }
        public static long ConvertToLong(string Value)
        {
            try
            {
                return Int64.Parse(Value);
            }
            catch
            {
                return 0;
            }
        }
        public static double ConvertToDouble(object Value)
        {
            try
            {

                return double.Parse(Value.ToString());
            }
            catch
            {
                return 0;
            }
        }
        public static string GetDateVN()
        {
            string[] ArrayDay = new string[] { "Chủ nhật", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7" };
            int currDay = (int)DateTime.Now.Date.DayOfWeek;
            string CurrDate = String.Format("{0:dd/MM/yyyy}", DateTime.Now.Date);
            int currMonth = DateTime.Now.Month;
            return ArrayDay[currDay] + ", " + CurrDate + ", " + String.Format("{0:HH:mm}", DateTime.Now);
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
        public static string GetDateVN1(DateTime _Date)
        {
            if (_Date == null) return "";
            string[] ArrayDay = new string[] { "Chủ nhật", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7" };
            int currDay = (int)_Date.DayOfWeek;

            string CurrDate = String.Format("{0:dd/MM/yyyy}", _Date);
            int currMonth = _Date.Month;
            return ArrayDay[currDay] + ", " + CurrDate;
            //return String.Format("{0:dd/MM/yyyy}", _Date);
        }
        public static string GetDateVN1(DateTime _Date, bool includeDayOfWeek)
        {
            if (_Date == null) return "";
            string[] ArrayDay = new string[] { "Chủ nhật", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7" };
            int currDay = (int)_Date.DayOfWeek;

            string CurrDate = String.Format("{0:dd/MM/yyyy}", _Date);
            int currMonth = _Date.Month;
            return (includeDayOfWeek ? ArrayDay[currDay] + ", " : "") + CurrDate;
            //return String.Format("{0:dd/MM/yyyy}", _Date);
        }
        public static string GetDateVN2(DateTime _Date)
        {
            if (_Date == null) return "";
            string[] ArrayDay = new string[] { "Chủ nhật", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7" };
            int currDay = (int)_Date.DayOfWeek;

            string CurrDate = String.Format("{0:dd/MM/yyyy}", _Date);
            int currMonth = _Date.Month;
            return String.Format("{0:HH:mm tt}", _Date) + ", " + CurrDate;
            //return String.Format("{0:dd/MM/yyyy}", _Date);
        }
        public static bool isExistFile(string FilePath)
        {
            return System.IO.File.Exists(FilePath);
        }
        public static string ReplaceEx(string original, string pattern, string replacement)
        {
            int count, position0, position1;
            count = position0 = position1 = 0;
            string upperString = original.ToUpper();
            string upperPattern = pattern.ToUpper();
            int inc = (original.Length / pattern.Length) *
                      (replacement.Length - pattern.Length);
            char[] chars = new char[original.Length + Math.Max(0, inc)];
            while ((position1 = upperString.IndexOf(upperPattern,
                                              position0)) != -1)
            {
                for (int i = position0; i < position1; ++i)
                    chars[count++] = original[i];
                for (int i = 0; i < replacement.Length; ++i)
                    chars[count++] = replacement[i];
                position0 = position1 + pattern.Length;
            }
            if (position0 == 0) return original;
            for (int i = position0; i < original.Length; ++i)
                chars[count++] = original[i];
            return new string(chars, 0, count);
        }
        
        static public string Replace(string original, string pattern, string replacement, StringComparison comparisonType)
        {
            if (original == null)
            {
                return null;
            }

            if (String.IsNullOrEmpty(pattern))
            {
                return original;
            }

            int lenPattern = pattern.Length;
            int idxPattern = -1;
            int idxLast = 0;

            StringBuilder result = new StringBuilder();

            while (true)
            {
                idxPattern = original.IndexOf(pattern, idxPattern + 1, comparisonType);

                if (idxPattern < 0)
                {
                    result.Append(original, idxLast, original.Length - idxLast);

                    break;
                }

                result.Append(original, idxLast, idxPattern - idxLast);
                string __strReplate = original.Substring(idxPattern, lenPattern);
                replacement = String.Format(replacement, __strReplate);
                result.Append(replacement);

                idxLast = idxPattern + lenPattern;
            }

            return result.ToString();
        }
        public static string SplitCode(string ListCode)
        {
            try
            {
                if (String.IsNullOrEmpty(ListCode)) return "";
                string[] __separator = new string[] { "&" };
                string[] __arrCode = ListCode.Split(__separator, StringSplitOptions.RemoveEmptyEntries);
                if (__arrCode.Length > 1) return "<b>(" + __arrCode[0] + " ...)</b>:";
                if (__arrCode.Length == 1) return "<b>" + __arrCode[0] + "</b>:";
                return "";
            }
            catch
            {
                return "";
            }

        }
        public static string RemoveAllControlChar(string __inStr)
        {
            __inStr = __inStr.Replace("'", "");
            __inStr = __inStr.Replace("\n", "");
            __inStr = __inStr.Replace("\"", "");
            __inStr = __inStr.Replace("\"\"", "");
            __inStr = __inStr.Replace("\r", "");
            int __post = 900;
            if (__inStr.Length > 900)
            {

                while (__post < __inStr.Length)
                {
                    __post = __inStr.IndexOf(" ", 900);
                    if (__post > 0) break;
                    else __post++;
                }
                if (__post <= 0) __post = __inStr.Length;
                __inStr = __inStr.Substring(0, __post);
            }
            if (__inStr.Length >= 900) __inStr += " ...";
            return __inStr;
        }
        public static string MySubString(string __Str, int Words)
        {
            System.Text.StringBuilder __returnStr = new System.Text.StringBuilder();
            string[] __separator = new string[] { " " };
            string[] __arrStr = __Str.Split(__separator, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < __arrStr.Length; i++)
            {
                if (i < Words && i < __arrStr.Length)
                {
                    __returnStr.Append(__arrStr[i] + " ");

                }
                else
                {
                    __returnStr.Append(" ...");
                    break;
                }
            }
            return __returnStr.ToString();
        }

        #endregion
    }
}
