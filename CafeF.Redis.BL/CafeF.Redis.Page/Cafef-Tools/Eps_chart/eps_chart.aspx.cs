using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace CafeF.GUI.CafeF_Tools
{
    public partial class eps_chart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                

                int width = (Object2Integer(Request.QueryString["width"]) == 0 ? 955 : Object2Integer(Request.QueryString["width"]));
                int height = (Object2Integer(Request.QueryString["height"]) == 0 ? 700 : Object2Integer(Request.QueryString["height"]));
                string symbol = (Request.QueryString["symbol"]??"").Trim().ToUpper();

                this.Title = symbol + " : Biểu đồ kỹ thuật | CafeF.vn";
                
                int tradeId = Object2Integer(Request.QueryString["tradeCenter"]);
                DateTime endDate = DateTime.Now;
                DateTime startDate = endDate.AddMonths(-6);

//                ltrChart.Text = @"<object id='mySwf' width='" + width.ToString() + @"' height='" + height.ToString() + @"' codebase='http://fpdownload.macromedia.com/get/flashplayer/current/swflash.cab' classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000'>
//                                    <param value='/CafeF-Tools/eps_chart/TechnicalChart.swf?stockSymbol=" + symbol + @"&stockExchangeID=" + tradeId.ToString() + @"&dateFrom=" + startDate.ToString("yyyy-MM-dd") + @"&dateTo=" + endDate.ToString("yyyy-MM-dd") + @"&wsdl=http://www.eps.com.vn/ws/chart.php?wsdl' name='src'/>
//                                    <embed width='" + width.ToString() + @"' height='" + height.ToString() + @"' pluginspage='http://www.adobe.com/go/getflashplayer' src='/CafeF-Tools/eps_chart/TechnicalChart.swf?stockSymbol=" + symbol + @"&stockExchangeID=" + tradeId.ToString() + @"&dateFrom=" + startDate.ToString("yyyy-MM-dd") + @"&dateTo=" + endDate.ToString("yyyy-MM-dd") + @"&wsdl=http://www.eps.com.vn/ws/chart.php?wsdl' type='application/x-shockwave-flash'/>
//                                </object>";
                ltrChart.Text = "<iframe id='iframeCafeF' src=\"https://www.vndirect.com.vn/vndsonline/online/brokerage/fchart/FlashChartWithoutLayout.do?symbol=" + symbol + "\" scrolling=\"no\" frameborder=\"no\" width='789px' height='760px'></iframe>";
            }
        }

        private int Object2Integer(object value)
        {
            if (value == null || value.ToString() == "")
            {
                return 0;
            }
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }
        }
    }
}
