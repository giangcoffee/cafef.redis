<%@ Control Language="C#" AutoEventWireup="true" Codebehind="ThongKeDatLenh.ascx.cs" Inherits="CafeF.Redis.Page.TraCuuLichSu2.ThongKeDatLenh" %>
<%@ Register Src="~/UserControl/DatePicker/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<script language="javascript" type="text/javascript" src="http://cafef3.vcmedia.vn/TraCuuLichSu/js/sort.js"></script>
<link href="http://cafef3.vcmedia.vn/TraCuuLichSu/js/css.css" rel="stylesheet" type="text/css" />
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
            <img src="http://cafef3.vcmedia.vn/images/images/xem.gif" alt="" onclick="select2()" style="cursor: pointer" id="IMG1" />
        </td>
    </tr>
</table>
<div style="text-align: center; border: solid 1px #e6e6e6; color: #024174; font-family: Verdana;
    font-size: 12px; background-color: #FFF; font-size: 13px;">
    <center>
        <div>
            <table style="color: #024174; width: 700px;"> 
                <tr>
                    <td align="left" style="color: #024174; width: 500px;">
                        <div id="div1" style="padding: 4px;" runat="server"></div>
                        <div id="div2" style="padding: 4px;" runat="server"></div>
                        <div id="div5" style="padding: 4px;" runat="server"></div>
                    </td>
                    <td style="color: #024174; border-left: solid 1px #024174;">
                        <div id="divDumua" runat="server" style="font-weight: bold;"></div>
                    </td>
                </tr>
            </table>
        </div>
        <div style="background-color: #f2f2f2">
            <table style="width: 700px;">
                <tr>
                    <td align="left" style="color: #024174; width: 500px;">
                        <div id="div3" style="padding: 4px;" runat="server"></div>
                        <div id="div4" style="padding: 4px;" runat="server"></div>
                        <div id="div6" style="padding: 4px;" runat="server"></div>
                    </td>
                    <td style="color: #024174; border-left: solid 1px #024174;">
                        <div id="divDuban" runat="server" style="font-weight: bold;"></div>
                    </td>
                </tr>
            </table>
        </div>
    </center>
