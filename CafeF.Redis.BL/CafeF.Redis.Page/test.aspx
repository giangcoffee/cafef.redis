<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SoLieu.Master" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="CafeF.Redis.Page.test" %>
<%@ Register src="UserControl/StockNewsControl/StockNewsInHomePage.ascx" tagname="StockNewsInHomePage" tagprefix="uc1" %>
<%@ Register src="UserControl/Report/AnalyseReportsInHomePage.ascx" tagname="report" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc1:StockNewsInHomePage ID="StockNewsInHomePage1" runat="server" />
<uc1:report ID="aaa" runat="server" />
</asp:Content>
