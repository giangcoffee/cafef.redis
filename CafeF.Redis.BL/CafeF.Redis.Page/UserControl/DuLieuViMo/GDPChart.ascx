<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GDPChart.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.DuLieuViMo.GDPChart" %>
<asp:Literal ID="ltrScript" runat="server"></asp:Literal>
<%--<div style="dlt-ten">Biểu đồ GDP</div>--%>
<div>
        <a name="theoquy"></a>
        <table>
            <tr>
                <td colspan="7" valign="top">
                    <strong>GIÁ TRỊ (THEO GIÁ TRỊ SO SÁNH NĂM 1994)</strong></td>
                <td colspan="1" valign="top" style="height:20px;">
                </td>
            </tr>
            <tr id="td2Quy">
                <td colspan="2" valign="top">
                    Chọn chỉ tiêu</td>
                <td colspan="5">
                    <asp:CheckBoxList ID="chkChitieu" runat="server" Height="90px">
                        <asp:ListItem Selected="True" Value="100000"> GDP</asp:ListItem>
                        <asp:ListItem Selected="True" Value="101000"> N&#244;ng, l&#226;m nghiệp v&#224; thủy sản</asp:ListItem>
                        <asp:ListItem Selected="True" Value="102000"> C&#244;ng nghiệp, x&#226;y dựng</asp:ListItem>
                        <asp:ListItem Selected="True" Value="103000"> Dịch vụ</asp:ListItem>
                    </asp:CheckBoxList></td>
                <td colspan="1" valign="top">
                    <asp:CheckBox ID="chkIsLabelQuy" runat="server" Text=" Hiển thị label" /></td>
            </tr>
            <tr id="td2Nam" style="display:none">
                <td colspan="2" valign="top">
                    Chọn chỉ tiêu</td>
                <td colspan="5">
                    <asp:CheckBoxList ID="chkChitieu1" runat="server" Height="90px">
                        <asp:ListItem Selected="True" Value="100000"> GDP</asp:ListItem>
                        <asp:ListItem Selected="True" Value="101000"> N&#244;ng, l&#226;m nghiệp v&#224; thủy sản</asp:ListItem>
                        <asp:ListItem Selected="True" Value="102000"> C&#244;ng nghiệp, x&#226;y dựng</asp:ListItem>
                        <asp:ListItem Selected="True" Value="103000"> Dịch vụ</asp:ListItem>
                    </asp:CheckBoxList></td>
                <td colspan="1" valign="top">
                    <asp:CheckBox ID="chkIsLabelNam" runat="server" Text=" Hiển thị label" /></td>
            </tr>
            <tr style="height:30px;">
                <td colspan="7">
                    <input id="radQuyGT" type="radio" name="DLVM_TypeGT" onclick="ChangeTypeGT(1);" /> Quý 
                    <input id="radNamGT" type="radio" name="DLVM_TypeGT" onclick="ChangeTypeGT(2);" /> Năm
                </td>
            </tr>
            <tr id="td3Quy">
                <td>
                    <asp:Label ID="lblFrom" runat="server" Text="Từ"></asp:Label>
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
                    <asp:DropDownList ID="dlQuarter2" runat="server">
                        <asp:ListItem Value="1">Q&#250;y 01</asp:ListItem>
                        <asp:ListItem Value="2">Q&#250;y 02</asp:ListItem>
                        <asp:ListItem Value="3">Q&#250;y 03</asp:ListItem>
                        <asp:ListItem Value="4">Q&#250;y 04</asp:ListItem>
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
                    <asp:Button ID="btnView" Width="80px" runat="server" Text="Vẽ lại" OnClick="btnView_Click" />
                    <input style="width:90px" id="btnZoom" type="button" value="Phóng to" onclick="openw('/UserControl/DuLieuViMo/Popup.aspx?type=gdp1');" />
                </td>
                <td>
                </td>
            </tr>
            <tr id="td3Nam" style="display:none">
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Từ"></asp:Label>&nbsp;
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
                    &nbsp;</td>
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
                    <asp:Button ID="btnView1" Width="80px" runat="server" Text="Vẽ lại" 
                        OnClick="btnView1_Click" />
                    <input style="width:90px" id="Button2" type="button" value="Phóng to" onclick="openw('/UserControl/DuLieuViMo/Popup.aspx?type=gdp2');" />
                </td>
                <td>
                </td>
            </tr>
        </table>
        <br />
        <div style="float:right;padding-right:120px">Đơn vị: nghìn tỷ đồng</div>
        <asp:Literal ID="ltrChart" runat="server"></asp:Literal>
        <asp:Literal ID="ltrChart1" runat="server"></asp:Literal>
        <br />
        <br />
        <div style="width:100%;height:1px; background-color:#D6D6D6;"></div><br />
        
        <a name="theoquyss"></a>
        <table>
            <tr>
                <td colspan="7" valign="top">
                    <strong>GIÁ TRỊ THỰC</strong></td>
                <td colspan="1" valign="top" style="height:20px;">
                </td>
            </tr>
            <tr id="td2QuySS">
                <td colspan="2" valign="top">
                    Chọn chỉ tiêu</td>
                <td colspan="5">
                    <asp:CheckBoxList ID="chkChitieuSS" runat="server" Height="90px">
                        <asp:ListItem Selected="True" Value="G100000"> GDP</asp:ListItem>
                        <asp:ListItem Selected="True" Value="G101000"> N&#244;ng, l&#226;m nghiệp v&#224; thủy sản</asp:ListItem>
                        <asp:ListItem Selected="True" Value="G102000"> C&#244;ng nghiệp, x&#226;y dựng</asp:ListItem>
                        <asp:ListItem Selected="True" Value="G103000"> Dịch vụ</asp:ListItem>
                    </asp:CheckBoxList></td>
                <td colspan="1" valign="top">
                    <asp:CheckBox ID="chkIsLabelQuySS" runat="server" Text=" Hiển thị label" /></td>
            </tr>
            <tr id="td2NamSS" style="display:none">
                <td colspan="2" valign="top">
                    Chọn chỉ tiêu</td>
                <td colspan="5">
                    <asp:CheckBoxList ID="chkChitieu1SS" runat="server" Height="90px">
                        <asp:ListItem Selected="True" Value="G100000"> GDP</asp:ListItem>
                        <asp:ListItem Selected="True" Value="G101000"> N&#244;ng, l&#226;m nghiệp v&#224; thủy sản</asp:ListItem>
                        <asp:ListItem Selected="True" Value="G102000"> C&#244;ng nghiệp, x&#226;y dựng</asp:ListItem>
                        <asp:ListItem Selected="True" Value="G103000"> Dịch vụ</asp:ListItem>
                    </asp:CheckBoxList></td>
                <td colspan="1" valign="top">
                    <asp:CheckBox ID="chkIsLabelNamSS" runat="server" Text=" Hiển thị label" /></td>
            </tr>
            <tr style="height:30px;">
                <td colspan="7">
                    <input id="radQuyGTSS" type="radio" name="DLVM_TypeGTSS" onclick="ChangeTypeGTSS(1);" /> Quý 
                    <input id="radNamGTSS" type="radio" name="DLVM_TypeGTSS" onclick="ChangeTypeGTSS(2);" /> Năm
                </td>
            </tr>
            <tr id="td3QuySS">
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Từ"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dlQuarter1SS" runat="server">
                        <asp:ListItem Value="1">Q&#250;y 01</asp:ListItem>
                        <asp:ListItem Value="2">Q&#250;y 02</asp:ListItem>
                        <asp:ListItem Value="3">Q&#250;y 03</asp:ListItem>
                        <asp:ListItem Value="4">Q&#250;y 04</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="dlYear1SS" runat="server">
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
                    <asp:Label ID="Label10" runat="server" Text="Đến"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dlQuarter2SS" runat="server">
                        <asp:ListItem Value="1">Q&#250;y 01</asp:ListItem>
                        <asp:ListItem Value="2">Q&#250;y 02</asp:ListItem>
                        <asp:ListItem Value="3">Q&#250;y 03</asp:ListItem>
                        <asp:ListItem Value="4">Q&#250;y 04</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="dlYear2SS" runat="server">
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
                    <asp:Button ID="btnViewSS" Width="80px" runat="server" Text="Vẽ lại" OnClick="btnViewSS_Click" />
                    <input style="width:90px" id="Button6" type="button" value="Phóng to" onclick="openw('/UserControl/DuLieuViMo/Popup.aspx?type=gdp1ss');" />
                </td>
                <td>
                </td>
            </tr>
            <tr id="td3NamSS" style="display:none">
                <td>
                    <asp:Label ID="Label11" runat="server" Text="Từ"></asp:Label>&nbsp;
                </td>                
                <td>
                    <asp:DropDownList ID="dlYear1ASS" runat="server">
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
                    <asp:Label ID="Label12" runat="server" Text="Đến"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:DropDownList ID="dlYear2ASS" runat="server">
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
                    <asp:Button ID="btnView1SS" Width="80px" runat="server" Text="Vẽ lại" 
                        OnClick="btnView1SS_Click" />
                    <input style="width:90px" id="Button8" type="button" value="Phóng to" onclick="openw('/UserControl/DuLieuViMo/Popup.aspx?type=gdp2');" />
                </td>
                <td>
                </td>
            </tr>
        </table>
        <br />
        <div style="float:right;padding-right:120px">Đơn vị: nghìn tỷ đồng</div>
        <asp:Literal ID="ltrChartSS" runat="server"></asp:Literal>
        <asp:Literal ID="ltrChart1SS" runat="server"></asp:Literal>
        <br />
        <br />
        <div style="width:100%;height:1px; background-color:#D6D6D6;"></div><br />
        
        <a name="gdpqoqsovoi"></a>
        <table>
            <tr>
                <td colspan="7" valign="top">
                    <strong>TĂNG TRƯỞNG GDP (THEO GIÁ TRỊ SO SÁNH NĂM 1994)</strong></td>
                <td colspan="1" valign="top" style="height:20px;">
                </td>
            </tr>
            <tr id="td2QuyTT">
                <td colspan="2" valign="top">
                    Chọn chỉ tiêu</td>
                <td colspan="5">
                    <asp:CheckBoxList ID="chkChitieuQoQSoVoi" runat="server" Height="90px">
                        <asp:ListItem Selected="True" Value="100000"> GDP</asp:ListItem>
                        <asp:ListItem Selected="True" Value="101000"> N&#244;ng, l&#226;m nghiệp v&#224; thủy sản</asp:ListItem>
                        <asp:ListItem Selected="True" Value="102000"> C&#244;ng nghiệp, x&#226;y dựng</asp:ListItem>
                        <asp:ListItem Selected="True" Value="103000"> Dịch vụ</asp:ListItem>
                    </asp:CheckBoxList></td>
                <td colspan="1" valign="top">
                    <asp:CheckBox ID="chkIsLabelQuyQoQSoVoi" runat="server" Text=" Hiển thị label" /></td>
            </tr>
            <tr id="td2NamTT" style="display:none">
                <td colspan="2" valign="top">
                    Chọn chỉ tiêu</td>
                <td colspan="5">
                    <asp:CheckBoxList ID="chkChitieuYoY" runat="server" Height="90px">
                        <asp:ListItem Selected="True" Value="100000"> GDP</asp:ListItem>
                        <asp:ListItem Selected="True" Value="101000"> N&#244;ng, l&#226;m nghiệp v&#224; thủy sản</asp:ListItem>
                        <asp:ListItem Selected="True" Value="102000"> C&#244;ng nghiệp, x&#226;y dựng</asp:ListItem>
                        <asp:ListItem Selected="True" Value="103000"> Dịch vụ</asp:ListItem>
                    </asp:CheckBoxList></td>
                <td colspan="1" valign="top">
                    <asp:CheckBox ID="chkIsLabelNamYoY" runat="server" Text=" Hiển thị label" /></td>
            </tr>
            <tr style="height:30px;">
                <td colspan="7">
                    <input id="radQuyTT" type="radio" name="DLVM_TypeTT" onclick="ChangeTypeGT(1);" /> Quý 
                    <input id="radNamTT" type="radio" name="DLVM_TypeTT" onclick="ChangeTypeGT(2);" /> Năm
                </td>
            </tr>
            <tr id="td3QuyTT">
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Từ"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dlQuarter1QoQSoVoi" runat="server">
                        <asp:ListItem Value="1">Q&#250;y 01</asp:ListItem>
                        <asp:ListItem Value="2">Q&#250;y 02</asp:ListItem>
                        <asp:ListItem Value="3">Q&#250;y 03</asp:ListItem>
                        <asp:ListItem Value="4">Q&#250;y 04</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="dlYear1QoQSoVoi" runat="server">
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
                    <asp:Label ID="Label8" runat="server" Text="Đến"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dlQuarter2QoQSoVoi" runat="server">
                        <asp:ListItem Value="1">Q&#250;y 01</asp:ListItem>
                        <asp:ListItem Value="2">Q&#250;y 02</asp:ListItem>
                        <asp:ListItem Value="3">Q&#250;y 03</asp:ListItem>
                        <asp:ListItem Value="4">Q&#250;y 04</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="dlYear2QoQSoVoi" runat="server">
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
                    <asp:Button ID="btnViewQoQSoVoi" Width="80px" runat="server" Text="Vẽ lại" 
                        OnClick="btnViewQoQSoVoi_Click" />
                    <input style="width:90px" id="Button5" type="button" value="Phóng to" onclick="openw('/UserControl/DuLieuViMo/Popup.aspx?type=gdpqoq');" />
                </td>
                <td>
                </td>
            </tr>
            <tr id="td3NamTT" style="display:none">
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Từ"></asp:Label>&nbsp;
                </td>               
                <td>
                    <asp:DropDownList ID="dlYear1YoY" runat="server">
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
                    <asp:Label ID="Label6" runat="server" Text="Đến"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:DropDownList ID="dlYear2YoY" runat="server">
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
                    <asp:Button ID="btnViewYoY" Width="80px" runat="server" Text="Vẽ lại" 
                        OnClick="btnViewYoY_Click" />
                    <input id="Button3" style="width:90px" type="button" value="Phóng to" onclick="openw('/UserControl/DuLieuViMo/Popup.aspx?type=gdpyoy');" />
                </td>
                <td>
                </td>
            </tr>
        </table>
        <br />
        <div style="float:right;padding-right:120px">Đơn vị: (%)</div>
        <asp:Literal ID="ltrChartQoQSoVoi" runat="server"></asp:Literal>
        <asp:Literal ID="ltrChartYoY" runat="server"></asp:Literal>
        
        <br />
        <br />
        <div style="width:100%;height:1px; background-color:#D6D6D6;"></div><br />
        
        <a name="gdpqoqsovoiss"></a>
        <table>
            <tr>
                <td colspan="7" valign="top">
                    <strong>TĂNG TRƯỞNG GDP THỰC</strong></td>
                <td colspan="1" valign="top" style="height:20px;">
                </td>
            </tr>
            <tr id="td2QuyTTSS">
                <td colspan="2" valign="top">
                    Chọn chỉ tiêu</td>
                <td colspan="5">
                    <asp:CheckBoxList ID="chkChitieuQoQSoVoiSS" runat="server" Height="90px">
                        <asp:ListItem Selected="True" Value="G100000"> GDP</asp:ListItem>
                        <asp:ListItem Selected="True" Value="G101000"> N&#244;ng, l&#226;m nghiệp v&#224; thủy sản</asp:ListItem>
                        <asp:ListItem Selected="True" Value="G102000"> C&#244;ng nghiệp, x&#226;y dựng</asp:ListItem>
                        <asp:ListItem Selected="True" Value="G103000"> Dịch vụ</asp:ListItem>
                    </asp:CheckBoxList></td>
                <td colspan="1" valign="top">
                    <asp:CheckBox ID="chkIsLabelQuyQoQSoVoiSS" runat="server" Text=" Hiển thị label" /></td>
            </tr>
            <tr id="td2NamTTSS" style="display:none">
                <td colspan="2" valign="top">
                    Chọn chỉ tiêu</td>
                <td colspan="5">
                    <asp:CheckBoxList ID="chkChitieuYoYSS" runat="server" Height="90px">
                        <asp:ListItem Selected="True" Value="G100000"> GDP</asp:ListItem>
                        <asp:ListItem Selected="True" Value="G101000"> N&#244;ng, l&#226;m nghiệp v&#224; thủy sản</asp:ListItem>
                        <asp:ListItem Selected="True" Value="G102000"> C&#244;ng nghiệp, x&#226;y dựng</asp:ListItem>
                        <asp:ListItem Selected="True" Value="G103000"> Dịch vụ</asp:ListItem>
                    </asp:CheckBoxList></td>
                <td colspan="1" valign="top">
                    <asp:CheckBox ID="chkIsLabelNamYoYSS" runat="server" Text=" Hiển thị label" /></td>
            </tr>
            <tr style="height:30px;">
                <td colspan="7">
                    <input id="radQuyTTSS" type="radio" name="DLVM_TypeTTSS" onclick="ChangeTypeGTSS(1);" /> Quý 
                    <input id="radNamTTSS" type="radio" name="DLVM_TypeTTSS" onclick="ChangeTypeGTSS(2);" /> Năm
                </td>
            </tr>
            <tr id="td3QuyTTSS">
                <td>
                    <asp:Label ID="Label13" runat="server" Text="Từ"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dlQuarter1QoQSoVoiSS" runat="server">
                        <asp:ListItem Value="1">Q&#250;y 01</asp:ListItem>
                        <asp:ListItem Value="2">Q&#250;y 02</asp:ListItem>
                        <asp:ListItem Value="3">Q&#250;y 03</asp:ListItem>
                        <asp:ListItem Value="4">Q&#250;y 04</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="dlYear1QoQSoVoiSS" runat="server">
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
                    <asp:Label ID="Label14" runat="server" Text="Đến"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dlQuarter2QoQSoVoiSS" runat="server">
                        <asp:ListItem Value="1">Q&#250;y 01</asp:ListItem>
                        <asp:ListItem Value="2">Q&#250;y 02</asp:ListItem>
                        <asp:ListItem Value="3">Q&#250;y 03</asp:ListItem>
                        <asp:ListItem Value="4">Q&#250;y 04</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="dlYear2QoQSoVoiSS" runat="server">
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
                    <asp:Button ID="btnViewQoQSoVoiSS" Width="80px" runat="server" Text="Vẽ lại" 
                        OnClick="btnViewQoQSoVoiSS_Click" />
                    <input style="width:90px" id="Button7" type="button" value="Phóng to" onclick="openw('/UserControl/DuLieuViMo/Popup.aspx?type=gdpqoqss');" />
                </td>
                <td>
                </td>
            </tr>
            <tr id="td3NamTTSS" style="display:none">
                <td>
                    <asp:Label ID="Label15" runat="server" Text="Từ"></asp:Label>&nbsp;
                </td>               
                <td>
                    <asp:DropDownList ID="dlYear1YoYSS" runat="server">
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
                    <asp:Label ID="Label16" runat="server" Text="Đến"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:DropDownList ID="dlYear2YoYSS" runat="server">
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
                    <asp:Button ID="btnViewYoYSS" Width="80px" runat="server" Text="Vẽ lại" 
                        OnClick="btnViewYoYSS_Click" />
                    <input id="Button10" style="width:90px" type="button" value="Phóng to" onclick="openw('/UserControl/DuLieuViMo/Popup.aspx?type=gdpyoyss');" />
                </td>
                <td>
                </td>
            </tr>
        </table>
        <br />
        <div style="float:right;padding-right:120px">Đơn vị: (%)</div>
        <asp:Literal ID="ltrChartQoQSoVoiSS" runat="server"></asp:Literal>
        <asp:Literal ID="ltrChartYoYSS" runat="server"></asp:Literal>
        
        <asp:HiddenField ID="hdfChart1" runat="server" Value="" />
        <asp:HiddenField ID="hdfChartQoQ" runat="server" Value="" />
        <asp:HiddenField ID="hdfChart2" runat="server" Value="" />
        <asp:HiddenField ID="hdfChartYoY" runat="server" Value="" />
        <asp:HiddenField ID="hdfChartQoQSoVoi" runat="server" Value="" />
        
        <asp:HiddenField ID="hdfChart1SS" runat="server" Value="" />
        <asp:HiddenField ID="hdfChart2SS" runat="server" Value="" />
        <asp:HiddenField ID="hdfChartYoYSS" runat="server" Value="" />
        <asp:HiddenField ID="hdfChartQoQSoVoiSS" runat="server" Value="" />
        
        <asp:HiddenField ID="hdfTypeGT" runat="server" Value="" />
        <asp:HiddenField ID="hdfTypeGTSS" runat="server" Value="" />
