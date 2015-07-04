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
    public partial class XNKChart : System.Web.UI.UserControl
    {
        private int parentID = 14;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GenChart(parentID, 1, 5, 1, 5, 2011, 2011, 2011);
                this.GenChartMoM(parentID, 1, 5, 1, 5, 2011, 2011, 2011);
                this.GenChartMoMSoVoi(parentID, 1, 5, 1, 5, 2011, 2011, 2011);

                this.GenChartQuy(parentID, 4, 12, 1, 3, 2010, 2010, 2011);
                this.GenChartNam(parentID, 1, 12, 1, 12, 2005, 2009, 2010);

                this.GenChartMoMQuy(parentID, 4, 12, 1, 3, 2010, 2010, 2011);
                this.GenChartMoMNam(parentID, 1, 12, 1, 12, 2005, 2009, 2010);
                this.SetDefault();
            }
        }

        private void SetDefault()
        {
            Common.FillDataToDropDownList(dlYearQuy1, 1999, 2012);
            Common.FillDataToDropDownList(dlYearQuy2, 1999, 2012);
            Common.FillDataToDropDownList(dlYear1YoY, 1999, 2012);
            Common.FillDataToDropDownList(dlYear2YoY, 1999, 2012);
            Common.FillDataToDropDownList(dlYearQuy1TT, 1999, 2012);
            Common.FillDataToDropDownList(dlYearQuy2TT, 1999, 2012);
            Common.FillDataToDropDownList(dlYear1YoYTT, 1999, 2012);
            Common.FillDataToDropDownList(dlYear2YoYTT, 1999, 2012);
            dlMonth1.SelectedValue = "1";
            dlMonth2.SelectedValue = "5";
            dlYear1.SelectedValue = "2011";
            dlYear2.SelectedValue = "2011";
            dlMonth1MoM.SelectedValue = "1";
            dlMonth2MoM.SelectedValue = "5";
            dlYear1MoM.SelectedValue = "2011";
            dlYear2MoM.SelectedValue = "2011";
            dlMonth1MoMSoVoi.SelectedValue = "1";
            dlMonth2MoMSoVoi.SelectedValue = "5";
            dlYear1MoMSoVoi.SelectedValue = "2011";
            dlYear2MoMSoVoi.SelectedValue = "2011";

            dlQuarter1.SelectedValue = "2";
            dlQuarter2.SelectedValue = "1";
            dlYearQuy1.SelectedValue = "2010";
            dlYearQuy2.SelectedValue = "2011";
            dlYear1YoY.SelectedValue = "2005";
            dlYear2YoY.SelectedValue = "2010";

            dlQuarter1TT.SelectedValue = "2";
            dlQuarter2TT.SelectedValue = "1";
            dlYearQuy1TT.SelectedValue = "2010";
            dlYearQuy2TT.SelectedValue = "2011";
            dlYear1YoYTT.SelectedValue = "2005";
            dlYear2YoYTT.SelectedValue = "2010";
            hdfTypeTT.Value = "1";
            hdfTypeGT.Value = "1";
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            int time1 = int.Parse(dlMonth1.SelectedValue);
            int time2 = 0;
            int time3 = 0;
            int time4 = int.Parse(dlMonth2.SelectedValue);
            int year1 = int.Parse(dlYear1.SelectedValue);
            int year2 = 0;
            int year3 = int.Parse(dlYear2.SelectedValue);
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
            this.GenChart(parentID, time1, time2, time3, time4, year1, year2, year3);
            hdfTypeGT.Value = "1";
            ltrScript.Text = "<script type=\"text/javascript\">window.location.href = '/dulieuvimo/xuat-nhap-khau.chn#giatrithuc';</script>";
        }

        private void GenChart(int parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
        {
            string srcImg = "";
            string phongto = "";
            string ft = "";
            for (int fil = 0; fil < chkChitieu.Items.Count; fil++)
            {
                if (chkChitieu.Items[fil].Selected)
                {
                    ft += "." + chkChitieu.Items[fil].Value;
                }
            }
            ft = ft.TrimStart('.');
            string keyr = String.Format("Cafef.Chart.BinaryData.XNK.Thang.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, chkIsLabel.Checked);
            string keyrPhongTo = String.Format("Cafef.Chart.BinaryData.XNK.Thang.Phongto.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, chkIsLabel.Checked);
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
                DataTable dtXNK = IndexBO.IndexCacheSql.pr_Chart_Month(parentid, time1, time2, time3, time4, year1, year2, year3);
                dtXNK.DefaultView.Sort = "CName DESC, MYear ASC, MTime ASC";
                DataTable dt = dtXNK.DefaultView.Table;
                DataTable dtTemp = dt;
                //foreach(DataRow row in dt.Select().o)
                //{
                //    dtTemp.ImportRow(row);
                //}
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
                        int itemp = i + 1;
                        chxp += itemp.ToString() + ",";
                    }
                }

                string[] chxlArr = chxl.TrimEnd('|').Split('|');
                int countF = chxlArr.Length;
                string filter = "";
                for (int fil = 0; fil < chkChitieu.Items.Count; fil++)
                {
                    if (chkChitieu.Items[fil].Selected)
                    {
                        filter += "OR CCode=" + chkChitieu.Items[fil].Value;
                    }
                }

                char[] c = { 'O', 'R' };
                dt.DefaultView.RowFilter = filter.TrimStart(c);
                string[] colors = { "4f81be", "c1504d", "9cbc59" };
                for (int i = 0; i < dt.DefaultView.Count; i++)
                {
                    double ind = double.Parse(dt.DefaultView[i]["MIndex"].ToString());
                    chd += ind.ToString() + ",";

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
                    chm += "N,FF0000," + j + ",-1,10|";
                }

                srcImg = "http://chart.apis.google.com/chart";
                srcImg += "?chf=c,s,FFFFFF";
                srcImg += "&chxl=1:|" + chxl;// +"2:|-3%|-2%|-1%|0%|1%|2%|3%|4%|5%";
                srcImg += "&chxp=" + chxp.TrimEnd(',');// +"|2,0,1,2,3,4,5,6,7,8";
                srcImg += "&chxr=0,-4000,12000|1,1," + countF;// +"|2,0,8";
                srcImg += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
                srcImg += "&chxt=y,x";
                srcImg += "&chbh=a,1,10";
                srcImg += "&chs=600x300";
                srcImg += "&cht=bvg";
                srcImg += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chds=-4000,12000,-4000,12000,-4000,12000";
                srcImg += "&chd=t2:" + chd.TrimEnd('|');
                srcImg += "&chdl=" + chdl.TrimEnd('|');
                srcImg += "&chdlp=b";
                srcImg += "&chm=D,9cbc59,2,-1,2|s,9cbc59,2,-1,6";
                srcImg += "&chg=0,12.45,1,1";
                if (chkIsLabel.Checked)
                {
                    srcImg += "&chm=" + chm.TrimEnd('|');
                }
                srcImg += "&chtt=Biểu+đồ+xuất+nhập+khẩu";
                Common.GetImageFromRedis(keyr, srcImg);

                phongto = "http://chart.apis.google.com/chart";
                phongto += "?chf=c,s,FFFFFF";
                phongto += "&chxl=1:|" + chxl;// +"2:|-3%|-2%|-1%|0%|1%|2%|3%|4%|5%";
                phongto += "&chxp=" + chxp.TrimEnd(',');// +"|2,0,1,2,3,4,5,6,7,8";
                phongto += "&chxr=0,-4000,12000|1,1," + countF;// +"|2,0,8";
                phongto += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
                phongto += "&chxt=y,x";
                phongto += "&chbh=a,1,10";
                phongto += "&chs=890x320";
                phongto += "&cht=bvg";
                phongto += "&chco=" + chco.TrimEnd(',');
                phongto += "&chds=-4000,12000,-4000,12000,-4000,12000";
                phongto += "&chd=t2:" + chd.TrimEnd('|');
                phongto += "&chdl=" + chdl.TrimEnd('|');
                phongto += "&chdlp=b";
                phongto += "&chm=D,9cbc59,2,-1,2|s,9cbc59,2,-1,6";
                phongto += "&chg=0,12.45,1,1";
                if (chkIsLabel.Checked)
                {
                    phongto += "&chm=" + chm.TrimEnd('|');
                }
                phongto += "&chtt=Biểu+đồ+xuất+nhập+khẩu";
                Common.GetImageFromRedis(keyrPhongTo, phongto);
            }
            ltrChart.Text = "<img id='imgThangGT' alt=\"\" src=" + srcImg + " />";
            hdfChart.Value = phongto;
        }

        private void GenChartQuy(int parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
        {
            string srcImg = "";
            string phongto = "";
            string ft = "";
            for (int fil = 0; fil < chkChitieu.Items.Count; fil++)
            {
                if (chkChitieu.Items[fil].Selected)
                {
                    ft += "." + chkChitieu.Items[fil].Value;
                }
            }
            ft = ft.TrimStart('.');
            string keyr = String.Format("Cafef.Chart.BinaryData.XNK.Quy.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, chkIsLabel.Checked);
            string keyrPhongTo = String.Format("Cafef.Chart.BinaryData.XNK.Quy.Phongto.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, chkIsLabel.Checked);
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
                DataTable dtGet = IndexBO.IndexCacheSql.pr_Chart_Month(parentid, time1, time2, time3, time4, year1, year2, year3);
                DataTable dtXNK = Common.GenQuarterData(dtGet);
                dtXNK.DefaultView.Sort = "CName DESC, MYear ASC, MTime ASC";
                DataTable dt = dtXNK.DefaultView.Table;
                DataTable dtTemp = dt;
                if (dt.Rows.Count > 0)
                {
                    dtTemp.DefaultView.RowFilter = "CCode = '" + dt.Rows[0]["CCode"].ToString() + "'";
                    for (int i = 0; i < dtTemp.DefaultView.Count; i++)
                    {
                        int t = int.Parse(dtTemp.DefaultView[i]["MTime"].ToString());
                        int y = int.Parse(dtTemp.DefaultView[i]["MYear"].ToString());
                        chxl += "q" + t.ToString() + "/" + y.ToString().Remove(0, 2) + "|";
                        int itemp = i + 1;
                        chxp += itemp.ToString() + ",";
                    }
                }

                string[] chxlArr = chxl.TrimEnd('|').Split('|');
                int countF = chxlArr.Length;
                string filter = "";
                for (int fil = 0; fil < chkChitieu.Items.Count; fil++)
                {
                    if (chkChitieu.Items[fil].Selected)
                    {
                        filter += "OR CCode=" + chkChitieu.Items[fil].Value;
                    }
                }

                char[] c = { 'O', 'R' };
                dt.DefaultView.RowFilter = filter.TrimStart(c);
                string[] colors = { "4f81be", "c1504d", "9cbc59" };
                for (int i = 0; i < dt.DefaultView.Count; i++)
                {
                    double ind = double.Parse(dt.DefaultView[i]["MIndex"].ToString());
                    chd += ind.ToString() + ",";

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
                    chm += "N,FF0000," + j + ",-1,10|";
                }

                srcImg = "http://chart.apis.google.com/chart";
                srcImg += "?chf=c,s,FFFFFF";
                srcImg += "&chxl=1:|" + chxl;// +"2:|-3%|-2%|-1%|0%|1%|2%|3%|4%|5%";
                srcImg += "&chxp=" + chxp.TrimEnd(',');// +"|2,0,1,2,3,4,5,6,7,8";
                srcImg += "&chxr=0,-4000,24000|1,1," + countF;// +"|2,0,8";
                srcImg += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
                srcImg += "&chxt=y,x";
                srcImg += "&chbh=a,1,10";
                srcImg += "&chs=600x300";
                srcImg += "&cht=bvg";
                srcImg += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chds=-4000,24000,-4000,24000,-4000,24000";
                srcImg += "&chd=t2:" + chd.TrimEnd('|');
                srcImg += "&chdl=" + chdl.TrimEnd('|');
                srcImg += "&chdlp=b";
                srcImg += "&chm=D,9cbc59,2,-1,2|s,9cbc59,2,-1,6";
                srcImg += "&chg=0,12.45,1,1";
                if (chkIsLabel.Checked)
                {
                    srcImg += "&chm=" + chm.TrimEnd('|');
                }
                srcImg += "&chtt=Biểu+đồ+xuất+nhập+khẩu";
                Common.GetImageFromRedis(keyr, srcImg);

                phongto = "http://chart.apis.google.com/chart";
                phongto += "?chf=c,s,FFFFFF";
                phongto += "&chxl=1:|" + chxl;// +"2:|-3%|-2%|-1%|0%|1%|2%|3%|4%|5%";
                phongto += "&chxp=" + chxp.TrimEnd(',');// +"|2,0,1,2,3,4,5,6,7,8";
                phongto += "&chxr=0,-4000,24000|1,1," + countF;// +"|2,0,8";
                phongto += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
                phongto += "&chxt=y,x";
                phongto += "&chbh=a,1,10";
                phongto += "&chs=890x320";
                phongto += "&cht=bvg";
                phongto += "&chco=" + chco.TrimEnd(',');
                phongto += "&chds=-4000,24000,-4000,24000,-4000,24000";
                phongto += "&chd=t2:" + chd.TrimEnd('|');
                phongto += "&chdl=" + chdl.TrimEnd('|');
                phongto += "&chdlp=b";
                phongto += "&chm=D,9cbc59,2,-1,2|s,9cbc59,2,-1,6";
                phongto += "&chg=0,12.45,1,1";
                if (chkIsLabel.Checked)
                {
                    phongto += "&chm=" + chm.TrimEnd('|');
                }
                phongto += "&chtt=Biểu+đồ+xuất+nhập+khẩu";
                Common.GetImageFromRedis(keyrPhongTo, phongto);
            }
            ltrChartGT2.Text = "<img id='imgQuyGT' alt=\"\" src=" + srcImg + " />";
            hdfChartGT2.Value = phongto;
        }

        private void GenChartNam(int parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
        {
            string srcImg = "";
            string phongto = "";
            string ft = "";
            for (int fil = 0; fil < chkChitieu.Items.Count; fil++)
            {
                if (chkChitieu.Items[fil].Selected)
                {
                    ft += "." + chkChitieu.Items[fil].Value;
                }
            }
            ft = ft.TrimStart('.');
            string keyr = String.Format("Cafef.Chart.BinaryData.XNK.Nam.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, chkIsLabel.Checked);
            string keyrPhongTo = String.Format("Cafef.Chart.BinaryData.XNK.Nam.Phongto.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, chkIsLabel.Checked);
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
                DataTable dtGet = IndexBO.IndexCacheSql.pr_Chart_Month(parentid, time1, time2, time3, time4, year1, year2, year3);
                DataTable dtXNK = Common.GenYearData(dtGet);
                dtXNK.DefaultView.Sort = "CName DESC, MYear ASC";
                DataTable dt = dtXNK.DefaultView.Table;
                DataTable dtTemp = dt;
                if (dt.Rows.Count > 0)
                {
                    dtTemp.DefaultView.RowFilter = "CCode = '" + dt.Rows[0]["CCode"].ToString() + "'";
                    for (int i = 0; i < dtTemp.DefaultView.Count; i++)
                    {
                        int t = int.Parse(dtTemp.DefaultView[i]["MYear"].ToString());
                        chxl += t.ToString() + "|";
                        int itemp = i + 1;
                        chxp += itemp.ToString() + ",";
                    }
                }

                string[] chxlArr = chxl.TrimEnd('|').Split('|');
                int countF = chxlArr.Length;
                string filter = "";
                for (int fil = 0; fil < chkChitieu.Items.Count; fil++)
                {
                    if (chkChitieu.Items[fil].Selected)
                    {
                        filter += "OR CCode=" + chkChitieu.Items[fil].Value;
                    }
                }

                char[] c = { 'O', 'R' };
                dt.DefaultView.RowFilter = filter.TrimStart(c);
                string[] colors = { "4f81be", "c1504d", "9cbc59" };
                for (int i = 0; i < dt.DefaultView.Count; i++)
                {
                    double ind = double.Parse(dt.DefaultView[i]["MIndex"].ToString());
                    chd += ind.ToString() + ",";

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
                    chm += "N,FF0000," + j + ",-1,10|";
                }

                srcImg = "http://chart.apis.google.com/chart";
                srcImg += "?chf=c,s,FFFFFF";
                srcImg += "&chxl=1:|" + chxl;// +"2:|-3%|-2%|-1%|0%|1%|2%|3%|4%|5%";
                srcImg += "&chxp=" + chxp.TrimEnd(',');// +"|2,0,1,2,3,4,5,6,7,8";
                srcImg += "&chxr=0,-20000,90000|1,1," + countF;// +"|2,0,8";
                srcImg += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
                srcImg += "&chxt=y,x";
                srcImg += "&chbh=a,1,10";
                srcImg += "&chs=600x300";
                srcImg += "&cht=bvg";
                srcImg += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chds=-20000,90000,-20000,90000,-20000,90000";
                srcImg += "&chd=t2:" + chd.TrimEnd('|');
                srcImg += "&chdl=" + chdl.TrimEnd('|');
                srcImg += "&chdlp=b";
                srcImg += "&chm=D,9cbc59,2,-1,2|s,9cbc59,2,-1,6";
                srcImg += "&chg=0,12.45,1,1";
                if (chkIsLabel.Checked)
                {
                    srcImg += "&chm=" + chm.TrimEnd('|');
                }
                srcImg += "&chtt=Biểu+đồ+xuất+nhập+khẩu";
                Common.GetImageFromRedis(keyr, srcImg);

                phongto = "http://chart.apis.google.com/chart";
                phongto += "?chf=c,s,FFFFFF";
                phongto += "&chxl=1:|" + chxl;// +"2:|-3%|-2%|-1%|0%|1%|2%|3%|4%|5%";
                phongto += "&chxp=" + chxp.TrimEnd(',');// +"|2,0,1,2,3,4,5,6,7,8";
                phongto += "&chxr=0,-20000,90000|1,1," + countF;// +"|2,0,8";
                phongto += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
                phongto += "&chxt=y,x";
                phongto += "&chbh=a,1,10";
                phongto += "&chs=890x320";
                phongto += "&cht=bvg";
                phongto += "&chco=" + chco.TrimEnd(',');
                phongto += "&chds=-20000,90000,-20000,90000,-20000,90000";
                phongto += "&chd=t2:" + chd.TrimEnd('|');
                phongto += "&chdl=" + chdl.TrimEnd('|');
                phongto += "&chdlp=b";
                phongto += "&chm=D,9cbc59,2,-1,2|s,9cbc59,2,-1,6";
                phongto += "&chg=0,12.45,1,1";
                if (chkIsLabel.Checked)
                {
                    phongto += "&chm=" + chm.TrimEnd('|');
                }
                phongto += "&chtt=Biểu+đồ+xuất+nhập+khẩu";
                Common.GetImageFromRedis(keyrPhongTo, phongto);
            }
            ltrChartGT3.Text = "<img id='imgNamGT' alt=\"\" src=" + srcImg + " />";
            hdfChartGT3.Value = phongto;
        }

        protected void btnViewMoM_Click(object sender, EventArgs e)
        {
            int time1 = int.Parse(dlMonth1MoM.SelectedValue);
            int time2 = 0;
            int time3 = 0;
            int time4 = int.Parse(dlMonth2MoM.SelectedValue);
            int year1 = int.Parse(dlYear1MoM.SelectedValue);
            int year2 = 0;
            int year3 = int.Parse(dlYear2MoM.SelectedValue);
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
            this.GenChartMoM(parentID, time1, time2, time3, time4, year1, year2, year3);
            //Response.Redirect("/dulieuvimo/san-luong-cong-nghiep.chn#phantram");
            ltrScript.Text = "<script type=\"text/javascript\">window.location.href = '/dulieuvimo/xuat-nhap-khau.chn#phantram';</script>";
        }

        private void GenChartMoM(int parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
        {
            string srcImg = "";
            string phongto = "";
            string ft = "";
            for (int fil = 0; fil < chkChitieuMoM.Items.Count; fil++)
            {
                if (chkChitieuMoM.Items[fil].Selected)
                {
                    ft += "." + chkChitieuMoM.Items[fil].Value;
                }
            }
            ft = ft.TrimStart('.');
            string keyr = String.Format("Cafef.Chart.BinaryData.XNKMoM.Thang.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, chkIsLabelMoM.Checked);
            string keyrPhongTo = String.Format("Cafef.Chart.BinaryData.XNKMoM.Thang.Phongto.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, chkIsLabelMoM.Checked);
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
                        int itemp = i + 1;
                        chxp += itemp.ToString() + ",";
                    }
                }

                string[] chxlArr = chxl.TrimEnd('|').Split('|');
                int countF = chxlArr.Length;
                string filter = "";
                for (int fil = 0; fil < chkChitieuMoM.Items.Count; fil++)
                {
                    if (chkChitieuMoM.Items[fil].Selected)
                    {
                        filter += "OR CCode=" + chkChitieuMoM.Items[fil].Value;
                    }
                }
                char[] c = { 'O', 'R' };
                dt.DefaultView.RowFilter = filter.TrimStart(c);
                string[] colors = { "4f81be", "c1504d", "9cbc59", "8064a3" };

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
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= -60)
                {
                    rangeTemp1 = "-40";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= -40)
                {
                    rangeTemp1 = "-20";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= -20)
                {
                    rangeTemp1 = "0";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 0)
                {
                    rangeTemp1 = "20";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 20)
                {
                    rangeTemp1 = "40";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 40)
                {
                    rangeTemp1 = "60";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 60)
                {
                    rangeTemp1 = "80";
                }
                if (double.Parse(rangeTemp[0]) <= 80)
                {
                    rangeTemp2 = "60";
                }
                if (double.Parse(rangeTemp[0]) <= 60)
                {
                    rangeTemp2 = "40";
                }
                if (double.Parse(rangeTemp[0]) <= 40)
                {
                    rangeTemp2 = "20";
                }
                if (double.Parse(rangeTemp[0]) <= 20)
                {
                    rangeTemp2 = "0";
                }
                if (double.Parse(rangeTemp[0]) <= 0)
                {
                    rangeTemp2 = "-20";
                }
                if (double.Parse(rangeTemp[0]) <= -20)
                {
                    rangeTemp2 = "-40";
                }
                if (double.Parse(rangeTemp[0]) <= -40)
                {
                    rangeTemp2 = "-60";
                }

                range = rangeTemp2 + "," + rangeTemp1;
                chds = rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1;

                srcImg = "http://chart.apis.google.com/chart";
                srcImg += "?chf=c,s,FFFFFF";
                srcImg += "&chxl=0:|" + chxl.TrimEnd('|');// +"1:|-40%|-20%|0%|20%|40%";
                srcImg += "&chxp=" + chxp; ;// +"|1,-40,20,0,20,40";
                srcImg += "&chxr=0,0," + countF + "|1," + range;
                srcImg += "&chxt=x,y";
                srcImg += "&chs=600x300";
                srcImg += "&cht=lc";
                //srcImg += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chds=" + chds;
                srcImg += "&chd=t:" + chd.TrimEnd('|');
                srcImg += "&chdl=" + chdl.TrimEnd('|');
                srcImg += "&chdlp=b";
                srcImg += "&chls=2|2|2|2";
                srcImg += "&chma=40,20,20,30";
                srcImg += "&chg=0,25,1,1";
                srcImg += "&chxs=1,676767,11.5,0.5,l,676767";
                if (chkIsLabelMoM.Checked)
                {
                    srcImg += "&chm=s,4f81be,0,-1,5|s,c1504d,1,-1,5|s,9cbc59,2,-1,5|s,8064a3,3,-1,5" + chm;
                }
                else
                {
                    srcImg += "&chm=s,4f81be,0,-1,5|s,c1504d,1,-1,5|s,9cbc59,2,-1,5|s,8064a3,3,-1,5";
                }
                srcImg += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chtt=Biểu+đồ+xuất+nhập+khẩu";
                Common.GetImageFromRedis(keyr, srcImg);

                phongto = "http://chart.apis.google.com/chart";
                phongto += "?chf=c,s,FFFFFF";
                phongto += "&chxl=0:|" + chxl.TrimEnd('|');// +"1:|-40%|-20%|0%|20%|40%";
                phongto += "&chxp=" + chxp;// +"|1,-40,20,0,20,40";
                phongto += "&chxr=0,0," + countF + "|1," + range;
                phongto += "&chxt=x,y";
                phongto += "&chs=890x320";
                phongto += "&cht=lc";
                //phongto += "&chco=" + chco.TrimEnd(',');
                phongto += "&chds=" + chds;
                phongto += "&chd=t:" + chd.TrimEnd('|');
                phongto += "&chdl=" + chdl.TrimEnd('|');
                phongto += "&chdlp=b";
                phongto += "&chls=2|2|2|2";
                phongto += "&chma=40,20,20,30";
                phongto += "&chg=0,25,1,1";
                phongto += "&chxs=1,676767,11.5,0.5,l,676767";
                if (chkIsLabelMoM.Checked)
                {
                    phongto += "&chm=s,4f81be,0,-1,5|s,c1504d,1,-1,5|s,9cbc59,2,-1,5|s,8064a3,3,-1,5" + chm;
                }
                else
                {
                    phongto += "&chm=s,4f81be,0,-1,5|s,c1504d,1,-1,5|s,9cbc59,2,-1,5|s,8064a3,3,-1,5";
                }
                phongto += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chtt=Biểu+đồ+xuất+nhập+khẩu";
                Common.GetImageFromRedis(keyrPhongTo, phongto);
            }
            ltrChartMoM.Text = "<img id='imgThangTT' alt=\"\" src=" + srcImg + " />";
            hdfChartMoM.Value = phongto;
        }

        private void GenChartMoMQuy(int parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
        {
            string srcImg = "";
            string phongto = "";
            string ft = "";
            for (int fil = 0; fil < chkChitieuMoM.Items.Count; fil++)
            {
                if (chkChitieuMoM.Items[fil].Selected)
                {
                    ft += "." + chkChitieuMoM.Items[fil].Value;
                }
            }
            ft = ft.TrimStart('.');
            string keyr = String.Format("Cafef.Chart.BinaryData.XNKMoM.Quy.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, chkIsLabelMoM.Checked);
            string keyrPhongTo = String.Format("Cafef.Chart.BinaryData.XNKMoM.Quy.Phongto.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, chkIsLabelMoM.Checked);
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
                        chxl += "q" + t.ToString() + "/" + y.ToString().Remove(0, 2) + "|";
                        int itemp = i + 1;
                        chxp += itemp.ToString() + ",";
                    }
                }

                string[] chxlArr = chxl.TrimEnd('|').Split('|');
                int countF = chxlArr.Length;
                string filter = "";
                for (int fil = 0; fil < chkChitieuMoM.Items.Count; fil++)
                {
                    if (chkChitieuMoM.Items[fil].Selected)
                    {
                        filter += "OR CCode=" + chkChitieuMoM.Items[fil].Value;
                    }
                }
                char[] c = { 'O', 'R' };
                dt.DefaultView.RowFilter = filter.TrimStart(c);
                string[] colors = { "4f81be", "c1504d", "9cbc59", "8064a3" };

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
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= -60)
                {
                    rangeTemp1 = "-40";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= -40)
                {
                    rangeTemp1 = "-20";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= -20)
                {
                    rangeTemp1 = "0";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 0)
                {
                    rangeTemp1 = "20";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 20)
                {
                    rangeTemp1 = "40";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 40)
                {
                    rangeTemp1 = "60";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 60)
                {
                    rangeTemp1 = "80";
                }
                if (double.Parse(rangeTemp[0]) <= 80)
                {
                    rangeTemp2 = "60";
                }
                if (double.Parse(rangeTemp[0]) <= 60)
                {
                    rangeTemp2 = "40";
                }
                if (double.Parse(rangeTemp[0]) <= 40)
                {
                    rangeTemp2 = "20";
                }
                if (double.Parse(rangeTemp[0]) <= 20)
                {
                    rangeTemp2 = "0";
                }
                if (double.Parse(rangeTemp[0]) <= 0)
                {
                    rangeTemp2 = "-20";
                }
                if (double.Parse(rangeTemp[0]) <= -20)
                {
                    rangeTemp2 = "-40";
                }
                if (double.Parse(rangeTemp[0]) <= -40)
                {
                    rangeTemp2 = "-60";
                }

                range = rangeTemp2 + "," + rangeTemp1;
                chds = rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1;

                srcImg = "http://chart.apis.google.com/chart";
                srcImg += "?chf=c,s,FFFFFF";
                srcImg += "&chxl=0:|" + chxl.TrimEnd('|');// +"1:|-40%|-20%|0%|20%|40%";
                srcImg += "&chxp=" + chxp; ;// +"|1,-40,20,0,20,40";
                srcImg += "&chxr=0,0," + countF + "|1," + range;
                srcImg += "&chxt=x,y";
                srcImg += "&chs=600x300";
                srcImg += "&cht=lc";
                //srcImg += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chds=" + chds;
                srcImg += "&chd=t:" + chd.TrimEnd('|');
                srcImg += "&chdl=" + chdl.TrimEnd('|');
                srcImg += "&chdlp=b";
                srcImg += "&chls=2|2|2|2";
                srcImg += "&chma=40,20,20,30";
                srcImg += "&chg=0,25,1,1";
                srcImg += "&chxs=1,676767,11.5,0.5,l,676767";
                if (chkIsLabelMoM.Checked)
                {
                    srcImg += "&chm=s,4f81be,0,-1,5|s,c1504d,1,-1,5|s,9cbc59,2,-1,5|s,8064a3,3,-1,5" + chm;
                }
                else
                {
                    srcImg += "&chm=s,4f81be,0,-1,5|s,c1504d,1,-1,5|s,9cbc59,2,-1,5|s,8064a3,3,-1,5";
                }
                srcImg += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chtt=Biểu+đồ+xuất+nhập+khẩu";
                Common.GetImageFromRedis(keyr, srcImg);

                phongto = "http://chart.apis.google.com/chart";
                phongto += "?chf=c,s,FFFFFF";
                phongto += "&chxl=0:|" + chxl.TrimEnd('|');// +"1:|-40%|-20%|0%|20%|40%";
                phongto += "&chxp=" + chxp;// +"|1,-40,20,0,20,40";
                phongto += "&chxr=0,0," + countF + "|1," + range;
                phongto += "&chxt=x,y";
                phongto += "&chs=890x320";
                phongto += "&cht=lc";
                //phongto += "&chco=" + chco.TrimEnd(',');
                phongto += "&chds=" + chds;
                phongto += "&chd=t:" + chd.TrimEnd('|');
                phongto += "&chdl=" + chdl.TrimEnd('|');
                phongto += "&chdlp=b";
                phongto += "&chls=2|2|2|2";
                phongto += "&chma=40,20,20,30";
                phongto += "&chg=0,25,1,1";
                phongto += "&chxs=1,676767,11.5,0.5,l,676767";
                if (chkIsLabelMoM.Checked)
                {
                    phongto += "&chm=s,4f81be,0,-1,5|s,c1504d,1,-1,5|s,9cbc59,2,-1,5|s,8064a3,3,-1,5" + chm;
                }
                else
                {
                    phongto += "&chm=s,4f81be,0,-1,5|s,c1504d,1,-1,5|s,9cbc59,2,-1,5|s,8064a3,3,-1,5";
                }
                phongto += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chtt=Biểu+đồ+xuất+nhập+khẩu";
                Common.GetImageFromRedis(keyrPhongTo, phongto);
            }
            ltrChartMoMTT2.Text = "<img id='imgQuyTT' alt=\"\" src=" + srcImg + " />";
            hdfChartMoMTT2.Value = phongto;
        }

        private void GenChartMoMNam(int parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
        {
            string srcImg = "";
            string phongto = "";
            string ft = "";
            for (int fil = 0; fil < chkChitieuMoM.Items.Count; fil++)
            {
                if (chkChitieuMoM.Items[fil].Selected)
                {
                    ft += "." + chkChitieuMoM.Items[fil].Value;
                }
            }
            ft = ft.TrimStart('.');
            string keyr = String.Format("Cafef.Chart.BinaryData.XNKMoM.Nam.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, chkIsLabelMoM.Checked);
            string keyrPhongTo = String.Format("Cafef.Chart.BinaryData.XNKMoM.Nam.Phongto.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, chkIsLabelMoM.Checked);
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
                        chxl += t.ToString() + "|";
                        int itemp = i + 1;
                        chxp += itemp.ToString() + ",";
                    }
                }

                string[] chxlArr = chxl.TrimEnd('|').Split('|');
                int countF = chxlArr.Length;
                string filter = "";
                for (int fil = 0; fil < chkChitieuMoM.Items.Count; fil++)
                {
                    if (chkChitieuMoM.Items[fil].Selected)
                    {
                        filter += "OR CCode=" + chkChitieuMoM.Items[fil].Value;
                    }
                }
                char[] c = { 'O', 'R' };
                dt.DefaultView.RowFilter = filter.TrimStart(c);
                string[] colors = { "4f81be", "c1504d", "9cbc59", "8064a3" };

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
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= -60)
                {
                    rangeTemp1 = "-40";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= -40)
                {
                    rangeTemp1 = "-20";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= -20)
                {
                    rangeTemp1 = "0";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 0)
                {
                    rangeTemp1 = "20";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 20)
                {
                    rangeTemp1 = "40";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 40)
                {
                    rangeTemp1 = "60";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 60)
                {
                    rangeTemp1 = "80";
                }
                if (double.Parse(rangeTemp[0]) <= 80)
                {
                    rangeTemp2 = "60";
                }
                if (double.Parse(rangeTemp[0]) <= 60)
                {
                    rangeTemp2 = "40";
                }
                if (double.Parse(rangeTemp[0]) <= 40)
                {
                    rangeTemp2 = "20";
                }
                if (double.Parse(rangeTemp[0]) <= 20)
                {
                    rangeTemp2 = "0";
                }
                if (double.Parse(rangeTemp[0]) <= 0)
                {
                    rangeTemp2 = "-20";
                }
                if (double.Parse(rangeTemp[0]) <= -20)
                {
                    rangeTemp2 = "-40";
                }
                if (double.Parse(rangeTemp[0]) <= -40)
                {
                    rangeTemp2 = "-60";
                }

                range = rangeTemp2 + "," + rangeTemp1;
                chds = rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1;

                srcImg = "http://chart.apis.google.com/chart";
                srcImg += "?chf=c,s,FFFFFF";
                srcImg += "&chxl=0:|" + chxl.TrimEnd('|');// +"1:|-40%|-20%|0%|20%|40%";
                srcImg += "&chxp=" + chxp; ;// +"|1,-40,20,0,20,40";
                srcImg += "&chxr=0,0," + countF + "|1," + range;
                srcImg += "&chxt=x,y";
                srcImg += "&chs=600x300";
                srcImg += "&cht=lc";
                //srcImg += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chds=" + chds;
                srcImg += "&chd=t:" + chd.TrimEnd('|');
                srcImg += "&chdl=" + chdl.TrimEnd('|');
                srcImg += "&chdlp=b";
                srcImg += "&chls=2|2|2|2";
                srcImg += "&chma=40,20,20,30";
                srcImg += "&chg=0,25,1,1";
                srcImg += "&chxs=1,676767,11.5,0.5,l,676767";
                if (chkIsLabelMoM.Checked)
                {
                    srcImg += "&chm=s,4f81be,0,-1,5|s,c1504d,1,-1,5|s,9cbc59,2,-1,5|s,8064a3,3,-1,5" + chm;
                }
                else
                {
                    srcImg += "&chm=s,4f81be,0,-1,5|s,c1504d,1,-1,5|s,9cbc59,2,-1,5|s,8064a3,3,-1,5";
                }
                srcImg += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chtt=Biểu+đồ+xuất+nhập+khẩu";
                Common.GetImageFromRedis(keyr, srcImg);

                phongto = "http://chart.apis.google.com/chart";
                phongto += "?chf=c,s,FFFFFF";
                phongto += "&chxl=0:|" + chxl.TrimEnd('|');// +"1:|-40%|-20%|0%|20%|40%";
                phongto += "&chxp=" + chxp;// +"|1,-40,20,0,20,40";
                phongto += "&chxr=0,0," + countF + "|1," + range;
                phongto += "&chxt=x,y";
                phongto += "&chs=890x320";
                phongto += "&cht=lc";
                //phongto += "&chco=" + chco.TrimEnd(',');
                phongto += "&chds=" + chds;
                phongto += "&chd=t:" + chd.TrimEnd('|');
                phongto += "&chdl=" + chdl.TrimEnd('|');
                phongto += "&chdlp=b";
                phongto += "&chls=2|2|2|2";
                phongto += "&chma=40,20,20,30";
                phongto += "&chg=0,25,1,1";
                phongto += "&chxs=1,676767,11.5,0.5,l,676767";
                if (chkIsLabelMoM.Checked)
                {
                    phongto += "&chm=s,4f81be,0,-1,5|s,c1504d,1,-1,5|s,9cbc59,2,-1,5|s,8064a3,3,-1,5" + chm;
                }
                else
                {
                    phongto += "&chm=s,4f81be,0,-1,5|s,c1504d,1,-1,5|s,9cbc59,2,-1,5|s,8064a3,3,-1,5";
                }
                phongto += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chtt=Biểu+đồ+xuất+nhập+khẩu";
                Common.GetImageFromRedis(keyrPhongTo, phongto);
            }
            ltrChartMoMTT3.Text = "<img id='imgNamTT' alt=\"\" src=" + srcImg + " />";
            hdfChartMoMTT3.Value = phongto;
        }

        protected void btnViewMoMSoVoi_Click(object sender, EventArgs e)
        {
            int time1 = int.Parse(dlMonth1MoMSoVoi.SelectedValue);
            int time2 = 0;
            int time3 = 0;
            int time4 = int.Parse(dlMonth2MoMSoVoi.SelectedValue);
            int year1 = int.Parse(dlYear1MoMSoVoi.SelectedValue);
            int year2 = 0;
            int year3 = int.Parse(dlYear2MoMSoVoi.SelectedValue);
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
            this.GenChartMoMSoVoi(parentID, time1, time2, time3, time4, year1, year2, year3);
            //Response.Redirect("/dulieuvimo/san-luong-cong-nghiep.chn#phantram");
            ltrScript.Text = "<script type=\"text/javascript\">window.location.href = '/dulieuvimo/xuat-nhap-khau.chn#phantramsovoi';</script>";
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
            this.GenChartQuy(parentID, time1, time2, time3, time4, year1, year2, year3);
            hdfTypeGT.Value = "2";
            ltrScript.Text = "<script type=\"text/javascript\">window.location.href = '/dulieuvimo/xuat-nhap-khau.chn#giatrithuc';</script>";
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
            this.GenChartNam(parentID, time1, time2, time3, time4, year1, year2, year3);
            hdfTypeGT.Value = "3";
            ltrScript.Text = "<script type=\"text/javascript\">window.location.href = '/dulieuvimo/xuat-nhap-khau.chn#giatrithuc';</script>";
        }

        protected void btnViewQuyTT_Click(object sender, EventArgs e)
        {
            int time1 = int.Parse(dlQuarter1TT.SelectedValue);
            int time2 = 0;
            int time3 = 0;
            int time4 = int.Parse(dlQuarter2TT.SelectedValue);
            int year1 = int.Parse(dlYearQuy1TT.SelectedValue);
            int year2 = 0;
            int year3 = int.Parse(dlYearQuy2TT.SelectedValue);
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
            this.GenChartMoMQuy(parentID, time1, time2, time3, time4, year1, year2, year3);
            hdfTypeTT.Value = "2";
            ltrScript.Text = "<script type=\"text/javascript\">window.location.href = '/dulieuvimo/xuat-nhap-khau.chn#phantram';</script>";
        }

        protected void btnViewYoYTT_Click(object sender, EventArgs e)
        {
            int time1 = 1;
            int time2 = 12;
            int time3 = 1;
            int time4 = 12;
            int year1 = int.Parse(dlYear1YoYTT.SelectedValue);
            int year2 = 0;
            int year3 = int.Parse(dlYear2YoYTT.SelectedValue);
            if (year1 == year3)
            {
                year2 = year3;
            }
            else
            {
                int kc = year3 - year1 - 1;
                year2 = year1 + kc;
            }
            this.GenChartMoMNam(parentID, time1, time2, time3, time4, year1, year2, year3);
            hdfTypeTT.Value = "3";
            ltrScript.Text = "<script type=\"text/javascript\">window.location.href = '/dulieuvimo/xuat-nhap-khau.chn#phantram';</script>";
        }

        private void GenChartMoMSoVoi(int parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
        {
            string srcImg = "";
            string phongto = "";
            string ft = "";
            for (int fil = 0; fil < chkChitieuMoMSoVoi.Items.Count; fil++)
            {
                if (chkChitieuMoMSoVoi.Items[fil].Selected)
                {
                    ft += "." + chkChitieuMoMSoVoi.Items[fil].Value;
                }
            }
            ft = ft.TrimStart('.');
            string keyr = String.Format("Cafef.Chart.BinaryData.XNKMoMSoVoi.Thang.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, chkIsLabelMoMSoVoi.Checked);
            string keyrPhongTo = String.Format("Cafef.Chart.BinaryData.XNKMoMSoVoi.Thang.Phongto.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, chkIsLabelMoMSoVoi.Checked);
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
                        int itemp = i + 1;
                        chxp += itemp.ToString() + ",";
                    }
                }

                string[] chxlArr = chxl.TrimEnd('|').Split('|');
                int countF = chxlArr.Length;
                string filter = "";
                for (int fil = 0; fil < chkChitieuMoMSoVoi.Items.Count; fil++)
                {
                    if (chkChitieuMoMSoVoi.Items[fil].Selected)
                    {
                        filter += "OR CCode=" + chkChitieuMoMSoVoi.Items[fil].Value;
                    }
                }
                char[] c = { 'O', 'R' };
                dt.DefaultView.RowFilter = filter.TrimStart(c);
                string[] colors = { "4f81be", "c1504d", "9cbc59", "8064a3" };

                for (int i = 0; i < dt.DefaultView.Count; i++)
                {
                    double ind = 0;
                    double bnind = (double.Parse(dt.DefaultView[i]["MIndex"].ToString()));
                    double unind = 0;
                    int ti = (int.Parse(dt.DefaultView[i]["MTime"].ToString()));
                    int ye = int.Parse(dt.DefaultView[i]["MYear"].ToString()) - 1;
                    DataTable dtOne = new DataTable();
                    //if (ti == 0)
                    //{
                    //    dtOne = IndexBO.IndexCacheSql.pr_IndexMonth_SelectByIDAndTime(int.Parse(dt.DefaultView[i]["Category_ID"].ToString()), 12, (int.Parse(dt.DefaultView[i]["MYear"].ToString())) - 1);
                    //}
                    //else
                    //{
                    dtOne = IndexBO.IndexCacheSql.pr_IndexMonth_SelectByIDAndTime(int.Parse(dt.DefaultView[i]["Category_ID"].ToString()), ti, ye);
                    //}
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
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= -60)
                {
                    rangeTemp1 = "-40";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= -40)
                {
                    rangeTemp1 = "-20";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= -20)
                {
                    rangeTemp1 = "0";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 0)
                {
                    rangeTemp1 = "20";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 20)
                {
                    rangeTemp1 = "40";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 40)
                {
                    rangeTemp1 = "60";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 60)
                {
                    rangeTemp1 = "80";
                }
                if (double.Parse(rangeTemp[0]) <= 80)
                {
                    rangeTemp2 = "60";
                }
                if (double.Parse(rangeTemp[0]) <= 60)
                {
                    rangeTemp2 = "40";
                }
                if (double.Parse(rangeTemp[0]) <= 40)
                {
                    rangeTemp2 = "20";
                }
                if (double.Parse(rangeTemp[0]) <= 20)
                {
                    rangeTemp2 = "0";
                }
                if (double.Parse(rangeTemp[0]) <= 0)
                {
                    rangeTemp2 = "-20";
                }
                if (double.Parse(rangeTemp[0]) <= -20)
                {
                    rangeTemp2 = "-40";
                }
                if (double.Parse(rangeTemp[0]) <= -40)
                {
                    rangeTemp2 = "-60";
                }

                range = rangeTemp2 + "," + rangeTemp1;
                chds = rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1;

                srcImg = "http://chart.apis.google.com/chart";
                srcImg += "?chf=c,s,FFFFFF";
                srcImg += "&chxl=0:|" + chxl.TrimEnd('|');// +"1:|-20%|0%|20%|40%";
                srcImg += "&chxp=" + chxp;// +"|1,20,0,20,40";
                srcImg += "&chxr=0,0," + countF + "|1," + range;
                srcImg += "&chxt=x,y";
                srcImg += "&chs=600x300";
                srcImg += "&cht=lc";
                //srcImg += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chds=" + chds;
                srcImg += "&chd=t:" + chd.TrimEnd('|');
                srcImg += "&chdl=" + chdl.TrimEnd('|');
                srcImg += "&chdlp=b";
                srcImg += "&chls=2|2|2|2";
                srcImg += "&chma=40,20,20,30";
                srcImg += "&chg=0,25,1,1";
                srcImg += "&chxs=1,676767,11.5,0.5,l,676767";
                if (chkIsLabelMoMSoVoi.Checked)
                {
                    srcImg += "&chm=s,4f81be,0,-1,5|s,c1504d,1,-1,5|s,9cbc59,2,-1,5|s,8064a3,3,-1,5" + chm;
                }
                else
                {
                    srcImg += "&chm=s,4f81be,0,-1,5|s,c1504d,1,-1,5|s,9cbc59,2,-1,5|s,8064a3,3,-1,5";
                }
                srcImg += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chtt=Biểu+đồ+xuất+nhập+khẩu";
                Common.GetImageFromRedis(keyr, srcImg);

                phongto = "http://chart.apis.google.com/chart";
                phongto += "?chf=c,s,FFFFFF";
                phongto += "&chxl=0:|" + chxl.TrimEnd('|');// +"1:|-40%|-20%|0%|20%|40%";
                phongto += "&chxp=" + chxp;// +"|1,-40,20,0,20,40";
                phongto += "&chxr=0,0," + countF + "|1," + range;
                phongto += "&chxt=x,y";
                phongto += "&chs=890x320";
                phongto += "&cht=lc";
                //phongto += "&chco=" + chco.TrimEnd(',');
                phongto += "&chds=" + chds;
                phongto += "&chd=t:" + chd.TrimEnd('|');
                phongto += "&chdl=" + chdl.TrimEnd('|');
                phongto += "&chdlp=b";
                phongto += "&chls=2|2|2|2";
                phongto += "&chma=40,20,20,30";
                phongto += "&chg=0,25,1,1";
                phongto += "&chxs=1,676767,11.5,0.5,l,676767";
                if (chkIsLabelMoMSoVoi.Checked)
                {
                    phongto += "&chm=s,4f81be,0,-1,5|s,c1504d,1,-1,5|s,9cbc59,2,-1,5|s,8064a3,3,-1,5" + chm;
                }
                else
                {
                    phongto += "&chm=s,4f81be,0,-1,5|s,c1504d,1,-1,5|s,9cbc59,2,-1,5|s,8064a3,3,-1,5";
                }
                phongto += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chtt=Biểu+đồ+xuất+nhập+khẩu";
                Common.GetImageFromRedis(keyrPhongTo, phongto);
            }
            ltrChartMoMSoVoi.Text = "<img alt=\"\" src=" + srcImg + " />";
            hdfChartMoMSoVoi.Value = phongto;
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