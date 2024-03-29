using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Reflection;
namespace CafeF.Redis.Page
{
    public class ConvertUtility
    {
        public static Guid ToGuid(object val, Guid defValue)
        {
            Guid ret = defValue;
            try
            {
                ret = new Guid(val.ToString());
            }
            catch { }

            return ret;
        }
        public static Guid ToGuid(object val)
        {
            return ConvertUtility.ToGuid(val.ToString(), Guid.Empty);
        }

        public static Boolean ToBoolean(object val, Boolean defValue)
        {
            Boolean ret = defValue;
            try
            {
                ret = Convert.ToBoolean(val);
            }
            catch { }

            return ret;
        }
        public static Boolean ToBoolean(object val)
        {
            return ConvertUtility.ToBoolean(val, false);
        }

        public static Int16 ToInt16(object val, Int16 defValue)
        {
            Int16 ret = defValue;
            try
            {
                ret = Convert.ToInt16(val);
            }
            catch { }

            return ret;
        }
        public static Int16 ToInt16(object val)
        {
            return ConvertUtility.ToInt16(val, 0);
        }

        public static Int64 ToInt64(object val, Int64 defValue)
        {
            Int64 ret = defValue;
            try
            {
                ret = Convert.ToInt64(val);
            }
            catch { }

            return ret;
        }
        public static Int64 ToInt64(object val)
        {
            return ConvertUtility.ToInt64(val, 0);
        }

        public static Int32 ToInt32(object val, Int32 defValue)
        {
            Int32 ret = defValue;
            try
            {
                ret = Convert.ToInt32(val);
            }
            catch { }

            return ret;
        }
        public static Int32 ToInt32(object val)
        {
            return ConvertUtility.ToInt32(val, 0);
        }

        public static double ToDouble(object val, double defValue)
        {
            double ret = defValue;
            try
            {
                ret = Convert.ToDouble(val);
            }
            catch { }

            return ret;
        }
        public static double ToDouble(object val)
        {
            return ConvertUtility.ToDouble(val, 0);
        }

        public static long ToLong(object val, long defValue)
        {
            long ret = defValue;
            try
            {
                ret =(long) Convert.ToDouble(val);
            }
            catch { }

            return ret;
        }
        public static long ToLong(object val)
        {
            return ConvertUtility.ToLong(val, 0);
        }

        public static Decimal ToDecimal(object val, Decimal defValue)
        {
            Decimal ret = defValue;
            try
            {
                ret = Convert.ToDecimal(val);
            }
            catch { }

            return ret;
        }
        public static Decimal ToDecimal(object val)
        {
            return ConvertUtility.ToDecimal(val, 0);
        }

        public static Single ToSingle(object val, Single defValue)
        {
            Single ret = defValue;
            try
            {
                ret = Convert.ToSingle(val);
            }
            catch { }

            return ret;
        }
        public static Single ToSingle(object val)
        {
            return ConvertUtility.ToSingle(val, 0);
        }

        public static DateTime ToDateTime(object val, DateTime defValue)
        {
            DateTime ret = defValue;
            try
            {
                ret = Convert.ToDateTime(val);
            }
            catch { }

            return ret;
        }
        public static DateTime ToDateTime(object val)
        {
            return ConvertUtility.ToDateTime(val, DateTime.Now);
        }

        public static String ToString(object val, String defValue)
        {
            String ret = defValue;
            try
            {
                ret = Convert.ToString(val);
            }
            catch { }

            return ret;
        }
        public static String ToString(object val)
        {
            return ConvertUtility.ToString(val, String.Empty);
        }
    }

    public class Hepler
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

        public static string Event_BuildLink(string NewsId, string symbol, string title, string d)
        {
            string __TitleUrlFormat = UnicodeToKoDauAndGach(title);
            string __link = "<a href=\"/{0}-{2}/{1}.chn\" title=\"{3}\">{5}</a> ({6})";
            string symTitle = symbol.Trim() == "" ? "" : symbol + ": ";
            __link = String.Format(__link, symbol, __TitleUrlFormat, NewsId, HttpUtility.HtmlEncode(title).Replace("'", "&#39;"), symTitle, title, d);
            return __link;
        }

