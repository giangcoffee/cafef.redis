<%@ Control Language="C#" AutoEventWireup="true" Codebehind="LichSuGia.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.LichSu.LichSuGia"  EnableViewState="false" %>
<%@ Register src="../DatePicker/DatePicker.ascx" tagname="DatePicker" tagprefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="CafeF.Redis.Page" Assembly="CafeF.Redis.Page" %>

<table cellpadding="0" cellspacing="0" width="100%" class="SearchDataHistory_SearchForm">
    <tr>
        <td>Mã<asp:TextBox runat="server" ID="txtKeyword" Width="200px" ValidationGroup="SearchData"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtKeyword" ErrorMessage="*" ValidationGroup="SearchData"></asp:RequiredFieldValidator></td>
        <td style="width:170px">Từ ngày<uc1:DatePicker ID="dpkTradeDate1" runat="server" /></td>
        <td style="width:170px">Đến ngày<uc1:DatePicker ID="dpkTradeDate2" runat="server" /></td>
        <td align="left" style="width:30px">
            <asp:ImageButton ID="btSearch" runat="server" ImageUrl="http://cafef3.vcmedia.vn/images/images/xem.gif" OnClick="btSearch_Click" ValidationGroup="SearchData" /></td>
        <td align="right">
            <a href="/TraCuuLichSu2/TraCuu.aspx?tab=1&san=HASTC&date=">Xem toàn thị trường theo phiên</a>
        </td>
    </tr>
</table><asp:UpdatePanel ID="panelAjax" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
<div id="notHO" runat="server" style="background-color:#FFF;width:100%;float:left" align="center" visible=false>
<table cellpadding="2" cellspacing="0" width="100%" style="border-top: solid 1px #e6e6e6"
            id="GirdTable">
            <tr style="border-bottom: solid 1px #e6e6e6; font-family: Arial; font-size: 10px;
                font-weight: bold; color: #004276; background-color: #FFF">
                <td rowspan="2" class="Header_DateItem" style="width:8%">Ngày</td>
                <td rowspan="2" class="Header_Price1" style="width:7%">Giá <br /> đóng cửa</td>
                <td rowspan="2" class="Header_Price1" runat="server" id="avgPriceHeader" style="width:7%">Giá <br /> bình quân</td>
                <td rowspan="2" class="Header_ChangeItem" colspan="2" style="width:11%">Thay đổi (+/-%)</td>
                <td colspan="2" class="Header_Price1" style="width:17%">GD khớp lệnh</td>
                <td colspan="2" class="Header_Price1" style="width:18%">GD thỏa thuận</td>
                <td rowspan="2" class="Header_Price1" runat="server" id="BasicPriceColHeader" style="width:7%">Giá <br /> tham chiếu</td>
                <td rowspan="2" class="Header_Price1" style="width:7%">Giá <br />mở cửa</td>
                <td rowspan="2" class="Header_Price1" style="width:7%">Giá <br />cao nhất</td>
                <td rowspan="2" class="Header_Price1" style="width:7%">Giá <br />thấp nhất</td>
            </tr>
            <tr style="border-bottom: solid 1px #e6e6e6; font-family: Arial; font-size: 10px;
                font-weight: bold; color: #004276; background-color: #FFF" >
                <td class="Header_Price1" style="width:7%"><asp:Label ID="lblKLGDKL" Text="KL" runat="server"></asp:Label></td>
                <td class="Header_Price1" style="width:11%"><asp:Label ID="lblGTGDKL" Text="GT" runat="server"></asp:Label></td>
                <td class="Header_Price1" style="width:7%"><asp:Label ID="lblKLGDTT" Text="KL" runat="server"></asp:Label></td>
                <td class="Header_LastItem" style="width:11%"><asp:Label ID="lblGRGDTT" Text="GT" runat="server"></asp:Label></td>
            </tr>     
