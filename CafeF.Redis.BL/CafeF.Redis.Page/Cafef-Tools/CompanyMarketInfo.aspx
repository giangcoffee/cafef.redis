<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Register Src="/UserControl/StockView/CompanyInfo.ascx" TagName="CompanyInfo" TagPrefix="uc" %>
<%@ Register Src="/UserControl/StockView/TradeInfo.ascx" TagName="ucTradeInfo" TagPrefix="uc" %>
<%@ Register Src="/UserControl/StockView/TradeInfoHeader.ascx" TagName="ucTradeInfoHeader" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
        <meta name="ROBOTS" content="NOINDEX, NOFOLLOW"/>
        <meta name="verify-v1" content="ZL5tw2W+Mxzp5kGkm/tEVCR+EB1//tU1Z57vW8BYhw4=" />
    <link href="http://cafef3.vcmedia.vn/styles/StyleDiv.css" rel="stylesheet" type="text/css" />
    <link href="http://cafef3.vcmedia.vn/styles/NewsDetailCss.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="http://cafef3.vcmedia.vn/v2/style/style.css" />
    <link rel="Stylesheet" type="text/css" href="http://cafef3.vcmedia.vn/v2/style/solieu.css" />
    <link rel="stylesheet" href="http://cafef3.vcmedia.vn/styles/CafeFOldStyle.css?123" type="text/css">
    <link rel="shortcut icon" href="http://cafef3.vcmedia.vn/images/iconcafef.ico?1" />
    <!--[if lte IE 6]>
    <link rel="stylesheet" type="text/css" href="/styles/ie6.css" />
    <![endif]-->
     <script type="text/javascript" src="http://cafef3.vcmedia.vn/v2/scripts/cafef.header.cometd.solieu.js"></script>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/v2/scripts/inlinestockpage.js"></script>
</head>
<body style="margin: 2px 2px 2px 2px; font-family: Arial; background: #FFFFFF;">
    <%--<table width="100%" cellpadding="0" cellspacing="5" style="background: #FFFFFF">
        <tr>
            <td align="left" valign="top" class="sukien_padding">
                <uc2:ThongTinCongTy ID="ThongTinCongTy1" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" class="sukien_padding">
                <uc1:CacChiSo ID="CacChiSo1" runat="server" />
            </td>
        </tr>
    </table>--%>
    <div class="dulieu" style="padding:0;">
	    <h2 class="cattitle">Thông tin giao dịch</h2>
            <!-- // CacChiSo -->  
            <uc:ucTradeInfoHeader ID="ucTradeInfoHeader" runat="server" EnableViewState=false />
            <uc:ucTradeInfo ID="ucTradeInfo" runat="server" EnableViewState=false/>
            <!-- // End -->
            <%--<div class="hosocongty">                           
                <uc:CompanyInfo ID="CompanyInfo" runat="server" EnableViewState=false/>
                <!-- // Hồ sơ công ty -->
            </div>--%>
        </div>
        <style type="text/css">
        .dlt-left {width:338px;}
        .dltl-update {width: 130px;}
        .dlt-right {width:250px;}
        .dltl-other .r {width: 89px;}
        </style>
</body>
</html>
