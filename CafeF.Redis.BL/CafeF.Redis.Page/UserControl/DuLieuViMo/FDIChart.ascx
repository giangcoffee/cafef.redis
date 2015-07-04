<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FDIChart.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.DuLieuViMo.FDIChart" %>
<asp:Literal ID="ltrScript" runat="server"></asp:Literal>
<div style="display:none">        
    <table>
        <tr>
            <td colspan="7" valign="top">
                <strong>ĐẦU TƯ TRỨC TIẾP NƯỚC NGOÀI</strong></td>
            <td colspan="1">
            </td>
        </tr>            
        <tr>
            <td colspan="7" valign="top">
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
            <td>
                <asp:Button ID="btnView" Width="80px" runat="server" Text="Vẽ lại" OnClick="btnView_Click" />
                <input style="width:90px" id="btnZoom" onclick="openw('/UserControl/DuLieuViMo/Popup.aspx?type=fdi');"
                    type="button" value="Phóng to" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="7" valign="top">
                <asp:RadioButtonList ID="radType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="radType_SelectedIndexChanged" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Selected="True" Value="1">Số dự &#225;n đăng k&#253; mới (lũy kế)</asp:ListItem>
                    <asp:ListItem Value="2">Tổng vốn đăng k&#253; mới (Triệu USD)</asp:ListItem>
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

<div>
<%--<div style="dlt-ten">Biểu đồ FDI</div>--%>
<a name="duan"></a>
<table>
    <tr>
        <td colspan="7" valign="top">
            <strong>1. DỰ ÁN ĐĂNG KÝ MỚI</strong></td>
        <td colspan="1" style="height:20px;">
        </td>
    </tr>
    <tr>        
        <td colspan="7" valign="top" style="height:25px;">
                    <asp:CheckBox ID="chkIsLabelDuAn" runat="server" Text=" Hiển thị label" /></td>
                <td colspan="1">
                </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Từ"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="dlMonth1DuAn" runat="server">
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
            <asp:DropDownList ID="dlYear1DuAn" runat="server">
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
            <asp:Label ID="Label3" runat="server" Text="Đến"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="dlMonth2DuAn" runat="server">
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
            <asp:DropDownList ID="dlYear2DuAn" runat="server">
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
            <asp:Button ID="btnViewDuAn" Width="80px" runat="server" Text="Vẽ lại" OnClick="btnViewDuAn_Click" />
            <input style="width:90px" id="Button2" type="button" value="Phóng to" onclick="openw('/UserControl/DuLieuViMo/Popup.aspx?type=fdi');" />
        </td>
        <td>
        </td>
    </tr>
</table>
<br />
<asp:Literal ID="ltrChartDuAn" runat="server"></asp:Literal><br />
    <br />
    <div style="width:100%;height:1px; background-color:#D6D6D6;"></div><br />
    <a name="sovon"></a>
<table>
    <tr>
        <td style="height:5px" colspan="7" valign="top">
        <td colspan="1">
        </td>
    </tr>
    <tr>        
        <td colspan="7" valign="top" style="height:25px;">
                    <asp:CheckBox ID="chkIsLabelSoVon" runat="server" Text=" Hiển thị label" /></td>
                <td colspan="1">
                </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label4" runat="server" Text="Từ"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="dlMonth1SoVon" runat="server">
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
            <asp:DropDownList ID="dlYear1SoVon" runat="server">
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
            <asp:Label ID="Label5" runat="server" Text="Đến"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="dlMonth2SoVon" runat="server">
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
            <asp:DropDownList ID="dlYear2SoVon" runat="server">
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
            <asp:Button ID="btnViewSoVon" Width="80px" runat="server" Text="Vẽ lại" OnClick="btnViewSoVon_Click" />
            <input style="width:90px" id="Button3" type="button" value="Phóng to" onclick="openw('/UserControl/DuLieuViMo/Popup.aspx?type=fdivon');" />
        </td>
        <td>
        </td>
    </tr>
