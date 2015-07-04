<%@ Page Language="C#" MasterPageFile="~/MasterPage/SoLieu.Master" AutoEventWireup="true" Codebehind="LichSuKien.aspx.cs" Inherits="CafeF.Redis.Page.LichSuKien" %>
<%@ Register Src="UserControl/LichSuKien/Lich_su_kien.ascx" TagName="LichSuKien" TagPrefix="uc1" %>
<%@ Register Src="UserControl/LichSuKien/Lich_su_kien_v2.ascx" TagName="LichSuKienv2" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="http://cafef3.vcmedia.vn/v2/style/lichsukien.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="http://cafef3.vcmedia.vn/styles/DatePicker/datepicker.min.css" rel="stylesheet" />
    <div style="width: 980px;" class="cf_WCBox">        
        <div class="cf_BoxContent">
            <div class="cf_Pad5TB9LR">
                <%--<uc1:LichSuKien ID="LichSuKien1" runat="server" />--%>
                <uc1:LichSuKienv2 ID="LichSuKien2" runat="server" />
            </div>
        </div>       
    </div>
</asp:Content>
