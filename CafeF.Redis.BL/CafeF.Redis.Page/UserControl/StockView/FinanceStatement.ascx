<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FinanceStatement.ascx.cs"
    Inherits="CafeF.Redis.Page.UserControl.StockView.FinanceStatement" %>
<%--<asp:UpdatePanel ID="panelAjax" runat="server" UpdateMode="Conditional">
    <ContentTemplate>--%>
        <div class="hosocongty">
           <%-- <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="panelAjax" DynamicLayout="true">
                <ProgressTemplate>
                    <div id="Div1" align="center" valign="middle" runat="server" style="visibility: visible;vertical-align: middle; border-color: White; z-index: 40;"><img src="/images/loading.gif"></div>
                </ProgressTemplate>
            </asp:UpdateProgress>--%>
            <div class="phanchia clearfix">
                <div class="l" style="cursor: pointer; padding: 5px 0;">
                    <a id="idTabTaiChinhQuy" onclick="changeTabTaiChinh(1);">Theo quý</a> | <a id="idTabTaiChinhNam" class="active" onclick="changeTabTaiChinh(2);">Theo năm</a>
                </div>
                <div class="r" style="cursor: pointer; padding: 5px 0;"><a href="/<%= CenterName %>/<%= Symbol %>/bao-cao-tai-chinh.chn" onclick="javascript:changeTabCongTy(5); return false;" id="lsTab5CT">Tải xuống BCTC &amp; Báo cáo khác</a><br><span>(1.000 VNĐ)</span></div>
            </div>
            <div id="divHoSoCongTyAjax">
            <table width="100%" border=0 cellspacing=0 cellpadding=0>
                <tr>
                    <th class="col1">Chỉ tiêu &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a onclick="ViewPage(1);" style="cursor: pointer;vertical-align:top;">
                    <img alt="Xem dữ liệu trước" runat="server" id="imgPre" src="http://cafef3.vcmedia.vn/images/Scontrols/Images/LSG/Previous_Black.gif" border="0">&nbsp;Trước
                        </a>&nbsp;&nbsp;&nbsp;&nbsp;<a onclick="ViewPage(-1);" style="cursor: pointer;vertical-align:top;">Sau&nbsp;<img alt="Xem dữ liệu tiếp" runat="server" id="imgNext" src="http://cafef3.vcmedia.vn/images/Scontrols/Images/LSG/Next_Black.gif" border="0"></a>
                    </th>
                     
                    <th align="center"><asp:Literal ID="ltrCol4" runat="server" Text="&nbsp;"></asp:Literal></th>
                    <th align="center"><asp:Literal ID="ltrCol3" runat="server" Text="&nbsp;"></asp:Literal></th>
                    <th align="center"><asp:Literal ID="ltrCol2" runat="server" Text="&nbsp;"></asp:Literal></th>
                    <th align="center"><asp:Literal ID="ltrCol1" runat="server" Text="&nbsp;"></asp:Literal></th>
                    <th align="right">Tăng trưởng</th>
                </tr>
                
                <asp:Repeater runat="server" ID="rptNhomChiTieu" OnItemDataBound="rptNhomChiTieu_ItemDataBound">
                    <ItemTemplate>
                        <tr class="tbheader">
                            <td colspan="4" class="tbhl"><div><%# DataBinder.Eval(Container.DataItem, "TenNhomChiTieu")%></div></td>
                            <td colspan="2" class="tbhr"><a href='<%# ReturnHref(Eval("NhomChiTieuID").ToString()) %>'>Xem đầy đủ</a></td>
                        </tr>
                        <asp:Repeater runat="server" ID="rptData">
                            <ItemTemplate>
                                <tr style="font-weight: normal; text-align: left;" runat="server" id="TrData">
                                    <td class="col1"><%# DataBinder.Eval(Container.DataItem, "TenChiTieu")%></td>
                                    <td style="text-align: right"><asp:Literal ID="ltrCol4" runat="server" Text="-"></asp:Literal></td>
                                    <td style="text-align: right"><asp:Literal ID="ltrCol3" runat="server" Text="-"></asp:Literal></td>
                                    <td style="text-align: right"><asp:Literal ID="ltrCol2" runat="server" Text="-"></asp:Literal></td>
                                    <td style="text-align: right"><asp:Literal ID="ltrCol1" runat="server" Text="-"></asp:Literal></td>
                                    <td style="text-align: right"><asp:Literal ID="ltrChart" runat="server" Text="-"></asp:Literal></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                </asp:Repeater>
                  
            </table> </div>
            <div class="dltlonote">(*): Bao gồm doanh thu thuần hàng hóa & dịch vụ, doanh thu tài chính và doanh thu khác<br />(**): Trừ LNST của cổ đông thiểu số (nếu có)</div>
        </div>
        <input type="hidden" id="txtIdx" value="0" runat="server" />
        <input type="hidden" id="txtType" value="2" runat="server" />
        <%--<div style="visibility: hidden"><asp:Button ID="btnAjax" runat="server" Text="Ajax" OnClick="btnAjax_Click" /></div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnAjax" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>--%>

<script language="javascript" type="text/javascript">
function changeTabTaiChinh(index)
{
    var txtType =  document.getElementById('<%= txtType.ClientID %>');
    txtType.value = index;
    var txtIdx =  document.getElementById('<%= txtIdx.ClientID %>');
    txtIdx.value = "0";
    
    var idTabTaiChinhQuy = document.getElementById('idTabTaiChinhQuy');
    var idTabTaiChinhNam = document.getElementById('idTabTaiChinhNam'); 
    if(index==1)
    {
        idTabTaiChinhQuy.className = "active";
        idTabTaiChinhNam.className = "";
    }
    else
    {
        idTabTaiChinhQuy.className = "";
        idTabTaiChinhNam.className = "active";
    }
    LoadHoSoCongTy('<% = StockSymbol %>',index, txtIdx.value,4);
}

function ViewPage(index)
{
    var txtIdx =  document.getElementById('<%= txtIdx.ClientID %>');
    var currIdx = parseInt(txtIdx.value)+  parseInt(index);
    if (currIdx <0) return;
    var item = '<%= TotalItem %>';
    var txtType =  document.getElementById('<%= txtType.ClientID %>');
    if (txtType.value == "2")
    item = '<%= TotalItemInvert %>';
    if ((currIdx)*4 > item) return; 
    txtIdx.value = currIdx ;
    
   
    LoadHoSoCongTy('<% = StockSymbol %>',txtType.value, txtIdx.value,4);
}

function LoadHoSoCongTy(symbol, type, index, size)
{  
    $.ajax({
		type: "GET",
		url: "/Ajax/HoSoCongTy.aspx",
		data: "symbol="+ _symbol + "&Type=" + type + "&PageIndex=" + index + "&PageSize=" + size ,
		success: function(msg){
		     document.getElementById("divHoSoCongTyAjax").innerHTML=msg;
		}
	});

}
if (document.getElementById("<%= txtType.ClientID %>").value == '1') {
    $('#idTabTaiChinhQuy').addClass('active');
    $('#idTabTaiChinhNam').removeClass('active');
}
</script>

