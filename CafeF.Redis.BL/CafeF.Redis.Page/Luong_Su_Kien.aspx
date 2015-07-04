<%@ Page Language="C#" MasterPageFile="~/MasterPage/SoLieu.Master" AutoEventWireup="true" CodeBehind="Luong_Su_Kien.aspx.cs" Inherits="CafeF.Redis.Page.Luong_Su_Kien"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="ListCateDiv1" align="center" style="width: 980px;">
    <div style="overflow: hidden">
        <div class="floatLeft">
            <img alt="" src="/images/images/conner_top_left.gif" /></div>
        <div style="float: right">
            <img alt="" src="/images/images/conner_top_right.gif" /></div>
    </div>
    <div align="left" class="ListCateDiv2">
        <div style="padding-left: 20px">
            <span class="sukien_DOANHNGHIEP_link"><asp:Literal runat="server" ID="ltrThreadName"></asp:Literal> </span></div>
    </div>
    <div style="font-family: Arial;padding: 5px 30px 0px 30px; text-align: left; font-size: 12px; font-weight: bold;"><asp:Label runat="server" ID="lblAuthour"></asp:Label></div>
    <div style="padding: 5px 30px 10px 30px; text-align: left; font-family: Arial;">
        <asp:Repeater ID="rptFavoriteNews" runat="server" OnItemDataBound="rptFavoriteNews_ItemDataBound">
            <ItemTemplate>
                <table border="0" cellpadding="3" cellspacing="0" style="margin-bottom: 10px;width:50%">
                    <tr>
                        <td style="float:left; text-align:left; width:400px " >
                            <img class="ListCateDiv4" src="/images/images/Home_r35_c23.jpg" alt=""/>
                            <asp:Literal runat="server" ID="ltrNews_Title"></asp:Literal> <span class="dxncItemDate_news"><asp:Label runat="server" ID="lblPublishDate"></asp:Label></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="dxncItemContent_news" style="font-size:12px;">                                                       
                            <asp:Literal runat="server" ID="ltrDescription"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div style="padding: 5px 30px 10px 300px; text-align: left; font-family: Arial;">
            
            <asp:Repeater EnableViewState="false" ID="rptPage" runat="server" OnItemDataBound="rptPage_ItemDataBound">
                <HeaderTemplate>
                    <table class="CafeF_Paging" border="0" cellpadding="3" cellspacing="3">
                        <tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <td align="center" style="width: 20px">
                        <asp:Literal ID="ltrPage" runat="server"></asp:Literal></td>
                </ItemTemplate>
                <FooterTemplate>
                    </tr> </table>
                </FooterTemplate>
            </asp:Repeater>
        
    </div>
    <div style="overflow: hidden">
        <div class="floatLeft"">
            <img alt="" src="/images/images/conner_bottom_left.gif" /></div>
            <div style="float: right">
        <img alt="" src="/images/images/conner_bottom_right.gif" /></div>
    </div>
</div>
<%--<script type="text/javascript" src="http://reporting.cafef.channelvn.net/js.js?dat123456"></script>--%>
<script type="text/javascript" src="http://cafef3.vcmedia.vn/reporting/log.js"></script>
    <script type="text/javascript">
        var log_website = 'cafef_reporting';        
        Log_AssignValue("-1", "", "1114", "luongsukien");
    </script> 
</asp:Content>
