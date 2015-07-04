<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CPIAfterChart.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.DuLieuViMo.CPIAfterChart" %>
<asp:Literal ID="ltrScript" runat="server"></asp:Literal>
<div>
        <a name="giatrithuc"></a>
        <table>
            <tr>
                <td colspan="7" valign="top">
                    <strong>CHỈ SỐ GIÁ TIÊU DÙNG (TỪ SAU THÁNG 11/2009)</strong></td>
                <td colspan="1" valign="top" style="height:20px;">
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="top">
                    Chọn chỉ tiêu</td>
                <td colspan="5" valign="top">
                    <table>
                        <tr>
                            <td valign="top">
                                <asp:CheckBoxList ID="chkChitieu" runat="server" Height="90px">
                                    <asp:ListItem Selected="True" Value="130000"> CPI</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="130001"> Core CPI</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="139800"> Chỉ số gi&#225; v&#224;ng</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="139900"> Chỉ số gi&#225; đ&#244; la mỹ</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <td>
                                <asp:CheckBoxList ID="chkChitieuchitiet1" runat="server" Height="250px">
                                    <asp:ListItem Value="130100"> Lương thực, thực phẩm</asp:ListItem>
                                    <asp:ListItem Value="130400"> Đồ uống v&#224; thuốc l&#225;</asp:ListItem>
                                    <asp:ListItem Value="130500"> May mặc, gi&#224;y d&#233;p v&#224; mũ n&#243;n</asp:ListItem>
                                    <asp:ListItem Value="130600"> Nh&#224; ở v&#224; vật liệu x&#226;y dựng</asp:ListItem>
                                    <asp:ListItem Value="130700"> Thiết bị v&#224; đồ d&#249;ng gia đ&#236;nh</asp:ListItem>
                                    <asp:ListItem Value="130800"> Dược phẩm, y tế</asp:ListItem>
                                    <asp:ListItem Value="130900"> Phương tiện đi lại, bưu điện</asp:ListItem>
                                    <asp:ListItem Value="131000"> Bưu ch&#237;nh viễn th&#244;ng</asp:ListItem>
                                    <asp:ListItem Value="131100"> Gi&#225;o dục</asp:ListItem>
                                    <asp:ListItem Value="131200"> Văn ho&#225;, thể thao, giải tr&#237;</asp:ListItem>
                                    <asp:ListItem Value="131300"> Đồ d&#249;ng v&#224; dịch vụ kh&#225;c</asp:ListItem>
                                </asp:CheckBoxList>&nbsp;</td>
                        </tr>
                    </table>
                    </td>
                <td colspan="1" valign="top">
                    <asp:CheckBox ID="chkIsLabel" runat="server" Text=" Hiển thị label" /></td>
            </tr>
            <tr style="height:30px;display:none;">
                <td colspan="7">
                    <input id="radThang" checked="checked" type="radio" name="DLVM_Type" onclick="ChangeType(1);" /> Tháng 
                    <input id="radQuy" type="radio" name="DLVM_Type" onclick="ChangeType(2);" /> Quý 
                    <input id="radNam" type="radio" name="DLVM_Type" onclick="ChangeType(3);" /> Năm
                </td>
            </tr>
            <tr id="trThangGT">
                <td>
                    <asp:Label ID="lblFrom" runat="server" Text="Từ"></asp:Label>&nbsp;
                </td>
                <td>
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
                    <asp:Button Width="80px" ID="btnView" runat="server" Text="Vẽ lại" OnClick="btnView_Click" />
                    <input style="width:90px" id="btnZoom" onclick="openw('/UserControl/DuLieuViMo/Popup.aspx?type=cpi1');"
                        type="button" value="Phóng to" />
                </td>
                <td>
                </td>
            </tr>
            <tr id="trQuyGT" style="display:none">
        <td>
            <asp:Label ID="Label5" runat="server" Text="Từ"></asp:Label>&nbsp;
        </td>
        <td>
            <asp:DropDownList ID="dlQuarter1" runat="server">
                <asp:ListItem Value="1">Q&#250;y 01</asp:ListItem>
                <asp:ListItem Value="2">Q&#250;y 02</asp:ListItem>
                <asp:ListItem Value="3">Q&#250;y 03</asp:ListItem>
                <asp:ListItem Value="4">Q&#250;y 04</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <asp:DropDownList ID="dlYearQuy1" runat="server"></asp:DropDownList>
        </td>
        <td>
            <asp:Label ID="Label6" runat="server" Text="Đến"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="dlQuarter2" runat="server">
                <asp:ListItem Value="1">Q&#250;y 01</asp:ListItem>
                <asp:ListItem Value="2">Q&#250;y 02</asp:ListItem>
                <asp:ListItem Value="3">Q&#250;y 03</asp:ListItem>
                <asp:ListItem Value="4">Q&#250;y 04</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <asp:DropDownList ID="dlYearQuy2" runat="server"></asp:DropDownList>
        </td>
        <td>
            <asp:Button ID="btnViewQuy" Width="80px" runat="server" Text="Vẽ lại" OnClick="btnViewQuy_Click" />
            <input style="width:90px" id="Button2" type="button" value="Phóng to" onclick="openw('/UserControl/DuLieuViMo/Popup.aspx?type=cpi1quy');" />
        </td>
    </tr>
    <tr id="trNamGT" style="display:none">
                <td align="right">
                    <asp:Label ID="Label7" runat="server" Text="Từ"></asp:Label>&nbsp;
                </td>
                <td>
                    <asp:DropDownList ID="dlYear1YoY" runat="server">                        
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Đến"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:DropDownList ID="dlYear2YoY" runat="server">                        
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnViewYoY" Width="80px" runat="server" Text="Vẽ lại" 
                        OnClick="btnViewYoY_Click" />
                    <input id="Button4" style="width:90px" type="button" value="Phóng to" onclick="openw('/UserControl/DuLieuViMo/Popup.aspx?type=cpi1nam');" />
                </td>
            </tr>
        </table>
        <br />
        <asp:Literal ID="ltrChart" runat="server"></asp:Literal>
        <asp:Literal ID="ltrChartGT2" runat="server"></asp:Literal>
