<%@ Page Language="C#" AutoEventWireup="true" Codebehind="TraCuu.aspx.cs" Inherits="CafeF.Redis.Page.TraCuuLichSu2.TraCuu" %>
<%@ Import Namespace="CafeF.Redis.BL"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    <script src='http://cafef3.vcmedia.vn/v2/kby/<%= Utils.GetKbyFolder() %>kby.js' type="text/javascript"></script> 
    <%--<script src="/scripts/HeaderScript.js" type="text/javascript"></script>--%>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/v2/scripts/cafef.header.cometd.solieu.js"></script>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/v2/scripts/cometd.framework.js"></script>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/v2/scripts/THEO_DOI_CHUNG_KHOAN.js"></script>
</head>
<body style="background-color: #FFF; margin: 2px 0px 0px 0px">
    <form id="form1" runat="server">
        <div align="center">
            <div class="cf_RedCBox" style="width: 1000px" align="center" id="cf_ContainerBox">
                <div class="cf_BoxContent">
                    <div class="cf_RedireBox">
                        <div class="cf_BoxHeader">
                            <div>
                            </div>
                        </div>
                        <div class="cf_BoxContent" style="background-color: #ba2020; text-align: left;">
                            <div style="float: left; width: 100%; background-color: #ba2020; color: #fff; font-family: Tahoma;
                                font-size: 12px; font-weight: bold; padding-top: 5px; text-align: left" id="div_cf_BoxContent">
                                <span style="float: right;">
                                    <table>
                                        <tr>
                                            <td valign="middle"><img src="http://cafef3.vcmedia.vn/images/iconhome1.gif" alt="" border="0" />&nbsp;</td>
                                            <td valign="middle"><a href="/" style="color: White;">Trang chủ</a>&nbsp;</td>
                                        </tr>
                                    </table>
                                </span>&nbsp;<asp:Literal runat="server" ID="ltrTitle"> </asp:Literal>
                                <span style="color: rgb(255, 255, 204);">
                                    <asp:Literal runat="server" ID="ltrText"></asp:Literal></span>
                                <div style="background-color: #ba2020; width: 100%; height: 26px; padding-top: 10px;">
                                    <div id="tabtracuu1" style="width: 200px; vertical-align: middle; float: left; padding-left: 10px;"></div>
                                    <div id="tabtracuu2" style="width: 200px; vertical-align: middle; float: left; padding-left: 10px;"></div>
                                    <div id="tabtracuu3" style="width: 200px; vertical-align: middle; float: left; padding-left: 10px;"></div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left; padding-left: 0px; background-color: #E1E1E1;"
                                align="left">
                                <asp:PlaceHolder runat="server" ID="pldContent"></asp:PlaceHolder>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script type="text/javascript">
        var tab = '<%= tab %>';
        var host = 'http://<%= this.Request.Url.Authority %>';
        var san = '<%= san %>';
        date = '<%= date %>';
    </script>
    <script language="javascript" type="text/javascript" src="http://cafef3.vcmedia.vn/TraCuuLichSu/js/tracuu.js"></script>
    <script src="http://www.google-analytics.com/urchin.js" type="text/javascript"></script>
    <script type="text/javascript">
        _uacct = "UA-3070916-9";
        urchinTracker();
    </script>
    <%--<script type="text/javascript" src="http://reporting.cafef.channelvn.net/js.js?dat123456"></script>--%>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/reporting/log.js"></script>
    <script type="text/javascript">
        var log_website = 'cafef_reporting';        
        Log_AssignValue("-1", "", "1115", "lichsugiaodich_all");
    </script>
    <script language="javascript" type="text/javascript" src="http://cafef3.vcmedia.vn/TraCuuLichSu/js/js.js"></script>
</body>
</html>
