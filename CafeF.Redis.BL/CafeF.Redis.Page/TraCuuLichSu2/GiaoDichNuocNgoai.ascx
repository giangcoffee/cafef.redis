<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GiaoDichNuocNgoai.ascx.cs" Inherits="CafeF.Redis.Page.TraCuuLichSu2.GiaoDichNuocNgoai" %>
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
            <img src="http://cafef3.vcmedia.vn/images/images/xem.gif" onclick="select2()" style="cursor: pointer" id="IMG1" />
        </td>
    </tr>
</table>
<div style="text-align: center;background-color:#FFF;border: #d3d3d3 1px solid; padding:4px;font-weight:bold;">
<center>
<asp:Repeater ID="rptHead" runat="server"><%--  OnItemDataBound="rptHead_ItemDataBound" --%>
    <ItemTemplate>
<table style="" cellpadding="6" cellspacing="0px">
    <tr>
        <td style="border: #d3d3d3 1px solid;color:#000000;"><%# DataBinder.Eval(Container.DataItem, "Symbol") %></td>
        <td style="border-top: #d3d3d3 1px solid;border-bottom: #d3d3d3 1px solid;border-right: #d3d3d3 1px solid;width:100px;">Khối lượng<br /> giao dịch&nbsp;(cp)</td>
        <td style="border-top: #d3d3d3 1px solid;border-bottom: #d3d3d3 1px solid;border-right: #d3d3d3 1px solid;color:#000000;">%KLGD/<br />Tổng KLGD</td>
        <td style="border-top: #d3d3d3 1px solid;border-bottom: #d3d3d3 1px solid;border-right: #d3d3d3 1px solid;color:#000000;width:100px;">Giá trị giao dịch&nbsp;(VNĐ)</td>
        <td style="border-top: #d3d3d3 1px solid;border-bottom: #d3d3d3 1px solid;border-right: #d3d3d3 1px solid;color:#000000;">%GTGD/<br />Tổng GTGD</td>
    </tr>
    <tr>
        <td style="border-left: #d3d3d3 1px solid;border-bottom: #d3d3d3 1px solid;border-right: #d3d3d3 1px solid;color:#000000;">Tổng mua</td>
        <td style="border-bottom: #d3d3d3 1px solid;border-right: #d3d3d3 1px solid;color:#000000;text-align:right;">
            <%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "BuyVolume"))%>
        </td>
        <td style="border-bottom: #d3d3d3 1px solid;border-right: #d3d3d3 1px solid;color:#000000;text-align:right;"><%#double.Parse(Eval("BuyVolumePercent").ToString()).ToString("#0.00")%>%
        </td>
        <td style="border-bottom: #d3d3d3 1px solid;border-right: #d3d3d3 1px solid;color:#000000;text-align:right;">
            <%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "BuyValue"))%>
        </td>
        <td style="border-bottom: #d3d3d3 1px solid;border-right: #d3d3d3 1px solid;color:#000000;text-align:right;">
            <%#double.Parse(Eval("BuyValuePercent").ToString()).ToString("#0.00") %>%
        </td>
    </tr>
    <tr>
        <td style="border-left: #d3d3d3 1px solid;border-bottom: #d3d3d3 1px solid;border-right: #d3d3d3 1px solid;color:#000000;">Tổng bán</td>
        <td style="border-bottom: #d3d3d3 1px solid;border-right: #d3d3d3 1px solid;color:#000000;text-align:right;">
            <%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "SellVolume"))%>
        </td>
        <td style="border-bottom: #d3d3d3 1px solid;border-right: #d3d3d3 1px solid;color:#000000;text-align:right;">
            <%#double.Parse(Eval("SellVolumePercent").ToString()).ToString("0.00") %>%
        </td>
        <td style="border-bottom: #d3d3d3 1px solid;border-right: #d3d3d3 1px solid;color:#000000;text-align:right;">
            <%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "SellValue"))%>
        </td>
        <td style="border-bottom: #d3d3d3 1px solid;border-right: #d3d3d3 1px solid;color:#000000;text-align:right;">
            <%#double.Parse(Eval("SellValuePercent").ToString()).ToString("0.00") %>%
        </td>
    </tr>
    <tr>
        <td style="border-left: #d3d3d3 1px solid;border-bottom: #d3d3d3 1px solid;border-right: #d3d3d3 1px solid;color:#0d3479;">Chênh lệch mua bán</td>
        <td style="border-bottom: #d3d3d3 1px solid;border-right: #d3d3d3 1px solid;color:#0d3479;text-align:right;font-size:14px;">
        <%# double.Parse(Eval("DiffVolume").ToString()) > 0 ?"+":"-" %><%# double.Parse(Eval("DiffVolume").ToString()).ToString("#,##0")%>
        </td><td style="border-bottom: #d3d3d3 1px solid;border-right: #d3d3d3 1px solid;color:#000000;">&nbsp;</td>
        <td style="border-bottom: #d3d3d3 1px solid;border-right: #d3d3d3 1px solid;color:#0d3479;text-align:right;font-size:14px;">
        <%# double.Parse(Eval("DiffValue").ToString()) > 0 ?"+":"-" %><%# double.Parse(Eval("DiffValue").ToString()).ToString("#,##0") %>
        </td>
        <td style="border-bottom: #d3d3d3 1px solid;border-right: #d3d3d3 1px solid;color:#000000;">&nbsp;</td>
    </tr>
