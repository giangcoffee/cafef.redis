function CafeF_TyGiaNgoaiTe(instanceName)
{
    this.host = 'http://solieu.cafef.vn';
    //this.host = 'http://localhost:8081';
    this.script_folder = this.host + '/www/cafef/';
    this.script_object = null;
    this.instance_name = instanceName;
    this.ie4=document.all;
    this.ns6=document.getElementById&&!document.all;

    
    this.SmallBoard = eval({'Row':[
        {'Name':'USD','BuyCash':0,'BuyTransfer':0,'Sell':0},
        {'Name':'EUR','BuyCash':0,'BuyTransfer':0,'Sell':0},
        {'Name':'GBP','BuyCash':0,'BuyTransfer':0,'Sell':0},
        {'Name':'HKD','BuyCash':0,'BuyTransfer':0,'Sell':0}
    ]});
    
//    this.CreateCssLink = function(href)
//    {
//        var css = document.createElement('link');
//        css.type = 'text/css';
//        css.rel = 'stylesheet';
//        css.href = href;
//        var head = document.getElementsByTagName('head')[0];
//        head.appendChild(css);
//    }
    
    this.InitScript = function()
    {
        //this.CreateCssLink(this.script_folder + 'css/cafef.css');
    
        var output = '';
        
        output += '<div class="cf_WCBox" style="width:240px;" >';
        output += '<div class="cf_BoxHeader" ><div></div></div>';
        output += '<div class="cf_BoxContent"><div class="cf_Pad5TB9LR"><div class="cf_WireBox"><div class="cf_BoxHeader" ><div></div></div>';
        output += '<div class="cf_BoxContent"><div style="width: 100%"><div class="CafeF_Padding_Box"><div style="padding-left: 5px;">';
        output += '<span class="Box_Title_Home">TỶ GIÁ NGOẠI TỆ</span></div></div><div style="padding-left: 10px;"><div align="center" class="giavang_date" id="CafeF_TygiaNgoaite_NgayCapnhat"></div></div>';
        output += '<div class="NormalText_ita" style="text-align: right; padding-left: 10px;">Đơn vị qui đổi: VND</div>';
        output += '<div class="cf_Box_BorderTop" id=CafeF_TygiaNgoaite_Dulieu></div>';
        
        output += '<div class="cf_WCBox" id="CafeF_TygiaNgoaite_Popup_Container" style="z-index: 1000; position: absolute; width: 242px; display: none;" onmouseover="' + this.instance_name + '.ShowDetail();" onmouseout="' + this.instance_name + '.HideDetail();">';
        output += '    <div class="cf_BoxContent">';
        output += '        <div style="padding: 0px 10px 9px 8px;">';
        output += '            <div class="cf_WireBox">';
        output += '                <div style="width: 220px;">';
        output += '                    <div class="cf_BoxContent" style="width: 100%;">';
        output += '                        <div id="CafeF_TygiaNgoaite_Popup" class="cf_Box_BorderTop"></div>';
        output += '                    </div>';
        output += '                    <div style="width: 220px; background:transparent url(http://cafef.channelvn.net/Images/Box_Tin_Doanh_Nghiep/corner_wire_BR._bg.gif) no-repeat scroll 100% 0;height:4px;overflow:hidden;">';
        output += '                        <div style="background:#FFFFFF url(http://cafef.channelvn.net/Images/Box_Tin_Doanh_Nghiep/corner_wire_BL.gif) no-repeat scroll 0 0;float:left;height:4px;width:4px;"></div>';
        output += '                    </div>';
        output += '                </div>';
        output += '            </div>';
        output += '        </div>';
        output += '        <div style="background:#FFFFFF url(http://cafef.channelvn.net/Images/Box_Tin_Doanh_Nghiep/corner_dg_BR.gif) no-repeat scroll 100% 0;height:4px;overflow:hidden;">';
        output += '            <div style="background:#FFFFFF url(http://cafef.channelvn.net/Images/Box_Tin_Doanh_Nghiep/corner_dg_BL.gif) no-repeat scroll 0 0;float:left;height:4px;overflow:hidden;width:4px;"></div>';
        output += '        </div>';
        output += '    </div>';
        output += '</div>';
        
        output += '<div class="cf_Box_BorderTop" style="text-align: right; padding: 3px 5px 0px 0px;"><a href="javascript:void();" onmouseover="' + this.instance_name + '.ShowDetail();" onmouseout="' + this.instance_name + '.HideDetail();">Xem thêm các ngoại tệ khác</a></div>';
        output += '</div></div><div class="cf_BoxFooter" ><div></div></div></div></div></div><div class="cf_BoxFooter"><div></div></div></div>';        
        
        document.getElementById('CafeF_TygiaNgoaite').innerHTML = output;
    }
    
    this.LoadData = function()
    {
        this.CreateScriptObject(this.host + '/ProxyHandler.ashx?RequestName=ExchangeRate&CallBack=' + this.instance_name + '.OnLoaded&RequestType=json');
    }

    this.OnLoaded = function(data, methodName)
    {
        var json = eval(data);
        
        document.getElementById('CafeF_TygiaNgoaite_NgayCapnhat').innerHTML = '' + (json.LastUpdate);
        
        var popup = '', smallBoard = '';
        
        smallBoard += '<div style="overflow: hidden; background-color: #F2F2F2; padding-top: 5px">';
        smallBoard += '<div class="cf_grd_ColumnHeader" style="padding-left: 10px; text-align: left; width: 30px;">';
        smallBoard += 'NT</div>';
        smallBoard += '<div class="cf_grd_ColumnHeader" style="text-align: right; width: 60px;">';
        smallBoard += 'Mua TM</div>';
        smallBoard += '<div class="cf_grd_ColumnHeader" style="text-align: right; width: 60px;">';
        smallBoard += 'Mua CK</div>';
        smallBoard += '<div class="cf_grd_ColumnHeader" style="text-align: right; width: 60px;">';
        smallBoard += 'Bán&nbsp;</div>';
        smallBoard += '</div>';
        
        var isAlternation = true, isSmallBoardAlternation = true;
        for (var i = 0; i < json.ExchangeRateItems.length; i++)
        {
            var name = json.ExchangeRateItems[i].Name.replace(',50-100', '');
            
            if (name.indexOf('USD,') >= 0) continue;
            
            if (!this.GetSmallBoard(json.ExchangeRateItems[i]))
            {
                if (isAlternation)
                {
                    popup += '<div style="overflow: hidden;padding-top:5px">';
                    popup += '<div class="cf_SymbolItem" style="float: left; padding-left: 10px; text-align: left;width: 30px;">' + name + '</div>';
                    popup += '<div class="NumericItem" style="float: left; text-align: right; width: 55px;">' + json.ExchangeRateItems[i].BuyCash + '</div>';
                    popup += '<div class="NumericItem" style="text-align: right; width: 55px;">' + json.ExchangeRateItems[i].BuyTransfer + '</div>';
                    popup += '<div class="NumericItem" style="text-align: right;">' + json.ExchangeRateItems[i].Sell + '&nbsp;</div>';
                    popup += '</div>';
                }
                else
                {
                    popup += '<div style="overflow: hidden; background-color: #f2f2f2; height: 20px; padding-top: 5px">';
                    popup += '<div class="cf_SymbolItem" style="padding-left: 10px; text-align: left; width: 30px;">' + name + '</div>';
                    popup += '<div class="NumericItem" style="text-align: right; width: 55px;">' + json.ExchangeRateItems[i].BuyCash + '</div>';
                    popup += '<div class="NumericItem" style="text-align: right; width: 55px;">' + json.ExchangeRateItems[i].BuyTransfer + '</div>';
                    popup += '<div class="NumericItem" style="text-align: right;">' + json.ExchangeRateItems[i].Sell + '&nbsp;</div>';
                    popup += '</div>';
                }
                isAlternation = !isAlternation;
            }
        }
        document.getElementById('CafeF_TygiaNgoaite_Popup').innerHTML = popup;
        
        for (var j = 0; j < this.SmallBoard.Row.length; j++)
        {
            var name = this.SmallBoard.Row[j].Name.replace(',50-100', '');
            
            if (isSmallBoardAlternation)
            {
                smallBoard += '<div style="overflow: hidden;padding-top:5px">';
                smallBoard += '<div class="cf_SymbolItem" style="float: left; padding-left: 10px; text-align: left;width: 30px;">' + name + '</div>';
                smallBoard += '<div class="NumericItem" style="float: left; text-align: right; width: 55px;">' + this.SmallBoard.Row[j].BuyCash + '</div>';
                smallBoard += '<div class="NumericItem" style="text-align: right; width: 55px;">' + this.SmallBoard.Row[j].BuyTransfer + '</div>';
                smallBoard += '<div class="NumericItem" style="text-align: right;">' + this.SmallBoard.Row[j].Sell + '&nbsp;</div>';
                smallBoard += '</div>';
            }
            else
            {
                smallBoard += '<div style="overflow: hidden; background-color: #f2f2f2; height: 20px; padding-top: 5px">';
                smallBoard += '<div class="cf_SymbolItem" style="padding-left: 10px; text-align: left; width: 30px;">' + name + '</div>';
                smallBoard += '<div class="NumericItem" style="text-align: right; width: 55px;">' + this.SmallBoard.Row[j].BuyCash + '</div>';
                smallBoard += '<div class="NumericItem" style="text-align: right; width: 55px;">' + this.SmallBoard.Row[j].BuyTransfer + '</div>';
                smallBoard += '<div class="NumericItem" style="text-align: right;">' + this.SmallBoard.Row[j].Sell + '&nbsp;</div>';
                smallBoard += '</div>';
            }
            isSmallBoardAlternation = !isSmallBoardAlternation;
        }
        document.getElementById('CafeF_TygiaNgoaite_Dulieu').innerHTML = smallBoard;
    }
    
    this.GetSmallBoard = function(obj)
    {
        for (var i = 0; i < this.SmallBoard.Row.length; i++)
        {
            var name = obj.Name.replace(',50-100', '');
            if (this.SmallBoard.Row[i].Name == name)
            {
                this.SmallBoard.Row[i].BuyCash = obj.BuyCash;
                this.SmallBoard.Row[i].BuyTransfer = obj.BuyTransfer;
                this.SmallBoard.Row[i].Sell = obj.Sell;
                
                return true;
            }
        }
        
        return false;
    }
    
    this.iecompattest = function()
    {
        return (document.compatMode && document.compatMode!="BackCompat")? document.documentElement : document.body
    }


    this.clearbrowseredge = function(obj, whichedge){
        var edgeoffset=0
        if (whichedge=="rightedge")
        {
            var windowedge=this.ie4 && !window.opera? this.iecompattest().scrollLeft+this.iecompattest().clientWidth-15 : window.pageXOffset+window.innerWidth-15;
            container.contentmeasure=container.offsetWidth;
            if (windowedge-container.x-obj.offsetWidth < container.contentmeasure)
            {
                edgeoffset=container.contentmeasure+obj.offsetWidth;
            }
        }
        else
        {
            var topedge=this.ie4 && !window.opera? this.iecompattest().scrollTop : window.pageYOffset;
            var windowedge=this.ie4 && !window.opera? this.iecompattest().scrollTop+this.iecompattest().clientHeight-15 : window.pageYOffset+window.innerHeight-18;
            container.contentmeasure=container.offsetHeight;
            if (windowedge-container.y < container.contentmeasure)
            { //move menu up?
                edgeoffset=container.contentmeasure-obj.offsetHeight;
                if ((container.y-topedge)<container.contentmeasure)
                { //up no good either? (position at top of viewable window then)
                    edgeoffset=container.y;
                }
            }
        }
        return edgeoffset;
    }


    
    this.ShowDetail = function()
    {
        var divPopup = document.getElementById('CafeF_TygiaNgoaite_Popup_Container');
        var left = container.offsetLeft;
        var leftParentElement = container.offsetParent;
        while (leftParentElement != null)
        {
            left += leftParentElement.offsetLeft;
            leftParentElement = leftParentElement.offsetParent;
        }
        
        divPopup.style.left = (left - 630) + 'px';
        divPopup.style.display = 'block';
    }
    
    this.HideDetail = function()
    {
        var divPopup = document.getElementById('CafeF_TygiaNgoaite_Popup_Container');
        divPopup.style.display = 'none';
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

//document.write('<div id="CafeF_TygiaNgoaite"></div>');
var container = document.getElementById('CafeF_TygiaNgoaite');
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
