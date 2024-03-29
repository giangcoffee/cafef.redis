function CafeF_TopStockSymbol(instanceName)
{
    this.host = 'http://solieu.cafef.vn';
    //this.host = 'http://localhost:8081';
    this.script_folder = 'http://solieu.cafef.vn/www/cafef/';
    this.script_object = null;
    this.instance_name = instanceName;
    this.Fields = {'Quantity':0,'Price':1,'Change':2,'ChangePercent':3};
    this.LoadingImage = '<div align="center"><img src="http://solieu.cafef.vn/www/cafef/images/loading.gif" /></div>';

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
        this.CreateCssLink(this.script_folder + 'css/cafef.css');
    
        var output = '';
        
        output += '<table class="CafeF_TopCompany" align="center" style="border: solid 1px #dadada; font-family: Arial;" cellpadding="0" cellspacing="0" width="300px">';
        output += '<tr><td style="text-align: left; font-size: 16px; padding: 5px 0px 5px 5px;">Top 10 cổ phiếu</td>';
        output += '<td align="right" nowrap="nowrap" width="155px" style="padding-right: 0px;">';
        output += '<div style="height: 19px; width: 44px; padding-top: 3px; text-align: center; overflow: hidden; float: right; background-image: url(' + this.script_folder + '/images/btn.gif); background-repeat: no-repeat; margin-right: 5px;"><a id="CafeF_TopCP_HaSTC" class="ButtonInActived" href="javascript:void(0)" onclick="' + this.instance_name + '.LoadData(2)">HaSTC</a></div>';
        output += '<div style="height: 19px; width: 44px; padding-top: 3px; text-align: center; overflow: hidden; float: right; background-image: url(' + this.script_folder + '/images/btn.gif); background-repeat: no-repeat; margin-right: 5px;"><a id="CafeF_TopCP_HoSE" class="ButtonInActived" href="javascript:void(0)" onclick="' + this.instance_name + '.LoadData(1)">HoSE</a></div>';
        output += '<div style="height: 19px; width: 44px; padding-top: 3px; text-align: center; overflow: hidden; float: right; background-image: url(' + this.script_folder + '/images/btn.gif); background-repeat: no-repeat; margin-right: 5px;"><a id="CafeF_TopCP_ViewAll" class="ButtonActived" href="javascript:void(0)" onclick="' + this.instance_name + '.LoadData(0)">Tất cả</a></div>';
        output += '</td></tr>';
        output += '<tr><td colspan="2" style="text-align: left;">';
        output += '<table cellpadding="0" cellspacing="0" border="0">';
        output += '<tr>';
        output += '<td style="width:3px;">&nbsp;</td>';
        output += '<td class="Button" id="CafeF_BoxChungKhoan_TabButton_TangGia" onclick="' + this.instance_name + '.SelectTab(\'TangGia\');" style="width: 85px; background-image:url(' + this.script_folder + 'images/top_co_phieu/btn_tanggia_on.gif); font-weight: bold;">Tăng giá</td>';
        output += '<td style="width:3px;">&nbsp;</td>';
        output += '<td class="Button" id="CafeF_BoxChungKhoan_TabButton_GiamGia" onclick="' + this.instance_name + '.SelectTab(\'GiamGia\');" style="width: 70px; background-image:url(' + this.script_folder + 'images/top_co_phieu/btn_giamgia_off.gif);">Giảm giá</td>';
        output += '<td style="width:3px;">&nbsp;</td>';
        output += '<td class="Button" id="CafeF_BoxChungKhoan_TabButton_KhoiLuong" onclick="' + this.instance_name + '.SelectTab(\'KhoiLuong\');" style="width: 80px; background-image:url(' + this.script_folder + 'images/top_co_phieu/btn_KLGD_off.gif);">KLGD (*)</td>';
        output += '</tr>';
        output += '</table>';
        output += '</td></tr>';
        output += '<tr><td colspan="2" style="text-align: left;border-bottom: solid 1px #dadada;border-top: solid 1px #dadada;">';
        output += '<div id="CafeF_BoxChungKhoan_Tab_TangGia"></div>';
        output += '<div style="display: none; " id="CafeF_BoxChungKhoan_Tab_GiamGia"></div>';
        output += '<div style="display: none; " id="CafeF_BoxChungKhoan_Tab_KhoiLuong"></div>';
        output += '</td></tr>';
        output += '<tr><td colspan="2" style="text-align: left; font-size: 11px; color: #999; padding: 5px 0px 5px 10px;"">';
        output += '(*) Khối lượng giao dịch nhiều nhất<br />';
        output += 'Đơn vị khối lượng: 10CP';
        output += '</td></tr>';
        output += '</table>';
        
        document.getElementById('CafeF_TopStockSymbol').innerHTML = output;
    }
    
    this.LoadData = function(tradeCenter)
    {
        document.getElementById('CafeF_BoxChungKhoan_Tab_TangGia').innerHTML = this.LoadingImage;
        document.getElementById('CafeF_BoxChungKhoan_Tab_GiamGia').innerHTML = this.LoadingImage;
        document.getElementById('CafeF_BoxChungKhoan_Tab_KhoiLuong').innerHTML = this.LoadingImage;
        
        this.CreateScriptObject(this.host + '/ProxyHandler.ashx?RequestName=TopStockSymbol&CallBack=' + this.instance_name + '.OnLoaded&RequestType=json&TradeCenter=' + tradeCenter);
        
        var Butotn_ViewAll = document.getElementById('CafeF_TopCP_ViewAll'); Butotn_ViewAll.className = 'ButtonInActived';
        var Butotn_HoSE = document.getElementById('CafeF_TopCP_HoSE'); Butotn_HoSE.className = 'ButtonInActived';
        var Butotn_HaSTC = document.getElementById('CafeF_TopCP_HaSTC'); Butotn_HaSTC.className = 'ButtonInActived';
        
        if (tradeCenter == 1)
        {
            Butotn_HoSE.className = 'ButtonActived';
        }
        else if (tradeCenter == 2)
        {
            Butotn_HaSTC.className = 'ButtonActived';
        }
        else
        {
            Butotn_ViewAll.className = 'ButtonActived';
        }
    }

    this.OnLoaded = function(data, methodName)
    {
        var json = eval(data);

        var output = '';
        
        output  = '<table width="100%" cellpadding="0" cellspacing="0">';
        output += '<tr><th style="text-align: left;">Mã CK</th><th style="width: 70px;">KL</th><th style="width: 40px;">Giá</th><th style="width: 100px;">+/-</th></tr>';
        var isAlternation = false;
        for (var i = 0; i < json.TopUp.Symbols.length; i++)
        {
            output += '<tr' + (isAlternation ? ' style="background-color:#f5f5f5"' : '') + '>';
            output += '<td style="text-align: left;"><a href="' + CafeF_JSLibrary.GetCompanyInfoLink(json.TopUp.Symbols[i].Symbol) + '">' + json.TopUp.Symbols[i].Symbol + '</a></td>';
            output += '<td>' + this.FormatNumber(json.TopUp.Symbols[i].Datas[this.Fields.Quantity]) + '</td>';
            output += '<td>' + this.FormatNumber(json.TopUp.Symbols[i].Datas[this.Fields.Price]) + '</td>';
            output += '<td>' + this.FormatChangeValue(json.TopUp.Symbols[i].Datas[this.Fields.Change], json.TopUp.Symbols[i].Datas[this.Fields.ChangePercent]) + '</td>';
            output += '</tr>';
            isAlternation = !isAlternation;
        }
        output += '</table>';
        
        document.getElementById('CafeF_BoxChungKhoan_Tab_TangGia').innerHTML = output;
        // ------------------------------
        output  = '<table width="100%" cellpadding="0" cellspacing="0">';
        output += '<tr><th style="text-align: left;">Mã CK</th><th style="width: 70px;">KL</th><th style="width: 40px;">Giá</th><th style="width: 100px;">+/-</th></tr>';
        isAlternation = false;
        for (var i = 0; i < json.TopDown.Symbols.length; i++)
        {
            output += '<tr' + (isAlternation ? ' style="background-color:#f5f5f5"' : '') + '>';
            output += '<td style="text-align: left;"><a href="' + CafeF_JSLibrary.GetCompanyInfoLink(json.TopDown.Symbols[i].Symbol) + '">' + json.TopDown.Symbols[i].Symbol + '</a></td>';
            output += '<td>' + this.FormatNumber(json.TopDown.Symbols[i].Datas[this.Fields.Quantity]) + '</td>';
            output += '<td>' + this.FormatNumber(json.TopDown.Symbols[i].Datas[this.Fields.Price]) + '</td>';
            output += '<td>' + this.FormatChangeValue(json.TopDown.Symbols[i].Datas[this.Fields.Change], json.TopDown.Symbols[i].Datas[this.Fields.ChangePercent]) + '</td>';
            output += '</tr>';
            isAlternation = !isAlternation;
        }
        output += '</table>';
        
        document.getElementById('CafeF_BoxChungKhoan_Tab_GiamGia').innerHTML = output;
        // ------------------------------
        output  = '<table width="100%" cellpadding="0" cellspacing="0">';
        output += '<tr><th style="text-align: left;">Mã CK</th><th style="width: 70px;">KL</th><th style="width: 40px;">Giá</th><th style="width: 100px;">+/-</th></tr>';
        isAlternation = false;
        for (var i = 0; i < json.TopQuantity.Symbols.length; i++)
        {
            output += '<tr' + (isAlternation ? ' style="background-color:#f5f5f5"' : '') + '>';
            output += '<td style="text-align: left;"><a href="' + CafeF_JSLibrary.GetCompanyInfoLink(json.TopQuantity.Symbols[i].Symbol) + '">' + json.TopQuantity.Symbols[i].Symbol + '</a></td>';
            output += '<td>' + this.FormatNumber(json.TopQuantity.Symbols[i].Datas[this.Fields.Quantity]) + '</td>';
            output += '<td>' + this.FormatNumber(json.TopQuantity.Symbols[i].Datas[this.Fields.Price]) + '</td>';
            output += '<td>' + this.FormatChangeValue(json.TopQuantity.Symbols[i].Datas[this.Fields.Change], json.TopQuantity.Symbols[i].Datas[this.Fields.ChangePercent]) + '</td>';
            output += '</tr>';
            isAlternation = !isAlternation;
        }
        output += '</table>';
        
        document.getElementById('CafeF_BoxChungKhoan_Tab_KhoiLuong').innerHTML = output;
    }
    
    this.SelectTab = function(id)
    {
        var tabbutton_Tanggia = document.getElementById('CafeF_BoxChungKhoan_TabButton_TangGia'); tabbutton_Tanggia.style.backgroundImage = 'url(' + this.script_folder + 'images/top_co_phieu/btn_tanggia_of.gif)'; tabbutton_Tanggia.style.fontWeight = 'normal';
        var tabbutton_Giamgia = document.getElementById('CafeF_BoxChungKhoan_TabButton_GiamGia'); tabbutton_Giamgia.style.backgroundImage = 'url(' + this.script_folder + 'images/top_co_phieu/btn_giamgia_off.gif)'; tabbutton_Giamgia.style.fontWeight = 'normal';
        var tabbutton_Khoiluong = document.getElementById('CafeF_BoxChungKhoan_TabButton_KhoiLuong'); tabbutton_Khoiluong.style.backgroundImage = 'url(' + this.script_folder + 'images/top_co_phieu/btn_KLGD_off.gif)'; tabbutton_Khoiluong.style.fontWeight = 'normal';
        
        var tab_Tanggia = document.getElementById('CafeF_BoxChungKhoan_Tab_TangGia'); tab_Tanggia.style.display = 'none';
        var tab_Giamgia = document.getElementById('CafeF_BoxChungKhoan_Tab_GiamGia'); tab_Giamgia.style.display = 'none';
        var tab_Khoiluong = document.getElementById('CafeF_BoxChungKhoan_Tab_KhoiLuong'); tab_Khoiluong.style.display = 'none';
        
        if (id == 'GiamGia')
        {
            tabbutton_Giamgia.style.backgroundImage = 'url(' + this.script_folder + 'images/top_co_phieu/btn_giamgia_on.gif)'; tabbutton_Giamgia.style.fontWeight = 'bold';
            tab_Giamgia.style.display = 'block';
        }
        else if (id == 'KhoiLuong')
        {
            tabbutton_Khoiluong.style.backgroundImage = 'url(' + this.script_folder + 'images/top_co_phieu/btn_KLGD_on.gif)'; tabbutton_Khoiluong.style.fontWeight = 'bold';
            tab_Khoiluong.style.display = 'block';
        }
        else
        {
            tabbutton_Tanggia.style.backgroundImage = 'url(' + this.script_folder + 'images/top_co_phieu/btn_tanggia_on.gif)'; tabbutton_Tanggia.style.fontWeight = 'bold';
            tab_Tanggia.style.display = 'block';
        }
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
    
    this.FormatChangeValue = function(value, percent)
    {
        var output = '';
        var temp = value + '';
        if (temp.indexOf('text_color_green') >= 0 || temp.indexOf('text_color_red') >= 0 || temp.indexOf('text_color_yellow') >= 0)
        {
            output = temp;
        }
        else
        {
            if (value > 0)
            {
                output = '<span style="color: #008000">+' + this.FormatNumber(value) + (percent ? '(+' + this.FormatNumber(percent) + '%)' : '') + '</span>';
            }
            else if (value < 0)
            {
                output = '<span style="color: #cc0000">' + this.FormatNumber(value) + (percent ? '(' + this.FormatNumber(percent) + '%)' : '') + '</span>';
            }
            else
            {
                output = '<span style="color: #ff9900">0' + (percent ? '(0%)' : '') + '</span>';
            }
        }
        //var temp=document.getElementById('CafeF_TopStockSymbol_TestOutput');
        //temp.innerHTML += '#' + output;
        return output;
    }

    this.FormatNumber = function(value, displayZero)
    {
        if (value == '') return (displayZero ? '0' : '');
        try
        {
            var temp = value + '';
            temp = temp.replace(',', '');
            var number = parseFloat(temp);
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

//document.write('<div id="CafeF_TopStockSymbol_TestOutput"></div>');
var container = document.getElementById('CafeF_TopStockSymbol');
if (container)
{
    var cafef_top_stock_symbol = new CafeF_TopStockSymbol('cafef_top_stock_symbol');
    cafef_top_stock_symbol.InitScript();

    if (container.delay)
    {
        setTimeout('cafef_top_stock_symbol.LoadData(0)', parseInt(container.delay));
    }
    else
    {
        cafef_top_stock_symbol.LoadData(0);
    }
}