</table>
    </ItemTemplate>
</asp:Repeater>
</center>
</div>
<table cellpadding="2" cellspacing="0" style="border-top: solid 1px #e6e6e6;border-bottom:solid 2px #e6e6e6;">
            <thead>
            <tr style="border-bottom: solid 1px #e6e6e6; font-family: Arial; font-size: 12px; font-weight: bold; color: #004276; background-color: #FFF">
                <td class="Header_DateItem" rowspan="2" style=" width: 50px">
                <center>
                    <table cellspacing="0" cellpadding="0" border="0">
                        <thead>
                            <tr>
                                <td align="center" rowspan="2">
                                <a class='SorttingLink' href="javascript:doSort2('up0','down0','0','table2sort');" onclick="javascript:setColor(this);">Mã</a>    
                                </td>
                                <td align="center">
                                    <img id='up0' class="updownimg" onclick='doSort(this, "0D", "table2sort");' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' /></td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <img id='down0' class="updownimg" onclick='doSort(this, "0A", "table2sort");' border="0" style="margin: 1px 0px 0px;" src="http://cafef3.vcmedia.vn/images/StockImages/img_dowon.gif" name='btnDown' /></td>
                            </tr>
                        </thead>
                    </table>
                    </center>
                </td>
                <td colspan="2" class="GDNN_MUABAN">Mua</td>
                <td colspan="2" class="GDNN_MUABAN">Bán</td>
                <td class="Header_Price" rowspan="2" style="width:100px;">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                        <thead>
                            <tr>
                                <td align="center" rowspan="2">
                                <a style="color:Red;" class='SorttingLink' href="javascript:doSort2('up5','down5','5','table2sort');" onclick="javascript:setColor(this);">
                                    Khối lượng giao dịch ròng</a></td>
                                <td align="center" valign="bottom">
                                    <img id='up5' class="updownimg" onclick='doSort(this, "5D", "table2sort");' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' /></td>
                            </tr>
                            <tr>
                                <td align="center" valign="top" style="padding-top:1px;">
                                    <img id='down5' class="updownimg" onclick='doSort(this, "5A", "table2sort");' border="0" style="margin: 1px 0px 0px;"
                                        src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown' /></td>
                            </tr>
                        </thead>
                    </table>
                    </td>
                <td class="Header_Price" rowspan="2" style="width:105px;">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                        <thead>
                            <tr>
                                <td align="center" rowspan="2">
                                <a class='SorttingLink' href="javascript:doSort2('up6','down6','6','table2sort');" onclick="javascript:setColor(this);">
                                    Giá trị giao dịch ròng</a></td>
                                <td align="center" valign="bottom">
                                    <img id='up6' class="updownimg" onclick='doSort(this, "6D", "table2sort");' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' /></td>
                            </tr>
                            <tr>
                                <td align="center" valign="top" style="padding-top:1px;">
                                    <img id='down6' class="updownimg" onclick='doSort(this, "6A", "table2sort");' border="0" style="margin: 1px 0px 0px;"
                                        src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown' /></td>
                            </tr>
                        </thead>
                    </table>
                    </td>
                <td class="Header_Price" rowspan="2" style="width:104px;">
                 <table width="100%" cellspacing="0" cellpadding="0" border="0">
                        <thead>
                            <tr>
                                <td align="center" rowspan="2">
                                <a class='SorttingLink' href="javascript:doSort2('up7','down7','7','table2sort');" onclick="javascript:setColor(this);">
                                   Room còn lại</a></td>
                                <td align="center">
                                    <img id='up7' class="updownimg" onclick='doSort(this, "7D", "table2sort");' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' /></td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <img id='down7' class="updownimg" onclick='doSort(this, "7A", "table2sort");' border="0" style="margin: 1px 0px 0px;" src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown' /></td>
                            </tr>
                        </thead>
                    </table>
                    </td>
                <td id="col7" class="Header_Price" rowspan="2" style="width:115px;">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                        <thead>
                            <tr>
                                <td align="center" rowspan="2">
                                <a class='SorttingLink' href="javascript:doSort2('up8','down8','8','table2sort');" onclick="javascript:setColor(this);">
                                   Đang sở hữu</a></td>
                                <td align="center" style="vertical-align:bottom;">
                                    <img id='up8' class="updownimg" onclick='doSort(this, "8D", "table2sort");' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' /></td>
                            </tr>
                            <tr>
                                <td align="center" style="vertical-align:top; padding-top:1px;">
                                    <img id='down8' class="updownimg" onclick='doSort(this, "8A", "table2sort");' border="0" style="margin: 1px 0px 0px;"
                                        src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown' /></td>
                            </tr>
                        </thead>
                    </table>
              </td>                    
            </tr>
            <tr>
                <td class="GDNN_Header" style="font-size:12px;font-family:arial;width:120px; background-color:#FFF;">
                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                        <thead>
                            <tr>
                                <td align="center" rowspan="2">
                                <a class='SorttingLink' href="javascript:doSort2('up1','down1','1','table2sort');" onclick="javascript:setColor(this);">
                                    Khối lượng</a></td>
                                <td align="center">
                                    <img id='up1' class="updownimg" onclick='doSort(this, "1D", "table2sort");' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' /></td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <img id='down1' class="updownimg" onclick='doSort(this, "1A", "table2sort");' border="0" style="margin: 1px 0px 0px;"
                                        src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown' /></td>
                            </tr>
                        </thead>
                    </table>
                </td>
                <td class="GDNN_Header" style="font-size:12px;font-family:arial;width:120px;background-color:#FFF;">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                        <thead>
                            <tr>
                                <td align="center" rowspan="2">
                                <a class='SorttingLink' href="javascript:doSort2('up2','down2','2','table2sort');" onclick="javascript:setColor(this);">Giá trị</a></td>
                                <td align="center">
                                    <img id='up2' class="updownimg" onclick='doSort(this, "2D", "table2sort");' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' /></td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <img id='down2' class="updownimg" onclick='doSort(this, "2A", "table2sort");' border="0" style="margin: 1px 0px 0px;" src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown' /></td>
                            </tr>
                        </thead>
                    </table>
                </td>
                <td class="GDNN_Header" style="font-size:12px;font-family:arial;width:120px;;background-color:#FFF;">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                        <thead>
                            <tr>
                                <td align="center" rowspan="2">
                                <a class='SorttingLink' href="javascript:doSort2('up3','down3','3','table2sort');" onclick="javascript:setColor(this);">
                                    Khối lượng</a></td>
                                <td align="center">
                                    <img id='up3' class="updownimg" onclick='doSort(this, "3D", "table2sort");' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' /></td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <img id='down3' class="updownimg" onclick='doSort(this, "3A", "table2sort");' border="0" style="margin: 1px 0px 0px;"
                                        src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown' /></td>
                            </tr>
                        </thead>
                    </table>
                </td>
                <td class="GDNN_Header" style="font-size:12px;font-family:arial;width:120px;;background-color:#FFF;">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                        <thead>
                            <tr>
                                <td align="center" rowspan="2">
                                <a class='SorttingLink' href="javascript:doSort2('up4','down4','4','table2sort');" onclick="javascript:setColor(this);">
                                    Giá trị</a></td>
                                <td align="center">
                                    <img id='up4' class="updownimg" onclick='doSort(this, "4D", "table2sort");' border="0" src="http://cafef3.vcmedia.vn/images/StockImages/img_upoff.gif" name='btnUp' /></td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <img id='down4' class="updownimg" onclick='doSort(this, "4A", "table2sort");' border="0" style="margin: 1px 0px 0px;"
                                        src="http://cafef3.vcmedia.vn/images/StockImages/img_dow.gif" name='btnDown' /></td>
                            </tr>
                        </thead>
                    </table>
                </td>
            </tr>
            </thead>
