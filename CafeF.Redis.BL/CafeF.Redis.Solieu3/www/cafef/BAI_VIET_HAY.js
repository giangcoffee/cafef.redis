function CafeF_BaiVietHay(instanceName)
{
    this.host = 'http://solieu.cafef.vn';
    //this.host = 'http://localhost:62045';
    this.script_folder = this.host + '/www/cafef/';
    this.script_object = null;
    this.instance_name = instanceName;
    this.Fields = {'Quantity':0,'Price':1,'Change':2,'ChangePercent':3};

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
    
        document.getElementById('CafeF_BaiVietHay').innerHTML = '<table width="350" border="0" cellspacing="0" cellpadding="0"><tr><td valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0"><tr><td><table width="100%" border="0" cellspacing="0" cellpadding="0"><tr><td width="4" height="28"><img src="' + this.script_folder + 'images/BaiVietHay/img_gocleft.gif" width="4" height="28"/></td><td background="' + this.script_folder + 'images/BaiVietHay/img_back.gif" class="title"><img src="' + this.script_folder + 'images/BaiVietHay/img_arrow.gif" width="8" height="4" style="margin: 0px 6px 0px 5px" align="absmiddle"/><a class="Box_Title_Home" title="Ý kiến chuyên gia" style="text-decoration:none;color:red" href="/y-kien-chuyen-gia.chn">Ý KIẾN CHUYÊN GIA</a></td><td width="4"><img src="' + this.script_folder + 'images/BaiVietHay/img_gocright.gif" width="4" height="28"/></td></tr></table></td></tr><tr><td bgcolor="#FFFFFF" valign="top" id="CafeF_BaiVietHay_Content"></td></tr><tr><td bgcolor="#FFFFFF" style="text-align: right;padding-right: 10px; padding-top: 5px;"><a class="title" style="font-size: 11px; font-weight: normal;" title="Ý kiến chuyên gia" style="text-decoration:none;" href="/y-kien-chuyen-gia.chn">Xem tiếp</a></td></tr><tr><td><img src="' + this.script_folder + 'images/BaiVietHay/img_day.gif" width="350" height="6"/></td></tr></table></td></tr></table>';;
    }
    
    this.LoadData = function()
    {
        this.CreateScriptObject(this.host + '/ProxyHandler.ashx?RequestName=FavoriteNews&CallBack=' + this.instance_name + '.OnLoaded&RequestType=json');
    }

    this.OnLoaded = function(data, methodName)
    {
        var json = eval(data);

        var output = '';
        
        output  = '<table width="100%" border="0" cellspacing="0" cellpadding="0">';
        for (var i = 0; i < json.FavoriteNewsItems.length; i++)
        {
            output += '<tr><td><img src="' + this.script_folder + 'images/BaiVietHay/img_dot1.gif" width="5" height="5" style="margin: 0px 6px 0px 10px" align="absmiddle"/></td><td style="background-image: url(' + this.script_folder + 'images/BaiVietHay/img_dot.gif); background-position: bottom; background-repeat: repeat-x; padding-top: 5px; padding-bottom: 5px;"><a class="content" title="' + json.FavoriteNewsItems[i].Title + '" href="http://cafef.vn' + json.FavoriteNewsItems[i].Link + '">' + json.FavoriteNewsItems[i].Title + '</a>';
            if (json.FavoriteNewsItems[i].Author != '')
            {
                output += '<br><span class="content1">Tác giả:</span> <a href="/y-kien-chuyen-gia/' + json.FavoriteNewsItems[i].Author + '.chn"<span class="tentacgia">' + json.FavoriteNewsItems[i].Author + '</span></a>';
            }
            output += '</td><td width="10px;">&nbsp;</td></tr>';
        }
        output += '</table>';
        
        document.getElementById('CafeF_BaiVietHay_Content').innerHTML = output;
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
}

//document.write('<div id="CafeF_TopStockSymbol_TestOutput"></div>');
var container = document.getElementById('CafeF_BaiVietHay');
if (container)
{
    var cafef_bai_viet_hay = new CafeF_BaiVietHay('cafef_bai_viet_hay');
    cafef_bai_viet_hay.InitScript();

    if (container.delay)
    {
        setTimeout('cafef_bai_viet_hay.LoadData()', parseInt(container.delay));
    }
    else
    {
        cafef_bai_viet_hay.LoadData();
    }
}