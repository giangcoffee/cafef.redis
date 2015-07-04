<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TinDoanhNghiepChiTiet.aspx.cs" Inherits="CafeF.Redis.Page.TinDoanhNghiepChiTiet" MasterPageFile="~/MasterPage/SoLieu.Master" %>
<%@ Register src="UserControl/LichSuKien/ChiTietTin.ascx" tagname="ChiTietTin" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc1:ChiTietTin ID="ChiTietTin1" runat="server" />
</asp:Content>