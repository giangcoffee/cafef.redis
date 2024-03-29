<%@ Control Language="C#" AutoEventWireup="true" Codebehind="ucThongKeAjax.ascx.cs"
    Inherits="CafeF.ThongKe.ucThongKeAjax" %>
<%@ Register Src="/UserControl/DatePicker/DatePicker.ascx" TagName="DatePicker"
    TagPrefix="uc1" %>
<link href="http://cafef3.vcmedia.vn/TraCuuLichSu/js/css.css" rel="stylesheet"
    type="text/css" />
<table cellpadding="2" cellspacing="2" class="SearchDataHistory_SearchForm" width="100%">
    <tr id="trDate" style="display: none">
        <td>
            Từ ngày&nbsp;<uc1:DatePicker ID="dpkTradeDate1" runat="server" />&nbsp;&nbsp;&nbsp;Đến
            ngày&nbsp;<uc1:DatePicker ID="dpkTradeDate2" runat="server" /></td>
        <td align="left">
            <img src="http://cafef3.vcmedia.vn/images/images/xem.gif" onclick="SelectDate();" style="cursor: pointer"
                id="IMG3" />
        </td>
    </tr>
    <tr>
        <td align="center" colspan="2">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 150px">
                        Sàn
                        <select id="sSan" runat="server" onchange="select();" style="width: 100px">
                            <option value="HASTC">HASTC</option>
                            <option value="HOSE" selected>HOSE</option>
                            <option value="UPCOM">UPCOM</option>
                        </select>
                    </td>
                    <td align="center">
                        <div id="divLoading" style="padding: 5px; font-weight: bold; color: #024174;">
                            Đang tải dữ liệu
                            <img height='8' alt='Loading ...' src='/images/screener_loading_text.gif'>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table cellpadding="2" cellspacing="0" style="border-top: solid 1px #e6e6e6; border-bottom: solid 2px #e6e6e6;"
    id="tblGridData">
    <tr style="border-bottom: solid 1px #e6e6e6; font-family: Arial; font-size: 10px;
        font-weight: bold; color: #004276; background-color: #FFF; height: 40px">
        <td class="Header_DateItem_lsg" style="width: 54px;">
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <thead>
                    <tr>
                        <td align="right" rowspan="2">
                            <a class='SorttingLink' href="javascript:doSort2('up0','down0','0','table2sort');"
                                onclick="javascript:setColor(this);">Mã </a>
                        </td>
                        <td align="center" style="vertical-align: bottom;">
                            <img id='up0' border="0" src="/images/StockImages/img_upoff.gif" name='btnUp' onclick='doSort(this, "0D", "table2sort");'
                                class="updownimg" style="visibility: hidden" /></td>
                    </tr>
                    <tr>
                        <td align="center" style="vertical-align: top; padding-top: 1px;">
                            <img id='down0' border="0" src="/images/StockImages/img_dowon.gif" name='btnDown'
                                style="margin: 1px 0px 0px; visibility: hidden" onclick='doSort(this, "0A", "table2sort");'
                                class="updownimg" /></td>
                    </tr>
                </thead>
            </table>
        </td>
        <td class="Header_Price1" style="width: 86px">
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <thead>
                    <tr>
                        <td align="center" rowspan="2">
                            <a class='SorttingLink' href="javascript:doSort2('up1','down1','3','table2sort');"
                                onclick="javascript:setColor(this);">Giá hiện tại</a>
                        </td>
                        <td align="center" style="vertical-align: bottom;">
                            <img id='up1' border="0" src="/images/StockImages/img_upoff.gif" name='btnUp' onclick='doSort(this, "1D", "table2sort");'
                                class="updownimg" style="visibility: hidden" /></td>
                    </tr>
                    <tr>
                        <td align="center" style="vertical-align: top; padding-top: 1px;">
                            <img id='down1' border="0" src="/images/StockImages/img_dow.gif" name='btnDown' style="margin: 1px 0px 0px;
                                visibility: hidden" onclick='doSort(this, "1A", "table2sort");' class="updownimg" /></td>
                    </tr>
                </thead>
            </table>
        </td>
        <td class="Header_Price1" style="width: 76px; border-right: solid 3px #e6e6e6;">
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <thead>
                    <tr>
                        <td align="center" rowspan="2">
                            <a style="color: Red;" class='SorttingLink' href="javascript:doSort2('up2','down2','4','table2sort');"
                                onclick="javascript:setColor(this);">
                                <asp:Literal ID="ltrPhienChange" runat="server"></asp:Literal></a>
                        </td>
                        <td align="center" style="vertical-align: bottom;">
                            <img id='up2' border="0" src="/images/StockImages/img_upoff.gif" name='btnUp' onclick='doSort(this, "2D", "table2sort");'
                                class="updownimg" style="visibility: hidden" /></td>
                    </tr>
                    <tr>
                        <td align="center" style="vertical-align: top; padding-top: 1px;">
                            <img id='down2' border="0" src="/images/StockImages/img_dow.gif" name='btnDown' style="margin: 1px 0px 0px;
                                visibility: hidden" onclick='doSort(this, "2A", "table2sort");' class="updownimg" /></td>
                    </tr>
                </thead>
            </table>
        </td>
        <td class="Header_Price1" style="border-right: solid 3px #e6e6e6;">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td colspan="7" align="center" style="padding-bottom: 5px; border-bottom: solid 1px #E6E6E6">
                        <span style="font-size: 13px; font-weight: bold; color: Blue">Chi tiết KLGD khớp lệnh
                            gần đây</span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 63px; padding-top: 5px; border-right: solid 1px #E6E6E6">
                        <table width="100%" cellspacing="0" cellpadding="0" border="0">
                            <thead>
                                <tr>
                                    <td align="center" rowspan="2">
                                        <span style="color: Black;">TB 20 phiên trước </span>
                                    </td>
                                    <td align="center" style="vertical-align: bottom;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="vertical-align: top; padding-top: 1px;">
                                    </td>
                                </tr>
                            </thead>
                        </table>
                    </td>
                    <td style="width: 65px; padding-top: 5px; border-right: solid 1px #E6E6E6">
                        <table width="100%" cellspacing="0" cellpadding="0" border="0">
                            <thead>
                                <tr>
                                    <td align="center" rowspan="2">
                                        <span style="color: Black;">TB 5 phiên trước </span>
                                    </td>
                                    <td align="center" style="vertical-align: bottom;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="vertical-align: top; padding-top: 1px;">
                                    </td>
                                </tr>
                            </thead>
                        </table>
                    </td>
                    <td style="width: 60px; border-right: solid 1px #E6E6E6">
                        <table width="100%" cellspacing="0" cellpadding="0" border="0">
                            <thead>
                                <tr>
                                    <td align="center" rowspan="2">
                                        <span style="color: Black;">Ngày<br />
                                            <asp:Literal ID="ltrPhien5" runat="server"></asp:Literal></span>
                                    </td>
                                    <td align="center" style="vertical-align: bottom;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="vertical-align: top; padding-top: 1px;">
                                    </td>
                                </tr>
                            </thead>
                        </table>
                    </td>
                    <td style="width: 60px; border-right: solid 1px #E6E6E6">
                        <table width="100%" cellspacing="0" cellpadding="0" border="0">
                            <thead>
                                <tr>
                                    <td align="center" rowspan="2">
                                        <span style="color: Black;">Ngày<br />
                                            <asp:Literal ID="ltrPhien4" runat="server"></asp:Literal></span>
                                    </td>
                                    <td align="center" style="vertical-align: bottom;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="vertical-align: top; padding-top: 1px;">
                                    </td>
                                </tr>
                            </thead>
                        </table>
                    </td>
                    <td style="width: 60px; border-right: solid 1px #E6E6E6">
                        <table width="100%" cellspacing="0" cellpadding="0" border="0">
                            <thead>
                                <tr>
                                    <td align="center" rowspan="2">
                                        <span style="color: Black;">Ngày<br />
                                            <asp:Literal ID="ltrPhien3" runat="server"></asp:Literal></span>
                                    </td>
                                    <td align="center" style="vertical-align: bottom;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="vertical-align: top; padding-top: 1px;">
                                    </td>
                                </tr>
                            </thead>
                        </table>
                    </td>
                    <td style="width: 60px; border-right: solid 1px #E6E6E6">
                        <table width="100%" cellspacing="0" cellpadding="0" border="0">
                            <thead>
                                <tr>
                                    <td align="center" rowspan="2">
                                        <span style="color: Black;">Ngày<br />
                                            <asp:Literal ID="ltrPhien2" runat="server"></asp:Literal></span>
                                    </td>
                                    <td align="center" style="vertical-align: bottom;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="vertical-align: top; padding-top: 1px;">
                                    </td>
                                </tr>
                            </thead>
                        </table>
                    </td>
                    <td style="width: 58px;">
                        <table width="100%" cellspacing="0" cellpadding="0" border="0">
                            <thead>
                                <tr>
                                    <td align="center" rowspan="2">
                                        <span style="color: Black; font-weight: bold">Hôm nay<br />
                                            <asp:Literal ID="ltrPhien1" runat="server"></asp:Literal></span>
                                    </td>
                                    <td align="center" style="vertical-align: bottom;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="vertical-align: top; padding-top: 1px;">
                                    </td>
                                </tr>
                            </thead>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
        <td align="center" class="Header_Price1" style="border-right: solid 3px #e6e6e6;">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td colspan="3" style="padding-bottom: 5px; border-bottom: solid 1px #E6E6E6" align="center">
                        <span style="color: Blue; font-weight: bold; font-size: 13px">So sánh KLGD hôm nay</span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 53px; border-right: solid 1px #E6E6E6; padding-top: 5px">
                        <table width="100%" cellspacing="0" cellpadding="0" border="0">
                            <thead>
                                <tr>
                                    <td align="center" rowspan="2">
                                        <a class='SorttingLink' href="javascript:doSort2('up12','down12','12','table2sort');"
                                            onclick="javascript:setColor(this);">với phiên trước</a>
                                    </td>
                                    <td align="center" style="vertical-align: bottom;">
                                        <img id='up12' border="0" src="/images/StockImages/img_upoff.gif" name='btnUp' onclick='doSort(this, "12D", "table2sort");'
                                            class="updownimg" style="visibility: hidden" /></td>
                                </tr>
                                <tr>
                                    <td align="center" style="vertical-align: top; padding-top: 1px;">
                                        <img id='down12' border="0" src="/images/StockImages/img_dow.gif" name='btnDown'
                                            style="margin: 1px 0px 0px; visibility: hidden" onclick='doSort(this, "12A", "table2sort");'
                                            class="updownimg" /></td>
                                </tr>
                            </thead>
                        </table>
                    </td>
                    <td style="width: 55px; border-right: solid 1px #E6E6E6; padding-top: 5px">
                        <table width="100%" cellspacing="0" cellpadding="0" border="0">
                            <thead>
                                <tr>
                                    <td align="center" rowspan="2">
                                        <a class='SorttingLink' href="javascript:doSort2('up13','down13','13','table2sort');"
                                            onclick="javascript:setColor(this);">với TB 5 phiên trước</a>
                                    </td>
                                    <td align="center" style="vertical-align: bottom;">
                                        <img id='up13' border="0" src="/images/StockImages/img_upoff.gif" name='btnUp' onclick='doSort(this, "13D", "table2sort");'
                                            class="updownimg" style="visibility: hidden" /></td>
                                </tr>
                                <tr>
                                    <td align="center" style="vertical-align: top; padding-top: 1px;">
                                        <img id='down13' border="0" src="/images/StockImages/img_dow.gif" name='btnDown'
                                            style="margin: 1px 0px 0px; visibility: hidden" onclick='doSort(this, "13A", "table2sort");'
                                            class="updownimg" /></td>
                                </tr>
                            </thead>
                        </table>
                    </td>
                    <td style="width: 60px; padding-top: 5px">
                        <table width="100%" cellspacing="0" cellpadding="0" border="0">
                            <thead>
                                <tr>
                                    <td align="center" rowspan="2">
                                        <a class='SorttingLink' href="javascript:doSort2('up14','down14','14','table2sort');"
                                            onclick="javascript:setColor(this);">với TB 20 phiên trước</a>
                                    </td>
                                    <td align="center" style="vertical-align: bottom;">
                                        <img id='up14' border="0" src="/images/StockImages/img_upoff.gif" name='btnUp' onclick='doSort(this, "14D", "table2sort");'
                                            class="updownimg" style="visibility: hidden" /></td>
                                </tr>
                                <tr>
                                    <td align="center" style="vertical-align: top; padding-top: 1px;">
                                        <img id='down14' border="0" src="/images/StockImages/img_dow.gif" name='btnDown'
                                            style="margin: 1px 0px 0px; visibility: hidden" onclick='doSort(this, "13A", "table2sort");'
                                            class="updownimg" /></td>
                                </tr>
                            </thead>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
        <td class="Header_Price1" style="width: 76px">
            <span style="color: Blue; font-weight: bold; font-size: 13px">Biểu đồ đặt lệnh</span>
        </td>
    </tr>
