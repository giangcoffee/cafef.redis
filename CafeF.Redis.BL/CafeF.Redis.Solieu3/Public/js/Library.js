function CafeF_Library()
{
    this.QueryString = function(key)
    {
        var strQueryString = window.location.search.substring(1);
        var values = strQueryString.split('&');
        for (i = 0; i < values.length; i++)
        {
            var value = values[i].split('=');
            if (value[0] == key)
            {
                return value[1];
            }
        }
        return '';
    }

    this.RefreshImage = function(imgId, url)
    {
        var chartImage = document.getElementById(imgId);
        
        var currentDateTime = new Date();
        
        chartImage.src = url + '?' + currentDateTime.getDate() + currentDateTime.getTime();
    }
    this.String2Float = function(value)
    {
        return (value != '' ? parseFloat(value) : 0);
    }

    this.Reload = function()
    {
        window.location.reload();
    }

    this.ltrim = function(str){
        if (str)
        {
            return str.toString().replace(/^\s+/, '');
        }
        else
        {
            return '';
        }
    }
    this.rtrim = function(str) {
        if (str)
        {
            return str.replace(/\s+$/, '');
        }
        else
        {
            return '';
        }
    }
    this.trim = function(str) {
        if (str)
        {
            return str.replace(/^\s+|\s+$/g, '');
        }
        else
        {
            return '';
        }
    }
    this.FormatNumber = function (number, decimals, decimalSeparator, thousandSeparator)
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
    
    this.CreateScriptObject = function(src)
    {
	    var script_object = document.createElement('script');

        script_object.setAttribute('type','text/javascript');
        script_object.setAttribute('src', src);
        
        var head = document.getElementsByTagName('head')[0];
        head.appendChild(script_object);
    }

    this.RemoveScriptObject = function(obj)
    {
        if(obj)
        {
	        obj.parentNode.removeChild(obj) ;
	        obj = null ;
	    }
    }
}

var CafeF_JSLibrary = new CafeF_Library();