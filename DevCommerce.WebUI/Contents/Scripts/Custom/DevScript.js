(function ($) {
    $.fn.devScript = function () {
        return $.devScript;
    };
    $.devScript = function () {

    };
    $.devScript.Initialize = function () {
        this.get("/Product/GetBasketProductCount/", "", function (response) {
            $(".numberCircle").html(response);
        }, "html", false, true, false);
    };

    $.devScript.get = function (requestUrl, headers, callBackFunction, dataType, cache, async, showOverlay) {
        if (showOverlay) {
            if ($(".loading-overlay").is(':visible')) {
                $(".loading-overlay").show();
            }
        }
        $.ajax({
            type: "GET",
            beforeSend: function (XMLHttpRequest) {
                XMLHttpRequest.setRequestHeader('Content-type', 'application/x-www-form-urlencoded; charset=utf-8');
            },
            headers: headers,
            dataType: (dataType == null || dataType == undefined ? "JSON" : dataType),
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            url: requestUrl,
            cache: (cache == null || cache == undefined ? false : cache),
            async: (async == null || async == undefined ? false : async),
            success: function (response) {
                if ($(".loading-overlay").not(':visible')) {
                    $(".loading-overlay").hide();
                }
                if (callBackFunction != null && callBackFunction != undefined)
                    callBackFunction(response);
            },
            error: function (e) {
                if ($(".loading-overlay").not(':visible')) {
                    $(".loading-overlay").hide();
                }
                //Dialog açılacak
            }
        });
    };

    $.devScript.post = function (requestUrl, parameters, callBackFunction, dataType, cache, async, showOverlay) {
        debugger
        if (showOverlay) {
            if ($(".loading-overlay").is(':visible')) {
                $(".loading-overlay").show();
            }
        }
        $.ajax({
            type: "POST",
            beforeSend: function (XMLHttpRequest) {
                XMLHttpRequest.setRequestHeader('Content-type', 'application/x-www-form-urlencoded; charset=utf-8');
            },
            dataType: (dataType == null || dataType == undefined ? "JSON" : dataType),
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            url: requestUrl,
            data: parameters,
            cache: (cache == null || cache == undefined ? false : cache),
            async: (async == null || async == undefined ? false : async),
            success: function (response) {
                if ($(".loading-overlay").not(':visible')) {
                    $(".loading-overlay").hide();
                }
                if (callBackFunction != null && callBackFunction != undefined)
                    callBackFunction(response);
            },
            error: function (e) {
                if ($(".loading-overlay").not(':visible')) {
                    $(".loading-overlay").hide();
                }
                //Dialog açılacak
            }
        });
    };

    $.devScript.Initialize();
}(jQuery));

//Header
//Result State
//Open Modal Bootstrap
//Close Modal Bootstrap
