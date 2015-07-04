<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CoPhieuQuy.ascx.cs"
    Inherits="CafeF.Redis.Page.UserControl.LichSu.CoPhieuQuy" %>
<%@ Register Src="../DatePicker/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="CafeF.Redis.Page" Assembly="CafeF.Redis.Page" %>
        <table cellpadding="0" cellspacing="0" width="100%" class="SearchDataHistory_SearchForm">
            <tr>
                <td>Mã <asp:TextBox runat="server" ID="txtKeyword" Width="200px" ValidationGroup="SearchData"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtKeyword"
                        ErrorMessage="*" ValidationGroup="SearchData"></asp:RequiredFieldValidator>
                </td>
                <td style="width: 170px">Từ ngày<uc1:DatePicker ID="dpkTradeDate1" runat="server" /></td>
                <td style="width: 170px">Đến ngày<uc1:DatePicker ID="dpkTradeDate2" runat="server" /></td>
                <td align="left" style="width: 30px">
                    <asp:ImageButton ID="btSearch" runat="server" ImageUrl="http://cafef3.vcmedia.vn/images/images/xem.gif"
                        OnClick="btSearch_Click" ValidationGroup="SearchData" />
                </td>
                <td align="right">
                    <img align="absmiddle" src="http://cafef3.vcmedia.vn/images/new.gif" alt="" /><a href="/TraCuuLichSu2/TraCuu.chn">Xem toàn thị trường theo phiên</a>
                </td>
            </tr>
        </table><asp:UpdatePanel ID="panelAjax" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div style="background-color: #FFF; width: 100%; float: left" align="center">
            <asp:Repeater runat="server" ID="rptData">
                <HeaderTemplate>
                    <table cellpadding="2" cellspacing="0" width="100%" style="border-top: solid 1px #e6e6e6"
                        class="GirdTable">
                        <tr style="border-bottom: solid 1px #e6e6e6; font-family: Arial; font-size: 10px;
                            font-weight: bold; color: #004276; background-color: #FFF">
                            <td class="Header_DateItem" style="height: 30px; width: 7%">Ngày giao dịch</td>
                            <td class="Header_Price" style="height: 30px; width: 5%">Mua/Bán</td>
                            <td class="Header_Price" style="height: 30px; width: 4%">KL<br />đăng ký</td>
                            <td class="Header_Price" style="height: 30px; width: 5%">KLGD<br />trong ngày</td>
                            <td class="Header_ChangeItem" style="height: 30px; width: 5%">%/KL<br />đăng ký</td>
                            <td class="Header_Price" style="height: 30px; width: 5%">KLGD<br />tích lũy</td>
                            <td class="Header_ChangeItem" style="height: 30px; width: 5%">%/KL<br />đăng ký</td>
                            <td class="Header_Price" style="height: 30px; width: 5%">KL<br />còn lại</td>
                            <td class="Header_ChangeItem" style="height: 30px; width: 5%">%/KL<br />đăng ký</td>
                            <td class="Header_DateItem" style="height: 30px; width: 7%">Ngày hết hạn</td>
                        </tr>
                </HeaderTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
                <ItemTemplate>
                    <tr style="font-family: Arial; font-size: 10px; font-weight: normal; background-color: #f2f2f2"
                        runat="server" id="itemTR">
                        <td class="Item_DateItem" style="height: 30px;"><%#String.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem, "TradeDate"))%></td>
                        <td align="center" style="border-right: solid 1px #e6e6e6; border-bottom: solid 1px #e6e6e6;vertical-align: middle; width: 85px; font-size: 11px; height: 30px; padding-right: 8px;"><%# DataBinder.Eval(Container.DataItem, "TransactionType")%></td>
                        <td class="Item_Price" style="height: 30px; padding-right: 8px"><%#String.Format("{0:#,##}", DataBinder.Eval(Container.DataItem, "PlanVolume"))%></td>
                        <td class="Item_Price" style="height: 30px;"><%#String.Format("{0:#,##}", DataBinder.Eval(Container.DataItem, "TodayVolume"))%>&nbsp;</td>
                        <td class="Item_Price" style="height: 30px;"><%#returnEmpty(Eval("TodayPercent").ToString())%>&nbsp;</td>
                        <td class="Item_Price" style="height: 30px; font-style: italic;"><%#String.Format("{0:#,##}", DataBinder.Eval(Container.DataItem, "AccumulateVolume"))%>&nbsp;</td>
                        <td class="Item_Price" style="height: 30px; font-style: italic;"><%#returnEmpty(Eval("AccumulatePercent").ToString())%>&nbsp;</td>
                        <td class="Item_Price" style="height: 30px;"><%#String.Format("{0:#,##}", DataBinder.Eval(Container.DataItem, "RemainVolume"))%>&nbsp;</td>
                        <td class="Item_Price" style="height: 30px;"><%#returnEmpty(Eval("RemainPercent").ToString())%>&nbsp;</td>
                        <td class="Item_DateItem" style="height: 30px;"><%# returnBS(Eval("ExpiredDate").ToString())%>&nbsp;</td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr style="font-family: Arial; font-size: 10px; font-weight: normal; background-color: #fff"
                        runat="server" id="Tr1">
                        <td class="Item_DateItem" style="height: 30px;"><%#String.Format("{0:dd/MM/yyyy}", DataBinder.Eval(Container.DataItem, "TradeDate"))%></td>
                        <td align="center" style="border-right: solid 1px #e6e6e6; border-bottom: solid 1px #e6e6e6;vertical-align: middle; width: 85px; font-size: 11px; width: 10px; height: 30px;padding-right: 8px"><%# DataBinder.Eval(Container.DataItem, "TransactionType")%></td>
                        <td class="Item_Price" style="height: 30px; padding-right: 8px"><%#String.Format("{0:#,##}", DataBinder.Eval(Container.DataItem, "PlanVolume"))%></td>
                        <td class="Item_Price" style="height: 30px;"><%#String.Format("{0:#,##}", DataBinder.Eval(Container.DataItem, "TodayVolume"))%>&nbsp;</td>
                        <td class="Item_Price" style="height: 30px;"><%#returnEmpty(Eval("TodayPercent").ToString())%>&nbsp;</td>
                        <td class="Item_Price" style="height: 30px; font-style: italic;"><%#String.Format("{0:#,##}", DataBinder.Eval(Container.DataItem, "AccumulateVolume"))%>&nbsp;</td>
                        <td class="Item_Price" style="height: 30px; font-style: italic;"><%#returnEmpty(Eval("AccumulatePercent").ToString())%>&nbsp;</td>
                        <td class="Item_Price" style="height: 30px;"><%#String.Format("{0:#,##}", DataBinder.Eval(Container.DataItem, "RemainVolume"))%>&nbsp;</td>
                        <td class="Item_Price" style="height: 30px;"><%#returnEmpty(Eval("RemainPercent").ToString())%>&nbsp;</td>
                        <td class="Item_DateItem" style="height: 30px;"><%# returnBS(Eval("ExpiredDate").ToString())%>&nbsp;</td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
            <div style="border-bottom: solid 0px #dadada;
                padding-bottom: 10px; text-align: right; float: right">
                <div style="float: right;">
                    <cc1:Pager ID="pager1" runat="server" AlternativeTextEnabled="False" CompactedPageCount="20"
                        EnableSmartShortCuts="False" MaxSmartShortCutCount="0" NextClause=">"
                        NotCompactedPageCount="1" OnCommand="pager1_Command" PageSize="20" PreviousClause="<"
                        ShowFirstLast="False" SmartShortCutRatio="0" SmartShortCutThreshold="0"></cc1:Pager>
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