</table>
<br />
<div style="float:right;padding-right:120px">Đơn vị: triệu USD</div>
<asp:Literal ID="ltrChartSoVon" runat="server"></asp:Literal><br />
    <br />
    <div style="width:100%;height:1px; background-color:#D6D6D6;"></div><br />
    <a name="giaingan"></a>
<table>
    <tr>
        <td colspan="7" valign="top">
            <strong>2. VỐN FDI GIẢI NGÂN</strong></td>
        <td colspan="1" style="height:20px;"> 
        </td>
    </tr>
    <tr>        
        <td colspan="7" valign="top" style="height:25px;">
                    <asp:CheckBox ID="chkIsLabelGiaiNgan" runat="server" Text=" Hiển thị label" /></td>
                <td colspan="1">
                </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label6" runat="server" Text="Từ"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="dlMonth1GiaiNgan" runat="server">
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
            <asp:DropDownList ID="dlYear1GiaiNgan" runat="server">
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
            <asp:Label ID="Label7" runat="server" Text="Đến"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="dlMonth2GiaiNgan" runat="server">
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
            <asp:DropDownList ID="dlYear2GiaiNgan" runat="server">
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
            <asp:Button ID="btnViewGiaiNgan" Width="80px" runat="server" Text="Vẽ lại" OnClick="btnViewGiaiNgan_Click" />
            <input style="width:90px" id="Button4" type="button" value="Phóng to" onclick="openw('/UserControl/DuLieuViMo/Popup.aspx?type=fdigiaingan');" />
        </td>
        <td>
        </td>
    </tr>
</table>
<br />
<asp:Literal ID="ltrChartGiaiNgan" runat="server"></asp:Literal><br />
    <br />
    <div style="width:100%;height:1px; background-color:#D6D6D6;"></div><br />
    <a name="phantramsovoi"></a>
    <table>
        <tr>
            <td colspan="7" valign="top">
                <strong>TĂNG TRƯỞNG FDI(SO VỚI CÙNG KỲ NĂM TRƯỚC)</strong></td>
            <td colspan="1" style="height:20px;">
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top">
                Chọn chỉ tiêu</td>
            <td colspan="5">
                <asp:CheckBoxList ID="chkChitieuMoMSoVoi" runat="server" Height="90px">
                    <asp:ListItem Selected="True" Value="151000"> Số dự án đăng ký mới (lũy kế)</asp:ListItem>
                    <asp:ListItem Selected="True" Value="152000"> Số vốn đăng ký mới</asp:ListItem>
                    <asp:ListItem Selected="True" Value="153000"> Vốn FDI giải ngân</asp:ListItem>
                </asp:CheckBoxList></td>
            <td colspan="1" valign="top">
                <asp:CheckBox ID="chkIsLabelMoMSoVoi" runat="server" Text=" Hiển thị label" /></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label8" runat="server" Text="Từ"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="dlMonth1MoMSoVoi" runat="server">
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
                <asp:DropDownList ID="dlYear1MoMSoVoi" runat="server">
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
                <asp:Label ID="Label9" runat="server" Text="Đến"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="dlMonth2MoMSoVoi" runat="server">
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
                <asp:DropDownList ID="dlYear2MoMSoVoi" runat="server">
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
                <asp:Button ID="btnViewMoMSoVoi" Width="80px" runat="server" Text="Vẽ lại" 
                    OnClick="btnViewMoMSoVoi_Click" />
                <input style="width:90px" id="Button1" type="button" value="Phóng to" onclick="openw('/UserControl/DuLieuViMo/Popup.aspx?type=fdimomsovoi');" />
            </td>
            <td>
            </td>
        </tr>
    </table>
    <br />
    <div style="float:right;padding-right:120px">Đơn vị: (%)</div>
    <asp:Literal ID="ltrChartMoMSoVoi" runat="server"></asp:Literal>
    <asp:HiddenField ID="hdfChartDuAn" runat="server" Value="" />
    <asp:HiddenField ID="hdfChartSoVon" runat="server" Value="" />
    <asp:HiddenField ID="hdfChartGiaiNgan" runat="server" Value="" />
    <asp:HiddenField ID="hdfChartMoMSoVoi" runat="server" Value="" />
</div>
