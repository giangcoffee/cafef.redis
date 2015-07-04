<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LatestEvents.ascx.cs"
    Inherits="CafeF.Redis.Page.UserControl.LichSuKien.LatestEvents" %>
<div class="cf_WCBox">
      <div class="cf_BoxContent">
        <div class="cf_Pad5TB9LR">
            <div class="cf_WireBox">
                <div class="cf_BoxContent">
                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td align="left" valign="top" class="CafeF_Padding10">
                                <div>
                                    <b>TIN TỨC DOANH NGHIỆP</b></div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" style="border-top: solid 1px #dadada; padding-top: 5px">
                                <asp:DataList ID="rptTopEvents" runat="server" Width="100%" OnItemDataBound="rptTopEvents_ItemDataBound">
                                    <HeaderTemplate>
                                        <table width="100%" cellpadding="0" cellspacing="0" class="CafeF_ContentPadding">
                                    </HeaderTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Event_Images">
                                            </td>
                                            <td class="F_text_listbai" style="padding-bottom: 5px">
                                                <asp:Literal runat="server" ID="ltrContent1"></asp:Literal>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" class="NormalText_Bold">
                                <a style="padding: 5px; font-size: 12px; color: #C00;" href="/Tin-doanh-nghiep.chn">[Xem tất cả]</a>
                            </td>
                        </tr>
                    </table>
                </div>
               <%-- <div class="cf_BoxFooter">
                    <div>
                    </div>
                </div>--%>
            </div>
        </div>
    </div>
   <%-- <div class="cf_BoxFooter">
        <div>
        </div>
    </div>--%>
</div>
