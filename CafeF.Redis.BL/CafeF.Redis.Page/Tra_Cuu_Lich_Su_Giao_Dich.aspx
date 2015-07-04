<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tra_Cuu_Lich_Su_Giao_Dich.aspx.cs"
    Inherits="CafeF.Redis.Page.Tra_Cuu_Lich_Su_Giao_Dich" EnableViewStateMac="false"
    EnableSessionState="True" EnableEventValidation="false" ValidateRequest="false"
    ViewStateEncryptionMode="Never" %>
<%@ Import Namespace="CafeF.Redis.BL"%>

<%@ Register Src="UserControl/LichSu/LichSuGia.ascx" TagName="LichSuGia" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/CafefToolbar.ascx" TagName="Toolbar" TagPrefix="uc1" %>
<%@ OutputCache Duration="180" VaryByParam="*" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title id="Title1" enableviewstate="false" runat="server">CafeF - Thông tin và dữ liệu
        tài chính chứng khoán Việt Nam | CafeF.vn</title>
    <meta name="Keywords" content="Thông tin tài chính, chứng khoán, bất động sản, kinh tế,  đầu tư, tài chính quốc tế, thông tin doanh nghiệp, lãi suất, tiền tệ, ngân hàng"
        id="KEYWORDS" runat="server" enableviewstate="false" />
    <meta name="Description" content="Thông tin thị trường tài chính Việt nam và quốc tế"
        runat="server" id="Meta1" enableviewstate="false" />
    <meta http-equiv="EXPIRES" content="0" />
    <meta name="RESOURCE-TYPE" content="DOCUMENT" />
    <meta name="DISTRIBUTION" content="GLOBAL" />
    <meta name="AUTHOR" content="cafefdotvn" />
    <meta name="COPYRIGHT" content="Copyright (c) by cafef - info@cafef.vn" />
    <meta name="ROBOTS" content="INDEX, FOLLOW" />
    <meta name="REVISIT-AFTER" content="1 DAYS" />
    <meta name="RATING" content="GENERAL" />
    <meta name="GENERATOR" content="Cafef" />
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="verify-v1" content="ZL5tw2W+Mxzp5kGkm/tEVCR+EB1//tU1Z57vW8BYhw4=" />
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link rel="shortcut icon" href="http://cafef3.vcmedia.vn/images/iconcafef.ico?1" />
    <link href="http://cafef3.vcmedia.vn/styles/cafef.css?v1.3" rel="stylesheet" type="text/css" />
    <link href="http://cafef3.vcmedia.vn/styles/DatePicker/datepicker.css" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript" src="http://cafef3.vcmedia.vn/js/Library.js?upd=28881135"></script>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/js/Performance/NumberFormat.js?a123"></script>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/js/AutoComplete/jquery.js?123"></script>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/js/jqDnR.js"></script>
    <script src='http://cafef3.vcmedia.vn/v2/kby/<%= Utils.GetKbyFolder() %>kby.js' type="text/javascript"></script>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/js/AutoComplete/tochuc.js?aaAupdate30092008"></script>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/js/AutoComplete/jquery.bgiframe.min.js"></script>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/js/AutoComplete/jquery.dimensions.js"></script>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/v2/scripts/jquery.autocomplete2.js"></script>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/js/jquery.treeview.pack.js"></script>
    <script src="http://static.admicro.vn/embed/ads.js" language="javascript" type="text/javascript"></script>--%>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/js/AutoComplete/tochuc.js"></script>
    <script src='http://cafef3.vcmedia.vn/v2/kby/<%= Utils.GetKbyFolder() %>kby.js' type="text/javascript"></script>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/v2/scripts/cafef.header.cometd.solieu.js"></script>
    
    <style type="text/css">
        #cafeftoolbar { margin: 5px 0pt;}
        #cafeftoolbar a, #cafeftoolbar a:visited {text-decoration: none; color: #000072; padding-right: 5px;}
    </style>
