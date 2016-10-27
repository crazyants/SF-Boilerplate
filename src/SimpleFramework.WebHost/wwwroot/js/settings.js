(function () {
    'use strict';
    window.SF= window.SF|| {};
    SF.settings = (function () {
        var _settings = {},
            exports = {
                initialize: function (options) {
                    if (typeof options === 'object') {
                        _settings = options;
                    }
                },
                get: function (key) {
                    return _settings[key];
                },
                insert: function (key, value) {
                    _settings[key] = value;
                },
                remove: function (key) {
                    if (_settings[key]) {
                        delete _settings[key];
                    }
                }
            };
 
        return exports;
    }());
}());