</table>
<div id='divScroll' style="overflow-y: scroll; overflow-x: scroll; height: 380px;
    width: 100%; text-align: left;">
    <table id="table2sort" cellpadding="0" cellspacing="0" style="border-top: solid 1px #e6e6e6;">
        <thead>
            <tr style="display: none;" id="trTop">
                <td>
                    &nbsp</td>
                <td>
                    &nbsp</td>
                <td>
                    &nbsp</td>
                <td>
                    &nbsp</td>
                <td>
                    &nbsp</td>
                <td>
                    &nbsp</td>
                <td>
                    &nbsp</td>
                <td>
                    &nbsp</td>
                <td>
                    &nbsp</td>
                <td>
                    &nbsp</td>
                <td>
                    &nbsp
                </td>
                <td>
                    &nbsp
                </td>
                <td>
                    &nbsp
                </td>
                <td>
                </td>
            </tr>
        </thead>
        <tbody id="tbody">
            <asp:Repeater runat="server" ID="repData">
                <ItemTemplate>
                    <tr style="font-family: Arial; font-size: 10px; font-weight: normal; background-color: #FFF;
                        height: 30px" runat="server" id="itemTR">
                        <td class="Item_DateItem_lsg" style="width: 50px">
                            <a href='<%# Eval("StockLink")%>' target="_blank" onmouseover="ShowBox(event, 'divChart_<%# Eval("Symbol") %>', 100,0,'<%# ReturnImgChart(Eval("Symbol").ToString()) %>','imgChart_<%# Eval("Symbol") %>','<%# Eval("Symbol") %>');">
                                <%#Eval("Symbol")%>
                            </a>
                            <div id='divChart_<%# Eval("Symbol") %>'
                                style="width: 270px; height: 280px; text-align: center; position: absolute; z-index: 10000;
                                background-color: #ffffff; border: solid 2px #77b143; padding: 3px; display: none;
                                -moz-opacity: 0.9; opacity: 0.9; filter: alpha(opacity=90);">
                                <img id="imgChart_<%# Eval("Symbol") %>" border="0" /><br />
                                <div class="Note" style="width:100%; float:left">
                                    <div style="overflow: hidden; float: right;">
                                        Đơn vị KL: 10,000 CP</div>
                                    <div style="overflow: hidden; float: left;">
                                        Đơn vị: 1,000 VND</div>
                                </div>
                                <div style="text-align: center; width: 100%;" class="CompanyVolumeChart">
                                    <a onclick="ChangePic('<%# Eval("Symbol") %>','imgChart_<%# Eval("Symbol") %>',1);" href="javascript:void(0)" id="lnkChart_<%# Eval("Symbol") %>_7days" class="Chart_InActived">1 tuần</a> | 
                                    <a onclick="ChangePic('<%# Eval("Symbol") %>','imgChart_<%# Eval("Symbol") %>',2);" href="javascript:void(0)" id="lnkChart_<%# Eval("Symbol") %>_1month" class="Chart_Actived">1 tháng</a> | 
                                    <a onclick="ChangePic('<%# Eval("Symbol") %>','imgChart_<%# Eval("Symbol") %>',3);" href="javascript:void(0)" id="lnkChart_<%# Eval("Symbol") %>_3months" class="Chart_InActived">3 tháng</a> | 
                                    <a onclick="ChangePic('<%# Eval("Symbol") %>','imgChart_<%# Eval("Symbol") %>',4);" href="javascript:void(0)" id="lnkChart_<%# Eval("Symbol") %>_6months" class="Chart_InActived">6 tháng</a> | 
                                    <a onclick="ChangePic('<%# Eval("Symbol") %>','imgChart_<%# Eval("Symbol") %>',5);" href="javascript:void(0)" id="lnkChart_<%# Eval("Symbol") %>_1year" class="Chart_InActived">1 năm</a>                                    
                                </div>
                                <div class="Note" style="font-weight: bold; color: #666666; text-align: center;" id="divNote_<%# Eval("Symbol") %>">
                                </div>
                                <div style="float:left; width:100%;"><a style="text-decoration:none; color:Blue"  href="javascript:HideBox('divChart_<%# Eval("Symbol") %>');">Đóng</a></div>
                            </div>
                        </td>
                        <td class="Item_Price1" style="width: 90px">
                            <%# Eval("PriceChange") %>
                            &nbsp;
                        </td>
                        <td class="Item_Price1" style="width: 80px; border-right: solid 3px #e6e6e6;" align="center">
                            <b style="color: <%# ReturnColorPriceChange(Eval("5PhienOrder").ToString()) %>">
                                <%# FormatValue(String.Format("{0:#,##0.#0}", Eval("5PhienOrder")))%>
                                % </b>&nbsp;
                        </td>
                        <td style="display: none">
                            <%# CafeF.BO.NewsHelper.ConvertToDouble(Eval("CurrentPriceOrder"))%>
                        </td>
                        <td style="display: none">
                            <%# CafeF.BO.NewsHelper.ConvertToDouble(Eval("5PhienOrder"))%>
                        </td>
                        <td class="Item_Price1" style="width: 65px; font-style: italic; font-size: 10px;"
                            align="right">
                            <%# FormatValue(String.Format("{0:#,##0}",Eval("KLGDTB1Thang")))%>
                            &nbsp;
                        </td>
                        <td class="Item_Price1" style="width: 65px; font-style: italic; font-size: 10px"
                            align="right">
                            <%# FormatValue(String.Format("{0:#,##0}",Eval("KLGDTB5Phien")))%>
                            &nbsp;
                        </td>
                        <td class="Item_Price1" style="width: 60px; font-style: italic; font-size: 10px"
                            align="right">
                            <%# FormatValue(String.Format("{0:#,##0}",Eval("Phien5")))%>
                            &nbsp;
                        </td>
                        <td class="Item_Price1" style="width: 60px; font-style: italic; font-size: 10px"
                            align="right">
                            <%# FormatValue(String.Format("{0:#,##0}", Eval("Phien4")))%>
                            &nbsp;
                        </td>
                        <td class="Item_Price1" style="width: 60px; font-style: italic; font-size: 10px"
                            align="right">
                            <%# FormatValue(String.Format("{0:#,##0}", Eval("Phien3")))%>
                            &nbsp;
                        </td>
                        <td class="Item_Price1" style="width: 60px; font-style: italic; font-size: 10px"
                            align="right">
                            <%# FormatValue(String.Format("{0:#,##0}", Eval("Phien2")))%>
                            &nbsp;
                        </td>
                        <td class="Item_Price1" style="width: 60px; font-style: italic; font-size: 10px;
                            border-right: solid 3px #e6e6e6;" align="right">
                            <%# FormatValue(String.Format("{0:#,##0}", Eval("Phien1")))%>
                            &nbsp;
                        </td>
                        <td class="Item_Price1" style="width: 55px; font-weight: bold" align="right">
                            <%# FormatValue(ReturnKLGDChange(Eval("Phien1").ToString(), Eval("Phien2").ToString()))%>
                            &nbsp;
                        </td>
                        <td class="Item_Price1" style="width: 55px; font-weight: bold" align="right">
                            <%# FormatValue(ReturnKLGDChange(Eval("Phien1").ToString(), Eval("KLGDTB5Phien").ToString()))%>
                            &nbsp;
                        </td>
                        <td class="Item_Price1" style="width: 62px; font-weight: bold; border-right: solid 3px #e6e6e6;"
                            align="right">
                            <%# FormatValue(ReturnKLGDChange(Eval("Phien1").ToString(), Eval("KLGDTB1Thang").ToString()))%>
                            &nbsp;
                        </td>
                        <td class="LastItem_Price" style="width: 80px;" align="center">
                            <%# ReturnChart(CafeF.BO.NewsHelper.ConvertToDouble(Eval("Chart1")),CafeF.BO.NewsHelper.ConvertToDouble(Eval("Chart2")),CafeF.BO.NewsHelper.ConvertToDouble(Eval("Chart3")),CafeF.BO.NewsHelper.ConvertToDouble(Eval("Chart4")),CafeF.BO.NewsHelper.ConvertToDouble(Eval("Chart5")))%>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr style="font-family: Arial; font-weight: normal; background-color: #f2f2f2; height: 30px"
                        runat="server" id="altitemTR">
                        <td class="Item_DateItem_lsg" style="width: 40px">
                            <a href='<%# Eval("StockLink")%>' target="_blank" onmouseover="ShowBox(event, 'divChart_<%# Eval("Symbol") %>', 100,0,'<%# ReturnImgChart(Eval("Symbol").ToString()) %>','imgChart_<%# Eval("Symbol") %>','<%# Eval("Symbol") %>');">
                                <%#Eval("Symbol")%>
                            </a>
                            <div id='divChart_<%# Eval("Symbol") %>'
                                style="width: 270px; height: 280px; text-align: center; position: absolute; z-index: 10000;
                                background-color: #ffffff; border: solid 2px #77b143; padding: 3px; display: none;
                                -moz-opacity: 0.9; opacity: 0.9; filter: alpha(opacity=90);">
                                <img id="imgChart_<%# Eval("Symbol") %>" border="0" /><br />
                                <div class="Note" style="float:left; width:100%">
                                    <div style="overflow: hidden; float: right;">
                                        Đơn vị KL: 10,000 CP</div>
                                    <div style="overflow: hidden; float: left;">
                                        Đơn vị: 1,000 VND</div>
                                </div>
                                 <div style="text-align: center; width: 100%;" class="CompanyVolumeChart">
                                    <a onclick="ChangePic('<%# Eval("Symbol") %>','imgChart_<%# Eval("Symbol") %>',1);" href="javascript:void(0)" id="lnkChart_<%# Eval("Symbol") %>_7days" class="Chart_InActived">1 tuần</a> | 
                                    <a onclick="ChangePic('<%# Eval("Symbol") %>','imgChart_<%# Eval("Symbol") %>',2);" href="javascript:void(0)" id="lnkChart_<%# Eval("Symbol") %>_1month" class="Chart_Actived">1 tháng</a> | 
                                    <a onclick="ChangePic('<%# Eval("Symbol") %>','imgChart_<%# Eval("Symbol") %>',3);" href="javascript:void(0)" id="lnkChart_<%# Eval("Symbol") %>_3months" class="Chart_InActived">3 tháng</a> | 
                                    <a onclick="ChangePic('<%# Eval("Symbol") %>','imgChart_<%# Eval("Symbol") %>',4);" href="javascript:void(0)" id="lnkChart_<%# Eval("Symbol") %>_6months" class="Chart_InActived">6 tháng</a> | 
                                    <a onclick="ChangePic('<%# Eval("Symbol") %>','imgChart_<%# Eval("Symbol") %>',5);" href="javascript:void(0)" id="lnkChart_<%# Eval("Symbol") %>_1year" class="Chart_InActived">1 năm</a>                                    
                                </div>
                                <div class="Note" style="font-weight: bold; color: #666666; text-align: center;" id="divNote_<%# Eval("Symbol") %>">
                                </div>
                                <div style="float:left; width:100%;"><a style="text-decoration:none; color:Blue" href="javascript:HideBox('divChart_<%# Eval("Symbol") %>');">Đóng</a></div>
                            </div>
                        </td>
                        <td class="Item_Price1" style="width: 90px">
                            <%# Eval("PriceChange") %>
                            &nbsp;
                        </td>
                        <td class="Item_Price1" style="width: 80px; border-right: solid 3px #e6e6e6;" align="center">
                            <b style="color: <%# ReturnColorPriceChange(Eval("5PhienOrder").ToString()) %>">
                                <%# FormatValue(String.Format("{0:#,##0.#0}", Eval("5PhienOrder")))%>
                                % </b>&nbsp;
                        </td>
                        <td style="display: none">
                            <%# CafeF.BO.NewsHelper.ConvertToDouble(Eval("CurrentPriceOrder"))%>
                        </td>
                        <td style="display: none">
                            <%# CafeF.BO.NewsHelper.ConvertToDouble(Eval("5PhienOrder"))%>
                        </td>
                        <td class="Item_Price1" style="width: 65px; font-style: italic; font-size: 10px;"
                            align="right">
                            <%# FormatValue(String.Format("{0:#,##0}", Eval("KLGDTB1Thang")))%>
                            &nbsp;
                        </td>
                        <td class="Item_Price1" style="width: 65px; font-style: italic; font-size: 10px"
                            align="right">
                            <%# FormatValue(String.Format("{0:#,##0}", Eval("KLGDTB5Phien"))) %>
                            &nbsp;
                        </td>
                        <td class="Item_Price1" style="width: 60px; font-style: italic; font-size: 10px"
                            align="right">
                            <%# FormatValue(String.Format("{0:#,##0}", Eval("Phien5")))%>
                            &nbsp;
                        </td>
                        <td class="Item_Price1" style="width: 60px; font-style: italic; font-size: 10px"
                            align="right">
                            <%# FormatValue(String.Format("{0:#,##0}", Eval("Phien4")))%>
                            &nbsp;
                        </td>
                        <td class="Item_Price1" style="width: 60px; font-style: italic; font-size: 10px"
                            align="right">
                            <%# FormatValue(String.Format("{0:#,##0}", Eval("Phien3")))%>
                            &nbsp;
                        </td>
                        <td class="Item_Price1" style="width: 60px; font-style: italic; font-size: 10px"
                            align="right">
                            <%# FormatValue(String.Format("{0:#,##0}", Eval("Phien2")))%>
                            &nbsp;
                        </td>
                        <td class="Item_Price1" style="width: 60px; font-style: italic; font-size: 10px;
                            border-right: solid 3px #e6e6e6;" align="right">
                            <%# FormatValue(String.Format("{0:#,##0}", Eval("Phien1")))%>
                            &nbsp;
                        </td>
                        <td class="Item_Price1" align="right" style="font-weight: bold">
                            <%# FormatValue(ReturnKLGDChange(Eval("Phien1").ToString(), Eval("Phien2").ToString()))%>
                            &nbsp;
                        </td>
                        <td class="Item_Price1" align="right" style="font-weight: bold">
                            <%# FormatValue(ReturnKLGDChange(Eval("Phien1").ToString(), Eval("KLGDTB5Phien").ToString()))%>
                            &nbsp;
                        </td>
                        <td class="Item_Price1" align="right" style="font-weight: bold; border-right: solid 3px #e6e6e6;">
                            <%# FormatValue(ReturnKLGDChange(Eval("Phien1").ToString(), Eval("KLGDTB1Thang").ToString()))%>
                            &nbsp;
                        </td>
                        <td class="LastItem_Price" style="width: 80px" align="center">
                            <%# ReturnChart(CafeF.BO.NewsHelper.ConvertToDouble(Eval("Chart1")),CafeF.BO.NewsHelper.ConvertToDouble(Eval("Chart2")),CafeF.BO.NewsHelper.ConvertToDouble(Eval("Chart3")),CafeF.BO.NewsHelper.ConvertToDouble(Eval("Chart4")),CafeF.BO.NewsHelper.ConvertToDouble(Eval("Chart5")))%>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
        </tbody>
        <tfoot>
            <tr style="border-bottom: solid 1px #e6e6e6; font-family: Arial; font-size: 10px;
                font-weight: bold; color: #004276; background-color: #FFF; visibility: hidden;">
                <td class="Header_DateItem_lsg" style="width: 50px;">
                    &nbsp</td>
                <td class="Header_Price1" style="width: 90px">
                    &nbsp</td>
                <td class="Header_Price1" style="width: 80px">
                    &nbsp</td>
                <td class="Header_Price1">
                    &nbsp</td>
                <td class="Header_Price1">
                    &nbsp</td>
                <td class="Header_Price1">
                    &nbsp</td>
                <td class="Header_Price1">
                    &nbsp</td>
                <td class="Header_Price1">
                    &nbsp</td>
                <td class="Header_Price1">
                    &nbsp</td>
                <td class="Header_Price1">
                    &nbsp</td>
                <td class="Header_Price1">
                    &nbsp</td>
                <td class="Header_Price1">
                    &nbsp</td>
                <td class="Header_Price1">
                    &nbsp</td>
                <td class="Header_LastItem" style="width: 80px">
                    &nbsp</td>
            </tr>
        </tfoot>
    </table>
