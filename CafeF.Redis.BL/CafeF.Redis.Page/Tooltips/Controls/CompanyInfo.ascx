<%@ Control Language="C#" AutoEventWireup="true" Codebehind="CompanyInfo.ascx.cs"
    Inherits="Portal.ToolTips.Controls.CompanyInfo" %>
<table cellpadding="0" cellspacing="0" width="100%" >
    <tr>
        <td align="left" valign="top">
            <span class="chitiet_date2" style="font-weight: bold; font-size: 24px; font-family: Arial">
                <asp:literal runat="server" id="ltrCurentIndex"></asp:literal>
            </span>
            <br />
            <asp:label runat="server" id="lblChange" style="font-weight: bold; font-size: 14px;
                font-family: Arial"></asp:label>
        </td>
        <td style="font-family: Arial; font-size: 9px">
          
        </td>
    </tr>
   
</table>

<script>
    var t=window.parent.document.getElementById('tipTitle');    
    var tt="<%=UpdateTimer()%>";
    t.innerHTML=tt;
    var astock=window.parent.document.getElementById("aStock");
    if(astock)
    {
        astock.href='/Tin-doanh-nghiep/<%=Request.QueryString["symbol"] %>/Event.chn';
    }
    else
    {
        astock=window.parent.document.getElementById("aStock")
    }
 function DrawImage(url)
    {
        return;
        var __img=document.getElementById('imgChart');
        __img.src=url;
    }
</script>
<asp:Literal runat="server" ID="ltrimgChart"></asp:Literal>


