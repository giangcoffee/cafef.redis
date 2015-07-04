typeof dojo!="undefined"&&dojo.provide("org.cometd.AckExtension");
org.cometd.AckExtension=function(){var c,e=false,d=-1;this.registered=function(a,b){c=b;c._debug("AckExtension: executing registration callback",void 0)};this.unregistered=function(){c._debug("AckExtension: executing unregistration callback",void 0);c=null};this.incoming=function(a){var b=a.channel;if(b=="/meta/handshake"){e=a.ext&&a.ext.ack;c._debug("AckExtension: server supports acks",e)}else if(e&&b=="/meta/connect"&&a.successful)if((b=a.ext)&&typeof b.ack==="number"){d=b.ack;c._debug("AckExtension: server sent ack id",
d)}return a};this.outgoing=function(a){var b=a.channel;if(b=="/meta/handshake"){if(!a.ext)a.ext={};a.ext.ack=c&&c.ackEnabled!==false;d=-1}else if(e&&b=="/meta/connect"){if(!a.ext)a.ext={};a.ext.ack=d;c._debug("AckExtension: client sending ack id",d)}return a}};