</div>
        <%--<a name="gdpqoq"></a>--%>
        <table style="display:none">
            <tr>
                <td colspan="7" valign="top">
                    <strong>Tăng trưởng GDP theo quý (so với quý liền trước)</strong></td>
                <td colspan="1" valign="top">
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="top">
                    Chọn chỉ tiêu</td>
                <td colspan="5">
                    <asp:CheckBoxList ID="chkChitieuQoQ" runat="server" Height="90px">
                        <asp:ListItem Selected="True" Value="100000"> GDP</asp:ListItem>
                        <asp:ListItem Selected="True" Value="101000"> N&#244;ng, l&#226;m nghiệp v&#224; thủy sản</asp:ListItem>
                        <asp:ListItem Selected="True" Value="102000"> C&#244;ng nghiệp, x&#226;y dựng</asp:ListItem>
                        <asp:ListItem Selected="True" Value="103000"> Dịch vụ</asp:ListItem>
                    </asp:CheckBoxList></td>
                <td colspan="1" valign="top">
                    <asp:CheckBox ID="chkIsLabelQuyQoQ" runat="server" Text=" Hiển thị label" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Từ"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dlQuarter1QoQ" runat="server">
                        <asp:ListItem Value="1">Q&#250;y 01</asp:ListItem>
                        <asp:ListItem Value="2">Q&#250;y 02</asp:ListItem>
                        <asp:ListItem Value="3">Q&#250;y 03</asp:ListItem>
                        <asp:ListItem Value="4">Q&#250;y 04</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="dlYear1QoQ" runat="server">
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
                    <asp:Label ID="Label4" runat="server" Text="Đến"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dlQuarter2QoQ" runat="server">
                        <asp:ListItem Value="1">Q&#250;y 01</asp:ListItem>
                        <asp:ListItem Value="2">Q&#250;y 02</asp:ListItem>
                        <asp:ListItem Value="3">Q&#250;y 03</asp:ListItem>
                        <asp:ListItem Value="4">Q&#250;y 04</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="dlYear2QoQ" runat="server">
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
                    <asp:Button ID="btnViewQoQ" Width="80px" runat="server" Text="Vẽ lại" 
                        OnClick="btnViewQoQ_Click" />
                    <input style="width:90px" id="Button1" type="button" value="Phóng to" onclick="openw('/UserControl/DuLieuViMo/Popup.aspx?type=gdpqoq');" />
                </td>
                <td>
                </td>
            </tr>
        </table>
        <%--<br />--%>
        <%--<div style="float:right;padding-right:120px">Đơn vị: (%)</div>--%>
        <asp:Literal ID="ltrChartQoQ" runat="server" Visible="false"></asp:Literal>
        <%--<br />
        <br />--%><br />
        
        
