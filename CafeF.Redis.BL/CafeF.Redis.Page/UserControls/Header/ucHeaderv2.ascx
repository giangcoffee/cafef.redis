<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucHeaderv2.ascx.cs"
    Inherits="CafeF.UserControls.Header.ucHeaderv2" %>
<%@ Register Src="ucMenuv2.ascx" TagName="ucMenuv2" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/Header/ucChart.ascx" TagName="ucChart" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/Header/ucHeaderIndex.ascx" TagName="ucHeaderIndex" TagPrefix="uc2" %>
<div id="top">
    <ul id="nav">
        <li><a id="a_0" class="trangchu" href="/" style="margin-left: 10px">&nbsp;&nbsp;Trang chủ</a></li>
        <li><a id="a_1000" href="/du-lieu.chn">Dữ liệu</a></li>
        <li><a id="a_1001" href="/danh-muc-dau-tu.chn">Danh mục đầu tư</a></li>
        <li><a href="#" style="color: Black; font-family: Times New Roman; font-size: 14px; background:url(http://cafef3.vcmedia.vn/v2/images/v2/icoTinmoi.gif) no-repeat scroll 37px 32px transparent">
            Tin mới</a></li>
    </ul>
    <div class="textTinmoiNP">
        <a href="#" onclick="LoadTinMoiPre();" class="per">&nbsp;</a> <a href="#" onclick="LoadTinMoiNext();"
            class="next">&nbsp;</a>
    </div>
    <div class="divgachmenu" style="left: 625px">
        &nbsp;
    </div>
    <div id="divDocNhanh">
        <h2>
            <a href="/doc-nhanh.chn">Đọc nhanh</a></h2>
    </div>
    <div id="divLogin">
        <ul id="navright">
            <li><a href="/help/cafef-mobile.htm">CafeF Mobile&nbsp;|</a></li><li><a href="/Danh-muc-dau-tu/Dang-nhap.chn">
                &nbsp;Đăng nhập&nbsp;</a></li><li><a href="/Danh-muc-dau-tu/Dang-ky.chn">| &nbsp;Đăng
                    ký</a></li></ul>
    </div>
    <div class="divgachmenu" style="right: 0px">
        &nbsp;
    </div>
</div>
<div class="pwbg">
    <div id="header" class="clearfix">
        <div id="logo">
            <div class="time" id="datetime">
            </div>
            <h1>
                <a href="/">CafeF</a></h1>
                <uc2:ucHeaderIndex runat="server" />
        </div>        
        <div id="divHeader">
            <div class="divgachmenucao">&nbsp;</div>
            <div id="divTinMoi" class="tinmoi">
            </div>
            <div class="bieudo clearfix">
                <uc2:ucChart ID="ucChart" runat="server" />
            </div>
        </div>
        <uc1:ucMenuv2 ID="ucMenuv21" runat="server" />
    </div>
</div>
<input id="hdPageIndex" type="hidden" value="1" />

<script type="text/javascript">
    LoadOverallHeader();      
    $(document).ready(function() {
        LoadUser('cafef.user', 'name');
    });
    var currentTime = parseFloat('<%= DateTime.Now.ToString("HHmmss") %>');
    function ReloadChart() {
        currentTime += 120;
        if ((currentTime % 100) >= 60) { currentTime += 40; }
        if ((currentTime % 10000) >= 6000) { currentTime += 4000; }
        if (currentTime >= 240000) { currentTime -= 240000; }
        if (currentTime < 81500 || currentTime > 111500) {
            if (currentTime < 111531) {
                var day = new Date();
                var src = '/chartindex/merge/cafefchart3.png?d=' + day.getFullYear() + (day.getMonth() + 1) + (day.getDate());
                $('div#headerchart1,div#headerchart2').css('background-image', "url(" + src + ")");
                src = '/chartindex/merge/cafefchart.dulieu.gif?d=' + day.getFullYear() + (day.getMonth() + 1) + (day.getDate());
                $('div#imgHoChart_Day,div#imgHaChart_Day').css('background-image', "url(" + src + ")");
            }
            return;
        }       
        var src2 = '/chart4.aspx' + "?ran=" + Math.floor(Math.random() * 1000000);
        $('div#headerchart1,div#headerchart2').css('background-image', "url(" + src2 + ")");
        src2 = '/chartdulieu.aspx' + "?ran=" + Math.floor(Math.random() * 1000000);
        $('div#imgHoChart_Day,div#imgHaChart_Day').css('background-image', "url(" + src2 + ")");
        $('img[src*="/Chart4.aspx"],img[src*="/Chart5.aspx"],img[src*="/Chart.aspx"]').each(function() {
            var src = $(this).attr('src');
            if (src.indexOf('&ran=') > 0) src = src.substring(0, src.indexOf('&ran='));
            src = src + "&ran=" + Math.floor(Math.random() * 1000000);
            $(this).attr('src', src);
        });
    }
    setInterval("ReloadChart()", 120000);
</script>

