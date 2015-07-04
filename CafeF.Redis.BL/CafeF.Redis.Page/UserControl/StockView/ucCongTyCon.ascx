<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCongTyCon.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.StockView.ucCongTyCon" %>
<style type="text/css">
    .congtycon td { padding: 2px;}
</style>
<div class="r" style="font-weight:bold;"><a href="/<%= CenterName %>/<%= Symbol %>/bao-cao-tai-chinh.chn" onclick="javascript:changeTabCongTy(5); return false;" id="lsTab5CT">Tải xuống BCTC &amp; Báo cáo khác</a></div>
<br />
<table style="width: 100%" cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td>
            <table cellpadding="2" cellspacing="0" width="100%" border="0" style="border: solid 1px #e6e6e6;
                border-bottom: none; border-right: none; border-left: none" class="congtycon">
                <tr style="background-color: #F2F2F2; height: 25px">
                    <td style="color: #004377; font-weight: bold; text-align: left; font-size: 11px;
                        width: 45%; vertical-align: top">
                    </td>
                    <td style="width: 70px; color: #004377; font-weight: bold; text-align: center; font-size: 11px;
                        border-left: solid 1px #e6e6e6; vertical-align: top">
                        Vốn điều lệ
                        <br />
                        <span style="font-size: 11px; font-weight: normal; font-style: italic; color: #000">
                            (tỷ đồng)</span>
                    </td>
                    <td style="width: 70px; color: #004377; font-weight: bold; text-align: center; font-size: 11px;
                        border-left: solid 1px #e6e6e6; vertical-align: top">
                        Vốn góp
                        <br />
                        <span style="font-size: 11px; font-weight: normal; font-style: italic; color: #000">
                            (tỷ đồng)</span>
                    </td>
                    <td style="width: 97px; color: #004377; font-weight: bold; text-align: center; font-size: 11px;
                        border-left: solid 1px #e6e6e6; vertical-align: top">
                        Tỷ lệ sở hữu
                        <br />
                        <span style="font-size: 11px; font-weight: normal; font-style: italic; color: #000">
                            (%)</span>
                    </td>
                    <td style="color: #004377; font-weight: bold; text-align: center; font-size: 11px;
                        border-left: solid 1px #e6e6e6; vertical-align: top">
                        Ghi chú
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:repeater runat="server" id="rptData" onitemdatabound="rptData_ItemDataBound">
                <HeaderTemplate>
                    <table cellpadding="2" cellspacing="0" width="100%" border="0" style="border:solid 1px #e6e6e6;border-bottom:none;border-left:none;border-right:none" class="congtycon">
                        <tr>
                            <td colspan="7" style="padding-top:5px;border-bottom:solid 1px #e6e6e6;">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                    <tr style="background-image: url('/images/CompanyInfo/banlanhdao_sohuu_bg.gif');
                                        background-repeat: repeat-x;">
                                        <td style="height: 18px">
                                            <div id="divCongTyCon" style="float: left;" runat="server" class="BlockTitle">
                                                <asp:HyperLink runat="server" ID="hplCongTyCon" Text="CÔNG TY CON" ToolTip="Công ty con góp vốn"
                                                    Font-Bold="True"></asp:HyperLink>
                                            </div>
                                            <div style="float: left">
                                                &nbsp;&nbsp;</div>
                                            <div style="float: left">
                                                &nbsp;&nbsp;</div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                </HeaderTemplate>
                <FooterTemplate>
</table>
</FooterTemplate>
<itemtemplate>
                    <tr style="font-weight:normal;">
                        <td style="vertical-align:top;width:45%;"><%# DataBinder.Eval(Container.DataItem, "Name")%></td>
                        <td style="text-align: right;border-left:solid 1px #e6e6e6;vertical-align:top;width: 70px;"><%# String.Format("{0:#,#0.##}", DataBinder.Eval(Container.DataItem, "TotalCapital"))%></td>
                        <td style="text-align: right;border-left:solid 1px #e6e6e6;vertical-align:top;width: 70px;"><%#  String.Format("{0:#,#0.##}", DataBinder.Eval(Container.DataItem, "SharedCapital"))%></td>
                        <td style="text-align: right;border-left:solid 1px #e6e6e6;vertical-align:top;width:45px"><%#  String.Format("{0:#,#0.##}", DataBinder.Eval(Container.DataItem, "OwnershipRate"))%>%</td>  
                        <td style="vertical-align:top;width:45px;"><asp:Literal runat="server" ID="ltrChart"></asp:Literal></td>                      
                        <td style="text-align: left;border-left:solid 1px #e6e6e6;vertical-align:top"><%# DataBinder.Eval(Container.DataItem, "Note")%>&nbsp;</td>
                        
                    </tr>
                </itemtemplate>
