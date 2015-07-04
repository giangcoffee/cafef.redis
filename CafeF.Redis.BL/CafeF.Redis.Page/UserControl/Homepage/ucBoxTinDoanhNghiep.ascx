<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucBoxTinDoanhNghiep.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.Homepage.ucBoxTinDoanhNghiep" %>
<div style="overflow-x: hidden; overflow-y: auto; height: 350px;">
<table cellspacing="0" cellpadding="0" border="0" width="280">
<tbody>
    <tr class="header"><th class="cn-1">Mã CP</th><th class="cn-2">Nội dung</th><th class="cn-3">Giá</th><th class="cn-4">Thay đổi</th></tr>
        <asp:repeater runat="server" id="repNews">
            <ItemTemplate>    
        <tr class='<%# double.Parse(Eval("RowIndex").ToString()) % 2 == 0 ?"odd":"even" %>' style="font-size:11px;">
            <td class="cn-1">
                <strong><a href="javascript:void(0)" class="symbol" rel="<%# Eval("Symbol") %>"><%# Eval("Symbol") %></a></strong> <%# DisplayDate(Eval("NewsDate"))%>
            </td>
            <td class="cn-2">
                <a href="<%# DisplayLink(Eval("Symbol").ToString(), Eval("NewsID").ToString(), Eval("NewsTitle").ToString()) %>"><%# Eval("NewsTitle").ToString() %></a>
            </td>
            <td class="cn-3" style="padding-left:3px;">
                <%# double.Parse(Eval("Price").ToString()).ToString("#0.0") %>
            </td>
            <td class="cn-4">
                <div class="<%# double.Parse(Eval("Change").ToString()) > 0 ? "up" : (double.Parse(Eval("Change").ToString()) < 0 ? "down" : "") %>">
                    <%# double.Parse(Eval("Change").ToString())> 0? "+":""%><%#  double.Parse(Eval("Change").ToString()).ToString("#0.0")%></div>
            </td>
        </tr></ItemTemplate>
        </asp:repeater>
    </tbody>
</table>
</div>
<div class="cn-bottom clearfix">
    <div class="cn-all">
        </div>
    <div class="cn-page">
       <%-- <asp:Repeater runat="server" id="repPage">
            <ItemTemplate><a href="javascript:void(0);" class="cnpaging <%# Eval("Current").ToString()=="1"?"current":"" %>" rel="<%# Eval("PageIndex") %>"><%# Eval("PageTitle") %></a></ItemTemplate>
        </asp:Repeater>--%>
        <a href="/tin-doanh-nghiep.chn">Xem tiếp &gt;</a>
        </div>
</div>
