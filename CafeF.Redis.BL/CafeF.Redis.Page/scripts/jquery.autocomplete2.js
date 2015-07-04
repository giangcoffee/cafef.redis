(function($) {
	
$.fn.extend({
	autocomplete: function(urlOrData, options) {
		var isUrl = typeof urlOrData == "string";
		options = $.extend({}, $.Autocompleter.defaults, {
			url: isUrl ? urlOrData : null,
			data: isUrl ? null : urlOrData,
			delay: isUrl ? $.Autocompleter.defaults.delay : 10,
			max: options && !options.scroll ? 5 : 150
		}, options);
		
		// if highlight is set to false, replace it with a do-nothing function
		options.highlight = options.highlight || function(value) { return value; };
		// if moreItems is false, replace it w/empty string
		options.moreItems = options.moreItems || "";
		
		return this.each(function() {
			new $.Autocompleter(this, options);
		});
	},
	result: function(handler) {
		return this.bind("result", handler);
	},
	search: function(handler) {
		return this.trigger("search", [handler]);
	},
	flushCache: function() {
		return this.trigger("flushCache");
	},
	setOptions: function(options){
		return this.trigger("setOptions", [options]);
	},
	unautocomplete: function() {
		return this.trigger("unautocomplete");
	}
});

$.Autocompleter = function(input, options) {

	var KEY = {
		UP: 38,
		DOWN: 40,
		DEL: 46,
		TAB: 9,
		RETURN: 13,
		ESC: 27,
		COMMA: 188,
		PAGEUP: 33,
		PAGEDOWN: 34
	};

	// Create $ object for input element
	var $input = $(input).attr("autocomplete", "off").addClass(options.inputClass);

	var timeout;
	var previousValue = "";
	var cache = $.Autocompleter.Cache(options);
	var hasFocus = 0;
	var lastKeyPressCode;
	var config = {
		mouseDownOnSelect: false
	};
	var select;
	
	if (options.isAddSymbolToFavorite)
	{
	    select = $.Autocompleter.Select(options, input, AddStockSymbolToFavorite, config);
	}
	else
	if(options.isTargetUrl)
	{
	   select = $.Autocompleter.Select(options, input, DoTarget, config); 
	}
	else
	if(options.TinChungKhoan)
	{
	    select = $.Autocompleter.Select(options, input, LoadTinChungKhoan, config); 
	}
	else
	{
	    select = $.Autocompleter.Select(options, input, selectCurrent, config);
	}
	
	
	$input.keydown(function(event) {
		// track last key pressed
		lastKeyPressCode = event.keyCode;
		switch(event.keyCode) {
		
			case KEY.UP:
				event.preventDefault();
				if ( select.visible() ) {
					select.prev();
				} else {
					onChange(0, true);
				}
				break;
				
			case KEY.DOWN:
				event.preventDefault();
				if ( select.visible() ) {
					select.next();
				} else {
					onChange(0, true);
				}
				break;
				
			case KEY.PAGEUP:
				event.preventDefault();
				if ( select.visible() ) {
					select.pageUp();
				} else {
					onChange(0, true);
				}
				break;
				
			case KEY.PAGEDOWN:
				event.preventDefault();
				if ( select.visible() ) {
					select.pageDown();
				} else {
					onChange(0, true);
				}
				break;
			
			// matches also semicolon
			case options.multiple && $.trim(options.multipleSeparator) == "," && KEY.COMMA:
			case KEY.TAB:
			case KEY.RETURN:
			    if (options.isAddSymbolToFavorite)
			    {
			        if (AddStockSymbolToFavorite())
			        {
			            // make sure to blur off the current field
					    //if( !options.multiple )
					    //{
						    //$input.blur();
						//}
					    event.preventDefault();
			        }
			        break;
			    }
			    if(options.isTargetUrl)
			    {
			        DoTarget();
			        event.preventDefault();
			        break;
			    }
			    if(options.TinChungKhoan)
			    {
			        LoadTinChungKhoan();
			        event.preventDefault();
			        break;
			    }
				if( selectCurrent() ){
					// make sure to blur off the current field
					if( !options.multiple )
						$input.blur();
					event.preventDefault();
				}
				break;
				
			case KEY.ESC:
				select.hide();
				break;
				
			default:
				clearTimeout(timeout);
				timeout = setTimeout(onChange, options.delay);
				break;
		}
	}).keypress(function() {
		// having fun with opera - remove this binding and Opera submits the form when we select an entry via return
	}).focus(function(){
		// track whether the field has focus, we shouldn't process any
		// results if the field no longer has focus
		hasFocus++;
	}).blur(function() {
		hasFocus = 0;
		if (!config.mouseDownOnSelect) {
			hideResults();
		}
	}).click(function() {
		// show select when clicking in a focused field
		if ( hasFocus++ > 1 && !select.visible() ) {
			onChange(0, true);
		}
	}).bind("search", function() {
		// TODO why not just specifying both arguments?
		var fn = (arguments.length > 1) ? arguments[1] : null;
		function findValueCallback(q, data) {
			var result;
			if( data && data.length ) {
				for (var i=0; i < data.length; i++) {
					if( data[i].result.toLowerCase() == q.toLowerCase() ) {
						result = data[i];
						break;
					}
				}
			}
			if( typeof fn == "function" ) fn(result);
			else $input.trigger("result", result && [result.data, result.value]);
		}
		$.each(trimWords($input.val()), function(i, value) {
			request(value, findValueCallback, findValueCallback);
		});
	}).bind("flushCache", function() {
		cache.flush();
	}).bind("setOptions", function() {
		$.extend(options, arguments[1]);
		// if we've updated the data, repopulate
		if ( "data" in arguments[1] )
			cache.populate();
	}).bind("unautocomplete", function() {
		select.unbind();
		$input.unbind();
	});
	
	
	function selectCurrent() {
		var selected = select.selected();
		if( !selected )
			return false;
		
		var v = selected.result;
		previousValue = v;
		
		if ( options.multiple ) {
			var words = trimWords($input.val());
			if ( words.length > 1 ) {
				v = words.slice(0, words.length - 1).join( options.multipleSeparator ) + options.multipleSeparator + v;
			}
			v += options.multipleSeparator;
		}
		
		$input.val(v);
		hideResultsNow();
		var gachngang = $.Autocompleter.gachNgang(selected.data.o);
		<!--$input.trigger("result", [selected.data, selected.value]);-->
		//document.location = "/CompInfo.chn?m=profile&symbol=" + selected.data.c;
		//document.location = "http://cafef.vn/DisplayContent.aspx?TabRef=CompInfo&m=profile&symbol=" + selected.data.c;
		//document.location = "http://cafef.vn/Thi-truong-niem-yet/Thong-tin-cong-ty/" + selected.data.c+".chn";
		//window.open("http://cafef.vn/DisplayContent.aspx?TabRef=CompInfo&m=profile&symbol=" + selected.data.c);
		if(options.LSK)
		{
		}
		else
		if( options.BCTC)
		{
		    //document.location='/BCTCFull/BCTCFull.aspx?symbol='+selected.data.c+"&nhom="+document.getElementById("hdNhom").value+"&type="+document.getElementById("hdType").value+"&quy="+document.getElementById("hdIsQuy").value;
		    document.location='/BCTCFull/BCTCFull.aspx?symbol='+selected.data.c+"&type=1&nhom="+document.getElementById("hdNhom").value+"&quy="+document.getElementById("hdIsQuy").value;
		}
		else		
		if(!options.Portfolio)
		{
		    //document.location =autocomplete_GetCompanyInfoLink(selected.data.c);// "http://cafef.vn/Thi-truong-niem-yet/Thong-tin-cong-ty/" + selected.data.c+".chn";
		   if(!options.GDNB && !options.tochuc)
		    {
		        document.location =autocomplete_GetCompanyInfoLink(selected.data.c);// "http://cafef.vn/Thi-truong-niem-yet/Thong-tin-cong-ty/" + selected.data.c+".chn";
		    }
		    else if(!options.tochuc)
{
var url=window.location.href;
//var host=window.location.host;
//var i=url.indexOf('Lich-su-giao-dich-');
//var tab=url.substring(i+18,url.indexOf('.chn');
//document.location='http://'+host+'/Lich-su-giao-dich-Symbol-'+selected.data.c+'/Trang-1-0-tab-'+tab+'.chn';
var i = url.indexOf('/Lich-su-giao-dich-')+19;
			var len = url.indexOf('.chn');
			var s = url.substring(i,len);
			if(url.indexOf('/Lich-su-giao-dich-ALL-4')>0){
			    document.location = '/Lich-su-giao-dich-'+selected.data.c+'-4.chn'
			}else{
			    document.location = '/Lich-su-giao-dich-'+selected.data.c+'-'+s.substring(s.lastIndexOf('-')+1,s.length)+'.chn';
			}
}
else
{
document.location='/Lich-su-giao-dich-ALL-4/trang-1-'+selected.data.i+'.chn';
}
		}
		else
		{
		    if (options.NextFocusControlId != '')
		    {
		        var nextControl = document.getElementById(options.NextFocusControlId);
		        if (nextControl)
		        {
		            nextControl.select();
		            nextControl.focus();
		        }
		    }
		}
		return true;
	}
	
	/*
	Chuc nang them 1 ma CK vao danh sach theo doi
	Can include them 2 file cookie.js va StockSymbolSlide.js
	*/
	function AddStockSymbolToFavorite() {
		var selected = select.selected();
		
		if( !selected )
			return false;
		
		var v = selected.result;
		previousValue = v;
		
		if ( options.multiple ) {
			var words = trimWords($input.val());
			if ( words.length > 1 ) {
				v = words.slice(0, words.length - 1).join( options.multipleSeparator ) + options.multipleSeparator + v;
			}
			v += options.multipleSeparator;
		}
		
		$input.val(v);
		hideResultsNow();
		var gachngang = $.Autocompleter.gachNgang(selected.data.o);
		<!--$input.trigger("result", [selected.data, selected.value]);-->
		//alert(selected.data.c);
		options.CafeF_StockSymbolSlideObject.AddSymbolToFavorite(selected.data.c);
		//CurrentAutoCompleteSymbol = selected.data.c;
		return true;
	}
	/* ============================================ */
	function DoTarget() {
		var selected = select.selected();
		if( !selected )
			return false;
		
		var v = selected.result;
		previousValue = v;
		
		if ( options.multiple ) {
			var words = trimWords($input.val());
			if ( words.length > 1 ) {
				v = words.slice(0, words.length - 1).join( options.multipleSeparator ) + options.multipleSeparator + v;
			}
			v += options.multipleSeparator;
		}
		
		$input.val(v);
		hideResultsNow();
		var gachngang = $.Autocompleter.gachNgang(selected.data.o);
		<!--$input.trigger("result", [selected.data, selected.value]);-->
		//window.open("http://cafef.vn/DisplayContent.aspx?TabRef=CompInfo&m=profile&symbol=" + selected.data.c);
		//document.location = "http://cafef.vn/DisplayContent.aspx?TabRef=CompInfo&m=profile&symbol=" + selected.data.c;
		document.location = "http://cafef.vn/Thi-truong-niem-yet/Thong-tin-cong-ty/" + selected.data.c+".chn";
		return false;
	}
	/* tn chung khoan */
	function LoadTinChungKhoan() {
		var selected = select.selected();
		if( !selected )
			return false;
		
		var v = selected.result;
		previousValue = v;
		
		if ( options.multiple ) {
			var words = trimWords($input.val());
			if ( words.length > 1 ) {
				v = words.slice(0, words.length - 1).join( options.multipleSeparator ) + options.multipleSeparator + v;
			}
			v += options.multipleSeparator;
		}
		
		$input.val(v);
		hideResultsNow();
		var gachngang = $.Autocompleter.gachNgang(selected.data.o);
		<!--$input.trigger("result", [selected.data, selected.value]);-->
		//load tin chung khoan
		//CafeF_makeFrame(selected.data.c);
		ResultSearchURL +='?CafeF_'+selected.data.c;
		document.location=ResultSearchURL;
		return true;
	}
	function CafeF_makeFrame(__Stock) 
    {               
       //var url="http://cafef.vn/Thi-truong-niem-yet/Thong-tin-cong-ty/"+__Stock+".chn";
       var url="http://cafef.vn/cafef-tools/chungkhoan24h/Companyinfo.aspx?symbol="+__Stock;
       var tDiv = document.getElementById('CafeF_ResultSearch'); 
       
       if(tDiv.hasChildNodes())
       {
         var child=tDiv.childNodes[0];
         tDiv.removeChild(child);        
       }
       if (!tDiv.hasChildNodes())
       {
           tDiv.style.height=document.body.scrollHeight+300+"px";//'900px';
           ifrm = document.createElement("IFRAME");
           ifrm.setAttribute("src", url);
           ifrm.setAttribute("ID", 'Cafef_Frame');
           //ifrm.setAttribute("onload", adjustMyFrameHeight());
           ifrm.style.width = "100%";
           ifrm.style.height =tDiv.style.height; //document.body.scrollHeight+"px";//"100%";
           ifrm.style.overflow='hidden';
           ifrm.scrolling='no';
           ifrm.frameBorder='0';
           ifrm.style.border='0';
           ifrm.scrolling='no';                        
           tDiv.appendChild(ifrm);
           //alert(ifrm.contentWindow.scrollHeight);
           
       }
      
    }
	/*======================================*/
	function onChange(crap, skipPrevCheck) {
		if( lastKeyPressCode == KEY.DEL ) {
			select.hide();
			return;
		}
		
		var currentValue = $input.val();
		
		if ( !skipPrevCheck && currentValue == previousValue )
			return;
		
		previousValue = currentValue;
		
		currentValue = lastWord(currentValue);
		if ( currentValue.length >= options.minChars) {
			$input.addClass(options.loadingClass);
			if (!options.matchCase)
				currentValue = currentValue.toLowerCase();
			request(currentValue, receiveData, hideResultsNow);
		} else {
			stopLoading();
			select.hide();
		}
	};
	
	function trimWords(value) {
		if ( !value ) {
			return [""];
		}
		var words = value.split( $.trim( options.multipleSeparator ) );
		var result = [];
		$.each(words, function(i, value) {
			if ( $.trim(value) )
				result[i] = $.trim(value);
		});
		return result;
	}
	
	function lastWord(value) {
		if ( !options.multiple )
			return value;
		var words = trimWords(value);
		return words[words.length - 1];
	}
	
	// fills in the input box w/the first match (assumed to be the best match)
	function autoFill(q, sValue){
		// autofill in the complete box w/the first match as long as the user hasn't entered in more data
		// if the last user key pressed was backspace, don't autofill
		if( options.autoFill && (lastWord($input.val()).toLowerCase() == q.toLowerCase()) && lastKeyPressCode != 8 ) {
			// fill in the value (keep the case the user has typed)
			$input.val($input.val() + sValue.substring(lastWord(previousValue).length));
			// select the portion of the value not typed by the user (so the next character will erase)
			$.Autocompleter.Selection(input, previousValue.length, previousValue.length + sValue.length);
		}
	};

	function hideResults() {
		clearTimeout(timeout);
		timeout = setTimeout(hideResultsNow, 200);
	};

	function hideResultsNow() {
		select.hide();
		clearTimeout(timeout);
		stopLoading();
		if (options.mustMatch) {
			// call search and run callback
			$input.search(
				function (result){
					// if no value found, clear the input box
					if( !result ) $input.val("");
				}
			);
		}
	};

	function receiveData(q, data) {
		if ( data && data.length && hasFocus ) {
			stopLoading();
			select.display(data, q);
			autoFill(q, data[0].value);
			select.show();
		} else {
			hideResultsNow();
		}
	};

	function request(term, success, failure) {
		if (!options.matchCase)
			term = term.toLowerCase();
		var data = cache.load(term);
		// recieve the cached data
		if (data && data.length) {
			success(term, data);
		// if an AJAX url has been supplied, try loading the data now
		} else if( (typeof options.url == "string") && (options.url.length > 0) ){
			
			var extraParams = {};
			$.each(options.extraParams, function(key, param) {
				extraParams[key] = typeof param == "function" ? param() : param;
			});
			
			$.ajax({
				// try to leverage ajaxQueue plugin to abort previous requests
				mode: "abort",
				// limit abortion to this input
				port: "autocomplete" + input.name,
				dataType: options.dataType,
				url: options.url,
				data: $.extend({
					q: lastWord(term),
					limit: options.max
				}, extraParams),
				success: function(data) {
					var parsed = options.parse && options.parse(data) || parse(data);
					cache.add(term, parsed);
					success(term, parsed);
				}
			});
		} else {
			failure(term);
		}
	};
	
	function parse(data) {
		var parsed = [];
		var rows = data.split("\n");
		for (var i=0; i < rows.length; i++) {
			var row = $.trim(rows[i]);
			if (row) {
				row = row.split("|");
				parsed[parsed.length] = {
					data: row,
					value: row[0],
					result: options.formatResult && options.formatResult(row, row[0]) || row[0]
				};
			}
		}
		return parsed;
	};

	function stopLoading() {
		$input.removeClass(options.loadingClass);
	};

};

$.Autocompleter.gachNgang = function(keyword)
{
	var len = keyword.length;
	var str = '', c;
	for(i=0; i < len; i++)
	{
		c = keyword.charCodeAt(i);
		str += (c == 32)? '-' : keyword.charAt(i);
	}
	var st = str.indexOf('---');
	if (st!=-1) str = str.substring(0,st) + '-' + str.substring(st+3);
	return str;
}

$.Autocompleter.supercut = function(keyword)
{
	var len = keyword.length;
	var str = '', c;
	for(i=0; i < len; i++)
	{
		c = keyword.charCodeAt(i);
		// alert(c);
		

		if(( c>= 192 && c <= 195) || ( c>= 224 && c <= 227) || c==258 || c==259 || ( c>= 461 && c <= 7863))
		{
			str+='a';
		}else
			if(c==272 || c==273 )
			{
				str+='d';
			}else
				if((c>=200 && c<=202) || (c>=232 && c<=234) || ( c>= 7864 && c <= 7879))
				{
					str+='e';
				}else
					if(c==204 || c==205 ||c==236 || c==237 ||c==296 || c==297 || ( c>= 7880 && c <= 7883))
					{
						str+='i';
					}else
						if(c==217 || c==218 || c==249 || c==250 || c==360 || c==361 || c==431 || c==432 || ( c>= 7908 && c <= 7921))
						{
							str+='u';
						}else
							if((c>=210 && c<=213) || (c>=242 && c<=245) || c==416 || c==417 || ( c>= 7884 && c <= 7907))
							{
								str+='o';
							} else
								if(c==221 || c==253 || (c>= 7922 && c <= 7929))
								{
									str+='y';
								} else
									str+= keyword.charAt(i);

	}

	return str;
}

$.Autocompleter.checkdau = function(sText)
{
	var ValidChars = "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";
	var codau = false;
	var Char;
	
	for (i = 0; i < sText.length && codau == false; i++) 
	  { 
	  Char = sText.charAt(i); 
	  if (ValidChars.indexOf(Char) != -1) 
		 {
		 codau = true;
		 }
	  }
	return codau;
}
$.Autocompleter.defaults = {
	inputClass: "ac_input",
	resultsClass: "ac_results",
	loadingClass: "ac_loading",
	minChars: 1,
	delay: 400,
	matchCase: false,
	matchSubset: true,
	matchContains: false,
	cacheLength: 10,
	max: 100,
	mustMatch: false,
	extraParams: {},
	selectFirst: true,
	formatItem: function(row) { return row[0]; },
	//moreItems: "&#x25be;&#x25be;&#x25be; more &#x25be;&#x25be;&#x25be;",
	moreItems: "",
	autoFill: false,
	width: 0,
	multiple: false,	
	Portfolio:false,
	BCTC:false,
	GDNB:false,
	LSK:false,
	tochuc:false,
	NextFocusControlId:'',
	multipleSeparator: ", ",
	highlight: function(value, term) {
		var kby = value.toLowerCase().indexOf( "@" );
		value = value.substring(0,kby);
		if ( $.Autocompleter.checkdau( term.toLowerCase() ) == true ){
			var st = value.toLowerCase().indexOf( term.toLowerCase() );
		}
		else {
			var kby = $.Autocompleter.supercut(value);
			var st = kby.toLowerCase().indexOf(term.toLowerCase() );
		}
		var output = value.substring(0,st) + "<em>" + value.substring(st, st+term.length) + "</em>" + value.substring(st+term.length);
		
		return output;
	},
    scroll: false,
    scrollHeight: 180,
	attachTo: 'body',
	isTargetUrl:false,
	TinChungKhoan:false,
	isAddSymbolToFavorite: false,
	CafeF_StockSymbolSlideObject: null
};

$.Autocompleter.Cache = function(options) {

	var data = {};
	var length = 0;
	
	function matchSubset(s, sub) {
		if (!options.matchCase) 
			s = s.toLowerCase();
		var i = s.indexOf(sub);
		if (i == -1) return false;
		return i == 0 || options.matchContains;
	};
	
	function add(q, value) {
		if (length > options.cacheLength){
			flush();
		}
		if (!data[q]){ 
			length++;
		}
		data[q] = value;
	}
	
	function populate(){
		if( !options.data ) return false;
		// track the matches
		var stMatchSets = {},
			nullData = 0;

		// no url was specified, we need to adjust the cache length to make sure it fits the local data store
		if( !options.url ) options.cacheLength = 1;
		
		// track all options for minChars = 0
		stMatchSets[""] = [];
		
		// loop through the array and create a lookup structure
		for ( var i = 0, ol = options.data.length; i < ol; i++ ) {
			var rawValue = options.data[i];
			// if rawValue is a string, make an array otherwise just reference the array
			rawValue = (typeof rawValue == "string") ? [rawValue] : rawValue;
			
			var value = options.formatItem(rawValue, i+1, options.data.length);
			if ( value === false )
				continue;
				
			var firstChar = value.charAt(0).toLowerCase();
			// if no lookup array for this character exists, look it up now
			if( !stMatchSets[firstChar] ) 
				stMatchSets[firstChar] = [];

			// if the match is a string
			var row = {
				value: value,
				data: rawValue,
				result: options.formatResult && options.formatResult(rawValue) || value
			};
			
			// push the current match into the set list
			stMatchSets[firstChar].push(row);

			// keep track of minChars zero items
			if ( nullData++ < options.max ) {
				stMatchSets[""].push(row);
			}
		};

		// add the data items to the cache
		$.each(stMatchSets, function(i, value) {
			// increase the cache size
			options.cacheLength++;
			// add to the cache
			add(i, value);
		});
	}
	
	// populate any existing data
	setTimeout(populate, 25);
	
	function flush(){
		data = {};
		length = 0;
	}
	
	return {
		flush: flush,
		add: add,
		populate: populate,
		load: function(q) {
			if (!options.cacheLength || !length)
				return null;
			/* 
			 * if dealing w/local data and matchContains than we must make sure
			 * to loop through all the data collections looking for matches
			 */
			if( !options.url && options.matchContains ){
				// track all matches
				var csub = [];
				// loop through all the data grids for matches
				for( var k in data ){
					// don't search through the stMatchSets[""] (minChars: 0) cache
					// this prevents duplicates
					if( k.length > 0 ){
						var c = data[k];
						$.each(c, function(i, x) {
							// if we've got a match, add it to the array
							if (matchSubset(x.value, q)) {
								csub.push(x);
							}
						});
					}
				}				
				return csub;
			} else 
			// if the exact item exists, use it
			if (data[q]){
				return data[q];
			} else
			if (options.matchSubset) {

				for (var i = q.length - 1; i >= options.minChars; i--) {
					var c = data[q.substr(0, i)];
					if (c) {
						var csub = [];
						$.each(c, function(i, x) {
							if (matchSubset(x.value, q)) {
								csub[csub.length] = x;
							}
						});
						return csub;
					}
				}
			}
			return null;
		}
	};
};

$.Autocompleter.Select = function (options, input, select, config) {
	var CLASSES = {
		ACTIVE: "ac_over"
	};
	
	var listItems,
		active = -1,
		data,
		term = "",
		needsInit = true,
		element,
		list,
		moreItems;
	
	// Create results
	function init() {
		if (!needsInit)
			return;
		element = $("<div>")
		.hide()
		.addClass(options.resultsClass)
		.css("position", "absolute")
		//.css("overflow","visible")
		.appendTo(options.attachTo);
	
		list = $("<ul>").appendTo(element).mouseover( function(event) {
			if(target(event).nodeName && target(event).nodeName.toUpperCase() == 'LI') {
	            active = $("li", list).removeClass().index(target(event));
			    $(target(event)).addClass(CLASSES.ACTIVE);            
	        }
		}).click(function(event) {
			$(target(event)).addClass(CLASSES.ACTIVE);
			select();
			input.focus();
			return false;
		}).mousedown(function() {
			config.mouseDownOnSelect = true;
		}).mouseup(function() {
			config.mouseDownOnSelect = false;
		});
		
		if( options.moreItems.length > 0 ) 
		moreItems = $("<div>")
			.addClass("ac_moreItems")
			.css("display", "none")
			//.//css("overflow","visible")
			.html(options.moreItems)
			.appendTo(element);
		
		if( options.width > 0 )
			element.css("width", options.width);
			
        LogoItems = $("<div>")
		    .html("<img src='http://solieu6.vcmedia.vn/www/vneconomy/images/img_cafef.gif'>")
		    .css("text-align","center")
		    .css("background-color","#FFFFFF")
		    .css("width",options.width)
		    .appendTo(element);			
		needsInit = false;
	} 
	
	function target(event) {
		var element = event.target;
		while(element && element.tagName != "LI")
			element = element.parentNode;
		// more fun with IE, sometimes event.target is empty, just ignore it then
		if(!element)
			return [];
		return element;
	}

	function moveSelect(step) {
		listItems.slice(active, active + 1).removeClass();
		movePosition(step);
        var activeItem = listItems.slice(active, active + 1).addClass(CLASSES.ACTIVE);
        if(options.scroll) {
            var offset = 0;
            listItems.slice(0, active).each(function() {
				offset += this.offsetHeight;
			});
            if((offset + activeItem[0].offsetHeight - list.scrollTop()) > list[0].clientHeight) {
                list.scrollTop(offset + activeItem[0].offsetHeight - list.innerHeight());
            } else if(offset < list.scrollTop()) {
                list.scrollTop(offset);
            }
        }
	};
	
	function movePosition(step) {
		active += step;
		if (active < 0) {
			active = listItems.size() - 1;
		} else if (active >= listItems.size()) {
			active = 0;
		}
	}
	
	function limitNumberOfItems(available) {
		return options.max && options.max < available
			? options.max
			: available;
	}
	
	function fillList() {
	    list.empty(); 
        var max = limitNumberOfItems(data.length); 
        var realMax = 0; 
        var NumItemSelected=0;
        var str="";
        for (var i=0; i < data.length; i++) {
            if (!data[i]) continue; 
            var formatted = options.formatItem(data[i].data, i+1, max, data[i].value, term); 
            if ( formatted === false )continue; 
            var arr = formatted.split('@'); 
            if (arr[0].toLowerCase().indexOf(term.toLowerCase()) == 0) 
            {         
                var li = $("<li>").html( options.highlight(formatted, term) ).appendTo(list)[0];
                $.data(li, "ac_data", data[i]);
                str+= i +",";
                NumItemSelected ++;
            }
            if(NumItemSelected==max) break;
        } 
        if(NumItemSelected<max)
        {
            for (var i=0; i < max; i++) {
            if (!data[i]) continue; 
            var formatted = options.formatItem(data[i].data, i+1, max, data[i].value, term); 
            if ( formatted === false )continue; 
            var arr = formatted.split('@'); 
            if(notinarray(str,i))
                {
                var li = $("<li>").html( options.highlight(formatted, term) ).appendTo(list)[0];
                $.data(li, "ac_data", data[i]);
            }
            } 
        }
		listItems = list.find("li");
		if ( options.selectFirst ) {
			listItems.slice(0, 1).addClass(CLASSES.ACTIVE);
			active = 0;
		}
		if( options.moreItems.length > 0 && !options.scroll)
			moreItems.css("display", (data.length > max)? "block" : "none");
		list.bgiframe();
	}
	
	function notinarray(string, i)
    { 
        var ids = string.split(',');
        for(j=0;j<ids.length-1;j++)
        {
        if(i==ids[j]) 
        {
        //alert(ids[i]);
        return false;
        }
        }
        return true;
    }
	
	return {
		display: function(d, q) {
			init();
			data = d;
			term = q;
			fillList();
		},
		next: function() {
			moveSelect(1);
		},
		prev: function() {
			moveSelect(-1);
		},
		pageUp: function() {
			if (active != 0 && active - 8 < 0) {
				moveSelect( -active );
			} else {
				moveSelect(-8);
			}
		},
		pageDown: function() {
			if (active != listItems.size() - 1 && active + 8 > listItems.size()) {
				moveSelect( listItems.size() - 1 - active );
			} else {
				moveSelect(8);
			}
		},
		hide: function() {
			element && element.hide();
			active = -1;
		},
		visible : function() {
			return element && element.is(":visible");
		},
		current: function() {
			return this.visible() && (listItems.filter("." + CLASSES.ACTIVE)[0] || options.selectFirst && listItems[0]);
		},
		show: function() {
			var offset = $(input).offset();
			element.css({
				width: typeof options.width == "string" || options.width > 0 ? options.width : $(input).width(),
				top: offset.top + input.offsetHeight,
				left: offset.left
			}).show();
            if(options.scroll) {
                list.scrollTop(0);
                list.css({
					maxHeight: options.scrollHeight,
					overflow: 'auto'
				});
				
                if($.browser.msie && typeof document.body.style.maxHeight === "undefined") {
					var listHeight = 0;
					listItems.each(function() {
						listHeight += this.offsetHeight;
					});
					var scrollbarsVisible = listHeight > options.scrollHeight;
                    list.css('height', scrollbarsVisible ? options.scrollHeight : listHeight );
					if (!scrollbarsVisible) {
						// IE doesn't recalculate width when scrollbar disappears
						listItems.width( list.width() - parseInt(listItems.css("padding-left")) - parseInt(listItems.css("padding-right")) );
					}
                }
                
            }
		},
		selected: function() {
			return listItems && $.data(listItems.filter("." + CLASSES.ACTIVE)[0], "ac_data");
		},
		unbind: function() {
			element && element.remove();
		}
	};
};

$.Autocompleter.Selection = function(field, start, end) {
	if( field.createTextRange ){
		var selRange = field.createTextRange();
		selRange.collapse(true);
		selRange.moveStart("character", start);
		selRange.moveEnd("character", end);
		selRange.select();
	} else if( field.setSelectionRange ){
		field.setSelectionRange(start, end);
	} else {
		if( field.selectionStart ){
			field.selectionStart = start;
			field.selectionEnd = end;
		}
	}
	field.focus();
};

})(jQuery);

