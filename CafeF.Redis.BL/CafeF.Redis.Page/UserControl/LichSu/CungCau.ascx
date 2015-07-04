<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CungCau.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.LichSu.CungCau" %>
<%@ Register src="../DatePicker/DatePicker.ascx" tagname="DatePicker" tagprefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="CafeF.Redis.Page" Assembly="CafeF.Redis.Page" %>

<table cellpadding="0" cellspacing="0" width="100%" class="SearchDataHistory_SearchForm">
    <tr>
        <td>Mã<asp:TextBox runat="server" ID="txtKeyword" Width="200px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtKeyword"
                ErrorMessage="*" ValidationGroup="SearchData"></asp:RequiredFieldValidator></td>
        <td style="width:170px">Từ ngày<uc1:DatePicker ID="dpkTradeDate1" runat="server" /></td>
        <td style="width:170px">Đến ngày<uc1:DatePicker ID="dpkTradeDate2" runat="server" /></td>
        <td align="left" style="width:30px"><asp:ImageButton ID="btSearch" runat="server" ImageUrl="http://cafef3.vcmedia.vn/images/images/xem.gif" OnClick="btSearch_Click" ValidationGroup="SearchData" /></td>
        <td align="right"><img align="absmiddle" src="http://cafef3.vcmedia.vn/images/new.gif" alt=""/><a href="/TraCuuLichSu2/TraCuu.chn">Xem toàn thị trường theo phiên</a></td>
    </tr>
</table><asp:UpdatePanel ID="panelAjax" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
<div style="background-color:#FFF;width:100%;float:left" align="center">

