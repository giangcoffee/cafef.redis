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
        var css = window.parent.document.createElement('link');
        css.type = 'text/css';
        css.rel = 'stylesheet';
        css.href = href;
        var head = window.parent.document.getElementsByTagName('head')[0];
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
        output += '<table style="width:100%; border:none; background-color: #F0F1F5; height:70px;"><tr><td>';
        output += '<table align="center" cellpadding="1" cellspacing="0">';
        output += '<tr><td>';
        output += '<input style="width:250px;" value="Gõ mã CK hoặc Tên công ty..." onblur="if (this.value == \'\') { this.value=\'Gõ mã CK hoặc Tên công ty...\'; }" onfocus="this.value = \'\'; window.frames[\'CafeF_EmbedDataRequest\'].' + this.instance_name + '.InitAutoComplete();" type="text" id="CafeF_SearchKeyword_Company" autocomplete="off" class="class_text_autocomplete ac_input" />';
        output += '<input style="width:250px; display: none;" value="Tìm kiếm tin tức..." onblur="if (this.value == \'\') { this.value=\'Tìm kiếm tin tức...\'; }" onfocus="this.value = \'\';" type="text" id="CafeF_SearchKeyword_News" />';
        output += '</td><td>';
        output += '<img onclick="window.frames[\'CafeF_EmbedDataRequest\'].' + this.instance_name + '.Seach()" alt="" src="' + this.script_folder + 'images/search.gif" /></td>';
        output += '</tr>';
        output += '<tr><td colspan="2" style="font-family: Arial; font-size: 12px;">';
        output += '&nbsp; &nbsp; <input type="radio" checked="checked" id="CafeF_BoxSearch_Type_Company" name="CafeF_BoxSearch_Type" onclick="window.frames[\'CafeF_EmbedDataRequest\'].' + this.instance_name + '.ChangeSearchType(0);" /> <label for="CafeF_BoxSearch_Type_Company">Công ty</label> &nbsp; &nbsp; &nbsp;';
        output += '&nbsp; &nbsp; <input type="radio" id="CafeF_BoxSearch_Type_News" name="CafeF_BoxSearch_Type" onclick="window.frames[\'CafeF_EmbedDataRequest\'].' + this.instance_name + '.ChangeSearchType(1);" /> <label for="CafeF_BoxSearch_Type_News">Tin tức</label>';
        output += '</td></tr>';
        output += '</table>';
        output += '</td></tr></table>';
        
        window.parent.document.getElementById('CafeF_BoxSearch').innerHTML = output;
    }
    
    this.ChangeSearchType = function(type)
    {
        this.SearchType = type;

        if (type == 1)
        {
            window.parent.document.getElementById('CafeF_SearchKeyword_Company').style.display = 'none';
            window.parent.document.getElementById('CafeF_SearchKeyword_News').style.display = 'block';
        }
        else
        {
            window.parent.document.getElementById('CafeF_SearchKeyword_Company').style.display = 'block';
            window.parent.document.getElementById('CafeF_SearchKeyword_News').style.display = 'none';
        }
    }
    
    this.Seach = function()
    {
        var keyword;
        if (this.SearchType == '1')
        {
            keyword = window.parent.document.getElementById('CafeF_SearchKeyword_News').value;
            if (keyword == 'Tìm kiếm tin tức...') keyword = '';
            window.location = '/Search.aspx?TabRef=news&keyword=' + keyword;
        }
        else
        {
            keyword = window.parent.document.getElementById('CafeF_SearchKeyword_Company').value;
            if (keyword == 'Gõ mã CK hoặc Tên công ty...') keyword = '';
            window.location = CafeF_JSLibrary.GetCompanyInfoLink(keyword);
        }
    }
    
    this.InitAutoComplete = function()
    {
        if (!this.IsAutocompleteInit)
        {
            window.parent.jQuery('#CafeF_SearchKeyword_Company').autocomplete(window.parent.oc, {
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
        }
    
        this.IsAutocompleteInit = true;
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

//window.parent.document.write('<div id="CafeF_BoxSearch"></div>');
var cafef_box_search;
var container = window.parent.document.getElementById('CafeF_BoxSearch');
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
}