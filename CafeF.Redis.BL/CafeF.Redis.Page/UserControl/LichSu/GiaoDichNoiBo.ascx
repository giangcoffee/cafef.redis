<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GiaoDichNoiBo.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.LichSu.GiaoDichNoiBo" %>
<%@ Register Src="../DatePicker/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="CafeF.Redis.Page" Assembly="CafeF.Redis.Page" %>
<div class="gdnoibo_div1">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 240px;font-size:12px;">
                &nbsp;Xem giao dich của CP khác&nbsp;</td>
            <td>
                <div align="left" style="float: left; text-align: left; background-color: #E1E1E1; color: #7a7a7a">
                    <asp:TextBox EnableViewState="false" runat="server" ID="txtKeyword" Width="200px"
                        autocomplete="off" CssClass="class_text_autocomplete ac_input"></asp:TextBox></div>
                <div class="footerDiv7" style="background-color: #E1E1E1; float: left">
                    Từ ngày<uc1:DatePicker ID="dpkTradeDate1" runat="server" />
                    Đến ngày<uc1:DatePicker ID="dpkTradeDate2" runat="server" />
                    
                   <asp:ImageButton EnableViewState="false" runat="server" ID="ibtnSearch" 
                        ImageUrl="http://cafef3.vcmedia.vn/images/images/xem.gif" onclick="ibtnSearch_Click" />
                </div>
            </td>

        </tr>
        <tr>
            <td style="text-align: right; width: 240px; font-style: italic;font-size:12px;">
                hoặc&nbsp;</td>
            <td>
            </td>

        </tr>
        <tr>
            <td style="font-size:12px;">
                &nbsp;Xem giao dịch của tổ chức /cá nhân&nbsp;</td>
            <td>
                <div align="left" style="float: left; text-align: left; background-color: #E1E1E1; color: #7a7a7a">
                    <asp:TextBox EnableViewState="false" runat="server" ID="txtKeyword2" Width="200px" autocomplete="off"
                        CssClass="class_text_autocomplete ac_input" ></asp:TextBox>
                </div>
                <div class="footerDiv7" style="background-color: #E1E1E1; float: left">
                    <asp:ImageButton EnableViewState="false" runat="server" ID="ibtnSearch2" 
                        ImageUrl="http://cafef3.vcmedia.vn/images/images/xem1.gif" onclick="ibtnSearch2_Click" /></div>
            </td>            
            <td style="padding-right: 10px; text-align: right; font-style: italic;;font-size:11px;" id="tdNote"
                runat="server">
                <span style="background-color: #ffffcc">&nbsp;Màu vàng&nbsp;</span>: Chưa có thông
                báo kết quả giao dịch
            </td>

        </tr>
    </table>