        public static string GetDateVN(DateTime _Date)
        {
            if (_Date == null) return "";
            string[] ArrayDay = new string[] { "Chủ nhật", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7" };
            int currDay = (int)_Date.DayOfWeek;

            string CurrDate = String.Format("{0:dd/MM/yyyy}", _Date);
            int currMonth = _Date.Month;
            return ArrayDay[currDay] + ", " + CurrDate;
        }

        public static string getSymbol(string list)
        {
            string sym = "";
            string[] sList =list.Split('&');
            for (int i = 0; i < sList.Length; i++)
                if (ConvertUtility.ToString(sList[i]) != "")
                    return ConvertUtility.ToString(sList[i]);
            return sym;
        }
    }

    public class GenericComparer
    {
        public List<GenericComparer> comparers { get; set; }
        int level = 0;

        public SortDirection SortDirection { get; set; }
        public PropertyInfo PropertyInfo { get; set; }

        public int Compare<T>(T t1, T t2)
        {
            int ret = 0;

            if (level >= comparers.Count)
                return 0;

            object t1Value = comparers[level].PropertyInfo.GetValue(t1, null);
            object t2Value = comparers[level].PropertyInfo.GetValue(t2, null);

            if (t1 == null || t1Value == null)
            {
                if (t2 == null || t2Value == null)
                {
                    ret = 0;
                }
                else
                {
                    ret = -1;
                }
            }
            else
            {
                if (t2 == null || t2Value == null)
                {
                    ret = 1;
                }
                else
                {
                    ret = ((IComparable)t1Value).CompareTo(((IComparable)t2Value));
                }
            }
            if (ret == 0)
            {
                level += 1;
                ret = Compare(t1, t2);
                level -= 1;
            }
            else
            {
                if (comparers[level].SortDirection == SortDirection.Descending)
                {
                    ret *= -1;
                }
            }
            return ret;
        }
    }

    public static class Utility
    {
        public static void Sort<T>(this List<T> list, string sortExpression)
        {
            string[] sortExpressions = sortExpression.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            List<GenericComparer> comparers = new List<GenericComparer>();

            foreach (string sortExpress in sortExpressions)
            {
                string sortProperty = sortExpress.Trim().Split(' ')[0].Trim();
                string sortDirection = sortExpress.Trim().Split(' ')[1].Trim();

                Type type = typeof(T);
                PropertyInfo PropertyInfo = type.GetProperty(sortProperty);
                if (PropertyInfo == null)
                {
                    PropertyInfo[] props = type.GetProperties();
                    foreach (PropertyInfo info in props)
                    {
                        if (info.Name.ToString().ToLower() == sortProperty.ToLower())
                        {
                            PropertyInfo = info;
                            break;
                        }
                    }
                    if (PropertyInfo == null)
                    {
                        throw new Exception(String.Format("{0} is not a valid property of type: \"{1}\"", sortProperty, type.Name));
                    }
                }

                SortDirection SortDirection = SortDirection.Ascending;
                if (sortDirection.ToLower() == "asc" || sortDirection.ToLower() == "ascending")
                {
                    SortDirection = SortDirection.Ascending;
                }
                else if (sortDirection.ToLower() == "desc" || sortDirection.ToLower() == "descending")
                {
                    SortDirection = SortDirection.Descending;
                }
                else
                {
                    throw new Exception("Valid SortDirections are: asc, ascending, desc and descending");
                }

                comparers.Add(new GenericComparer { SortDirection = SortDirection, PropertyInfo = PropertyInfo, comparers = comparers });
            }
            list.Sort(comparers[0].Compare);
        }

        public static List<T> GetPaging<T>(this List<T> list, int idx, int pagecount)
        {
            List<T> ret = new List<T>();
            for (int i = (idx - 1) * pagecount; i < idx * pagecount; i++)
            {
                if (i < list.Count)
                    ret.Add(list[i]);
            }
            return ret;
        }
    }
}
