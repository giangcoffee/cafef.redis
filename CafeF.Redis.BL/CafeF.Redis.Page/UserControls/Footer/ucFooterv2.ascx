<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucFooterv2.ascx.cs"
    Inherits="CafeF.UserControls.Footer.ucFooterv2" %>
<ul id="mainnavfooter">
    <li><a href="/thi-truong-chung-khoan.chn">Thị trường chứng khoán</a></li>
    <li><a href="/bat-dong-san.chn">Bất động sản</a></li>
    <li><a href="/doanh-nghiep.chn">Doanh nghiệp</a></li>
    <li><a href="/tai-chinh-ngan-hang.chn">Tài chính - ngân hàng</a></li>
    <li><a href="/tai-chinh-quoc-te.chn">Tài chính quốc tế</a></li>
    <li><a href="/vi-mo-dau-tu.chn">Kinh tế vĩ mô - Đầu tư</a></li>
    <li><a href="/hang-hoa-nguyen-lieu.chn">Hàng hóa - Nguyên liệu</a></li>
    <li><a href="/doanh-nhan.chn">Doanh nhân</a></li>
</ul>
<div class="wrap clearfix">
    <div class="ft-qc">
        Phòng quảng cáo Admicro<br>
        Ms. Lệ Quyên<br>
        Mobile: 0936 737 727<br>
        Email: <a href="mailto:doanhnghiep@admicro.vn">doanhnghiep@admicro.vn</a></div>
    <div class="ft-info">
        Ban biên tập CafeF, tầng 22, Tháp B VinCom City Tower.<br>
        191 Bà Triệu, Hà Nội.<br>
        Điện thoại: 04-39743410 <span>Máy lẻ 562</span>. Fax: 04-39744082<br>
        Email: <a href="mailto:info@cafef.vn">info@cafef.vn</a><br>
        Ghi rõ nguồn "CafeF" khi phát hành lại thông tin từ cổng thông tin này.<br>
    </div>
    <div class="ft-cp">
        <strong>Copyright 2007</strong>&ndash;Công ty cổ phần truyền thôngViệt Nam&ndash;VCCorp.<br>
        Tầng 22-23,Vincom Tower B,191 Bà Triệu,Hà Nội<br>
        Giấy phép số 218/GP-TTĐT;<br>
        Cục QL phát thanh, truyền hình và thông tin điện tử, Bộ thông tin và truyền thông.<br>
    </div>
</div>
<input id="hdIP" runat="server" type="hidden" />
<asp:Literal ID="ltrScript" runat="server"></asp:Literal>
<script type="text/javascript">    
    GetDate();
    setTimeout("GetDate()",1000);        
</script>

<%--<script type="text/javascript" src="http://reporting.cafef.channelvn.net/js.js?"></script>--%>
<script type="text/javascript" src="http://cafef3.vcmedia.vn/reporting/log.js"></script>
<script type="text/javascript">
        var log_website = 'cafef_reporting'; 
        var newsid='<%= newsid %>';
        var news_title='<% = news_title %>';
        var catid='<%= catid %>';
        var cat_name='<%= cat_name %>';          
        Log_AssignValue(newsid, news_title, catid, cat_name);
</script>

