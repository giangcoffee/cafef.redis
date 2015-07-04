
function CafeF_HangHoaNguyenLieu(instanceName)
{
    //this.host = 'http://solieu.cafef.vn';
    this.host = 'http://localhost:8081';
    this.script_folder = this.host + '/www/cafef/';
    this.script_object = null;
    this.instance_name = instanceName;

    this.InitScript = function()
    {
        document.getElementById('CafeF_BoxHangHoaNguyenLieu').innerHTML = '<div class="cf_WCBox"><div class="cf_BoxHeader"><div><!-- --></div></div><div class="cf_BoxContent" style="padding: 5px 10px 5px 10px;"><table class="Cafef_RelatedCompany" cellpadding="0" cellspacing="0"><tr><td class="Title">Giá hàng hóa nguyên liệu</td></tr><tr><td id="CafeF_HangHoaNguyenLieu_NongSan"></td></tr><tr><td id="CafeF_HangHoaNguyenLieu_KimLoai"></td></tr></table></div><div class="cf_BoxFooter"><div><!-- --></div></div></div>';
    }
    
    this.LoadData = function()
    {
        this.CreateScriptObject(this.host + '/ProxyHandler.ashx?RequestName=HangHoaNguyenLieu&CallBack=' + this.instance_name + '.OnLoaded&RequestType=json');
    }
    
    this.OnLoaded = function(data, methodName)
    {
        var json = eval(data);
        
        var nongsan = '<table rules="all" cellspacing="0">';
        nongsan += '<tr><th class="Name">Nông sản</th><th style="width: 70px;">Giá</th><th>+/-</th></tr>';
        for (var i = 0; i < json.Softs.Items.length; i++)
        {
            nongsan += '<tr>';
            nongsan += '<td class="Name">' + this.GetItemName(json.Softs.Items[i].Name) + '</td>';
            nongsan += '<td>' + json.Softs.Items[i].Index + '</td>';
            nongsan += '<td>' + json.Softs.Items[i].Change + '</td>';
            nongsan += '</tr>';
        }
        for (var i = 0; i < json.Grains.Items.length; i++)
        {
            nongsan += '<tr>';
            nongsan += '<td class="Name">' + this.GetItemName(json.Grains.Items[i].Name) + '</td>';
            nongsan += '<td>' + this.FormatNumber(json.Grains.Items[i].Index, false) + '</td>';
            nongsan += '<td>' + this.FormatChangeValue(json.Grains.Items[i].Change) + '</td>';
            nongsan += '</tr>';
        }
        nongsan += '</table>';
        document.getElementById('CafeF_HangHoaNguyenLieu_NongSan').innerHTML = nongsan;
        
        var kimloai = '<table rules="all" cellspacing="0">';
        kimloai += '<tr><th class="Name">Kim loại</th><th style="width: 70px;">Giá</th><th>+/-</th></tr>';
        for (var i = 0; i < json.Metals.Items.length; i++)
        {
            kimloai += '<tr>';
            kimloai += '<td class="Name">' + this.GetItemName(json.Metals.Items[i].Name) + '</td>';
            kimloai += '<td>' + this.FormatNumber(json.Metals.Items[i].Index, false) + '</td>';
            kimloai += '<td>' + this.FormatChangeValue(json.Metals.Items[i].Change) + '</td>';
            kimloai += '</tr>';
        }
        kimloai += '</table>';
        document.getElementById('CafeF_HangHoaNguyenLieu_KimLoai').innerHTML = kimloai;
    }
    
    this.GetItemName = function(name)
    {
        name = name.toLowerCase();
        name = name.substr(0, name.indexOf(' '));
        switch (name)
        {
            case 'gold':
                return 'Vàng';
                break;
            case 'aluminum':
                return 'Nhôm';
                break;
            case 'copper':
                return 'Đồng';
                break;
            case 'silver':
                return 'Bạc';
                break;
            case 'platinum':
                return 'Bạch kim';
                break;
            case 'rough':
                return 'Gạo';
                break;
            case 'corn':
                return 'Ngô';
                break;
            case 'soybeans':
                return 'Đậu tương';
                break;
            case 'cocoa':
                return 'Cacao';
                break;
            case 'coffee':
                return 'Cà phê';
                break;
            default:
                return '';
                break;
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
    
    this.FormatChangeValue = function(value)
    {
        var output = '';
        if (value > 0)
        {
            output = '<span style="color: #008000">+' + this.FormatNumber(value, true) + '</span>';
        }
        else if (value < 0)
        {
            output = '<span style="color: #cc0000">' + this.FormatNumber(value, true) + '</span>';
        }
        else
        {
            output = '<span style="color: #ff9900">0</span>';
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

//document.write('<div id="CafeF_BoxHangHoaNguyenLieu"></div>');
var container = document.getElementById('CafeF_BoxHangHoaNguyenLieu');
if (container)
{
    var cafef_hang_hoa_nguyen_lieu = new CafeF_HangHoaNguyenLieu('cafef_hang_hoa_nguyen_lieu');
    cafef_hang_hoa_nguyen_lieu.InitScript();
    
    if (container.delay)
    {
        setTimeout('cafef_hang_hoa_nguyen_lieu.LoadData()', parseInt(container.delay));
    }
    else
    {
        cafef_hang_hoa_nguyen_lieu.LoadData(1);
    }
}