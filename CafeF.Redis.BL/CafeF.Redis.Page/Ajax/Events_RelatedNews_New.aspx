<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Events_RelatedNews_New.aspx.cs" Inherits="CafeF.Redis.Page.Ajax.Events_RelatedNews_New" %>
    <div id="divEvents" runat="server">
        <asp:Repeater ID="rptTopEvents" runat="server" OnItemDataBound="rptTopEvents_ItemDataBound">
            <HeaderTemplate>
                <ul class="News_Title_Link">
            </HeaderTemplate>
            <ItemTemplate>
                <li style="line-height:20px"><asp:Literal runat="server" ID="ltrContent"></asp:Literal></li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </div>
