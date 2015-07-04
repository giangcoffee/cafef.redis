<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CeoPosition.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.Ceo.CeoPosition" %>
<%@ Import Namespace="CafeF.Redis.BL"%>
<div id="divPosition" runat="server">
<table class="cl_ceo" cellspacing="0" cellpadding="0" border="0" style="width: 100%">
    <tr><td style="border-bottom:solid 1px #D6D6D6" colspan="3"><span class="ceo_title2">CHỨC VỤ HIỆN TẠI</span></td></tr>
    <tr>
        <td style="width:30%;font-weight:bold">Vị trí</td>
        <td style="width:50%;font-weight:bold">Tổ chức</td>
        <td style="width:20%;font-weight:bold;">Thời gian bổ nhiệm</td>
    </tr>
    <asp:Repeater EnableViewState="false" ID="rpData" runat="server">
        <HeaderTemplate></HeaderTemplate>
        <ItemTemplate>
            <tr valign="top">
                <td><%# Eval("PositionTitle")%></td>
                <td><%# (Eval("CeoSymbol") == null || String.IsNullOrEmpty(Eval("CeoSymbol").ToString()) || Eval("CeoSymbolCenterId") == null) ? Eval("PositionCompany") : String.Format("<a href='/{0}/{1}-{2}.chn' title='{3}'>{4}  ({2})</a>", Utils.GetCenterFolder(Eval("CeoSymbolCenterId").ToString()), Eval("CeoSymbol"), Utils.UnicodeToKoDauAndGach(Eval("PositionCompany").ToString()), HttpUtility.HtmlEncode(Eval("PositionCompany").ToString()).Replace("'", ""), Eval("PositionCompany"))%></td>
                <td><%# Eval("CeoPosDate")%></td>
            </tr>            
        </ItemTemplate>
        <FooterTemplate></FooterTemplate>
    </asp:Repeater>
</table>
</div>