<asp:Repeater runat="server" ID="rptData" OnItemDataBound="rptData_ItemDataBound">
    <HeaderTemplate>       
    <table cellpadding="2" cellspacing="0" width="100%" style="border-top: solid 1px #e6e6e6"
            class="GirdTable">
            <tr style="border-bottom: solid 1px #e6e6e6; font-family: Arial; font-size: 10px;
                font-weight: bold; color: #004276; background-color: #FFF">
                <td class="Header_DateItem" style="height:30px;width:6%">Ngày</td>
                <td class="Header_Price" style="height:30px;width:7%">Dư mua</td>
                <td class="Header_Price" style="height:30px;width:7%">Dư bán</td>
                <td class="Header_Price" style="height:30px;width:10%">Thay đổi (+/-%)</td>    
                <td class="Header_ChangeItem" style="height:30px;width:6%">Số lệnh <br /> mua</td>
                <td class="Header_Price" style="height:30px;width:6%">Khối lượng <br /> đặt mua</td>
                  <td class="Header_ChangeItem" style="height:30px;width:6%"><i> KLTB <br /> 1 lệnh mua</i> </td>    
                <td class="Header_Price" style="height:30px;width:6%">Số lệnh đặt bán</td>
                <td class="Header_Price" style="height:30px;width:6%">Khối lượng <br /> đặt bán</td>
                  <td class="Header_ChangeItem" style="height:30px;width:6%"><i>KLTB <br /> 1 lệnh bán</i> </td>    
                <td class="Header_Price" style="height:30px;width:10%">Chênh lệch KL <br/> đặt mua - đặt bán</td>
            </tr> 
    </HeaderTemplate>
    <FooterTemplate></table>
        </FooterTemplate>
    <ItemTemplate>
        <tr style="font-family: Arial; font-size: 10px; font-weight: normal; background-color: #f2f2f2" runat="server" id="itemTR">
            <td class="Item_DateItem" style="height:30px;"><%#String.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem, "TradeDate"))%></td>            
            <td class="Item_Price" style="height:30px;padding-right:8px"><%# String.Format("{0:#,##0}", Convert.ToInt64(DataBinder.Eval(Container.DataItem, "BuyLeft")) )%></td>
            <td class="Item_Price" style="height:30px;padding-right:8px"><%# String.Format("{0:#,##0}", Convert.ToInt64(DataBinder.Eval(Container.DataItem, "SellLeft")))%></td>
            <td class="Item_Price" style="height:30px;"><asp:Literal runat="server" ID="ltrPrice"></asp:Literal></td>
            <td class="Item_Price" style="height:30px;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "BuyOrderCount"))%>&nbsp;</td>
            <td class="Item_Price"  style="height:30px;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "BuyVolume"))%>&nbsp;</td>
            <td class="Item_Price" style="height:30px;font-style:italic;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "BuyAverage"))%> &nbsp;</td>
            <td class="Item_Price" style="height:30px;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "SellOrderCount"))%>&nbsp;</td>
            <td class="Item_Price"  style="height:30px;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "SellVolume"))%>&nbsp;</td>  
            <td class="Item_Price"  style="height:30px;font-style:italic;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "SellAverage"))%> &nbsp;</td>            
            <td class="Item_Price"  style="height:30px;padding-right:8px"><asp:Literal runat="server" ID="ltrChange"></asp:Literal></td>
        </tr>
    </ItemTemplate>
    <AlternatingItemTemplate>
        <tr style="font-family: Arial; font-size: 10px; font-weight: normal; background-color: #fff" runat="server" id="Tr1">
            <td class="Item_DateItem" style="height:30px;"><%#String.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem, "TradeDate"))%></td>       
            <td class="Item_Price" style="height:30px;padding-right:8px"><%# String.Format("{0:#,##0}", Convert.ToInt64(DataBinder.Eval(Container.DataItem, "BuyLeft")) )%></td>
            <td class="Item_Price" style="height:30px;padding-right:8px"><%# String.Format("{0:#,##0}", Convert.ToInt64(DataBinder.Eval(Container.DataItem, "SellLeft")))%></td>
            <td class="Item_Price" style="height:30px;"><asp:Literal runat="server" ID="ltrPrice"></asp:Literal></td>
            <td class="Item_Price" style="height:30px;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "BuyOrderCount"))%>&nbsp;</td>
            <td class="Item_Price"  style="height:30px;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "BuyVolume"))%>&nbsp;</td>
            <td class="Item_Price" style="height:30px;font-style:italic;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "BuyAverage"))%> &nbsp;</td>
            <td class="Item_Price"  style="height:30px;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "SellOrderCount"))%>&nbsp;</td>
            <td class="Item_Price"  style="height:30px;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "SellVolume"))%>&nbsp;</td>            
            <td class="Item_Price"  style="height:30px;font-style:italic;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "SellAverage"))%> &nbsp;</td>  
            <td class="Item_Price"  style="height:30px;padding-right:8px"><asp:Literal runat="server" ID="ltrChange"></asp:Literal></td>
        </tr>
    </AlternatingItemTemplate>
</asp:Repeater>
<div style="border-bottom: solid 0px #dadada;padding-bottom: 10px; text-align: right; float: right">
    <div style="float: right;">
        <cc1:pager id="pager1" runat="server" alternativetextenabled="False" compactedpagecount="20"
                         enablesmartshortcuts="False" maxsmartshortcutcount="0" nextclause=">" notcompactedpagecount="1"
                         oncommand="pager1_Command" pagesize="20" previousclause="<" showfirstlast="False"
                         smartshortcutratio="0" smartshortcutthreshold="0"></cc1:pager>
    </div>
</div>
</div>
 </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="pager1" EventName="Command" />
        <asp:AsyncPostBackTrigger ControlID="btSearch" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
<script type="text/javascript">
    var TextBox_KeywordId = '<%=txtKeyword.ClientID%>';
    $().ready(function() {
        $('#' + TextBox_KeywordId).autocomplete(oc, {
            minChars: 1,
            delay: 10,
            width: 400,
            matchContains: true,
            autoFill: false,
            Portfolio:false,
            GDNB:true,
            formatItem: function(row) {
                return row.c + " - " + row.m + "@" + row.o;
            },
            formatResult: function(row) {
                return row.c;
            }            
        });
    });
</script>