<asp:Literal ID="ltrChartGT3" runat="server"></asp:Literal>
        <br />
        <br />
        <div style="width:100%;height:1px; background-color:#D6D6D6;"></div><br />
        <a name="phantram"></a>
        <table>
            <tr>
                <td colspan="7" valign="top">
                    <strong>TĂNG TRƯỞNG CHỈ SỐ GIÁ TIÊU DÙNG (TỪ SAU THÁNG 11/2009)</strong></td>
                <td colspan="1" valign="top" style="height:20px;">
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="top">
                    Chọn chỉ tiêu</td>
                <td colspan="5">
                    <table>
                        <tr>
                            <td valign="top">
                                <asp:CheckBoxList ID="chkChitieu1" runat="server" Height="90px">
                                    <asp:ListItem Selected="True" Value="130000"> CPI</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="130001"> Core CPI</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="139800"> Chỉ số gi&#225; v&#224;ng</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="139900"> Chỉ số gi&#225; đ&#244; la mỹ</asp:ListItem>
                                </asp:CheckBoxList>
                                </td>
                                <td>
                                    <asp:CheckBoxList ID="chkChitieuchitiet2" runat="server" Height="250px">
                                        <asp:ListItem Value="130100"> Lương thực, thực phẩm</asp:ListItem>
                                        <asp:ListItem Value="130400"> Đồ uống v&#224; thuốc l&#225;</asp:ListItem>
                                        <asp:ListItem Value="130500"> May mặc, gi&#224;y d&#233;p v&#224; mũ n&#243;n</asp:ListItem>
                                        <asp:ListItem Value="130600"> Nh&#224; ở v&#224; vật liệu x&#226;y dựng</asp:ListItem>
                                        <asp:ListItem Value="130700"> Thiết bị v&#224; đồ d&#249;ng gia đ&#236;nh</asp:ListItem>
                                        <asp:ListItem Value="130800"> Dược phẩm, y tế</asp:ListItem>
                                        <asp:ListItem Value="130900"> Phương tiện đi lại, bưu điện</asp:ListItem>
                                        <asp:ListItem Value="131000"> Bưu ch&#237;nh viễn th&#244;ng</asp:ListItem>
                                        <asp:ListItem Value="131100"> Gi&#225;o dục</asp:ListItem>
                                        <asp:ListItem Value="131200"> Văn ho&#225;, thể thao, giải tr&#237;</asp:ListItem>
                                        <asp:ListItem Value="131300"> Đồ d&#249;ng v&#224; dịch vụ kh&#225;c</asp:ListItem>
                                    </asp:CheckBoxList>&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                <td colspan="1" valign="top">
                    <asp:CheckBox ID="CheckBox1" runat="server" Text=" Hiển thị label" /></td>
            </tr>
            <tr style="height:30px;">
                <td colspan="7">
                    <input id="radThangTT" checked="checked" type="radio" name="DLVM_TypeTT" onclick="ChangeTypeTT(1);" /> Tháng 
                    <input id="radQuyTT" type="radio" name="DLVM_TypeTT" onclick="ChangeTypeTT(2);" /> Quý 
                    <input style="display:none" id="radNamTT" type="radio" name="DLVM_TypeTT" onclick="ChangeTypeTT(3);" />
                </td>
            </tr>
            <tr id="trThangTT">
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Từ"></asp:Label>&nbsp;
                </td>
                <td>
                    <asp:DropDownList ID="dlMonth1A" runat="server">
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
                    <asp:DropDownList ID="dlYear1A" runat="server">
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
                    <asp:Label ID="Label2" runat="server" Text="Đến"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dlMonth2A" runat="server">
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
                    <asp:DropDownList ID="dlYear2A" runat="server">
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
                    <asp:Button Width="80px" ID="btnView1" runat="server" Text="Vẽ lại" 
                        OnClick="btnView1_Click" />
                    <input style="width:90px" id="Button1" onclick="openw('/UserControl/DuLieuViMo/Popup.aspx?type=cpi2');"
                        type="button" value="Phóng to" />
                </td>
                <td>
                </td>
            </tr>
            <tr id="trQuyTT" style="display:none">
        <td>
            <asp:Label ID="Label9" runat="server" Text="Từ"></asp:Label>&nbsp;
        </td>
        <td>
            <asp:DropDownList ID="dlQuarter1TT" runat="server">
                <asp:ListItem Value="1">Q&#250;y 01</asp:ListItem>
                <asp:ListItem Value="2">Q&#250;y 02</asp:ListItem>
                <asp:ListItem Value="3">Q&#250;y 03</asp:ListItem>
                <asp:ListItem Value="4">Q&#250;y 04</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <asp:DropDownList ID="dlYearQuy1TT" runat="server"></asp:DropDownList>
        </td>
        <td>
            <asp:Label ID="Label10" runat="server" Text="Đến"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="dlQuarter2TT" runat="server">
                <asp:ListItem Value="1">Q&#250;y 01</asp:ListItem>
                <asp:ListItem Value="2">Q&#250;y 02</asp:ListItem>
                <asp:ListItem Value="3">Q&#250;y 03</asp:ListItem>
                <asp:ListItem Value="4">Q&#250;y 04</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <asp:DropDownList ID="dlYearQuy2TT" runat="server"></asp:DropDownList>
        </td>
        <td>
            <asp:Button ID="btnViewQuyTT" Width="80px" runat="server" Text="Vẽ lại" OnClick="btnViewQuyTT_Click" />
            <input style="width:90px" id="Button6" type="button" value="Phóng to" onclick="openw('/UserControl/DuLieuViMo/Popup.aspx?type=cpi2quy');" />
        </td>
    </tr>
    <tr id="trNamTT" style="display:none">
                <td align="right">
                    <asp:Label ID="Label11" runat="server" Text="Từ"></asp:Label>&nbsp;
                </td>
                <td>
                    <asp:DropDownList ID="dlYear1YoYTT" runat="server">                        
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="Label12" runat="server" Text="Đến"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:DropDownList ID="dlYear2YoYTT" runat="server">                        
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnViewYoYTT" Width="80px" runat="server" Text="Vẽ lại" 
                        OnClick="btnViewYoYTT_Click" />
                    <input id="Button8" style="width:90px" type="button" value="Phóng to" onclick="openw('/UserControl/DuLieuViMo/Popup.aspx?type=cpi2nam');" />
                </td>
            </tr>
        </table>
        <br />
        <div style="float:right;padding-right:120px">Đơn vị: (%)</div>
        <asp:Literal ID="ltrChart1" runat="server"></asp:Literal>
        <asp:Literal ID="ltrChartMoMTT2" runat="server"></asp:Literal>
    <asp:Literal ID="ltrChartMoMTT3" runat="server"></asp:Literal>
        <br />
        <asp:HiddenField ID="hdfChart1" runat="server" Value="" />
        <asp:HiddenField ID="hdfChart2" runat="server" Value="" />
        
        <asp:HiddenField ID="hdfChartGT2" runat="server" Value="" />
            <asp:HiddenField ID="hdfChartGT3" runat="server" Value="" />
            <asp:HiddenField ID="hdfTypeGT" runat="server" Value="" />
            <asp:HiddenField ID="hdfChartMoMTT2" runat="server" Value="" />
            <asp:HiddenField ID="hdfChartMoMTT3" runat="server" Value="" />
            <asp:HiddenField ID="hdfTypeTT" runat="server" Value="" />
