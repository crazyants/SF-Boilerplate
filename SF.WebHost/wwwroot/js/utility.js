(function () {
    'use strict';
    window.SF = window.SF || {};

    SF.utility = (function () {
        var _utility = {},
            exports = {
               confirmAjax :function (options) {
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
                    SF.utility.dialogConfirm(options.msg, function (r) {
                        if (r) {
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
                                        if (data.type == "3") {
                                            SF.utility.dialogAlert(data.message);
                                        } else {
                                            SF.utility.dialogMsg(data.message, 1);
                                            options.success(data);
                                        }
                                    },
                                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                                        SF.utility.loading(false);
                                        SF.utility.dialogMsg(errorThrown, -1);
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
                    });
                },
                setContext: function (restController, entityId) {
                    // Get the current block instance object
                    $.ajax({
                        type: 'PUT',
                        url: SF.settings.get('baseUrl') + 'api/' + restController + '/SetContext/' + entityId,
                        success: function (getData, status, xhr) {
                        },
                        error: function (xhr, status, error) {
                            SF.utility.dialogAlert(status + ' [' + error + ']: ' + xhr.responseText);

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
                isNullOrEmpty: function (obj) {
                    if ((typeof (obj) == "string" && obj == "") || obj == null || obj == undefined) {
                        return true;
                    }
                    else {
                        return false;
                    }
                },
                //安全退出
                outLogin: function () {
                    SF.utility.dialogConfirm("注：您确定要安全退出本次登录吗？", function (r) {
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
                        //  contentType: "application/json",
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
                            //  contentType: options.contentType,
                            success: function (data) {
                                if (data.state != "success") {
                                    SF.utility.dialogAlert(data.message);
                                } else {
                                    options.success(data);
                                    if (options.close == true) {
                                        SF.utility.dialogClose();
                                    }
                                }
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                SF.utility.dialogAlert(XMLHttpRequest.responseText, -1);
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
                            if (data != null && data.state != undefined && data.state != "success") {
                                SF.utility.dialogAlert(data.message);
                            } else {
                                options.success(data);
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            SF.utility.dialogAlert(XMLHttpRequest.responseText, -1);
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
                    SF.utility.dialogConfirm(options.msg, function (r) {
                        if (r) {
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
                                            SF.utility.dialogAlert(data.message);
                                        } else {
                                            SF.utility.dialogMsg(data.message, 1);
                                            options.success(data);
                                        }
                                    },
                                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                                        SF.utility.dialogAlert(XMLHttpRequest.responseText, -1);
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
                    });
                },
                existField: function (controlId, url, param) {
                    var $control = $("#" + controlId);
                    if (!$control.val()) {
                        return false;
                    }
                    var data = {
                        keyValue: SF.utility.request('keyValue')
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
                            if (data.toLocaleLowerCase() == 'true') {
                                ValidationMessage($control, '已存在,请重新输入');
                                $control.attr('fieldexist', 'yes');
                            } else {
                                $control.attr('fieldexist', 'no');
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            SF.utility.dialogAlert(XMLHttpRequest.responseText, -1);
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
                changeUrlParam: function (url, key, value) {
                    var newUrl = "";
                    var reg = new RegExp("(^|)" + key + "=([^&]*)(|$)");
                    var tmp = key + "=" + value;
                    if (url.match(reg) != null) {
                        newUrl = url.replace(eval(reg), tmp);
                    } else {
                        if (url.match("[\?]")) {
                            newUrl = url + "&" + tmp;
                        }
                        else {
                            newUrl = url + "?" + tmp;
                        }
                    }
                    return newUrl;
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
                parentIframeId: function () {
                    return "iframepage";

                },
                currentIframe: function () {
                    if (SF.utility.isbrowsername() == "Chrome" || SF.utility.isbrowsername() == "FF") {
                        return top.frames[SF.utility.parentIframeId()];
                    }
                    else {
                        return top.frames[SF.utility.parentIframeId()];
                    }
                },
                isbrowsername: function () {
                    var userAgent = navigator.userAgent; //取得浏览器的userAgent字符串
                    var isOpera = userAgent.indexOf("Opera") > -1;
                    if (isOpera) {
                        return "Opera"
                    }; //判断是否Opera浏览器
                    if (userAgent.indexOf("Firefox") > -1) {
                        return "FF";
                    } //判断是否Firefox浏览器
                    if (userAgent.indexOf("Chrome") > -1) {
                        if (window.navigator.webkitPersistentStorage.toString().indexOf('DeprecatedStorageQuota') > -1) {
                            return "Chrome";
                        } else {
                            return "360";
                        }
                    }//判断是否Chrome浏览器//360浏览器
                    if (userAgent.indexOf("Safari") > -1) {
                        return "Safari";
                    } //判断是否Safari浏览器
                    if (userAgent.indexOf("compatible") > -1 && userAgent.indexOf("MSIE") > -1 && !isOpera) {
                        return "IE";
                    }; //判断是否IE浏览器
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
                        title: "提示",
                        zIndex: top.layer.zIndex, //重点1
                    });
                },
                dialogConfirm: function (content, callBack) {
                    top.layer.confirm(content, {
                        icon: 7,
                        title: "提示",
                        zIndex: top.layer.zIndex, //重点1
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
                    top.layer.msg(content,
                        {
                            icon: type,
                            time: 4000,
                            shift: 5,
                            zIndex: top.layer.zIndex, //重点1
                        }
                        );
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
                setWebControls: function (formId, data) {
                    var $id = $('#' + formId)
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
                                    if (value == 1 ) {
                                        id.attr("checked", 'checked');
                                    } else {
                                        id.removeAttr("checked");
                                    }
                                    break;
                                case "select":
                                    SF.utility.comboBoxSetValue(id, value);
                                    break;
                                case "selectTree":
                                    SF.utility.comboBoxTreeSetValue(id, value);
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
                },
                checkedRow: function (id) {
                    var isOK = true;
                    if (id == undefined || id == "" || id == 'null' || id == 'undefined') {
                        isOK = false;
                        SF.utility.dialogMsg('您没有选中任何数据项,请选中后再操作！', 0);
                    } else if (id.split(",").length > 1) {
                        isOK = false;
                        SF.utility.dialogMsg('很抱歉,一次只能选择一条记录！', 0);
                    }
                    return isOK;
                },
                itemPickerTreeSetValue: function (element, value) {
                    if (value == "") {
                        return;
                    }
                    var $control = $(element),
                         $hfItemIds = $control.find('.js-item-id-value'),
                         $hfItemNames = $control.find('.js-item-name-value'),
                         $treeView = $control.find('.treeview'),
                         $spanNames = $control.find('.selected-names');


                    var data_text = $treeView.find('ul').find('[data-id=' + value + ']').find('span').attr("title");
                    if (data_text) {
                        $hfItemNames.val(data_text);
                        $spanNames.text(data_text);
                    }

                },
                itemPickerTreeSetValues: function (element, values) {
                    if (values == "") {
                        return;
                    }
                    var $control = $(element),
                    $hfItemIds = $control.find('.js-item-id-value'),
                    $hfItemNames = $control.find('.js-item-name-value'),
                    $treeView = $control.find('.treeview'),
                    $spanNames = $control.find('.selected-names');

                    $hfItemIds.val(values.join(','));
                    var $ul = $treeView.find('ul');
                    var selectedNames = [];
                    $.each(values, function (index, value) {
                        var data_text = $ul.find('[data-id=' + value + ']').find('span').attr("title")
                        selectedNames.push(data_text);
                    });

                    if (selectedNames.length > 0) {
                        $spanNames.text(selectedNames.join(', '));
                    }

                },
                comboBox: function (element, options) {
                    //options参数：description,height,width,allowSearch,url,param,data
                    var $select = $(element);

                    if (!$select.attr('id')) {
                        return false;
                    }
                    if (options) {
                        if ($select.find('.ui-select-text').length == 0) {
                            var $select_html = "";
                            $select_html += "<div class=\"ui-select-text\" style='color:#999;'>" + options.description + "</div>";
                            $select_html += "<div class=\"ui-select-option\">";
                            $select_html += "<div class=\"ui-select-option-content\" style=\"max-height: " + options.height + "\">" + $select.html() + "</div>";
                            if (options.allowSearch) {
                                $select_html += "<div class=\"ui-select-option-search\"><input type=\"text\" class=\"form-control\" placeholder=\"搜索关键字\" /><span class=\"input-query\" title=\"Search\"><i class=\"fa fa-search\"></i></span></div>";
                            }
                            $select_html += "</div>";
                            $select.html('');
                            $select.append($select_html);
                        }
                    }
                    var $option_html = $($("<p>").append($select.find('.ui-select-option').clone()).html());
                    $option_html.attr('id', $select.attr('id') + '-option');
                    $select.find('.ui-select-option').remove();
                    if ($option_html.length > 0) {
                        $('body').find('#' + $select.attr('id') + '-option').remove();
                    }
                    $('body').prepend($option_html);
                    var $option = $("#" + $select.attr('id') + "-option");
                    if (options.url != undefined) {
                        $option.find('.ui-select-option-content').html('');
                        $.ajax({
                            url: options.url,
                            data: options.param,
                            type: "GET",
                            dataType: "json",
                            async: false,
                            success: function (data) {
                                options.data = data;
                                var json = data;
                                loadComboBoxView(json);
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                SF.utility.dialogMsg(errorThrown, -1);
                            }
                        });
                    }
                    else if (options.data != undefined) {
                        var json = options.data;
                        loadComboBoxView(json);
                    }
                    else {
                        $option.find('li').css('padding', "0 5px");
                        $option.find('li').click(function (e) {
                            var data_text = $(this).text();
                            var data_value = $(this).attr('data-value');
                            $select.attr("data-value", data_value).attr("data-text", data_text);
                            $select.find('.ui-select-text').html(data_text).css('color', '#000');
                            $option.slideUp(150);
                            $select.trigger("change");
                            e.stopPropagation();
                        }).hover(function (e) {
                            if (!$(this).hasClass('liactive')) {
                                $(this).toggleClass('on');
                            }
                            e.stopPropagation();
                        });
                    }
                    function loadComboBoxView(json, searchValue, m) {
                        if (json.length > 0) {
                            var $_html = $('<ul></ul>');
                            if (options.description) {
                                $_html.append('<li data-value="">' + options.description + '</li>');
                            }
                            $.each(json, function (i) {
                                var row = json[i];
                                var title = row[options.title];
                                if (title == undefined) {
                                    title = "";
                                }
                                if (searchValue != undefined) {
                                    if (row[m.text].indexOf(searchValue) != -1) {
                                        $_html.append('<li data-value="' + row[options.id] + '" title="' + title + '">' + row[options.text] + '</li>');
                                    }
                                }
                                else {
                                    $_html.append('<li data-value="' + row[options.id] + '" title="' + title + '">' + row[options.text] + '</li>');
                                }
                            });
                            $option.find('.ui-select-option-content').html($_html);
                            $option.find('li').css('padding', "0 5px");
                            $option.find('li').click(function (e) {
                                var data_text = $(this).text();
                                var data_value = $(this).attr('data-value');
                                $select.attr("data-value", data_value).attr("data-text", data_text);
                                $select.find('.ui-select-text').html(data_text).css('color', '#000');
                                $option.slideUp(150);
                                $select.trigger("change");
                                e.stopPropagation();
                            }).hover(function (e) {
                                if (!$(this).hasClass('liactive')) {
                                    $(this).toggleClass('on');
                                }
                                e.stopPropagation();
                            });
                        }
                    }
                    //操作搜索事件
                    if (options.allowSearch) {
                        $option.find('.ui-select-option-search').find('input').bind("keypress", function (e) {
                            if (event.keyCode == "13") {
                                var value = $(this).val();
                                loadComboBoxView($(this)[0].options.data, value, $(this)[0].options);
                            }
                        }).focus(function () {
                            $(this).select();
                        })[0]["options"] = options;
                    }

                    $select.unbind('click');
                    $select.bind("click", function (e) {
                        if ($select.attr('readonly') == 'readonly' || $select.attr('disabled') == 'disabled') {
                            return false;
                        }
                        $(this).addClass('ui-select-focus');
                        if ($option.is(":hidden")) {
                            $select.find('.ui-select-option').hide();
                            $('.ui-select-option').hide();
                            var left = $select.offset().left;
                            var top = $select.offset().top + 29;
                            var width = $select.width();
                            if (options.width) {
                                width = options.width;
                            }
                            if (($option.height() + top) < $(window).height()) {
                                $option.slideDown(150).css({ top: top, left: left, width: width });
                            } else {
                                var _top = (top - $option.height() - 32)
                                $option.show().css({ top: _top, left: left, width: width });
                                $option.attr('data-show', true);
                            }
                            $option.css('border-top', '1px solid #ccc');
                            $option.find('li').removeClass('liactive');
                            $option.find('[data-value=' + $select.attr('data-value') + ']').addClass('liactive');
                            $option.find('.ui-select-option-search').find('input').select();
                        } else {
                            if ($option.attr('data-show')) {
                                $option.hide();
                            } else {
                                $option.slideUp(150);
                            }
                        }
                        e.stopPropagation();
                    });
                    $(document).click(function (e) {
                        var e = e ? e : window.event;
                        var tar = e.srcElement || e.target;
                        if (!$(tar).hasClass('form-control')) {
                            if ($option.attr('data-show')) {
                                $option.hide();
                            } else {
                                $option.slideUp(150);
                            }
                            $select.removeClass('ui-select-focus');
                            e.stopPropagation();
                        }
                    });
                    return $select;
                },
                comboBoxSetValue: function (element, value) {
                    if (SF.utility.isNullOrEmpty(value)) {
                        return;
                    }
                    var $select = $(element);
                    var $option = $("#" + $select.attr('id') + "-option");
                    $select.attr('data-value', value);
                    var data_text = $option.find('ul').find('[data-value=' + value + ']').html();
                    if (data_text) {
                        $select.attr('data-text', data_text);
                        $select.find('.ui-select-text').html(data_text).css('color', '#000');
                        $option.find('ul').find('[data-value=' + value + ']').addClass('liactive')
                    }
                    return $select;
                },
                comboBoxTree: function (element, options) {
                    //options参数：description,height,allowSearch,appendTo,click,url,param,method,icon,callback
                    var $select = $(element);
                    if (!$select.attr('id')) {
                        return false;
                    }
                    if ($select.find('.ui-select-text').length == 0) {
                        var $select_html = "";
                        $select_html += "<div class=\"ui-select-text\"  style='color:#999;'>" + options.description + "</div>";
                        $select_html += "<div class=\"ui-select-option\">";
                        $select_html += "<div class=\"ui-select-option-content\" style=\"max-height: " + options.height + "\"></div>";
                        if (options.allowSearch) {
                            $select_html += "<div class=\"ui-select-option-search\"><input type=\"text\" class=\"form-control\" placeholder=\"搜索关键字\" /><span class=\"input-query\" title=\"Search\"><i class=\"fa fa-search\" title=\"按回车查询\"></i></span></div>";
                        }
                        $select_html += "</div>";
                        $select.append($select_html);
                    }


                    var $option_html = $($("<p>").append($select.find('.ui-select-option').clone()).html());
                    $option_html.attr('id', $select.attr('id') + '-option');
                    $select.find('.ui-select-option').remove();
                    if (options.appendTo) {
                        $(options.appendTo).prepend($option_html);
                    } else {
                        $('body').prepend($option_html);
                    }
                    var $option = $("#" + $select.attr('id') + "-option");
                    var $option_content = $("#" + $select.attr('id') + "-option").find('.ui-select-option-content');
                    loadtreeview(options.url);
                    function loadtreeview(url) {
                        $option_content.treeview({
                            onnodeclick: function (item) {
                                $select.attr("data-value", item.id).attr("data-text", item.text);
                                $select.find('.ui-select-text').html(item.text).css('color', '#000');
                                $select.trigger("change");
                                if (options.click) {
                                    options.click(item);
                                }
                            },
                            callback: function (item) {
                                if (options.callback) {
                                    options.callback(item);
                                }
                            },
                            height: options.height,
                            url: url,
                            param: options.param,
                            method: options.method,
                            description: options.description
                        });
                    }
                    if (options.allowSearch) {
                        $option.find('.ui-select-option-search').find('input').attr('data-url', options.url);
                        $option.find('.ui-select-option-search').find('input').bind("keypress", function (e) {
                            if (event.keyCode == "13") {
                                var value = $(this).val();
                                var url = changeUrlParam($option.find('.ui-select-option-search').find('input').attr('data-url'), "keyword", escape(value));
                                loadtreeview(url);
                            }
                        }).focus(function () {
                            $(this).select();
                        });
                    }
                    if (options.icon) {
                        $option.find('i').remove();
                        $option.find('img').remove();
                    }
                    $select.find('.ui-select-text').unbind('click');
                    $select.find('.ui-select-text').bind("click", function (e) {
                        if ($select.attr('readonly') == 'readonly' || $select.attr('disabled') == 'disabled') {
                            return false;
                        }
                        $(this).parent().addClass('ui-select-focus');
                        if ($option.is(":hidden")) {
                            $select.find('.ui-select-option').hide();
                            $('.ui-select-option').hide();
                            var left = $select.offset().left;
                            var top = $select.offset().top + 29;
                            var width = $select.width();
                            if (options.width) {
                                width = options.width;
                            }
                            if (($option.height() + top) < $(window).height()) {
                                $option.slideDown(150).css({ top: top, left: left, width: width });
                            } else {
                                var _top = (top - $option.height() - 32);
                                $option.show().css({ top: _top, left: left, width: width });
                                $option.attr('data-show', true);
                            }
                            $option.css('border-top', '1px solid #ccc');
                            if (options.appendTo) {
                                $option.css("position", "inherit")
                            }
                            $option.find('.ui-select-option-search').find('input').select();
                        } else {
                            if ($option.attr('data-show')) {
                                $option.hide();
                            } else {
                                $option.slideUp(150);
                            }
                        }
                        e.stopPropagation();
                    });
                    $select.find('li div').click(function (e) {
                        var e = e ? e : window.event;
                        var tar = e.srcElement || e.target;
                        if (!$(tar).hasClass('bbit-tree-ec-icon')) {
                            $option.slideUp(150);
                            e.stopPropagation();
                        }
                    });
                    $(document).click(function (e) {
                        var e = e ? e : window.event;
                        var tar = e.srcElement || e.target;
                        if (!$(tar).hasClass('bbit-tree-ec-icon') && !$(tar).hasClass('form-control')) {
                            if ($option.attr('data-show')) {
                                $option.hide();
                            } else {
                                $option.slideUp(150);
                            }
                            $select.removeClass('ui-select-focus');
                            e.stopPropagation();
                        }
                    });
                    return $select;
                },
                comboBoxTreeSetValue: function (element, value) {
                    if (value == "") {
                        return;
                    }
                    var $select = $(element);
                    var $option = $("#" + $select.attr('id') + "-option");
                    $select.attr('data-value', value);
                    var data_text = $option.find('ul').find('[data-value=' + value + ']').html();
                    if (data_text) {
                        $select.attr('data-text', data_text);
                        $select.find('.ui-select-text').html(data_text).css('color', '#000');
                        $option.find('ul').find('[data-value=' + value + ']').parent().parent().addClass('bbit-tree-selected');
                    }
                    return $select;
                },
                resizeScrollbar: function (scrollControl) {
                    var overviewHeight = $(scrollControl).find('.overview').height();

                    $(scrollControl).find('.viewport').height(overviewHeight);

                    scrollControl.tinyscrollbar_update('relative');
                },
                resizeScrollbarWithWinHeight: function (scrollControl, height) {
                    $(scrollControl).find('.viewport').height(height);

                    scrollControl.tinyscrollbar_update('relative');
                },
            }

        return exports;
    }());
}());
