(function ($) {
    'use strict';
    window.SF= window.SF|| {};
    SF.controls = SF.controls || {};

    SF.controls.bootstrapButton = (function () {

        var exports = {
            showLoading: function (btn) {

                if (typeof (Page_ClientValidate) == 'function') {

                    if (Page_IsValid) {
                        // make sure page really is valid
                        Page_ClientValidate();
                    }
                }

                var $btn = $(btn);

                if (Page_IsValid) {
                    setTimeout(function () {
                        $btn.prop('disabled', true);
                        $btn.attr('disabled', 'disabled');
                        $btn.addClass('disabled');
                        $btn.html($btn.attr('data-loading-text'));
                    }, 0)
                }

                return true;
            }
        };

        return exports;
    }());
}(jQuery));