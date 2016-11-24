(function ($) {
    'use strict';
    window.SF= window.SF|| {};
    SF.controls = SF.controls || {};

    SF.controls.util = (function () {
        var exports = {
            googleMapsIsLoaded: function () {
                // trigger googleMapsIsLoaded so that controls (like the geoPicker) know about it
                $(window).trigger('googleMapsIsLoaded');
            },
            loadGoogleMapsApi: function (apiSrc) {
                // ensure that js for googleMapsApi is added to page
                if (!$('#googleMapsApi').length) {
                    // by default, jquery adds a cache-busting parameter on dynamically added script tags. set the ajaxSetup cache:true to prevent this
                    $.ajaxSetup({ cache: true });
                    var src = apiSrc + '&callback=SF.controls.util.googleMapsIsLoaded';
                    $('head').prepend("<script id='googleMapsApi' src='" + src + "' />");
                } else if (typeof google == 'object' && typeof google.maps == 'object') {
                    this.googleMapsIsLoaded();
                }
            }
        };

        return exports;

    }());
}(jQuery));