<asp:Repeater runat="server" ID="rptData" onitemdatabound="rptData_ItemDataBound">
    <HeaderTemplate>       
    </HeaderTemplate>
    <FooterTemplate>
        </table></FooterTemplate>
    <ItemTemplate>
        <tr style="font-family: Arial; font-size: 10px; font-weight: normal; background-color: #FFF;"
            runat="server" id="itemTR">
            <td class="Item_DateItem"><%#String.Format("{0:dd/MM/yyyy}",DataBinder.Eval(Container.DataItem, "TradeDate"))%></td>
            <td class="Item_Price1"><%#String.Format("{0:#,##0.0}",DataBinder.Eval(Container.DataItem, "ClosePrice"))%>&nbsp;</td>
            <td class="Item_Price1" runat="server" id="avgPriceItem"><asp:Literal runat="server" ID="ltrAveragePrice"></asp:Literal>&nbsp;</td>
            <td class="Item_ChangePrice"><asp:Literal runat="server" ID="ltrChange"></asp:Literal></td>
            <td class="Item_Image"><asp:Literal runat="server" ID="ltrImage"></asp:Literal></td>
            <td class="Item_Price1"><%#String.Format("{0:#,##0}",DataBinder.Eval(Container.DataItem, "Volume"))%>&nbsp;</td>
            <td class="Item_Price1"><%#String.Format("{0:#,##0}",DataBinder.Eval(Container.DataItem, "TotalValue"))%>&nbsp;</td>
            <td class="Item_Price1"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "AgreedVolume"))%>&nbsp;</td>
            <td class="LastItem_Price"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "AgreedValue"))%>&nbsp;</td>
            <td class="Item_Price1" runat="server" id="BasicPriceColItem"><%#String.Format("{0:#,##0.0}",DataBinder.Eval(Container.DataItem, "BasicPrice"))%>&nbsp;</td>
            <td class="Item_Price1"><%#String.Format("{0:#,##0.0}",DataBinder.Eval(Container.DataItem, "OpenPrice"))%>&nbsp;</td>
            <td class="Item_Price1"><%#String.Format("{0:#,##0.0}",DataBinder.Eval(Container.DataItem, "HighPrice"))%>&nbsp;</td>
            <td class="Item_Price1"><%#String.Format("{0:#,##0.0}",DataBinder.Eval(Container.DataItem, "LowPrice"))%>&nbsp;</td>
        </tr>
    </ItemTemplate>
    <AlternatingItemTemplate>
        <tr style="font-family: Arial; font-weight: normal; background-color: #f2f2f2"
            runat="server" id="altitemTR">
            <td class="Item_DateItem"><%#String.Format("{0:dd/MM/yyyy}",DataBinder.Eval(Container.DataItem, "TradeDate"))%></td>
            <td class="Item_Price1"><%#String.Format("{0:#,##0.0}",DataBinder.Eval(Container.DataItem, "ClosePrice"))%>&nbsp;</td>
            <td class="Item_Price1"  runat="server" id="avgPriceItem"><asp:Literal runat="server" ID="ltrAveragePrice"></asp:Literal>&nbsp;</td>
            <td class="Item_ChangePrice"><asp:Literal runat="server" ID="ltrChange"></asp:Literal></td>
            <td class="Item_Image"><asp:Literal runat="server" ID="ltrImage"></asp:Literal></td>
            <td class="Item_Price1"><%#String.Format("{0:#,##0}",DataBinder.Eval(Container.DataItem, "Volume"))%>&nbsp;</td>
            <td class="Item_Price1"><%#String.Format("{0:#,##0}",DataBinder.Eval(Container.DataItem, "TotalValue"))%>&nbsp;</td>
            <td class="Item_Price1"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "AgreedVolume"))%>&nbsp;</td>
            <td class="LastItem_Price"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "AgreedValue"))%>&nbsp;</td>
            <td class="Item_Price1" runat="server" id="BasicPriceColItem"><%#String.Format("{0:#,##0.0}",DataBinder.Eval(Container.DataItem, "BasicPrice"))%>&nbsp;</td>
            <td class="Item_Price1"><%#String.Format("{0:#,##0.0}",DataBinder.Eval(Container.DataItem, "OpenPrice"))%>&nbsp;</td>
            <td class="Item_Price1"><%#String.Format("{0:#,##0.0}",DataBinder.Eval(Container.DataItem, "HighPrice"))%>&nbsp;</td>
            <td class="Item_Price1"><%#String.Format("{0:#,##0.0}",DataBinder.Eval(Container.DataItem, "LowPrice"))%>&nbsp;</td>
        </tr>
    </AlternatingItemTemplate>