</div>
<div style="float: left; overflow: hidden; width: 100%; background-color: White">
    <table cellpadding="2" cellspacing="2" width="100%">
        <tr>
            <td align="left" style="padding-top: 10px">
                (*) Thay đổi giá theo thời gian được tính bằng <span style="text-decoration: underline;
                    font-weight: bold">giá đóng của điều chỉnh</span>
                <br />
                (*) Biểu đồ đặt lệnh hiển thị tỷ lệ <span style="text-decoration: underline; font-weight: bold">
                    Trung bình 1 lệnh mua / Trung bình 1 lệnh bán</span> 5 phiên gần nhất
            </td>
        </tr>
    </table>
</div>

<script>
var dtPicker1 = document.getElementById("<%= this.dpkTradeDate1.ClientID %>_txtDatePicker");
var dtPicker2 = document.getElementById("<%= this.dpkTradeDate2.ClientID %>_txtDatePicker");
var sanValue = '<%= san %>';
var tab = '<%= tab %>';
var sSan = document.getElementById("<%= sSan.ClientID %>");
var dateValue1 = '<%= dateTextBox1 %>';
var dateValue2 = '<%= dateTextBox2 %>';
var orderValue = '<%= strOrder %>';
var btTang = document.getElementById('btUp');
var btGiam = document.getElementById('btDown');
var timenow = '<%= timenow %>'; 
var countPage = '<%= strCountPage %>';
var wooYayIntervalId = 0;
function LoadTabThongKe()
{
    if(orderValue=='asc')
    {
        btTang.style.color='black';
        btGiam.style.color='red';
    }
    else
    {
        btTang.style.color='red';
        btGiam.style.color='black';
    }

    if(tab=='1')
    {
        document.getElementById('a5Phien').setAttribute('style','font-weight: bold; color: Red; font-family: Arial; text-decoration: none; font-size: 12px; margin-top: 4px;');
    }
    else if(tab=='2')
    {
        document.getElementById('a10Phien').setAttribute('style','font-weight: bold; color: Red; font-family: Arial; text-decoration: none; font-size: 12px; margin-top: 4px;');
    }
    else if(tab=='3')
    {
        document.getElementById('a1Thang').setAttribute('style','font-weight: bold; color: Red; font-family: Arial; text-decoration: none; font-size: 12px; margin-top: 4px;');
    }
    else if(tab=='5')
    {
        document.getElementById('a3Thang').setAttribute('style','font-weight: bold; color: Red; font-family: Arial; text-decoration: none; font-size: 12px; margin-top: 4px;');
    }
    else if(tab=='6')
    {
        document.getElementById('a6Thang').setAttribute('style','font-weight: bold; color: Red; font-family: Arial; text-decoration: none; font-size: 12px; margin-top: 4px;');
    }
    else if(tab=='7')
    {
        document.getElementById('a1Nam').setAttribute('style','font-weight: bold; color: Red; font-family: Arial; text-decoration: none; font-size: 12px; margin-top: 4px;');
    }
    else
    {
        document.getElementById('aTuyChon').setAttribute('style','font-weight: bold; color: Red; font-family: Arial; text-decoration: none; font-size: 12px; margin-top: 4px;');
        document.getElementById('trDate').style.display="block";
    } 
}

