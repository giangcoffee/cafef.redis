<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatePicker.ascx.cs" Inherits="CafeF.Redis.Page.UserControl.DatePicker.DatePicker" %>
<script language="javascript" type="text/javascript">
    function txtDatePicker_KeyDownHandler(e)
    {
        if (!e) e = window.event;
        
        if (e.keyCode == 13 || e.keyCode == 9)
        {
            var nextControl = document.getElementById('ctl00_ContentPlaceHolder1_txtTradeVolume');
	        if (nextControl)
	        {
	            //nextControl.select();
		        nextControl.focus();
	        }
            e.cancelBubble = true;
            e.returnValue=false;
            e.cancel = true;
        }
        
        return false;
    }
</script>
<!-- /CafeF-Tools/Controls/DatePicker/datepicker.js -->
<script type="text/javascript" src="http://cafef3.vcmedia.vn/styles/DatePicker/datepicker.js"></script>
<asp:TextBox ID="txtDatePicker" runat="server" Width="80px" MaxLength="10" OnInit="txtDatePicker_Init"  ></asp:TextBox> <img alt="Chọn ngày tháng" src="http://cafef3.vcmedia.vn/styles/DatePicker/calendar.gif" onclick='displayDatePicker("<% =txtDatePicker.ClientID %>", this, "dmy", "/");' align="absmiddle" />
