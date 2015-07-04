//var change=true;
//cometd
//(function($)
//{

var isTrading = (servertime >= 80000 && servertime <= 120000);
function ReloadCometd() {
    servertime += 120;
    if ((servertime % 100) >= 60) { servertime += 40; }
    if ((servertime % 10000) >= 6000) { servertime += 4000; }
    if (servertime >= 240000) { servertime -= 240000 };
    if (servertime >= 80000 && servertime <= 120000) {
        /*if (!isTrading) chart.join();*/
        isTrading = true;
    } else {
        isTrading = false;
    }
}
setInterval("ReloadCometd()", 30000);

function _Chart(state) {
    var _self = this,
        	_wasConnected = false,
    		_connected = false,
    		_clientKey, _detailKey,
    		_cookieName = 'favorite_stocks',
            _stateCookiesName = 'favorite_stocks_state',
    //_lastUser,
    		_disconnecting,
    		_chatSubscription,
    		_membersSubscription,
    		_centerSubscription,
    		_indexSubscription;

    this.explode = function(delimiter, string, limit) {
        var emptyArray = { 0: '' };
        if (arguments.length < 2 || typeof arguments[0] == 'undefined' || typeof arguments[1] == 'undefined') {
            return null;
        }
        if (delimiter === '' || delimiter === false || delimiter === null) {
            return false;
        }
        if (typeof delimiter == 'function' || typeof delimiter == 'object' || typeof string == 'function' || typeof string == 'object') {
            return emptyArray;
        }
        if (delimiter === true) {
            delimiter = '1';
        }
        if (!limit) {
            return string.toString().split(delimiter.toString());
        } else {
            var splitted = string.toString().split(delimiter.toString()), partA = splitted.splice(0, limit - 1), partB = splitted.join(delimiter.toString());
            if (typeof (partB) != 'undefined' || partB != '' || partB > 0) {
                partA.push(partB);
            }
            return partA;
        }
    };
    this.join = function() {
        _disconnecting = false;
        _clientKey = [];
        if ((org.cometd.COOKIE ? org.cometd.COOKIE.get(_stateCookiesName) : "0") == "1") {
            var list = (org.cometd.COOKIE ? org.cometd.COOKIE.get(_cookieName) : "");
            if (list == null) list = '';
            if (list != '') {
                var listOfSymbol = list.split(';');
                for (var i = 0; i < listOfSymbol.length; i++) {
                    _clientKey.push(listOfSymbol[i]);
                }
            }
        }
        _detailKey = [];
        if (_symbol != '') _detailKey.push(_symbol);
        if (_clientKey.length > 0 || _detailKey.length > 0) {
            var cometdURL = "http://push.cafef.vcmedia.vn/compush/cometd";
            $.cometd.configure({
                url: cometdURL/*,
                    logLevel: 'debug'*/
            });
            $.cometd.handshake();
        } else {
            $.cometd.batch(function() {
                _unsubscribe();
            });
            $.cometd.disconnect();
            _clientKey = null;
            //_lastUser = null;
            _disconnecting = true;
        }
    };

    this.leave = function() {
        $.cometd.batch(function() {
            _unsubscribe();
        });
        $.cometd.disconnect();
        _clientKey = null;
        //_lastUser = null;
        _disconnecting = true;
    };

    this.send = function() {       /*     
        	$.cometd.publish("/service/magazine", {
                user: _username,
                chat: ''
            });
			*/
    };

    //Receive data -- stock bar
    this.receive = function(message) {
        var chanCont = '',
		   		_pKey = eval(message)['data']['codes'],
				_pKAr = _self.explode(';', _pKey), 		//_self.explode(',',_pKey),
				_pKaLen = _pKAr.length;

        var arrStock = _pKey.split('|');
        var _pCont;
        if (_pKaLen > 0) {

            if (isTrading == false && (_symbol == '')) {
                $.cometd.batch(function() {
                    _unsubscribe();
                });
                $.cometd.disconnect();
                _clientKey = null;
                //_lastUser = null;
                _disconnecting = true;
            }
            //            if (isTrading == false) { return; }
        }
        for (c = 0; c < _pKaLen; c++) {
            //if(c==0) alert(_pKAr[0]);
            _pKAr[c] = _pKAr[c].replace(/[\(\)\/}\{\][*]/g, "");
            //_pKAr[c]=_pKAr[c].replace(/[\(\)\/}\{\][+]/g, "");			

            _pCont = _self.explode('|', _pKAr[c]).splice(0, 7);

            //AAA|0|43|565400|43.3|0|40.3|24498980000|46.3|565400

            var _daT = "",

					_pCLen = _pCont.length,
					_pcStl = '_pcNor';

            var _sym = _pCont[0].toUpperCase(),
					_san = _pCont[1],
					_price = _pCont[2],
					_volume = _pCont[3],
					_change = _pCont[4],
					_cssNo = _pCont[5],
					_percent = _pCont[6],
					_css;
            switch (_cssNo) {
                case "1": _css = "Floor"; break;
                case "2": _css = "Down"; break;
                case "3": _css = "NoChange"; break;
                case "4": _css = "Up"; break;
                case "5": _css = "Ceiling"; break;
                default: _css = "NoChange"; break;
            }
            //var _percent = (parseFloat(eval(_change.replace('.',''))) / eval(_price.replace('.','')) * 100);

            //change color when different
            var bp = (_price != $('#slide_price_' + _sym + '').html());
            var bc = (_change != $('#slide_change_' + _sym + '').html());
            var bcp = (("(" + _percent + "%)") != $('#slide_percent_' + _sym + '').html());

            //update data
            $('#slide_price_' + _sym + '').html(_price);
            $('#slide_change_' + _sym + '').html(_change).attr('class', _css);
            $('#slide_percent_' + _sym + '').html("(" + _percent + "%)").attr('class', _css); ;
            $('#slide_image_' + _sym + '').attr('src', 'http://cafef3.vcmedia.vn/solieu/solieu3/images/' + (_percent > 0 ? "up" : (_percent < 0 ? "down" : "nochange")) + '.gif');

            if (bp) { $('#slide_price_' + _sym + '').css('background-color', '#FF6A00'); }
            window.setTimeout("$('#slide_price_" + _sym + "').css('background-color','');", 1000);
            if (bc) { $('#slide_change_' + _sym + '').css('background-color', '#FF6A00'); }
            window.setTimeout("$('#slide_change_" + _sym + "').css('background-color','');", 1000);
            if (bcp) { $('#slide_percent_' + _sym + '').css('background-color', '#FF6A00'); }
            window.setTimeout("$('#slide_percent_" + _sym + "').css('background-color','');", 1000);
        }
        // There seems to be no easy way in jQuery to handle the scrollTop property
        //Chart[0].scrollTop = Chart[0].scrollHeight - Chart.outerHeight();
    };

    //receive center data
    this.receiveIndex = function(message) {
        var chanCont = '';
        _pKey = eval(message)['data']["report"];
        _pKAr = _self.explode(';', _pKey);
        _pKaLen = _pKAr.length;
        //var arrStock = _pKey.split('|');	    
        var _pCont;

        if (_pKaLen > 0) {
            if (isTrading == false) {
                $.cometd.batch(function() {
                    _unsubscribe();
                });
                $.cometd.disconnect();
                _clientKey = null;
                //_lastUser = null;
                _disconnecting = true;
            }
        }

        for (c = 0; c < _pKaLen; c++) {
            _pKAr[c] = _pKAr[c].replace(/[\(\)\/}\{\][+]/g, "");

            _pCont = _self.explode('|', _pKAr[c]).splice(0, 11);

            //[{HNXINDEX|111.319999694824|-1.23000335693359|-1.09285057626161|414.754|20941774|73|5|60|213|29}]
            var _daT = "",

					_pCLen = _pCont.length,
					_pcStl = '_pcNor';

            var _center = _pCont[0] == "HNXINDEX" ? "2" : "1",
				    _index = eval(_pCont[1]),
				    _change = eval(_pCont[2]),
				    _percent = eval(_pCont[3]),
				    _value = Math.round(eval(_pCont[4])),
				    _volume = eval(_pCont[5]),
				    _tang = eval(_pCont[6]),
				    _tran = eval(_pCont[7]),
				    _ngang = eval(_pCont[8]),
				    _giam = eval(_pCont[9]),
				    _san = eval(_pCont[10]);

            /*index on menu*/
            var sign = ""; if (_change > 0) sign = "+";
            var cls = "down"; if (_change > 0) cls = "up";
            var img = "no_change"; if (_change < 0) cls = "btdown"; if (_change > 0) cls = "btup";
            var str = "<b>" + _index.toFixed(2) + "</b> " + sign + _change.toFixed(2) + " (" + sign + _percent.toFixed(2) + "%)";
            var str2 = "<span style='color: #666; font-weight: bold;'>" + _index.toFix(2) + "</span><img align='absmiddle' style='margin: 0 3px;' border='0' src='http://cafef3.vcmedia.vn/images/" + img + ".gif'><span class='Index_" + cls + "'>" + sign + "" + _change.toFixed(2) + " (" + sign + "" + _percent.toFixed(2) + " %)</span>";
            var _bIndex = (str != $('#' + (_center == "1" ? "vnindex" : "hnxindex")).html());
            var _bVolume = (_volume != $('#VAL_' + _center).html());
            var _bValue = (_value.toFixed(1) != $('#' + (_center == "1" ? "vnindexval" : "hnxindexval")).html());
            var _bTang = (_tang != $('#CSI_' + _center + '').html());
            var _bTran = (_tran != $('#CSC_' + _center + '').html());
            var _bNgang = (_ngang != $('#CSN_' + _center + '').html());
            var _bGiam = (_giam != $('#CSD_' + _center + '').html());
            var _bSan = (_san != $('#CSF_' + _center + '').html());
            if (_bIndex) {
                $('#' + (_center == "1" ? "vnindex" : "hnxindex")).html(str).css('background-color', '#FF6A00');
                $('#' + (_center == "1" ? "vnindex" : "hnxindex")).parent().attr('class', 'bd-text ' + cls);
                $('#IND_' + _center).html(str2);
            }
            if (_bValue) {
                $('#' + (_center == "1" ? "vnindexval" : "hnxindexval")).html(_value.toFixed(1)).css('background-color', '#FF6A00');
                $('#VAL_' + _center).html(_value.toFixed(1)).css('background-color', '#FF6A00');
            }
            if (_bVolume) { $('#VOL_' + _center).html(_volume).css('background-color', '#FF6A00'); }
            if (_bTang) { $('#CSI_' + _center + '').html(_tang).css('background-color', '#FF6A00'); }
            if (_bTran) { $('#CSC_' + _center + '').html(_tran).css('background-color', '#FF6A00'); }
            if (_bNgang) { $('#CSN_' + _center + '').html(_ngang).css('background-color', '#FF6A00'); }
            if (_bGiam) { $('#CSD_' + _center + '').html(_giam).css('background-color', '#FF6A00'); }
            if (_bSan) { $('#CSF_' + _center + '').html(_san).css('background-color', '#FF6A00'); }
            window.setTimeout("$('.bd-text > span, #centerindex span').css('background-color','');", 1000);

        }
        // There seems to be no easy way in jQuery to handle the scrollTop property
        //Chart[0].scrollTop = Chart[0].scrollHeight - Chart.outerHeight();

    };

    //receive data for stock detail page
    this.receiveDetail = function(message) {

        var chanCont = '',
		   		_pKey = eval(message)['data']['codes'],
				_pKAr = _self.explode(';', _pKey),
				_pKaLen = _pKAr.length;

        var arrStock = _pKey.split('|');
        var _pCont;

        if (_pKaLen > 0) {
            if (isTrading == false) return;
            /*
            if (isTrading == false) {
            $.cometd.batch(function() {
            _unsubscribe();
            });
            $.cometd.disconnect();
            _clientKey = null;
            //_lastUser = null;
            _disconnecting = true;
            return;
            }*/
        }

        for (c = 0; c < _pKaLen; c++) {
            _pKAr[c] = _pKAr[c].replace(/[\(\)\/}\{\][*]/g, "");

            _pCont = _self.explode('|', _pKAr[c]).splice(0, 48);
            //if(_pCont[0]!=_symbol) continue;

            var _symbol = _pCont[0], _san = _pCont[1], _price = _pCont[2], _volume = _pCont[3],
					_ref = _pCont[4], _change = _pCont[5], _floor = _pCont[6], _ceiling = _pCont[7],
					_bp1 = _pCont[8], _bp2 = _pCont[9], _bp3 = _pCont[10], _bv1 = _pCont[11], _bv2 = _pCont[12], _bv3 = _pCont[13],
					_sp1 = _pCont[14], _sp2 = _pCont[15], _sp3 = _pCont[16], _sv1 = _pCont[17], _sv2 = _pCont[18], _sv3 = _pCont[19],
					_lo = _pCont[20], _hi = _pCont[21], _av = _pCont[22], _fb = _pCont[23], _fs = _pCont[24],
					_bt = _pCont[25], _at = _pCont[26], _ba = _pCont[27],
					_ps = GetCss2(_pCont[28]), _ls = GetCss2(_pCont[35]), _hs = GetCss2(_pCont[36]), _as = GetCss2(_pCont[37]), _img = GetImage(_pCont[28]),
					_bs1 = GetCss(_pCont[29]), _bs2 = GetCss(_pCont[30]), _bs3 = GetCss(_pCont[31]),
					_as1 = GetCss(_pCont[32]), _as2 = GetCss(_pCont[33]), _as3 = GetCss(_pCont[34]),
					_bgt = _pCont[38], _agt = _pCont[39], _obt = _pCont[40], _oat = _pCont[41], _loadbagt = _pCont[42], _percent = _pCont[43], _open = _pCont[44], _close = _pCont[45], _fbs = _pCont[46], _remain = _pCont[47];
            $('#bidaskcon').css('display', (_loadbagt == "1" ? '' : 'none'));
            if (eval(_volume) != '0') $('#NoTrade').hide();
            //change color when different
            var cp = (_price != $('#CP').html()), cc = ((" <img src='http://cafef3.vcmedia.vn/images/" + _img + "' align='absmiddle'> &nbsp;&nbsp;" + _change + ' (' + _percent + ' %)') != $('#CC').html()), cv = (_volume != $('#CV').html()), co = (_open != $('#OP').html());
            var cbp1 = (_bp1 != $('#BP1').html()), cbp2 = (_bp2 != $('#BP2').html()), cbp3 = (_bp3 != $('#BP3').html()),
					cbv1 = (_bv1 != $('#BV1').html()), cbv2 = (_bv2 != $('#BV2').html()), cbv3 = (_bv3 != $('#BV3').html()),
					csp1 = (_sp1 != $('#SP1').html()), csp2 = (_sp2 != $('#SP2').html()), csp3 = (_sp3 != $('#SP3').html()),
					csv1 = (_sv1 != $('#SV1').html()), csv2 = (_sv2 != $('#SV2').html()), csv3 = (_sv3 != $('#SV3').html()),
					cbt = (_bt != $('#SB').html()), cat = (_at != $('#SS').html()), cba = (_ba != $('#DBS').html()),
					chi = (_hi != $('#HI').html()), clo = (_lo != $('#LO').html()), cav = (_av != $('#AV').html()),
					cfb = (_fb != $('#FB').html()), cfs = (_fs != $('#FS').html()), cfbs = (_fbs != $('#FBS').html()),
					cbgt = (_bgt != $('#BGT').html()), cagt = (_agt != $('#AGT').html()), cobt = (_obt != $('#OBT').html()), coat = (_oat != $('#OAT').html()),
	                cfre = (_remain != $('#FRE').html());

            $('#CE').html(_ceiling); $('#REF').html(_ref); $('#FL').html(_floor);

            $('#CC').html("<div class='" + GetCssV2(_pCont[37]) + "'>" + strChgIndex + "</div>");
            /*$('#CP').html(_price).attr('class', 'cacchiso_td1_div2 ' + _ps); */
            /*$('#CC').html(" <img src='http://cafef3.vcmedia.vn/images/" + _img + "' align='absmiddle'> &nbsp;&nbsp;" + _change + ' (' + _percent + ' %)').attr('class', 'cacchiso_td1_div3 ' + _ps); */
            $('#CV').html(_volume); $('#OP').html(_open); $('#CO').html(_close);
            $('#HI').html(_hi); $('#LO').html(_lo); //$('#AV').html(_av).attr('class','_pc'+_as);
            $('#BP1').html(_bp1).attr('class', '_pc' + _bs1); $('#BV1').html(_bv1).attr('class', '_pc' + _bs1);
            $('#BP2').html(_bp2).attr('class', '_pc' + _bs2); $('#BV2').html(_bv2).attr('class', '_pc' + _bs2);
            $('#BP3').html(_bp3).attr('class', '_pc' + _bs3); $('#BV3').html(_bv3).attr('class', '_pc' + _bs3);
            $('#SP1').html(_sp1).attr('class', '_pc' + _as1); $('#SV1').html(_sv1).attr('class', '_pc' + _as1);
            $('#SP2').html(_sp2).attr('class', '_pc' + _as2); $('#SV2').html(_sv2).attr('class', '_pc' + _as2);
            $('#SP3').html(_sp3).attr('class', '_pc' + _as3); $('#SV3').html(_sv3).attr('class', '_pc' + _as3);
            $('#SB').html(_bt); $('#DBS').html(_ba); $('#SS').html(_at);
            $('#FB').html(_fb); $('#FS').html(_fs); //$('#FBS').html(_fbs);
            $('#BGT').html(_bgt); $('#AGT').html(_agt); $('#OBT').html(_obt); $('#OAT').html(_oat);
            $('#FRE').html(_remain);

            if (cfre) { $('#FRE').css('background-color', '#FF6A00'); }
            //if(cfbs){$('#FBS').css('background-color','#FF6A00');}
            if (co) { $('#OP').css('background-color', '#FF6A00'); }
            if (chi) { $('#HI').css('background-color', '#FF6A00'); }
            //window.setTimeout("$('#HI').css('background-color','');",1000);
            if (clo) { $('#LO').css('background-color', '#FF6A00'); }
            //window.setTimeout("$('#LO').css('background-color','');",1000);
            if (cav) { $('#AV').css('background-color', '#FF6A00'); }
            //window.setTimeout("$('#AV').css('background-color','');",1000);
            if (cp) { $('#CP').css('background-color', '#FF6A00'); }
            //window.setTimeout("$('#CP').css('background-color','');",1000);
            if (cc) { $('#CC').css('background-color', '#FF6A00'); }
            //window.setTimeout("$('#CC').css('background-color','');",1000);
            if (cv) { $('#CV').css('background-color', '#FF6A00'); }
            //window.setTimeout("$('#CV').css('background-color','');",1000);
            if (cbp1) { $('#BP1').css('background-color', '#FF6A00'); }
            //window.setTimeout("$('#BP1').css('background-color','');",1000);
            if (cbv1) { $('#BV1').css('background-color', '#FF6A00'); }
            //window.setTimeout("$('#BV1').css('background-color','');",1000);
            if (cbp2) { $('#BP2').css('background-color', '#FF6A00'); }
            //window.setTimeout("$('#BP2').css('background-color','');",1000);
            if (cbv2) { $('#BV2').css('background-color', '#FF6A00'); }
            //window.setTimeout("$('#BV2').css('background-color','');",1000);
            if (cbp3) { $('#BP3').css('background-color', '#FF6A00'); }
            //window.setTimeout("$('#BP3').css('background-color','');",1000);
            if (cbv3) { $('#BV3').css('background-color', '#FF6A00'); }
            //window.setTimeout("$('#BV3').css('background-color','');",1000);
            if (csp1) { $('#SP1').css('background-color', '#FF6A00'); }
            //window.setTimeout("$('#SP1').css('background-color','');",1000);
            if (csv1) { $('#SV1').css('background-color', '#FF6A00'); }
            //window.setTimeout("$('#SV1').css('background-color','');",1000);
            if (csp2) { $('#SP2').css('background-color', '#FF6A00'); }
            //window.setTimeout("$('#SP2').css('background-color','');",1000);
            if (csv2) { $('#SV2').css('background-color', '#FF6A00'); }
            //window.setTimeout("$('#SV2').css('background-color','');",1000);
            if (csp3) { $('#SP3').css('background-color', '#FF6A00'); }
            //window.setTimeout("$('#SP3').css('background-color','');",1000);
            if (csv3) { $('#SV3').css('background-color', '#FF6A00'); }
            //window.setTimeout("$('#SV3').css('background-color','');",1000);
            if (cbt) { $('#SB').css('background-color', '#FF6A00'); }
            //window.setTimeout("$('#SB').css('background-color','');",1000);
            if (cba) { $('#DBS').css('background-color', '#FF6A00'); }
            //window.setTimeout("$('#DBS').css('background-color','');",1000);
            if (cat) { $('#SS').css('background-color', '#FF6A00'); }
            //window.setTimeout("$('#SS').css('background-color','');",1000);
            if (cfb) { $('#FS').css('background-color', '#FF6A00'); }
            //window.setTimeout("$('#FS').css('background-color','');",1000);
            if (cfs) { $('#FB').css('background-color', '#FF6A00'); }
            //window.setTimeout("$('#FB').css('background-color','');",1000);
            if (cbgt) { $('#BGT').css('background-color', '#FF6A00'); }
            if (cagt) { $('#AGT').css('background-color', '#FF6A00'); }
            if (cobt) { $('#OBT').css('background-color', '#FF6A00'); }
            if (coat) { $('#OAT').css('background-color', '#FF6A00'); }
            window.setTimeout("$('#HI,#LO,#AV,#CP,#CC,#CV,#BP1,#BV1,#BP2,#BV2,#BP3,#BV3,#SP1,#SV1,#SP2,#SV2,#SP3,#SV3,#SB,#DBS,#SS,#FS,#FB,#BGT,#AGT,#OBT,#OAT,#OP,#FBS,#FRE').css('background-color','');", 1000);
        }
        // There seems to be no easy way in jQuery to handle the scrollTop property
        //Chart[0].scrollTop = Chart[0].scrollHeight - Chart.outerHeight();
    };
    function GetCssV2(val) {
        switch (val) {
            case "1": return "dltlu-down blue";
            case "2": return "dltlu-down red";
            case "3": return "dltlu-nochange orange";
            case "4": return "dltlu-up green";
            case "5": return "dltlu-up pink";
            default: return "dltlu-nochange orange";
        }
    }
    function GetCss(val) {
        switch (val) {
            case "1": return "Floor";
            case "2": return "Dow";
            case "3": return "Nor";
            case "4": return "Up";
            case "5": return "Cell";
            default: return "Nor";
        }
    }
    function GetCss2(val) {
        switch (val) {
            case "1": return "Index_Floor";
            case "2": return "Index_Down";
            case "3": return "Index_NoChange";
            case "4": return "Index_Up";
            case "5": return "Index_Ceiling";
            default: return "Index_NoChange";
        }
    }
    function GetImage(val) {
        switch (val) {
            case "1": return "Down_h.png";
            case "2": return "Down_h.png";
            case "3": return "Nochange_h.png";
            case "4": return "Up_h.png";
            case "5": return "Up_h.png";
            default: return "Nochange_h";
        }
    }
    /**
    * Updates the members list.
    * This function is called when a message arrives on channel /chat/members
    */
    this.members = function(message) {
        /*var list = '';
        $.each(message.data, function()
        {
        list += this + '<br />';
        });
        $('#members').html(list);*/
    };

    function _unsubscribe() {
        if (_chatSubscription) {
            $.cometd.unsubscribe(_chatSubscription);
        }
        _chatSubscription = null;
        if (_membersSubscription) {
            $.cometd.unsubscribe(_membersSubscription);
        }
        _membersSubscription = null;
        if (_centerSubscription) {
            $.cometd.unsubscribe(_centerSubscription);
        }
        _centerSubscription = null;
        if (_indexSubscription) {
            $.cometd.unsubscribe(_indexSubscription);
        }
        _indexSubscription = null;
    }

    function _subscribe() {
        _membersSubscription = $.cometd.subscribe('/service/magazine', _self.receive);
        _chatSubscription = $.cometd.subscribe('/service/codes', _self.receive);
        _centerSubscription = $.cometd.subscribe('/service/detail', _self.receiveDetail);
        _indexSubscription = $.cometd.subscribe('/service/report', _self.receiveIndex);
    }

    function _connectionInitialized() {
        _clientKey = [];
        if ((org.cometd.COOKIE ? org.cometd.COOKIE.get(_stateCookiesName) : "0") == "1") {
            var list = (org.cometd.COOKIE ? org.cometd.COOKIE.get(_cookieName) : "");
            if (list == null) list = '';
            if (list != '') {
                var listOfSymbol = list.split(';');
                for (var i = 0; i < listOfSymbol.length; i++) {
                    _clientKey.push(listOfSymbol[i]);
                }
            }
        }
        _detailKey = [];
        if (_symbol != '') _detailKey.push(_symbol);
        var _centerKey = [];
        _centerKey.push('VNINDEX'); _centerKey.push('HNXINDEX');
        // first time connection for this client, so subscribe tell everybody.
        $.cometd.batch(function() {
            _subscribe();
            if (_clientKey.length > 0) {
                $.cometd.publish("/service/codes", {
                    codes: "" + org.cometd.JSON.toJSON(_clientKey) + ""
                });
            }
            if (_detailKey.length > 0) {
                $.cometd.publish("/service/detail", {
                    codes: "" + org.cometd.JSON.toJSON(_detailKey) + ""
                });
            }
            $.cometd.publish("/service/report", {
                codes: "" + org.cometd.JSON.toJSON(_centerKey) + ""
            });
        });
    }

    function _connectionEstablished() {
        // connection establish (maybe not for first time), so just
        // tell local user and update membership
        _self.receive({
            data: {
                //user: 'system',
                Chart: 'Connection to Server Opened'
            }
        });
        _self.receiveDetail({
            data: {
                //user: 'system',
                Chart: 'Connection to Server Opened'
            }
        });
        _self.receiveIndex({
            data: {
                //user: 'system',
                Chart: 'Connection to Server Opened'
            }
        });
        //$.cometd.publish('/service/magazine', {});
    }

    function _connectionBroken() {
        _self.receive({
            data: {
                user: 'system',
                Chart: 'Connection to Server Broken'
            }
        });
        _self.receiveDetail({
            data: {
                user: 'system',
                Chart: 'Connection to Server Broken'
            }
        });
        _self.receiveIndex({
            data: {
                user: 'system',
                Chart: 'Connection to Server Broken'
            }
        });
    }

    function _connectionClosed() {
        _self.receive({
            data: {
                user: 'system',
                Chart: 'Connection to Server Closed'
            }
        });
        _self.receiveDetail({
            data: {
                user: 'system',
                Chart: 'Connection to Server Closed'
            }
        });
        _self.receiveIndex({
            data: {
                user: 'system',
                Chart: 'Connection to Server Closed'
            }
        });
    }

    function _metaConnect(message) {
        if (_disconnecting) {
            _connected = false;
            _connectionClosed();
        }
        else {
            _wasConnected = _connected;
            _connected = message.successful === true;
            if (!_wasConnected && _connected) {
                _connectionEstablished();
            }
            else if (_wasConnected && !_connected) {
                _connectionBroken();
            }
        }
    }

    function _metaHandshake(message) {
        if (message.successful) {
            _connectionInitialized();
        }
    }

    $.cometd.addListener('/meta/handshake', _metaHandshake);
    $.cometd.addListener('/meta/connect', _metaConnect);

    // Restore the state, if present
    if (state) {
        setTimeout(function() {
            // This will perform the handshake
            _self.join();
        }, 0);
    }

    $(window).unload(function() {

        if ($.cometd.reload) {
            $.cometd.reload();
        }
        else {
            $.cometd.disconnect();
        }
    });
}

