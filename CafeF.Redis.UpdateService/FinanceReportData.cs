using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;

namespace CafeF.Redis.UpdateService
{
    public class Update_DanhSach_BCTC
    {
        public const string CACHE_NAME_HTML_DANH_SACH_BCTC = "MemCached_DanhSach_BCTC_{0}";
        class Const
        {
            public static string BCTC = "Báo cáo tài chính quý {0} năm {1}";
            public static string BCTC_HN = "Báo cáo tài chính hợp nhất quý {0} năm {1}";
            public static string CN_BCTC_HN = "Báo cáo tài chính hợp nhất năm {0}";
            public static string BCTC_M = "Báo cáo tài chính công ty mẹ quý {0} năm {1}";
            public static string CN_BCTC_M = "Báo cáo tài chính công ty mẹ năm {0}";
            public static string BCTC_KQKD = "Báo cáo KQKD quý {0} năm {1}";
            public static string CN_BCTC_KQKD = "Báo cáo KQKD năm {0}";
            public static string BCTC_KQKD_HN = "Báo cáo KQKD hợp nhất quý {0} năm {1}";
            public static string CN_BCTC_KQKD_HN = "Báo cáo KQKD hợp nhất năm {0}";
            public static string BCTC_KQKD_M = "Báo cáo KQKD công ty mẹ quý {0} năm {1}";
            public static string CN_BCTC_KQKD_M = "Báo cáo KQKD công ty mẹ năm {0}";
            public static string BCTC_TT = "Báo cáo tài chính tóm tắt quý {0} năm {1}";
            public static string CN_BCTC_TT = "Báo cáo tài chính tóm tắt năm {0}";
            public static string BCTC_HNKT = "Báo cáo tài chính hợp nhất quý {0} năm {1} (đã kiểm toán)";
            public static string CN_BCTC_HNKT = "Báo cáo tài chính hợp nhất năm {0} (đã kiểm toán)";
            public static string BCTC_MKT = "Báo cáo tài chính công ty mẹ quý {0} năm {1} (đã kiểm toán)";
            public static string CN_BCTC_MKT = "Báo cáo tài chính công ty mẹ năm {0} (đã kiểm toán)";
            public static string CN_BCTC_KT = "Báo cáo tài chính năm {0} (đã kiểm toán)";
            public static string CN_BCTC = "Báo cáo tài chính năm {0}";

            public static string BCTC_KQKD_KT = "Báo cáo KQKD hợp nhất quý {0} năm {1} (đã kiểm toán)";
            public static string CN_BCTC_KQKD_KT = "Báo cáo KQKD hợp nhất năm {0} (đã kiểm toán)";
            public static string BCTC_HNSX = "Báo cáo tài chính hợp nhất quý {0} năm {1} (đã soát xét)";
            public static string CN_BCTC_HNSX = "Báo cáo tài chính hợp nhất năm {0} (đã soát xét)";
            public static string BCTC_MSX = "Báo cáo tài chính công ty mẹ quý {0} năm {1} (đã soát xét)";
            public static string CN_BCTC_MSX = "Báo cáo tài chính công ty mẹ năm {0} (đã soát xét)";
            public static string BCTC_KQKD_SX = "Báo cáo KQKD hợp nhất quý {0} năm {1} (đã soát xét)";
            public static string CN_BCTC_KQKD_SX = "Báo cáo KQKD hợp nhất năm {0} (đã soát xét)";
            public static string BCTC_TM = "Thuyết minh Báo cáo tài chính quý {0} năm {1}";
            public static string CN_BCTC_TM = "Thuyết minh Báo cáo tài chính năm {0}";
            public static string BCB = "Bản cáo bạch";
            public static string BCTN = "Báo cáo thường niên năm {0}";
            public static string BCTC_KT = "Báo cáo tài chính quý {0} năm {1} (đã kiểm toán)";

            public static string NQHDQT = "Nghị quyết Hội đồng Quản trị ngày {0}";
            public static string NQDHCD_TN = "Nghị quyết Đại hội cổ đông thường niên năm {0}";
            public static string NQDHCD_BT = "Nghị quyết Đại hội cổ đông bất thường ngày {0}";
            public static string NQDHCD = "Nghị quyết đại hội cổ đông ngày {0}";

        }

