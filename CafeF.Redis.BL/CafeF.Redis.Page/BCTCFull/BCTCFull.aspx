<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BCTCFull.aspx.cs" Inherits="BCTCFull.BCTCFull" %>
<%@ Import Namespace="CafeF.Redis.BL"%>

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
    <style type="text/css">
		.IContent
        {
            width : 600px;position:absolute;
            color :#C0C0C0;z-index:-1;
            visibility:hidden;
            left: 255px; top: 372px;
        }
        .TContent
        {
            font : 1% Verdana;
        }
		</style>
    <link href="http://cafef3.vcmedia.vn/styles/cafef.css" rel="stylesheet" type="text/css" />
    <link href="http://cafef3.vcmedia.vn/styles/DatePicker/datepicker.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/js/Library.js"></script>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/js/Performance/NumberFormat.js"></script>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/js/AutoComplete/jquery.js"></script>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/js/jqDnR.js"></script>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/v2/kby/<%= Utils.GetKbyFolder() %>kby.js"></script>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/js/AutoComplete/tochuc.js?aaAupdate30092008"></script>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/js/AutoComplete/jquery.bgiframe.min.js"></script>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/js/AutoComplete/jquery.dimensions.js"></script>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/js/AutoComplete/jquery.autocomplete2.js"></script>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/js/jquery.treeview.pack.js"></script>
    
    
