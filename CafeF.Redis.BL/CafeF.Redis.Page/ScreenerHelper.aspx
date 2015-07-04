<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SoLieu.Master" %>
<%@ Register Src="/UserControl/CafefToolbar.ascx" TagName="Toolbar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<link rel="stylesheet" type="text/css" href="/style/help.css" />
<div class="cf_WCBox" style="width: 950px; margin: 0pt 10px;" >
    <div style="text-align:center;"><uc1:Toolbar runat="server" id="ucToolbar" /></div>
    <div class="cf_BoxContent">
        <div style="padding: 10px;text-align:center;color:#C60000;font-size:20px;font-weight:bold">HƯỚNG DẪN SỬ DỤNG BỘ CÔNG CỤ DỮ LIỆU CỦA CAFEF</div>
        <div style="text-align:left;padding-left:50px">
            <ol class="ol">
                <li><a class="dxncItemHeadernews" href="#1">Tại sao tôi cần bộ Công cụ dữ liệu của CafeF?</a></li>
                <li><a class="dxncItemHeadernews" href="#2">Bộ Công cụ dữ liệu của CafeF có đặc điểm gì nổi bật?</a></li>
                <li><a class="dxncItemHeadernews" href="#3">Bộ Công cụ dữ liệu của CafeF có những tính năng gì?</a></li>
                <li><a class="dxncItemHeadernews" href="#4">Tôi tìm bộ Công cụ dữ liệu ở đâu?</a></li>
                <li><a class="dxncItemHeadernews" href="#5">Tôi sử dụng bộ lọc cổ phiếu để làm gì?</a></li>
                <li><a class="dxncItemHeadernews" href="#6">Bộ lọc cổ phiếu bao gồm những tiêu chí nào?</a></li>
                <li><a class="dxncItemHeadernews" href="#7">Tôi sử dụng bộ lọc cổ phiếu như thế nào?</a></li>
                <li><a class="dxncItemHeadernews" href="#8">Tôi muốn sử dụng thêm các tiêu chí khác?</a></li>
                <li><a class="dxncItemHeadernews" href="#9">Nhiều chỉ tiêu quá, tôi muốn bỏ bớt đi?</a></li>
                <li><a class="dxncItemHeadernews" href="#10">Tôi chẳng hiểu những tiêu chí này có ý nghĩa gì cả!</a></li>
                <li><a class="dxncItemHeadernews" href="#11">Tôi có thể lưu lại bộ lọc mình đã dùng và gửi cho bạn bè được chứ?</a></li>
                <li><a class="dxncItemHeadernews" href="#12">Tôi dùng bộ Thống kê biến động giá cổ phiếu để làm gì?</a></li>
                <li><a class="dxncItemHeadernews" href="#13">Tôi sử dụng bộ Thống kê biến động giá cổ phiếu như thế nào?</a></li>
                <li><a class="dxncItemHeadernews" href="#14">CafeF có những dữ liệu gì trong phần Tra cứu dữ liệu lịch sử?</a></li>
                <li><a class="dxncItemHeadernews" href="#15">Tôi muốn tra cứu dữ liệu lịch sử của mã chứng khoán khác?</a></li>
                <li><a class="dxncItemHeadernews" href="#16">Tôi muốn thay đổi thời gian tra cứu dữ liệu lịch sử?</a></li>
                <li><a class="dxncItemHeadernews" href="#17">Tôi muốn biết cổ đông lớn/cổ đông nội bộ đó đã thực hiện những giao dịch nào?</a></li>
                <li><a class="dxncItemHeadernews" href="#18">CafeF có những dữ liệu gì đối với một cổ phiếu?</a></li>
                <li><a class="dxncItemHeadernews" href="#19">Tôi muốn tải xuống Báo cáo tài chính và Báo cáo khác?</a></li>
                <li><a class="dxncItemHeadernews" href="#20">Tôi muốn biết Ban lãnh đạo của Công ty mình đang đầu tư là những ai? Công ty ấy có những cổ đông lớn nào?</a></li>
                <li><a class="dxncItemHeadernews" href="#21">Công ty tôi đang đầu tư hiện có những Công ty con/Công ty liên kết nào?</a></li>
                <li><a class="dxncItemHeadernews" href="#22">CafeF có bộ lọc tin tức doanh nghiệp theo từng chủ đề không?</a></li>
            </ol>
        </div>
        <div style="line-height:20px; border:1px solid #DADADA;padding:5px 5px 10px 5px;text-align:justify;width:930px;color:#343434;font-size:14px;font-family:Arial,Helvetica,sans-serif; margin: 10px;">
            <div style="padding:0px 5px 0px 10px">
            <p class="helpstyle3" style="text-indent: -0.1in;">
            <span style="font-family: Symbol;"><img width="13" height="13" alt="*" src="http://cafef3.vcmedia.vn/help/Help_moi_cho_portfolio_files/image001.gif" />
            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <b><span>
                <a name="1"></a>1. Tại sao tôi cần bộ Công cụ dữ liệu của CafeF?
            </span></b></p>
            <p class="helpstyle3"><span>
                Thị trường chứng khoán Việt Nam đang phát triển rất mạnh mẽ, số công ty niêm yết trên sàn ngày càng lớn. Những ngày đầu thành lập, một nhà đầu tư có thể nhớ toàn bộ các chỉ số cơ bản của SAM và REE thì nay ngay việc nhớ hết toàn bộ hơn 500 mã chứng khoán cũng đã là thử thách không tưởng đối với tuyệt đại đa số nhà đầu tư, ấy là còn chưa nói tới việc muốn đầu tư thành công, rất cần những dữ liệu về tình hình hoạt động của công ty như lợi nhuận, ROE cũng như lịch sử giao dịch của bản thân mã cổ phiếu như biến động về giá hay khối lượng giao dịch. Bộ Công cụ dữ liệu do CafeF xây dựng là kết tinh từ trình độ của các chuyên gia chứng khoán hàng đầu và sự am hiểu sâu sắc nhu cầu độc giả của đội ngũ biên tập viên giàu kinh nghiệm tại CafeF. Bộ Công cụ dữ liệu của CafeF sẽ là trợ thử đắc lực để bạn dành được lợi nhuận tối ưu.
            </span></p>
            
            <p class="helpstyle3" style="margin-left: 0.5in; text-indent: -0.25in;">
            <span style="font-family: Symbol;"><img width="13" height="13" alt="*" src="http://cafef3.vcmedia.vn/help/Help_moi_cho_portfolio_files/image001.gif" />
            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <b><span>
                <a name="2"></a>2. Bộ Công cụ dữ liệu của CafeF có đặc điểm gì nổi bật?
            </span></b></p>
            <p class="helpstyle3"><span>
                <b>- &nbsp;&nbsp;Dễ sử dụng nhất:</b> Bạn không cần là một người giỏi thao tác trên máy tính, mọi tính năng đều được chúng tôi thiết kế thật dễ hiểu, sao cho việc sử dụng là đơn giản nhất. <br />
                <b>- &nbsp;&nbsp;Tốc độ nhanh nhất:</b> nhờ hệ thống sắp xếp và xử lý dữ liệu hiện đại cùng tốc độ đường truyền cao, CafeF hiện đang sở hữu bộ lọc cổ phiếu nhanh nhất thị trường. Bạn sẽ mất không quá 3s để lọc dữ liệu từ gần 30 tiêu chí khác nhau.<br />
                <b>- &nbsp;&nbsp;Dễ tìm kiếm nhất:</b> Bạn có thể tìm thấy bộ Công cụ dữ liệu ở bất kỳ mục nào trong website cafef.vn<br />
                <b>- &nbsp;&nbsp;“Cộng đồng” nhất:</b> Bạn tìm ra cách lọc cổ phiếu hiệu quả và muốn chia sẻ nó với bạn mình? CafeF giúp Bạn lưu giữ cách lọc cổ phiếu cũng như gửi nó cho bạn bè chỉ với một đường link.<br />
                <b>- &nbsp;&nbsp;Thiết kế thân thiện nhất:</b> Bộ Công cụ dữ liệu được thiết kế để bạn có thể tìm kiếm được nhiều thông tin nhất trong thời gian ngắn nhất, chúng tôi tính tới cả chuyện bạn phải di chuột bao xa và mắt phải liếc trung bình mấy lần để tìm thông tin.
            </span></p>
            
            <p class="helpstyle3" style="margin-left: 0.5in; text-indent: -0.25in;">
            <span style="font-family: Symbol;"><img width="13" height="13" alt="*" src="http://cafef3.vcmedia.vn/help/Help_moi_cho_portfolio_files/image001.gif" />
            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <b><span>
                <a name="3"></a>3. Bộ Công cụ dữ liệu của CafeF có những tính năng gì?
            </span></b></p>
            <p class="helpstyle3"><span>
                <b>- &nbsp;&nbsp;Bộ lọc cổ phiếu: </b> với gần 30 tiêu chí khác nhau tập trung vào các chỉ tiêu cơ bản của công ty cùng những biến động về giá và khối lượng, Bạn sẽ tìm được cổ phiếu thỏa mãn nhu cầu đầu tư của mình trong không quá 3s. <br />
                <b>- &nbsp;&nbsp;Thống kê biến động giá cổ phiếu: </b> thống kê những cổ phiếu tăng/giảm giá mạnh nhất trong thời gian qua, giúp Bạn nhanh chóng nhận ra cổ phiếu nào đột biến về khối lượng giao dịch.<br />
                <b>- &nbsp;&nbsp;Tra cứu dữ liệu lịch sử: </b> bao gồm toàn bộ các dữ liệu về lịch sử giá, thống kê đặt lệnh, giao dịch nhà đầu tư nước ngoài, giao dịch của cổ đông lớn và cổ đông nội bộ và giao dịch cổ phiếu quỹ được CafeF tập hợp từ khi cổ phiếu mới lên sàn.<br />
                <b>- &nbsp;&nbsp;Dữ liệu doanh nghiệp:</b> từ các dữ liệu phổ biến như EPS, P/E, kết quả kinh doanh, tin tức sự kiện tới các dữ liệu riêng có của CafeF như tỷ lệ góp vốn vào Công ty con và Công ty liên kết, ban lãnh đạo và tỷ lệ sở hữu, kế hoạch kinh doanh 2010 cùng báo cáo phân tích của nhiều công ty chứng khoán, Bạn sẽ tìm thấy tất cả chỉ sau một cú click.<br />
            </span></p>
            
            <p class="helpstyle3" style="margin-left: 0.5in; text-indent: -0.25in;">
            <span style="font-family: Symbol;"><img width="13" height="13" alt="*" src="http://cafef3.vcmedia.vn/help/Help_moi_cho_portfolio_files/image001.gif" />
            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <b><span>
                <a name="4"></a>4. Tôi tìm bộ Công cụ dữ liệu ở đâu?
            </span></b></p>
            <p class="helpstyle3"><span>
                Bạn có thể tìm bộ Công cụ dữ liệu ở mọi nơi trên website CafeF.vn
                <b>Ở trang chính:</b> Bạn <b>chọn mục Dữ liệu trên thanh công cụ đầu trang</b> để truy cập Bộ Công cụ dữ liệu hoàn chỉnh của CafeF. 
                Với thông tin <b>về từng mã chứng khoán,</b> Bạn gõ mã chứng khoán cần tìm vào ô tìm kiếm ở phía trên bên phải mỗi trang. Không chỉ tại trang chính, <b>bạn có thể thực hiện thao tác này tại cùng một vị trí tại mọi trang trên website CafeF.</b>
            </span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image002.png"  alt="" /></div>
            <p class="helpstyle3"><span><b>Ở trang dữ liệu của từng mã chứng khoán</b></span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image004.png"  alt="" /></div>
            <p class="helpstyle3"><span><b>Ở các trang tin tức:</b></span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image006.png"  alt="" /></div>
            <p class="helpstyle3"><span><b>Tại trang dữ liệu:</b> Toàn bộ các Công cụ dữ liệu có thể được truy cập từ khung bên phải. Ngoài ra, trong trang này bạn có thể truy cập đồ thị phân tích kỹ thuật hai chỉ số VN-Index và HNX-Index bằng cách click trực tiếp vào đồ thị</span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image008.png"  alt="" /></div>
            
            <p class="helpstyle3" style="margin-left: 0.5in; text-indent: -0.25in;">
            <span style="font-family: Symbol;"><img width="13" height="13" alt="*" src="http://cafef3.vcmedia.vn/help/Help_moi_cho_portfolio_files/image001.gif" />
            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <b><span>
                <a name="5"></a>5. Tôi sử dụng bộ lọc cổ phiếu để làm gì?
            </span></b></p>
            <p class="helpstyle3"><span>
              Bộ lọc cổ phiếu của CafeF với gần 30 tiêu chí khác nhau giúp nhà đầu tư <strong>trong vòng không quá 3s chọn lựa được những mã</strong> có các tiêu chí cơ bản và biến động giao dịch <strong>phù hợp nhất với chiến lược đầu tư của mình. </strong></span></p>
            
            <p class="helpstyle3" style="margin-left: 0.5in; text-indent: -0.25in;">
            <span style="font-family: Symbol;"><img width="13" height="13" alt="*" src="http://cafef3.vcmedia.vn/help/Help_moi_cho_portfolio_files/image001.gif" />
            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <b><span>
                <a name="6"></a>6. Bộ lọc cổ phiếu bao gồm những tiêu chí nào?
            </span></b></p>
            <p class="helpstyle3"><span>
                <i>Bộ lọc cổ phiếu được chia thành 4 nhóm tiêu chí chính là: Biến động giá, Biến động khối lượng, Định giá và Tài chính.</i>
                <div style="padding-left:20px;padding-bottom:10px;padding-top:10px">
                <b>- &nbsp;&nbsp;Biến động giá: </b>các tiêu chí đo lường mức độ biến động của giá cổ phiếu trong các phiên gần đây như: Thay đổi giá so với giá bình quân 5 phiên trước, Thay đổi giá so với giá bình quân 20 phiên trước, Thay đổi giá so với 5 phiên trước, Thay đổi giá so với 20 phiên trước và Vốn hóa thị trường. <br />
                <b>- &nbsp;&nbsp;Biến động khối lượng: </b>các tiêu chí đo lường mức độ biến động của khối lượng giao dịch trong các phiên gần đây như: Bình quân khối lượng giao dịch 5 phiên gần đây nhất, Bình quân khối lượng giao dịch 10 phiên gần đây nhất, Thay đổi khối lượng giao dịch so với bình quân khối lượng giao dịch 5 phiên trước, Thay đổi khối lượng giao dịch so với bình quân khối lượng giao dịch 10 phiên trước.<br />
                <b>- &nbsp;&nbsp;Định giá: </b>các tiêu chí phục vụ cho việc định giá doanh nghiệp như: EPS, P/E, P/B, Hệ số Beta, Kế hoạch trả cổ tức bằng tiền, Kế hoạch trả cổ tức bằng cổ phiếu.<br />
                <b>- &nbsp;&nbsp;Tài chính:</b>các chỉ tiêu tài chính cơ bản của một cổ phiếu như Tỷ lệ hoàn thành kế hoạch lợi nhuận sau thuế 2010, ROA, ROE.<br />
                </div>
                <i>Ngoài ra, Bạn có thêm hai tiêu chí nữa là lọc theo <strong>Sàn giao dịch</strong> và<strong> Lọc theo ngành nghề</strong>. Phân ngành của CafeF tuân theo chuẩn của Dow Jones, đảm bảo tính chính xác và chuyên nghiệp cao nhất cho nhà đầu tư.</i>
            </span></p>
            
            <p class="helpstyle3" style="margin-left: 0.5in; text-indent: -0.25in;">
            <span style="font-family: Symbol;"><img width="13" height="13" alt="*" src="http://cafef3.vcmedia.vn/help/Help_moi_cho_portfolio_files/image001.gif" />
            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <b><span>
                <a name="7"></a>7. Tôi sử dụng bộ lọc cổ phiếu như thế nào?
            </span></b></p>
            <p class="helpstyle3"><span>
                <b>Rất đơn giản, với mỗi tiêu chí, bạn có 2 cách để lựa chọn khoảng giá trị cần lọc:</b>
            </span></p>
            <p class="helpstyle3"><span>
              <strong>Thứ nhất, kéo và thả</strong> hai cột ở hai bên để xác định khoảng giá trị cần lọc </span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image010.png"  alt="" /></div>
            <p class="helpstyle3"><span><b>Thứ hai, gõ trực tiếp</b> khoảng giá trị cần lọc vào 2 ô Min và Max.</span></p>
            <p class="helpstyle3"><span>Ví dụ như trong hình trên, gõ trực tiếp cận dưới 10 nghìn tỷ đồng vào tiêu chí lọc Vốn hóa thị trường. </span></p>
            <p class="helpstyle3"><span><b>CafeF lấy giá trị mặc định của Min và Max ở đâu? </b> Giá trị Min và Max mặc định là giá trị nhỏ nhất và lớn nhất của thị trường sau khi đóng cửa phiên hôm đó. Ví dụ như trong tiêu chí Vốn hóa thị trường ở hình trên, giá trị Max 47,61 nghìn tỷ đồng là giá trị vốn hóa của cổ phiếu VCB đóng cửa phiên ngày 06/10/2010.</span></p>
            <p class="helpstyle3"><span><b>Sau khi chọn khoảng cần lọc, Bạn sẽ mất không quá 3s để có được kết quả mình cần.</b></span></p>
            <p class="helpstyle3"><span>Sau khi có kết quả lọc, bạn có thể <strong>sắp xếp kết quả lọc theo từng tiêu chí</strong>. Chỉ cần <strong>click vào tiêu chí cần sắp xếp 1</strong> lần để sắp xếp từ cao đến thấp, click thêm 1 lần nữa, hệ thống sẽ đảo ngược thứ tự và sắp xếp từ thấp đến cao.</span></p>
            <p class="helpstyle3"><span>Khi kết quả và thứ tự lọc đã ưng ý, bạn có thể<strong> trích xuất kết quả thu được ra file Excel</strong> đơn giản chỉ bằng một cú click chuột vào <strong>ô phía trên bên phải</strong> (như hình vẽ).</span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image012.png"  alt="" /></div>
            <p class="helpstyle3"><span>
                <strong>Sau khi kéo và thả để có khoảng giá trị cần lọc, Bạn muốn hai cận trên/dưới của bộ lọc quay trở lại giá trị Min, Max</strong> nhưng lại có quá nhiều tiêu chí, nếu lần lượt kéo và thả sẽ rất mất thời gian. Chúng tôi đã thiết kế để chỉ cần một cú click, Bạn có thể đưa tất cả các cận trên/dưới trong bộ lọc quay trở lại giá trị Min, Max: đó là ô <span style="color:Blue">Lấy giá trị mặc định</span>
            </span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image102.png"  alt="" /></div>
            <p class="helpstyle3"><span>
                Sau khi click vào ô <span style="color:Blue">Lấy giá trị mặc định</span> , các cận trên/dưới sẽ tự động di chuyển theo chiều mũi tên về các giá trị Min, Max
            </span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image103.png"  alt="" /></div>
            
            <p class="helpstyle3" style="margin-left: 0.5in; text-indent: -0.25in;">
            <span style="font-family: Symbol;"><img width="13" height="13" alt="*" src="http://cafef3.vcmedia.vn/help/Help_moi_cho_portfolio_files/image001.gif" />
            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <b><span>
                <a name="8"></a>8. Tôi muốn sử dụng thêm các tiêu chí khác?
            </span></b></p>
            <p class="helpstyle3"><span>
              <strong>Bước 1:</strong> Chọn mục <strong>“Thêm chỉ tiêu” ở phía dưới bên trái bộ lọc </strong></span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image014.png"  alt="" /></div>
            <p class="helpstyle3"><span>
              <strong>Bước 2: Chọn các chỉ tiêu cần dùng</strong>. Chỉ tiêu nào đã xuất hiện trong bộ lọc được bôi màu xám </span></p>
            <p class="helpstyle3"><span>
                <strong>Bước 3: Click</strong> <img src="http://cafef3.vcmedia.vn/help/screenerImg/image016.png" alt="" /> để bổ sung chỉ tiêu đó vào bộ lọc
            </span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image018.png"  alt="" /></div>
            <p class="helpstyle3"><span>
                 <b>CafeF đã thiết kế sẵn giúp bạn một Bộ lọc mặc định gồm 6 tiêu chí thường được nhà đầu tư quan tâm nhất khi tiến hành lọc tìm cổ phiếu:</b>
            </span></p>
            <div style="padding-left:20px;">
                -	Thay đổi giá so với 5 phiên trước (%)<br />
                -	Vốn hóa thị trường (tỷ đồng)<br />
                -	Thay đổi khối lượng giao dịch so với bình quân khối lượng giao dịch 5 phiên trước (%)<br />
                -	EPS<br />
                -	P/E<br />
                -	Hệ số Beta<br />
            </div>
            <p class="helpstyle3"><span>
                 Sau khi thay đổi Bộ lọc theo nhu cầu sử dụng của mình và <strong>muốn quay trở lại bộ lọc mặc định</strong> của CafeF, Bạn chỉ cần <strong>click vào ô</strong> <span style="color:Blue;font-weight:bold;">Sử dụng bộ chỉ tiêu mặc định</span>
            </span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image100.png"  alt="" /></div>
            <p class="helpstyle3"><span>
                 <strong>Sau khi bạn Click vào ô <span style="color:Blue;font-weight:bold;">Sử dụng bộ chỉ tiêu mặc định</span>, hệ thống sẽ đưa bạn quay lại Bộ chỉ tiêu mặc định của CafeF:
              </strong></span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image101.png"  alt="" /></div>
            
            <p class="helpstyle3" style="margin-left: 0.5in; text-indent: -0.25in;">
            <span style="font-family: Symbol;"><img width="13" height="13" alt="*" src="http://cafef3.vcmedia.vn/help/Help_moi_cho_portfolio_files/image001.gif" />
            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <b><span>
                <a name="9"></a>9. Nhiều chỉ tiêu quá, tôi muốn bỏ bớt đi?
            </span></b></p>
            <p class="helpstyle3"><span>
                 Bạn chỉ cần <strong>click vào ô <img src="http://cafef3.vcmedia.vn/help/screenerImg/image020.png" alt="" /> ở bên phải mỗi tiêu chí
              </strong></span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image022.png"  alt="" /></div>
            
            <p class="helpstyle3" style="margin-left: 0.5in; text-indent: -0.25in;">
            <span style="font-family: Symbol;"><img width="13" height="13" alt="*" src="http://cafef3.vcmedia.vn/help/Help_moi_cho_portfolio_files/image001.gif" />
            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <b><span>
                <a name="10"></a>10. Tôi chẳng hiểu những tiêu chí này có ý nghĩa gì cả!
            </span></b></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image024.png"  alt="" /></div>
            <p class="helpstyle3"><span>
                 Ý nghĩa của từng tiêu chí đều được giải thích tỉ mỉ và rõ ràng. Bạn có thể tìm đọc ở phần Thêm chỉ tiêu khi click vào mỗi tiêu chí. Với những tiêu chí đã được chọn đưa vào bộ lọc, chỉ cần đưa con trỏ tới chữ <img src="http://cafef3.vcmedia.vn/help/screenerImg/image026.png" alt="" /> ngay phía sau tên mỗi tiêu chí, Bạn sẽ thấy hiện lên một bảng màu vàng (xem hình) giải thích ý nghĩa của tiêu chí đó.
            </span></p>
            
            <p class="helpstyle3" style="margin-left: 0.5in; text-indent: -0.25in;">
            <span style="font-family: Symbol;"><img width="13" height="13" alt="*" src="http://cafef3.vcmedia.vn/help/Help_moi_cho_portfolio_files/image001.gif" />
            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <b><span>
                <a name="11"></a>11. Tôi có thể lưu lại bộ lọc mình đã dùng và gửi cho bạn bè được chứ?
            </span></b></p>
            <p class="helpstyle3"><span>
                Hoàn toàn có thể! Đây cũng là điểm khác biệt giữa bộ lọc cổ phiếu của CafeF với các bộ lọc khác.
            </span></p>
            <p class="helpstyle3"><span>
              Nếu muốn <strong>chia sẻ bộ lọc mình đã xây dựng</strong>, chỉ cần <strong>copy đường link và gửi cho bạn bè! </strong></span></p>
            <p class="helpstyle3"><span>
                Đây là đường link khi toàn bộ các tiêu chí đều được sử dụng
            </span></p>
            <p class="helpstyle3"><span>
                <a href="http://cafef.vn/screener.aspx#center=-1&cate=-1&char=&criterion=12;7;18;17;20;27;4;5;8;13;14;16;19;21;22;23;24&min=24.37038;-26.48;3.288468;0.0339;-0.5162674;0.5659182;-21.66;-38.26;-50.5;2;10;-99.83598;0.0042;5;0.12;0.0045;0.0072&max=47605.37;42.85;1087.74;53.631;1.89627;113.7846;25;39.06;74.26;2178914;3755191;2677.778;12.6787;100;60;118.2649;219.0942&size=20&page=1&order=12&dir=desc">
                    http://cafef.vn/screener.aspx#center=-1cate=-1char=criterion=12;7;18;17;20;27;4;5;8;13;14;16;19;21;22;23;24min=24.37038;-26.48;3<br />
                    .288468;0.0339;-0.5162674;0.5659182;-21.66;-38.26;-50.5;2;10;-99.83598;0.0042;5;0.12;0.0045;0.0072max=47605.37;42.85;1087.74;53.631;1<br />
                    .89627;113.7846;25;39.06;74.26;2178914;3755191;2677.778;12.6787;100;60;118.2649;219.0942size=20page=1order=12dir=desc<br />
                </a>
            </span></p>
            
            <p class="helpstyle3" style="margin-left: 0.5in; text-indent: -0.25in;">
            <span style="font-family: Symbol;"><img width="13" height="13" alt="*" src="http://cafef3.vcmedia.vn/help/Help_moi_cho_portfolio_files/image001.gif" />
            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <b><span>
                <a name="12"></a>12. Tôi dùng bộ Thống kê biến động giá cổ phiếu để làm gì?
            </span></b></p>
            <p class="helpstyle3"><span>
                <a href="http://cafef.vn/thong-ke.chn">
                    Bộ Thống kế biến động giá cổ phiếu
                </a> có hai chức năng chính:
            </span></p>
            <div style="padding-left:20px;padding-bottom:10px;padding-top:10px">
                <b>- &nbsp;&nbsp;Sắp xếp các cổ phiếu theo thứ tự tăng/giảm giá </b>trong một khoảng thời gian nhất định tính từ phiên giao dịch gần nhất (1 tuần, 2 tuần, 1 tháng, 3 tháng, 6 tháng, 1 năm). <br />
                <b>- &nbsp;&nbsp;Phát hiện các cổ phiếu có khối lượng giao dịch đột biến </b>
            </div>
            
            <p class="helpstyle3" style="margin-left: 0.5in; text-indent: -0.25in;">
            <span style="font-family: Symbol;"><img width="13" height="13" alt="*" src="http://cafef3.vcmedia.vn/help/Help_moi_cho_portfolio_files/image001.gif" />
            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <b><span>
                <a name="13"></a>13. Tôi sử dụng bộ Thống kê biến động giá cổ phiếu như thế nào?
            </span></b></p>
            <p class="helpstyle3"><span>
              Bạn có thể<strong> chọn cách sắp xếp cổ phiếu theo thứ tự tăng/giảm giá và chọn khoảng thời gian</strong> tính từ phiên giao dịch gần nhất. </span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image030.png"  alt="" /></div>
            <p class="helpstyle3"><span>
              Nhanh chóng sử dụng các chỉ tiêu về khối lượng để phát hiện các cổ phiếu có khối lượng giao dịch đột biến. Số liệu về <strong>khối lượng giao dịch trung bình 5 và 20 phiên trước nằm ở cột thứ 4 và 5 từ bên phải, còn So sánh khối lượng giao dịch hôm nay với phiên hôm trước, trung bình 5 và 20 phiên trước ở các cột 2, 3, 4 tính từ bên trái. </strong></span></p>
            <p class="helpstyle3"><span>
                Chi tiết khối lượng giao dịch các phiên gần đây được mô hình hóa để nhà đầu tư nhận diện được nhanh chóng nhất bất kỳ một sự biến động bất thường nào về khối lượng giao dịch.
            </span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image032.png"  alt="" /></div>
            
            <p class="helpstyle3" style="margin-left: 0.5in; text-indent: -0.25in;">
            <span style="font-family: Symbol;"><img width="13" height="13" alt="*" src="http://cafef3.vcmedia.vn/help/Help_moi_cho_portfolio_files/image001.gif" />
            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <b><span>
                <a name="14"></a>14. CafeF có những dữ liệu gì trong phần Tra cứu dữ liệu lịch sử?
            </span></b></p>
            <p class="helpstyle3"><span>
                <a href="http://cafef.vn/Lich-su-giao-dich-Symbol-VNINDEX/trang-1-0-tab-3-d1-07_09_2010-d2-07_10_2010.chn">Dữ liệu lịch sử của CafeF</a> được chia thành 5 phần chính, bao phủ hầu hết những chủ đề được nhà đầu tư quan tâm:
            </span></p>
            <b>- &nbsp;&nbsp;<a href="http://cafef.vn/Lich-su-giao-dich-Symbol-VNINDEX/trang-1-0-tab-1-d1-07_09_2010-d2-07_10_2010.chn">Lịch sử giá</a> </b>toàn bộ các dữ liệu về giá và khối lượng của một cổ phiếu trong từng phiên như khối lượng giao dịch, giá trị giao dịch, giá mở cửa, giá đóng cửa, giá cao nhất, giá thấp nhất, … Ngoài ra, CafeF là website duy nhất cung cấp khối lượng giao dịch các mã cổ phiếu theo từng đợt khớp lệnh. <br />
            <b>- &nbsp;&nbsp;<a href="http://cafef.vn/Lich-su-giao-dich-Symbol-VNINDEX/trang-1-0-tab-2-d1-07_09_2010-d2-07_10_2010.chn">Thống kê đặt lệnh</a> </b>Bạn có thể tìm thấy dễ dàng số Dư mua, Dư bán, Khối lượng đặt mua, đặt bán, Khối lượng trung bình một lệnh mua, một lệnh bán cũng như Chênh lệch khối lượng đặt mua – đặt bán để có được đánh giá chính xác nhất về tình hình cung – cầu của một mã chứng khoán. <br />
            <b>- &nbsp;&nbsp;<a href="http://cafef.vn/Lich-su-giao-dich-Symbol-VNINDEX/trang-1-0-tab-3-d1-07_09_2010-d2-07_10_2010.chn">Giao dịch nhà đầu tư nước ngoài</a> </b>Khối lượng, Giá trị mua/bán; Khối lượng, Giá trị mua/bán ròng. Bạn chỉ có thể tìm thấy dữ liệu về số room còn lại của nhà đầu tư nước ngoài cũng như tỷ lệ sở hữu của họ sau từng phiên tại CafeF. <br />
            <b>- &nbsp;&nbsp;<a href="http://cafef.vn/Lich-su-giao-dich-Symbol-CII/Trang-1-0-tab-4.chn">Giao dịch cổ đông lớn và cổ đông nội bộ</a> </b>bất kỳ thông báo nào về giao dịch của cổ đông lớn và cổ đông nội bộ đều được lưu trữ trong mục này, bao gồm cả ngày đăng ký/thực hiện và số lượng cổ phiếu sở hữu trước và sau giao dịch. <br />
            <b>- &nbsp;&nbsp;<a href="http://cafef.vn/Lich-su-giao-dich-Symbol-VSH/Trang-1-0-tab-5.chn">Giao dịch cổ phiếu quỹ</a> </b>các giao dịch mua/bán cổ phiếu quỹ của công ty niêm yết (dữ liệu có đầu năm 2009). <br />
            <p class="helpstyle3"><span>
                <b>Bạn có thể tra cứu 5 phần này trên thanh tác vụ của mục Tra cứu dữ liệu lịch sử:</b>
            </span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image034.png"  alt="" /></div>
            
            <p class="helpstyle3" style="margin-left: 0.5in; text-indent: -0.25in;">
            <span style="font-family: Symbol;"><img width="13" height="13" alt="*" src="http://cafef3.vcmedia.vn/help/Help_moi_cho_portfolio_files/image001.gif" />
            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <b><span>
                <a name="15"></a>15. Tôi muốn tra cứu dữ liệu lịch sử của mã chứng khoán khác?
            </span></b></p>
            <p class="helpstyle3"><span>
              Bạn <strong>gõ mã chứng khoán cần tra cứu vào ô Mã,</strong> hệ thống sẽ tự động gợi ý cho bạn theo vần ABC (hình) </span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image036.png"  alt="" /></div>
            <p class="helpstyle3"><span>
              Nếu bạn không nhớ được mã chứng khoán mà chỉ nhớ một phần tên công ty, <strong>hệ thống sẽ gợi ý cho bạn các mã chứng khoán cần tìm</strong>. Ví dụ: bạn gõ chữ<strong> Kim Khí</strong>, hệ thống sẽ gợi ý cho bạn 3 Mã chứng khoán có chữ Kim Khí trong tên công ty là KKC, HMC và KMT </span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image038.png"  alt="" /></div>
            
            <p class="helpstyle3" style="margin-left: 0.5in; text-indent: -0.25in;">
            <span style="font-family: Symbol;"><img width="13" height="13" alt="*" src="http://cafef3.vcmedia.vn/help/Help_moi_cho_portfolio_files/image001.gif" />
            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <b><span>
                <a name="16"></a>16. Tôi muốn thay đổi thời gian tra cứu dữ liệu lịch sử?
            </span></b></p>
            <p class="helpstyle3"><span>
              Có<strong> 2 cách</strong> để Bạn thay đổi thời gian tra cứu </span></p>
            <p class="helpstyle3"><span>
                <strong>Cách thứ nhất là gõ trực tiếp khoảng thời gian tra cứu</strong>. Ví dụ Bạn muốn tìm dữ liệu từ ngày 01/06/2010 đến ngày 07/10/2010, Bạn chỉ cần gõ 01/06/2010 vào ô <img src="http://cafef3.vcmedia.vn/help/screenerImg/image040.png" alt="" /> và gõ 07/10/2010 vào ô  <img src="http://cafef3.vcmedia.vn/help/screenerImg/image042.png" alt="" />
            </span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image044.png"  alt="" /></div>
            <p class="helpstyle3"><span>
                <strong>Cách thứ hai là click vào biểu tượng</strong> <img src="http://cafef3.vcmedia.vn/help/screenerImg/image046.png" alt="" /> để chọn ngày tháng. Sau đó Bạn chọn ngày tháng bắt đầu/kết thúc cho khoảng thời gian mình cần tra cứu trong tờ lịch sẽ hiện ra sau đó.
            </span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image048.png"  alt="" /></div>
            <p class="helpstyle3"><span>
                Cuối cùng Bạn <strong>click vào</strong> <img src="http://cafef3.vcmedia.vn/help/screenerImg/image050.png" alt="" /> để tra cứu dữ liệu lịch sử.
            </span></p>
            
            <p class="helpstyle3" style="margin-left: 0.5in; text-indent: -0.25in;">
            <span style="font-family: Symbol;"><img width="13" height="13" alt="*" src="http://cafef3.vcmedia.vn/help/Help_moi_cho_portfolio_files/image001.gif" />
            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <b><span>
                <a name="17"></a>17. Tôi muốn biết cổ đông lớn/cổ đông nội bộ đó đã thực hiện những giao dịch nào?
            </span></b></p>
            <p class="helpstyle3"><span>
              Sau khi tra cứu các giao dịch của cổ đông lớn/cổ đông nội bộ, nếu bạn muốn biết cổ đông đó đã thực hiện những giao dịch nào thuộc diện phải công bố thông tin (có thể với nhiều cổ phiếu khác nhau), Bạn chỉ cần <strong>click trực tiếp vào tên cổ đông</strong> (hình). </span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image052.png"  alt="" /></div>
            <p class="helpstyle3"><span>
                Hệ thống sẽ hiển thị tất cả các giao dịch của tổ chức này đối với nhiều cổ phiếu khác nhau.
            </span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image054.png"  alt="" /></div>
            <p class="helpstyle3"><span>
              Nếu Bạn <strong>chỉ nhớ tên </strong>của một tổ chức/cá nhân và muốn tra cứu xem tổ chức/cá nhân này đã tiến hành những giao dịch gì, ví dụ như bạn chỉ nhớ cần tra cứu một quỹ nào đó có chữ Vietnam trong tên, Bạn chỉ cần gõ vào <strong>ô Xem giao dịch của tổ chức/cá nhân</strong> (hình). Hệ thống sẽ đưa ra gợi ý các tổ chức có chữ Vietnam trong tên. Sau khi đã chọn tổ chức mình cần,<strong> Bạn bấm <img src="http://cafef3.vcmedia.vn/help/screenerImg/image056.png" alt="" /> hoặc <img src="http://cafef3.vcmedia.vn/help/screenerImg/image050.png" alt="" /> để tiến hành tra cứu</strong> (chức năng hoàn toàn giống nhau). </span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image058.png"  alt="" /></div>
            
            <p class="helpstyle3" style="margin-left: 0.5in; text-indent: -0.25in;">
            <span style="font-family: Symbol;"><img width="13" height="13" alt="*" src="http://cafef3.vcmedia.vn/help/Help_moi_cho_portfolio_files/image001.gif" />
            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <b><span>
                <a name="18"></a>18. CafeF có những dữ liệu gì đối với một cổ phiếu?
            </span></b></p>
            <p class="helpstyle3"><span>
                CafeF có toàn bộ những dữ liệu mà bạn cần để tiến hành phân tích một mã cổ phiếu:
            </span></p>
            <div style="padding-left:20px;padding-bottom:10px;padding-top:10px">
                - &nbsp;<strong>&nbsp;Kết quả giao dịch ngày hôm đó: </strong>Giá đóng/mở cửa, Giá cao/thấp nhất, Khối lượng giao dịch và Giao dịch của nhà đầu tư nước ngoài. <br />
                <b>- &nbsp;</b><strong>&nbsp;Đồ thị kỹ thuật:</strong> Bạn có thể click vào đồ thị để sử dụng chức năng đồ thị nâng cao.<br /> 
                - &nbsp;&nbsp;<strong>Các dữ liệu cơ bản dùng để định giá cổ phiếu: </strong>EPS, P/E, Giá trị sổ sách/cổ phiếu, Khối lượng cổ phiếu đang lưu hành, Vốn hóa thị trường, …<br />
                - &nbsp;&nbsp;<strong>Tin tức – Sự kiện:</strong> các tin liên quan đến cổ phiếu được phân thành nhiều loại tin như Trả cổ tức – Chốt quyền, Tình hình sản xuất kinh doanh và Phân tích khác, …<br />
                - &nbsp;&nbsp;<strong>Dữ liệu lịch sử giao dịch:</strong> lịch sử giao dịch các phiên gần đây, Bạn có thể xem toàn bộ dữ liệu lịch sử khi click vào <img src="http://cafef3.vcmedia.vn/help/screenerImg/image060.png" alt="" /><br />
                - &nbsp;<strong>&nbsp;Báo cáo phân tích: </strong>báo cáo của các công ty chứng khoán có liên quan tới cổ phiếu<br />
                - &nbsp;&nbsp;<strong>Công ty cùng ngành:</strong> các công ty trong cùng một ngành được phân theo chuẩn Dow Jones, đảm bảo tính chuyên nghiệp và chính xác.<br />
                - &nbsp;<strong>&nbsp;Kế hoạch kinh doanh năm nay: </strong>kế hoạch của công ty về doanh thu, lợi nhuận và cổ tức, Bạn có thể <img src="http://cafef3.vcmedia.vn/help/screenerImg/image060.png" alt="" />  bằng cách click vào biểu tượng trên<br />
                - &nbsp;<strong>&nbsp;Các công ty có EPS và P/E tương đương</strong><br />
              - &nbsp;<strong>&nbsp;Hồ sơ công ty:</strong> bao gồm Thông tin tài chính theo quý/theo năm, Thông tin chung về doanh nghiệp, Ban lãnh đạo và sở hữu và khoản đầu tư và các Công ty con/Công ty liên kết. </div>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image062.png"  alt="" /></div>
            
            <p class="helpstyle3" style="margin-left: 0.5in; text-indent: -0.25in;">
            <span style="font-family: Symbol;"><img width="13" height="13" alt="*" src="http://cafef3.vcmedia.vn/help/Help_moi_cho_portfolio_files/image001.gif" />
            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <b><span>
                <a name="19"></a>19. Tôi muốn tải xuống Báo cáo tài chính và Báo cáo khác?
            </span></b></p>
            <p class="helpstyle3"><span>
                <strong>Bước 1: Bạn gõ mã chứng khoán cần tìm vào ô</strong> <img src="http://cafef3.vcmedia.vn/help/screenerImg/image064.png" alt="" />
            </span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image066.png"  alt="" /></div>
            <p class="helpstyle3"><span>
              <strong>Bước 2</strong>: Bạn <strong>click vào chữ Tải xuống Báo cáo tài chính và Báo cáo khác</strong> trong trang dữ liệu doanh nghiệp (<strong>việc tải xuống là hoàn toàn miễn phí</strong>) </span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image068.png"  alt="" /></div>
            <p class="helpstyle3"><span>
              <strong>Bước 3: Chọn tải xuống Báo cáo Bạn cần </strong></span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image070.png"  alt="" /></div>
            
            <p class="helpstyle3" style="margin-left: 0.5in; text-indent: -0.25in;">
            <span style="font-family: Symbol;"><img width="13" height="13" alt="*" src="http://cafef3.vcmedia.vn/help/Help_moi_cho_portfolio_files/image001.gif" />
            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <b><span>
                <a name="20"></a>20. Tôi muốn biết Ban lãnh đạo của Công ty mình đang đầu tư là những ai? Công ty ấy có những cổ đông lớn nào?
            </span></b></p>
            <p class="helpstyle3"><span>
                Bước 1: Bạn <strong>gõ mã chứng khoán cần tìm vào ô</strong> <img src="http://cafef3.vcmedia.vn/help/screenerImg/image064.png" alt="" />
            </span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image066.png"  alt="" /></div>
            <p class="helpstyle3"><span>
              Bước 2: <strong>Click vào ô Ban lãnh đạo và sở hữu</strong> trong trang dữ liệu doanh nghiệp </span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image073.gif"  alt="" /></div>
            <p class="helpstyle3"><span>
              <strong>Bước 3</strong>: Sau khi click vào ô Ban lãnh đạo và sở hữu, hệ thống sẽ<strong> tự động hiển thị tên và chức vụ các thành viên trong Hội đồng quản trị, Ban Giám đốc và Ban Kiểm soát của Công ty</strong>. Nếu muốn biết thêm Công ty có những Cổ đông lớn nào, bạn click vào ô Cổ đông lớn (hình) </span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image074.png"  alt="" /></div>
            <p class="helpstyle3"><span>
              <strong>Cuối cùng, Bạn sẽ có danh sách các cổ đông lớn, số cổ phiếu họ nắm giữ và tỷ lệ nắm giữ.</strong> </span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image076.png"  alt="" /></div>
            
            <p class="helpstyle3" style="margin-left: 0.5in; text-indent: -0.25in;">
            <span style="font-family: Symbol;"><img width="13" height="13" alt="*" src="http://cafef3.vcmedia.vn/help/Help_moi_cho_portfolio_files/image001.gif" />
            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <b><span>
                <a name="21"></a>21. Công ty tôi đang đầu tư hiện có những Công ty con/Công ty liên kết nào?
            </span></b></p>
            <p class="helpstyle3"><span>
                <strong>Bước 1: Bạn gõ mã chứng khoán cần tìm vào ô</strong> <img src="http://cafef3.vcmedia.vn/help/screenerImg/image064.png" alt="" />
            </span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image066.png"  alt="" /></div>
            <p class="helpstyle3"><span>
              <strong>Bước 2: Click vào ô Cty con & Cty liên kết</strong> trong trang dữ liệu doanh nghiệp </span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image078.png"  alt="" /></div>
            <p class="helpstyle3"><span>
              <strong>Bạn sẽ có được dữ liệu Công ty mình đang đầu tư đã góp vốn vào những doanh nghiệp nào </strong></span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image080.png"  alt="" /></div>
            
            <p class="helpstyle3" style="margin-left: 0.5in; text-indent: -0.25in;">
            <span style="font-family: Symbol;"><img width="13" height="13" alt="*" src="http://cafef3.vcmedia.vn/help/Help_moi_cho_portfolio_files/image001.gif" />
            </span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <b><span>
                <a name="22"></a>22. CafeF có bộ lọc tin tức doanh nghiệp theo từng chủ đề không?
            </span></b></p>
            <p class="helpstyle3"><span>
                Nếu như ngay cả việc Bạn phải di chuột bao xa và liếc mắt mấy lần để tìm kiếm thông tin chúng tôi cũng tính tới thì đương nhiên một bộ lọc tin là không thể thiếu.
            </span></p>
            <p class="helpstyle3"><span>
              Ví dụ Bạn muốn tra cứu xem <strong>Công ty Cổ phần Viglacera Đông Anh</strong> có những <strong>thông tin</strong> gì liên quan tới việc<strong> trả cổ tức và chốt quyền, Bạn làm theo 3 bước sau:</strong></span></p>
            <p class="helpstyle3"><span>
                <strong>Bước 1:</strong> Bạn<strong> gõ mã chứng khoán cần tìm vào ô</strong> <img src="http://cafef3.vcmedia.vn/help/screenerImg/image064.png" alt="" />
            </span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image066.png"  alt="" /></div>
            <p class="helpstyle3"><span>
              <strong>Bước 2: Bạn tìm mục Tin tức và sự kiện ở chính giữa trang dữ liệu doanh nghiệp. Bộ lọc tin nằm ở cuối mục Tin tức và sự kiện </strong></span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image082.png"  alt="" /></div>
            <p class="helpstyle3"><span>
              <strong>Bước 3: Chọn mục Trả cổ tức – Chốt quyền</strong> </span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image084.png"  alt="" /></div>
            <p class="helpstyle3"><span>
              <strong>Hệ thống sẽ lọc cho Bạn toàn bộ những thông tin về Trả cổ tức – Chốt quyền của cổ phiếu DAC </strong></span></p>
            <div style="text-align:center"><img src="http://cafef3.vcmedia.vn/help/screenerImg/image086.png"  alt="" /></div>
            <div style="padding-top:10px; text-align:center;color:#C60000;font-size:20px;font-weight:bold">Chúc bạn luôn tìm được các thông tin và dữ liệu cần thiết từ bộ Cung cụ dữ liệu của CafeF !</div>
            
            </div>
        </div>
    </div>    
    <div class="cf_BoxFooter"><div></div></div>
</div>        
</asp:Content>

