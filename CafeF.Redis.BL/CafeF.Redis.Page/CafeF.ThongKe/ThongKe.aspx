<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ThongKe.aspx.cs" Inherits="CafeF.ThongKe.ThongKe" %>
<%@ Register Src="ucThongKeAjax.ascx" TagName="ucThongKeAjax" TagPrefix="uc3" %>
<%@ Register Src="ucThongKePadding.ascx" TagName="ucThongKePadding" TagPrefix="uc1" %>
<%@ Register Src="ucThongKe.ascx" TagName="ucThongKe" TagPrefix="uc2" %>
<%@ Register Src="/UserControl/CafefToolbar.ascx" TagName="Toolbar" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title id="Title1" enableviewstate="false" runat="server">Thống kê giá và khối lượng giao dịch của cổ phiếu | CafeF.vn</title>
    <meta name="Keywords" content="Thông tin tài chính, chứng khoán, bất động sản, kinh tế,  đầu tư, tài chính quốc tế, thông tin doanh nghiệp, lãi suất, tiền tệ, ngân hàng"
        id="KEYWORDS" runat="server" enableviewstate="false" />
    <meta name="Description" content="Thống kê chỉ số giá và khối lượng giao dịch cổ phiếu trên HSX, HNX, UPCOM theo thời gian tuần, tháng, quý, năm"
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
        <style type="text/css">
            .gamekPadding313131 a,.gamekPadding313131{color:#313131;font-weight:bold;padding:3px 6px; font-family:Tahoma; text-decoration:none; font-size:13px}
            #cafeftoolbar { margin: 5px 0pt;}
	#cafeftoolbar a, #cafeftoolbar a:visited {text-decoration: none; color: #000072; padding-right: 5px;}
        </style>
    <link href="http://cafef3.vcmedia.vn/TraCuuLichSu/js/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/js/thongke/jquery-1.4.2.js"></script>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/js/thongke/jquery.delay.js"></script>
    
</head>
<body style="background-color: #FFF; margin: 2px 0px 0px 0px">
    <form id="form1" runat="server">
        <div align="center">
            <uc1:Toolbar runat="server" id="ucToolbar" />
            <div class="cf_RedCBox" style="width: 950px" align="center" id="cf_ContainerBox">
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
                                            <td valign="middle">
                                                <img src="http://cafef3.vcmedia.vn/images/iconhome1.gif" border="0" />&nbsp;</td>
                                            <td valign="middle">
                                                <a href="http://cafef.vn" style="color: White;">Trang chủ</a>&nbsp;</td>
                                        </tr>
                                    </table>
                                </span>&nbsp;<asp:Literal runat="server" ID="ltrTitle"> </asp:Literal>
                                <span style="color: rgb(255, 255, 204);">
                                    Thống kê cổ phiếu theo thời gian</span>
                                <div style="background-color: #ba2020; width: 940px; height: 26px; padding: 10px 0px 0px 10px;">                                    
                                     <input id="btUp" type="button" value="Tăng giá" onclick="OrderGia('desc');" style="color:Red" />&nbsp;&nbsp;<input id="btDown" type="button" value="Giảm giá" onclick="OrderGia('asc');" />
                                </div>
                                <div style="background-color: #ba2020; width: 100%; height: 26px; padding-top: 10px;">
                                    <div id="tabtracuu1" style="width: 70px; vertical-align: middle; float: left; padding-left: 10px;">
                                        <div style="background: transparent url(http://cafef3.vcmedia.vn/TraCuuLichSu/images/Trai.png) no-repeat scroll 0% 0%; float: left; width: 7px; height: 26px; -moz-background-clip: border; -moz-background-origin: padding; -moz-background-inline-policy: continuous;"></div>
	                                    <div style="background: transparent url(http://cafef3.vcmedia.vn/TraCuuLichSu/images/Phai.png) no-repeat scroll 0% 0%; float: right; width: 7px; height: 26px; -moz-background-clip: border; -moz-background-origin: padding; -moz-background-inline-policy: continuous;"></div>
	                                    <div style="background: transparent url(http://cafef3.vcmedia.vn/TraCuuLichSu/images/Giua.png) repeat scroll 0% 0%; height: 21px; padding-top: 5px; -moz-background-clip: border; -moz-background-origin: padding; -moz-background-inline-policy: continuous; text-align: center;">
		                                    <a id="a5Phien" style="font-weight: normal; color: Black; font-family: Arial; text-decoration: none; font-size: 12px; margin-top: 4px;" href="javascript:SelectTab(1);">1 Tuần</a>
	                                    </div>
                                    </div>
                                    <div id="tabtracuu2" style="width: 70px; vertical-align: middle; float: left; padding-left: 5px;">
                                         <div style="background: transparent url(http://cafef3.vcmedia.vn/TraCuuLichSu/images/Trai.png) no-repeat scroll 0% 0%; float: left; width: 7px; height: 26px; -moz-background-clip: border; -moz-background-origin: padding; -moz-background-inline-policy: continuous;"></div>
	                                    <div style="background: transparent url(http://cafef3.vcmedia.vn/TraCuuLichSu/images/Phai.png) no-repeat scroll 0% 0%; float: right; width: 7px; height: 26px; -moz-background-clip: border; -moz-background-origin: padding; -moz-background-inline-policy: continuous;"></div>
	                                    <div style="background: transparent url(http://cafef3.vcmedia.vn/TraCuuLichSu/images/Giua.png) repeat scroll 0% 0%; height: 21px; padding-top: 5px; -moz-background-clip: border; -moz-background-origin: padding; -moz-background-inline-policy: continuous; text-align: center;">
		                                    <a id="a10Phien" style="color: Black; font-family: Arial; text-decoration: none; font-size: 12px; margin-top: 4px; font-weight:normal" href="javascript:SelectTab(2);">2 Tuần</a>
	                                    </div>
                                    </div>
                                    <div id="tabtracuu3" style="width: 70px; vertical-align: middle; float: left; padding-left: 5px;">
                                          <div style="background: transparent url(http://cafef3.vcmedia.vn/TraCuuLichSu/images/Trai.png) no-repeat scroll 0% 0%; float: left; width: 7px; height: 26px; -moz-background-clip: border; -moz-background-origin: padding; -moz-background-inline-policy: continuous;"></div>
	                                    <div style="background: transparent url(http://cafef3.vcmedia.vn/TraCuuLichSu/images/Phai.png) no-repeat scroll 0% 0%; float: right; width: 7px; height: 26px; -moz-background-clip: border; -moz-background-origin: padding; -moz-background-inline-policy: continuous;"></div>
	                                    <div style="background: transparent url(http://cafef3.vcmedia.vn/TraCuuLichSu/images/Giua.png) repeat scroll 0% 0%; height: 21px; padding-top: 5px; -moz-background-clip: border; -moz-background-origin: padding; -moz-background-inline-policy: continuous; text-align: center;">
		                                    <a id="a1Thang" style="color: Black; font-family: Arial; text-decoration: none; font-size: 12px; margin-top: 4px; font-weight:normal" href="javascript:SelectTab(3);">1 Tháng</a>
	                                    </div>
                                    </div>
                                    <div id="tabtracuu5" style="width: 70px; vertical-align: middle; float: left; padding-left: 5px;">
                                          <div style="background: transparent url(http://cafef3.vcmedia.vn/TraCuuLichSu/images/Trai.png) no-repeat scroll 0% 0%; float: left; width: 7px; height: 26px; -moz-background-clip: border; -moz-background-origin: padding; -moz-background-inline-policy: continuous;"></div>
	                                    <div style="background: transparent url(http://cafef3.vcmedia.vn/TraCuuLichSu/images/Phai.png) no-repeat scroll 0% 0%; float: right; width: 7px; height: 26px; -moz-background-clip: border; -moz-background-origin: padding; -moz-background-inline-policy: continuous;"></div>
	                                    <div style="background: transparent url(http://cafef3.vcmedia.vn/TraCuuLichSu/images/Giua.png) repeat scroll 0% 0%; height: 21px; padding-top: 5px; -moz-background-clip: border; -moz-background-origin: padding; -moz-background-inline-policy: continuous; text-align: center;">
		                                    <a id="a3Thang" style="color: Black; font-family: Arial; text-decoration: none; font-size: 12px; margin-top: 4px; font-weight:normal" href="javascript:SelectTab(5);">3 Tháng</a>
	                                    </div>
                                    </div>
                                    <div id="tabtracuu6" style="width: 70px; vertical-align: middle; float: left; padding-left: 5px;">
                                          <div style="background: transparent url(http://cafef3.vcmedia.vn/TraCuuLichSu/images/Trai.png) no-repeat scroll 0% 0%; float: left; width: 7px; height: 26px; -moz-background-clip: border; -moz-background-origin: padding; -moz-background-inline-policy: continuous;"></div>
	                                    <div style="background: transparent url(http://cafef3.vcmedia.vn/TraCuuLichSu/images/Phai.png) no-repeat scroll 0% 0%; float: right; width: 7px; height: 26px; -moz-background-clip: border; -moz-background-origin: padding; -moz-background-inline-policy: continuous;"></div>
	                                    <div style="background: transparent url(http://cafef3.vcmedia.vn/TraCuuLichSu/images/Giua.png) repeat scroll 0% 0%; height: 21px; padding-top: 5px; -moz-background-clip: border; -moz-background-origin: padding; -moz-background-inline-policy: continuous; text-align: center;">
		                                    <a id="a6Thang" style="color: Black; font-family: Arial; text-decoration: none; font-size: 12px; margin-top: 4px; font-weight:normal" href="javascript:SelectTab(6);">6 Tháng</a>
	                                    </div>
                                    </div>
                                     <div id="tabtracuu7" style="width: 70px; vertical-align: middle; float: left; padding-left: 5px;">
                                          <div style="background: transparent url(http://cafef3.vcmedia.vn/TraCuuLichSu/images/Trai.png) no-repeat scroll 0% 0%; float: left; width: 7px; height: 26px; -moz-background-clip: border; -moz-background-origin: padding; -moz-background-inline-policy: continuous;"></div>
	                                    <div style="background: transparent url(http://cafef3.vcmedia.vn/TraCuuLichSu/images/Phai.png) no-repeat scroll 0% 0%; float: right; width: 7px; height: 26px; -moz-background-clip: border; -moz-background-origin: padding; -moz-background-inline-policy: continuous;"></div>
	                                    <div style="background: transparent url(http://cafef3.vcmedia.vn/TraCuuLichSu/images/Giua.png) repeat scroll 0% 0%; height: 21px; padding-top: 5px; -moz-background-clip: border; -moz-background-origin: padding; -moz-background-inline-policy: continuous; text-align: center;">
		                                    <a id="a1Nam" style="color: Black; font-family: Arial; text-decoration: none; font-size: 12px; margin-top: 4px; font-weight:normal" href="javascript:SelectTab(7);">1 Năm</a>
	                                    </div>
                                    </div>
                                     <div id="tabtracuu4" style="width: 130px; vertical-align: middle; float: left; padding-left: 5px;">
                                          <div style="background: transparent url(http://cafef3.vcmedia.vn/TraCuuLichSu/images/Trai.png) no-repeat scroll 0% 0%; float: left; width: 7px; height: 26px; -moz-background-clip: border; -moz-background-origin: padding; -moz-background-inline-policy: continuous;"></div>
	                                    <div style="background: transparent url(http://cafef3.vcmedia.vn/TraCuuLichSu/images/Phai.png) no-repeat scroll 0% 0%; float: right; width: 7px; height: 26px; -moz-background-clip: border; -moz-background-origin: padding; -moz-background-inline-policy: continuous;"></div>
	                                    <div style="background: transparent url(http://cafef3.vcmedia.vn/TraCuuLichSu/images/Giua.png) repeat scroll 0% 0%; height: 21px; padding-top: 5px; -moz-background-clip: border; -moz-background-origin: padding; -moz-background-inline-policy: continuous; text-align: center;">
		                                    <a id="aTuyChon" style="color: Black; font-family: Arial; text-decoration: none; font-size: 12px; margin-top: 4px;font-weight:normal" href="javascript:LoadTabTuyChon();">Thời gian tùy chọn</a>
	                                    </div>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left; padding-left: 0px; background-color: #E1E1E1;" align="left">
                                <uc3:ucThongKeAjax id="UcThongKeAjax1" runat="server"></uc3:ucThongKeAjax>
                                <%--<uc2:ucThongKe id="UcThongKe1" runat="server"></uc2:ucThongKe> <uc1:ucThongKePadding id="ucThongKePadding1" runat="server"></uc1:ucThongKePadding>--%></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
       
    </form>
    
    <%--<script src="http://www.google-analytics.com/urchin.js" type="text/javascript"></script>

    <script type="text/javascript">
        _uacct = "UA-3070916-9";
        urchinTracker();
    </script>--%>
     <script type="text/javascript">
    var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
    document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
    </script>
    <script type="text/javascript">
    var pageTracker = _gat._getTracker("UA-3070916-9");
    pageTracker._trackPageview();
    </script>
    <%--<script type="text/javascript" src="http://reporting.cafef.channelvn.net/js.js?dat123456"></script>--%>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/reporting/log.js"></script>
    <script type="text/javascript">
        var log_website = 'cafef_reporting';        
        Log_AssignValue("-1", "", "2010", "thongke");
    </script>
    <script language="javascript" type="text/javascript" src="http://cafef3.vcmedia.vn/js/thongke/sort.js"></script>
    <script language="javascript" type="text/javascript" src="http://cafef3.vcmedia.vn/TraCuuLichSu/js/sort.js"></script>

</body>
</html>
