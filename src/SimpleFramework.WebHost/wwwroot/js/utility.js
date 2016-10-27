(function () {
    'use strict';
    window.SF = window.SF || {};

    SF.utility = (function () {
        var _utility = {},
            exports = {

                setContext: function (restController, entityId) {
                    // Get the current block instance object
                    $.ajax({
                        type: 'PUT',
                        url: SF.settings.get('baseUrl') + 'api/' + restController + '/SetContext/' + entityId,
                        success: function (getData, status, xhr) {
                        },
                        error: function (xhr, status, error) {
                            alert(status + ' [' + error + ']: ' + xhr.responseText);
                        }
                    });
                },
                loading: function (bool, text) {
                    var $loadingpage = top.$("#updateProgress");
                    // var $loadingtext = $loadingpage.find('.loading-content');
                    if (bool) {
                        $loadingpage.show();
                    } else {
                        // if ($loadingtext.attr('istableloading') == undefined) {
                        $loadingpage.hide();
                        // }
                    }
                    if (!!text) {
                        // $loadingtext.html(text);
                    } else {
                        // $loadingtext.html("数据加载中，请稍后…");
                    }
                    //  $loadingtext.css("left", (top.$('body').width() - $loadingtext.width()) / 2 - 50);
                    // $loadingtext.css("top", (top.$('body').height() - $loadingtext.height()) / 2);
                }
            };

        return exports;
    }());
}());