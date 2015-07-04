<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SameEPS_PE.ascx.cs"
    Inherits="CafeF.Redis.Page.UserControl.StockView.SameEPS_PE" %>
<%@ Import Namespace="CafeF.Redis.BL"%>

<div class="epspe">
    <ul class="tabs3">
        <li><a id="lstab1EPS" style="cursor: pointer" onclick="javascript:changeTabEPS(1);">EPS tương đương</a></li>
        <li><a id="lstab2EPS" style="cursor: pointer" onclick="javascript:changeTabEPS(2);">P/E tương đương</a></li>
    </ul>
    <div style="clear:both"></div>
     <div id='divData1EPS'>
    <div id='divData1EPSInner'>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <th class="col1" align="center">Mã</th>
                <th class="col1" align="center">Sàn</th>
                <th class="col2">EPS</th>
                <th class="col3">Giá</th>
                <th class="col4">P/E</th>
                <th class="col5" width="75">Vốn hóa TT (Tỷ đồng)</th>
            </tr>
            <asp:Repeater ID="rptEPS" runat="server" OnItemDataBound="rptEPS_ItemDataBound">
                <ItemTemplate>
                    <tr class="even">
                        <td  align="center"><a title='<%#HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "Name").ToString()).Replace("'", "&#39;")%>' href='<%# Utils.GetSymbolLink(Eval("Symbol").ToString(), Eval("Name").ToString(), Eval("TradeCenterId").ToString())%>'><%#DataBinder.Eval(Container.DataItem, "Symbol")%></a></td>
                        <td  align="center"><asp:Literal runat="server" ID="ltrSan"></asp:Literal></td>
                        <td><asp:Literal runat="server" ID="ltrEPS"></asp:Literal></td>
                        <td><asp:Literal runat="server" ID="ltrPrice"></asp:Literal></td>
                        <td><asp:Literal runat="server" ID="ltrPE"></asp:Literal></td>
                        <td><asp:Literal runat="server" ID="ltrVonHoa"></asp:Literal></td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="odd">
                        <td  align="center"><a title='<%#HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "Name").ToString()).Replace("'", "&#39;")%>' href='<%# Utils.GetSymbolLink(Eval("Symbol").ToString(), Eval("Name").ToString(), Eval("TradeCenterId").ToString())%>'><%#DataBinder.Eval(Container.DataItem, "Symbol")%></a></td>
                        <td align="center"><asp:Literal runat="server" ID="ltrSan"></asp:Literal></td>
                        <td><asp:Literal runat="server" ID="ltrEPS"></asp:Literal></td>
                        <td><asp:Literal runat="server" ID="ltrPrice"></asp:Literal></td>
                        <td><asp:Literal runat="server" ID="ltrPE"></asp:Literal></td>
                        <td><asp:Literal runat="server" ID="ltrVonHoa"></asp:Literal></td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    <div class="paging" id="paging" runat="server"></div><div class="pagingnote" id="pagingnoteeps"><asp:Literal ID="ltrGhiChu" runat="server"></asp:Literal></div>
        <div class="epse-note" id="eps_pe_notes">(EPS +/-0.5)</div><input type="hidden" id="txtIdxSameEPS" value="1" /></div>
     <div id="divData2EPS">
     <div id='divData2EPSInner'></div>
        <div class="" id="pagingPE"></div>
        <div class="epse-note" id="eps_pe_notespe">(PE +/-1.0)</div>
        <input type="hidden" id="txtIdxSamePE" value="1" />
    </div>
 </div>
 <script language="javascript" type="text/javascript">
var TotalPagePE = '<%= TotalPage %>';
var TotalItemPE = '<%= TotalItem %>';
var strPE = ""; var strPEPaging = ""; var pagingPE = document.getElementById("pagingPE"); var divData1EPS = document.getElementById("divData1EPS"); var divData2EPS = document.getElementById("divData2EPS"); var tab1EPS = document.getElementById("lstab1EPS"); var tab2EPS = document.getElementById("lstab2EPS"); divData2EPS.style.display = "none"; document.getElementById('txtIdxSameEPS').value = 1; document.getElementById('txtIdxSamePE').value = 1;
//var symbol = '<%= Symbol %>';
//$(document).ready(function(e) { changeTabEPS(1); });
</script>
<%--<script type="text/javascript" src="/scripts/InlineJSStock.SameEPS.PE.js"></script>
--%>
