<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HoSoCongTy.aspx.cs" Inherits="CafeF.Redis.Page.Ajax.HoSoCongTy" %>
 <table width="100%" border=0 cellspacing=0 cellpadding=0>
                <tr>
                    <th class="col1">Chỉ tiêu &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a onclick="ViewPage(1);" style="cursor: pointer;vertical-align:top;">
                    <img alt="Xem dữ liệu trước" runat="server" id="imgPre" src="http://cafef3.vcmedia.vn/images/Scontrols/Images/LSG/Previous_Black.gif" border="0">&nbsp;Trước
                        </a>&nbsp;&nbsp;&nbsp;&nbsp;<a onclick="ViewPage(-1);" style="cursor: pointer;vertical-align:top;">Sau&nbsp;<img alt="Xem dữ liệu tiếp" runat="server" id="imgNext" src="http://cafef3.vcmedia.vn/images/Scontrols/Images/LSG/Next_Black.gif" border="0"></a>
                    </th>
                     
                    <th align="right"><asp:Literal ID="ltrCol4" runat="server" Text="&nbsp;"></asp:Literal></th>
                    <th align="right"><asp:Literal ID="ltrCol3" runat="server" Text="&nbsp;"></asp:Literal></th>
                    <th align="right"><asp:Literal ID="ltrCol2" runat="server" Text="&nbsp;"></asp:Literal></th>
                    <th align="right"><asp:Literal ID="ltrCol1" runat="server" Text="&nbsp;"></asp:Literal></th>
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
                  
            </table>
           