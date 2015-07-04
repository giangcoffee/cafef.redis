using System;
using System.Data;
using System.Web;
using System.Text;
using System.Configuration;
using CafeF.BCTC.DAL;
using CafeF.BCTC.BO.Cache;
using CafeF.BCTC.BO.Utilitis;

namespace CafeF.BCTC.DA.BO.BaoCaoTaiChinh
{
    public class BCTCBO
    {
        public class BCTCNoCacheSql
        {
            public static DataTable sp_CafeF_DataCrawler_GetFinanceValue(string symbol, string type, int year, int quarter)
            {
                DataTable __result = new DataTable();
                using (MainDB db = new MainDB())
                {
                    __result = db.StoredProcedures.sp_CafeF_DataCrawler_GetFinanceValue(symbol, type, year, quarter);
                    db.Close();
                }
                return __result;
            }

            public static DataTable sp_CafeF_DataCrawler_GetFinanceValue_Recent4part(string symbol, string type, int year, int quarter, string showtype)
            {
                DataTable __result = new DataTable();
                int mapcode = 0;
                string _style = "";
                string _exco = "";
                string _imgexco = "";
                double maxValue = 0;
                double minValue = 0;
                string mc = "";
                
                using (MainDB db = new MainDB())
                {
                    __result = db.StoredProcedures.sp_CafeF_DataCrawler_GetFinanceValue_Recent4part(symbol, type, year, quarter);
                    db.Close();
                }
                DataTable dtData = new DataTable();
                dtData = __result.DefaultView.ToTable(true, new string[] { "Symbol", "MappingName", "MappingCode","OrderCode" });
                DataTable dtTime = __result.DefaultView.ToTable(true, new string[] { "FinanceYear", "FinancePart" });
                for (int i = 0; i < dtTime.Rows.Count; i++)
                    dtData.Columns.Add(dtTime.Rows[i][1].ToString() + '-' + dtTime.Rows[i][0].ToString());
                dtData.Columns.Add("Chart");
                dtData.Columns.Add("SStyle");
                dtData.Columns.Add("ForEC");
                dtData.Columns.Add("ImgEC");
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    for (int j = 0; j < dtTime.Rows.Count; j++)
                    {
                        try
                        {
                            dtData.Rows[i][dtTime.Rows[j][1].ToString() + '-' + dtTime.Rows[j][0].ToString()] = __result.Select("MappingCode='" + dtData.Rows[i][2].ToString() + "'" + " AND FinanceYear=" + dtTime.Rows[j][0].ToString() + " AND FinancePart=" + dtTime.Rows[j][1].ToString())[0]["ChitieuValue"];
                            //if (double.Parse(dtData.Rows[i][dtTime.Rows[j][1].ToString() + '-' + dtTime.Rows[j][0].ToString()].ToString()) == -1)
                            //{
                            //    dtData.Rows[i][dtTime.Rows[j][1].ToString() + '-' + dtTime.Rows[j][0].ToString()] = "";
                            //}
                        }
                        catch (Exception) { }
                    }
                    StringBuilder chart = new StringBuilder();
                    maxValue = double.Parse(dtData.Rows[i][4].ToString());
                    if (double.Parse(dtData.Rows[i][5].ToString()) > maxValue) maxValue = double.Parse(dtData.Rows[i][5].ToString());
                    if (double.Parse(dtData.Rows[i][6].ToString()) > maxValue) maxValue = double.Parse(dtData.Rows[i][6].ToString());
                    if (double.Parse(dtData.Rows[i][7].ToString()) > maxValue) maxValue = double.Parse(dtData.Rows[i][7].ToString());
                    minValue = double.Parse(dtData.Rows[i][4].ToString());
                    if (double.Parse(dtData.Rows[i][5].ToString()) < minValue) minValue = double.Parse(dtData.Rows[i][5].ToString());
                    if (double.Parse(dtData.Rows[i][6].ToString()) < minValue) minValue = double.Parse(dtData.Rows[i][6].ToString());
                    if (double.Parse(dtData.Rows[i][7].ToString()) < minValue) minValue = double.Parse(dtData.Rows[i][7].ToString());
                    if (maxValue == -1 || maxValue == -2) { maxValue = 0; }
                    if (minValue == -1 || minValue == -2) { minValue = 0; }

                    chart.Append("<center><table class='BaoCaoTaiChinh_Chart' border='0' cellpadding='0' cellspacing='0' style='margin-top:3px;'><tr>");
                    if (double.Parse(dtData.Rows[i][4].ToString()) != -1 && double.Parse(dtData.Rows[i][4].ToString()) != -2){
                        chart.Append(Common.GenerateChart(double.Parse(dtData.Rows[i][4].ToString()), maxValue, minValue));
                    }
                    chart.Append("<td><img alt='' style='width: 3px;' src='http://cafef3.vcmedia.vn/images/images/spacer.gif'/></td>");
                    if (double.Parse(dtData.Rows[i][5].ToString()) != -1 && double.Parse(dtData.Rows[i][5].ToString()) != -2){
                        chart.Append(Common.GenerateChart(double.Parse(dtData.Rows[i][5].ToString()), maxValue, minValue));
                    }
                    chart.Append("<td><img alt='' style='width: 3px;' src='http://cafef3.vcmedia.vn/images/images/spacer.gif'/></td>");
                    if (double.Parse(dtData.Rows[i][6].ToString()) != -1 && double.Parse(dtData.Rows[i][6].ToString()) != -2){
                        chart.Append(Common.GenerateChart(double.Parse(dtData.Rows[i][6].ToString()), maxValue, minValue));
                    }
                    chart.Append("<td><img alt='' style='width: 3px;' src='http://cafef3.vcmedia.vn/images/images/spacer.gif'/></td>");
                    if (double.Parse(dtData.Rows[i][7].ToString()) != -1 && double.Parse(dtData.Rows[i][7].ToString()) != -2){
                        chart.Append(Common.GenerateChart(double.Parse(dtData.Rows[i][7].ToString()), maxValue, minValue));
                    }
                    chart.Append("<tr></table></center>");
                    dtData.Rows[i]["Chart"] = chart;
                    try
                    {
                        mapcode = int.Parse(dtData.Rows[i][2].ToString());
                    }
                    catch (Exception) { }
                    if (type.Equals("BSheet"))
                    {
                        if (mapcode % 100 == 0)
                        {
                            _style = "color:#333333;font-weight:bold;font-size:13px";
                            _exco = "";
                            _imgexco = "&nbsp;&nbsp;&nbsp;&nbsp;";
                        }
                        else if (mapcode % 10 == 0 && mapcode % 100 != 0)
                        {
                            _style = "color:#014377;font-weight:bold;";
                            _exco = "style='cursor:pointer' onclick='expandcollapse(this);'";
                            _imgexco = "<img id='img_"+mapcode.ToString()+"' border='0' src='http://images1.cafef.vn/batdongsan/Images/media/plusi.gif' />";
                        }
                        else if (mapcode < 10)
                        {
                            _style = "color:black;font-weight:bold;font-size:13px;text-align:center;";
                            _exco = "";
                            _imgexco = "&nbsp;&nbsp;&nbsp;&nbsp;";
                        }
                        else
                        {                            
                            _style = "color:#014377;";
                            if (showtype.Equals("1"))
                            {
                                _exco = "";
                            }
                            else
                            {
                                _exco = "style='display:none'";
                            }
                            _imgexco = "&nbsp;&nbsp;&nbsp;&nbsp;";
                        }
                    }
                    else if (type.Equals("IncSta"))
                    {
                        mc = dtData.Rows[i][2].ToString();
                        if (dtData.Rows[i][1].ToString().Contains("-"))
                        {
                            //_exco = "style='display:none'";
                            _style = "color:#014377;font-style:italic;";
                            //_imgexco = "&nbsp;&nbsp;&nbsp;&nbsp;";
                        }
                        else if (mc.Equals("01") || mc.Equals("02") || mc.Equals("10") || mc.Equals("20") || mc.Equals("30") || mc.Equals("40") || mc.Equals("50") || mc.Equals("60") || mc.Equals("62"))
                        {
                            _style = "color:#014377;font-weight:bold;";
                            //_exco = "style='cursor:pointer' onclick='expandcollapse(this);'";
                            //_imgexco = "<img id='img_"+mc+"' border='0' src='http://images1.cafef.vn/batdongsan/Images/media/plusi.gif' />";
                        }
                        else
                        {
                            //_exco = "style='display:none'";
                            _style = "color:#014377;";
                            //_imgexco = "&nbsp;&nbsp;&nbsp;&nbsp;";
                        }
                    }
                    else if (type.Equals("CashFlow") || type.Equals("CashFlowDirect"))
                    {
                        mc = dtData.Rows[i][2].ToString();
                        if (mc.Equals("001") || mc.Equals("002") || mc.Equals("003") || mc.Equals("010") || mc.Equals("110") || mc.Equals("210") || mc.Equals("310"))
                        {
                            _style = "color:black;font-weight:bold;font-size:13px";
                        }
                        else
                        {
                            _style = "color:#014377;";
                        }
                    }
                    dtData.Rows[i]["SStyle"] = _style;
                    dtData.Rows[i]["ForEC"] = _exco;
                    dtData.Rows[i]["ImgEC"] = _imgexco;
                }
                return dtData;
            }

