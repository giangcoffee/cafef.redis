<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CeoProfile.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.Ceo.CeoProfile" %>
<h2 class="cattitle">
    <a style="float: right; text-transform: none; text-decoration: underline; font-weight: normal;" href="/<%= San %>/<%= Symbol %>/ban-lanh-dao.chn">Quay lại</a>
    THÔNG TIN CHI TIẾT</h2>    
<img style="float:left;margin:0 10px 10px" alt="<%=name %>" src='<%= StorageServer + "thumb_w/150/" + CeoStoragePath %><%= image=="" ? "noimage.jpg" : image %>' onerror="this.src='http://cafef3.vcmedia.vn/v2/images/noimage.jpg';" />
<!-- // left -->
<div style="width:400px;float:left;">
    <table class="ceo_info" cellspacing="10px" cellpadding="10px" border="0" style="width: 100%">
        <tr>
            <td style="width:60px">Tên</td>
            <td>:</td>
            <td style="width:310px"><span style="font-weight:bold; font-size:15px;"><%= name %></span></td>
        </tr>
        <tr>
            <td>Sinh năm</td>
            <td>:</td>
            <td><span><%=birthday %></span></td>
        </tr><% if(!string.IsNullOrEmpty(idNo)){ %>
        <tr>
            <td>Số CMND</td>
            <td>:</td>
            <td><span><%=idNo %></span></td>
        </tr><%} %>
        <tr>
            <td>Quê quán</td>
            <td>:</td>
            <td><span><%=homeTown %></span></td>
        </tr>
        <tr>
            <td valign="top">Học vị</td>
            <td valign="top">:</td>
            <td>
                <span><%=schoolDegree %></span>
                <%--<span><%=achievements %></span> <br />
                <asp:Repeater EnableViewState="false" ID="rpData" runat="server">
                    <HeaderTemplate>
                    <ul>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li><%# Eval("CeoTitle")%><span><%# Eval("SchoolYear")%></span></li>
                    </ItemTemplate>
                    <FooterTemplate>
                    </ul>
                    </FooterTemplate>
                </asp:Repeater>--%>
            </td>
        </tr>
    </table>
</div>