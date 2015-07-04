<%@ Control Language="C#" AutoEventWireup="true" Codebehind="ucMenu.ascx.cs" Inherits="CafeF.UserControls.Header.ucMenu" %>
<%@ Register src="ucChart.ascx" tagname="ucChart" tagprefix="uc1" %>
<%@ Register Src="/UserControl/NewsTitleHot/ucNewsTitleHot.ascx" TagName="TitleHot" TagPrefix="uc1" %>
<div id="header" class="clearfix">
    <div id="logo">
        <h1>
            <a href="/">CafeF</a></h1>
        <div class="time" id="datetime"><%= string.Format(DateTime.Now.ToString("{0} dd {1} M {2} yyyy<br/>h:mm tt"), "Ngày", "tháng","năm") %></div>
    </div>
    <!-- //logo -->
    <div id="divHeader">
        <div class="tinmoi" id="divTinMoi">
            <div style="float:left; width:100%; text-align:center; padding-top:40px"><img src="http://cafef3.vcmedia.vn/images/loading.gif" height='8' alt="" /></div>       
        </div>
        <%--<script type="text/javascript">
    //        LoadTinMoiHeader(1);
    //        LoadUser();
            /*$(document).ready(function(e) {  });*/
        </script>--%>
        <!-- //tin mới -->
        <div class="bieudo clearfix">
            <uc1:ucChart ID="ucChart1" runat="server" />
        </div>
    </div>
    <script type="text/javascript">
        LoadOverallHeader();
        /*LoadUser();*/
        $(document).ready(function() {
        /*LoadHomepageBoxes();*/
        LoadUser('cafef.user', 'name');
        });
    </script>
    <input id="hdPageIndex" type="hidden" value="1" />
    <!-- //Biểu đồ -->
    <ul id="mainnav">
        <li><a id="a_31" href="/thi-truong-chung-khoan.chn">Thị trường chứng khoán</a></li>
        <li><a id="a_35" href="/bat-dong-san.chn">Bất động sản</a></li>
        <li><a id="a_36" href="/doanh-nghiep.chn">Doanh nghiệp</a></li>
        <li><a id="a_34" href="/tai-chinh-ngan-hang.chn">Tài chính - ngân hàng</a></li>
        <li><a id="a_32" href="/tai-chinh-quoc-te.chn">Tài chính quốc tế</a></li>
        <li><a id="a_33" href="/vi-mo-dau-tu.chn">Kinh tế vĩ mô - Đầu tư</a></li>
        <li><a id="a_39" href="/hang-hoa-nguyen-lieu.chn">Hàng hóa - Nguyên liệu</a></li>
        <li><a id="a_40" href="/doanh-nhan.chn">Doanh Nhân</a></li>
    </ul>
    <!-- //mainnav -->
</div>
    <div id="macktheodoi"></div>
    <%--
    <div id="macktheodoi">
        <div class="danhsachma">
            <a href="#" class="prev">Prev</a> <a href="#" class="next">Next</a>
            <ul>
                <li class="wait">Chọn mã CK<br />
                    cần theo dõi</li>
                <li class="wait">Chọn mã CK<br />
                    cần theo dõi</li>
                <li class="wait">Chọn mã CK<br />
                    cần theo dõi</li>
                <li class="wait">Chọn mã CK<br />
                    cần theo dõi</li>
                <li class="wait">Chọn mã CK<br />
                    cần theo dõi</li>
                <li class="wait">Chọn mã CK<br />
                    cần theo dõi</li>
                <li class="wait">Chọn mã CK<br />
                    cần theo dõi</li>
            </ul>
            <div class="manager">
                <a href="#">Quản lý</a><span>close</span></div>
        </div>
    </div>--%>
    <!-- // Mã chứng khoán theo dõi -->
    <asp:Literal ID="ltrScript" runat="server"></asp:Literal>
<uc1:TitleHot runat="server" />