</table>
<div id="divScroll" style="overflow-y: auto;overflow-x: hidden; width:100%; height: 440px;border-bottom:1px solid #E6E6E6; background-color: #ffffff;">
<asp:Repeater runat="server" ID="rptData" OnItemDataBound="rptData_ItemDataBound">
    <HeaderTemplate>
<table id="table2sort" cellpadding="2" cellspacing="0" style="border-top: solid 1px #e6e6e6; background-color:#FFF" class="GirdTable">
            <thead><tr style="display:none;"><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr></thead>
            <tfoot>
             <tr style="border-bottom: solid 1px #e6e6e6; font-family: Arial; font-size: 12px; font-weight: bold; color: #004276; background-color: #FFF; visibility:hidden;">
                    <td rowspan="2" style="width: 52px;"></td>
                    <td colspan="2"></td>
                    <td colspan="2"></td>
                    <td rowspan="2" style="width:101px;"></td>
                    <td rowspan="2" style="width:106px;"></td>
                    <td rowspan="2" style="width:105px;"></td>
                    <td rowspan="2" style="width:100px;" id="lastCol"></td>
                </tr>        
                <tr style="visibility:hidden;" >
                    <td style="width:121px;"></td>
                    <td style="width:121px;"></td>
                    <td style="width:121px;"></td>
                    <td style="width:121px;"></td>
                </tr>
            </tfoot>
    </HeaderTemplate>
    <ItemTemplate>
        <tr style="font-family: Arial; font-size: 10px; font-weight: normal; background-color: #fff" runat="server" id="itemTR">
            <td class="CodeItem"><a href='javascript:void(0);' class="symbol" rel="<%# Eval("Symbol") %>"><%#DataBinder.Eval(Container.DataItem, "Symbol")%></a></td>            
            <td class="Item"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "BuyVolume"))%>&nbsp;</td>
            <td class="Item"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "BuyValue"))%>&nbsp;</td>
            <td class="Item"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "SellVolume"))%>&nbsp;</td>
            <td class="Item"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "SellValue"))%>&nbsp;</td>
            <td class="Item" style="font-weight:bold;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "NetVolume"))%>&nbsp;</td>
            <td class="Item" style="font-weight:bold;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "NetValue"))%>&nbsp;</td>
            <td class="Item"><asp:Literal runat="server" ID="ltrROOM_CONLAI"></asp:Literal>&nbsp;</td>
            <td class="Item"><asp:Literal runat="server" ID="ltrSOHUU"></asp:Literal>&nbsp;</td>
        </tr>
    </ItemTemplate>
    <AlternatingItemTemplate>
        <tr style="font-family: Arial; font-size: 10px; font-weight: normal; background-color: #f2f2f2" runat="server" id="itemTR">
           <td class="CodeItem"><a href='javascript:void(0);' class="symbol" rel="<%# Eval("Symbol") %>"><%#DataBinder.Eval(Container.DataItem, "Symbol")%></a></td>            
            <td class="Item"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "BuyVolume"))%>&nbsp;</td>
            <td class="Item"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "BuyValue"))%>&nbsp;</td>
            <td class="Item"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "SellVolume"))%>&nbsp;</td>
            <td class="Item"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "SellValue"))%>&nbsp;</td>
            <td class="Item" style="font-weight:bold;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "NetVolume"))%>&nbsp;</td>
            <td class="Item" style="font-weight:bold;"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "NetValue"))%>&nbsp;</td>
            <td class="Item"><asp:Literal runat="server" ID="ltrROOM_CONLAI"></asp:Literal>&nbsp;</td>
            <td class="Item"><asp:Literal runat="server" ID="ltrSOHUU"></asp:Literal>&nbsp;</td>
        </tr>
    </AlternatingItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>
<script language="javascript" type="text/javascript">
    var sSan = document.getElementById("<%= sSan.ClientID %>");
    var dtPicker = document.getElementById("<%= this.dpkTradeDate1.ClientID %>");
    var dateValue = '<%= dateTextBox %>';
    var sanValue = '<%= this.san %>';
    var host = "http://<%= this.Request.Url.Authority %>";
    var tab = 3;
    
    if(window.location.href.match('UPCOM'))
    {
        document.getElementById('lastCol').style.width='117px';
        document.getElementById('col7').style.width = '115px';
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
<script language="javascript" type="text/javascript" src="http://cafef3.vcmedia.vn/TraCuuLichSu/js/js.js">
</script>
</div>
