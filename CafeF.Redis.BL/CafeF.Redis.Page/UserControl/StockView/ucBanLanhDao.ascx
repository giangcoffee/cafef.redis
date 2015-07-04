<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucBanLanhDao.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.StockView.ucBanLanhDao" %>
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
        <asp:Literal ID="ltrBanLanhdao" runat="server"></asp:Literal>
    </div>
    <div id="divViewCoDongLon" style="display: none; text-align: right;">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; font-family: Arial; vertical-align: middle; color: #666666; font-size: 12px;">
            <tr>
                <td align="right">
                    <table border="0" cellpadding="0" cellspacing="5" runat="server" id="tblKLCP">
                        <tr style="display: none">
                            <td style="border: 1px solid #e2e2e2; padding: 5px 20px 5px 20px; font-weight: bold">
                                Khối lượng CP đang niêm yết:
                                <asp:Literal ID="ltrKhoiluongNiemyet" runat="server"></asp:Literal>
                            </td>
                            <td style="border: 1px solid #e2e2e2; padding: 5px 20px 5px 20px; font-weight: bold;">
                                <asp:Literal ID="ltrTitle" runat="server" Text="Khối lượng CP đang lưu hành:"></asp:Literal>
                                <asp:Literal ID="ltrSLCPLuuHanh" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid #e2e2e2; padding: 5px 20px 5px 20px; font-weight: bold; color: #343434" colspan="2" align="right">
                                Vốn điều lệ:&nbsp;<asp:Literal ID="ltrVonDieuLe" runat="server"></asp:Literal>&nbsp;đồng
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
