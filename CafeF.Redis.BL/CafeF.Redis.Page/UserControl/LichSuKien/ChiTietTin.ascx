<%@ Control Language="C#" AutoEventWireup="true" Codebehind="ChiTietTin.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.LichSuKien.ChiTietTin" %>
<%@ Register Src="LichSuKien_TomTat.ascx" TagName="LichSuKien_TomTat" TagPrefix="uc1" %>
<%@ Register src="NewsContent.ascx" tagname="NewsContent" tagprefix="uc2" %>
<%@ Register src="LatestEvents.ascx" tagname="LatestEvents" tagprefix="uc3" %>
<%@ Register Src="/UserControl/StockView/Search.ascx" TagName="ucSearch" TagPrefix="uc4" %>
<div id="content">
    <uc2:NewsContent ID="NewsContent1" runat="server" />
</div>
<div id="sidebar">
    <uc4:ucSearch ID="ucSearch" runat="server" EnableViewState="false" />
    <uc3:LatestEvents ID="LatestEvents1" runat="server" />
</div>