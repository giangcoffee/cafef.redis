<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/StockMain.Master" CodeBehind="Ceo.aspx.cs" Inherits="CafeF.Redis.Page.Ceo" EnableViewState="false" %>
<%@ Register Src="UserControls/Header/ucHeaderv2.ascx" TagName="ucHeader" TagPrefix="uc1" %>
<%@ Register Src="UserControls/Footer/ucFooterv2.ascx" TagName="ucFooter" TagPrefix="uc3" %>
<%@ Register Src="UserControl/NewsTitleHot/ucNewsTitleHot.ascx" TagName="ucNewsTitleHot" TagPrefix="uc2" %>
<%@ Register Src="UserControl/StockView/Search.ascx" TagName="ucSearch" TagPrefix="uc4" %>
<%@ Register src="UserControl/Ceo/CeoProfile.ascx" tagname="CeoProfile" tagprefix="uc5" %>
<%@ Register src="UserControl/Ceo/CeoPosition.ascx" tagname="CeoPosition" tagprefix="uc6" %>
<%@ Register src="UserControl/Ceo/CeoAsset.ascx" tagname="CeoAsset" tagprefix="uc7" %>
<%@ Register src="UserControl/Ceo/CeoRelation.ascx" tagname="CeoRelation" tagprefix="uc8" %>
<%@ Register src="UserControl/Ceo/CeoSchool.ascx" tagname="CeoSchool" tagprefix="uc9" %>
<%@ Register src="UserControl/Ceo/CeoProcess.ascx" tagname="CeoProcess" tagprefix="uc10" %>
<%@ Register src="UserControl/Ceo/CeoNews.ascx" tagname="CeoNews" tagprefix="uc11" %>
<%@ Register src="UserControl/Ceo/CeoAchievement.ascx" tagname="CeoAchievement" tagprefix="uc" %>
<%@ Register src="UserControl/Ceo/CeoIn.ascx" tagname="CeoIn" tagprefix="uc12" %>
<%@ Register src="UserControl/Ceo/CeoNote.ascx" tagname="CeoNote" tagprefix="uc" %>
<%@ Register src="UserControl/Footer/ucPartner.ascx" tagname="Partner" tagprefix="uc" %>

<asp:Content EnableViewState="False" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" type="text/css" href="http://cafef3.vcmedia.vn/v2/style/ceo.css" />
    <uc1:ucHeader ID="UcHeader" runat="server" EnableViewState="false"></uc1:ucHeader>
    <div id="pagewrap">
        <div id="container" class="clearfix">
            <div id="macktheodoi">
            </div>
            <div style="overflow: hidden; padding-bottom: 10px">
                <script type="text/javascript" src="http://admicro1.vcmedia.vn/ads_codes/ads_box_96.ads"></script>
            </div>
            <div class="botop">
                <div style="padding-top: 10px;">
                    <uc2:ucNewsTitleHot ID="ucNewsTitleHot1" runat="server" />
                </div>
                <div class="contentMain">
                    <div id="content">
                        <!--//Content-->
                        <div class="dulieu">
                            <div style="width:629px">
                                <!--//Profile-->
                                <uc5:CeoProfile ID="CeoProfile1" runat="server" />
                            </div>
                            <div style="width:629px;padding-top:0px;float:left; padding-left:15px;">
                                <!--//Position-->
                                <uc6:CeoPosition ID="CeoPosition1" runat="server" />
                                <!--//End-->
                                
                                <!--//Asset-->
                                <uc7:CeoAsset ID="CeoAsset1" runat="server" />
                                <!--//End-->
                                
                                <!--//Relation-->
                                <uc8:CeoRelation ID="CeoRelation1" runat="server" />
                                <!--//End-->
                                <uc:CeoNote ID="CeoNote1" runat="server" />
                                <!--//School-->
                                <uc9:CeoSchool ID="CeoSchool1" runat="server" />
                                <!--//End-->
                                <uc:CeoAchievement runat="server" />
                                <!--//Process-->
                                <uc10:CeoProcess ID="CeoProcess1" runat="server" />
                                <!--//End-->
                                
                                <!--//News-->
                                <uc11:CeoNews ID="CeoNews1" runat="server" />
                                <!--//End-->                                
                            </div>
                        </div>
                    </div>
                    <!-- //column 1 -->
                <div id="sidebar">
                    <!-- //search -->
                    <uc4:ucSearch ID="ucSearch" runat="server" EnableViewState="false"></uc4:ucSearch>
                    <!-- //End -->
                    
                    <!--//HDQT-->
                    <uc12:CeoIn ID="CeoIn1" runat="server" Name="HỘI ĐỒNG QUẢN TRỊ" Type="1" />
                    <!-- //End -->
                    <!--//BGD/KT-->
                    <uc12:CeoIn ID="CeoIn2" runat="server" Name="BAN GIÁM ĐỐC/KẾ TOÁN" Type="2" />
                    <!-- //End -->
                    <!--//BKS-->
                    <uc12:CeoIn ID="CeoIn3" runat="server" Name="BAN KIỂM SOÁT" Type="3" />
                    <!-- //End -->
                    
                    <%--<div style="padding: 8px 0">
                        <script type="text/javascript" src="http://admicro1.vcmedia.vn/ads_codes/ads_box_97.ads"></script>
                    </div>
                    <!--//Right-->
                    <div style="padding: 8px 0">
                        <script type="text/javascript" src="http://admicro1.vcmedia.vn/ads_codes/ads_box_344.ads"></script>
                    </div>--%>
                </div>
                <!-- //sidebar -->
                <div class="totop">
                        <a href="#">
                        [ Về đầu trang ]</a></div>
                    <div class="doitac clearfix">
                        <uc:Partner ID="Partner1" runat="server" EnableViewState="false" />
                    </div>
                    <div style="background-color: #fff; padding-top: 10px; margin: 0 10px">
                        <uc3:ucFooter ID="UcFooter" runat="server" EnableViewState="false"></uc3:ucFooter>
                    </div>
                </div>
                <div class="bobottom">
                </div>
            </div>
        </div>
        <!-- //Pagewrap background -->
    </div>
</asp:Content>