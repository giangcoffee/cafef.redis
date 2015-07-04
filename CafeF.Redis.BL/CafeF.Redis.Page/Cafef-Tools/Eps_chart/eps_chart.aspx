<%@ Page Language="C#" MasterPageFile="~/MasterPage/SoLieu.Master" AutoEventWireup="true"
    Codebehind="eps_chart.aspx.cs" Inherits="CafeF.GUI.CafeF_Tools.eps_chart" %>
<%@ Register Src="/UserControl/CafefToolbar.ascx" TagName="Toolbar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="cf_WCBox" style="width:980px;text-align:center">
        <%--<div class="cf_BoxHeader">
            <div>
            </div>
        </div>--%>
        <uc1:Toolbar runat="server" id="ucToolbar" />
        <div class="cf_BoxContent">
            <div class="cf_Pad5TB9LR">
                <div class="cf_WireBox">
                    <div class="cf_BoxHeader">
                        <div>
                        </div>
                    </div>                    
                    <div class="cf_BoxContent">
                       <%-- <div class="sukien_DOANHNGHIEP_link" style="padding:5px 5px 5px 10px;border-bottom:solid 1px #dadada;width:300px;float:left"></div>
                        <div style="float:right;width:600px;"></div>--%>
                        <table align="center" width="100%" cellspacing="0" cellpadding="0" style="border-bottom:solid 1px #dadada;height:30px">
                            <tr>
                                <td class="sukien_DOANHNGHIEP_link">BIỂU ĐỒ CHỨNG KHOÁN</td>
                                <td valign="top" style="color:#555555;font-family:Arial;font-size:12px;vertical-align:middle;text-align:right;padding-right:10px">
                                    Chuyên mục hợp tác với Công ty Chứng khoán VNDirect &nbsp;<a href="http://cafef.vn"><img src="http://cafef3.vcmedia.vn/v2/images/partner/logoXX25.jpg" border="0" style="vertical-align: middle"/></a>&nbsp;
                                    <a href="http://www.vndirect.com.vn" target="_blank" ><img src="http://cafef3.vcmedia.vn/v2/images/partner/ckvnds.gif" style="vertical-align: middle" border="0" /></a>
                                    </td>
                            </tr>
                        </table>
                        <table align="center" width="100%" cellspacing="0" cellpadding="0" style="background: rgb(255, 255, 255) none repeat scroll 0%;">
                            <tr>
                                <td valign="top" align="center" style="padding-top: 10px;">
                                    <asp:Literal ID="ltrChart" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                        
                    </div>
                    <div class="cf_BoxFooter">
                        <div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
<%--        <div class="cf_BoxFooter">
            <div>
            </div>
        </div>
--%>    </div>
    
</asp:Content>
<%--<span class="cms_category">BIỂU ĐỒ CHỨNG KHOÁN</span>--%>
