<%@ Control Language="C#" AutoEventWireup="true" Codebehind="LichSuGia.ascx.cs" Inherits="CafeF.Redis.Page.TraCuuLichSu2.LichSuGia" %>
<%@ Register Src="~/UserControl/DatePicker/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<link href="http://cafef3.vcmedia.vn/TraCuuLichSu/js/css.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript" src="http://cafef3.vcmedia.vn/TraCuuLichSu/js/sort.js"></script>
<table cellpadding="0" class="SearchDataHistory_SearchForm">
    <tr>
        <td style="width: 250px">Sàn<select id="sSan" runat="server" onchange="select2();" style="width: 200px">
                <option value="HASTC">HASTC</option>
                <option value="HOSE">HOSE</option>
                <option value="UPCOM">UPCOM</option>
            </select>
        </td>
        <td>Ngày<uc1:DatePicker ID="dpkTradeDate1" runat="server" /></td>
        <td align="left">
            <img src="http://cafef3.vcmedia.vn/images/images/xem.gif" onclick="select2()" style="cursor: pointer" id="IMG1" />
        </td>
    </tr>
</table>
<div style="background-color: #FFF; width: 100%; float: left" align="left">
    <div style="text-align: center; border: solid 1px #e6e6e6; color: #024174; font-family: Verdana;
        font-size: 12px;">
        <div id="lblIndexPoint" runat="server" style="padding: 5px; font-weight: bold;">
        </div>
        <div style="border-top: solid 1px #e6e6e6; padding: 5px; background-color: #f2f2f2;
            font-weight: bold;">
            <span id="divToTalUp" runat="server"></span><span id="spanKichTran" runat="server"></span>
            <span id="divTotalNochange" runat="server"></span><span id="divTotalDown" runat="server">
            </span><span id="spanKichSan" runat="server"></span>
        </div>
        <div runat="server" id="div1" style="border-top: solid 1px #e6e6e6; padding: 5px;">
        </div>
        <div runat="server" id="div2" style="border-top: solid 1px #e6e6e6; padding: 5px;
            background-color: #f2f2f2;">
        </div>
    </div>
    <table cellpadding="2" cellspacing="0" style="border-top: solid 1px #e6e6e6; border-bottom: solid 2px #e6e6e6;"
        id="tblGridData">
        <tr style="border-bottom: solid 1px #e6e6e6; font-family: Arial; font-size: 10px;
            font-weight: bold; color: #004276; background-color: #FFF">
            <td class="Header_DateItem_lsg" style="width: 60px;">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <thead>
                        <tr>
                            <td align="center" rowspan="2">
                                <a class='SorttingLink' href="javascript:doSort2('up0','down0','0','table2sort');"
                                    onclick="javascript:setColor(this);">Mã </a>
                            </td>
                            <td align="center" style="vertical-align: bottom;">
                                <img id='up0' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' onclick='doSort(this, "0D", "table2sort");'
                                    class="updownimg" /></td>
                        </tr>
                        <tr>
                            <td align="center" style="vertical-align: top; padding-top: 1px;">
                                <img id='down0' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_dowon.gif" name='btnDown'
                                    style="margin: 1px 0px 0px;" onclick='doSort(this, "0A", "table2sort");' class="updownimg" /></td>
                        </tr>
                    </thead>
                </table>
            </td>
            <td class="Header_Price1" style="width: 60px">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <thead>
                        <tr>
                            <td align="center" rowspan="2">
                                <a class='SorttingLink' href="javascript:doSort2('up1','down1','1','table2sort');"
                                    onclick="javascript:setColor(this);">Giá đóng cửa </a>
                            </td>
                            <td align="center" style="vertical-align: bottom;">
                                <img id='up1' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' onclick='doSort(this, "1D", "table2sort");'
                                    class="updownimg" /></td>
                        </tr>
                        <tr>
                            <td align="center" style="vertical-align: top; padding-top: 1px;">
                                <img id='down1' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown' style="margin: 1px 0px 0px;"
                                    onclick='doSort(this, "1A", "table2sort");' class="updownimg" /></td>
                        </tr>
                    </thead>
                </table>
            </td>
            <td class="Header_Price1" runat="server" id="avgPriceHeader" style="width: 60px">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <thead>
                        <tr>
                            <td align="center" rowspan="2">
                                <a class='SorttingLink' href="javascript:doSort2('up2','down2','2','table2sort');"
                                    onclick="javascript:setColor(this);">Giá bình quân </a>
                            </td>
                            <td align="center" style="vertical-align: bottom;">
                                <img id='up2' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' onclick='doSort(this, "2D", "table2sort");'
                                    class="updownimg" /></td>
                        </tr>
                        <tr>
                            <td align="center" style="vertical-align: top; padding-top: 1px;">
                                <img id='down2' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown' style="margin: 1px 0px 0px;"
                                    onclick='doSort(this, "2A", "table2sort");' class="updownimg" /></td>
                        </tr>
                    </thead>
                </table>
            </td>
            <td class="Header_Price1" style="width: 95px">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <thead>
                        <tr>
                            <td align="center" rowspan="2">
                                <a style="color: Red;" class='SorttingLink' href="javascript:doSort2('up3','down3','13','table2sort');"
                                    onclick="javascript:setColor(this);">Thay đổi (+/-%) </a>
                            </td>
                            <td align="center" style="vertical-align: bottom;">
                                <img id='up3' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' onclick='doSort(this, "13D", "table2sort");'
                                    class="updownimg" /></td>
                        </tr>
                        <tr>
                            <td align="center" style="vertical-align: top; padding-top: 1px;">
                                <img id='down3' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown' style="margin: 1px 0px 0px;"
                                    onclick='doSort(this, "3A", "table2sort");' class="updownimg" /></td>
                        </tr>
                    </thead>
                </table>
            </td>
            <td class="Header_Price1" runat="server" id="BasicPriceColHeader" style="width: 70px">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <thead>
                        <tr>
                            <td align="center" rowspan="2">
                                <a class='SorttingLink' href="javascript:doSort2('up5','down5','5','table2sort');"
                                    onclick="javascript:setColor(this);">Giá tham chiếu </a>
                            </td>
                            <td align="center" style="vertical-align: bottom;">
                                <img id='up5' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' onclick='doSort(this, "5D", "table2sort");'
                                    class="updownimg" /></td>
                        </tr>
                        <tr>
                            <td align="center" style="vertical-align: top; padding-top: 1px;">
                                <img id='down5' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown' style="margin: 1px 0px 0px;"
                                    onclick='doSort(this, "5A", "table2sort");' class="updownimg" /></td>
                        </tr>
                    </thead>
                </table>
            </td>
            <td class="Header_Price1" style="width: 70px">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <thead>
                        <tr>
                            <td align="center" rowspan="2">
                                <a class='SorttingLink' href="javascript:doSort2('up6','down6','6','table2sort');"
                                    onclick="javascript:setColor(this);">Giá mở cửa </a>
                            </td>
                            <td align="center" style="vertical-align: bottom;">
                                <img id='up6' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' onclick='doSort(this, "6D", "table2sort");'
                                    class="updownimg" /></td>
                        </tr>
                        <tr>
                            <td align="center" style="vertical-align: top; padding-top: 1px;">
                                <img id='down6' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown' style="margin: 1px 0px 0px;"
                                    onclick='doSort(this, "6A", "table2sort");' class="updownimg" /></td>
                        </tr>
                    </thead>
                </table>
            </td>
            <td class="Header_Price1" style="width: 70px">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <thead>
                        <tr>
                            <td align="center" rowspan="2">
                                <a class='SorttingLink' href="javascript:doSort2('up7','down7','7','table2sort');"
                                    onclick="javascript:setColor(this);">Giá cao nhất </a>
                            </td>
                            <td align="center" style="vertical-align: bottom;">
                                <img id='up7' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' onclick='doSort(this, "7D", "table2sort");'
                                    class="updownimg" /></td>
                        </tr>
                        <tr>
                            <td align="center" style="vertical-align: top; padding-top: 1px;">
                                <img id='down7' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown' style="margin: 1px 0px 0px;"
                                    onclick='doSort(this, "7A", "table2sort");' class="updownimg" /></td>
                        </tr>
                    </thead>
                </table>
            </td>
            <td class="Header_Price1" style="width: 70px">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <thead>
                        <tr>
                            <td align="center" rowspan="2">
                                <a class='SorttingLink' href="javascript:doSort2('up8','down8','8','table2sort');"
                                    onclick="javascript:setColor(this);">Giá thấp nhất </a>
                            </td>
                            <td align="center" style="vertical-align: bottom;">
                                <img id='up8' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' onclick='doSort(this, "8D", "table2sort");'
                                    class="updownimg" /></td>
                        </tr>
                        <tr>
                            <td align="center" style="vertical-align: top; padding-top: 1px;">
                                <img id='down8' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown' style="margin: 1px 0px 0px;"
                                    onclick='doSort(this, "8A", "table2sort");' class="updownimg" /></td>
                        </tr>
                    </thead>
                </table>
            </td>
            <td class="Header_Price1" style="width: 90px">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <thead>
                        <tr>
                            <td align="center" rowspan="2">
                                <a id="_lkgdKhopLenhLabel" class='SorttingLink' href="javascript:doSort2('up9','down9','9','table2sort');"
                                    onclick="javascript:setColor(this);">KLGD khớp lệnh </a>
                            </td>
                            <td align="center" style="vertical-align: bottom;">
                                <img id='up9' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' onclick='doSort(this, "9D", "table2sort");'
                                    class="updownimg" /></td>
                        </tr>
                        <tr>
                            <td align="center" style="vertical-align: top; padding-top: 1px;">
                                <img id='down9' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown' style="margin: 1px 0px 0px;"
                                    onclick='doSort(this, "9A", "table2sort");' class="updownimg" /></td>
                        </tr>
                    </thead>
                </table>
            </td>
            <td class="Header_Price1" style="width: 95px" id="gtgdSort" runat="server">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <thead>
                        <tr>
                            <td align="center" rowspan="2">
                                <a id="_gtgdKhopLenhLabel" class='SorttingLink' href="javascript:doSort2('up10','down10','10','table2sort');"
                                    onclick="javascript:setColor(this);">GTGD khớp lệnh </a>
                            </td>
                            <td align="center" style="vertical-align: bottom;">
                                <img id='up10' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' onclick='doSort(this, "10D", "table2sort");'
                                    class="updownimg" /></td>
                        </tr>
                        <tr>
                            <td align="center" style="vertical-align: top; padding-top: 1px;">
                                <img id='down10' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown'
                                    style="margin: 1px 0px 0px;" onclick='doSort(this, "10A", "table2sort");' class="updownimg" /></td>
                        </tr>
                    </thead>
                </table>
            </td>
            <td class="Header_Price1" style="width: 90px" id="klgdThoaThuanSort" runat="server">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <thead>
                        <tr>
                            <td align="center" rowspan="2">
                                <a id="_lkgdThoathuanLabel" class='SorttingLink' href="javascript:doSort2('up11','down11','11','table2sort');"
                                    onclick="javascript:setColor(this);">KLGD thỏa thuận </a>
                            </td>
                            <td align="center" style="vertical-align: bottom;">
                                <img id='up11' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' onclick='doSort(this, "11D", "table2sort");'
                                    class="updownimg" /></td>
                        </tr>
                        <tr>
                            <td align="center" style="vertical-align: top; padding-top: 1px;">
                                <img id='down11' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown'
                                    style="margin: 1px 0px 0px;" onclick='doSort(this, "11A", "table2sort");' class="updownimg" /></td>
                        </tr>
                    </thead>
                </table>
            </td>
            <td class="Header_Price1" style="width: 109px" id="gtgdThoaThuanSort" runat="server">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <thead>
                        <tr>
                            <td align="center" rowspan="2">
                                <a id="_gtgdThoathuanLabel" class='SorttingLink' href="javascript:doSort2('up12','down12','12','table2sort');"
                                    onclick="javascript:setColor(this);">GTGD thỏa thuận </a>
                            </td>
                            <td align="center" style="vertical-align: bottom;">
                                <img id='up12' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' onclick='doSort(this, "12D", "table2sort");'
                                    class="updownimg" /></td>
                        </tr>
                        <tr>
                            <td align="center" style="vertical-align: top; padding-top: 1px;">
                                <img id='down12' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown'
                                    style="margin: 1px 0px 0px;" onclick='doSort(this, "12A", "table2sort");' class="updownimg" /></td>
                        </tr>
                    </thead>
                </table>
            </td>
        </tr>
    </table>
    <div id='divScroll' style="overflow-y: scroll; overflow-x: hidden; height: 440px;
        width: 100%; text-align: left;">
        <table id="table2sort" cellpadding="0" cellspacing="0" style="border-top: solid 1px #e6e6e6;">
            <thead>
                <tr style="display: none;" id="trTop">
                    <td>&nbsp</td>
                    <td>&nbsp</td>
                    <td>&nbsp</td>
                    <td id="avgHeadTD" runat="server">&nbsp</td>
                    <td>&nbsp</td>
                    <td>&nbsp</td>
                    <td>&nbsp</td>
                    <td>&nbsp</td>
                    <td>&nbsp</td>
                    <td>&nbsp</td>
                    <td>&nbsp</td>
                    <td>&nbsp</td>
                    <td>&nbsp</td>
                    <td></td>
                </tr>
            </thead>
            <asp:Repeater runat="server" ID="rptData" OnItemDataBound="rptData_ItemDataBound">
                <ItemTemplate>
                    <tr style="font-family: Arial; font-size: 10px; font-weight: normal; background-color: #FFF;"
                        runat="server" id="itemTR">
                        <td class="Item_DateItem_lsg"><a href="javascript:void(0);" class="symbol" rel="<%# Eval("Symbol") %>"><%#DataBinder.Eval(Container.DataItem, "Symbol")%></a></td>
                        <td class="Item_Price1"><%#String.Format("{0:#,##0.0}",DataBinder.Eval(Container.DataItem, "ClosePrice"))%>&nbsp;</td>
                        <td class="Item_Price1" runat="server" id="avgPriceItem"><asp:Literal runat="server" ID="ltrAveragePrice"></asp:Literal>&nbsp;</td>
                        <td class="Item_ChangePrice_lsg"><asp:Literal runat="server" ID="ltrChange"></asp:Literal></td>
                        <td class="Item_Image"><asp:Literal runat="server" ID="ltrImage"></asp:Literal></td>
                        <td class="Item_Price1" runat="server" id="BasicPriceColItem"><%#String.Format("{0:#,##0.0}",DataBinder.Eval(Container.DataItem, "BasicPrice"))%>&nbsp;</td>
                        <td class="Item_Price1"><%#String.Format("{0:#,##0.0}",DataBinder.Eval(Container.DataItem, "OpenPrice"))%>&nbsp;</td>
                        <td class="Item_Price1"><%#String.Format("{0:#,##0.0}",DataBinder.Eval(Container.DataItem, "HighPrice"))%>&nbsp;</td>
                        <td class="Item_Price1"><%#String.Format("{0:#,##0.0}",DataBinder.Eval(Container.DataItem, "LowPrice"))%>&nbsp;</td>
                        <td class="Item_Price1"><%#String.Format("{0:#,##0}",DataBinder.Eval(Container.DataItem, "Volume"))%>&nbsp;</td>
                        <td class="Item_Price1"><%#String.Format("{0:#,##0}",DataBinder.Eval(Container.DataItem, "TotalValue"))%>&nbsp;</td>
                        <td class="Item_Price1"><%# DataBinder.Eval(Container.DataItem, "AgreedVolume") != DBNull.Value ? String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "AgreedVolume")) : ""%>&nbsp;</td>
                        <td class="LastItem_Price"><%# DataBinder.Eval(Container.DataItem, "AgreedValue") != DBNull.Value ? String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "AgreedValue")) : ""%>&nbsp;</td>
                        <td id="percentTd" runat="server" style="display: none;">&nbsp;</td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr style="font-family: Arial; font-weight: normal; background-color: #f2f2f2" runat="server"
                        id="altitemTR">
                        <td class="Item_DateItem_lsg"><a href=''><%#DataBinder.Eval(Container.DataItem, "Symbol")%></a></td>
                        <td class="Item_Price1"><%#String.Format("{0:#,##0.0}",DataBinder.Eval(Container.DataItem, "ClosePrice"))%>&nbsp;</td>
                        <td class="Item_Price1" runat="server" id="avgPriceItem"><asp:Literal runat="server" ID="ltrAveragePrice"></asp:Literal>&nbsp;</td>
                        <td class="Item_ChangePrice_lsg"><asp:Literal runat="server" ID="ltrChange"></asp:Literal></td>
                        <td class="Item_Image"><asp:Literal runat="server" ID="ltrImage"></asp:Literal></td>
                        <td class="Item_Price1" runat="server" id="BasicPriceColItem"><%#String.Format("{0:#,##0.0}",DataBinder.Eval(Container.DataItem, "BasicPrice"))%>&nbsp;</td>
                        <td class="Item_Price1"><%#String.Format("{0:#,##0.0}",DataBinder.Eval(Container.DataItem, "OpenPrice"))%>&nbsp;</td>
                        <td class="Item_Price1"><%#String.Format("{0:#,##0.0}",DataBinder.Eval(Container.DataItem, "HighPrice"))%>&nbsp;</td>
                        <td class="Item_Price1"><%#String.Format("{0:#,##0.0}",DataBinder.Eval(Container.DataItem, "LowPrice"))%>&nbsp;</td>
                        <td class="Item_Price1"><%#String.Format("{0:#,##0}",DataBinder.Eval(Container.DataItem, "Volume"))%>&nbsp;</td>
                        <td class="Item_Price1"><%#String.Format("{0:#,##0}",DataBinder.Eval(Container.DataItem, "TotalValue"))%>&nbsp;</td>
                        <td class="Item_Price1"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "AgreedVolume"))%>&nbsp;</td>
                        <td class="LastItem_Price"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "AgreedValue"))%>&nbsp;</td>
                        <td id="percentTd" runat="server" style="display: none;">&nbsp;</td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
            <tfoot>
                <tr style="border-bottom: solid 1px #e6e6e6; font-family: Arial; font-size: 10px;
                    font-weight: bold; color: #004276; background-color: #FFF;visibility:hidden;">
                    <td class="Header_DateItem_lsg" style="width: 64px;">&nbsp</td>
                    <td class="Header_Price1" style="width: 64px">&nbsp</td>
                    <td class="Header_Price1" style="width: 64px" runat="server" id="avgFootTD">&nbsp</td>
                    <td class="Header_ChangeItem" style="width: 78px">&nbsp</td>
                    <td class="Header_Price1" style="width: 20px" runat="server" id="BasicPriceColHeader1">&nbsp</td>
                    <td class="Header_Price1" style="width: 74px">&nbsp</td>
                    <td class="Header_Price1" style="width: 74px">&nbsp</td>
                    <td class="Header_Price1" style="width: 74px">&nbsp</td>
                    <td class="Header_Price1" style="width: 74px">&nbsp</td>
                    <td class="Header_Price1" style="width: 94px">&nbsp</td>
                    <td class="Header_Price1" style="width: 99px" id="gtgdHead" runat="server">&nbsp</td>
                    <td class="Header_Price1" style="width: 94px" id="klgdThoaThuanHead" runat="server">&nbsp</td>
                    <td class="Header_LastItem" style="width: 96px" id="gtgdThoaThuanHead" runat="server">&nbsp</td>
                    <td style="display: none;"></td>
                </tr>
            </tfoot>
        </table>
    </div>
</div>
<script language="javascript" type="text/javascript">
var sSan = document.getElementById("<%= sSan.ClientID %>");
var dtPicker = document.getElementById("<%= this.dpkTradeDate1.ClientID %>");
var dateValue = '<%= dateTextBox %>';
var sanValue = '<%= san %>';
var host = "http://<%= this.Request.Url.Authority %>";
var tab = 1;
function select2(){
    var date = dtPicker.value;
    var url = host + "/TraCuuLichSu2/" + tab + "/" + sSan.value + "/" + date + ".chn";
    window.location = url;
}
var lib = new CafeF_Library();
$(document).ready(function(e) {    
    $('.symbol').each(function() {
    $(this).attr('href', lib.GetCompanyInfoLink($(this).attr('rel')));
    });
});

</script>
<script language="javascript" type="text/javascript" src="http://cafef3.vcmedia.vn/TraCuuLichSu/js/js.js">
</script>
