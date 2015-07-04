function CafeF_BangGiaChungKhoanRutGon(instanceName)
{
    this.host = 'http://solieu4.cafef.vn';
    //this.host = 'http://localhost:8081';
    this.script_folder = 'http://solieu.cafef.vn/www/cafef/';
    this.script_object = null;
    this.stock_price_table_data = null;
    this.instance_name = instanceName;
    this.Fields = {'BasicPrice':0,'Price':1,'Change':2,'Quantity':3,'GDNN':4};
    this.LoadingImage = '<div align="center" style="padding-top:50px; padding-bottom: 50px;"><img src="http://solieu.cafef.vn/www/cafef/images/loading.gif" /></div>';
    
    this.CreateCssLink = function(href)
    {
        var css = window.parent.document.createElement('link');
        css.type = 'text/css';
        css.rel = 'stylesheet';
        css.href = href;
        var head = window.parent.document.getElementsByTagName('head')[0];
        head.appendChild(css);
    }

    this.InitStockPriceTable = function()
    {
        this.CreateCssLink(this.script_folder + 'css/cafef.css');
    
        window.parent.document.getElementById('CafeF_BangChungKhoanRutGon').innerHTML = '<table width="100%" cellspacing="0" cellpadding="0" class="CafeF_RoundCornerBox"><tr class="Top"><td class="Left"><img width="4" height="4" src="' + this.script_folder + 'images/conner_top_left.gif" alt="" /></td><td></td><td class="Right"><img width="4" height="4" src="' + this.script_folder + 'images/conner_top_right.gif" id="CafeF_RoundCornerBox" alt="" /></td></tr><tr><td></td><td class="Middle"><table cellspacing="0" cellpadding="0" class="Cafef_RoundCornerChildBox"><tr class="Top"><td class="Left" height="5"><img height="5" width="9" src="' + this.script_folder + 'images/small_cornuu_top_left.gif" alt="" /></td><td class="Title" height="5" width="100%"><img width="5" height="5" src="' + this.script_folder + 'images/spacer.gif" alt="" /></td><td class="Right" height="5"><img height="5" width="9" src="' + this.script_folder + 'images/small_cornuu_top_right.gif" alt="" /></td></tr><tr class="Middle"><td colspan="3" class="Center_Border"><table width="100%" cellspacing="0" cellpadding="0" border="0"><tr><td valign="top" height="28" style="padding: 0px 0px 0px 6px;" class="indx_title">Bảng chứng khoán trực tuyến</td></tr><tr><td><table cellspacing="0" cellpadding="0" border="0"><tr><td width="7" height="29"></td><td width="85" align="center" id="tdHoSEPriceTable" runat="server" class="indx_btnon"><a onclick="window.frames[\'CafeF_EmbedDataRequest\'].' + this.instance_name + '.LoadStockPriceTable(\'HoSE\');" href="javascript:void(0);">HoSE</a></td><td width="3"></td><td width="85" align="center" id="tdHaSTCPriceTable" runat="server" class="style1"><a onclick="window.frames[\'CafeF_EmbedDataRequest\'].' + this.instance_name + '.LoadStockPriceTable(\'HaSTC\');" href="javascript:void(0);">HaSTC</a></td><td></td></tr></table></td></tr><tr><td height="29" background="' + this.script_folder + 'images/img_bk.gif"><table width="100%" cellspacing="0" cellpadding="0" border="0"><tr><td width="34" height="27" align="right" class="indx_btnoff"><strong class="indx_mack">CK</strong></td><td width="20"><table width="100%" cellspacing="2" cellpadding="0" border="0"><tr><td align="center"><a onclick="window.frames[\'CafeF_EmbedDataRequest\'].' + this.instance_name + '.Sort(\'Symbol\', \'up\');" href="javascript:void(0);" style="padding: 0px;"><img width="10" height="6" border="0" src="' + this.script_folder + 'images/img_up.gif" id="SymbolSort_Up" /></a></td></tr><tr><td align="center"><a onclick="window.frames[\'CafeF_EmbedDataRequest\'].' + this.instance_name + '.Sort(\'Symbol\', \'down\');" href="javascript:void(0);" style="padding: 0px;"><img width="10" height="6" border="0" src="' + this.script_folder + 'images/img_dow.gif" id="SymbolSort_Down" /></a></td></tr></table></td><td width="65" align="right" class="indx_mack"><strong>KL</strong></td><td width="20"><table width="100%" cellspacing="2" cellpadding="0" border="0"><tr><td align="center"><a onclick="window.frames[\'CafeF_EmbedDataRequest\'].' + this.instance_name + '.Sort(\'Volume\', \'up\');" href="javascript:void(0);" style="padding: 0px;"><img width="10" height="6" border="0" src="' + this.script_folder + 'images/img_upoff.gif" id="VolumeSort_Up" /></a></td></tr><tr><td align="center"><a onclick="window.frames[\'CafeF_EmbedDataRequest\'].' + this.instance_name + '.Sort(\'Volume\', \'down\');" href="javascript:void(0);" style="padding: 0px;"><img width="10" height="6" border="0" src="' + this.script_folder + 'images/img_dow.gif" id="VolumeSort_Down" /></a></td></tr></table></td><td width="45" align="right" class="indx_mack"><strong>Giá</strong></td><td width="20"><table width="100%" cellspacing="2" cellpadding="0" border="0"><tr><td align="center"><a onclick="window.frames[\'CafeF_EmbedDataRequest\'].' + this.instance_name + '.Sort(\'Price\', \'up\');" href="javascript:void(0);" style="padding: 0px;"><img width="10" height="6" border="0" src="' + this.script_folder + 'images/img_upoff.gif" id="PriceSort_Up" /></a></td></tr><tr><td align="center"><a onclick="window.frames[\'CafeF_EmbedDataRequest\'].' + this.instance_name + '.Sort(\'Price\', \'down\');" href="javascript:void(0);" style="padding: 0px;"><img width="10" height="6" border="0" src="' + this.script_folder + 'images/img_dow.gif" id="PriceSort_Down" /></a></td></tr></table></td><td width="45" align="right" class="indx_mack"><strong>+/-</strong></td><td width="30"><table width="100%" cellspacing="2" cellpadding="0" border="0"><tr><td align="center"><a onclick="window.frames[\'CafeF_EmbedDataRequest\'].' + this.instance_name + '.Sort(\'Change\', \'up\');" href="javascript:void(0);" style="padding: 0px;"><img width="10" height="6" border="0" src="' + this.script_folder + 'images/img_upoff.gif" id="ChangeSort_Up" /></a></td></tr><tr><td align="center"><a onclick="window.frames[\'CafeF_EmbedDataRequest\'].' + this.instance_name + '.Sort(\'Change\', \'down\');" href="javascript:void(0);" style="padding: 0px;"><img width="10" height="6" border="0" src="' + this.script_folder + 'images/img_dow.gif" id="ChangeSort_Down" /></a></td></tr></table></td><td width="60" align="right" class="indx_mack"><strong>GDNN</strong></td><td width="30"><table width="100%" cellspacing="2" cellpadding="0" border="0"><tr><td align="center"><a onclick="window.frames[\'CafeF_EmbedDataRequest\'].' + this.instance_name + '.Sort(\'GDNN\', \'up\');" href="javascript:void(0);" style="padding: 0px;"><img width="10" height="6" border="0" src="' + this.script_folder + 'images/img_upoff.gif" id="GDNNSort_Up" /></a></td></tr><tr><td align="center"><a onclick="window.frames[\'CafeF_EmbedDataRequest\'].' + this.instance_name + '.Sort(\'GDNN\', \'down\');" href="javascript:void(0);" style="padding: 0px;"><img width="10" height="6" border="0" src="' + this.script_folder + 'images/img_dow.gif" id="GDNNSort_Down" /></a></td></tr></table></td></tr></table></td></tr><tr><td><table width="100%" cellspacing="0" cellpadding="0" border="0"><tr><td valign="top"><div id="BriefStockTable"></div></td></tr></table></td></tr><tr><td style="padding: 0px 4px;"><div class="style3" style="float:left; overflow:hidden;">Đơn vị khối lượng: 10cp</div><div id="divBanggia_ChuthichGDNN" class="style3" style="float:right; overflow:hidden;"></div><br /><span class="gray">Được cung cấp bởi <a target="_blank" href="http://www.vincomsc.com.vn/" style="text-decoration:none;font-weight:bold;" class="gray">Công ty chứng khoán Vincom</a></span><br /><span id="spanLinkFullPriceBoard"></span></td></tr></table></td></tr><tr class="Bottom"><td class="Left"><img width="9" height="5" src="' + this.script_folder + 'images/cornuu_bottom_left.gif" alt="" /></td><td class="Center"><img height="4" src="' + this.script_folder + 'images/spacer.gif" alt="" /></td><td class="Right"><img width="9" height="5" src="' + this.script_folder + 'images/cornuu_bottom_right.gif" alt="" /></td></tr></table></td><td></td></tr><tr class="Bottom"><td class="Left"><img width="4" height="4" src="' + this.script_folder + 'images/conner_bottom_left.gif" alt="" /></td><td><img height="4" src="' + this.script_folder + 'images/spacer.gif" alt="" /></td><td class="Right"><img width="4" height="4" src="' + this.script_folder + 'images/conner_bottom_right.gif" alt="" /></td></tr></table>';
    }
    
    this.LoadStockPriceTable = function(type)
    {
        window.parent.document.getElementById('BriefStockTable').innerHTML = this.LoadingImage;
        if (type == 'HoSE')
        {
            this.CreateScriptObject(this.host + '/ProxyHandler.ashx?RequestName=AllStockSymbol&CallBack=' + this.instance_name + '.OnLoaded&RequestType=json&TradeCenter=HoSE');
            window.parent.document.getElementById('tdHoSEPriceTable').className = 'indx_btnon';
            window.parent.document.getElementById('tdHaSTCPriceTable').className = 'style1';
            window.parent.document.getElementById('spanLinkFullPriceBoard').innerHTML = '<a target="_blank" style="color:#999999;font-size:11px;font-weight:normal;" href="http://tradding.cafef.vn/hose.aspx">Xem bảng chứng khoán đầy đủ tại đây</a> <a target="_blank" href="http://tradding.cafef.net/hose.aspx"><img src="' + this.script_folder + 'images/img_btn.gif" width="9" height="9" border="0" align="absmiddle"></a>';
            window.parent.document.getElementById('divBanggia_ChuthichGDNN').innerHTML = 'GDNN: Khối lượng mua';
        }
        else
        {
            this.CreateScriptObject(this.host + '/ProxyHandler.ashx?RequestName=AllStockSymbol&CallBack=' + this.instance_name + '.OnLoaded&RequestType=json&TradeCenter=HaSTC');
            window.parent.document.getElementById('tdHoSEPriceTable').className = 'style1';
            window.parent.document.getElementById('tdHaSTCPriceTable').className = 'indx_btnon';
            window.parent.document.getElementById('spanLinkFullPriceBoard').innerHTML = '<a target="_blank" style="color:#999999;font-size:11px;font-weight:normal;" href="http://tradding.cafef.vn/hastc.aspx">Xem bảng chứng khoán đầy đủ tại đây</a> <a target="_blank" href="http://tradding.cafef.net/hastc.aspx"><img src="' + this.script_folder + 'images/img_btn.gif" width="9" height="9" border="0" align="absmiddle"></a>';
            window.parent.document.getElementById('divBanggia_ChuthichGDNN').innerHTML = 'GDNN: Giao dịch ròng';
        }
    }

    this.OnLoaded = function(data, methodName)
    {
        this.stock_price_table_data = eval(data);
        
        this.Sort('Symbol', 'up');
    }

    this.RefreshStockPriceTable = function()
    {
        var output = '';
//        if (window.parent.document.all)
//        {
//            output = output.concat('<table width="94%" border="0" cellspacing="0" cellpadding="0">');
//        }
//        else
//        {
            output = output.concat('<table width="100%" border="0" cellspacing="0" cellpadding="0">');
//        }

        var  isAlternation = true;
        for (var i = 0; i < this.stock_price_table_data.Symbols.length; i++)
        {
            if (isAlternation)
            {
                output = output.concat('<tr><td height="25" style="background-color: #f6f6f6"><table width="100%" border="0" cellspacing="0" cellpadding="0"><tr><td width="50" height="25" class="content" style="padding:0px 0px 0px 10px"><a target="_top" href="' + CafeF_JSLibrary.GetCompanyInfoLink(this.stock_price_table_data.Symbols[i].Symbol) + '">' + this.stock_price_table_data.Symbols[i].Symbol + '</a></td><td width="55" class="content" align="right" style="padding:0px 0px 0px 0px">' + this.FormatNumber(this.stock_price_table_data.Symbols[i].Datas[this.Fields.Quantity], true) + '</td><td width="60" class="content" align="right">' + this.FormatNumber(this.stock_price_table_data.Symbols[i].Datas[this.Fields.Price], true) + '</td><td width="60" align="right">' + this.FormatChangeValue(this.stock_price_table_data.Symbols[i].Datas[this.Fields.Change]) + '</td><td align="right" class="content" style="padding-right: 5px;">' + this.FormatNumber(this.stock_price_table_data.Symbols[i].Datas[this.Fields.GDNN], true) + '</td></tr></table></td></tr>');
            }
            else
            {
                output = output.concat('<tr><td height="25"><table width="100%" border="0" cellspacing="0" cellpadding="0"><tr><td width="50" height="25" class="content" style="padding:0px 0px 0px 10px"><a target="_top" href="' + CafeF_JSLibrary.GetCompanyInfoLink(this.stock_price_table_data.Symbols[i].Symbol) + '">' + this.stock_price_table_data.Symbols[i].Symbol + '</a></td><td width="55" class="content" align="right" style="padding:0px 0px 0px 0px">' + this.FormatNumber(this.stock_price_table_data.Symbols[i].Datas[this.Fields.Quantity], true) + '</td><td width="60" class="content" align="right">' + this.FormatNumber(this.stock_price_table_data.Symbols[i].Datas[this.Fields.Price], true) + '</td><td width="60" align="right">' + this.FormatChangeValue(this.stock_price_table_data.Symbols[i].Datas[this.Fields.Change]) + '</td><td align="right" class="content" style="padding-right: 5px;">' + this.FormatNumber(this.stock_price_table_data.Symbols[i].Datas[this.Fields.GDNN], true) + '</td></tr></table></td></tr>');
            }
            isAlternation = !isAlternation;
        }                                 
        output = output.concat('</table>');
        
        window.parent.document.getElementById('BriefStockTable').innerHTML = output;
    }
    
    // Tach thanh 2 ham Sort de hien thi cai anh waiting thoi (phai delay 100ms thi moi hien dc anh)
    this.Sort = function(field, type)
    {
        window.parent.document.getElementById('BriefStockTable').innerHTML = this.LoadingImage;
        
        setTimeout('cafef_bang_chung_khoan_rut_gon.SortDelay("' + field + '", "' + type + '")', 100);
    }

    this.SortDelay = function(field, type)
    {
        var symbolUp = window.parent.document.getElementById('SymbolSort_Up'); symbolUp.src = this.script_folder + 'images/StockImages/img_upoff.gif';
        var symbolDown = window.parent.document.getElementById('SymbolSort_Down'); symbolDown.src = this.script_folder + 'images/StockImages/img_dow.gif';
        
        var volumeUp = window.parent.document.getElementById('VolumeSort_Up'); volumeUp.src = this.script_folder + 'images/StockImages/img_upoff.gif';
        var volumeDown = window.parent.document.getElementById('VolumeSort_Down'); volumeDown.src = this.script_folder + 'images/StockImages/img_dow.gif';
        
        var priceUp = window.parent.document.getElementById('PriceSort_Up'); priceUp.src = this.script_folder + 'images/StockImages/img_upoff.gif';
        var priceDown = window.parent.document.getElementById('PriceSort_Down'); priceDown.src = this.script_folder + 'images/StockImages/img_dow.gif';
        
        var changeUp = window.parent.document.getElementById('ChangeSort_Up'); changeUp.src = this.script_folder + 'images/StockImages/img_upoff.gif';
        var changeDown = window.parent.document.getElementById('ChangeSort_Down'); changeDown.src = this.script_folder + 'images/StockImages/img_dow.gif';
        
        var gdnnUp = window.parent.document.getElementById('GDNNSort_Up'); gdnnUp.src = this.script_folder + 'images/StockImages/img_upoff.gif';
        var gdnnDown = window.parent.document.getElementById('GDNNSort_Down'); gdnnDown.src = this.script_folder + 'images/StockImages/img_dow.gif';
        
        if (field == 'Symbol')
        {
            if (type == 'up')
            {
                this.stock_price_table_data.Symbols.sort(function (a, b) {
                    for (var i = 0; i < a.Symbol.length && i < b.Symbol.length; i++)
                    {
                        if (a.Symbol.charCodeAt(i) != b.Symbol.charCodeAt(i))
                        {
                            return (a.Symbol.charCodeAt(i) - b.Symbol.charCodeAt(i));
                        }
                    }
                    return 0;
                });
                symbolUp.src = this.script_folder + 'images/StockImages/img_up.gif';
            }
            else
            {
                this.stock_price_table_data.Symbols.sort(function (a, b) {
                    for (var i = 0; i < a.Symbol.length && i < b.Symbol.length; i++)
                    {
                        if (a.Symbol.charCodeAt(i) != b.Symbol.charCodeAt(i))
                        {
                            
                            return (b.Symbol.charCodeAt(i) - a.Symbol.charCodeAt(i));
                        }
                    }
                    return 0;
                });
                symbolDown.src = this.script_folder + 'images/StockImages/img_dowon.gif';
            }
        }
        else if (field == 'Volume')
        {
            if (type == 'up')
            {
                this.stock_price_table_data.Symbols.sort(function (a, b) { return (a.Datas[3] - b.Datas[3]); });
                volumeUp.src = this.script_folder + 'images/StockImages/img_up.gif';
            }
            else
            {
                this.stock_price_table_data.Symbols.sort(function (a, b) { return (b.Datas[3] - a.Datas[3]); });
                volumeDown.src = this.script_folder + 'images/StockImages/img_dowon.gif';
            }
        }
        else if (field == 'Price')
        {
            if (type == 'up')
            {
                this.stock_price_table_data.Symbols.sort(function (a, b) { return (a.Datas[1] - b.Datas[1]); });
                priceUp.src = this.script_folder + 'images/StockImages/img_up.gif';
            }
            else
            {
                this.stock_price_table_data.Symbols.sort(function (a, b) { return (b.Datas[1] - a.Datas[1]); });
                priceDown.src = this.script_folder + 'images/StockImages/img_dowon.gif';
            }
        }
        else if (field == 'Change')
        {
            if (type == 'up')
            {
                this.stock_price_table_data.Symbols.sort(function (a, b) { return (a.Datas[2] - b.Datas[2]); });
                changeUp.src = this.script_folder + 'images/StockImages/img_up.gif';
            }
            else
            {
                this.stock_price_table_data.Symbols.sort(function (a, b) { return (b.Datas[2] - a.Datas[2]); });
                changeDown.src = this.script_folder + 'images/StockImages/img_dowon.gif';
            }
        }
        else if (field == 'GDNN')
        {
            if (type == 'up')
            {
                this.stock_price_table_data.Symbols.sort(function (a, b) { return (a.Datas[4] - b.Datas[4]); });
                gdnnUp.src = this.script_folder + 'images/StockImages/img_up.gif';
            }
            else
            {
                this.stock_price_table_data.Symbols.sort(function (a, b) { return (b.Datas[4] - a.Datas[4]); });
                gdnnDown.src = this.script_folder + 'images/StockImages/img_dowon.gif';
            }
        }
        this.RefreshStockPriceTable();
    }

    this.FormatChangeValue = function(value)
    {
        if (value > 0)
        {
            return '<span class="green">' + this.FormatNumber(value, true) + '</span>';
        }
        else if (value < 0)
        {
            return '<span class="red">' + this.FormatNumber(value, true) + '</span>';
        }
        else
        {
            return '<span class="stc_nochange">' + this.FormatNumber(value, true) + '</span>';
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
        
        setTimeout(this.instance_name + '.AppendScript()', 10);
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

    this.FormatNumber = function(value, displayZero)
    {
        if (!value || value == '') return (displayZero ? '0' : '');
        try
        {
            var number = parseFloat(value);
            value = this.FormatNumber1(number, 2, '.', ',');
            if (value == '.aN') value = (displayZero ? '0' : '');
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
	    
	    //if (temp.indexOf('.') < 0) temp = temp + '.0';
    	
        return temp.replace('-,', '-');
    }
}

//window.parent.document.write('<div id="CafeF_BangChungKhoanRutGon"></div>');
var container = window.parent.document.getElementById('CafeF_BangChungKhoanRutGon');
if (container)
{
    var cafef_bang_chung_khoan_rut_gon = new CafeF_BangGiaChungKhoanRutGon('cafef_bang_chung_khoan_rut_gon');
    cafef_bang_chung_khoan_rut_gon.InitStockPriceTable();
    if (container.delay)
    {
        setTimeout('cafef_bang_chung_khoan_rut_gon.LoadStockPriceTable("HoSE")', parseInt(container.delay));
    }
    else
    {
        cafef_bang_chung_khoan_rut_gon.LoadStockPriceTable('HoSE');
    }
}