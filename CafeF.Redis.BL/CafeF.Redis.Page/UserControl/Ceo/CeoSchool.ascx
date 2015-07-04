<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CeoSchool.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.Ceo.CeoSchool" %>
<div id="divSchool" runat="server">
<table class="cl_ceo" cellspacing="0" cellpadding="0" border="0" style="width: 100%">
    <tr><td style="border-bottom:solid 1px #D6D6D6"><span class="ceo_title2">THÀNH TÍCH</span></td></tr>
    <tr>
        <td>
        <%=profileShort%>
        <%--<asp:Repeater EnableViewState="false" ID="rpData" runat="server">
            <HeaderTemplate>
            <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li><%# Eval("CeoTitle")%></li>
            </ItemTemplate>
            <FooterTemplate>
            </ul>
            </FooterTemplate>
        </asp:Repeater>--%>
        </td>
    </tr>
</table>
</div>
