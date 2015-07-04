<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KenhF.aspx.cs" Inherits="CafeF.Redis.Page.KenhF" MasterPageFile="~/MasterPage/SoLieu.Master" %>
<%@ Import Namespace="CafeF.Redis.BL"%>
<%@ Register src="UserControl/LichSuKien/LichSuKien_TomTat.ascx" tagname="LichSuKien_TomTat" tagprefix="uc1" %>
<%@ Register src="UserControl/LichSuKien/LichSuKien_TomTat_v2.ascx" tagname="LichSuKien_TomTat_v2" tagprefix="uc1" %>
<%@ Register Src="UserControl/StockView/Search.ascx" TagName="ucSearch" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <link type="text/css" href="http://cafef3.vcmedia.vn/v2/style/dulieu.css" rel="Stylesheet" />
<div id="container" class="clearfix">
      <div id="content">
        <div>
            <div style="overflow: hidden;">
                <div class="cf_WCBox">
                    <%-- <div class="cf_BoxHeader">
                        <div>
                        </div>
                    </div>--%>
                    <div class="cf_BoxContent" style="background-color: #fff;">
                        <div style="overflow: hidden; height: 27px; padding: 8px 0px 0px 10px; font-weight: bold; border-bottom: solid 1px #dadada;">
                            <div style="float: left; text-align: left; font-size: 15px;">
                                THỊ TRƯỜNG NGÀY
                                <asp:Literal EnableViewState="false" ID="ltrLastUpdate" runat="server"></asp:Literal></div>
                            <div style="float: left; text-align: right; font-size: 14px; padding-left: 95px;">
                                <img align="top" src="http://cafef3.vcmedia.vn/images/new.gif" alt="" /><a class="Black_Bold_Link" href="/TraCuuLichSu2/TraCuu.chn">Xem toàn thị trường theo phiên</a></div>
                        </div>
                        <div style="overflow: hidden; margin-top: 8px; font-size: 12px;" id="centerindex">
                            <div style="position: relative; padding-left: 5px; float: left; width: 310px; text-align: center; border-right: solid 1px #dadada;">
                                <h1 title="Diễn biến chỉ số Vn-index">
                                    <div style="background-image: url('http://cafef3.vcmedia.vn/images/images/index_bullet.gif'); text-align: left; margin-left: 5px; background-position: 0px 5px; background-repeat: no-repeat; padding-left: 10px; font-size: 12px; font-weight: normal;">
                                        <table border="0" cellpadding="2" cellspacing="0" width="100%" style="line-height:150%;">
                                            <tr>
                                                <td style="font-size: 17px; padding-bottom:10px;">
                                                    HoSE</td>
                                                <td style="font-size: 10px; color: #45769c; text-align: right; padding-right: 20px;">
                                                    Trạng thái thị trường:&nbsp;<asp:Label EnableViewState="false" ID="lblHoSE_State" Font-Bold="true" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="font-weight: bold; color: #004370">
                                                    VN - Index:
                                                    <asp:Literal runat="server" ID="ltrVnIndex" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    KLGD:
                                                    <span class="ivl_1" style="font-weight:bold; color: #07499a;"><asp:Literal runat="server" ID="ltrVnVol" /></span> cp &nbsp;-&nbsp; GTGD: <span class="idv_1" style="font-weight:bold; color: #07499a;"><asp:Literal runat="server" ID="ltrVnVal" /></span> tỷ VNĐ</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    GDNN:
                                                    <span class="idf_1" style="font-weight:bold; color: #07499a;"><asp:Literal runat="server" ID="ltrVnForeignVol" /></span> cp</td>
                                            </tr>
                                             <tr>
                                                <td colspan="2" align="center">
                                                    <img alt="" border="0" src="http://cafef3.vcmedia.vn/images/SummaryUp.png" alt="" />&nbsp;<span class="up_1"><asp:Literal runat="server" ID="ltrVnUp" /></span>(<span style="color: #E200FF;" class="ceiling_1"><asp:Literal runat="server" ID="ltrVnCeiling" /></span>)&nbsp;<img border="0" src="http://cafef3.vcmedia.vn/images/SummaryNo.png" alt="" />&nbsp;<span class="normal_1"><asp:Literal runat="server" ID="ltrVnNormal" /></span>&nbsp;<img border="0" src="http://cafef3.vcmedia.vn/images/SummaryDown.png" alt="" />&nbsp;<span class="down_1"><asp:Literal runat="server" ID="ltrVnDown" /></span>(<span style="color: #66CCFF;" class="floor_1"><asp:Literal runat="server" ID="ltrVnFloor" /></span>)
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="right" style="padding-right: 20px;">
                                                    <span class="NoteText" style="font-size: 10px; cursor: hand; cursor: pointer;" onmouseover="ShowDetailBox();" onmouseout="HideDetailBox();">Chi tiết</span>
                                                    <div onmouseover="ShowDetailBox();" onmouseout="HideDetailBox();" id="divVNIndexDetail" style="text-align: left; position: absolute; width: 280px; z-index: 10000; height: 130px; background-color: #ffffff; border: solid 1px #004370; padding: 3px; display: none; -moz-opacity: 0.9; opacity: 0.9; filter: alpha(opacity=90);">
                                                        <table visible="false" id="tblVNIndex1" runat="server" border="0" cellpadding="2" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td colspan="2" style="font-weight: bold; color: #004370">
                                                                    Đợt 1:
                                                                    <asp:Label EnableViewState="false" ID="lblVNIndex1" runat="server" ForeColor="#666666" Font-Bold="true" Text=""></asp:Label><asp:Image EnableViewState="false" ID="imgVNIndex1" Style="margin-left: 3px; margin-right: 3px;" ImageAlign="AbsMiddle" ImageUrl="http://cafef3.vcmedia.vn/images/btdown.gif" runat="server" /><asp:Label EnableViewState="false" ID="lblVNIndexChange1" CssClass="Index_Down" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    KLGD:
                                                                    <asp:Label EnableViewState="false" ID="lblHoKLGD1" Style="color: #07499a" runat="server" Text=""></asp:Label> cp &nbsp;-&nbsp; GTGD:
                                                                    <asp:Label EnableViewState="false" ID="lblHoGTGD1" Style="color: #07499a" runat="server" Text=""></asp:Label> tỷ VNĐ</td>
                                                            </tr>
                                                        </table>
                                                        <table visible="false" style="margin-top: 5px;" id="tblVNIndex2" runat="server" border="0" cellpadding="2" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td colspan="2" style="font-weight: bold; color: #004370">
                                                                    Đợt 2:
                                                                    <asp:Label EnableViewState="false" ID="lblVNIndex2" runat="server" ForeColor="#666666" Font-Bold="true" Text=""></asp:Label><asp:Image EnableViewState="false" ID="imgVNIndex2" Style="margin-left: 3px; margin-right: 3px;" ImageAlign="AbsMiddle" ImageUrl="http://cafef3.vcmedia.vn/images/btdown.gif" runat="server" /><asp:Label EnableViewState="false" ID="lblVNIndexChange2" CssClass="Index_Down" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    KLGD:
                                                                    <asp:Label EnableViewState="false" ID="lblHoKLGD2" Style="color: #07499a" runat="server" Text=""></asp:Label>
                                                                    cp &nbsp;-&nbsp; GTGD:
                                                                    <asp:Label EnableViewState="false" ID="lblHoGTGD2" Style="color: #07499a" runat="server" Text=""></asp:Label>
                                                                    tỷ VNĐ</td>
                                                            </tr>
                                                        </table>
                                                        <table visible="false" style="margin-top: 5px;" id="tblVNIndex3" runat="server" border="0" cellpadding="2" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td colspan="2" style="font-weight: bold; color: #004370">
                                                                    Đợt 3:
                                                                    <asp:Label EnableViewState="false" ID="lblVNIndex3" runat="server" ForeColor="#666666" Font-Bold="true" Text=""></asp:Label><asp:Image EnableViewState="false" ID="imgVNIndex3" Style="margin-left: 3px; margin-right: 3px;" ImageAlign="AbsMiddle" ImageUrl="http://cafef3.vcmedia.vn/images/btdown.gif" runat="server" /><asp:Label EnableViewState="false" ID="lblVNIndexChange3" CssClass="Index_Down" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    KLGD:
                                                                    <asp:Label EnableViewState="false" ID="lblHoKLGD3" Style="color: #07499a" runat="server" Text=""></asp:Label> cp &nbsp;-&nbsp; GTGD:
                                                                    <asp:Label EnableViewState="false" ID="lblHoGTGD3" Style="color: #07499a" runat="server" Text=""></asp:Label> tỷ VNĐ</td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="margin-top: 5px;">
                                        <a href="/Thi-truong-niem-yet/Bieu-do-ky-thuat/EPS-VNINDEX-2.chn">
                                            <div style="background: url('<%= Utils.GetDuLieuChartLink() %>') no-repeat scroll 0pt 0pt transparent;" id="imgHoChart_Day">&nbsp;</div>
                                            <img border="0" alt="Biểu đồ giá trị và khối lượng giao dịch tại sàn HoSE" style="display: none; z-index: 0;" id="imgHoChart" src="http://cafef4.vcmedia.vn/<%=ChartUpdatedDate %>/ho_7days.png" />
