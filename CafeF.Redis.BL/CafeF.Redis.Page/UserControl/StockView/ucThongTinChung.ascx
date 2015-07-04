<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucThongTinChung.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.StockView.ucThongTinChung" EnableViewState="false" %>
<style type="text/css">
    #companyinfo {font-weight: normal;}
    #companyinfo ul li{list-style-type: disc; list-style-position: inside; margin: 4px 2px 4px 10px;}
</style>
<div style="padding: 0px 5px 5px 5px; text-align: left;" id="companyinfo">
    <div class="r" style="font-weight: bold;">
        <a href="/<%= CenterName %>/<%= Symbol %>/bao-cao-tai-chinh.chn" onclick="javascript:changeTabCongTy(5); return false;" id="lsTab5CT">Tải xuống BCTC &amp; Báo cáo khác</a></div>
    <br />
    <asp:Label ID="lblTitle_Nhomnganh" runat="server" ForeColor="#004377" Font-Bold="True" Text="Nhóm ngành:"></asp:Label>
    <asp:Literal ID="ltrContent_Nhomnganh" runat="server"></asp:Literal>
    <br />
    <asp:Label ID="lblTitle_VonDieule" runat="server" ForeColor="#004377" Font-Bold="True" Text="Vốn điều lệ:"></asp:Label>
    <asp:Literal ID="ltrContent_VonDieule" runat="server"></asp:Literal>&nbsp;đồng<br />
    <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#004377" Text="KL CP đang niêm yết:"></asp:Label>
    <asp:Literal ID="ltrKLCPNiemYet" runat="server"></asp:Literal>&nbsp;cp<br />
    <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#004377" Text="KL CP đang lưu hành:"></asp:Label>
    <asp:Literal ID="ltrKLCPLuuHanh" runat="server"></asp:Literal>&nbsp;cp<br /><br />
    <asp:Panel runat="server" ID="pnAuditConsultant">
        <asp:Panel runat="server" ID="pnConsultant">
            <span><b style="color:#004377;">Tổ chức tư vấn niêm yết:</b></span>
            &nbsp;&nbsp;&nbsp;<asp:Literal ID="ltrConsultant" runat="server" />
            <asp:Panel runat="server" ID="pnConsultantStock" style="display: inline;">
            &nbsp;-&nbsp;Mã CK: <asp:Literal ID="ltrConsultantStock" runat="server" />
            </asp:Panel>
            <br />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnAudit">
            <span><b style="color:#004377;">Tổ chức kiểm toán:</b></span>
            &nbsp;&nbsp;&nbsp;<asp:Literal ID="ltrAudit" runat="server" /><br />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnBusiness">
            <span><b style="color:#004377;">Mã số đăng ký kinh doanh:</b></span>
            &nbsp;&nbsp;&nbsp;<asp:Literal ID="ltrBusinessLicense" runat="server" /><br />
        </asp:Panel>
        <br />
    </asp:Panel>
    <asp:Label ID="lblTitle_Gioithieu" runat="server" Font-Bold="True" Text="Giới thiệu:"></asp:Label>
    <asp:Literal ID="ltrContent_Gioithieu" runat="server"></asp:Literal>
</div>
