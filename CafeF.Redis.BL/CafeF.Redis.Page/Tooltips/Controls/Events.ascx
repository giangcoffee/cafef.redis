<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Events.ascx.cs" Inherits="Portal.ToolTips.Controls.Events" %>
<table width="300px" cellspacing="0" cellpadding="0" border="0">
    <%--<tr>
        <td>
            <div id="div1" style="padding-bottom:5px">
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></div>
        </td>
    </tr>--%>
    <%--<tr>
        <td>
            <div id="divCompany" style="padding-bottom:5px"></div>
        </td>
    </tr>--%>
    <tr>
        <td align="left" valign="top">
            <asp:Repeater ID="rptTopEvents" runat="server" OnItemDataBound="rptTopEvents_ItemDataBound">
                <HeaderTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="Event_Images"></td>
                        <td class="F_text_listbai_Tip" style="padding-bottom: 5px"><asp:Literal runat="server" ID="ltrContent"></asp:Literal></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </td>
    </tr>
    <tr>
        <td valign="top" align="right">
            &nbsp;&nbsp;&nbsp;&nbsp; <a id="aStock" target="_blank" href="/Tin-doanh-nghiep/<%=Request.QueryString["symbol"] %>/Event.chn">Xem thêm</a>&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
</table>

<script language="javascript" type="text/javascript">
    //var objCompany=document.getElementById('divCompany');
    //objCompany.innerHTML =autocomplete_GetCompanyName('<%=Request.QueryString["symbol"] %>');
</script>

