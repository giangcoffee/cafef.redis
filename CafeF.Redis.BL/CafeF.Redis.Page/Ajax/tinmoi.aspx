<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tinmoi.aspx.cs" Inherits="CafeF.Ajax.tinmoi" %>
<div class="tmheader clearfix"><div class="tm-title">Tin mới</div><div class="tm-viewall"><a onclick="LoadTinMoiNext();" style="cursor:pointer" class="down" id="aDown">Down</a> <a onclick="LoadTinMoiPre();" style="cursor:pointer" class="up" id="aUp">Up</a></div></div>
<ul>
    <asp:repeater id="repListNews" runat="server">
        <ItemTemplate>
            <li >
                <div style="float:left; width:34px">
                    <%# Eval("PublishDate").ToString()%>
                </div>
                <div style="float:left; width:330px">
                    <a href="<%# Eval("NewsUrl") %>" title="<%# Eval("News_Title_Encode") %>"><%# Eval("News_Title") %></a>
                </div>
             </li>        
        </ItemTemplate>
    </asp:repeater>
</ul>
