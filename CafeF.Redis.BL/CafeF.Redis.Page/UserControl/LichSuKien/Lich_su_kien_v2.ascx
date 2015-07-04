<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Lich_su_kien_v2.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.LichSuKien.Lich_su_kien_v2" %>

<%@ Register Src="../DatePicker/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<div class="SuKienTrongThang">
    <div class="top">
        <div class="top_inner"><img alt="" src="http://cafef3.vcmedia.vn/images/sukientrongthang_05.gif" /></div>
    </div>
    <div class="border title" style="text-align: center;">
        LỊCH SỰ KIỆN THỊ TRƯỜNG <asp:Literal runat="server" ID="ltrMonth" Visible="false"></asp:Literal>
    </div>
    <div style="width:960px;overflow:hidden;  background-color:#ffffff;border-top:solid 1px #dadada;padding-top:5px;padding-bottom:20px;">
        <div style="float:left;width:60px;">
            <div style="margin-top:50px;">                 
                   <script type="text/javascript">
                   </script>
             </div>      
        </div>
        <div style="float:left;width:900px;overflow:hidden;text-align:left;">
            <div style="float:left;width:900px;font-weight:bold;"><img alt="" src="http://cafef3.vcmedia.vn/images/Calendar/calendar.gif" border="0" /> Tìm lịch sự kiện</div>
            <div style="float:left;width:900px;padding-top:20px;">
                <div style="float:left;width:100px;padding-left:20px;font-family:Arial;font-size:12px;">Mã CK: </div>
                <div style="float:left;width:780px;">
                       <asp:TextBox runat="server" ID="txtKeyword" Width="200px"></asp:TextBox>
                </div>
            </div>
            <div style="float:left;width:900px;padding-top:10px;">
                <div style="float:left;width:100px;padding-left:20px;font-family:Arial;font-size:12px;">
                    Sự kiện: </div>
                <div style="float:left;width:780px;">
                    <asp:DropDownList ID="dlType" runat="server">
                        <asp:ListItem Value="0">-- Chọn --</asp:ListItem>
                        <asp:ListItem Value="22">Bán ưu đãi</asp:ListItem>
                        <asp:ListItem Value="1">Đại hội cổ đông</asp:ListItem>
                        <asp:ListItem Value="10">Đấu giá cổ phần</asp:ListItem>
                        <asp:ListItem Value="32">Đấu giá quyền mua</asp:ListItem>
                        <asp:ListItem Value="33">GD chính thức cp phát hành thêm</asp:ListItem>
                        <asp:ListItem Value="17">GD lần đầu tại HNX</asp:ListItem>
                        <asp:ListItem Value="27">GD lần đầu tại HoSE</asp:ListItem>
                        <asp:ListItem Value="31">GD lần đầu tại UpCom</asp:ListItem>
                        <asp:ListItem Value="25">Hủy niêm yết tại HNX</asp:ListItem>
                        <asp:ListItem Value="26">Hủy niêm yết tại HoSE</asp:ListItem>
                        <asp:ListItem Value="35">Hủy niêm yết tại UpCOM</asp:ListItem>
                        <asp:ListItem Value="2">IPO</asp:ListItem>
                        <asp:ListItem Value="21">Lấy ý kiến cổ đông</asp:ListItem>
                        <asp:ListItem Value="34">Niêm yết cổ phiếu phát hành thêm</asp:ListItem>
                        <asp:ListItem Value="36">Niêm yết lần đầu tại HNX</asp:ListItem>
                        <asp:ListItem Value="29">Niêm yết lần đầu tại HoSE</asp:ListItem>
                        <asp:ListItem Value="30">Niêm yết lần đầu tại UpCom</asp:ListItem>
                        <asp:ListItem Value="12">Phát hành thêm cổ phiếu</asp:ListItem>
                        <asp:ListItem Value="20">Thưởng cổ phiếu</asp:ListItem>
                        <asp:ListItem Value="24">Trả cổ tức bằng cổ phiếu</asp:ListItem>
                        <asp:ListItem Value="23">Trả cổ tức bằng tiền</asp:ListItem>
                    </asp:DropDownList>                   
                </div>
            </div>
             <div style="float:left;width:900px;font-family:Arial;padding-top:10px;font-size:12px;">
                <div style="float:left;width:100px;padding-left:20px;">Từ ngày: </div>
                <div style="float:left;width:780px;">
                    <uc1:DatePicker ID="dpkTradeDate1" runat="server" /> &nbsp;&nbsp;&nbsp; 
                    Đến ngày <uc1:DatePicker ID="dpkTradeDate2" runat="server" />
                </div>
            </div>
            <div style="float:left;width:900px;padding-left:346px;padding-top:10px;">
                <asp:ImageButton ID="btSearch" runat="server" ImageUrl="http://cafef3.vcmedia.vn/images/images/xem.gif" OnClick="btSearch_Click" />
            </div>
        </div>
    </div>
    <div style="text-align:left; margin-left:5px;">
        <table cellpadding="4" cellspacing="0" border="0" width="100%">
            <tr>
                <td colspan ="2">
                     <asp:GridView ID="grvLichSuKien" runat="server" Width="961px" AutoGenerateColumns="False" CellPadding="4" CellSpacing="0" AllowPaging="True" OnRowDataBound="grvLichSuKien_RowDataBound" PageSize="20" OnPageIndexChanging="grvLichSuKien_PageIndexChanging">
                        <HeaderStyle HorizontalAlign="Center" />
                        <AlternatingRowStyle CssClass="Alt" />
                        <Columns>
                           
                            <asp:TemplateField HeaderText="Ng&#224;y GD kh&#244;ng&lt;br /&gt;hưởng quyền">
                                <ItemStyle CssClass="leftcell normal"  HorizontalAlign="center"/>
                                <ItemTemplate>
                                    <asp:Literal ID="ltrNgayBatDau" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ngày đăng ký &lt;br /&gt; cuối cùng">
                                <ItemStyle CssClass="leftcell normal"/>
                                <ItemTemplate>
                                    <asp:Literal ID="ltrNgayKetThuc" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ngày thực hiện">
                                <ItemStyle CssClass="leftcell normal" />
                                <ItemTemplate>
                                    <asp:Literal ID="ltrNgayThucHien" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="M&#227; CK">
                                <HeaderStyle CssClass="leftcell" />
                                <ItemStyle CssClass="leftcell normal" HorizontalAlign="Left" Font-Bold="true" Width="50px" />
                                <ItemTemplate>
                                    <asp:Literal ID="ltrMaCK" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="S&#224;n">
                                <ItemStyle CssClass="leftcell normal" Width="50px" />
                                <ItemTemplate>
                                    <asp:Literal ID="ltrSan" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sự kiện" Visible="false">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass="normal" />
                                <ItemTemplate>
                                    <asp:Literal ID="ltrSuKien" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nội dung sự kiện">
                               <HeaderStyle HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="Left" CssClass="normal" />
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkNoiDung" Target="_blank" runat="server"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText='Giá hiện tại' ItemStyle-Width="140px">
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemStyle HorizontalAlign="right" CssClass="normal" />
                                <ItemTemplate>
                                    <asp:Literal ID="ltrChange" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
             <tr>
                <td align="right">
                     <asp:Repeater  ID="rptPage" runat="server" OnItemDataBound="rptPage_ItemDataBound" OnItemCommand="rptPage_ItemCommand">
                        <HeaderTemplate>
                            <table class="CafeF_Paging" border="0" cellpadding="3" cellspacing="3">
                                <tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <td align="center" style="width: 20px"><asp:Button runat="server" ID="btnpage"  CommandName="paging" /><asp:Literal ID="ltrPage" runat="server"></asp:Literal></td>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tr> </table>
                        </FooterTemplate>
                    </asp:Repeater>                   
                </td>
                <td align="right" style="width:120px; padding-right:10px">
                    <asp:LinkButton ID="lbkStatusTrue" runat="server" Text="LỊCH SỰ KIỆN MỚI" ForeColor="Black" Font-Bold="true" Visible="false" OnClick="lbkStatusTrue_Click"></asp:LinkButton>
                    <asp:LinkButton ID="lbkStatusFalse" runat="server" Text="LỊCH SỰ KIỆN CŨ" ForeColor="Black" Font-Bold="true" OnClick="lbkStatusFalse_Click"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <div class="bottom">
        <div class="bottomleft"><img src="http://cafef3.vcmedia.vn/images/sukientrongthang_09.gif" alt="" /></div>
        <div class="bottomright"><img src="http://cafef3.vcmedia.vn/images/sukientrongthang_10.gif" alt="" /></div>
    </div>
