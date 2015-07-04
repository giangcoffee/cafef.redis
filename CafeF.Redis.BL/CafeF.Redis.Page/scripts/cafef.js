function loadErrorImage(id, src) {

    if (id.getAttribute("loi") == null) {
        id.setAttribute("loi", "1");
    }
    else {
        id.setAttribute("loi", eval(id.getAttribute("loi")) + 1);
    }
    if (eval(id.getAttribute("loi")) >= 2) {
        var width = src.substr(src.lastIndexOf("=") + 1, src.length - src.lastIndexOf("="));
        id.onerror = null;
        id.src = "http://cafef3.vcmedia.vn/v2/images/no_image.jpg";
    }
    else {
        id.src = src;
    }
}

function MenuActive(id) {
    var aLink = document.getElementById("a_" + id);
    if (aLink != null) {
        aLink.className = "active";
    }
    else {
        aLink.className = "";
    }

    if (id == 0) {
        aLink.className = "trangchuActive active";
    }
}

var hours;
var minutes;
var seconds;
var dn;
function GetDate() {
    monthname = new Array("tháng 1", "tháng 2", "tháng 3", "tháng 4", "tháng 5", "tháng 6", "tháng 7", "tháng 8", "tháng 9", "tháng 10", "tháng 11", "Tháng 12");
    now = new Date();
    date = now.getDate();
    monthnum = now.getMonth() + 1;
    month = monthname[monthnum];

    hours = now.getHours();
    minutes = now.getMinutes();
    seconds = now.getSeconds();
    dn = "PM";
    if (hours < 12)
        dn = "AM";
    if (hours > 12)
        hours = hours - 12;
    if (hours == 0)
        hours = 12;
    if (minutes <= 9)
        minutes = "0" + minutes;

    var divTime = document.getElementById("datetime");
    /*divTime.innerHTML = "Ngày "+date + " " + month +" năm " +now.getFullYear() + "<br/>" + hours+":"+minutes + " "+dn;*/

    divTime.innerHTML = hours + ":" + minutes + " " + dn + " | " + date + "." + monthnum + "." + now.getFullYear();
}



function LoadTieuDiem_DNN(idTD, idDNN, bl, adnn, atd) {
    var divDNN, divTieuDiem, aDNN, aTD;
    divTieuDiem = document.getElementById(idTD);
    divDNN = document.getElementById(idDNN);
    aDNN = document.getElementById(adnn);
    aTD = document.getElementById(atd);

    /* bl=true : load doc nhieu nhat va nguoc lai*/

    if (bl) {
        divDNN.style.display = "block";
        divTieuDiem.style.display = "none";
        aDNN.className = "aEEE-active";
        aTD.className = "aEEE";
    }
    else {
        divDNN.style.display = "none";
        divTieuDiem.style.display = "block";
        aDNN.className = "aEEE";
        aTD.className = "aEEE-active";
    }
}

function LoadTinMoiHeader(index) {
    $.get('/ajax/tinmoi.aspx?pageindex=' + index, function(data) {
        $('#divTinMoi').html(data);
    });
}

function LoadTinMoiNext() {
    var hdpageindex = document.getElementById('hdPageIndex');
    var index = hdpageindex.value;
    index = eval(eval(index) + 1);
    LoadTinMoiHeader(index);
    hdpageindex.value = index;
}
function LoadTinMoiPre() {
    var hdpageindex = document.getElementById('hdPageIndex');
    var index = hdpageindex.value;
    index = eval(eval(index) - 1);
    if (index < 1)
        index = 1;
    LoadTinMoiHeader(index);
    hdpageindex.value = index;
}

function openWindow(sUrl, title) {
    window.open(sUrl, title, 'scrollbars,resizable=yes,status=yes');
}

function ShareWeb(type) {
    var url = '';
    switch (type) {
        case 1:
            url = "http://www.facebook.com/sharer.php?u=" + window.location.href;
            break;
        case 2:
            url = "http://twitthis.com/twit?url=" + window.location.href;
            break;
    }
    var newWindow = window.open(url, '', '_blank,resizable=yes,width=800,height=450');
    newWindow.focus();
    return false;
}

function getCookie(c_nameobj, c_name) {
    var i, x, y, ARRcookies = document.cookie.split(";");
    for (i = 0; i < ARRcookies.length; i++) {
        x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
        y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x == c_nameobj) {
            var arr = y.split("&");
            for (j = 0; j < arr.length; j++) {
                var value = arr[j].substr(0, arr[j].indexOf("="));
                var _re = arr[j].substr(arr[j].indexOf("=") + 1);
                value = value.replace(/^\s+|\s+$/g, "");
                if (value == c_name) {
                    return _re;
                }
            }
        }
    }
}

function LoadUser(c_nameobj, c_name) {
    var userName = getCookie(c_nameobj, c_name);
    var divLogin = document.getElementById('divLogin');
    var html = '';
    if (userName != null && userName != 'undefined') {
        /*       
        html += '<ul id="navright">';
        html += '<li class="cfmb"><a href="/help/cafef-mobile.htm">CafeF Mobile</a></li>';
        html += '<li><a href="#" style="color:red; font-weight:bold">' + userName +'</a></li>';
        html += '<li><a style="font-weight:bold" href="javascript:void(0)" onclick="porfolio_logout();">THOÁT</a></li>';
        html += '</ul>'       
        */

        html += '<div id="divLogin">';
        html += '<ul id="navright">';
        html += '<li><a href="/help/cafef-mobile.htm">CafeF Mobile&nbsp;|</a></li>';
        html += '<li><a href="#" style="color:red; font-weight:bold">&nbsp;' + userName + '&nbsp;</a></li>';
        html += '<li><a style="font-weight:bold" href="javascript:void(0)" onclick="porfolio_logout();">|&nbsp;THOÁT</a></li></ul></div>';

        divLogin.innerHTML = html;
    }

}
function porfolio_logout() {
    $.ajax(
        {
            type: 'GET',
            url: '/pages/signout.aspx',
            success: function(data) {
                window.location = 'http://cafef.vn/';
            }
        }
        );
}