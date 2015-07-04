<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BondHomepageBox.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.Bond.BondHomepageBox" %>
<table style="padding-left:5px;" width="100%" cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td style="width:20%;border-right:0px solid #E0E0E0;">
            <a style="color: #CC0001;" id="lnkVietnam" href="javascript:void(0)" onclick="ChangeCountry('1', 'lnkVietnam');">Việt Nam</a>
        </td>
        <td rowspan="8"><img src="/BondChart.aspx?country=1&type=1" alt="" border="0" width="260" id="imgChart" /></td>
    </tr>
    <tr>
        <td valign="top" style="border-right:0px solid #E0E0E0;"><a id="lnkMy" href="javascript:void(0)" onclick="ChangeCountry('2', 'lnkMy');">Mỹ</a></td>
    </tr>
    <tr>
        <td valign="top" style="border-right:0px solid #E0E0E0;"><a id="lnkAnh" href="javascript:void(0)" onclick="ChangeCountry('3', 'lnkAnh');">Anh</a></td>
    </tr>
    <tr>
        <td valign="top" style="border-right:0px solid #E0E0E0;"><a id="lnkTrungQuoc" href="javascript:void(0)" onclick="ChangeCountry('4', 'lnkTrungQuoc');">Trung Quốc</a></td>
    </tr>
    <tr>
        <td valign="top" style="border-right:0px solid #E0E0E0;"><a id="lnkNhat" href="javascript:void(0)" onclick="ChangeCountry('5', 'lnkNhat');">Nhật</a></td>
    </tr>
    <tr>
        <td valign="top" style="border-right:0px solid #E0E0E0;"><a id="lnkUc" href="javascript:void(0)" onclick="ChangeCountry('6', 'lnkUc');">Úc</a></td>
    </tr>
    <tr>
        <td valign="top" style="border-right:0px solid #E0E0E0;"><a id="lnkHyLap" href="javascript:void(0)" onclick="ChangeCountry('7', 'lnkHyLap');">Hy Lạp</a></td>
    </tr>
    <tr>
        <td valign="top" style="border-right:0px solid #E0E0E0;"><a id="lnkDuc" href="javascript:void(0)" onclick="ChangeCountry('8', 'lnkDuc');">Đức</a></td>
    </tr>
    <tr>
        <td style="border-right:0px solid #E0E0E0;"></td>
        <td align="center" style="border-right:0px solid #E0E0E0;">
            <div style="float:left;">Kỳ hạn:</div>
            <div class="dltr-time">                
                <a style="color: #CC0001;" id="lnkChart_1nam" href="javascript:void(0)" onclick="ChangeImage('1', 'lnkChart_1nam');">1 năm</a> | 
                <a id="lnkChart_3nam" href="javascript:void(0)" onclick="ChangeImage('3', 'lnkChart_3nam');">3 năm</a> | 
                <a id="lnkChart_5nam" href="javascript:void(0)" onclick="ChangeImage('5', 'lnkChart_5nam');">5 năm</a> | 
                <a id="lnkChart_10nam" href="javascript:void(0)" onclick="ChangeImage('10', 'lnkChart_10nam');">10 năm</a>
                <input type="hidden" id="hfCountry" value="1" />
            </div>
        </td>
    </tr>
</table>
<script type="text/javascript">
function SetFirst()
{
    var url = "/BondChart.aspx?country=1&type=1";
    document.getElementById('imgChart').setAttribute('src',url);    
}
SetFirst();
function ChangeImage(type, lnk)
{
    var cou = document.getElementById('hfCountry').value;
    document.getElementById('imgChart').setAttribute('src','/BondChart.aspx?country=' + cou + '&type=' + type);
    document.getElementById('lnkChart_1nam').style.color = '#013567';
    document.getElementById('lnkChart_3nam').style.color = '#013567';
    document.getElementById('lnkChart_5nam').style.color = '#013567';
    document.getElementById('lnkChart_10nam').style.color = '#013567';
    document.getElementById(lnk).style.color = '#CC0001';
}
function ChangeCountry(country,lnk)
{
    document.getElementById('hfCountry').value = country;
    document.getElementById('lnkVietnam').style.color = '#013567';
    document.getElementById('lnkMy').style.color = '#013567';
    document.getElementById('lnkAnh').style.color = '#013567';
    document.getElementById('lnkTrungQuoc').style.color = '#013567';
    document.getElementById('lnkNhat').style.color = '#013567';
    document.getElementById('lnkUc').style.color = '#013567';
    document.getElementById('lnkHyLap').style.color = '#013567';
    document.getElementById('lnkDuc').style.color = '#013567';
    document.getElementById(lnk).style.color = '#CC0001';
    if(country==2||country==7)
    {
        document.getElementById('lnkChart_1nam').style.display = 'none';
        ChangeImage(3, 'lnkChart_1nam');
        //document.getElementById('imgChart').setAttribute('src','/BondChart.aspx?country=' + country + '&type=3');
    }
    else
    {
        document.getElementById('lnkChart_1nam').style.display = '';
        ChangeImage(1, 'lnkChart_1nam');
        //document.getElementById('imgChart').setAttribute('src','/BondChart.aspx?country=' + country + '&type=1');
    }
}
</script>