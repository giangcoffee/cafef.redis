<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AnalyseReports.ascx.cs"
    Inherits="CafeF.Redis.Page.UserControl.StockView.AnalyseReports" %>

<div id="divCompany" class="baocaophantich" runat="server">
<h3 class="cattitle noborder">BÁO CÁO PHÂN TÍCH</h3>
    <asp:Repeater EnableViewState="false" ID="rpData" runat="server">
        <HeaderTemplate>
            <ul>
        </HeaderTemplate>
        <ItemTemplate>
            <li>
                <div><a href="/phan-tich-bao-cao<%# Eval("ID") %>.chn"><%# Eval("Title")%><%# GetSource(Eval("ResourceCode"))%>&nbsp;</a>(<%# Convert.ToDateTime(Eval("DateDeploy")).ToString("dd/MM/yyyy")%>)</div>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>    
    <div class="xemtiep"><a href="/phan-tich-bao-cao.chn">Xem tiếp</a></div>
</div>