</div>
<asp:UpdatePanel ID="panelAjax" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
<div style="float: left; width: 100%; background-color: #FFF">
    <asp:Repeater runat="server" ID="rptData" 
        OnItemDataBound="rptData_ItemDataBound" onitemcommand="rptData_ItemCommand">
        <HeaderTemplate>
            <table cellpadding="2" cellspacing="0" width="100%" style="border-top: solid 1px #e6e6e6;border-left:solid 1px #e6e6e6;;border-right:solid 1px #e6e6e6"
                class="GirdTable">
                <tr class="HeaderTR">
                    <td rowspan="2" class="ItemTD1">Mã CP</td>
                    <td rowspan="2" class="ItemTD2">T&#7893; ch&#7913;c /ng&#432;&#7901;i giao d&#7883;ch</td>
                    <td rowspan="2" class="ItemTD3">Ch&#7913;c v&#7909;</td>
                    <td colspan="2" class="ItemTD4">Ng&#432;&#7901;i liên quan</td>
                    <td rowspan="2" style="border-right: solid 1px #e6e6e6; border-bottom: solid 1px #e6e6e6;text-align: center; vertical-align: top">SLCP<br />tr&#432;&#7899;c GD</td>
                    <td colspan="4" style="background-color:#eeece1;text-align: center; border-right: solid 1px #e6e6e6; border-bottom: solid 1px #e6e6e6">&#272;&#259;ng ký</td>
                    <td colspan="3" style="background-color:#d8d8d8;text-align: center; border-right: solid 1px #e6e6e6; border-bottom: solid 1px #e6e6e6">Kết quả</td>
                    <%--<td rowspan="2" style="text-align: center; border-right: solid 1px #e6e6e6; border-bottom: solid 1px #e6e6e6;vertical-align: top">Ngày công b&#7889;</td>--%>
                    <td rowspan="2" style="border-right: solid 1px #e6e6e6; border-bottom: solid 1px #e6e6e6;vertical-align: top; text-align: center">SLCP<br />sau GD</td>
                    <td rowspan="2" style="border-right: solid 1px #e6e6e6; border-bottom: solid 1px #e6e6e6;vertical-align: top; text-align: center">Tỷ lệ %</td>
                    <td rowspan="2" style="border-bottom: solid 1px #e6e6e6;vertical-align: top">Ghi chú</td>
                </tr>
                <tr class="HeaderTR">
                    <td style="border-right: solid 1px #e6e6e6; border-bottom: solid 1px #e6e6e6; font-weight: normal;width: 40px; text-align: center">Tên</td>
                    <td style="border-right: solid 1px #e6e6e6; border-bottom: solid 1px #e6e6e6; font-weight: normal;width: 40px; text-align: center">Chức vụ</td>
                    <td style="background-color:#eeece1;border-right: solid 1px #e6e6e6; border-bottom: solid 1px #e6e6e6; font-weight: normal;width: 40px; text-align: center">Mua</td>
                    <td style="background-color:#eeece1;border-right: solid 1px #e6e6e6; border-bottom: solid 1px #e6e6e6; font-weight: normal;width: 40px; text-align: center">Bán</td>
                    <td style="background-color:#eeece1;text-align: center; border-right: solid 1px #e6e6e6; border-bottom: solid 1px #e6e6e6;font-weight: normal; text-align: center">Ngày<br />b&#7855;t &#273;&#7847;u</td>
                    <td style="background-color:#eeece1;text-align: center; border-right: solid 1px #e6e6e6; border-bottom: solid 1px #e6e6e6;font-weight: normal; text-align: center">Ngày<br />k&#7871;t thúc</td>
                    <td style="background-color:#d8d8d8;border-right: solid 1px #e6e6e6; border-bottom: solid 1px #e6e6e6; font-weight: normal;width: 40px; text-align: center">Mua</td>
                    <td style="background-color:#d8d8d8;border-right: solid 1px #e6e6e6; border-bottom: solid 1px #e6e6e6; font-weight: normal;width: 40px; text-align: center">Bán</td>
                    <td style="background-color:#d8d8d8;text-align: center; border-bottom: solid 1px #e6e6e6;border-right: solid 1px #e6e6e6;font-weight: normal; text-align: center">Ngày<br />th&#7921;c hi&#7879;n</td>
                </tr>
        </HeaderTemplate>
        <FooterTemplate>
            </table></FooterTemplate>
        <ItemTemplate>
            <tr class="gdnoiboNormalItem"
                runat="server" id="itemTR">
                <td rowspan="1" style="border-right: #e6e6e6 1px solid; text-align: left; font-weight: normal;
                    vertical-align: top; border-bottom: solid 1px #e6e6e6" class="Item">
                    <a style="font-weight: normal" href='/Lich-su-giao-dich-<%#DataBinder.Eval(Container.DataItem,"Stock")%>-4.chn' title="<%=CompanyName %>">
                       <b> <%#DataBinder.Eval(Container.DataItem, "Stock")%></b>
                    </a>
                </td>
                <td rowspan="1" style="border-right: #e6e6e6 1px solid; text-align: left; font-weight: normal;
                    vertical-align: top; width: 150px; border-bottom: solid 1px #e6e6e6" class="Item">
                     <asp:LinkButton ID=lkbutton CommandName="ViewHolder" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"HolderID")%>' runat=server ToolTip='<%#DataBinder.Eval(Container.DataItem, "TransactionMan")%>' Text='<%#DataBinder.Eval(Container.DataItem, "TransactionMan")%>'></asp:LinkButton>
                </td>
                <td rowspan="1" style="border-right: #e6e6e6 1px solid; text-align: left; vertical-align: top;width: 60px; border-bottom: solid 1px #e6e6e6" class="Item"><%#DataBinder.Eval(Container.DataItem, "TransactionManPosition")%>&nbsp;</td>
                <td rowspan="1" style="border-right: #e6e6e6 1px solid; text-align: left; vertical-align: top;width: 60px; border-bottom: solid 1px #e6e6e6" class="Item"><%#DataBinder.Eval(Container.DataItem, "RelatedMan")%>&nbsp;</td>
                <td rowspan="1" style="border-right: #e6e6e6 1px solid; text-align: left; vertical-align: top;border-bottom: solid 1px #e6e6e6" class="Item"><%#DataBinder.Eval(Container.DataItem, "RelatedManPosition")%>&nbsp;</td>
                <td style="border-right: #e6e6e6 1px solid; text-align: right; vertical-align: top;border-bottom: solid 1px #e6e6e6" class="Item"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "VolumeBeforeTransaction"))%>&nbsp;</td>
                <td style="border-right: #e6e6e6 1px solid; text-align: right; vertical-align: top;border-bottom: solid 1px #e6e6e6;background-color:#eeece1;" class="Item" id="e_td1"><%#String.Format("{0:#,##}", DataBinder.Eval(Container.DataItem, "PlanBuyVolume"))%>&nbsp;</td>
                <td style="border-right: #e6e6e6 1px solid; text-align: right; vertical-align: top;border-bottom: solid 1px #e6e6e6;background-color:#eeece1;" class="Item" id="e_td2"><%#String.Format("{0:#,##}", DataBinder.Eval(Container.DataItem, "PlanSellVolume"))%>&nbsp;</td>
                <td style="border-right: #e6e6e6 1px solid; text-align: right; vertical-align: top;border-bottom: solid 1px #e6e6e6;background-color:#eeece1;" class="Item" id="e_td3"><%#String.Format("{0:dd/MM/yyyy}",DataBinder.Eval(Container.DataItem, "PlanBeginDate"))%>&nbsp;</td>
                <td style="border-right: #e6e6e6 1px solid; text-align: right; vertical-align: top;border-bottom: solid 1px #e6e6e6;background-color:#eeece1;" class="Item" id="e_td4"><%#String.Format("{0:dd/MM/yyyy}",DataBinder.Eval(Container.DataItem, "PlanEndDate"))%>&nbsp;</td>
                <td style="border-right: #e6e6e6 1px solid; text-align: right; vertical-align: top;border-bottom: solid 1px #e6e6e6;background-color:#d8d8d8;" class="Item" id="e_td5"><%#String.Format("{0:#,##}",DataBinder.Eval(Container.DataItem, "RealBuyVolume"))%>&nbsp;</td>
                <td style="border-right: #e6e6e6 1px solid; text-align: right; vertical-align: top;border-bottom: solid 1px #e6e6e6;background-color:#d8d8d8;" class="Item" id="e_td6"><%#String.Format("{0:#,##}",DataBinder.Eval(Container.DataItem, "RealSellVolume"))%>&nbsp;</td>
                <td rowspan="1" style="border-right: #e6e6e6 1px solid; text-align: right; vertical-align: top;border-bottom: solid 1px #e6e6e6;background-color:#d8d8d8;" class="Item" id="e_td7"><%#String.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem, "RealEndDate"))%>&nbsp;</td>
                <%--<td rowspan="1" style="border-right: #e6e6e6 1px solid; text-align: right; vertical-align: top;border-bottom: solid 1px #e6e6e6" class="Item"><%#String.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem, "PublishedDate"))%>&nbsp;</td>--%>
                <td rowspan="1" style="border-right: #e6e6e6 1px solid; text-align: right; vertical-align: top;border-bottom: solid 1px #e6e6e6" class="Item"><%#String.Format("{0:#,##}",DataBinder.Eval(Container.DataItem, "VolumeAfterTransaction"))%>&nbsp;</td>
                <td rowspan="1" style="border-right: #e6e6e6 1px solid; text-align: right; vertical-align: top;border-bottom: solid 1px #e6e6e6" class="Item"><asp:Literal ID="ltrTyLe" runat="server"></asp:Literal></td>
                <td rowspan="1" style="text-align: left; vertical-align: top; width: 60px; border-bottom: solid 1px #e6e6e6" class="Item"><%#DataBinder.Eval(Container.DataItem, "TransactionNote")%>&nbsp;</td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="font-family: Arial; font-size: 11px; font-weight: normal; background-color: #fff"
                runat="server" id="altitemTR">
                <td rowspan="1" style="border-right: #e6e6e6 1px solid; text-align: left; font-weight: normal;
                    vertical-align: top; border-bottom: solid 1px #e6e6e6" class="Item">
                    <a style="font-weight: normal" href='/Lich-su-giao-dich-<%#DataBinder.Eval(Container.DataItem,"Stock")%>-4.chn' title="<%=CompanyName %>">
                        <b><%#DataBinder.Eval(Container.DataItem, "Stock")%></b>
                    </a>
                </td>
                <td rowspan="1" style="border-right: #e6e6e6 1px solid; text-align: left; font-weight: normal;vertical-align: top; width: 150px; border-bottom: solid 1px #e6e6e6" class="Item"><asp:LinkButton ID=lkbutton CommandName="ViewHolder" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"HolderID")%>' runat=server ToolTip='<%#DataBinder.Eval(Container.DataItem, "TransactionMan")%>' Text='<%#DataBinder.Eval(Container.DataItem, "TransactionMan")%>'></asp:LinkButton></td>
                <td rowspan="1" style="border-right: #e6e6e6 1px solid; text-align: left; vertical-align: top;width: 60px; border-bottom: solid 1px #e6e6e6" class="Item"><%#DataBinder.Eval(Container.DataItem, "TransactionManPosition")%>&nbsp;</td>
                <td rowspan="1" style="border-right: #e6e6e6 1px solid; text-align: left; vertical-align: top;width: 60px; border-bottom: solid 1px #e6e6e6" class="Item"><%#DataBinder.Eval(Container.DataItem, "RelatedMan")%>&nbsp;</td>
                <td rowspan="1" style="border-right: #e6e6e6 1px solid; text-align: left; vertical-align: top;border-bottom: solid 1px #e6e6e6" class="Item"><%#DataBinder.Eval(Container.DataItem, "RelatedManPosition")%>&nbsp;</td>
                <td style="border-right: #e6e6e6 1px solid; text-align: right; vertical-align: top;border-bottom: solid 1px #e6e6e6" class="Item"><%#String.Format("{0:#,##0}", DataBinder.Eval(Container.DataItem, "VolumeBeforeTransaction"))%>&nbsp;</td>
                <td style="border-right: #e6e6e6 1px solid; text-align: right; vertical-align: top;border-bottom: solid 1px #e6e6e6;background-color:#eeece1;" class="Item" id="a_td1"><%#String.Format("{0:#,##}", DataBinder.Eval(Container.DataItem, "PlanBuyVolume"))%>&nbsp;</td>
                <td style="border-right: #e6e6e6 1px solid; text-align: right; vertical-align: top;border-bottom: solid 1px #e6e6e6;background-color:#eeece1;" class="Item" id="a_td2"><%#String.Format("{0:#,##}", DataBinder.Eval(Container.DataItem, "PlanSellVolume"))%>&nbsp;</td>
                <td style="border-right: #e6e6e6 1px solid; text-align: right; vertical-align: top;border-bottom: solid 1px #e6e6e6;background-color:#eeece1;" class="Item" id="a_td3"><%#String.Format("{0:dd/MM/yyyy}",DataBinder.Eval(Container.DataItem, "PlanBeginDate"))%>&nbsp;</td>
                <td style="border-right: #e6e6e6 1px solid; text-align: right; vertical-align: top;border-bottom: solid 1px #e6e6e6;background-color:#eeece1;" class="Item" id="a_td4"><%#String.Format("{0:dd/MM/yyyy}",DataBinder.Eval(Container.DataItem, "PlanEndDate"))%>&nbsp;</td>
                <td style="border-right: #e6e6e6 1px solid; text-align: right; vertical-align: top;border-bottom: solid 1px #e6e6e6;background-color:#d8d8d8;" class="Item" id="a_td5"><%#String.Format("{0:#,##}",DataBinder.Eval(Container.DataItem, "RealBuyVolume"))%>&nbsp;</td>
                <td style="border-right: #e6e6e6 1px solid; text-align: right; vertical-align: top;border-bottom: solid 1px #e6e6e6;background-color:#d8d8d8;" class="Item" id="a_td6"><%#String.Format("{0:#,##}",DataBinder.Eval(Container.DataItem, "RealSellVolume"))%>&nbsp;</td>
                <td rowspan="1" style="border-right: #e6e6e6 1px solid; text-align: right; vertical-align: top;border-bottom: solid 1px #e6e6e6;background-color:#d8d8d8;" class="Item" id="a_td7"><%#String.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem, "RealEndDate"))%>&nbsp;</td>
                <%--<td rowspan="1" style="border-right: #e6e6e6 1px solid; text-align: right; vertical-align: top;border-bottom: solid 1px #e6e6e6" class="Item"><%#String.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem, "PublishedDate"))%>&nbsp;</td>--%>
                <td rowspan="1" style="border-right: #e6e6e6 1px solid; text-align: right; vertical-align: top;border-bottom: solid 1px #e6e6e6" class="Item"><%#String.Format("{0:#,##}",DataBinder.Eval(Container.DataItem, "VolumeAfterTransaction"))%>&nbsp;</td>
                <td rowspan="1" style="border-right: #e6e6e6 1px solid; text-align: right; vertical-align: top;border-bottom: solid 1px #e6e6e6" class="Item"><asp:Literal ID="ltrTyLe" runat="server"></asp:Literal></td>
                <td rowspan="1" style="text-align: left; vertical-align: top; width: 70px; border-bottom: solid 1px #e6e6e6" class="Item"><%#DataBinder.Eval(Container.DataItem, "TransactionNote")%>&nbsp;</td>
            </tr>
        </AlternatingItemTemplate>
    </asp:Repeater>
    <div style="float: left; width: 1023px; padding: 0px 10px 10px 10px">
        <div style="float: left; color: rgb(153, 153, 153); font-size: 11px; font-weight: normal;
            font-family: arial; padding-top: 5px; text-align: left"><input type="hidden" id="txtType" value="1" runat="server" />
            Dữ liệu cập nhật từ 01/01/2008
            <br />
            Xây dựng bởi CafeF.vn&nbsp; &nbsp;<a href="http://cafef.vn"><img border="0" src="http://cafef3.vcmedia.vn/images/images/logosmall.jpg" /></a>
        </div>
        <div style="float: left; color: rgb(153, 153, 153); font-size: 11px; font-weight: normal;
            font-family: arial; padding-top: 24px; vertical-align: top; height: 30px">
        </div>
        <div style=" border-bottom: solid 0px #dadada;
            padding-bottom: 10px; text-align: right; float: right">
            <div style="float: right;">
                 <cc1:Pager ID="pager1" runat="server" AlternativeTextEnabled="False" CompactedPageCount="20"
                        EnableSmartShortCuts="False" MaxSmartShortCutCount="0" NextClause=">"
                        NotCompactedPageCount="1" OnCommand="pager1_Command" PageSize="20" PreviousClause="<"
                        ShowFirstLast="False" SmartShortCutRatio="0" SmartShortCutThreshold="0"></cc1:Pager>
               
            </div>
        </div>
    </div>
