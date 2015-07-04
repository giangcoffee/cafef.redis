<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/StockMain.Master" CodeBehind="TienDoBDS.aspx.cs" Inherits="CafeF.Redis.Page.TienDoBDS" %>

<%@ Register Src="UserControls/Footer/ucFooterv2.ascx" TagName="ucFooter" TagPrefix="uc3" %>
<%@ Register Src="UserControls/Header/ucHeaderv2.ascx" TagName="ucHeader" TagPrefix="uc1" %>
<%@ Register Src="UserControl/NewsTitleHot/ucNewsTitleHot.ascx" TagName="ucNewsTitleHot" TagPrefix="uc3" %>
<%@ Register Src="UserControl/StockView/Search.ascx" TagName="ucSearch" TagPrefix="uc4" %>
<%@ Register Src="UserControl/StockView/AnalyseReports.ascx" TagName="ucAnalyseReports" TagPrefix="uc5" %>
<%@ Register Src="UserControl/StockView/BusinessPlan.ascx" TagName="ucBusinessPlan" TagPrefix="uc6" %>
<%@ Register Src="UserControl/StockView/TradeHistory.ascx" TagName="ucTradeHistory" TagPrefix="uc8" %>
<%@ Register Src="UserControl/StockView/InSameCategory.ascx" TagName="ucInSameCategory" TagPrefix="uc11" %>
<%@ Register Src="UserControl/StockView/SameEPS_PE.ascx" TagName="SameEPS_PE" TagPrefix="uc12" %>

<%@ Register src="UserControl/TienDoBDS/BDSDuanChitiet.ascx" tagname="BDSDuanChitiet" tagprefix="uc2" %>
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
                                CÁC DỰ ÁN <%= (Request["Symbol"]??"").ToUpper() %> ĐANG THỰC HIỆN</h2>                          
                           
                            <uc2:BDSDuanChitiet ID="BDSDuanChitiet1" runat="server" />
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
