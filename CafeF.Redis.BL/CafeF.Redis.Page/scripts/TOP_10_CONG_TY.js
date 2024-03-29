function CafeF_Top_PE_EPS(instanceName)
{
    this.host = 'http://solieu6.vcmedia.vn';
    //this.host = 'http://localhost:8081';
    this.script_folder = 'http://cafef3.vcmedia.vn/solieu/solieu6/'; //'http://solieu6.vcmedia.vn/www/cafef/';
    this.script_object = null;
    this.instance_name = instanceName;
    this.LoadingImage = '<div align="center"><img src="http://cafef3.vcmedia.vn/solieu/solieu6/images/loading.gif" /></div>';
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
        //this.CreateCssLink(this.script_folder + 'css/cafef.css');
    
        var output = '';
        
        output += '<table class="CafeF_TopCompany" align="center" style="border: solid 1px #dadada; font-family: Arial;" cellpadding="0" cellspacing="0" width="300px">';
        output += '<tr><td style="text-align: left; font-size: 16px; padding: 5px 0px 5px 5px;">Top 10 công ty</td>';
        output += '<td align="right" nowrap="nowrap" width="155px" align="right" style="padding-right: 0px;">';
        output += '<div style="height: 19px; width: 44px; padding-top: 3px; text-align: center; overflow: hidden; float: right; background-image: url(' + this.script_folder + 'images/btn.gif); background-repeat: no-repeat; margin-right: 5px;"><a id="CafeF_TopCTy_HaSTC" class="ButtonInActived" href="javascript:void(0)" onclick="' + this.instance_name + '.LoadData(2)">HaSTC</a></div>';
        output += '<div style="height: 19px; width: 44px; padding-top: 3px; text-align: center; text-align: center; overflow: hidden; float: right; background-image: url(' + this.script_folder + 'images/btn.gif); background-repeat: no-repeat; margin-right: 5px;"><a id="CafeF_TopCTy_HoSE" class="ButtonInActived" href="javascript:void(0)" onclick="' + this.instance_name + '.LoadData(1)">HoSE</a></div>';
        output += '<div style="height: 19px; width: 44px; padding-top: 3px; text-align: center; text-align: center; overflow: hidden; float: right; background-image: url(' + this.script_folder + 'images/btn.gif); background-repeat: no-repeat; margin-right: 5px;"><a id="CafeF_TopCTy_ViewAll" class="ButtonActived" href="javascript:void(0)" onclick="' + this.instance_name + '.LoadData(0)">Tất cả</a></div>';
        output += '</td></tr>';
        output += '<tr><td colspan="2" style="text-align: left;">';
        output += '<table cellpadding="0" cellspacing="0" border="0">';
        output += '<tr>';
        output += '<td style="width:3px;">&nbsp;</td>';
        output += '<td class="Button" id="CafeF_BoxChungKhoan_TabButton_TopPE" onclick="' + this.instance_name + '.SelectTab(\'TopPE\');" style="width: 73px; background-image:url(' + this.script_folder + 'images/top_co_phieu/btn_PE_on.gif); font-weight: bold;">P/E</td>';
        output += '<td style="width:3px;">&nbsp;</td>';
        output += '<td class="Button" id="CafeF_BoxChungKhoan_TabButton_TopEPS" onclick="' + this.instance_name + '.SelectTab(\'TopEPS\');" style="width: 70px; background-image:url(' + this.script_folder + 'images/top_co_phieu/btn_EPS_off.gif);">EPS</td>';
        output += '<td style="width:3px;">&nbsp;</td>';
        output += '<td class="Button" id="CafeF_BoxChungKhoan_TabButton_TopCapital" onclick="' + this.instance_name + '.SelectTab(\'TopCapital\');" style="width: 80px; background-image:url(' + this.script_folder + 'images/top_co_phieu/btn_vonhoa_off.gif);">Vốn hóa</td>';
        output += '</tr>';
        output += '</table>';
        output += '</td></tr>';
        output += '<tr><td colspan="2" style="text-align: left;border-bottom: solid 1px #dadada;border-top: solid 1px #dadada;">';
        output += '<div id="CafeF_BoxChungKhoan_Tab_TopPE"></div>';
        output += '<div style="display: none; " id="CafeF_BoxChungKhoan_Tab_TopEPS"></div>';
        output += '<div style="display: none; " id="CafeF_BoxChungKhoan_Tab_TopCapital"></div>';
        output += '</td></tr>';
        output += '<tr><td colspan="2" style="text-align: left; font-size: 11px; color: #999; padding: 5px 0px 5px 10px;"">';
        output += '(*) P/E thấp nhất &nbsp; &nbsp; (*) EPS lớn nhất<br/>';
        output += '(*) Vốn hóa lớn nhất (Tỷ đồng)';
        output += '</td></tr>';
        output += '</table>';
        
        document.getElementById('CafeF_Top_PE_EPS').innerHTML = output;
    }
    
    this.LoadData = function(tradeCenter)
    {
        this.CreateScriptObject(this.host + '/ProxyHandler.ashx?RequestName=TopFinanceStatement&CallBack=' + this.instance_name + '.OnLoaded&RequestType=json&TradeCenter=' + tradeCenter);
        
        var Butotn_ViewAll = document.getElementById('CafeF_TopCTy_ViewAll'); Butotn_ViewAll.className = 'ButtonInActived';
        var Butotn_HoSE = document.getElementById('CafeF_TopCTy_HoSE'); Butotn_HoSE.className = 'ButtonInActived';
        var Butotn_HaSTC = document.getElementById('CafeF_TopCTy_HaSTC'); Butotn_HaSTC.className = 'ButtonInActived';
        
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
        document.getElementById('CafeF_BoxChungKhoan_Tab_TopPE').innerHTML = this.LoadingImage;
        document.getElementById('CafeF_BoxChungKhoan_Tab_TopEPS').innerHTML = this.LoadingImage;
        document.getElementById('CafeF_BoxChungKhoan_Tab_TopCapital').innerHTML = this.LoadingImage;
    }

    this.OnLoaded = function(data, methodName)
    {
        var json = eval(data);
        
        var output = '';

        output  = '<table width="100%" cellpadding="0" cellspacing="0">';
        output += '<tr><th style="text-align: left;" nowrap="nowrap">Mã CK</th><th style="width: 100px;">P/E</th><th style="width: 50px;">EPS</th><th style="width: 60px;">Giá</th></tr>';
        
        var isAlternation = false;
    
        for (var i = 0; i < json.TopPE.FinanceStatements.length; i++)
        {
            output += '<tr' + (isAlternation ? ' style="background-color:#f5f5f5"' : '') + '>';
            output += '<td style="text-align: left;"><a href="' + CafeF_JSLibrary.GetCompanyInfoLink(json.TopPE.FinanceStatements[i].Symbol) + '">' + json.TopPE.FinanceStatements[i].Symbol + '</a></td>';
            output += '<td>' + this.FormatNumber(json.TopPE.FinanceStatements[i].PE, true) + '</td>';
            output += '<td>' + this.FormatNumber(json.TopPE.FinanceStatements[i].EPS, true) + '</td>';
            output += '<td>' + this.FormatChangeValue(json.TopPE.FinanceStatements[i].Change, json.TopPE.FinanceStatements[i].Price) + '</td>';
            output += '</tr>';
            isAlternation = !isAlternation;
        }
        output += '</table>';
        
        document.getElementById('CafeF_BoxChungKhoan_Tab_TopPE').innerHTML = output;
        // ------------------------------
        output  = '<table width="100%" cellpadding="0" cellspacing="0">';
        output += '<tr><th style="text-align: left;">Mã CK</th><th style="width: 100px;">EPS</th><th style="width: 50px;">P/E</th><th style="width: 60px;">Giá</th></tr>';
        isAlternation = false;
        for (var i = 0; i < json.TopEPS.FinanceStatements.length; i++)
        {
            output += '<tr' + (isAlternation ? ' style="background-color:#f5f5f5"' : '') + '>';
            output += '<td style="text-align: left;"><a href="' + CafeF_JSLibrary.GetCompanyInfoLink(json.TopEPS.FinanceStatements[i].Symbol) + '">' + json.TopEPS.FinanceStatements[i].Symbol + '</a></td>';
            output += '<td>' + this.FormatNumber(json.TopEPS.FinanceStatements[i].EPS, true) + '</td>';
            output += '<td>' + this.FormatNumber(json.TopEPS.FinanceStatements[i].PE, true) + '</td>';
            output += '<td>' + this.FormatChangeValue(json.TopEPS.FinanceStatements[i].Change, json.TopEPS.FinanceStatements[i].Price) + '</td>';
            output += '</tr>';
            isAlternation = !isAlternation;
        }
        output += '</table>';
        
        document.getElementById('CafeF_BoxChungKhoan_Tab_TopEPS').innerHTML = output;
        // ------------------------------
        output  = '<table width="100%" cellpadding="0" cellspacing="0">';
        output += '<tr><th style="text-align: left;">Mã CK</th><th style="width: 100px;">Vốn hóa</th><th style="width: 50px;">P/E</th><th style="width: 60px;">Giá</th></tr>';
        isAlternation = false;
        for (var i = 0; i < json.TopCapital.FinanceStatements.length; i++)
        {
            output += '<tr' + (isAlternation ? ' style="background-color:#f5f5f5"' : '') + '>';
            output += '<td style="text-align: left;"><a href="' + CafeF_JSLibrary.GetCompanyInfoLink(json.TopCapital.FinanceStatements[i].Symbol) + '">' + json.TopCapital.FinanceStatements[i].Symbol + '</a></td>';
            output += '<td>' + this.FormatNumber(json.TopCapital.FinanceStatements[i].Capital, true) + '</td>';
            output += '<td>' + this.FormatNumber(json.TopCapital.FinanceStatements[i].PE, true) + '</td>';
            output += '<td>' + this.FormatChangeValue(json.TopCapital.FinanceStatements[i].Change, json.TopCapital.FinanceStatements[i].Price) + '</td>';
            output += '</tr>';
            isAlternation = !isAlternation;
        }
        output += '</table>';
        
        document.getElementById('CafeF_BoxChungKhoan_Tab_TopCapital').innerHTML = output;
    }
    
    this.SelectTab = function(id)
    {
        var tabbutton_TopPE = document.getElementById('CafeF_BoxChungKhoan_TabButton_TopPE'); tabbutton_TopPE.style.backgroundImage = 'url(' + this.script_folder + 'images/top_co_phieu/btn_PE_off.gif)'; tabbutton_TopPE.style.fontWeight = 'normal';
        var tabbutton_TopEPS = document.getElementById('CafeF_BoxChungKhoan_TabButton_TopEPS'); tabbutton_TopEPS.style.backgroundImage = 'url(' + this.script_folder + 'images/top_co_phieu/btn_EPS_off.gif)'; tabbutton_TopEPS.style.fontWeight = 'normal';
        var tabbutton_TopCapital = document.getElementById('CafeF_BoxChungKhoan_TabButton_TopCapital'); tabbutton_TopCapital.style.backgroundImage = 'url(' + this.script_folder + 'images/top_co_phieu/btn_vonhoa_off.gif)'; tabbutton_TopCapital.style.fontWeight = 'normal';
        
        var tab_TopPE = document.getElementById('CafeF_BoxChungKhoan_Tab_TopPE'); tab_TopPE.style.display = 'none';
        var tab_TopEPS = document.getElementById('CafeF_BoxChungKhoan_Tab_TopEPS'); tab_TopEPS.style.display = 'none';
        var tab_TopCapital = document.getElementById('CafeF_BoxChungKhoan_Tab_TopCapital'); tab_TopCapital.style.display = 'none';
        
        if (id == 'TopEPS')
        {
            tabbutton_TopEPS.style.backgroundImage = 'url(' + this.script_folder + 'images/top_co_phieu/btn_EPS_on.gif)'; tabbutton_TopEPS.style.fontWeight = 'bold';
            tab_TopEPS.style.display = 'block';
        }
        else if (id == 'TopCapital')
        {
            tabbutton_TopCapital.style.backgroundImage = 'url(' + this.script_folder + 'images/top_co_phieu/btn_vonhoa_on.gif)'; tabbutton_TopCapital.style.fontWeight = 'bold';
            tab_TopCapital.style.display = 'block';
        }
        else
        {
            tabbutton_TopPE.style.backgroundImage = 'url(' + this.script_folder + 'images/top_co_phieu/btn_PE_on.gif)'; tabbutton_TopPE.style.fontWeight = 'bold';
            tab_TopPE.style.display = 'block';
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
    
    this.FormatChangeValue = function(change, price)
    {
        var output = '';
        if (change > 0)
        {
            output = '<span style="color: #008000">' + this.FormatNumber(price) + '(+' + this.FormatNumber(change) + ')</span>';
        }
        else if (change < 0)
        {
            output = '<span style="color: #cc0000">' + this.FormatNumber(price) + '(' + this.FormatNumber(change) + ')</span>';
        }
        else
        {
            output = '<span style="color: #ff9900">' + this.FormatNumber(price) + '(0)</span>';
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

//document.write('<div id="CafeF_Top_PE_EPS"></div>');
var container = document.getElementById('CafeF_Top_PE_EPS');
if (container)
{
    var cafef_top_pe_eps = new CafeF_Top_PE_EPS('cafef_top_pe_eps');
    cafef_top_pe_eps.InitScript();

    if (container.delay)
    {
        setTimeout('cafef_top_pe_eps.LoadData(0)', parseInt(container.delay));
    }
    else
    {
        cafef_top_pe_eps.LoadData(0);
    }
}