///
function autocomplete_GetCompanyInfoLink(sym)
{
    var link='';
    //oc[i].i=1=> hose
    for (i=0;i<oc.length;i++)
    {
        if( oc[i].c.toLowerCase()==sym.toLowerCase())
        {
            var san='hose';
            if(oc[i].san=='2') san='hastc';
            var cName=UnicodeToKoDauAndGach(oc[i].m);
            link='http://cafef.vn/'+san+'/'+sym+'-'+cName+'.chn';
            
            if(sym == "VNINDEX")
                link = 'http://cafef.vn/Lich-su-giao-dich-VNINDEX-1.chn';
            if(sym == 'HNX-INDEX')
                link = 'http://cafef.vn/Lich-su-giao-dich-HNXINDEX-1.chn';
            if(sym == 'UPCOM-INDEX')
                link = 'http://cafef.vn/Lich-su-giao-dich-UPCOMINDEX-1.chn';
        }
    }
    return link;
}
var KoDauChars = 'aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIOOOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU';
var uniChars = 'àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ';

function UnicodeToKoDau(input)
{
    //alert(input);
    var retVal = '';
    var s = input.split('');
    
    var arr_KoDauChars = KoDauChars.split('');
    
    var pos;
    for (var i = 0; i < s.length; i++)
    {
        pos = uniChars.indexOf(s[i]);
        if (pos >= 0)
            retVal += arr_KoDauChars[pos];
        else
            retVal += s[i];
    }
    return retVal;
}

