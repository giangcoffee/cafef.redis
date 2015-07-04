<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Popup.aspx.cs" Inherits="CafeF.Redis.Page.UserControl.DuLieuViMo.Popup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    function querySt(ji) 
    {
        hu = window.location.search.substring(1);
        gy = hu.split("&");
        for (i=0;i<gy.length;i++) {
        ft = gy[i].split("=");
        if (ft[0] == ji) {
        return ft[1];
            }
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <img src="" id="imgChart" alt="" />
        <script type="text/javascript">
            var al = '';
            var koko = querySt("type");
            if(koko == 'gdp1')
            {                
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChart1').value;
            }
            else if(koko == 'gdp2')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChart2').value;
            }
            else if(koko == 'gdpqoq')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChartQoQ').value;
            }
            else if(koko == 'gdpyoy')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChartYoY').value;
            }
            else if(koko == 'cpi1')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChart1').value;
            }
            else if(koko == 'cpi2')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChart2').value;
            }
            else if(koko == 'cpi3')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChart3').value;
            }
            else if(koko == 'gtslcn')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChart').value;
            }
            else if(koko == 'gtslcnmom')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChartMoM').value;
            }
            else if(koko == 'tmbl')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChart').value;
            }
            else if(koko == 'tmblmom')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChartMoM').value;
            }
            else if(koko == 'tmblmomsovoi')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChartMoMSoVoi').value;
            }
            else if(koko == 'xnk')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChart').value;
            }
            else if(koko == 'xnkmom')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChartMOM').value;
            }
            else if(koko == 'xnkmomsovoi')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChartMOMSoVoi').value;
            }
            else if(koko == 'fdi')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChartDuAn').value;
            }
            else if(koko == 'fdivon')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChartSoVon').value;
            }
            else if(koko == 'fdigiaingan')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChartGiaiNgan').value;
            }
            else if(koko == 'fdimomsovoi')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChartMoMSoVoi').value;
            }
            else if(koko == 'vdttnsnn')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChart').value;
            }
            else if(koko == 'gdptruocquy')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChart4').value;
            }
            else if(koko == 'gdptruocnam')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChart5').value;
            }
            else if(koko == 'tmblquy')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChartGT2').value;
            }
            else if(koko == 'tmblnam')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChartGT3').value;
            }
            else if(koko == 'tmblmomquy')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChartMoMTT2').value;
            }
            else if(koko == 'tmblmomnam')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChartMoMTT3').value;
            }
            else if(koko == 'gtslcnquy')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChartGT2').value;
            }
            else if(koko == 'gtslcnnam')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChartGT3').value;
            }
            else if(koko == 'gtslcnmomquy')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChartMoMTT2').value;
            }
            else if(koko == 'gtslcnmomnam')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChartMoMTT3').value;
            }
            else if(koko == 'cpi1quy')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChartGT2').value;
            }
            else if(koko == 'cpi1nam')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChartGT3').value;
            }
            else if(koko == 'cpi2quy')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChartMoMTT2').value;
            }
            else if(koko == 'cpi2nam')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChartMoMTT3').value;
            }
            else if(koko == 'xnkquy')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChartGT2').value;
            }
            else if(koko == 'xnknam')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChartGT3').value;
            }
            else if(koko == 'xnkmomquy')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChartMoMTT2').value;
            }
            else if(koko == 'xnkmomnam')
            {
                al = window.opener.document.getElementById('ctl00_ContentPlaceHolder1_ctl01_hdfChartMoMTT3').value;
            }
            document.getElementById('imgChart').src = al;
        </script>
    </div>
    </form>
</body>
</html>