</div>
<asp:HiddenField runat="server" ID="hdfStatus" Value="1" /> <%-- 1: moi; 2: cu; --%>
<asp:HiddenField runat="server" ID="hdfPageIndex" />
<asp:HiddenField runat="server" ID="hdfSymbol" />
<asp:HiddenField runat="server" ID="hdfDate1" />
<asp:HiddenField runat="server" ID="hdfDate2" />
<asp:Literal runat="server" ID="ltrScript"></asp:Literal>
<script type="text/javascript">    
        
        for(j=0;j<__arrma.length;j++)
        {
            var __sp=document.getElementById('sp_'+__arrma[j]);            
            if(__sp)
            {
                for (i=0;i<oc.length;i++)
                {
                    if( oc[i].c.toLowerCase()==__arrma[j].toLowerCase())
                    {                        
                        var san='Hose';
                        if(oc[i].san=='2') 
                        {
                            san='Hastc';     
                        }
                        else if(oc[i].san=='9')
                        {
                            san = 'UpCom'; 
                        }      
                        else
                        {
                            san = 'Hose';
                        }            
                        __sp.innerHTML=san;
                        
                        break;
                    }        
                }
                
            }
         }        
</script>

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
            LSK:true,
            formatItem: function(row) {
                return row.c + " - " + row.m + "@" + row.o;
            },
            formatResult: function(row) {
                return row.c;
            }
        });
    });
    
</script>