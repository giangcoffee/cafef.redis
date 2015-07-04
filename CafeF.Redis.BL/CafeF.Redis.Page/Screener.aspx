<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Screener.aspx.cs" Inherits="CafeF.Redis.Page.Screener" MasterPageFile="~/MasterPage/SoLieu.Master" %>

<%@ Register src="UserControl/Screener/ucCafef_Screener.ascx" tagname="ucCafef_Screener" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ucCafef_Screener ID="ucCafef_Screener1" runat="server" />
</asp:Content>