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
                            SF.dialogs.alert(status + ' [' + error + ']: ' + xhr.responseText);
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
                        top.$(".ajax-loader").remove();
                        // }
                    }
                    if (!!text) {
                        // $loadingtext.html(text);
                    } else {
                        // $loadingtext.html("数据加载中，请稍后…");
                    }
                    //  $loadingtext.css("left", (top.$('body').width() - $loadingtext.width()) / 2 - 50);
                    // $loadingtext.css("top", (top.$('body').height() - $loadingtext.height()) / 2);
                },
                //安全退出
                outLogin: function () {
                    SF.dialogs.confirm("注：您确定要安全退出本次登录吗？", function (r) {
                        if (r) {
                            SF.utility.loading(true, "正在安全退出...");
                            window.setTimeout(function () {
                                $.ajax({
                                    url: "/Account/LogOff",
                                    type: "post",
                                    dataType: "json",
                                    success: function (data) {
                                        window.location.href = "/Login";
                                    }
                                });
                            }, 500);
                        }
                    });
                },
                saveForm: function (options) {
                    var defaults = {
                        url: "",
                        param: [],
                        type: "post",
                        dataType: "json",
                        loading: "正在处理数据...",
                        success: null,
                        close: true
                    };
                    var options = $.extend(defaults, options);
                    SF.utility.loading(true, options.loading);
                    if ($('[name=__RequestVerificationToken]').length > 0) {
                        options.param["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
                    }
                    window.setTimeout(function () {
                        $.ajax({
                            url: options.url,
                            data: options.param,
                            type: options.type,
                            dataType: options.dataType,
                            success: function (data) {
                                if (data.state != "success") {
                                    SF.dialogs.alert(data.message);
                                } else {
                                    SF.utility.loading(false);
                                    SF.dialogs.alert(data.message);
                                    options.success(data);
                                    if (options.close == true) {
                                        // dialogClose();
                                    }
                                }
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                SF.utility.loading(false);
                                SF.dialogs.alert(errorThrown);
                            },
                            beforeSend: function () {
                                SF.utility.loading(true, options.loading);
                            },
                            complete: function () {
                                SF.utility.loading(false);
                            }
                        });
                    }, 500);
                },
                setForm: function (options) {
                    var defaults = {
                        url: "",
                        param: [],
                        type: "get",
                        dataType: "json",
                        success: null,
                        async: false
                    };
                    var options = $.extend(defaults, options);
                    $.ajax({
                        url: options.url,
                        data: options.param,
                        type: options.type,
                        dataType: options.dataType,
                        async: options.async,
                        success: function (data) {
                            if (data != null && data.state != "success") {
                                SF.dialogs.alert(data.message, -1);
                            } else {
                                options.success(data);
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            SF.dialogs.alert(errorThrown, -1);
                        }, beforeSend: function () {
                            SF.utility.loading(true);
                        },
                        complete: function () {
                            SF.utility.loading(false);
                        }
                    });
                },
                removeForm: function (options) {
                    var defaults = {
                        msg: "注：您确定要删除吗？该操作将无法恢复",
                        loading: "正在删除数据...",
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
                                                if (data.state != "success") {
                                                    SF.dialogs.alert(data.message);
                                                } else {
                                                    SF.dialogs.alert(data.message);
                                                    options.success(data);
                                                }
                                            },
                                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                                SF.utility.loading(false);
                                                SF.dialogs.alert(errorThrown);
                                            },
                                            beforeSend: function () {
                                                SF.utility.loading(true, options.loading);
                                            },
                                            complete: function () {
                                                SF.utility.loading(false);
                                            }
                                        });
                                    }, 500);
                                }
                            },
                            cancel: {
                                label: 'Cancel',
                                className: 'btn-secondary',
                            }
                        }
                    });
                },
                existField: function (controlId, url, param) {
                    var $control = $("#" + controlId);
                    if (!$control.val()) {
                        return false;
                    }
                    var data = {
                        keyValue: request('keyValue')
                    };
                    data[controlId] = $control.val();
                    var options = $.extend(data, param);
                    $.ajax({
                        url: url,
                        data: options,
                        type: "get",
                        dataType: "text",
                        async: false,
                        success: function (data) {
                            if (data.toLocaleLowerCase() == 'false') {
                                ValidationMessage($control, '已存在,请重新输入');
                                $control.attr('fieldexist', 'yes');
                            } else {
                                $control.attr('fieldexist', 'no');
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            SF.dialogs.alert(errorThrown, -1);
                        }
                    });
                },
                newGuid: function () {
                    var guid = "";
                    for (var i = 1; i <= 32; i++) {
                        var n = Math.floor(Math.random() * 16.0).toString(16);
                        guid += n;
                        if ((i == 8) || (i == 12) || (i == 16) || (i == 20)) guid += "-";
                    }
                    return guid;
                },
                formatDate: function (v, format) {
                    if (!v) return "";
                    var d = v;
                    if (typeof v === 'string') {
                        if (v.indexOf("/Date(") > -1)
                            d = new Date(parseInt(v.replace("/Date(", "").replace(")/", ""), 10));
                        else
                            d = new Date(Date.parse(v.replace(/-/g, "/").replace("T", " ").split(".")[0]));//.split(".")[0] 用来处理出现毫秒的情况，截取掉.xxx，否则会出错
                    }
                    var o = {
                        "M+": d.getMonth() + 1,  //month
                        "d+": d.getDate(),       //day
                        "h+": d.getHours(),      //hour
                        "m+": d.getMinutes(),    //minute
                        "s+": d.getSeconds(),    //second
                        "q+": Math.floor((d.getMonth() + 3) / 3),  //quarter
                        "S": d.getMilliseconds() //millisecond
                    };
                    if (/(y+)/.test(format)) {
                        format = format.replace(RegExp.$1, (d.getFullYear() + "").substr(4 - RegExp.$1.length));
                    }
                    for (var k in o) {
                        if (new RegExp("(" + k + ")").test(format)) {
                            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
                        }
                    }
                    return format;
                },
                toDecimal: function (num) {
                    if (num == null) {
                        num = "0";
                    }
                    num = num.toString().replace(/\$|\,/g, '');
                    if (isNaN(num))
                        num = "0";
                    sign = (num == (num = Math.abs(num)));
                    num = Math.floor(num * 100 + 0.50000000001);
                    cents = num % 100;
                    num = Math.floor(num / 100).toString();
                    if (cents < 10)
                        cents = "0" + cents;
                    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3) ; i++)
                        num = num.substring(0, num.length - (4 * i + 3)) + '' +
                                num.substring(num.length - (4 * i + 3));
                    return (((sign) ? '' : '-') + num + '.' + cents);
                },
                request: function (name) {
                    var search = location.search.slice(1);
                    var arr = search.split("&");
                    for (var i = 0; i < arr.length; i++) {
                        var ar = arr[i].split("=");
                        if (ar[0] == name) {
                            if (unescape(ar[1]) == 'undefined') {
                                return "";
                            } else {
                                return unescape(ar[1]);
                            }
                        }
                    }
                    return "";
                },
                browser: function () {
                    var userAgent = navigator.userAgent;
                    var isOpera = userAgent.indexOf("Opera") > -1;
                    if (isOpera) {
                        return "Opera"
                    };
                    if (userAgent.indexOf("Firefox") > -1) {
                        return "FF";
                    }
                    if (userAgent.indexOf("Chrome") > -1) {
                        if (window.navigator.webkitPersistentStorage.toString().indexOf('DeprecatedStorageQuota') > -1) {
                            return "Chrome";
                        } else {
                            return "360";
                        }
                    }
                    if (userAgent.indexOf("Safari") > -1) {
                        return "Safari";
                    }
                    if (userAgent.indexOf("compatible") > -1 && userAgent.indexOf("MSIE") > -1 && !isOpera) {
                        return "IE";
                    };
                },
                reload: function () {
                    location.reload();
                    return false;
                },
                dialogOpen: function (options) {
                    SF.utility.loading(true);
                    var defaults = {
                        id: null,
                        title: '系统窗口',
                        width: "100px",
                        height: "100px",
                        url: '',
                        shade: 0.3,
                        btn: ['确认', '关闭'],
                        callBack: null
                    };
                    var options = $.extend(defaults, options);
                    var _url = options.url;
                    var _width = top.SF.utility.windowWidth() > parseInt(options.width.replace('px', '')) ? options.width : top.SF.utility.windowWidth() + 'px';
                    var _height = top.SF.utility.windowHeight() > parseInt(options.height.replace('px', '')) ? options.height : top.SF.utility.windowHeight() + 'px';
                    //var _width = top.$.windowWidth() > parseInt(options.width.replace('px', '')) ? options.width : top.$.windowWidth() + 'px';
                    //var _height = top.$.windowHeight() > parseInt(options.height.replace('px', '')) ? options.height : top.$.windowHeight() + 'px';
                    top.layer.open({
                        id: options.id,
                        type: 2,//此处以iframe
                        shade: options.shade,
                        title: options.title,
                        fix: false,
                        zIndex: top.layer.zIndex, //重点1
                        area: [_width, _height],
                        content: _url,
                        btn: options.btn,
                        yes: function () {
                            options.callBack(options.id)
                        }, cancel: function () {
                            return true;
                        }, success: function (layero) {
                            top.layer.setTop(layero); //重点2
                        }
                    });
                },
                dialogContent: function (options) {
                    var defaults = {
                        id: null,
                        title: '系统窗口',
                        width: "100px",
                        height: "100px",
                        content: '',
                        btn: ['确认', '关闭'],
                        callBack: null
                    };
                    var options = $.extend(defaults, options);
                    top.layer.open({
                        id: options.id,
                        type: 1,
                        title: options.title,
                        fix: false,
                        zIndex: top.layer.zIndex, //重点1
                        area: [options.width, options.height],
                        content: options.content,
                        btn: options.btn,
                        yes: function () {
                            options.callBack(options.id)
                        }
                    });
                },
                dialogAlert: function (content, type) {
                    if (type == -1) {
                        type = 2;
                    }
                    top.layer.alert(content, {
                        icon: type,
                        title: "提示"
                    });
                },
                dialogConfirm: function (content, callBack) {
                    top.layer.confirm(content, {
                        icon: 7,
                        title: "提示",
                        btn: ['确认', '取消'],
                    }, function () {
                        callBack(true);
                    }, function () {
                        callBack(false)
                    });
                },
                dialogMsg: function (content, type) {
                    if (type == -1) {
                        type = 2;
                    }
                    top.layer.msg(content, { icon: type, time: 4000, shift: 5 });
                },
                dialogClose: function () {
                    try {
                        var index = top.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
                        var $IsdialogClose = top.$("#layui-layer" + index).find('.layui-layer-btn').find("#IsdialogClose");
                        var IsClose = $IsdialogClose.is(":checked");
                        if ($IsdialogClose.length == 0) {
                            IsClose = true;
                        }
                        if (IsClose) {
                            top.layer.close(index);
                        } else {
                            location.reload();
                        }
                    } catch (e) {
                        alert(e)
                    }
                },
                windowWidth: function () {
                    return $(window).width();
                },
                windowHeight: function () {
                    return $(window).height();
                },
                request: function (keyValue) {
                    var search = location.search.slice(1);
                    var arr = search.split("&");
                    for (var i = 0; i < arr.length; i++) {
                        var ar = arr[i].split("=");
                        if (ar[0] == keyValue) {
                            if (unescape(ar[1]) == 'undefined') {
                                return "";
                            } else {
                                return unescape(ar[1]);
                            }
                        }
                    }
                    return "";
                },
                getWebControls: function (formId, keyValue) {
                    var reVal = "";
                    var $id = $('#' + formId)
                   $id.find('input,select,textarea,.ui-select').each(function (r) {
                        var id = $(this).attr('id');
                        var type = $(this).attr('type');
                        switch (type) {
                            case "checkbox":
                                if ($("#" + id).is(":checked")) {
                                    reVal += '"' + id + '"' + ':' + '"1",'
                                } else {
                                    reVal += '"' + id + '"' + ':' + '"0",'
                                }
                                break;
                            case "select":
                                var value = $("#" + id).attr('data-value');
                                if (value == "") {
                                    value = "&nbsp;";
                                }
                                reVal += '"' + id + '"' + ':' + '"' + $.trim(value) + '",'
                                break;
                            case "selectTree":
                                var value = $("#" + id).attr('data-value');
                                if (value == "") {
                                    value = "&nbsp;";
                                }
                                reVal += '"' + id + '"' + ':' + '"' + $.trim(value) + '",'
                                break;
                            default:
                                var value = $("#" + id).val();
                                if (value == "") {
                                    value = "&nbsp;";
                                }
                                reVal += '"' + id + '"' + ':' + '"' + $.trim(value) + '",'
                                break;
                        }
                    });
                    reVal = reVal.substr(0, reVal.length - 1);
                    if (!keyValue) {
                        reVal = reVal.replace(/&nbsp;/g, '');
                    }
                    reVal = reVal.replace(/\\/g, '\\\\');
                    reVal = reVal.replace(/\n/g, '\\n');
                    var postdata = jQuery.parseJSON('{' + reVal + '}');
                    //阻止伪造请求
                    //if ($('[name=__RequestVerificationToken]').length > 0) {
                    //    postdata["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
                    //}
                    return postdata;
                },
                setWebControls: function (formId,data) {
                    var $id = $('#'+formId)
                    for (var key in data) {
                        var id = $id.find('#' + key);
                        if (id.attr('id')) {
                            var type = id.attr('type');
                            if (id.hasClass("input-datepicker")) {
                                type = "datepicker";
                            }
                            var value = $.trim(data[key]).replace(/&nbsp;/g, '');
                            switch (type) {
                                case "checkbox":
                                    if (value == 1) {
                                        id.attr("checked", 'checked');
                                    } else {
                                        id.removeAttr("checked");
                                    }
                                    break;
                                case "select":
                                 //   id.ComboBoxSetValue(value);
                                    break;
                                case "selectTree":
                                  //  id.ComboBoxTreeSetValue(value);
                                    break;
                                case "datepicker":
                                    id.val(SF.utility.formatDate(value, 'yyyy-MM-dd'));
                                    break;
                                default:
                                    id.val(value);
                                    break;
                            }
                        }
                    }
                }
            };

        return exports;
    }());
}());
