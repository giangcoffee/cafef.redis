function CafeF_StockSymbolSlide(instanceName)
{
    this.bm_st_time_exp = 720*365
    this.bm_st_state_time_exp = 720;;
    this.CookiesName = 'favorite_stocks';
    this.StateCookiesName = 'favorite_stocks_state';
    
    this.AUTO_REFRESH_TIME = 15000;
    this.timerId_Refresh = -1;
    
    this.IsLogged = 0;
    
    this.MaxSymbolsInQueue = 10;
    this.MaxSymbolsDisplay = 7;
    this.OldCellDatas = new Array();
    for (var i = 0; i < this.MaxSymbolsDisplay; i++)
    {
        this.OldCellDatas[i] = '';
    }
    
    this.host = 'http://solieu4.cafef.vn';
    //this.host = 'http://localhost:8081';
    this.script_folder = 'http://solieu.cafef.vn/www/cafef/';
    this.script_object = null;
    this.stock_symbols_data = null;
    this.symbol_list = '';
    this.display_list = '';
    this.instance_name = instanceName;
    
    this.StockTrading_StartTime = '08:00:00';
    this.StockTrading_EndTime = '11:05:00';
    this.StockTrading_DayOfWeek = '1,2,3,4,5';
    this.Fields = {'Price':0,'Change':1,'ChangePercent':2};
    
    this.CreateCssLink = function(href)
    {
        var css = document.createElement('link');
        css.type = 'text/css';
        css.rel = 'stylesheet';
        css.href = href;
        var head = document.getElementsByTagName('head')[0];
        head.appendChild(css);
    }
    
    this.IsStockTrading = function()
    {
        var startTime = new Date();
        var endTime = new Date();
        var now = new Date();
        var day = ',' + this.StockTrading_DayOfWeek + ',';
        
        var start = this.StockTrading_StartTime.split(':');
        var end = this.StockTrading_EndTime.split(':');
        
        startTime.setHours(start[0], start[1], start[2]);
        endTime.setHours(end[0], end[1], end[2]);
        
        return (now >= startTime && now <= endTime && day.indexOf(',' + now.getDay() + ',') >= 0);
    }

    this.InitScript = function(containerId)
    {
        this.CreateCssLink(this.script_folder + 'css/cafef.css');
//        this.CreateScriptObject(this.host + '/Public/js/jquery.js');
//        this.CreateScriptObject('http://cafef.vn/Scripts/Library.js?upd=26881057');
//        this.CreateScriptObject(this.host + '/Public/js/jqDnR.js');
//        this.CreateScriptObject('http://cafef.vn/Scripts/AutoComplete/kby.js');
//        this.CreateScriptObject(this.host + '/Public/js/jquery.bgiframe.min.js');
//        this.CreateScriptObject(this.host + '/Public/js/jquery.dimensions.js');
//        this.CreateScriptObject(this.host + '/Public/js/jquery.autocomplete2.js');
        
        var output = '';
        
        output += '<table align="center" id="CafeF_StockSymbolSlideTable" border="0" cellpadding="0" cellspacing="0"><tr>';
        output += '<td class="Left_Right"><img alt="" src="' + this.script_folder + 'Images/symbolslide_left.gif" /></td>';
        output += '<td width="15px" class="Button RightBorder" align="center" onclick="' + this.instance_name + '.MoveLeft();"><img alt="" src="' + this.script_folder + 'Images/left.gif" /></td>';
        output += '<td id="CafeF_StockSymbolSlideTable_Cell6" style="background-color: #F6F6F6;" class="RightBorder"></td>';
        output += '<td id="CafeF_StockSymbolSlideTable_Cell5" class="RightBorder"></td>';
        output += '<td id="CafeF_StockSymbolSlideTable_Cell4" style="background-color: #F6F6F6;" class="RightBorder"></td>';
        output += '<td id="CafeF_StockSymbolSlideTable_Cell3" class="RightBorder"></td>';
        output += '<td id="CafeF_StockSymbolSlideTable_Cell2" style="background-color: #F6F6F6;" class="RightBorder"></td>';
        output += '<td id="CafeF_StockSymbolSlideTable_Cell1" class="RightBorder"></td>';
        output += '<td id="CafeF_StockSymbolSlideTable_Cell0" style="background-color: #F6F6F6;" class="RightBorder"></td>';
        output += '<td class="Button RightBorder" onclick="' + this.instance_name + '.MoveRight();"><img alt="" src="' + this.script_folder + 'Images/right.gif" /></td>';
        output += '<td nowrap="nowrap">';
        output += '<div style="width: 100%; text-align: right; height: 6px;"><img alt="" title="Ẩn danh sách CP đang theo dõi" onclick="' + this.instance_name + '.HideSlide()" src="' + this.script_folder + 'Images/close2.gif" /></div>';
        output += '<div style="text-align:center; padding-bottom: 5px;"><a href="javascript:void(0);" onclick="' + this.instance_name + '.OpenPopup(\'CafeF_StockSymbolSlidePopup\');">Quản lý</a></div>';
        output += '</td>';
        output += '<td align="right" class="Left_Right"><img alt="" src="' + this.script_folder + 'Images/symbolslide_right.gif" /></td>';
        output += '</tr></table>';
        output += '<div style="display: none; text-align: right; padding-top: 5px;" class="menu_under_link KenhF_Content" id="CafeF_StockSymbolSlideTable_Hide"><a href="javascript:' + this.instance_name + '.ShowSlide()">Hiện danh sách CP đang theo dõi</a></div>';
        
        
        output += '<div id="CafeF_StockSymbolSlidePopup">';
        output += '<div class="PopupRow">';
        output += '<div id="CafeF_StockSymbolSlidePopupTitle">Gõ mã CK cần thêm vào danh sách</div>';
        output += '<div style="float: right;">';
        output += '<img alt="" style="cursor: hand; cursor: pointer;" onclick="' + this.instance_name + '.ClosePopup(\'CafeF_StockSymbolSlidePopup\')" border="0" src="' + this.script_folder + 'Images/close1.gif" />';
        output += '</div>';
        output += '</div>';
        output += '<div class="PopupRow">';
        output += '<div style="float: left"><input type="text" id="CafeF_StockSymbolSlideKeyword" style="width: 225px;" autocomplete="off" class="class_text_autocomplete ac_input" /></div>';
        output += '<div style="float: right;padding-top: 3px;padding-right: 2px;"><a href="javascript:void(0);" onclick="' + this.instance_name + '.AddSymbol();return false;"><img align="middle" src="' + this.script_folder + 'images/add.gif" /></a></div></div>';
        output += '<div class="PopupRow" id="CafeF_StockSymbolSlidePopupList"></div>';
        output += '<div style="float: right; margin-right: 10px;"><a style="color: Red; font-size: 11px; padding: 0px;" href="javascript:void(0)" onclick="' + this.instance_name + '.RemoveAllSymbols();">Xóa toàn bộ</a></div>';
        output += '</div>';
        
        document.getElementById(containerId).innerHTML = output;
        
        if (this.GetCookies(this.CookiesName))
        {
            if (this.GetCookies(this.CookiesName).indexOf('@') >= 0) // Neu la cookies cua version cu (truoc khi tach So lieu va Tin tuc)
            {
                this.GetSymbolListFromCoolies_OldVersion();
            }
            else // Cookies cua version moi
            {
                this.GetSymbolListFromCoolies();
            }
        }
        
        var state = this.GetCookies(this.StateCookiesName);
        if (state)
        {
            state = state.replace('@', '');
            if (state == '1')
            {
                this.ShowSlide();
            }
            else
            {
                this.HideSlide();
            }
        }
        else
        {
            this.ShowSlide();
        }
        
        if (state && this.display_list != '' && this.IsLogged == 0)
        {
            this.Log();
            this.IsLogged = 1;
        }
    }
    
    this.GetSymbolListFromCoolies = function()
    {
        this.symbol_list = this.GetCookies(this.CookiesName);
        if (this.symbol_list != '')
        {
            var listOfSymbol = this.symbol_list.split(';');
            
            for (var i = 0; i < listOfSymbol.length && i < this.MaxSymbolsDisplay; i++)
            {
                this.display_list += ';' + listOfSymbol[i];
            }
            if (this.display_list != '') this.display_list = this.display_list.substring(1);
        }
        else
        {
            this.display_list = '';
        }
    }
    
    this.GetSymbolListFromCoolies_OldVersion = function()
    {
        var temp = this.GetCookies(this.CookiesName);
        if (temp != '')
        {
            var arrSymbols = temp.split('@');
            
            for (var i = 0; i < arrSymbols.length; i++)
            {
                if (arrSymbols[i] != '')
                {
                    var arrData = arrSymbols[i].split('|');
                    this.symbol_list += ';' + arrData[1];
                }
            }
            if (this.symbol_list != '') this.symbol_list = this.symbol_list.substring(1);
            
            var listOfSymbol = this.symbol_list.split(';');
            
            for (var i = 0; i < listOfSymbol.length && i < this.MaxSymbolsDisplay; i++)
            {
                this.display_list += ';' + listOfSymbol[i];
            }
            if (this.display_list != '') this.display_list = this.display_list.substring(1);
        }
        else
        {
            this.display_list = '';
        }
    }
    
    this.HideSlide = function()
    {
        jQuery('#CafeF_StockSymbolSlideTable').hide();
        jQuery('#CafeF_StockSymbolSlideTable_Hide').show();
        this.SetCookie(this.StateCookiesName, 0, this.bm_st_state_time_exp);
    }
    
    this.ShowSlide = function()
    {
        jQuery('#CafeF_StockSymbolSlideTable').show();
        jQuery('#CafeF_StockSymbolSlideTable_Hide').hide();
        this.SetCookie(this.StateCookiesName, 1, this.bm_st_state_time_exp);
    }
    
    this.LoadSymbolData = function(isFirstRequest)
    {
        if (this.IsStockTrading() || (!this.IsStockTrading() && isFirstRequest))
        //if ((this.IsStockTrading() && CafeF_StockSymbolSlide_IsWindowFocus) || isFirstRequest)
        {
            var currentDate = new Date();
            this.CreateScriptObject(this.host + '/ProxyHandler.ashx?RequestName=StockSymbolSlide&CallBack=' + this.instance_name + '.OnLoaded&RequestType=json&sym=' + this.display_list + '&ut=' + currentDate.getDate() + currentDate.getTime(), true);
        }
        else
        {
            clearTimeout(this.timerId_Refresh);
            this.timerId_Refresh = setTimeout(this.instance_name + '.LoadSymbolData(false)', this.AUTO_REFRESH_TIME);
        }
    }
    
    this.Log = function()
    {
        var currentDate = new Date();
        var src = this.host + '/ProxyHandler.ashx?RequestName=StockSymbolSlide&CallBack=' + this.instance_name + '.OnLoaded&RequestType=json&log=1&n=Home&ut=' + currentDate.getDate() + currentDate.getTime();

        var objScript = document.createElement('script');

        objScript.setAttribute('type','text/javascript');
        objScript.setAttribute('src', src);
        
        var head = document.getElementsByTagName('head')[0];
        head.appendChild(objScript);
    }
    
    this.OnLoaded = function(data, methodName)
    {
        var json = eval(data);
        
        var output = '';
        
        var displayList = this.display_list.split(';');
        //alert(displayList);
        for (var i = 0; i < this.MaxSymbolsDisplay; i++)
        {
            var displayIndex = i;
            
            if (i < json.Symbols.length)
            {    
                
                for (displayIndex = 0; displayIndex < displayList.length; displayIndex++)
                {
                    if (displayList[displayIndex] == json.Symbols[i].Symbol) break;
                }
                //alert(json.Symbols[i].Symbol + '-' + displayIndex);
                //this.Fields = {'Price':0,'Change':1,'ChangePercent':2};
                var image = 'nochange.gif';
                var style = 'NoChange';
                var sign = '';
                if (json.Symbols[i].Datas[this.Fields.Change] > 0)
                {
                    image = 'up.gif';
                    style = 'Up';
                    sign = '+';
                }
                else if (json.Symbols[i].Datas[this.Fields.Change] < 0)
                {
                    image = 'down.gif';
                    style = 'Down';
                }
                
                output  = '<table width="100%" cellspacing="0" cellpadding="0" border="0"><tr><td>';
                output += '<div class="FloatLeft"><a href="' + CafeF_JSLibrary.GetCompanyInfoLink(json.Symbols[i].Symbol) + '">' + json.Symbols[i].Symbol + '</a><img alt="" src="' + this.script_folder + 'images/' + image + '" /></div>';
                output += '<div class="FloatRight"><img style="margin-top:1px;" src="' + this.script_folder + 'images/close.gif" alt="Xóa mã CK này khỏi danh sách" onclick="' + this.instance_name + '.RemoveSymbol(\'' + json.Symbols[i].Symbol + '\')" /></div>';
                output += '</td></tr><tr><td>';
                output += '<div class="FloatLeft Price">' + this.FormatNumber(json.Symbols[i].Datas[this.Fields.Price], true) + '</div>';
                output += '<div class="FloatRight"><span class="' + style + '">' + this.FormatNumber(json.Symbols[i].Datas[this.Fields.Change], true) + ' (' + this.FormatNumber(json.Symbols[i].Datas[this.Fields.ChangePercent], true) + '%)</span></div>';
                output += '</td></tr></table>';
            }
            else
            {
                output = '<div onclick="' + this.instance_name + '.OpenPopup(\'CafeF_StockSymbolSlidePopup\');" title="Click vào đây để chọn mã chứng khoán cần theo dõi" class="HiddenText">Chọn mã CK<br/>cần theo dõi</div>';
            }
            
            this.UpdateCell(displayIndex, output);
        }
        
        clearTimeout(this.timerId_Refresh);
        this.timerId_Refresh = setTimeout(this.instance_name + '.LoadSymbolData(false)', this.AUTO_REFRESH_TIME);
    }
    
    this.OpenPopup = function(id)
    {
        jQuery('#CafeF_StockSymbolSlidePopup').jqDrag('#CafeF_StockSymbolSlidePopupTitle');
        
        jQuery('#CafeF_StockSymbolSlideKeyword').autocomplete(oc, {
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
            },
            isAddSymbolToFavorite: true,
            CafeF_StockSymbolSlideObject: this
        });
        
        this.RefreshListOfSymbolInConfigWindow();
        
        jQuery('#' + id).show();
        var txt = document.getElementById('CafeF_StockSymbolSlideKeyword');
        txt.value = '';
        txt.focus();
    }
    
    this.ClosePopup = function(id)
    {
        var txt = document.getElementById('CafeF_StockSymbolSlideKeyword');
        txt.value = '';
        jQuery('#' + id).hide();
    }
    
    this.MoveLeft = function()
    {
        if (this.symbol_list.indexOf(this.display_list) == 0 || this.displayList == '') return;
        
        var listOfSymbol = this.symbol_list.split(';');
        var displayList = this.display_list.split(';');
        
        var startIndex = 0;
        
        for (startIndex = 0; startIndex < listOfSymbol.length; startIndex++)
        {
            if (listOfSymbol[startIndex] == displayList[0]) break;
        }
        
        this.display_list = '';
        for (var  i = startIndex - 1, j = 0; i < listOfSymbol.length && j < this.MaxSymbolsDisplay; i++)
        {
            this.display_list += ';' + listOfSymbol[i];
            j++;
        }
        if (this.display_list != '') this.display_list = this.display_list.substring(1);
        //alert(this.display_list);
        this.LoadSymbolData(true);
    }
    
    this.MoveRight = function()
    {
        if (this.symbol_list.indexOf(this.display_list) + this.display_list.length == this.symbol_list.length || this.displayList == '') return;
        
        var listOfSymbol = this.symbol_list.split(';');
        var displayList = this.display_list.split(';');
        
        var startIndex = 0;
        
        for (startIndex = 0; startIndex < listOfSymbol.length; startIndex++)
        {
            if (listOfSymbol[startIndex] == displayList[0]) break;
        }
        
        this.display_list = '';
        for (var  i = startIndex + 1, j = 0; i < listOfSymbol.length && j < this.MaxSymbolsDisplay; i++)
        {
            this.display_list += ';' + listOfSymbol[i];
            j++;
        }
        if (this.display_list != '') this.display_list = this.display_list.substring(1);
        //alert(this.display_list);
        this.LoadSymbolData(true);
    }
    
    this.AddSymbol = function()
    {
        //
    }
    
    this.AddSymbolToFavorite = function(symbol)
    {
        var listOfSymbol;
        
        var txt = document.getElementById('CafeF_StockSymbolSlideKeyword');
        
        if (this.symbol_list != '')
        {
            listOfSymbol = this.symbol_list.split(';');
            
            for (var i = 0; i < listOfSymbol.length; i++)
            {
                if (listOfSymbol[i] == symbol)
                {
                    txt.value = '';
                    return;
                }
            }
            
            if (listOfSymbol.length >= this.MaxSymbolsInQueue)
            {
                alert('Đã đủ 10 mã CK trong danh sách theo dõi');
                txt.value = '';
                return;
            }
            
            this.symbol_list += ';' + symbol;
        }
        else
        {
            this.symbol_list = symbol;
        }
        
        listOfSymbol = this.symbol_list.split(';');
        if (listOfSymbol.length <= this.MaxSymbolsDisplay)
        {
            this.display_list = this.symbol_list;
        }
        else
        {
            this.display_list = '';
            for (var i = listOfSymbol.length - this.MaxSymbolsDisplay; i < listOfSymbol.length; i++)
            {
                this.display_list += ';' + listOfSymbol[i];
            }
            if (this.display_list != '') this.display_list = this.display_list.substring(1);
        }
        
        this.SetCookie(this.CookiesName, this.symbol_list, this.bm_st_time_exp);
        
        this.LoadSymbolData(true);
        
        this.RefreshListOfSymbolInConfigWindow();
        txt.value = '';
        
        if (this.IsLogged == 0)
        {
            this.Log();
            this.IsLogged = 1;
        }
    }
    
    this.RemoveAllSymbols = function()
    {
        this.display_list = '';
        this.symbol_list = '';
        
        this.SetCookie(this.CookiesName, this.symbol_list, this.bm_st_time_exp);
        
        this.LoadSymbolData(true);
        
        this.RefreshListOfSymbolInConfigWindow();
        var txt = document.getElementById('CafeF_StockSymbolSlideKeyword');
        txt.value = '';
        txt.focus();
    }
    
    this.RemoveSymbol = function(symbol)
    {
        var temp = ';' + this.display_list + ';';
        var listOfSymbol = this.symbol_list.split(';');
        this.symbol_list = '';
        var endDisplayIndex = -1;
        
        this.display_list = ';' + this.display_list + ';';
        this.display_list = this.display_list.replace(';' + symbol + ';', ';');
        this.display_list = this.display_list.substring(1, this.display_list.length - 1);
        
        var lastSymbol = (this.display_list == '' ? '' : this.display_list.substring(this.display_list.lastIndexOf(';') + 1));
        
        for (var i = 0, j = -1; i < listOfSymbol.length; i++)
        {
            if (listOfSymbol[i] != symbol)
            {
                j++;
                if (lastSymbol != '')
                {
                    if (endDisplayIndex == -1 && listOfSymbol[i] == lastSymbol)
                    {
                        endDisplayIndex = j;
                    }
                }
                this.symbol_list += ';' + listOfSymbol[i];
            }
        }
        if (this.symbol_list != '') this.symbol_list = this.symbol_list.substring(1);
        listOfSymbol = this.symbol_list.split(';');
        
        if (endDisplayIndex < this.MaxSymbolsDisplay && listOfSymbol > this.MaxSymbolsDisplay) endDisplayIndex++;
        
        // Item bi xoa nam trong danh sach dang hien thi
        if (temp.indexOf(';' + symbol + ';') >= 0)
        {
            if (this.symbol_list.indexOf(this.display_list) == 0 && listOfSymbol.length > this.MaxSymbolsDisplay) endDisplayIndex++;
            
            this.display_list = '';
            for (var i = 0; i < this.MaxSymbolsDisplay && endDisplayIndex >= 0; i++)
            {
                this.display_list = ';' + listOfSymbol[endDisplayIndex] + this.display_list;
                endDisplayIndex--;
            }
        }
        if (this.display_list != '') this.display_list = this.display_list.substring(1);
        
        this.SetCookie(this.CookiesName, this.symbol_list, this.bm_st_time_exp);
        
        this.LoadSymbolData(true);
        
        this.RefreshListOfSymbolInConfigWindow();
        var txt = document.getElementById('CafeF_StockSymbolSlideKeyword');
        txt.value = '';
        txt.focus();
    }
    
    this.RefreshListOfSymbolInConfigWindow = function()
    {
        var strList = '';
        
        if (this.symbol_list != '')
        {
            strList = '<table align="center" width="90%" border="0" cellpadding="3" cellspacing="0">';
            
            var listOfSymbol = this.symbol_list.split(';');
                
            for (var i = 0; i < listOfSymbol.length; i++)
            {
                strList += '<tr>';
                strList += '<td style="border:none;"><span style="color: #aaaaaa">' + (i + 1) + '.</span> <strong>' + listOfSymbol[i] + '</strong></td>';
                strList += '<td style="border:none;"></td>';
                strList += '<td style="border:none;" align="right"><img alt="Xóa mã CK khỏi danh sách" onclick="' + this.instance_name + '.RemoveSymbol(\'' + listOfSymbol[i] + '\')" src="' + this.script_folder + 'images/delete.gif" /></td>';
                strList += '</tr>';
            }
            
            strList += "</table>";
        }
        jQuery('#CafeF_StockSymbolSlidePopupList').html(strList);
    }
    
    this.FormatNumber = function(value, displayZero)
    {
        if (value == '') return (displayZero ? '0' : '');
        try
        {
            var number = parseFloat(value);
            value = CafeF_JSLibrary.FormatNumber(number, 2, '.', ',');
            return (value);
        }
        catch (err)
        {
            return (displayZero ? '0' : '');
        }
    }
    
    this.UpdateCell = function(index, value)
    {
        if (this.OldCellDatas[index] != value)
        {
            var cell = document.getElementById('CafeF_StockSymbolSlideTable_Cell' + index);
            if (cell)
            {
                cell.innerHTML = value;
            }
            this.OldCellDatas[index] = value
        }
    }
    
    this.CreateScriptObject = function(src, useScriptObject)
    {
        if (useScriptObject && this.script_object)
	    {
		    this.RemoveScriptObject();
	    }
    	
	    this.script_object = document.createElement('script');

        this.script_object.setAttribute('type','text/javascript');
        this.script_object.setAttribute('src', src);
        
        var head = document.getElementsByTagName('head')[0];
        head.appendChild(this.script_object);
    }
    
    this.AppendScriptObject = function(script)
    {
	    var obj = document.createElement('script');

        obj.setAttribute('type','text/javascript');
        
        obj.appendChild(document.createTextNode(script));
        
        var head = document.getElementsByTagName('head')[0];
        head.appendChild(obj);
    }

    this.RemoveScriptObject = function()
    {
	    this.script_object.parentNode.removeChild(this.script_object) ;
	    this.script_object = null ;
    }
    
    this.SetCookie = function(c_name, value, time_expire)
    {
        var exdate=new Date();
        
        exdate.setDate(exdate.getDate() + time_expire);
        
        document.cookie = c_name + '=' + escape(value) + ((time_expire == null) ? '' : ';expires=' + exdate.toGMTString()) + ';path=/';
    }
    this.GetCookies = function(c_name)
    {
        if (document.cookie.length > 0)
        {
            var c_start = document.cookie.indexOf(c_name + "=");
            if (c_start != -1)
            { 
                c_start = c_start + c_name.length + 1; 
                var c_end = document.cookie.indexOf(";", c_start);
                if (c_end == -1) c_end = document.cookie.length;
                return unescape(document.cookie.substring(c_start, c_end));
            } 
        }
        return '';
    }
}

//document.write('<div id="CafeF_StockSymbolSlideTable_Container"></div>');
var container = document.getElementById('CafeF_StockSymbolSlideTable_Container');
if (container)
{
    var danh_sach_ma_chung_khoan_theo_doi = new CafeF_StockSymbolSlide('danh_sach_ma_chung_khoan_theo_doi');
    danh_sach_ma_chung_khoan_theo_doi.InitScript('CafeF_StockSymbolSlideTable_Container');
    if (container.delay)
    {
        setTimeout('danh_sach_ma_chung_khoan_theo_doi.LoadSymbolData(true)', parseInt(container.delay));
    }
    else
    {
        danh_sach_ma_chung_khoan_theo_doi.LoadSymbolData(true);
    }
}

var CafeF_StockSymbolSlide_IsWindowFocus = true;

window.onfocus = function()
{
    CafeF_StockSymbolSlide_IsWindowFocus = true;
}
window.onblur = function()
{
    CafeF_StockSymbolSlide_IsWindowFocus = false;
}