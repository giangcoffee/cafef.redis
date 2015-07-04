<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TradeInfo.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.StockView.TradeInfo" %>

<script type="text/javascript">
 var IE = document.all?true:false
var folder = '<%= SymbolFolder %>'
var nB;
if (!IE) document.captureEvents(Event.MOUSEMOVE)
document.onmousemove = getMouseXY;
var tempX = 0
var tempY = 0
function getMouseXY(e) {
  if (IE) {
    tempX = event.clientX + document.body.scrollLeft
    tempY = event.clientY + document.body.scrollTop
  } else { 
    tempX = e.pageX
    tempY = e.pageY
  }  
  if (tempX < 0){tempX = 0}
  if (tempY < 0){tempY = 0}  
  return true
}
 function ShowDetailBox ()
{
    var divCompanyInfo = document.getElementById('divCompanyInfo');
    divCompanyInfo.style.display = 'block';
    divCompanyInfo.style.left = tempX;
}
function HideDetailBox ()
{
    var divCompanyInfo = document.getElementById('divCompanyInfo');
    divCompanyInfo.style.display = 'none';
}
function SetFirst()
{
    var sym =  '<%= __Symbol %>';
    var url = "http://cafef4.vcmedia.vn/" + folder + "/" + sym + "/6months.png";
    document.getElementById('imgChart').setAttribute('src',url);    
}
</script>
<input type="hidden" id="thuphat" />
<div class="dl-thongtin clearfix" style="padding-bottom:0;">
    <div class="dlt-left">
        <div class="dltl-update">
            <asp:Label runat="server" ID="lblStatus" CssClass="dltlu-time" style="color:#CC0000;padding:5px 0;" />
            <div class="dltlu-time"><asp:Literal runat="server" ID="ltrDateTime"></asp:Literal></div>
            <div class="dltlu-point" id="CP"><asp:Literal runat="server" ID="ltrCurentIndex"></asp:Literal></div>
            <span id="CC"><asp:Literal runat="server" ID="lblChange"></asp:Literal></span>
        </div>
        <div class="dltl-price">
            <ul>
                <li class="clearfix"><div class="l">Giá tham chiếu</div><div class="r" id="REF"><asp:Literal runat="server" ID="ltrBasicPrice"></asp:Literal></div></li>
                <li class="clearfix"><div class="l">Giá mở cửa</div><div class="r" id="OP"><asp:Literal runat="server" ID="ltrOpenPrice"></asp:Literal></div></li>
                <li class="clearfix"><div class="l">Giá cao nhất</div><div class="r" id="HI"><asp:Literal runat="server" ID="ltrHighestPrice"></asp:Literal></div></li>
                <li class="clearfix"><div class="l">Giá thấp nhất</div><div class="r" id="LO"><asp:Literal runat="server" ID="ltrLowerPrice"></asp:Literal></div></li>
                <li class="clearfix"><div class="l">Giá đóng cửa</div><div class="r" id="CO"><asp:Literal runat="server" ID="ltrClosePrice"></asp:Literal></div></li>
                <li class="clearfix"><div class="l">Khối lượng</div><div class="r" id="CV"><asp:Literal runat="server" ID="ltrKhoiLuong"></asp:Literal></div></li>
                <li class="clearfix"><div class="l"><asp:Literal runat="server" ID="ltrGDNNTitle" /></div><div class="r"<%-- id="FBS"--%>><asp:Literal runat="server" ID="ltrGDNN"></asp:Literal></div></li>
                <li class="clearfix"><div class="l">Room NN còn lại</div><div class="r" <%--id="FRE"--%>><asp:Literal runat="server" ID="ltrRoomNNConlai"></asp:Literal> (%)</div></li>
            </ul>
            <div class="dltlnote">Đơn vị giá: 1000 VNĐ<%--<br />(*)Đơn vị KL Room NN: 10.000 CP--%></div>
        </div>
        <div class="dltl-other">
            <asp:Panel runat="server" ID="pnCCQDate" Visible="false"><p style="color: red; font-weight:bold; margin-bottom: 3px;">Kỳ báo cáo tính tới ngày <asp:Literal runat="server" ID="ltrCCQDate" /></p></asp:Panel>
            <ul>
                <asp:Panel runat="server" ID="pnCCQ" Visible="false">
                    <li class="clearfix"><div class="l"><b>Thay đổi giá trị tài sản ròng (tỷ đồng):</b></div><div class="r"><asp:Literal runat="server" ID="ltrCCQv3" /></div></li>
                    <li class="clearfix"><div class="l"><b>Giá trị tài sản ròng/1 CCQ (nghìn đồng):</b></div><div class="r"><asp:Literal runat="server" ID="ltrCCQv6" /></div></li>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnEPS">
                <li class="clearfix"><div class="l"><b>(*) EPS 4 quý gần nhất</b> (nghìn đồng):</div><div class="r"><asp:Literal runat="server" ID="ltrEPS"></asp:Literal></div></li>
                <li class="clearfix"><div class="l"><b>P/E :</b></div><div class="r"><asp:Literal runat="server" ID="ltrPE"></asp:Literal></div></li>
                </asp:Panel>
                <li class="clearfix"><div class="l"><b>Giá trị sổ sách /cp</b> (nghìn đồng):</div><div class="r"><asp:Literal runat="server" ID="ltrPB"></asp:Literal></div></li>
                <li class="clearfix"><div class="l"><b>(**) Hệ số beta:</b></div><div class="r"><asp:Literal runat="server" ID="ltrCovarian"></asp:Literal></div></li>
                <li class="clearfix"><div class="l"><b>KLGD khớp lệnh trung bình 10 phiên:</b></div><div class="r"><asp:Literal runat="server" ID="ltrKLTrungBinh20Ngay"></asp:Literal></div></li>
                <li class="clearfix"><div class="l"><b>KLCP đang lưu hành:</b></div><div class="r"><asp:Literal runat="server" ID="ltrTotalCPLuuHanh"></asp:Literal></div></li>
                <li class="clearfix"><div class="l"><b>Vốn hóa thị trường</b> (tỷ đồng):</div><div class="r"><asp:Literal runat="server" ID="ltrMarketcap"></asp:Literal></div></li>
            </ul>
            <div class="dltlonote">
                <asp:Panel runat="server" ID="pnEPSNote" Visible="false" style="display:inline;">(*) <asp:Literal runat="server" ID="ltrEPSDate" /> | </asp:Panel><a href="/help/calcRate.html">Xem cách tính</a><br />(**) Hệ số beta tính với dữ liệu 100 phiên | <a href="/help/hesobeta.aspx">Xem cách tính</a>
            </div>
        </div>
    </div>
    <!-- // left -->
    <div class="dlt-right">
        <div class="dothi"><a href="/Thi-truong-niem-yet/Bieu-do-ky-thuat/EPS-<%=__Symbol%>-2.chn"><img src="" alt="" border="0" width="260" id="imgChart" /></a></div>
        <div class="dothi-note clearfix"><div class="dltr-l">Đồ thị vẽ theo giá điều chỉnh</div><div class="dltr-r">đv KLg: 10,000cp</div></div>
        <div class="dltr-time">
            <a id="lnkChart_7days" href="javascript:void(0)" onclick="ChangeImage('7days', 'lnkChart_7days');">
                1 tuần</a> | <a id="lnkChart_1month" href="javascript:void(0)" onclick="ChangeImage('1month', 'lnkChart_1month');">
                    1 tháng</a> | <a id="lnkChart_3months" href="javascript:void(0)" onclick="ChangeImage('3months', 'lnkChart_3months');">
                        3 tháng</a> | <a style="color: #CC0001;" id="lnkChart_6months" href="javascript:void(0)"
                            onclick="ChangeImage('6months', 'lnkChart_6months');">6 tháng</a>
            | <a id="lnkChart_1year" href="javascript:void(0)" onclick="ChangeImage('1year', 'lnkChart_1year');">
                1 năm</a>
            <br />
            <a href="/Thi-truong-niem-yet/Bieu-do-ky-thuat/EPS-<%=__Symbol%>-2.chn"><b>Xem đồ thị kỹ thuật</b></a>
        </div>
        <div class="dltr-other">
            <div style="float: right;" runat="server" id="divFirstInfo" class="tt"><a href="javascript:void(0);" style="text-decoration: none; color: rgb(153, 153, 153);">Chi tiết</a>
    <span class="tooltip">
        <span class="top"></span>
        <span class="middle">
            <asp:Literal runat="server" ID="ltrFirstInfo" />
        </span>
        <span class="bottom"></span>
    </span>
    </div>
            
            <div>Ngày giao dịch đầu tiên: <b><asp:Literal runat="server" ID="ltrNgayGiaoDich"></asp:Literal></b>
            </div>
            <div>Giá đóng cửa phiên GD đầu tiên(nghìn đồng): <b><asp:Literal runat="server" ID="ltrFirstPrice"></asp:Literal></b></div>
            <div>Khối lượng cổ phiếu niêm yết lần đầu: <b><asp:Literal runat="server" ID="ltrFirstVolume"></asp:Literal></b></div>
            <div class="tt" style="font-size: 11px; color: #000072; font-weight: bold; font-family: tahoma, verdana, arial;">
                <a style="font-size: 11px; cursor: pointer;" class="dangky">Lịch sử trả cổ tức và chia thưởng</a><div class="tooltip">
                        <div class="top"></div>
                        <div class="middle" style="padding-left: 10px; font-weight: normal"><asp:Literal runat="server" ID="ltrTraCoTuc"></asp:Literal></div>
                        <div class="bottom">(*)Ngày hiển thị là ngày GD không hưởng quyền</div>
                    </div>
            </div>
        </div>
    </div>
</div>

<script type="text/javascript">    
SetFirst();
function ChangeImage(id, lnk)
{
    document.getElementById('imgChart').setAttribute('src','http://cafef4.vcmedia.vn/' + folder + '/' + '<%=__Symbol%>' + '/'+id+'.png');    
    document.getElementById('lnkChart_7days').style.color = '#013567';
    document.getElementById('lnkChart_1month').style.color = '#013567';
    document.getElementById('lnkChart_3months').style.color = '#013567';
    document.getElementById('lnkChart_6months').style.color = '#013567';
    document.getElementById('lnkChart_1year').style.color = '#013567';    
    document.getElementById(lnk).style.color = '#CC0001';   
}
</script>

