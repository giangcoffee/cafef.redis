function CafeF_CongtyCoEPSTuongduong(instanceName)
{
    this.host = 'http://solieu.cafef.vn';
    //this.host = 'http://localhost:8084';
    this.script_folder = this.host + '/www/cafef/';
    this.script_object = null;
    this.instance_name = instanceName;
    this.Symbol = '';
    
    this.default_page_size = 10;
    
    this.page_size = this.default_page_size;
    this.page_index = 1;
    this.record_count = 0;
    this.page_count = 1;

    this.GetSymbol = function()
    {
        var query = window.location.href;
        query = query.toLowerCase();
        if(query.indexOf('thong-tin-chung/')<0 && query.indexOf('thong-tin-tai-chinh/')<0 && query.indexOf('ban-lanh-dao/')<0 && query.indexOf('bao-cao-tai-chinh/')<0)
        {
            if (query.indexOf('hose') > 0 || query.indexOf('hastc') > 0)
            {
                if (query.indexOf('hose') > 0)
                {
                    query = query.substring(query.indexOf('hose/') + 5);
                }
                else
                {
                    query = query.substring(query.indexOf('hastc/') + 6);
                }
                if (query.indexOf('thong-tin-tai-chinh') >= 0 || 
                    query.indexOf('thong-tin-chung') >= 0 || 
                    query.indexOf('ban-lanh-dao') >= 0 || 
                    query.indexOf('bao-cao-tai-chinh') >= 0)
                {
                    query = query.substring(0, query.indexOf('/'));
                }
                else
                {
                    query = query.substring(0, query.indexOf('-'));
                }
            }
            else
            {
                query = query.substring(query.indexOf('thong-tin-cong-ty/') + 18);
                query = query.substring(0, query.indexOf('.chn'));
            }
         }
         else
         {
            if(query.indexOf('thong-tin-chung/')>0)
            {
                query = query.substring(query.indexOf('thong-tin-chung/') + 16);
            }
            if(query.indexOf('thong-tin-tai-chinh/')>0)
            {
                //i=length('thong-tin-tai-chinh/');
                query = query.substring(query.indexOf('thong-tin-tai-chinh/') + 20);
            }
            if(query.indexOf('ban-lanh-dao/')>0)
            {
                query = query.substring(query.indexOf('ban-lanh-dao/') + 13);
            }
            if(query.indexOf('bao-cao-tai-chinh/')>0)
            {
                query = query.substring(query.indexOf('bao-cao-tai-chinh/') + 18);
            }
            query = query.substring(0, query.indexOf('-'));
         }
        return query.toUpperCase();
    }

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
        this.Symbol = this.GetSymbol();
        this.CreateCssLink(this.script_folder + 'css/cafef.css');
    
        document.getElementById('CafeF_CongtyCoEPSTuongduong').innerHTML = '<div class="cf_WCBox"><div class="cf_BoxHeader"><div></div></div><div class="cf_BoxContent" style="padding: 5px 10px 5px 10px;"><table class="Cafef_RelatedCompany" cellpadding="0" cellspacing="0"><tr><td style="font-weight: bold; font-size: 15px; text-align:left;">CÔNG TY CÓ EPS TƯƠNG ĐƯƠNG</td></tr><tr><td id="CafeF_EquivalentEPS_Dulieu" class="Dulieu"></td></tr><tr><td id="CafeF_EquivalentEPS_Phantrang" class="Phantrang"></td></tr><tr><td id="CafeF_EquivalentEPS_Note" class="Note"></td><tr><td id="CafeF_EquivalentEPS_GhiChu" class="Ghichu"></td></tr></table></div><div class="cf_BoxFooter"><div></div></div></div>';
    }
    
    this.LoadData = function(pageIndex)
    {
        this.page_index = pageIndex;
        this.CreateScriptObject(this.host + '/ProxyHandler.ashx?RequestName=EquivalentEPS&CallBack=' + this.instance_name + '.OnLoaded&RequestType=json&Symbol=' + this.Symbol + '&PageSize=' + this.page_size + '&PageIndex=' + this.page_index);
    }
    
    this.OnLoaded = function(data, methodName)
    {
        var json = eval(data);
        var output = '<table cellpadding="3" cellspacing="0"><tr><th style="border-right: solid 1px #fff;text-align:left;">Mã CK</th><th style="border-right: solid 1px #fff;text-align:right;">EPS</th><th style="border-right: solid 1px #fff;text-align:right;">Giá</th><th style="text-align:right;">Thay đổi</th></tr>';
        var isAlternation = false;
        for (var i = 0; i < json.FinanceStatements.length; i++)
        {
            output += '<tr' + (isAlternation ? ' class="Alternation"' : '') + '><td class="Symbol"><a href="' + CafeF_JSLibrary.GetCompanyInfoLink(json.FinanceStatements[i].Symbol) + '">' + json.FinanceStatements[i].Symbol + '</a></td><td>' + (json.FinanceStatements[i].EPS > 0 ? this.FormatNumber(json.FinanceStatements[i].EPS, true) : 'N/A') + '</td><td>' + this.FormatNumber(json.FinanceStatements[i].Price, true) + '</td><td>' + this.FormatChangeValue(json.FinanceStatements[i].Change, json.FinanceStatements[i].Percent) + '</td></tr>';
            isAlternation = !isAlternation;
        }
        output += '</table>';
        
        document.getElementById('CafeF_EquivalentEPS_Dulieu').innerHTML = output;
        document.getElementById('CafeF_EquivalentEPS_Note').innerHTML = 'Các công ty có EPS tương đương trên sàn ' + (json.OtherData == 1 ? 'HoSE' : 'HaSTC') + '<br>EPS lấy từ Bản tin ' + (json.OtherData == 1 ? 'HoSE' : 'HaSTC') + '<br>(EPS +/- 0.5)';
        document.getElementById('CafeF_EquivalentEPS_GhiChu').innerHTML = 'Trang ' + json.PageIndex + '/' + json.PageCount + ' (Tổng số: ' + json.RecordCount + ' công ty)';
        
        var page = '';
        if (json.PageCount > 1)
        {
            if (json.PageIndex > 1)
            {
                page += '<img alt="" onclick="' + this.instance_name + '.LoadData(' + (json.PageIndex - 1) + ')" src="images/paging_prev_enable.png" />';
            }
            else
            {
                page += '<img alt="" src="images/paging_prev_disable.png" />';
            }
            
            for (var j = 1; j <= json.PageCount; j++)
            {
                if (j == json.PageIndex)
                {
                    page += '&nbsp;[' + j + ']&nbsp;';
                }
                else
                {
                    page += '&nbsp;<a href="javascript:void(0)" onclick="' + this.instance_name + '.LoadData(' + j + ')">' + j + '</a>&nbsp;';
                }
            }
            
            if (json.PageIndex < json.PageCount)
            {
                page += '<img alt="" onclick="' + this.instance_name + '.LoadData(' + (json.PageIndex + 1) + ')" src="images/paging_next_enable.png" />';
            }
            else
            {
                page += '<img alt="" src="images/paging_next_disable.png" />';
            }
        }
        document.getElementById('CafeF_EquivalentEPS_Phantrang').innerHTML = page;
        //document.getElementById('CafeF_EquivalentEPS_Name').innerHTML = json.OtherData;
        
        this.record_count = json.RecordCount;
        this.page_index = json.PageIndex;
        this.page_count = json.PageCount;
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
    
    this.FormatChangeValue = function(value, percent)
    {
        var output = '';
        if (value > 0)
        {
            output = '<span style="color: #008000">+' + this.FormatNumber(value, true) + '(+' + this.FormatNumber(percent, true) + '%)</span>';
        }
        else if (value < 0)
        {
            output = '<span style="color: #cc0000">' + this.FormatNumber(value, true) + '(' + this.FormatNumber(percent, true) + '%)</span>';
        }
        else
        {
            output = '<span style="color: #ff9900">0(0%)</span>';
        }
        return output;
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

//document.write('<div id="CafeF_CongtyCoEPSTuongduong"></div>');
var container = document.getElementById('CafeF_CongtyCoEPSTuongduong');
if (container)
{
    var cafef_cong_ty_co_eps_tuong_duong = new CafeF_CongtyCoEPSTuongduong('cafef_cong_ty_co_eps_tuong_duong');
    cafef_cong_ty_co_eps_tuong_duong.InitScript();
    
    if (container.delay)
    {
        setTimeout('cafef_cong_ty_co_eps_tuong_duong.LoadData(1)', parseInt(container.delay));
    }
    else
    {
        cafef_cong_ty_co_eps_tuong_duong.LoadData(1);
    }
}