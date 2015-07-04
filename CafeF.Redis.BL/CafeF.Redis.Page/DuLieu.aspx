<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/StockMain.Master" CodeBehind="DuLieu.aspx.cs" Inherits="CafeF.Redis.Page.DuLieu" %>

<%@ Register Src="UserControls/Footer/ucFooterv2.ascx" TagName="ucFooter" TagPrefix="uc3" %>
<%@ Register Src="UserControls/Header/ucHeaderv2.ascx" TagName="ucHeader" TagPrefix="uc1" %>
<%@ Register Src="UserControl/NewsTitleHot/ucNewsTitleHot.ascx" TagName="ucNewsTitleHot" TagPrefix="uc3" %>
<%@ Register Src="UserControl/StockView/Search.ascx" TagName="ucSearch" TagPrefix="uc4" %>
<%@ Register Src="UserControl/StockView/AnalyseReports.ascx" TagName="ucAnalyseReports" TagPrefix="uc5" %>
<%@ Register Src="UserControl/StockView/BusinessPlan.ascx" TagName="ucBusinessPlan" TagPrefix="uc6" %>
<%@ Register Src="UserControl/StockView/SymbolNews.ascx" TagName="ucSymbolNews" TagPrefix="uc7" %>
<%@ Register Src="UserControl/StockView/TradeHistory.ascx" TagName="ucTradeHistory" TagPrefix="uc8" %>
<%@ Register Src="UserControl/StockView/CompanyInfo.ascx" TagName="CompanyInfo" TagPrefix="uc12" %>
<%@ Register Src="UserControl/StockView/TradeInfo.ascx" TagName="ucTradeInfo" TagPrefix="uc9" %>
<%@ Register Src="UserControl/StockView/TradeInfoHeader.ascx" TagName="ucTradeInfoHeader" TagPrefix="uc10" %>
<%@ Register Src="UserControl/StockView/InSameCategory.ascx" TagName="ucInSameCategory" TagPrefix="uc11" %>
<%@ Register Src="UserControl/StockView/SameEPS_PE.ascx" TagName="SameEPS_PE" TagPrefix="uc12" %>
<%@ Register src="UserControl/TienDoBDS/BDSDuanThamgia.ascx" tagname="BDSDuanThamgia" tagprefix="uc2" %>
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
                    <uc3:ucNewsTitleHot ID="ucNewsTitleHot1" runat="server" />
                </div>
                <div class="contentMain">
                    <div id="content">
                        <div class="dulieu">
                            <h2 class="cattitle">
                                Thông tin giao dịch</h2>
                            <!-- // CacChiSo -->
                            <uc10:ucTradeInfoHeader ID="ucTradeInfoHeader" runat="server" EnableViewState="false" />
                            <uc9:ucTradeInfo ID="ucTradeInfo" runat="server" EnableViewState="false" />
                            <!-- // End -->
                            <div class="tracuu clearfix">
                                <a href="/Lich-su-giao-dich-<%= (Request["Symbol"]??"").ToUpper() %>-1.chn" class="tc-ls">Tra cứu dữ liệu lịch sử</a> <a href="/Lich-su-giao-dich-<%= (Request["Symbol"]??"").ToUpper() %>-4.chn" class="tc-cd">Tra cứu GD cổ đông lớn &amp; cổ đông nội bộ</a>
                            </div>
                            <!-- //Tin tức - Sự kiện -->
                            <uc7:ucSymbolNews ID="ucSymbolNews" runat="server" EnableViewState="false"></uc7:ucSymbolNews>
                            <!-- end -->
                            <div class="hosocongty">
                                <uc12:CompanyInfo ID="CompanyInfo" runat="server" EnableViewState="false" />
                                <!-- // Hồ sơ công ty -->
                            </div>
                            <div><uc2:BDSDuanThamgia ID="BDSDuanThamgia1" runat="server" />
                            </div>
                            <%--<div class="dulieu-more clearfix">
      	                    <div class="baocaophantich">
      	                        <uc5:ucAnalyseReports ID="ucAnalyseReports" runat="server" EnableViewState=false></uc5:ucAnalyseReports>
      	                        <!-- //Báo cáo phân tích -->
                            </div>
                            <div class="kehoachkd">
                                <uc6:ucBusinessPlan ID="ucBusinessPlan" runat="server" EnableViewState=false></uc6:ucBusinessPlan>
                                <!-- //Kế hoạch kinh doanh -->
                            </div>
                        </div>--%>
                        </div>
                    </div>
                    <!-- //column 1 -->
                <div id="sidebar">
                    <!-- //search -->
                    <uc4:ucSearch ID="ucSearch" runat="server" EnableViewState="false"></uc4:ucSearch>
                    <!-- //End -->
                    <!-- //lichsu giao dich -->
                    <uc8:ucTradeHistory ID="ucTradeHistory" runat="server" EnableViewState="false"></uc8:ucTradeHistory>
                    <!-- end-->
                    <!-- //Kế hoạch kinh doanh -->
                    <uc6:ucBusinessPlan ID="ucBusinessPlan" runat="server" EnableViewState="false"></uc6:ucBusinessPlan>
                    <!-- end-->
                    <!-- //Báo cáo phân tích -->
                    <uc5:ucAnalyseReports ID="ucAnalyseReports" runat="server" EnableViewState="false"></uc5:ucAnalyseReports>
                    <!-- end-->
                    <!-- // Công ty cùng ngành -->
                    <uc11:ucInSameCategory ID="ucInSameCategory" runat="server" EnableViewState="false"></uc11:ucInSameCategory>
                    <!-- End-->
                    <!-- EPS, P/E tương đương -->
                    <uc12:SameEPS_PE ID="SameEPS_PE" runat="server" EnableViewState="false"></uc12:SameEPS_PE>
                    <!-- end -->
                </div>
                <!-- //sidebar -->
                <div class="totop">
                        <a href="#">[ Về đầu trang ]</a></div>
                    <div class="doitac clearfix">
                        <uc:Partner runat="server" EnableViewState="false" />
                    </div>
                    <div style="background-color: #fff; padding-top: 10px; margin: 0 10px">
                        <uc3:ucFooter ID="UcFooter1" runat="server" EnableViewState="false"></uc3:ucFooter>
                    </div>
                </div>
                <div class="bobottom">
                </div>
            </div>
        </div>
        <!-- //Pagewrap background -->
    </div>
</asp:Content>
