function CafeF_BoxSearch(instanceName)
{
    this.host = 'http://solieu.cafef.vn';
    //this.host = 'http://localhost:8084';
    this.script_folder = this.host + '/www/cafef/';
    this.instance_name = instanceName;
    this.IsAutocompleteInit = false;
    this.SearchType = 0; // 0:Công ty; 1:Tin tức
    
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
//        this.CreateCssLink(this.script_folder + 'css/cafef.css');
//        this.CreateScriptObject(this.host + '/Public/js/jquery.js');
//        this.CreateScriptObject(this.host + '/Public/js/Library.js');
//        this.CreateScriptObject(this.script_folder + 'js/jqDnR.js');
//        this.CreateScriptObject('http://cafef.vn/Scripts/AutoComplete/kby.js');
//        this.CreateScriptObject(this.host + '/Public/js/jquery.bgiframe.min.js');
//        this.CreateScriptObject(this.host + '/Public/js/jquery.dimensions.js');
//        this.CreateScriptObject(this.host + '/Public/js/jquery.autocomplete2.js');
        
        var output = '';
        output += '<div style="width: 352px; background-color: #EBEBEB; overflow: hidden;">';
        output += '<div style="overflow: hidden">';
        output += '<div style="overflow: hidden; float: left; height: 9px;"><img alt="" src="http://cafef.vn/images/images/bottom_corn_top_left.gif" /></div>';
        output += '<div style="overflow: hidden; float: right; height: 9px;"><img alt="" src="http://cafef.vn/images/images/bottom_corn_top_right.gif" /></div>';
        output += '</div>';
        
        output += '<table align="center" cellpadding="1" cellspacing="0" style="margin-top: 3px;">';
        output += '<tr><td>';
        output += '<input style="width:230px;" onkeydown="return CafeF_BoxSearch_OnEnter(event, 0);" value="Gõ mã CK hoặc Tên công ty..." onblur="if (this.value == \'\') { this.value=\'Gõ mã CK hoặc Tên công ty...\'; }" onfocus="this.value = \'\'; ' + this.instance_name + '.InitAutoComplete();" type="text" id="CafeF_SearchKeyword_Company" autocomplete="off" class="class_text_autocomplete ac_input" />';
        output += '<input style="width:230px; display: none;" onkeydown="return CafeF_BoxSearch_OnEnter(event, 1);" value="Tìm kiếm tin tức..." onblur="if (this.value == \'\') { this.value=\'Tìm kiếm tin tức...\'; }" onfocus="this.value = \'\';" type="text" id="CafeF_SearchKeyword_News" />';
        output += '</td><td>';
        output += '<input type="image" onclick="' + this.instance_name + '.Seach();return false;" src="' + this.script_folder + 'images/search.gif" /></td>';
        output += '</tr>';
        output += '<tr><td colspan="2" style="font-family: Arial; font-size: 12px;">';
        output += '&nbsp; &nbsp; <input type="radio" checked="checked" id="CafeF_BoxSearch_Type_Company" name="CafeF_BoxSearch_Type" onclick="' + this.instance_name + '.ChangeSearchType(0);" /> <label for="CafeF_BoxSearch_Type_Company">Công ty</label> &nbsp; &nbsp; &nbsp;';
        output += '&nbsp; &nbsp; <input type="radio" id="CafeF_BoxSearch_Type_News" name="CafeF_BoxSearch_Type" onclick="' + this.instance_name + '.ChangeSearchType(1);" /> <label for="CafeF_BoxSearch_Type_News">Tin tức</label>';
        output += '</td></tr>';
        output += '</table>';
        
        output += '<div style="overflow: hidden">';
        output += '<div style="overflow: hidden; float: left; height: 9px;"><img alt="" src="http://cafef.vn/images/images/bottom_corn_bottom_left.gif" /></div>';
        output += '<div style="overflow: hidden; float: right; height: 9px;"><img alt="" src="http://cafef.vn/images/images/bottom_corn_bottom_right.gif" /></div>';
        output += '</div>';
        output += '</div>';
        
        document.getElementById('CafeF_BoxSearch').innerHTML = output;
    }
    
    this.ChangeSearchType = function(type)
    {
        this.SearchType = type;

        if (type == 1)
        {
            document.getElementById('CafeF_SearchKeyword_Company').style.display = 'none';
            document.getElementById('CafeF_SearchKeyword_News').style.display = 'block';
        }
        else
        {
            document.getElementById('CafeF_SearchKeyword_Company').style.display = 'block';
            document.getElementById('CafeF_SearchKeyword_News').style.display = 'none';
        }
    }
    
    this.Seach = function()
    {
        var keyword;
        
        if (this.SearchType == '1')
        {
            keyword = document.getElementById('CafeF_SearchKeyword_News').value;
            if (keyword == 'Tìm kiếm tin tức...') keyword = '';
            window.location = '/Search.aspx?TabRef=news&keyword=' + keyword;
        }
        else
        {
            keyword = document.getElementById('CafeF_SearchKeyword_Company').value;
            if (keyword == 'Gõ mã CK hoặc Tên công ty...') keyword = '';
            window.location = CafeF_JSLibrary.GetCompanyInfoLink(keyword);
        }
    }
    
    this.InitAutoComplete = function()
    {
        if (!this.IsAutocompleteInit)
        {
            jQuery('#CafeF_SearchKeyword_Company').autocomplete(oc, {
                minChars: 1,
                delay: 10,
                width: 300,
                matchContains: true,
                autoFill: false,
                formatItem: function(row) {
                    return row.c + " - " + row.m + "@" + row.o;
                    //return row.m + "@" + row.o;
                },
                formatResult: function(row) {
                    return row.c + " - " + row.m;
                    //return row.m;
                }
            });
        
            this.IsAutocompleteInit = true;
        }
    }
    
    this.CreateScriptObject = function(src, obj)
    {
        if (obj != null)
	    {
		    this.RemoveScriptObject(obj);
	    }
    	
	    obj = document.createElement('script');

        obj.setAttribute('type','text/javascript');
        obj.setAttribute('src', src);
        
        var head = document.getElementsByTagName('head')[0];
        head.appendChild(obj);
    }
    
    this.AppendScriptObject = function(script)
    {
	    var obj = document.createElement('script');

        obj.setAttribute('type','text/javascript');
        
        obj.appendChild(document.createTextNode(script));
        
        var head = document.getElementsByTagName('head')[0];
        head.appendChild(obj);
    }

    this.RemoveScriptObject = function(obj)
    {
	    obj.parentNode.removeChild(obj) ;
	    obj = null ;
    }
}

