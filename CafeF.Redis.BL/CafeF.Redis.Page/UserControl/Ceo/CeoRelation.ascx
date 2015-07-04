<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CeoRelation.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.Ceo.CeoRelation" %>
<%@ Import Namespace="CafeF.Redis.BL"%>
<table class="cl_ceo" cellspacing="0" cellpadding="0" border="0" style="width: 100%">
    <tr><td colspan="6" style="border-bottom:solid 1px #D6D6D6"><span class="ceo_title2">CÁ NHÂN CÓ LIÊN QUAN</span></td></tr>
    <tr>
        <td style="width:25%;font-weight:bold">Họ và tên</td>
        <td style="width:10%;font-weight:bold">Quan hệ</td>
        <td style="width:10%;font-weight:bold">Cổ phiếu</td>
        <td style="width:15%;font-weight:bold">Số lượng</td>
        <td style="width:20%;font-weight:bold">Tính đến ngày</td>
        <td style="width:20%;font-weight:bold"><span style="color:red">*</span> Giá trị (tỷ VNĐ)</td>
    </tr>
    <asp:Repeater EnableViewState="false" ID="rpData" runat="server">
        <HeaderTemplate>
        </HeaderTemplate>
        <ItemTemplate>
            <tr valign="top">
                <td><%# Eval("CeoCode").ToString()==Eval("LastCeoCode").ToString() ? "": (Eval("CeoCode").ToString().ToUpper().StartsWith("LQ_") ? Eval("Name") : (string.Format("<a href='/ceo/{0}/{1}.chn' title='{3}'>{2}</a>", Eval("CeoCode"), Utils.UnicodeToKoDauAndGach(Eval("Name").ToString()), Eval("Name").ToString(), HttpUtility.HtmlEncode(Eval("Name").ToString()).Replace("'", ""))))%></td>
                <td><%# Eval("CeoCode").ToString() == Eval("LastCeoCode").ToString() ? "" : Eval("RelationTitle")%></td>
                <td><%# GetLink(Eval("Symbol").ToString())%></td>
                <td><%# Eval("AssetVolume")%></td>
                <td><%# Eval("UpdatedDate")%></td>
                <td style="padding-left:10px;"><%# GetValue(double.Parse(Eval("AssetVolume").ToString()), Eval("Symbol").ToString())%></td>
            </tr>            
        </ItemTemplate>
        <FooterTemplate>
        </FooterTemplate>
    </asp:Repeater>
</table>