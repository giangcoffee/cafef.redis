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
using System.Text;
using System.Xml.Linq;
using CafeF.DLVM.DA.BO.DuLieuViMo;
using CafeF.DLVM.BO.Cache;
using CafeF.DLVM.BO.Utilitis;

namespace CafeF.Redis.Page.UserControl.DuLieuViMo
{
    public partial class FDIChart : System.Web.UI.UserControl
    {
        private int parentID = 15;
        private string code = "151000";
        private string duan = "151000";
        private string sovon = "152000";
        private string giaingan = "153000";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.SetDefault();
                this.GenChartDuAn(parentID, 6, 12, 1, 5, 2010, 2010, 2011);
                this.GenChartSoVon(parentID, 6, 12, 1, 5, 2010, 2010, 2011);
                this.GenChartGiaiNgan(parentID, 6, 12, 1, 5, 2010, 2010, 2011);
                this.GenChartMoMSoVoi(parentID, 6, 12, 1, 5, 2010, 2010, 2011);
                //this.GenChart(parentID, 9, 12, 2009, 2010, code);
            }
        }

        private void SetDefault()
        {
            dlMonth1.SelectedValue = "9";
            dlMonth2.SelectedValue = "12";
            dlYear1.SelectedValue = "2009";
            dlYear2.SelectedValue = "2010";
            dlMonth1DuAn.SelectedValue = "6";
            dlMonth2DuAn.SelectedValue = "5";
            dlYear1DuAn.SelectedValue = "2010";
            dlYear2DuAn.SelectedValue = "2011";
            dlMonth1SoVon.SelectedValue = "6";
            dlMonth2SoVon.SelectedValue = "5";
            dlYear1SoVon.SelectedValue = "2010";
            dlYear2SoVon.SelectedValue = "2011";
            dlMonth1GiaiNgan.SelectedValue = "6";
            dlMonth2GiaiNgan.SelectedValue = "5";
            dlYear1GiaiNgan.SelectedValue = "2010";
            dlYear2GiaiNgan.SelectedValue = "2011";
            dlMonth1MoMSoVoi.SelectedValue = "6";
            dlMonth2MoMSoVoi.SelectedValue = "5";
            dlYear1MoMSoVoi.SelectedValue = "2010";
            dlYear2MoMSoVoi.SelectedValue = "2011";
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            int time1 = int.Parse(dlMonth1.SelectedValue);
            int time2 = int.Parse(dlMonth2.SelectedValue);
            int year1 = int.Parse(dlYear1.SelectedValue);
            int year2 = int.Parse(dlYear2.SelectedValue);
            if (radType.SelectedValue.Equals("2"))
            {
                this.GenChart1(parentID, time1, time2, year1, year2, code);
            }
            else
            {
                this.GenChart(parentID, time1, time2, year1, year2, code);
            }
        }

        private void GenChart(int parentid, int time1, int time2, int year1, int year2, string code)
        {
            string srcImg = "";
            string phongto = "";
            string keyr = String.Format("Cafef.Chart.BinaryData.FDI.Thang.{0}.{1}.{2}.{3}.{4}.{5}", parentid, time1, time2, year1, year2, chkIsLabel.Checked);
            string keyrPhongTo = String.Format("Cafef.Chart.BinaryData.FDI.Thang.Phongto.{0}.{1}.{2}.{3}.{4}.{5}", parentid, time1, time2, year1, year2, chkIsLabel.Checked);
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
                DataTable dt = IndexBO.IndexCacheSql.pr_Chart_MonthCompare(parentid, time1, time2, year1, year2, code);
                DataTable dtTemp = dt;
                if (dt.Rows.Count > 0)
                {
                    DataTable dtDistinct = dt.DefaultView.ToTable(true, new string[] { "MTime" });
                    for (int i = 0; i < dtDistinct.Rows.Count; i++)
                    {
                        chxl += "Tháng " + dtDistinct.Rows[i]["MTime"].ToString() + "|";
                        int itemp = i + 1;
                        chxp += itemp.ToString() + ",";
                    }
                }

                string[] chxlArr = chxl.TrimEnd('|').Split('|');
                int countF = chxlArr.Length;
                string filter = "";

                char[] c = { 'O', 'R' };
                dt.DefaultView.RowFilter = filter.TrimStart(c);
                string[] colors = { "4f81be", "c1504d" };
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double ind = double.Parse(dt.Rows[i]["MIndex"].ToString());
                    chd += ind.ToString() + ",";

                    if ((i + 1) % countF == 0)
                    {
                        chd = chd.TrimEnd(',') + "|";
                    }
                }

                chdl += dlYear1.SelectedValue + "|" + dlYear2.SelectedValue + "|";
                chco += colors[0] + "," + colors[1] + ",";
                chm += "N,FF0000," + 0 + ",-1,10|" + "N,FF0000," + 1 + ",-1,10|";

                srcImg = "http://chart.apis.google.com/chart";
                srcImg += "?chf=c,s,FFFFFF";
                srcImg += "&chxl=1:|" + chxl;// +"2:|-3%|-2%|-1%|0%|1%|2%|3%|4%|5%";
                srcImg += "&chxp=" + chxp.TrimEnd(',');// +"|2,0,1,2,3,4,5,6,7,8";
                srcImg += "&chxr=0,0,1000|1,1," + countF;// +"|2,0,8";
                srcImg += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
                srcImg += "&chxt=y,x";
                srcImg += "&chbh=a,1,40";
                srcImg += "&chs=600x300";
                srcImg += "&cht=bvg";
                srcImg += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chds=0,1000,0,1000";
                srcImg += "&chd=t:" + chd.TrimEnd('|');
                srcImg += "&chdl=" + chdl.TrimEnd('|');
                srcImg += "&chdlp=b";
                srcImg += "&chg=0,9.95,1,1";
                if (chkIsLabel.Checked)
                {
                    srcImg += "&chm=" + chm.TrimEnd('|');
                }
                srcImg += "&chtt=Biểu+đồ+FDI";
                Common.GetImageFromRedis(keyr, srcImg);

                phongto = "http://chart.apis.google.com/chart";
                phongto += "?chf=c,s,FFFFFF";
                phongto += "&chxl=1:|" + chxl;// +"2:|-3%|-2%|-1%|0%|1%|2%|3%|4%|5%";
                phongto += "&chxp=" + chxp.TrimEnd(',');// +"|2,0,1,2,3,4,5,6,7,8";
                phongto += "&chxr=0,0,1000|1,1," + countF;// +"|2,0,8";
                phongto += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
                phongto += "&chxt=y,x";
                phongto += "&chbh=a,1,40";
                phongto += "&chs=890x320";
                phongto += "&cht=bvg";
                phongto += "&chco=" + chco.TrimEnd(',');
                phongto += "&chds=0,1000,0,1000";
                phongto += "&chd=t:" + chd.TrimEnd('|');
                phongto += "&chdl=" + chdl.TrimEnd('|');
                phongto += "&chdlp=b";
                phongto += "&chg=0,9.95,1,1";
                if (chkIsLabel.Checked)
                {
                    phongto += "&chm=" + chm.TrimEnd('|');
                }
                phongto += "&chtt=Biểu+đồ+FDI";
                Common.GetImageFromRedis(keyrPhongTo, phongto);
            }
            ltrChart.Text = "<img alt=\"\" src='" + srcImg + "' />";
            hdfChart.Value = phongto;
        }

        private void GenChart1(int parentid, int time1, int time2, int year1, int year2, string code)
        {
            code = "152000";
            string chxl = "";
            string chd = "";
            string chdl = "";
            string chco = "";
            string chxp = "1,";
            string chm = "";
            DataTable dt = IndexBO.IndexCacheSql.pr_Chart_MonthCompare(parentid, time1, time2, year1, year2, code);
            DataTable dtTemp = dt;
            if (dt.Rows.Count > 0)
            {
                DataTable dtDistinct = dt.DefaultView.ToTable(true, new string[] { "MTime" });
                for (int i = 0; i < dtDistinct.Rows.Count; i++)
                {
                    chxl += "Tháng " + dtDistinct.Rows[i]["MTime"].ToString() + "|";
                    int itemp = i + 1;
                    chxp += itemp.ToString() + ",";
                }
            }

            string[] chxlArr = chxl.TrimEnd('|').Split('|');
            int countF = chxlArr.Length;
            string filter = "";

            char[] c = { 'O', 'R' };
            dt.DefaultView.RowFilter = filter.TrimStart(c);
            string[] colors = { "4f81be", "c1504d" };
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double ind = double.Parse(dt.Rows[i]["MIndex"].ToString());
                chd += ind.ToString() + ",";

                if ((i + 1) % countF == 0)
                {
                    chd = chd.TrimEnd(',') + "|";
                }
            }            

            chdl += dlYear1.SelectedValue + "|" + dlYear2.SelectedValue + "|";
            chco += colors[0] + "," + colors[1] + ",";
            chm += "N,FF0000," + 0 + ",-1,10|" + "N,FF0000," + 1 + ",-1,10|";

            string srcImg = "http://chart.apis.google.com/chart";
            srcImg += "?chf=c,s,FFFFFF";
            srcImg += "&chxl=1:|" + chxl;// +"2:|-3%|-2%|-1%|0%|1%|2%|3%|4%|5%";
            srcImg += "&chxp=" + chxp.TrimEnd(',');// +"|2,0,1,2,3,4,5,6,7,8";
            srcImg += "&chxr=0,0,18000|1,1," + countF;// +"|2,0,8";
            srcImg += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
            srcImg += "&chxt=y,x";
            srcImg += "&chbh=a,1,40";
            srcImg += "&chs=600x300";
            srcImg += "&cht=bvg";
            srcImg += "&chco=" + chco.TrimEnd(',');
            srcImg += "&chds=0,18000,0,18000";
            srcImg += "&chd=t:" + chd.TrimEnd('|');
            srcImg += "&chdl=" + chdl.TrimEnd('|');
            srcImg += "&chdlp=b";
            srcImg += "&chg=0,12.45,1,1";
            if (chkIsLabel.Checked)
            {
                srcImg += "&chm=" + chm.TrimEnd('|');
            }
            srcImg += "&chtt=Biểu+đồ+FDI";
            ltrChart.Text = "<img alt=\"\" src='" + srcImg + "' />";

            string phongto = "http://chart.apis.google.com/chart";
            phongto += "?chf=c,s,FFFFFF";
            phongto += "&chxl=1:|" + chxl;// +"2:|-3%|-2%|-1%|0%|1%|2%|3%|4%|5%";
            phongto += "&chxp=" + chxp.TrimEnd(',');// +"|2,0,1,2,3,4,5,6,7,8";
            phongto += "&chxr=0,0,18000|1,1," + countF;// +"|2,0,8";
            phongto += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
            phongto += "&chxt=y,x";
            phongto += "&chbh=a,1,40";
            phongto += "&chs=890x320";
            phongto += "&cht=bvg";
            phongto += "&chco=" + chco.TrimEnd(',');
            phongto += "&chds=0,18000,0,18000";
            phongto += "&chd=t:" + chd.TrimEnd('|');
            phongto += "&chdl=" + chdl.TrimEnd('|');
            phongto += "&chdlp=b";
            phongto += "&chg=0,12.45,1,1";
            if (chkIsLabel.Checked)
            {
                phongto += "&chm=" + chm.TrimEnd('|');
            }
            phongto += "&chtt=Biểu+đồ+FDI";
            hdfChart.Value = phongto;
        }

        protected void radType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int time1 = int.Parse(dlMonth1.SelectedValue);
            int time2 = int.Parse(dlMonth2.SelectedValue);
            int year1 = int.Parse(dlYear1.SelectedValue);
            int year2 = int.Parse(dlYear2.SelectedValue);
            if (radType.SelectedValue.Equals("2"))
            {
                this.GenChart1(parentID, time1, time2, year1, year2, code);
            }
            else
            {
                this.GenChart(parentID, time1, time2, year1, year2, code);
            }
        }

        protected void btnViewDuAn_Click(object sender, EventArgs e)
        {
            int time1 = int.Parse(dlMonth1DuAn.SelectedValue);
            int time2 = 0;
            int time3 = 0;
            int time4 = int.Parse(dlMonth2DuAn.SelectedValue);
            int year1 = int.Parse(dlYear1DuAn.SelectedValue);
            int year2 = 0;
            int year3 = int.Parse(dlYear2DuAn.SelectedValue);
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
            this.GenChartDuAn(parentID, time1, time2, time3, time4, year1, year2, year3);
            //Response.Redirect("/dulieuvimo/san-luong-cong-nghiep.chn#giatrithuc");
            ltrScript.Text = "<script type=\"text/javascript\">window.location.href = '/dulieuvimo/fdi.chn#duan';</script>";
        }

        private void GenChartDuAn(int parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
        {
            string srcImg = "";
            string phongto = "";
            string keyr = String.Format("Cafef.Chart.BinaryData.FDI.DuAn.{0}.{1}.{2}.{3}.{4}.{5}.{6}", parentid, time1, time2, year1, year2, year3, chkIsLabel.Checked);
            string keyrPhongTo = String.Format("Cafef.Chart.BinaryData.FDI.DuAn.Phongto.{0}.{1}.{2}.{3}.{4}.{5}.{6}", parentid, time1, time2, year1, year2, year3, chkIsLabel.Checked);
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

                if (countF > 20)
                {
                    string chxlTemp = "";
                    string chxpTemp = "1,";
                    int itemp = 0;
                    for (int k = 0; k < countF; k = k + 2)
                    {
                        chxlTemp += chxlArr[k] + "|";
                        itemp = k + 1;
                        chxpTemp += itemp.ToString() + ",";
                    }
                    chxp = chxpTemp;
                    chxl = chxlTemp;
                }

                string filter = "";
                //for (int fil = 0; fil < chkChitieu.Items.Count; fil++)
                //{
                //    if (chkChitieu.Items[fil].Selected)
                //    {
                //        filter += "OR CCode=" + chkChitieu.Items[fil].Value;
                //    }
                //}
                filter += "OR CCode=" + duan;
                char[] c = { 'O', 'R' };
                dt.DefaultView.RowFilter = filter.TrimStart(c);
                string[] colors = { "4f81be", "c1504d", "9cbc59", "8064a3" };
                for (int i = 0; i < dt.DefaultView.Count; i++)
                {
                    //double ind = (double.Parse(dt.DefaultView[i]["MIndex"].ToString()) / 1000);
                    chd += dt.DefaultView[i]["MIndex"].ToString() + ",";
                    //chd += EncodeDataExtended(double.Parse(dt.DefaultView[i]["MIndex"].ToString()));//ind.ToString() + ",";
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
                srcImg += "&chxl=1:|" + chxl;// +"2:|-1%|0%|1%|2%|3%|4%|5%|6%";
                srcImg += "&chxp=" + chxp.TrimEnd(',');// +"|2,0,1,2,3,4,5,6,7";
                srcImg += "&chxr=0,0,1000|1,1," + countF;// +"|2,0,7";
                srcImg += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
                srcImg += "&chxt=y,x";
                srcImg += "&chbh=a";
                srcImg += "&chs=600x300";
                srcImg += "&cht=bvg";
                srcImg += "&chco=" + chco.TrimEnd(',');//A2C180,3D7930,FF9900,B5771A";
                srcImg += "&chds=0,1000,0,1000,0,1000,0,1000";
                srcImg += "&chd=t:" + chd.TrimEnd('|').TrimEnd(',');
                srcImg += "&chdl=" + chdl.TrimEnd('|');
                srcImg += "&chdlp=b";
                srcImg += "&chg=0,11,1,1";
                if (chkIsLabelDuAn.Checked)
                {
                    srcImg += "&chm=" + chm.TrimEnd('|');
                }
                srcImg += "&chtt=Số+dự+án+đăng+ký+mới(lũy+kế)";
                Common.GetImageFromRedis(keyr, srcImg);

                phongto = "http://chart.apis.google.com/chart";
                phongto += "?chf=c,s,FFFFFF";
                phongto += "&chxl=1:|" + chxl;// +"2:|-1%|0%|1%|2%|3%|4%|5%|6%";
                phongto += "&chxp=" + chxp.TrimEnd(',');// +"|2,0,1,2,3,4,5,6,7";
                phongto += "&chxr=0,0,1000|1,1," + countF;// +"|2,0,7";
                phongto += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
                phongto += "&chxt=y,x";
                phongto += "&chbh=a";
                phongto += "&chs=890x320";
                phongto += "&cht=bvg";
                phongto += "&chco=" + chco.TrimEnd(',');//A2C180,3D7930,FF9900,B5771A";
                phongto += "&chds=0,1000,0,1000,0,1000,0,1000";
                phongto += "&chd=t:" + chd.TrimEnd('|');
                phongto += "&chdl=" + chdl.TrimEnd('|');
                phongto += "&chdlp=b";
                phongto += "&chg=0,11,1,1";
                if (chkIsLabelDuAn.Checked)
                {
                    phongto += "&chm=" + chm.TrimEnd('|');
                }
                phongto += "&chtt=Số+dự+án+đăng+ký+mới(lũy+kế)";
                Common.GetImageFromRedis(keyrPhongTo, phongto);

                //byte[] buffer = Encoding.UTF8.GetBytes(srcImg);

                /*Updated 15/06/2011*/
                //save to cache in 1 minute
                //Param: parentid, time1, time2, time3, time4, year1, year2, year3, code, chkIsLabel.Checked
                //string cacheName = String.Format("CafeF.DLVM.Chart.BinaryData.{0}.{1}.{2}.{3}.{4}.{5}.{6}", parentid.ToString(), time1.ToString(), time2.ToString(), year1.ToString(), year2.ToString(), code, chkIsLabel.Checked.ToString());
                //byte[] buffer = Encoding.UTF8.GetBytes(srcImg);
                //CacheUtils.AddChartDataToDistributedCache(cacheName, buffer, 3600);
            }
            ltrChartDuAn.Text = "<img alt=\"\" src=" + srcImg + " />";
            hdfChartDuAn.Value = phongto;
        }

        protected void btnViewSoVon_Click(object sender, EventArgs e)
        {
            int time1 = int.Parse(dlMonth1SoVon.SelectedValue);
            int time2 = 0;
            int time3 = 0;
            int time4 = int.Parse(dlMonth2SoVon.SelectedValue);
            int year1 = int.Parse(dlYear1SoVon.SelectedValue);
            int year2 = 0;
            int year3 = int.Parse(dlYear2SoVon.SelectedValue);
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
            this.GenChartSoVon(parentID, time1, time2, time3, time4, year1, year2, year3);
            //Response.Redirect("/dulieuvimo/san-luong-cong-nghiep.chn#giatrithuc");
            ltrScript.Text = "<script type=\"text/javascript\">window.location.href = '/dulieuvimo/fdi.chn#sovon';</script>";
        }

        private void GenChartSoVon(int parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
        {
            string srcImg = "";
            string phongto = "";
            string keyr = String.Format("Cafef.Chart.BinaryData.FDI.SoVon.{0}.{1}.{2}.{3}.{4}.{5}.{6}", parentid, time1, time2, year1, year2, year3, chkIsLabel.Checked);
            string keyrPhongTo = String.Format("Cafef.Chart.BinaryData.FDI.SoVon.Phongto.{0}.{1}.{2}.{3}.{4}.{5}.{6}", parentid, time1, time2, year1, year2, year3, chkIsLabel.Checked);
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

                if (countF > 20)
                {
                    string chxlTemp = "";
                    string chxpTemp = "1,";
                    int itemp = 0;
                    for (int k = 0; k < countF; k = k + 2)
                    {
                        chxlTemp += chxlArr[k] + "|";
                        itemp = k + 1;
                        chxpTemp += itemp.ToString() + ",";
                    }
                    chxp = chxpTemp;
                    chxl = chxlTemp;
                }

                string filter = "";
                //for (int fil = 0; fil < chkChitieu.Items.Count; fil++)
                //{
                //    if (chkChitieu.Items[fil].Selected)
                //    {
                //        filter += "OR CCode=" + chkChitieu.Items[fil].Value;
                //    }
                //}
                filter += "OR CCode=" + sovon;
                char[] c = { 'O', 'R' };
                dt.DefaultView.RowFilter = filter.TrimStart(c);
                string[] colors = { "4f81be", "c1504d", "9cbc59", "8064a3" };
                for (int i = 0; i < dt.DefaultView.Count; i++)
                {
                    //double ind = (double.Parse(dt.DefaultView[i]["MIndex"].ToString()) / 1000);
                    chd += dt.DefaultView[i]["MIndex"].ToString() + ",";

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
                srcImg += "&chxl=1:|" + chxl;// +"2:|-1%|0%|1%|2%|3%|4%|5%|6%";
                srcImg += "&chxp=" + chxp.TrimEnd(',');// +"|2,0,1,2,3,4,5,6,7";
                srcImg += "&chxr=0,0,18000|1,1," + countF;// +"|2,0,7";
                srcImg += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
                srcImg += "&chxt=y,x";
                srcImg += "&chbh=a";
                srcImg += "&chs=600x300";
                srcImg += "&cht=bvg";
                srcImg += "&chco=" + chco.TrimEnd(',');//A2C180,3D7930,FF9900,B5771A";
                srcImg += "&chds=0,18000,0,18000,0,18000,0,18000";
                srcImg += "&chd=t:" + chd.TrimEnd('|');
                srcImg += "&chdl=" + chdl.TrimEnd('|');
                srcImg += "&chdlp=b";
                srcImg += "&chg=0,11,1,1";
                if (chkIsLabelSoVon.Checked)
                {
                    srcImg += "&chm=" + chm.TrimEnd('|');
                }
                srcImg += "&chtt=Số+vốn+đăng+ký+mới";
                Common.GetImageFromRedis(keyr, srcImg);

                phongto = "http://chart.apis.google.com/chart";
                phongto += "?chf=c,s,FFFFFF";
                phongto += "&chxl=1:|" + chxl;// +"2:|-1%|0%|1%|2%|3%|4%|5%|6%";
                phongto += "&chxp=" + chxp.TrimEnd(',');// +"|2,0,1,2,3,4,5,6,7";
                phongto += "&chxr=0,0,18000|1,1," + countF;// +"|2,0,7";
                phongto += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
                phongto += "&chxt=y,x";
                phongto += "&chbh=a";
                phongto += "&chs=890x320";
                phongto += "&cht=bvg";
                phongto += "&chco=" + chco.TrimEnd(',');//A2C180,3D7930,FF9900,B5771A";
                phongto += "&chds=0,18000,0,18000,0,18000,0,18000";
                phongto += "&chd=t:" + chd.TrimEnd('|').TrimEnd(',');
                phongto += "&chdl=" + chdl.TrimEnd('|');
                phongto += "&chdlp=b";
                phongto += "&chg=0,11,1,1";
                if (chkIsLabelSoVon.Checked)
                {
                    phongto += "&chm=" + chm.TrimEnd('|');
                }
                phongto += "&chtt=Số+vốn+đăng+ký+mới";
                Common.GetImageFromRedis(keyrPhongTo, phongto);
            }
            ltrChartSoVon.Text = "<img alt=\"\" src=" + srcImg + " />";
            hdfChartSoVon.Value = phongto;
        }

        protected void btnViewGiaiNgan_Click(object sender, EventArgs e)
        {
            int time1 = int.Parse(dlMonth1GiaiNgan.SelectedValue);
            int time2 = 0;
            int time3 = 0;
            int time4 = int.Parse(dlMonth2GiaiNgan.SelectedValue);
            int year1 = int.Parse(dlYear1GiaiNgan.SelectedValue);
            int year2 = 0;
            int year3 = int.Parse(dlYear2GiaiNgan.SelectedValue);
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
            this.GenChartGiaiNgan(parentID, time1, time2, time3, time4, year1, year2, year3);
            //Response.Redirect("/dulieuvimo/san-luong-cong-nghiep.chn#giatrithuc");
            ltrScript.Text = "<script type=\"text/javascript\">window.location.href = '/dulieuvimo/fdi.chn#giaingan';</script>";
        }

        private void GenChartGiaiNgan(int parentid, int time1, int time2, int time3, int time4, int year1, int year2, int year3)
        {
            string srcImg = "";
            string phongto = "";
            string keyr = String.Format("Cafef.Chart.BinaryData.FDI.GiaiNgan.{0}.{1}.{2}.{3}.{4}.{5}.{6}", parentid, time1, time2, year1, year2, year3, chkIsLabel.Checked);
            string keyrPhongTo = String.Format("Cafef.Chart.BinaryData.FDI.GiaiNgan.Phongto.{0}.{1}.{2}.{3}.{4}.{5}.{6}", parentid, time1, time2, year1, year2, year3, chkIsLabel.Checked);
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

                if (countF > 20)
                {
                    string chxlTemp = "";
                    string chxpTemp = "1,";
                    int itemp = 0;
                    for (int k = 0; k < countF; k = k + 2)
                    {
                        chxlTemp += chxlArr[k] + "|";
                        itemp = k + 1;
                        chxpTemp += itemp.ToString() + ",";
                    }
                    chxp = chxpTemp;
                    chxl = chxlTemp;
                }

                string filter = "";
                //for (int fil = 0; fil < chkChitieu.Items.Count; fil++)
                //{
                //    if (chkChitieu.Items[fil].Selected)
                //    {
                //        filter += "OR CCode=" + chkChitieu.Items[fil].Value;
                //    }
                //}
                filter += "OR CCode=" + giaingan;
                char[] c = { 'O', 'R' };
                dt.DefaultView.RowFilter = filter.TrimStart(c);
                string[] colors = { "4f81be", "c1504d", "9cbc59", "8064a3" };
                for (int i = 0; i < dt.DefaultView.Count; i++)
                {
                    //double ind = (double.Parse(dt.DefaultView[i]["MIndex"].ToString()) / 1000);
                    chd += dt.DefaultView[i]["MIndex"].ToString() + ",";

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
                srcImg += "&chxl=1:|" + chxl;// +"2:|-1%|0%|1%|2%|3%|4%|5%|6%";
                srcImg += "&chxp=" + chxp.TrimEnd(',');// +"|2,0,1,2,3,4,5,6,7";
                srcImg += "&chxr=0,0,12000|1,1," + countF;// +"|2,0,7";
                srcImg += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
                srcImg += "&chxt=y,x";
                srcImg += "&chbh=a";
                srcImg += "&chs=600x300";
                srcImg += "&cht=bvg";
                srcImg += "&chco=" + chco.TrimEnd(',');//A2C180,3D7930,FF9900,B5771A";
                srcImg += "&chds=0,12000,0,12000,0,12000,0,12000";
                srcImg += "&chd=t:" + chd.TrimEnd('|');
                srcImg += "&chdl=" + chdl.TrimEnd('|');
                srcImg += "&chdlp=b";
                srcImg += "&chg=0,11,1,1";
                if (chkIsLabelGiaiNgan.Checked)
                {
                    srcImg += "&chm=" + chm.TrimEnd('|');
                }
                srcImg += "&chtt=Vốn+fdi+giải+ngân";
                Common.GetImageFromRedis(keyr, srcImg);

                phongto = "http://chart.apis.google.com/chart";
                phongto += "?chf=c,s,FFFFFF";
                phongto += "&chxl=1:|" + chxl;// +"2:|-1%|0%|1%|2%|3%|4%|5%|6%";
                phongto += "&chxp=" + chxp.TrimEnd(',');// +"|2,0,1,2,3,4,5,6,7";
                phongto += "&chxr=0,0,12000|1,1," + countF;// +"|2,0,7";
                phongto += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
                phongto += "&chxt=y,x";
                phongto += "&chbh=a";
                phongto += "&chs=890x320";
                phongto += "&cht=bvg";
                phongto += "&chco=" + chco.TrimEnd(',');//A2C180,3D7930,FF9900,B5771A";
                phongto += "&chds=0,12000,0,12000,0,12000,0,12000";
                phongto += "&chd=t:" + chd.TrimEnd('|');
                phongto += "&chdl=" + chdl.TrimEnd('|');
                phongto += "&chdlp=b";
                phongto += "&chg=0,11,1,1";
                if (chkIsLabelGiaiNgan.Checked)
                {
                    phongto += "&chm=" + chm.TrimEnd('|');
                }
                phongto += "&chtt=Vốn+fdi+giải+ngân";
                Common.GetImageFromRedis(keyrPhongTo, phongto);
            }
            ltrChartGiaiNgan.Text = "<img alt=\"\" src=" + srcImg + " />";
            hdfChartGiaiNgan.Value = phongto;
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
            ltrScript.Text = "<script type=\"text/javascript\">window.location.href = '/dulieuvimo/fdi.chn#phantramsovoi';</script>";
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
            string keyr = String.Format("Cafef.Chart.BinaryData.FDI.MoM.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, chkIsLabel.Checked);
            string keyrPhongTo = String.Format("Cafef.Chart.BinaryData.FDI.MoM.Phongto.{0}.{1}.{2}.{3}.{4}.{5}.{6}.{7}.{8}.{9}", parentid, time1, time2, time3, time4, year1, year2, year3, ft, chkIsLabel.Checked);
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
                    if (unind != 0)
                    {
                        ind = (bnind / unind - 1) * 100;
                        chd += String.Format("{0:f}", ind) + ",";
                    }

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

                srcImg = "http://chart.apis.google.com/chart";
                srcImg += "?chf=c,s,FFFFFF";
                srcImg += "&chxl=0:|" + chxl + "1:|-60|-40|-20|0|20|40|60|80";
                srcImg += "&chxp=" + chxp.TrimEnd(',') + "|1,-60,-40,-20,0,20,40,60,80";
                srcImg += "&chxr=0,0," + countF + "|1,-60,80";
                srcImg += "&chxt=x,y";
                srcImg += "&chs=600x300";
                srcImg += "&cht=lc";
                //srcImg += "&chco=" + chco.TrimEnd(',');
                srcImg += "&chds=-60,80,-60,80,-60,80,-60,80";
                srcImg += "&chd=t:" + chd.TrimEnd('|').TrimEnd(',');
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
                srcImg += "&chtt=Tăng+trưởng+FDI";
                Common.GetImageFromRedis(keyr, srcImg);

                phongto = "http://chart.apis.google.com/chart";
                phongto += "?chf=c,s,FFFFFF";
                phongto += "&chxl=0:|" + chxl + "1:|-60|-40|-20|0|20|40|60|80";
                phongto += "&chxp=" + chxp.TrimEnd(',') + "|1,-60,-40,-20,0,20,40,60,80";
                phongto += "&chxr=0,0," + countF + "|1,-60,80";
                phongto += "&chxt=x,y";
                phongto += "&chs=890x320";
                phongto += "&cht=lc";
                //phongto += "&chco=" + chco.TrimEnd(',');
                phongto += "&chds=-60,80,-60,80,-60,80,-60,80";
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
                srcImg += "&chtt=Tăng+trưởng+FDI";
                Common.GetImageFromRedis(keyrPhongTo, phongto);
            }
            ltrChartMoMSoVoi.Text = "<img alt=\"\" src=" + srcImg + " />";
            hdfChartMoMSoVoi.Value = phongto;
        }

        private bool GetImageFromCache(int parentid, int time1, int time2, int year1, int year2, string code, bool isLabel)
        {
            try
            {
                if ((ConfigurationManager.AppSettings["AllowImageCache"] ?? "true") != "true") return false;
                string cacheName = String.Format("CafeF.DLVM.Chart.BinaryData.{0}.{1}.{2}.{3}.{4}.{5}.{6}", parentid.ToString(), time1.ToString(), time2.ToString(), year1.ToString(), year2.ToString(), code, chkIsLabel.Checked.ToString());
                Byte[] bf = CacheUtils.GetChartDataFromDistributedCache<Byte[]>(cacheName);
                if (bf == null || bf.Length == 0) return false;
                Response.Clear();

                //File.WriteAllBytes(dirPath + filePath, bf);
                Response.OutputStream.Write(bf, 0, bf.Length);
                Response.ContentType = "image/png";
                return true;

            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        private string EncodeDataExtended(double val)
        {
            string EXTENDED_MAP = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-.";
            int EXTENDED_MAP_LENGTH = EXTENDED_MAP.Length;            
            string chartData = "";
            int maxVal = 100;            
            // In case the array vals were translated to strings.
            double numericVal = ((val - 40) / (969 - 40) * 100);
            // Scale the value to maxVal.
            int scaledVal = (int)Math.Floor((EXTENDED_MAP_LENGTH * EXTENDED_MAP_LENGTH * numericVal) / maxVal);

            if (scaledVal > (EXTENDED_MAP_LENGTH * EXTENDED_MAP_LENGTH) - 1)
            {
                chartData += "..";
            }
            else if (scaledVal <= 0)
            {
                chartData += "__";
            }
            else
            {
                // Calculate first and second digits and add them to the output.
                int quotient = (int)Math.Floor((float)scaledVal / EXTENDED_MAP_LENGTH);
                int remainder = scaledVal - EXTENDED_MAP_LENGTH * quotient;
                chartData += EXTENDED_MAP.Substring(quotient, 1) + EXTENDED_MAP.Substring(remainder, 1);
            }            
            return chartData;
        }
    }
}