<%@ Control Language="C#" AutoEventWireup="true" %>
<%@ Register Src="/UserControl/CafefToolbar.ascx" TagName="Toolbar" TagPrefix="uc1" %>
<style media="all">
    .CafeF_Padding10 p{margin:5px 0;}
</style>
<div style="width: 980px;" class="cf_WCBox">
    <uc1:Toolbar runat="server" id="ucToolbar" />
    <%--<div class="cf_BoxHeader">
        <div>
        </div>
    </div>--%>
    <div class="cf_BoxContent">
        <div style="width: 980px;" class="cf_WCBox">
            <div class="cf_BoxContent" style="text-align: left; padding: 0pt 20px;">
                <div style="padding-top: 10px;" class="cf_MBTop">
                    <h1 id="title" style="font-size:110%; text-transform:uppercase; margin:6px;">
                        <b>Bộ lọc chứng khoán - Hướng dẫn sử dụng nhanh</b></h1>
                    <div style="clear: both;">
                    </div>
                </div>
                <table style="background: none repeat scroll 0% 0% rgb(255, 255, 255); padding-top: 0px;" width="100%" cellpadding="0" cellspacing="0">
                    <tbody>
                        <tr>
                            <td class="CafeF_Padding10" style="font-size: 13px; line-height: 120%;" valign="top">
                                <p>
                                    <strong>1. Kéo thả để lấy giá trị cần lọc</strong></p>
                                <p>
                                    Tại mỗi chỉ tiêu cần lọc: sử dụng chuột kéo thanh trượt (2 đầu) để xác định giá trị lọc. Sau khi thực hiện thao tác kéo thả, bộ lọc sẽ tự động lọc ra giá trị ngay bên dưới.</p>
                                <p align="center">
                                    <img alt="" src="http://cafef3.vcmedia.vn/images/screener/image001.png" /></p>
                                <p>
                                    &nbsp;</p>
                                <p>
                                    <strong>2. Thêm chỉ tiêu vào bộ lọc</strong></p>
                                <p>
                                    Click vào dòng &ldquo;Thêm chỉ tiêu&rdquo;. Sau đó click tiếp nhóm chỉ tiêu [&ldquo;Biến động giá&rdquo;, &ldquo;Biến động khối lượng&rdquo;, &ldquo;Định giá&rdquo;, hoặc &ldquo;Tài chính&rdquo;]. Tiếp tục chọn chỉ tiêu cần đưa vào bộ lọc và bấm nút &ldquo;Thêm chỉ tiêu&rdquo;. <em>(Xem hình minh họa dưới)</em></p>
                                <p align="center">
                                    <img alt="" src="http://cafef3.vcmedia.vn/images/screener/image003.png" /><br />
                                    <img alt="" src="http://cafef3.vcmedia.vn/images/screener/image005.png" /></p>
                                <p>
                                    &nbsp;</p>
                                <p>
                                    <em>Chỉ tiêu lọc đã được thêm vào bộ lọc.</em><em> Kéo thả để lấy giá trị cần lọc.</em></p>
                                <p align="center">
                                    <img alt="" src="http://cafef3.vcmedia.vn/images/screener/image007.png" /></p>
                                <p>
                                    &nbsp;</p>
                                <p>
                                    <strong>3. Sử dụng kết quả lọc</strong></p>
                                <p>
                                    Kết quả sẽ được hiển thị ngay bảng bên dưới với các tiêu chí lọc đã xác định ở trên. Xem tiếp bằng cách click vào số thứ tự trang [1, 2, 3…], hoặc chữ cái [A, B, C, ….] để xem theo mã chứng khoán. <em>(Khoanh tròn màu xanh trong hình)</em></p>
                                <p>
                                    Click vào tiêu đề tại các cột ở bảng này để sắp xếp thứ tự lớn nhất, hoặc nhỏ nhất, hoặc cùng loại. <em>(Khoanh tròn màu đỏ)</em></p>
                                <p>
                                    Cuối cùng, nếu muốn bạn có thể xuất ra excel kết quả này. <em>(Khoanh tròn màu đen)</em></p>
                                <p align="center">
                                    <img alt="" src="http://cafef3.vcmedia.vn/images/screener/image009.png" /></p>
                                <p>
                                    &nbsp;</p>
                                <p>
                                    <strong>4. Chia sẻ kết quả lọc với người khác</strong></p>
                                <p>
                                    Sau khi lọc xong đã có kết quả. Bạn chỉ cần copy đường link trên thanh địa chỉ của trình duyệt và gửi đi, người khác sẽ xem được kết quả này.</p>
                                <p align="center">
                                    <img alt="" src="http://cafef3.vcmedia.vn/images/screener/image011.png" /></p>
                                <p>
                                    &nbsp;</p>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <%--<div class="cf_BoxFooter">
        <div>
        </div>
    </div>--%>
</div>
