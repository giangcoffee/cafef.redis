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
    public partial class GDPChart : System.Web.UI.UserControl
    {
        private int parentID = 5;
        private int parentIDSS = 397;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GenChartGDP(parentID, 2, 4, 1, 1, 2010, 2010, 2011);
                this.GenChartGDP1(parentID, 2006, 2010);
                //this.GenChartQoQ(parentID, 2, 4, 1, 1, 2010, 2010, 2011);
                this.GenChartQoQSoVoi(parentID, 2, 4, 1, 1, 2010, 2010, 2011);
                this.GenChartYoY(parentID, 2006, 2010);

                this.GenChartGDPSS(parentIDSS, 2, 4, 1, 1, 2010, 2010, 2011);
                this.GenChartGDP1SS(parentIDSS, 2006, 2010);
                this.GenChartQoQSoVoiSS(parentIDSS, 2, 4, 1, 1, 2010, 2010, 2011);
                this.GenChartYoYSS(parentIDSS, 2006, 2010);

                this.SetDefault();
            }
        }

        private void SetDefault()
        {
            dlQuarter1.SelectedValue = "2";
            dlQuarter2.SelectedValue = "1";
            dlYear1.SelectedValue = "2010";
            dlYear2.SelectedValue = "2011";
            dlYear1A.SelectedValue = "2006";
            dlYear2A.SelectedValue = "2010";
            dlQuarter1QoQ.SelectedValue = "2";
            dlQuarter2QoQ.SelectedValue = "1";
            dlYear1QoQ.SelectedValue = "2010";
            dlYear2QoQ.SelectedValue = "2011";
            dlYear1YoY.SelectedValue = "2006";
            dlYear2YoY.SelectedValue = "2010";
            dlQuarter1QoQSoVoi.SelectedValue = "2";
            dlQuarter2QoQSoVoi.SelectedValue = "1";
            dlYear1QoQSoVoi.SelectedValue = "2010";
            dlYear2QoQSoVoi.SelectedValue = "2011";
            hdfTypeGT.Value = "1";

            dlQuarter1SS.SelectedValue = "2";
            dlQuarter1SS.SelectedValue = "1";
            dlYear1SS.SelectedValue = "2010";
            dlYear2SS.SelectedValue = "2011";
            dlYear1ASS.SelectedValue = "2006";
            dlYear2ASS.SelectedValue = "2010";
            dlYear1YoYSS.SelectedValue = "2006";
            dlYear2YoYSS.SelectedValue = "2010";
            dlQuarter1QoQSoVoiSS.SelectedValue = "2";
            dlQuarter2QoQSoVoiSS.SelectedValue = "1";
            dlYear1QoQSoVoiSS.SelectedValue = "2010";
            dlYear2QoQSoVoiSS.SelectedValue = "2011";
            hdfTypeGTSS.Value = "1";
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            int time1 = int.Parse(dlQuarter1.SelectedValue);
            int time2 = 0;
            int time3 = 0;
            int time4 = int.Parse(dlQuarter2.SelectedValue);
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
                time2 = 4;
                time3 = 1;
                year2 = year1 + kc;
            }
            this.GenChartGDP(parentID, time1, time2, time3, time4, year1, year2, year3);
            hdfTypeGT.Value = "1";
            //Response.Redirect("/dulieuvimo/gdp.chn#theoquy");
            ltrScript.Text = "<script type=\"text/javascript\">window.location.href = '/dulieuvimo/gdp.chn#theoquy';</script>";
        }

        protected void btnViewSS_Click(object sender, EventArgs e)
        {
            int time1 = int.Parse(dlQuarter1SS.SelectedValue);
            int time2 = 0;
            int time3 = 0;
            int time4 = int.Parse(dlQuarter2SS.SelectedValue);
            int year1 = int.Parse(dlYear1SS.SelectedValue);
            int year2 = 0;
            int year3 = int.Parse(dlYear2SS.SelectedValue);
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
            this.GenChartGDPSS(parentIDSS, time1, time2, time3, time4, year1, year2, year3);
            hdfTypeGTSS.Value = "1";
            //Response.Redirect("/dulieuvimo/gdp.chn#theoquy");
            ltrScript.Text = "<script type=\"text/javascript\">window.location.href = '/dulieuvimo/gdp.chn#theoquyss';</script>";
        }

        private void GenChartGDP(int parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
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
            string keyr = String.Format("Cafef.Chart.BinaryData.GDP.Quy.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, chkIsLabelQuy.Checked);
            string keyrPhongTo = String.Format("Cafef.Chart.BinaryData.GDP.Quy.Phongto.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, chkIsLabelQuy.Checked);
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
                DataTable dt = IndexBO.IndexCacheSql.pr_Chart_Quarter(parentid, time1, time2, time3, time4, year1, year2, year3);
                DataTable dtTemp = dt;
                if (dt.Rows.Count > 0)
                {
                    dtTemp.DefaultView.RowFilter = "CCode = '" + dt.Rows[0]["CCode"].ToString() + "'";
                    for (int i = 0; i < dtTemp.DefaultView.Count; i++)
                    {
                        int t = int.Parse(dtTemp.DefaultView[i]["QTime"].ToString());
                        int y = int.Parse(dtTemp.DefaultView[i]["QYear"].ToString());
                        chxl += "0" + t.ToString() + "/" + y.ToString().Remove(0, 2) + "|";
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
                string[] colors = { "4f81be", "c1504d", "9cbc59", "8064a3" };
                for (int i = 0; i < dt.DefaultView.Count; i++)
                {
                    double ind = (double.Parse(dt.DefaultView[i]["QIndex"].ToString()) / 1000);
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
                srcImg += "&chxl=1:|" + chxl;// +"2:|0%|1%|2%|3%|4%|5%|6%|7%|8%|9%|10%";
                srcImg += "&chxp=" + chxp.TrimEnd(',');// +"|2,0,1,2,3,4,5,6,7,8,9,10";
                srcImg += "&chxr=0,0,180|1,1," + countF;// +"|2,0,10";
                srcImg += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
                srcImg += "&chxt=y,x";
                srcImg += "&chbh=a";
                srcImg += "&chs=600x300";
                srcImg += "&cht=bvg";
                srcImg += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chds=0,180,0,180,0,180,0,180";
                srcImg += "&chd=t:" + chd.TrimEnd('|');
                srcImg += "&chdl=" + chdl.TrimEnd('|');
                srcImg += "&chdlp=b";
                srcImg += "&chg=0,11,1,1";
                if (chkIsLabelQuy.Checked)
                {
                    srcImg += "&chm=" + chm.TrimEnd('|');
                }
                srcImg += "&chtt=Tổng+sản+phẩm+quốc+nội+(theo+giá+so+sánh+năm+1994)";
                Common.GetImageFromRedis(keyr, srcImg);

                phongto = "http://chart.apis.google.com/chart";
                phongto += "?chf=c,s,FFFFFF";
                phongto += "&chxl=1:|" + chxl;// +"2:|0%|1%|2%|3%|4%|5%|6%|7%|8%|9%|10%";
                phongto += "&chxp=" + chxp.TrimEnd(',');// +"|2,0,1,2,3,4,5,6,7,8,9,10";
                phongto += "&chxr=0,0,180|1,1," + countF;// +"|2,0,10";
                phongto += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
                phongto += "&chxt=y,x";
                phongto += "&chbh=a";
                phongto += "&chs=890x320";
                phongto += "&cht=bvg";
                phongto += "&chco=" + chco.TrimEnd(',');
                phongto += "&chds=0,180,0,180,0,180,0,180";
                phongto += "&chd=t:" + chd.TrimEnd('|');
                phongto += "&chdl=" + chdl.TrimEnd('|');
                phongto += "&chdlp=b";
                phongto += "&chg=0,11,1,1";
                if (chkIsLabelQuy.Checked)
                {
                    phongto += "&chm=" + chm.TrimEnd('|');
                }
                phongto += "&chtt=Tổng+sản+phẩm+quốc+nội+(theo+giá+so+sánh+năm+1994)";
                Common.GetImageFromRedis(keyrPhongTo, phongto);
            }
            ltrChart.Text = "<img id='imgQuyGT' alt=\"\" src=" + srcImg + " />";
            hdfChart1.Value = phongto;
        }

        private void GenChartGDPSS(int parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
        {
            string srcImg = "";
            string phongto = "";
            string ft = "";
            for (int fil = 0; fil < chkChitieuSS.Items.Count; fil++)
            {
                if (chkChitieuSS.Items[fil].Selected)
                {
                    ft += "." + chkChitieuSS.Items[fil].Value;
                }
            }
            ft = ft.TrimStart('.');
            string keyr = String.Format("Cafef.Chart.BinaryData.GDPSS.Quy.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, chkIsLabelQuySS.Checked);
            string keyrPhongTo = String.Format("Cafef.Chart.BinaryData.GDPSS.Quy.Phongto.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, chkIsLabelQuySS.Checked);
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
                DataTable dt = IndexBO.IndexCacheSql.pr_Chart_Quarter(parentid, time1, time2, time3, time4, year1, year2, year3);
                DataTable dtTemp = dt;
                if (dt.Rows.Count > 0)
                {
                    dtTemp.DefaultView.RowFilter = "CCode = '" + dt.Rows[0]["CCode"].ToString() + "'";
                    for (int i = 0; i < dtTemp.DefaultView.Count; i++)
                    {
                        int t = int.Parse(dtTemp.DefaultView[i]["QTime"].ToString());
                        int y = int.Parse(dtTemp.DefaultView[i]["QYear"].ToString());
                        chxl += "0" + t.ToString() + "/" + y.ToString().Remove(0, 2) + "|";
                        int itemp = i + 1;
                        chxp += itemp.ToString() + ",";
                    }
                }

                string[] chxlArr = chxl.TrimEnd('|').Split('|');
                int countF = chxlArr.Length;
                string filter = "";
                for (int fil = 0; fil < chkChitieuSS.Items.Count; fil++)
                {
                    if (chkChitieuSS.Items[fil].Selected)
                    {
                        filter += "OR CCode='" + chkChitieuSS.Items[fil].Value + "'";
                    }
                }
                char[] c = { 'O', 'R' };
                dt.DefaultView.RowFilter = filter.TrimStart(c);
                string[] colors = { "4f81be", "c1504d", "9cbc59", "8064a3" };
                for (int i = 0; i < dt.DefaultView.Count; i++)
                {
                    double ind = (double.Parse(dt.DefaultView[i]["QIndex"].ToString()) / 1000);
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
                srcImg += "&chxl=1:|" + chxl;// +"2:|0%|1%|2%|3%|4%|5%|6%|7%|8%|9%|10%";
                srcImg += "&chxp=" + chxp.TrimEnd(',');// +"|2,0,1,2,3,4,5,6,7,8,9,10";
                srcImg += "&chxr=0,0,600|1,1," + countF;// +"|2,0,10";
                srcImg += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
                srcImg += "&chxt=y,x";
                srcImg += "&chbh=a";
                srcImg += "&chs=600x300";
                srcImg += "&cht=bvg";
                srcImg += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chds=0,600,0,600,0,600,0,600";
                srcImg += "&chd=t:" + chd.TrimEnd('|');
                srcImg += "&chdl=" + chdl.TrimEnd('|');
                srcImg += "&chdlp=b";
                srcImg += "&chg=0,11,1,1";
                if (chkIsLabelQuySS.Checked)
                {
                    srcImg += "&chm=" + chm.TrimEnd('|');
                }
                srcImg += "&chtt=Tổng+sản+phẩm+quốc+nội+(theo+giá+so+sánh+năm+1994)";
                Common.GetImageFromRedis(keyr, srcImg);

                phongto = "http://chart.apis.google.com/chart";
                phongto += "?chf=c,s,FFFFFF";
                phongto += "&chxl=1:|" + chxl;// +"2:|0%|1%|2%|3%|4%|5%|6%|7%|8%|9%|10%";
                phongto += "&chxp=" + chxp.TrimEnd(',');// +"|2,0,1,2,3,4,5,6,7,8,9,10";
                phongto += "&chxr=0,0,600|1,1," + countF;// +"|2,0,10";
                phongto += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
                phongto += "&chxt=y,x";
                phongto += "&chbh=a";
                phongto += "&chs=890x320";
                phongto += "&cht=bvg";
                phongto += "&chco=" + chco.TrimEnd(',');
                phongto += "&chds=0,600,0,600,0,600,0,600";
                phongto += "&chd=t:" + chd.TrimEnd('|');
                phongto += "&chdl=" + chdl.TrimEnd('|');
                phongto += "&chdlp=b";
                phongto += "&chg=0,11,1,1";
                if (chkIsLabelQuySS.Checked)
                {
                    phongto += "&chm=" + chm.TrimEnd('|');
                }
                phongto += "&chtt=Tổng+sản+phẩm+quốc+nội";
                Common.GetImageFromRedis(keyrPhongTo, phongto);
            }
            ltrChartSS.Text = "<img id='imgQuyGTSS' alt=\"\" src=" + srcImg + " />";
            hdfChart1SS.Value = phongto;
        }

        protected void btnView1_Click(object sender, EventArgs e)
        {
            int year1 = int.Parse(dlYear1A.SelectedValue);
            int year3 = int.Parse(dlYear2A.SelectedValue);
            this.GenChartGDP1(5, year1, year3);
            //Response.Redirect("/dulieuvimo/gdp.chn#theonam");
            hdfTypeGT.Value = "2";
            ltrScript.Text = "<script type=\"text/javascript\">window.location.href = '/dulieuvimo/gdp.chn#theoquy';</script>";
        }

        protected void btnView1SS_Click(object sender, EventArgs e)
        {
            int year1 = int.Parse(dlYear1ASS.SelectedValue);
            int year3 = int.Parse(dlYear2ASS.SelectedValue);
            this.GenChartGDP1SS(parentIDSS, year1, year3);
            //Response.Redirect("/dulieuvimo/gdp.chn#theonam");
            hdfTypeGTSS.Value = "2";
            ltrScript.Text = "<script type=\"text/javascript\">window.location.href = '/dulieuvimo/gdp.chn#theoquyss';</script>";
        }

        private void GenChartGDP1(int parentid, int time1, int time2)
        {
            string srcImg = "";
            string phongto = "";
            string ft = "";
            for (int fil = 0; fil < chkChitieu1.Items.Count; fil++)
            {
                if (chkChitieu1.Items[fil].Selected)
                {
                    ft += "." + chkChitieu1.Items[fil].Value;
                }
            }
            ft = ft.TrimStart('.');
            string keyr = String.Format("Cafef.Chart.BinaryData.GDP.Nam.{0}.{1}.{2}.{3}.{4}", parentid, time1, time2, ft, chkIsLabelNam.Checked);
            string keyrPhongTo = String.Format("Cafef.Chart.BinaryData.GDP.Nam.Phongto.{0}.{1}.{2}.{3}.{4}", parentid, time1, time2, ft, chkIsLabelNam.Checked);
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
                DataTable dt = IndexBO.IndexCacheSql.pr_Chart_Year(parentid, time1, time2);
                DataTable dtTemp = dt;
                if (dt.Rows.Count > 0)
                {
                    dtTemp.DefaultView.RowFilter = "CCode = '" + dt.Rows[0]["CCode"].ToString() + "'";
                    for (int i = 0; i < dtTemp.DefaultView.Count; i++)
                    {
                        int t = int.Parse(dtTemp.DefaultView[i]["YTime"].ToString());
                        //int y = int.Parse(dtTemp.DefaultView[i]["QYear"].ToString());
                        chxl += t.ToString() + "|";
                        int itemp = i + 1;
                        chxp += itemp.ToString() + ",";
                    }
                }

                string[] chxlArr = chxl.TrimEnd('|').Split('|');
                int countF = chxlArr.Length;
                string filter = "";
                for (int fil = 0; fil < chkChitieu1.Items.Count; fil++)
                {
                    if (chkChitieu1.Items[fil].Selected)
                    {
                        filter += "OR CCode=" + chkChitieu1.Items[fil].Value;
                    }
                }
                char[] c = { 'O', 'R' };
                dt.DefaultView.RowFilter = filter.TrimStart(c);
                string[] colors = { "4f81be", "c1504d", "9cbc59", "8064a3" };
                for (int i = 0; i < dt.DefaultView.Count; i++)
                {
                    double ind = (double.Parse(dt.DefaultView[i]["YIndex"].ToString()) / 1000);
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
                srcImg += "&chxl=1:|" + chxl; //+ "2:|0%|1%|2%|3%|4%|5%|6%|7%|8%|9%|10%";
                srcImg += "&chxp=" + chxp.TrimEnd(','); //+ "|2,0,1,2,3,4,5,6,7,8,9,10";
                srcImg += "&chxr=0,0,600|1,1," + countF;// + "|2,0,10";
                srcImg += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
                srcImg += "&chxt=y,x";
                srcImg += "&chbh=a";
                srcImg += "&chs=600x300";
                srcImg += "&cht=bvg";
                srcImg += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chds=0,600,0,600,0,600,0,600";
                srcImg += "&chd=t:" + chd.TrimEnd('|');
                srcImg += "&chdl=" + chdl.TrimEnd('|');
                srcImg += "&chdlp=b";
                srcImg += "&chg=0,8.3,1,1";
                if (chkIsLabelNam.Checked)
                {
                    srcImg += "&chm=" + chm.TrimEnd('|');
                }
                srcImg += "&chtt=Tổng+sản+phẩm+quốc+nội+(theo+giá+so+sánh+năm+1994)";
                Common.GetImageFromRedis(keyr, srcImg);

                phongto = "http://chart.apis.google.com/chart";
                phongto += "?chf=c,s,FFFFFF";
                phongto += "&chxl=1:|" + chxl; //+ "2:|0%|1%|2%|3%|4%|5%|6%|7%|8%|9%|10%";
                phongto += "&chxp=" + chxp.TrimEnd(','); //+ "|2,0,1,2,3,4,5,6,7,8,9,10";
                phongto += "&chxr=0,0,600|1,1," + countF;// + "|2,0,10";
                phongto += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
                phongto += "&chxt=y,x";
                phongto += "&chbh=a";
                phongto += "&chs=890x320";
                phongto += "&cht=bvg";
                phongto += "&chco=" + chco.TrimEnd(',');
                phongto += "&chds=0,600,0,600,0,600,0,600";
                phongto += "&chd=t:" + chd.TrimEnd('|');
                phongto += "&chdl=" + chdl.TrimEnd('|');
                phongto += "&chdlp=b";
                phongto += "&chg=0,8.3,1,1";
                if (chkIsLabelNam.Checked)
                {
                    phongto += "&chm=" + chm.TrimEnd('|');
                }
                phongto += "&chtt=Tổng+sản+phẩm+quốc+nội+(theo+giá+so+sánh+năm+1994)";
                Common.GetImageFromRedis(keyrPhongTo, phongto);
            }
            ltrChart1.Text = "<img id='imgNamGT' alt=\"\" src=" + srcImg + " />";
            hdfChart2.Value = phongto;
        }

        private void GenChartGDP1SS(int parentid, int time1, int time2)
        {
            string srcImg = "";
            string phongto = "";
            string ft = "";
            for (int fil = 0; fil < chkChitieu1SS.Items.Count; fil++)
            {
                if (chkChitieu1SS.Items[fil].Selected)
                {
                    ft += "." + chkChitieu1SS.Items[fil].Value;
                }
            }
            ft = ft.TrimStart('.');
            string keyr = String.Format("Cafef.Chart.BinaryData.GDPSS.Nam.{0}.{1}.{2}.{3}.{4}", parentid, time1, time2, ft, chkIsLabelNamSS.Checked);
            string keyrPhongTo = String.Format("Cafef.Chart.BinaryData.GDPSS.Nam.Phongto.{0}.{1}.{2}.{3}.{4}", parentid, time1, time2, ft, chkIsLabelNamSS.Checked);
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
                DataTable dt = IndexBO.IndexCacheSql.pr_Chart_Year(parentid, time1, time2);
                DataTable dtTemp = dt;
                if (dt.Rows.Count > 0)
                {
                    dtTemp.DefaultView.RowFilter = "CCode = '" + dt.Rows[0]["CCode"].ToString() + "'";
                    for (int i = 0; i < dtTemp.DefaultView.Count; i++)
                    {
                        int t = int.Parse(dtTemp.DefaultView[i]["YTime"].ToString());
                        //int y = int.Parse(dtTemp.DefaultView[i]["QYear"].ToString());
                        chxl += t.ToString() + "|";
                        int itemp = i + 1;
                        chxp += itemp.ToString() + ",";
                    }
                }

                string[] chxlArr = chxl.TrimEnd('|').Split('|');
                int countF = chxlArr.Length;
                string filter = "";
                for (int fil = 0; fil < chkChitieu1SS.Items.Count; fil++)
                {
                    if (chkChitieu1SS.Items[fil].Selected)
                    {
                        filter += "OR CCode='" + chkChitieu1SS.Items[fil].Value + "'";
                    }
                }
                char[] c = { 'O', 'R' };
                dt.DefaultView.RowFilter = filter.TrimStart(c);
                string[] colors = { "4f81be", "c1504d", "9cbc59", "8064a3" };
                for (int i = 0; i < dt.DefaultView.Count; i++)
                {
                    double ind = (double.Parse(dt.DefaultView[i]["YIndex"].ToString()) / 1000);
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
                srcImg += "&chxl=1:|" + chxl; //+ "2:|0%|1%|2%|3%|4%|5%|6%|7%|8%|9%|10%";
                srcImg += "&chxp=" + chxp.TrimEnd(','); //+ "|2,0,1,2,3,4,5,6,7,8,9,10";
                srcImg += "&chxr=0,0,2000|1,1," + countF;// + "|2,0,10";
                srcImg += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
                srcImg += "&chxt=y,x";
                srcImg += "&chbh=a";
                srcImg += "&chs=600x300";
                srcImg += "&cht=bvg";
                srcImg += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chds=0,2000,0,2000,0,2000,0,2000";
                srcImg += "&chd=t:" + chd.TrimEnd('|');
                srcImg += "&chdl=" + chdl.TrimEnd('|');
                srcImg += "&chdlp=b";
                srcImg += "&chg=0,8.3,1,1";
                if (chkIsLabelNamSS.Checked)
                {
                    srcImg += "&chm=" + chm.TrimEnd('|');
                }
                srcImg += "&chtt=Tổng+sản+phẩm+quốc+nội+(theo+giá+so+sánh+năm+1994)";
                Common.GetImageFromRedis(keyr, srcImg);

                phongto = "http://chart.apis.google.com/chart";
                phongto += "?chf=c,s,FFFFFF";
                phongto += "&chxl=1:|" + chxl; //+ "2:|0%|1%|2%|3%|4%|5%|6%|7%|8%|9%|10%";
                phongto += "&chxp=" + chxp.TrimEnd(','); //+ "|2,0,1,2,3,4,5,6,7,8,9,10";
                phongto += "&chxr=0,0,2000|1,1," + countF;// + "|2,0,10";
                phongto += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
                phongto += "&chxt=y,x";
                phongto += "&chbh=a";
                phongto += "&chs=890x320";
                phongto += "&cht=bvg";
                phongto += "&chco=" + chco.TrimEnd(',');
                phongto += "&chds=0,2000,0,2000,0,2000,0,2000";
                phongto += "&chd=t:" + chd.TrimEnd('|');
                phongto += "&chdl=" + chdl.TrimEnd('|');
                phongto += "&chdlp=b";
                phongto += "&chg=0,8.3,1,1";
                if (chkIsLabelNamSS.Checked)
                {
                    phongto += "&chm=" + chm.TrimEnd('|');
                }
                phongto += "&chtt=Tổng+sản+phẩm+quốc+nội";
                Common.GetImageFromRedis(keyrPhongTo, phongto);
            }
            ltrChart1SS.Text = "<img id='imgNamGTSS' alt=\"\" src=" + srcImg + " />";
            hdfChart2SS.Value = phongto;
        }

        protected void btnViewQoQ_Click(object sender, EventArgs e)
        {
            int time1 = int.Parse(dlQuarter1QoQ.SelectedValue);
            int time2 = 0;
            int time3 = 0;
            int time4 = int.Parse(dlQuarter2QoQ.SelectedValue);
            int year1 = int.Parse(dlYear1QoQ.SelectedValue);
            int year2 = 0;
            int year3 = int.Parse(dlYear2QoQ.SelectedValue);
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
            this.GenChartQoQ(parentID, time1, time2, time3, time4, year1, year2, year3);
            ltrScript.Text = "<script type=\"text/javascript\">window.location.href = '/dulieuvimo/gdp.chn#gdpqoq';</script>";
        }

        private void GenChartQoQ(int parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
        {
            string srcImg = "";
            string phongto = "";
            string ft = "";
            for (int fil = 0; fil < chkChitieuQoQ.Items.Count; fil++)
            {
                if (chkChitieuQoQ.Items[fil].Selected)
                {
                    ft += "." + chkChitieuQoQ.Items[fil].Value;
                }
            }
            ft = ft.TrimStart('.');
            string keyr = String.Format("Cafef.Chart.BinaryData.GDP.QoQ.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, chkIsLabelQuyQoQ.Checked);
            string keyrPhongTo = String.Format("Cafef.Chart.BinaryData.GDP.QoQ.Phongto.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, chkIsLabelQuyQoQ.Checked);
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
                DataTable dt = IndexBO.IndexCacheSql.pr_Chart_Quarter(parentid, time1, time2, time3, time4, year1, year2, year3);
                DataTable dtTemp = dt;
                if (dt.Rows.Count > 0)
                {
                    dtTemp.DefaultView.RowFilter = "CCode = '" + dt.Rows[0]["CCode"].ToString() + "'";
                    for (int i = 0; i < dtTemp.DefaultView.Count; i++)
                    {
                        int t = int.Parse(dtTemp.DefaultView[i]["QTime"].ToString());
                        int y = int.Parse(dtTemp.DefaultView[i]["QYear"].ToString());
                        chxl += "q" + t.ToString() + "/" + y.ToString().Remove(0, 2) + "|";
                        int itemp = i + 1;
                        chxp += itemp.ToString() + ",";
                    }
                }

                string[] chxlArr = chxl.TrimEnd('|').Split('|');
                int countF = chxlArr.Length;
                string filter = "";
                for (int fil = 0; fil < chkChitieuQoQ.Items.Count; fil++)
                {
                    if (chkChitieuQoQ.Items[fil].Selected)
                    {
                        filter += "OR CCode=" + chkChitieuQoQ.Items[fil].Value;
                    }
                }
                char[] c = { 'O', 'R' };
                dt.DefaultView.RowFilter = filter.TrimStart(c);
                string[] colors = { "4f81be", "c1504d", "9cbc59", "8064a3" };

                for (int i = 0; i < dt.DefaultView.Count; i++)
                {
                    double ind = 0;
                    double bnind = (double.Parse(dt.DefaultView[i]["QIndex"].ToString()));
                    double unind = 0;
                    int ti = (int.Parse(dt.DefaultView[i]["QTime"].ToString())) - 1;
                    DataTable dtOne = new DataTable();
                    if (ti == 0)
                    {
                        dtOne = IndexBO.IndexCacheSql.pr_IndexQuarter_SelectByIDAndTime(int.Parse(dt.DefaultView[i]["Category_ID"].ToString()), 4, (int.Parse(dt.DefaultView[i]["QYear"].ToString())) - 1);
                    }
                    else
                    {
                        dtOne = IndexBO.IndexCacheSql.pr_IndexQuarter_SelectByIDAndTime(int.Parse(dt.DefaultView[i]["Category_ID"].ToString()), ti, int.Parse(dt.DefaultView[i]["QYear"].ToString()));
                    }
                    if (dtOne.Rows.Count > 0)
                    {
                        unind = double.Parse(dtOne.Rows[0]["QIndex"].ToString());
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
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 0)
                {
                    rangeTemp1 = "50";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 50)
                {
                    rangeTemp1 = "100";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 100)
                {
                    rangeTemp1 = "150";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 150)
                {
                    rangeTemp1 = "200";
                }
                if (double.Parse(rangeTemp[0]) <= 0)
                {
                    rangeTemp2 = "-50";
                }
                if (double.Parse(rangeTemp[0]) <= -50)
                {
                    rangeTemp2 = "-100";
                }

                range = rangeTemp2 + "," + rangeTemp1;
                chds = rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1;

                srcImg = "http://chart.apis.google.com/chart";
                srcImg += "?chf=c,s,FFFFFF";
                srcImg += "&chxl=0:|" + chxl.TrimEnd('|');// +"1:|-100%|-50%|-0%|50%|100%|150%|200%";
                srcImg += "&chxp=" + chxp;// +"|1,-100,-50,-0,50,100,150,200";
                srcImg += "&chxr=0,0," + countF + "|1," + range;
                srcImg += "&chxt=x,y";
                srcImg += "&chs=600x300";
                srcImg += "&cht=lc";
                srcImg += "&chco=2A3B5C,AD8A56,021A54,2AD0A9";
                srcImg += "&chds=" + chds;
                srcImg += "&chd=t:" + chd.TrimEnd('|');
                srcImg += "&chdl=" + chdl.TrimEnd('|');
                srcImg += "&chdlp=b";
                srcImg += "&chls=2|2|2|2";
                srcImg += "&chma=40,20,20,30";
                srcImg += "&chg=0,16.7,1,1";
                srcImg += "&chxs=1,676767,11.5,0.5,l,676767";
                if (chkIsLabelQuyQoQ.Checked)
                {
                    srcImg += "&chm=s,2A3B5C,0,-1,5|s,AD8A56,1,-1,5|s,021A54,2,-1,5|s,2AD0A9,3,-1,5" + chm;
                }
                else
                {
                    srcImg += "&chm=s,2A3B5C,0,-1,5|s,AD8A56,1,-1,5|s,021A54,2,-1,5|s,2AD0A9,3,-1,5";
                }
                srcImg += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chtt=Tổng+sản+phẩm+quốc+nội+(GDP)";
                Common.GetImageFromRedis(keyr, srcImg);

                phongto = "http://chart.apis.google.com/chart";
                phongto += "?chf=c,s,FFFFFF";
                phongto += "&chxl=0:|" + chxl.TrimEnd('|');// +"1:|-100%|-50%|-0%|50%|100%|150%|200%";
                phongto += "&chxp=" + chxp;// +"|1,-100,-50,-0,50,100,150,200";
                phongto += "&chxr=0,0," + countF + "|1," + range;
                phongto += "&chxt=x,y";
                phongto += "&chs=890x320";
                phongto += "&cht=lc";
                phongto += "&chco=2A3B5C,AD8A56,021A54,2AD0A9";
                phongto += "&chds=" + chds;
                phongto += "&chd=t:" + chd.TrimEnd('|');
                phongto += "&chdl=" + chdl.TrimEnd('|');
                phongto += "&chdlp=b";
                phongto += "&chls=2|2|2|2";
                phongto += "&chma=40,20,20,30";
                phongto += "&chg=0,16.7,1,1";
                phongto += "&chxs=1,676767,11.5,0.5,l,676767";
                if (chkIsLabelQuyQoQ.Checked)
                {
                    phongto += "&chm=s,2A3B5C,0,-1,5|s,AD8A56,1,-1,5|s,021A54,2,-1,5|s,2AD0A9,3,-1,5" + chm;
                }
                else
                {
                    phongto += "&chm=s,2A3B5C,0,-1,5|s,AD8A56,1,-1,5|s,021A54,2,-1,5|s,2AD0A9,3,-1,5";
                }
                phongto += "&chco=" + chco.TrimEnd(',');
                phongto += "&chtt=Tổng+sản+phẩm+quốc+nội+(GDP)";
                Common.GetImageFromRedis(keyrPhongTo, phongto);
            }
            ltrChartQoQ.Text = "<img alt=\"\" src=" + srcImg + " />";
            hdfChartQoQ.Value = phongto;
        }

        protected void btnViewYoY_Click(object sender, EventArgs e)
        {
            int year1 = int.Parse(dlYear1YoY.SelectedValue);
            int year3 = int.Parse(dlYear2YoY.SelectedValue);
            this.GenChartYoY(parentID, year1, year3);
            hdfTypeGT.Value = "2";
            //Response.Redirect("/dulieuvimo/gdp.chn#gdpyoy");
            ltrScript.Text = "<script type=\"text/javascript\">window.location.href = '/dulieuvimo/gdp.chn#gdpqoqsovoi';</script>";
        }

        protected void btnViewYoYSS_Click(object sender, EventArgs e)
        {
            int year1 = int.Parse(dlYear1YoYSS.SelectedValue);
            int year3 = int.Parse(dlYear2YoYSS.SelectedValue);
            this.GenChartYoYSS(parentIDSS, year1, year3);
            hdfTypeGTSS.Value = "2";
            //Response.Redirect("/dulieuvimo/gdp.chn#gdpyoy");
            ltrScript.Text = "<script type=\"text/javascript\">window.location.href = '/dulieuvimo/gdp.chn#gdpqoqsovoiss';</script>";
        }

        private void GenChartYoY(int parentid, int time1, int time2)
        {
            string srcImg = "";
            string phongto = "";
            string ft = "";
            for (int fil = 0; fil < chkChitieuYoY.Items.Count; fil++)
            {
                if (chkChitieuYoY.Items[fil].Selected)
                {
                    ft += "." + chkChitieuYoY.Items[fil].Value;
                }
            }
            ft = ft.TrimStart('.');
            string keyr = String.Format("Cafef.Chart.BinaryData.GDP.YoY.{0}.{1}.{2}.{3}.{4}", parentid, time1, time2, ft, chkIsLabelNamYoY.Checked);
            string keyrPhongTo = String.Format("Cafef.Chart.BinaryData.GDP.YoY.Phongto.{0}.{1}.{2}.{3}.{4}", parentid, time1, time2, ft, chkIsLabelNamYoY.Checked);
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
                DataTable dt = IndexBO.IndexCacheSql.pr_Chart_Year(parentid, time1, time2);
                DataTable dtTemp = dt;
                if (dt.Rows.Count > 0)
                {
                    dtTemp.DefaultView.RowFilter = "CCode = '" + dt.Rows[0]["CCode"].ToString() + "'";
                    for (int i = 0; i < dtTemp.DefaultView.Count; i++)
                    {
                        int t = int.Parse(dtTemp.DefaultView[i]["YTime"].ToString());
                        //int y = int.Parse(dtTemp.DefaultView[i]["QYear"].ToString());
                        chxl += t.ToString() + "|";
                        int itemp = i + 1;
                        chxp += itemp.ToString() + ",";
                    }
                }

                string[] chxlArr = chxl.TrimEnd('|').Split('|');
                int countF = chxlArr.Length;
                string filter = "";
                for (int fil = 0; fil < chkChitieuYoY.Items.Count; fil++)
                {
                    if (chkChitieuYoY.Items[fil].Selected)
                    {
                        filter += "OR CCode=" + chkChitieuYoY.Items[fil].Value;
                    }
                }
                char[] c = { 'O', 'R' };
                dt.DefaultView.RowFilter = filter.TrimStart(c);
                string[] colors = { "4f81be", "c1504d", "9cbc59", "8064a3" };

                for (int i = 0; i < dt.DefaultView.Count; i++)
                {
                    double ind = 0;
                    double bnind = (double.Parse(dt.DefaultView[i]["YIndex"].ToString()));
                    double unind = 0;
                    int ti = (int.Parse(dt.DefaultView[i]["YTime"].ToString())) - 1;
                    DataTable dtOne = new DataTable();

                    dtOne = IndexBO.IndexCacheSql.pr_IndexYear_SelectByIDAndTime(int.Parse(dt.DefaultView[i]["Category_ID"].ToString()), ti);
                    if (dtOne.Rows.Count > 0)
                    {
                        unind = double.Parse(dtOne.Rows[0]["YIndex"].ToString());
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
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 10)
                {
                    rangeTemp1 = "12";
                }
                if (double.Parse(rangeTemp[0]) <= 12)
                {
                    rangeTemp2 = "10";
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

                range = rangeTemp2 + "," + rangeTemp1;
                chds = rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1;

                srcImg = "http://chart.apis.google.com/chart";
                srcImg += "?chf=c,s,FFFFFF";
                srcImg += "&chxl=0:|" + chxl.TrimEnd('|'); //+"1:|0%|2%|4%|6%|8%|10%|12%";
                srcImg += "&chxp=" + chxp; //+"|1,0,2,4,6,8,10,12";
                srcImg += "&chxr=0,0," + countF + "|1," + range;
                srcImg += "&chxt=x,y";
                srcImg += "&chs=600x300";
                srcImg += "&cht=lc";
                srcImg += "&chco=2A3B5C,AD8A56,021A54,2AD0A9";
                srcImg += "&chds=" + chds;
                srcImg += "&chd=t:" + chd.TrimEnd('|');
                srcImg += "&chdl=" + chdl.TrimEnd('|');
                srcImg += "&chdlp=b";
                srcImg += "&chls=2|2|2|2";
                srcImg += "&chma=40,20,20,30";
                srcImg += "&chg=0,16.6,1,1";
                srcImg += "&chxs=1,676767,11.5,0.5,l,676767";
                if (chkIsLabelNamYoY.Checked)
                {
                    srcImg += "&chm=s,2A3B5C,0,-1,5|s,AD8A56,1,-1,5|s,021A54,2,-1,5|s,2AD0A9,3,-1,5" + chm;
                }
                else
                {
                    srcImg += "&chm=s,2A3B5C,0,-1,5|s,AD8A56,1,-1,5|s,021A54,2,-1,5|s,2AD0A9,3,-1,5";
                }
                srcImg += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chtt=Tổng+sản+phẩm+quốc+nội+(GDP)";
                Common.GetImageFromRedis(keyr, srcImg);

                phongto = "http://chart.apis.google.com/chart";
                phongto += "?chf=c,s,FFFFFF";
                phongto += "&chxl=0:|" + chxl.TrimEnd('|');// +"1:|0%|2%|4%|6%|8%|10%|12%";
                phongto += "&chxp=" + chxp;// +"|1,0,2,4,6,8,10,12";
                phongto += "&chxr=0,0," + countF + "|1," + range;
                phongto += "&chxt=x,y";
                phongto += "&chs=890x320";
                phongto += "&cht=lc";
                phongto += "&chco=2A3B5C,AD8A56,021A54,2AD0A9";
                phongto += "&chds=" + chds;
                phongto += "&chd=t:" + chd.TrimEnd('|');
                phongto += "&chdl=" + chdl.TrimEnd('|');
                phongto += "&chdlp=b";
                phongto += "&chls=2|2|2|2";
                phongto += "&chma=40,20,20,30";
                phongto += "&chg=0,16.6,1,1";
                phongto += "&chxs=1,676767,11.5,0.5,l,676767";
                if (chkIsLabelNamYoY.Checked)
                {
                    phongto += "&chm=s,2A3B5C,0,-1,5|s,AD8A56,1,-1,5|s,021A54,2,-1,5|s,2AD0A9,3,-1,5" + chm;
                }
                else
                {
                    phongto += "&chm=s,2A3B5C,0,-1,5|s,AD8A56,1,-1,5|s,021A54,2,-1,5|s,2AD0A9,3,-1,5";
                }
                phongto += "&chco=" + chco.TrimEnd(',');
                phongto += "&chtt=Tổng+sản+phẩm+quốc+nội+(theo+giá+so+sánh+năm+1994)";
                Common.GetImageFromRedis(keyrPhongTo, phongto);
            }
            ltrChartYoY.Text = "<img id='imgNamTT' alt=\"\" src=" + srcImg + " />";
            hdfChartYoY.Value = phongto;
        }

        private void GenChartYoYSS(int parentid, int time1, int time2)
        {
            string srcImg = "";
            string phongto = "";
            string ft = "";
            for (int fil = 0; fil < chkChitieuYoYSS.Items.Count; fil++)
            {
                if (chkChitieuYoYSS.Items[fil].Selected)
                {
                    ft += "." + chkChitieuYoYSS.Items[fil].Value;
                }
            }
            ft = ft.TrimStart('.');
            string keyr = String.Format("Cafef.Chart.BinaryData.GDPSS.YoY.{0}.{1}.{2}.{3}.{4}", parentid, time1, time2, ft, chkIsLabelNamYoYSS.Checked);
            string keyrPhongTo = String.Format("Cafef.Chart.BinaryData.GDPSS.YoY.Phongto.{0}.{1}.{2}.{3}.{4}", parentid, time1, time2, ft, chkIsLabelNamYoYSS.Checked);
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
                DataTable dt = IndexBO.IndexCacheSql.pr_Chart_Year(parentid, time1, time2);
                DataTable dtTemp = dt;
                if (dt.Rows.Count > 0)
                {
                    dtTemp.DefaultView.RowFilter = "CCode = '" + dt.Rows[0]["CCode"].ToString() + "'";
                    for (int i = 0; i < dtTemp.DefaultView.Count; i++)
                    {
                        int t = int.Parse(dtTemp.DefaultView[i]["YTime"].ToString());
                        //int y = int.Parse(dtTemp.DefaultView[i]["QYear"].ToString());
                        chxl += t.ToString() + "|";
                        int itemp = i + 1;
                        chxp += itemp.ToString() + ",";
                    }
                }

                string[] chxlArr = chxl.TrimEnd('|').Split('|');
                int countF = chxlArr.Length;
                string filter = "";
                for (int fil = 0; fil < chkChitieuYoYSS.Items.Count; fil++)
                {
                    if (chkChitieuYoYSS.Items[fil].Selected)
                    {
                        filter += "OR CCode='" + chkChitieuYoYSS.Items[fil].Value + "'";
                    }
                }
                char[] c = { 'O', 'R' };
                dt.DefaultView.RowFilter = filter.TrimStart(c);
                string[] colors = { "4f81be", "c1504d", "9cbc59", "8064a3" };

                for (int i = 0; i < dt.DefaultView.Count; i++)
                {
                    double ind = 0;
                    double bnind = (double.Parse(dt.DefaultView[i]["YIndex"].ToString()));
                    double unind = 0;
                    int ti = (int.Parse(dt.DefaultView[i]["YTime"].ToString())) - 1;
                    DataTable dtOne = new DataTable();

                    dtOne = IndexBO.IndexCacheSql.pr_IndexYear_SelectByIDAndTime(int.Parse(dt.DefaultView[i]["Category_ID"].ToString()), ti);
                    if (dtOne.Rows.Count > 0)
                    {
                        unind = double.Parse(dtOne.Rows[0]["YIndex"].ToString());
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
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 0)
                {
                    rangeTemp1 = "10";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 10)
                {
                    rangeTemp1 = "20";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 20)
                {
                    rangeTemp1 = "30";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 30)
                {
                    rangeTemp1 = "40";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 40)
                {
                    rangeTemp1 = "50";
                }
                if (double.Parse(rangeTemp[0]) <= 50)
                {
                    rangeTemp2 = "40";
                }
                if (double.Parse(rangeTemp[0]) <= 40)
                {
                    rangeTemp2 = "30";
                }
                if (double.Parse(rangeTemp[0]) <= 30)
                {
                    rangeTemp2 = "20";
                }
                if (double.Parse(rangeTemp[0]) <= 20)
                {
                    rangeTemp2 = "10";
                }
                if (double.Parse(rangeTemp[0]) <= 10)
                {
                    rangeTemp2 = "0";
                }
                range = rangeTemp2 + "," + rangeTemp1;
                chds = rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1;

                srcImg = "http://chart.apis.google.com/chart";
                srcImg += "?chf=c,s,FFFFFF";
                srcImg += "&chxl=0:|" + chxl.TrimEnd('|'); //+"1:|0%|2%|4%|6%|8%|10%|12%";
                srcImg += "&chxp=" + chxp; //+"|1,0,2,4,6,8,10,12";
                srcImg += "&chxr=0,0," + countF + "|1," + range;
                srcImg += "&chxt=x,y";
                srcImg += "&chs=600x300";
                srcImg += "&cht=lc";
                srcImg += "&chco=2A3B5C,AD8A56,021A54,2AD0A9";
                srcImg += "&chds=" + chds;
                srcImg += "&chd=t:" + chd.TrimEnd('|');
                srcImg += "&chdl=" + chdl.TrimEnd('|');
                srcImg += "&chdlp=b";
                srcImg += "&chls=2|2|2|2";
                srcImg += "&chma=40,20,20,30";
                srcImg += "&chg=0,16.6,1,1";
                srcImg += "&chxs=1,676767,11.5,0.5,l,676767";
                if (chkIsLabelNamYoYSS.Checked)
                {
                    srcImg += "&chm=s,2A3B5C,0,-1,5|s,AD8A56,1,-1,5|s,021A54,2,-1,5|s,2AD0A9,3,-1,5" + chm;
                }
                else
                {
                    srcImg += "&chm=s,2A3B5C,0,-1,5|s,AD8A56,1,-1,5|s,021A54,2,-1,5|s,2AD0A9,3,-1,5";
                }
                srcImg += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chtt=Tổng+sản+phẩm+quốc+nội+(GDP)";
                Common.GetImageFromRedis(keyr, srcImg);

                phongto = "http://chart.apis.google.com/chart";
                phongto += "?chf=c,s,FFFFFF";
                phongto += "&chxl=0:|" + chxl.TrimEnd('|');// +"1:|0%|2%|4%|6%|8%|10%|12%";
                phongto += "&chxp=" + chxp;// +"|1,0,2,4,6,8,10,12";
                phongto += "&chxr=0,0," + countF + "|1," + range;
                phongto += "&chxt=x,y";
                phongto += "&chs=890x320";
                phongto += "&cht=lc";
                phongto += "&chco=2A3B5C,AD8A56,021A54,2AD0A9";
                phongto += "&chds=" + chds;
                phongto += "&chd=t:" + chd.TrimEnd('|');
                phongto += "&chdl=" + chdl.TrimEnd('|');
                phongto += "&chdlp=b";
                phongto += "&chls=2|2|2|2";
                phongto += "&chma=40,20,20,30";
                phongto += "&chg=0,16.6,1,1";
                phongto += "&chxs=1,676767,11.5,0.5,l,676767";
                if (chkIsLabelNamYoYSS.Checked)
                {
                    phongto += "&chm=s,2A3B5C,0,-1,5|s,AD8A56,1,-1,5|s,021A54,2,-1,5|s,2AD0A9,3,-1,5" + chm;
                }
                else
                {
                    phongto += "&chm=s,2A3B5C,0,-1,5|s,AD8A56,1,-1,5|s,021A54,2,-1,5|s,2AD0A9,3,-1,5";
                }
                phongto += "&chco=" + chco.TrimEnd(',');
                phongto += "&chtt=Tổng+sản+phẩm+quốc+nội";
                Common.GetImageFromRedis(keyrPhongTo, phongto);
            }
            ltrChartYoYSS.Text = "<img id='imgNamTTSS' alt=\"\" src=" + srcImg + " />";
            hdfChartYoYSS.Value = phongto;
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

        protected void btnViewQoQSoVoi_Click(object sender, EventArgs e)
        {
            int time1 = int.Parse(dlQuarter1QoQSoVoi.SelectedValue);
            int time2 = 0;
            int time3 = 0;
            int time4 = int.Parse(dlQuarter2QoQSoVoi.SelectedValue);
            int year1 = int.Parse(dlYear1QoQSoVoi.SelectedValue);
            int year2 = 0;
            int year3 = int.Parse(dlYear2QoQSoVoi.SelectedValue);
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
            this.GenChartQoQSoVoi(parentID, time1, time2, time3, time4, year1, year2, year3);
            hdfTypeGT.Value = "1";
            ltrScript.Text = "<script type=\"text/javascript\">window.location.href = '/dulieuvimo/gdp.chn#gdpqoqsovoi';</script>";
        }

        protected void btnViewQoQSoVoiSS_Click(object sender, EventArgs e)
        {
            int time1 = int.Parse(dlQuarter1QoQSoVoiSS.SelectedValue);
            int time2 = 0;
            int time3 = 0;
            int time4 = int.Parse(dlQuarter2QoQSoVoiSS.SelectedValue);
            int year1 = int.Parse(dlYear1QoQSoVoiSS.SelectedValue);
            int year2 = 0;
            int year3 = int.Parse(dlYear2QoQSoVoiSS.SelectedValue);
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
            this.GenChartQoQSoVoiSS(parentIDSS, time1, time2, time3, time4, year1, year2, year3);
            hdfTypeGTSS.Value = "1";
            ltrScript.Text = "<script type=\"text/javascript\">window.location.href = '/dulieuvimo/gdp.chn#gdpqoqsovoiss';</script>";
        }

        private void GenChartQoQSoVoi(int parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
        {
            string srcImg = "";
            string phongto = "";
            string ft = "";
            for (int fil = 0; fil < chkChitieuQoQSoVoi.Items.Count; fil++)
            {
                if (chkChitieuQoQSoVoi.Items[fil].Selected)
                {
                    ft += "." + chkChitieuQoQSoVoi.Items[fil].Value;
                }
            }
            ft = ft.TrimStart('.');
            string keyr = String.Format("Cafef.Chart.BinaryData.GDP.QoQSoVoi.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, chkIsLabelQuyQoQSoVoi.Checked);
            string keyrPhongTo = String.Format("Cafef.Chart.BinaryData.GDP.QoQSoVoi.Phongto.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, chkIsLabelQuyQoQSoVoi.Checked);
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
                DataTable dt = IndexBO.IndexCacheSql.pr_Chart_Quarter(parentid, time1, time2, time3, time4, year1, year2, year3);
                DataTable dtTemp = dt;
                if (dt.Rows.Count > 0)
                {
                    dtTemp.DefaultView.RowFilter = "CCode = '" + dt.Rows[0]["CCode"].ToString() + "'";
                    for (int i = 0; i < dtTemp.DefaultView.Count; i++)
                    {
                        int t = int.Parse(dtTemp.DefaultView[i]["QTime"].ToString());
                        int y = int.Parse(dtTemp.DefaultView[i]["QYear"].ToString());
                        chxl += "0" + t.ToString() + "/" + y.ToString().Remove(0, 2) + "|";
                        int itemp = i + 1;
                        chxp += itemp.ToString() + ",";
                    }
                }

                string[] chxlArr = chxl.TrimEnd('|').Split('|');
                int countF = chxlArr.Length;
                string filter = "";
                for (int fil = 0; fil < chkChitieuQoQSoVoi.Items.Count; fil++)
                {
                    if (chkChitieuQoQSoVoi.Items[fil].Selected)
                    {
                        filter += "OR CCode=" + chkChitieuQoQSoVoi.Items[fil].Value;
                    }
                }
                char[] c = { 'O', 'R' };
                dt.DefaultView.RowFilter = filter.TrimStart(c);
                string[] colors = { "4f81be", "c1504d", "9cbc59", "8064a3" };

                for (int i = 0; i < dt.DefaultView.Count; i++)
                {
                    double ind = 0;
                    double bnind = (double.Parse(dt.DefaultView[i]["QIndex"].ToString()));
                    double unind = 0;
                    int ti = (int.Parse(dt.DefaultView[i]["QTime"].ToString()));
                    int ye = int.Parse(dt.DefaultView[i]["QYear"].ToString()) - 1;
                    DataTable dtOne = new DataTable();
                    //if (ti == 0)
                    //{
                    //    dtOne = IndexBO.IndexCacheSql.pr_IndexQuarter_SelectByIDAndTime(int.Parse(dt.DefaultView[i]["Category_ID"].ToString()), 4, (int.Parse(dt.DefaultView[i]["QYear"].ToString())) - 1);
                    //}
                    //else
                    //{
                    dtOne = IndexBO.IndexCacheSql.pr_IndexQuarter_SelectByIDAndTime(int.Parse(dt.DefaultView[i]["Category_ID"].ToString()), ti, ye);
                    //}
                    if (dtOne.Rows.Count > 0)
                    {
                        unind = double.Parse(dtOne.Rows[0]["QIndex"].ToString());
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
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 10)
                {
                    rangeTemp1 = "12";
                }
                if (double.Parse(rangeTemp[0]) <= 12)
                {
                    rangeTemp2 = "10";
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

                range = rangeTemp2 + "," + rangeTemp1;
                chds = rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1;

                srcImg = "http://chart.apis.google.com/chart";
                srcImg += "?chf=c,s,FFFFFF";
                srcImg += "&chxl=0:|" + chxl.TrimEnd('|');// +"1:|-100%|-50%|-0%|50%|100%|150%|200%";
                srcImg += "&chxp=" + chxp;// +"|1,-100,-50,-0,50,100,150,200";
                srcImg += "&chxr=0,0," + countF + "|1," + range;
                srcImg += "&chxt=x,y";
                srcImg += "&chs=600x300";
                srcImg += "&cht=lc";
                srcImg += "&chco=2A3B5C,AD8A56,021A54,2AD0A9";
                srcImg += "&chds=" + chds;
                srcImg += "&chd=t:" + chd.TrimEnd('|');
                srcImg += "&chdl=" + chdl.TrimEnd('|');
                srcImg += "&chdlp=b";
                srcImg += "&chls=2|2|2|2";
                srcImg += "&chma=40,20,20,30";
                srcImg += "&chg=0,16.7,1,1";
                srcImg += "&chxs=1,676767,11.5,0.5,l,676767";
                if (chkIsLabelQuyQoQSoVoi.Checked)
                {
                    srcImg += "&chm=s,2A3B5C,0,-1,5|s,AD8A56,1,-1,5|s,021A54,2,-1,5|s,2AD0A9,3,-1,5" + chm;
                }
                else
                {
                    srcImg += "&chm=s,2A3B5C,0,-1,5|s,AD8A56,1,-1,5|s,021A54,2,-1,5|s,2AD0A9,3,-1,5";
                }
                srcImg += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chtt=Tổng+sản+phẩm+quốc+nội+(GDP)";
                Common.GetImageFromRedis(keyr, srcImg);

                phongto = "http://chart.apis.google.com/chart";
                phongto += "?chf=c,s,FFFFFF";
                phongto += "&chxl=0:|" + chxl.TrimEnd('|');// +"1:|-100%|-50%|-0%|50%|100%|150%|200%";
                phongto += "&chxp=" + chxp;// +"|1,-100,-50,-0,50,100,150,200";
                phongto += "&chxr=0,0," + countF + "|1," + range;
                phongto += "&chxt=x,y";
                phongto += "&chs=890x320";
                phongto += "&cht=lc";
                phongto += "&chco=2A3B5C,AD8A56,021A54,2AD0A9";
                phongto += "&chds=" + chds;
                phongto += "&chd=t:" + chd.TrimEnd('|');
                phongto += "&chdl=" + chdl.TrimEnd('|');
                phongto += "&chdlp=b";
                phongto += "&chls=2|2|2|2";
                phongto += "&chma=40,20,20,30";
                phongto += "&chg=0,16.7,1,1";
                phongto += "&chxs=1,676767,11.5,0.5,l,676767";
                if (chkIsLabelQuyQoQSoVoi.Checked)
                {
                    phongto += "&chm=s,2A3B5C,0,-1,5|s,AD8A56,1,-1,5|s,021A54,2,-1,5|s,2AD0A9,3,-1,5" + chm;
                }
                else
                {
                    phongto += "&chm=s,2A3B5C,0,-1,5|s,AD8A56,1,-1,5|s,021A54,2,-1,5|s,2AD0A9,3,-1,5";
                }
                phongto += "&chco=" + chco.TrimEnd(',');
                phongto += "&chtt=Tổng+sản+phẩm+quốc+nội+(theo+giá+so+sánh+năm+1994)";
                Common.GetImageFromRedis(keyrPhongTo, phongto);
            }
            ltrChartQoQSoVoi.Text = "<img id='imgQuyTT' alt=\"\" src=" + srcImg + " />";
            hdfChartQoQ.Value = phongto;
        }

        private void GenChartQoQSoVoiSS(int parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
        {
            string srcImg = "";
            string phongto = "";
            string ft = "";
            for (int fil = 0; fil < chkChitieuQoQSoVoiSS.Items.Count; fil++)
            {
                if (chkChitieuQoQSoVoiSS.Items[fil].Selected)
                {
                    ft += "." + chkChitieuQoQSoVoiSS.Items[fil].Value;
                }
            }
            ft = ft.TrimStart('.');
            string keyr = String.Format("Cafef.Chart.BinaryData.GDPSS.QoQSoVoi.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, chkIsLabelQuyQoQSoVoiSS.Checked);
            string keyrPhongTo = String.Format("Cafef.Chart.BinaryData.GDPSS.QoQSoVoi.Phongto.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, chkIsLabelQuyQoQSoVoiSS.Checked);
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
                DataTable dt = IndexBO.IndexCacheSql.pr_Chart_Quarter(parentid, time1, time2, time3, time4, year1, year2, year3);
                DataTable dtTemp = dt;
                if (dt.Rows.Count > 0)
                {
                    dtTemp.DefaultView.RowFilter = "CCode = '" + dt.Rows[0]["CCode"].ToString() + "'";
                    for (int i = 0; i < dtTemp.DefaultView.Count; i++)
                    {
                        int t = int.Parse(dtTemp.DefaultView[i]["QTime"].ToString());
                        int y = int.Parse(dtTemp.DefaultView[i]["QYear"].ToString());
                        chxl += "0" + t.ToString() + "/" + y.ToString().Remove(0, 2) + "|";
                        int itemp = i + 1;
                        chxp += itemp.ToString() + ",";
                    }
                }

                string[] chxlArr = chxl.TrimEnd('|').Split('|');
                int countF = chxlArr.Length;
                string filter = "";
                for (int fil = 0; fil < chkChitieuQoQSoVoiSS.Items.Count; fil++)
                {
                    if (chkChitieuQoQSoVoiSS.Items[fil].Selected)
                    {
                        filter += "OR CCode='" + chkChitieuQoQSoVoiSS.Items[fil].Value + "'";
                    }
                }
                char[] c = { 'O', 'R' };
                dt.DefaultView.RowFilter = filter.TrimStart(c);
                string[] colors = { "4f81be", "c1504d", "9cbc59", "8064a3" };

                for (int i = 0; i < dt.DefaultView.Count; i++)
                {
                    double ind = 0;
                    double bnind = (double.Parse(dt.DefaultView[i]["QIndex"].ToString()));
                    double unind = 0;
                    int ti = (int.Parse(dt.DefaultView[i]["QTime"].ToString()));
                    int ye = int.Parse(dt.DefaultView[i]["QYear"].ToString()) - 1;
                    DataTable dtOne = new DataTable();
                    //if (ti == 0)
                    //{
                    //    dtOne = IndexBO.IndexCacheSql.pr_IndexQuarter_SelectByIDAndTime(int.Parse(dt.DefaultView[i]["Category_ID"].ToString()), 4, (int.Parse(dt.DefaultView[i]["QYear"].ToString())) - 1);
                    //}
                    //else
                    //{
                    dtOne = IndexBO.IndexCacheSql.pr_IndexQuarter_SelectByIDAndTime(int.Parse(dt.DefaultView[i]["Category_ID"].ToString()), ti, ye);
                    //}
                    if (dtOne.Rows.Count > 0)
                    {
                        unind = double.Parse(dtOne.Rows[0]["QIndex"].ToString());
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
                    rangeTemp1 = "10";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 10)
                {
                    rangeTemp1 = "20";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 20)
                {
                    rangeTemp1 = "30";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 30)
                {
                    rangeTemp1 = "40";
                }
                if (double.Parse(rangeTemp[rangeTemp.Length - 1]) >= 40)
                {
                    rangeTemp1 = "50";
                }
                if (double.Parse(rangeTemp[0]) <= 50)
                {
                    rangeTemp2 = "40";
                }
                if (double.Parse(rangeTemp[0]) <= 40)
                {
                    rangeTemp2 = "30";
                }
                if (double.Parse(rangeTemp[0]) <= 30)
                {
                    rangeTemp2 = "20";
                }
                if (double.Parse(rangeTemp[0]) <= 20)
                {
                    rangeTemp2 = "10";
                }
                if (double.Parse(rangeTemp[0]) <= 10)
                {
                    rangeTemp2 = "0";
                }                

                range = rangeTemp2 + "," + rangeTemp1;
                chds = rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1 + "," + rangeTemp2 + "," + rangeTemp1;

                srcImg = "http://chart.apis.google.com/chart";
                srcImg += "?chf=c,s,FFFFFF";
                srcImg += "&chxl=0:|" + chxl.TrimEnd('|');// +"1:|-100%|-50%|-0%|50%|100%|150%|200%";
                srcImg += "&chxp=" + chxp;// +"|1,-100,-50,-0,50,100,150,200";
                srcImg += "&chxr=0,0," + countF + "|1," + range;
                srcImg += "&chxt=x,y";
                srcImg += "&chs=600x300";
                srcImg += "&cht=lc";
                srcImg += "&chco=2A3B5C,AD8A56,021A54,2AD0A9";
                srcImg += "&chds=" + chds;
                srcImg += "&chd=t:" + chd.TrimEnd('|');
                srcImg += "&chdl=" + chdl.TrimEnd('|');
                srcImg += "&chdlp=b";
                srcImg += "&chls=2|2|2|2";
                srcImg += "&chma=40,20,20,30";
                srcImg += "&chg=0,16.7,1,1";
                srcImg += "&chxs=1,676767,11.5,0.5,l,676767";
                if (chkIsLabelQuyQoQSoVoiSS.Checked)
                {
                    srcImg += "&chm=s,2A3B5C,0,-1,5|s,AD8A56,1,-1,5|s,021A54,2,-1,5|s,2AD0A9,3,-1,5" + chm;
                }
                else
                {
                    srcImg += "&chm=s,2A3B5C,0,-1,5|s,AD8A56,1,-1,5|s,021A54,2,-1,5|s,2AD0A9,3,-1,5";
                }
                srcImg += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chtt=Tổng+sản+phẩm+quốc+nội+(GDP)";
                Common.GetImageFromRedis(keyr, srcImg);

                phongto = "http://chart.apis.google.com/chart";
                phongto += "?chf=c,s,FFFFFF";
                phongto += "&chxl=0:|" + chxl.TrimEnd('|');// +"1:|-100%|-50%|-0%|50%|100%|150%|200%";
                phongto += "&chxp=" + chxp;// +"|1,-100,-50,-0,50,100,150,200";
                phongto += "&chxr=0,0," + countF + "|1," + range;
                phongto += "&chxt=x,y";
                phongto += "&chs=890x320";
                phongto += "&cht=lc";
                phongto += "&chco=2A3B5C,AD8A56,021A54,2AD0A9";
                phongto += "&chds=" + chds;
                phongto += "&chd=t:" + chd.TrimEnd('|');
                phongto += "&chdl=" + chdl.TrimEnd('|');
                phongto += "&chdlp=b";
                phongto += "&chls=2|2|2|2";
                phongto += "&chma=40,20,20,30";
                phongto += "&chg=0,16.7,1,1";
                phongto += "&chxs=1,676767,11.5,0.5,l,676767";
                if (chkIsLabelQuyQoQSoVoiSS.Checked)
                {
                    phongto += "&chm=s,2A3B5C,0,-1,5|s,AD8A56,1,-1,5|s,021A54,2,-1,5|s,2AD0A9,3,-1,5" + chm;
                }
                else
                {
                    phongto += "&chm=s,2A3B5C,0,-1,5|s,AD8A56,1,-1,5|s,021A54,2,-1,5|s,2AD0A9,3,-1,5";
                }
                phongto += "&chco=" + chco.TrimEnd(',');
                phongto += "&chtt=Tổng+sản+phẩm+quốc+nội+(theo+giá+so+sánh+năm+1994)";
                Common.GetImageFromRedis(keyrPhongTo, phongto);
            }
            ltrChartQoQSoVoiSS.Text = "<img id='imgQuyTTSS' alt=\"\" src=" + srcImg + " />";
            hdfChartQoQSoVoiSS.Value = phongto;
        }
    }
}