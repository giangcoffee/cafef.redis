function CafeF_ACBGoldMarket(instanceName)
{
    //this.host = 'http://solieu.cafef.vn';
    this.host = 'http://localhost:8081';
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
        output += '<td class="Box_Title_Home" style="border-top:solid 1px #dadada">SÀN GIAO DỊCH VÀNG ACB</td>';
        output += '<td class="Right">';
        output += '<img alt="" width="9" height="20" src="' + this.script_folder + 'images/cornuu_top_right.gif" /></td>';
        output += '</tr>';
        output += '<tr class="Middle">';
        output += '<td class="Left"><img alt="" width="8" src="' + this.script_folder + 'images/spacer.gif" /></td>';
        output += '<td class="Center">';
        output += '<table cellspacing="0" cellpadding="2">';
        output += '<tr>';
        output += '<td colspan="2">';
        output += 'Nghìn/lượng:&nbsp;<span id="CafeF_ACBGoldMarket_Price" class="Price"></span><span id="CafeF_ACBGoldMarket_Change"></span></td>';
        output += '</tr>';
        output += '<tr>';
        output += '<td>';
        output += 'KLGD:&nbsp;<span class="Price" id="CafeF_ACBGoldMarket_Quantity"></span>&nbsp;lượng</td>';
        output += '<td>';
        output += 'GTGD:&nbsp;<span class="Price" id="CafeF_ACBGoldMarket_Value"></span>&nbsp;tỷ VNĐ</td>';
        output += '</tr>';
        output += '<tr>';
        output += '<td id="CafeF_ACBGoldMarket_Chart" colspan="2"></td>';
        output += '</tr>';
        output += '<tr>';
        output += '<td align="right" class="Note" id="CafeF_ACBGoldMarket_UpdateTime" colspan="2"></td>';
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
        
        window.parent.document.getElementById('CafeF_ACBGoldMarket').innerHTML = output;
    }
    
    this.LoadData = function()
    {
        this.CreateScriptObject(this.host + '/ProxyHandler.ashx?RequestName=ACBGoldMarket&CallBack=' + this.instance_name + '.OnLoaded&RequestType=json');
    }

    this.OnLoaded = function(data, methodName)
    {
        var json = eval(data);
        
        window.parent.document.getElementById('CafeF_ACBGoldMarket_Price').innerHTML = this.FormatNumber(json.Price);
        window.parent.document.getElementById('CafeF_ACBGoldMarket_Value').innerHTML = this.FormatNumber(json.TotalValue);
        window.parent.document.getElementById('CafeF_ACBGoldMarket_Quantity').innerHTML = this.FormatNumber(json.TotalVolume);
        window.parent.document.getElementById('CafeF_ACBGoldMarket_UpdateTime').innerHTML = 'Cập nhật vào ' + json.UpdateTime;
        
        var currentDate = new Date();
        
        if (json.Change > 0)
        {
            window.parent.document.getElementById('CafeF_ACBGoldMarket_Change').innerHTML = '<img style="margin-left:3px;margin-right:3px;" alt="" src="' + this.script_folder + 'images/up.gif"/><span class="Up">+' + this.FormatNumber(json.Change) + '</span>';
            window.parent.document.getElementById('CafeF_ACBGoldMarket_Chart').innerHTML = '<img alt="Diễn biến giá vàng trên sàn giao dịch vàng ACB" src="http://solieu.cafef.vn/Public/charts/ACBGoldMarket/home_goldmarket_up.gif?udt' + currentDate.toDateString() + currentDate.toTimeString() + '" />';
        }
        else if (json.Change < 0)
        {
            window.parent.document.getElementById('CafeF_ACBGoldMarket_Change').innerHTML = '<img style="margin-left:3px;margin-right:3px;" alt="" src="' + this.script_folder + 'images/down.gif"/><span class="Down">' + this.FormatNumber(json.Change) + '</span>';
            window.parent.document.getElementById('CafeF_ACBGoldMarket_Chart').innerHTML = '<img alt="Diễn biến giá vàng trên sàn giao dịch vàng ACB" src="http://solieu.cafef.vn/Public/charts/ACBGoldMarket/home_goldmarket_down.gif?udt' + currentDate.toDateString() + currentDate.toTimeString() + '" />';
        }
        else
        {
            window.parent.document.getElementById('CafeF_ACBGoldMarket_Change').innerHTML = '<img style="margin-left:3px;margin-right:3px;" alt="" src="' + this.script_folder + 'images/nochange.gif"/><span class="NoChange">' + this.FormatNumber(json.Change) + '</span>';
            window.parent.document.getElementById('CafeF_ACBGoldMarket_Chart').innerHTML = '<img alt="Diễn biến giá vàng trên sàn giao dịch vàng ACB" src="http://solieu.cafef.vn/Public/charts/ACBGoldMarket/home_goldmarket_nochange.gif?udt' + currentDate.toDateString() + currentDate.toTimeString() + '" />';
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
    	temp = temp.replace('-,', '-');
        return temp;
    }
}

//window.parent.document.write('<div id="CafeF_ACBGoldMarket"></div>');
var container = window.parent.document.getElementById('CafeF_ACBGoldMarket');
if (container)
{
    var cafef_acb_gold_market = new CafeF_ACBGoldMarket('cafef_acb_gold_market');
    cafef_acb_gold_market.InitScript();
    if (container.delay)
    {
        setTimeout('cafef_acb_gold_market.LoadData()', parseInt(container.delay));
    }
    else
    {
        cafef_acb_gold_market.LoadData();
    }
}