<%@ Control Language="C#" AutoEventWireup="true" Codebehind="OtherCrawlerNews.ascx.cs"
    Inherits="CafeF.Redis.Page.UserControl.LichSuKien.OtherCrawlerNews" %>
<%@ Import Namespace="CafeF.BO" %>
<table width="100%" border="0" cellspacing="0" cellpadding="0" style="padding-left:10px">
    <tr>
        <td style="border-bottom: dotted 1px #dadada; padding-bottom: 5px">
            <asp:datalist id="DataList1" runat="server" onitemdatabound="DataList1_ItemDataBound">
                <FooterTemplate>
                </FooterTemplate>
                <ItemTemplate>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="padding-right: 0px">
                        <tr>
                            <td class="Event_Images">
                            </td>
                            <td class="F_text_listbai" style="padding-bottom: 5px">
                                <asp:Literal runat="server" ID="ltrContent1"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:datalist>
        </td>
    </tr>
    <tr>
        <td class="NormalText_Bold" valign="top" align="right">
            <div style="margin: 5px 0px; float: right;">
                <asp:hyperlink runat="server" id="hplMore" cssclass="News_ViewMore_Link" text="Xem tiếp"></asp:hyperlink>
            </div>
        </td>
    </tr>
</table>
<script type="text/javascript">
var lnkViewMore = document.getElementById('<% =hplMore.ClientID %>');

if (lnkViewMore)
{
    lnkViewMore.href = lnkViewMore.href.replace('&', '').replace('&', '');
}
</script>
