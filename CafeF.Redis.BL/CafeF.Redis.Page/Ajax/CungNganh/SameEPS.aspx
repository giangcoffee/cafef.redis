<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SameEPS.aspx.cs" Inherits="CafeF.Redis.Page.Ajax.CungNganh.SameEPS" %>
<%@ Import Namespace="CafeF.Redis.BL"%>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr><th class="col1" align="center">Mã</th><th align="center">Sàn</th>
        <th class="col2">EPS</th>
        <th class="col3">Giá</th>
        <th class="col4">P/E</th>
        <th class="col5" width="75">Vốn hóa TT (Tỷ đồng)</th>
    </tr>
    <asp:repeater id="rptEPS" runat="server" onitemdatabound="rptEPS_ItemDataBound">
                <ItemTemplate>
                    <tr class="even">
                        <td align="center"><a title='<%#HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "Name").ToString()).Replace("'", "&#39;")%>' href='<%# Utils.GetSymbolLink(Eval("Symbol").ToString(), Eval("Name").ToString(), Eval("TradeCenterId").ToString())%>'><%#DataBinder.Eval(Container.DataItem, "Symbol")%></a></td>
                        <td align="center"><asp:Literal runat="server" ID="ltrSan"></asp:Literal></td>
                        <td><asp:Literal runat="server" ID="ltrEPS"></asp:Literal></td>
                        <td><asp:Literal runat="server" ID="ltrPrice"></asp:Literal></td>
                        <td><asp:Literal runat="server" ID="ltrPE"></asp:Literal></td>
                        <td><asp:Literal runat="server" ID="ltrVonHoa"></asp:Literal></td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="odd">
                        <td align="center"><a title='<%#HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "Name").ToString()).Replace("'", "&#39;")%>' href='<%# Utils.GetSymbolLink(Eval("Symbol").ToString(), Eval("Name").ToString(), Eval("TradeCenterId").ToString())%>'><%#DataBinder.Eval(Container.DataItem, "Symbol")%></a></td>
                         <td align="center"><asp:Literal runat="server" ID="ltrSan"></asp:Literal></td>
                        <td><asp:Literal runat="server" ID="ltrEPS"></asp:Literal></td>
                        <td><asp:Literal runat="server" ID="ltrPrice"></asp:Literal></td>
                        <td><asp:Literal runat="server" ID="ltrPE"></asp:Literal></td>
                        <td><asp:Literal runat="server" ID="ltrVonHoa"></asp:Literal></td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:repeater>
</table>
