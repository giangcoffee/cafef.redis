<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompanyInfo.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.StockView.CompanyInfo" %>
<%@ Register Src="FinanceStatement.ascx" TagName="FinanceStatement" TagPrefix="uc1" %>
<%@ Register Src="ucThongTinChung.ascx" TagName="ucThongTinChung" TagPrefix="uc2" %>
<%@ Register Src="ucCongTyCon.ascx" TagName="ucCongTyCon" TagPrefix="uc3" %>
<%@ Register Src="ucBanLanhDao.ascx" TagName="ucBanLanhDao" TagPrefix="uc4" %>
<%@ Register Src="ucBanLanhDaoV2.ascx" TagName="ucBanLanhDaov2" TagPrefix="uc4" %>
<%@ Register Src="ucBaoCaoTaiChinh.ascx" TagName="ucBaoCaoTaiChinh" TagPrefix="uc5" %>
<style type="text/css">
.tabs4 li {margin-left: 5px;}
</style>
<h2 class="cattitle noborder" id="taichinh">
    Hồ sơ công ty</h2>
<ul class="tabs4">
    <li id="liTabCongTy1CT"><a id="lsTab1CT" href="/<%= CenterName %>/<%= Symbol %>/thong-tin-tai-chinh.chn" onclick="changeTabCongTy(1); return false;">Thông tin tài chính</a></li>
    <li id="liTabCongTy2CT"><a id="lsTab2CT" href="/<%= CenterName %>/<%= Symbol %>/thong-tin-chung.chn" onclick="changeTabCongTy(2); return false;">Thông tin chung</a></li>
    <li id="liTabCongTy3CT"><a id="lsTab3CT" href="/<%= CenterName %>/<%= Symbol %>/ban-lanh-dao.chn" onclick="changeTabCongTy(3); return false;">Ban lãnh đạo và sở hữu <%= HasCeo?"<img border='0' width='30px' src='http://cafef3.vcmedia.vn/images/new.gif'>": "" %></a></li>
    <li id="liTabCongTy4CT"><a id="lsTab4CT" href="/<%= CenterName %>/<%= Symbol %>/cong-ty-con.chn" onclick="changeTabCongTy(4); return false;">Công ty con &amp; Cty liên kết</a></li>
</ul>
<div style="clear:both"></div>
<div class="phanchia clearfix">
    <%-- <div style="text-align: right;">
        <a id="lsTab5CT" href="javascript:changeTabCongTy(5);">Tải xuống BCTC &amp; Báo cáo khác<br />
            <span>(1.000VNĐ)</span></a>
    </div>--%>
    <div id="divStart" style="<%= (Request["tabid"]??"1")=="1"?"":"display:none;"%>">
        <uc1:FinanceStatement ID="FinanceStatement1" runat="server" />
    </div>
    <div id="divStart2" style="<%= (Request["tabid"]??"1")!="1"?"":"display:none;"%>">
        <uc2:ucThongTinChung ID="ucThongTinChung1" runat="server" Visible="false" />
        <uc3:ucCongTyCon ID="ucCongTyCon1" runat="server" Visible="false" />
        <uc4:ucBanLanhDao ID="ucBanLanhDao1" runat="server" Visible="false" />
        <uc4:ucBanLanhDaov2 ID="ucBanLanhDao2" runat="server" Visible="false" />
        <uc5:ucBaoCaoTaiChinh ID="ucBaoCaoTaiChinh1" runat="server" Visible="false" />
    </div>
    <div id="divAjax" align="center" style="overflow: hidden; padding-top: 6px; display: none">
        <div id="loading">
            <img src="http://cafef3.vcmedia.vn/v2/images/loading.gif" alt="" />
        </div>
    </div>
</div>
<%-- <script type="text/javascript" src="/scripts/InlineJSStock.CompanyInfo.js"></script>
--%>

<script type="text/javascript">
    var strThongTinChung = ""; var strBanLanhDaoVaSoHuu = ""; var strCongTyCon = ""; var strBaoCaoTaiChinh = ""; var divStart = document.getElementById("divStart"); var divAjax = document.getElementById("divAjax"); var tab1CT = document.getElementById("lsTab1CT"); var tab2CT = document.getElementById("lsTab2CT"); var tab3CT = document.getElementById("lsTab3CT"); var tab4CT = document.getElementById("lsTab4CT"); var liTabCongTy1CT = document.getElementById("liTabCongTy1CT"); var liTabCongTy2CT = document.getElementById("liTabCongTy2CT"); var liTabCongTy3CT = document.getElementById("liTabCongTy3CT"); var liTabCongTy4CT = document.getElementById("liTabCongTy4CT"); var sym = '<%= Symbol %>'; divAjax.style.display = "none"; 
    var currenttab = <%= Request["tabid"]??"1" %>;
    $(document).ready(function(e) {
        //if(currenttab!=0) {changeTabCongTy(currenttab);}
        $('#liTabCongTy'+currenttab+'CT').addClass('active');
        $('#liTabCongTy'+currenttab+'CT a').css('color','#C00');
    });
</script>

