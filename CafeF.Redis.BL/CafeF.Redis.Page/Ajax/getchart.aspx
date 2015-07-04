<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="getchart.aspx.cs" Inherits="CafeF.Redis.Page.Ajax.getchart" %>
<%@ Import Namespace="CafeF.Redis.BL"%>
<div class="bd-up">
        <asp:Literal runat="server" ID="ltrDateTime" /></div>
    <div class="bd-vni" runat="server" id="divVnIndex">
        <div class="bd-text">
            VN-Index: <span id="vnindex">
                <asp:Literal runat="server" ID="ltrVnIndex" /></span><br />
            GTGD : <span id="vnindexval">
                <asp:Literal runat="server" ID="ltrVnIndexValue" /></span> tỷ VNĐ</div>
        <div style="width: 160px; height: 109px; background: url(<%= Utils.GetHeaderChartLink() %>) no-repeat scroll 0 0pt transparent;" id="headerchart1">
            &nbsp;</div>
    </div>
    <div class="bd-hnxi" runat="server" id="divHnxIndex">
        <div class="bd-text">
            HNX-Index: <span id="hnxindex">
                <asp:Literal runat="server" ID="ltrHnxIndex" /></span><br />
            GTGD : <span id="hnxindexval">
                <asp:Literal runat="server" ID="ltrHnxIndexValue" /></span> tỷ VNĐ</div>
        <div style="width: 160px; height: 109px; background: url(<%= Utils.GetHeaderChartLink() %>) no-repeat scroll -165px 0pt transparent;" id="headerchart2">
            &nbsp;</div>
    </div>