<%--                                            <script type="text/javascript">
                                    var now = new Date();
                                    document.write('<img border="0" alt="Biểu đồ trong ngày"  style="display: none; z-index: 0; margin-top: 35px; margin-bottom: 20px;" id="imgHoChart_Day" alt="" src="/ChartIndex/Chart.aspx?center=1&width=260&height=165&ran=" />');
                                    
                                    document.write('<img border="0" alt="Biểu đồ giá trị và khối lượng giao dịch tại sàn HoSE trong 7 ngày" style="display: none; z-index: 0;" id="imgHoChart_7days" alt="" src="/FinanceStatementData/Ho_7days.png?upd=' + now.getTime() + '" />');
                                    document.write('<img border="0" alt="Biểu đồ giá trị và khối lượng giao dịch tại sàn HoSE trong 1 tháng" style="display: none; z-index: 0;" id="imgHoChart_1month" alt="" src="/FinanceStatementData/Ho_1month.png?upd=' + now.getTime() + '" />');
                                    document.write('<img border="0" alt="Biểu đồ giá trị và khối lượng giao dịch tại sàn HoSE trong 3 tháng" style="z-index: 0;" id="imgHoChart_3months" alt="" src="/FinanceStatementData/Ho_3months.png?upd=' + now.getTime() + '" />');
                                    document.write('<img border="0" alt="Biểu đồ giá trị và khối lượng giao dịch tại sàn HoSE trong 6 tháng" style="display: none; z-index: 0;" id="imgHoChart_6months" alt="" src="/FinanceStatementData/Ho_6months.png?upd=' + now.getTime() + '" />');
                                    document.write('<img border="0" alt="Biểu đồ giá trị và khối lượng giao dịch tại sàn HoSE trong 1 năm" style="display: none; z-index: 0;" id="imgHoChart_1year" alt="" src="/FinanceStatementData/Ho_1year.png?upd=' + now.getTime() + '" />');
                                            </script>--%>

                                        </a>
                                    </div>
                                    <div class="HoChart" style="text-align: center; width: 100%; text-align: right; font-size: 10px; color: #999999;">
                                        Đơn vị KL: 1,000,000 CP&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                    <div class="HoChart" style="text-align: center; width: 100%; font-size: 12px; font-weight: normal;">
                                        <div>
                                            <a class="Chart_InActived" id="lnkHoChart_Day" href="javascript:void(0)" onclick="ChangeImage('Ho', 'Day');">Trong ngày</a> | <a class="Chart_InActived" id="lnkHoChart_7days" href="javascript:void(0)" onclick="ChangeImage('Ho', '7days');">1 tuần</a> | <a class="Chart_InActived" id="lnkHoChart_1month" href="javascript:void(0)" onclick="ChangeImage('Ho', '1month');">1 tháng</a> | <a class="Chart_InActived" id="lnkHoChart_3months" href="javascript:void(0)" onclick="ChangeImage('Ho', '3months');">3 tháng</a> | <a class="Chart_InActived" id="lnkHoChart_6months" href="javascript:void(0)" onclick="ChangeImage('Ho', '6months');">6 tháng</a> | <a class="Chart_InActived" id="lnkHoChart_1year" href="javascript:void(0)" onclick="ChangeImage('Ho', '1year');">1 năm</a>
                                        </div>
                                        <div style="margin: 5px 20px 0px 27px;">
                                            <div style="overflow: hidden; float: left;">
                                                <a class="DarkBlue_Bold_Link" href="/Thi-truong-niem-yet/Bieu-do-ky-thuat/EPS-VNINDEX-2.chn">Xem đồ thị kỹ thuật</a></div>
                                            <div style="overflow: hidden; float: right;">
                                                <a target="_blank" class="DarkBlue_Bold_Link" href="/Lich-su-giao-dich-VNINDEX-1.chn">Tra cứu DL lịch sử</a></div>
                                        </div>
                                    </div>
                                </h1>
                            </div>
                            <div style="position: relative; float: right; width: 305px; text-align: center;">
                                <h1 title="Diễn biến chỉ số HNX-index">
                                    <div style="background-image: url('http://cafef3.vcmedia.vn/images/images/index_bullet.gif'); text-align: left; margin-left: 5px; background-position: 0px 5px; background-repeat: no-repeat; padding-left: 10px; font-size: 12px; font-weight: normal;">
                                        <table border="0" cellpadding="2" cellspacing="0" width="100%" style="line-height:150%;">
                                            <tr>
                                                <td style="font-size: 17px; padding-bottom:10px;">
                                                    HNX</td>
                                                <td style="font-size: 10px; color: #45769c; text-align: right; padding-right: 20px;">
                                                    Trạng thái thị trường:&nbsp;<asp:Label EnableViewState="false" ID="lblHaSTC_State" Font-Bold="true" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="font-weight: bold; color: #004370">
                                                    HNX - Index:
                                                    <asp:Literal runat="server" ID="ltrHnxIndex" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    KLGD:
                                                    <span class="ivl_2" style="font-weight:bold; color: #07499a;"><asp:Literal runat="server" ID="ltrHnxVolume" /></span> cp &nbsp;-&nbsp; GTGD: <span class="idv_2" style="font-weight:bold; color: #07499a;"><asp:Literal runat="server" ID="ltrHnxValue" /></span> tỷ VNĐ</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    GDNN:
                                                    <span class="idf_2" style="font-weight:bold; color: #07499a;"><asp:Literal runat="server" ID="ltrHnxForeignVol" /></span> cp</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                <img alt="" border="0" src="http://cafef3.vcmedia.vn/images/SummaryUp.png" alt="" />&nbsp;<span class="up_2"><asp:Literal runat="server" ID="ltrHnxUp" /></span>(<span style="color: #E200FF;" class="ceiling_2"><asp:Literal runat="server" ID="ltrHnxCeiling" /></span>)&nbsp;<img border="0" src="http://cafef3.vcmedia.vn/images/SummaryNo.png" alt="" />&nbsp;<span class="normal_2"><asp:Literal runat="server" ID="ltrHnxNormal" /></span>&nbsp;<img border="0" src="http://cafef3.vcmedia.vn/images/SummaryDown.png" alt="" />&nbsp;<span class="down_2"><asp:Literal runat="server" ID="ltrHnxDown" /></span>(<span style="color: #66CCFF;" class="floor_2"><asp:Literal runat="server" ID="ltrHnxFloor" /></span>)
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="margin-top: 5px;">
                                        <a href="/Thi-truong-niem-yet/Bieu-do-ky-thuat/EPS-HNX-2.chn">