<style type="text/css">
.n_t1 
{background-image:url(http://cafef3.vcmedia.vn/images/BCTCFull/n_left_t1.jpg);background-position:left;float:left;width:120px;background-repeat:no-repeat}
.n_t1_i
{background-image:url(http://cafef3.vcmedia.vn/images/BCTCFull/n_right_t1.jpg);background-position:right;background-repeat:no-repeat}

.s_t1
{background-image:url(http://cafef3.vcmedia.vn/images/BCTCFull/s_left.jpg);background-position:left;float:left;width:120px;background-repeat:no-repeat}
.s_t1_i
{background-image:url(http://cafef3.vcmedia.vn/images/BCTCFull/s_right.jpg);background-position:right;background-repeat:no-repeat}

.n_t2 
{background-image:url(http://cafef3.vcmedia.vn/images/BCTCFull/n_left.jpg);background-position:left;float:left;width:230px;background-repeat:no-repeat}
.n_t2_none 
{float:left;width:0px;}
.n_t2_i
{background-image:url(http://cafef3.vcmedia.vn/images/BCTCFull/n_right.jpg);background-position:right;background-repeat:no-repeat}

.s_t2
{background-image:url(http://cafef3.vcmedia.vn/images/BCTCFull/s_left_2.jpg);background-position:left;float:left;width:230px;background-repeat:no-repeat}
.s_t2_i
{background-image:url(http://cafef3.vcmedia.vn/images/BCTCFull/s_right_2.jpg);background-position:right;background-repeat:no-repeat}

.h_t
{color:#004276;font-weight:bold;font-family:Tahoma;border-right:solid 1px #cccccc;text-align:center}
.h_t_tt
{color:#004276;font-weight:bold;font-family:Tahoma;text-align:center}

.r_item
{font-family: Arial; font-size: 10px; font-weight: normal; background-color: #f2f2f2;height:30px}

.r_item_a
{font-family: Arial; font-weight: normal; background-color: #fff;height:30px}

.b_r_c
{border-right:solid 1px #cccccc}

.n_c
{width: 7px; border-bottom: solid 0px #D6D6D8; float: left;}

.d_item
{text-align: center;vertical-align: top; height:25px;vertical-align:middle;}

.width_100pc{width:100%}
.width_824px{width:824px}
</style>
    
</head>
<body style="background-color: #FFF; margin: 0px 0px 0px 0px">
    <form id="form1" runat="server">
        <div align="center" id="divTop" style="overflow:hidden;width:100%">
        <div class="cf_RedCBox" style="width: 840px;overflow:hidden;border-bottom:solid 1px #cecece" id="cf_ContainerBox">
            <div class="cf_BoxContent">
                <div class="cf_RedireBox">
                    <div class="cf_BoxHeader">
                        <div>
                        </div>
                    </div>
                    <div class="cf_BoxContent">
                        <div style="float: left; width: 100%; background-color: #ba2020; height: 20px; color: #fff;
                            font-family: Tahoma; font-size: 12px; font-weight: bold; padding-top: 5px; text-align: left"
                            id="div_cf_BoxContent">
                            &nbsp;<asp:Literal runat="server" ID="ltrTitle"> </asp:Literal><span style="color: rgb(255, 255, 204);">
                                <asp:Literal runat="server" ID="ltrText"></asp:Literal></span></div>
                                
                        <div class="cf_ResearchDataHistoryInfo">
                            <div class="n_c">&nbsp;</div>
                            
                            <div id="divNhom1" class="n_t1">
                                <div id="divNhom1_i" class="n_t1_i">
                                    <div class="d_item">
                                        <div style="padding-top:3px">
                                        <a id="aNhom1" class="NormalTab" href="javascript:void(0)" onclick="redirectBCTC(1)">Cân đối kế toán</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            
                            
                            <div class="n_c">&nbsp;</div>
                            
                            <div id="divNhom2" class="n_t2">
                                <div id="divNhom2_i" class="n_t2_i">
                                    <div class="d_item">
                                        <div style="padding-top:3px">
                                        <a id="aNhom2" class="NormalTab" href="javascript:void(0)" onclick="redirectBCTC(2)">Kết quả hoạt động kinh doanh</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="n_c">&nbsp;</div>
                            
                            <div id="divNhom3" class="n_t2">
                                <div id="divNhom3_i" class="n_t2_i">
                                    <div class="d_item">
                                        <div style="padding-top:3px">
                                        <a id="aNhom3" class="NormalTab" href="javascript:void(0)" onclick="redirectBCTC(3)">Lưu chuyển tiền tệ gián tiếp</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="n_c" id="nc3">&nbsp;</div>                            
                            
                            <div id="divNhom4" class="n_t2">
                                <div id="divNhom4_i" class="n_t2_i">
                                    <div class="d_item">
                                        <div style="padding-top:3px">
                                        <a id="aNhom4" class="NormalTab" href="javascript:void(0)" onclick="redirectBCTC(4)">Lưu chuyển tiền tệ trực tiếp</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; float: left; padding-left: 0px; background-color: #E1E1E1;" align="left">                               
                                
                                
                                <!-- -->

<table cellpadding="0" cellspacing="0" width="100%" style="background-image:url(http://cafef3.vcmedia.vn/images/BCTCFull/back_search.jpg);height:36px">
    <tr valign="middle">
        <td style="width:250px;padding-left:20px">
            <span style="font-size:12px;font-weight:bold;color:#333333">Mã</span> &nbsp;&nbsp;&nbsp;
            <asp:TextBox Font-Names="tahoma" Font-Size="12px" BorderColor="#cecece" BorderStyle="solid" BorderWidth="1px" runat="server" ID="txtKeyword" Width="200px" ValidationGroup="SearchData"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtKeyword"
                ErrorMessage="*" ValidationGroup="SearchData"></asp:RequiredFieldValidator>
            
        </td>        
        <td style="width:50px">
            <select id="sQuy" onchange="redirectFromSelect(this)" style="font-family:Tahoma;font-size:12px">
                <option value="0">4 quý gần nhất</option>
                <option value="1">4 năm gần nhất</option>
            </select>
        </td>
        <td align="left" style="padding-left:20px">
            <asp:ImageButton ID="btSearch" runat="server" ImageUrl="http://cafef3.vcmedia.vn/images/images/xem.gif" OnClick="btSearch_Click" ValidationGroup="SearchData" /></td>
        <td style="float:right" align="right">
            <select id="sOption" onchange="redirectChange(this)" style="font-family:Tahoma;font-size:12px">
                <option value="1">1000 VNĐ</option>
                <option value="2">VNĐ</option>
            </select>
        </td>
    </tr>
</table>

<div style="background-color:#e1e1e1;overflow:hidden" class="width_824px" id="divTableHeader">
<table cellpadding="0" cellspacing="0" width="100%" style="border-top: solid 1px #e6e6e6;border-left:solid 1px #cccccc;"
            id="tblGridData">
            <tr style="border-bottom: solid 1px #e6e6e6; font-family: Arial; font-size: 10px;
                font-weight: bold; color: #004276; background-color: #e1e1e1;height:26px" >
                <td class="b_r_c" style="width:296px">
                    <div style="overflow:hidden;width:99%" align="right">
                        <a href="javascript:void(0)"  onclick="<%= btnAPrevious %>;rePre()"><img alt="Xem dữ liệu trước" src="/BCTCFull/Images/Previous<%= stylePrevious %>.jpg" border="0" /></a>
                        <a href="javascript:void(0)"  onclick="<%= btnANext %>;reNext()"><img alt="Xem dữ liệu tiếp" src="/BCTCFull/Images/Next<%= styleNext %>.jpg" border="0" /></a>
                    </div>
                    </td>
                <td class="h_t" style="width: 114px;">
                    <asp:Literal runat="server" ID="ltrLabel1"></asp:Literal></td>
                <td class="h_t" style="width: 114px;">
                    <asp:Literal runat="server" ID="ltrLabel2"></asp:Literal></td>
                <td class="h_t" style="width: 114px;">
                    <asp:Literal runat="server" ID="ltrLabel3"></asp:Literal></td>
                <td class="h_t" style="width: 114px;">
                    <asp:Literal runat="server" ID="ltrLabel4"></asp:Literal></td>
                <td class="h_t_tt">
                    Tăng trưởng</td>
            </tr>    
</table>
</div>
<div style="overflow:hidden;width:100%;border-bottom:solid 1px #cecece">
<asp:Repeater runat="server" ID="rpData" OnItemDataBound="rpData_ItemDataBound">
    <HeaderTemplate>
        <div style="overflow:auto;height:500px;width:100%" id="divTableContent">            
            <table id="tableContent" cellpadding="0" cellspacing="0" width="100%" style="border-top: solid 1px #e6e6e6;border-left:solid 1px #cccccc;border-bottom:solid 1px #cccccc">
    </HeaderTemplate>
    <ItemTemplate>
        <tr class="r_item">
            <td style="<%# Eval("Format") %>;width:36%" class="b_r_c"><%# Eval("TenChiTieu") %></td>
            <td class="b_r_c" align="right" style="width:14%;padding:4px">&nbsp;<asp:Literal runat="server" ID="ltrQ1"></asp:Literal></td>
            <td class="b_r_c" align="right" style="width:14%;padding:4px">&nbsp;<asp:Literal runat="server" ID="ltrQ2"></asp:Literal></td>
            <td class="b_r_c" align="right" style="width:14%;padding:4px">&nbsp;<asp:Literal runat="server" ID="ltrQ3"></asp:Literal></td>
            <td class="b_r_c" align="right" style="width:14%;padding:4px">&nbsp;<asp:Literal runat="server" ID="ltrQ4"></asp:Literal></td>
            <td class="b_r_c" align="right"><asp:Literal runat="server" ID="ltrChart"></asp:Literal></td>
        </tr>
    </ItemTemplate>
    <AlternatingItemTemplate>
        <tr class="r_item_a">
            <td style="<%# Eval("Format") %>;width:36%" class="b_r_c"><%# Eval("TenChiTieu") %></td>
            <td class="b_r_c" style="width:14%;padding:4px" align="right">&nbsp;<asp:Literal runat="server" ID="ltrQ1"></asp:Literal></td>
            <td class="b_r_c" style="width:14%;padding:4px" align="right">&nbsp;<asp:Literal runat="server" ID="ltrQ2"></asp:Literal></td>
            <td class="b_r_c" style="width:14%;padding:4px" align="right">&nbsp;<asp:Literal runat="server" ID="ltrQ3"></asp:Literal></td>
            <td class="b_r_c" style="width:14%;padding:4px" align="right">&nbsp;<asp:Literal runat="server" ID="ltrQ4"></asp:Literal></td>
            <td class="b_r_c" align="right"><asp:Literal runat="server" ID="ltrChart"></asp:Literal></td>
        </tr>
    </AlternatingItemTemplate>
    <FooterTemplate>
            </table>
            <div style="overflow:hidden;width:99%;background-color:White;border-bottom:solid 1px #cecece;border-left:solid 1px #cecece;border-right:solid 1px #cecece;">
                <div style="float:right;">                 
                 <a href="http://cafef.vn" ><img border="0" src="http://cafef3.vcmedia.vn/images/BCTCFull/Cafef.jpg" /></a>
                 <%--<a href="http://vincomsc.com.vn" ><img border="0" src="http://cafef3.vcmedia.vn/images/BCTCFull/vincomsc.jpg" /></a>--%>
                 </div>
                 <%--<div style="float:right;padding:10px;color:#555555;font-size:12px;font-family:arial">Chuyên mục hợp tác với công ty chứng khoán Vincom</div>--%>
            </div>
        </div>
    </FooterTemplate>
</asp:Repeater>    
</div>
        
                                <!-- -->
                                
                            </div>
                        </div>
                        
                    </div>
            </div>
        </div>
        </div>

        </div>
        
    </form>
    
    <input type="hidden" id="hdSymbol" value="<%= Symbol %>" />
    <input type="hidden" id="hdNhom" value="<%= Nhom %>" />
    <input type="hidden" id="hdType" value="<%= Type %>" />
    <input type="hidden" id="hdIsQuy" value="<%= IsQuy %>" />
    <input type="hidden" id="hdQuy" value="<%= Quy %>" />
    <input type="hidden" id="hdYear" value="<%= Year %>" />
    <input type="hidden" id="hdDesign" value="<%= Design %>" />
   <%-- <script type="text/javascript" src="http://reporting.cafef.channelvn.net/js.js?dat123456"></script>
--%>
<script type="text/javascript" src="http://cafef3.vcmedia.vn/reporting/log.js"></script>
    <script type="text/javascript">
        var log_website = 'cafef_reporting';        
        Log_AssignValue("-2", "", "1117", "BCTCFull");
    </script>

    <script src="http://www.google-analytics.com/urchin.js" type="text/javascript"></script>

    <script type="text/javascript">
        _uacct = "UA-3070916-9";
        urchinTracker();
    </script>
    
<script type="text/javascript">
    var nhom = document.getElementById("hdNhom").value;
    var div = document.getElementById("divNhom"+nhom);
    var div_i = document.getElementById("divNhom"+nhom+"_i");
    var alink = document.getElementById("aNhom"+nhom);
    
//    var sb = document.getElementById("hdSymbol").value; //BMI, PVI, VNR
//    if(sb == 'STB' || sb == 'ACB' || sb == 'PVF' || sb == 'BMI' || sb == 'PVI' || sb == 'VNR')
//    {
//        document.getElementById('divNhom4').className = '';
//        document.getElementById('divNhom4').innerHTML = '';
//        document.getElementById('aNhom3').innerHTML = 'Lưu chuyển tiền tệ';
//    }
    
    var design = document.getElementById('hdDesign').value;
    if(design == "2")
    {
        document.getElementById('divNhom4').className = '';
        document.getElementById('divNhom4').innerHTML = '';
    } 
    else if(design == "1")
    {
        document.getElementById('divNhom3').className = 'n_t2_none';
        document.getElementById('divNhom3').innerHTML = '';
        document.getElementById('nc3').style.display = 'none';
    }
    else if(design == "3")
    {
        document.getElementById('divNhom4').className = '';
        document.getElementById('divNhom4').innerHTML = '';
        document.getElementById('aNhom3').innerHTML = 'Lưu chuyển tiền tệ';
    }
    
//    div.className = "cf_ResearchDataHistory_Tab1_Sel";

    if(nhom=="1")
    {
        div.className = "s_t1";
        div_i.className = "s_t1_i";
    }
    else
    {
        div.className = "s_t2";
        div_i.className = "s_t2_i";
    }

    alink.className = "SelectedTab";
    
    document.getElementById('<%= txtKeyword.ClientID %>').value = '<%= Symbol %>'
    document.getElementById('sOption').value = document.getElementById("hdType").value;
    document.getElementById('sQuy').value = document.getElementById("hdIsQuy").value;
    
    function rePre()
    {
        if(document.getElementById("hdIsQuy").value == "0")
        {
            var quy = document.getElementById("hdQuy").value;
            var nam = document.getElementById("hdYear").value;
            if(quy == 1)
            {
                quy = 4;
                nam = nam - 1;
            }
            else
                quy = quy - 1;
                
            redirectBc(
                    nhom,
                    document.getElementById("hdType").value,
                    document.getElementById("hdIsQuy").value,
                    quy,
                    nam);
        }
        else
        {
            var nam = document.getElementById("hdYear").value;
            nam = nam - 1;
            redirectBc(
                    nhom,
                    document.getElementById("hdType").value,
                    document.getElementById("hdIsQuy").value,
                    document.getElementById("hdQuy").value,
                    nam);
        }
    }
    
    function reNext()
    {
        if(document.getElementById("hdIsQuy").value == "0")
        {
            var quy = document.getElementById("hdQuy").value;
            var nam = document.getElementById("hdYear").value;
            if(quy == 4)
            {
                quy = 1;
                nam = parseInt(nam) + 1;
            }
            else
                quy = parseInt(quy) + 1;
                
            redirectBc(
                    nhom,
                    document.getElementById("hdType").value,
                    document.getElementById("hdIsQuy").value,
                    quy,
                    nam);
        }
        else
        {
            var nam = document.getElementById("hdYear").value;
            nam = parseInt(nam) + 1;
            redirectBc(
                    nhom,
                    document.getElementById("hdType").value,
                    document.getElementById("hdIsQuy").value,
                    document.getElementById("hdQuy").value,
                    nam);
        }
    }
    
    function redirectBCTC(nh)
    {
        redirectBc(
                nh,
                document.getElementById("hdType").value,
                document.getElementById("hdIsQuy").value,
                document.getElementById("hdQuy").value,
                document.getElementById("hdYear").value);
    }
    function redirectChange(target)
    {
        redirectBc(
                nhom,
                target.value,
                document.getElementById("hdIsQuy").value,
                document.getElementById("hdQuy").value,
                document.getElementById("hdYear").value);
    }
    function redirectFromSelect(target)
    {
        window.location = "/BCTCFull/BCTCFull.aspx?symbol="+document.getElementById("hdSymbol").value + 
            "&nhom=" + nhom +
            "&type=" + document.getElementById("hdType").value +  
            "&quy=" + target.value;

    }
    
    function redirectBc(nh,type,isquy,q,y)
    {
    
        window.location = "/BCTCFull/BCTCFull.aspx?symbol="+document.getElementById("hdSymbol").value + 
            "&nhom=" + nh +
            "&type=" + type + 
            "&y=" + y + 
            "&q=" + q + 
            "&quy=" + isquy;
    }
    function redirectFromSymbol(symbol,nh,type,isquy,q,y)
    {
        window.location = "/BCTCFull/BCTCFull.aspx?symbol="+symbol + 
            "&nhom=" + nh +
            "&type=" + type + 
            "&y=" + y + 
            "&q=" + q + 
            "&quy=" + isquy;
    }
</script>
    
<script type="text/javascript">
    var TextBox_KeywordId = '<%=txtKeyword.ClientID%>';
    $().ready(function() {
        $('#' + TextBox_KeywordId).autocomplete(oc, {
            minChars: 1,
            delay: 10,
            width: 400,
            matchContains: true,
            autoFill: false,
            BCTC:true,            
            formatItem: function(row) {
                return row.c + " - " + row.m + "@" + row.o;
                //return row.m + "@" + row.o;
            },
            formatResult: function(row) {
                return row.c;
                //return row.m;
            },
            NextFocusControlId: '<%=btSearch.ClientID %>'
        });
    });
    
        if(document.getElementById("divTop").clientHeight >= 600)
            document.getElementById("divTableContent").style.height = (document.getElementById("divTop").clientHeight - 130) + "px";            

</script>
</body>
</html>