<script type="text/javascript">
function ChangeTypeGT(type)
{    
    if (type == 1)
    {
        document.getElementById('td2Nam').style.display = 'none';
        document.getElementById('td3Nam').style.display = 'none';
        document.getElementById('td2Quy').style.display = '';
        document.getElementById('td3Quy').style.display = '';
        document.getElementById('imgQuyGT').style.display = '';
        document.getElementById('imgNamGT').style.display = 'none';
    }
    else if(type == 2)
    {
        document.getElementById('td2Nam').style.display = '';
        document.getElementById('td3Nam').style.display = '';
        document.getElementById('td2Quy').style.display = 'none';
        document.getElementById('td3Quy').style.display = 'none';
        document.getElementById('imgQuyGT').style.display = 'none';
        document.getElementById('imgNamGT').style.display = '';
    }    
    ChangeTypeTT(type);
    if(type == 1) document.getElementById('radQuyTT').checked = 'checked';
    else if(type == 2) document.getElementById('radNamTT').checked = 'checked';
    if(type == 1) document.getElementById('radQuyGT').checked = 'checked';
    else if(type == 2) document.getElementById('radNamGT').checked = 'checked';
}
function ChangeTypeTT(type)
{    
    if (type == 1)
    {
        document.getElementById('td2NamTT').style.display = 'none';
        document.getElementById('td3NamTT').style.display = 'none';
        document.getElementById('td2QuyTT').style.display = '';
        document.getElementById('td3QuyTT').style.display = '';
        document.getElementById('imgQuyTT').style.display = '';
        document.getElementById('imgNamTT').style.display = 'none';
    }
    else if(type == 2)
    {
        document.getElementById('td2NamTT').style.display = '';
        document.getElementById('td3NamTT').style.display = '';
        document.getElementById('td2QuyTT').style.display = 'none';
        document.getElementById('td3QuyTT').style.display = 'none';
        document.getElementById('imgQuyTT').style.display = 'none';
        document.getElementById('imgNamTT').style.display = '';
    }    
}
var ty = document.getElementById('<%=hdfTypeGT.ClientID %>').value;
ChangeTypeGT(ty);

