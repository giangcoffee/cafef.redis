<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NDTNN.aspx.cs" Inherits="CafeF.Redis.Page.Ajax.NDTNN" %>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr><th class="col1">Ngày</th><th class="col5">KLGD<br />ròng</th><th class="col5">GTGD<br /> ròng</th><th class="col6">% GD mua<br />toàn TT</th><th class="col6">% GD bán<br />toàn TT</th></tr>
    <asp:repeater id="rptTDTNN" runat="server">
        <ItemTemplate>
            <tr class="even">
                <td class="col1"><%#String.Format("{0:dd/MM}", DataBinder.Eval(Container.DataItem, "TradeDate"))%></td>
                <td align="right"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "NetVolume"))%></td>
                <td align="right" style="white-space:nowrap;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "NetValue"))%></td>
                <td align="right"><%#String.Format("{0:#,##0.00}", DataBinder.Eval(Container.DataItem, "BuyPercent"))%>%</td>
                <td align="right"><%#String.Format("{0:#,##0.00}", DataBinder.Eval(Container.DataItem, "SellPercent"))%>%</td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="odd">
                <td class="col1"><%#String.Format("{0:dd/MM}", DataBinder.Eval(Container.DataItem, "TradeDate"))%></td>
                <td align="right"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "NetVolume"))%></td>
                <td align="right" style="white-space:nowrap;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "NetValue"))%></td>
                <td align="right"><%#String.Format("{0:#,##0.00}", DataBinder.Eval(Container.DataItem, "BuyPercent"))%>%</td>
                <td align="right"><%#String.Format("{0:#,##0.00}", DataBinder.Eval(Container.DataItem, "SellPercent"))%>%</td>
            </tr>
        </AlternatingItemTemplate>
    </asp:repeater>
</table>
<div class="xemtiep clearfix">
    <a target="_blank" href='/Lich-su-giao-dich-<%= symbol %>-3.chn'>Xem tất cả</a>
    <div class="donvigtgd">Đơn vị GTGD: VNĐ</div>
</div>