//})(jQuery);
var chart = new _Chart(null);
function CafeF_StockSymbolSlide(instanceName) {
    this.bm_st_time_exp = 720 * 365
    this.bm_st_state_time_exp = 720; ;
    this.CookiesName = 'favorite_stocks';
    this.StateCookiesName = 'favorite_stocks_state';

    this.AUTO_REFRESH_TIME = 15000;
    this.timerId_Refresh = -1;

    this.IsLogged = 0;

    this.MaxSymbolsInQueue = 15;
    this.MaxSymbolsDisplay = 10;
    this.OldCellDatas = new Array();
    for (var i = 0; i < this.MaxSymbolsDisplay; i++) {
        this.OldCellDatas[i] = '';
    }
    //quan ly ma cty
    this.host = 'http://solieu3.vcmedia.vn';
    //this.host = 'http://localhost:8081';
    this.script_folder = 'http://cafef3.vcmedia.vn/solieu/solieu3/'; //'http://solieu6.vcmedia.vn/www/cafef/';    
    this.script_object = null;
    this.stock_symbols_data = null;
    this.symbol_list = '';
    this.display_list = '';
    this.instance_name = instanceName;
    this.containerId = '';

    this.StockTrading_StartTime = '08:00:00';
    this.StockTrading_EndTime = '11:05:00';
    this.StockTrading_DayOfWeek = '1,2,3,4,5';
    //    this.Fields = {'Price':0,'Change':1,'ChangePercent':2};
    this.Fields = { 'Price': 0, 'Change': 1, 'ChangePercent': 2, 'ceiling': 3, 'floor': 4 };

    this.CreateCssLink = function(href) {
        var css = document.createElement('link');
        css.type = 'text/css';
        css.rel = 'stylesheet';
        css.href = href;
        var head = document.getElementsByTagName('head')[0];
        head.appendChild(css);
    }

    this.IsStockTrading = function() {
        var startTime = new Date();
        var endTime = new Date();
        var now = new Date();
        var day = ',' + this.StockTrading_DayOfWeek + ',';

        var start = this.StockTrading_StartTime.split(':');
        var end = this.StockTrading_EndTime.split(':');

        startTime.setHours(start[0], start[1], start[2]);
        endTime.setHours(end[0], end[1], end[2]);

        return (now >= startTime && now <= endTime && day.indexOf(',' + now.getDay() + ',') >= 0);
    }

    this.InitScript = function(containerId) {
        //this.CreateCssLink(this.script_folder + 'css/cafef.css');
        //        this.CreateScriptObject(this.host + '/Public/js/jquery.js');
        //        this.CreateScriptObject('http://cafef.vn/Scripts/Library.js?upd=26881057');
        //        this.CreateScriptObject(this.host + '/Public/js/jqDnR.js');
        //        this.CreateScriptObject('http://cafef.vn/Scripts/AutoComplete/kby.js');
        //        this.CreateScriptObject(this.host + '/Public/js/jquery.bgiframe.min.js');
        //        this.CreateScriptObject(this.host + '/Public/js/jquery.dimensions.js');
        //        this.CreateScriptObject(this.host + '/Public/js/jquery.autocomplete2.js');

        var output = '';

        output += '<div class="danhsachma"> <a href="javascript:void(0);" class="prev" onclick="' + this.instance_name + '.MoveLeft();">Prev</a> <a href="javascript:void(0);" class="next" onclick="' + this.instance_name + '.MoveRight();">Next</a><ul id="stockbar">';
        output += '<li class="wait" id="stockbarli0" onclick="' + this.instance_name + '.OpenPopup(\'CafeF_StockSymbolSlidePopup\');" title="Click vào đây để chọn mã chứng khoán cần theo dõi">Chọn mã CK<br />cần theo dõi</li>';
        output += '<li class="wait" id="stockbarli1" onclick="' + this.instance_name + '.OpenPopup(\'CafeF_StockSymbolSlidePopup\');" title="Click vào đây để chọn mã chứng khoán cần theo dõi">Chọn mã CK<br />cần theo dõi</li>';
        output += '<li class="wait" id="stockbarli2" onclick="' + this.instance_name + '.OpenPopup(\'CafeF_StockSymbolSlidePopup\');" title="Click vào đây để chọn mã chứng khoán cần theo dõi">Chọn mã CK<br />cần theo dõi</li>';
        output += '<li class="wait" id="stockbarli3" onclick="' + this.instance_name + '.OpenPopup(\'CafeF_StockSymbolSlidePopup\');" title="Click vào đây để chọn mã chứng khoán cần theo dõi">Chọn mã CK<br />cần theo dõi</li>';
        output += '<li class="wait" id="stockbarli4" onclick="' + this.instance_name + '.OpenPopup(\'CafeF_StockSymbolSlidePopup\');" title="Click vào đây để chọn mã chứng khoán cần theo dõi">Chọn mã CK<br />cần theo dõi</li>';
        output += '<li class="wait" id="stockbarli5" onclick="' + this.instance_name + '.OpenPopup(\'CafeF_StockSymbolSlidePopup\');" title="Click vào đây để chọn mã chứng khoán cần theo dõi">Chọn mã CK<br />cần theo dõi</li>';
        output += '<li class="wait" id="stockbarli6" onclick="' + this.instance_name + '.OpenPopup(\'CafeF_StockSymbolSlidePopup\');" title="Click vào đây để chọn mã chứng khoán cần theo dõi">Chọn mã CK<br />cần theo dõi</li>';
        output += '<li class="wait" id="stockbarli7" onclick="' + this.instance_name + '.OpenPopup(\'CafeF_StockSymbolSlidePopup\');" title="Click vào đây để chọn mã chứng khoán cần theo dõi">Chọn mã CK<br />cần theo dõi</li>';
        output += '<li class="wait" id="stockbarli8" onclick="' + this.instance_name + '.OpenPopup(\'CafeF_StockSymbolSlidePopup\');" title="Click vào đây để chọn mã chứng khoán cần theo dõi">Chọn mã CK<br />cần theo dõi</li>';
        output += '<li class="wait" id="stockbarli9" onclick="' + this.instance_name + '.OpenPopup(\'CafeF_StockSymbolSlidePopup\');" title="Click vào đây để chọn mã chứng khoán cần theo dõi">Chọn mã CK<br />cần theo dõi</li>';
        output += '</ul><div class="manager"><a href="javascript:void(0);" onclick="' + this.instance_name + '.OpenPopup(\'CafeF_StockSymbolSlidePopup\');">Quản lý</a><a href="javascript:void(0);" onclick="' + this.instance_name + '.HideSlide()"><span>close</span></a></div></div></div>';
        output += '<div style="display: none; text-align: right; padding-top: 5px;" class="menu_under_link KenhF_Content" id="CafeF_StockSymbolSlideTable_Hide"><a href="javascript:' + this.instance_name + '.ShowSlide()">Hiện danh sách CP đang theo dõi</a></div>';
        output += '<div id="CafeF_StockSymbolSlidePopup">';
        output += '<div class="PopupRow">';
        output += '<div id="CafeF_StockSymbolSlidePopupTitle">Gõ mã CK cần thêm vào danh sách</div>';
        output += '<div style="float: right;">';
        output += '<img alt="" style="cursor: hand; cursor: pointer;" onclick="' + this.instance_name + '.ClosePopup(\'CafeF_StockSymbolSlidePopup\')" border="0" src="' + this.script_folder + 'images/close1.gif" />';
        output += '</div>';
        output += '</div>';
        output += '<div class="PopupRow">';
        output += '<div style="float: left"><input type="text" id="CafeF_StockSymbolSlideKeyword" style="width: 225px;" autocomplete="off" class="class_text_autocomplete ac_input" /></div>';
        output += '<div style="float: right;padding-top: 3px;padding-right: 2px;"><a href="javascript:void(0);" onclick="' + this.instance_name + '.AddSymbol();return false;"><img align="middle" src="' + this.script_folder + 'images/add.gif" /></a></div></div>';
        output += '<div class="PopupRow" id="CafeF_StockSymbolSlidePopupList"></div>';
        output += '<div style="float: right; margin-right: 10px;"><a style="color: Red; font-size: 11px; padding: 0px;" href="javascript:void(0)" onclick="' + this.instance_name + '.RemoveAllSymbols();">Xóa toàn bộ</a></div>';
        output += '</div>';

        document.getElementById(containerId).innerHTML = output;
        this.containerId = containerId;
        if (this.GetCookies(this.CookiesName)) {
            if (this.GetCookies(this.CookiesName).indexOf('@') >= 0) // Neu la cookies cua version cu (truoc khi tach So lieu va Tin tuc)
            {
                this.GetSymbolListFromCoolies_OldVersion();
            }
            else // Cookies cua version moi
            {
                this.GetSymbolListFromCoolies();
            }
        }

        var state = this.GetCookies(this.StateCookiesName);
        if (state) {
            state = state.replace('@', '');
            if (state == '1') {
                this.ShowSlide();
            }
            else {
                this.HideSlide();
            }
        }
        else {
            this.ShowSlide();
        }

        if (state && this.display_list != '' && this.IsLogged == 0) {
            this.Log();
            this.IsLogged = 1;
        }

        //this.CreateScriptObject("http://admicro1.vcmedia.vn/cafef/framework.mini.17.10.js", true);
    }

    this.GetSymbolListFromCoolies = function() {
        this.display_list = '';
        this.symbol_list = this.GetCookies(this.CookiesName);
        if (this.symbol_list != '') {
            var listOfSymbol = this.symbol_list.split(';');

            for (var i = 0; i < listOfSymbol.length && i < this.MaxSymbolsDisplay; i++) {
                this.display_list += ';' + listOfSymbol[i];
            }
            if (this.display_list != '') this.display_list = this.display_list.substring(1);
        }
        else {
            this.display_list = '';
        }
    }

    this.GetSymbolListFromCoolies_OldVersion = function() {
        var temp = this.GetCookies(this.CookiesName);
        if (temp != '') {
            var arrSymbols = temp.split('@');

            for (var i = 0; i < arrSymbols.length; i++) {
                if (arrSymbols[i] != '') {
                    var arrData = arrSymbols[i].split('|');
                    this.symbol_list += ';' + arrData[1];
                }
            }
            if (this.symbol_list != '') this.symbol_list = this.symbol_list.substring(1);

            var listOfSymbol = this.symbol_list.split(';');

            for (var i = 0; i < listOfSymbol.length && i < this.MaxSymbolsDisplay; i++) {
                this.display_list += ';' + listOfSymbol[i];
            }
            if (this.display_list != '') this.display_list = this.display_list.substring(1);
        }
        else {
            this.display_list = '';
        }
    }

    this.HideSlide = function() {
        jQuery('#CafeF_StockSymbolSlideTable').hide();
        jQuery('#CafeF_StockSymbolSlideTable_Hide').show();
        this.SetCookie(this.StateCookiesName, 0, this.bm_st_state_time_exp);
    }

    this.ShowSlide = function() {
        jQuery('#CafeF_StockSymbolSlideTable').show();
        jQuery('#CafeF_StockSymbolSlideTable_Hide').hide();
        this.SetCookie(this.StateCookiesName, 1, this.bm_st_state_time_exp);
    }

    this.LoadSymbolData = function(isFirstRequest) {

        //if ((this.IsStockTrading() && CafeF_StockSymbolSlide_IsWindowFocus) || isFirstRequest)
        if (this.IsStockTrading() || (!this.IsStockTrading() && isFirstRequest)) {
            /* if (this.display_list != '' || isFirstRequest) {
               if (this.symbol_list.length > 0) {*/
                    var currentDate = new Date();
                    this.CreateScriptObject(this.host + '/ProxyHandler.ashx?RequestName=StockSymbolSlide&CallBack=' + this.instance_name + '.OnLoaded&RequestType=json&sym=' + this.display_list + (window.location.toString().toLowerCase().indexOf('/du-lieu.chn')>0?";HSXD;HNXD":"") + '&ut=' + currentDate.getDate() + currentDate.getTime(), true);
            /*    }
            }*/
        }
        else {
            //neu ton tai ma cty moi load du lieu realtime
            /*if (this.symbol_list.length > 0) {*/
                clearTimeout(this.timerId_Refresh);
                this.timerId_Refresh = setTimeout(this.instance_name + '.LoadSymbolData(false)', this.AUTO_REFRESH_TIME);
            /*}*/
        }
    }

    this.Log = function() {
        /* var currentDate = new Date();
        var src = this.host + '/ProxyHandler.ashx?RequestName=StockSymbolSlide&CallBack=' + this.instance_name + '.OnLoaded&RequestType=json&log=1&n=Home&ut=' + currentDate.getDate() + currentDate.getTime();

        var objScript = document.createElement('script');

        objScript.setAttribute('type','text/javascript');
        objScript.setAttribute('src', src);
        
        var head = document.getElementsByTagName('head')[0];
        head.appendChild(objScript);
        */
    }

    this.OnLoaded = function(data, methodName) {
        var json = eval(data);
        if (json && json.Symbols) //thanhbv
        {
            var output = '';
            var displayList = this.display_list.split(';');
            //alert(displayList);
            //for (var i = 0; i < this.MaxSymbolsDisplay; i++) {
            var i = 0; var j = 0; var maxdisp = this.MaxSymbolsDisplay;
            while (i < maxdisp) {
                try {
                    var displayIndex = 1000;
                    if (i < json.Symbols.length) {

                        if (json.Symbols[i].Symbol == 'HSX' || json.Symbols[i].Symbol == 'HNX' || json.Symbols[i].Symbol == 'HSXD' || json.Symbols[i].Symbol == 'HNXD') {
                            i++; maxdisp++;
                            continue;
                        }
                        for (displayIndex = 0; displayIndex < displayList.length; displayIndex++) {
                            if (displayList[displayIndex] == json.Symbols[i].Symbol) break;
                        }
                        //if (displayList[displayIndex] != json.Symbols[i].Symbol) { displayIndex = 1000; }
                        //alert(json.Symbols[i].Symbol + '-' + displayIndex);
                        //this.Fields = {'Price':0,'Change':1,'ChangePercent':2};
                        var image = 'nochange.gif';
                        var style = 'NoChange';
                        var sign = '';
                        if (json.Symbols[i].Datas[this.Fields.Change] > 0) {
                            image = 'up.gif';
                            style = 'Up';
                            sign = '+';
                        }
                        else if (json.Symbols[i].Datas[this.Fields.Change] < 0) {
                            image = 'down.gif';
                            style = 'Down';
                        }
                        //hung
                        if ((json.Symbols[i].Datas[this.Fields.ceiling] > 0) && (json.Symbols[i].Datas[this.Fields.ceiling] == json.Symbols[i].Datas[this.Fields.Price])) {
                            //                        image = 'Ceiling_.gif';
                            style = 'Ceiling';
                            sign = '+';

                        }
                        if ((json.Symbols[i].Datas[this.Fields.floor] > 0) && (json.Symbols[i].Datas[this.Fields.floor] == json.Symbols[i].Datas[this.Fields.Price])) {
                            //                        image = 'Floor_.gif';
                            style = 'Floor';

                        }
                        //end
                        /*
                        <table width="100%" cellspacing="0" cellpadding="0" border="0"><tbody><tr><td><div class="FloatLeft"><a href="/hose/VIC-cong-ty-co-phan-vincom.chn">VIC</a>
                        <div style="" class="Price" id="slide_price_VIC">101.0</div>
                        </div><div class="FloatRight"><img style="margin-top: 1px;" src="http://cafef3.vcmedia.vn/solieu/solieu3/images/close.gif" alt="Xóa mã CK này khỏi danh sách" onclick="danh_sach_ma_chung_khoan_theo_doi.RemoveSymbol('VIC')"></div></td></tr><tr><td> <div style="margin-top: 2px; margin-right: 6px;" class="FloatRight"><img id="slide_image_VIC" src="http://cafef3.vcmedia.vn/solieu/solieu3/images/up.gif" alt=""><span style="" class="Up" id="slide_change_VIC">+2.0</span> <span style="" class="Up" id="slide_percent_VIC">(+2.0%)</span></div></td></tr></tbody></table>
                        */
                        output = '<table width="100%" cellspacing="0" cellpadding="0" border="0"><tr><td>';
                        output += '<div class="FloatLeft"><a href="' + CafeF_JSLibrary.GetCompanyInfoLink(json.Symbols[i].Symbol) + '">' + json.Symbols[i].Symbol + '</a>'
                        output += '<div class="Price" id="slide_price_' + json.Symbols[i].Symbol + '" style="display: inline;">' + this.FormatNumber(json.Symbols[i].Datas[this.Fields.Price], true) + '</div>';
                        output += '</div>';
                        output += '<div class="FloatRight"><img style="margin-top:1px;" src="' + this.script_folder + 'images/close.gif" alt="Xóa mã CK này khỏi danh sách" onclick="' + this.instance_name + '.RemoveSymbol(\'' + json.Symbols[i].Symbol + '\')" /></div>';
                        output += '</td></tr><tr><td>';
                        output += '<div class="FloatRight"><img alt="" src="' + this.script_folder + 'images/' + image + '" id="slide_image_' + json.Symbols[i].Symbol + '" />&nbsp;<span class="' + style + '" id="slide_change_' + json.Symbols[i].Symbol + '">' + sign + this.FormatNumber(json.Symbols[i].Datas[this.Fields.Change], true) + '</span> <span class="' + style + '" id="slide_percent_' + json.Symbols[i].Symbol + '">(' + sign + this.FormatNumber(json.Symbols[i].Datas[this.Fields.ChangePercent], true) + '%)</span></div>';
                        output += '</td></tr></table>';
                    }
                    else {
                        output = 'Chọn mã CK<br />cần theo dõi';
                    }

                    this.UpdateCell(displayIndex, output);
                }
                catch (ex) {
                }
                //end
                i++;
            }
            //update center
            for (var i = 0; i < json.Symbols.length; i++) {
                if (json.Symbols[i].Symbol == 'HSX' || json.Symbols[i].Symbol == 'HNX') {
                    var _center = json.Symbols[i].Symbol == "HNX" ? "2" : "1";
                    var datas = json.Symbols[i].Datas;
                    var _bidx = $('.idx_' + _center).html() != datas[0].toString(),
                        _bidc = $('.idc_' + _center).html() != datas[1].toString(),
                        _bidp = $('.idp_' + _center).html() != (datas[2].toString() + '%'),
                        _bidv = $('.idv_' + _center).html() != datas[3].toString();
                    $('.idx_' + _center).html(datas[0].toString()); //index
                    $('.idc_' + _center).html(datas[1].toString()); //change
                    $('.idp_' + _center).html(datas[2].toString() + '%'); //percent
                    $('.idv_' + _center).html(datas[3].toString()); //value
                    $('.idt_' + _center).html(datas[4].toString()); //time
                    $('.idx_' + _center).parents(".bd-vni").removeClass('up').removeClass('down').addClass(datas[1].toString().indexOf('-') >= 0 ? 'down' : 'up');
                    $('.idc_' + _center).parents("#idxd_" + _center).removeClass('Index_Up').removeClass('Index_Down').addClass(datas[1].toString().indexOf('-') >= 0 ? 'Index_Down' : 'Index_Up');
                    $('.img_' + _center).attr('src', 'http://cafef3.vcmedia.vn/images/' + (datas[1].toString().indexOf('-') >= 0 ? "btdown" : "btup") + '.gif');
                    if (_bidx) { $('.idx_' + _center).css('background-color', '#FF6A00'); }
                    if (_bidc) { $('.idc_' + _center).css('background-color', '#FF6A00'); }
                    if (_bidp) { $('.idp_' + _center).css('background-color', '#FF6A00'); }
                    if (_bidv) { $('.idv_' + _center).css('background-color', '#FF6A00'); }
                    window.setTimeout("$('.idx_" + _center + ",.idc_" + _center + ",.idp_" + _center + ",.idv_" + _center + "').css('background-color', '');", 1000);
                }
                else if (json.Symbols[i].Symbol == 'HSXD' || json.Symbols[i].Symbol == 'HNXD') {
                    var _center = json.Symbols[i].Symbol == "HNXD" ? "2" : "1";
                    var datas = json.Symbols[i].Datas;
                    var _stats = datas[2].toString().split('|');
                    var _bivl = $('.ivl_' + _center).html() != datas[0].toString(),
                        _bidf = $('.idf_' + _center).html() != datas[1].toString(),
                        _bceiling = $('.ceiling_' + _center).html() != _stats[0].toString(),
                        _bup = $('.up_' + _center).html() != _stats[1].toString(),
                        _bnormal = $('.normal_' + _center).html() != _stats[2].toString(),
                        _bdown = $('.down_' + _center).html() != _stats[3].toString(),
                        _bfloor = $('.floor_' + _center).html() != _stats[4].toString();
                    $('.ivl_' + _center).html(datas[0].toString()); //volume
                    $('.idf_' + _center).html(datas[1].toString()); //foreign
                    $('.ceiling_' + _center).html(_stats[0].toString()); //ceiling
                    $('.up_' + _center).html(_stats[1].toString()); //up
                    $('.normal_' + _center).html(_stats[2].toString()); //normal
                    $('.down_' + _center).html(_stats[3].toString()); //down
                    $('.floor_' + _center).html(_stats[4].toString()); //floor

                    if (_bivl) { $('.ivl_' + _center).css('background-color', '#FF6A00'); }
                    if (_bidf) { $('.idf_' + _center).css('background-color', '#FF6A00'); }
                    if (_bceiling) { $('.ceiling_' + _center).css('background-color', '#FF6A00'); }
                    if (_bup) { $('.up_' + _center).css('background-color', '#FF6A00'); }
                    if (_bnormal) { $('.normal_' + _center).css('background-color', '#FF6A00'); }
                    if (_bdown) { $('.down_' + _center).css('background-color', '#FF6A00'); }
                    if (_bfloor) { $('.floor_' + _center).css('background-color', '#FF6A00'); }
                    window.setTimeout("$('.idf_" + _center + ",.ivl_" + _center + ",.ceiling_" + _center + ",.up_" + _center + ",.normal_" + _center + ",.down_" + _center + ",.floor_" + _center + "').css('background-color', '');", 1000);
                } else {
                    continue;
                }
            }


            //hieubt - 21/12/2010
            //use cometd
            //if(isTrading) chart.join();
            //return;

            clearTimeout(this.timerId_Refresh);
            /*if (this.symbol_list.length > 0 && isTrading) {*/
            if (isTrading) {
                this.timerId_Refresh = setTimeout(this.instance_name + '.LoadSymbolData(false)', this.AUTO_REFRESH_TIME);
            }
        }
    }

    this.OpenPopup = function(id) {
        jQuery('#CafeF_StockSymbolSlidePopup').jqDrag('#CafeF_StockSymbolSlidePopupTitle');

        jQuery('#CafeF_StockSymbolSlideKeyword').autocomplete(oc, {
            minChars: 1,
            delay: 10,
            width: 300,
            matchContains: true,
            autoFill: false, max: 15,
            formatItem: function(row) {
                return row.c + " - " + row.m + "@" + row.o;
                //return row.m + "@" + row.o;
            },
            formatResult: function(row) {
                return row.c + " - " + row.m;
                //return row.m;
            },
            isAddSymbolToFavorite: true,
            CafeF_StockSymbolSlideObject: this
        });

        this.RefreshListOfSymbolInConfigWindow();

        jQuery('#' + id).show();
        var txt = document.getElementById('CafeF_StockSymbolSlideKeyword');
        txt.value = '';
        txt.focus();
    }

    this.ClosePopup = function(id) {
        var txt = document.getElementById('CafeF_StockSymbolSlideKeyword');
        txt.value = '';
        jQuery('#' + id).hide();
    }

    this.MoveLeft = function() {
        if (this.symbol_list.indexOf(this.display_list) == 0 || this.displayList == '') return;

        var listOfSymbol = this.symbol_list.split(';');
        var displayList = this.display_list.split(';');

        var startIndex = 0;

        for (startIndex = 0; startIndex < listOfSymbol.length; startIndex++) {
            if (listOfSymbol[startIndex] == displayList[0]) break;
        }

        this.display_list = '';
        for (var i = startIndex - 1, j = 0; i < listOfSymbol.length && j < this.MaxSymbolsDisplay; i++) {
            this.display_list += ';' + listOfSymbol[i];
            j++;
        }
        if (this.display_list != '') this.display_list = this.display_list.substring(1);
        //alert(this.display_list);
        this.LoadSymbolData(true);
    }

    this.MoveRight = function() {
        if (this.symbol_list.indexOf(this.display_list) + this.display_list.length == this.symbol_list.length || this.displayList == '') return;

        var listOfSymbol = this.symbol_list.split(';');
        var displayList = this.display_list.split(';');

        var startIndex = 0;

        for (startIndex = 0; startIndex < listOfSymbol.length; startIndex++) {
            if (listOfSymbol[startIndex] == displayList[0]) break;
        }

        this.display_list = '';
        for (var i = startIndex + 1, j = 0; i < listOfSymbol.length && j < this.MaxSymbolsDisplay; i++) {
            this.display_list += ';' + listOfSymbol[i];
            j++;
        }
        if (this.display_list != '') this.display_list = this.display_list.substring(1);
        //alert(this.display_list);
        this.LoadSymbolData(true);
    }

    this.AddSymbol = function() {
        //
    }

    this.AddSymbolToFavorite = function(symbol) {
        var listOfSymbol;

        var txt = document.getElementById('CafeF_StockSymbolSlideKeyword');

        if (this.symbol_list != '') {
            listOfSymbol = this.symbol_list.split(';');

            for (var i = 0; i < listOfSymbol.length; i++) {
                if (listOfSymbol[i] == symbol) {
                    txt.value = '';
                    return;
                }
            }

            if (listOfSymbol.length >= this.MaxSymbolsInQueue) {
                alert('Đã đủ 10 mã CK trong danh sách theo dõi');
                txt.value = '';
                return;
            }

            this.symbol_list += ';' + symbol;
        }
        else {
            this.symbol_list = symbol;
        }

        listOfSymbol = this.symbol_list.split(';');
        if (listOfSymbol.length <= this.MaxSymbolsDisplay) {
            this.display_list = this.symbol_list;
        }
        else {
            this.display_list = '';
            for (var i = listOfSymbol.length - this.MaxSymbolsDisplay; i < listOfSymbol.length; i++) {
                this.display_list += ';' + listOfSymbol[i];
            }
            if (this.display_list != '') this.display_list = this.display_list.substring(1);
        }

        this.SetCookie(this.CookiesName, this.symbol_list, this.bm_st_time_exp);

        this.LoadSymbolData(true);

        this.RefreshListOfSymbolInConfigWindow();
        txt.value = '';

        if (this.IsLogged == 0) {
            //this.Log();
            this.IsLogged = 1;
        }
    }

    this.RemoveAllSymbols = function() {
        this.display_list = '';
        this.symbol_list = '';

        this.SetCookie(this.CookiesName, this.symbol_list, this.bm_st_time_exp);
        this.InitScript(this.containerId);
        //this.LoadSymbolData(true);

        this.RefreshListOfSymbolInConfigWindow();
        var txt = document.getElementById('CafeF_StockSymbolSlideKeyword');
        txt.value = '';
        txt.focus();
    }

    this.RemoveSymbol = function(symbol) {
        var temp = ';' + this.display_list + ';';
        var listOfSymbol = this.symbol_list.split(';');
        this.symbol_list = '';
        var endDisplayIndex = -1;

        this.display_list = ';' + this.display_list + ';';
        this.display_list = this.display_list.replace(';' + symbol + ';', ';');
        this.display_list = this.display_list.substring(1, this.display_list.length - 1);

        var lastSymbol = (this.display_list == '' ? '' : this.display_list.substring(this.display_list.lastIndexOf(';') + 1));

        for (var i = 0, j = -1; i < listOfSymbol.length; i++) {
            if (listOfSymbol[i] != symbol) {
                j++;
                if (lastSymbol != '') {
                    if (endDisplayIndex == -1 && listOfSymbol[i] == lastSymbol) {
                        endDisplayIndex = j;
                    }
                }
                this.symbol_list += ';' + listOfSymbol[i];
            }
        }
        if (this.symbol_list != '') this.symbol_list = this.symbol_list.substring(1);
        listOfSymbol = this.symbol_list.split(';');

        if (endDisplayIndex < this.MaxSymbolsDisplay && listOfSymbol > this.MaxSymbolsDisplay) endDisplayIndex++;
        
        // Item bi xoa nam trong danh sach dang hien thi
        if (temp.indexOf(';' + symbol + ';') >= 0) {
            if (this.symbol_list.indexOf(this.display_list) == 0 && listOfSymbol.length > this.MaxSymbolsDisplay) endDisplayIndex++;

            this.display_list = '';
            for (var i = 0; i < this.MaxSymbolsDisplay && endDisplayIndex >= 0; i++) {
                this.display_list = ';' + listOfSymbol[endDisplayIndex] + this.display_list;
                endDisplayIndex--;
            }
        }
        if (this.display_list != '') this.display_list = this.display_list.substring(1);

        this.SetCookie(this.CookiesName, this.symbol_list, this.bm_st_time_exp);
        this.InitScript(this.containerId);
        this.LoadSymbolData(true);
        this.RefreshListOfSymbolInConfigWindow();
        var txt = document.getElementById('CafeF_StockSymbolSlideKeyword');
        txt.value = '';
        try { txt.focus(); } catch (e) { }
    }

    this.RefreshListOfSymbolInConfigWindow = function() {
        var strList = '';

        if (this.symbol_list != '') {
            strList = '<table align="center" width="90%" border="0" cellpadding="3" cellspacing="0">';

            var listOfSymbol = this.symbol_list.split(';');

            for (var i = 0; i < listOfSymbol.length; i++) {
                strList += '<tr>';
                strList += '<td style="border:none;"><span style="color: #aaaaaa">' + (i + 1) + '.</span> <strong>' + listOfSymbol[i] + '</strong></td>';
                strList += '<td style="border:none;"></td>';
                strList += '<td style="border:none;" align="right"><img alt="Xóa mã CK khỏi danh sách" onclick="' + this.instance_name + '.RemoveSymbol(\'' + listOfSymbol[i] + '\')" src="' + this.script_folder + 'images/delete.gif" /></td>';
                strList += '</tr>';
            }

            strList += "</table>";
        }
        jQuery('#CafeF_StockSymbolSlidePopupList').html(strList);
    }

    this.FormatNumber = function(value, displayZero) {
        if (value == '') return (displayZero ? '0' : '');
        try {
            var number = parseFloat(value);
            value = CafeF_JSLibrary.FormatNumber(number, 2, '.', ',');
            return (value);
        }
        catch (err) {
            return (displayZero ? '0' : '');
        }
    }

    this.UpdateCell = function(index, value) {
        //if (this.OldCellDatas[index] != value) {
            //var cell = document.getElementById('CafeF_StockSymbolSlideTable_Cell' + index);
            var cell = document.getElementById('stockbarli' + index);
            if (cell) {
                cell.innerHTML = value;
            }
            if (value.toString().indexOf("table") > 0)
                $('#stockbarli' + index).attr('onclick', '');
            else
                $('#stockbarli' + index).attr('onclick', this.instance_name + '.OpenPopup(\'CafeF_StockSymbolSlidePopup\');');
            this.OldCellDatas[index] = value
        //}
    }

    this.CreateScriptObject = function(src, useScriptObject) {
        if (useScriptObject && this.script_object) {
            this.RemoveScriptObject();
        }

        this.script_object = document.createElement('script');

        this.script_object.setAttribute('type', 'text/javascript');
        this.script_object.setAttribute('src', src);

        var head = document.getElementsByTagName('head')[0];
        head.appendChild(this.script_object);
    }

    this.AppendScriptObject = function(script) {
        var obj = document.createElement('script');

        obj.setAttribute('type', 'text/javascript');

        obj.appendChild(document.createTextNode(script));

        var head = document.getElementsByTagName('head')[0];
        head.appendChild(obj);
    }

    this.RemoveScriptObject = function() {
        this.script_object.parentNode.removeChild(this.script_object);
        this.script_object = null;
    }

    this.SetCookie = function(c_name, value, time_expire) {
        var exdate = new Date();

        exdate.setDate(exdate.getDate() + time_expire);

        document.cookie = c_name + '=' + escape(value) + ((time_expire == null) ? '' : ';expires=' + exdate.toGMTString()) + ';path=/';
    }
    this.GetCookies = function(c_name) {
        if (document.cookie.length > 0) {
            var c_start = document.cookie.indexOf(c_name + "=");
            if (c_start != -1) {
                c_start = c_start + c_name.length + 1;
                var c_end = document.cookie.indexOf(";", c_start);
                if (c_end == -1) c_end = document.cookie.length;
                return unescape(document.cookie.substring(c_start, c_end));
            }
        }
        return '';
    }
}