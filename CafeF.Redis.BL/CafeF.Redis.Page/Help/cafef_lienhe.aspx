<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cafef_lienhe.aspx.cs" Inherits="CafeF.Redis.Page.Help.cafef_lienhe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>CafeF - Dịch vụ dữ liệu thông tin tài chính chuyên sâu | CafeF.vn</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link rel="stylesheet" href="http://cafef3.vcmedia.vn/images/cafefmobile/style.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="warp">
		<div class="main">
			<div class="mainLeft fl" align="right">
				<div class="leftLogo">
					<a href="http://cafef.vn/" target="_blank"><img src="http://cafef3.vcmedia.vn/images/cafefmobile/logo/left.png" alt="cafef.vn" /></a>
				</div>
				<div class="leftMenu">
					<ul>
						<li><a href="/help/cafef-mobile.htm"><span>CAFEF MOBILE</span></a></li>
						<li><a href="/help/cafef-huongdan.htm"><span>Hướng dẫn sử dụng</span></a></li>
						<li><a href="/help/cafef-lienhe.htm" class="actived"><span>Liên hệ giải đáp</span></a></li>
					</ul>
				</div>
			</div>
			<div class="mainRight fl">
				<div class="rightLogo">
					<a href="http://m.cafef.vn/" target="_blank" style="color:#C1272D; font-size:40px; font-family:Verdana; font-weight:bold; text-decoration:none">
					    m.cafef.vn
					</a>
					<%--<img src="http://cafef3.vcmedia.vn/images/cafefmobile/30ngay.png" alt="Miễn phí 30 ngày" align="middle" />--%>
				</div>
				<div class="contactContent">
					<div class="mobileTitle" style="padding-top:0;">
						LIÊN HỆ GIẢI ĐÁP<br />
						<span class="font12">Hãy liên lạc với chúng tôi để được giải đáp về dịch vụ</span>
					</div>
					<div class="contactSubContent">
						<div class="blue">
							<div class="bullet2">
								Số điện thoại chăm sóc khách hàng: 1900 561 501.<br />
								Hotline: 0984 537 369
							</div>
							
							<div class="bullet1 mT20">Email: cc@vccorp.vn hoặc info@cafef.vn</div>
						</div>
						<div class="mT20 pL10">
							<b>Nội dung phản hồi xin vui lòng gửi kèm các thông tin sau: </b><br />
							Loại máy điện thoại sử dụng.<br />
							Số điện thoại nhắn tin.<br />
							Thời gian nhắn tin.<br />
							Thắc mắc cần giải đáp.
						</div>
						<div class="mT20 pL10" style="font-size:17px;color:#C1272D;font-weight:bold">PHẢN HỒI TRỰC TUYẾN</div>
						<div class="mT20 pL10">
						    <table width="400px" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td style="" >
                                </td>
                                <td style="">
                                    <table>
                                        <tr>
                                            <td >
                                                </td>
                                            <td>
                                                <asp:Label ID="lblMessage" runat="server" Text="" BackColor="White" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td >
                                                Email</td>
                                            <td>
                                                <asp:TextBox ID="txtEmail" runat="server" Width="252px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >
                                                Số điện thoại</td>
                                            <td>
                                                <asp:TextBox ID="txtPhone" runat="server" Width="252px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >
                                                Tiêu đề</td>
                                            <td>
                                                <asp:TextBox ID="txtTile" runat="server" Width="252px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" >
                                                Nội dung</td>
                                            <td>
                                                <asp:TextBox ID="txtContent" runat="server" Height="82px" TextMode="MultiLine" Width="252px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td >
                                            </td>
                                            <td align="left">
                                                &nbsp;<asp:Button ID="btnSend" runat="server" OnClick="btnSend_Click" Text="Gửi phản hồi" /></td>
                                        </tr>
                                    </table>
                                </td>
                                <td">
                                </td>
                            </tr>
                        </table>
						</div>
						<div class="mT20 pL10">Chúng tôi sẽ trả lời bạn ngay khi nhận được yêu cầu!</div>
						
						<div class="mT20 pL10">Cám ơn bạn đã sử dụng dịch vụ tra cứu thông tin chứng khoán <a href="http://m.cafef.vn">m.cafef.vn</a>trên điện thoại di động của chúng tôi!</div>

					</div>
				</div>
				<div class="rightFooter">
					<div>Chat trên mobile mọi lúc mọi nơi</div>
				</div>
			</div>
			<div class="c"></div>
			<div class="footer">
				Sản phẩm được phát triển bới <a href="http://cafef.vn/" target="_blank">CafeF.vn</a> - <a href="http://cafef.vn/" target="_blank">VCmobile</a><br />
				Công ty truyền thông Việt Nam
			</div>
		</div>
	</div>
    </form>
</body>
</html>
