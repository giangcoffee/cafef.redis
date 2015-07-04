<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BDSDuanThamgia.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.TienDoBDS.BDSDuanThamgia" %>

 <style type="text/css">        
    
.tiendo {margin: 10px 0 0 0; padding: 0; border-bottom: 1px solid #eee}	
.tiendo table {font-size: 11px;border-bottom: 1px solid #ddd }
.tiendo th {color: #000000; font-weight: bold; text-align:center; background: #eee; border-bottom: 1px solid #ddd; border-right: 1px solid #ddd; padding: 8px; text-align: center}
.tiendo td { padding: 4px 2px}
.tiendo .col1 {border-right: 1px solid #ddd;width: 200px}
.tiendo .col2 {border-right: 1px solid #ddd;width: 150px;}
.tiendo .col3 {border-right: 1px solid #ddd;width: 150px; }
.tiendo .col4 {width: 200px; }
.tiendo tr.even td {background: #eee}  
.tiendo br {margin: 12px; }
.tiendo th2 {color: #000000; font-weight: bold; text-align:center; background: #eee; border-bottom: 1px solid #ddd; border-right: 1px solid #ddd; text-align: center}

    </style>
    
<div class="tiendo" style='<%=style%>'>    
<h2 class="cattitle noborder"><a href="/tiendobds/<%= (Request["Symbol"]??"").ToUpper() %>.chn">Tiến độ các dự án đã tham gia</a></h2>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
                <asp:Repeater EnableViewState="false" ID="rpData" runat="server" 
                     onitemdatabound="rpData_ItemDataBound">
                    <HeaderTemplate>
                     <tr valign="top">
                            <th class="col1">Tên dự án</th>
                            <th class="col2">Tổng vốn đầu tư/Tổng giá trị gói thầu</th>
                            <th class="col3">Địa điểm</th>
                            <th class="col4">Ghi chú về hiện trạng  và tiến độ dự án</th>
                        </tr>          
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr valign="top">
                            <td class="col1"><a id="ahre" href="/tiendobds/<%= (Request["Symbol"]??"").ToUpper() %>/trang-<%# getPageIdx(1+ Convert.ToInt32(DataBinder.Eval(Container, "ItemIndex", "")),5)%>.chn#<%# Eval("MaTienDo")%>"><%# Eval("TenDuAn")%></a></td>
                            <td class="col2" align=left><%# Eval("TongVon")%> <%# Eval("Donvi")%></td>
                            <td class="col3"><%# Eval("DiaDiem")%></td>
                            <td class="col4"><asp:Literal ID="ltrGhichu" runat="server"></asp:Literal> <asp:Literal ID="ltDate" runat="server"></asp:Literal> </td>
                        </tr>            
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                     <tr valign="top" class="even">
                            <td class="col1"><a id="ahre" href="/tiendobds/<%= (Request["Symbol"]??"").ToUpper() %>/trang-<%# getPageIdx(1+ Convert.ToInt32(DataBinder.Eval(Container, "ItemIndex", "")),5)%>.chn#<%# Eval("MaTienDo")%>"><%# Eval("TenDuAn")%></a></td>
                            <td class="col2" align=left><%# Eval("TongVon")%> <%# Eval("Donvi")%></td>
                            <td class="col3"><%# Eval("DiaDiem")%></td>
                            <td class="col4"><asp:Literal ID="ltrGhichu" runat="server"></asp:Literal> <asp:Literal ID="ltDate" runat="server"></asp:Literal></td>
                        </tr>      
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        
                    </FooterTemplate>
                </asp:Repeater>                
            </table>
       <br />
</div>
