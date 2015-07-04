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

namespace CafeF.Redis.Page.UserControl.DuLieuViMo
{
    public partial class BCNSNN : System.Web.UI.UserControl
    {
        private int parentID = 17;
        private string code = "160000";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.SetDefault();
                this.GenChart(parentID, 2, 5, 2010, 2011, code);
            }
        }

        private void SetDefault()
        {
            dlMonth1.SelectedValue = "2";
            dlMonth2.SelectedValue = "5";
            dlYear1.SelectedValue = "2010";
            dlYear2.SelectedValue = "2011";
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
            srcImg += "&chxr=0,0,30000|1,1," + countF;// +"|2,0,8";
            srcImg += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
            srcImg += "&chxt=y,x";
            srcImg += "&chbh=a,1,40";
            srcImg += "&chs=600x300";
            srcImg += "&cht=bvg";
            srcImg += "&chco=" + chco.TrimEnd(',');
            srcImg += "&chds=0,30000,0,30000";
            srcImg += "&chd=t:" + chd.TrimEnd('|');
            srcImg += "&chdl=" + chdl.TrimEnd('|');
            srcImg += "&chdlp=b";
            srcImg += "&chg=0,8.3,1,1";
            if (chkIsLabel.Checked)
            {
                srcImg += "&chm=" + chm.TrimEnd('|');
            }
            srcImg += "&chtt=Vốn+đầu+tư+từ+NSNN";
            ltrChart.Text = "<img alt=\"\" src='" + srcImg + "' />";

            string phongto = "http://chart.apis.google.com/chart";
            phongto += "?chf=c,s,FFFFFF";
            phongto += "&chxl=1:|" + chxl;// +"2:|-3%|-2%|-1%|0%|1%|2%|3%|4%|5%";
            phongto += "&chxp=" + chxp.TrimEnd(',');// +"|2,0,1,2,3,4,5,6,7,8";
            phongto += "&chxr=0,0,30000|1,1," + countF;// +"|2,0,8";
            phongto += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
            phongto += "&chxt=y,x";
            phongto += "&chbh=a,1,40";
            phongto += "&chs=890x320";
            phongto += "&cht=bvg";
            phongto += "&chco=" + chco.TrimEnd(',');
            phongto += "&chds=0,30000,0,30000";
            phongto += "&chd=t:" + chd.TrimEnd('|');
            phongto += "&chdl=" + chdl.TrimEnd('|');
            phongto += "&chdlp=b";
            phongto += "&chg=0,8.3,1,1";
            if (chkIsLabel.Checked)
            {
                phongto += "&chm=" + chm.TrimEnd('|');
            }
            phongto += "&chtt=Vốn+đầu+tư+từ+NSNN";
            hdfChart.Value = phongto;
        }

        private void GenChart1(int parentid, int time1, int time2, int year1, int year2, string code)
        {
            code = "161000";
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
            srcImg += "&chxr=0,0,10000|1,1," + countF;// +"|2,0,8";
            srcImg += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
            srcImg += "&chxt=y,x";
            srcImg += "&chbh=a,1,40";
            srcImg += "&chs=600x300";
            srcImg += "&cht=bvg";
            srcImg += "&chco=" + chco.TrimEnd(',');
            srcImg += "&chds=0,10000,0,10000";
            srcImg += "&chd=t:" + chd.TrimEnd('|');
            srcImg += "&chdl=" + chdl.TrimEnd('|');
            srcImg += "&chdlp=b";
            srcImg += "&chg=0,9.95,1,1";
            if (chkIsLabel.Checked)
            {
                srcImg += "&chm=" + chm.TrimEnd('|');
            }
            srcImg += "&chtt=Vốn+đầu+tư+từ+NSNN";
            ltrChart.Text = "<img alt=\"\" src='" + srcImg + "' />";

            string phongto = "http://chart.apis.google.com/chart";
            phongto += "?chf=c,s,FFFFFF";
            phongto += "&chxl=1:|" + chxl;// +"2:|-3%|-2%|-1%|0%|1%|2%|3%|4%|5%";
            phongto += "&chxp=" + chxp.TrimEnd(',');// +"|2,0,1,2,3,4,5,6,7,8";
            phongto += "&chxr=0,0,10000|1,1," + countF;// +"|2,0,8";
            phongto += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
            phongto += "&chxt=y,x";
            phongto += "&chbh=a,1,40";
            phongto += "&chs=890x320";
            phongto += "&cht=bvg";
            phongto += "&chco=" + chco.TrimEnd(',');
            phongto += "&chds=0,10000,0,10000";
            phongto += "&chd=t:" + chd.TrimEnd('|');
            phongto += "&chdl=" + chdl.TrimEnd('|');
            phongto += "&chdlp=b";
            phongto += "&chg=0,9.95,1,1";
            if (chkIsLabel.Checked)
            {
                phongto += "&chm=" + chm.TrimEnd('|');
            }
            phongto += "&chtt=Vốn+đầu+tư+từ+NSNN";
            hdfChart.Value = phongto;
        }

        private void GenChart2(int parentid, int time1, int time2, int year1, int year2, string code)
        {
            code = "162000";
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
            srcImg += "&chxr=0,0,20000|1,1," + countF;// +"|2,0,8";
            srcImg += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
            srcImg += "&chxt=y,x";
            srcImg += "&chbh=a,1,40";
            srcImg += "&chs=600x300";
            srcImg += "&cht=bvg";
            srcImg += "&chco=" + chco.TrimEnd(',');
            srcImg += "&chds=0,20000,0,20000";
            srcImg += "&chd=t:" + chd.TrimEnd('|');
            srcImg += "&chdl=" + chdl.TrimEnd('|');
            srcImg += "&chdlp=b";
            srcImg += "&chg=0,9.95,1,1";
            if (chkIsLabel.Checked)
            {
                srcImg += "&chm=" + chm.TrimEnd('|');
            }
            srcImg += "&chtt=Vốn+đầu+tư+từ+NSNN";
            ltrChart.Text = "<img alt=\"\" src='" + srcImg + "' />";

            string phongto = "http://chart.apis.google.com/chart";
            phongto += "?chf=c,s,FFFFFF";
            phongto += "&chxl=1:|" + chxl;// +"2:|-3%|-2%|-1%|0%|1%|2%|3%|4%|5%";
            phongto += "&chxp=" + chxp.TrimEnd(',');// +"|2,0,1,2,3,4,5,6,7,8";
            phongto += "&chxr=0,0,20000|1,1," + countF;// +"|2,0,8";
            phongto += "&chxs=0,676767,10.5,0,l,676767|1,676767,10.5,0,lt,676767";
            phongto += "&chxt=y,x";
            phongto += "&chbh=a,1,40";
            phongto += "&chs=890x320";
            phongto += "&cht=bvg";
            phongto += "&chco=" + chco.TrimEnd(',');
            phongto += "&chds=0,20000,0,20000";
            phongto += "&chd=t:" + chd.TrimEnd('|');
            phongto += "&chdl=" + chdl.TrimEnd('|');
            phongto += "&chdlp=b";
            phongto += "&chg=0,9.95,1,1";
            if (chkIsLabel.Checked)
            {
                phongto += "&chm=" + chm.TrimEnd('|');
            }
            phongto += "&chtt=Vốn+đầu+tư+từ+NSNN";
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
            else if (radType.SelectedValue.Equals("3"))
            {
                this.GenChart2(parentID, time1, time2, year1, year2, code);
            }
            else
            {
                this.GenChart(parentID, time1, time2, year1, year2, code);
            }
        }
    }
}