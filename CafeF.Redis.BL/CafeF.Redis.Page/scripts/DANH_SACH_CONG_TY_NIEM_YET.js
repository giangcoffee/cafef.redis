var CafeF_DSCongtyNiemyet_offsetfromcursorX = 12;
var CafeF_DSCongtyNiemyet_offsetfromcursorY = 10;
var CafeF_DSCongtyNiemyet_offsetdivfrompointerX = 10;
var CafeF_DSCongtyNiemyet_offsetdivfrompointerY = 13;
var CafeF_DSCongtyNiemyet_enabletip = false;
document.write('<div id="dhtmltooltip"></div>');
document.write('<img id="dhtmlpointer" src="http://cafef3.vcmedia.vn/images/tooltiparrow.gif">');

var CafeF_DSCongtyNiemyet_ie = document.all;
var CafeF_DSCongtyNiemyet_ns6 = document.getElementById && ! document.all;
CafeF_DSCongtyNiemyet_enabletip = false;

if (CafeF_DSCongtyNiemyet_ie || CafeF_DSCongtyNiemyet_ns6)
	var CafeF_DSCongtyNiemyet_tipobj = document.all ? document.all["dhtmltooltip"] : document.getElementById ? document.getElementById("dhtmltooltip") : "";

var CafeF_DSCongtyNiemyet_pointerobj = document.all ? document.all["dhtmlpointer"] : document.getElementById ? document.getElementById("dhtmlpointer") : "";

function CafeF_DSCongtyNiemyet_ietruebody() {
	return (document.compatMode && document.compatMode != "BackCompat") ? document.documentElement : document.body;
}

String.prototype.trim = function () {
    return this.replace(/^\s*/, "").replace(/\s*$/, "");
}

function CafeF_DSCongtyNiemyet_CompanyMarketInfo(symbol,divId)
{
    var url='/CafeF-Tools/CompanyMarketInfo.aspx?symbol=' + symbol;    
    
    var tDiv = document.getElementById(divId);               
    //tDiv.appendChild(ifrm);
    ///alert(typeof(tDiv.getElementsByTagName("IFRAME")));
    if(tDiv.hasChildNodes())
    {
        var child=tDiv.childNodes[0];
        tDiv.removeChild(child);        
    }
    if (!tDiv.hasChildNodes())
    {
        ifrm = document.createElement("IFRAME");
        ifrm.setAttribute("src", url);
        ifrm.style.width = "100%";
        ifrm.style.height = "100%";
        ifrm.style.overflow='hidden';
        ifrm.scrolling='no';
        ifrm.frameBorder='0';
        ifrm.style.border='0';
        ifrm.scrolling='no';
        tDiv.appendChild(ifrm);
   }
    
}

function CafeF_DSCongtyNiemyet_showtip(thetext, thewidth, theheight, thecolor) {
	if (CafeF_DSCongtyNiemyet_ns6 || CafeF_DSCongtyNiemyet_ie) {
		if (typeof thewidth != "undefined")
			CafeF_DSCongtyNiemyet_tipobj.style.width = thewidth + "px";
		if (typeof theheight != "undefined")
			CafeF_DSCongtyNiemyet_tipobj.style.height = theheight + "px";
		if (typeof thecolor != "undefined" && thecolor != "")
			CafeF_DSCongtyNiemyet_tipobj.style.backgroundColor = thecolor;
		thetext = thetext.trim();
		var arr = thetext.split(" ");
		for(i=0;i<arr.length;i++)
			if(arr[i].length>=40)
				thetext=thetext.replace(arr[i],arr[i].substr(0,40)+"...");
		//tipobj.innerHTML = thetext;
CafeF_DSCongtyNiemyet_CompanyMarketInfo(thetext, 'dhtmltooltip');
		
		CafeF_DSCongtyNiemyet_enabletip = true;
		return false;
	}
}

