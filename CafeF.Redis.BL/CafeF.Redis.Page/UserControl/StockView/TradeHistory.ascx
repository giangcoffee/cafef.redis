<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TradeHistory.ascx.cs"
    Inherits="CafeF.Redis.Page.UserControl.StockView.TradeHistory" %>
<div class="lichsu">
    <ul class="tabs3">
        <li id="lstab1" ><a id=aLSGD href="javascript:changeTabLichSu(1);" style="color:#cc0000;">Lịch sử GD</a></li>
        <li id="lstab2"><a id=aTKDL href="javascript:changeTabLichSu(2);">TK Đặt lệnh</a></li>
        <li id="lstab3"><a id=aNDTNN href="javascript:changeTabLichSu(3);">NĐTNN</a></li>
    </ul>
    <div style="clear:both"></div>
    <div id='divData1LichSu'>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <th class="col1">Ngày</th>
          <th class="col2">Thay đổi giá</th>
          <th class="col3">KL khớp lệnh</th>
          <th class="col4">Tổng GTGD</th>
        </tr>
        <asp:Repeater ID="rptLichSuGD" runat="server" OnItemDataBound="rptLichSuGD_ItemDataBound">
            <ItemTemplate>
                 <tr class="even">
                    <td class="col1"><%#String.Format("{0:dd/MM}", DataBinder.Eval(Container.DataItem, "TradeDate"))%></td>
                    <td class="col2"><div class="l"><asp:Literal runat="server" ID="ltrPrice"></asp:Literal></div><asp:Literal runat="server" ID="ltrChange"></asp:Literal></td>
                    <td class="col3"><%#String.Format("{0:#,##0}",DataBinder.Eval(Container.DataItem, "Volume"))%></td>
                    <td class="col4"><asp:Literal runat="server" ID="ltrTotal"></asp:Literal></td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr class="odd">
                    <td class="col1"><%#String.Format("{0:dd/MM}", DataBinder.Eval(Container.DataItem, "TradeDate"))%></td>
                    <td class="col2"><div class="l"><asp:Literal runat="server" ID="ltrPrice"></asp:Literal></div><asp:Literal runat="server" ID="ltrChange"></asp:Literal></td>
                    <td class="col3"><%#String.Format("{0:#,##0}",DataBinder.Eval(Container.DataItem, "Volume"))%></td>
                    <td class="col4"><asp:Literal runat="server" ID="ltrTotal"></asp:Literal></td>
                </tr>
            </AlternatingItemTemplate>
        </asp:Repeater>    
	</table>
	 <div class="xemtiep clearfix">
  	    <a target="_blank" href='/Lich-su-giao-dich-<%= Symbol %>-1.chn'>Xem tất cả</a>
        <div class="donvigtgd">Đơn vị GTGD: VNĐ </div>
    </div>
	</div>
	<div id="divData2LichSu" align="center" style="overflow: hidden; padding-top: 6px; display:none">
	    <div id="loading"><img src="/images/loading.gif" /></div>
    </div>
</div>
<script language="javascript" type="text/javascript">
    var sym = '<%= Symbol %>';
    var strNDTNNN = ""; var strKLDT = ""; var divData1LichSu = document.getElementById("divData1LichSu"); var divData2LichSu = document.getElementById("divData2LichSu"); var tab1LichSu = document.getElementById("aLSGD"); var tab2LichSu = document.getElementById("aTKDL"); var tab3LichSu = document.getElementById("aNDTNN"); divData2LichSu.style.display = "none"; tab1LichSu.style.color = "#cc0000";
 </script>
<%--<script type="text/javascript" src="/scripts/InlineJSStock.TradeHistory.js"></script>
--%>
