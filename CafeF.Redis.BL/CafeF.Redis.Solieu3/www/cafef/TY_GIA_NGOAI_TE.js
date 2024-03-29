function CafeF_TyGiaNgoaiTe(instanceName)
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
        
//        output += '<div style="background-color:White;width:240px" align="center">';
//        output += '<div style="overflow:hidden">';
//        output += '<div style="float:left"><img alt="" src="/images/images/conner_top_left.gif" /></div>';
//        output += '<div style="float:right"><img alt="" src="/images/images/conner_top_right.gif" /></div>';
//        output += '</div>';
//        output += '<div style="overflow:hidden;width:222px;">';
//        output += '<div><img alt="" src="/Images/Images/Untitled-1_r2_c2.gif" /></div>';
//        output += '<div style="border-left:solid 1px #cccccc;border-right:solid 1px #cccccc;border-bottom:solid 1px #cccccc" >';
//        output += '<div class="giavang_title" align="left">TỶ GIÁ NGOẠI TỆ</div>';
//        output += '<div align="center" class="giavang_date" id="CafeF_TygiaNgoaite_NgayCapnhat"></div>';
//        output += '<div class="giavang_donvi" align="right">Đơn quy đổi :VND</div>';
//        output += '</div>';
//        output += '</div>';
//        output += '<div id="CafeF_TygiaNgoaite_Dulieu" style="width:220px;border-left:solid 1px #cccccc;border-right:solid 1px #cccccc"></div>';
//        output += '<div><img alt="" src="/Images/Images/Untitled-1_r4_c2.gif" /></div>';
//        output += '<div style="overflow:hidden">';
//        output += '<div style="float:left"><img alt="" src="/images/images/conner_bottom_left.gif" /></div>';
//        output += '<div style="float:right"><img alt="" src="/images/images/conner_bottom_right.gif" /></div>';
//        output += '</div>';
//        output += '</div>';
        output += '<div class="cf_WCBox" style="width:240px;" >';
        output += '<div class="cf_BoxHeader" ><div></div></div>';
        output += '<div class="cf_BoxContent"><div class="cf_Pad5TB9LR"><div class="cf_WireBox"><div class="cf_BoxHeader" ><div></div></div>';
        output += '<div class="cf_BoxContent"><div style="width: 100%"><div class="CafeF_Padding_Box"><div style="padding-left: 5px;">';
        output += 'TỶ GIÁ NGOẠI TỆ</div></div><div style="padding-left: 10px;"><div align="center" class="giavang_date" id="CafeF_TygiaNgoaite_NgayCapnhat"></div></div>';
        output += '<div class="NormalText_ita" style="text-align: right; padding-left: 10px;">Đơn vị qui đổi: VND</div>';
        output += '<div class="cf_Box_BorderTop" id=CafeF_TygiaNgoaite_Dulieu></div>';
        output += '</div></div><div class="cf_BoxFooter" ><div></div></div></div></div></div><div class="cf_BoxFooter"><div></div></div></div>';        
        window.parent.document.getElementById('CafeF_TygiaNgoaite').innerHTML = output;
        
        window.parent.document.getElementById('CafeF_TygiaNgoaite').innerHTML = output;
    }
    
    this.LoadData = function()
    {
        this.CreateScriptObject(this.host + '/ProxyHandler.ashx?RequestName=ExchangeRate&CallBack=' + this.instance_name + '.OnLoaded&RequestType=json');
    }

    this.OnLoaded = function(data, methodName)
    {
        var json = eval(data);
        
        window.parent.document.getElementById('CafeF_TygiaNgoaite_NgayCapnhat').innerHTML = 'Cập nhật ngày ' + (json.LastUpdate);
        
        var output = '';
        output += '<div style="overflow: hidden; background-color: #F2F2F2; padding-top: 5px">';
        output += '<div class="cf_grd_ColumnHeader" style="padding-left: 10px; text-align: left; width: 30px;">';
        output += 'NT</div>';
        output += '<div class="cf_grd_ColumnHeader" style="text-align: right; width: 60px;">';
        output += 'Mua TM</div>';
        output += '<div class="cf_grd_ColumnHeader" style="text-align: right; width: 60px;">';
        output += 'Mua CK</div>';
        output += '<div class="cf_grd_ColumnHeader" style="text-align: right; width: 60px;">';
        output += 'Bán&nbsp;</div>';
        output += '</div>';
        var isAlternation = true;
        for (var i = 0; i < json.ExchangeRateItems.length; i++)
        {
            //if (isNaN(json.ExchangeRateItems[i].BuyCash) && isNaN(json.ExchangeRateItems[i].BuyTransfer) && isNaN(json.ExchangeRateItems[i].Sell)) continue;
            if (isAlternation)
            {
                output += '<div style="overflow: hidden;padding-top:5px">';
                output += '<div class="cf_SymbolItem" style="float: left; padding-left: 10px; text-align: left;width: 30px;">' + json.ExchangeRateItems[i].Name.replace(',50-100', '') + '</div>';
                output += '<div class="NumericItem" style="float: left; text-align: right; width: 55px;">' + json.ExchangeRateItems[i].BuyCash + '</div>';
                output += '<div class="NumericItem" style="text-align: right; width: 55px;">' + json.ExchangeRateItems[i].BuyTransfer + '</div>';
                output += '<div class="NumericItem" style="text-align: right;">' + json.ExchangeRateItems[i].Sell + '&nbsp;</div>';
                output += '</div>';
            }
            else
            {
                output += '<div style="overflow: hidden; background-color: #f2f2f2; height: 20px; padding-top: 5px">';
                output += '<div class="cf_SymbolItem" style="padding-left: 10px; text-align: left; width: 30px;">' + json.ExchangeRateItems[i].Name.replace(',50-100', '') + '</div>';
                output += '<div class="NumericItem" style="text-align: right; width: 55px;">' + json.ExchangeRateItems[i].BuyCash + '</div>';
                output += '<div class="NumericItem" style="text-align: right; width: 55px;">' + json.ExchangeRateItems[i].BuyTransfer + '</div>';
                output += '<div class="NumericItem" style="text-align: right;">' + json.ExchangeRateItems[i].Sell + '&nbsp;</div>';
                output += '</div>';
            }
            isAlternation = !isAlternation;
        }
        window.parent.document.getElementById('CafeF_TygiaNgoaite_Dulieu').innerHTML = output;
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

//window.parent.document.write('<div id="CafeF_TygiaNgoaite"></div>');
var container = window.parent.document.getElementById('CafeF_TygiaNgoaite');
if (container)
{
    var cafef_ty_gia_ngoai_te = new CafeF_TyGiaNgoaiTe('cafef_ty_gia_ngoai_te');
    cafef_ty_gia_ngoai_te.InitScript();
    
    if (container.delay)
    {
        setTimeout('cafef_ty_gia_ngoai_te.LoadData()', parseInt(container.delay));
    }
    else
    {
        cafef_ty_gia_ngoai_te.LoadData();
    }
}