function CafeF_DSCongtyNiemyet_positiontip(e) {
   // alert(enabletip);
	if (CafeF_DSCongtyNiemyet_enabletip) {		
		var nondefaultpos = false;
		var curX = (CafeF_DSCongtyNiemyet_ns6) ? e.pageX : event.clientX + CafeF_DSCongtyNiemyet_ietruebody().scrollLeft;
		var curY = (CafeF_DSCongtyNiemyet_ns6) ? e.pageY : event.clientY + CafeF_DSCongtyNiemyet_ietruebody().scrollTop;
		
		var winwidth = CafeF_DSCongtyNiemyet_ie && ! window.opera ? CafeF_DSCongtyNiemyet_ietruebody().clientWidth : window.innerWidth - 20;
		var winheight = CafeF_DSCongtyNiemyet_ie && ! window.opera ? CafeF_DSCongtyNiemyet_ietruebody().clientHeight : window.innerHeight - 20;

		var rightedge = CafeF_DSCongtyNiemyet_ie && ! window.opera ? winwidth - event.clientX - CafeF_DSCongtyNiemyet_offsetfromcursorX : winwidth - e.clientX - CafeF_DSCongtyNiemyet_offsetfromcursorX;
		var bottomedge = CafeF_DSCongtyNiemyet_ie && ! window.opera ? winheight - event.clientY - CafeF_DSCongtyNiemyet_offsetfromcursorY : winheight - e.clientY - CafeF_DSCongtyNiemyet_offsetfromcursorY;

		var leftedge = (CafeF_DSCongtyNiemyet_offsetfromcursorX < 0) ? CafeF_DSCongtyNiemyet_offsetfromcursorX * (- 1) : - 1000;

		if (rightedge < CafeF_DSCongtyNiemyet_tipobj.offsetWidth) {
			CafeF_DSCongtyNiemyet_tipobj.style.left = curX - CafeF_DSCongtyNiemyet_tipobj.offsetWidth + "px";
			CafeF_DSCongtyNiemyet_nondefaultpos = true;
		}
		else if (curX < leftedge)
			CafeF_DSCongtyNiemyet_tipobj.style.left = "5px";
		else {
			CafeF_DSCongtyNiemyet_tipobj.style.left = curX + CafeF_DSCongtyNiemyet_offsetfromcursorX - CafeF_DSCongtyNiemyet_offsetdivfrompointerX + "px";
			CafeF_DSCongtyNiemyet_pointerobj.style.left = curX + CafeF_DSCongtyNiemyet_offsetfromcursorX + "px";
		}

		if (bottomedge < CafeF_DSCongtyNiemyet_tipobj.offsetHeight) {
			CafeF_DSCongtyNiemyet_tipobj.style.top = curY - CafeF_DSCongtyNiemyet_tipobj.offsetHeight - CafeF_DSCongtyNiemyet_offsetfromcursorY + "px";
			nondefaultpos = true;
		}
		else {
			CafeF_DSCongtyNiemyet_tipobj.style.top = curY + CafeF_DSCongtyNiemyet_offsetfromcursorY + CafeF_DSCongtyNiemyet_offsetdivfrompointerY + "px";
			CafeF_DSCongtyNiemyet_pointerobj.style.top = curY + CafeF_DSCongtyNiemyet_offsetfromcursorY + "px";
		}

		CafeF_DSCongtyNiemyet_tipobj.style.visibility = "visible";

		if (! nondefaultpos)
			CafeF_DSCongtyNiemyet_pointerobj.style.visibility = "visible";
		else
			CafeF_DSCongtyNiemyet_pointerobj.style.visibility = "hidden";
	}
}

function CafeF_DSCongtyNiemyet_hidetip() {
	if (CafeF_DSCongtyNiemyet_ns6 || CafeF_DSCongtyNiemyet_ie) {
		CafeF_DSCongtyNiemyet_enabletip = false;
		setTimeout('',50000);
		CafeF_DSCongtyNiemyet_tipobj.delay=50000;
		CafeF_DSCongtyNiemyet_tipobj.style.visibility = "hidden";
		CafeF_DSCongtyNiemyet_pointerobj.style.visibility = "hidden";
		CafeF_DSCongtyNiemyet_tipobj.style.left = "-1000px";
		CafeF_DSCongtyNiemyet_tipobj.style.backgroundColor = '';
		CafeF_DSCongtyNiemyet_tipobj.style.width = '';
	}
}

document.onmousemove = CafeF_DSCongtyNiemyet_positiontip;

