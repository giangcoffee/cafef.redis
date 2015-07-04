/* cometd.js */
if(typeof dojo!=="undefined")dojo.provide("org.cometd");else{this.org=this.org||{};org.cometd={}}org.cometd.JSON={};org.cometd.JSON.toJSON=org.cometd.JSON.fromJSON=function(){throw"Abstract";};org.cometd.TransportRegistry=function(){var v=[],u={};this.getTransportTypes=function(){return v.slice(0)};this.findTransportTypes=function(s,q){for(var E=[],n=0;n<v.length;++n){var A=v[n];u[A].accept(s,q)&&E.push(A)}return E};this.negotiateTransport=function(s,q,E){for(var n=0;n<v.length;++n)for(var A=v[n],y=0;y<s.length;++y)if(A==s[y]){var h=u[A];if(h.accept(q,E)===true)return h}return null};this.add=function(s,q,E){for(var n=false,A=0;A<v.length;++A)if(v[A]==s){n=true;break}if(!n){typeof E!=="number"?v.push(s):v.splice(E,0,s);u[s]=q}return!n};this.remove=function(s){for(var q=0;q<v.length;++q)if(v[q]==s){v.splice(q,1);q=u[s];delete u[s];return q}return null};this.reset=function(){for(var s=0;s<v.length;++s)u[v[s]].reset()}};org.cometd.Cometd=function(v){function u(a,b){for(var c=b||{},d=2;d<arguments.length;++d){var e=arguments[d];if(!(e===undefined||e===null))for(var i in e){var j=e[i];if(j!==b)if(j!==undefined)c[i]=a&&typeof j==="object"&&j!==null?j instanceof Array?u(a,[],j):u(a,{},j):j}}return c}function s(a,b){for(var c=0;c<b.length;++c)if(a==b[c])return c;return-1}function q(a){if(a===undefined||a===null)return false;return typeof a==="string"||a instanceof String}function E(a){if(a===undefined||a===null)return false;return a instanceof Array}function n(a){if(a===undefined||a===null)return false;return typeof a==="function"}function A(a,b){if(window.console){var c=window.console[a];n(c)&&c.apply(window.console,b)}}function y(){U!="warn"&&A("info",arguments)}function h(){U=="debug"&&A("debug",arguments)}function ka(){for(var a in I)for(var b=I[a],c=0;c<b.length;++c){var d=b[c];if(d&&!d.listener){delete b[c];h("Removed subscription",d,"for channel",a)}}}function z(a){if(L!=a){h("Status",L,"->",a);L=a}}function x(){return L=="disconnecting"||L=="disconnected"}function la(a,b,c,d,e){try{return b.call(a,d)}catch(i){h("Exception during execution of extension",c,i);a=M.onExtensionException;if(n(a)){h("Invoking extension exception callback",c,i);try{a.call(M,i,c,e,d)}catch(j){y("Exception during execution of exception callback in extension",c,j)}}return d}}function Ba(a){for(var b=0;b<B.length;++b){if(a===undefined||a===null)break;var c=B[b],d=c.extension.outgoing;if(n(d)){c=la(c.extension,d,c.name,a,true);a=c===undefined?a:c}}return a}function ca(a,b){var c=I[a];if(c&&c.length>0)for(var d=0;d<c.length;++d){var e=c[d];if(e)try{e.callback.call(e.scope,b)}catch(i){h("Exception during notification",e,b,i);var j=M.onListenerException;if(n(j)){h("Invoking listener exception callback",e,i);try{j.call(M,i,e.handle,e.listener,b)}catch(p){y("Exception during execution of listener callback",e,p)}}}}}function l(a,b){ca(a,b);for(var c=a.split("/"),d=c.length-1,e=d;e>0;--e){var i=c.slice(0,e).join("/")+"/*";e==d&&ca(i,b);i+="*";ca(i,b)}}function F(a,b){return setTimeout(function(){try{a()}catch(c){h("Exception invoking timed function",a,c)}},b)}function ma(){V!==null&&clearTimeout(V);V=null}function na(a){ma();var b=w.interval+t;h("Function scheduled in",b,"ms, interval =",w.interval,"backoff =",t,a);V=F(a,b)}function O(a,b,c,d){for(var e=0;e<b.length;++e){var i=b[e];i.id=""+ ++Ca;if(N)i.clientId=N;i=Ba(i);if(i!==undefined&&i!==null)b[e]=i;else b.splice(e--,1)}if(b.length!==0){e=P;if(Q){e.match(/\/$/)||(e+="/");if(d)e+=d}d={url:e,sync:a,messages:b,onSuccess:function(j){try{oa.call(M,j)}catch(p){h("Exception during handling of messages",p)}},onFailure:function(j,p,m){try{pa.call(M,j,b,p,m)}catch(g){h("Exception during handling of failure",g)}}};h("Send, sync =",a,d);G.send(d,c)}}function W(a){K>0||R===true?X.push(a):O(false,[a],false)}function Y(){if(t<qa)t+=Z}function ra(){var a=X;X=[];a.length>0&&O(false,a,false)}function $(){z("connecting");na(function(){if(!x()){var a={channel:"/meta/connect",connectionType:G.getType()};if(!da)a.advice={timeout:0};z("connecting");h("Connect sent",a);O(false,[a],true,"connect");z("connected")}})}function ea(a){if(a){w=u(false,{},S,a);h("New advice",w,org.cometd.JSON.toJSON(w))}}function sa(a){N=null;ka();x()&&J.reset();x()?ea(S):ea(u(false,w,{reconnect:"retry"}));K=0;R=true;fa=a;a=J.findTransportTypes("1.0",T);var b=u(false,{},fa,{version:"1.0",minimumVersion:"0.9",channel:"/meta/handshake",supportedConnectionTypes:a,advice:{timeout:w.timeout,interval:w.interval}});G=J.negotiateTransport(a,"1.0",T);h("Initial transport is",G);z("handshaking");h("Handshake sent",b);O(false,[b],false,"handshake")}function aa(){z("handshaking");R=true;na(function(){sa(fa)})}function ga(a){ma();a&&G.abort();N=null;z("disconnected");K=0;X=[];t=0}function ta(a){a=a;for(var b=0;b<B.length;++b){if(a===undefined||a===null)break;var c=B[ua?B.length-1-b:b],d=c.extension.incoming;if(n(d)){c=la(c.extension,d,c.name,a,false);a=c===undefined?a:c}}a=a;if(!(a===undefined||a===null)){ea(a.advice);switch(a.channel){case "/meta/handshake":a=a;if(a.successful){N=a.clientId;b=J.negotiateTransport(a.supportedConnectionTypes,a.version,T);if(b===null)throw"Could not negotiate transport with server; client "+J.findTransportTypes(a.version,T)+", server "+a.supportedConnectionTypes;else if(G!=b){h("Transport",G,"->",b);G=b}R=false;ra();a.reestablish=ha;ha=true;l("/meta/handshake",a);a=x()?"none":w.reconnect;switch(a){case "retry":t=0;$();break;case "none":t=0;z("disconnected");break;default:throw"Unrecognized advice action "+a;}}else{l("/meta/handshake",a);l("/meta/unsuccessful",a);if(!x()&&w.reconnect!="none"){Y();aa()}else{t=0;z("disconnected")}}break;case "/meta/connect":a=a;if(da=a.successful){l("/meta/connect",a);a=x()?"none":w.reconnect;switch(a){case "retry":t=0;$();break;case "none":t=0;z("disconnected");break;default:throw"Unrecognized advice action "+a;}}else{y("Connect failed:",a.error);l("/meta/connect",a);l("/meta/unsuccessful",a);a=x()?"none":w.reconnect;switch(a){case "retry":Y();$();break;case "handshake":t=0;aa();break;case "none":t=0;z("disconnected");break;default:throw"Unrecognized advice action"+a;}}break;case "/meta/disconnect":a=a;if(a.successful){ga(false);l("/meta/disconnect",a)}else{ga(true);l("/meta/disconnect",a);l("/meta/unsuccessful",a)}break;case "/meta/subscribe":a=a;if(a.successful)l("/meta/subscribe",a);else{y("Subscription to",a.subscription,"failed:",a.error);l("/meta/subscribe",a);l("/meta/unsuccessful",a)}break;case "/meta/unsubscribe":a=a;if(a.successful)l("/meta/unsubscribe",a);else{y("Unsubscription to",a.subscription,"failed:",a.error);l("/meta/unsubscribe",a);l("/meta/unsuccessful",a)}break;default:a=a;if(a.successful===undefined)a.data?l(a.channel,a):h("Unknown message",a);else if(a.successful)l("/meta/publish",a);else{y("Publish failed:",a.error);l("/meta/publish",a);l("/meta/unsuccessful",a)}}}}function va(a){if(a=I[a])for(var b=0;b<a.length;++b)if(a[b])return true;return false}function wa(a,b){var c={scope:a,method:b};if(n(a)){c.scope=undefined;c.method=a}else if(q(b)){if(!a)throw"Invalid scope "+a;c.method=a[b];if(!n(c.method))throw"Invalid callback "+b+" for scope "+a;}else if(!n(b))throw"Invalid callback "+b;return c}function xa(a,b,c,d){b=wa(b,c);h("Adding listener on",a,"with scope",b.scope,"and callback",b.method);d={channel:a,scope:b.scope,callback:b.method,listener:d};b=I[a];if(!b){b=[];I[a]=b}b=b.push(d)-1;d.id=b;d.handle=[a,b];h("Added listener",d,"for channel",a,"having id =",b);return d.handle}var M=this,Da=v||"default",U,P,ya,Z,qa,ua,ba,ia,Q,za,T=false,J=new org.cometd.TransportRegistry,G,L="disconnected",Ca=0,N=null,K=0,X=[],R=false,I={},t=0,V=null,B=[],S={},w={},fa,ha=false,da=false;this._mixin=u;this._warn=function(){A("warn",arguments)};this._info=y;this._debug=h;var oa,pa;this.send=W;this.receive=ta;oa=function(a){h("Received",a,org.cometd.JSON.toJSON(a));for(var b=0;b<a.length;++b)ta(a[b])};pa=function(a,b,c,d){h("handleFailure",a,b,c,d);for(c=0;c<b.length;++c){d=b[c];switch(d.channel){case "/meta/handshake":d={successful:false,failure:true,channel:"/meta/handshake",request:d,xhr:a,advice:{reconnect:"retry",interval:t}};l("/meta/handshake",d);l("/meta/unsuccessful",d);if(!x()&&w.reconnect!="none"){Y();aa()}else{t=0;z("disconnected")}break;case "/meta/connect":var e=a;d=d;da=false;d={successful:false,failure:true,channel:"/meta/connect",request:d,xhr:e,advice:{reconnect:"retry",interval:t}};l("/meta/connect",d);l("/meta/unsuccessful",d);d=x()?"none":w.reconnect;switch(d){case "retry":Y();$();break;case "handshake":t=0;aa();break;case "none":t=0;z("disconnected");break;default:throw"Unrecognized advice action"+d;}break;case "/meta/disconnect":e=a;d=d;ga(true);d={successful:false,failure:true,channel:"/meta/disconnect",request:d,xhr:e,advice:{reconnect:"none",interval:0}};l("/meta/disconnect",d);l("/meta/unsuccessful",d);break;case "/meta/subscribe":d={successful:false,failure:true,channel:"/meta/subscribe",request:d,xhr:a,advice:{reconnect:"none",interval:0}};l("/meta/subscribe",d);l("/meta/unsuccessful",d);break;case "/meta/unsubscribe":d={successful:false,failure:true,channel:"/meta/unsubscribe",request:d,xhr:a,advice:{reconnect:"none",interval:0}};l("/meta/unsubscribe",d);l("/meta/unsuccessful",d);break;default:d={successful:false,failure:true,channel:d.channel,request:d,xhr:a,advice:{reconnect:"none",interval:0}};l("/meta/publish",d);l("/meta/unsuccessful",d)}}};this.registerTransport=function(a,b,c){if(c=J.add(a,b,c)){h("Registered transport",a);n(b.registered)&&b.registered(a,this)}return c};this.getTransportTypes=function(){return J.getTransportTypes()};this.unregisterTransport=function(a){var b=J.remove(a);if(b!==null){h("Unregistered transport",a);n(b.unregistered)&&b.unregistered()}return b};this.configure=function(a){a=a;h("Configuring cometd object with",a);if(q(a))a={url:a};a||(a={});P=a.url;if(!P)throw"Missing required configuration parameter 'url' specifying the Bayeux server URL";ya=a.maxConnections||2;Z=a.backoffIncrement||1E3;qa=a.maxBackoff||6E4;U=a.logLevel||"info";ua=a.reverseIncomingExtensions!==false;ba=a.maxNetworkDelay||1E4;ia=a.requestHeaders||{};Q=a.appendMessageTypeToURL!==false;za=a.autoBatch===true;S.timeout=a.timeout||6E4;S.interval=a.interval||0;S.reconnect=a.reconnect||"retry";a=/(^https?:)?(\/\/(([^:\/\?#]+)(:(\d+))?))?([^\?#]*)(.*)?/.exec(P);T=a[3]&&a[3]!=window.location.host;if(Q)if(a[8]!==undefined){y("Appending message type to URI "+a[7]+a[8]+" is not supported, disabling 'appendMessageTypeToURL' configuration");Q=false}else{var b=a[7].split("/"),c=b.length-1;if(a[7].match(/\/$/))c-=1;if(b[c].indexOf(".")>=0){y("Appending message type to URI "+a[7]+" is not supported, disabling 'appendMessageTypeToURL' configuration");Q=false}}};this.init=function(a,b){this.configure(a);this.handshake(b)};this.handshake=function(a){z("disconnected");ha=false;sa(a)};this.disconnect=function(a,b){if(!x()){if(b===undefined)if(typeof a!=="boolean"){b=a;a=false}var c=u(false,{},b,{channel:"/meta/disconnect"});z("disconnecting");O(a===true,[c],false,"disconnect")}};this.startBatch=function(){++K};this.endBatch=function(){--K;if(K<0)throw"Calls to startBatch() and endBatch() are not paired";K===0&&!x()&&!R&&ra()};this.batch=function(a,b){var c=wa(a,b);this.startBatch();try{c.method.call(c.scope);this.endBatch()}catch(d){h("Exception during execution of batch",d);this.endBatch();throw d;}};this.addListener=function(a,b,c){if(arguments.length<2)throw"Illegal arguments number: required 2, got "+arguments.length;if(!q(a))throw"Illegal argument type: channel must be a string";return xa(a,b,c,true)};this.removeListener=function(a){if(!E(a))throw"Invalid argument: expected subscription, not "+a;var b=I[a[0]];if(b){delete b[a[1]];h("Removed listener",a)}};this.clearListeners=function(){I={}};this.subscribe=function(a,b,c,d){if(arguments.length<2)throw"Illegal arguments number: required 2, got "+arguments.length;if(!q(a))throw"Illegal argument type: channel must be a string";if(x())throw"Illegal state: already disconnected";if(n(b)){d=c;c=b;b=undefined}var e=!va(a),i=xa(a,b,c,false);if(e){e=u(false,{},d,{channel:"/meta/subscribe",subscription:a});W(e)}return i};this.unsubscribe=function(a,b){if(arguments.length<1)throw"Illegal arguments number: required 1, got "+arguments.length;if(x())throw"Illegal state: already disconnected";this.removeListener(a);var c=a[0];if(!va(c)){c=u(false,{},b,{channel:"/meta/unsubscribe",subscription:c});W(c)}};this.clearSubscriptions=function(){ka()};this.publish=function(a,b,c){if(arguments.length<1)throw"Illegal arguments number: required 1, got "+arguments.length;if(!q(a))throw"Illegal argument type: channel must be a string";if(x())throw"Illegal state: already disconnected";var d=u(false,{},c,{channel:a,data:b});W(d)};this.getStatus=function(){return L};this.isDisconnected=x;this.setBackoffIncrement=function(a){Z=a};this.getBackoffIncrement=function(){return Z};this.getBackoffPeriod=function(){return t};this.setLogLevel=function(a){U=a};this.registerExtension=function(a,b){if(arguments.length<2)throw"Illegal arguments number: required 2, got "+arguments.length;if(!q(a))throw"Illegal argument type: extension name must be a string";for(var c=false,d=0;d<B.length;++d)if(B[d].name==a){c=true;break}if(c){y("Could not register extension with name",a,"since another extension with the same name already exists");return false}else{B.push({name:a,extension:b});h("Registered extension",a);n(b.registered)&&b.registered(a,this);return true}};this.unregisterExtension=function(a){if(!q(a))throw"Illegal argument type: extension name must be a string";for(var b=false,c=0;c<B.length;++c){var d=B[c];if(d.name==a){B.splice(c,1);b=true;h("Unregistered extension",a);a=d.extension;n(a.unregistered)&&a.unregistered();break}}return b};this.getExtension=function(a){for(var b=0;b<B.length;++b){var c=B[b];if(c.name==a)return c.extension}return null};this.getName=function(){return Da};this.getClientId=function(){return N};this.getURL=function(){return P};this.getTransport=function(){return G};org.cometd.Transport=function(){var a;this.registered=function(b){a=b};this.unregistered=function(){a=null};this.convertToMessages=function(b){if(q(b))try{return org.cometd.JSON.fromJSON(b)}catch(c){h("Could not convert to JSON the following string",'"'+b+'"');throw c;}if(E(b))return b;if(b===undefined||b===null)return[];if(b instanceof Object)return[b];throw"Conversion Error "+b+", typeof "+typeof b;};this.accept=function(){throw"Abstract";};this.getType=function(){return a};this.send=function(){throw"Abstract";};this.reset=function(){}};org.cometd.Transport.derive=function(a){function b(){}b.prototype=a;return new b};org.cometd.RequestTransport=function(){function a(g){for(;m.length>0;){var f=m[0],k=f[0];f=f[1];if(k.url===g.url&&k.sync===g.sync){m.shift();g.messages=g.messages.concat(k.messages);h("Coalesced",k.messages.length,"messages from request",f.id)}else break}}function b(g,f){var k=this,o=s(g,p);o>=0&&p.splice(o,1);if(m.length>0){o=m.shift();var C=o[0],r=o[1];h("Transport dequeued request",r.id);if(f){za&&a(C);d.call(this,C);h("Transport completed request",g.id,C)}else setTimeout(function(){k.complete(r,false,r.metaConnect);C.onFailure(r.xhr,"error","Previous request failed")},0)}}function c(g,f){var k=this;this.transportSend(g,f);f.expired=false;if(!g.sync){var o=ba;if(f.metaConnect===true)o+=w.timeout;h("Will wait at most",o,"ms for the response, maxNetworkDelay =",ba);f.timeout=F(function(){f.expired=true;f.xhr&&f.xhr.abort();var C="Request "+f.id+" of transport "+k.getType()+" exceeded "+o+" ms max network delay";h(C);k.complete(f,false,f.metaConnect);g.onFailure(f.xhr,"timeout",C)},o)}}function d(g){var f=++i,k={id:f,metaConnect:false};if(p.length<ya-1){h("Transport sending request",f,g);c.call(this,g,k);p.push(k)}else{h("Transport queueing request",f,g);m.push([g,k])}}var e=new org.cometd.Transport;e=org.cometd.Transport.derive(e);var i=0,j=null,p=[],m=[];e.complete=function(g,f,k){if(k){g=g.id;h("metaConnect complete",this.getType(),g);if(j!==null&&j.id!==g)throw"Longpoll request mismatch, completing request "+g;j=null}else b.call(this,g,f)};e.transportSend=function(){throw"Abstract";};e.transportSuccess=function(g,f,k){if(!f.expired){clearTimeout(f.timeout);this.complete(f,true,f.metaConnect);k&&k.length>0?g.onSuccess(k):g.onFailure(f,"Empty HTTP response")}};e.transportFailure=function(g,f,k,o){if(!f.expired){clearTimeout(f.timeout);this.complete(f,false,f.metaConnect);g.onFailure(f.xhr,k,o)}};e.send=function(g,f){if(f){if(j!==null)throw"Concurrent metaConnect requests not allowed, request id="+j.id+" not yet completed";var k=++i;h("metaConnect send ",this.getType(),k,g);k={id:k,metaConnect:true};c.call(this,g,k);j=k}else d.call(this,g)};e.abort=function(){for(var g=0;g<p.length;++g){var f=p[g];h("Aborting request",f);f.xhr&&f.xhr.abort()}if(j){h("Aborting metaConnect request",j);j.xhr&&j.xhr.abort()}this.reset()};e.reset=function(){j=null;p=[];m=[]};e.toString=function(){return this.getType()};return e};org.cometd.LongPollingTransport=function(){var a=new org.cometd.RequestTransport,b=org.cometd.Transport.derive(a),c=true;b.accept=function(d,e){return c||!e};b.xhrSend=function(){throw"Abstract";};b.transportSend=function(d,e){var i=this;try{var j=true;e.xhr=this.xhrSend({transport:this,url:d.url,sync:d.sync,headers:ia,body:org.cometd.JSON.toJSON(d.messages),onSuccess:function(m){h("Transport",i,"received response",m);var g=false;try{var f=i.convertToMessages(m);if(f.length===0){c=false;i.transportFailure(d,e,"no response",null)}else{g=true;i.transportSuccess(d,e,f)}}catch(k){h(k);if(!g){c=false;i.transportFailure(d,e,"bad response",k)}}},onError:function(m,g){c=false;j?F(function(){i.transportFailure(d,e,m,g)},0):i.transportFailure(d,e,m,g)}});j=false}catch(p){c=false;F(function(){i.transportFailure(d,e,"error",p)},0)}};b.reset=function(){a.reset();c=true};return b};org.cometd.CallbackPollingTransport=function(){var a=new org.cometd.RequestTransport;a=org.cometd.Transport.derive(a);a.accept=function(){return true};a.jsonpSend=function(){throw"Abstract";};a.transportSend=function(b,c){var d=this,e=org.cometd.JSON.toJSON(b.messages),i=b.url.length+encodeURI(e).length;if(i>2E3){var j=b.messages.length>1?"Too many bayeux messages in the same batch resulting in message too big ("+i+" bytes, max is 2000) for transport "+this.getType():"Bayeux message too big ("+i+" bytes, max is 2000) for transport "+this.getType();F(function(){d.transportFailure(b,c,"error",j)},0)}else try{var p=true;this.jsonpSend({transport:this,url:b.url,sync:b.sync,headers:ia,body:e,onSuccess:function(g){var f=false;try{var k=d.convertToMessages(g);if(k.length===0)d.transportFailure(b,c,"no response",null);else{f=true;d.transportSuccess(b,c,k)}}catch(o){h(o);f||d.transportFailure(b,c,"bad response",o)}},onError:function(g,f){p?F(function(){d.transportFailure(b,c,g,f)},0):d.transportFailure(b,c,g,f)}});p=false}catch(m){F(function(){d.transportFailure(b,c,"error",m)},0)}};return a};org.cometd.WebSocketTransport=function(){function a(f,k){if(d.send(org.cometd.JSON.toJSON(f.messages))){var o=ba;if(k)o+=w.timeout;for(var C=0;C<f.messages.length;++C){var r=f.messages[C];if(r.id){m[r.id]=F(function(){delete m[r.id];var D="TIMEOUT message "+r.id+" exceeded "+o+"ms";h(D);f.onFailure(d,"timeout",D)},o);h("waiting",o," for  ",r.id,org.cometd.JSON.toJSON(m))}}}else F(function(){f.onFailure(d,"failed",null)},0)}var b=new org.cometd.Transport,c=org.cometd.Transport.derive(b),d,e=true,i,j,p,m={},g;if(window.WebSocket){g=window.WebSocket;j=g.CLOSED}c.accept=function(){return e&&g!==null&&typeof g==="function"};c.send=function(f,k){h("Transport",this,"sending",f,"metaConnect",k);if(k)p=f;else i=f;if(j===g.OPEN)a(f,k);else{var o=f.url.replace(/^http/,"ws");h("Transport",this,"URL",o);var C=this,r=new g(o);r.onopen=function(){h("Opened",r);j=g.OPEN;d=r;a(f,k)};r.onclose=function(){h("Closed",r);if(j!==g.OPEN){e=false;f.onFailure(r,"cannot open",null)}else{j=g.CLOSED;for(var D in m){clearTimeout(m[D]);delete m[D]}}};r.onmessage=function(D){h("Message",D);if(j===g.OPEN){D=C.convertToMessages(D.data);for(var Aa=false,ja=0;ja<D.length;++ja){var H=D[ja];if("/meta/connect"==H.channelId)Aa=true;h("timeouts",m,org.cometd.JSON.toJSON(m));if(!H.data&&H.id&&m[H.id]){h("timeout",m[H.id]);clearTimeout(m[H.id]);delete m[H.id]}"/meta/disconnect"==H.channel&&H.successful&&r.close()}Aa?p.onSuccess(D):i.onSuccess(D)}else f.onFailure(r,"closed",null)}}};c.reset=function(){h("reset",d);b.reset();d&&d.close();e=true;j=g.CLOSED;p=i=null};return c}};
/* AckExtension.js */
typeof dojo!="undefined"&&dojo.provide("org.cometd.AckExtension");org.cometd.AckExtension=function(){var c,e=false,d=-1;this.registered=function(a,b){c=b;c._debug("AckExtension: executing registration callback",void 0)};this.unregistered=function(){c._debug("AckExtension: executing unregistration callback",void 0);c=null};this.incoming=function(a){var b=a.channel;if(b=="/meta/handshake"){e=a.ext&&a.ext.ack;c._debug("AckExtension: server supports acks",e)}else if(e&&b=="/meta/connect"&&a.successful)if((b=a.ext)&&typeof b.ack==="number"){d=b.ack;c._debug("AckExtension: server sent ack id",d)}return a};this.outgoing=function(a){var b=a.channel;if(b=="/meta/handshake"){if(!a.ext)a.ext={};a.ext.ack=c&&c.ackEnabled!==false;d=-1}else if(e&&b=="/meta/connect"){if(!a.ext)a.ext={};a.ext.ack=d;c._debug("AckExtension: client sending ack id",d)}return a}};
/* ReloadExtension.js */
typeof dojo!="undefined"&&dojo.provide("org.cometd.ReloadExtension");if(!org.cometd.COOKIE){org.cometd.COOKIE={};org.cometd.COOKIE.set=function(){throw"Abstract";};org.cometd.COOKIE.get=function(){throw"Abstract";}}org.cometd.ReloadExtension=function(i){function k(){if(a&&a.handshakeResponse!==null){var b=org.cometd.JSON.toJSON(a);c("Reload extension saving cookie value",b);org.cometd.COOKIE.set("org.cometd.reload",b,{"max-age":j,path:"/",expires:new Date((new Date).getTime()+j*1E3)})}}var d,c,a=null,j=i&&i.cookieMaxAge||5,g=false;this.registered=function(b,e){d=e;d.reload=k;c=d._debug};this.unregistered=function(){delete d.reload;d=null};this.outgoing=function(b){var e=b.channel;if(e=="/meta/handshake"){a={};a.url=d.getURL();e=org.cometd.COOKIE.get("org.cometd.reload");c("Reload extension found cookie value",e);if(e)try{org.cometd.COOKIE.set("org.cometd.reload","",{path:"/"});var f=org.cometd.JSON.fromJSON(e);if(f&&f.handshakeResponse&&a.url==f.url){c("Reload extension restoring state",f);setTimeout(function(){c("Reload extension replaying handshake response",f.handshakeResponse);a.handshakeResponse=f.handshakeResponse;a.transportType=f.transportType;a.reloading=true;var h=d._mixin(true,{},a.handshakeResponse);h.supportedConnectionTypes=[a.transportType];d.receive(h);c("Reload extension replayed handshake response",h)},0);if(!g){g=true;d.startBatch()}return null}else c("Reload extension could not restore state",f)}catch(l){c("Reload extension error while trying to restore cookie",l)}}else if(e=="/meta/connect")if(!a.transportType){a.transportType=b.connectionType;c("Reload extension tracked transport type",a.transportType)}return b};this.incoming=function(b){if(b.successful)switch(b.channel){case "/meta/handshake":if(!a.handshakeResponse){a.handshakeResponse=b;c("Reload extension tracked handshake response",b)}break;case "/meta/disconnect":a=null;break;case "/meta/connect":g&&d.endBatch();g=false}return b}};
/* jquery.json-2.2.js */
/*
 * jQuery JSON Plugin
 * version: 2.1 (2009-08-14)
 *
 * This document is licensed as free software under the terms of the
 * MIT License: http://www.opensource.org/licenses/mit-license.php
 *
 * Brantley Harris wrote this plugin. It is based somewhat on the JSON.org 
 * website's http://www.json.org/json2.js, which proclaims:
 * "NO WARRANTY EXPRESSED OR IMPLIED. USE AT YOUR OWN RISK.", a sentiment that
 * I uphold.
 *
 * It is also influenced heavily by MochiKit's serializeJSON, which is 
 * copyrighted 2005 by Bob Ippolito.
 */ 
(function($) {
    /** jQuery.toJSON( json-serializble )
        Converts the given argument into a JSON respresentation.

        If an object has a "toJSON" function, that will be used to get the representation.
        Non-integer/string keys are skipped in the object, as are keys that point to a function.

        json-serializble:
            The *thing* to be converted.
     **/
    $.toJSON = function(o)
    {
        if (typeof(JSON) == 'object' && JSON.stringify)
            return JSON.stringify(o);
        
        var type = typeof(o);
    
        if (o === null)
            return "null";
    
        if (type == "undefined")
            return undefined;
        
        if (type == "number" || type == "boolean")
            return o + "";
    
        if (type == "string")
            return $.quoteString(o);
    
        if (type == 'object')
        {
            if (typeof o.toJSON == "function") 
                return $.toJSON( o.toJSON() );
            
            if (o.constructor === Date)
            {
                var month = o.getUTCMonth() + 1;
                if (month < 10) month = '0' + month;

                var day = o.getUTCDate();
                if (day < 10) day = '0' + day;

                var year = o.getUTCFullYear();
                
                var hours = o.getUTCHours();
                if (hours < 10) hours = '0' + hours;
                
                var minutes = o.getUTCMinutes();
                if (minutes < 10) minutes = '0' + minutes;
                
                var seconds = o.getUTCSeconds();
                if (seconds < 10) seconds = '0' + seconds;
                
                var milli = o.getUTCMilliseconds();
                if (milli < 100) milli = '0' + milli;
                if (milli < 10) milli = '0' + milli;

                return '"' + year + '-' + month + '-' + day + 'T' +
                             hours + ':' + minutes + ':' + seconds + 
                             '.' + milli + 'Z"'; 
            }

            if (o.constructor === Array) 
            {
                var ret = [];
                for (var i = 0; i < o.length; i++)
                    ret.push( $.toJSON(o[i]) || "null" );

                return "[" + ret.join(",") + "]";
            }
        
            var pairs = [];
            for (var k in o) {
                var name;
                var type = typeof k;

                if (type == "number")
                    name = '"' + k + '"';
                else if (type == "string")
                    name = $.quoteString(k);
                else
                    continue;  //skip non-string or number keys
            
                if (typeof o[k] == "function") 
                    continue;  //skip pairs where the value is a function.
            
                var val = $.toJSON(o[k]);
            
                pairs.push(name + ":" + val);
            }

            return "{" + pairs.join(", ") + "}";
        }
    };

    /** jQuery.evalJSON(src)
        Evaluates a given piece of json source.
     **/
    $.evalJSON = function(src)
    {
        if (typeof(JSON) == 'object' && JSON.parse)
            return JSON.parse(src);
        return eval("(" + src + ")");
    };
    
    /** jQuery.secureEvalJSON(src)
        Evals JSON in a way that is *more* secure.
    **/
    $.secureEvalJSON = function(src)
    {
        if (typeof(JSON) == 'object' && JSON.parse)
            return JSON.parse(src);
        
        var filtered = src;
        filtered = filtered.replace(/\\["\\\/bfnrtu]/g, '@');
        filtered = filtered.replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, ']');
        filtered = filtered.replace(/(?:^|:|,)(?:\s*\[)+/g, '');
        
        if (/^[\],:{}\s]*$/.test(filtered))
            return eval("(" + src + ")");
        else
            throw new SyntaxError("Error parsing JSON, source is not valid.");
    };

    /** jQuery.quoteString(string)
        Returns a string-repr of a string, escaping quotes intelligently.  
        Mostly a support function for toJSON.
    
        Examples:
            >>> jQuery.quoteString("apple")
            "apple"
        
            >>> jQuery.quoteString('"Where are we going?", she asked.')
            "\"Where are we going?\", she asked."
     **/
    $.quoteString = function(string)
    {
        if (string.match(_escapeable))
        {
            return '"' + string.replace(_escapeable, function (a) 
            {
                var c = _meta[a];
                if (typeof c === 'string') return c;
                c = a.charCodeAt();
                return '\\u00' + Math.floor(c / 16).toString(16) + (c % 16).toString(16);
            }) + '"';
        }
        return '"' + string + '"';
    };
    
    var _escapeable = /["\\\x00-\x1f\x7f-\x9f]/g;
    
    var _meta = {
        '\b': '\\b',
        '\t': '\\t',
        '\n': '\\n',
        '\f': '\\f',
        '\r': '\\r',
        '"' : '\\"',
        '\\': '\\\\'
    };
})(jQuery);
/*jquery.cookie.js*/
/**
 * Dual licensed under the Apache License 2.0 and the MIT license.
 * $Revision: 740 $ $Date: 2009-12-17 03:59:25 +1100 (Thu, 17 Dec 2009) $
 */
(function($)
{
    var _defaultConfig = {
        'max-age' : 30 * 60,
        path : '/'
    };

    function _set(key, value, options)
    {
        var o = $.extend({}, _defaultConfig, options);
        if (value === null || value === undefined)
        {
            value = '';
            o['max-age'] = 0;
            o.expires = new Date(new Date().getTime() - 1000);
        }

        // Create the cookie string
        var result = key + '=' + encodeURIComponent(value);
        if (o.expires && o.expires.toUTCString)
        {
            result += '; expires=' + o.expires.toUTCString();
        }
        if (o['max-age'] && typeof o['max-age'] === 'number')
        {
            result += '; max-age=' + o['max-age'];
        }
        if (o.path)
        {
            result += '; path=' + (o.path);
        }
        if (o.domain)
        {
            result += '; domain=' + (o.domain);
        }
        if (o.secure)
        {
            result +='; secure';
        }

        document.cookie = result;
    }

    function _get(key)
    {
        var cookies = document.cookie.split(';');
        for (var i = 0; i < cookies.length; ++i)
        {
            var cookie = $.trim(cookies[i]);
            if (cookie.substring(0, key.length + 1) == (key + '='))
            {
                return decodeURIComponent(cookie.substring(key.length + 1));
            }
        }
        return null;
    }

    $.cookie = function(key, value, options)
    {
        if (arguments.length > 1)
        {
            _set(key, value, options);
            return undefined;
        }
        else
        {
            return _get(key);
        }
    };

})(jQuery);
/*jquery.cometd.js*/
/**
 * Dual licensed under the Apache License 2.0 and the MIT license.
 * $Revision$ $Date: 2010-05-27 23:51:02 +1000 (Thu, 27 May 2010) $
 */
(function($)
{
    // Remap cometd JSON functions to jquery JSON functions
    org.cometd.JSON.toJSON = $.toJSON;
    org.cometd.JSON.fromJSON = $.secureEvalJSON;

    function _setHeaders(xhr, headers)
    {
        if (headers)
        {
            for (var headerName in headers)
            {
                if (headerName.toLowerCase() === 'content-type')
                {
                    continue;
                }
                xhr.setRequestHeader(headerName, headers[headerName]);
            }
        }
    }

    // The default cometd instance
    $.cometd = new org.cometd.Cometd();

    // Remap toolkit-specific transport calls
    $.cometd.LongPollingTransport = function()
    {
        var _super = new org.cometd.LongPollingTransport();
        var that = org.cometd.Transport.derive(_super);

        that.xhrSend = function(packet)
        {
            return $.ajax({
                url: packet.url,
                async: packet.sync !== true,
                type: 'POST',
                contentType: 'application/json;charset=UTF-8',
                data: packet.body,
                beforeSend: function(xhr)
                {
                    _setHeaders(xhr, packet.headers);
                    // Returning false will abort the XHR send
                    return true;
                },
                success: packet.onSuccess,
                error: function(xhr, reason, exception)
                {
                    packet.onError(reason, exception);
                }
            });
        };

        return that;
    };

    $.cometd.CallbackPollingTransport = function()
    {
        var _super = new org.cometd.CallbackPollingTransport();
        var that = org.cometd.Transport.derive(_super);

        that.jsonpSend = function(packet)
        {
            $.ajax({
                url: packet.url,
                async: packet.sync !== true,
                type: 'GET',
                dataType: 'jsonp',
                jsonp: 'jsonp',
                data: {
                    // In callback-polling, the content must be sent via the 'message' parameter
                    message: packet.body
                },
                beforeSend: function(xhr)
                {
                    _setHeaders(xhr, packet.headers);
                    // Returning false will abort the XHR send
                    return true;
                },
                success: packet.onSuccess,
                error: function(xhr, reason, exception)
                {
                    packet.onError(reason, exception);
                }
            });
        };

        return that;
    };

    if (window.WebSocket)
    {
        $.cometd.registerTransport('websocket', new org.cometd.WebSocketTransport());
    }
    $.cometd.registerTransport('long-polling', new $.cometd.LongPollingTransport());
    $.cometd.registerTransport('callback-polling', new $.cometd.CallbackPollingTransport());

})(jQuery);
/*jquery.cometd-reload.js*/
/**
 * Dual licensed under the Apache License 2.0 and the MIT license.
 * $Revision: 686 $ $Date: 2009-07-03 19:07:24 +1000 (Fri, 03 Jul 2009) $
 */
(function($)
{
    // Remap cometd COOKIE functions to jquery cookie functions
    // Avoid to set to undefined if the jquery cookie plugin is not present
    if ($.cookie)
    {
        org.cometd.COOKIE.set = $.cookie;
        org.cometd.COOKIE.get = $.cookie;
    }

    $.cometd.registerExtension('reload', new org.cometd.ReloadExtension());
})(jQuery);
