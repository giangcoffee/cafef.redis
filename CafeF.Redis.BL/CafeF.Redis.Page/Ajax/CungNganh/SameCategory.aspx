<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SameCategory.aspx.cs" Inherits="CafeF.Redis.Page.Ajax.CungNganh.SameCategory" %>
<%@ Import Namespace="CafeF.Redis.BL"%>
<table width="100%" border="0" cellspacing="0" cellpadding="0"><tr><th>Mã CK</th><th>Sàn</th><th>&nbsp;</th><th>Giá</th><th>EPS</th><th>P/E</th></tr>       
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
</asp:Repeater></div> 