<div style="background: url('<%= Utils.GetDuLieuChartLink() %>') no-repeat scroll -260px 0pt transparent;" id="imgHaChart_Day">&nbsp;</div>
<img border="0" alt="Biểu đồ giá trị và khối lượng giao dịch tại sàn HNX"  style="display: none; z-index: 0;" id="imgHaChart" alt="" src="http://cafef4.vcmedia.vn/<%=ChartUpdatedDate %>/ha_7days.png" />
                                           <%-- <script type="text/javascript">
document.write('<img border="0" alt="Biểu đồ trong ngày"  style="display: none; z-index: 0; margin-top: 35px; margin-bottom: 20px;" id="imgHaChart_Day" alt="" src="/ChartIndex/Chart.aspx?center=2&width=260&height=165&ran=" />');
                                                                        
document.write('<img border="0" alt="Biểu đồ giá trị và khối lượng giao dịch tại sàn HNX trong 7 ngày"  style="display: none; z-index: 0;" id="imgHaChart_7days" alt="" src="/FinanceStatementData/Ha_7days.png?upd=' + now.getTime() + '" />');
                                    document.write('<img border="0" alt="Biểu đồ giá trị và khối lượng giao dịch tại sàn HNX trong 1 tháng"  style="display: none; z-index: 0;" id="imgHaChart_1month" alt="" src="/FinanceStatementData/Ha_1month.png?upd=' + now.getTime() + '" />');
                                    document.write('<img border="0" alt="Biểu đồ giá trị và khối lượng giao dịch tại sàn HNX trong 3 tháng"  style="z-index: 0;" id="imgHaChart_3months" alt="" src="/FinanceStatementData/Ha_3months.png?upd=' + now.getTime() + '" />');
                                    document.write('<img border="0" alt="Biểu đồ giá trị và khối lượng giao dịch tại sàn HNX trong 6 tháng"  style="display: none; z-index: 0;" id="imgHaChart_6months" alt="" src="/FinanceStatementData/Ha_6months.png?upd=' + now.getTime() + '" />');
                                    document.write('<img border="0" alt="Biểu đồ giá trị và khối lượng giao dịch tại sàn HNX trong 1 năm"  style="display: none; z-index: 0;" id="imgHaChart_1year" alt="" src="/FinanceStatementData/Ha_1year.png?upd=' + now.getTime() + '" />');
                                            </script>--%>

                                        </a>
                                    </div>
                                    <div class="HaChart" style="text-align: center; width: 100%; text-align: right; font-size: 10px; color: #999999;">
                                        Đơn vị KL: 1,000,000 CP&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                    <div class="HaChart" style="text-align: center; width: 100%; font-size: 12px; font-weight: normal;">
                                        <div>
                                            <a class="Chart_InActived" id="lnkHaChart_Day" href="javascript:void(0)" onclick="ChangeImage('Ha', 'Day');">Trong ngày</a> | <a class="Chart_InActived" id="lnkHaChart_7days" href="javascript:void(0)" onclick="ChangeImage('Ha', '7days');">1 tuần</a> | <a class="Chart_InActived" id="lnkHaChart_1month" href="javascript:void(0)" onclick="ChangeImage('Ha', '1month');">1 tháng</a> | <a class="Chart_InActived" id="lnkHaChart_3months" href="javascript:void(0)" onclick="ChangeImage('Ha', '3months');">3 tháng</a> | <a class="Chart_InActived" id="lnkHaChart_6months" href="javascript:void(0)" onclick="ChangeImage('Ha', '6months');">6 tháng</a> | <a class="Chart_InActived" id="lnkHaChart_1year" href="javascript:void(0)" onclick="ChangeImage('Ha', '1year');">1 năm</a>
                                        </div>
                                        <div style="margin: 5px 20px 0px 20px;">
                                            <div style="overflow: hidden; float: left;">
                                                <a class="DarkBlue_Bold_Link" href="/Thi-truong-niem-yet/Bieu-do-ky-thuat/EPS-HNX-2.chn">Xem đồ thị kỹ thuật</a></div>
                                            <div style="overflow: hidden; float: right;">
                                                <a target="_blank" class="DarkBlue_Bold_Link" href="http://cafef.vn/Lich-su-giao-dich-HNX-INDEX-1.chn">Tra cứu DL lịch sử</a></div>
                                        </div>
                                    </div>
                                </h1>
                            </div>
                        </div>
                        <div id="hiddenlist" style="margin: 8px 5px 0 5px; padding: 5px 0 10px 0; width: 620px; background-color: #fff; border: solid 1px #ccc; text-align:center">
                            <a href="#" style="font-size: 16px; text-decoration: none;">Xem danh sách các mã cổ phiếu biến động mạnh</a>
                            <style type="text/css">#hiddenlist a:hover{text-decoration:underline;}</style>
                            <script type="text/javascript">
                            $(document).ready(function(){
                                $('#hiddenlist a').click(function(e){
                                    e.preventDefault();
                                    $('#hiddenlist').hide(500);
                                    $('#liststock').show(500);
                                });
                            })
                            </script>

                        </div>
                        <div id="liststock" style="display: none; overflow: hidden; margin-top: 8px; padding-bottom: 10px; background-color: #fff; width: 630px;">
                            <div style="float: left; width: 290px; margin-left: 10px;">
                                <h1 title="10 mã cổ phiếu biến động mạnh nhất trên thị trường chứng khoán">
                                    <h1 visible="false" runat="server" class="h1title_white" id="h1TopSymbols"> 
                                    </h1>
                                    <div id="CafeF_TopStockSymbol">
                                    </div>
                                </h1>
                            </div>
                            <div style="float: right; width: 290px; margin-right: 10px;">
                                <h1 title="10 công ty có chỉ số tốt nhất">
                                    <h1 visible="false" runat="server" class="h1title_white" id="h1TopCompany"></h1>
                                    <div id="CafeF_Top_PE_EPS"></div>
                                </h1>
                            </div>
                        </div>
                    </div>
                    <%-- <div class="cf_BoxFooter">
                        <div>
                        </div>
                    </div>--%>
                </div>
            </div>
             
            <div style="margin-top: 8px; text-align: left">
                <h1 title="Dữ liệu công ty niêm yết trên thị trường chứng khoán Việt Nam">
                    <div id="CafeF_DSCongtyNiemyet">
                    </div>
                </h1>
            </div>
        </div>
      </div><!-- //column 1 -->
      <div id="sidebar">
      	<uc4:ucSearch ID="ucSearch" runat="server" EnableViewState=false></uc4:ucSearch>
				
        <div class="congcudl">
        	<h3>Công cụ dữ liệu</h3>
          <ul>
            <li>
            	<a href="/dulieuvimo/gdp.chn">Dữ liệu kinh tế vĩ mô</a><img align="AbsMiddle" src="http://cafef3.vcmedia.vn/images/new.gif" border="0">
            </li>
          	<li>
            	<a href="/screener.aspx">Bộ lọc cổ phiếu</a>
              <p>Tìm kiếm cổ phiếu theo gần 30 tiêu chí khác nhau.</p>
            </li>
            <li>
            	<a href="/thong-ke.chn">Thống kê biến động giá cổ phiếu</a>
            </li>
            <li>
            	<a href="/Lich-su-giao-dich-Symbol-VNINDEX/Trang-1-0-tab-1.chn">Tra cứu dữ liệu lịch sử</a>
              <p>Lịch sử giá, lệnh đặt, nhà đầu tư nước ngoài, cổ đông lớn và cổ đông nội bộ ... </p>
            </li>
            <li>
            	<a href="/Thi-truong-niem-yet/Bieu-do-ky-thuat/EPS-VNINDEX-2.chn">Đồ thị phân tích kỹ thuật</a>
            </li>
            <li class="last">
            	<a href="/huong-dan-su-dung.chn">Hướng dẫn sử dụng</a>
            </li>
          </ul>
        </div><!-- // Công cụ dữ liệu -->
        
        <div class="lichsk">
        	<%--<uc1:LichSuKien_TomTat ID="LichSuKien_TomTat" runat="server" EnableViewState="false"/>--%>
        	<uc1:LichSuKien_TomTat_v2 ID="LichSuKien_TomTat1" runat="server" EnableViewState="false"/>
        </div>
      </div><!-- //sidebar -->
    </div>