</div>
 </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="pager1" EventName="Command" />
        <asp:AsyncPostBackTrigger ControlID="ibtnSearch" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="ibtnSearch" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
<script type="text/javascript">
        var TextBox_KeywordId = '<% =txtKeyword.ClientID %>';
        $().ready(function() {
        $('#' + TextBox_KeywordId).autocomplete(oc, {
            minChars: 1,
            delay: 20,
            width: 400,
            max:15,
            matchContains: true,
            autoFill: false,
            GDNB:true,
            formatItem: function(row) {
                return row.c + " - " + row.m + "@" + row.o;
            },
            formatResult: function(row) {
                return row.c;
            },
            NextFocusControlId: 'TextBox_KeywordId'
        });
         });
         
         var TextBox_KeywordId2 = '<% =txtKeyword2.ClientID %>';
        $().ready(function() {
        $('#' + TextBox_KeywordId2).autocomplete(tochucgddata, {
            minChars: 1,
            delay: 20,
            width: 400,
            max:15,
            matchContains: true,
            autoFill: false,
            tochuc:true,
            formatItem: function(row) {
                return row.c + "@" + row.m;               
            },
            formatResult: function(row) {
                return row.i;               
            },
            NextFocusControlId: 'TextBox_KeywordId2'
        });
         });
         
        var swidth=screen.width;
        var divC=document.getElementById('cf_ContainerBoxTCLSDD');
        if(swidth==1280)
        {
            divC.style.width='1220px';
            divC.style.paddingLeft='20px';
        }
       
</script>
