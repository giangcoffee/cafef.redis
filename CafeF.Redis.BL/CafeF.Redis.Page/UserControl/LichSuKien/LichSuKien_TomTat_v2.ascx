<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LichSuKien_TomTat_v2.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.LichSuKien.LichSuKien_TomTat_v2" %>

<h3><a title="Lịch sự kiện thị trường" href="/lich-su-kien.chn">LỊCH SỰ KIỆN THỊ TRƯỜNG THÁNG <asp:Literal runat="server" ID="ltrMonth"></asp:Literal></a></h3>
<asp:Repeater ID="rptLichSukien" runat="server" OnItemDataBound="rptLichSukien_ItemDataBound">
    <HeaderTemplate>
        <table cellpadding="4" cellspacing="0">
            <tr>
                <td class="lsk-title1" style="background-color: #fff;">
                    Ngày</td>
                <td class="lsk-title1">
                    Mã CK</td>
                <td class="lsk-title2">
                    Nội dung sự kiện</td>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td class="lsk-title1" style="background-color: #fff;">
                <asp:Literal ID="ltrNgay" runat="server"></asp:Literal></td>
            <td class="lsk1">
                <asp:Literal ID="ltrMaCK" runat="server"></asp:Literal></td>
            <td class="lsk2">
                <asp:HyperLink ID="lnkNoiDung" runat="server"></asp:HyperLink></td>
        </tr>
    </ItemTemplate>
    <AlternatingItemTemplate>
        <tr>
            <td class="lsk-title1" style="background-color: #fff;">
                <asp:Literal ID="ltrNgay" runat="server"></asp:Literal>&nbsp;</td>
            <td class="lsk1">
                <asp:Literal ID="ltrMaCK" runat="server"></asp:Literal>&nbsp;</td>
            <td class="lsk2">
                <asp:HyperLink ID="lnkNoiDung" runat="server"></asp:HyperLink>&nbsp;</td>
        </tr>
    </AlternatingItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>
<div class="xemtatca"><a href="/lich-su-kien.chn" class="News_ViewMore_Link">Xem tất cả</a></div>