LoadTabThongKe();

function LoadTabTuyChon()
{
    document.getElementById('a1Thang').setAttribute('style','font-weight: normal; color: Black; font-family: Arial; text-decoration: none; font-size: 12px; margin-top: 4px;');
    document.getElementById('a10Phien').setAttribute('style','font-weight: normal; color: Black; font-family: Arial; text-decoration: none; font-size: 12px; margin-top: 4px;');
    document.getElementById('a5Phien').setAttribute('style','font-weight: normal; color: Black; font-family: Arial; text-decoration: none; font-size: 12px; margin-top: 4px;');
    document.getElementById('aTuyChon').setAttribute('style','font-weight: bold; color: Red; font-family: Arial; text-decoration: none; font-size: 12px; margin-top: 4px;');
    document.getElementById('trDate').style.display="block";
}

function Sort()
{
    var btSort = document.getElementById('btSort');
    if(btSort.value=="Tăng giá")
        btSort.value="Giảm giá";
    else
        btSort.value="Tăng giá";
        
    doSort2('up2','down2','4','table2sort');
}



		$(document).ready(function(){			    
			runit();
		});
			
var status = false;
var k=2;
var request = new XMLHttpRequest();
var content="";
	function runit()
	{			    
	    if(k<=countPage)
	    {	
	        if(tab!='4')
	        {
	            request.open('GET', '/cafef.thongke/ThongKeAjax.aspx?tab=' + tab + '&order=' + orderValue + '&san=' + sanValue + '&page=' + k + '&timenow=' + timenow, true);
	            request.onreadystatechange = function (event) { 
	            if (request.readyState == 4) 
                {           
                    content = request.responseText;   
                    $('#tbody').append(content);
                    k++;
                    runit();	  
                    
               }};request.send(null);		
            }
            else
            {
                request.open('GET', '/cafef.thongke/ThongKeAjax.aspx?tab=' + tab + '&order=' + orderValue + '&san=' + sanValue + '&page=' + k + '&date1=' + dateValue1 + '&date2=' + dateValue2, true);
	            request.onreadystatechange = function (event) { 
	            if (request.readyState == 4) 
                {           
                    content = request.responseText;   
                    $('#tbody').append(content);
                    k++;
                    runit();	  
                    
               }};request.send(null);	
            }  
		    document.getElementById("divLoading").style.display = "block";
		}
		else
		{
		    document.getElementById("divLoading").style.display="none";
		}
		
	}

