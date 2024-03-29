<%@ Page Language="C#" MasterPageFile="~/MasterPage/SoLieu.Master" AutoEventWireup="true"
    Title="Cách tính hệ số beta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 980px;" class="cf_WCBox">
        <div class="cf_BoxContent">
            <div style="width: 980px;" class="cf_WCBox">
                <div class="cf_BoxContent" style="text-align: left; padding: 20px 30px 20px 30px">
                    Hệ số rủi ro beta là hệ số đo lường mức độ biến động hay còn gọi là thước đo rủi ro hệ thống của một chứng khoán hay một danh mục đầu tư trong tương quan với toàn bộ thị trường. Beta được sử dụng trong mô hình định giá tài sản vốn (CAPM) để tính toán tỷ suất sinh lời kỳ vọng của một tài sản dựa vào hệ số beta của nó và tỷ suất sinh lời trên thị trường.<br /><br />
                    Nếu một chứng khoán có hệ số beta:
                    <br /><br />
                    &nbsp;&nbsp;&nbsp;+ Bằng 1, mức biến động của giá chứng khoán này sẽ bằng với mức biến động của thị trường.<br />
                    &nbsp;&nbsp;&nbsp;+ Nhỏ hơn 1, mức độ biến động của giá chứng khoán này thấp hơn mức biến động của
                    thị trường.<br />
                    &nbsp;&nbsp;&nbsp;+ Lớn hơn 1: mức độ biến động giá của chứng khoán này lớn hơn mức biến động của
                    thị trường.<br /><br />
                    Cụ thể hơn, nếu một chứng khoán có beta bằng 1,2 thì trên lý thuyết mức biến động của chứng khoán này sẽ cao hơn mức biến động chung của thị trường 20%.<br /><br />
                    Công thức tính hệ số beta:<br /><br />
                    <div style="width:980px; float:left; text-align:center;font-weight:bold">Beta = Covar(Ri,Rm)/Var(Rm)</div>
                    Trong đó:<br /><br />
                    &nbsp;&nbsp;&nbsp;• Ri : Tỷ suất sinh lời của chứng khoán.<br />
                    &nbsp;&nbsp;&nbsp;• Rm: Tỷ suất sinh lời của thị trường (ở đây là VN-Index).<br />
                    &nbsp;&nbsp;&nbsp;• Var(Rm): Phương sai của tỷ suất sinh lời của thị trường.<br />
                    &nbsp;&nbsp;&nbsp;• Covar(Ri,Rm): Hiệp phương sai của tỷ suất sinh lời của chứng khoán và tỷ suất sinh lời của thị trường.<br /><br />
                    Tỷ suất sinh lời được tính như sau:<br /><br />
                    <div style="float:left; width:980px; text-align:center; font-weight:bold">R = (p1-p0)/p0</div>
                    Trong đó:<br /><br />
                    &nbsp;&nbsp;&nbsp;• P1: giá đóng cửa điều chỉnh phiên đang xét.<br />
                    &nbsp;&nbsp;&nbsp;• P0: giá đóng cửa điều chỉnh phiên trước đó.<br /><br />
                    Hệ số beta của 1 chứng khoán trên webiste www.cafef.vn được tính dựa trên dữ liệu giao dịch 100 phiên liên tiếp gần thời điểm hiện tại nhất của chứng khoán đó.<br /><br />
Đối với những chứng khoán có số phiên giao dịch dưới 30: không tiến hành tính beta.<br /><br />
Đối với những chứng khoán có số phiên giao dịch từ 30 tới nhỏ hơn 100, beta được tính dựa trên dữ liệu từ khi chứng khoán bắt đầu giao dịch tới phiên giao dịch gần thời điểm hiện tại nhất.<br /><br />
Mọi góp ý, phản hồi về sự chính xác của công thức cũng như số liệu hiển thị trên website, vui lòng liên hệ qua email: info@cafef.vn.<br />

                </div>
            </div>
            
        </div>
    </div>
</asp:Content>
