<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SmallTip.aspx.cs" Inherits="Portal.ToolTips.SmallTip" %>

<%@ Register Src="Controls/Events.ascx" TagName="Events" TagPrefix="uc3" %>
<%--<%@ Register Src="Controls/CompanyMountainChart.ascx" TagName="CompanyMountainChart"
    TagPrefix="uc1" %>--%>
<%@ Register Src="Controls/CompanyInfo.ascx" TagName="CompanyInfo" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
   

   <link rel="stylesheet" href="http://cafef3.vcmedia.vn/styles/cafef.css" type="text/css" />
   <link rel="stylesheet" href="http://cafef3.vcmedia.vn/styles/datacenter.css" type="text/css" />
 
</head>
<body style="margin: 0px 5px 0px 0px;background-color:#fff">
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td colspan="2" style="font-family: Arial; font-size: 11px; font-weight: normal;color:#004276">
                        <asp:Literal runat="server" ID="txtFullname"></asp:Literal></td>
                </tr>
                <tr>
                    <td style="width: 60%" valign="top">
                        <img id="imgChart" runat="server"  />
                    </td>
                    <td style="width: 50%" valign="middle">
                        <uc2:CompanyInfo ID="CompanyInfo1" runat="server" />
                    </td>
                </tr>
                <tr>
                <%--<td style="height: 70%" colspan="2">
                    <uc3:Events ID="Events1" runat="server" />
                </td>--%>
                </tr>
                
            </table>
            <%--<div style="float:left;width:80"><a  class="jtSearch" href="javascript:void(0)" rel="/Scripts/cluetip/demo/ajax6.htm?symbol=ACB" onclick="Search();">Search</a></div>--%>
        </div>
    </form>
    
   
</body>
</html>