function CafeF_DSCongtyNiemyet(instanceName)
{
    this.host = 'http://solieu6.vcmedia.vn';
    //this.host = 'http://localhost:8081';
    this.script_folder = 'http://cafef3.vcmedia.vn/solieu/solieu6/'; //'http://solieu6.vcmedia.vn/www/cafef/';
    this.script_object = null;
    this.instance_name = instanceName;
    this.is_filter_by_letter = false;
    this.selected_char_id = 'CafeF_ThiTruongNiemYet_ChuCai_All';
    this.LoadingImage = '<div align="center"><img src="http://cafef3.vcmedia.vn/solieu/solieu6/images/loading.gif" /></div>';
    this.default_page_size = 20;
    
    this.page_size = this.default_page_size;
    this.page_index = 1;
    this.record_count = 0;
    this.page_count = 1;
    
    this.TradeId = 0;
    this.IndustryId = 0;
    this.Keyword = '';

    this.CreateCssLink = function(href)
    {
        var css = document.createElement('link');
        css.type = 'text/css';
        css.rel = 'stylesheet';
        css.href = href;
        var head = document.getElementsByTagName('head')[0];
        head.appendChild(css);
    }
    
    this.ShowDateTime = function()
    {
        var today = new Date();
        
        if (today.getHours() < 8)
        {
            today.setDate(today.getDate() - 1);
            today.setHours(11, 0, 0, 0);
        }
        
        if (today.getDay() == 6)
        {
            today.setDate(today.getDate() - 1);
            today.setHours(11, 0, 0, 0);
        }
        else if (today.getDay() == 0)
        {
            today.setDate(today.getDate() - 2);
            today.setHours(11, 0, 0, 0);
        }
        
        if (today.getHours() >= 11)
        {
            today.setHours(11, 0, 0, 0);
        }
        
        var d = today.getDate(); d = (d < 10 ? '0' : '') + d;
        var mo = today.getMonth()+1; mo = (mo < 10 ? '0' : '') + mo;
        var y = today.getFullYear(); y = (y < 10 ? '0' : '') + y;
        
        var h = today.getHours(); h = (h < 10 ? '0' : '') + h;
        var mi = today.getMinutes(); mi = (mi < 10 ? '0' : '') + mi;
        var s = today.getSeconds(); s = (s < 10 ? '0' : '') + s;
        
        document.getElementById('CafeF_ThiTruongNiemYet_DateTime').innerHTML = ('Thứ ' + (today.getDay() + 1) + ', ' + d + '/' + mo + '/' + y + ', ' + h + ':' + mi + ':' + s);
        setTimeout(this.instance_name + '.ShowDateTime()',1000);
    }
    
    this.InitScript = function()
    {
 //       this.CreateCssLink(this.script_folder + 'css/cafef.css');
//        this.CreateScriptObject(this.host + '/Public/js/jquery.js');
//        this.CreateScriptObject('http://cafef.vn/Scripts/AutoComplete/kby.js');
//        this.CreateScriptObject(this.host + '/Public/js/jquery.bgiframe.min.js');
//        this.CreateScriptObject(this.host + '/Public/js/jquery.dimensions.js');
//        this.CreateScriptObject(this.host + '/Public/js/jquery.autocomplete2.js');
    
        var output = '';
        
        output += '<div class="cf_WCBox">';
        //output += '<div class="cf_BoxHeader"><div></div></div>';
        output += '<div class="cf_BoxContent" style="padding-left: 10px; padding-right: 10px;">';
        output += '    <table border="0" style="width: 100%;" cellpadding="3" cellspacing="0">';
        output += '        <tr><td style="font-size: 15px; font-weight: bold;" nowrap="nowrap"><a style="font-size: 15px; text-decoration: none;" href="/Du-lieu-doanh-nghiep.chn">DỮ LIỆU DOANH NGHIỆP</a></td>';
        output += '        <td id="CafeF_ThiTruongNiemYet_DateTime" style="font-size: 11px; text-align: right; color: #666666">Thứ 2, 07/07/2008. 14:34</td></tr>';
        output += '        <tr><td style="border-top: solid 1px #dadada;color: #cc0000; font-weight: bold;font-size:13px;" nowrap="nowrap">';
        output += '            Danh sách các công ty niêm yết';
        output += '        </td><td style="border-top: solid 1px #dadada;">';
        output += '        <table align="right" border="0" cellpadding="3" cellspacing="0">';
        output += '            <tr><td>';
        output += '                    <div class="cf_WireBox">';
        //output += '                        <div class="cf_BoxHeader"><div></div></div>';
        output += '                        <div class="cf_BoxContent" style="padding-left: 5px; padding-right: 5px; white-space: nowrap;">';
        output += '                            <a href="javascript:void(0);" id="CafeF_ThiTruongNiemYet_TabButton_XemToanBo" class="Actived" onclick="' + this.instance_name + '.SelectTab(\'XemToanBo\');">Xem toàn bộ</a>';
        output += '                        </div>';
        //output += '                        <div class="cf_BoxFooter"><div></div></div>';
        output += '                    </div>';
        output += '                </td><td>';
        output += '                    <div class="cf_WireBox">';
        //output += '                        <div class="cf_BoxHeader"><div></div></div>';
        output += '                        <div class="cf_BoxContent" style="padding-left: 5px; padding-right: 5px; white-space: nowrap;">';
        output += '                            <a href="javascript:void(0);" id="CafeF_ThiTruongNiemYet_TabButton_XemHoSE" class="InActived" onclick="' + this.instance_name + '.SelectTab(\'XemHoSE\');">HSX</a>';
        output += '                        </div>';
        //output += '                        <div class="cf_BoxFooter"><div></div></div>';
        output += '                    </div>';
        output += '                </td><td>';
        output += '                    <div class="cf_WireBox">';
        //output += '                        <div class="cf_BoxHeader"><div></div></div>';
        output += '                        <div class="cf_BoxContent" style="padding-left: 5px; padding-right: 5px; white-space: nowrap;">';
        output += '                            <a href="javascript:void(0);" id="CafeF_ThiTruongNiemYet_TabButton_XemHaSTC" class="InActived" onclick="' + this.instance_name + '.SelectTab(\'XemHaSTC\');">HNX</a>';
        output += '                        </div>';
        //output += '                        <div class="cf_BoxFooter"><div></div></div>';
        output += '                    </div>';
        output += '                </td><td>';
        output += '                    <div class="cf_WireBox">';
        //output += '                        <div class="cf_BoxHeader"><div></div></div>';
        output += '                        <div class="cf_BoxContent" style="padding-left: 5px; padding-right: 5px; white-space: nowrap;">';
        output += '                            <a href="javascript:void(0);" id="CafeF_ThiTruongNiemYet_TabButton_XemUpCom" class="InActived" onclick="' + this.instance_name + '.SelectTab(\'XemUpCom\');">UpCom</a>';
        output += '                        </div>';
        //output += '                        <div class="cf_BoxFooter"><div></div></div>';
        output += '                    </div>';
        output += '                </td></tr>';
        output += '        </table>';
        output += '        </td></tr>';
        output += '        <tr><td colspan="2">';
        output += '        <table border="0" cellpadding="3" cellspacing="0" align="center">';
        output += '            <tr><td>Mã CK (Tên công ty)</td><td>Ngành</td></tr>';
        output += '            <tr><td><input style="width: 250px;" onfocus="' + this.instance_name + '.InitAutoComplete()" type="text" id="CafeF_ThiTruongNiemYet_TuKhoa" autocomplete="off" class="class_text_autocomplete ac_input" /></td>';
        output += '                <td><select id="CafeF_ThiTruongNiemYet_Nganh">';
	    output += '                        <option value="8">Bánh kẹo</option>';
	    output += '                        <option value="9">Bao bì - Đóng gói</option>';
	    output += '                        <option value="10">Bảo hiểm</option>';
	    output += '                        <option value="11">Bất động sản</option>';
	    output += '                        <option value="12">Bê tông</option>';
	    output += '                        <option value="14">Cao su & Săm lốp</option>';
	    output += '                        <option value="15">Chế biến nông sản</option>';
	    output += '                        <option value="16">Chứng khoán</option>';
	    output += '                        <option value="17">Cơ khí & Chế tạo máy</option>';
	    output += '                        <option value="18">Công nghệ và thiết bị viễn thông</option>';
	    output += '                        <option value="19">Công nghệ và Truyền thông</option>';
	    output += '                        <option value="20">Dệt may</option>';
	    output += '                        <option value="21">Dịch vụ & Cung ứng công nghiệp</option>';
	    output += '                        <option value="22">Dịch vụ Dầu khí</option>';
	    output += '                        <option value="23">Dịch vụ giải trí</option>';
	    output += '                        <option value="24">Dịch vụ tiêu dùng</option>';
	    output += '                        <option value="25">Dịch vụ vận tải</option>';
	    output += '                        <option value="26">Dịch vụ xăng dầu</option>';
	    output += '                        <option value="27">Điện tử gia dụng</option>';
	    output += '                        <option value="28">Đồ uống</option>';
	    output += '                        <option value="29">Dược phẩm</option>';
	    output += '                        <option value="30">Mía đường</option>';
	    output += '                        <option value="31">Gạch men & Đá ốp lát</option>';
	    output += '                        <option value="32">Giấy - Lâm sản</option>';
	    output += '                        <option value="33">Hàng công nghiệp</option>';
	    output += '                        <option value="34">Hàng gia dụng </option>';
	    output += '                        <option value="35">Hóa dầu</option>';
	    output += '                        <option value="36">Khai khoáng</option>';
	    output += '                        <option value="37">Khai thác dầu khí</option>';
	    output += '                        <option value="38">Kinh doanh khí hóa lỏng</option>';
	    output += '                        <option value="39">Ngân hàng</option>';
	    output += '                        <option value="40">Ngành nhựa</option>';
	    output += '                        <option value="42">Nội thất</option>';
	    output += '                        <option value="43">Phân phối điện năng</option>';
	    output += '                        <option value="44">Phân phối hàng điện tử</option>';
	    output += '                        <option value="45">Phụ gia thực phẩm</option>';
	    output += '                        <option value="46">Thiết bị giáo dục</option>';
	    output += '                        <option value="47">Sản phẩm sữa</option>';
	    output += '                        <option value="48">Sản xuất & kinh doanh Thép</option>';
	    output += '                        <option value="49">Tài chính phức hợp</option>';
	    output += '                        <option value="50">Tập đoàn đa ngành</option>';
	    output += '                        <option value="51">Than đá</option>';
	    output += '                        <option value="52">Thiết bị y tế</option>';
	    output += '                        <option value="53">Thuốc lá</option>';
	    output += '                        <option value="54">Thương mại</option>';
	    output += '                        <option value="55">Thủy điện</option>';
	    output += '                        <option value="56">Thủy sản</option>';
	    output += '                        <option value="57">Văn hóa phẩm</option>';
	    output += '                        <option value="58">Vật tư nông nghiệp</option>';
	    output += '                        <option value="59">VLXD</option>';
	    output += '                        <option value="60">Xây dựng</option>';
	    output += '                        <option value="61">Xi măng</option>';
	    output += '                        <option value="63">Nhiệt điện</option>';
	    output += '                        <option value="64">Quỹ đầu tư</option>';
	    output += '                        <option value="-1" selected="selected">[Tất cả]</option>';
        output += '                    </select></td>';
        output += '                <td><img style="cursor: hand; cursor: pointer;" onclick="' + this.instance_name + '.SearchByKeyword()" alt="" src="' + this.script_folder + 'images/search.gif" /></td></tr>';
        output += '        </table>';
        output += '        </td></tr>';
        output += '        <tr><td colspan="2">';
        output += '            <table align="center" class="CafeF_ThiTruongNiemYet_ChuCai" cellpadding="3" cellspacing="2"><tr>';
        output += '            <td><a id="CafeF_ThiTruongNiemYet_ChuCai_A" href="javascript:void(0)" onclick="' + this.instance_name + '.ViewByLetter(\'A\');">A</a></td>';
        output += '            <td><a id="CafeF_ThiTruongNiemYet_ChuCai_B" href="javascript:void(0)" onclick="' + this.instance_name + '.ViewByLetter(\'B\');">B</a></td>';
        output += '            <td><a id="CafeF_ThiTruongNiemYet_ChuCai_C" href="javascript:void(0)" onclick="' + this.instance_name + '.ViewByLetter(\'C\');">C</a></td>';
        output += '            <td><a id="CafeF_ThiTruongNiemYet_ChuCai_D" href="javascript:void(0)" onclick="' + this.instance_name + '.ViewByLetter(\'D\');">D</a></td>';
        output += '            <td><a id="CafeF_ThiTruongNiemYet_ChuCai_E" href="javascript:void(0)" onclick="' + this.instance_name + '.ViewByLetter(\'E\');">E</a></td>';
        output += '            <td><a id="CafeF_ThiTruongNiemYet_ChuCai_F" href="javascript:void(0)" onclick="' + this.instance_name + '.ViewByLetter(\'F\');">F</a></td>';
        output += '            <td><a id="CafeF_ThiTruongNiemYet_ChuCai_G" href="javascript:void(0)" onclick="' + this.instance_name + '.ViewByLetter(\'G\');">G</a></td>';
        output += '            <td><a id="CafeF_ThiTruongNiemYet_ChuCai_H" href="javascript:void(0)" onclick="' + this.instance_name + '.ViewByLetter(\'H\');">H</a></td>';
        output += '            <td><a id="CafeF_ThiTruongNiemYet_ChuCai_I" href="javascript:void(0)" onclick="' + this.instance_name + '.ViewByLetter(\'I\');">I</a></td>';
        output += '            <td><a id="CafeF_ThiTruongNiemYet_ChuCai_J" href="javascript:void(0)" onclick="' + this.instance_name + '.ViewByLetter(\'J\');">J</a></td>';
        output += '            <td><a id="CafeF_ThiTruongNiemYet_ChuCai_K" href="javascript:void(0)" onclick="' + this.instance_name + '.ViewByLetter(\'K\');">K</a></td>';
        output += '            <td><a id="CafeF_ThiTruongNiemYet_ChuCai_L" href="javascript:void(0)" onclick="' + this.instance_name + '.ViewByLetter(\'L\');">L</a></td>';
        output += '            <td><a id="CafeF_ThiTruongNiemYet_ChuCai_M" href="javascript:void(0)" onclick="' + this.instance_name + '.ViewByLetter(\'M\');">M</a></td>';
        output += '            <td><a id="CafeF_ThiTruongNiemYet_ChuCai_N" href="javascript:void(0)" onclick="' + this.instance_name + '.ViewByLetter(\'N\');">N</a></td>';
        output += '            <td><a id="CafeF_ThiTruongNiemYet_ChuCai_O" href="javascript:void(0)" onclick="' + this.instance_name + '.ViewByLetter(\'O\');">O</a></td>';
        output += '            <td><a id="CafeF_ThiTruongNiemYet_ChuCai_P" href="javascript:void(0)" onclick="' + this.instance_name + '.ViewByLetter(\'P\');">P</a></td>';
        output += '            <td><a id="CafeF_ThiTruongNiemYet_ChuCai_Q" href="javascript:void(0)" onclick="' + this.instance_name + '.ViewByLetter(\'Q\');">Q</a></td>';
        output += '            <td><a id="CafeF_ThiTruongNiemYet_ChuCai_R" href="javascript:void(0)" onclick="' + this.instance_name + '.ViewByLetter(\'R\');">R</a></td>';
        output += '            <td><a id="CafeF_ThiTruongNiemYet_ChuCai_S" href="javascript:void(0)" onclick="' + this.instance_name + '.ViewByLetter(\'S\');">S</a></td>';
        output += '            <td><a id="CafeF_ThiTruongNiemYet_ChuCai_T" href="javascript:void(0)" onclick="' + this.instance_name + '.ViewByLetter(\'T\');">T</a></td>';
        output += '            <td><a id="CafeF_ThiTruongNiemYet_ChuCai_U" href="javascript:void(0)" onclick="' + this.instance_name + '.ViewByLetter(\'U\');">U</a></td>';
        output += '            <td><a id="CafeF_ThiTruongNiemYet_ChuCai_V" href="javascript:void(0)" onclick="' + this.instance_name + '.ViewByLetter(\'V\');">V</a></td>';
        output += '            <td><a id="CafeF_ThiTruongNiemYet_ChuCai_W" href="javascript:void(0)" onclick="' + this.instance_name + '.ViewByLetter(\'W\');">W</a></td>';
        output += '            <td><a id="CafeF_ThiTruongNiemYet_ChuCai_X" href="javascript:void(0)" onclick="' + this.instance_name + '.ViewByLetter(\'X\');">X</a></td>';
        output += '            <td><a id="CafeF_ThiTruongNiemYet_ChuCai_Y" href="javascript:void(0)" onclick="' + this.instance_name + '.ViewByLetter(\'Y\');">Y</a></td>';
        output += '            <td><a id="CafeF_ThiTruongNiemYet_ChuCai_Z" href="javascript:void(0)" onclick="' + this.instance_name + '.ViewByLetter(\'Z\');">Z</a></td>';
        output += '            <td><a id="CafeF_ThiTruongNiemYet_ChuCai_All" class="Actived" href="javascript:void(0)" onclick="' + this.instance_name + '.ViewByLetter(\'\');">Tất cả</a></td>';
        output += '            </tr></table>';
        output += '        </td></tr>';
        output += '        <tr><td colspan="2" id="CafeF_ThiTruongNiemYet_Content"></td></tr>';
        output += '        <tr><td colspan="2" style="border-top: solid 1px #dadada;">';
        output += '            <table width="100%" border="0" cellpadding="3" cellspacing="0">';
        output += '            <tr><td id="CafeF_ThiTruongNiemYet_TongSoTrang"></td><td id="CafeF_ThiTruongNiemYet_Trang" align="right"></td></tr>';
        output += '            </table>';
        output += '        </td></tr>';
        output += '    </table>';
        output += '</div>';
        output += '<div class="cf_BoxFooter"><div></div></div>';
        output += '</div>';

        document.getElementById('CafeF_DSCongtyNiemyet').innerHTML = output;

        this.ShowDateTime();
    }
    
    this.Search = function(isFilterByLetter)
    {
        document.getElementById('CafeF_ThiTruongNiemYet_Content').innerHTML = this.LoadingImage;
        this.is_filter_by_letter = isFilterByLetter;
        this.CreateScriptObject(this.host + '/ProxyHandler.ashx?RequestName=CompanyInfo&CallBack=' + this.instance_name + '.OnLoaded&RequestType=json&TradeId=' + this.TradeId + '&IndustryId=' + this.IndustryId + '&Keyword=' + this.Keyword + '&PageIndex=' + this.page_index + '&PageSize=' + this.page_size + '&Type=' + (this.is_filter_by_letter ? '1' : '0'));
    }
    
    this.Search1 = function()
    {
        document.getElementById('CafeF_ThiTruongNiemYet_Content').innerHTML = this.LoadingImage;
        this.CreateScriptObject(this.host + '/ProxyHandler.ashx?RequestName=CompanyInfo&CallBack=' + this.instance_name + '.OnLoaded&RequestType=json&TradeId=' + this.TradeId + '&IndustryId=' + this.IndustryId + '&Keyword=' + this.Keyword + '&PageIndex=' + this.page_index + '&PageSize=' + this.page_size + '&Type=' + (this.is_filter_by_letter ? '1' : '0'));
    }

    this.ViewAll = function()
    {
        document.getElementById('CafeF_ThiTruongNiemYet_Content').innerHTML = this.LoadingImage
        this.page_index = 1;
        this.CreateScriptObject(this.host + '/ProxyHandler.ashx?RequestName=CompanyInfo&CallBack=' + this.instance_name + '.OnLoaded&RequestType=json&TradeId=' + this.TradeId + '&ViewAll=1');
    }
    
    this.Download = function(symbol)
    {
        var popupDownload=window.open('/Ultility/FilesDownLoad.aspx?symbol=' + symbol,'popupDownload','scrollbars,resizable=yes,status=yes, width=450,height=550,left=100,top=100');
        return false;
    }
    
    this.OnLoaded = function(data, methodName)
    {
        var json = eval(data);
        
        var output = '';
        output += '<table cellspacing="0" cellpadding="3" align="center" style="width: 100%;">';
        output += '<tr>';
        output += '<th style="width: 40px;">MÃ CK</th>';
        output += '<th>TÊN CÔNG TY</th>';
        output += '<th style="width: 30px;text-align:right;">EPS</th>';
        output += '<th style="width: 30px;text-align:right;">GIÁ</th>';
        output += '<th style="width: 30px;text-align:right;padding-right:10px;">P/E</th>';
        output += '<th style="width: 40px;">SÀN</th>';
        output += '<th style="width: 60px;">Báo cáo TC</th>';
        output += '</tr>';
        var isAlternation = false;
        for (var i = 0; i < json.CompanyInfos.length; i++)
        {
            output += '<tr>';
            output += '<td' + (isAlternation ? ' class="Alternation"' : '') + ' style="text-align: left;"><a style="font-weight: normal; color: #004370;" onmouseover="CafeF_DSCongtyNiemyet_showtip(\'' + json.CompanyInfos[i].Symbol + '\', 630, 440);" onmouseout="CafeF_DSCongtyNiemyet_hidetip();" href="' + CafeF_JSLibrary.GetCompanyInfoLink(json.CompanyInfos[i].Symbol) + '">' + json.CompanyInfos[i].Symbol + '</a></td>';
            output += '<td' + (isAlternation ? ' class="Alternation"' : '') + ' style="text-align: left;"><a style="font-weight: normal;" href="' + CafeF_JSLibrary.GetCompanyInfoLink(json.CompanyInfos[i].Symbol) + '">' + json.CompanyInfos[i].CompanyName + '</a></td>';
            output += '<td' + (isAlternation ? ' class="Alternation"' : '') + ' style="text-align: right; padding-right:10px;">' + (json.CompanyInfos[i].EPS > 0 ? json.CompanyInfos[i].EPS : 'N/A') + '</td>';
            output += '<td' + (isAlternation ? ' class="Alternation"' : '') + ' style="text-align: right;">' + this.FormatNumber(json.CompanyInfos[i].Price) + '</td>';
            output += '<td' + (isAlternation ? ' class="Alternation"' : '') + ' style="text-align: right; padding-right:10px;">' + (json.CompanyInfos[i].PE > 0 ? json.CompanyInfos[i].PE : 'N/A') + '</td>';
            output += '<td' + (isAlternation ? ' class="Alternation"' : '') + ' style="text-align: left">' + json.CompanyInfos[i].TradeCenter + '</td>';
            output += '<td' + (isAlternation ? ' class="Alternation"' : '') + ' style="text-align: center;"><a href="javascript:void(0);" onclick="' + this.instance_name + '.Download(\'' + json.CompanyInfos[i].Symbol + '\');"><span style="font-weight: normal;">Download</span></a></td>';
            output += '</tr>';
            isAlternation = !isAlternation;
        }
        output += '</table>';
        
        document.getElementById('CafeF_ThiTruongNiemYet_Content').innerHTML = output;
        document.getElementById('CafeF_ThiTruongNiemYet_TongSoTrang').innerHTML = 'Trang ' + json.PageIndex + '/' + json.PageCount + ' (Tổng số: ' + json.RecordCount + ' công ty)';
        
        var page = '';
        if (json.PageIndex > 1)
        {
            page += '<a href="javascript:void(0)" onclick="' + this.instance_name + '.GotoPage(' + (json.PageIndex - 1) + ')">Trang trước</a> | ';
        }
        if (json.PageIndex < json.PageCount)
        {
            page += '<a href="javascript:void(0)" onclick="' + this.instance_name + '.GotoPage(' + (json.PageIndex + 1) + ')">Trang sau</a> | ';
        }
        
        if (json.PageCount > 1)
        {
            page += '<a href="javascript:void(0)" onclick="' + this.instance_name + '.GotoPage(0)">Xem toàn bộ</a>';
        }
        else
        {
            if (json.RecordCount > this.default_page_size)
            {
                page += '<a href="javascript:void(0)" onclick="' + this.instance_name + '.GotoPage(1)">Xem trang đầu</a>';
            }
        }
        document.getElementById('CafeF_ThiTruongNiemYet_Trang').innerHTML = page;
        
        this.record_count = json.RecordCount;
        this.page_index = json.PageIndex;
        this.page_count = json.PageCount;
    }
    
    this.GotoPage = function(page)
    {
        if (page == 0)
        {
            this.page_size = this.record_count;
            this.page_index = 1;
        }
        else
        {
            this.page_size = this.default_page_size;
            this.page_index = page;
        }
        this.Search1();
    }

    this.SelectTab = function(id) {
        var tabbutton_XemToanBo = document.getElementById('CafeF_ThiTruongNiemYet_TabButton_XemToanBo'); tabbutton_XemToanBo.className = 'InActived';
        var tabbutton_XemHoSE = document.getElementById('CafeF_ThiTruongNiemYet_TabButton_XemHoSE'); tabbutton_XemHoSE.className = 'InActived';
        var tabbutton_XemHaSTC = document.getElementById('CafeF_ThiTruongNiemYet_TabButton_XemHaSTC'); tabbutton_XemHaSTC.className = 'InActived';
        var tabbutton_XemUpCom = document.getElementById('CafeF_ThiTruongNiemYet_TabButton_XemUpCom'); tabbutton_XemUpCom.className = 'InActived';

        if (id == 'XemHoSE') {
            tabbutton_XemHoSE.className = 'Actived';
            this.TradeId = 1;
        }
        else if (id == 'XemHaSTC') {
            tabbutton_XemHaSTC.className = 'Actived';
            this.TradeId = 2;
        }
        else if (id == 'XemUpCom') {
            tabbutton_XemUpCom.className = 'Actived';
            this.TradeId = 9;
        }
        else if (id == 'XemToanBo') {
            tabbutton_XemToanBo.className = 'Actived';
            this.TradeId = -2;  // = 0 --> lay tat ca cac san // = -2 --> lay HSX va HNX
        }
        else {
            this.TradeId = 0;
        }

        this.page_index = 1;
        this.IndustryId = 0;
        this.page_size = this.default_page_size;
        this.Keyword = '';
        this.Search(false);
        this.SelectChar('All');
    }
    
    this.ViewByLetter = function(letter)
    {
        this.page_index = 1;
        this.page_size = this.default_page_size;
        this.IndustryId = 0;
        this.Keyword = letter;
        this.IndustryId = 0;
        this.Search(true);
        this.SelectChar(letter);
    }
    
    this.SearchByKeyword = function()
    {
        this.page_index = 1;
        this.page_size = this.default_page_size;
        
        var keyword = document.getElementById('CafeF_ThiTruongNiemYet_TuKhoa');
        this.Keyword = keyword.value;
        
        var industry = document.getElementById('CafeF_ThiTruongNiemYet_Nganh');
        this.IndustryId = industry.value;
        this.Search(false);
        this.SelectChar('All');
    }
    
    this.SelectChar = function(letter)
    {
        var id = 'CafeF_ThiTruongNiemYet_ChuCai_' + letter
        var objCurrentChar = document.getElementById(this.selected_char_id);
        if (objCurrentChar)
        {
            objCurrentChar.className = '';
        }
        var objChar = document.getElementById(id);
        if (objChar)
        {
            this.selected_char_id = id;
            objChar.className = 'Actived';
        }
        else
        {
            this.selected_char_id = 'CafeF_ThiTruongNiemYet_ChuCai_All';
            document.getElementById(this.selected_char_id).className = 'Actived';
        }
    }
    
    this.InitAutoComplete = function()
    {
        $('#CafeF_ThiTruongNiemYet_TuKhoa').autocomplete(oc, {
            minChars: 1,
            delay: 10,
            width: 300,
            matchContains: true,
            autoFill: false,
            formatItem: function(row) {
                return row.c + " - " + row.m + "@" + row.o;
                //return row.m + "@" + row.o;
            },
            formatResult: function(row) {
                return row.c + " - " + row.m;
                //return row.m;
            }
        });
    }
    
    this.CreateScriptObject = function(src)
    {
        if (this.script_object != null)
	    {
		    this.RemoveScriptObject();
	    }
    	
	    this.script_object = document.createElement('script');

        this.script_object.setAttribute('type','text/javascript');
        this.script_object.setAttribute('src', src);
        
//        var head = document.getElementsByTagName('head')[0];
//        head.appendChild(this.script_object);
        setTimeout(this.instance_name + '.AppendScript()', 100);
    }
    
    this.AppendScript = function()
    {
        var head = document.getElementsByTagName('head')[0];
        head.appendChild(this.script_object);
    }

    this.RemoveScriptObject = function()
    {
	    this.script_object.parentNode.removeChild(this.script_object) ;
	    this.script_object = null ;
    }
    
    this.FormatChangeValue = function(value)
    {
        var output = '';
        if (value > 0)
        {
            output = '<span style="color: #008000">' + this.FormatNumber(value, true) + '</span>';
        }
        else if (value < 0)
        {
            output = '<span style="color: #cc0000">' + this.FormatNumber(value, true) + '</span>';
        }
        else
        {
            output = '<span style="color: #ff9900">' + this.FormatNumber(value, true) + '</span>';
        }
        return output;
    }

    this.FormatNumber = function(value, displayZero)
    {
        if (value == '') return (displayZero ? '0' : '');
        try
        {
            var number = parseFloat(value);
            value = this.FormatNumber1(number, 2, '.', ',');
            return (value);
        }
        catch (err)
        {
            return (displayZero ? '0' : '');
        }
    }

    this.FormatNumber1 = function(number, decimals, decimalSeparator, thousandSeparator) 
    {
        var number = number.toFixed(decimals);
        
        var temp = number.toString();
        
        var f = temp.substr(temp.length - decimals, decimals);
        
        while (f != '' && f.charAt(f.length - 1) == '0') f = f.substr(0, f.length - 1);
        
        if (f != '') f = decimalSeparator + f;
        
        var t = temp.substr(0, temp.length - 3);
        
        if (thousandSeparator != '' && t.length > 3) 
	    {
		    h = t;
		    t = '';
    		
		    for (j = 3; j < h.length; j += 3) 
		    {
			    i = h.slice(h.length - j, h.length - j + 3);
			    t = thousandSeparator + i +  t + '';
		    }
    		
		    j = h.substr(0, (h.length % 3 == 0) ? 3 : (h.length % 3));
		    t = j + t;
	    }
    	
	    temp = t + f;
    	
        return temp;
    }
}

//document.write('<div style="font-size:10px;" id="CafeF_DSCongtyNiemyet_Test"></div>');
var container = document.getElementById('CafeF_DSCongtyNiemyet');
if (container)
{
    var cafef_ds_cong_ty_niem_yet = new CafeF_DSCongtyNiemyet('cafef_ds_cong_ty_niem_yet');
    cafef_ds_cong_ty_niem_yet.InitScript();
    
    if (container.delay)
    {
        setTimeout('cafef_ds_cong_ty_niem_yet.SelectTab(\'XemToanBo\')', parseInt(container.delay));
    }
    else
    {
        cafef_ds_cong_ty_niem_yet.SelectTab('XemToanBo');
    }
}