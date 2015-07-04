<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CeoNews.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.Ceo.CeoNews" %>
<div id="divNews" runat="server">
<table class="cl_ceo" cellspacing="0" cellpadding="0" border="0" style="width: 100%">
    <tr><td style="border-bottom:solid 1px #D6D6D6"><span class="ceo_title2">CÁC BÀI VIẾT LIÊN QUAN</span></td></tr>
    <tr>
        <td>
            <asp:Repeater EnableViewState="false" ID="rpData" runat="server">
                <HeaderTemplate>
                <ul>
                </HeaderTemplate>
                <ItemTemplate>
                    <li><a title="" href='<%# Eval("NewsLink")%>'><%# Eval("Title")%></a> <span>(<%# ProcessDate(Eval("PublishDate")) %>)</span></li>
                </ItemTemplate>
                <FooterTemplate>
                </ul>
                </FooterTemplate>
            </asp:Repeater>
        </td>
    </tr>                                    
</table>
</div>