</div>
<div style="background-color: #FFF; width: 100%; float: left" align="left">
    <table cellpadding="2" cellspacing="0" style="border-top: solid 1px #e6e6e6; border-bottom: solid 2px #e6e6e6;">
        <tr style="border-bottom: solid 1px #e6e6e6; font-family: Arial; font-size: 10px;
            font-weight: bold; color: #004276; background-color: #FFF">
            <td class="HeaderGrid" style="border-left: 1px solid #E6E6E6; width: 80px; text-align: center;">
                <center>
                    <table cellspacing="0" cellpadding="0" border="0">
                        <thead>
                            <tr>
                                <td align="center" rowspan="2">
                                    <a class='SorttingLink' href="javascript:doSort2('up0','down0','0','table2sort');"
                                        onclick="javascript:setColor(this);">Mã </a>
                                </td>
                                <td align="center">
                                    <img id='up0' class="updownimg" onclick='doSort(this, "0D", "table2sort");' border="0"
                                        src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' /></td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <img id='down0' class="updownimg" onclick='doSort(this, "0A", "table2sort");' border="0"
                                        style="margin: 1px 0px 0px;" src="http://cafef3.vcmedia.vn/images/StockImages/img_dowon.gif" name='btnDown' /></td>
                            </tr>
                        </thead>
                    </table>
                </center>
            </td>
            <td class="HeaderGrid" style="width: 80px;">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <thead>
                        <tr>
                            <td align="center" rowspan="2">
                                <a style="color: Red" class='SorttingLink' href="javascript:doSort2('up1','down1','1','table2sort');"
                                    onclick="javascript:setColor(this);">Dư mua </a>
                            </td>
                            <td align="center">
                                <img id='up1' class="updownimg" onclick='doSort(this, "1D", "table2sort");' border="0"
                                    src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' /></td>
                        </tr>
                        <tr>
                            <td align="center">
                                <img id='down1' class="updownimg" onclick='doSort(this, "1A", "table2sort");' border="0"
                                    style="margin: 1px 0px 0px;" src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown' /></td>
                        </tr>
                    </thead>
                </table>
            </td>
            <td class="HeaderGrid" style="width: 80px;">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <thead>
                        <tr>
                            <td align="center" rowspan="2">
                                <a class='SorttingLink' href="javascript:doSort2('up2','down2','2','table2sort');"
                                    onclick="javascript:setColor(this);">Dư bán </a>
                            </td>
                            <td align="center">
                                <img id='up2' class="updownimg" onclick='doSort(this, "2D", "table2sort");' border="0"
                                    src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' /></td>
                        </tr>
                        <tr>
                            <td align="center">
                                <img id='down2' class="updownimg" onclick='doSort(this, "2A", "table2sort");' border="0"
                                    style="margin: 1px 0px 0px;" src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown' /></td>
                        </tr>
                    </thead>
                </table>
            </td>
            <td class="HeaderGrid" style="width: 60px;">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <thead>
                        <tr>
                            <td align="center" rowspan="2">
                                <a class='SorttingLink' href="javascript:doSort2('up3','down3','3','table2sort');"
                                    onclick="javascript:setColor(this);">Giá </a>
                            </td>
                            <td align="center" style="vertical-align: bottom;">
                                <img id='up3' class="updownimg" onclick='doSort(this, "3D", "table2sort");' border="0"
                                    src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' /></td>
                        </tr>
                        <tr>
                            <td align="center" style="vertical-align: top; padding-top: 1px;">
                                <img id='down3' class="updownimg" onclick='doSort(this, "3A", "table2sort");' border="0"
                                    style="margin: 1px 0px 0px;" src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown' /></td>
                        </tr>
                    </thead>
                </table>
            </td>
             <td class="HeaderGrid" style="width: 100px;">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <thead>
                        <tr>
                            <td align="center" rowspan="2">
                                <a class='SorttingLink' href="javascript:doSort2('up4','down4','4','table2sort');"
                                    onclick="javascript:setColor(this);">Thay đổi </a>
                            </td>
                            <td align="center" style="vertical-align: bottom;">
                                <img id='up4' class="updownimg" onclick='doSort(this, "4D", "table2sort");' border="0"
                                    src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' /></td>
                        </tr>
                        <tr>
                            <td align="center" style="vertical-align: top; padding-top: 1px;">
                                <img id='down4' class="updownimg" onclick='doSort(this, "4A", "table2sort");' border="0"
                                    style="margin: 1px 0px 0px;" src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown' /></td>
                        </tr>
                    </thead>
                </table>
            </td>
            <td class="HeaderGrid" style="width: 90px;">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <thead>
                        <tr>
                            <td align="center" rowspan="2">
                                <a class='SorttingLink' href="javascript:doSort2('up5','down5','5','table2sort');"
                                    onclick="javascript:setColor(this);">Số lệnh đặt mua </a>
                            </td>
                            <td align="center" style="vertical-align: bottom;">
                                <img id='up5' class="updownimg" onclick='doSort(this, "5D", "table2sort");' border="0"
                                    src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' /></td>
                        </tr>
                        <tr>
                            <td align="center" style="vertical-align: top; padding-top: 1px;">
                                <img id='down5' class="updownimg" onclick='doSort(this, "5A", "table2sort");' border="0"
                                    style="margin: 1px 0px 0px;" src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown' /></td>
                        </tr>
                    </thead>
                </table>
            </td>
            <td class="HeaderGrid" style="width: 90px;">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <thead>
                        <tr>
                            <td align="center" rowspan="2">
                                <a class='SorttingLink' href="javascript:doSort2('up6','down6','6','table2sort');"
                                    onclick="javascript:setColor(this);">Khối lượng đặt mua </a>
                            </td>
                            <td align="center" style="vertical-align: bottom;">
                                <img id='up6' class="updownimg" onclick='doSort(this, "6D", "table2sort");' border="0"
                                    src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' /></td>
                        </tr>
                        <tr>
                            <td align="center" style="vertical-align: top; padding-top: 1px;">
                                <img id='down6' class="updownimg" onclick='doSort(this, "6A", "table2sort");' border="0"
                                    style="margin: 1px 0px 0px;" src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown' /></td>
                        </tr>
                    </thead>
                </table>
            </td>
              <td class="HeaderGrid" style="width: 90px;">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <thead>
                        <tr>
                            <td align="center" rowspan="2">
                                <a class='SorttingLink' href="javascript:doSort2('up7','down7','7','table2sort');"
                                    onclick="javascript:setColor(this);"><i> KLTB <br /> 1 lệnh mua</i> </a>
                            </td>
                            <td align="center" style="vertical-align: bottom;">
                                <img id='up7' class="updownimg" onclick='doSort(this, "7D", "table2sort");' border="0"
                                    src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' /></td>
                        </tr>
                        <tr>
                            <td align="center" style="vertical-align: top; padding-top: 1px;">
                                <img id='down7' class="updownimg" onclick='doSort(this, "7A", "table2sort");' border="0"
                                    style="margin: 1px 0px 0px;" src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown' /></td>
                        </tr>
                    </thead>
                </table>
            </td>
            <td class="HeaderGrid" style="width: 90px;">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <thead>
                        <tr>
                            <td align="center" rowspan="2">
                                <a class='SorttingLink' href="javascript:doSort2('up8','down8','8','table2sort');"
                                    onclick="javascript:setColor(this);">Số lệnh đặt bán </a>
                            </td>
                            <td align="center" style="vertical-align: bottom;">
                                <img id='up8' class="updownimg" onclick='doSort(this, "8D", "table2sort");' border="0"
                                    src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' /></td>
                        </tr>
                        <tr>
                            <td align="center" style="vertical-align: top; padding-top: 1px;">
                                <img id='down8' class="updownimg" onclick='doSort(this, "8A", "table2sort");' border="0"
                                    style="margin: 1px 0px 0px;" src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown' /></td>
                        </tr>
                    </thead>
                </table>
            </td>
            <td class="HeaderGrid" style="width: 90px;">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <thead>
                        <tr>
                            <td align="center" rowspan="2">
                                <a class='SorttingLink' href="javascript:doSort2('up9','down9','9','table2sort');"
                                    onclick="javascript:setColor(this);">Khối lượng đặt bán </a>
                            </td>
                            <td align="center" style="vertical-align: bottom;">
                                <img id='up9' class="updownimg" onclick='doSort(this, "9D", "table2sort");' border="0"
                                    src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' /></td>
                        </tr>
                        <tr>
                            <td align="center" style="vertical-align: top; padding-top: 1px;">
                                <img id='down9' class="updownimg" onclick='doSort(this, "9A", "table2sort");' border="0"
                                    style="margin: 1px 0px 0px;" src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown' /></td>
                        </tr>
                    </thead>
                </table>
            </td>
            <td class="HeaderGrid" style="width: 90px;">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <thead>
                        <tr>
                            <td align="center" rowspan="2">
                                <a class='SorttingLink' href="javascript:doSort2('up10','down10','10','table2sort');"
                                    onclick="javascript:setColor(this);"><i>KLTB <br /> 1 lệnh bán</i> </a>
                            </td>
                            <td align="center" style="vertical-align: bottom;">
                                <img id='up10' class="updownimg" onclick='doSort(this, "10D", "table2sort");' border="0"
                                    src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' /></td>
                        </tr>
                        <tr>
                            <td align="center" style="vertical-align: top; padding-top: 1px;">
                                <img id='down10' class="updownimg" onclick='doSort(this, "10A", "table2sort");' border="0"
                                    style="margin: 1px 0px 0px;" src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown' /></td>
                        </tr>
                    </thead>
                </table>
            </td>
            <td id="col7" class="HeaderGrid" style="width: 110px;">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <thead>
                        <tr>
                            <td align="center" rowspan="2">
                                <a class='SorttingLink' href="javascript:doSort2('up11','down11','11','table2sort');"
                                    onclick="javascript:setColor(this);">Chênh lệch KL đặt mua - đặt bán </a>
                            </td>
                            <td align="center" style="vertical-align: bottom;">
                                <img id='up11' class="updownimg" onclick='doSort(this, "11D", "table2sort");' border="0"
                                    src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' /></td>
                        </tr>
                        <tr>
                            <td align="center" style="vertical-align: top; padding-top: 1px;">
                                <img id='down11' class="updownimg" onclick='doSort(this, "11A", "table2sort");' border="0"
                                    style="margin: 1px 0px 0px;" src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown' /></td>
                        </tr>
                    </thead>
                </table>
            </td>
        </tr>
    </table>
   <div id="divScroll" style="overflow-y: auto; overflow-x: hidden; width: 100%; height: 440px;border-bottom: 1px solid #E6E6E6;">
        <asp:Repeater runat="server" ID="rptData" OnItemDataBound="rptData_ItemDataBound">
            <HeaderTemplate>
                   <table id="table2sort" cellpadding="0" cellspacing="0">
                    <thead><tr style="display: none; border-bottom: solid 1px #e6e6e6; font-family: Arial; font-size: 10px;font-weight: bold; color: #004276; background-color: #FFF;">
                        <td class="HeaderGrid" style="border-left: 1px solid #E6E6E6;">&nbsp</td>
                        <td class="HeaderGrid">&nbsp</td>
                        <td class="HeaderGrid">&nbsp</td>
                        <td class="HeaderGrid">&nbsp</td>
                        <td class="HeaderGrid">&nbsp</td>
                        <td class="HeaderGrid">&nbsp</td>
                        <td class="HeaderGrid">&nbsp</td>
                        <td class="HeaderGrid">&nbsp</td>
                        <td class="HeaderGrid">&nbsp</td>
                        <td class="HeaderGrid">&nbsp</td>
                        <td class="HeaderGrid">&nbsp</td>
                        <td class="HeaderGrid">&nbsp</td></tr></thead>
            </HeaderTemplate>
            <ItemTemplate>
                <tr style="font-family: Arial; font-size: 10px; font-weight: normal; background-color: #f2f2f2">
                    <td class="CodeItem" style="text-align: center; border-left: 1px solid #E6E6E6;width:78px;"><a href="javascript:void(0);" class="symbol" rel="<%#Eval("Symbol") %>"><%#DataBinder.Eval(Container.DataItem, "Symbol")%></a></td>
                    <td class="Item" style="font-weight: bold;width:80px;" ><%# String.Format("{0:#,##0}", Convert.ToInt64(DataBinder.Eval(Container.DataItem, "BuyLeft")) )%>&nbsp;</td>
                    <td class="Item" style="font-weight: bold;width:80px;"><%# String.Format("{0:#,##0}", Convert.ToInt64(DataBinder.Eval(Container.DataItem, "SellLeft")))%>&nbsp;</td>
                    <td class="Item" style="width:60px;text-align:right;"><%#String.Format("{0:#.#0}",DataBinder.Eval(Container.DataItem, "Price"))%>&nbsp;</td>
                    <td class="Item" style="width:100px;text-align:right;"><asp:Literal runat="server" ID="ltrChangePrice"></asp:Literal>&nbsp;</td>
                    <td class="Item" style="width:90px;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "BuyOrderCount"))%>&nbsp;</td>
                    <td class="Item" style="width:90px;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "BuyVolume"))%>&nbsp;</td>
                    <td class="Item" style="width:90px;font-style:italic;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "BuyAverage"))%></td>
                    <td class="Item" style="width:90px;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "SellOrderCount"))%>&nbsp;</td>
                    <td class="Item" style="width:90px;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "SellVolume"))%>&nbsp;</td>
                    <td class="Item" style="width:90px;font-style:italic;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "SellAverage"))%> </td>
                    <td class="Item" style="padding-right: 4px;width:85px;"><asp:Literal runat="server" ID="ltrChange"></asp:Literal>&nbsp;</td></tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr style="font-family: Arial; font-size: 10px; font-weight: normal; background-color: #fff">
                    <td class="CodeItem" style="text-align: center; border-left: 1px solid #E6E6E6;width:78px;"><a href="javascript:CafeF_Library.GetCompanyInfoLink('<%#DataBinder.Eval(Container.DataItem, "Symbol")%>')"><%#DataBinder.Eval(Container.DataItem, "Symbol")%></a></td>
                    <td class="Item" style="font-weight: bold;width:80px;"><%# String.Format("{0:#,##0}", Convert.ToInt64(DataBinder.Eval(Container.DataItem, "BuyLeft")) )%>&nbsp;</td>
                    <td class="Item" style="font-weight: bold;width:80px;"><%# String.Format("{0:#,##0}", Convert.ToInt64(DataBinder.Eval(Container.DataItem, "SellLeft")))%>&nbsp;</td>
                    <td class="Item" style="width:60px;text-align:right;"><%#String.Format("{0:#.#0}",DataBinder.Eval(Container.DataItem, "Price"))%>&nbsp;</td>
                    <td class="Item" style="width:100px;text-align:right;"><asp:Literal runat="server" ID="ltrChangePrice"></asp:Literal>&nbsp;</td>
                    <td class="Item" style="width:90px;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "BuyOrderCount"))%>&nbsp;</td>
                    <td class="Item" style="width:90px;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "BuyVolume"))%>&nbsp;</td>
                    <td class="Item" style="width:90px;font-style:italic;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "BuyAverage"))%></td>
                    <td class="Item" style="width:90px;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "SellOrderCount"))%>&nbsp;</td>
                    <td class="Item" style="width:90px;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "SellVolume"))%>&nbsp;</td>
                    <td class="Item" style="width:90px;font-style:italic;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "SellAverage"))%> </td>
                    <td class="Item" style="padding-right: 4px;width:85px;"><asp:Literal runat="server" ID="ltrChange"></asp:Literal>&nbsp;</td></tr>
            </AlternatingItemTemplate>
            <FooterTemplate>
               <tfoot><tr style="display: none; border-bottom: solid 1px #e6e6e6; font-family: Arial; font-size: 10px;font-weight: bold; color: #004276; background-color: #FFF;">
                        <td class="HeaderGrid" style="border-left: 1px solid #E6E6E6;">&nbsp</td>
                        <td class="HeaderGrid">&nbsp</td>
                        <td class="HeaderGrid">&nbsp</td>
                        <td class="HeaderGrid">&nbsp</td>
                        <td class="HeaderGrid">&nbsp</td>
                        <td class="HeaderGrid">&nbsp</td>
                        <td class="HeaderGrid">&nbsp</td>
                        <td class="HeaderGrid">&nbsp</td>
                        <td class="HeaderGrid">&nbsp</td>
                        <td class="HeaderGrid">&nbsp</td>
                        <td class="HeaderGrid">&nbsp</td>
                        <td class="HeaderGrid">&nbsp</td></tr></tfoot>   
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</div>
<script type="text/javascript" language="javascript">
var sSan = document.getElementById("<%= sSan.ClientID %>");
var dtPicker = document.getElementById("<%= this.dpkTradeDate1.ClientID %>");
var dateValue = '<%= dateTextBox %>';
var sanValue = '<%= san %>';
var host = "http://<%= this.Request.Url.Authority %>";
var tab = 2;
if(window.location.href.match('UPCOM'))
{
    document.getElementById('lastCol').style.width='112px';
}
function select2() {
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