function ChangeTypeGTSS(type)
{    
    if (type == 1)
    {
        document.getElementById('td2NamSS').style.display = 'none';
        document.getElementById('td3NamSS').style.display = 'none';
        document.getElementById('td2QuySS').style.display = '';
        document.getElementById('td3QuySS').style.display = '';
        document.getElementById('imgQuyGTSS').style.display = '';
        document.getElementById('imgNamGTSS').style.display = 'none';
    }
    else if(type == 2)
    {
        document.getElementById('td2NamSS').style.display = '';
        document.getElementById('td3NamSS').style.display = '';
        document.getElementById('td2QuySS').style.display = 'none';
        document.getElementById('td3QuySS').style.display = 'none';
        document.getElementById('imgQuyGTSS').style.display = 'none';
        document.getElementById('imgNamGTSS').style.display = '';
    }    
    ChangeTypeTTSS(type);
    if(type == 1) document.getElementById('radQuyTTSS').checked = 'checked';
    else if(type == 2) document.getElementById('radNamTTSS').checked = 'checked';
    if(type == 1) document.getElementById('radQuyGTSS').checked = 'checked';
    else if(type == 2) document.getElementById('radNamGTSS').checked = 'checked';
}
function ChangeTypeTTSS(type)
{    
    if (type == 1)
    {
        document.getElementById('td2NamTTSS').style.display = 'none';
        document.getElementById('td3NamTTSS').style.display = 'none';
        document.getElementById('td2QuyTTSS').style.display = '';
        document.getElementById('td3QuyTTSS').style.display = '';
        document.getElementById('imgQuyTTSS').style.display = '';
        document.getElementById('imgNamTTSS').style.display = 'none';
    }
    else if(type == 2)
    {
        document.getElementById('td2NamTTSS').style.display = '';
        document.getElementById('td3NamTTSS').style.display = '';
        document.getElementById('td2QuyTTSS').style.display = 'none';
        document.getElementById('td3QuyTTSS').style.display = 'none';
        document.getElementById('imgQuyTTSS').style.display = 'none';
        document.getElementById('imgNamTTSS').style.display = '';
    }    
}
var tySS = document.getElementById('<%=hdfTypeGTSS.ClientID %>').value;
ChangeTypeGTSS(tySS);

</script>