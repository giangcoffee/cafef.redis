<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucBanLanhDaoV2.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.StockView.ucBanLanhDaoV2" %>
<style type="text/css">
    .cl_ceo tr {text-align:center;}
    .cl_ceo td {font-weight: normal;padding-top: 5px;color: #666666;padding-bottom: 5px;}
</style>
<div style="overflow: hidden; vertical-align: middle; float: right; margin-right: 10px; text-align: right;">
</div>
<div class="r" style="font-weight: bold;">
    <a href="/<%= CenterName %>/<%= Symbol %>/bao-cao-tai-chinh.chn" onclick="javascript:changeTabCongTy(5); return false;" id="lsTab5CT">Tải xuống BCTC &amp; Báo cáo khác</a></div>
<br />
<table style="width: 100%" cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td>
            <div id="divBanLanhDao" style="float: left;" class="BanLanhDaoCoCuSoHuu_<%= TabBLD ? "": "Un"%>Sel">
                <a title="Ban lãnh đạo" href="/<%= CenterName %>/<%= Symbol %>/ban-lanh-dao.chn" onclick="changeTabBanLanhDao(1); return false;">Ban lãnh đạo</a>
            </div>
            <div style="float: left;">
                &nbsp;&nbsp;</div>
            <div id="divCoDongLon" style="float: left;" class="BanLanhDaoCoCuSoHuu_<%= TabBLD ? "Un": ""%>Sel">
                <a title="Cổ đông lớn" href="/<%= CenterName %>/<%= Symbol %>/co-dong-lon.chn" onclick="changeTabBanLanhDao(2); return false;">Cổ đông lớn</a>
            </div>
            <div style="float: left;">
                &nbsp;&nbsp;</div>
            <div id="divNoiBo" style="float: left; width: 200px; padding-top: 5px; padding-bottom: 5px;" class="BanLanhDaoCoCuSoHuu_UnSel">
                <a title="GD cổ đông nội bộ &amp; cổ đông lớn" target="_blank" href="http://cafef.vn/Lich-su-giao-dich-<%= Symbol %>-4.chn">GD cổ đông nội bộ &amp; cổ đông lớn</a>
            </div>
        </td>
    </tr>
</table>
<div>
    <div style="text-align: left" id="divViewBanLanhDao">
        <table cellspacing="0" cellpadding="0" border="0" style="width: 100%">
            <tr style='<%=style1%>'>
                <td style="padding-bottom: 3px; color: #004370; font-weight:bold;">
                    HỘI ĐỒNG QUẢN TRỊ
                </td>
            </tr>
            <tr style='<%=style1%>'>
                <td style="height: 1px; width: 100%; background: #D6D6D6;">
                </td>
            </tr>
            <tr style='<%=style1%>'>
                <td>
                    <table class="cl_ceo" cellspacing="0" cellpadding="0" border="0" style="width: 100%">
                        <tr align="center" style="background-color:#F6F6F6;font-family:Arial,Helvetica,sans-serif;">
                            <td style="width: 25%; font-weight: bold">
                                Chức vụ
                            </td>
                            <td style="width: 25%; font-weight: bold">
                                Họ tên
                            </td>
                            <td style="width: 12%">
                            </td>
                            <td style="width: 8%; font-weight: bold">
                                Tuổi
                            </td>
                            <td style="width: 30%; font-weight: bold;">
                                Quá trình công tác
                            </td>
                        </tr>
                        <asp:Repeater EnableViewState="false" ID="rpHDQT" runat="server">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr valign="top">
                                    <td>
                                        <%# Eval("Positions")%>
                                    </td>
                                    <td align="left">
                                        <a href="<%# makeLink(Eval("CeoCode").ToString(),Eval("Name").ToString()) %>"><%# Eval("Name")%></a>
                                    </td>
                                    <td>
                                        <a href="<%# makeLink(Eval("CeoCode").ToString(),Eval("Name").ToString()) %>" title="<%# HttpUtility.HtmlEncode(Eval("Name").ToString()).Replace("\"","")%>"><img border="0" alt='<%# Eval("Name")%>' src='<%# StorageServer + "zoom/36_45/" + CeoStoragePath %><%# GetImages(Eval("CeoCode").ToString()) %>' onerror="this.src='http://cafef3.vcmedia.vn/v2/images/noimage.jpg';" width="36px" height="45px" /></a>
                                    </td>
                                    <td>
                                        <%# convertAge(Eval("Age").ToString())%>
                                    </td> 
                                    <td style="text-align: justify">
                                        <%# Eval("Process")%><br />
                                        <span style="float: right"><a href='<%# makeLink(Eval("CeoCode").ToString(),Eval("Name").ToString()) %>' target="_blank">Chi tiết...</a></span>
                                    </td>
                                </tr>
                            </ItemTemplate>
                           <AlternatingItemTemplate>
                                <tr valign="top" style="background-color:#F6F6F6;">
                                    <td>
                                        <%# Eval("Positions")%>
                                    </td>
                                    <td align="left">
                                        <a href="<%# makeLink(Eval("CeoCode").ToString(),Eval("Name").ToString()) %>"><%# Eval("Name")%></a>
                                    </td>
                                    <td>
                                         <a href="<%# makeLink(Eval("CeoCode").ToString(),Eval("Name").ToString()) %>" title="<%# HttpUtility.HtmlEncode(Eval("Name").ToString()).Replace("\"","")%>"><img border="0" alt='<%# Eval("Name")%>' src='<%# StorageServer + "zoom/36_45/" + CeoStoragePath %><%# GetImages(Eval("CeoCode").ToString()) %>' onerror="this.src='http://cafef3.vcmedia.vn/v2/images/noimage.jpg';" width="36px" height="45px" /></a>
                                    </td>
                                    <td>
                                        <%# convertAge(Eval("Age").ToString())%>
                                    </td> 
                                    <td style="text-align: justify">
                                        <%# Eval("Process")%><br />
                                        <span style="float: right"><a href='<%# makeLink(Eval("CeoCode").ToString(),Eval("Name").ToString()) %>' target="_blank">Chi tiết...</a></span>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                            <FooterTemplate>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
            <tr style='<%=style1%>'>
                <td style="height: 1px; width: 100%; background: #D6D6D6;">
                </td>
            </tr>
            <tr style='<%=style2%>'>
                <td style="padding: 5px 0 5px 0; color: #004370; font-weight:bold;">
                    BAN GIÁM ĐỐC/KẾ TOÁN TRƯỞNG
                </td>
            </tr>
            <tr style='<%=style2%>'>
                <td style="height: 1px; width: 100%; background: #D6D6D6;">
                </td>
            </tr>
            <tr style='<%=style2%>'>
                <td>
                    <table class="cl_ceo" cellspacing="0" cellpadding="0" border="0" style="width: 100%">
                        <tr align="center" style="background-color:#F6F6F6;font-family:Arial,Helvetica,sans-serif;">
                            <td style="width: 25%; font-weight: bold">
                                Chức vụ
                            </td>
                            <td style="width: 25%; font-weight: bold">
                                Họ tên
                            </td>
                            <td style="width: 12%">
                            </td>
                            <td style="width: 8%; font-weight: bold">
                                Tuổi
                            </td>
                            <td style="width: 30%; font-weight: bold;">
                                Quá trình công tác
                            </td>
                        </tr>
                        <asp:Repeater EnableViewState="false" ID="rptBGD" runat="server">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr valign="top">
                                    <td>
                                        <%# Eval("Positions")%>
                                    </td>
                                    <td align="left">
                                        <a href="<%# makeLink(Eval("CeoCode").ToString(),Eval("Name").ToString()) %>" title="<%# HttpUtility.HtmlEncode(Eval("Name").ToString()).Replace("\"","")%>"><%# Eval("Name")%></a>
                                    </td>
                                    <td>
                                         <a href="<%# makeLink(Eval("CeoCode").ToString(),Eval("Name").ToString()) %>" title="<%# HttpUtility.HtmlEncode(Eval("Name").ToString()).Replace("\"","")%>"><img border="0" alt='<%# Eval("Name")%>' src='<%# StorageServer + "zoom/36_45/" + CeoStoragePath %><%# GetImages(Eval("CeoCode").ToString()) %>' onerror="this.src='http://cafef3.vcmedia.vn/v2/images/noimage.jpg';" width="36px" height="45px" /></a>
                                    </td>
                                    <td>
                                        <%# convertAge(Eval("Age").ToString())%>
                                    </td>
                                    <td style="text-align: justify">
                                        <%# Eval("Process")%><br />
                                        <span style="float: right"><a href="<%# makeLink(Eval("CeoCode").ToString(),Eval("Name").ToString()) %>" target="_blank">Chi tiết...</a></span>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr valign="top" style="background-color:#F6F6F6">
                                    <td>
                                        <%# Eval("Positions")%>
                                    </td>
                                    <td align="left">
                                        <a href="<%# makeLink(Eval("CeoCode").ToString(),Eval("Name").ToString()) %>" title="<%# HttpUtility.HtmlEncode(Eval("Name").ToString()).Replace("\"","")%>"><%# Eval("Name")%></a>
                                    </td>
                                    <td>
                                        <a href="<%# makeLink(Eval("CeoCode").ToString(),Eval("Name").ToString()) %>" title="<%# HttpUtility.HtmlEncode(Eval("Name").ToString()).Replace("\"","")%>"><img border="0" alt='<%# Eval("Name")%>' src='<%# StorageServer + "zoom/36_45/" + CeoStoragePath %><%# GetImages(Eval("CeoCode").ToString()) %>' onerror="this.src='http://cafef3.vcmedia.vn/v2/images/noimage.jpg';" width="36px" height="45px" /></a>
                                    </td>
                                    <td>
                                        <%# convertAge(Eval("Age").ToString())%>
                                    </td>
                                    <td style="text-align: justify">
                                        <%# Eval("Process")%><br />
                                        <span style="float: right"><a href="<%# makeLink(Eval("CeoCode").ToString(),Eval("Name").ToString()) %>" target="_blank">Chi tiết...</a></span>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                            <FooterTemplate>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
            <tr style='<%=style2%>'>
                <td style="height: 1px; width: 100%; background: #D6D6D6;">
                </td>
            </tr>
            <tr style='<%=style3%>'>
                <td style="padding: 5px 0 5px 0; color: #004370; font-weight:bold;">
                    BAN KIẾM SOÁT
                </td>
            </tr>
            <tr style='<%=style3%>'>
                <td style="height: 1px; width: 100%; background: #D6D6D6;">
                </td>
            </tr>
            <tr style='<%=style3%>'>
                <td>
                    <table class="cl_ceo" cellspacing="0" cellpadding="0" border="0" style="width: 100%">
                        <tr align="center" style="background-color:#F6F6F6;font-family:Arial,Helvetica,sans-serif;">
                            <td style="width: 25%; font-weight: bold">
                                Chức vụ
                            </td>
                            <td style="width: 25%; font-weight: bold">
                                Họ tên
                            </td>
                            <td style="width: 12%">
                            </td>
                            <td style="width: 8%; font-weight: bold">
                                Tuổi
                            </td>
                            <td style="width: 30%; font-weight: bold;">
                                Quá trình công tác
                            </td>
                        </tr>
                        <asp:Repeater EnableViewState="false" ID="rptBKS" runat="server">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr valign="top">
                                    <td>
                                        <%# Eval("Positions")%>
                                    </td>
                                    <td align="left">
                                        <a href="<%# makeLink(Eval("CeoCode").ToString(),Eval("Name").ToString()) %>" title="<%# HttpUtility.HtmlEncode(Eval("Name").ToString()).Replace("\"","")%>"><%# Eval("Name")%></a>
                                    </td>
                                    <td>
                                        <a href="<%# makeLink(Eval("CeoCode").ToString(),Eval("Name").ToString()) %>" title="<%# HttpUtility.HtmlEncode(Eval("Name").ToString()).Replace("\"","")%>"><img border="0" alt='<%# Eval("Name")%>' src='<%# StorageServer + "zoom/36_45/" + CeoStoragePath %><%# GetImages(Eval("CeoCode").ToString()) %>' onerror="this.src='http://cafef3.vcmedia.vn/v2/images/noimage.jpg';" width="36px" height="45px" /></a>
                                    </td>
                                    <td>
                                       <%# convertAge(Eval("Age").ToString())%>
                                    </td>
                                    <td style="text-align: justify">
                                        <%# Eval("Process")%><br />
                                        <span style="float: right"><a href="<%# makeLink(Eval("CeoCode").ToString(),Eval("Name").ToString()) %>" target="_blank">Chi tiết...</a></span>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr valign="top" style="background-color:#F6F6F6;">
                                    <td>
                                        <%# Eval("Positions")%>
                                    </td>
                                    <td align="left">
                                        <a href="<%# makeLink(Eval("CeoCode").ToString(),Eval("Name").ToString()) %>" title="<%# HttpUtility.HtmlEncode(Eval("Name").ToString()).Replace("\"","")%>"><%# Eval("Name")%></a>
                                    </td>
                                    <td>
                                         <a href="<%# makeLink(Eval("CeoCode").ToString(),Eval("Name").ToString()) %>" title="<%# HttpUtility.HtmlEncode(Eval("Name").ToString()).Replace("\"","")%>"><img border="0" alt='<%# Eval("Name")%>' src='<%# StorageServer + "zoom/36_45/" + CeoStoragePath %><%# GetImages(Eval("CeoCode").ToString()) %>' onerror="this.src='http://cafef3.vcmedia.vn/v2/images/noimage.jpg';" width="36px" height="45px" /></a>
                                    </td>
                                    <td>
                                       <%# convertAge(Eval("Age").ToString())%>
                                    </td>
                                    <td style="text-align: justify">
                                        <%# Eval("Process")%><br />
                                        <span style="float: right"><a href="<%# makeLink(Eval("CeoCode").ToString(),Eval("Name").ToString()) %>" target="_blank">Chi tiết...</a></span>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                            <FooterTemplate>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="divViewCoDongLon" style="display: none; text-align: right;">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; font-family: Arial; vertical-align: middle; color: #666666; font-size: 12px;">
            <tr>
                <td align="right">
                    <table border="0" cellpadding="0" cellspacing="5" runat="server" id="tblKLCP">
                       <%-- <tr style="display: none">
                            <td style="border: 1px solid #e2e2e2; padding: 5px 20px 5px 20px; font-weight: bold">
                                Khối lượng CP đang niêm yết:
                                <asp:Literal ID="ltrKhoiluongNiemyet" runat="server"></asp:Literal>
                            </td>
                            <td style="border: 1px solid #e2e2e2; padding: 5px 20px 5px 20px; font-weight: bold;">
                                <asp:Literal ID="ltrTitle" runat="server" Text="Khối lượng CP đang lưu hành:"></asp:Literal>
                                <asp:Literal ID="ltrSLCPLuuHanh" runat="server"></asp:Literal>
                            </td>
                        </tr>--%>
                        <tr>
                            <td style="border: 1px solid #e2e2e2; padding: 5px 20px 5px 20px; font-weight: bold; color: #343434" colspan="2" align="right">
                                KL CP đang niêm yết :&nbsp;<asp:Literal ID="ltrVonDieuLe" runat="server"></asp:Literal>&nbsp;cp
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="font-weight: normal; line-height: 20px">
                <td style="border-top: solid 1px #c9c9c9;" align="left">
                    <asp:Repeater runat="server" ID="rptBanLanhDao">
                        <HeaderTemplate>
                            <table cellpadding="2" cellspacing="0" width="100%" border="0">
                                <tr>
                                    <td align="left" class="BlockTitle">
                                        TÊN CỔ ĐÔNG
                                    </td>
                                    <td align="right" class="BlockTitle">
                                        SỐ CỔ PHIẾU
                                    </td>
                                    <td align="right" class="BlockTitle">
                                        TỶ LỆ %(*)
                                    </td>
                                    <td align="center" class="BlockTitle">
                                        TÍNH ĐẾN NGÀY
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                        <ItemTemplate>
                            <tr>
                                <td align="left">
                                    <%# DataBinder.Eval(Container.DataItem, "Name")%>
                                </td>
                                <td align="right">
                                    <%# (Eval("Volume") != null ? System.Convert.ToDouble(Eval("Volume")).ToString("#,##0.##") : "")%>
                                </td>
                                <td align="right">
                                    <%# (Eval("Rate") != null ? System.Convert.ToDouble(Eval("Rate")).ToString("#,##0.0#") : "")%>
                                </td>
                                <td align="center">
                                    <%# (Eval("ToDate") != null ? System.Convert.ToDateTime(Eval("ToDate")).ToString("dd/MM/yyyy") : "")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr backcolor="#F2F2F2">
                                <td align="left">
                                    <%# DataBinder.Eval(Container.DataItem, "Name")%>
                                </td>
                                <td align="right">
                                    <%# (Eval("Volume") != null ? System.Convert.ToDouble(Eval("Volume")).ToString("#,##0.##") : "")%>
                                </td>
                                <td align="right">
                                    <%# (Eval("Rate") != null ? System.Convert.ToDouble(Eval("Rate")).ToString("#,##0.0#") : "")%>
                                </td>
                                <td align="center">
                                    <%# (Eval("ToDate") != null ? System.Convert.ToDateTime(Eval("ToDate")).ToString("dd/MM/yyyy") : "")%>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                    <span class="dltlonote" style="padding-left: 5px; padding-top: 5px; text-align: left;">(*):% sở hữu trên vốn điều lệ</span>
                </td>
            </tr>
        </table>
    </div>
</div>
