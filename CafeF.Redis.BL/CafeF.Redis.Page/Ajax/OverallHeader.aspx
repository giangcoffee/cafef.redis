<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OverallHeader.aspx.cs" Inherits="CafeF.Redis.Page.Ajax.OverallHeader" %>
<%@ Import Namespace="CafeF.BO"%>
<%@ Import Namespace="CafeF.BO.Utilitis"%>
<%@ Import Namespace="CafeF.Redis.BL" %>
<%@ Register Src="/UserControl/Header/ucHeaderIndex.ascx" TagPrefix="uc" TagName="HeaderIndex" %>
<%@ Register src="../UserControl/Header/ucHeaderIndex.ascx" tagname="ucHeaderIndex" tagprefix="uc1" %>
<div class="divgachmenucao">&nbsp;</div>
<div class="tinmoi" id="divTinMoi">
    <ul>
    <asp:repeater id="repListNews" runat="server">
        <ItemTemplate>
            <li >
                <div class="time">
                    <%# Eval("PublishDate").ToString()%>
                </div>
                <div class="title">
                    <a href="<%# Common.CheckBDS(Eval("Cat_ID").ToString()) ? string.Format(CafeF.BO.Utilitis.Const.BDS_SITE_LINK,Eval("News_ID"), Eval("Cat_ID"), UnicodeUtility.UnicodeToKoDauAndGach(Eval("News_Title").ToString())) : Eval("NewsUrl") %>" <%= ( Common.CheckBDS(Eval("Cat_ID").ToString())?"target='_blank'":"") %> title="<%# Eval("News_Title_Encode") %>"><%# Eval("News_Title") %></a>
                </div>
             </li>        
        </ItemTemplate>
    </asp:repeater>
</ul>
</div>
<div class="bieudo clearfix" style="margin-right:0;">
    <div id="headerchart">
       <%-- <div class="bd-up">
            <asp:literal runat="server" id="ltrDateTime" />
        </div>--%>
        <div class="bd-vni" runat="server" id="divVnIndex">
            <div class="bd-text">
                VN-Index: <span id="vnindex">                    
                <b class="idx_1"><asp:Literal runat="server" id="ltrVnIndex" /></b>&nbsp;<span class="idc_1"><asp:Literal runat="server" id="ltrVnIndexChange" /></span>&nbsp;<span class="idp_1"><asp:Literal runat="server" id="ltrVnIndexPercent" /></span></span>
                <br />
                GTGD : <span id="vnindexval" class="idv_1"><asp:literal runat="server" id="ltrVnIndexValue" /></span> tỷ VNĐ</div>
            <div style="background: url(<%= Utils.GetHeaderChartLink() %>) no-repeat scroll 0 0pt transparent;" id="headerchart1">&nbsp;</div>
        </div>
        <div class="bd-hnxi" runat="server" id="divHnxIndex">
            <div class="bd-text">
                HNX-Index: <span id="hnxindex">
                <b class="idx_2"><asp:Literal runat="server" id="ltrHnxIndex" /></b>&nbsp;<span class="idc_2"><asp:Literal runat="server" id="ltrHnxIndexChange" /></span>&nbsp;<span class="idp_2"><asp:Literal runat="server" id="ltrHnxIndexPercent" /></span></span>
                <br />
                GTGD : <span id="Span1" class="idv_2"><asp:literal runat="server" id="ltrHnxIndexValue" /></span> tỷ VNĐ</div>
            <div style="background: url(<%= Utils.GetHeaderChartLink() %>) no-repeat scroll -140px 0pt transparent;" id="headerchart2">&nbsp;</div>
        </div>
    </div>
</div>
<div style="display:none;">
    <asp:Literal runat="server" id="ltrError" />
</div>
<!--!@#!@#-->
<uc1:ucHeaderIndex ID="ucHeaderIndex1" runat="server" />
