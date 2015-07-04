<%@ Page Language="C#" AutoEventWireup="true" Codebehind="TKDL.aspx.cs" Inherits="CafeF.Redis.Page.Ajax.TKDL" %>
 <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr><th class="col1">Ngày</th><th class="col3">Dư mua</td><th class="col3">Dư bán</td><th class="col3">KLTB<br />1 lệnh mua</td><th class="col3">KLTB<br />1 lệnh bán</td></tr>
    <asp:repeater id="rptTKDatlenh" runat="server" >
        <ItemTemplate>
            <tr class="even">
                <td class="col1"><%#String.Format("{0:dd/MM}", DataBinder.Eval(Container.DataItem, "TradeDate"))%></td>
                <td class="col3"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "BidLeft"))%></td>
                <td class="col3"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "AskLeft"))%></td>
                <td class="col3"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "BidAverageVolume"))%>&nbsp;</td>
                <td class="col3"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "AskAverageVolume"))%>&nbsp;<asp:Literal runat="server" ID="ltrChange"  Visible="false"></asp:Literal></td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="odd">
                <td class="col1"><%#String.Format("{0:dd/MM}", DataBinder.Eval(Container.DataItem, "TradeDate"))%></td>
                <td class="col3"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "BidLeft"))%></td>
                <td class="col3"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "AskLeft"))%></td>
                <td class="col3"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "BidAverageVolume"))%>&nbsp;</td>
                <td class="col3"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "AskAverageVolume"))%>&nbsp;<asp:Literal runat="server" ID="ltrChange"  Visible="false"></asp:Literal></td>
            </tr>
        </AlternatingItemTemplate>
    </asp:repeater>
</table>
 <div class="xemtiep clearfix"><a target="_blank" href='/Lich-su-giao-dich-<%= symbol %>-2.chn'>Xem tất cả</a><div class="donvigtgd">Đơn vị GTGD: VNĐ </div></div>
