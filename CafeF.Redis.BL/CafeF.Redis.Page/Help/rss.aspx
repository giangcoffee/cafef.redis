<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SoLieu.Master" AutoEventWireup="true" CodeBehind="rss.aspx.cs" Inherits="CafeF.Redis.Page.Help.rss" Title="Hướng dẫn sử dụng CafeF RSS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 980px;" class="cf_WCBox">
        <div class="cf_BoxContent">
            <div style="width: 980px;" class="cf_WCBox">
                <div class="cf_BoxContent" style="text-align: left; padding: 20px 30px 20px 30px">
                <p class="font_bold">
                        <strong>RSS là gì? </strong>
                    </p>
                    <p>
                        RSS (Really Simple Syndication) Dịch vụ cung cấp thông tin cực kì đơn giản. Dành
                        cho việc phân tán và khai thác nội dung thông tin Web từ xa(ví dụ như các tiêu đề,
                        tin tức). Sử dụng RSS, các nhà cung cấp nội dung Web có thể dễ dàng tạo và phổ biến
                        các nguồn dữ liệu ví dụ như các link tin tức, tiêu đề, và tóm tắt.</p>
                    <p>
                        Một cách sử dụng nguồn kênh tin RSS được nhiều người ưa thích là kết hợp nội dung
                        vào các nhật trình Web (weblogs, hay "blogs"). Blogs là những trang web mang tính
                        các nhân và bao gồm các mẩu tin và liên kết ngắn, thường xuyên cập nhật.</p>
                    <p>
                        <strong><span class="font_bold">Danh mục tin RSS mà CafeF.vn cung cấp</span>: </strong>
                    </p>
                    <p>
                        CafeF.vn hiện tại cung cấp các nguồn kênh dữ liệu dưới đây theo định dạng chuẩn
                        mới nhất RSS 2.0. Các nguồn kênh tin này là miễn phí cho việc sử dụng dưới mục đích
                        cá nhân và phi lợi nhuận. Bạn chỉ việc copy và dán các địa chỉ URL này vào những
                        trang web hoặc phần mềm hỗ trợ đọc tin từ RSS Feeds hoặc kéo thả biểu tượng RSS
                        dưới đây vào các phần mềm hỗ trợ RSS là được.</p>
                    <ul>
                        <li><a href="http://rss.cafef.channelvn.net/Thi-truong-chung-khoan.rss">
                            <img width="36" height="14" border="0" src="http://cafef3.vcmedia.vn/images/rss.gif" /></a> <span class="menu_link">
                                <a href="http://rss.cafef.channelvn.net/Thi-truong-chung-khoan.rss">Thị trường chứng
                                    khoán</a></span></li>
                        <li><a href="http://rss.cafef.channelvn.net/Bat-dong-san.rss">
                            <img width="36" height="14" border="0" src="http://cafef3.vcmedia.vn/images/rss.gif" /></a> <span class="menu_link">
                                <a href="http://rss.cafef.channelvn.net/Bat-dong-san.rss">Bất động sản</a></span></li>
                        <li><a href="http://rss.cafef.channelvn.net/Tai-chinh-quoc-te.rss">
                            <img width="36" height="14" border="0" src="http://cafef3.vcmedia.vn/images/rss.gif" /></a> <span class="menu_link">
                                <a href="http://rss.cafef.channelvn.net/Tai-chinh-quoc-te.rss">Tài chính quốc tế</a></span></li>
                        <li><a href="http://rss.cafef.channelvn.net/Tai-chinh-ngan-hang.rss">
                            <img width="36" height="14" border="0" src="http://cafef3.vcmedia.vn/images/rss.gif" /></a> <span class="menu_link">
                                <a href="http://rss.cafef.channelvn.net/Tai-chinh-ngan-hang.rss">Tài chính - Ngân hàng</a></span></li>
                        <li><a href="http://rss.cafef.channelvn.net/doanh-nghiep.rss">
                            <img width="36" height="14" border="0" src="http://cafef3.vcmedia.vn/images/rss.gif" /></a> <span class="menu_link">
                                <a href="http://rss.cafef.channelvn.net/doanh-nghiep.rss">Doanh nghiệp</a></span></li>
                        
                        <li><a href="http://rss.cafef.channelvn.net/hang-hoa-nguyen-lieu.rss">
                    <img width="36" height="14" border="0" src="http://cafef3.vcmedia.vn/images/rss.gif" /></a> <span class="menu_link">
                        <a href="http://rss.cafef.channelvn.net/hang-hoa-nguyen-lieu.rss">Hàng hóa - Nguyên liệu</a></span></li>    
                        <li><a href="http://rss.cafef.channelvn.net/Dau-tu.rss">
                            <img width="36" height="14" border="0" src="http://cafef3.vcmedia.vn/images/rss.gif" /></a> <span class="menu_link">
                                <a href="http://rss.cafef.channelvn.net/Dau-tu.rss">Kinh tế - Đầu tư</a></span></li>
                        <li><a href="http://rss.cafef.channelvn.net/Cafef.rss">
                            <img width="36" height="14" border="0" src="http://cafef3.vcmedia.vn/images/rss.gif" /></a> <span class="menu_link">
                                <a href="http://rss.cafef.channelvn.net/Cafef.rss">Tất cả tin tức từ cafef.channelvn.net</a></span></li>
                    </ul>
                    <p class="font_bold">
                        <strong>Sử dụng RSS như thế nào? </strong>
                    </p>
                    <p>
                        Các ứng dụng phổ biến về việc cung cấp và sử dụng RSS bao gồm:
                    </p>
                    <ul>
                        <li>Sử dụng một chương trình Khai Thác Thông Tin (News Aggregator,News Spider) để thu
                            thập, cập nhật và hiển thị các nguồn kênh tin dạng RSS</li>
                        <li>Kết hợp nguồn kênh tin RSS vào một nhật trình Web (weblogs) </li>
                    </ul>
                    <p>
                        News Aggregators (hay trình Khai Thác Thông Tin) sẽ tải xuống và hiển thị các nguồn
                        kênh tin RSS cho bạn. Có một số các chương trình dạng này cho phép bạn download
                        miễn phí. Các chương trình phổ biến gồm có : AmphetaDesk, NetNewsWire, và Radio
                        Userland.
                    </p>
                    <p class="font_bold">
                        <strong>Các giới hạn sử dụng:</strong></p>
                    <p>
                        Các nguồn kênh tin được cung cấp miễn phí cho các cá nhân và các tổ chức phi lợi
                        nhuận. Chúng tôi yêu cầu bạn cung cấp rõ các thông tin cần thiết khi bạn sử dụng
                        các nguồn kênh tin này từ CafeF.vn</p>
                    <p>
                        Nếu bạn định ghi dưới dạng văn bản, hãy ghi: "CafeF.vn” Nếu bạn định ghi dưới dạng
                        đồ họa, hãy sử dụng biểu tượng của CafeF.vn đi kèm trong mỗi nguồn tin</p>
                    <p>
                        CafeF.vn hoàn toàn có quyền yêu cầu bạn ngừng cung cấp và phân tán thông tin dưới
                        dạng này ở bất kỳ thời điểm nào và với bất kỳ lý do nào.
                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
