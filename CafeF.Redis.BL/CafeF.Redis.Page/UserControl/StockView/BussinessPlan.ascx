<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BussinessPlan.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.StockView.BussinessPlan" %>
<link href="/style/tooltip.css" media="screen, projection" rel="stylesheet" type="text/css">
<div class="kehoachkd">
<h3 class="cattitle noborder">KẾ HOẠCH KINH DOANH NĂM 2011</h3>
    <ul>
        <li class="clearfix">
            <div class="l">Doanh thu</div>
            <div class="r"><asp:Literal ID=ltDoanhthu runat=server></asp:Literal></div>
        </li>
        <li class="clearfix">
            <div class="l">Lợi nhuận trước thuế</div>
            <div class="r"><asp:Literal ID=ltLoinhuanTruocthue runat=server></asp:Literal></div>
        </li>
        <li class="clearfix">
            <div class="l">Lợi nhuận sau thuế</div>
            <div class="r"><asp:Literal ID=ltLoinhuanSauthue runat=server></asp:Literal></div>
        </li>
        <li class="clearfix">
            <div class="l">Cổ tức bằng tiền mặt</div>
            <div class="r"><asp:Literal ID=ltCotucTienmat runat=server></asp:Literal></div>
        </li>
        <li class="clearfix">
            <div class="l">Cổ tức bằng cổ phiếu</div>
            <div class="r"><asp:Literal ID=ltCotucCophieu runat=server></asp:Literal></div>
        </li>
        <li class="clearfix">
            <div class="l">Dự kiến tăng vốn lên</div>
            <div class="r"><asp:Literal ID=ltTangVon runat=server></asp:Literal></div>
        </li>
    </ul>
    <div class="xemtiep"><asp:Literal runat="server" ID="ltrLink"></asp:Literal></div>
</div>