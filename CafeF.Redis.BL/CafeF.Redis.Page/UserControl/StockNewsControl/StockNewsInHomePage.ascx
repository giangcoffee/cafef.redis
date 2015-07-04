<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StockNewsInHomePage.ascx.cs"
    Inherits="CafeF.Redis.Page.UserControl.StockNewsControl.StockNewsInHomePage" %>
<asp:ScriptManager ID="scriptmanager" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="panelAjaxStockNews" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div id="divCompany" runat="server" class="companynews">
            <h3>Tin tức doanh nghiệp niêm yết</h3>
            <asp:Repeater ID="rptStockNews" runat="server" >
                <HeaderTemplate>
                    <table width="300" border="0" cellpadding="0" cellspacing="0">
                        <tbody>
                            <tr>
                                <th class="cn-1">Mã CP</th>
                                <th class="cn-2">Công ty</th>
                                <th class="cn-3">Giá</th>
                                <th class="cn-4">Thay đổi</th>
                            </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="odd">
                        <td class="cn-1">
                            <b>
                                <asp:Literal runat="server" ID="ltrSymbol"></asp:Literal></b>
                            <%#String.Format("{0:dd/MM}", (DateTime)Eval("PostTime"))%>
                        </td>
                        <td class="cn-2">
                            <a id=idLink href="#" runat=server>
                                <%# DataBinder.Eval(Container.DataItem, "Title")%></a>
                        </td>
                        <td class="cn-3">
                            <%# Eval("Price") %>
                        </td>
                        <td class="cn-4">
                            <%# Eval("ChangeString") %>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="even">
                         <td class="cn-1">
                            <b>
                                <asp:Literal runat="server" ID="ltrSymbol"></asp:Literal></b>
                            <%#String.Format("{0:dd/MM}", (DateTime)Eval("PostTime"))%>
                        </td>
                        <td class="cn-2">
                            <a id=idLink href="#" runat=server>
                                <%# DataBinder.Eval(Container.DataItem, "Title")%></a>
                        </td>
                        <td class="cn-3">
                            <%# Eval("Price") %>
                        </td>
                        <td class="cn-4">
                            <%# Eval("ChangeString") %>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </tbody> </table>
                </FooterTemplate>
            </asp:Repeater>
            <div class="cn-bottom clearfix">
                <div class="cn-all">
                    <a href="/Tin-doanh-nghiep.chn">Xem tất cả &gt;</a></div>
                <div class="cn-page" id="paging" runat="server">
                </div>
            </div>
        </div>
        <input type="hidden" id="txtIdx" value="1" runat="server" />
        <div style="visibility: hidden">
            <asp:Button ID="btnAjax" runat="server" Text="Ajax" OnClick="btnAjax_Click" /></div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnAjax" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>

<script language="javascript" type="text/javascript">
function ViewPageStockNews(index)
{
    var txtIdx =  document.getElementById('<%= txtIdx.ClientID %>');
    if (index <1) return;
    var TotalPage = '<%= TotalPage %>';
    if (index > TotalPage) return;
   
    txtIdx.value = index ;
    var objAjax = document.getElementById('<%= btnAjax.ClientID %>');
    if (objAjax != null)
        objAjax.click();   
}
</script>

