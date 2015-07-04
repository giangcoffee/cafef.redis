using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace CafeF_EmbedData.Common
{
    public static class Lib
    {
        public enum TradeCenter
        {
            AlTradeCenter = 0,
            HoSE = 1,
            HaSTC = 2
        }

        /// <summary>
        /// Chuyển đổi 1 giá trị sang kiểu Integer
        /// </summary>
        /// <param name="value">Giá trị cần chuyển đổi</param>
        /// <returns>Số kiểu Integer</returns>
        public static int Object2Integer(object value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// Chuyển đổi 1 giá trị sang kiểu Long
        /// </summary>
        /// <param name="value">Giá trị cần chuyển đổi</param>
        /// <returns>Số kiểu Long</returns>
        public static long Object2Long(object value)
        {
            try
            {
                return Convert.ToInt64(value);
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// Chuyển đổi 1 giá trị sang kiểu Double
        /// </summary>
        /// <param name="value">Giá trị cần chuyển đổi</param>
        /// <returns>Số kiểu Double</returns>
        public static double Object2Double(object value)
        {
            try
            {
                return Convert.ToDouble(value);
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// Chuyển đổi 1 giá trị sang kiểu float
        /// </summary>
        /// <param name="value">Giá trị cần chuyển đổi</param>
        /// <returns>Số kiểu float</returns>
        public static float Object2Float(object value)
        {
            try
            {
                return float.Parse(value.ToString());
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// Chuyển đổi 1 giá trị sang kiểu DateTime
        /// </summary>
        /// <param name="value">Giá trị cần chuyển đổi</param>
        /// <returns>Số kiểu DateTime</returns>
        public static DateTime Object2DateTime(object value)
        {
            try
            {
                return Convert.ToDateTime(value);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
        /// <summary>
        /// Chuyển đổi 1 giá trị sang kiểu DateTime
        /// </summary>
        /// <param name="value">Giá trị cần chuyển đổi</param>
        /// <returns>Số kiểu DateTime</returns>
        public static DateTime ConvertDatasaseString2DateTime(object value)
        {
            try
            {
                string date = value.ToString();
                string year = date.Substring(0, 4);
                date = date.Substring(4);
                string month = date.Substring(0, 2);
                date = date.Substring(2);

                return String2Date(date + "/" + month + "/" + year);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
        /// <summary>
        /// Chuyển đổi 1 giá trị sang xâu DateTime theo định dạng dd/MM/yyyy
        /// </summary>
        /// <param name="value">Giá trị cần chuyển đổi</param>
        /// <returns>Xâu DateTime theo định dạng dd/MM/yyyy</returns>
        public static string FormatDatasaseDateString(object value)
        {
            try
            {
                return FormatDate(ConvertDatasaseString2DateTime(value));
            }
            catch
            {
                return FormatDate(DateTime.Now);
            }
        }
        /// <summary>
        /// Chuyển đổi 1 giá trị sang xâu DateTime theo định dạng dd/MM/yyyy hh:mm:ss
        /// </summary>
        /// <param name="value">Giá trị cần chuyển đổi</param>
        /// <returns>Xâu DateTime theo định dạng dd/MM/yyyy hh:mm:ss</returns>
        public static string FormatDatasaseDateTimeString(object value)
        {
            try
            {
                return FormatDateTime(ConvertDatasaseString2DateTime(value));
            }
            catch
            {
                return FormatDateTime(DateTime.Now);
            }
        }
        /// <summary>
        /// Hiển thị dạng xâu của 1 giá trị kiểu ngày tháng
        /// </summary>
        /// <param name="value">Giá trị cần hiển thị</param>
        /// <returns>Xâu hiển thị (Lỗi trả về xâu rỗng)</returns>
        public static string FormatDate(object value)
        {
            try
            {
                return Convert.ToDateTime(value).ToString("dd/MM/yyyy");
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// Hiển thị dạng xâu của 1 giá trị kiểu ngày tháng
        /// </summary>
        /// <param name="value">Giá trị cần hiển thị</param>
        /// <returns>Xâu hiển thị (Lỗi trả về xâu rỗng)</returns>
        public static string FormatTime(object value)
        {
            try
            {
                return Convert.ToDateTime(value).ToString("HH:mm:ss");
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// Hiển thị dạng xâu của 1 giá trị kiểu ngày tháng
        /// </summary>
        /// <param name="value">Giá trị cần hiển thị</param>
        /// <returns>Xâu hiển thị (Lỗi trả về xâu rỗng)</returns>
        public static string FormatDateTime(object value)
        {
            try
            {
                return Convert.ToDateTime(value).ToString("dd/MM/yyyy HH:mm:ss");
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// Hiển thị dạng xâu của 1 số kiểu Double theo định dạng chuẩn (Ví dụ: 1,000.5)
        /// </summary>
        /// <param name="value">Giá trị cần hiển thị</param>
        /// <returns>Xâu hiển thị</returns>
        public static string FormatDouble(object value)
        {
            string returnValue = (value == null ? "0" : value.ToString());
            try
            {
                double number = Convert.ToDouble(value);
                returnValue = number.ToString("#,##0.00");
                while (returnValue.EndsWith("0"))
                {
                    returnValue = returnValue.Substring(0, returnValue.Length - 1);
                }
                if (returnValue.EndsWith("."))
                {
                    returnValue = returnValue.Substring(0, returnValue.Length - 1);
                }
            }
            catch { }
            return (returnValue == "0" ? "0" : returnValue);
        }
        /// <summary>
        /// Chuyển đổi 1 xâu ngày tháng dạng dd/MM/yyyy sang ngày tháng
        /// </summary>
        /// <param name="value">Xâu nhập</param>
        /// <returns>Trả về kiểu DateTime cua ngày cần chuyển đổi (Nếu lỗi thì trả về DateTime.MinValue)</returns>
        public static DateTime String2Date(string value)
        {
            string temp = value;

            string date = temp.Substring(0, temp.IndexOf("/"));
            temp = temp.Substring(temp.IndexOf("/") + 1);
            string month = temp.Substring(0, temp.IndexOf("/"));
            string year = temp.Substring(temp.IndexOf("/") + 1);

            string[] months = new string[] { "jan", "feb", "mar", "apr", "may", "jun", "jul", "aug", "sep", "oct", "nov", "dec" };
            try
            {
                return Convert.ToDateTime(date + " " + months[Convert.ToInt32(month) - 1] + " " + year);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
        /// <summary>
        /// Lấy 1 xâu ngẫu nhiên
        /// </summary>
        /// <param name="length">Số lượng ký tự</param>
        /// <returns>Xâu ngẫu nhiên</returns>
        public static string GetRamdomString(int length)
        {
            string temp = "";
            string[] myAlphabet = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

            Random Rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                temp += myAlphabet[Rnd.Next(0, myAlphabet.Length - 1)];
            }
            return temp;
        }
        /// <summary>
        /// Produce a string in double quotes with backslash sequences in all the right places.
        /// </summary>
        /// <param name="s">A String</param>
        /// <returns>A String correctly formatted for insertion in a JSON message.</returns>
        public static string EncodeJSON(string input)
        {
            if (input == null || input.Length == 0)
            {
                return "";
            }

            string value = input.Replace("'", "\"");
            char c;
            int i;
            int len = value.Length;
            StringBuilder sb = new StringBuilder(len + 4);
            string t;

            //sb.Append('"');
            for (i = 0; i < len; i += 1)
            {
                c = value[i];
                if ((c == '\\') || (c == '"') || (c == '>'))
                {
                    sb.Append('\\');
                    sb.Append(c);
                }
                else if (c == '\b')
                    sb.Append("\\b");
                else if (c == '\t')
                    sb.Append("\\t");
                else if (c == '\n')
                    sb.Append("\\n");
                else if (c == '\f')
                    sb.Append("\\f");
                else if (c == '\r')
                    sb.Append("\\r");
                else
                {
                    if (c < ' ')
                    {
                        //t = "000" + Integer.toHexString(c);
                        string tmp = new string(c, 1);
                        t = "000" + int.Parse(tmp, System.Globalization.NumberStyles.HexNumber);
                        sb.Append("\\u" + t.Substring(t.Length - 4));
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
            }
            //sb.Append('"');
            return sb.ToString();
        }
        /// <summary>
        /// Write log to "logs/log.txt" file
        /// </summary>
        /// <param name="message">Message to write</param>
        public static void WriteLog(string message)
        {
            //System.IO.StreamWriter writer;

            //try
            //{
            //    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath("logs/log.txt")))
            //    {
            //        writer = System.IO.File.AppendText(System.Web.HttpContext.Current.Server.MapPath("logs/log.txt"));
            //    }
            //    else
            //    {
            //        writer = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("logs/log.txt"));
            //    }
            //    writer.WriteLine(message);
            //    writer.Close();
            //    writer.Dispose();
            //}
            //catch
            //{
            //}
        }
        public static void WriteLog(string message, string filePath)
        {
            //System.IO.StreamWriter writer;

            //try
            //{
            //    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(filePath)))
            //    {
            //        writer = System.IO.File.AppendText(System.Web.HttpContext.Current.Server.MapPath(filePath));
            //    }
            //    else
            //    {
            //        writer = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath(filePath));
            //    }
            //    writer.WriteLine(message);
            //    writer.Close();
            //    writer.Dispose();
            //}
            //catch
            //{
            //}
        }

        public static void WriteLog(Exception ex)
        {
            //System.IO.StreamWriter writer;

            //try
            //{
            //    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath("logs/log.txt")))
            //    {
            //        writer = System.IO.File.AppendText(System.Web.HttpContext.Current.Server.MapPath("logs/log.txt"));
            //    }
            //    else
            //    {
            //        writer = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("logs/log.txt"));
            //    }

            //    string message = "";
            //    if (ex.InnerException != null)
            //    {
            //        if (ex.InnerException.InnerException != null)
            //        {
            //            message = ex.InnerException.InnerException.Message + "\n" +
            //                      ex.InnerException.InnerException.StackTrace + "===========================\n";
            //        }
            //        else
            //        {
            //            message = ex.Message + "\n" + ex.StackTrace + "===========================\n";
            //        }
            //    }
            //    else
            //    {
            //        message = ex.Message + "\n" + ex.StackTrace;
            //    }

            //    writer.WriteLine(message);
            //    writer.Close();
            //    writer.Dispose();
            //}
            //catch
            //{
            //}
        }
        public static void WriteLog(Exception ex, string filePath)
        {
            //System.IO.StreamWriter writer;

            //try
            //{
            //    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(filePath)))
            //    {
            //        writer = System.IO.File.AppendText(System.Web.HttpContext.Current.Server.MapPath(filePath));
            //    }
            //    else
            //    {
            //        writer = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath(filePath));
            //    }
            //    string message = "";

            //    Exception tempEx = ex;

            //    while (tempEx != null)
            //    {
            //        message += "\n\r" + tempEx.Message + "\n\r" + tempEx.StackTrace + "\n\r";
            //        tempEx = tempEx.InnerException;
            //    }
            //    if (!string.IsNullOrEmpty(message)) message += "======================================";

            //    writer.WriteLine(message);
            //    writer.Close();
            //    writer.Dispose();
            //}
            //catch
            //{
            //}
        }
        public static void WriteError(string message)
        {

            try
            {
                if (ConfigurationManager.AppSettings["AllowLog"] != null && ConfigurationManager.AppSettings["AllowLog"].ToLower() == "true")
                {
                    System.IO.StreamWriter writer;
                    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath("logs/log.txt")))
                    {
                        writer = System.IO.File.AppendText(System.Web.HttpContext.Current.Server.MapPath("logs/log.txt"));
                    }
                    else
                    {
                        writer = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("logs/log.txt"));
                    }
                    writer.WriteLine(message);
                    writer.Close();
                    writer.Dispose();
                }
            }
            catch
            {
            }
        }

        public static void WriteErrorOnly(string message)
        {

            try
            {
                
                    System.IO.StreamWriter writer;
                    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath("logs/log.txt")))
                    {
                        writer = System.IO.File.AppendText(System.Web.HttpContext.Current.Server.MapPath("logs/log.txt"));
                    }
                    else
                    {
                        writer = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("logs/log.txt"));
                    }
                    writer.WriteLine(message);
                    writer.Close();
                    writer.Dispose();
            }
            catch
            {
            }
        }

        #region SqlHelper
        public static int ExecuteNoneQuery(string connectionString, string commandText, CommandType commandType, params SqlParameter[] sqlParams)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, cnn))
                {
                    cmd.CommandType = commandType;

                    foreach (SqlParameter param in sqlParams)
                    {
                        cmd.Parameters.Add(param);
                    }

                    int returnValue = 0;

                    cnn.Open();
                    returnValue = cmd.ExecuteNonQuery();
                    cnn.Close();

                    return returnValue;
                }
            }
        }

        public static DataTable ExecuteDataTable(string connectionString, string commandText, CommandType commandType, params SqlParameter[] sqlParams)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, cnn))
                {
                    cmd.CommandType = commandType;

                    foreach (SqlParameter param in sqlParams)
                    {
                        cmd.Parameters.Add(param);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dtReturnValue = new DataTable();
                        adapter.Fill(dtReturnValue);
                        return dtReturnValue;
                    }
                }
            }
        }

        public static DataTable ExecuteDataTable(string connectionString, string commandText, CommandType commandType, int startRecord, int maxRecord, params SqlParameter[] sqlParams)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, cnn))
                {
                    cmd.CommandType = commandType;

                    foreach (SqlParameter param in sqlParams)
                    {
                        cmd.Parameters.Add(param);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dtReturnValue = new DataTable();
                        adapter.Fill(startRecord, maxRecord, dtReturnValue);
                        return dtReturnValue;
                    }
                }
            }
        }
        #endregion

        #region Unicode
        public const string KoDauChars =
            "aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIOOOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU";

        public const string uniChars =
            "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";

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

        public static string News_BuildUrl(string newsId, string catId, string title)
        {
            string __urlFormart = "/{0}CA{1}/{2}.chn";
            return string.Format(__urlFormart, newsId, catId, UnicodeToKoDauAndGach(title));
        }
        #endregion
    }
}