        private const string TableHeader = "<table cellpadding='0' cellspacing='0' width='96%' style='border:solid 1px #e2e2e2' >" +
                                        "<tr style='background-color:#dce5f3'>" +
                                            "<td align='left' style='padding:5px 0 5px 10px; color:#003366; font-weight:bold; font-family:Tahoma; font-size:13px'>Loại báo cáo</td>" +
                                            "<td align='center' style=' color:#003366; font-weight:bold;font-family:Tahoma; font-size:13px'>Thời gian</td>" +
                                            "<td align='center' style='color:#003366; font-weight:bold;font-family:Tahoma; font-size:13px'>Tải về</td>" +
                                        "</tr>         ";
        private const string TableEnd = "</table>";
        private const string TableBody = "<tr style='background-color:{3}'>" +
                                        "<td style='padding:5px 0 5px 10px;font-family:Tahoma; font-size:12px;border-bottom:1px solid #E2E2E2'>{0}</td>" +
                                        "<td align='center' style='padding:5px 0 5px 10px;font-family:Tahoma; font-size:12px; border-left:1px solid #E2E2E2; border-right:1px solid #E2E2E2; border-bottom:1px solid #E2E2E2'>&nbsp;{1}&nbsp;</td>" +
                                        "<td align='center' style='padding:5px 0 5px 10px;font-family:Tahoma; font-size:12px;border-bottom:1px solid #E2E2E2'>&nbsp;{2}&nbsp;</td>" +
                                    "</tr>";

        public const int LOGON32_LOGON_INTERACTIVE = 2;
        public const int LOGON32_PROVIDER_DEFAULT = 0;

        public static WindowsImpersonationContext impersonationContext;