            public static DataTable sp_CafeF_DataCrawler_GetFinanceValue_Recent4year(string symbol, string type, int year, string showtype)
            {
                DataTable __result = new DataTable();
                int mapcode = 0;
                string _style = "";
                string _exco = "";
                string _imgexco = "";
                double maxValue = 0;
                double minValue = 0;
                string mc = "";
                using (MainDB db = new MainDB())
                {
                    __result = db.StoredProcedures.sp_CafeF_DataCrawler_GetFinanceValue_Recent4year(symbol, type, year);
                    db.Close();
                }
                DataTable dtData = new DataTable();
                dtData = __result.DefaultView.ToTable(true, new string[] { "Symbol", "MappingName", "MappingCode", "OrderCode" });
                DataTable dtTime = __result.DefaultView.ToTable(true, new string[] { "FinanceYear" });
                for (int i = 0; i < dtTime.Rows.Count; i++)
                    dtData.Columns.Add(dtTime.Rows[i][0].ToString());
                dtData.Columns.Add("Chart");
                dtData.Columns.Add("SStyle");
                dtData.Columns.Add("ForEC");
                dtData.Columns.Add("ImgEC");
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    for (int j = 0; j < dtTime.Rows.Count; j++)
                    {
                        try
                        {
                            dtData.Rows[i][dtTime.Rows[j][0].ToString()] = __result.Select("MappingCode='" + dtData.Rows[i][2].ToString() + "'" + " AND FinanceYear=" + dtTime.Rows[j][0].ToString())[0]["ChitieuValue"];
                            //if (double.Parse(dtData.Rows[i][dtTime.Rows[j][0].ToString()].ToString()) == -1)
                            //{
                            //    dtData.Rows[i][dtTime.Rows[j][0].ToString()] = "";
                            //}
                        }
                        catch (Exception) { }
                    }
                    StringBuilder chart = new StringBuilder();
                    maxValue = double.Parse(dtData.Rows[i][4].ToString());
                    if (double.Parse(dtData.Rows[i][5].ToString()) > maxValue) maxValue = double.Parse(dtData.Rows[i][5].ToString());
                    if (double.Parse(dtData.Rows[i][6].ToString()) > maxValue) maxValue = double.Parse(dtData.Rows[i][6].ToString());
                    if (double.Parse(dtData.Rows[i][7].ToString()) > maxValue) maxValue = double.Parse(dtData.Rows[i][7].ToString());
                    minValue = double.Parse(dtData.Rows[i][4].ToString());
                    if (double.Parse(dtData.Rows[i][5].ToString()) < minValue) minValue = double.Parse(dtData.Rows[i][5].ToString());
                    if (double.Parse(dtData.Rows[i][6].ToString()) < minValue) minValue = double.Parse(dtData.Rows[i][6].ToString());
                    if (double.Parse(dtData.Rows[i][7].ToString()) < minValue) minValue = double.Parse(dtData.Rows[i][7].ToString());
                    if (maxValue == -1 || maxValue == -2) { maxValue = 0; }
                    if (minValue == -1 || minValue == -2) { minValue = 0; }

                    chart.Append("<center><table class='BaoCaoTaiChinh_Chart' border='0' cellpadding='0' cellspacing='0' style='margin-top:3px;'><tr>");
                    if (double.Parse(dtData.Rows[i][4].ToString()) != -1 && double.Parse(dtData.Rows[i][4].ToString()) != -2)
                    {
                        chart.Append(Common.GenerateChart(double.Parse(dtData.Rows[i][4].ToString()), maxValue, minValue));
                    }
                    chart.Append("<td><img alt='' style='width: 3px;' src='http://cafef3.vcmedia.vn/images/images/spacer.gif'/></td>");
                    if (double.Parse(dtData.Rows[i][5].ToString()) != -1 && double.Parse(dtData.Rows[i][5].ToString()) != -2)
                    {
                        chart.Append(Common.GenerateChart(double.Parse(dtData.Rows[i][5].ToString()), maxValue, minValue));
                    }
                    chart.Append("<td><img alt='' style='width: 3px;' src='http://cafef3.vcmedia.vn/images/images/spacer.gif'/></td>");
                    if (double.Parse(dtData.Rows[i][6].ToString()) != -1 && double.Parse(dtData.Rows[i][6].ToString()) != -2)
                    {
                        chart.Append(Common.GenerateChart(double.Parse(dtData.Rows[i][6].ToString()), maxValue, minValue));
                    }
                    chart.Append("<td><img alt='' style='width: 3px;' src='http://cafef3.vcmedia.vn/images/images/spacer.gif'/></td>");
                    if (double.Parse(dtData.Rows[i][7].ToString()) != -1 && double.Parse(dtData.Rows[i][7].ToString()) != -2)
                    {
                        chart.Append(Common.GenerateChart(double.Parse(dtData.Rows[i][7].ToString()), maxValue, minValue));
                    }
                    chart.Append("<tr></table></center>");
                    dtData.Rows[i]["Chart"] = chart;
                    try
                    {
                        mapcode = int.Parse(dtData.Rows[i][2].ToString());
                    }
                    catch (Exception) { }
                    if (type.Equals("BSheet"))
                    {
                        if (mapcode % 100 == 0)
                        {
                            _style = "color:#333333;font-weight:bold;font-size:13px";
                            _exco = "";
                            _imgexco = "&nbsp;&nbsp;&nbsp;&nbsp;";
                        }
                        else if (mapcode % 10 == 0 && mapcode % 100 != 0)
                        {
                            _style = "color:#014377;font-weight:bold;";
                            _exco = "style='cursor:pointer' onclick='expandcollapse(this);'";
                            _imgexco = "<img id='img_" + mapcode.ToString() + "' border='0' src='http://images1.cafef.vn/batdongsan/Images/media/plusi.gif' />";
                        }
                        else if (mapcode < 10)
                        {
                            _style = "color:black;font-weight:bold;font-size:13px;text-align:center;";
                            _exco = "";
                            _imgexco = "&nbsp;&nbsp;&nbsp;&nbsp;";
                        }
                        else
                        {
                            _style = "color:#014377;";
                            if (showtype.Equals("1"))
                            {
                                _exco = "";
                            }
                            else
                            {
                                _exco = "style='display:none'";
                            }
                            _imgexco = "&nbsp;&nbsp;&nbsp;&nbsp;";
                        }
                    }
                    else if (type.Equals("IncSta"))
                    {
                        mc = dtData.Rows[i][2].ToString();
                        if (dtData.Rows[i][1].ToString().Contains("-"))
                        {
                            //_exco = "style='display:none'";
                            _style = "color:#014377;font-style:italic;";
                            //_imgexco = "&nbsp;&nbsp;&nbsp;&nbsp;";
                        }
                        else if (mc.Equals("01") || mc.Equals("02") || mc.Equals("10") || mc.Equals("20") || mc.Equals("30") || mc.Equals("40") || mc.Equals("50") || mc.Equals("60") || mc.Equals("62"))
                        {
                            _style = "color:#014377;font-weight:bold;";
                            //_exco = "style='cursor:pointer' onclick='expandcollapse(this);'";
                            //_imgexco = "<img id='img_" + mc + "' border='0' src='http://images1.cafef.vn/batdongsan/Images/media/plusi.gif' />";
                        }
                        else
                        {
                            //_exco = "style='display:none'";
                            _style = "color:#014377;";
                            //_imgexco = "&nbsp;&nbsp;&nbsp;&nbsp;";
                        }
                    }
                    else if (type.Equals("CashFlow") || type.Equals("CashFlowDirect"))
                    {
                        mc = dtData.Rows[i][2].ToString();
                        if (mc.Equals("001") || mc.Equals("002") || mc.Equals("003") || mc.Equals("010") || mc.Equals("110") || mc.Equals("210") || mc.Equals("310"))
                        {
                            _style = "color:black;font-weight:bold;font-size:13px";
                        }
                        else
                        {
                            _style = "color:#014377;";
                        }
                    }
                    dtData.Rows[i]["SStyle"] = _style;
                    dtData.Rows[i]["ForEC"] = _exco;
                    dtData.Rows[i]["ImgEC"] = _imgexco;
                }
                return dtData;
            }
        }

        public class BCTCCacheSql
        {
            public static DataTable sp_CafeF_DataCrawler_GetFinanceValue(string symbol, string type, int year, int quarter)
            {
                string cacheName = CacheSqlHelper.CacheNameFormat.sp_CafeF_DataCrawler_GetFinanceValue(symbol, type, year, quarter);
                DataTable __result = CacheSqlHelper.GetFromCacheDependency<DataTable>(cacheName);
                if (__result == null)
                {
                    __result = BCTCNoCacheSql.sp_CafeF_DataCrawler_GetFinanceValue(symbol, type, year, quarter);
                    CacheSqlHelper.SaveToCacheDependency(Const.DATABASE_NAME, new string[] { Const.TABLE_CHITIEU, Const.TABLE_MAPPING, Const.TABLE_SYMBOL, Const.TABLE_SYMBOLTYPE, Const.TABLE_VALUE }, cacheName, __result);
                }

                return __result;
            }

            public static DataTable sp_CafeF_DataCrawler_GetFinanceValue_Recent4part(string symbol, string type, int year, int quarter, string showtype)
            {
                string cacheName = CacheSqlHelper.CacheNameFormat.sp_CafeF_DataCrawler_GetFinanceValue_Recent4part(symbol, type, year, quarter, showtype);
                DataTable __result = CacheSqlHelper.GetFromCacheDependency<DataTable>(cacheName);
                if (__result == null)
                {
                    __result = BCTCNoCacheSql.sp_CafeF_DataCrawler_GetFinanceValue_Recent4part(symbol, type, year, quarter, showtype);
                    CacheSqlHelper.SaveToCacheDependency(Const.DATABASE_NAME, new string[] { Const.TABLE_CHITIEU, Const.TABLE_MAPPING, Const.TABLE_SYMBOL, Const.TABLE_SYMBOLTYPE, Const.TABLE_VALUE }, cacheName, __result);
                }

                return __result;
            }

            public static DataTable sp_CafeF_DataCrawler_GetFinanceValue_Recent4year(string symbol, string type, int year, string showtype)
            {
                string cacheName = CacheSqlHelper.CacheNameFormat.sp_CafeF_DataCrawler_GetFinanceValue_Recent4year(symbol, type, year, showtype);
                DataTable __result = CacheSqlHelper.GetFromCacheDependency<DataTable>(cacheName);
                if (__result == null)
                {
                    __result = BCTCNoCacheSql.sp_CafeF_DataCrawler_GetFinanceValue_Recent4year(symbol, type, year, showtype);
                    CacheSqlHelper.SaveToCacheDependency(Const.DATABASE_NAME, new string[] { Const.TABLE_CHITIEU, Const.TABLE_MAPPING, Const.TABLE_SYMBOL, Const.TABLE_SYMBOLTYPE, Const.TABLE_VALUE }, cacheName, __result);
                }

                return __result;
            }
        }
    }
}