<script type="text/javascript">
$(document).ready(function(){
    $('#hiddenlist a').click(function(e){
        e.preventDefault();
        $('#hiddenlist').hide(500);
        $('#liststock').show(500);
    });
})
</script>
<script type="text/javascript">
    function ShowDetailBox()
    {
        var VNIndexDetail = document.getElementById('divVNIndexDetail');
        VNIndexDetail.style.display = 'block';
        VNIndexDetail.style.left = '20px';
        if (document.all)
        {
            VNIndexDetail.style.top = '95px';
        }
    }
    function HideDetailBox()
    {
        var VNIndexDetail = document.getElementById('divVNIndexDetail');
        VNIndexDetail.style.display = 'none';
    }
    function ChangeImage(tradeCenter, duration)
    {
        if(duration=='Day'){
            document.getElementById('img' + tradeCenter + 'Chart_Day').style.display = '';
            document.getElementById('img' + tradeCenter + 'Chart').style.display = 'none';
        }else{
            document.getElementById('img' + tradeCenter + 'Chart').setAttribute('src','http://cafef4.vcmedia.vn/<%=ChartUpdatedDate %>/'+tradeCenter.toLowerCase()+'_'+duration.toLowerCase()+'.png');
            document.getElementById('img' + tradeCenter + 'Chart_Day').style.display = 'none';
            document.getElementById('img' + tradeCenter + 'Chart').style.display = '';            
        }
        
        document.getElementById('lnk' + tradeCenter + 'Chart_7days').className = 'Chart_InActived';
        document.getElementById('lnk' + tradeCenter + 'Chart_1month').className = 'Chart_InActived';
        document.getElementById('lnk' + tradeCenter + 'Chart_3months').className = 'Chart_InActived';
        document.getElementById('lnk' + tradeCenter + 'Chart_6months').className = 'Chart_InActived'; 
        document.getElementById('lnk' + tradeCenter + 'Chart_1year').className = 'Chart_InActived';
        document.getElementById('lnk' + tradeCenter + 'Chart_Day').className = 'Chart_InActived';        
        document.getElementById('lnk' + tradeCenter + 'Chart_' + duration).className = 'Chart_Actived';
    }
    ChangeImage('Ha', 'Day');
    ChangeImage('Ho', 'Day');
    </script>

    <script type="text/javascript" src="http://cafef3.vcmedia.vn/solieu/solieu6/TOP_10_CO_PHIEU.js?utp=1"></script>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/solieu/solieu6/TOP_10_CONG_TY.js?utp=1"></script>
    <script type="text/javascript" src="http://cafef3.vcmedia.vn/v2/scripts/DANH_SACH_CONG_TY_NIEM_YET.js"></script>
    <script type="text/javascript">        
        //Log_AssignValue("-1","","1001","ThiTruongNiemYet");
    </script>
    <input type="hidden" id="Log_AssignValue_NewsID" value="-1" />
    <input type="hidden" id="Log_AssignValue_NewsTitle" value="" />
    <input type="hidden" id="Log_AssignValue_CatID" value="1001" />
    <input type="hidden" id="Log_AssignValue_CatTitle" value="ThiTruongNiemYet" />
</asp:Content>