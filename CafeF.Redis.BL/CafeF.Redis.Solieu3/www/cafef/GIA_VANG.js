function CafeF_GiaVang(instanceName)
{
    this.host = 'http://solieu.cafef.vn';
    //this.host = 'http://localhost:8084';
    this.script_folder = this.host + '/www/cafef/';
    this.script_object = null;
    this.instance_name = instanceName;
    
//    this.CreateCssLink = function(href)
//    {
//        var css = window.parent.document.createElement('link');
//        css.type = 'text/css';
//        css.rel = 'stylesheet';
//        css.href = href;
//        var head = window.parent.document.getElementsByTagName('head')[0];
//        head.appendChild(css);
//    }
    
    this.InitScript = function()
    {
        //this.CreateCssLink(this.script_folder + 'css/cafef.css');
    
        var output = '';

        output += '<div class="cf_WCBox" style="width:240px;"><div class="cf_BoxHeader"><div></div></div><div class="cf_BoxContent"><div class="cf_Pad5TB9LR">';
        output += '<div class="cf_WireBox"><div class="cf_BoxHeader"><div></div></div><div class="cf_BoxContent">';
        output += '<div style="width: 100%"><div class="CafeF_Padding_Box"><div style="padding-left: 5px;">';
        output += '<span class="Box_Title_Home">BẢNG GIÁ VÀNG</span></div></div><div style="padding-left: 5px;"><div id="CafeF_GiaVang_NgayCapnhat" class="DateTime"></div></div>';
        output += '<div class="NormalText_ita" style="text-align: right">Đơn vị: triệu đồng/lượng</div>';
        output += '<div class="cf_Box_BorderTop" id="CafeF_GiaVang_Dulieu"></div>';
        output += '<div style="border-top: solid 1px #dadada; padding: 2px 5px 2px 5px"><span class="NormalText_Bold">(<span style="color: Red;">*</span>)</span> <span class="NormalText_ita">';
        output += ': USD/OZ. </span><span class="NormalText_Bold">(<span style="color: Red;">**</span>)</span><span class="NormalText_ita">:&nbsp; Tỷ giá';
        output += '<span id="CafeF_GiaVang_Tygia"></span>&nbsp;VNĐ</span></div><div></div></div></div><div class="cf_BoxFooter"><div></div></div></div></div></div>';
        output += '<div class="cf_BoxFooter"><div></div></div></div>'
        window.parent.document.getElementById('CafeF_GiaVang').innerHTML = output;
    }
    
    this.LoadData = function()
    {
        this.CreateScriptObject(this.host + '/ProxyHandler.ashx?RequestName=GoldMarket&CallBack=' + this.instance_name + '.OnLoaded&RequestType=json');
    }

    this.OnLoaded = function(data, methodName)
    {
        var json = eval(data);
        
        window.parent.document.getElementById('CafeF_GiaVang_NgayCapnhat').innerHTML = 'Cập nhật ngày ' + (json.LastUpdate);
        window.parent.document.getElementById('CafeF_GiaVang_Tygia').innerHTML = "&nbsp;"+json.ExchangeRate +"&nbsp;";
        
        var output = '';
        output += '<div style="overflow: hidden; background-color: #f2f2f2; height: 20px; padding-top: 4px">';
        output += '<div style="float: left; width: 100px; text-align: left; padding-left: 10px" class="giavang_subtitle">V&#224;ng</div>';
        output += '<div style="float: left; width: 55px" class="giavang_subtitle">Mua v&#224;o</div>';
        output += '<div style="float: left; width: 55px" class="giavang_subtitle">B&#225;n ra</div>';
        output += '</div>';
        var isAlternation = false;
        var excludeSymbol = '#NJC#';
        for (var i = 0; i < json.GoldMarketDataItems.length; i++)
        {
            if (excludeSymbol.indexOf('#' + json.GoldMarketDataItems[i].Name + '#') >= 0) continue;
            
            if (isAlternation)
            {
                output += '<div style="overflow: hidden; height: 20px; padding-top: 5px">';
                output += '<div class="giavang_tenvang" style="width: 95px; padding-left: 10px;" >' + json.GoldMarketDataItems[i].Name + '</div>';
                output += '<div class="giavang_text" style="width:55px;text-align:right">' + this.fixNumber(json.GoldMarketDataItems[i].Buy) + '&nbsp;</div>';
                output += '<div class="giavang_text" style="width:60px;text-align:right">' + this.fixNumber(json.GoldMarketDataItems[i].Sell) + '&nbsp;</div>';
                output += '</div>';
            }
            else
            {
                output += '<div style="overflow: hidden; background-color: #fff9e1; height: 20px; padding-top: 5px">';
                output += '<div class="giavang_tenvang" style="width: 95px; padding-left: 10px;">' + json.GoldMarketDataItems[i].Name + '</div>';
                output += '<div class="giavang_text" style="width:55px;text-align:right">' + this.fixNumber(json.GoldMarketDataItems[i].Buy) + '&nbsp;</div>';
                output += '<div class="giavang_text" style="width:60px;text-align:right">' + this.fixNumber(json.GoldMarketDataItems[i].Sell) + '&nbsp;</div>';
                output += '</div>';
            }
            isAlternation = !isAlternation;
        }
        window.parent.document.getElementById('CafeF_GiaVang_Dulieu').innerHTML = output;
    }
    
    this.fixNumber = function(str)
    {
        var temp = str.replace(/^\s+|\s+$/g, ''); // trim
        
        temp = temp.substring(temp.indexOf('.') + 1);
        
        if (temp.length == 3)
        {
            str = str.replace(',', '#');
            str = str.replace('.', ',');
            str = str.replace('#', '.');
        }
        
        return str;
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
        
        var head = document.getElementsByTagName('head')[0];
        head.appendChild(this.script_object);
    }

    this.RemoveScriptObject = function()
    {
	    this.script_object.parentNode.removeChild(this.script_object) ;
	    this.script_object = null ;
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
    this.FormatNumber2 = function(value, displayZero,decimalSeparator)
        {
            if (value == '') return (displayZero ? '0' : '');
            try
            {
                var number = parseFloat(value);
                value = this.FormatNumber1(number, decimalSeparator, '.', ',');
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
    	temp = temp.replace('-,', '-');
        return temp;
    }
}

//window.parent.document.write('<div id="CafeF_GiaVang"></div>');
var container = window.parent.document.getElementById('CafeF_GiaVang');
if (container)
{
    var cafef_gia_vang = new CafeF_GiaVang('cafef_gia_vang');
    cafef_gia_vang.InitScript();
    if (container.delay)
    {
        setTimeout('cafef_gia_vang.LoadData()', parseInt(container.delay));
    }
    else
    {
        cafef_gia_vang.LoadData();
    }
}