<alternatingitemtemplate>
                    <tr style="background-color: #F6F6F6;font-weight:normal;">
                        <td style="vertical-align:top"><%# DataBinder.Eval(Container.DataItem, "Name")%></td>
                        <td style="text-align: right;border-left:solid 1px #e6e6e6;vertical-align:top"><%# String.Format("{0:#,#0.##}", DataBinder.Eval(Container.DataItem, "TotalCapital"))%></td>
                        <td style="text-align: right;border-left:solid 1px #e6e6e6;vertical-align:top" ><%#  String.Format("{0:#,#0.##}", DataBinder.Eval(Container.DataItem, "SharedCapital"))%></td>
                        <td style="text-align: right;border-left:solid 1px #e6e6e6;vertical-align:top;width:45px" ><%#  String.Format("{0:#,#0.##}", DataBinder.Eval(Container.DataItem, "OwnershipRate"))%>%</td>                        
                        <td style="vertical-align:top;width:45px;">                            <asp:Literal runat="server" ID="ltrChart"></asp:Literal></td>
                        <td style="text-align: left;border-left:solid 1px #e6e6e6;vertical-align:top" ><%# DataBinder.Eval(Container.DataItem, "Note")%>&nbsp;</td>
                        
                    </tr>
                </alternatingitemtemplate>
</asp:Repeater> </td> </tr>
<tr>
    <td style="padding-top: 10px">
    </td>
</tr>
<tr>
    <td>
        <asp:repeater runat="server" id="rptCtyLienKet" onitemdatabound="rptCtyLienKet_ItemDataBound">
                <HeaderTemplate>
                    <table cellpadding="2" cellspacing="0" width="100%" border="0" style="border:solid 1px #e6e6e6;border-bottom:none;border-left:none;border-right:none" class="congtycon">
                        <tr>
                            <td colspan="7" style="padding-top:5px;border-bottom:solid 1px #e6e6e6;">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                    <tr style="background-image: url('/images/CompanyInfo/banlanhdao_sohuu_bg.gif');
                                        background-repeat: repeat-x;">
                                        <td style="height: 18px">
                                            <div id="divCongTyLienKet" style="float: left" runat="server" class="BlockTitle">
                                                <asp:HyperLink runat="server" ID="hplCongTyLienKet" Text="CÔNG TY LIÊN KẾT" ToolTip="Công ty liên kết góp vốn"
                                                    Font-Bold="True"></asp:HyperLink>
                                            </div>
                                            <div style="float: left">&nbsp;&nbsp;</div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                </HeaderTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
                <ItemTemplate>
                    <tr style="font-weight:normal;">
                        <td style="vertical-align:top; width:45%;"><%# DataBinder.Eval(Container.DataItem, "Name")%></td>
                        <td style="text-align: right;border-left:solid 1px #e6e6e6;vertical-align:top;width:70px;"><%# String.Format("{0:#,#0.##}", DataBinder.Eval(Container.DataItem, "TotalCapital"))%></td>
                        <td style="text-align: right;border-left:solid 1px #e6e6e6;vertical-align:top;width:70px;"><%#  String.Format("{0:#,#0.##}", DataBinder.Eval(Container.DataItem, "SharedCapital"))%></td>
                        <td style="text-align: right;border-left:solid 1px #e6e6e6;vertical-align:top;width:45px"><%#  String.Format("{0:#,#0.0#}", DataBinder.Eval(Container.DataItem, "OwnershipRate"))%>%</td>  
                        <td style="vertical-align:top;width:45px;"><asp:Literal runat="server" ID="ltrChart"></asp:Literal></td>                      
                        <td style="text-align: left;border-left:solid 1px #e6e6e6;vertical-align:top"><%# DataBinder.Eval(Container.DataItem, "Note")%>&nbsp;</td>
                        
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr style="background-color: #F6F6F6;font-weight:normal;">
                        <td style="vertical-align:top"><%# DataBinder.Eval(Container.DataItem, "Name")%></td>
                        <td style="text-align: right;border-left:solid 1px #e6e6e6;vertical-align:top"><%# String.Format("{0:#,#0.##}", DataBinder.Eval(Container.DataItem, "TotalCapital"))%></td>
                        <td style="text-align: right;border-left:solid 1px #e6e6e6;vertical-align:top" ><%#  String.Format("{0:#,#0.##}", DataBinder.Eval(Container.DataItem, "SharedCapital"))%></td>
                        <td style="text-align: right;border-left:solid 1px #e6e6e6;vertical-align:top;width:45px" ><%#  String.Format("{0:#,#0.##}", DataBinder.Eval(Container.DataItem, "OwnershipRate"))%>%</td>                        
                        <td style="vertical-align:top;width:45px;">                            <asp:Literal runat="server" ID="ltrChart"></asp:Literal></td>
                        <td style="text-align: left;border-left:solid 1px #e6e6e6;vertical-align:top" ><%# DataBinder.Eval(Container.DataItem, "Note")%>&nbsp;</td>
                        
                    </tr>
                </AlternatingItemTemplate>
            </asp:repeater>
    </td>
</tr>
</table>