</asp:Repeater>
<div style=" border-bottom: solid 0px #dadada;
    padding-bottom: 10px; text-align: right; float: right">
    <div style="float: right;">
       <cc1:pager id="pager1" runat="server" alternativetextenabled="False" compactedpagecount="20"
                         enablesmartshortcuts="False" maxsmartshortcutcount="0" nextclause=">" notcompactedpagecount="1"
                         oncommand="pager1_Command" pagesize="20" previousclause="<" showfirstlast="False"
                         smartshortcutratio="0" smartshortcutthreshold="0" ></cc1:pager></i></td> 
                
    </div>
</div>
</div>
<div id="divHO" runat="server" style="background-color:#FFF;width:100%;float:left" visible=false align="center">
<table cellpadding="2" cellspacing="0" width="100%" style="border-top: solid 1px #e6e6e6"
            id="GirdTable2">
            <tr style="border-bottom: solid 1px #e6e6e6; font-family: Arial; font-size: 10px;
                font-weight: bold; color: #004276; background-color: #FFF" >
                <td rowspan="2" class="Header_DateItem" style="width:6%">Ngày</td>
                <td rowspan="2" class="Header_Price1" style="width:7%">Giá <br /> đóng cửa</td>
                <td rowspan="2" class="Header_ChangeItem" colspan="2" style="width:11%">Thay đổi (+/-%)</td>
                <td colspan="2" class="Header_Price1" style="width:20%">GD khớp lệnh</td>
                <td colspan="2" class="Header_Price1" style="width:17%">GD thỏa thuận</td>
                <td rowspan="2" class="Header_Price1" style="width:6%">Giá <br />mở cửa</td>               
                <td rowspan="2" class="Header_Price1" style="width:6.5%">Giá <br />cao nhất</td>
                <td rowspan="2" class="Header_Price1" style="width:7%">Giá <br />thấp nhất</td>
                <td rowspan="2" class="Header_Price1" style="width:6.5%">KL <br /> đợt 1</td>
                <td rowspan="2" class="Header_Price1" style="width:6.5%">KL <br /> đợt 2</td>
                <td rowspan="2" class="Header_Price1" style="width:6.5%">KL <br /> đợt 3</td>
            </tr>
            <tr style="border-bottom: solid 1px #e6e6e6; font-family: Arial; font-size: 10px;
                font-weight: bold; color: #004276; background-color: #FFF" >
                <td class="Header_Price1" style="width:8%">KL</td>
                <td class="Header_Price1" style="width:11.5%">GT</td>
                <td class="Header_Price1" style="width:7%">KL</td>
                <td class="Header_Price1" style="width:10.5%">GT</td>
            </tr>     