</div>

<script type="text/javascript">
function ChangeTypeGT(type)
{    
    if (type == 1)
    {
//        document.getElementById('trQuyGT').style.display = 'none';
//        //document.getElementById('trNamGT').style.display = 'none';
//        document.getElementById('trThangGT').style.display = '';
//        document.getElementById('imgThangGT').style.display = '';
//        document.getElementById('imgQuyGT').style.display = 'none';
        //document.getElementById('imgNamGT').style.display = 'none';
    }
    else if(type == 2)
    {
//        document.getElementById('trQuyGT').style.display = '';
//        document.getElementById('imgQuyGT').style.display = '';
//        //document.getElementById('trNamGT').style.display = 'none';
//        document.getElementById('trThangGT').style.display = 'none';
//        document.getElementById('imgThangGT').style.display = 'none';
//        //document.getElementById('imgNamGT').style.display = 'none';
    }
    else
    {
//        document.getElementById('imgQuyGT').style.display = 'none';
//        document.getElementById('trQuyGT').style.display = 'none';
//        //document.getElementById('trNamGT').style.display = '';
//        document.getElementById('trThangGT').style.display = 'none';
//        document.getElementById('imgThangGT').style.display = 'none';
        //document.getElementById('imgNamGT').style.display = '';
    }
}
function ChangeTypeTT(type)
{    
    if (type == 1)
    {
        document.getElementById('trQuyTT').style.display = 'none';
        //document.getElementById('trNamTT').style.display = 'none';
        document.getElementById('trThangTT').style.display = '';
        document.getElementById('imgThangTT').style.display = '';
        document.getElementById('imgQuyTT').style.display = 'none';
        //document.getElementById('imgNamTT').style.display = 'none';
    }
    else if(type == 2)
    {
        document.getElementById('trQuyTT').style.display = '';
        document.getElementById('imgQuyTT').style.display = '';
        //document.getElementById('trNamTT').style.display = 'none';
        document.getElementById('trThangTT').style.display = 'none';
        document.getElementById('imgThangTT').style.display = 'none';
        //document.getElementById('imgNamTT').style.display = 'none';
    }
    else
    {
        document.getElementById('imgQuyTT').style.display = 'none';
        document.getElementById('trQuyTT').style.display = 'none';
        //document.getElementById('trNamTT').style.display = '';
        document.getElementById('trThangTT').style.display = 'none';
        document.getElementById('imgThangTT').style.display = 'none';
        //document.getElementById('imgNamTT').style.display = '';
    }
}
//var ty = document.getElementById('<%=hdfTypeGT.ClientID %>').value;
//ChangeTypeGT(ty);
//if(ty == 1) document.getElementById('radThangGT').checked = 'checked';
//else if(ty == 2) document.getElementById('radQuyGT').checked = 'checked';
//else if(ty == 3) document.getElementById('radNamGT').checked = 'checked';
var tyTT = document.getElementById('<%=hdfTypeTT.ClientID %>').value;
ChangeTypeTT(tyTT);
if(tyTT == 1) document.getElementById('radThangTT').checked = 'checked';
else if(tyTT == 2) document.getElementById('radQuyTT').checked = 'checked';
else if(tyTT == 3) document.getElementById('radNamTT').checked = 'checked';

</script>
