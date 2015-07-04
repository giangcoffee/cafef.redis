<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AnalyseReportsInHomePage.ascx.cs"
    Inherits="CafeF.Redis.Page.UserControl.StockView.AnalyseReportsInHomePage" %>
<div class="block bcpt" style="float: none; display: inline; margin-right: 0pt;">
<div id="divCompany" class="blcontent" runat="server">
<h3>Báo cáo phân tích</h3>
    <asp:Repeater EnableViewState="false" ID="rpData" runat="server">
        <HeaderTemplate>
            <ul style="padding: 0 5px;">
        </HeaderTemplate>
        <ItemTemplate>
            <li>
                <a href="/phan-tich-bao-cao<%# Eval("ID") %>.chn"><%# Eval("Title")%><%# GetSource(Eval("ResourceCode"))%> <span style="color:#444">(<%# Convert.ToDateTime(Eval("DateDeploy")).ToString("dd/MM/yyyy")%>)</span> <%# (bool)Eval("IsHot")?" <img border='0' width='30px' align='absmiddle' src='http://cafef3.vcmedia.vn/images/new.gif'>":"" %></a>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            <li style="border-bottom:none;padding: 8px 5px 0 5px; background:none">
            <a href="/phan-tich-bao-cao.chn">Xem tất cả ></a>
            </li>
            </ul>
        </FooterTemplate>
    </asp:Repeater>        
</div>
</div>