</head>
<body style="background-color: #FFF;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptmanager" runat="server">
    </asp:ScriptManager>
    
    <table align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <uc1:Toolbar runat="server" id="ucToolbar" />
                <div class="cf_RedCBox" runat="server" id="cf_ContainerBoxTCLSDD" style="width: 903px;">
                    <div class="cf_BoxContent">
                        <div class="cf_RedireBox">
                            <div class="cf_BoxHeader"><div></div></div>
                            <div class="cf_BoxContent">
                                <div style="float: left; width: 100%; background-color: #ba2020; height: 20px; color: #fff;
                                    font-family: Tahoma; font-size: 12px; font-weight: bold; padding-top: 5px; text-align: left"
                                    id="div_cf_BoxContent">
                                    <span style="float: right;">
                                        <table>
                                            <tr>
                                                <td valign="middle"><img src="http://cafef3.vcmedia.vn/images/iconhome1.gif" border="0" />&nbsp;</td>
                                                <td valign="middle"><a href="http://cafef.vn" style="color: White;">Trang chủ</a>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </span>&nbsp;
                                    <asp:UpdatePanel ID="LabelUpdatePanel" runat="server" ChildrenAsTriggers="false"
                                        RenderMode="Inline" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Literal runat="server" ID="ltrTitle"> </asp:Literal><span style="color: rgb(255, 255, 204);">
                                                <asp:Literal runat="server" ID="ltrText"></asp:Literal></span>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="cf_ResearchDataHistoryInfo">
                                    <div style="width: 7px; border-bottom: solid 0px #e1e1e1; float: left; height: 30px">
                                    </div>
                                     <asp:UpdatePanel ID="LinkUpdatePanel" runat="server" ChildrenAsTriggers="false"
                                        RenderMode="Inline" UpdateMode="Conditional">
                                        <ContentTemplate>
                                    <div class="cf_ResearchDataHistory_Tab1_Sel" id="divDataHistory" runat="server">
                                        <div class="ResearchDataHistory_BoxHeader"><div></div></div>
                                        <div class="ResearchDataHistory_BoxContent" onclick="javascript:changeTabXemLichSu(1);"><a id="aLSG" runat="server">Lịch sử giá</a></div>
                                    </div>
                                    <div style="width: 7px; border-bottom: solid 0px #e1e1e1; float: left; height: 30px"></div>
                                    <div class="cf_ThongKeDatLenh" id="divCungCau" runat="server">
                                        <div class="ResearchDataHistory_BoxHeader"><div></div></div>
                                        <div class="ResearchDataHistory_BoxContent" onclick="javascript:changeTabXemLichSu(2);">
                                            <a id="aTKL" runat="server">Thống kê đặt lệnh</a>
                                        </div>
                                    </div>
                                    <div style="width: 7px; border-bottom: solid 0px #D6D6D8; float: left; height: 30px"></div>
                                    <div class="cf_ResearchDataHistory" id="divGDNN" runat="server">
                                        <div class="ResearchDataHistory_BoxHeader"><div></div></div>
                                        <div class="ResearchDataHistory_BoxContent" onclick="javascript:changeTabXemLichSu(3);">
                                            <a id="aNDTNG" runat="server">Giao dịch NĐT nước ngoài</a>
                                        </div>
                                    </div>
                                    <div style="width: 7px; border-bottom: solid 0px #D6D6D8; float: left; height: 30px"></div>
                                    <div class="cf_Codonglon" id="divLocalTrans" runat="server">
                                        <div class="ResearchDataHistory_BoxHeader"><div></div></div>
                                        <div class="ResearchDataHistory_BoxContent" onclick="javascript:changeTabXemLichSu(4);">
                                            <a id="aCDL" runat="server">Giao dịch cổ đông lớn & cổ đông nội bộ</a>
                                        </div>
                                    </div>
                                    <div style="width: 7px; border-bottom: solid 0px #D6D6D8; float: left; height: 30px"></div>
                                    <div class="cf_ResearchDataHistory" id="divMBLFinal" runat="server">
                                        <div class="ResearchDataHistory_BoxHeader"><div></div></div>
                                        <div class="ResearchDataHistory_BoxContent" onclick="javascript:changeTabXemLichSu(5);">
                                            <a id="aCPQ" runat="server">Giao dịch cổ phiếu quỹ</a>
                                        </div>
                                    </div>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div style="width: 100%; float: left; padding-left: 0px; background-color: #E1E1E1;" align="center" id="divStart">
                                        <asp:PlaceHolder runat="server" ID="pldContent"></asp:PlaceHolder>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <%--<script type="text/javascript" src="http://reporting.cafef.channelvn.net/js.js?dat123456"></script>--%>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/reporting/log.js"></script>
    <script type="text/javascript">
        var log_website = 'cafef_reporting';        
        Log_AssignValue("-1", "", "1115", "lichsugiaodich_all");
    </script>
    <script type="text/javascript">
    var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
    document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
    </script>
<script type="text/javascript">
    var pageTracker = _gat._getTracker("UA-3070916-9");
    pageTracker._trackPageview();
</script>
<script type="text/javascript">H_ResizeToCenter();</script>
</form>
</body>
</html>
