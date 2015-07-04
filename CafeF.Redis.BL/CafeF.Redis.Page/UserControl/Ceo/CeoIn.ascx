<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CeoIn.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.Ceo.CeoIn" %>
<div style='<%=style%>'>
<table class="ceo_man">
    <tr>
        <td class="ceo_man_title"><%=Name %></td>
    </tr>
    <tr>
        <td>
            <table>
                <asp:Repeater EnableViewState="false" ID="rpData" runat="server">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr valign="top">
                            <td style="width:45px"><a href="<%# makeLink(Eval("CeoCode").ToString(),Eval("Name").ToString()) %>" title="<%# HttpUtility.HtmlEncode(Eval("Name").ToString()).Replace("\"","")%>"><img border="0" alt='<%# Eval("Name")%>' src='<%# StorageServer + "zoom/36_45/" + CeoStoragePath %><%# GetImages(Eval("CeoCode").ToString()) %>' onerror="this.src='http://cafef3.vcmedia.vn/v2/images/noimage.jpg';" width="36px" height="45px" /></a></td>
                            <td style="width:155px"><a href="<%# makeLink(Eval("CeoCode").ToString(),Eval("Name").ToString()) %>"><%# Eval("Name")%></a></td>
                            <td><%# Eval("Positions")%></td>
                        </tr>            
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:Repeater>                
            </table>
        </td>
    </tr>                                    
</table><br />
</div>