<asp:Repeater runat="server" ID="rptData2" onitemdatabound="rptData2_ItemDataBound" >
    <HeaderTemplate>       
    </HeaderTemplate>
    <FooterTemplate>
        </table></FooterTemplate>
    <ItemTemplate>
        <tr style="font-family: Arial; font-size: 10px; font-weight: normal; background-color: #FFF;height:30px;padding-right:5px"
            runat="server" id="itemTR">
            <td class="Item_DateItem"><%#String.Format("{0:dd/MM/yyyy}",DataBinder.Eval(Container.DataItem, "TradeDate"))%></td>
            <td class="Item_Price10"><%#String.Format("{0:#,##0.0}",DataBinder.Eval(Container.DataItem, "ClosePrice"))%>&nbsp;</td>
            <td class="Item_ChangePrice"><asp:Literal runat="server" ID="ltrChange1"></asp:Literal></td>
            <td class="Item_Image"><asp:Literal runat="server" ID="ltrImage1"></asp:Literal></td>
            <td class="Item_Price10"><%#String.Format("{0:#,##0}",DataBinder.Eval(Container.DataItem, "Volume"))%>&nbsp;</td>
            <td class="Item_Price10"><%#String.Format("{0:#,##0}",DataBinder.Eval(Container.DataItem, "TotalValue"))%>&nbsp;</td>
            <td class="Item_Price10"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "AgreedVolume"))%>&nbsp;</td>
            <td class="LastItem_Price"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "AgreedValue"))%>&nbsp;</td>
            <td class="Item_Price10"><%#String.Format("{0:#,##0.0}",DataBinder.Eval(Container.DataItem, "OpenPrice"))%>&nbsp;</td>                        
            <td class="Item_Price10"><%#String.Format("{0:#,##0.0}",DataBinder.Eval(Container.DataItem, "HighPrice"))%>&nbsp;</td>
            <td class="Item_Price10"><%#String.Format("{0:#,##0.0}",DataBinder.Eval(Container.DataItem, "LowPrice"))%>&nbsp;</td>
            <td class="Item_Price10"><%#String.Format("{0:#,##0}",DataBinder.Eval(Container.DataItem, "KLGDDot1"))%>&nbsp;</td>
            <td class="Item_Price10"><%#String.Format("{0:#,##0}",DataBinder.Eval(Container.DataItem, "KLGDDot2"))%>&nbsp;</td>
            <td class="Item_Price10"><%#String.Format("{0:#,##0}",DataBinder.Eval(Container.DataItem, "KLGDDot3"))%>&nbsp;</td>            
        </tr>
    </ItemTemplate>
    <AlternatingItemTemplate>
        <tr style="font-family: Arial; font-weight: normal; background-color: #f2f2f2;height:30px;padding-right:5px"
            runat="server" id="altitemTR">
            <td class="Item_DateItem"><%#String.Format("{0:dd/MM/yyyy}",DataBinder.Eval(Container.DataItem, "TradeDate"))%></td>
            <td class="Item_Price10"><%#String.Format("{0:#,##0.0}",DataBinder.Eval(Container.DataItem, "ClosePrice"))%>&nbsp;</td>
            <td class="Item_ChangePrice"><asp:Literal runat="server" ID="ltrChange1"></asp:Literal></td>
            <td class="Item_Image"><asp:Literal runat="server" ID="ltrImage1"></asp:Literal></td>
            <td class="Item_Price10"><%#String.Format("{0:#,##0}",DataBinder.Eval(Container.DataItem, "Volume"))%>&nbsp;</td>
            <td class="Item_Price10"><%#String.Format("{0:#,##0}",DataBinder.Eval(Container.DataItem, "TotalValue"))%>&nbsp;</td>
            <td class="Item_Price10"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "AgreedVolume"))%>&nbsp;</td>
            <td class="LastItem_Price"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "AgreedValue"))%>&nbsp;</td>
            <td class="Item_Price10"><%#String.Format("{0:#,##0.0}",DataBinder.Eval(Container.DataItem, "OpenPrice"))%>&nbsp;</td>                      
            <td class="Item_Price10"><%#String.Format("{0:#,##0.0}",DataBinder.Eval(Container.DataItem, "HighPrice"))%>&nbsp;</td>
            <td class="Item_Price10"><%#String.Format("{0:#,##0.0}",DataBinder.Eval(Container.DataItem, "LowPrice"))%>&nbsp;</td>
            <td class="Item_Price10"><%#String.Format("{0:#,##0}",DataBinder.Eval(Container.DataItem, "KLGDDot1"))%>&nbsp;</td>
            <td class="Item_Price10"><%#String.Format("{0:#,##0}",DataBinder.Eval(Container.DataItem, "KLGDDot2"))%>&nbsp;</td>
            <td class="Item_Price10"><%#String.Format("{0:#,##0}",DataBinder.Eval(Container.DataItem, "KLGDDot3"))%>&nbsp;</td>
        </tr>
    </AlternatingItemTemplate>
</asp:Repeater>
<div style=" border-bottom: solid 0px #dadada;
    padding-bottom: 10px; text-align: right; float: right">
    <div style="float: right;">
        <cc1:pager id="pager2" runat="server" alternativetextenabled="False" compactedpagecount="20"
                         enablesmartshortcuts="False" maxsmartshortcutcount="0" nextclause=">" notcompactedpagecount="1"
                         oncommand="pager2_Command" pagesize="20" previousclause="<" showfirstlast="False"
                         smartshortcutratio="0" smartshortcutthreshold="0"></cc1:pager></i></td> </div>
</div>
</div>

    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="pager1" EventName="Command" />
        <asp:AsyncPostBackTrigger ControlID="pager2" EventName="Command" />
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