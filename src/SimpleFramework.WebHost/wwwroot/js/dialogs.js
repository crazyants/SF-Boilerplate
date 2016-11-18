(function ($) {
    window.SF = window.SF || {};

    SF.dialogs = (function () {
        var _dialogs = {},
            exports = {
                // Presents a bootstrap style alert box with the specified message 
                // then executes the callback function(result)
                alert: function (msg) {
                    bootbox.dialog({
                        message: msg,
                        buttons: {
                            ok: {
                                label: 'OK',
                                className: 'btn-primary'
                            }
                        }
                    });
                },

                // Presents a bootstrap style alert box with the specified message 
                // then executes the callback function(result)
                confirm: function (msg, callback) {
                    bootbox.dialog({
                        message: msg,
                        buttons: {
                            ok: {
                                label: 'OK',
                                className: 'btn-primary',
                                callback: function () {
                                    callback(true);
                                }
                            },
                            cancel: {
                                label: 'Cancel',
                                className: 'btn-secondary',
                                callback: function () {
                                    callback(false);
                                }
                            }
                        }
                    });
                },

                // Presents a bootstrap style alert box with a 'Are you sure you want to delete this ...' message 
                // Returns true if the user selects OK
                confirmDelete: function (e, nameText, additionalMsg) {
                    // make sure the element that triggered this event isn't disabled
                    if (e.currentTarget && e.currentTarget.disabled) {
                        return false;
                    }

                    e.preventDefault();
                    var msg = 'Are you sure you want to delete this ' + nameText + '?';
                    if (additionalMsg) {
                        msg += ' ' + additionalMsg;
                    }

                    bootbox.dialog({
                        message: msg,
                        buttons: {
                            ok: {
                                label: 'OK',
                                className: 'btn-primary',
                                callback: function () {
                                    var postbackJs = e.target.href ? e.target.href : e.target.parentElement.href;
                                    window.location = postbackJs;
                                }
                            },
                            cancel: {
                                label: 'Cancel',
                                className: 'btn-secondary'
                            }
                        }
                    });
                },

                confirmAjax: function (options) {
                    var defaults = {
                        msg: "提示信息",
                        loading: "正在处理数据...",
                        url: "",
                        param: [],
                        type: "post",
                        dataType: "json",
                        success: null
                    };
                    var options = $.extend(defaults, options);
                    bootbox.dialog({
                        message: options.msg,
                        buttons: {
                            ok: {
                                label: 'OK',
                                className: 'btn-primary',
                                callback: function () {
                                    SF.utility.loading(true, options.loading);
                                    window.setTimeout(function () {
                                        var postdata = options.param;
                                        if ($('[name=__RequestVerificationToken]').length > 0) {
                                            postdata["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
                                        }
                                        $.ajax({
                                            url: options.url,
                                            data: postdata,
                                            type: options.type,
                                            dataType: options.dataType,
                                            success: function (data) {
                                                SF.utility.loading(false);
                                                if (data.state != "success") {
                                                    dialogs.alert(data.message);
                                                } else {
                                                    dialogs.alert(data.message);
                                                    options.success(data);
                                                }
                                            },
                                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                                SF.utility.loading(false);
                                                dialogs.alert(errorThrown);
                                            },
                                            beforeSend: function () {
                                                SF.utility.loading(true, options.loading);
                                            },
                                            complete: function () {
                                                SF.utility.loading(false);
                                            }
                                        });
                                    }, 200);
                                }
                            },
                            cancel: {
                                label: 'Cancel',
                                className: 'btn-secondary',
                            }
                        }
                    });

                },

                // Updates the modal so that scrolling works
                updateModalScrollBar: function (controlId) {
                    SF.controls.modal.updateSize(controlId);
                }
            }

        return exports;
    }());
}(jQuery));