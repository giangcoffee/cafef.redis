<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CacheThongKe.aspx.cs" Inherits="CafeF.ThongKe.CacheThongKe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    Ngay 1<asp:TextBox ID="txtNgay1" runat="server"></asp:TextBox> &nbsp; Ngay 2 : <asp:TextBox ID="txtNgay2" runat="server"></asp:TextBox>
    <asp:DropDownList ID="ddlCacheName" runat="server">
        <asp:ListItem Value="CafeF_FN_StockDataHistory_GetStockSymbol_" Text="CafeF_FN_StockDataHistory_GetStockSymbol_"></asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="ddlSan" runat="server">
        <asp:ListItem Value="HASTC" Text="HASTC"></asp:ListItem>
        <asp:ListItem Value="HOSE" Text="HOSE"></asp:ListItem>
        <asp:ListItem Value="UPCOM" Text="UPCOM"></asp:ListItem>
    </asp:DropDownList>
    <asp:Label ID="lblCountSan" runat="server" ForeColor="red"></asp:Label>
        <br />
        <asp:GridView ID="grdViewCache" runat="server">            
        </asp:GridView>
        <br />
    <asp:Button ID="btUpdateCacheSan" runat="server" Text="Update Cache StockSymbol" OnClick="btUpdateCacheSan_Click" />
    <asp:Button ID="btUpdateStockValue" runat="server" Text="Update Stock Value" OnClick="btUpdateStockValue_Click" />
    <asp:Button ID="btDeleteCacheSan" runat="server" Text="Delete Cache StockSymbol" OnClick="btDeleteCacheSan_Click" />
    <asp:Button ID= "btDeleteAllCache" runat="server" Text="Delete All Cache" OnClick="btDeleteAllCache_Click"/> 
        <br />
        <asp:Button ID="btViewCache" runat="server" Text="View Cache" OnClick="btViewCache_Click" />
    <br /><br />
    
    Tim cache theo key : <br />
    <asp:TextBox ID="txtCaceName" runat="server" Width="400px"></asp:TextBox> <asp:DropDownList ID="ddlPort" runat="server"><asp:ListItem Value="8888" Text="Portfolio"></asp:ListItem><asp:ListItem Value="9999" Text="Thong ke"></asp:ListItem></asp:DropDownList> <asp:Button ID="btGetCache" runat="server" Text="Get Cache" OnClick="btGetCache_Click" /><asp:Button ID="btDel" runat="server" Text="Xoa cache" OnClick="btDel_Click" /> 
    <asp:Label ID="lblTime" runat="server"></asp:Label><br />
        <br />
        <asp:DropDownList ID="ddlOptionUpdate" runat="server">
            <asp:ListItem Value="1" Text="1"></asp:ListItem>
            <asp:ListItem Value="2" Text="2"></asp:ListItem>
            <asp:ListItem Value="3" Text="3"></asp:ListItem>
        </asp:DropDownList>
       <asp:DropDownList ID="ddlCacheNameUpdate" runat="server">
            <asp:ListItem Value="1" Text="cafefThongKe_All_DataTable_{0}_{1}_{2}"></asp:ListItem>
            <asp:ListItem Value="2" Text="cafefThongKe_Padding_DataTable_{0}_{1}_{2}_{3}_{4}"></asp:ListItem>
            
        </asp:DropDownList>
        PageIndex:
        <asp:TextBox ID="txtPageIndex" runat="server"></asp:TextBox>
        <asp:DropDownList ID="ddlSanUpdate" runat="server">
            <asp:ListItem Value="2" Text="hastc"></asp:ListItem>
            <asp:ListItem Value="1" Text="hose"></asp:ListItem>
            <asp:ListItem Value="3" Text="upcom"></asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="ddlOrderUpdate" runat="server">
            <asp:ListItem Value="desc" Text="desc"></asp:ListItem>
            <asp:ListItem Value="asc" Text="asc"></asp:ListItem>
        </asp:DropDownList>
        Tab<asp:TextBox ID="txtTab" runat="server"></asp:TextBox>
        TimeNow
        <asp:TextBox ID="txtTimeNow" runat="server"></asp:TextBox>
        <asp:Button ID="btUpdateCache" runat="server" Text="Update All Table" OnClick="btUpdateCache_Click" />&nbsp;<asp:Button
            ID="btUpdatePadding" runat="server" OnClick="btUpdatePadding_Click" Text="Update Padding" /><br />
        1. cafefThongKe_All_DataTable_" + san + "_" + tab + "_" + order<br />
        2. cafefThongKe_Padding_DataTable_" + PageIndex + "_" + PageSize + "_" + san + "_" + order + "_" + tab<br />        
        3. cafefThongKe_HTML_" + strOrder + "_" + tab + "_" + san + "_" + PageIndex + "_" + timeNow (cacheThongKe_desc_2_hose_2_16-09)<asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Rows="20" Width="100%"></asp:TextBox>
     <%--"cafefThongKe_Padding_DataTable_" + PageIndex + "_" + PageSize + "_" + san + "_" + order + "_" + tab;--%>   
    </div>
    <div style="float:left">
        Xoa cache do thi ngoai trang chu:<br />
        <asp:Button ID="btDelHa" runat="server" Text="Del Cache Ha" OnClick="btDelHa_Click" /><asp:Button ID="btDelHo" runat="server" Text="Del Cache Ho" OnClick="btDelHo_Click" /><asp:Button ID="btViewHa" runat="server" Text="View Cache Ha" OnClick="btViewHa_Click" /><asp:Button ID="btViewHo" runat="server" Text="View Cache Ho" OnClick="btViewHo_Click"  />
    </div>
    </form>
</body>
</html>
