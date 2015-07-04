using System;
using System.Collections;
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
using CafeF.DLVM.DA.BO.DuLieuViMo;
using CafeF.DLVM.BO.Utilitis;

namespace CafeF.Redis.Page.UserControl.DuLieuViMo
{
    public partial class CPIBeforeChart : System.Web.UI.UserControl
    {
        private int parentID = 12;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.SetDefault();
                this.GenChart2(parentID, 5, 11, 5, 11, 2009, 2009, 2009);
                this.GenChart3(12, 10, 12, 1, 9, 2008, 2008, 2009);
                this.GenChart4(12, 1, 12, 1, 12, 2006, 2007, 2008);
            }
        }

        private void SetDefault()
        {
            Common.FillDataToDropDownList(dlYearQuy1, 1999, 2012);
            Common.FillDataToDropDownList(dlYearQuy2, 1999, 2012);
            Common.FillDataToDropDownList(dlYear1YoY, 1999, 2012);
            Common.FillDataToDropDownList(dlYear2YoY, 1999, 2012);
            dlMonth1B.SelectedValue = "5";
            dlMonth2B.SelectedValue = "11";
            dlYear1B.SelectedValue = "2009";
            dlYear2B.SelectedValue = "2009";
            dlQuarter1.SelectedValue = "4";
            dlQuarter2.SelectedValue = "3";
            dlYearQuy1.SelectedValue = "2008";
            dlYearQuy2.SelectedValue = "2009";
            dlYear1YoY.SelectedValue = "2006";
            dlYear2YoY.SelectedValue = "2008";
            hdfType.Value = "1";
        }
        
        protected void btnView2_Click(object sender, EventArgs e)
        {
            int time1 = int.Parse(dlMonth1B.SelectedValue);
            int time2 = 0;
            int time3 = 0;
            int time4 = int.Parse(dlMonth2B.SelectedValue);
            int year1 = int.Parse(dlYear1B.SelectedValue);
            int year2 = 0;
            int year3 = int.Parse(dlYear2B.SelectedValue);
            if (year1 == year3)
            {
                time2 = time4;
                time3 = time1;
                year2 = year3;
            }
            else
            {
                int kc = year3 - year1 - 1;
                time2 = 12;
                time3 = 1;
                year2 = year1 + kc;
            }
            this.GenChart2(parentID, time1, time2, time3, time4, year1, year2, year3);
            hdfType.Value = "1";
        }

        private void GenChart2(int parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
        {
            string srcImg = "";
            string phongto = "";
            string ft = "";
            for (int fil = 0; fil < chkChitieu2.Items.Count; fil++)
            {
                if (chkChitieu2.Items[fil].Selected)
                {
                    ft += "." + chkChitieu2.Items[fil].Value;
                }
            }
            for (int li = 0; li < chkChitieuchitiet3.Items.Count; li++)
            {
                if (chkChitieuchitiet3.Items[li].Selected)
                {
                    ft += "." + chkChitieuchitiet3.Items[li].Value;
                }
            }
            ft = ft.TrimStart('.');
            string keyr = String.Format("Cafef.Chart.BinaryData.CPICu.Thang.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, CheckBox2.Checked);
            string keyrPhongTo = String.Format("Cafef.Chart.BinaryData.CPICu.Thang.Phongto.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, CheckBox2.Checked);
            srcImg = Common.GetImageFromRedis(keyr, srcImg);
            phongto = Common.GetImageFromRedis(keyrPhongTo, phongto);
            if (srcImg.Equals("") || phongto.Equals(""))
            {
                string chxl = "";
                string chd = "";
                string chdl = "";
                string chco = "";
                string chxp = "1,";
                string chm = "";
                string range = "";
                string chds = "";
                DataTable dt = IndexBO.IndexCacheSql.pr_Chart_Month(parentid, time1, time2, time3, time4, year1, year2, year3);
                DataTable dtTemp = dt;
                if (dt.Rows.Count > 0)
                {
                    dtTemp.DefaultView.RowFilter = "CCode = '" + dt.Rows[0]["CCode"].ToString() + "'";
                    for (int i = 0; i < dtTemp.DefaultView.Count; i++)
                    {
                        int t = int.Parse(dtTemp.DefaultView[i]["MTime"].ToString());
                        int y = int.Parse(dtTemp.DefaultView[i]["MYear"].ToString());
                        if (t < 10)
                        {
                            chxl += "0" + t.ToString() + "/" + y.ToString().Remove(0, 2) + "|";
                        }
                        else
                        {
                            chxl += t.ToString() + "/" + y.ToString().Remove(0, 2) + "|";
                        }
                        int itemp = i;
                        chxp += itemp.ToString() + ",";
                    }
                }

                string[] chxlArr = chxl.TrimEnd('|').Split('|');
                int countF = chxlArr.Length;
                string filter = "";
                for (int fil = 0; fil < chkChitieu2.Items.Count; fil++)
                {
                    if (chkChitieu2.Items[fil].Selected)
                    {
                        filter += "OR CCode=" + chkChitieu2.Items[fil].Value;
                    }
                }
                for (int li = 0; li < chkChitieuchitiet3.Items.Count; li++)
                {
                    if (chkChitieuchitiet3.Items[li].Selected)
                    {
                        filter += "OR CCode=" + chkChitieuchitiet3.Items[li].Value;
                    }
                }
                char[] c = { 'O', 'R' };
                dt.DefaultView.RowFilter = filter.TrimStart(c);
                string[] colors = { "4f81be", "c1504d", "9cbc59", "8064a3", "3c8da3", "cc7b38", "4f81bd", "c0504d", "9bbb59", "8064a2", "4bacc6", "f79646", "aabad7", "d9aaa9", "c6d6ac" };

                for (int i = 0; i < dt.DefaultView.Count; i++)
                {
                    double ind = 0;
                    double bnind = (double.Parse(dt.DefaultView[i]["MIndex"].ToString()));
                    double unind = 0;
                    int ti = (int.Parse(dt.DefaultView[i]["MTime"].ToString())) - 1;
                    DataTable dtOne = new DataTable();
                    if (ti == 0)
                    {
                        dtOne = IndexBO.IndexCacheSql.pr_IndexMonth_SelectByIDAndTime(int.Parse(dt.DefaultView[i]["Category_ID"].ToString()), 12, (int.Parse(dt.DefaultView[i]["MYear"].ToString())) - 1);
                    }
                    else
                    {
                        dtOne = IndexBO.IndexCacheSql.pr_IndexMonth_SelectByIDAndTime(int.Parse(dt.DefaultView[i]["Category_ID"].ToString()), ti, int.Parse(dt.DefaultView[i]["MYear"].ToString()));
                    }
                    if (dtOne.Rows.Count > 0)
                    {
                        unind = double.Parse(dtOne.Rows[0]["MIndex"].ToString());
                    }
                    ind = (bnind / unind - 1) * 100;
                    chd += String.Format("{0:f}", ind) + ",";

                    if ((i + 1) % countF == 0)
                    {
                        chd = chd.TrimEnd(',') + "|";
                    }
                }

                DataTable dtDistinct = dt.DefaultView.ToTable(true, new string[] { "CCode", "CName" });

                for (int j = 0; j < dtDistinct.Rows.Count; j++)
                {
                    chdl += dtDistinct.Rows[j]["CName"].ToString().Replace(' ', '+') + "|";
                    chco += colors[j] + ",";
                    chm += "|N,FF0000," + j + ",-1,10";
                }

                string[] rangeTemp = chd.TrimEnd('|').Replace('|', ',').Split(',');
                bubbleSort(rangeTemp, rangeTemp.Length);
                string rangeTemp1 = "";
                string rangeTemp2 = "";
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= -2)
                {
                    rangeTemp1 = "0";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 0)
                {
                    rangeTemp1 = "2";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 2)
                {
                    rangeTemp1 = "4";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 4)
                {
                    rangeTemp1 = "6";
                }

                if (double.Parse(rangeTemp[0]) <= 6)
                {
                    rangeTemp2 = "4";
                }
                if (double.Parse(rangeTemp[0]) <= 4)
                {
                    rangeTemp2 = "2";
                }
                if (double.Parse(rangeTemp[0]) <= 2)
                {
                    rangeTemp2 = "0";
                }
                if (double.Parse(rangeTemp[0]) <= 0)
                {
                    rangeTemp2 = "-2";
                }

                range = rangeTemp2 + "," + rangeTemp1;
                chds = rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1;

                srcImg = "http://chart.apis.google.com/chart";
                srcImg += "?chf=c,s,FFFFFF";
                srcImg += "&chxl=0:|" + chxl.TrimEnd('|');// +"1:|-2%|0%|2%|4%|6%";
                srcImg += "&chxp=" + chxp;// +"|1,-2,0,2,4,6";
                srcImg += "&chxr=0,0," + countF + "|1," + range;
                srcImg += "&chxt=x,y";
                srcImg += "&chs=600x300";
                srcImg += "&cht=lc";
                srcImg += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chds=" + chds;
                srcImg += "&chd=t:" + chd.TrimEnd('|');
                srcImg += "&chdl=" + chdl.TrimEnd('|');
                srcImg += "&chdlp=b";
                srcImg += "&chls=2|2|2|2";
                srcImg += "&chma=40,20,20,30";
                srcImg += "&chg=0,12.5,1,1";
                srcImg += "&chxs=1,676767,11.5,0.5,l,676767";
                if (CheckBox2.Checked)
                {
                    srcImg += "&chm=s,4f81be,0,-1,5|s,c1504d,1,-1,5|s,9cbc59,2,-1,5|s,8064a3,3,-1,5|s,3c8da3,4,-1,5|s,cc7b38,5,-1,5|s,4f81bd,6,-1,5|s,c0504d,7,-1,5|s,9bbb59,8,-1,5|s,8064a2,9,-1,5|s,4bacc6,10,-1,5|s,f79646,11,-1,5|s,aabad7,12,-1,5|s,d9aaa9,13,-1,5|s,c6d6ac,14,-1,5" + chm;
                }
                else
                {
                    srcImg += "&chm=s,4f81be,0,-1,5|s,c1504d,1,-1,5|s,9cbc59,2,-1,5|s,8064a3,3,-1,5|s,3c8da3,4,-1,5|s,cc7b38,5,-1,5|s,4f81bd,6,-1,5|s,c0504d,7,-1,5|s,9bbb59,8,-1,5|s,8064a2,9,-1,5|s,4bacc6,10,-1,5|s,f79646,11,-1,5|s,aabad7,12,-1,5|s,d9aaa9,13,-1,5|s,c6d6ac,14,-1,5";
                }
                srcImg += "&chtt=Chỉ+số+giá+tiêu+dùng(CPI)";
                Common.GetImageFromRedis(keyr, srcImg);

                phongto = "http://chart.apis.google.com/chart";
                phongto += "?chf=c,s,FFFFFF";
                phongto += "&chxl=0:|" + chxl.TrimEnd('|');// +"1:|-2%|0%|2%|4%|6%";
                phongto += "&chxp=" + chxp;// +"|1,-2,0,2,4,6";
                phongto += "&chxr=0,0," + countF + "|1," + range;
                phongto += "&chxt=x,y";
                phongto += "&chs=890x320";
                phongto += "&cht=lc";
                phongto += "&chco=" + chco.TrimEnd(',');
                phongto += "&chds=" + chds;
                phongto += "&chd=t:" + chd.TrimEnd('|');
                phongto += "&chdl=" + chdl.TrimEnd('|');
                phongto += "&chdlp=b";
                phongto += "&chls=2|2|2|2";
                phongto += "&chma=40,20,20,30";
                phongto += "&chg=0,12.5,1,1";
                phongto += "&chxs=1,676767,11.5,0.5,l,676767";
                if (CheckBox2.Checked)
                {
                    phongto += "&chm=s,4f81be,0,-1,5|s,c1504d,1,-1,5|s,9cbc59,2,-1,5|s,8064a3,3,-1,5|s,3c8da3,4,-1,5|s,cc7b38,5,-1,5|s,4f81bd,6,-1,5|s,c0504d,7,-1,5|s,9bbb59,8,-1,5|s,8064a2,9,-1,5|s,4bacc6,10,-1,5|s,f79646,11,-1,5|s,aabad7,12,-1,5|s,d9aaa9,13,-1,5|s,c6d6ac,14,-1,5" + chm;
                }
                else
                {
                    phongto += "&chm=s,4f81be,0,-1,5|s,c1504d,1,-1,5|s,9cbc59,2,-1,5|s,8064a3,3,-1,5|s,3c8da3,4,-1,5|s,cc7b38,5,-1,5|s,4f81bd,6,-1,5|s,c0504d,7,-1,5|s,9bbb59,8,-1,5|s,8064a2,9,-1,5|s,4bacc6,10,-1,5|s,f79646,11,-1,5|s,aabad7,12,-1,5|s,d9aaa9,13,-1,5|s,c6d6ac,14,-1,5";
                }
                phongto += "&chtt=Chỉ+số+giá+tiêu+dùng(CPI)";
                Common.GetImageFromRedis(keyrPhongTo, phongto);
            }
            ltrChart2.Text = "<img id='imgThang' alt=\"\" src=" + srcImg + " />";
            hdfChart3.Value = phongto;
        }

        private void GenChart3(int parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
        {
            string srcImg = "";
            string phongto = "";
            string ft = "";
            for (int fil = 0; fil < chkChitieu2.Items.Count; fil++)
            {
                if (chkChitieu2.Items[fil].Selected)
                {
                    ft += "." + chkChitieu2.Items[fil].Value;
                }
            }
            for (int li = 0; li < chkChitieuchitiet3.Items.Count; li++)
            {
                if (chkChitieuchitiet3.Items[li].Selected)
                {
                    ft += "." + chkChitieuchitiet3.Items[li].Value;
                }
            }
            ft = ft.TrimStart('.');
            string keyr = String.Format("Cafef.Chart.BinaryData.CPICu.Quy.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, CheckBox2.Checked);
            string keyrPhongTo = String.Format("Cafef.Chart.BinaryData.CPICu.Thang.Phongto.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, CheckBox2.Checked);
            srcImg = Common.GetImageFromRedis(keyr, srcImg);
            phongto = Common.GetImageFromRedis(keyrPhongTo, phongto);
            if (srcImg.Equals("") || phongto.Equals(""))
            {
                string chxl = "";
                string chd = "";
                string chdl = "";
                string chco = "";
                string chxp = "1,";
                string chm = "";
                string range = "";
                string chds = "";
                DataTable dtGet = IndexBO.IndexCacheSql.pr_Chart_Month(parentid, time1, time2, time3, time4, year1, year2, year3);
                DataTable dt = Common.GenQuarterData(dtGet);
                DataTable dtTemp = dt;
                if (dt.Rows.Count > 0)
                {
                    dtTemp.DefaultView.RowFilter = "CCode = '" + dt.Rows[0]["CCode"].ToString() + "'";
                    for (int i = 0; i < dtTemp.DefaultView.Count; i++)
                    {
                        int t = int.Parse(dtTemp.DefaultView[i]["MTime"].ToString());
                        int y = int.Parse(dtTemp.DefaultView[i]["MYear"].ToString());
                        //if (t < 10)
                        //{
                        //    chxl += "0" + t.ToString() + "/" + y.ToString().Remove(0, 2) + "|";
                        //}
                        //else
                        //{
                        chxl += "q" + t.ToString() + "/" + y.ToString().Remove(0, 2) + "|";
                        //}
                        int itemp = i + 1;
                        chxp += itemp.ToString() + ",";
                    }
                }

                string[] chxlArr = chxl.TrimEnd('|').Split('|');
                int countF = chxlArr.Length;
                string filter = "";
                for (int fil = 0; fil < chkChitieu2.Items.Count; fil++)
                {
                    if (chkChitieu2.Items[fil].Selected)
                    {
                        filter += "OR CCode=" + chkChitieu2.Items[fil].Value;
                    }
                }
                for (int li = 0; li < chkChitieuchitiet3.Items.Count; li++)
                {
                    if (chkChitieuchitiet3.Items[li].Selected)
                    {
                        filter += "OR CCode=" + chkChitieuchitiet3.Items[li].Value;
                    }
                }
                char[] c = { 'O', 'R' };
                dt.DefaultView.RowFilter = filter.TrimStart(c);
                string[] colors = { "4f81be", "c1504d", "9cbc59", "8064a3", "3c8da3", "cc7b38", "4f81bd", "c0504d", "9bbb59", "8064a2", "4bacc6", "f79646", "aabad7", "d9aaa9", "c6d6ac" };

                for (int i = 0; i < dt.DefaultView.Count; i++)
                {
                    double ind = 0;
                    double bnind = (double.Parse(dt.DefaultView[i]["MIndex"].ToString()));
                    double unind = 0;
                    int ti = (int.Parse(dt.DefaultView[i]["MTime"].ToString())) - 1;
                    DataTable dtOne = new DataTable();
                    if (ti == 0)
                    {
                        dtOne = IndexBO.IndexCacheSql.pr_IndexMonth_SelectByIDAndTimeForQuy(int.Parse(dt.DefaultView[i]["Category_ID"].ToString()), 10, 12, (int.Parse(dt.DefaultView[i]["MYear"].ToString())) - 1);
                    }
                    else
                    {
                        if (ti == 1)
                        {
                            dtOne = IndexBO.IndexCacheSql.pr_IndexMonth_SelectByIDAndTimeForQuy(int.Parse(dt.DefaultView[i]["Category_ID"].ToString()), 1, 3, int.Parse(dt.DefaultView[i]["MYear"].ToString()));
                        }
                        else if (ti == 2)
                        {
                            dtOne = IndexBO.IndexCacheSql.pr_IndexMonth_SelectByIDAndTimeForQuy(int.Parse(dt.DefaultView[i]["Category_ID"].ToString()), 4, 6, int.Parse(dt.DefaultView[i]["MYear"].ToString()));
                        }
                        else if (ti == 3)
                        {
                            dtOne = IndexBO.IndexCacheSql.pr_IndexMonth_SelectByIDAndTimeForQuy(int.Parse(dt.DefaultView[i]["Category_ID"].ToString()), 7, 9, int.Parse(dt.DefaultView[i]["MYear"].ToString()));
                        }
                    }
                    if (dtOne.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtOne.Rows)
                        {
                            unind = unind + double.Parse(row["MIndex"].ToString());
                        }
                    }
                    if (unind != 0)
                    {
                        ind = (bnind / unind - 1) * 100;
                    }
                    chd += String.Format("{0:f}", ind) + ",";

                    if ((i + 1) % countF == 0)
                    {
                        chd = chd.TrimEnd(',') + "|";
                    }
                }

                DataTable dtDistinct = dt.DefaultView.ToTable(true, new string[] { "CCode", "CName" });

                for (int j = 0; j < dtDistinct.Rows.Count; j++)
                {
                    chdl += dtDistinct.Rows[j]["CName"].ToString().Replace(' ', '+') + "|";
                    chco += colors[j] + ",";
                    chm += "|N,FF0000," + j + ",-1,10";
                }

                string[] rangeTemp = chd.TrimEnd('|').Replace('|', ',').Split(',');
                bubbleSort(rangeTemp, rangeTemp.Length);
                string rangeTemp1 = "";
                string rangeTemp2 = "";
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= -6)
                {
                    rangeTemp1 = "-4";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= -4)
                {
                    rangeTemp1 = "-2";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= -2)
                {
                    rangeTemp1 = "0";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 0)
                {
                    rangeTemp1 = "2";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 2)
                {
                    rangeTemp1 = "4";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 4)
                {
                    rangeTemp1 = "6";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 6)
                {
                    rangeTemp1 = "8";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 8)
                {
                    rangeTemp1 = "10";
                }

                if (double.Parse(rangeTemp[0]) <= 10)
                {
                    rangeTemp2 = "8";
                }
                if (double.Parse(rangeTemp[0]) <= 8)
                {
                    rangeTemp2 = "6";
                }
                if (double.Parse(rangeTemp[0]) <= 6)
                {
                    rangeTemp2 = "4";
                }
                if (double.Parse(rangeTemp[0]) <= 4)
                {
                    rangeTemp2 = "2";
                }
                if (double.Parse(rangeTemp[0]) <= 2)
                {
                    rangeTemp2 = "0";
                }
                if (double.Parse(rangeTemp[0]) <= 0)
                {
                    rangeTemp2 = "-2";
                }
                if (double.Parse(rangeTemp[0]) <= -2)
                {
                    rangeTemp2 = "-4";
                }
                if (double.Parse(rangeTemp[0]) <= -4)
                {
                    rangeTemp2 = "-6";
                }

                range = rangeTemp2 + "," + rangeTemp1;
                chds = rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1;

                srcImg = "http://chart.apis.google.com/chart";
                srcImg += "?chf=c,s,FFFFFF";
                srcImg += "&chxl=0:|" + chxl.TrimEnd('|');// +"1:|-2%|0%|2%|4%|6%";
                srcImg += "&chxp=" + chxp;// +"|1,-2,0,2,4,6";
                srcImg += "&chxr=0,0," + countF + "|1," + range;
                srcImg += "&chxt=x,y";
                srcImg += "&chs=600x300";
                srcImg += "&cht=lc";
                srcImg += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chds=" + chds;
                srcImg += "&chd=t:" + chd.TrimEnd('|');
                srcImg += "&chdl=" + chdl.TrimEnd('|');
                srcImg += "&chdlp=b";
                srcImg += "&chls=2|2|2|2";
                srcImg += "&chma=40,20,20,30";
                srcImg += "&chg=0,12.5,1,1";
                srcImg += "&chxs=1,676767,11.5,0.5,l,676767";
                if (CheckBox2.Checked)
                {
                    srcImg += "&chm=s,4f81be,0,-1,5|s,c1504d,1,-1,5|s,9cbc59,2,-1,5|s,8064a3,3,-1,5|s,3c8da3,4,-1,5|s,cc7b38,5,-1,5|s,4f81bd,6,-1,5|s,c0504d,7,-1,5|s,9bbb59,8,-1,5|s,8064a2,9,-1,5|s,4bacc6,10,-1,5|s,f79646,11,-1,5|s,aabad7,12,-1,5|s,d9aaa9,13,-1,5|s,c6d6ac,14,-1,5" + chm;
                }
                else
                {
                    srcImg += "&chm=s,4f81be,0,-1,5|s,c1504d,1,-1,5|s,9cbc59,2,-1,5|s,8064a3,3,-1,5|s,3c8da3,4,-1,5|s,cc7b38,5,-1,5|s,4f81bd,6,-1,5|s,c0504d,7,-1,5|s,9bbb59,8,-1,5|s,8064a2,9,-1,5|s,4bacc6,10,-1,5|s,f79646,11,-1,5|s,aabad7,12,-1,5|s,d9aaa9,13,-1,5|s,c6d6ac,14,-1,5";
                }
                srcImg += "&chtt=Chỉ+số+giá+tiêu+dùng(CPI)";
                Common.GetImageFromRedis(keyr, srcImg);

                phongto = "http://chart.apis.google.com/chart";
                phongto += "?chf=c,s,FFFFFF";
                phongto += "&chxl=0:|" + chxl.TrimEnd('|');// +"1:|-2%|0%|2%|4%|6%";
                phongto += "&chxp=" + chxp;// +"|1,-2,0,2,4,6";
                phongto += "&chxr=0,0," + countF + "|1," + range;
                phongto += "&chxt=x,y";
                phongto += "&chs=890x320";
                phongto += "&cht=lc";
                phongto += "&chco=" + chco.TrimEnd(',');
                phongto += "&chds=" + chds;
                phongto += "&chd=t:" + chd.TrimEnd('|');
                phongto += "&chdl=" + chdl.TrimEnd('|');
                phongto += "&chdlp=b";
                phongto += "&chls=2|2|2|2";
                phongto += "&chma=40,20,20,30";
                phongto += "&chg=0,12.5,1,1";
                phongto += "&chxs=1,676767,11.5,0.5,l,676767";
                if (CheckBox2.Checked)
                {
                    phongto += "&chm=s,4f81be,0,-1,5|s,c1504d,1,-1,5|s,9cbc59,2,-1,5|s,8064a3,3,-1,5|s,3c8da3,4,-1,5|s,cc7b38,5,-1,5|s,4f81bd,6,-1,5|s,c0504d,7,-1,5|s,9bbb59,8,-1,5|s,8064a2,9,-1,5|s,4bacc6,10,-1,5|s,f79646,11,-1,5|s,aabad7,12,-1,5|s,d9aaa9,13,-1,5|s,c6d6ac,14,-1,5" + chm;
                }
                else
                {
                    phongto += "&chm=s,4f81be,0,-1,5|s,c1504d,1,-1,5|s,9cbc59,2,-1,5|s,8064a3,3,-1,5|s,3c8da3,4,-1,5|s,cc7b38,5,-1,5|s,4f81bd,6,-1,5|s,c0504d,7,-1,5|s,9bbb59,8,-1,5|s,8064a2,9,-1,5|s,4bacc6,10,-1,5|s,f79646,11,-1,5|s,aabad7,12,-1,5|s,d9aaa9,13,-1,5|s,c6d6ac,14,-1,5";
                }
                phongto += "&chtt=Chỉ+số+giá+tiêu+dùng(CPI)";
                Common.GetImageFromRedis(keyrPhongTo, phongto);
            }
            ltrChart3.Text = "<img style='display:none' id='imgQuy' alt=\"\" src=" + srcImg + " />";
            hdfChart4.Value = phongto;
        }

        private void GenChart4(int parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
        {
            string srcImg = "";
            string phongto = "";
            string ft = "";
            for (int fil = 0; fil < chkChitieu2.Items.Count; fil++)
            {
                if (chkChitieu2.Items[fil].Selected)
                {
                    ft += "." + chkChitieu2.Items[fil].Value;
                }
            }
            for (int li = 0; li < chkChitieuchitiet3.Items.Count; li++)
            {
                if (chkChitieuchitiet3.Items[li].Selected)
                {
                    ft += "." + chkChitieuchitiet3.Items[li].Value;
                }
            }
            ft = ft.TrimStart('.');
            string keyr = String.Format("Cafef.Chart.BinaryData.CPICu.Nam.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, CheckBox2.Checked);
            string keyrPhongTo = String.Format("Cafef.Chart.BinaryData.CPICu.Thang.Phongto.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, CheckBox2.Checked);
            srcImg = Common.GetImageFromRedis(keyr, srcImg);
            phongto = Common.GetImageFromRedis(keyrPhongTo, phongto);
            if (srcImg.Equals("") || phongto.Equals(""))
            {
                string chxl = "";
                string chd = "";
                string chdl = "";
                string chco = "";
                string chxp = "1,";
                string chm = "";
                string range = "";
                string chds = "";
                DataTable dtGet = IndexBO.IndexCacheSql.pr_Chart_Month(parentid, time1, time2, time3, time4, year1, year2, year3);
                DataTable dt = Common.GenYearData(dtGet);
                DataTable dtTemp = dt;
                if (dt.Rows.Count > 0)
                {
                    dtTemp.DefaultView.RowFilter = "CCode = '" + dt.Rows[0]["CCode"].ToString() + "'";
                    for (int i = 0; i < dtTemp.DefaultView.Count; i++)
                    {
                        int t = int.Parse(dtTemp.DefaultView[i]["MYear"].ToString());
                        //int y = int.Parse(dtTemp.DefaultView[i]["QYear"].ToString());
                        chxl += t.ToString() + "|";
                        int itemp = i + 1;
                        chxp += itemp.ToString() + ",";
                    }
                }

                string[] chxlArr = chxl.TrimEnd('|').Split('|');
                int countF = chxlArr.Length;
                string filter = "";
                for (int fil = 0; fil < chkChitieu2.Items.Count; fil++)
                {
                    if (chkChitieu2.Items[fil].Selected)
                    {
                        filter += "OR CCode=" + chkChitieu2.Items[fil].Value;
                    }
                }
                for (int li = 0; li < chkChitieuchitiet3.Items.Count; li++)
                {
                    if (chkChitieuchitiet3.Items[li].Selected)
                    {
                        filter += "OR CCode=" + chkChitieuchitiet3.Items[li].Value;
                    }
                }
                char[] c = { 'O', 'R' };
                dt.DefaultView.RowFilter = filter.TrimStart(c);
                string[] colors = { "4f81be", "c1504d", "9cbc59", "8064a3", "3c8da3", "cc7b38", "4f81bd", "c0504d", "9bbb59", "8064a2", "4bacc6", "f79646", "aabad7", "d9aaa9", "c6d6ac" };

                for (int i = 0; i < dt.DefaultView.Count; i++)
                {
                    double ind = 0;
                    double bnind = (double.Parse(dt.DefaultView[i]["MIndex"].ToString()));
                    double unind = 0;
                    int ti = (int.Parse(dt.DefaultView[i]["MYear"].ToString())) - 1;
                    DataTable dtOne = new DataTable();
                    dtOne = IndexBO.IndexCacheSql.pr_IndexMonth_SelectByIDAndTimeForQuy(int.Parse(dt.DefaultView[i]["Category_ID"].ToString()), 1, 12, ti);
                    if (dtOne.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtOne.Rows)
                        {
                            unind = unind + double.Parse(row["MIndex"].ToString());
                        }
                    }
                    if (unind != 0)
                    {
                        ind = (bnind / unind - 1) * 100;
                    }
                    chd += String.Format("{0:f}", ind) + ",";

                    if ((i + 1) % countF == 0)
                    {
                        chd = chd.TrimEnd(',') + "|";
                    }
                }

                DataTable dtDistinct = dt.DefaultView.ToTable(true, new string[] { "CCode", "CName" });

                for (int j = 0; j < dtDistinct.Rows.Count; j++)
                {
                    chdl += dtDistinct.Rows[j]["CName"].ToString().Replace(' ', '+') + "|";
                    chco += colors[j] + ",";
                    chm += "|N,FF0000," + j + ",-1,10";
                }

                string[] rangeTemp = chd.TrimEnd('|').Replace('|', ',').Split(',');
                bubbleSort(rangeTemp, rangeTemp.Length);
                string rangeTemp1 = "";
                string rangeTemp2 = "";

                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 0)
                {
                    rangeTemp1 = "5";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 5)
                {
                    rangeTemp1 = "10";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 10)
                {
                    rangeTemp1 = "15";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 15)
                {
                    rangeTemp1 = "20";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 20)
                {
                    rangeTemp1 = "25";
                }

                if (double.Parse(rangeTemp[0]) <= 25)
                {
                    rangeTemp2 = "20";
                }
                if (double.Parse(rangeTemp[0]) <= 20)
                {
                    rangeTemp2 = "15";
                }
                if (double.Parse(rangeTemp[0]) <= 15)
                {
                    rangeTemp2 = "10";
                }
                if (double.Parse(rangeTemp[0]) <= 10)
                {
                    rangeTemp2 = "5";
                }
                if (double.Parse(rangeTemp[0]) <= 5)
                {
                    rangeTemp2 = "0";
                }

                range = rangeTemp2 + "," + rangeTemp1;
                chds = rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1;

                srcImg = "http://chart.apis.google.com/chart";
                srcImg += "?chf=c,s,FFFFFF";
                srcImg += "&chxl=0:|" + chxl.TrimEnd('|');// +"1:|-2%|0%|2%|4%|6%";
                srcImg += "&chxp=" + chxp;// +"|1,-2,0,2,4,6";
                srcImg += "&chxr=0,0," + countF + "|1," + range;
                srcImg += "&chxt=x,y";
                srcImg += "&chs=600x300";
                srcImg += "&cht=lc";
                srcImg += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chds=" + chds;
                srcImg += "&chd=t:" + chd.TrimEnd('|');
                srcImg += "&chdl=" + chdl.TrimEnd('|');
                srcImg += "&chdlp=b";
                srcImg += "&chls=2|2|2|2";
                srcImg += "&chma=40,20,20,30";
                srcImg += "&chg=0,12.5,1,1";
                srcImg += "&chxs=1,676767,11.5,0.5,l,676767";
                if (CheckBox2.Checked)
                {
                    srcImg += "&chm=s,4f81be,0,-1,5|s,c1504d,1,-1,5|s,9cbc59,2,-1,5|s,8064a3,3,-1,5|s,3c8da3,4,-1,5|s,cc7b38,5,-1,5|s,4f81bd,6,-1,5|s,c0504d,7,-1,5|s,9bbb59,8,-1,5|s,8064a2,9,-1,5|s,4bacc6,10,-1,5|s,f79646,11,-1,5|s,aabad7,12,-1,5|s,d9aaa9,13,-1,5|s,c6d6ac,14,-1,5" + chm;
                }
                else
                {
                    srcImg += "&chm=s,4f81be,0,-1,5|s,c1504d,1,-1,5|s,9cbc59,2,-1,5|s,8064a3,3,-1,5|s,3c8da3,4,-1,5|s,cc7b38,5,-1,5|s,4f81bd,6,-1,5|s,c0504d,7,-1,5|s,9bbb59,8,-1,5|s,8064a2,9,-1,5|s,4bacc6,10,-1,5|s,f79646,11,-1,5|s,aabad7,12,-1,5|s,d9aaa9,13,-1,5|s,c6d6ac,14,-1,5";
                }
                srcImg += "&chtt=Chỉ+số+giá+tiêu+dùng(CPI)";
                Common.GetImageFromRedis(keyr, srcImg);

                phongto = "http://chart.apis.google.com/chart";
                phongto += "?chf=c,s,FFFFFF";
                phongto += "&chxl=0:|" + chxl.TrimEnd('|');// +"1:|-2%|0%|2%|4%|6%";
                phongto += "&chxp=" + chxp;// +"|1,-2,0,2,4,6";
                phongto += "&chxr=0,0," + countF + "|1," + range;
                phongto += "&chxt=x,y";
                phongto += "&chs=890x320";
                phongto += "&cht=lc";
                phongto += "&chco=" + chco.TrimEnd(',');
                phongto += "&chds=" + chds;
                phongto += "&chd=t:" + chd.TrimEnd('|');
                phongto += "&chdl=" + chdl.TrimEnd('|');
                phongto += "&chdlp=b";
                phongto += "&chls=2|2|2|2";
                phongto += "&chma=40,20,20,30";
                phongto += "&chg=0,12.5,1,1";
                phongto += "&chxs=1,676767,11.5,0.5,l,676767";
                if (CheckBox2.Checked)
                {
                    phongto += "&chm=s,4f81be,0,-1,5|s,c1504d,1,-1,5|s,9cbc59,2,-1,5|s,8064a3,3,-1,5|s,3c8da3,4,-1,5|s,cc7b38,5,-1,5|s,4f81bd,6,-1,5|s,c0504d,7,-1,5|s,9bbb59,8,-1,5|s,8064a2,9,-1,5|s,4bacc6,10,-1,5|s,f79646,11,-1,5|s,aabad7,12,-1,5|s,d9aaa9,13,-1,5|s,c6d6ac,14,-1,5" + chm;
                }
                else
                {
                    phongto += "&chm=s,4f81be,0,-1,5|s,c1504d,1,-1,5|s,9cbc59,2,-1,5|s,8064a3,3,-1,5|s,3c8da3,4,-1,5|s,cc7b38,5,-1,5|s,4f81bd,6,-1,5|s,c0504d,7,-1,5|s,9bbb59,8,-1,5|s,8064a2,9,-1,5|s,4bacc6,10,-1,5|s,f79646,11,-1,5|s,aabad7,12,-1,5|s,d9aaa9,13,-1,5|s,c6d6ac,14,-1,5";
                }
                phongto += "&chtt=Chỉ+số+giá+tiêu+dùng(CPI)";
                Common.GetImageFromRedis(keyrPhongTo, phongto);
            }
            ltrChart4.Text = "<img style='display:none' id='imgNam' alt=\"\" src=" + srcImg + " />";
            hdfChart5.Value = phongto;
        }

        protected void btnViewQuy_Click(object sender, EventArgs e)
        {
            int time1 = int.Parse(dlQuarter1.SelectedValue);
            int time2 = 0;
            int time3 = 0;
            int time4 = int.Parse(dlQuarter2.SelectedValue);
            int year1 = int.Parse(dlYearQuy1.SelectedValue);
            int year2 = 0;
            int year3 = int.Parse(dlYearQuy2.SelectedValue);
            if (year1 == year3)
            {
                time2 = time4;
                time3 = time1;
                year2 = year3;
            }
            else
            {
                int kc = year3 - year1 - 1;
                time2 = 4;
                time3 = 1;
                year2 = year1 + kc;
            }
            if (time1 == 2) time1 = 4;
            else if (time1 == 3) time1 = 7;
            else if (time1 == 4) time1 = 10;
            if (time2 == 1) time2 = 3;
            else if (time2 == 2) time2 = 6;
            else if (time2 == 3) time2 = 9;
            else if (time2 == 4) time2 = 12;
            if (time3 == 2) time3 = 4;
            else if (time3 == 3) time3 = 7;
            else if (time3 == 4) time3 = 10;
            if (time4 == 1) time4 = 3;
            else if (time4 == 2) time4 = 6;
            else if (time4 == 3) time4 = 9;
            else if (time4 == 4) time4 = 12;
            this.GenChart3(parentID, time1, time2, time3, time4, year1, year2, year3);
            hdfType.Value = "2";
        }

        protected void btnViewYoY_Click(object sender, EventArgs e)
        {
            int time1 = 1;
            int time2 = 12;
            int time3 = 1;
            int time4 = 12;
            int year1 = int.Parse(dlYear1YoY.SelectedValue);
            int year2 = 0;
            int year3 = int.Parse(dlYear2YoY.SelectedValue);
            if (year1 == year3)
            {
                year2 = year3;
            }
            else
            {
                int kc = year3 - year1 - 1;
                year2 = year1 + kc;
            }
            this.GenChart4(parentID, time1, time2, time3, time4, year1, year2, year3);
            hdfType.Value = "3";
        }

        private void bubbleSort(string[] unsortedArray, int length)
        {
            string temp = "";
            int counter, index;
            for (counter = 0; counter < length - 1; counter++)
            { //Loop once for each element in the array.
                for (index = 0; index < length - 1 - counter; index++)
                { //Once for each element, minus the counter.
                    if (double.Parse(unsortedArray[index]) > double.Parse(unsortedArray[index + 1]))
                    { //Test if need a swap or not.
                        temp = unsortedArray[index]; //These three lines just swap the two elements:
                        unsortedArray[index] = unsortedArray[index + 1];
                        unsortedArray[index + 1] = temp;
                    }
                }
            }
        }
    }
}