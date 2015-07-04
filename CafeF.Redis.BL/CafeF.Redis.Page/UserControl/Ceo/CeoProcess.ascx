<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CeoProcess.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.Ceo.CeoProcess" %>
<asp:Repeater EnableViewState="false" ID="rpData" runat="server">
    <HeaderTemplate>
        <table class="cl_ceo" cellspacing="0" cellpadding="0" border="0" style="width: 100%">
            <tr>
                <td style="border-bottom: solid 1px #D6D6D6">
                    <span class="ceo_title2">QUÁ TRÌNH CÔNG TÁC</span>
                </td>
            </tr>
            <tr><td><ul>
    </HeaderTemplate>
    <ItemTemplate>
        <li><%# Eval("ProcessDesc")%></li>
    </ItemTemplate>
    <FooterTemplate>
        </ul> </td> </tr> </table>
    </FooterTemplate>
</asp:Repeater>
