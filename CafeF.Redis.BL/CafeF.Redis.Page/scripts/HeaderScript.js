function CafeF_Library()
{
this.QueryString = function(key)
{
var strQueryString = window.location.search.substring(1);
var values = strQueryString.split('&');
for (i = 0; i < values.length; i++)
{
var value = values[i].split('=');
if (value[0] == key)
{
return value[1];
}
}
return '';
}
this.RefreshImage = function(imgId, url)
{
var chartImage = document.getElementById(imgId);
var currentDateTime = new Date();
chartImage.src = url+'?'+currentDateTime.getDate()+currentDateTime.getTime();
}
this.String2Float = function(value)
{
return (value != '' ? parseFloat(value) : 0);
}
this.Reload = function()
{
window.location.reload();
}
this.ltrim = function(str){
if (str)
{
return str.toString().replace(/^\s+/, '');
}
else
{
return '';
}
}
this.rtrim = function(str) {
if (str)
{
return str.replace(/\s+$/, '');
}
else
{
return '';
}
}
this.trim = function(str) {
if (str)
{
return str.replace(/^\s+|\s+$/g, '');
}
else
{
return '';
}
}
this.FormatNumber = function (number, decimals, decimalSeparator, thousandSeparator)
{
var number = number.toFixed(decimals);
var temp = number.toString();
var f = temp.substr(temp.length-decimals, decimals);
while (f != '' && f.charAt(f.length-1) == '0') f = f.substr(0, f.length-1);
if (f != '') f = decimalSeparator+f;
var t = temp.substr(0, temp.length-3);
if (thousandSeparator != '' && t.length > 3)
{
h = t;
t = '';
for (j = 3; j < h.length; j+= 3)
{
i = h.slice(h.length-j, h.length-j+3);
t = thousandSeparator+i+ t+'';
}
j = h.substr(0, (h.length % 3 == 0) ? 3 : (h.length % 3));
t = j+t;
}
temp = t+f;
return temp;
}
this.CreateScriptObject = function(src)
{
var script_object = document.createElement('script');
script_object.setAttribute('type','text/javascript');
script_object.setAttribute('src', src);
var head = document.getElementsByTagName('head')[0];
head.appendChild(script_object);
}
this.RemoveScriptObject = function(obj)
{
if(obj)
{
obj.parentNode.removeChild(obj) ;
obj = null ;
}
}
this.KoDauChars = 'aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIOOOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU';
this.uniChars = 'àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ';
this.UnicodeToKoDau = function(input)
{
var retVal = '';
input = this.trim(input);
var s = input.split('');
var arr_KoDauChars = this.KoDauChars.split('');
var pos;
for (var i = 0; i < s.length; i++)
{
pos = this.uniChars.indexOf(s[i]);
if (pos >= 0)
retVal+= arr_KoDauChars[pos];
else
retVal+= s[i];
}
return retVal;
}
this.UnicodeToKoDauAndGach = function(input)
{
var strChar = 'abcdefghiklmnopqrstxyzuvxw0123456789 ';
input = this.trim(input);
var str = input.replace("–", "");
str = str.replace("  ", " ");
str = this.UnicodeToKoDau(str.toLowerCase());
var s = str.split('');
var sReturn = "";
for (var i = 0; i < s.length; i++)
{
if (strChar.indexOf(s[i]) >-1)
{
if (s[i] != ' ')
sReturn+= s[i];
else if (i > 0 && s[i-1] != ' ' && s[i-1] != '-')
sReturn+= "-";
}
}
return sReturn;
}
this.GetCompanyInfoLink = function(sym)
{
var link='';
for (i=0;i<oc.length;i++)
{
if( oc[i].c.toLowerCase()==sym.toLowerCase())
{
var san='hose';
if(oc[i].san=='2') san='hastc';
if(oc[i].san=='9') san='upcom';
var cName = oc[i].m;
if(cName.lastIndexOf('(')>0)cName = cName.substring(0, cName.lastIndexOf('('))
cName = this.UnicodeToKoDauAndGach(cName)
link='/'+san+'/'+sym+'-'+cName+'.chn';
break;
}
}
return link;
}
this.GetCompanyName = function(sym)
{
var link='';
for (i=0;i<oc.length;i++)
{
if( oc[i].c.toLowerCase()==sym.toLowerCase())
{
var cName = oc[i].m;
if(cName.lastIndexOf('(')>0)cName = cName.substring(0, cName.lastIndexOf('('))
link = cName
break;
}
}
return link;
}
this.CreateImageElement=function (url,aUrl,Cat_ID)
{
var divObj=document.getElementById("divBannerSlide"+Cat_ID);
if(divObj.hasChildNodes())
{
var child=divObj.childNodes[0];
divObj.removeChild(child);
}
link=document.createElement("A");
link.href=aUrl
img = document.createElement("IMG");
img.src=url;
img.setAttribute("border","0");
img.border="0";
link.appendChild(img);
divObj.appendChild(link);
}
this.CreateFlashElement=function(url,w,h,Cat_ID)
{
var divObjANZ=document.getElementById("divBannerSlide"+Cat_ID);
if(divObjANZ.hasChildNodes())
{
var child=divObjANZ.childNodes[0];
divObjANZ.removeChild(child);
}
this.CreateFlashBanner(divObjANZ,url,w,h,'ANZ'+Cat_ID);
}
this.CreateFlashBanner=function (ctr,FlashFile,Width,Height,codename)
{
var div = document.createElement("DIV");
div.id = "div_"+codename;
ctr.appendChild(div);
FlashFile = 'http://cafef.vn/'+FlashFile;
var s11 = new SWFObject(FlashFile,"mediaplayer",Width,Height,"7", "#FFFFFF");
s11.write(div.id);
}
this.resizeFrame=function (id, height, width)
{
var objFrame = document.getElementById(id);
if (height > 0) objFrame.height = height;
if (width > 0) objFrame.width = width;
}
this.GotoSite=function ()
{
window.location.href=url;
}
var imgArray='';
var currImg='';
}
/*menu*/
function cf_ChangeMenu()
{
var __location=window.location.href;
var ok=(__location=="http://cafef.vn/");
this.changeMenuTT=function ()
{
document.getElementById("home1").src="http://cafef3.vcmedia.vn/images/images/menu_bg_left.gif";
document.getElementById("home2").style.backgroundColor="#cc0001";
document.getElementById("home4").style.color="white";
document.getElementById("home4").style.fontWeight="bold";
document.getElementById("home4").style.fontSize="11px";
document.getElementById("home4").style.fontFamily="Verdana";
document.getElementById("home3").src="http://cafef3.vcmedia.vn/images/images/menu_bg_right.gif";
}
if(this.ok)
{
document.getElementById("home1").src="http://cafef3.vcmedia.vn/images/images/menu_bg_left.gif";
document.getElementById("home2").style.backgroundColor="#cc0001";
document.getElementById("home4").style.color="white"
document.getElementById("home3").src="http://cafef3.vcmedia.vn/images/images/menu_bg_right.gif";
}
if(__location.indexOf("/du-lieu")>0||__location.indexOf("/Thi-truong-niem-yet")>0||__location.indexOf("-TTNY-")>0||__location.indexOf("/TDN-")>0 || __location.indexOf("/Tin-doanh-nghiep")>0 || __location.indexOf("/chart.aspx")>0)
{
document.getElementById("thitruong1").src="http://cafef3.vcmedia.vn/images/images/menu_bg_left.gif";
document.getElementById("thitruong2").style.backgroundColor="#cc0001";
document.getElementById("thitruong4").style.color="white"
document.getElementById("thitruong3").src="http://cafef3.vcmedia.vn/images/images/menu_bg_right.gif";
}
if(__location.indexOf("/Mua-ban-OTC")>0)
{
document.getElementById("otc1").src="http://cafef3.vcmedia.vn/images/images/menu_bg_left.gif";
document.getElementById("otc2").style.backgroundColor="#cc0001";
document.getElementById("otc4").style.color="white"
document.getElementById("otc3").src="http://cafef3.vcmedia.vn/images/images/menu_bg_right.gif";
}
if(__location.indexOf("/Danh-muc-dau-tu")>0)
{
document.getElementById("dmdt1").src="http://cafef3.vcmedia.vn/images/images/menu_bg_left.gif";
document.getElementById("dmdt2").style.backgroundColor="#cc0001";
document.getElementById("dmdt4").style.color="white"
document.getElementById("dmdt3").src="http://cafef3.vcmedia.vn/images/images/menu_bg_right.gif";
}
if(__location.indexOf("CA31/")>0||__location.indexOf("/thi-truong-chung-khoan")>0)
{
document.getElementById("tttk").style.color="#cc0001";
this.changeMenuTT();
}
if(__location.indexOf("CA35/")>0||__location.indexOf("/bat-dong-san")>0)
{
document.getElementById("bds").style.color="#cc0001";
this.changeMenuTT();
}
if(__location.indexOf("CA34/")>0||__location.indexOf("/tai-chinh-ngan-hang")>0)
{
document.getElementById("tcnh").style.color="#cc0001";
this.changeMenuTT();
}
if(__location.indexOf("CA32/")>0||__location.indexOf("/tai-chinh-quoc-te")>0)
{
document.getElementById("tcqt").style.color="#cc0001";
this.changeMenuTT();
}
if(__location.indexOf("CA36/")>0||__location.indexOf("/doanh-nghiep")>0)
{
document.getElementById("dn").style.color="#cc0001";
this.changeMenuTT();
}
if(__location.indexOf("CA33/")>0||__location.indexOf("kinh-te-dau-tu")>0 )
{
document.getElementById("ktdt").style.color="#cc0001";
this.changeMenuTT();
}
if(__location.indexOf("CA39/")>0||__location.indexOf("hang-hoa-nguyen-lieu")>0 )
{
if(document.getElementById("tthh"))
{
document.getElementById("tthh").style.color="#cc0001";
this.changeMenuTT();
}
}
var isHome=(__location=="http://cafef.vn/" || __location=="http://www.cafef.vn/");
isHome=isHome || (__location=="http://cafef.vn" || __location=="http://www.cafef.vn");
if(__location=="http://tintuc.cafef.channelvn.net/"||__location.indexOf("/home.chn")>0||__location.indexOf("/News.chn")>0||__location.indexOf("/y-kien-doc-gia.chn")>0 || isHome )
{
this.changeMenuTT();
}
this.ViewLiveBoard= function()
{
window.open('http://cafef.vn/liveboard/?f=1', '', 'fullscreen=1,scrollbars=1,directories=0,location=0,menubar=0,status=0,titlebar=0,toolbar=0,resizable=1');
}
if(__location=="http://cafef.vn" || __location=="http://www.cafef.vn" ||__location.indexOf("/News/")>0||__location.indexOf("/news.chn")>0)
{
document.getElementById("submenu").style.display="block";
}
if(__location=="http://www.cafef.vn/" || __location=="http://www.cafef.vn")
{
document.getElementById("submenu").style.display="block";
}
if(__location=="http://cafef.vn/"||__location.indexOf("/home.chn")>0||__location.indexOf("/News.chn")>0||__location.indexOf("/y-kien-doc-gia.chn")>0 ||__location.indexOf("/news.chn"))
{
document.getElementById("submenu").style.display="block";
}
if(__location.indexOf("CA31/")>0||__location.indexOf("/thi-truong-chung-khoan")>0)
{
document.getElementById("submenu").style.display="block";
}
if(__location.indexOf("CA35/")>0||__location.indexOf("/bat-dong-san")>0)
{
document.getElementById("submenu").style.display="block";
}
if(__location.indexOf("CA34/")>0||__location.indexOf("/tai-chinh-ngan-hang")>0)
{
document.getElementById("submenu").style.display="block";
}
if(__location.indexOf("CA32/")>0||__location.indexOf("/tai-chinh-quoc-te")>0)
{
document.getElementById("submenu").style.display="block";
}
if(__location.indexOf("CA36/")>0||__location.indexOf("/doanh-nghiep")>0)
{
document.getElementById("submenu").style.display="block";
}
if(__location.indexOf("CA33/")>0||__location.indexOf("kinh-te-dau-tu")>0 )
{
document.getElementById("submenu").style.display="block";
}
if(__location.indexOf("CA39/")>0||__location.indexOf("hang-hoa-nguyen-lieu")>0 )
{
document.getElementById("submenu").style.display="block";
}
}
function LaiXuatNganHang()
{
this.laixuat=function (val)
{
var data='';
this.setDefault(document.getElementById("div_okyhan"));
this.setDefault(document.getElementById("div_1"));
this.setDefault(document.getElementById("div_3"));
this.setDefault(document.getElementById("div_6"));
this.setDefault(document.getElementById("div_9"));
this.setDefault(document.getElementById("div_12"));
if (val==0)
{
data=laixuatkhongkyhan;
divitem=document.getElementById("div_okyhan")
this.setColor(divitem);
}
else if(val==1)
{
data=laixuat1T;
this.setColor(document.getElementById("div_1"));
}
else if(val==3)
{
data=laixuat3T;
this.setColor(document.getElementById("div_3"));
}
else if(val==6)
{
data=laixuat6T;
this.setColor(document.getElementById("div_6"));
}
else if(val==9)
{
data=laixuat12T;//nham nhot giua du lieu 9 thang va 12 thang
this.setColor(document.getElementById("div_9"));
}
else if(val==12)
{
data=laixuat9T;
this.setColor(document.getElementById("div_12"));
}
this.buildData(data);
}
this.UpdateLaiXuat=function (data)
{
var responseData = data;
}
this.buildData=function buildData(data)
{
var contentdiv=document.getElementById("laixuat_content");
if(contentdiv)
{
if(data && data.NewDataSet && data.NewDataSet.Table)
{
var Item='';
var alt=false;
for(i=0;i<data.NewDataSet.Table.length;i++)
{
if(alt){bg=' background-color: #F2F2F2;';}
else
{
bg=" background-color: #F2F2F2;";
}
alt=!alt;
Item+="<div style=\"overflow: hidden; "+bg+"  padding-top: 0px;height: 20px;border-bottom:solid 1px #e6e6e6\" >";
Item+="<div style=\"float: left; width: 92px; text-align: left; padding-left: 10px; font-weight:normal;padding-top: 3px;\">"+data.NewDataSet.Table[i].NganHang_Name+"</div>" ;
Item =Item+"<div style=\"float: left; width: 1px;background-color:#F2F2F2;height: 20px;\">&nbsp;</div>" ;
var SoTienGui='&nbsp;';
if(data.NewDataSet.Table[i].LaiXuat_SoTienGui)
{
SoTienGui=data.NewDataSet.Table[i].LaiXuat_SoTienGui;
}
Item =Item+"<div style=\"float: left; width: 52px;text-align:right;padding-right:3px;padding-top: 3px;\">"+data.NewDataSet.Table[i].LaiXuat_TienGui+"%</div>" ;
Item =Item+"<div style=\"float: left; width: 1px;background-color:#F2F2F2;height: 20px;\">&nbsp;</div>" ;
Item =Item+"<div style=\"float: left; width: 52px;text-align:right;padding-right:3px;padding-top: 3px;\">"+SoTienGui+"</div></div>" ;
}
contentdiv.innerHTML=Item;
}
}
}
this.setDefault=function (divitem)
{
if(divitem)
{
divitem.style.borderRight="solid 1px #E6E6E6";
divitem.style.backgroundColor="#fff" ;
divitem.style.borderBottom="solid 1px #E6E6E6";
}
}
this.setColor=function (divitem)
{
if(divitem)
{
divitem.style.borderRight="solid 1px #E6E6E6";
divitem.style.backgroundColor="#F2F2F2" ;
divitem.style.borderBottom="solid 1px #E6E6E6";
}
}
}
var CafeF_LaiXuatNH=new LaiXuatNganHang();
var CafeF_JSLibrary = new CafeF_Library();
function DownLoadFile(ID,Path) {
jQuery.ajax({
type: "GET",
url: "/GUI/Front_End/Portfolio/Controls/UpdatePortfolio.ashx",
data:{'a':'9','ID':ID} ,
contentType: "application/json; charset=utf-8",
dataType: "json",
success: function(msg) {
msg.Response.Message='';
},
failure: function(msg) {					;
}
});
}
function String2Float(value)
{
value=value.replace(',','');
return (value != '' ? parseFloat(value) : 0);
}
function AutoCompleteData()
{
this.doAutoComplete=function(ctl,inputWidth)
{
jQuery().ready(function() {
if(jQuery('#txtKeyword')==null) return;
jQuery('#txtKeyword').autocomplete(oc, {
minChars: 1,
delay: 10,
width: inputWidth,
matchContains: true,
autoFill: false,
formatItem: function(row) {
return row.c+"-"+row.m+"@"+row.o;
},
formatResult: function(row) {
return row.c+"-"+row.m;
}
});
});
}
}
/* Thumb for masterpage */
function loadErrorImage(id,src)
{
if (id.getAttribute("loi") == null)
{
id.setAttribute("loi","1");
}
else
{
id.setAttribute("loi",eval(id.getAttribute("loi"))+1);
}
if (eval(id.getAttribute("loi")) >=2)
{
var width = src.substr(src.lastIndexOf("=")+1,src.length-src.lastIndexOf("="));
id.onerror = null;
id.src = "http://cafef3.vcmedia.vn/images/ImagesGUI/no_image_138.jpg";
}
else
{
id.src = src;
}
}
function changeFrameHeight()
{
if(navigator.appName != "Microsoft Internet Explorer")
{
window.parent.CafeF_JSLibrary.resizeFrame('CafeF_EmbedDataRequest', document.body.scrollHeight-15 , '0');
}
else //IE
{
window.parent.CafeF_JSLibrary.resizeFrame('CafeF_EmbedDataRequest', document.body.scrollHeight , '0');
}
}
/*news list */
function NewsCategory()
{
this.__location=window.location.href;
this.BatDongSan=function()
{
if(this.__location.indexOf('/bat-dong-san.chn')>0)
{
var bEnbac=document.getElementById('divEnbac');
var bRongBay=document.getElementById('phdBDSRongbay');
var frmRongBay=document.getElementById('bds_rongbay');
if(bEnbac)
{
bEnbac.style.display='';
bEnbac.style.paddingTop='8px';
}
if(bRongBay)
{
bRongBay.style.display='';
bRongBay.style.paddingTop='8px';
if(frmRongBay)
{
frmRongBay.setAttribute("src", 'http://rongbay.com/widget/cafef.php');
}
}
}
var divWorldMarket=document.getElementById("divWorldMarket");
if(this.__location.indexOf('/thi-truong-chung-khoan.chn')>0)
{
var ifrm=document.getElementById("CafeF_EmbedDataRequest");
var divStockTable=document.getElementById("divSimpleStock");
var divStockTable=document.getElementById("divSimpleStock");
var divPortfolioUser=document.getElementById("divPortfolioUser");
var divLichSuLien=document.getElementById("divEvents");
if(divPortfolioUser)
{
divPortfolioUser.style.display='';
divPortfolioUser.style.paddingTop='8px';
}
if(divStockTable)
{
divStockTable.style.display='';
divStockTable.style.paddingTop='8px';
if(ifrm)
{
ifrm.setAttribute("src", '/cafef-tools/cf_stocklist.aspx');
}
}
if(divLichSuLien)
{
divLichSuLien.style.display='';
divLichSuLien.style.paddingTop='8px';
divLichSuLien.style.width="350px";
}
}
if(this.__location.indexOf('/thi-truong-chung-khoan.chn')>0 || this.__location.indexOf('/tai-chinh-quoc-te.chn')>0)
{
if(divWorldMarket)
{
divWorldMarket.style.display='';
divWorldMarket.style.paddingTop='8px';
divWorldMarket.style.width="350px";
}
}
}
}
function Format_onFocus(Id)
{
var obj=document.getElementById(ID)
if (obj)
{
if(obj.value!='')
{
}
}
}
function Format_onblur(obj,Place)
{
if (obj)
{
if(obj.value!='')
{
obj.value=formatNumber_input_decimal_format(obj.value,Place);
}
}
}
function Format_onblur_Check(obj,Place,max,InputType)
{
if (obj)
{
if(obj.value!='')
{
if(max>0)
{
GainLossAlert(false);
if(ConvertToFloat(obj.value)>max)
{
if(InputType==1) //price
{
PriceAlert(true);
obj.focus();
return;
}
else if(InputType==2) //gainloss
{
GainLossAlert(true);
obj.focus();
return;
}
}
}
obj.value=formatNumber_input_decimal_format(obj.value,Place);
}
}
}
function formatNumber_input_decimal_format(value,place)
{
var num = new NumberFormat();
num.setInputDecimal('.');
num.setNumber(value); // obj.value is '-1000,7'
num.setPlaces(place, true);
num.setCurrencyValue('$');
num.setCurrency(false);
num.setCurrencyPosition(num.LEFT_OUTSIDE);
num.setNegativeFormat(num.LEFT_DASH);
num.setNegativeRed(false);
num.setSeparators(true, ',', '.');
return num.toFormatted();
}
function pNumberFormat(value)
{
var num = new NumberFormat();
num.setInputDecimal('.');
num.setNumber(value);
num.setPlaces('2', false);
num.setNegativeFormat(num.LEFT_DASH);
num.setNegativeRed(false);
num.setSeparators(true, ',', '.');
return num.toFormatted();
}
function pNumberFormat(value,Place)
{
var num = new NumberFormat();
num.setInputDecimal('.');
num.setNumber(value);
if(Place=='')Place='2';
if(!Place ||Place=='undefined')Place='2';
num.setPlaces(Place, true);
num.setNegativeFormat(num.LEFT_DASH);
num.setNegativeRed(false);
num.setSeparators(true, ',', '.');
return num.toFormatted();
}
function NumberFormat(num, inputDecimal)
{
this.VERSION = 'Number Format v1.5.4';
this.COMMA = ',';//orginal:,
this.PERIOD = '.';//orginal:.
this.DASH = '-';
this.LEFT_PAREN = '(';
this.RIGHT_PAREN = ')';
this.LEFT_OUTSIDE = 0;
this.LEFT_INSIDE = 1;
this.RIGHT_INSIDE = 2;
this.RIGHT_OUTSIDE = 3;
this.LEFT_DASH = 0;
this.RIGHT_DASH = 1;
this.PARENTHESIS = 2;
this.NO_ROUNDING =-1
this.num;
this.numOriginal;
this.hasSeparators = false;
this.separatorValue;
this.inputDecimalValue;
this.decimalValue;
this.negativeFormat;
this.negativeRed;
this.hasCurrency;
this.currencyPosition;
this.currencyValue;
this.places;
this.roundToPlaces;
this.truncate;
this.setNumber = setNumberNF;
this.toUnformatted = toUnformattedNF;
this.setInputDecimal = setInputDecimalNF;
this.setSeparators = setSeparatorsNF;
this.setCommas = setCommasNF;
this.setNegativeFormat = setNegativeFormatNF;
this.setNegativeRed = setNegativeRedNF;
this.setCurrency = setCurrencyNF;
this.setCurrencyPrefix = setCurrencyPrefixNF;
this.setCurrencyValue = setCurrencyValueNF;
this.setCurrencyPosition = setCurrencyPositionNF;
this.setPlaces = setPlacesNF;
this.toFormatted = toFormattedNF;
this.toPercentage = toPercentageNF;
this.getOriginal = getOriginalNF;
this.moveDecimalRight = moveDecimalRightNF;
this.moveDecimalLeft = moveDecimalLeftNF;
this.getRounded = getRoundedNF;
this.preserveZeros = preserveZerosNF;
this.justNumber = justNumberNF;
this.expandExponential = expandExponentialNF;
this.getZeros = getZerosNF;
this.moveDecimalAsString = moveDecimalAsStringNF;
this.moveDecimal = moveDecimalNF;
this.addSeparators = addSeparatorsNF;
if (inputDecimal == null) {
this.setNumber(num, this.PERIOD);
} else {
this.setNumber(num, inputDecimal);
}
this.setCommas(true);
this.setNegativeFormat(this.LEFT_DASH);
this.setNegativeRed(false);
this.setCurrency(false);
this.setCurrencyPrefix('$');
this.setPlaces(2);
}
function setInputDecimalNF(val)
{
this.inputDecimalValue = val;
}
function setNumberNF(num, inputDecimal)
{
if (inputDecimal != null) {
this.setInputDecimal(inputDecimal);
}
this.numOriginal = num;
this.num = this.justNumber(num);
}
function toUnformattedNF()
{
return (this.num);
}
function getOriginalNF()
{
return (this.numOriginal);
}
function setNegativeFormatNF(format)
{
this.negativeFormat = format;
}
function setNegativeRedNF(isRed)
{
this.negativeRed = isRed;
}
function setSeparatorsNF(isC, separator, decimal)
{
this.hasSeparators = isC;
if (separator == null) separator = this.COMMA;
if (decimal == null) decimal = this.PERIOD;
if (separator == decimal) {
this.decimalValue = (decimal == this.PERIOD) ? this.COMMA : this.PERIOD;
} else {
this.decimalValue = decimal;
}
this.separatorValue = separator;
}
function setCommasNF(isC)
{
this.setSeparators(isC, this.COMMA, this.PERIOD);
}
function setCurrencyNF(isC)
{
this.hasCurrency = isC;
}
function setCurrencyValueNF(val)
{
this.currencyValue = val;
}
function setCurrencyPrefixNF(cp)
{
this.setCurrencyValue(cp);
this.setCurrencyPosition(this.LEFT_OUTSIDE);
}
function setCurrencyPositionNF(cp)
{
this.currencyPosition = cp
}
function setPlacesNF(p, tr)
{
this.roundToPlaces = !(p == this.NO_ROUNDING);
this.truncate = (tr != null && tr);
this.places = (p < 0) ? 0 : p;
}
function addSeparatorsNF(nStr, inD, outD, sep)
{
nStr+= '';
var dpos = nStr.indexOf(inD);
var nStrEnd = '';
if (dpos !=-1) {
nStrEnd = outD+nStr.substring(dpos+1, nStr.length);
nStr = nStr.substring(0, dpos);
}
var rgx = /(\d+)(\d{3})/;
while (rgx.test(nStr)) {
nStr = nStr.replace(rgx, '$1'+sep+'$2');
}
return nStr+nStrEnd;
}
function toFormattedNF()
{
var pos;
var nNum = this.num;
var nStr;
var splitString = new Array(2);
if (this.roundToPlaces) {
nNum = this.getRounded(nNum);
nStr = this.preserveZeros(Math.abs(nNum));
} else {
nStr = this.expandExponential(Math.abs(nNum));
}
if (this.hasSeparators) {
nStr = this.addSeparators(nStr, this.PERIOD, this.decimalValue, this.separatorValue);
} else {
nStr = nStr.replace(new RegExp('\\'+this.PERIOD), this.decimalValue);
}
var c0 = '';
var n0 = '';
var c1 = '';
var n1 = '';
var n2 = '';
var c2 = '';
var n3 = '';
var c3 = '';
var negSignL = (this.negativeFormat == this.PARENTHESIS) ? this.LEFT_PAREN : this.DASH;
var negSignR = (this.negativeFormat == this.PARENTHESIS) ? this.RIGHT_PAREN : this.DASH;
if (this.currencyPosition == this.LEFT_OUTSIDE) {
if (nNum < 0) {
if (this.negativeFormat == this.LEFT_DASH || this.negativeFormat == this.PARENTHESIS) n1 = negSignL;
if (this.negativeFormat == this.RIGHT_DASH || this.negativeFormat == this.PARENTHESIS) n2 = negSignR;
}
if (this.hasCurrency) c0 = this.currencyValue;
} else if (this.currencyPosition == this.LEFT_INSIDE) {
if (nNum < 0) {
if (this.negativeFormat == this.LEFT_DASH || this.negativeFormat == this.PARENTHESIS) n0 = negSignL;
if (this.negativeFormat == this.RIGHT_DASH || this.negativeFormat == this.PARENTHESIS) n3 = negSignR;
}
if (this.hasCurrency) c1 = this.currencyValue;
}
else if (this.currencyPosition == this.RIGHT_INSIDE) {
if (nNum < 0) {
if (this.negativeFormat == this.LEFT_DASH || this.negativeFormat == this.PARENTHESIS) n0 = negSignL;
if (this.negativeFormat == this.RIGHT_DASH || this.negativeFormat == this.PARENTHESIS) n3 = negSignR;
}
if (this.hasCurrency) c2 = this.currencyValue;
}
else if (this.currencyPosition == this.RIGHT_OUTSIDE) {
if (nNum < 0) {
if (this.negativeFormat == this.LEFT_DASH || this.negativeFormat == this.PARENTHESIS) n1 = negSignL;
if (this.negativeFormat == this.RIGHT_DASH || this.negativeFormat == this.PARENTHESIS) n2 = negSignR;
}
if (this.hasCurrency) c3 = this.currencyValue;
}
nStr = c0+n0+c1+n1+nStr+n2+c2+n3+c3;
if (this.negativeRed && nNum < 0) {
nStr = '<font color="red">'+nStr+'</font>';
}
return (nStr);
}
function toPercentageNF()
{
nNum = this.num * 100;
nNum = this.getRounded(nNum);
return nNum+'%';
}
function getZerosNF(places)
{
var extraZ = '';
var i;
for (i=0; i<places; i++) {
extraZ+= '0';
}
return extraZ;
}
function expandExponentialNF(origVal)
{
if (isNaN(origVal)) return origVal;
var newVal = parseFloat(origVal)+'';
var eLoc = newVal.toLowerCase().indexOf('e');
if (eLoc !=-1) {
var plusLoc = newVal.toLowerCase().indexOf('+');
var negLoc = newVal.toLowerCase().indexOf('-', eLoc);
var justNumber = newVal.substring(0, eLoc);
if (negLoc !=-1) {
var places = newVal.substring(negLoc+1, newVal.length);
justNumber = this.moveDecimalAsString(justNumber, true, parseInt(places));
} else {
if (plusLoc ==-1) plusLoc = eLoc;
var places = newVal.substring(plusLoc+1, newVal.length);
justNumber = this.moveDecimalAsString(justNumber, false, parseInt(places));
}
newVal = justNumber;
}
return newVal;
}
function moveDecimalRightNF(val, places)
{
var newVal = '';
if (places == null) {
newVal = this.moveDecimal(val, false);
} else {
newVal = this.moveDecimal(val, false, places);
}
return newVal;
}
function moveDecimalLeftNF(val, places)
{
var newVal = '';
if (places == null) {
newVal = this.moveDecimal(val, true);
} else {
newVal = this.moveDecimal(val, true, places);
}
return newVal;
}
function moveDecimalAsStringNF(val, left, places)
{
var spaces = (arguments.length < 3) ? this.places : places;
if (spaces <= 0) return val;
var newVal = val+'';
var extraZ = this.getZeros(spaces);
var re1 = new RegExp('([0-9.]+)');
if (left) {
newVal = newVal.replace(re1, extraZ+'$1');
var re2 = new RegExp('(-?)([0-9]*)([0-9]{'+spaces+'})(\\.?)');
newVal = newVal.replace(re2, '$1$2.$3');
} else {
var reArray = re1.exec(newVal);
if (reArray != null) {
newVal = newVal.substring(0,reArray.index)+reArray[1]+extraZ+newVal.substring(reArray.index+reArray[0].length);
}
var re2 = new RegExp('(-?)([0-9]*)(\\.?)([0-9]{'+spaces+'})');
newVal = newVal.replace(re2, '$1$2$4.');
}
newVal = newVal.replace(/\.$/, '');
return newVal;
}
function moveDecimalNF(val, left, places)
{
var newVal = '';
if (places == null) {
newVal = this.moveDecimalAsString(val, left);
} else {
newVal = this.moveDecimalAsString(val, left, places);
}
return parseFloat(newVal);
}
function getRoundedNF(val)
{
val = this.moveDecimalRight(val);
if (this.truncate) {
val = val >= 0 ? Math.floor(val) : Math.ceil(val);
} else {
val = Math.round(val);
}
val = this.moveDecimalLeft(val);
return val;
}
function preserveZerosNF(val)
{
var i;
val = this.expandExponential(val);
if (this.places <= 0) return val;
var decimalPos = val.indexOf('.');
if (decimalPos ==-1) {
val+= '.';
for (i=0; i<this.places; i++) {
val+= '0';
}
} else {
var actualDecimals = (val.length-1)-decimalPos;
var difference = this.places-actualDecimals;
for (i=0; i<difference; i++) {
val+= '0';
}
}
return val;
}
function justNumberNF(val)
{
newVal = val+'';
var isPercentage = false;
if (newVal.indexOf('%') !=-1) {
newVal = newVal.replace(/\%/g, '');
isPercentage = true;
}
var re = new RegExp('[^\\'+this.inputDecimalValue+'\\d\\-\\+\\(\\)eE]', 'g');
newVal = newVal.replace(re, '');
var tempRe = new RegExp('['+this.inputDecimalValue+']', 'g');
var treArray = tempRe.exec(newVal);
if (treArray != null) {
var tempRight = newVal.substring(treArray.index+treArray[0].length);
newVal = newVal.substring(0,treArray.index)+this.PERIOD+tempRight.replace(tempRe, '');
}
if (newVal.charAt(newVal.length-1) == this.DASH ) {
newVal = newVal.substring(0, newVal.length-1);
newVal = '-'+newVal;
}
else if (newVal.charAt(0) == this.LEFT_PAREN
&& newVal.charAt(newVal.length-1) == this.RIGHT_PAREN) {
newVal = newVal.substring(1, newVal.length-1);
newVal = '-'+newVal;
}
newVal = parseFloat(newVal);
if (!isFinite(newVal)) {
newVal = 0;
}
if (isPercentage) {
newVal = this.moveDecimalLeft(newVal, 2);
}
return newVal;
}
function H_ResizeToCenter()
{
var swidth=screen.width;
var url=window.location.href;
var host=window.location.host;
var i=url.indexOf('tab-');
var tab=url.substring(i+4,i+5);
var divC=document.getElementById('cf_ContainerBox');
if(swidth>=1280)
{
if (tab!='4')
{
if(tab=='2')
{
divC.style.width='1024px';
}
else
{
divC.style.width='1160px';
}
}
else
{
divC.style.width='1220px';
}
divC.style.paddingLeft='20px';
}
else
{
divC.style.width='1000px';
divC.style.paddingLeft='0px';
}
}
function LichSuGia_KeyHander(e,nextId)
{
if (!e) e = window.event;
if (e.keyCode == 13 || e.keyCode == 9)
{
var nextControl = document.getElementById(nextId);
if (nextControl)
{
nextControl.focus();
}
e.cancelBubble = true;
e.returnValue=false;
e.cancel = true;
return false;
}
return true;
}(function(){
/*
* jQuery @VERSION-New Wave Javascript
*
* Copyright (c) 2007 John Resig (jquery.com)
* Dual licensed under the MIT (MIT-LICENSE.txt)
* and GPL (GPL-LICENSE.txt) licenses.
*
* $Date: 2007-11-19 17:07:44+0100 (Mon, 19 Nov 2007) $
* $Rev: 3856 $
*/
if ( window.jQuery )
var _jQuery = window.jQuery;
var jQuery = window.jQuery = function( selector, context ) {
return this instanceof jQuery ?
this.init( selector, context ) :
new jQuery( selector, context );
};
if ( window.$ )
var _$ = window.$;
window.$ = jQuery;
var quickExpr = /^[^<]*(<(.|\s)+>)[^>]*$|^#(\w+)$/;
jQuery.fn = jQuery.prototype = {
init: function( selector, context ) {
selector = selector || document;
if ( typeof selector  == "string" ) {
var match = quickExpr.exec( selector );
if ( match && (match[1] || !context) ) {
if ( match[1] )
selector = jQuery.clean( [ match[1] ], context );
else {
var elem = document.getElementById( match[3] );
if ( elem )
if ( elem.id != match[3] )
return jQuery().find( selector );
else {
this[0] = elem;
this.length = 1;
return this;
}
else
selector = [];
}
} else
return new jQuery( context ).find( selector );
} else if ( jQuery.isFunction( selector ) )
return new jQuery( document )[ jQuery.fn.ready ? "ready" : "load" ]( selector );
return this.setArray(
selector.constructor == Array && selector ||
(selector.jquery || selector.length && selector != window && !selector.nodeType && selector[0] != undefined && selector[0].nodeType) && jQuery.makeArray( selector ) ||
[ selector ] );
},
jquery: "@VERSION",
size: function() {
return this.length;
},
length: 0,
get: function( num ) {
return num == undefined ?
jQuery.makeArray( this ) :
this[ num ];
},
pushStack: function( elems ) {
var ret = jQuery( elems );
ret.prevObject = this;
return ret;
},
setArray: function( elems ) {
this.length = 0;
Array.prototype.push.apply( this, elems );
return this;
},
each: function( callback, args ) {
return jQuery.each( this, callback, args );
},
index: function( elem ) {
var ret =-1;
this.each(function(i){
if ( this == elem )
ret = i;
});
return ret;
},
attr: function( name, value, type ) {
var options = name;
if ( name.constructor == String )
if ( value == undefined )
return this.length && jQuery[ type || "attr" ]( this[0], name ) || undefined;
else {
options = {};
options[ name ] = value;
}
return this.each(function(i){
for ( name in options )
jQuery.attr(
type ?
this.style :
this,
name, jQuery.prop( this, options[ name ], type, i, name )
);
});
},
css: function( key, value ) {
return this.attr( key, value, "curCSS" );
},
text: function( text ) {
if ( typeof text != "object" && text != null )
return this.empty().append( document.createTextNode( text ) );
var ret = "";
jQuery.each( text || this, function(){
jQuery.each( this.childNodes, function(){
if ( this.nodeType != 8 )
ret+= this.nodeType != 1 ?
this.nodeValue :
jQuery.fn.text( [ this ] );
});
});
return ret;
},
wrapAll: function( html ) {
if ( this[0] )
jQuery( html, this[0].ownerDocument )
.clone()
.insertBefore( this[0] )
.map(function(){
var elem = this;
while ( elem.firstChild )
elem = elem.firstChild;
return elem;
})
.append(this);
return this;
},
wrapInner: function( html ) {
return this.each(function(){
jQuery( this ).contents().wrapAll( html );
});
},
wrap: function( html ) {
return this.each(function(){
jQuery( this ).wrapAll( html );
});
},
append: function() {
return this.domManip(arguments, true, false, function(elem){
this.appendChild( elem );
});
},
prepend: function() {
return this.domManip(arguments, true, true, function(elem){
this.insertBefore( elem, this.firstChild );
});
},
before: function() {
return this.domManip(arguments, false, false, function(elem){
this.parentNode.insertBefore( elem, this );
});
},
after: function() {
return this.domManip(arguments, false, true, function(elem){
this.parentNode.insertBefore( elem, this.nextSibling );
});
},
end: function() {
return this.prevObject || jQuery( [] );
},
find: function( selector ) {
var elems = jQuery.map(this, function(elem){
return jQuery.find( selector, elem );
});
return this.pushStack( /[^+>] [^+>]/.test( selector ) || selector.indexOf("..") >-1 ?
jQuery.unique( elems ) :
elems );
},
clone: function( events ) {
var ret = this.map(function(){
return this.outerHTML ?
jQuery( this.outerHTML )[0] :
this.cloneNode( true );
});
var clone = ret.find("*").andSelf().each(function(){
if ( this[ expando ] != undefined )
this[ expando ] = null;
});
if ( events === true )
this.find("*").andSelf().each(function(i){
var events = jQuery.data( this, "events" );
for ( var type in events )
for ( var handler in events[ type ] )
jQuery.event.add( clone[ i ], type, events[ type ][ handler ], events[ type ][ handler ].data );
});
return ret;
},
filter: function( selector ) {
return this.pushStack(
jQuery.isFunction( selector ) &&
jQuery.grep(this, function(elem, i){
return selector.call( elem, i );
}) ||
jQuery.multiFilter( selector, this ) );
},
not: function( selector ) {
return this.pushStack(
selector.constructor == String &&
jQuery.multiFilter( selector, this, true ) ||
jQuery.grep(this, function(elem) {
return selector.constructor == Array || selector.jquery ?
jQuery.inArray( elem, selector ) < 0 :
elem != selector;
}) );
},
add: function( selector ) {
return this.pushStack( jQuery.merge(
this.get(),
selector.constructor == String ?
jQuery( selector ).get() :
selector.length != undefined && (!selector.nodeName || jQuery.nodeName(selector, "form")) ?
selector : [selector] ) );
},
is: function( selector ) {
return selector ?
jQuery.multiFilter( selector, this ).length > 0 :
false;
},
hasClass: function( selector ) {
return this.is( "."+selector );
},
val: function( value ) {
if ( value == undefined ) {
if ( this.length ) {
var elem = this[0];
if ( jQuery.nodeName( elem, "select" ) ) {
var index = elem.selectedIndex,
values = [],
options = elem.options,
one = elem.type == "select-one";
if ( index < 0 )
return null;
for ( var i = one ? index : 0, max = one ? index+1 : options.length; i < max; i++) {
var option = options[ i ];
if ( option.selected ) {
value = jQuery.browser.msie && !option.attributes.value.specified ? option.text : option.value;
if ( one )
return value;
values.push( value );
}
}
return values;
} else
return this[0].value.replace(/\r/g, "");
}
} else
return this.each(function(){
if ( value.constructor == Array && /radio|checkbox/.test( this.type ) )
this.checked = (jQuery.inArray(this.value, value) >= 0 ||
jQuery.inArray(this.name, value) >= 0);
else if ( jQuery.nodeName( this, "select" ) ) {
var values = value.constructor == Array ?
value :
[ value ];
jQuery( "option", this ).each(function(){
this.selected = (jQuery.inArray( this.value, values ) >= 0 ||
jQuery.inArray( this.text, values ) >= 0);
});
if ( !values.length )
this.selectedIndex =-1;
} else
this.value = value;
});
},
html: function( value ) {
return value == undefined ?
(this.length ?
this[0].innerHTML :
null) :
this.empty().append( value );
},
replaceWith: function( value ) {
return this.after( value ).remove();
},
eq: function( i ) {
return this.slice( i, i+1 );
},
slice: function() {
return this.pushStack( Array.prototype.slice.apply( this, arguments ) );
},
map: function( callback ) {
return this.pushStack( jQuery.map(this, function(elem, i){
return callback.call( elem, i, elem );
}));
},
andSelf: function() {
return this.add( this.prevObject );
},
domManip: function( args, table, reverse, callback ) {
var clone = this.length > 1, elems;
return this.each(function(){
if ( !elems ) {
elems = jQuery.clean( args, this.ownerDocument );
if ( reverse )
elems.reverse();
}
var obj = this;
if ( table && jQuery.nodeName( this, "table" ) && jQuery.nodeName( elems[0], "tr" ) )
obj = this.getElementsByTagName("tbody")[0] || this.appendChild( document.createElement("tbody") );
var scripts = jQuery( [] );
jQuery.each(elems, function(){
var elem = clone ?
this.cloneNode( true ) :
this;
if ( jQuery.nodeName( elem, "script" ) ) {
if ( scripts.length )
scripts = scripts.add( elem );
else
evalScript( 0, elem );
} else {
if ( elem.nodeType == 1 )
scripts = scripts.add( jQuery( "script", elem ).remove() );
callback.call( obj, elem );
}
});
scripts.each( evalScript );
});
}
};
function evalScript( i, elem ) {
if ( elem.src )
jQuery.ajax({
url: elem.src,
async: false,
dataType: "script"
});
else
jQuery.globalEval( elem.text || elem.textContent || elem.innerHTML || "" );
if ( elem.parentNode )
elem.parentNode.removeChild( elem );
}
jQuery.extend = jQuery.fn.extend = function() {
var target = arguments[0] || {}, i = 1, length = arguments.length, deep = false, options;
if ( target.constructor == Boolean ) {
deep = target;
target = arguments[1] || {};
i = 2;
}
if ( typeof target != "object" )
target = {};
if ( length == 1 ) {
target = this;
i = 0;
}
for ( ; i < length; i++)
if ( (options = arguments[ i ]) != null )
for ( var name in options ) {
if ( target === options[ name ] )
continue;
if ( deep && typeof options[ name ] == "object" && target[ name ] && !options[ name ].nodeType )
target[ name ] = jQuery.extend( target[ name ], options[ name ] );
else if ( options[ name ] != undefined )
target[ name ] = options[ name ];
}
return target;
};
var expando = "jQuery"+(new Date()).getTime(), uuid = 0, windowData = {};
var exclude = /z-?index|font-?weight|opacity|zoom|line-?height/i;
jQuery.extend({
noConflict: function( deep ) {
window.$ = _$;
if ( deep )
window.jQuery = _jQuery;
return jQuery;
},
isFunction: function( fn ) {
return !!fn && typeof fn != "string" && !fn.nodeName &&
fn.constructor != Array && /function/i.test( fn+"" );
},
isXMLDoc: function( elem ) {
return elem.documentElement && !elem.body ||
elem.tagName && elem.ownerDocument && !elem.ownerDocument.body;
},
globalEval: function( data ) {
data = jQuery.trim( data );
if ( data ) {
var head = document.getElementsByTagName("head")[0] || document.documentElement,
script = document.createElement("script");
script.type = "text/javascript";
if ( jQuery.browser.msie )
script.text = data;
else
script.appendChild( document.createTextNode( data ) );
head.appendChild( script );
head.removeChild( script );
}
},
nodeName: function( elem, name ) {
return elem.nodeName && elem.nodeName.toUpperCase() == name.toUpperCase();
},
cache: {},
data: function( elem, name, data ) {
elem = elem == window ?
windowData :
elem;
var id = elem[ expando ];
if ( !id )
id = elem[ expando ] =++uuid;
if ( name && !jQuery.cache[ id ] )
jQuery.cache[ id ] = {};
if ( data != undefined )
jQuery.cache[ id ][ name ] = data;
return name ?
jQuery.cache[ id ][ name ] :
id;
},
removeData: function( elem, name ) {
elem = elem == window ?
windowData :
elem;
var id = elem[ expando ];
if ( name ) {
if ( jQuery.cache[ id ] ) {
delete jQuery.cache[ id ][ name ];
name = "";
for ( name in jQuery.cache[ id ] )
break;
if ( !name )
jQuery.removeData( elem );
}
} else {
try {
delete elem[ expando ];
} catch(e){
if ( elem.removeAttribute )
elem.removeAttribute( expando );
}
delete jQuery.cache[ id ];
}
},
each: function( object, callback, args ) {
if ( args ) {
if ( object.length == undefined )
for ( var name in object )
callback.apply( object[ name ], args );
else
for ( var i = 0, length = object.length; i < length; i++)
if ( callback.apply( object[ i ], args ) === false )
break;
} else {
if ( object.length == undefined )
for ( var name in object )
callback.call( object[ name ], name, object[ name ] );
else
for ( var i = 0, length = object.length, value = object[0];
i < length && callback.call( value, i, value ) !== false; value = object[++i] ){}
}
return object;
},
prop: function( elem, value, type, i, name ) {
if ( jQuery.isFunction( value ) )
value = value.call( elem, i );
return value && value.constructor == Number && type == "curCSS" && !exclude.test( name ) ?
value+"px" :
value;
},
className: {
add: function( elem, classNames ) {
jQuery.each((classNames || "").split(/\s+/), function(i, className){
if ( !jQuery.className.has( elem.className, className ) )
elem.className+= (elem.className ? " " : "")+className;
});
},
remove: function( elem, classNames ) {
elem.className = classNames != undefined ?
jQuery.grep(elem.className.split(/\s+/), function(className){
return !jQuery.className.has( classNames, className );
}).join(" ") :
"";
},
has: function( elem, className ) {
return jQuery.inArray( className, (elem.className || elem).toString().split(/\s+/) ) >-1;
}
},
swap: function( elem, options, callback ) {
for ( var name in options ) {
elem.style[ "old"+name ] = elem.style[ name ];
elem.style[ name ] = options[ name ];
}
callback.call( elem );
for ( var name in options )
elem.style[ name ] = elem.style[ "old"+name ];
},
css: function( elem, name ) {
if ( name == "height" || name == "width" ) {
var old = {}, height, width;
jQuery.each([ "Top", "Bottom", "Right", "Left" ], function(){
old[ "padding"+this ] = 0;
old[ "border"+this+"Width" ] = 0;
});
jQuery.swap( elem, old, function() {
if ( jQuery( elem ).is(":visible") ) {
height = elem.offsetHeight;
width = elem.offsetWidth;
} else {
elem = jQuery( elem.cloneNode(true) )
.find(":radio").removeAttr("checked").removeAttr("defaultChecked").end()
.css({
visibility: "hidden",
position: "absolute",
display: "block",
right: "0",
left: "0"
}).appendTo( elem.parentNode )[0];
var position = jQuery.css( elem.parentNode, "position" ) || "static";
if ( position == "static" )
elem.parentNode.style.position = "relative";
height = elem.clientHeight;
width = elem.clientWidth;
if ( position == "static" )
elem.parentNode.style.position = "static";
elem.parentNode.removeChild( elem );
}
});
return name == "height" ?
height :
width;
}
return jQuery.curCSS( elem, name );
},
curCSS: function( elem, name, force ) {
var ret;
function color( elem ) {
if ( !jQuery.browser.safari )
return false;
var ret = document.defaultView.getComputedStyle( elem, null );
return !ret || ret.getPropertyValue("color") == "";
}
if ( name == "opacity" && jQuery.browser.msie ) {
ret = jQuery.attr( elem.style, "opacity" );
return ret == "" ?
"1" :
ret;
}
if ( name.match( /float/i ) )
name = styleFloat;
if ( !force && elem.style[ name ] )
ret = elem.style[ name ];
else if ( document.defaultView && document.defaultView.getComputedStyle ) {
if ( name.match( /float/i ) )
name = "float";
name = name.replace( /([A-Z])/g, "-$1" ).toLowerCase();
var getComputedStyle = document.defaultView.getComputedStyle( elem, null );
if ( getComputedStyle && !color( elem ) )
ret = getComputedStyle.getPropertyValue( name );
else {
var swap = [], stack = [];
for ( var a = elem; a && color(a); a = a.parentNode )
stack.unshift(a);
for ( var i = 0; i < stack.length; i++)
if ( color( stack[ i ] ) ) {
swap[ i ] = stack[ i ].style.display;
stack[ i ].style.display = "block";
}
ret = name == "display" && swap[ stack.length-1 ] != null ?
"none" :
( getComputedStyle && getComputedStyle.getPropertyValue( name ) ) || "";
for ( var i = 0; i < swap.length; i++)
if ( swap[ i ] != null )
stack[ i ].style.display = swap[ i ];
}
if ( name == "opacity" && ret == "" )
ret = "1";
} else if ( elem.currentStyle ) {
var camelCase = name.replace(/\-(\w)/g, function(all, letter){
return letter.toUpperCase();
});
ret = elem.currentStyle[ name ] || elem.currentStyle[ camelCase ];
if ( !/^\d+(px)?$/i.test( ret ) && /^\d/.test( ret ) ) {
var style = elem.style.left, runtimeStyle = elem.runtimeStyle.left;
elem.runtimeStyle.left = elem.currentStyle.left;
elem.style.left = ret || 0;
ret = elem.style.pixelLeft+"px";
elem.style.left = style;
elem.runtimeStyle.left = runtimeStyle;
}
}
return ret;
},
clean: function( elems, context ) {
var ret = [];
context = context || document;
jQuery.each(elems, function(i, elem){
if ( !elem )
return;
if ( elem.constructor == Number )
elem = elem.toString();
if ( typeof elem == "string" ) {
elem = elem.replace(/(<(\w+)[^>]*?)\/>/g, function(all, front, tag){
return tag.match(/^(abbr|br|col|img|input|link|meta|param|hr|area)$/i) ?
all :
front+"></"+tag+">";
});
var tags = jQuery.trim( elem ).toLowerCase(), div = context.createElement("div");
var wrap =
!tags.indexOf("<opt") &&
[ 1, "<select multiple='multiple'>", "</select>" ] ||
!tags.indexOf("<leg") &&
[ 1, "<fieldset>", "</fieldset>" ] ||
tags.match(/^<(thead|tbody|tfoot|colg|cap)/) &&
[ 1, "<table>", "</table>" ] ||
!tags.indexOf("<tr") &&
[ 2, "<table><tbody>", "</tbody></table>" ] ||
(!tags.indexOf("<td") || !tags.indexOf("<th")) &&
[ 3, "<table><tbody><tr>", "</tr></tbody></table>" ] ||
!tags.indexOf("<col") &&
[ 2, "<table><tbody></tbody><colgroup>", "</colgroup></table>" ] ||
jQuery.browser.msie &&
[ 1, "div<div>", "</div>" ] ||
[ 0, "", "" ];
div.innerHTML = wrap[1]+elem+wrap[2];
while ( wrap[0]--)
div = div.lastChild;
if ( jQuery.browser.msie ) {
var tbody = !tags.indexOf("<table") && tags.indexOf("<tbody") < 0 ?
div.firstChild && div.firstChild.childNodes :
wrap[1] == "<table>" && tags.indexOf("<tbody") < 0 ?
div.childNodes :
[];
for ( var i = tbody.length-1; i >= 0 ;--i )
if ( jQuery.nodeName( tbody[ i ], "tbody" ) && !tbody[ i ].childNodes.length )
tbody[ i ].parentNode.removeChild( tbody[ i ] );
if ( /^\s/.test( elem ) )
div.insertBefore( context.createTextNode( elem.match(/^\s*/)[0] ), div.firstChild );
}
elem = jQuery.makeArray( div.childNodes );
}
if ( elem.length === 0 && (!jQuery.nodeName( elem, "form" ) && !jQuery.nodeName( elem, "select" )) )
return;
if ( elem[0] == undefined || jQuery.nodeName( elem, "form" ) || elem.options )
ret.push( elem );
else
ret = jQuery.merge( ret, elem );
});
return ret;
},
attr: function( elem, name, value ) {
var fix = jQuery.isXMLDoc( elem ) ?
{} :
jQuery.props;
if ( name == "selected" && jQuery.browser.safari )
elem.parentNode.selectedIndex;
if ( fix[ name ] ) {
if ( value != undefined )
elem[ fix[ name ] ] = value;
return elem[ fix[ name ] ];
} else if ( jQuery.browser.msie && name == "style" )
return jQuery.attr( elem.style, "cssText", value );
else if ( value == undefined && jQuery.browser.msie && jQuery.nodeName( elem, "form" ) && (name == "action" || name == "method") )
return elem.getAttributeNode( name ).nodeValue;
else if ( elem.tagName ) {
if ( value != undefined ) {
if ( name == "type" && jQuery.nodeName( elem, "input" ) && elem.parentNode )
throw "type property can't be changed";
elem.setAttribute( name, value );
}
if ( jQuery.browser.msie && /href|src/.test( name ) && !jQuery.isXMLDoc( elem ) )
return elem.getAttribute( name, 2 );
return elem.getAttribute( name );
} else {
if ( name == "opacity" && jQuery.browser.msie ) {
if ( value != undefined ) {
elem.zoom = 1;
elem.filter = (elem.filter || "").replace( /alpha\([^)]*\)/, "" )+
(parseFloat( value ).toString() == "NaN" ? "" : "alpha(opacity="+value * 100+")");
}
return elem.filter ?
(parseFloat( elem.filter.match(/opacity=([^)]*)/)[1] ) / 100).toString() :
"";
}
name = name.replace(/-([a-z])/ig, function(all, letter){
return letter.toUpperCase();
});
if ( value != undefined )
elem[ name ] = value;
return elem[ name ];
}
},
trim: function( text ) {
return (text || "").replace( /^\s+|\s+$/g, "" );
},
makeArray: function( array ) {
var ret = [];
if ( typeof array != "array" )
for ( var i = 0, length = array.length; i < length; i++)
ret.push( array[ i ] );
else
ret = array.slice( 0 );
return ret;
},
inArray: function( elem, array ) {
for ( var i = 0, length = array.length; i < length; i++)
if ( array[ i ] == elem )
return i;
return-1;
},
merge: function( first, second ) {
if ( jQuery.browser.msie ) {
for ( var i = 0; second[ i ]; i++)
if ( second[ i ].nodeType != 8 )
first.push( second[ i ] );
} else
for ( var i = 0; second[ i ]; i++)
first.push( second[ i ] );
return first;
},
unique: function( array ) {
var ret = [], done = {};
try {
for ( var i = 0, length = array.length; i < length; i++) {
var id = jQuery.data( array[ i ] );
if ( !done[ id ] ) {
done[ id ] = true;
ret.push( array[ i ] );
}
}
} catch( e ) {
ret = array;
}
return ret;
},
grep: function( elems, callback, inv ) {
if ( typeof callback == "string" )
callback = eval("false||function(a,i){return "+callback+"}");
var ret = [];
for ( var i = 0, length = elems.length; i < length; i++)
if ( !inv && callback( elems[ i ], i ) || inv && !callback( elems[ i ], i ) )
ret.push( elems[ i ] );
return ret;
},
map: function( elems, callback ) {
var ret = [];
for ( var i = 0, length = elems.length; i < length; i++) {
var value = callback( elems[ i ], i );
if ( value !== null && value != undefined ) {
if ( value.constructor != Array )
value = [ value ];
ret = ret.concat( value );
}
}
return ret;
}
});
var userAgent = navigator.userAgent.toLowerCase();
jQuery.browser = {
version: (userAgent.match( /.+(?:rv|it|ra|ie)[\/: ]([\d.]+)/ ) || [])[1],
safari: /webkit/.test( userAgent ),
opera: /opera/.test( userAgent ),
msie: /msie/.test( userAgent ) && !/opera/.test( userAgent ),
mozilla: /mozilla/.test( userAgent ) && !/(compatible|webkit)/.test( userAgent )
};
var styleFloat = jQuery.browser.msie ?
"styleFloat" :
"cssFloat";
jQuery.extend({
boxModel: !jQuery.browser.msie || document.compatMode == "CSS1Compat",
props: {
"for": "htmlFor",
"class": "className",
"float": styleFloat,
cssFloat: styleFloat,
styleFloat: styleFloat,
innerHTML: "innerHTML",
className: "className",
value: "value",
disabled: "disabled",
checked: "checked",
readonly: "readOnly",
selected: "selected",
maxlength: "maxLength",
selectedIndex: "selectedIndex",
defaultValue: "defaultValue",
tagName: "tagName",
nodeName: "nodeName"
}
});
jQuery.each({
parent: "elem.parentNode",
parents: "jQuery.dir(elem,'parentNode')",
next: "jQuery.nth(elem,2,'nextSibling')",
prev: "jQuery.nth(elem,2,'previousSibling')",
nextAll: "jQuery.dir(elem,'nextSibling')",
prevAll: "jQuery.dir(elem,'previousSibling')",
siblings: "jQuery.sibling(elem.parentNode.firstChild,elem)",
children: "jQuery.sibling(elem.firstChild)",
contents: "jQuery.nodeName(elem,'iframe')?elem.contentDocument||elem.contentWindow.document:jQuery.makeArray(elem.childNodes)"
}, function(name, fn){
fn = eval("false||function(elem){return "+fn+"}");
jQuery.fn[ name ] = function( selector ) {
var ret = jQuery.map( this, fn );
if ( selector && typeof selector == "string" )
ret = jQuery.multiFilter( selector, ret );
return this.pushStack( jQuery.unique( ret ) );
};
});
jQuery.each({
appendTo: "append",
prependTo: "prepend",
insertBefore: "before",
insertAfter: "after",
replaceAll: "replaceWith"
}, function(name, original){
jQuery.fn[ name ] = function() {
var args = arguments;
return this.each(function(){
for ( var i = 0, length = args.length; i < length; i++)
jQuery( args[ i ] )[ original ]( this );
});
};
});
jQuery.each({
removeAttr: function( name ) {
jQuery.attr( this, name, "" );
this.removeAttribute( name );
},
addClass: function( classNames ) {
jQuery.className.add( this, classNames );
},
removeClass: function( classNames ) {
jQuery.className.remove( this, classNames );
},
toggleClass: function( classNames ) {
jQuery.className[ jQuery.className.has( this, classNames ) ? "remove" : "add" ]( this, classNames );
},
remove: function( selector ) {
if ( !selector || jQuery.filter( selector, [ this ] ).r.length ) {
jQuery( "*", this ).add(this).each(function(){
jQuery.event.remove(this);
jQuery.removeData(this);
});
if (this.parentNode)
this.parentNode.removeChild( this );
}
},
empty: function() {
jQuery( ">*", this ).remove();
while ( this.firstChild )
this.removeChild( this.firstChild );
}
}, function(name, fn){
jQuery.fn[ name ] = function(){
return this.each( fn, arguments );
};
});
jQuery.each([ "Height", "Width" ], function(i, name){
var type = name.toLowerCase();
jQuery.fn[ type ] = function( size ) {
return this[0] == window ?
jQuery.browser.opera && document.body[ "client"+name ] ||
jQuery.browser.safari && window[ "inner"+name ] ||
document.compatMode == "CSS1Compat" && document.documentElement[ "client"+name ] || document.body[ "client"+name ] :
this[0] == document ?
Math.max( document.body[ "scroll"+name ], document.body[ "offset"+name ] ) :
size == undefined ?
(this.length ? jQuery.css( this[0], type ) : null) :
this.css( type, size.constructor == String ? size : size+"px" );
};
});
var chars = jQuery.browser.safari && parseInt(jQuery.browser.version) < 417 ?
"(?:[\\w*_-]|\\\\.)" :
"(?:[\\w\u0128-\uFFFF*_-]|\\\\.)",
quickChild = new RegExp("^>\\s*("+chars+"+)"),
quickID = new RegExp("^("+chars+"+)(#)("+chars+"+)"),
quickClass = new RegExp("^([#.]?)("+chars+"*)");
jQuery.extend({
expr: {
"": "m[2]=='*'||jQuery.nodeName(a,m[2])",
"#": "a.getAttribute('id')==m[2]",
":": {
lt: "i<m[3]-0",
gt: "i>m[3]-0",
nth: "m[3]-0==i",
eq: "m[3]-0==i",
first: "i==0",
last: "i==r.length-1",
even: "i%2==0",
odd: "i%2",
"first-child": "a.parentNode.getElementsByTagName('*')[0]==a",
"last-child": "jQuery.nth(a.parentNode.lastChild,1,'previousSibling')==a",
"only-child": "!jQuery.nth(a.parentNode.lastChild,2,'previousSibling')",
parent: "a.firstChild",
empty: "!a.firstChild",
contains: "(a.textContent||a.innerText||jQuery(a).text()||'').indexOf(m[3])>=0",
visible: '"hidden"!=a.type&&jQuery.css(a,"display")!="none"&&jQuery.css(a,"visibility")!="hidden"',
hidden: '"hidden"==a.type||jQuery.css(a,"display")=="none"||jQuery.css(a,"visibility")=="hidden"',
enabled: "!a.disabled",
disabled: "a.disabled",
checked: "a.checked",
selected: "a.selected||jQuery.attr(a,'selected')",
text: "'text'==a.type",
radio: "'radio'==a.type",
checkbox: "'checkbox'==a.type",
file: "'file'==a.type",
password: "'password'==a.type",
submit: "'submit'==a.type",
image: "'image'==a.type",
reset: "'reset'==a.type",
button: '"button"==a.type||jQuery.nodeName(a,"button")',
input: "/input|select|textarea|button/i.test(a.nodeName)",
has: "jQuery.find(m[3],a).length",
header: "/h\\d/i.test(a.nodeName)",
animated: "jQuery.grep(jQuery.timers,function(fn){return a==fn.elem;}).length"
}
},
parse: [
/^(\[) *@?([\w-]+) *([!*$^~=]*) *('?"?)(.*?)\4 *\]/,
/^(:)([\w-]+)\("?'?(.*?(\(.*?\))?[^(]*?)"?'?\)/,
new RegExp("^([:.#]*)("+chars+"+)")
],
multiFilter: function( expr, elems, not ) {
var old, cur = [];
while ( expr && expr != old ) {
old = expr;
var f = jQuery.filter( expr, elems, not );
expr = f.t.replace(/^\s*,\s*/, "" );
cur = not ? elems = f.r : jQuery.merge( cur, f.r );
}
return cur;
},
find: function( t, context ) {
if ( typeof t != "string" )
return [ t ];
if ( context && !context.nodeType )
context = null;
context = context || document;
var ret = [context], done = [], last;
while ( t && last != t ) {
var r = [];
last = t;
t = jQuery.trim(t);
var foundToken = false;
var re = quickChild;
var m = re.exec(t);
if ( m ) {
var nodeName = m[1].toUpperCase();
for ( var i = 0; ret[i]; i++)
for ( var c = ret[i].firstChild; c; c = c.nextSibling )
if ( c.nodeType == 1 && (nodeName == "*" || c.nodeName.toUpperCase() == nodeName.toUpperCase()) )
r.push( c );
ret = r;
t = t.replace( re, "" );
if ( t.indexOf(" ") == 0 ) continue;
foundToken = true;
} else {
re = /^([>+~])\s*(\w*)/i;
if ( (m = re.exec(t)) != null ) {
r = [];
var nodeName = m[2], merge = {};
m = m[1];
for ( var j = 0, rl = ret.length; j < rl; j++) {
var n = m == "~" || m == "+" ? ret[j].nextSibling : ret[j].firstChild;
for ( ; n; n = n.nextSibling )
if ( n.nodeType == 1 ) {
var id = jQuery.data(n);
if ( m == "~" && merge[id] ) break;
if (!nodeName || n.nodeName.toUpperCase() == nodeName.toUpperCase() ) {
if ( m == "~" ) merge[id] = true;
r.push( n );
}
if ( m == "+" ) break;
}
}
ret = r;
t = jQuery.trim( t.replace( re, "" ) );
foundToken = true;
}
}
if ( t && !foundToken ) {
if ( !t.indexOf(",") ) {
if ( context == ret[0] ) ret.shift();
done = jQuery.merge( done, ret );
r = ret = [context];
t = " "+t.substr(1,t.length);
} else {
var re2 = quickID;
var m = re2.exec(t);
if ( m ) {
m = [ 0, m[2], m[3], m[1] ];
} else {
re2 = quickClass;
m = re2.exec(t);
}
m[2] = m[2].replace(/\\/g, "");
var elem = ret[ret.length-1];
if ( m[1] == "#" && elem && elem.getElementById && !jQuery.isXMLDoc(elem) ) {
var oid = elem.getElementById(m[2]);
if ( (jQuery.browser.msie||jQuery.browser.opera) && oid && typeof oid.id == "string" && oid.id != m[2] )
oid = jQuery('[@id="'+m[2]+'"]', elem)[0];
ret = r = oid && (!m[3] || jQuery.nodeName(oid, m[3])) ? [oid] : [];
} else {
for ( var i = 0; ret[i]; i++) {
var tag = m[1] == "#" && m[3] ? m[3] : m[1] != "" || m[0] == "" ? "*" : m[2];
if ( tag == "*" && ret[i].nodeName.toLowerCase() == "object" )
tag = "param";
r = jQuery.merge( r, ret[i].getElementsByTagName( tag ));
}
if ( m[1] == "." )
r = jQuery.classFilter( r, m[2] );
if ( m[1] == "#" ) {
var tmp = [];
for ( var i = 0; r[i]; i++)
if ( r[i].getAttribute("id") == m[2] ) {
tmp = [ r[i] ];
break;
}
r = tmp;
}
ret = r;
}
t = t.replace( re2, "" );
}
}
if ( t ) {
var val = jQuery.filter(t,r);
ret = r = val.r;
t = jQuery.trim(val.t);
}
}
if ( t )
ret = [];
if ( ret && context == ret[0] )
ret.shift();
done = jQuery.merge( done, ret );
return done;
},
classFilter: function(r,m,not){
m = " "+m+" ";
var tmp = [];
for ( var i = 0; r[i]; i++) {
var pass = (" "+r[i].className+" ").indexOf( m ) >= 0;
if ( !not && pass || not && !pass )
tmp.push( r[i] );
}
return tmp;
},
filter: function(t,r,not) {
var last;
while ( t && t != last ) {
last = t;
var p = jQuery.parse, m;
for ( var i = 0; p[i]; i++) {
m = p[i].exec( t );
if ( m ) {
t = t.substring( m[0].length );
m[2] = m[2].replace(/\\/g, "");
break;
}
}
if ( !m )
break;
if ( m[1] == ":" && m[2] == "not" )
r = jQuery.filter(m[3], r, true).r;
else if ( m[1] == "." )
r = jQuery.classFilter(r, m[2], not);
else if ( m[1] == "[" ) {
var tmp = [], type = m[3];
for ( var i = 0, rl = r.length; i < rl; i++) {
var a = r[i], z = a[ jQuery.props[m[2]] || m[2] ];
if ( z == null || /href|src|selected/.test(m[2]) )
z = jQuery.attr(a,m[2]) || '';
if ( (type == "" && !!z ||
type == "=" && z == m[5] ||
type == "!=" && z != m[5] ||
type == "^=" && z && !z.indexOf(m[5]) ||
type == "$=" && z.substr(z.length-m[5].length) == m[5] ||
(type == "*=" || type == "~=") && z.indexOf(m[5]) >= 0) ^ not )
tmp.push( a );
}
r = tmp;
} else if ( m[1] == ":" && m[2] == "nth-child" ) {
var merge = {}, tmp = [],
test = /(-?)(\d*)n((?:\+|-)?\d*)/.exec(
m[3] == "even" && "2n" || m[3] == "odd" && "2n+1" ||
!/\D/.test(m[3]) && "0n+"+m[3] || m[3]),
first = (test[1]+(test[2] || 1))-0, last = test[3]-0;
for ( var i = 0, rl = r.length; i < rl; i++) {
var node = r[i], parentNode = node.parentNode, id = jQuery.data(parentNode);
if ( !merge[id] ) {
var c = 1;
for ( var n = parentNode.firstChild; n; n = n.nextSibling )
if ( n.nodeType == 1 )
n.nodeIndex = c++;
merge[id] = true;
}
var add = false;
if ( first == 0 ) {
if ( node.nodeIndex == last )
add = true;
} else if ( (node.nodeIndex-last) % first == 0 && (node.nodeIndex-last) / first >= 0 )
add = true;
if ( add ^ not )
tmp.push( node );
}
r = tmp;
} else {
var f = jQuery.expr[m[1]];
if ( typeof f != "string" )
f = jQuery.expr[m[1]][m[2]];
f = eval("false||function(a,i){return "+f+"}");
r = jQuery.grep( r, f, not );
}
}
return { r: r, t: t };
},
dir: function( elem, dir ){
var matched = [];
var cur = elem[dir];
while ( cur && cur != document ) {
if ( cur.nodeType == 1 )
matched.push( cur );
cur = cur[dir];
}
return matched;
},
nth: function(cur,result,dir,elem){
result = result || 1;
var num = 0;
for ( ; cur; cur = cur[dir] )
if ( cur.nodeType == 1 &&++num == result )
break;
return cur;
},
sibling: function( n, elem ) {
var r = [];
for ( ; n; n = n.nextSibling ) {
if ( n.nodeType == 1 && (!elem || n != elem) )
r.push( n );
}
return r;
}
});
/*
* A number of helper functions used for managing events.
* Many of the ideas behind this code orignated from
* Dean Edwards' addEvent library.
*/
jQuery.event = {
add: function(element, type, handler, data) {
if ( jQuery.browser.msie && element.setInterval != undefined )
element = window;
if ( !handler.guid )
handler.guid = this.guid++;
if( data != undefined ) {
var fn = handler;
handler = function() {
return fn.apply(this, arguments);
};
handler.data = data;
handler.guid = fn.guid;
}
var parts = type.split(".");
type = parts[0];
handler.type = parts[1];
var events = jQuery.data(element, "events") || jQuery.data(element, "events", {});
var handle = jQuery.data(element, "handle") || jQuery.data(element, "handle", function(){
var val;
if ( typeof jQuery == "undefined" || jQuery.event.triggered )
return val;
val = jQuery.event.handle.apply(element, arguments);
return val;
});
var handlers = events[type];
if (!handlers) {
handlers = events[type] = {};
if (element.addEventListener)
element.addEventListener(type, handle, false);
else
element.attachEvent("on"+type, handle);
}
handlers[handler.guid] = handler;
this.global[type] = true;
},
guid: 1,
global: {},
remove: function(element, type, handler) {
var events = jQuery.data(element, "events"), ret, index;
if ( typeof type == "string" ) {
var parts = type.split(".");
type = parts[0];
}
if ( events ) {
if ( type && type.type ) {
handler = type.handler;
type = type.type;
}
if ( !type ) {
for ( type in events )
this.remove( element, type );
} else if ( events[type] ) {
if ( handler )
delete events[type][handler.guid];
else
for ( handler in events[type] )
if ( !parts[1] || events[type][handler].type == parts[1] )
delete events[type][handler];
for ( ret in events[type] ) break;
if ( !ret ) {
if (element.removeEventListener)
element.removeEventListener(type, jQuery.data(element, "handle"), false);
else
element.detachEvent("on"+type, jQuery.data(element, "handle"));
ret = null;
delete events[type];
}
}
for ( ret in events ) break;
if ( !ret ) {
jQuery.removeData( element, "events" );
jQuery.removeData( element, "handle" );
}
}
},
trigger: function(type, data, element, donative, extra) {
data = jQuery.makeArray(data || []);
if ( !element ) {
if ( this.global[type] )
jQuery("*").add([window, document]).trigger(type, data);
} else {
var val, ret, fn = jQuery.isFunction( element[ type ] || null ),
event = !data[0] || !data[0].preventDefault;
if ( event )
data.unshift( this.fix({ type: type, target: element }) );
data[0].type = type;
if ( jQuery.isFunction( jQuery.data(element, "handle") ) )
val = jQuery.data(element, "handle").apply( element, data );
if ( !fn && element["on"+type] && element["on"+type].apply( element, data ) === false )
val = false;
if ( event )
data.shift();
if ( extra && extra.apply( element, data ) === false )
val = false;
if ( fn && donative !== false && val !== false && !(jQuery.nodeName(element, 'a') && type == "click") ) {
this.triggered = true;
element[ type ]();
}
this.triggered = false;
}
return val;
},
handle: function(event) {
var val;
event = jQuery.event.fix( event || window.event || {} );
var parts = event.type.split(".");
event.type = parts[0];
var handlers = jQuery.data(this, "events") && jQuery.data(this, "events")[event.type], args = Array.prototype.slice.call( arguments, 1 );
args.unshift( event );
for ( var j in handlers ) {
var handler = handlers[j];
args[0].handler = handler;
args[0].data = handler.data;
if ( !parts[1] || handler.type == parts[1] ) {
var ret = handler.apply( this, args );
if ( val !== false )
val = ret;
if ( ret === false ) {
event.preventDefault();
event.stopPropagation();
}
}
}
if (jQuery.browser.msie)
event.target = event.preventDefault = event.stopPropagation =
event.handler = event.data = null;
return val;
},
fix: function(event) {
var originalEvent = event;
event = jQuery.extend({}, originalEvent);
event.preventDefault = function() {
if (originalEvent.preventDefault)
originalEvent.preventDefault();
originalEvent.returnValue = false;
};
event.stopPropagation = function() {
if (originalEvent.stopPropagation)
originalEvent.stopPropagation();
originalEvent.cancelBubble = true;
};
if ( !event.target )
event.target = event.srcElement || document; // Fixes #1925 where srcElement might not be defined either
if ( event.target.nodeType == 3 )
event.target = originalEvent.target.parentNode;
if ( !event.relatedTarget && event.fromElement )
event.relatedTarget = event.fromElement == event.target ? event.toElement : event.fromElement;
if ( event.pageX == null && event.clientX != null ) {
var doc = document.documentElement, body = document.body;
event.pageX = event.clientX+(doc && doc.scrollLeft || body && body.scrollLeft || 0)-(doc.clientLeft || 0);
event.pageY = event.clientY+(doc && doc.scrollTop  || body && body.scrollTop  || 0)-(doc.clientLeft || 0);
}
if ( !event.which && (event.charCode || event.keyCode) )
event.which = event.charCode || event.keyCode;
if ( !event.metaKey && event.ctrlKey )
event.metaKey = event.ctrlKey;
if ( !event.which && event.button )
event.which = (event.button & 1 ? 1 : ( event.button & 2 ? 3 : ( event.button & 4 ? 2 : 0 ) ));
return event;
}
};
jQuery.fn.extend({
bind: function( type, data, fn ) {
return type == "unload" ? this.one(type, data, fn) : this.each(function(){
jQuery.event.add( this, type, fn || data, fn && data );
});
},
one: function( type, data, fn ) {
return this.each(function(){
jQuery.event.add( this, type, function(event) {
jQuery(this).unbind(event);
return (fn || data).apply( this, arguments);
}, fn && data);
});
},
unbind: function( type, fn ) {
return this.each(function(){
jQuery.event.remove( this, type, fn );
});
},
trigger: function( type, data, fn ) {
return this.each(function(){
jQuery.event.trigger( type, data, this, true, fn );
});
},
triggerHandler: function( type, data, fn ) {
if ( this[0] )
return jQuery.event.trigger( type, data, this[0], false, fn );
},
toggle: function() {
var args = arguments;
return this.click(function(event) {
this.lastToggle = 0 == this.lastToggle ? 1 : 0;
event.preventDefault();
return args[this.lastToggle].apply( this, [event] ) || false;
});
},
hover: function(fnOver, fnOut) {
function handleHover(event) {
var parent = event.relatedTarget;
while ( parent && parent != this ) try { parent = parent.parentNode; } catch(error) { parent = this; };
if ( parent == this ) return false;
return (event.type == "mouseover" ? fnOver : fnOut).apply(this, [event]);
}
return this.mouseover(handleHover).mouseout(handleHover);
},
ready: function(fn) {
bindReady();
if ( jQuery.isReady )
fn.apply( document, [jQuery] );
else
jQuery.readyList.push( function() { return fn.apply(this, [jQuery]); } );
return this;
}
});
jQuery.extend({
/*
* All the code that makes DOM Ready work nicely.
*/
isReady: false,
readyList: [],
ready: function() {
if ( !jQuery.isReady ) {
jQuery.isReady = true;
if ( jQuery.readyList ) {
jQuery.each( jQuery.readyList, function(){
this.apply( document );
});
jQuery.readyList = null;
}
if ( document.removeEventListener )
document.removeEventListener( "DOMContentLoaded", jQuery.ready, false );
}
}
});
jQuery.each( ("blur,focus,load,resize,scroll,unload,click,dblclick,"+
"mousedown,mouseup,mousemove,mouseover,mouseout,change,select,"+
"submit,keydown,keypress,keyup,error").split(","), function(i, name){
jQuery.fn[name] = function(fn){
return fn ? this.bind(name, fn) : this.trigger(name);
};
});
var readyBound = false;
function bindReady(){
if ( readyBound ) return;
readyBound = true;
if ( document.addEventListener )
document.addEventListener( "DOMContentLoaded", jQuery.ready, false );
if  (jQuery.browser.msie || jQuery.browser.safari ) (function(){
try {
if ( jQuery.browser.msie || document.readyState != "loaded" && document.readyState != "complete" )
document.documentElement.doScroll("left");
} catch( error ) {
return setTimeout( arguments.callee, 0 );
}
jQuery.ready();
})();
jQuery.event.add( window, "load", jQuery.ready );
}
jQuery(window).bind("unload", function() {
jQuery("*").add(document).unbind();
});
jQuery.fn.extend({
load: function( url, params, callback ) {
if ( jQuery.isFunction( url ) )
return this.bind("load", url);
var off = url.indexOf(" ");
if ( off >= 0 ) {
var selector = url.slice(off, url.length);
url = url.slice(0, off);
}
callback = callback || function(){};
var type = "GET";
if ( params )
if ( jQuery.isFunction( params ) ) {
callback = params;
params = null;
} else {
params = jQuery.param( params );
type = "POST";
}
var self = this;
jQuery.ajax({
url: url,
type: type,
data: params,
complete: function(res, status){
if ( status == "success" || status == "notmodified" )
self.html( selector ?
jQuery("<div/>")
.append(res.responseText.replace(/<script(.|\s)*?\/script>/g, ""))
.find(selector) :
res.responseText );
setTimeout(function(){
self.each( callback, [res.responseText, status, res] );
}, 13);
}
});
return this;
},
serialize: function() {
return jQuery.param(this.serializeArray());
},
serializeArray: function() {
return this.map(function(){
return jQuery.nodeName(this, "form") ?
jQuery.makeArray(this.elements) : this;
})
.filter(function(){
return this.name && !this.disabled &&
(this.checked || /select|textarea/i.test(this.nodeName) ||
/text|hidden|password/i.test(this.type));
})
.map(function(i, elem){
var val = jQuery(this).val();
return val == null ? null :
val.constructor == Array ?
jQuery.map( val, function(val, i){
return {name: elem.name, value: val};
}) :
{name: elem.name, value: val};
}).get();
}
});
jQuery.each( "ajaxStart,ajaxStop,ajaxComplete,ajaxError,ajaxSuccess,ajaxSend".split(","), function(i,o){
jQuery.fn[o] = function(f){
return this.bind(o, f);
};
});
var jsc = (new Date).getTime();
jQuery.extend({
get: function( url, data, callback, type ) {
if ( jQuery.isFunction( data ) ) {
callback = data;
data = null;
}
return jQuery.ajax({
type: "GET",
url: url,
data: data,
success: callback,
dataType: type
});
},
getScript: function( url, callback ) {
return jQuery.get(url, null, callback, "script");
},
getJSON: function( url, data, callback ) {
return jQuery.get(url, data, callback, "json");
},
post: function( url, data, callback, type ) {
if ( jQuery.isFunction( data ) ) {
callback = data;
data = {};
}
return jQuery.ajax({
type: "POST",
url: url,
data: data,
success: callback,
dataType: type
});
},
ajaxSetup: function( settings ) {
jQuery.extend( jQuery.ajaxSettings, settings );
},
ajaxSettings: {
global: true,
type: "GET",
timeout: 0,
contentType: "application/x-www-form-urlencoded",
processData: true,
async: true,
data: null
},
lastModified: {},
ajax: function( s ) {
var jsonp, jsre = /=(\?|%3F)/g, status, data;
s = jQuery.extend(true, s, jQuery.extend(true, {}, jQuery.ajaxSettings, s));
if ( s.data && s.processData && typeof s.data != "string" )
s.data = jQuery.param(s.data);
if ( s.dataType == "jsonp" ) {
if ( s.type.toLowerCase() == "get" ) {
if ( !s.url.match(jsre) )
s.url+= (s.url.match(/\?/) ? "&" : "?")+(s.jsonp || "callback")+"=?";
} else if ( !s.data || !s.data.match(jsre) )
s.data = (s.data ? s.data+"&" : "")+(s.jsonp || "callback")+"=?";
s.dataType = "json";
}
if ( s.dataType == "json" && (s.data && s.data.match(jsre) || s.url.match(jsre)) ) {
jsonp = "jsonp"+jsc++;
if ( s.data )
s.data = (s.data+"").replace(jsre, "="+jsonp);
s.url = s.url.replace(jsre, "="+jsonp);
s.dataType = "script";
window[ jsonp ] = function(tmp){
data = tmp;
success();
complete();
window[ jsonp ] = undefined;
try{ delete window[ jsonp ]; } catch(e){}
};
}
if ( s.dataType == "script" && s.cache == null )
s.cache = false;
if ( s.cache === false && s.type.toLowerCase() == "get" )
s.url+= (s.url.match(/\?/) ? "&" : "?")+"_="+(new Date()).getTime();
if ( s.data && s.type.toLowerCase() == "get" ) {
s.url+= (s.url.match(/\?/) ? "&" : "?")+s.data;
s.data = null;
}
if ( s.global && ! jQuery.active++)
jQuery.event.trigger( "ajaxStart" );
if ( !s.url.indexOf("http") && s.dataType == "script" ) {
var head = document.getElementsByTagName("head")[0];
var script = document.createElement("script");
script.src = s.url;
if ( !jsonp ) {
var done = false;
script.onload = script.onreadystatechange = function(){
if ( !done && (!this.readyState ||
this.readyState == "loaded" || this.readyState == "complete") ) {
done = true;
success();
complete();
head.removeChild( script );
}
};
}
head.appendChild(script);
return;
}
var requestDone = false;
var xml = window.ActiveXObject ? new ActiveXObject("Microsoft.XMLHTTP") : new XMLHttpRequest();
xml.open(s.type, s.url, s.async);
if ( s.data )
xml.setRequestHeader("Content-Type", s.contentType);
if ( s.ifModified )
xml.setRequestHeader("If-Modified-Since",
jQuery.lastModified[s.url] || "Thu, 01 Jan 1970 00:00:00 GMT" );
xml.setRequestHeader("X-Requested-With", "XMLHttpRequest");
if ( s.beforeSend )
s.beforeSend(xml);
if ( s.global )
jQuery.event.trigger("ajaxSend", [xml, s]);
var onreadystatechange = function(isTimeout){
if ( !requestDone && xml && (xml.readyState == 4 || isTimeout == "timeout") ) {
requestDone = true;
if (ival) {
clearInterval(ival);
ival = null;
}
status = isTimeout == "timeout" && "timeout" ||
!jQuery.httpSuccess( xml ) && "error" ||
s.ifModified && jQuery.httpNotModified( xml, s.url ) && "notmodified" ||
"success";
if ( status == "success" ) {
try {
data = jQuery.httpData( xml, s.dataType );
} catch(e) {
status = "parsererror";
}
}
if ( status == "success" ) {
var modRes;
try {
modRes = xml.getResponseHeader("Last-Modified");
} catch(e) {} // swallow exception thrown by FF if header is not available
if ( s.ifModified && modRes )
jQuery.lastModified[s.url] = modRes;
if ( !jsonp )
success();
} else
jQuery.handleError(s, xml, status);
complete();
if ( s.async )
xml = null;
}
};
if ( s.async ) {
var ival = setInterval(onreadystatechange, 13);
if ( s.timeout > 0 )
setTimeout(function(){
if ( xml ) {
xml.abort();
if( !requestDone )
onreadystatechange( "timeout" );
}
}, s.timeout);
}
try {
xml.send(s.data);
} catch(e) {
jQuery.handleError(s, xml, null, e);
}
if ( !s.async )
onreadystatechange();
return xml;
function success(){
if ( s.success )
s.success( data, status );
if ( s.global )
jQuery.event.trigger( "ajaxSuccess", [xml, s] );
}
function complete(){
if ( s.complete )
s.complete(xml, status);
if ( s.global )
jQuery.event.trigger( "ajaxComplete", [xml, s] );
if ( s.global && !--jQuery.active )
jQuery.event.trigger( "ajaxStop" );
}
},
handleError: function( s, xml, status, e ) {
if ( s.error ) s.error( xml, status, e );
if ( s.global )
jQuery.event.trigger( "ajaxError", [xml, s, e] );
},
active: 0,
httpSuccess: function( r ) {
try {
return !r.status && location.protocol == "file:" ||
( r.status >= 200 && r.status < 300 ) || r.status == 304 ||
jQuery.browser.safari && r.status == undefined;
} catch(e){}
return false;
},
httpNotModified: function( xml, url ) {
try {
var xmlRes = xml.getResponseHeader("Last-Modified");
return xml.status == 304 || xmlRes == jQuery.lastModified[url] ||
jQuery.browser.safari && xml.status == undefined;
} catch(e){}
return false;
},
httpData: function( r, type ) {
var ct = r.getResponseHeader("content-type");
var xml = type == "xml" || !type && ct && ct.indexOf("xml") >= 0;
var data = xml ? r.responseXML : r.responseText;
if ( xml && data.documentElement.tagName == "parsererror" )
throw "parsererror";
if ( type == "script" )
jQuery.globalEval( data );
if ( type == "json" )
data = eval("("+data+")");
return data;
},
param: function( a ) {
var s = [];
if ( a.constructor == Array || a.jquery )
jQuery.each( a, function(){
s.push( encodeURIComponent(this.name)+"="+encodeURIComponent( this.value ) );
});
else
for ( var j in a )
if ( a[j] && a[j].constructor == Array )
jQuery.each( a[j], function(){
s.push( encodeURIComponent(j)+"="+encodeURIComponent( this ) );
});
else
s.push( encodeURIComponent(j)+"="+encodeURIComponent( a[j] ) );
return s.join("&").replace(/%20/g, "+");
}
});
jQuery.fn.extend({
show: function(speed,callback){
return speed ?
this.animate({
height: "show", width: "show", opacity: "show"
}, speed, callback) :
this.filter(":hidden").each(function(){
this.style.display = this.oldblock ? this.oldblock : "";
if ( jQuery.css(this,"display") == "none" )
this.style.display = "block";
}).end();
},
hide: function(speed,callback){
return speed ?
this.animate({
height: "hide", width: "hide", opacity: "hide"
}, speed, callback) :
this.filter(":visible").each(function(){
this.oldblock = this.oldblock || jQuery.css(this,"display");
if ( this.oldblock == "none" )
this.oldblock = "block";
this.style.display = "none";
}).end();
},
_toggle: jQuery.fn.toggle,
toggle: function( fn, fn2 ){
return jQuery.isFunction(fn) && jQuery.isFunction(fn2) ?
this._toggle( fn, fn2 ) :
fn ?
this.animate({
height: "toggle", width: "toggle", opacity: "toggle"
}, fn, fn2) :
this.each(function(){
jQuery(this)[ jQuery(this).is(":hidden") ? "show" : "hide" ]();
});
},
slideDown: function(speed,callback){
return this.animate({height: "show"}, speed, callback);
},
slideUp: function(speed,callback){
return this.animate({height: "hide"}, speed, callback);
},
slideToggle: function(speed, callback){
return this.animate({height: "toggle"}, speed, callback);
},
fadeIn: function(speed, callback){
return this.animate({opacity: "show"}, speed, callback);
},
fadeOut: function(speed, callback){
return this.animate({opacity: "hide"}, speed, callback);
},
fadeTo: function(speed,to,callback){
return this.animate({opacity: to}, speed, callback);
},
animate: function( prop, speed, easing, callback ) {
var optall = jQuery.speed(speed, easing, callback);
return this[ optall.queue === false ? "each" : "queue" ](function(){
var opt = jQuery.extend({}, optall);
var hidden = jQuery(this).is(":hidden"), self = this;
for ( var p in prop ) {
if ( prop[p] == "hide" && hidden || prop[p] == "show" && !hidden )
return jQuery.isFunction(opt.complete) && opt.complete.apply(this);
if ( p == "height" || p == "width" ) {
opt.display = jQuery.css(this, "display");
opt.overflow = this.style.overflow;
}
}
if ( opt.overflow != null )
this.style.overflow = "hidden";
opt.curAnim = jQuery.extend({}, prop);
jQuery.each( prop, function(name, val){
var e = new jQuery.fx( self, opt, name );
if ( /toggle|show|hide/.test(val) )
e[ val == "toggle" ? hidden ? "show" : "hide" : val ]( prop );
else {
var parts = val.toString().match(/^([+-]=)?([\d+-.]+)(.*)$/),
start = e.cur(true) || 0;
if ( parts ) {
var end = parseFloat(parts[2]),
unit = parts[3] || "px";
if ( unit != "px" ) {
self.style[ name ] = (end || 1)+unit;
start = ((end || 1) / e.cur(true)) * start;
self.style[ name ] = start+unit;
}
if ( parts[1] )
end = ((parts[1] == "-=" ?-1 : 1) * end)+start;
e.custom( start, end, unit );
} else
e.custom( start, val, "" );
}
});
return true;
});
},
queue: function(type, fn){
if ( jQuery.isFunction(type) || ( type && type.constructor == Array )) {
fn = type;
type = "fx";
}
if ( !type || (typeof type == "string" && !fn) )
return queue( this[0], type );
return this.each(function(){
if ( fn.constructor == Array )
queue(this, type, fn);
else {
queue(this, type).push( fn );
if ( queue(this, type).length == 1 )
fn.apply(this);
}
});
},
stop: function(){
var timers = jQuery.timers;
return this.each(function(){
for ( var i = 0; i < timers.length; i++)
if ( timers[i].elem == this )
timers.splice(i--, 1);
}).dequeue();
}
});
var queue = function( elem, type, array ) {
if ( !elem )
return;
type = type || "fx";
var q = jQuery.data( elem, type+"queue" );
if ( !q || array )
q = jQuery.data( elem, type+"queue",
array ? jQuery.makeArray(array) : [] );
return q;
};
jQuery.fn.dequeue = function(type){
type = type || "fx";
return this.each(function(){
var q = queue(this, type);
q.shift();
if ( q.length )
q[0].apply( this );
});
};
jQuery.extend({
speed: function(speed, easing, fn) {
var opt = speed && speed.constructor == Object ? speed : {
complete: fn || !fn && easing ||
jQuery.isFunction( speed ) && speed,
duration: speed,
easing: fn && easing || easing && easing.constructor != Function && easing
};
opt.duration = (opt.duration && opt.duration.constructor == Number ?
opt.duration :
{ slow: 600, fast: 200 }[opt.duration]) || 400;
opt.old = opt.complete;
opt.complete = function(){
if ( opt.queue !== false )
jQuery(this).dequeue();
if ( jQuery.isFunction( opt.old ) )
opt.old.apply( this );
};
return opt;
},
easing: {
linear: function( p, n, firstNum, diff ) {
return firstNum+diff * p;
},
swing: function( p, n, firstNum, diff ) {
return ((-Math.cos(p*Math.PI)/2)+0.5) * diff+firstNum;
}
},
timers: [],
timerId: null,
fx: function( elem, options, prop ){
this.options = options;
this.elem = elem;
this.prop = prop;
if ( !options.orig )
options.orig = {};
}
});
jQuery.fx.prototype = {
update: function(){
if ( this.options.step )
this.options.step.apply( this.elem, [ this.now, this ] );
(jQuery.fx.step[this.prop] || jQuery.fx.step._default)( this );
if ( this.prop == "height" || this.prop == "width" )
this.elem.style.display = "block";
},
cur: function(force){
if ( this.elem[this.prop] != null && this.elem.style[this.prop] == null )
return this.elem[ this.prop ];
var r = parseFloat(jQuery.curCSS(this.elem, this.prop, force));
return r && r >-10000 ? r : parseFloat(jQuery.css(this.elem, this.prop)) || 0;
},
custom: function(from, to, unit){
this.startTime = (new Date()).getTime();
this.start = from;
this.end = to;
this.unit = unit || this.unit || "px";
this.now = this.start;
this.pos = this.state = 0;
this.update();
var self = this;
function t(){
return self.step();
}
t.elem = this.elem;
jQuery.timers.push(t);
if ( jQuery.timerId == null ) {
jQuery.timerId = setInterval(function(){
var timers = jQuery.timers;
for ( var i = 0; i < timers.length; i++)
if ( !timers[i]() )
timers.splice(i--, 1);
if ( !timers.length ) {
clearInterval( jQuery.timerId );
jQuery.timerId = null;
}
}, 13);
}
},
show: function(){
this.options.orig[this.prop] = jQuery.attr( this.elem.style, this.prop );
this.options.show = true;
this.custom(0, this.cur());
if ( this.prop == "width" || this.prop == "height" )
this.elem.style[this.prop] = "1px";
jQuery(this.elem).show();
},
hide: function(){
this.options.orig[this.prop] = jQuery.attr( this.elem.style, this.prop );
this.options.hide = true;
this.custom(this.cur(), 0);
},
step: function(){
var t = (new Date()).getTime();
if ( t > this.options.duration+this.startTime ) {
this.now = this.end;
this.pos = this.state = 1;
this.update();
this.options.curAnim[ this.prop ] = true;
var done = true;
for ( var i in this.options.curAnim )
if ( this.options.curAnim[i] !== true )
done = false;
if ( done ) {
if ( this.options.display != null ) {
this.elem.style.overflow = this.options.overflow;
this.elem.style.display = this.options.display;
if ( jQuery.css(this.elem, "display") == "none" )
this.elem.style.display = "block";
}
if ( this.options.hide )
this.elem.style.display = "none";
if ( this.options.hide || this.options.show )
for ( var p in this.options.curAnim )
jQuery.attr(this.elem.style, p, this.options.orig[p]);
}
if ( done && jQuery.isFunction( this.options.complete ) )
this.options.complete.apply( this.elem );
return false;
} else {
var n = t-this.startTime;
this.state = n / this.options.duration;
this.pos = jQuery.easing[this.options.easing || (jQuery.easing.swing ? "swing" : "linear")](this.state, n, 0, 1, this.options.duration);
this.now = this.start+((this.end-this.start) * this.pos);
this.update();
}
return true;
}
};
jQuery.fx.step = {
scrollLeft: function(fx){
fx.elem.scrollLeft = fx.now;
},
scrollTop: function(fx){
fx.elem.scrollTop = fx.now;
},
opacity: function(fx){
jQuery.attr(fx.elem.style, "opacity", fx.now);
},
_default: function(fx){
fx.elem.style[ fx.prop ] = fx.now+fx.unit;
}
};
jQuery.fn.offset = function() {
var left = 0, top = 0, elem = this[0], results;
if ( elem ) with ( jQuery.browser ) {
var	parent       = elem.parentNode,
offsetChild  = elem,
offsetParent = elem.offsetParent,
doc          = elem.ownerDocument,
safari2      = safari && parseInt(version) < 522,
fixed        = jQuery.css(elem, "position") == "fixed";
if ( elem.getBoundingClientRect ) {
var box = elem.getBoundingClientRect();
add(
box.left+Math.max(doc.documentElement.scrollLeft, doc.body.scrollLeft),
box.top +Math.max(doc.documentElement.scrollTop,  doc.body.scrollTop)
);
if ( msie ) {
var border = jQuery("html").css("borderWidth");
border = (border == "medium" || jQuery.boxModel && parseInt(version) >= 7) && 2 || border;
add(-border,-border );
}
} else {
add( elem.offsetLeft, elem.offsetTop );
while ( offsetParent ) {
add( offsetParent.offsetLeft, offsetParent.offsetTop );
if ( mozilla && !/^t(able|d|h)$/i.test(offsetParent.tagName) || safari && !safari2 )
border( offsetParent );
if ( !fixed && jQuery.css(offsetParent, "position") == "fixed" )
fixed = true;
offsetChild  = /^body$/i.test(offsetParent.tagName) ? offsetChild : offsetParent;
offsetParent = offsetParent.offsetParent;
}
while ( parent.tagName && !/^body|html$/i.test(parent.tagName) ) {
if ( !/^inline|table-row.*$/i.test(jQuery.css(parent, "display")) )
add(-parent.scrollLeft,-parent.scrollTop );
if ( mozilla && jQuery.css(parent, "overflow") != "visible" )
border( parent );
parent = parent.parentNode;
}
if ( (safari2 && (fixed || jQuery.css(offsetChild, "position") == "absolute")) ||
(mozilla && jQuery.css(offsetChild, "position") != "absoltue") )
add(-doc.body.offsetLeft,-doc.body.offsetTop );
if ( fixed )
add(
Math.max(doc.documentElement.scrollLeft, doc.body.scrollLeft),
Math.max(doc.documentElement.scrollTop,  doc.body.scrollTop)
);
}
results = { top: top, left: left };
}
return results;
function border(elem) {
add( jQuery.css(elem, "borderLeftWidth"), jQuery.css(elem, "borderTopWidth") );
}
function add(l, t) {
left+= parseInt(l) || 0;
top+= parseInt(t) || 0;
}
};
})();
function Porfolio_UserLog()
{
$.ajax({
type: "GET",
url: "/GUI/Front_End/Portfolio/Controls/UpdatePortfolio.ashx",
data:{'a':'10','ID':0} ,
beforeSend: function(xhr) {
xhr.setRequestHeader("Content-type",
"application/json; charset=utf-8");
},
contentType: "application/json; charset=utf-8",
dataType: "json",
success: function(msg) {
},
failure: function(msg) {
}
});
}/*
* jqDnR-Minimalistic Drag'n'Resize for jQuery.
*
* Copyright (c) 2007 Brice Burgess <bhb@iceburg.net>, http://www.iceburg.net
* Licensed under the MIT License:
* http://www.opensource.org/licenses/mit-license.php
*
* $Version: 2007.08.19+r2
*/
(function($){
$.fn.jqDrag=function(h){return i(this,h,'d');};
$.fn.jqResize=function(h){return i(this,h,'r');};
$.jqDnR={dnr:{},e:0,
drag:function(v){
if(M.k == 'd')E.css({left:M.X+v.pageX-M.pX,top:M.Y+v.pageY-M.pY});
else E.css({width:Math.max(v.pageX-M.pX+M.W,0),height:Math.max(v.pageY-M.pY+M.H,0)});
return false;},
stop:function(){E.css('opacity',M.o);$().unbind('mousemove',J.drag).unbind('mouseup',J.stop);}
};
var J=$.jqDnR,M=J.dnr,E=J.e,
i=function(e,h,k){return e.each(function(){h=(h)?$(h,e):e;
h.bind('mousedown',{e:e,k:k},function(v){var d=v.data,p={};E=d.e;
if(E.css('position') != 'relative'){try{E.position(p);}catch(e){}}
M={X:p.left||f('left')||0,Y:p.top||f('top')||0,W:f('width')||E[0].scrollWidth||0,H:f('height')||E[0].scrollHeight||0,pX:v.pageX,pY:v.pageY,k:d.k,o:E.css('opacity')};
E.css({opacity:0.8});$().mousemove($.jqDnR.drag).mouseup($.jqDnR.stop);
return false;
});
});},
f=function(k){return parseInt(E.css(k))||false;};
})(jQuery);/* Copyright (c) 2006 Brandon Aaron (http://brandonaaron.net)
* Dual licensed under the MIT (http://www.opensource.org/licenses/mit-license.php)
* and GPL (http://www.opensource.org/licenses/gpl-license.php) licenses.
*
* $LastChangedDate: 2007-07-22 01:45:56+0200 (Son, 22 Jul 2007) $
* $Rev: 2447 $
*
* Version 2.1.1
*/
(function($){$.fn.bgIframe=$.fn.bgiframe=function(s){if($.browser.msie&&/6.0/.test(navigator.userAgent)){s=$.extend({top:'auto',left:'auto',width:'auto',height:'auto',opacity:true,src:'javascript:false;'},s||{});var prop=function(n){return n&&n.constructor==Number?n+'px':n;},html='<iframe class="bgiframe"frameborder="0"tabindex="-1"src="'+s.src+'"'+'style="display:block;position:absolute;z-index:-1;'+(s.opacity!==false?'filter:Alpha(Opacity=\'0\');':'')+'top:'+(s.top=='auto'?'expression(((parseInt(this.parentNode.currentStyle.borderTopWidth)||0)*-1)+\'px\')':prop(s.top))+';'+'left:'+(s.left=='auto'?'expression(((parseInt(this.parentNode.currentStyle.borderLeftWidth)||0)*-1)+\'px\')':prop(s.left))+';'+'width:'+(s.width=='auto'?'expression(this.parentNode.offsetWidth+\'px\')':prop(s.width))+';'+'height:'+(s.height=='auto'?'expression(this.parentNode.offsetHeight+\'px\')':prop(s.height))+';'+'"/>';return this.each(function(){if($('> iframe.bgiframe',this).length==0)this.insertBefore(document.createElement(html),this.firstChild);});}return this;};})
(jQuery);/* Copyright (c) 2007 Paul Bakaus (paul.bakaus@googlemail.com) and Brandon Aaron (brandon.aaron@gmail.com || http://brandonaaron.net) * Dual licensed under the MIT (http://www.opensource.org/licenses/mit-license.php)
 * and GPL (http://www.opensource.org/licenses/gpl-license.php) licenses.
 *
 * $LastChangedDate: 2007-09-11 05:38:31+0300 (Вт, 11 сен 2007) $
 * $Rev: 3238 $
 *
 * Version: @VERSION
 *
 * Requires: jQuery 1.2+*/
(function($){
	
$.dimensions = {
	version: '@VERSION'};
// Create innerHeight, innerWidth, outerHeight and outerWidth methods
$.each( [ 'Height', 'Width' ], function(i, name){
	
	// innerHeight and innerWidth
	$.fn[ 'inner'+name ] = function() {
		if (!this[0]) return;
		
		var torl = name == 'Height' ? 'Top'    : 'Left',  // top or left
		    borr = name == 'Height' ? 'Bottom' : 'Right'; // bottom or right
		
		return this[ name.toLowerCase() ]()+num(this, 'padding'+torl)+num(this, 'padding'+borr);
	};
	
	// outerHeight and outerWidth
	$.fn[ 'outer'+name ] = function(options) {
		if (!this[0]) return;
		
		var torl = name == 'Height' ? 'Top'    : 'Left',  // top or left
		    borr = name == 'Height' ? 'Bottom' : 'Right'; // bottom or right
		
		options = $.extend({ margin: false }, options || {});
		
		return this[ name.toLowerCase() ]()
				+num(this, 'border'+torl+'Width')+num(this, 'border'+borr+'Width')
				+num(this, 'padding'+torl)+num(this, 'padding'+borr)
				+(options.margin ? (num(this, 'margin'+torl)+num(this, 'margin'+borr)) : 0);
	};});
// Create scrollLeft and scrollTop methods
$.each( ['Left', 'Top'], function(i, name) {
	$.fn[ 'scroll'+name ] = function(val) {
		if (!this[0]) return;
		
		return val != undefined ?
		
			// Set the scroll offset
			this.each(function() {
				this == window || this == document ?
					window.scrollTo( 
						name == 'Left' ? val : $(window)[ 'scrollLeft' ](),
						name == 'Top'  ? val : $(window)[ 'scrollTop'  ]()
					) :
					this[ 'scroll'+name ] = val;
			}) :
			
			// Return the scroll offset
			this[0] == window || this[0] == document ?
				self[ (name == 'Left' ? 'pageXOffset' : 'pageYOffset') ] ||
					$.boxModel && document.documentElement[ 'scroll'+name ] ||
					document.body[ 'scroll'+name ] :
				this[0][ 'scroll'+name ];
	};});

$.fn.extend({
	position: function() {
		var left = 0, top = 0, elem = this[0], offset, parentOffset, offsetParent, results;
		
		if (elem) {
			// Get *real* offsetParent
			offsetParent = this.offsetParent();
			
			// Get correct offsets
			offset       = this.offset();
			parentOffset = offsetParent.offset();
			
			// Subtract element margins
			offset.top -= num(elem, 'marginTop');
			offset.left-= num(elem, 'marginLeft');
			
			// Add offsetParent borders
			parentOffset.top += num(offsetParent, 'borderTopWidth');
			parentOffset.left+= num(offsetParent, 'borderLeftWidth');
			
			// Subtract the two offsets
			results = {
				top:  offset.top -parentOffset.top,
				left: offset.left-parentOffset.left
			};
		}
		
		return results;
	},
	
	offsetParent: function() {
		var offsetParent = this[0].offsetParent;
		while ( offsetParent && (!/^body|html$/i.test(offsetParent.tagName) && $.css(offsetParent, 'position') == 'static') )
			offsetParent = offsetParent.offsetParent;
		return $(offsetParent);
	}});

var num = function(el, prop) {
	return parseInt($.css(el.jquery?el[0]:el,prop))||0;};
})(jQuery);(function($) {
$.fn.extend({
autocomplete: function(urlOrData, options) {
var isUrl = typeof urlOrData == "string";
options = $.extend({}, $.Autocompleter.defaults, {
url: isUrl ? urlOrData : null,
data: isUrl ? null : urlOrData,
delay: isUrl ? $.Autocompleter.defaults.delay : 10,
max: options && !options.scroll ? 5 : 150
}, options);
options.highlight = options.highlight || function(value) { return value; };
options.moreItems = options.moreItems || "";
return this.each(function() {
new $.Autocompleter(this, options);
});
},
result: function(handler) {
return this.bind("result", handler);
},
search: function(handler) {
return this.trigger("search", [handler]);
},
flushCache: function() {
return this.trigger("flushCache");
},
setOptions: function(options){
return this.trigger("setOptions", [options]);
},
unautocomplete: function() {
return this.trigger("unautocomplete");
}
});
$.Autocompleter = function(input, options) {
var KEY = {
UP: 38,
DOWN: 40,
DEL: 46,
TAB: 9,
RETURN: 13,
ESC: 27,
COMMA: 188,
PAGEUP: 33,
PAGEDOWN: 34
};
var $input = $(input).attr("autocomplete", "off").addClass(options.inputClass);
var timeout;
var previousValue = "";
var cache = $.Autocompleter.Cache(options);
var hasFocus = 0;
var lastKeyPressCode;
var config = {
mouseDownOnSelect: false
};
var select;
if (options.isAddSymbolToFavorite)
{
select = $.Autocompleter.Select(options, input, AddStockSymbolToFavorite, config);
}
else
if(options.isTargetUrl)
{
select = $.Autocompleter.Select(options, input, DoTarget, config);
}
else
if(options.TinChungKhoan)
{
select = $.Autocompleter.Select(options, input, LoadTinChungKhoan, config);
}
else
{
select = $.Autocompleter.Select(options, input, selectCurrent, config);
}
$input.keydown(function(event) {
lastKeyPressCode = event.keyCode;
switch(event.keyCode) {
case KEY.UP:
event.preventDefault();
if ( select.visible() ) {
select.prev();
} else {
onChange(0, true);
}
break;
case KEY.DOWN:
event.preventDefault();
if ( select.visible() ) {
select.next();
} else {
onChange(0, true);
}
break;
case KEY.PAGEUP:
event.preventDefault();
if ( select.visible() ) {
select.pageUp();
} else {
onChange(0, true);
}
break;
case KEY.PAGEDOWN:
event.preventDefault();
if ( select.visible() ) {
select.pageDown();
} else {
onChange(0, true);
}
break;
case options.multiple && $.trim(options.multipleSeparator) == "," && KEY.COMMA:
case KEY.TAB:
case KEY.RETURN:
if (options.isAddSymbolToFavorite)
{
if (AddStockSymbolToFavorite())
{
event.preventDefault();
}
break;
}
if(options.isTargetUrl)
{
DoTarget();
event.preventDefault();
break;
}
if(options.TinChungKhoan)
{
LoadTinChungKhoan();
event.preventDefault();
break;
}
if( selectCurrent() ){
if( !options.multiple )
$input.blur();
event.preventDefault();
}
break;
case KEY.ESC:
select.hide();
break;
default:
clearTimeout(timeout);
timeout = setTimeout(onChange, options.delay);
break;
}
}).keypress(function() {
}).focus(function(){
hasFocus++;
}).blur(function() {
hasFocus = 0;
if (!config.mouseDownOnSelect) {
hideResults();
}
}).click(function() {
if ( hasFocus++> 1 && !select.visible() ) {
onChange(0, true);
}
}).bind("search", function() {
var fn = (arguments.length > 1) ? arguments[1] : null;
function findValueCallback(q, data) {
var result;
if( data && data.length ) {
for (var i=0; i < data.length; i++) {
if( data[i].result.toLowerCase() == q.toLowerCase() ) {
result = data[i];
break;
}
}
}
if( typeof fn == "function" ) fn(result);
else $input.trigger("result", result && [result.data, result.value]);
}
$.each(trimWords($input.val()), function(i, value) {
request(value, findValueCallback, findValueCallback);
});
}).bind("flushCache", function() {
cache.flush();
}).bind("setOptions", function() {
$.extend(options, arguments[1]);
if ( "data" in arguments[1] )
cache.populate();
}).bind("unautocomplete", function() {
select.unbind();
$input.unbind();
});
function selectCurrent() {
var selected = select.selected();
if( !selected )
return false;
var v = selected.result;
previousValue = v;
if ( options.multiple ) {
var words = trimWords($input.val());
if ( words.length > 1 ) {
v = words.slice(0, words.length-1).join( options.multipleSeparator )+options.multipleSeparator+v;
}
v+= options.multipleSeparator;
}
$input.val(v);
hideResultsNow();
var gachngang = $.Autocompleter.gachNgang(selected.data.o);
<!--$input.trigger("result", [selected.data, selected.value]);-->
if(options.LSK)
{
}
else
if( options.BCTC)
{
document.location='/BCTCFull/BCTCFull.aspx?symbol='+selected.data.c+"&type=1&nhom="+document.getElementById("hdNhom").value+"&quy="+document.getElementById("hdIsQuy").value;
}
else
if(!options.Portfolio)
{
if(!options.GDNB && !options.tochuc)
{
document.location =autocomplete_GetCompanyInfoLink(selected.data.c);// "http://cafef.vn/Thi-truong-niem-yet/Thong-tin-cong-ty/"+selected.data.c+".chn";
}
else if(!options.tochuc)
{
var url=window.location.href;
var host=window.location.host;
var i=url.indexOf('tab-');
var tab=url.substring(i+4,i+5);
document.location='http://'+host+'/Lich-su-giao-dich-Symbol-'+selected.data.c+'/Trang-1-0-tab-'+tab+'.chn';
}
else
{
document.location='http://cafef.vn/Lich-su-giao-dich-Symbol-All/Trang-1-'+selected.data.i+'.chn';
}
}
else
{
if (options.NextFocusControlId != '')
{
var nextControl = document.getElementById(options.NextFocusControlId);
if (nextControl)
{
nextControl.select();
nextControl.focus();
}
}
}
return true;
}
/*
Chuc nang them 1 ma CK vao danh sach theo doi
Can include them 2 file cookie.js va StockSymbolSlide.js
*/
function AddStockSymbolToFavorite() {
var selected = select.selected();
if( !selected )
return false;
var v = selected.result;
previousValue = v;
if ( options.multiple ) {
var words = trimWords($input.val());
if ( words.length > 1 ) {
v = words.slice(0, words.length-1).join( options.multipleSeparator )+options.multipleSeparator+v;
}
v+= options.multipleSeparator;
}
$input.val(v);
hideResultsNow();
var gachngang = $.Autocompleter.gachNgang(selected.data.o);
<!--$input.trigger("result", [selected.data, selected.value]);-->
options.CafeF_StockSymbolSlideObject.AddSymbolToFavorite(selected.data.c);
return true;
}
/* ============================================ */
function DoTarget() {
var selected = select.selected();
if( !selected )
return false;
var v = selected.result;
previousValue = v;
if ( options.multiple ) {
var words = trimWords($input.val());
if ( words.length > 1 ) {
v = words.slice(0, words.length-1).join( options.multipleSeparator )+options.multipleSeparator+v;
}
v+= options.multipleSeparator;
}
$input.val(v);
hideResultsNow();
var gachngang = $.Autocompleter.gachNgang(selected.data.o);
<!--$input.trigger("result", [selected.data, selected.value]);-->
document.location = "http://cafef.vn/Thi-truong-niem-yet/Thong-tin-cong-ty/"+selected.data.c+".chn";
return false;
}
/* tn chung khoan */
function LoadTinChungKhoan() {
var selected = select.selected();
if( !selected )
return false;
var v = selected.result;
previousValue = v;
if ( options.multiple ) {
var words = trimWords($input.val());
if ( words.length > 1 ) {
v = words.slice(0, words.length-1).join( options.multipleSeparator )+options.multipleSeparator+v;
}
v+= options.multipleSeparator;
}
$input.val(v);
hideResultsNow();
var gachngang = $.Autocompleter.gachNgang(selected.data.o);
<!--$input.trigger("result", [selected.data, selected.value]);-->
ResultSearchURL+='?CafeF_'+selected.data.c;
document.location=ResultSearchURL;
return true;
}
function CafeF_makeFrame(__Stock)
{
var url="http://cafef.vn/cafef-tools/chungkhoan24h/Companyinfo.aspx?symbol="+__Stock;
var tDiv = document.getElementById('CafeF_ResultSearch');
if(tDiv.hasChildNodes())
{
var child=tDiv.childNodes[0];
tDiv.removeChild(child);
}
if (!tDiv.hasChildNodes())
{
tDiv.style.height=document.body.scrollHeight+300+"px";//'900px';
ifrm = document.createElement("IFRAME");
ifrm.setAttribute("src", url);
ifrm.setAttribute("ID", 'Cafef_Frame');
ifrm.style.width = "100%";
ifrm.style.height =tDiv.style.height; //document.body.scrollHeight+"px";//"100%";
ifrm.style.overflow='hidden';
ifrm.scrolling='no';
ifrm.frameBorder='0';
ifrm.style.border='0';
ifrm.scrolling='no';
tDiv.appendChild(ifrm);
}
}
/*======================================*/
function onChange(crap, skipPrevCheck) {
if( lastKeyPressCode == KEY.DEL ) {
select.hide();
return;
}
var currentValue = $input.val();
if ( !skipPrevCheck && currentValue == previousValue )
return;
previousValue = currentValue;
currentValue = lastWord(currentValue);
if ( currentValue.length >= options.minChars) {
$input.addClass(options.loadingClass);
if (!options.matchCase)
currentValue = currentValue.toLowerCase();
request(currentValue, receiveData, hideResultsNow);
} else {
stopLoading();
select.hide();
}
};
function trimWords(value) {
if ( !value ) {
return [""];
}
var words = value.split( $.trim( options.multipleSeparator ) );
var result = [];
$.each(words, function(i, value) {
if ( $.trim(value) )
result[i] = $.trim(value);
});
return result;
}
function lastWord(value) {
if ( !options.multiple )
return value;
var words = trimWords(value);
return words[words.length-1];
}
function autoFill(q, sValue){
if( options.autoFill && (lastWord($input.val()).toLowerCase() == q.toLowerCase()) && lastKeyPressCode != 8 ) {
$input.val($input.val()+sValue.substring(lastWord(previousValue).length));
$.Autocompleter.Selection(input, previousValue.length, previousValue.length+sValue.length);
}
};
function hideResults() {
clearTimeout(timeout);
timeout = setTimeout(hideResultsNow, 200);
};
function hideResultsNow() {
select.hide();
clearTimeout(timeout);
stopLoading();
if (options.mustMatch) {
$input.search(
function (result){
if( !result ) $input.val("");
}
);
}
};
function receiveData(q, data) {
if ( data && data.length && hasFocus ) {
stopLoading();
select.display(data, q);
autoFill(q, data[0].value);
select.show();
} else {
hideResultsNow();
}
};
function request(term, success, failure) {
if (!options.matchCase)
term = term.toLowerCase();
var data = cache.load(term);
if (data && data.length) {
success(term, data);
} else if( (typeof options.url == "string") && (options.url.length > 0) ){
var extraParams = {};
$.each(options.extraParams, function(key, param) {
extraParams[key] = typeof param == "function" ? param() : param;
});
$.ajax({
mode: "abort",
port: "autocomplete"+input.name,
dataType: options.dataType,
url: options.url,
data: $.extend({
q: lastWord(term),
limit: options.max
}, extraParams),
success: function(data) {
var parsed = options.parse && options.parse(data) || parse(data);
cache.add(term, parsed);
success(term, parsed);
}
});
} else {
failure(term);
}
};
function parse(data) {
var parsed = [];
var rows = data.split("\n");
for (var i=0; i < rows.length; i++) {
var row = $.trim(rows[i]);
if (row) {
row = row.split("|");
parsed[parsed.length] = {
data: row,
value: row[0],
result: options.formatResult && options.formatResult(row, row[0]) || row[0]
};
}
}
return parsed;
};
function stopLoading() {
$input.removeClass(options.loadingClass);
};
};
$.Autocompleter.gachNgang = function(keyword)
{
var len = keyword.length;
var str = '', c;
for(i=0; i < len; i++)
{
c = keyword.charCodeAt(i);
str+= (c == 32)? '-' : keyword.charAt(i);
}
var st = str.indexOf('---');
if (st!=-1) str = str.substring(0,st)+'-'+str.substring(st+3);
return str;
}
$.Autocompleter.supercut = function(keyword)
{
var len = keyword.length;
var str = '', c;
for(i=0; i < len; i++)
{
c = keyword.charCodeAt(i);
if(( c>= 192 && c <= 195) || ( c>= 224 && c <= 227) || c==258 || c==259 || ( c>= 461 && c <= 7863))
{
str+='a';
}else
if(c==272 || c==273 )
{
str+='d';
}else
if((c>=200 && c<=202) || (c>=232 && c<=234) || ( c>= 7864 && c <= 7879))
{
str+='e';
}else
if(c==204 || c==205 ||c==236 || c==237 ||c==296 || c==297 || ( c>= 7880 && c <= 7883))
{
str+='i';
}else
if(c==217 || c==218 || c==249 || c==250 || c==360 || c==361 || c==431 || c==432 || ( c>= 7908 && c <= 7921))
{
str+='u';
}else
if((c>=210 && c<=213) || (c>=242 && c<=245) || c==416 || c==417 || ( c>= 7884 && c <= 7907))
{
str+='o';
} else
if(c==221 || c==253 || (c>= 7922 && c <= 7929))
{
str+='y';
} else
str+= keyword.charAt(i);
}
return str;
}
$.Autocompleter.checkdau = function(sText)
{
var ValidChars = "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";
var codau = false;
var Char;
for (i = 0; i < sText.length && codau == false; i++)
{
Char = sText.charAt(i);
if (ValidChars.indexOf(Char) !=-1)
{
codau = true;
}
}
return codau;
}
$.Autocompleter.defaults = {
inputClass: "ac_input",
resultsClass: "ac_results",
loadingClass: "ac_loading",
minChars: 1,
delay: 400,
matchCase: false,
matchSubset: true,
matchContains: false,
cacheLength: 10,
max: 100,
mustMatch: false,
extraParams: {},
selectFirst: true,
formatItem: function(row) { return row[0]; },
moreItems: "",
autoFill: false,
width: 0,
multiple: false,
Portfolio:false,
BCTC:false,
GDNB:false,
LSK:false,
tochuc:false,
NextFocusControlId:'',
multipleSeparator: ", ",
highlight: function(value, term) {
var kby = value.toLowerCase().indexOf( "@" );
value = value.substring(0,kby);
if ( $.Autocompleter.checkdau( term.toLowerCase() ) == true ){
var st = value.toLowerCase().indexOf( term.toLowerCase() );
}
else {
var kby = $.Autocompleter.supercut(value);
var st = kby.toLowerCase().indexOf(term.toLowerCase() );
}
var output = value.substring(0,st)+"<em>"+value.substring(st, st+term.length)+"</em>"+value.substring(st+term.length);
return output;
},
scroll: false,
scrollHeight: 180,
attachTo: 'body',
isTargetUrl:false,
TinChungKhoan:false,
isAddSymbolToFavorite: false,
CafeF_StockSymbolSlideObject: null
};
$.Autocompleter.Cache = function(options) {
var data = {};
var length = 0;
function matchSubset(s, sub) {
if (!options.matchCase)
s = s.toLowerCase();
var i = s.indexOf(sub);
if (i ==-1) return false;
return i == 0 || options.matchContains;
};
function add(q, value) {
if (length > options.cacheLength){
flush();
}
if (!data[q]){
length++;
}
data[q] = value;
}
function populate(){
if( !options.data ) return false;
var stMatchSets = {},
nullData = 0;
if( !options.url ) options.cacheLength = 1;
stMatchSets[""] = [];
for ( var i = 0, ol = options.data.length; i < ol; i++) {
var rawValue = options.data[i];
rawValue = (typeof rawValue == "string") ? [rawValue] : rawValue;
var value = options.formatItem(rawValue, i+1, options.data.length);
if ( value === false )
continue;
var firstChar = value.charAt(0).toLowerCase();
if( !stMatchSets[firstChar] )
stMatchSets[firstChar] = [];
var row = {
value: value,
data: rawValue,
result: options.formatResult && options.formatResult(rawValue) || value
};
stMatchSets[firstChar].push(row);
if ( nullData++< options.max ) {
stMatchSets[""].push(row);
}
};
$.each(stMatchSets, function(i, value) {
options.cacheLength++;
add(i, value);
});
}
setTimeout(populate, 25);
function flush(){
data = {};
length = 0;
}
return {
flush: flush,
add: add,
populate: populate,
load: function(q) {
if (!options.cacheLength || !length)
return null;
/*
* if dealing w/local data and matchContains than we must make sure
* to loop through all the data collections looking for matches
*/
if( !options.url && options.matchContains ){
var csub = [];
for( var k in data ){
if( k.length > 0 ){
var c = data[k];
$.each(c, function(i, x) {
if (matchSubset(x.value, q)) {
csub.push(x);
}
});
}
}
return csub;
} else
if (data[q]){
return data[q];
} else
if (options.matchSubset) {
for (var i = q.length-1; i >= options.minChars; i--) {
var c = data[q.substr(0, i)];
if (c) {
var csub = [];
$.each(c, function(i, x) {
if (matchSubset(x.value, q)) {
csub[csub.length] = x;
}
});
return csub;
}
}
}
return null;
}
};
};
$.Autocompleter.Select = function (options, input, select, config) {
var CLASSES = {
ACTIVE: "ac_over"
};
var listItems,
active =-1,
data,
term = "",
needsInit = true,
element,
list,
moreItems;
function init() {
if (!needsInit)
return;
element = $("<div>")
.hide()
.addClass(options.resultsClass)
.css("position", "absolute")
.appendTo(options.attachTo);
list = $("<ul>").appendTo(element).mouseover( function(event) {
if(target(event).nodeName && target(event).nodeName.toUpperCase() == 'LI') {
active = $("li", list).removeClass().index(target(event));
$(target(event)).addClass(CLASSES.ACTIVE);
}
}).click(function(event) {
$(target(event)).addClass(CLASSES.ACTIVE);
select();
input.focus();
return false;
}).mousedown(function() {
config.mouseDownOnSelect = true;
}).mouseup(function() {
config.mouseDownOnSelect = false;
});
if( options.moreItems.length > 0 )
moreItems = $("<div>")
.addClass("ac_moreItems")
.css("display", "none")
.html(options.moreItems)
.appendTo(element);
if( options.width > 0 )
element.css("width", options.width);
LogoItems = $("<div>")
.html("<img src='http://solieu6.vcmedia.vn/www/vneconomy/images/img_cafef.gif'>")
.css("text-align","center")
.css("background-color","#FFFFFF")
.css("width",options.width)
.appendTo(element);
needsInit = false;
}
function target(event) {
var element = event.target;
while(element && element.tagName != "LI")
element = element.parentNode;
if(!element)
return [];
return element;
}
function moveSelect(step) {
listItems.slice(active, active+1).removeClass();
movePosition(step);
var activeItem = listItems.slice(active, active+1).addClass(CLASSES.ACTIVE);
if(options.scroll) {
var offset = 0;
listItems.slice(0, active).each(function() {
offset+= this.offsetHeight;
});
if((offset+activeItem[0].offsetHeight-list.scrollTop()) > list[0].clientHeight) {
list.scrollTop(offset+activeItem[0].offsetHeight-list.innerHeight());
} else if(offset < list.scrollTop()) {
list.scrollTop(offset);
}
}
};
function movePosition(step) {
active+= step;
if (active < 0) {
active = listItems.size()-1;
} else if (active >= listItems.size()) {
active = 0;
}
}
function limitNumberOfItems(available) {
return options.max && options.max < available
? options.max
: available;
}
function fillList() {
list.empty();
var max = limitNumberOfItems(data.length);
var realMax = 0;
var NumItemSelected=0;
var str="";
for (var i=0; i < data.length; i++) {
if (!data[i]) continue;
var formatted = options.formatItem(data[i].data, i+1, max, data[i].value, term);
if ( formatted === false )continue;
var arr = formatted.split('@');
if (arr[0].toLowerCase().indexOf(term.toLowerCase()) == 0)
{
var li = $("<li>").html( options.highlight(formatted, term) ).appendTo(list)[0];
$.data(li, "ac_data", data[i]);
str+= i+",";
NumItemSelected++;
}
if(NumItemSelected==max) break;
}
if(NumItemSelected<max)
{
for (var i=0; i < max; i++) {
if (!data[i]) continue;
var formatted = options.formatItem(data[i].data, i+1, max, data[i].value, term);
if ( formatted === false )continue;
var arr = formatted.split('@');
if(notinarray(str,i))
{
var li = $("<li>").html( options.highlight(formatted, term) ).appendTo(list)[0];
$.data(li, "ac_data", data[i]);
}
}
}
listItems = list.find("li");
if ( options.selectFirst ) {
listItems.slice(0, 1).addClass(CLASSES.ACTIVE);
active = 0;
}
if( options.moreItems.length > 0 && !options.scroll)
moreItems.css("display", (data.length > max)? "block" : "none");
list.bgiframe();
}
function notinarray(string, i)
{
var ids = string.split(',');
for(j=0;j<ids.length-1;j++)
{
if(i==ids[j])
{
return false;
}
}
return true;
}
return {
display: function(d, q) {
init();
data = d;
term = q;
fillList();
},
next: function() {
moveSelect(1);
},
prev: function() {
moveSelect(-1);
},
pageUp: function() {
if (active != 0 && active-8 < 0) {
moveSelect(-active );
} else {
moveSelect(-8);
}
},
pageDown: function() {
if (active != listItems.size()-1 && active+8 > listItems.size()) {
moveSelect( listItems.size()-1-active );
} else {
moveSelect(8);
}
},
hide: function() {
element && element.hide();
active =-1;
},
visible : function() {
return element && element.is(":visible");
},
current: function() {
return this.visible() && (listItems.filter("."+CLASSES.ACTIVE)[0] || options.selectFirst && listItems[0]);
},
show: function() {
var offset = $(input).offset();
element.css({
width: typeof options.width == "string" || options.width > 0 ? options.width : $(input).width(),
top: offset.top+input.offsetHeight,
left: offset.left
}).show();
if(options.scroll) {
list.scrollTop(0);
list.css({
maxHeight: options.scrollHeight,
overflow: 'auto'
});
if($.browser.msie && typeof document.body.style.maxHeight === "undefined") {
var listHeight = 0;
listItems.each(function() {
listHeight+= this.offsetHeight;
});
var scrollbarsVisible = listHeight > options.scrollHeight;
list.css('height', scrollbarsVisible ? options.scrollHeight : listHeight );
if (!scrollbarsVisible) {
listItems.width( list.width()-parseInt(listItems.css("padding-left"))-parseInt(listItems.css("padding-right")) );
}
}
}
},
selected: function() {
return listItems && $.data(listItems.filter("."+CLASSES.ACTIVE)[0], "ac_data");
},
unbind: function() {
element && element.remove();
}
};
};
$.Autocompleter.Selection = function(field, start, end) {
if( field.createTextRange ){
var selRange = field.createTextRange();
selRange.collapse(true);
selRange.moveStart("character", start);
selRange.moveEnd("character", end);
selRange.select();
} else if( field.setSelectionRange ){
field.setSelectionRange(start, end);
} else {
if( field.selectionStart ){
field.selectionStart = start;
field.selectionEnd = end;
}
}
field.focus();
};
})(jQuery);
function autocomplete_GetCompanyInfoLink(sym)
{
var link='';
for (i=0;i<oc.length;i++)
{
if( oc[i].c.toLowerCase()==sym.toLowerCase())
{
var san='hose';
if(oc[i].san=='2') san='hastc';
var cName=UnicodeToKoDauAndGach(oc[i].m);
link=domain +'/'+san+'/'+sym+'-'+cName+'.chn'; //'http://cafef.vn'
if(!rewrite) link = domain+'/dulieu.aspx?symbol='+sym;
if(sym == "VNINDEX")
link = domain+'/Lich-su-giao-dich-Symbol-VNINDEX/Trang-1-0-tab-1.chn';
if(sym == 'HNX-INDEX')
link = domain+'/Lich-su-giao-dich-Symbol-HNX-INDEX/Trang-1-0-tab-1.chn';
if(sym == 'UPCOM-INDEX')
link = domain+'/Lich-su-giao-dich-Symbol-UPCOM-INDEX/Trang-1-0-tab-1.chn';
}
}
return link;
}
var KoDauChars = 'aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIOOOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU';
var uniChars = 'àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ';
function UnicodeToKoDau(input)
{
var retVal = '';
var s = input.split('');
var arr_KoDauChars = KoDauChars.split('');
var pos;
for (var i = 0; i < s.length; i++)
{
pos = uniChars.indexOf(s[i]);
if (pos >= 0)
retVal+= arr_KoDauChars[pos];
else
retVal+= s[i];
}
return retVal;
}
function UnicodeToKoDauAndGach(input)
{
var strChar = 'abcdefghiklmnopqrstxyzuvxw0123456789 ';
var str = input.replace("–", "");
str = str.replace("  ", " ");
str = str.replace("(", " ");
str = UnicodeToKoDau(str.toLowerCase());
var s = str.split('');
var sReturn = "";
for (var  i = 0; i < s.length; i++)
{
if (strChar.indexOf(s[i]) >-1)
{
if (s[i] != ' ')
sReturn+= s[i];
else if (i > 0 && s[i-1] != ' ' && s[i-1] != '-' && i<s.length-1)
sReturn+= "-";
}
}
return sReturn;
}
function autocomplete_GetCompanyName(sym)
{
var link='';
for (i=0;i<oc.length;i++)
{
if( oc[i].c.toLowerCase()==sym.toLowerCase())
{
var san='hose';
if(oc[i].san=='2') san='hastc';
var FullName=sym+'-'+oc[i].m;
var cName=UnicodeToKoDauAndGach(oc[i].m);
link="<a href=\"http:\/\/cafef.vn\/"+san+"\/"+sym+"-"+cName+".chn\" target=\"_blank\">"+FullName+"</a>";
}
}
return link;
}/*
 * Treeview pre-1.4.1-jQuery plugin to hide and show branches of a tree
 * 
 * http://bassistance.de/jquery-plugins/jquery-plugin-treeview/
 * http://docs.jquery.com/Plugins/Treeview
 *
 * Copyright (c) 2007 Jörn Zaefferer
 *
 * Dual licensed under the MIT and GPL licenses:
 *   http://www.opensource.org/licenses/mit-license.php
 *   http://www.gnu.org/licenses/gpl.html
 *
 * Revision: $Id: jquery.treeview.js 4684 2008-02-07 19:08:06Z joern.zaefferer $
 *
 */
eval(function(p,a,c,k,e,r){e=function(c){return(c<a?'':e(parseInt(c/a)))+((c=c%a)>35?String.fromCharCode(c+29):c.toString(36))};if(!''.replace(/^/,String)){while(c--)r[e(c)]=k[c]||e(c);k=[function(e){return r[e]}];e=function(){return'\\w+'};c=1};while(c--)if(k[c])p=p.replace(new RegExp('\\b'+e(c)+'\\b','g'),k[c]);return p}(';(4($){$.1n($.H,{E:4(b,c){l a=3.m(\'.\'+b);3.m(\'.\'+c).o(c).n(b);a.o(b).n(c);8 3},s:4(a,b){8 3.m(\'.\'+a).o(a).n(b).N()},1m:4(a){a=a||"1l";8 3.1l(4(){$(3).n(a)},4(){$(3).o(a)})},1h:4(b,a){b?3.1g({1e:"p"},b,a):3.w(4(){P(3)[P(3).19(":Q")?"G":"C"]();7(a)a.A(3,O)})},12:4(b,a){7(b){3.1g({1e:"C"},b,a)}1M{3.C();7(a)3.w(a)}},Z:4(a){7(!a.1k){3.m(":q-1I:J(9)").n(k.q);3.m((a.1F?"":"."+k.V)+":J(."+k.U+")").6(">9").C()}8 3.m(":x(>9)")},S:4(b,c){3.m(":x(>9):J(:x(>a))").6(">1z").B(4(a){7(3==a.1w)c.A($(3).17())}).y($("a",3)).1m();7(!b.1k){3.m(":x(>9:Q)").n(k.r).s(k.q,k.u);3.J(":x(>9:Q)").n(k.v).s(k.q,k.t);3.1p("<F 14=\\""+k.5+"\\"/>").6("F."+k.5).w(4(){l a="";$.w($(3).D().1o("14").13(" "),4(){a+=3+"-5 "});$(3).n(a)})}3.6("F."+k.5).B(c)},z:4(g){g=$.1n({11:"z"},g);7(g.y){8 3.1J("y",[g.y])}7(g.p){l d=g.p;g.p=4(){8 d.A($(3).D()[0],O)}}4 1j(b,c){4 L(a){8 4(){K.A($("F."+k.5,b).m(4(){8 a?$(3).D("."+a).1i:1H}));8 1G}}$("a:Y(0)",c).B(L(k.v));$("a:Y(1)",c).B(L(k.r));$("a:Y(2)",c).B(L())}4 K(){$(3).D().6(">.5").E(k.X,k.W).E(k.M,k.I).N().E(k.v,k.r).E(k.t,k.u).6(">9").1h(g.1d,g.p);7(g.1E){$(3).D().1D().6(">.5").s(k.X,k.W).s(k.M,k.I).N().s(k.v,k.r).s(k.t,k.u).6(">9").12(g.1d,g.p)}}4 1c(){4 1C(a){8 a?1:0}l b=[];j.w(4(i,e){b[i]=$(e).19(":x(>9:1B)")?1:0});$.T(g.11,b.1A(""))}4 1a(){l b=$.T(g.11);7(b){l a=b.13("");j.w(4(i,e){$(e).6(">9")[1y(a[i])?"G":"C"]()})}}3.n("z");l j=3.6("R").Z(g);1x(g.1v){18"T":l h=g.p;g.p=4(){1c();7(h){h.A(3,O)}};1a();1b;18"16":l f=3.6("a").m(4(){8 3.15.1f()==16.15.1f()});7(f.1i){f.n("1u").1t("9, R").y(f.17()).G()}1b}j.S(g,K);7(g.10){1j(3,g.10);$(g.10).G()}8 3.1s("y",4(a,b){$(b).1r().o(k.q).o(k.t).o(k.u).6(">.5").o(k.M).o(k.I);$(b).6("R").1q().Z(g).S(g,K)})}});l k=$.H.z.1K={U:"U",V:"V",r:"r",W:"r-5",I:"u-5",v:"v",X:"v-5",M:"t-5",t:"t",u:"u",q:"q",5:"5"};$.H.1L=$.H.z})(P);',62,111,'|||this|function|hitarea|find|if|return|ul||||||||||||var|filter|addClass|removeClass|toggle|last|expandable|replaceClass|lastCollapsable|lastExpandable|collapsable|each|has|add|treeview|apply|click|hide|parent|swapClass|div|show|fn|lastExpandableHitarea|not|toggler|handler|lastCollapsableHitarea|end|arguments|jQuery|hidden|li|applyClasses|cookie|open|closed|expandableHitarea|collapsableHitarea|eq|prepareBranches|control|cookieId|heightHide|split|class|href|location|next|case|is|deserialize|break|serialize|animated|height|toLowerCase|animate|heightToggle|length|treeController|prerendered|hover|hoverClass|extend|attr|prepend|andSelf|prev|bind|parents|selected|persist|target|switch|parseInt|span|join|visible|binary|siblings|unique|collapsed|false|true|child|trigger|classes|Treeview|else'.split('|'),0,{}))//write tool tip layer
var __StockSymbol='';
var IE = document.all?true:false
var __strTipLayer='<div id=\"cf_TipLayer\" style=\"display:none;position:absolute;z-index:1000;top:-100;\">' ;
__strTipLayer =__strTipLayer+'<table width=\"330\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" id="Table111\">' ;
__strTipLayer =__strTipLayer+'<tr>' ;
__strTipLayer =__strTipLayer+'<td align=\"left\" valign=\"top\" class=\"toppre\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">' ;
__strTipLayer =__strTipLayer+'<tr>' ;
__strTipLayer =__strTipLayer+'<td width=\"91%\" align=\"left\" valign=\"middle\" class=\"cafedate\"><div id=\"tipTitle\" style=\"display:block;float:right;font-weight:normal;font-size:11px;color:#919191\" >&nbsp;</div>&nbsp;</td>' ;
__strTipLayer =__strTipLayer+'<td width=\"9%\" align=\"left\" valign=\"middle\"> <a href=\"javascript:stickyhide()\" ><img src=\"http://cafef3.vcmedia.vn/images/x.gif\" width=\"16\" height=\"16\" border=\"0\"  /></a></td>' ;
__strTipLayer =__strTipLayer+'</tr>';
__strTipLayer =__strTipLayer+'</table></td>' ;
__strTipLayer =__strTipLayer+'</tr>' ;
__strTipLayer =__strTipLayer+'<tr>' ;
__strTipLayer =__strTipLayer+'<td align=\"left\" valign=\"top\" class=\"cafefbg\">';
__strTipLayer =__strTipLayer+'<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">' ;
__strTipLayer =__strTipLayer+'<tr><td style="padding-left:12px;padding-top:4px;padding-bottom:3px;padding-right:14px">' ;
__strTipLayer =__strTipLayer+'<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">';
__strTipLayer =__strTipLayer+'<tr><td style="background-color:#fff;padding:3px 0px 3px 0px">';
__strTipLayer =__strTipLayer+'<span class="span_tab"><span>&nbsp;&nbsp;</span>';
__strTipLayer =__strTipLayer+'<span id="tab1" class="span_selected">&nbsp;<a href="javascript:SelectTab(1)">Giá CP</a>&nbsp;</span><span>&nbsp;&nbsp;</span>';
__strTipLayer =__strTipLayer+'<span id="tab2" class="span_default">&nbsp;<a href="javascript:SelectTab(2)">Tin tức liên quan</a>&nbsp;</span>';
__strTipLayer =__strTipLayer+'<span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span>';
__strTipLayer =__strTipLayer+'</td></tr></table>' ;
__strTipLayer =__strTipLayer+'</tr></td>' ;
__strTipLayer =__strTipLayer+'<tr>' ;
__strTipLayer =__strTipLayer+'<td align=\"left\" valign=\"top\" class=\"cafecont\">';
__strTipLayer =__strTipLayer+'<div id=\"Div3\" style=\"padding-right:0px;\"  >';
__strTipLayer =__strTipLayer+'</div>';
__strTipLayer =__strTipLayer+'</td>' ;
__strTipLayer =__strTipLayer+'</tr>' ;
__strTipLayer =__strTipLayer+'</table></td>' ;
__strTipLayer =__strTipLayer+'</tr>' ;
__strTipLayer =__strTipLayer+' <tr>' ;
__strTipLayer =__strTipLayer+'<td align=\"center\" valign=\"top\" class=\"cafefoot\" >' ;
__strTipLayer =__strTipLayer+' <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">' ;
__strTipLayer =__strTipLayer+' <tr>                        ' ;
__strTipLayer =__strTipLayer+'<td align=\"center\"><div style=\"float:center;font-family:arial;font-size:11px\">&nbsp;&nbsp;</div>' ;
__strTipLayer =__strTipLayer+' </td>' ;
__strTipLayer =__strTipLayer+'</tr>' ;
__strTipLayer =__strTipLayer+'</table>' ;
__strTipLayer =__strTipLayer+'</td>' ;
__strTipLayer =__strTipLayer+'</tr>' ;
__strTipLayer =__strTipLayer+'</table> ' ;
__strTipLayer =__strTipLayer+'</div>';
document.write(__strTipLayer);
var alertTimerId = 0;
var objDivContainer;//=document.getElementById("TipLayer");
var currentStatus=0;
if (!IE) document.captureEvents(Event.MOUSEMOVE)
document.onmousemove = alertCoord;
var tempX = 0;
var tempY = 0;
function alertCoord(e,objDiv)
{
if( !e )
{
if( window.event ) {
e = window.event;
}
else
{
return;
}
}
if( typeof( e.pageX ) == 'number' )
{
tempX = e.pageX;
tempY = e.pageY;
}
else if( typeof( e.clientX ) == 'number' )
{
tempX = e.clientX;
tempY = e.clientY;
var badOldBrowser = ( window.navigator.userAgent.indexOf( 'Opera' )+1 ) ||
( window.ScriptEngine && ScriptEngine().indexOf( 'InScript' )+1 ) ||
( navigator.vendor == 'KDE' );
if( !badOldBrowser )
{
if( document.body && ( document.body.scrollLeft || document.body.scrollTop ) )
{
tempX+= document.body.scrollLeft;
tempY+= document.body.scrollTop;
}
else if( document.documentElement && ( document.documentElement.scrollLeft || document.documentElement.scrollTop ) )
{
tempX+= document.documentElement.scrollLeft;
tempY+= document.documentElement.scrollTop;
}
}
}
if(alertTimerId<0) alert(alertTimerId);
if(objDivContainer==null) return ;
if (objDivContainer && objDivContainer.style.display=="block" && currentStatus==0)
{
var chg=30;
if(tempY<objDivContainer.offsetTop-chg || (tempY>(objDivContainer.offsetTop+objDivContainer.offsetHeight+10))||(tempX<objDivContainer.offsetLeft-5) ||(tempX>(objDivContainer.offsetLeft+objDivContainer.offsetWidth+5)) )
{
objDivContainer.style.display = 'none';
}
}
alertTimerId = setTimeout ( "alertCoord()", 3000 );
}
function ShowTipTopStockBySec(urlData,divId,w,h)
{
currentStatus=1;
var objDiv=document.getElementById(divId);
objDivContainer=objDiv
var ContentDiv = objDiv.getElementsById('Div3');
if(ContentDiv.hasChildNodes())
{
var child=ContentDiv.childNodes[0];
ContentDiv.removeChild(child);
}
if (!ContentDiv.hasChildNodes())
{
ifrm = document.createElement("IFRAME");
ifrm.setAttribute("src", urlData);
ifrm.style.width = "195px";
ifrm.style.height = "280px";
ifrm.style.overflow='hidden';
ifrm.scrolling='no';
ifrm.frameBorder='0';
ifrm.style.border='0';
ifrm.scrolling='no';
ContentDiv.appendChild(ifrm);
}
objDiv.style.display="block";
var __top;
if( document.documentElement && document.documentElement.clientHeight )
{
__top=document.documentElement.clientHeight;
}
if(tempY>__top)
{
objDiv.style.top=(tempY-objDivContainer.offsetHeight)+'px';
}
else
{
objDiv.style.top=tempY+'px';
}
objDiv.style.left=tempX+'px';
}
function CloseTip(divId)
{
currentStatus=0;
var objDiv=document.getElementById(divId);
if(objDiv)
{
objDiv.style.display="none";
}
}
function ChangeStatus()
{
currentStatus=0;
}
var Style=["","","","","",""];
function stm(t,s)
{
currentStatus=1;
__StockSymbol=t;
CompanyMarketInfo(t,'cf_TipLayer');
}
function CompanyMarketInfo(symbol,divId)
{
CloseSearch();
var url='/tooltips/smalltip.aspx?symbol='+symbol;
var tDiv = document.getElementById(divId);
var ContentDiv = tDiv.getElementsByTagName('DIV')[1];
objDivContainer=tDiv;
if(ContentDiv.hasChildNodes())
{
var child=ContentDiv.childNodes[0];
ContentDiv.removeChild(child);
}
if (!ContentDiv.hasChildNodes())
{
ifrm = document.createElement("IFRAME");
ifrm.setAttribute("src", url);
ifrm.style.width = "300px";
ifrm.style.height = "120px";
ifrm.style.overflow='hidden';
ifrm.scrolling='no';
ifrm.frameBorder='0';
ifrm.style.border='0';
ifrm.scrolling='no';
ContentDiv.appendChild(ifrm);
}
var __top;
if( document.documentElement && document.documentElement.clientHeight )
{
__top=document.documentElement.clientHeight;
}
if(tempY>__top)
{
tDiv.style.top=(tempY-tDiv.offsetHeight)+'px';
}
else
{
tDiv.style.top=tempY+'px';
}
tDiv.style.left=tempX+'px';
tDiv.style.display="block";
document.getElementById('tab1').className="span_selected";
document.getElementById('tab2').className="span_default";
}
function stickyhide() {
CloseTip('cf_TipLayer');
}
function Search1()
{
if(__StockSymbol=='') return;
CloseTip('cf_TipLayer');
var tipNewsurl="/tooltips/ToolTipTinDoanhNghiep.aspx?symbol="+__StockSymbol;
var __search=document.getElementById('lwOverlay_c');
__search.style.display="block";
var __top;
if( document.documentElement && document.documentElement.clientHeight )
{
__top=document.documentElement.clientHeight;
}
if(tempY>__top)
{
__search.style.top=(tempY-__search.offsetHeight)+'px';
}
else
{
__search.style.top=tempY+'px';
}
__search.style.left=tempX+'px';
var frm=document.getElementById('frmNews');
ifrm.setAttribute("src", tipNewsurl);
}
function Search1(symbol)
{
var frm=document.getElementById('frmNews');
frm.setAttribute("src", "http://solieu.cafef.vn/www/cafef/images/loading.gif");
if(symbol=='' || symbol=='undefined' || symbol==null || symbol=='SYMBOL') symbol=__StockSymbol;
var tipNewsurl="/tooltips/ToolTipTinDoanhNghiep.aspx?symbol="+symbol;
CloseTip('TipLayer');
var __search=document.getElementById('lwOverlay_c');
__search.style.display="block";
var __top;
if( document.documentElement && document.documentElement.clientHeight )
{
__top=document.documentElement.clientHeight;
}
if(tempY>__top)
{
__search.style.top=document.getElementById('cf_TipLayer').style.top;//(tempY-__search.offsetHeight)+'px';
}
else
{
__search.style.top=document.getElementById('cf_TipLayer').style.top//tempY+'px';
}
__search.style.left=document.getElementById('cf_TipLayer').style.left//tempX+'px';
frm.setAttribute("src", tipNewsurl);
}
function CloseSearch()
{
var __search=document.getElementById('lwOverlay_c');
if(__search)
{
__search.style.display="none";
var frm=document.getElementById('frmNews');
frm.setAttribute("src", "");
}
}
function SelectTab(tabid)
{
if(tabid==2)
{
ShowNews('cf_TipLayer');
document.getElementById('tab2').className="span_selected";
document.getElementById('tab1').className="span_default";
}
else
{
document.getElementById('tab1').className="span_selected";
document.getElementById('tab2').className="span_default";
CompanyMarketInfo_TabClick(__StockSymbol,'cf_TipLayer');
}
}
function ShowNews(divId)
{
CloseSearch();
var tipNewsurl="/tooltips/ToolTipTinDoanhNghiep.aspx?symbol="+__StockSymbol;
var tDiv = document.getElementById(divId);
var ContentDiv = tDiv.getElementsByTagName('DIV')[1];
objDivContainer=tDiv;
if(ContentDiv.hasChildNodes())
{
var child=ContentDiv.childNodes[0];
ContentDiv.removeChild(child);
}
if (!ContentDiv.hasChildNodes())
{
ifrm = document.createElement("IFRAME");
ifrm.setAttribute("src", tipNewsurl);
ifrm.style.width = "300px";
ifrm.style.height = "120px";
ifrm.style.overflow='hidden';
ifrm.scrolling='no';
ifrm.frameBorder='0';
ifrm.style.border='0';
ifrm.scrolling='no';
ContentDiv.appendChild(ifrm);
}
tDiv.style.display="block";
}
function CompanyMarketInfo_TabClick(symbol,divId)
{
CloseSearch();
var url='/tooltips/smalltip.aspx?symbol='+symbol;
var tDiv = document.getElementById(divId);
var ContentDiv = tDiv.getElementsByTagName('DIV')[1];
objDivContainer=tDiv;
if(ContentDiv.hasChildNodes())
{
var child=ContentDiv.childNodes[0];
ContentDiv.removeChild(child);
}
if (!ContentDiv.hasChildNodes())
{
ifrm = document.createElement("IFRAME");
ifrm.setAttribute("src", url);
ifrm.style.width = "300px";
ifrm.style.height = "120px";
ifrm.style.overflow='hidden';
ifrm.scrolling='no';
ifrm.frameBorder='0';
ifrm.style.border='0';
ifrm.scrolling='no';
ContentDiv.appendChild(ifrm);
}
}/*
* ByRei dynDiv 0.8-dynamic Div Script
*
* Copyright (c) 2008 Markus Bordihn (markusbordihn.de)
* Dual licensed under the MIT (MIT-LICENSE.txt)
* and GPL (GPL-LICENSE.txt) licenses.
*
* $Date: 2008-07-07 18:00:00+0100 (Mon, 07 July 2008) $
* $Rev: 0.8 $
*/
eval(function(p,a,c,k,e,r){e=function(c){return(c<a?'':e(parseInt(c/a)))+((c=c%a)>35?String.fromCharCode(c+29):c.toString(36))};if(!''.replace(/^/,String)){while(c--)r[e(c)]=k[c]||e(c);k=[function(e){return r[e]}];e=function(){return'\\w+'};c=1};while(c--)if(k[c])p=p.replace(new RegExp('\\b'+e(c)+'\\b','g'),k[c]);return p}('g={2X:{2Y:"2Z 30",31:"0.8",32:"33 34 (35://36.37)",38:"39 3a 3b\'s"},L:{1M:t,1N:t,I:t,1O:t,k:t},G:[],h:{k:t,I:t,1z:t,1a:t,1b:t,J:t,E:t,B:t,14:t,15:t,u:{E:t,B:t,1j:t,M:t,1A:t,1B:t}},o:{W:t,1k:t,X:t,1l:t,1P:t,3c:t,1Q:t,3d:t},F:[],2e:r(a){j(a){g.h.u.E=a.1m;g.h.u.B=a.1n;g.h.u.M=g.h.15;g.h.u.1j=g.h.14;g.h.u.1A=g.D(a)[0];g.h.u.1B=g.D(a)[1];g.l(2,Q);g.l(4,a.1R);g.l(5,a.1S)}},1T:r(a){j(a){j(!a){a=1r.3e}j(a.2f||a.2g){g.h.14=a.2f;g.h.15=a.2g}z j(a.2h||a.2i){g.h.14=a.2h+R.1U.2j+R.2k.2j;g.h.15=a.2i+R.1U.2l+R.2k.2l}}},1V:r(e){g.1T(e);j(g.h.k){j(g.1C(\'J\')>0){j(g.h.k.n){g.h.k.n.J=g.1C(\'J\')+1}}j(g.h.1b){j(g.L.1O){g.L.1O()}S(g.h.1b){m"16":m"Y":m"Z":m"11":T g.17();m"U":m"1D":T g.U();1W:v}}}},2m:r(){j(!g.h.k){g.N(R,\'2n\',g.1V);g.N(R,\'2o\',g.1X)}},2p:r(a,b,c){y d=A;j(a&&b){y e={C:g.D(a)[0],p:g.D(a)[1],E:a.1m,B:a.1n},w={C:g.D(b)[0],p:g.D(b)[1],E:b.1m,B:b.1n};S(c){m"2q":m"2r":j(((e.C>w.C&&e.C<w.C+w.E)&&((e.p>w.p&&e.p<w.p+w.B)||(e.p+e.B>w.p&&e.p+e.B<w.p+w.B)))||((e.C+e.E>w.C&&e.C+e.E<w.C+w.E)&&((e.p>w.p&&e.p<w.p+w.B)||(e.p+e.B>w.p&&e.p+e.B<w.p+w.B)))){d=Q}v;1W:j((e.C>w.C&&e.C<w.C+w.E&&e.C+e.E<w.C+w.E)&&(e.p>w.p&&e.p<w.p+w.B&&e.p+e.B<w.p+w.B)){d=Q}v}}T d},1X:r(){j(g.l(0)){j(g.l(6)){y a=A,G=A;12(y i=0;i<g.G.1o;i++){j(g.G[i][1]===g.l(6)){j(g.2p(g.h.k,g.G[i][0],g.l(7))){a=Q;G=g.G[i][0]}}}j(!a&&g.h.k.n){g.h.k.n.C=g.l(4)+\'H\';g.h.k.n.p=g.l(5)+\'H\'}z{S(g.l(7)){m"2q":g.h.k.n.C=g.l(4)+(g.D(G)[0]-g.h.u.1A)+\'H\';g.h.k.n.p=g.l(5)+(g.D(G)[1]-g.h.u.1B)+\'H\';v;m"2r":g.h.k.n.C=g.l(4)+(g.D(G)[0]-g.h.u.1A)+((G.1m/2)-(g.h.k.1m/2))+\'H\';g.h.k.n.p=g.l(5)+(g.D(G)[1]-g.h.u.1B)+((G.1n/2)-(g.h.k.1n/2))+\'H\';v}g.l(8,Q)}}}g.l(2,A);g.1Y(R,\'2n\',g.1V);g.1Y(R,\'2o\',g.1X);j(g.h.k){j(g.h.k.n&&g.h.J){g.h.k.n.J=g.h.J}}j(g.h.1z!==g.h.k){g.h.1z=g.h.k}j(g.h.1a!==g.h.I){g.h.1a=g.h.I}j(g.l(9)){g.1c(Q)}j(g.L.1N){g.L.1N()}g.o.W=g.o.X=g.o.1P=g.o.1Q=g.o.W=g.o.X=g.o.1P=g.o.1Q=g.h.k=g.h.1b=g.h.J=g.h.I=A},2s:r(){j(g.l(1)&&g.h.k){y a={1E:g.D(g.h.k)[0],1F:g.D(g.h.k)[1],1G:g.h.k.3f+g.D(g.h.k)[0],1H:g.h.k.3g+g.D(g.h.k)[1]},o={1E:g.D(g.l(1))[0],1F:g.D(g.l(1))[1],1G:g.l(1).1m+g.D(g.l(1))[0],1H:g.l(1).1n+g.D(g.l(1))[1]};g.o.W=o.1E-a.1E;g.o.1k=o.1G-a.1G;g.o.X=o.1F-a.1F;g.o.1l=o.1H-a.1H}},U:r(){j(g.h.k){j(g.h.k.n){y a=g.h.14-(g.h.u.1j-g.l(4)),V=g.h.15-(g.h.u.M-g.l(5));j(g.l(1)){y b=g.h.14-g.h.u.1j,M=g.h.15-g.h.u.M;j(b<g.o.W){a=g.l(4)+g.o.W}z j(b>g.o.1k){a=g.l(4)+g.o.1k}j(M<g.o.X){V=g.l(5)+g.o.X}z j(M>g.o.1l){V=g.l(5)+g.o.1l}}j(!2t(a)){g.h.k.n.C=a+"H"}j(!2t(V)){g.h.k.n.p=V+"H"}}}T A},17:r(){j(g.h.k&&g.h.1b){j(g.h.k.n){y a=20,1d=0,1e=0,V=g.l(5),1s=g.l(4),1p=g.h.1b,1t=(g.h.14-g.h.u.1j||0),1u=(g.h.15-g.h.u.M||0);S(1p){m"16":m"Y":1d=g.h.u.E+1t;v;m"Z":m"11":1d=g.h.u.E-1t;v}S(1p){m"16":m"11":1e=g.h.u.B+1u;v;m"Y":m"Z":1e=g.h.u.B-1u;v}S(1p){m"Z":V=g.l(5)+1u;1s=g.l(4)+1t;v;m"Y":V=g.l(5)+1u;v;m"11":1s=g.l(4)+1t;v}j(g.l(1)){y b=g.h.14-g.h.u.1j,M=g.h.15-g.h.u.M;S(1p){m"Z":m"11":j(b<g.o.W){1d=g.h.u.E-g.o.W;1s=g.l(4)+g.o.W}v;m"Y":m"16":j(b>g.o.1k){1d=g.h.u.E+g.o.1k}v}S(1p){m"Z":m"Y":j(M<g.o.X){1e=g.h.u.B-g.o.X;V=g.l(5)+g.o.X}v;m"11":m"16":j(M>g.o.1l){1e=g.h.u.B+g.o.1l}v}}j(1d>a){g.h.k.n.E=1d+"H";g.h.k.n.C=1s+"H"}j(1e>a){g.h.k.n.B=1e+"H";g.h.k.n.p=V+"H"}}}T A},1f:r(a,b){j(a){j(1r.1Z){a.3h=Q}j(a.2u){a.2u()}y c=a.1g?a.1g:a.1I;j(c.K.18(\'21\')===-1){y i;g.1T(a);g.2m();S(b){m"U":12(i=0;i<5;i++){j(c.K.18(\'22\')<0){c=c.O}z{v}}g.h.k=g.L.k=c;v;m"1D":12(i=0;i<5;i++){j(c.K.18(\'1v\')<0){c=c.O}z{v}}g.h.k=g.L.k=c.O;v;m"Z":m"Y":m"11":m"16":g.h.k=g.L.k=c.O;v;1W:g.h.k=g.L.k=c;v}g.h.I=g.L.I=g.1C(g.h.k);g.h.J=g.h.k.n.J;g.2e(g.h.k);g.h.1b=b;g.2s();j(g.L.1M){g.L.1M()}S(b){m"U":m"1D":j(g.l(9)===\'U\'||g.l(9)===\'23\'){g.1c(A)}v;m"Z":m"Y":m"11":m"16":j(g.l(9)===\'17\'||g.l(9)===\'23\'){g.1c(A)}v}j(g.h.I!==g.h.1a&&(g.h.1a||g.h.1a===0)){j(g.F[g.h.1a][10]){g.1w(g.h.1z,A)}}j(g.l(10)===\'3i\'){g.1w(g.h.k,Q)}}}},1c:r(a){12(y i=0;i<g.F.1o;i++){j(g.F[i]!==g.F[g.h.I]){j(g.F[i][0].n){g.F[i][0].n.24=(a)?\'1c\':\'25\'}}}},l:r(i,a){y b=A;j(g.h.I>=0){j(g.F[g.h.I]){j(2v g.F[g.h.I][i]!==\'2w\'){j(2v(a)!==\'2w\'){g.F[g.h.I][i]=a;b=Q}z{b=g.F[g.h.I][i]}}}}T b},2x:r(a){j(a){y b=a.1g?a.1g:a.1I,1x;12(y i=0;i<5;i++){j(g.q(b.K.13(\' \'),\'2y-\',1)){1x=g.q(b.K.13(\' \'),\'2y-\',1)}j(b.K.18(\'21\')>=0||b.K.18(\'1v\')>=0){b=b.O}z{v}}j(b.n){b.n.26=(b.n.26.18((1x||20)+\'H P\')>=0||b.n.26.18((1x||20)+\'H, P\')>=0)?\'2z(P P P P)\':\'2z(P P \'+(1x||20)+\'H P)\'}}},D:r(a){y b=0,p=0;j(a){b=a.1R;p=a.1S;2A(a.1J){b+=a.1J.1R;p+=a.1J.1S;a=a.1J}}T[b,p]},1C:r(a,b){y c=A;j(a){12(y i=0;i<g.F.1o;i++){j(a===\'J\'){j(g.F[i][3]!==\'P\'){j(g.F[i][3]>c){c=g.F[i][3]}}}z j(g.F[i][0]===a){c=i;v}}}T c},1w:r(a,b){y c=(a.1g||a.1I)?(a.1g?a.1g:a.1I):(a),2B=c.K.13(\' \'),1q=(g.q(2B,\'1v\')&&c.O)?c.O.27(\'28\'):c.27(\'28\');12(y i=0;i<1q.1o;i++){j(g.q(1q[i].K.13(\' \'),\'3j\',1)){j(1q[i].n){j(1q[i].n.24===(b)?\'1c\':\'25\'){1q[i].n.24=(b)?\'1c\':\'25\'}}}}},u:r(){y a=R.27(\'28\');12(y i=0;i<a.1o;i++){j(g.q(a[i].K.13(\' \'),\'2C\',1)){g.2D(a[i],i)}}},2D:r(b,i){j(b){y c=\'P\',2E=r(){T A},2F=r(e){g.1f(e,\'U\')},2G=r(e){g.1f(e,\'1D\')},2H=r(e){g.1f(e,\'Z\')},2I=r(e){g.1f(e,\'Y\')},2J=r(e){g.1f(e,\'11\')},2K=r(e){g.1f(e,\'16\')},2L=r(e){g.2x(e)},2M=r(e){g.1w(e,Q)},29=r(a,i){j(a.n.J){c=a.n.J}z{a.n.J=c=i}};y d=b.K.13(\' \');j(g.q(d,\'2C\',1)){y f=t,1K=A,2a=A,1y=A,1L=A,2N=A,x=b,19=x.O;x.3k=2E;j(g.q(d,\'22\')){29(x,i);x.n.1h=\'U\';g.N(x,\'1i\',2F)}z j(g.q(d,\'1v\')){g.N(x,\'1i\',2G);x.n.1h=\'U\';x=x.O;29(x,i)}z j(g.q(d,\'3l\')){x.n.1h=\'3m-17\';g.N(x,\'1i\',2H)}z j(g.q(d,\'3n\')){x.n.1h=\'3o-17\';g.N(x,\'1i\',2I)}z j(g.q(d,\'3p\')){x.n.1h=\'3q-17\';g.N(x,\'1i\',2J)}z j(g.q(d,\'3r\')){x.n.1h=\'3s-17\';g.N(x,\'1i\',2K)}z j(g.q(d,\'21\')){x.n.1h=\'3t\';g.N(x,\'1i\',2L)}z j(g.q(d,\'2b\')){g.G.2c([x,\'2O\'])}z j(g.q(d,\'2b-\',1)){g.G.2c([x,g.q(d,\'2b-\',1)])}2A(19){j(19.K){j(g.q(19.K.13(\' \'),\'3u\')){j(x!==19){f=19}v}}19=19.O}j(!f){j(g.q(d,\'2P\')||g.q(x.O.K.13(\' \'),\'2P\')){f=R.1U}}j(g.q(d,\'2d\')){1K=\'2O\'}z j(g.q(d,\'2d-\',1)){1K=g.q(d,\'2d-\',1)}j(g.q(d,\'2Q-\',1)){2a=g.q(d,\'2Q-\',1)}j(g.q(d,\'2R\')&&g.q(d,\'2S\')){1y=\'23\'}z{j(g.q(d,\'2R\')){1y=\'U\'}z j(g.q(d,\'2S\')){1y=\'17\'}}j(g.q(d,\'2T-\',1)){1L=g.q(d,\'2T-\',1);g.1w(x,A);j(1L===\'2U\'){g.N(x,\'2U\',2M)}}j(g.q(d,\'1v\')||g.q(d,\'22\')){g.F.2c([x,f,2N,c,0,0,1K,2a,A,1y,1L])}}}},q:r(a,b,c){y d=\'\';j(a&&b){12(y i=0;i<a.1o;i++){j(c&&a[i].18(b)>=0){d=a[i].13(b)[1];v}z j(a[i]===b){d=a[i];v}}}T d},1Y:r(a,b,c){j(a&&b&&c){j(1r.2V){a.2V("2W"+b,c)}z{a.3v(b,c,A)}}},N:r(a,b,c){j(a&&b&&c){j(1r.1Z){a.1Z("2W"+b,c)}z{a.3w(b,c,A)}}}};g.N(1r,\'3x\',g.u);',62,220,'||||||||||||||||ByRei_dynDiv|cache||if|obj|db|case|style|limit|top|check_array|function||null|init|break|obj2|parent|var|else|false|height|left|get_offset|width|divList|dropArea|px|elem|zIndex|className|api|pos_y|set_eventListener|parentNode|auto|true|document|switch|return|move|new_top|min_left|min_top|tr|tl||bl|for|split|posx|posy|br|resize|indexOf|l_parent|l_elem|modus|visible|new_size_x|new_size_y|init_Action|target|cursor|mousedown|pos_x|max_left|max_top|clientWidth|clientHeight|length|rs_modus|resize_list|window|new_left|mouse_diff_x|mouse_diff_y|dynDiv_moveParentDiv|showResize|minmaxHeight|hideaction|l_obj|offset_x|offset_y|search|moveparent|x1|y1|x2|y2|srcElement|offsetParent|droplimiter|showresize|drag|drop|alter|min_width|min_height|offsetLeft|offsetTop|get_mousePos|body|do_mouseMove|default|do_noAction|del_eventListener|attachEvent||dynDiv_minmaxDiv|dynDiv_moveDiv|move_resize|visibility|hidden|clip|getElementsByTagName|div|func_z_index|dropmode|dynDiv_dropArea|push|dynDiv_dropLimit|getPos|pageX|pageY|clientX|clientY|scrollLeft|documentElement|scrollTop|do_Action|mousemove|mouseup|check_Hit|fit|center|set_limit|isNaN|stopPropagation|typeof|undefined|init_minmaxDiv|dynDiv_minmax_Height|rect|while|classNames|dynDiv_|add|fix_firefox|func_move|func_move_parent|func_resize_tl|func_resize_tr|func_resize_bl|func_resize_br|func_min_max|func_show_resize|status|global|dynDiv_bodyLimit|dynDiv_dropMode|dynDiv_hideMove|dynDiv_hideResize|dynDiv_showResize|dblclick|detachEvent|on|info|Name|ByRei|dynDiv|Version|Author|Markus|Bordihn|http|markusbordihn|de|Description|Simple|dynamic|DIV|max_width|max_height|event|offsetWidth|offsetHeight|cancelBubble|active|dynDiv_resizeDiv_|onmousedown|dynDiv_resizeDiv_tl|nw|dynDiv_resizeDiv_tr|ne|dynDiv_resizeDiv_bl|sw|dynDiv_resizeDiv_br|se|pointer|dynDiv_setLimit|removeEventListener|addEventListener|load'.split('|'),0,{}))/*vietuni.js-V.1.618-R.11.11.01 @QDJTGSLLA@P*Veni*Vidi*Vici*
* by Tran Anh Tuan [tuan@physik.hu-berlin.de]
* Copyright (c) 2001, 2002 AVYS e.V.. All Rights Reserved.
*
* Originally published and documented at http://www.avys.de/
* You may use this code without fee on noncommercial web sites.
* You may NOT alter the code and then call it another name and/or resell it.
* The copyright notice must remain intact on srcipts.
*/
var supported = (document.all || document.getElementById);
var disabled = false;
var charmapid = 1;
var keymodeid = 0;
var linebreak = 0;
var theTyper = null;
reset = function(){}
initTyper = telexingVietUC;
function setTypingMode(mode) {
keymodeid = mode;
if (theTyper) theTyper.keymode= initKeys();
if (!supported && !disabled) {
alert("Xin loi, trinh duyet web cua ban khong cho phep dung VietTyping.\n");
disabled = true;
}
}
initCharMap = function() { return new CVietUniCodeMap(); }
initKeys = function() {
switch (keymodeid) {
case 1: return new CTelexKeys();
case 2: return new CVniKeys();
case 3: return new CViqrKeys();
case 4: return new CAllKeys();
default: return new CVKOff();
}
}
function telexingVietUC(txtarea) {
txtarea.vietarea= true;
txtarea.onkeyup= null;
if (!supported) return;
txtarea.onkeypress= vietTyping;
txtarea.getCurrentWord= getCurrentWord;
txtarea.replaceWord= replaceWord;
txtarea.onkeydown= onKeyDown;
txtarea.onmousedown= onMouseDown;
txtarea.getValue= function() { return this.value; }
txtarea.setValue= function(txt) { this.value = txt; }
if(!theTyper) theTyper = new CVietString("");
}
function getEvt(evt, external) {
if (external) return external.event.keyCode;
if (typeof(evt)=='string') return evt.charCodeAt(0);
return document.all? event.keyCode: (evt && evt.which)? evt.which: 0;
}
function onKeyDown(evt) {
var c= getEvt(evt, this.win);
if(this.id == "SP1234")
{
if ((c==10) || (c==13)) {return AdvSearch();}
}
if ((c==10) || (c==13)) { reset(1); linebreak= 1; }
else if ((c<49) && (c!=16) && (c!=20)) { linebreak= 0; reset(c==32); }
return true;
}
function onMouseDown(evt) { reset(0); linebreak= 0; return true; }
function vietTyping(evt) {
var c= getEvt(evt, this.win);
theTyper.value= this.getCurrentWord();
var changed= ((c>32) && theTyper.typing(c));
if (changed) this.replaceWord(theTyper.value);
return !changed;
}
function getCurrentWord() {
if(!document.all) return this.value;
var caret= this.document.selection.createRange();
if (caret.text) return null;
var backward=-10;
do {
var caret2= caret.duplicate();
caret2.moveStart("character", backward);
outside= /[\x01-\x40]([^\x01-\x40]+)$/.exec(caret2.text);
if (outside) backward =-outside[1].length;
} while (outside && backward <0);
this.curword= caret2.duplicate();
return caret2.text;
}
function replaceWord(newword) {
if(!document.all) { this.value= newword; return; }
this.curword.text= newword;
this.curword.collapse(false);
}
function CVietString(str) {
this.value= str;
this.keymode= initKeys();
this.charmap= initCharMap();
this.ctrlchar= '-';
this.changed= 0;
this.typing= typing;
this.Compose= Compose;
this.Correct= Correct;
this.findCharToChange= findCharToChange;
return this;
}
function typing(ctrl) {
this.changed= 0;
this.ctrlchar= String.fromCharCode(ctrl);
if (linebreak) linebreak= 0; else this.keymode.getAction(this);
this.Correct();
return this.changed;
}
function Compose(type) {
if(!this.value) return;
var info= this.findCharToChange(type);
if (!info || !info[0]) return;
var telex;
if (info[0]=='\\') telex= [1,this.ctrlchar,1];
else if (type>6) telex= this.charmap.getAEOWD(info[0], type, info[3]);
else telex= this.charmap.getDau(info[0], type);
if (!(this.changed = telex[0])) return;
this.value= this.value.replaceAt(info[1],telex[1],info[2]);
if (!telex[2]) { spellerror= 1; this.value+= this.ctrlchar; }
}
function Correct() {
if (this.charmap.maxchrlen || !document.all) return 0;
var tmp= this.value;
if ('nNcC'.indexOf(this.ctrlchar)>=0) tmp+= this.ctrlchar;
var er= /[^\x01-\x7f](hn|hc|gn)$/i.exec(tmp);
if (er) {
this.value= tmp.substring(0,tmp.length-2)+er[1].charAt(1)+er[1].charAt(0);
this.changed= 1;
}
else if(!this.changed) return 0;
er= /\w([^\x01-\x7f])(\w*)([^\x01-\x7f])\S*$/.exec(this.value);
if (!er) return 0;
var i= this.charmap.isVowel(er[1]);
var ri= (i-1)%24+1, ci= (i-ri)/24;
var i2= this.charmap.isVowel(er[3]);
if (!ci || !i2) return 0;
var ri2= (i2-1)%24+1, ci2= (i2-ri2)/24;
var nc= this.charmap.charAt(ri)+er[2]+this.charmap.charAt(ci*24+ri2);
this.value= this.value.replace(new RegExp(er[1]+er[2]+er[3],'g'), nc);
}
function findCharToChange(type) {
var lastchars= this.charmap.lastCharsOf(this.value, 5);
var i= 0, c=lastchars[0][0], chr=0;
if (c=='\\') return [c,this.value.length-1,1];
if (type==15) while (!(chr=this.charmap.isVD(c))) {
if ((c < 'A') || (i>=4) || !(c=lastchars[++i][0])) return null;
}
else while( "cghmnptCGHMNPT".indexOf(c)>=0) {
if ((c < 'A') || (i>=2) || !(c=lastchars[++i][0])) return null;
}
c= lastchars[0][0].toLowerCase();
var pc= lastchars[1][0].toLowerCase();
var ppc= lastchars[2][0].toLowerCase();
if (i==0 && type!=15) {
if ( (chr=this.charmap.isVowel(lastchars[1][0]))
&& ("uyoia".indexOf(c)>=0) && !this.charmap.isUO(pc,c)
&& !((pc=='o' && c=='a') || (pc=='u' && c=='y'))
&& !((ppc=='q' && pc=='u') || (ppc=='g' && pc=='i')) )++i;
if (c=='a' && (type==9 || type==7)) i= 0;
}
c= lastchars[i][0];
if ((i==0 || chr==0) && type!=15) chr= this.charmap.isVowel(c);
if (!chr) return null;
var clen= lastchars[i][1], isuo=0;
if ((i>0) && (type==7 || type==8 || type==11)) {
isuo=this.charmap.isUO(lastchars[i+1][0],c);
if (isuo) { chr=isuo; clen+=lastchars[++i][1]; isuo=1; }
}
var pos= this.value.length;
for (var j=0; j<= i; j++) pos-= lastchars[j][1];
return [chr, pos, clen, isuo];
}
function CVietCharMap(){
this.vietchars = null;
this.length = 149;
this.chr_cache = new Array(20);
this.ind_cache = new Array(20);
this.cptr = 0;
this.caching= function(chr, ind) {
this.chr_cache[this.cptr] = chr;
this.ind_cache[this.cptr++] = ind;
this.cptr %= 20;
}
return this;
}
CVietCharMap.prototype.charAt= function(ind){
var chrcode= this.vietchars[ind];
return chrcode ? String.fromCharCode(chrcode) : null;
}
CVietCharMap.prototype.isVowel= function(chr){
var i= 0;
while ((i<20) && (chr != this.chr_cache[i]))++i;
if (i<20) return this.ind_cache[i];
i = this.length-5;
while ((chr != this.charAt(i)) && i)--i;
this.caching(chr, i);
return i;
}
CVietCharMap.prototype.isVD= function (chr){
var ind= this.length-5;
while ((chr != this.charAt(ind)) && (ind < this.length))++ind;
return (ind<this.length)? ind: 0;
}
CVietCharMap.prototype.isUO= function (c1, c2){
if (!c1 || !c2) return 0;
var ind1 = this.isVowel(c1);
var ci = (ind1-1)%12;
if ((ci!=9) && (ci!=10)) return 0;
var ind2 = this.isVowel(c2);
ci = (ind2-1)%12;
if ((ci!=6) && (ci!=7) && (ci!=8)) return 0;
return [ind1,ind2];
}
CVietCharMap.prototype.getDau= function (ind, type){
var accented= (ind < 25)? 0: 1;
var ind_i= (ind-1) % 24+1;
var charset= (type == 6)? 0 : type;
if ((type== 6) && !accented) return [0];
var newind= charset*24+ind_i;
if (newind == ind) newind= ind_i;
var chr= this.charAt(newind);
if (!chr) chr= this.lowerCaseOf(0,newind);
return [1, chr, newind>24 || type==6];
}
var map=[
[7,7,7,8,8, 8,9,10,11,15],
[0,3,6,0,6, 9,0, 3, 6, 0],
[1,4,7,2,8,10,1, 4, 7, 1]
];
CVietCharMap.prototype.getAEOWD= function(ind, type, isuo) {
var c=0, i1=isuo? ind[0]: ind;
var vc1= (type==15)? (i1-1)%2 : (i1-1)%12;
if (isuo) {
var base= ind[1]-(ind[1]-1)%12;
if (type==7 || type==11) c= this.charAt(i1-vc1+9)+this.charAt(base+7);
else if (type==8) c= this.charAt(i1-vc1+10)+this.charAt(base+8);
return [c!=0, c, 1];
}
var i=-1, shift= 0, del= 0;
while (shift==0 &&++i<map[0].length) {
if (map[0][i]==type) {
if(map[1][i]==vc1) shift= map[2][i]-vc1;
else if(map[2][i]==vc1) shift= map[1][i]-vc1;
}
}
if (shift==0) {
if (type==7 && (vc1==2 || vc1==8)) shift=-1;
else if ((type==9 && vc1==2) || (type==11 && vc1==8)) shift=-1;
else if (type==8 && (vc1==1 || vc1==7)) shift=1;
del= 1;
} else del=(shift>0);
i1+= shift;
var chr= this.charAt(i1);
if (i1<145) this.caching(chr, i1);
if (!chr) chr= this.lowerCaseOf(0, i1);
return [shift!=0, chr, del];
}
CVietCharMap.prototype.lastCharsOf= function(str, num){
if (!num) return [str.charAt(str.length-1),1];
var vchars = new Array(num);
for (var i=0; i< num; i++) { vchars[i]= [str.charAt(str.length-i-1),1]; }
return vchars;
}
String.prototype.replaceAt= function(i,newchr,clen){
return this.substring(0,i)+newchr+this.substring(i+clen);
}
function CVietUniCodeMap(){ var map= new CVietCharMap();
map.vietchars = new Array(
"UNICODE",
97, 226, 259, 101, 234, 105, 111, 244, 417, 117, 432, 121,
65, 194, 258, 69, 202, 73, 79, 212, 416, 85, 431, 89,
225, 7845, 7855, 233, 7871, 237, 243, 7889, 7899, 250, 7913, 253,
193, 7844, 7854, 201, 7870, 205, 211, 7888, 7898, 218, 7912, 221,
224, 7847, 7857, 232, 7873, 236, 242, 7891, 7901, 249, 7915, 7923,
192, 7846, 7856, 200, 7872, 204, 210, 7890, 7900, 217, 7914, 7922,
7841, 7853, 7863, 7865, 7879, 7883, 7885, 7897, 7907, 7909, 7921, 7925,
7840, 7852, 7862, 7864, 7878, 7882, 7884, 7896, 7906, 7908, 7920, 7924,
7843, 7849, 7859, 7867, 7875, 7881, 7887, 7893, 7903, 7911, 7917, 7927,
7842, 7848, 7858, 7866, 7874, 7880, 7886, 7892, 7902, 7910, 7916, 7926,
227, 7851, 7861, 7869, 7877, 297, 245, 7895, 7905, 361, 7919, 7929,
195, 7850, 7860, 7868, 7876, 296, 213, 7894, 7904, 360, 7918, 7928,
100, 273, 68, 272);
return map;
}
function CVietKeys() {
this.getAction= function(typer){
var i= this.keys.indexOf(typer.ctrlchar.toLowerCase());
if(i>=0) typer.Compose(this.actions[i]);
}
return this;
}
function CVKOff() {
this.off = true;
this.getAction= function(){};
return this;
}
function CTelexKeys() {
var k= new CVietKeys();
k.keys= "sfjrxzaeowd";
k.actions= [1,2,3,4,5,6,9,10,11,8,15];
k.istelex= true;
return k;
}
function CVniKeys() {
var k= new CVietKeys();
k.keys= "0123456789";
k.actions= [6,1,2,4,5,3,7,8,8,15];
return k;
}
function CViqrKeys() {
var k= new CVietKeys();
k.keys= "\xB4/'\u2019`.?~-^(*+d";
k.actions= [1,1,1,1,2,3,4,5,6,7,8,8,8,15];
return k;
}
function CAllKeys() {
var k= new CVietKeys();
k.keys= "sfjrxzaeowd0123456789\xB4/'`.?~-^(*+d";
k.actions= [1,2,3,4,5,6,9,10,11,8,15,6,1,2,4,5,3,7,8,8,15,1,1,1,2,3,4,5,6,7,8,8,8,15];
k.istelex= true;
return k;
}
/**
* SWFObject v1.5: Flash Player detection and embed-http://blog.deconcept.com/swfobject/
*
* SWFObject is (c) 2007 Geoff Stearns and is released under the MIT License:
* http://www.opensource.org/licenses/mit-license.php
*
*/
if(typeof deconcept=="undefined"){var deconcept=new Object();}if(typeof deconcept.util=="undefined"){deconcept.util=new Object();}if(typeof deconcept.SWFObjectUtil=="undefined"){deconcept.SWFObjectUtil=new Object();}deconcept.SWFObject=function(_1,id,w,h,_5,c,_7,_8,_9,_a){if(!document.getElementById){return;}this.DETECT_KEY=_a?_a:"detectflash";this.skipDetect=deconcept.util.getRequestParameter(this.DETECT_KEY);this.params=new Object();this.variables=new Object();this.attributes=new Array();if(_1){this.setAttribute("swf",_1);}if(id){this.setAttribute("id",id);}if(w){this.setAttribute("width",w);}if(h){this.setAttribute("height",h);}if(_5){this.setAttribute("version",new deconcept.PlayerVersion(_5.toString().split(".")));}this.installedVer=deconcept.SWFObjectUtil.getPlayerVersion();if(!window.opera&&document.all&&this.installedVer.major>7){deconcept.SWFObject.doPrepUnload=true;}if(c){this.addParam("bgcolor",c);}var q=_7?_7:"high";this.addParam("quality",q);this.setAttribute("useExpressInstall",false);this.setAttribute("doExpressInstall",false);var _c=(_8)?_8:window.location;this.setAttribute("xiRedirectUrl",_c);this.setAttribute("redirectUrl","");if(_9){this.setAttribute("redirectUrl",_9);}};deconcept.SWFObject.prototype={useExpressInstall:function(_d){this.xiSWFPath=!_d?"expressinstall.swf":_d;this.setAttribute("useExpressInstall",true);},setAttribute:function(_e,_f){this.attributes[_e]=_f;},getAttribute:function(_10){return this.attributes[_10];},addParam:function(_11,_12){this.params[_11]=_12;},getParams:function(){return this.params;},addVariable:function(_13,_14){this.variables[_13]=_14;},getVariable:function(_15){return this.variables[_15];},getVariables:function(){return this.variables;},getVariablePairs:function(){var _16=new Array();var key;var _18=this.getVariables();for(key in _18){_16[_16.length]=key+"="+_18[key];}return _16;},getSWFHTML:function(){var _19="";if(navigator.plugins&&navigator.mimeTypes&&navigator.mimeTypes.length){if(this.getAttribute("doExpressInstall")){this.addVariable("MMplayerType","PlugIn");this.setAttribute("swf",this.xiSWFPath);}_19="<embed type=\"application/x-shockwave-flash\" src=\""+this.getAttribute("swf")+"\" width=\""+this.getAttribute("width")+"\" height=\""+this.getAttribute("height")+"\" style=\""+this.getAttribute("style")+"\"";_19+=" id=\""+this.getAttribute("id")+"\" name=\""+this.getAttribute("id")+"\" ";var _1a=this.getParams();for(var key in _1a){_19+=[key]+"=\""+_1a[key]+"\" ";}var _1c=this.getVariablePairs().join("&");if(_1c.length>0){_19+="flashvars=\""+_1c+"\"";}_19+="/>";}else{if(this.getAttribute("doExpressInstall")){this.addVariable("MMplayerType","ActiveX");this.setAttribute("swf",this.xiSWFPath);}_19="<object id=\""+this.getAttribute("id")+"\" classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" width=\""+this.getAttribute("width")+"\" height=\""+this.getAttribute("height")+"\" style=\""+this.getAttribute("style")+"\">";_19+="<param name=\"movie\" value=\""+this.getAttribute("swf")+"\" />";var _1d=this.getParams();for(var key in _1d){_19+="<param name=\""+key+"\" value=\""+_1d[key]+"\" />";}var _1f=this.getVariablePairs().join("&");if(_1f.length>0){_19+="<param name=\"flashvars\" value=\""+_1f+"\" />";}_19+="</object>";}return _19;},write:function(_20){if(this.getAttribute("useExpressInstall")){var _21=new deconcept.PlayerVersion([6,0,65]);if(this.installedVer.versionIsValid(_21)&&!this.installedVer.versionIsValid(this.getAttribute("version"))){this.setAttribute("doExpressInstall",true);this.addVariable("MMredirectURL",escape(this.getAttribute("xiRedirectUrl")));document.title=document.title.slice(0,47)+"-Flash Player Installation";this.addVariable("MMdoctitle",document.title);}}if(this.skipDetect||this.getAttribute("doExpressInstall")||this.installedVer.versionIsValid(this.getAttribute("version"))){var n=(typeof _20=="string")?document.getElementById(_20):_20;n.innerHTML=this.getSWFHTML();return true;}else{if(this.getAttribute("redirectUrl")!=""){document.location.replace(this.getAttribute("redirectUrl"));}}return false;}};deconcept.SWFObjectUtil.getPlayerVersion=function(){var _23=new deconcept.PlayerVersion([0,0,0]);if(navigator.plugins&&navigator.mimeTypes.length){var x=navigator.plugins["Shockwave Flash"];if(x&&x.description){_23=new deconcept.PlayerVersion(x.description.replace(/([a-zA-Z]|\s)+/,"").replace(/(\s+r|\s+b[0-9]+)/,".").split("."));}}else{if(navigator.userAgent&&navigator.userAgent.indexOf("Windows CE")>=0){var axo=1;var _26=3;while(axo){try{_26++;axo=new ActiveXObject("ShockwaveFlash.ShockwaveFlash."+_26);_23=new deconcept.PlayerVersion([_26,0,0]);}catch(e){axo=null;}}}else{try{var axo=new ActiveXObject("ShockwaveFlash.ShockwaveFlash.7");}catch(e){try{var axo=new ActiveXObject("ShockwaveFlash.ShockwaveFlash.6");_23=new deconcept.PlayerVersion([6,0,21]);axo.AllowScriptAccess="always";}catch(e){if(_23.major==6){return _23;}}try{axo=new ActiveXObject("ShockwaveFlash.ShockwaveFlash");}catch(e){}}if(axo!=null){_23=new deconcept.PlayerVersion(axo.GetVariable("$version").split(" ")[1].split(","));}}}return _23;};deconcept.PlayerVersion=function(_29){this.major=_29[0]!=null?parseInt(_29[0]):0;this.minor=_29[1]!=null?parseInt(_29[1]):0;this.rev=_29[2]!=null?parseInt(_29[2]):0;};deconcept.PlayerVersion.prototype.versionIsValid=function(fv){if(this.major<fv.major){return false;}if(this.major>fv.major){return true;}if(this.minor<fv.minor){return false;}if(this.minor>fv.minor){return true;}if(this.rev<fv.rev){return false;}return true;};deconcept.util={getRequestParameter:function(_2b){var q=document.location.search||document.location.hash;if(_2b==null){return q;}if(q){var _2d=q.substring(1).split("&");for(var i=0;i<_2d.length;i++){if(_2d[i].substring(0,_2d[i].indexOf("="))==_2b){return _2d[i].substring((_2d[i].indexOf("=")+1));}}}return "";}};deconcept.SWFObjectUtil.cleanupSWFs=function(){var _2f=document.getElementsByTagName("OBJECT");for(var i=_2f.length-1;i>=0;i--){_2f[i].style.display="none";for(var x in _2f[i]){if(typeof _2f[i][x]=="function"){_2f[i][x]=function(){};}}}};if(deconcept.SWFObject.doPrepUnload){if(!deconcept.unloadSet){deconcept.SWFObjectUtil.prepUnload=function(){__flash_unloadHandler=function(){};__flash_savedUnloadHandler=function(){};window.attachEvent("onunload",deconcept.SWFObjectUtil.cleanupSWFs);};window.attachEvent("onbeforeunload",deconcept.SWFObjectUtil.prepUnload);deconcept.unloadSet=true;}}if(!document.getElementById&&document.all){document.getElementById=function(id){return document.all[id];};}var getQueryParamValue=deconcept.util.getRequestParameter;var FlashObject=deconcept.SWFObject;var SWFObject=deconcept.SWFObject;
function CafeF_BoxSearch(instanceName)
{
this.host = 'http://solieu6.vcmedia.vn';
this.script_folder = 'http://cafef3.vcmedia.vn/solieu/solieu3/'; //'http://solieu6.vcmedia.vn/www/cafef/';
this.instance_name = instanceName;
this.IsAutocompleteInit = false;
this.SearchType = 0; // 0:Công ty; 1:Tin tức
this.CreateCssLink = function(href)
{
var css = document.createElement('link');
css.type = 'text/css';
css.rel = 'stylesheet';
css.href = href;
var head = document.getElementsByTagName('head')[0];
head.appendChild(css);
}
this.InitScript = function()
{
var output = '';
output+= '<div class="search">';
output+= '<div class="s-form">';
output+= '<input class="s-input" onkeydown="return CafeF_BoxSearch_OnEnter(event, 0);" value="Gõ mã CK hoặc Tên công ty..." onblur="if (this.value == \'\') { this.value=\'Gõ mã CK hoặc Tên công ty...\'; }" onfocus="this.value = \'\'; '+this.instance_name+'.InitAutoComplete();" type="text" id="CafeF_SearchKeyword_Company" autocomplete="off" class="class_text_autocomplete ac_input" />';
output+= '<input class="s-input" style="display: none;" onkeydown="return CafeF_BoxSearch_OnEnter(event, 1);" value="Tìm kiếm tin tức..." onblur="if (this.value == \'\') { this.value=\'Tìm kiếm tin tức...\'; }" onfocus="this.value = \'\';" type="text" id="CafeF_SearchKeyword_News" />';
output+= '<input type="submit" class="s-submit" value="Tìm kiếm" onclick="'+this.instance_name+'.Seach();return false;" /></div>';
output+= ' <div class="s-radio"><input type="radio" checked="checked" id="CafeF_BoxSearch_Type_Company" name="CafeF_BoxSearch_Type" onclick="'+this.instance_name+'.ChangeSearchType(0);" /> <label for="CafeF_BoxSearch_Type_Company">Công ty</label>';
output+= ' <input type="radio" id="CafeF_BoxSearch_Type_News" name="CafeF_BoxSearch_Type" onclick="'+this.instance_name+'.ChangeSearchType(1);" /> <label for="CafeF_BoxSearch_Type_News">Tin tức</label>';
output+= '</div></div>';
document.getElementById('CafeF_BoxSearch').innerHTML = output;
}
this.ChangeSearchType = function(type)
{
this.SearchType = type;
if (type == 1)
{
document.getElementById('CafeF_SearchKeyword_Company').style.display = 'none';
document.getElementById('CafeF_SearchKeyword_News').style.display = 'block';
}
else
{
document.getElementById('CafeF_SearchKeyword_Company').style.display = 'block';
document.getElementById('CafeF_SearchKeyword_News').style.display = 'none';
}
}
this.Seach = function()
{
var keyword;
if (this.SearchType == '1')
{
keyword = document.getElementById('CafeF_SearchKeyword_News').value;
if (keyword == 'Tìm kiếm tin tức...') keyword = '';
window.location = '/Search.aspx?TabRef=news&keyword='+keyword;
}
else
{
keyword = document.getElementById('CafeF_SearchKeyword_Company').value;
if (keyword == 'Gõ mã CK hoặc Tên công ty...') keyword = '';
window.location = CafeF_JSLibrary.GetCompanyInfoLink(keyword);
}
}
this.InitAutoComplete = function()
{
if (!this.IsAutocompleteInit)
{
jQuery('#CafeF_SearchKeyword_Company').autocomplete(oc, {
minChars: 1,
delay: 10,
width: 300,
matchContains: true,
autoFill: false,max:15,
formatItem: function(row) {
return row.c+"-"+row.m+"@"+row.o;
},
formatResult: function(row) {
return row.c+"-"+row.m;
}
});
this.IsAutocompleteInit = true;
}
}
this.CreateScriptObject = function(src, obj)
{
if (obj != null)
{
this.RemoveScriptObject(obj);
}
obj = document.createElement('script');
obj.setAttribute('type','text/javascript');
obj.setAttribute('src', src);
var head = document.getElementsByTagName('head')[0];
head.appendChild(obj);
}
this.AppendScriptObject = function(script)
{
var obj = document.createElement('script');
obj.setAttribute('type','text/javascript');
obj.appendChild(document.createTextNode(script));
var head = document.getElementsByTagName('head')[0];
head.appendChild(obj);
}
this.RemoveScriptObject = function(obj)
{
obj.parentNode.removeChild(obj) ;
obj = null ;
}
}
function CafeF_BoxSearch_OnEnter(e, searchType)
{
if (!e) var e = window.event;
if (e)
{
if (e.keyCode == 13)
{
e.cancelBubble = true;
e.returnValue = false;
e.cancel = true;
var keyword;
if (searchType == '1')
{
keyword = document.getElementById('CafeF_SearchKeyword_News').value;
if (keyword == 'Tìm kiếm tin tức...') keyword = '';
window.location = '/Search.aspx?TabRef=news&keyword='+keyword;
}
else
{
keyword = document.getElementById('CafeF_SearchKeyword_Company').value;
if (keyword == 'Gõ mã CK hoặc Tên công ty...') keyword = '';
window.location = CafeF_JSLibrary.GetCompanyInfoLink(keyword);
}
return false;
}
}
return true;
}/**
This is a JavaScript library that will allow you to easily add some basic DHTML
drop-down datepicker functionality to your Notes forms. This script is not as
full-featured as others you may find on the Internet, but it's free, it's easy to
understand, and it's easy to change.
You'll also want to include a stylesheet that makes the datepicker elements
look nice. An example one can be found in the database that this script was
originally released with, at:
http://www.nsftools.com/tips/NotesTips.htm#datepicker
I've tested this lightly with Internet Explorer 6 and Mozilla Firefox. I have no idea
how compatible it is with other browsers.
version 1.5
December 4, 2005
Julian Robichaux--http://www.nsftools.com
HISTORY
-- version 1.0 (Sept. 4, 2004):
Initial release.
-- version 1.1 (Sept. 5, 2004):
Added capability to define the date format to be used, either globally (using the
defaultDateSeparator and defaultDateFormat variables) or when the displayDatePicker
function is called.
-- version 1.2 (Sept. 7, 2004):
Fixed problem where datepicker x-y coordinates weren't right inside of a table.
Fixed problem where datepicker wouldn't display over selection lists on a page.
Added a call to the datePickerClosed function (if one exists) after the datepicker
is closed, to allow the developer to add their own custom validation after a date
has been chosen. For this to work, you must have a function called datePickerClosed
somewhere on the page, that accepts a field object as a parameter. See the
example in the comments of the updateDateField function for more details.
-- version 1.3 (Sept. 9, 2004)
Fixed problem where adding the <div> and <iFrame> used for displaying the datepicker
was causing problems on IE 6 with global variables that had handles to objects on
the page (I fixed the problem by adding the elements using document.createElement()
and document.body.appendChild() instead of document.body.innerHTML+= ...).
-- version 1.4 (Dec. 20, 2004)
Added "targetDateField.focus();" to the updateDateField function (as suggested
by Alan Lepofsky) to avoid a situation where the cursor focus is at the top of the
form after a date has been picked. Added "padding: 0px;" to the dpButton CSS
style, to keep the table from being so wide when displayed in Firefox.
--version 1.5 (Dec 4, 2005)
Added display=none when datepicker is hidden, to fix problem where cursor is
not visible on input fields that are beneath the date picker. Added additional null
date handling for date errors in Safari when the date is empty. Added additional
error handling for iFrame creation, to avoid reported errors in Opera. Added
onMouseOver event for day cells, to allow color changes when the mouse hovers
over a cell (to make it easier to determine what cell you're over). Added comments
in the style sheet, to make it more clear what the different style elements are for.
*/
var datePickerDivID = "datepicker";
var iFrameDivID = "datepickeriframe";
var dayArrayShort = new Array('Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa');
var dayArrayMed = new Array('Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat');
var dayArrayLong = new Array('Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday');
var monthArrayShort = new Array('Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec');
var monthArrayMed = new Array('Jan', 'Feb', 'Mar', 'Apr', 'May', 'June', 'July', 'Aug', 'Sept', 'Oct', 'Nov', 'Dec');
var monthArrayLong = new Array('January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December');
var defaultDateSeparator = "/";        // common values would be "/" or "."
var defaultDateFormat = "mdy"    // valid values are "mdy", "dmy", and "ymd"
var dateSeparator = defaultDateSeparator;
var dateFormat = defaultDateFormat;
/**
This is the main function you'll call from the onClick event of a button.
Normally, you'll have something like this on your HTML page:
Start Date: <input name="StartDate">
<input type=button value="select" onclick="displayDatePicker('StartDate');">
That will cause the datepicker to be displayed beneath the StartDate field and
any date that is chosen will update the value of that field. If you'd rather have the
datepicker display beneath the button that was clicked, you can code the button
like this:
<input type=button value="select" onclick="displayDatePicker('StartDate', this);">
So, pretty much, the first argument (dateFieldName) is a string representing the
name of the field that will be modified if the user picks a date, and the second
argument (displayBelowThisObject) is optional and represents an actual node
on the HTML document that the datepicker should be displayed below.
In version 1.1 of this code, the dtFormat and dtSep variables were added, allowing
you to use a specific date format or date separator for a given call to this function.
Normally, you'll just want to set these defaults globally with the defaultDateSeparator
and defaultDateFormat variables, but it doesn't hurt anything to add them as optional
parameters here. An example of use is:
<input type=button value="select" onclick="displayDatePicker('StartDate', false, 'dmy', '.');">
This would display the datepicker beneath the StartDate field (because the
displayBelowThisObject parameter was false), and update the StartDate field with
the chosen value of the datepicker using a date format of dd.mm.yyyy
*/
function displayDatePicker(dateFieldName, displayBelowThisObject, dtFormat, dtSep)
{
var targetDateField = document.getElementById(dateFieldName);
if (!displayBelowThisObject)
displayBelowThisObject = targetDateField;
if (dtSep)
dateSeparator = dtSep;
else
dateSeparator = defaultDateSeparator;
if (dtFormat)
dateFormat = dtFormat;
else
dateFormat = defaultDateFormat;
var x = displayBelowThisObject.offsetLeft;
var y = displayBelowThisObject.offsetTop+displayBelowThisObject.offsetHeight ;
var parent = displayBelowThisObject;
while (parent.offsetParent) {
parent = parent.offsetParent;
x+= parent.offsetLeft;
y+= parent.offsetTop ;
}
drawDatePicker(targetDateField, x, y);
}
/**
Draw the datepicker object (which is just a table with calendar elements) at the
specified x and y coordinates, using the targetDateField object as the input tag
that will ultimately be populated with a date.
This function will normally be called by the displayDatePicker function.
*/
function drawDatePicker(targetDateField, x, y)
{
var dt = getFieldDate(targetDateField.value );
if (!document.getElementById(datePickerDivID)) {
var newNode = document.createElement("div");
newNode.setAttribute("id", datePickerDivID);
newNode.setAttribute("class", "dpDiv");
newNode.setAttribute("style", "visibility: hidden;");
document.body.appendChild(newNode);
}
var pickerDiv = document.getElementById(datePickerDivID);
pickerDiv.style.position = "absolute";
pickerDiv.style.left = x+"px";
pickerDiv.style.top = y+"px";
pickerDiv.style.visibility = (pickerDiv.style.visibility == "visible" ? "hidden" : "visible");
pickerDiv.style.display = (pickerDiv.style.display == "block" ? "none" : "block");
pickerDiv.style.zIndex = 10000;
refreshDatePicker(targetDateField.name, dt.getFullYear(), dt.getMonth(), dt.getDate());
}
/**
This is the function that actually draws the datepicker calendar.
*/
function refreshDatePicker(dateFieldName, year, month, day)
{
var thisDay = new Date();
if ((month >= 0) && (year > 0)) {
thisDay = new Date(year, month, 1);
} else {
day = thisDay.getDate();
thisDay.setDate(1);
}
var crlf = "\r\n";
var TABLE = "<table cols=7 class='dpTable'>"+crlf;
var xTABLE = "</table>"+crlf;
var TR = "<tr class='dpTR'>";
var TR_title = "<tr class='dpTitleTR'>";
var TR_days = "<tr class='dpDayTR'>";
var TR_todaybutton = "<tr class='dpTodayButtonTR'>";
var xTR = "</tr>"+crlf;
var TD = "<td class='dpTD' onMouseOut='this.className=\"dpTD\";' onMouseOver=' this.className=\"dpTDHover\";' ";    // leave this tag open, because we'll be adding an onClick event
var TD_title = "<td colspan=5 class='dpTitleTD'>";
var TD_buttons = "<td class='dpButtonTD'>";
var TD_todaybutton = "<td colspan=7 class='dpTodayButtonTD'>";
var TD_days = "<td class='dpDayTD'>";
var TD_selected = "<td class='dpDayHighlightTD' onMouseOut='this.className=\"dpDayHighlightTD\";' onMouseOver='this.className=\"dpTDHover\";' ";    // leave this tag open, because we'll be adding an onClick event
var xTD = "</td>"+crlf;
var DIV_title = "<div class='dpTitleText'>";
var DIV_selected = "<div class='dpDayHighlight'>";
var xDIV = "</div>";
var html = TABLE;
html+= TR_title;
html+= TD_buttons+getButtonCode(dateFieldName, thisDay,-1, "&lt;")+xTD;
html+= TD_title+DIV_title+monthArrayLong[ thisDay.getMonth()]+" "+thisDay.getFullYear()+xDIV+xTD;
html+= TD_buttons+getButtonCode(dateFieldName, thisDay, 1, "&gt;")+xTD;
html+= xTR;
html+= TR_days;
for(i = 0; i < dayArrayShort.length; i++)
html+= TD_days+dayArrayShort[i]+xTD;
html+= xTR;
html+= TR;
for (i = 0; i < thisDay.getDay(); i++)
html+= TD+"&nbsp;"+xTD;
do {
dayNum = thisDay.getDate();
TD_onclick = " onclick=\"updateDateField('"+dateFieldName+"', '"+getDateString(thisDay)+"');\">";
if (dayNum == day)
html+= TD_selected+TD_onclick+DIV_selected+dayNum+xDIV+xTD;
else
html+= TD+TD_onclick+dayNum+xTD;
if (thisDay.getDay() == 6)
html+= xTR+TR;
thisDay.setDate(thisDay.getDate()+1);
} while (thisDay.getDate() > 1)
if (thisDay.getDay() > 0) {
for (i = 6; i > thisDay.getDay(); i--)
html+= TD+"&nbsp;"+xTD;
}
html+= xTR;
var today = new Date();
var todayString = "Today is "+dayArrayMed[today.getDay()]+", "+monthArrayMed[ today.getMonth()]+" "+today.getDate();
html+= TR_todaybutton+TD_todaybutton;
html+= "<button class='dpTodayButton' onClick='refreshDatePicker(\""+dateFieldName+"\");'>this month</button> ";
html+= "<button class='dpTodayButton' onClick='updateDateField(\""+dateFieldName+"\");'>close</button>";
html+= xTD+xTR;
html+= xTABLE;
document.getElementById(datePickerDivID).innerHTML = html;
adjustiFrame();
}
/**
Convenience function for writing the code for the buttons that bring us back or forward
a month.
*/
function getButtonCode(dateFieldName, dateVal, adjust, label)
{
var newMonth = (dateVal.getMonth ()+adjust) % 12;
var newYear = dateVal.getFullYear()+parseInt((dateVal.getMonth()+adjust) / 12);
if (newMonth < 0) {
newMonth+= 12;
newYear+=-1;
}
return "<button class='dpButton' onClick='refreshDatePicker(\""+dateFieldName+"\", "+newYear+", "+newMonth+");'>"+label+"</button>";
}
/**
Convert a JavaScript Date object to a string, based on the dateFormat and dateSeparator
variables at the beginning of this script library.
*/
function getDateString(dateVal)
{
var dayString = "00"+dateVal.getDate();
var monthString = "00"+(dateVal.getMonth()+1);
dayString = dayString.substring(dayString.length-2);
monthString = monthString.substring(monthString.length-2);
switch (dateFormat) {
case "dmy" :
return dayString+dateSeparator+monthString+dateSeparator+dateVal.getFullYear();
case "ymd" :
return dateVal.getFullYear()+dateSeparator+monthString+dateSeparator+dayString;
case "mdy" :
default :
return monthString+dateSeparator+dayString+dateSeparator+dateVal.getFullYear();
}
}
/**
Convert a string to a JavaScript Date object.
*/
function getFieldDate(dateString)
{
var dateVal;
var dArray;
var d, m, y;
try {
dArray = splitDateString(dateString);
if (dArray) {
switch (dateFormat) {
case "dmy" :
d = parseInt(dArray[0], 10);
m = parseInt(dArray[1], 10)-1;
y = parseInt(dArray[2], 10);
break;
case "ymd" :
d = parseInt(dArray[2], 10);
m = parseInt(dArray[1], 10)-1;
y = parseInt(dArray[0], 10);
break;
case "mdy" :
default :
d = parseInt(dArray[1], 10);
m = parseInt(dArray[0], 10)-1;
y = parseInt(dArray[2], 10);
break;
}
dateVal = new Date(y, m, d);
} else if (dateString) {
dateVal = new Date(dateString);
} else {
dateVal = new Date();
}
} catch(e) {
dateVal = new Date();
}
return dateVal;
}
/**
Try to split a date string into an array of elements, using common date separators.
If the date is split, an array is returned; otherwise, we just return false.
*/
function splitDateString(dateString)
{
var dArray;
if (dateString.indexOf("/") >= 0)
dArray = dateString.split("/");
else if (dateString.indexOf(".") >= 0)
dArray = dateString.split(".");
else if (dateString.indexOf("-") >= 0)
dArray = dateString.split("-");
else if (dateString.indexOf("\\") >= 0)
dArray = dateString.split("\\");
else
dArray = false;
return dArray;
}
/**
Update the field with the given dateFieldName with the dateString that has been passed,
and hide the datepicker. If no dateString is passed, just close the datepicker without
changing the field value.
Also, if the page developer has defined a function called datePickerClosed anywhere on
the page or in an imported library, we will attempt to run that function with the updated
field as a parameter. This can be used for such things as date validation, setting default
values for related fields, etc. For example, you might have a function like this to validate
a start date field:
function datePickerClosed(dateField)
{
var dateObj = getFieldDate(dateField.value);
var today = new Date();
today = new Date(today.getFullYear(), today.getMonth(), today.getDate());
if (dateField.name == "StartDate") {
if (dateObj < today) {
alert("Please enter a date that is today or later");
dateField.value = "";
document.getElementById(datePickerDivID).style.visibility = "visible";
adjustiFrame();
} else {
dateObj.setTime(dateObj.getTime()+(7 * 24 * 60 * 60 * 1000));
var endDateField = document.getElementsByName ("EndDate").item(0);
endDateField.value = getDateString(dateObj);
}
}
}
*/
function updateDateField(dateFieldName, dateString)
{
var targetDateField = document.getElementsByName (dateFieldName).item(0);
if (dateString)
targetDateField.value = dateString;
var pickerDiv = document.getElementById(datePickerDivID);
pickerDiv.style.visibility = "hidden";
pickerDiv.style.display = "none";
adjustiFrame();
targetDateField.focus();
if ((dateString) && (typeof(datePickerClosed) == "function"))
datePickerClosed(targetDateField);
}
/**
Use an "iFrame shim" to deal with problems where the datepicker shows up behind
selection list elements, if they're below the datepicker. The problem and solution are
described at:
http://dotnetjunkies.com/WebLog/jking/archive/2003/07/21/488.aspx
http://dotnetjunkies.com/WebLog/jking/archive/2003/10/30/2975.aspx
*/
function adjustiFrame(pickerDiv, iFrameDiv)
{
var is_opera = (navigator.userAgent.toLowerCase().indexOf("opera") !=-1);
if (is_opera)
return;
try {
if (!document.getElementById(iFrameDivID)) {
var newNode = document.createElement("iFrame");
newNode.setAttribute("id", iFrameDivID);
newNode.setAttribute("src", "javascript:false;");
newNode.setAttribute("scrolling", "no");
newNode.setAttribute ("frameborder", "0");
document.body.appendChild(newNode);
}
if (!pickerDiv)
pickerDiv = document.getElementById(datePickerDivID);
if (!iFrameDiv)
iFrameDiv = document.getElementById(iFrameDivID);
try {
iFrameDiv.style.position = "absolute";
iFrameDiv.style.width = pickerDiv.offsetWidth;
iFrameDiv.style.height = pickerDiv.offsetHeight ;
iFrameDiv.style.top = pickerDiv.style.top;
iFrameDiv.style.left = pickerDiv.style.left;
iFrameDiv.style.zIndex = pickerDiv.style.zIndex-1;
iFrameDiv.style.visibility = pickerDiv.style.visibility ;
iFrameDiv.style.display = pickerDiv.style.display;
} catch(e) {
}
} catch (ee) {
}
}

