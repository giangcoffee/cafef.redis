function CafeF_StockMarketOverview(instanceName)
{
    this.host = 'http://solieu.cafef.vn';
    //this.host = 'http://localhost:8084';
    this.script_folder = 'http://solieu.cafef.vn/www/cafef/';
    this.script_object = null;
    this.instance_name = instanceName;
    
    this.CreateCssLink = function(href)
    {
        var css = window.parent.document.createElement('link');
        css.type = 'text/css';
        css.rel = 'stylesheet';
        css.href = href;
        var head = window.parent.document.getElementsByTagName('head')[0];
        head.appendChild(css);
    }

    this.InitScript = function()
    {
        this.CreateCssLink(this.script_folder + 'css/cafef.css');
    
        var output = '';
        
        output += '<table width="100%" cellpadding="0" cellspacing="0" class="CafeF_RoundCornerBox">';
        output += '<tr class="Top">';
        output += '<td class="Left">';
        output += '<img alt="" width="4" height="4" src="' + this.script_folder + 'images/conner_top_left.gif" /></td>';
        output += '<td></td>';
        output += '<td class="Right">';
        output += '<img alt="" width="4" height="4" id="CafeF_RoundCornerBox" src="' + this.script_folder + 'images/conner_top_right.gif" /></td>';
        output += '</tr>';
        output += '<tr>';
        output += '<td>';
        output += '</td>';
        output += '<td class="Middle">';
        output += '<table class="Cafef_RoundCornerChildBox" cellpadding="0" cellspacing="0">';
        output += '<tr class="Top">';
        output += '<td class="Left">';
        output += '<img alt="" width="9" height="20" src="' + this.script_folder + 'images/cornuu_top_left.gif" /></td>';
        output += '<td class="Title" id="CafeF_MarketOverview_UpdateTime"></td>';
        output += '<td class="Right">';
        output += '<img alt="" width="9" height="20" src="' + this.script_folder + 'images/cornuu_top_right.gif" /></td>';
        output += '</tr>';
        output += '<tr class="Middle">';
        output += '<td class="Left"><img alt="" width="8" src="' + this.script_folder + 'images/spacer.gif" /></td>';
        output += '<td class="Center">';
        output += '<table cellspacing="0" cellpadding="2" style="margin-top: 3px;">';
        output += '<tr>';
        output += '<td width="160px" id="CafeF_MarketOverview_VNIndex_img" class="Title"></td>';
        output += '<td id="CafeF_MarketOverview_HaSTCIndex_img" class="Title"></td>';
        output += '</tr>';
        output += '<tr>';
        output += '<td id="CafeF_MarketOverview_VNIndex" class="Price"></td>';
        output += '<td id="CafeF_MarketOverview_HaSTCIndex" class="Price"></td>';
        output += '</tr>';
        output += '<tr>';
        output += '<td>KLGD: <span id="CafeF_MarketOverview_HoTotalVol" class="Price"></span> cp</td>';
        output += '<td>KLGD: <span id="CafeF_MarketOverview_HaTotalVol" class="Price"></span> cp</td>';
        output += '</tr>';
        output += '<tr>';
        output += '<td>GTGD: <span id="CafeF_MarketOverview_HoTotalValue" class="Price"></span> tỷ VNĐ</td>';
        output += '<td>GTGD: <span id="CafeF_MarketOverview_HaTotalValue" class="Price"></span> tỷ VNĐ</td>';
        output += '</tr>';
        output += '<tr>';
        output += '<td id="CafeF_MarketOverview_VNIndex_Chart" style="padding-left: 5px;"></td>';
        output += '<td id="CafeF_MarketOverview_HaSTCIndex_Chart"></td>';
        output += '</tr>';
        output += '<tr>';
        output += '<td colspan="2" style="font-size: 11px; color: #999999;" id="CafeF_MarketOverview_TradeState"></td>';
        output += '</tr>';
        output += '</table>';
        output += '</td>';
        output += '<td class="Right"><img alt="" width="8" src="' + this.script_folder + 'images/spacer.gif" /></td>';
        output += '</tr>';
        output += '<tr class="Bottom">';
        output += '<td class="Left">';
        output += '<img alt="" width="9" height="5" src="' + this.script_folder + 'images/cornuu_bottom_left.gif" /></td>';
        output += '<td class="Center"><img alt="" height="4" src="' + this.script_folder + 'images/spacer.gif" /></td>';
        output += '<td class="Right">';
        output += '<img alt="" width="9" height="5" src="' + this.script_folder + 'images/cornuu_bottom_right.gif" /></td>';
        output += '</tr>';
        output += '</table>';
        output += '</td>';
        output += '<td>';
        output += '</td>';
        output += '</tr>';
        output += '<tr class="Bottom">';
        output += '<td class="Left">';
        output += '<img alt="" width="4" height="4" src="' + this.script_folder + 'images/conner_bottom_left.gif" /></td>';
        output += '<td><img alt="" height="4" src="' + this.script_folder + 'images/spacer.gif" /></td>';
        output += '<td class="Right">';
        output += '<img alt="" width="4" height="4" src="' + this.script_folder + 'images/conner_bottom_right.gif" /></td>';
        output += '</tr>';
        output += '</table>';
        
        window.parent.document.getElementById('CafeF_StockMarketOverview').innerHTML = output;
    }
    
    this.LoadData = function()
    {
        this.CreateScriptObject(this.host + '/ProxyHandler.ashx?RequestName=MarketSymmary&CallBack=' + this.instance_name + '.OnLoaded&RequestType=json');
    }

    this.OnLoaded = function(data, methodName)
    {
        var json = eval(data);
        
        window.parent.document.getElementById('CafeF_MarketOverview_VNIndex_Chart').innerHTML = this.GetChartImage('HoSE', json.VNIndex_Change);
        window.parent.document.getElementById('CafeF_MarketOverview_HaSTCIndex_Chart').innerHTML = this.GetChartImage('HaSTC', json.HaSTCIndex_Change);
        window.parent.document.getElementById('CafeF_MarketOverview_UpdateTime').innerHTML = json.LastTradeDate;
        if (json.VNIndex_Change > 0)
        {
            window.parent.document.getElementById('CafeF_MarketOverview_VNIndex_img').innerHTML = '<img style="margin-right:3px;" alt="" src="' + this.script_folder + 'images/up.gif"/><a href="/Thi-truong-niem-yet.chn">VN-Index</a>';
            window.parent.document.getElementById('CafeF_MarketOverview_VNIndex').innerHTML = this.FormatNumber(json.VNIndex_Index) + '&nbsp;&nbsp;<span class="Up">+' + this.FormatNumber(json.VNIndex_Change) + '(+' + this.FormatNumber(json.VNIndex_ChangePercent) + '%)</span>';
        }
        else if (json.VNIndex_Change < 0)
        {
            window.parent.document.getElementById('CafeF_MarketOverview_VNIndex_img').innerHTML = '<img style="margin-right:3px;" alt="" src="' + this.script_folder + 'images/down.gif"/><a href="/Thi-truong-niem-yet.chn">VN-Index</a>';
            window.parent.document.getElementById('CafeF_MarketOverview_VNIndex').innerHTML = this.FormatNumber(json.VNIndex_Index) + '&nbsp;&nbsp;<span class="Down">' + this.FormatNumber(json.VNIndex_Change) + '(' + this.FormatNumber(json.VNIndex_ChangePercent) + '%)</span>';
        }
        else
        {
            window.parent.document.getElementById('CafeF_MarketOverview_VNIndex_img').innerHTML = '<img style="margin-right:3px;" alt="" src="' + this.script_folder + 'images/nochange.gif"/><a href="/Thi-truong-niem-yet.chn">VN-Index</a>';
            window.parent.document.getElementById('CafeF_MarketOverview_VNIndex').innerHTML = this.FormatNumber(json.VNIndex_Index) + '&nbsp;&nbsp;<span class="NoChange">0(0%)</span>';
        }
        if (json.HaSTCIndex_Change > 0)
        {
            window.parent.document.getElementById('CafeF_MarketOverview_HaSTCIndex_img').innerHTML = '<img style="margin-right:3px;" alt="" src="' + this.script_folder + 'images/up.gif"/><a href="/Thi-truong-niem-yet.chn">HaSTC-Index</a>';
            window.parent.document.getElementById('CafeF_MarketOverview_HaSTCIndex').innerHTML = this.FormatNumber(json.HaSTCIndex_Index) + '&nbsp;&nbsp;<span class="Up">+' + this.FormatNumber(json.HaSTCIndex_Change) + '(+' + this.FormatNumber(json.HaSTCIndex_ChangePercent) + '%)</span>';
        }
        else if (json.HaSTCIndex_Change < 0)
        {
            window.parent.document.getElementById('CafeF_MarketOverview_HaSTCIndex_img').innerHTML = '<img style="margin-right:3px;" alt="" src="' + this.script_folder + 'images/down.gif"/><a href="/Thi-truong-niem-yet.chn">HaSTC-Index</a>';
            window.parent.document.getElementById('CafeF_MarketOverview_HaSTCIndex').innerHTML = this.FormatNumber(json.HaSTCIndex_Index) + '&nbsp;&nbsp;<span class="Down">' + this.FormatNumber(json.HaSTCIndex_Change) + '(' + this.FormatNumber(json.HaSTCIndex_ChangePercent) + '%)</span>';
        }
        else
        {
            window.parent.document.getElementById('CafeF_MarketOverview_HaSTCIndex_img').innerHTML = '<img style="margin-right:3px;" alt="" src="' + this.script_folder + 'images/nochange.gif"/><a href="/Thi-truong-niem-yet.chn">HaSTC-Index</a>';
            window.parent.document.getElementById('CafeF_MarketOverview_HaSTCIndex').innerHTML = this.FormatNumber(json.HaSTCIndex_Index) + '&nbsp;&nbsp;<span class="NoChange">0(0%)</span>';
        }
        
        if (json.TradeState == "1")
        {
            window.parent.document.getElementById('CafeF_MarketOverview_TradeState').innerHTML = 'Trạng thái thị trường: <span style="color: rgb(69, 118, 156);">Đang giao dịch</span>';
        }
        else
        {
            window.parent.document.getElementById('CafeF_MarketOverview_TradeState').innerHTML = 'Trạng thái thị trường: <span style="color: #666666">Đóng cửa</span>';
        }
        
        window.parent.document.getElementById('CafeF_MarketOverview_HoTotalVol').innerHTML = this.FormatNumber(json.VNIndex_TotalVolume);
        window.parent.document.getElementById('CafeF_MarketOverview_HaTotalVol').innerHTML = this.FormatNumber(json.HaSTCIndex_TotalVolume);
        
        window.parent.document.getElementById('CafeF_MarketOverview_HoTotalValue').innerHTML = this.FormatNumber(json.VNIndex_TotalValue);
        window.parent.document.getElementById('CafeF_MarketOverview_HaTotalValue').innerHTML = this.FormatNumber(json.HaSTCIndex_TotalValue);
    }
    
    this.GetChartImage = function(center, change)
    {
        var prefix = (center == 'HoSE' ? 'ho' : 'ha');
        
        var alt = '';
        
        if (center == 'HoSE')
        {
            alt = 'Diễn biến chỉ số chứng khoán tại sàn  TPHCM';
        }
        else
        {
            alt = 'Diễn biến chỉ số chứng khoán tại sàn  Hà Nội';
        }
        
        var currentDate = new Date();
        
        if (change > 0)
        {
            return '<img alt="' + alt + '" src="http://cafef.vn/HomeMountainChartImage/' + prefix + '_green.png?udt' + currentDate.toDateString() + currentDate.toTimeString() + '"/>';
        }
        else if (change < 0)
        {
            return '<img alt="' + alt + '" src="http://cafef.vn/HomeMountainChartImage/' + prefix + '_red.png?udt' + currentDate.toDateString() + currentDate.toTimeString() + '"/>';
        }
        else
        {
            return '<img alt="' + alt + '" src="http://cafef.vn/HomeMountainChartImage/' + prefix + '_yellow.png?udt' + currentDate.toDateString() + currentDate.toTimeString() + '"/>';
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

//window.parent.document.write('<div id="CafeF_StockMarketOverview"></div>');
var container = window.parent.document.getElementById('CafeF_StockMarketOverview');
if (container)
{
    var cafef_stock_market_overview = new CafeF_StockMarketOverview('cafef_stock_market_overview');
    cafef_stock_market_overview.InitScript();
    
    if (container.delay)
    {
        setTimeout('cafef_stock_market_overview.LoadData()', parseInt(container.delay));
    }
    else
    {
        cafef_stock_market_overview.LoadData();
    }
}