<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsContent.ascx.cs"
    Inherits="CafeF.Redis.Page.UserControl.LichSuKien.NewsContent" %>
<%@ Register Src="../StockView/TradeInfoHeader.ascx" TagName="TradeInfoHeader" TagPrefix="uc1" %>
<%@ Register Src="OtherCrawlerNews.ascx" TagName="OtherCrawlerNews" TagPrefix="uc2" %>

<script>
    function openpreview(sUrl,w,h)
    {
        var winX = 0;
	    var winY = 0;
	    if (parseInt(navigator.appVersion) >= 4)
	    {
		    winX = (screen.availWidth - w)*.5;
		    winY = (screen.availHeight - h)*.5;
	    }
	    popupLoadnWin=window.open(sUrl,'popupLoadnWin','scrollbars,resizable=no,status=yes, width=550,height=400,left=100,top=100');
    }
</script>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td valign="top">
            <table width="100%" cellpadding="0" cellspacing="0" style="background: #FFFFFF">
                <tr>
                    <td align="left" valign="top">
                        <table style="width:100%"><tr><td><h2 class="cattitle" style="padding-left:30px">THÔNG TIN CÔNG TY</h2></td><td style="text-align:right;padding-right:5px;vertical-align:top"><asp:Label runat="server" ID="lblNgay"></asp:Label></td></tr></table>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top" class="CafeF_Padding10" style="height: 294px; padding-left: 10px;
                        padding-right: 10px;">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="padding-bottom:10px">
                                    <uc1:TradeInfoHeader ID="TradeInfoHeader1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div runat="server" id="divThiTruong">
                                        <table border="0" cellpadding="0" cellspacing="0" style="margin-top: 2px; border: none;
                                            background-image: url(http://cafef3.vcmedia.vn/images/gia_tin_dn_bkg.gif); background-repeat: no-repeat;">
                                            <tr>
                                                <td>
                                                    Giá hiện tại:
                                                    <asp:Label runat="server" ID="lblSymbol" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="text-align: right; padding-right: 3px; padding-left: 3px; height: 20px;">
                                                    <asp:Image runat="server" ID="imgChange" />
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblPrice" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="padding-left: 3px;">
                                                    <asp:Label runat="server" ID="lblChange"></asp:Label>
                                                </td>
                                                <td style="padding-left: 10px; padding-right: 20px;">
                                                    <asp:HyperLink ID="lnkBieudo" runat="server"><img border="0" id="imgViewChart" valign="absmidle" alt="Biểu đồ GTGD và KLGD trong 1 tháng" src="http://cafef3.vcmedia.vn/images/chart.gif" style="cursor: pointer; cursor: hand;" onmouseover="ShowBox(event, 'divChart', 100);" onmouseout="HideBox('divChart');" /></asp:HyperLink>
                                                </td>
                                                <td style="padding-right: 10px;">
                                                    <asp:HyperLink ID="lnkTangtruong" runat="server"><img border="0" id="imgTangTruong" valign="absmidle" alt="Thông tin tài chính" src="http://cafef3.vcmedia.vn/images/CompanyInfo/icon_chart.gif"  /></asp:HyperLink>
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="lnkCompany" ForeColor="#666666" runat="server">Hồ sơ công ty</asp:HyperLink>
                                                </td>
                                                <td style="padding-left: 10px;" class="F_text_listbai">
                                                    <img alt="" src="http://cafef3.vcmedia.vn/images/icon2a.jpg" width="33px" align="absmiddle">
                                                    <a style="font-weight: bold" href="/Lich-su-giao-dich-<% =System.Web.HttpContext.Current.Request.QueryString["symbol"] %>-4.chn">
                                                        Tra cứu GDCĐ lớn & CĐ nội bộ</a>
                                                </td>
                                            </tr>
                                        </table>
                                        <div id="divChart" onmouseover="ShowBox(event, 'divChart');" onmouseout="HideBox('divChart');"
                                            style="width: 270px; height: 250px; text-align: center; position: absolute; z-index: 10000;
                                            background-color: #ffffff; border: solid 2px #77b143; padding: 3px; display: none;
                                            -moz-opacity: 0.9; opacity: 0.9; filter: alpha(opacity=90);">
                                            <asp:Image ID="imgChart" runat="server" /><br />
                                            <div class="Note">
                                                <div style="overflow: hidden; float: right;">
                                                    Đơn vị KL: 10,000 CP</div>
                                                <div style="overflow: hidden; float: left;">
                                                    Đơn vị giá: 1,000 VND</div>
                                            </div>
                                            <br />
                                            <div class="Note" style="font-weight: bold; color: #666666; text-align: center;">
                                                Biểu đồ GTGD và KLGD trong 1 tháng</div>
                                        </div>
                                        <div id="divTangTruong" onmouseover="ShowBox(event, 'divTangTruong');" onmouseout="HideBox('divTangTruong');"
                                            style="width: 605px; height: 450px; text-align: center; position: absolute; z-index: 10000;
                                            background-color: #ffffff; border: solid 2px #77b143; padding: 3px; display: none;
                                            -moz-opacity: 0.95; opacity: 0.95; filter: alpha(opacity=95);">
                                            <div style="margin-top: 3px; margin-bottom: 5px; text-align: left; color: #004377;
                                                font-size: 12px; font-weight: bold;">
                                                Thông tin tài chính</div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 10px;">
                                    <table align="center" width="99%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="text_noibat_cacbaikhac" style="padding-bottom: 5px">
                                                <span class="cms_blue">
                                                    <asp:Literal runat="server" ID="lblTitle"></asp:Literal>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" class="Crawler_style">
                                                <table border="0" align="left" cellpadding="0">
                                                    <tr>
                                                        <td align="left" style="padding: 0px 5px 0px 0px">
                                                            <asp:Image runat="server" ID="img" Width="150" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <span class="DetailSapo">
                                                    <asp:Literal runat="server" ID="initContent"></asp:Literal></span>
                                                <br />
                                                <br />
                                                <div class="KenhF_Content_News3">
                                                    <asp:PlaceHolder ID="pldContent" runat="server"></asp:PlaceHolder>
                                                </div>
                                                <%--</span>--%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="height: 10px">
        </td>
    </tr>
    <tr>
        <td valign="top">
            <table width="100%" cellpadding="0" cellspacing="0" style="background: #FFFFFF">
                <tr>
                    <td align="left" valign="top">
                                    <span class="sukien_DOANHNGHIEP_link">C&aacute;c tin kh&aacute;c </span>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top" class="CafeF_Padding10">
                        <uc2:OtherCrawlerNews ID="OtherCrawlerNews1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<asp:Literal runat="server" ID="ltrCounter"></asp:Literal>

<script type="text/javascript">
function ShowBox(e, boxId, leftPosition)
{
    if (!e) e = window.even;
    
    var chart = document.getElementById(boxId);
    chart.style.display = 'block';
    if (leftPosition && !isNaN(leftPosition)) chart.style.left = (e.clientX - leftPosition) + 'px';
    chart.style.top = '295px';
}
function HideBox(boxId)
{
    var chart = document.getElementById(boxId);
    chart.style.display = 'none';
}
</script>

