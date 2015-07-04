<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportList.aspx.cs" MasterPageFile="~/MasterPage/SoLieu.Master"
 Inherits="CafeF.Redis.Page.ReportList" EnableEventValidation="false" EnableViewState="true" ViewStateEncryptionMode="never" %>

<%@ Register Src="UserControl/Report/AnalyzeReportList.ascx" TagName="AnalyzeReportList" TagPrefix="uc2" %>
<%@ Register src="/UserControl/LichSuKien/LatestEvents.ascx" tagname="LatestEvents" tagprefix="uc3" %>
<%@ Register Src="/UserControl/StockView/Search.ascx" TagName="ucSearch" TagPrefix="uc4" %>

<asp:Content ID="Content1" EnableViewState="false" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content">
        <uc2:AnalyzeReportList ID="AnalyzeReportList1" runat="server" />
    </div>
    <div id="sidebar">
        <uc4:ucSearch ID="ucSearch" runat="server" EnableViewState="false" />
        <uc3:LatestEvents ID="LatestEvents1" runat="server" />
    </div>
</asp:Content>