function CafeF_BoxSearch_OnEnter(e, searchType)
{
    if (!e) var e = window.event;
    
    if (e)
    {
        if (e.keyCode == 13)
        {
            e.cancelBubble = true;
            e.returnValue = false;
            e.cancel = true;
            
            var keyword;
    
            if (searchType == '1')
            {
                keyword = document.getElementById('CafeF_SearchKeyword_News').value;
                if (keyword == 'Tìm kiếm tin tức...') keyword = '';
                window.location = '/Search.aspx?TabRef=news&keyword=' + keyword;
            }
            else
            {
                keyword = document.getElementById('CafeF_SearchKeyword_Company').value;
                if (keyword == 'Gõ mã CK hoặc Tên công ty...') keyword = '';
                window.location = CafeF_JSLibrary.GetCompanyInfoLink(keyword);
            }
            return false;
        }
    }
    return true;
}

//document.write('<div id="CafeF_BoxSearch"></div>');
var container = document.getElementById('CafeF_BoxSearch');
var cafef_box_search;
if (container)
{
    cafef_box_search = new CafeF_BoxSearch('cafef_box_search');
    if (container.delay)
    {
        setTimeout('cafef_box_search.InitScript()', parseInt(container.delay));
    }
    else
    {
        cafef_box_search.InitScript();
    }
    if (!document.all)
    {
        setTimeout('cafef_box_search.InitAutoComplete()', 1000);
    }
}