function UnicodeToKoDauAndGach(input)
{
    //alert(input);
    var strChar = 'abcdefghiklmnopqrstxyzuvxw0123456789 ';

    var str = input.replace("–", "");
    str = str.replace("  ", " ");
	str = str.replace("(", " ");
    str = UnicodeToKoDau(str.toLowerCase());
    
    var s = str.split('');
    var sReturn = "";
    for (var  i = 0; i < s.length; i++)
    {
        if (strChar.indexOf(s[i]) > -1)
        {
            if (s[i] != ' ')
                sReturn += s[i];
            else if (i > 0 && s[i - 1] != ' ' && s[i - 1] != '-' && i<s.length-1)
                sReturn += "-";
        }
    }

    return sReturn;
}

function autocomplete_GetCompanyName(sym)
{
    var link='';    
    for (i=0;i<oc.length;i++)
    {
        if( oc[i].c.toLowerCase()==sym.toLowerCase())
        {
            var san='hose';
            if(oc[i].san=='2') san='hastc';
            var FullName=sym+' - ' + oc[i].m;
            var cName=UnicodeToKoDauAndGach(oc[i].m);
            link="<a href=\"http:\/\/cafef.vn\/"+san+"\/"+sym+"-" +cName+".chn\" target=\"_blank\">"+FullName+"</a>";
        }
    }
    return link;
}