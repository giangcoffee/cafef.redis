<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CafeFTA.aspx.cs" Inherits="CafeF.Redis.Page.CafeFTA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="http://cafef3.vcmedia.vn/v2/styles/v2/styles.css" />
    <link rel="stylesheet" type="text/css" href="http://cafef3.vcmedia.vn/v2/style/solieu.v2.css" />
    <link rel="stylesheet" type="text/css" href="http://cafef3.vcmedia.vn/v2/style/stockbar.v2.css" />
</head>
<body topmargin=0>
    <form id="form1" runat="server">
   <div class="lichsu" style="margin-top:0;">    
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <th class="col1">Ngày</th>
          <th class="col1" style="text-align:left;">Tín hiệu</th>
          <th class="col2">Thay đổi giá</th>
          <th class="col3">KL khớp lệnh</th>
          <th class="col4">Tổng GTGD</th>
        </tr>
        <asp:Repeater ID="rptLichSuGD" runat="server" OnItemDataBound="rptLichSuGD_ItemDataBound">
            <ItemTemplate>
                 <tr class="even">
                     
                    <td class="col1" style="width:10%;"><%#String.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem, "TransDate"))%></td>
                    <td class="col1" style="width:50%;"><%#DataBinder.Eval(Container.DataItem, "LIST")%></td>
                    <td class="col2" style="width:20%;"><div class="l"><asp:Literal runat="server" ID="ltrPrice"></asp:Literal></div><asp:Literal runat="server" ID="ltrChange"></asp:Literal></td>
                    <td class="col3" style="width:10%;text-align:right;"><asp:Literal runat="server" ID="ltrVolume"></asp:Literal></td>
                    <td class="col4" style="width:10%;"><asp:Literal runat="server" ID="ltrTotal"></asp:Literal></td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr class="odd">
                  <td class="col1" style="width:10%;"><%#String.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem, "TransDate"))%></td>
                    <td class="col1" style="width:50%;"><%#DataBinder.Eval(Container.DataItem, "LIST")%></td>
                    <td class="col2" style="width:20%;"><div class="l"><asp:Literal runat="server" ID="ltrPrice"></asp:Literal></div><asp:Literal runat="server" ID="ltrChange"></asp:Literal></td>
                    <td class="col3" style="width:10%; text-align:right;"><asp:Literal runat="server" ID="ltrVolume"></asp:Literal></td>
                    <td class="col4" style="width:10%;"><asp:Literal runat="server" ID="ltrTotal"></asp:Literal></td>
                </tr>
            </AlternatingItemTemplate>
        </asp:Repeater>    
	</table>
	
</div>
<div class="paging">
 <a href="" style="padding-left:5px; cursor:pointer;" id="apre" runat="server">Trang trước</a>
 <a href="" style="padding-left:5px; cursor:pointer;" id="anext" runat="server">Trang sau</a>
 </div>
    </form>
</body>
</html>
