<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThongKeAjax.aspx.cs" Inherits="CafeF.ThongKe.ThongKeAjax" %>

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
                    <td class="Item_Price1" style="width: 80px;border-right: solid 3px #e6e6e6;" align="center">
                        <b style="color:<%# ReturnColorPriceChange(Eval("5PhienOrder").ToString()) %>">
                            <%# FormatValue(String.Format("{0:#,##0.#0}", Eval("5PhienOrder")))%>%
                        </b>&nbsp;
                    </td>
                    <td style="display: none">
                        <%# CafeF.BO.NewsHelper.ConvertToDouble(Eval("CurrentPriceOrder"))%>
                    </td>
                    <td style="display: none">
                        <%# CafeF.BO.NewsHelper.ConvertToDouble(Eval("5PhienOrder"))%>
                    </td>
                    <td class="Item_Price1" style="width: 65px; font-style: italic;font-size:10px;" align="right">
                        <%# FormatValue(String.Format("{0:#,##0}",Eval("KLGDTB1Thang")))%>
                        &nbsp;
                    </td>
                    <td class="Item_Price1" style="width: 65px; font-style: italic;font-size:10px" align="right">
                        <%# FormatValue(String.Format("{0:#,##0}",Eval("KLGDTB5Phien")))%>
                        &nbsp;
                    </td>
                    <td class="Item_Price1" style="width: 60px; font-style: italic;font-size:10px" align="right">
                        <%# FormatValue(String.Format("{0:#,##0}",Eval("Phien5")))%>
                        &nbsp;
                    </td>
                    <td class="Item_Price1" style="width: 60px; font-style: italic;font-size:10px" align="right">
                        <%# FormatValue(String.Format("{0:#,##0}", Eval("Phien4")))%>
                        &nbsp;
                    </td>
                    <td class="Item_Price1" style="width: 60px; font-style: italic;font-size:10px" align="right">
                        <%# FormatValue(String.Format("{0:#,##0}", Eval("Phien3")))%>
                        &nbsp;
                    </td>
                    <td class="Item_Price1" style="width: 60px; font-style: italic;font-size:10px" align="right">
                        <%# FormatValue(String.Format("{0:#,##0}", Eval("Phien2")))%>
                        &nbsp;
                    </td>
                    <td class="Item_Price1" style="width: 60px; font-style: italic;font-size:10px;border-right: solid 3px #e6e6e6;" align="right">
                        <%# FormatValue(String.Format("{0:#,##0}", Eval("Phien1")))%>
                        &nbsp;
                    </td>
                    <td class="Item_Price1" style="width: 55px; font-weight:bold" align="right">
                        <%# FormatValue(ReturnKLGDChange(Eval("Phien1").ToString(), Eval("Phien2").ToString()))%>
                        &nbsp;
                    </td>
                    <td class="Item_Price1" style="width: 55px; font-weight:bold" align="right">
                        <%# FormatValue(ReturnKLGDChange(Eval("Phien1").ToString(), Eval("KLGDTB5Phien").ToString()))%>
                        &nbsp;
                    </td>
                    <td class="Item_Price1" style="width: 62px; font-weight:bold; border-right: solid 3px #e6e6e6;" align="right">
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
                    <td class="Item_Price1" style="width: 80px;border-right: solid 3px #e6e6e6;" align="center">
                        <b style="color:<%# ReturnColorPriceChange(Eval("5PhienOrder").ToString()) %>">
                            <%# FormatValue(String.Format("{0:#,##0.#0}", Eval("5PhienOrder")))%>%
                        </b>&nbsp;
                    </td>
                    <td style="display: none">
                        <%# CafeF.BO.NewsHelper.ConvertToDouble(Eval("CurrentPriceOrder"))%>
                    </td>
                    <td style="display: none">
                        <%# CafeF.BO.NewsHelper.ConvertToDouble(Eval("5PhienOrder"))%>
                    </td>
                    <td class="Item_Price1" style="width: 65px; font-style: italic;font-size:10px;" align="right">
                        <%# FormatValue(String.Format("{0:#,##0}", Eval("KLGDTB1Thang")))%>
                        &nbsp;
                    </td>
                    <td class="Item_Price1" style="width: 65px; font-style: italic;font-size:10px" align="right">
                        <%# FormatValue(String.Format("{0:#,##0}", Eval("KLGDTB5Phien"))) %>
                        &nbsp;
                    </td>
                    <td class="Item_Price1" style="width: 60px; font-style: italic;font-size:10px" align="right">
                        <%# FormatValue(String.Format("{0:#,##0}", Eval("Phien5")))%>
                        &nbsp;
                    </td>
                    <td class="Item_Price1" style="width: 60px; font-style: italic;font-size:10px" align="right">
                        <%# FormatValue(String.Format("{0:#,##0}", Eval("Phien4")))%>
                        &nbsp;
                    </td>
                    <td class="Item_Price1" style="width: 60px; font-style: italic;font-size:10px" align="right">
                        <%# FormatValue(String.Format("{0:#,##0}", Eval("Phien3")))%>
                        &nbsp;
                    </td>
                    <td class="Item_Price1" style="width: 60px; font-style: italic;font-size:10px" align="right">
                        <%# FormatValue(String.Format("{0:#,##0}", Eval("Phien2")))%>
                        &nbsp;
                    </td>
                    <td class="Item_Price1" style="width: 60px; font-style: italic; font-size:10px;border-right: solid 3px #e6e6e6;" align="right">
                        <%# FormatValue(String.Format("{0:#,##0}", Eval("Phien1")))%>
                        &nbsp;
                    </td>
                    <td class="Item_Price1" align="right" style="font-weight:bold">
                        <%# FormatValue(ReturnKLGDChange(Eval("Phien1").ToString(), Eval("Phien2").ToString()))%>
                        &nbsp;
                    </td>
                    <td class="Item_Price1" align="right" style="font-weight:bold">
                        <%# FormatValue(ReturnKLGDChange(Eval("Phien1").ToString(), Eval("KLGDTB5Phien").ToString()))%>
                        &nbsp;
                    </td>
                    <td class="Item_Price1" align="right" style="font-weight:bold; border-right: solid 3px #e6e6e6;">
                        <%# FormatValue(ReturnKLGDChange(Eval("Phien1").ToString(), Eval("KLGDTB1Thang").ToString()))%>
                        &nbsp;
                    </td>
                    <td class="LastItem_Price" style="width: 80px" align="center">
                        <%# ReturnChart(CafeF.BO.NewsHelper.ConvertToDouble(Eval("Chart1")),CafeF.BO.NewsHelper.ConvertToDouble(Eval("Chart2")),CafeF.BO.NewsHelper.ConvertToDouble(Eval("Chart3")),CafeF.BO.NewsHelper.ConvertToDouble(Eval("Chart4")),CafeF.BO.NewsHelper.ConvertToDouble(Eval("Chart5")))%>
                    </td>
                    
                </tr>
            </AlternatingItemTemplate>
        </asp:Repeater>
