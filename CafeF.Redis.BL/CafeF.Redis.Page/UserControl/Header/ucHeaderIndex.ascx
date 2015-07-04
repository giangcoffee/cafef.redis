<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucHeaderIndex.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.Header.ucHeaderIndex" %>
<div id="headerleftindex">
<asp:Repeater runat="server" EnableViewState="false" ID="repIndex">
    <ItemTemplate><p><b><%# Eval("Name") %></b> <span class="index"><%# Eval("Index") %></span> <%# Eval("Change") %> <span class="<%# Eval("ChangeClass") %>">(<%# Eval("ChangePercent") %>%)</span></p></ItemTemplate>
</asp:Repeater>
</div>