<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="CafeF.Redis.Page.UserControl.KhopLenh.test" %>
<%@ Register Src="~/UserControl/DatePicker/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Chi tiết khớp lệnh theo thời gian</h1>
        <div>
            Mã : <asp:TextBox runat="server" ID="txtSymbol" Width="50" /> Ngày : <uc1:DatePicker ID="dpkTradeDate1" runat="server" /> <asp:ImageButton runat="server" ID="btnXem" OnClick="btnXem_Click" ImageUrl="http://cafef3.vcmedia.vn/images/images/xem.gif" AlternateText="Xem" />
        </div>
        <br />
        <div id="result">
            <asp:Repeater runat="server" ID="repResult">
                <HeaderTemplate>
                    <table border="0" cellpadding="0" cellspacing="1">
                        <tr>
                            <th>Thời gian</th>
                            <th>Giá</th>
                            <th>Khối lượng</th>
                            <th>Tích lũy</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                        <tr>
                            <td><%# DisplayTime(Eval("TradeDate")) %></td>
                            <td><%# DisplayPrice(Eval("Price")) %></td>
                            <td><%# DisplayVolume(Eval("Volume")) %></td>
                            <td><%# DisplayVolume(Eval("TotalVolume")) %></td>
                        </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
    </form>
</body>
</html>
