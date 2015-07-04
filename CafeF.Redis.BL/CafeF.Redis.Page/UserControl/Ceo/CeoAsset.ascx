<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CeoAsset.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.Ceo.CeoAsset" %>
<table class="cl_ceo" cellspacing="0" cellpadding="0" border="0" style="width: 100%">
    <tr><td colspan="4" style="border-bottom:solid 1px #D6D6D6"><span class="ceo_title2">CỔ PHIẾU ĐANG NẮM GIỮ</span></td></tr>
    <tr>
        <td style="width:30%;font-weight:bold">Tổ chức/Mã CP</td>
        <td style="width:15%;font-weight:bold">Số lượng</td>
        <td style="width:30%;font-weight:bold;">Tính đến ngày</td>
        <td style="width:25%;font-weight:bold"><span style="color:red">*</span> Giá trị (tỷ VNĐ)</td>
    </tr>
    <asp:Repeater EnableViewState="false" ID="rpData" runat="server">
        <HeaderTemplate>
        </HeaderTemplate>
        <ItemTemplate>
            <tr valign="top">
                <td><%# GetLink(Eval("Symbol").ToString())%></td>
                <td><%# Eval("AssetVolume")%></td>
                <td><%# Eval("UpdatedDate")%></td>
                <td style="padding-left:10px;"><%# GetValue(double.Parse(Eval("AssetVolume").ToString()), Eval("Symbol").ToString())%></td>
            </tr>            
        </ItemTemplate>
        <FooterTemplate>
        </FooterTemplate>
    </asp:Repeater>
</table>