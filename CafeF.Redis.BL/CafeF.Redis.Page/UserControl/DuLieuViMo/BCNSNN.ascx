<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BCNSNN.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.DuLieuViMo.BCNSNN" %>

<div>        
        <table>
            <tr>
                <td colspan="7" valign="top">
                    <strong>ĐẦU TƯ TRỰC TIẾP NƯỚC NGOÀI</strong></td>
                <td colspan="1" style="height:25px;">
                </td>
            </tr>            
            <tr>
                <td colspan="7" valign="top" style="height:25px;">
                    <asp:CheckBox ID="chkIsLabel" runat="server" Text=" Hiển thị label" /></td>
                <td colspan="1">
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFrom" runat="server" Text="Chọn"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dlYear1" runat="server">
                        <asp:ListItem Value="2011">2011</asp:ListItem>
                        <asp:ListItem Value="2010">2010</asp:ListItem>
                        <asp:ListItem Value="2009">2009</asp:ListItem>
                        <asp:ListItem Value="2008">2008</asp:ListItem>
                        <asp:ListItem Value="2007">2007</asp:ListItem>
                        <asp:ListItem Value="2006">2006</asp:ListItem>
                        <asp:ListItem Value="2005">2005</asp:ListItem>
                        <asp:ListItem Value="2004">2004</asp:ListItem>
                        <asp:ListItem Value="2003">2003</asp:ListItem>
                        <asp:ListItem Value="2002">2002</asp:ListItem>
                        <asp:ListItem Value="2001">2001</asp:ListItem>
                        <asp:ListItem Value="2000">2000</asp:ListItem>
                        <asp:ListItem Value="1999">1999</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="dlYear2" runat="server">
                        <asp:ListItem Value="2011">2011</asp:ListItem>
                        <asp:ListItem Value="2010">2010</asp:ListItem>
                        <asp:ListItem Value="2009">2009</asp:ListItem>
                        <asp:ListItem Value="2008">2008</asp:ListItem>
                        <asp:ListItem Value="2007">2007</asp:ListItem>
                        <asp:ListItem Value="2006">2006</asp:ListItem>
                        <asp:ListItem Value="2005">2005</asp:ListItem>
                        <asp:ListItem Value="2004">2004</asp:ListItem>
                        <asp:ListItem Value="2003">2003</asp:ListItem>
                        <asp:ListItem Value="2002">2002</asp:ListItem>
                        <asp:ListItem Value="2001">2001</asp:ListItem>
                        <asp:ListItem Value="2000">2000</asp:ListItem>
                        <asp:ListItem Value="1999">1999</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Từ"></asp:Label>
                    <asp:DropDownList ID="dlMonth1" runat="server">
                        <asp:ListItem Value="1">Th&#225;ng 01</asp:ListItem>
                        <asp:ListItem Value="2">Th&#225;ng 02</asp:ListItem>
                        <asp:ListItem Value="3">Th&#225;ng 03</asp:ListItem>
                        <asp:ListItem Value="4">Th&#225;ng 04</asp:ListItem>
                        <asp:ListItem Value="5">Th&#225;ng 05</asp:ListItem>
                        <asp:ListItem Value="6">Th&#225;ng 06</asp:ListItem>
                        <asp:ListItem Value="7">Th&#225;ng 07</asp:ListItem>
                        <asp:ListItem Value="8">Th&#225;ng 08</asp:ListItem>
                        <asp:ListItem Value="9">Th&#225;ng 09</asp:ListItem>
                        <asp:ListItem Value="10">Th&#225;ng 10</asp:ListItem>
                        <asp:ListItem Value="11">Th&#225;ng 11</asp:ListItem>
                        <asp:ListItem Value="12">Th&#225;ng 12</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblTo" runat="server" Text="Đến"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dlMonth2" runat="server">
                        <asp:ListItem Value="1">Th&#225;ng 01</asp:ListItem>
                        <asp:ListItem Value="2">Th&#225;ng 02</asp:ListItem>
                        <asp:ListItem Value="3">Th&#225;ng 03</asp:ListItem>
                        <asp:ListItem Value="4">Th&#225;ng 04</asp:ListItem>
                        <asp:ListItem Value="5">Th&#225;ng 05</asp:ListItem>
                        <asp:ListItem Value="6">Th&#225;ng 06</asp:ListItem>
                        <asp:ListItem Value="7">Th&#225;ng 07</asp:ListItem>
                        <asp:ListItem Value="8">Th&#225;ng 08</asp:ListItem>
                        <asp:ListItem Value="9">Th&#225;ng 09</asp:ListItem>
                        <asp:ListItem Value="10">Th&#225;ng 10</asp:ListItem>
                        <asp:ListItem Value="11">Th&#225;ng 11</asp:ListItem>
                        <asp:ListItem Value="12">Th&#225;ng 12</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="height:25px;">
                    <asp:Button ID="btnView" Width="80px" runat="server" Text="Xem" OnClick="btnView_Click" />
                    <input style="width:90px" id="btnZoom" onclick="openw('/UserControl/DuLieuViMo/Popup.aspx?type=vdttnsnn');"
                        type="button" value="Phóng to" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="7" valign="middle" style="height:35px;">
                    <asp:RadioButtonList ID="radType" runat="server" AutoPostBack="True" 
                        OnSelectedIndexChanged="radType_SelectedIndexChanged" 
                        RepeatDirection="Horizontal" RepeatLayout="Flow" >
                        <asp:ListItem Selected="True" Value="1">Tổng số</asp:ListItem>
                        <asp:ListItem Value="2">Trung ương</asp:ListItem>
                        <asp:ListItem Value="3">Địa phương</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td colspan="1">
                </td>
            </tr>
        </table>
        <br />
        <asp:Literal ID="ltrChart" runat="server"></asp:Literal>
        <asp:HiddenField ID="hdfChart" runat="server" Value="" />            
    </div>
