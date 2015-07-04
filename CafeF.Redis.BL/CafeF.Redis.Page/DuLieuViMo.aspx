<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/StockMain.Master" CodeBehind="DuLieuViMo.aspx.cs" Inherits="CafeF.Redis.Page.DuLieuViMo" %>

<%@ Register Src="UserControls/Footer/ucFooterv2.ascx" TagName="ucFooter" TagPrefix="uc2" %>
<%@ Register Src="UserControls/Header/ucHeaderv2.ascx" TagName="ucHeader" TagPrefix="uc1" %>
<%@ Register Src="UserControl/NewsTitleHot/ucNewsTitleHot.ascx" TagName="ucNewsTitleHot" TagPrefix="uc3" %>
<%@ Register src="UserControl/Footer/ucPartner.ascx" tagname="Partner" tagprefix="uc" %>

<asp:Content EnableViewState="False" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ucHeader ID="UcHeader1" runat="server" EnableViewState="false"></uc1:ucHeader>
    <div id="pagewrap">
        <div id="container" class="clearfix">   
            <div id="macktheodoi">
            </div>
            <div style="overflow: hidden; padding-bottom: 10px">
                <script type="text/javascript" src="http://admicro1.vcmedia.vn/ads_codes/ads_box_96.ads"></script>
            </div>
            <div class="botop">
                <div style="padding-top: 10px;">
                    <%--<uc3:ucNewsTitleHot ID="ucNewsTitleHot1" runat="server" />--%>
                </div>
                <div class="contentMain">
                    <div id="dlvmf" style="width:220px;padding:0px 15px 15px 15px;float:left;">
                        <div class="kehoachkd">
                            <h3 class="cattitle noborder">TĂNG TRƯỞNG KINH TẾ</h3>
                            <ul>
                                <li class="clearfix">
                                    <a id="a_gdp" runat="server" href="/dulieuvimo/gdp.chn">GDP</a>
                                </li>
                                <li class="clearfix">
                                    <a id="a_slcn" runat="server" href="/dulieuvimo/san-luong-cong-nghiep.chn">Sản lượng công nghiệp</a>
                                </li>
                                <li class="clearfix">
                                    <a id="a_tmbl" runat="server" href="/dulieuvimo/tong-muc-ban-le.chn">Tổng mức bán lẻ</a>
                                </li>
                            </ul>
                        </div>
                        <div class="kehoachkd">
                            <h3 class="cattitle noborder">GIÁ CẢ VÀ LÃI SUẤT</h3>
                            <ul>
                                <li class="clearfix">
                                    <a id="a_cpisau" runat="server" href="/dulieuvimo/cpi-sau-11-2009.chn">CPI sau 11/2009</a>
                                </li>
                                <li class="clearfix">
                                    <a id="a_cpitruoc" runat="server" href="/dulieuvimo/cpi-truoc-11-2009.chn">CPI trước 11/2009</a>
                                </li>                               
                            </ul>
                        </div>
                        <div class="kehoachkd">
                            <h3 class="cattitle noborder">XUẤT NHẬP KHẨU</h3>
                            <ul>
                                <li class="clearfix">
                                    <a id="a_xnk" runat="server" href="/dulieuvimo/xuat-nhap-khau.chn">Xuất nhập khẩu</a>
                                </li>                                
                            </ul>
                        </div>
                        <div class="kehoachkd">
                            <h3 class="cattitle noborder">ĐẦU TƯ</h3>
                            <ul>
                                <li class="clearfix">
                                    <a id="a_fdi" runat="server" href="/dulieuvimo/fdi.chn">FDI</a>
                                </li>
                                <li class="clearfix">
                                    <a id="a_nsnn" runat="server" href="/dulieuvimo/dau-tu-tu-nsnn.chn">Đầu tư từ NSNN</a>
                                </li>                                
                            </ul>
                        </div>
                    </div>
                    <div style="padding:15px 15px 15px 15px;float:left;width:698px;border-left:solid 1px #EEE">
                        <asp:PlaceHolder runat="server" ID="pldContent"></asp:PlaceHolder>
                        <br />
                        <div class="dltlonote">(*) Khuyến cáo: Dữ liệu được tổng hợp từ các nguồn đáng tin cậy, có giá trị tham khảo với các nhà đầu tư. Tuy nhiên, chúng tôi không chịu trách nhiệm trước mọi rủi ro nào do sử dụng lại các dữ liệu này.</div>
                    </div>
                    <div class="totop">
                        <a href="#">[ Về đầu trang ]</a>
                    </div>
                    <div class="doitac clearfix">
                        <uc:Partner runat="server" EnableViewState="false" />
                    </div>
                    <div style="background-color: #fff; padding-top: 10px; margin: 0 10px">
                        <uc2:ucFooter ID="UcFooter1" runat="server" EnableViewState="false"></uc2:ucFooter>
                    </div>
                </div>
                <div class="bobottom">
                </div>
            </div>
        </div>
        <!-- //Pagewrap background -->
    </div>
    
    <script type="text/javascript">
    function openw(url)
    {
	     var width  = 900;
         var height = 700;
         var left   = (screen.width  - width)/2;
         var top    = (screen.height - height)/2;
         var params = 'width='+width+', height='+height;
         params += ', top='+top+', left='+left;
         params += ', directories=0';
         params += ', location=0';
         params += ', menubar=0';
         params += ', resizable=1';
         params += ', scrollbars=1';
         params += ', status=0';
         params += ', toolbar=0';
         newwin=window.open(url,'News', params);
         if (window.focus) {newwin.focus()}
         return false;
    }
</script>
</asp:Content>