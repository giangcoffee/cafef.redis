<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucBoxHangHoa.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.Homepage.ucBoxHangHoa" %>
<%@ Register src="../Bond/BondHomepageBox.ascx" tagname="BondHomepageBox" tagprefix="uc1" %>
<div id="divHangHoa">
<asp:Repeater runat="server" id="repVietnam">
<HeaderTemplate>
    <table width="100%" cellspacing="0" cellpadding="0" border="0"><tbody><tr class="head"><td>&nbsp;</td><td>Giá mua</td><td>Giá bán</td><td>Thay đổi</td></tr>
</HeaderTemplate>
<ItemTemplate>
    <tr class="<%# double.Parse(Eval("RowIndex").ToString()) % 2 ==0 ?"odd" : "even" %>"><td class="col1"><%# Eval("ProductName") %></td><td class="col2" align="right"><%# Eval("PriceString")%></td><td class="col2" align="right"><%# Eval("OtherString")%></td><td class="col3" align="right"><%# Eval("ChangeString")%></td></tr></ItemTemplate>
<FooterTemplate></tbody></table></FooterTemplate>
</asp:Repeater>
<asp:Repeater runat="server" id="repTheGioi">
<HeaderTemplate>
    <table width="100%" cellspacing="0" cellpadding="0" border="0"><tbody><tr class="head"><td>&nbsp;</td><td>Index</td><td>Thay đổi</td></tr>
</HeaderTemplate>
<ItemTemplate>
    <tr class="<%# double.Parse(Eval("RowIndex").ToString()) % 2 ==0 ?"odd" : "even" %>"><td class="col1"><%# Eval("ProductName") %></td><td class="col2" align="right"><%# Eval("PriceString")%></td><td class="col3" align="right"><%# Eval("ChangeString")%></td></tr></ItemTemplate>
<FooterTemplate></tbody></table></FooterTemplate>
</asp:Repeater>
<asp:Repeater runat="server" id="repHangHoa">
<HeaderTemplate>
    <table width="100%" cellspacing="0" cellpadding="0" border="0"><tbody><tr class="head"><td>&nbsp;</td><td>Giá USD</td><td>Thay đổi</td></tr>
</HeaderTemplate>
<ItemTemplate>
    <tr class="<%# double.Parse(Eval("RowIndex").ToString()) % 2 ==0 ?"odd" : "even" %>"><td class="col1"><%# Eval("ProductName") %></td><td class="col2" align="right"><%# Eval("PriceString")%></td><td class="col3" align="right"><%# Eval("ChangeString")%></td></tr></ItemTemplate>
<FooterTemplate></tbody></table></FooterTemplate>
</asp:Repeater>
</div>
<div id="divBond" runat="server">
    <uc1:BondHomepageBox ID="BondHomepageBox1" runat="server" />
</div>