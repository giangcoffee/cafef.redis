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