        [DllImport("advapi32.dll")]
        public static extern int LogonUserA(String lpszUserName, String lpszDomain, String lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int DuplicateToken(IntPtr hToken, int impersonationLevel, ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);

        public static bool impersonateValidUser(String userName, String domain, String password)
        {
            WindowsIdentity tempWindowsIdentity;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            if (RevertToSelf())
            {
                if (LogonUserA(userName, domain, password, LOGON32_LOGON_INTERACTIVE,
                    LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                {
                    if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                    {
                        tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                        impersonationContext = tempWindowsIdentity.Impersonate();
                        if (impersonationContext != null)
                        {
                            CloseHandle(token);
                            CloseHandle(tokenDuplicate);
                            return true;
                        }
                    }
                }
            }
            if (token != IntPtr.Zero)
                CloseHandle(token);
            if (tokenDuplicate != IntPtr.Zero)
                CloseHandle(tokenDuplicate);
            return false;
        }
        public static void undoImpersonation()
        {
            impersonationContext.Undo();
        }
        public static string GetConfig(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }
        /// <summary>
        /// Lấy DS BCTC từ DataTable có sẵn
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static string ReturnHTML_BCTC(DataRow[] rows)
        {
            if (rows == null || rows.Length == 0) return TableHeader + TableEnd;
            string __result = "";
            //string user = GetConfig("user");
            //string pass = GetConfig("pass");
            //string domain = GetConfig("domain");
            //if (impersonateValidUser(user, domain, pass))
            //{
            //DataTable dt = new DataTable();
            // dt = GetData(symbol);
            string value = "";
            if (rows != null)
            {

                int count = rows.Length;
                for (int i = 0; i < count; i++)
                {
                    if (rows[i]["LoaiBaoCao"].ToString() != "")
                    {
                        if (i % 2 == 0)
                        {
                            value += String.Format(TableBody, rows[i]["LoaiBaoCao"].ToString(), rows[i]["Time"].ToString(), rows[i]["LinkFile"].ToString(), "#FFF");
                        }
                        else
                        {
                            value += String.Format(TableBody, rows[i]["LoaiBaoCao"].ToString(), rows[i]["Time"].ToString(), rows[i]["LinkFile"].ToString(), "#f2f2f2");
                        }
                    }
                }
            }
            __result = TableHeader + value + TableEnd;


            //}

            return __result;
        }
        public static string ReturnHTML_BCTC(string symbol)
        {
            string __result = "";
            string user = GetConfig("user");
            string pass = GetConfig("pass");
            string domain = GetConfig("domain");
            if (impersonateValidUser(user, domain, pass))
            {
                DataTable dt = new DataTable();
                dt = GetData(symbol);
                string value = "";
                if (dt != null)
                {

                    int count = dt.Rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (dt.Rows[i]["LoaiBaoCao"].ToString() != "")
                        {
                            if (i % 2 == 0)
                            {
                                value += String.Format(TableBody, dt.Rows[i]["LoaiBaoCao"].ToString(), dt.Rows[i]["Time"].ToString(), dt.Rows[i]["LinkFile"].ToString(), "#FFF");
                            }
                            else
                            {
                                value += String.Format(TableBody, dt.Rows[i]["LoaiBaoCao"].ToString(), dt.Rows[i]["Time"].ToString(), dt.Rows[i]["LinkFile"].ToString(), "#f2f2f2");
                            }
                        }
                    }
                }
                __result = TableHeader + value + TableEnd;


            }

            return __result;
        }
        /// <summary>
        /// hieubt - 28/01/11 - get all files
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllData()
        {
            string __result = "";
            string user = GetConfig("user");
            string pass = GetConfig("pass");
            string domain = GetConfig("domain");
            if (!(impersonateValidUser(user, domain, pass))) return new DataTable();


            DataTable tblFile = new DataTable();
            //try
            //{
            string folderPath = ConfigurationManager.AppSettings["BCTC_FolderDownload"];

            //folderPath = Server.MapPath(folderPath);

            DirectoryInfo folder = new DirectoryInfo(folderPath);
            DirectoryInfo[] subFolders = folder.GetDirectories();
            DirectoryInfo[] folderShowed = new DirectoryInfo[subFolders.Length];
            DirectoryInfo[] folderYear = new DirectoryInfo[subFolders.Length];
            DirectoryInfo[] folderOther = new DirectoryInfo[subFolders.Length];

            int i = 0, j = 0;

            foreach (DirectoryInfo subFolder in subFolders)
            {
                if (IsNumber(subFolder.Name))
                {
                    folderYear[i] = subFolder;
                    i++;
                }
                else
                {
                    folderOther[j] = subFolder;
                    j++;
                }
            }
            Array.Sort(folderYear, 0, i, new FolderSort());
            folderYear.CopyTo(folderShowed, 0);
            for (int a = i, b = 0; a < folderOther.Length && b < j; a++, b++)
            {
                folderShowed[a] = folderOther[b];
            }


            tblFile.Columns.Add("FileName");
            tblFile.Columns.Add("LinkFile");
            tblFile.Columns.Add("Time");
            tblFile.Columns.Add("LoaiBaoCao");
            tblFile.Columns.Add("Symbol");

            DataRow dr;
            foreach (DirectoryInfo subFolder in folderShowed)
            {
                string searchPattern = "";

                //searchPattern = symbol + "_";

                FileInfo[] files = subFolder.GetFiles(searchPattern + "*", SearchOption.TopDirectoryOnly);
                Array.Sort(files, 0, files.Length, new FileSort());

                string downloadFolder = ConfigurationManager.AppSettings["Host"];
                if (!downloadFolder.EndsWith("/")) downloadFolder += "/";
                downloadFolder += ConfigurationSettings.AppSettings["FolderDownload"];
                if (!downloadFolder.EndsWith("/")) downloadFolder += "/";
                if (files.Length > 0)
                {
                    string fileName = "";
                    string linkFile = "";
                    string year = "";
                    string quy = "";
                    string ngaythang = "";
                    foreach (FileInfo file in files)
                    {
                        try
                        {
                            dr = tblFile.NewRow();
                            linkFile = downloadFolder + subFolder + "/" + file.Name;
                            fileName = file.Name;
                            if (!fileName.Contains("_")) continue;
                            dr["Symbol"] = fileName.Substring(0, fileName.IndexOf("_"));
                            dr["FileName"] = fileName;
                            dr["LinkFile"] = ReturnLinkFile(file.Extension, linkFile);

                            if (IsNumber(subFolder.Name))
                            {
                                quy = ReturnTimeBCTC(file.Name);
                                if (IsNumber(quy))
                                {
                                    year = (quy=="5" ? "CN" :("Q" + quy)) + "/" + subFolder.Name;
                                    dr["LoaiBaoCao"] = ReturnName(file.Name.Substring(0, file.Name.IndexOf(".")), quy, subFolder.Name);
                                    dr["Time"] = year;
                                }
                                else
                                {
                                    string tempNT = ReturnTimeNQ(file.Name);
                                    if (tempNT != "")
                                    {
                                        ngaythang = tempNT + "-" + subFolder.Name;
                                    }
                                    else
                                    {
                                        ngaythang = subFolder.Name;
                                    }
                                    dr["Time"] = ngaythang;
                                    dr["LoaiBaoCao"] = ReturnName(file.Name.Substring(0, file.Name.IndexOf(".")), "", ngaythang);
                                }
                            }
                            else
                            {
                                dr["LoaiBaoCao"] = ReturnName(file.Name.Substring(0, file.Name.IndexOf(".")), quy, subFolder.Name);
                            }

                            tblFile.Rows.Add(dr);
                        }
                        catch (Exception ex)
                        {
                            //    EventLog.WriteEntry("MemCached_DanhSachBCTC_GetFile", ex.InnerException.Message + ".\n" + ex.InnerException.StackTrace, EventLogEntryType.Error);
                            //throw new Exception(file.Name + " - " + ex.ToString());
                        }

                    }

                }


            }
            tblFile.AcceptChanges();
            //}
            //catch (Exception ex)
            //{
            //    EventLog.WriteEntry("MemCached_DanhSachBCTC_", ex.InnerException.Message + ".\n" + ex.InnerException.StackTrace, EventLogEntryType.Error);
            //}
            return tblFile;
        }
        private static DataTable GetData(string symbol)
        {
            DataTable tblFile = new DataTable();
            try
            {
                string folderPath = ConfigurationManager.AppSettings["BCTC_FolderDownload"];

                //folderPath = Server.MapPath(folderPath);

                DirectoryInfo folder = new DirectoryInfo(folderPath);
                DirectoryInfo[] subFolders = folder.GetDirectories();
                DirectoryInfo[] folderShowed = new DirectoryInfo[subFolders.Length];
                DirectoryInfo[] folderYear = new DirectoryInfo[subFolders.Length];
                DirectoryInfo[] folderOther = new DirectoryInfo[subFolders.Length];

                int i = 0, j = 0;

                foreach (DirectoryInfo subFolder in subFolders)
                {
                    if (IsNumber(subFolder.Name))
                    {
                        folderYear[i] = subFolder;
                        i++;
                    }
                    else
                    {
                        folderOther[j] = subFolder;
                        j++;
                    }
                }
                Array.Sort(folderYear, 0, i, new FolderSort());
                folderYear.CopyTo(folderShowed, 0);
                for (int a = i, b = 0; a < folderOther.Length && b < j; a++, b++)
                {
                    folderShowed[a] = folderOther[b];
                }


                tblFile.Columns.Add("FileName");
                tblFile.Columns.Add("LinkFile");
                tblFile.Columns.Add("Time");
                tblFile.Columns.Add("LoaiBaoCao");

                DataRow dr;
                foreach (DirectoryInfo subFolder in folderShowed)
                {
                    string searchPattern = "";

                    searchPattern = symbol + "_";

                    FileInfo[] files = subFolder.GetFiles(searchPattern + "*", SearchOption.TopDirectoryOnly);
                    Array.Sort(files, 0, files.Length, new FileSort());

                    string downloadFolder = ConfigurationManager.AppSettings["Host"];
                    if (!downloadFolder.EndsWith("/")) downloadFolder += "/";
                    downloadFolder += ConfigurationSettings.AppSettings["FolderDownload"];
                    if (!downloadFolder.EndsWith("/")) downloadFolder += "/";
                    if (files.Length > 0)
                    {
                        string fileName = "";
                        string linkFile = "";
                        string year = "";
                        string quy = "";
                        string ngaythang = "";
                        foreach (FileInfo file in files)
                        {
                            try
                            {
                                dr = tblFile.NewRow();
                                linkFile = downloadFolder + subFolder + "/" + file.Name;
                                fileName = file.Name;
                                dr["FileName"] = fileName;
                                dr["LinkFile"] = ReturnLinkFile(file.Extension, linkFile);

                                if (IsNumber(subFolder.Name))
                                {
                                    quy = ReturnTimeBCTC(file.Name);
                                    if (IsNumber(quy))
                                    {
                                        year = "Q" + quy + "/" + subFolder.Name;
                                        dr["LoaiBaoCao"] = ReturnName(file.Name.Substring(0, file.Name.IndexOf(".")), quy, subFolder.Name);
                                        dr["Time"] = year;
                                    }
                                    else
                                    {
                                        string tempNT = ReturnTimeNQ(file.Name);
                                        if (tempNT != "")
                                        {
                                            ngaythang = tempNT + "-" + subFolder.Name;
                                        }
                                        else
                                        {
                                            ngaythang = subFolder.Name;
                                        }
                                        dr["Time"] = ngaythang;
                                        dr["LoaiBaoCao"] = ReturnName(file.Name.Substring(0, file.Name.IndexOf(".")), "", ngaythang);
                                    }
                                }
                                else
                                {
                                    dr["LoaiBaoCao"] = ReturnName(file.Name.Substring(0, file.Name.IndexOf(".")), quy, subFolder.Name);
                                }

                                tblFile.Rows.Add(dr);
                            }
                            catch (Exception ex)
                            {
                                EventLog.WriteEntry("MemCached_DanhSachBCTC_GetFile", ex.InnerException.Message + ".\n" + ex.InnerException.StackTrace, EventLogEntryType.Error);
                            }

                        }

                    }


                }
                tblFile.AcceptChanges();
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("MemCached_DanhSachBCTC_", ex.InnerException.Message + ".\n" + ex.InnerException.StackTrace, EventLogEntryType.Error);
            }
            return tblFile;
        }

        private static string ReturnLinkFile(string ext, string linkfile)
        {
            string strLink = "";
            if (ext.ToLower() == ".xls")
            {
                strLink = "<a href='" + linkfile + "'><img src='http://cafef3.vcmedia.vn/images/downloadbctc/xls.jpg' border='0' /></a>";
            }
            else if (ext.ToLower() == ".doc" || ext.ToLower() == ".docx")
            {
                strLink = "<a href='" + linkfile + "'><img src='http://cafef3.vcmedia.vn/images/downloadbctc/word.jpg' border='0' /></a>";
            }
            else if (ext.ToLower() == ".rar" || ext.ToLower() == ".zip")
            {
                strLink = "<a href='" + linkfile + "'><img src='http://cafef3.vcmedia.vn/images/downloadbctc/rar.jpg' border='0' width='16px' /></a>";
            }
            else
            {
                strLink = "<a href='" + linkfile + "'><img src='http://cafef3.vcmedia.vn/images/downloadbctc/pdf.jpg' border='0' /></a>";
            }

            return strLink;
        }

        private static string ReturnTimeBCTC(string filename)
        {
            string __result = "";
            string[] arr = filename.Split("_".ToCharArray());
            string quy = "";
            quy = arr[1].Substring(arr[1].Length - 2, 2);
            if(quy =="CN")
            {
                quy = "5";
            }else{
                quy = arr[1].Substring(arr[1].Length - 1, 1);
            }
            __result = quy;

            return __result;

        }

        private static string ReturnTimeNQ(string filename)
        {
            string __result = "";
            if (filename.Contains(".")) filename = filename.Remove(filename.IndexOf("."));
            string[] arr = filename.Split("_".ToCharArray());
            string ngaythang = "";
            if (filename.IndexOf("NQHDQT") > 0)
            {
                ngaythang = arr[2];
            }
            else if (filename.IndexOf("NQDHCD_TN") > 0)
            {
                //ngaythang = arr[3];
            }
            else if (filename.IndexOf("NQDHCD_BT") > 0)
            {
                ngaythang = arr[3];
            }
            else if (filename.IndexOf("NQDHCD") > 0)
            {
                ngaythang = arr[2];
            }
            else if (filename.IndexOf("BCTN") > 0)
            {

            }
            string temp = "";
            if (ngaythang != "")
            {
                if (ngaythang.IndexOf("-") < 0)
                {
                    temp = ngaythang.Substring(0, 2) + "-" + ngaythang.Substring(2, 2);
                }
                else
                {
                    temp = ngaythang.Substring(0, ngaythang.IndexOf("-")) + ngaythang.Substring(ngaythang.IndexOf("-"));
                }
            }
            __result = temp;
            return __result;

        }

        private static string ReturnName(string filename, string quy, string nam)
        {
            string __result = "";
            string temp = "";
            string temp2 = "";
            if (filename.IndexOf("CN_BCTC_KT") > 0)
            {
                temp = filename.Substring(filename.IndexOf("CN_BCTC_KT"));
            }
            else if (filename.IndexOf("CN_BCTC") > 0)
            {
                temp = filename.Substring(filename.IndexOf("CN_BCTC"));
            }
            else if (filename.IndexOf("BCTC") > 0)
            {
                temp = filename.Substring(filename.IndexOf("BCTC"));
            }
            else if (filename.IndexOf("BCB") > 0)
            {
                temp = filename.Substring(filename.IndexOf("BCB"));
            }
            else if (filename.IndexOf("NQDHCD_TN") > 0)
            {
                temp2 = filename.Substring(filename.IndexOf("NQDHCD_TN"));
                temp = temp2.Substring(0, temp2.LastIndexOf("_"));

            }
            else if (filename.IndexOf("NQHDQT") > 0)
            {
                temp2 = filename.Substring(filename.IndexOf("NQHDQT"));
                temp = temp2.Substring(0, temp2.IndexOf("_"));
            }
            else if (filename.IndexOf("NQDHCD_BT") > 0)
            {
                temp2 = filename.Substring(filename.IndexOf("NQDHCD_BT"));
                temp = temp2.Substring(0, temp2.LastIndexOf("_"));

            }
            else if (filename.IndexOf("NQDHCD") > 0)
            {
                temp2 = filename.Substring(filename.IndexOf("NQDHCD"));
                temp = temp2.Substring(0, temp2.IndexOf("_"));

            }
            else if (filename.IndexOf("BCTN") > 0)
            {
                temp = filename.Substring(filename.IndexOf("BCTN"));
            }

            switch (temp)
            {
                case "CN_BCTC_KT":
                    __result = String.Format(Const.CN_BCTC_KT, nam);
                    break;
                case "CN_BCTC":
                    __result = String.Format(Const.CN_BCTC, nam);
                    break;
                case "BCTC":
                    __result = quy == "5" ? String.Format(Const.CN_BCTC, nam) : String.Format(Const.BCTC, quy, nam);
                    break;
                case "BCTC_KT":
                    __result = quy == "5" ? String.Format(Const.CN_BCTC_KT, nam) : String.Format(Const.BCTC_KT, quy, nam);
                    break;
                case "CN_BCTC_HN":
                case "BCTC_HN":
                    __result = quy == "5" ? String.Format(Const.CN_BCTC_HN, nam) : String.Format(Const.BCTC_HN, quy, nam);
                    break;
                case "CN_BCTC_M":
                case "BCTC_M":
                    __result = quy == "5" ? String.Format(Const.CN_BCTC_M, nam) : String.Format(Const.BCTC_M, quy, nam);
                    break;
                case "CN_BCTC_KQKD_HN":
                case "BCTC_KQKD_HN":
                    __result = quy == "5" ? String.Format(Const.CN_BCTC_KQKD_HN, nam) : String.Format(Const.BCTC_KQKD_HN, quy, nam);
                    break;
                case "CN_BCTC_KQKD":
                case "BCTC_KQKD":
                    __result = quy == "5" ? String.Format(Const.CN_BCTC_KQKD, nam) : String.Format(Const.BCTC_KQKD, quy, nam);
                    break;
                case "CN_BCTC_KQKD_M":
                case "BCTC_KQKD_M":
                    __result = quy == "5" ? String.Format(Const.CN_BCTC_KQKD_M, nam) : String.Format(Const.BCTC_KQKD_M, quy, nam);
                    break;
                case "CN_BCTC_TT":
                case "BCTC_TT":
                    __result = quy == "5" ? String.Format(Const.CN_BCTC_TT, nam) : String.Format(Const.BCTC_TT, quy, nam);
                    break;
                case "CN_BCTC_HNKT":
                case "BCTC_HNKT":
                    __result = quy == "5" ? String.Format(Const.CN_BCTC_HNKT, nam) : String.Format(Const.BCTC_HNKT, quy, nam);
                    break;
                case "CN_BCTC_MKT":
                case "BCTC_MKT":
                    __result = quy == "5" ? String.Format(Const.CN_BCTC_MKT, nam) : String.Format(Const.BCTC_MKT, quy, nam);
                    break;
                case "CN_BCTC_KQKD_KT":
                case "BCTC_KQKD_KT":
                    __result = quy == "5" ? String.Format(Const.CN_BCTC_KQKD_KT, nam) : String.Format(Const.BCTC_KQKD_KT, quy, nam);
                    break;
                case "CN_BCTC_HNSX":
                case "BCTC_HNSX":
                    __result = quy == "5" ? String.Format(Const.CN_BCTC_HNSX, nam) : String.Format(Const.BCTC_HNSX, quy, nam);
                    break;
                case "CN_BCTC_MSX":
                case "BCTC_MSX":
                    __result = quy == "5" ? String.Format(Const.CN_BCTC_MSX, nam) : String.Format(Const.BCTC_MSX, quy, nam);
                    break;
                case "CN_BCTC_KQKD_SX":
                case "BCTC_KQKD_SX":
                    __result = quy == "5" ? String.Format(Const.CN_BCTC_KQKD_SX, nam) : String.Format(Const.BCTC_KQKD_SX, quy, nam);
                    break;
                case "CN_BCTC_TM":
                case "BCTC_TM":
                    __result = quy == "5" ? String.Format(Const.CN_BCTC_TM, nam) : String.Format(Const.BCTC_TM, quy, nam);
                    break;
                case "BCB":
                    __result = Const.BCB;
                    break;
                case "BCTN":
                    __result = String.Format(Const.BCTN, nam);
                    break;
                case "NQHDQT":
                    __result = String.Format(Const.NQHDQT, nam);
                    break;
                case "NQDHCD_TN":
                    __result = String.Format(Const.NQDHCD_TN, nam);
                    break;
                case "NQDHCD_BT":
                    __result = String.Format(Const.NQDHCD_BT, nam);
                    break;
                case "NQDHCD":
                    __result = String.Format(Const.NQDHCD, nam);
                    break;
                default:
                    break;
            }

            return __result;
        }
        private static bool IsNumber(object stringTest)
        {
            try
            {
                Convert.ToInt32(stringTest);
                return true;
            }
            catch (FormatException ex)
            {
                return false;
            }
        }
    }
    class FileSort : IComparer
    {

        public int Compare(object x, object y)
        {
            FileInfo f1 = x as FileInfo;
            FileInfo f2 = y as FileInfo;
            return f2.CreationTime.CompareTo(f1.CreationTime);
        }

    }
    class FolderSort : IComparer
    {

        public int Compare(object x, object y)
        {
            DirectoryInfo f1 = x as DirectoryInfo;
            DirectoryInfo f2 = y as DirectoryInfo;
            return f2.Name.CompareTo(f1.Name);
        }

    }
}
