using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Cafef_DAL;

namespace CafeF.Redis.BO
{
    public class NewsTitleHotHelper
    {
        public static DataTable CafeF_News_Title_Hot_Select(int Cat_ID)
        {
            string cacheName = "CafeF_News_Title_Hot_Select_" + Cat_ID;
            DataTable __result = NewsHelper_NoCached.GetFromCacheDependency<DataTable>(cacheName);
            if (__result == null)
            {
                using (MainDB db = new MainDB())
                {
                    __result = db.StoredProcedures.CafeF_News_Title_Hot_Select(Cat_ID);
                    db.Close();
                }

                NewsHelper_NoCached.SaveToCacheDependency(Const.DATABASE_NAME, "News_Title_Hot", cacheName, __result);
            }

            return __result;
        }

        //private static string StrNewsTitleHot = "<a href=\"{0}\" class=\"dangky\" {1}" + "<img src=\"http://cafef3.vcmedia.vn/images/images/muiten1.gif\" border=\"0\">&nbsp;{2}{3}</a>&nbsp;&nbsp;&nbsp;";
        private static string StrNewsTitleHot = "<li><a href=\"{0}\" {1}" + ">{2}{3}</a></li>";
        private static string strNewIcon = "<img align=\"AbsMiddle\" src=\"http://cafef3.vcmedia.vn/images/new.gif\" border=\"0\" />";
        //private static string strFormat = "style=\"font: Times New Roman; font-size: 13px;\">";
        private static string strFormat = "";

        public static String ReturnNewsTitleHot(int Cat_ID)
        {
            string cacheName = "ReturnNewsTitleHot_" + Cat_ID;
            String __result = NewsHelper_NoCached.GetFromCacheDependency<String>(cacheName);
            if (__result == null)
            {
                string value = "";
                DataTable dt = new DataTable();
                dt = CafeF_News_Title_Hot_Select(Cat_ID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    int count = dt.Rows.Count;
                    bool isNewIcon = false;
                    string strformat = "";
                    string link = "";
                    string title = "";
                    for (int i = 0; i < count; i++)
                    {
                        isNewIcon = Convert.ToBoolean(dt.Rows[i]["NewIcon"]);
                        strformat = dt.Rows[i]["Format"].ToString();
                        link = dt.Rows[i]["Link"].ToString();
                        title = dt.Rows[i]["Title"].ToString();
                        if (strformat == "")
                            strformat = strFormat;
                        if (isNewIcon)
                        {
                            value += String.Format(StrNewsTitleHot, link, strformat, title, strNewIcon);
                        }
                        else
                        {
                            value += String.Format(StrNewsTitleHot, link, strformat, title, "");
                        }
                    }

                    __result = value;

                    NewsHelper_NoCached.SaveToCacheDependency(Const.DATABASE_NAME, "News_Title_Hot", cacheName, __result);
                }
            }

            return __result;
        }
    }
}