var IE = document.all?true:false

// If NS -- that is, !IE -- then set up for mouse capture
if (!IE) document.captureEvents(Event.MOUSEMOVE)
document.onmousemove = getMouseXY;
var tempX = 0;
var tempY = 0;

function getMouseXY(e) {
  if (IE) { // grab the x-y pos.s if browser is IE
    tempX = event.x + document.body.scrollLeft;
    tempY = event.y + document.body.scrollTop;
  } else {  // grab the x-y pos.s if browser is NS
    tempX = e.pageX;
    tempY = e.pageY;
  }    
  if (tempX < 0){tempX = 0}
  if (tempY < 0){tempY = 0}  
  
  return true
}

			
var isShowBox = false;
    var CurrentDiv;   
    var currentStatus=0;
    var divNote;
    function ShowBox(e, boxId, leftPosition,chartType, srcImg, img, stocksymbol)
    {
        var chart = document.getElementById(boxId); 
        divNote = document.getElementById('divNote_' + stocksymbol);
        var imgChart = document.getElementById(img);
        
        imgChart.src = srcImg; 
       
        if(CurrentDiv && chart.style.display == 'none' )
        {
            CurrentDiv.style.display ='none';
            currentStatus=0;
        }
        CurrentDiv=chart;
        var screenHeight = screen.height/2;
        if(tempY>screenHeight)
            tempY = tempY/2;
        if( chart.style.display == 'none')
        {        
            chart.style.top =(tempY)+'px';
            chart.style.left =(tempX+50)+'px';
            chart.style.display = 'block';
            isShowBox = true;
        }
        currentStatus=1;        
        divNote.innerHTML = "Biểu đồ GTGD và KLGD mã " + stocksymbol +" trong 1 tháng";
    }
    function HideBox(boxId)
    {
        var chart = document.getElementById(boxId);
        var chg=10;    
        chart.style.display = 'none';
        currentStatus=0;
    }
    
    function ChangePic(stocksymbol, imgChart, type)
    {
        var imgchart = document.getElementById(imgChart);
        divNote = document.getElementById('divNote_' + stocksymbol);
        var img="";
        var alnkChart7d = document.getElementById('lnkChart_' + stocksymbol + '_7days');
        var alnkChart1m = document.getElementById('lnkChart_' + stocksymbol + '_1month');
        var alnkChart3m = document.getElementById('lnkChart_' + stocksymbol + '_3months');
        var alnkChart6m = document.getElementById('lnkChart_' + stocksymbol + '_6months');
        var alnkChart1y = document.getElementById('lnkChart_' + stocksymbol + '_1year');
        
        if(type==1)
        {
            img = "http://cafef.vn/FinanceStatementData/" + stocksymbol + "/7days.png?upd=" ;
            divNote.innerHTML = "Biểu đồ GTGD và KLGD mã " + stocksymbol + " trong 7 ngày";
            alnkChart7d.className = 'Chart_Actived';
            alnkChart1m.className = 'Chart_InActived';
            alnkChart3m.className = 'Chart_InActived';
            alnkChart6m.className = 'Chart_InActived';
            alnkChart1y.className = 'Chart_InActived';
        }
        else if(type==2)
        {
            img = "http://cafef.vn/FinanceStatementData/" + stocksymbol + "/1month.png?upd=";
            divNote.innerHTML = "Biểu đồ GTGD và KLGD mã " + stocksymbol +" trong 1 tháng";
            alnkChart7d.className = 'Chart_InActived';
            alnkChart1m.className = 'Chart_Actived';
            alnkChart3m.className = 'Chart_InActived';
            alnkChart6m.className = 'Chart_InActived';
            alnkChart1y.className = 'Chart_InActived';
        }
        else if(type==3)
        {
            img = "http://cafef.vn/FinanceStatementData/" + stocksymbol + "/3months.png?upd=" ;
            divNote.innerHTML = "Biểu đồ GTGD và KLGD mã " + stocksymbol + " trong 3 tháng";
            
            alnkChart7d.className = 'Chart_InActived';
            alnkChart1m.className = 'Chart_InActived';
            alnkChart3m.className = 'Chart_Actived';
            alnkChart6m.className = 'Chart_InActived';
            alnkChart1y.className = 'Chart_InActived';
        }
        else if(type==4)
        {
            img = "http://cafef.vn/FinanceStatementData/" + stocksymbol + "/6months.png?upd=" ;
            divNote.innerHTML = "Biểu đồ GTGD và KLGD mã " + stocksymbol +  " trong 6 tháng";
            
            alnkChart7d.className = 'Chart_InActived';
            alnkChart1m.className = 'Chart_InActived';
            alnkChart3m.className = 'Chart_InActived';
            alnkChart6m.className = 'Chart_Actived';
            alnkChart1y.className = 'Chart_InActived';
        }
        else
        {
            img = "http://cafef.vn/FinanceStatementData/" + stocksymbol + "/1year.png?upd=" ;
            divNote.innerHTML = "Biểu đồ GTGD và KLGD mã " + stocksymbol + " trong 1 năm";
            
            alnkChart7d.className = 'Chart_InActived';
            alnkChart1m.className = 'Chart_InActived';
            alnkChart3m.className = 'Chart_InActived';
            alnkChart6m.className = 'Chart_InActived';
            alnkChart1y.className = 'Chart_Actived';
        }        
        imgchart.src =img;
        
    }
	
</script>

