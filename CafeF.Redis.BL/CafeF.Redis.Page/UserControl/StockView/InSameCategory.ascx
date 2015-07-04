<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InSameCategory.ascx.cs"
    Inherits="CafeF.Redis.Page.UserControl.StockView.InSameCategory" %>
<%@ Import Namespace="CafeF.Redis.BL"%>
        <div class="congtycungnganh">
            <h3>CTY CÙNG NGÀNH <span class="subtitle" style="color:Red;"><asp:Literal ID="ltrNganh" runat="server" /></span></h3>
            <%--<div class="subtitle">
                <a href="#"><asp:Literal ID="ltrNganh" runat="server"></asp:Literal></a></div>--%>
                <div  id="tblCTCN">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <th>Mã CK</th>
                     <th>Sàn</th>
                    <th>&nbsp;</th>
                    <th>Giá</th>
                    <th>EPS</th>
                    <th>P/E</th>
                </tr>                
                <asp:Repeater ID="rptSameCategory" runat="server" OnItemDataBound="rptSameCategory_ItemDataBound">
                    <ItemTemplate>
                        <tr class="even">
                            <td class="col1"><a title='<%#HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "Name").ToString()).Replace("'", "&#39;")%>' href='<%# Utils.GetSymbolLink(Eval("Symbol").ToString(), Eval("Name").ToString(), Eval("TradeCenterId").ToString())%>'><%#DataBinder.Eval(Container.DataItem, "Symbol")%></a></td>
                            <td class="col1"><asp:Literal runat="server" ID="ltrSan"></asp:Literal></td>
                            <td class="col2"><asp:Literal runat="server" ID="ltrPrice"></asp:Literal></td>
                            <td class="col3"><div id=divColor runat=server><asp:Literal runat="server" ID="ltrPercent"></asp:Literal></div></td>
                            <td class="col4"><asp:Literal runat="server" ID="ltrEPS"></asp:Literal></td>
                            <td class="col5"><asp:Literal runat="server" ID="ltrPE"></asp:Literal></td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="odd">
                            <td class="col1"><a title='<%#HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "Name").ToString()).Replace("'", "&#39;")%>' href='<%# Utils.GetSymbolLink(Eval("Symbol").ToString(), Eval("Name").ToString(), Eval("TradeCenterId").ToString())%>'><%#DataBinder.Eval(Container.DataItem, "Symbol")%></a></td>
                            <td class="col1"><asp:Literal runat="server" ID="ltrSan"></asp:Literal></td>
                            <td class="col2"><asp:Literal runat="server" ID="ltrPrice"></asp:Literal></td>
                            <td class="col3"><div id=divColor runat=server><asp:Literal runat="server" ID="ltrPercent"></asp:Literal></div></td>
                            <td class="col4"><asp:Literal runat="server" ID="ltrEPS"></asp:Literal></td>
                            <td class="col5"><asp:Literal runat="server" ID="ltrPE"></asp:Literal></td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>               
            </table>
            </div> 
            <div class="paging" id="paging" runat="server"></div>
             <div class="pagingnote" id="pagingnote"><asp:Literal ID="ltrGhiChu" runat="server"></asp:Literal></div>
        </div><input type="hidden" id="txtIdxSameCategory" value="1" />
<script language="javascript" type="text/javascript">
var symbol = '<%= StockSymbol %>';
var TotalPage = '<%= TotalPage %>';
//document.getElementById('txtIdxSameCategory').value = '1'; 
</script>
<%--<script type="text/javascript" src="/scripts/InlineJSStock.SameCategory.js"></script>--%>

