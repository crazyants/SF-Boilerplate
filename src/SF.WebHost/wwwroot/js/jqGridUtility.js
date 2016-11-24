
$.fn.jqGridEx = function (options) {
    var $jqGrid = $(this);
    var _selectedRowIndex;
    if (!$jqGrid.attr('id')) {
        return false;
    }
    var defaults = {
        url: "",
        datatype: "json",
        height: $(window).height() - 139.5,
        autowidth: true,
        colModel: [],
        viewrecords: true,
        rowNum: 30,
        rowList: [30, 50, 100],
        pager: "#gridPager",
        sortname: 'CreateDate desc',
        rownumbers: true,
        shrinkToFit: false,
        gridview: true,
        onSelectRow: function () {
            _selectedRowIndex = $("#" + this.id).getGridParam('selrow');
        },
        gridComplete: function () {
            $("#" + this.id).setSelection(_selectedRowIndex, false);
        }
    };
    var options = $.extend(defaults, options);
    $jqGrid.jqGrid(options);
}
$.fn.jqGridRowValue = function (code) {
    var $jgrid = $(this);
    var json = [];
    var selectedRowIds = $jgrid.jqGrid("getGridParam", "selarrrow");
    if (selectedRowIds != undefined && selectedRowIds != "") {
        var len = selectedRowIds.length;
        for (var i = 0; i < len ; i++) {
            var rowData = $jgrid.jqGrid('getRowData', selectedRowIds[i]);
            json.push(rowData[code]);
        }
    } else {
        var rowData = $jgrid.jqGrid('getRowData', $jgrid.jqGrid('getGridParam', 'selrow'));
        json.push(rowData[code]);
    }
    return String(json);
}
$.fn.jqGridRow = function () {
    var $jgrid = $(this);
    var json = [];
    var selectedRowIds = $jgrid.jqGrid("getGridParam", "selarrrow");
    if (selectedRowIds != "") {
        var len = selectedRowIds.length;
        for (var i = 0; i < len ; i++) {
            var rowData = $jgrid.jqGrid('getRowData', selectedRowIds[i]);
            json.push(rowData);
        }
    } else {
        var rowData = $jgrid.jqGrid('getRowData', $jgrid.jqGrid('getGridParam', 'selrow'));
        json.push(rowData);
    }
    return json;
}
$.fn.dataGrid = function (options) {
    var defaults = {
        datatype: "json",
        autowidth: true,
        rownumbers: true,
        shrinkToFit: false,
        gridview: true
    };
    var options = $.extend(defaults, options);
    var $element = $(this);
    options["onSelectRow"] = function (rowid) {
        var length = $(this).jqGrid("getGridParam", "selrow").length;
        var $operate = $(".operate");
        if (length > 0) {
            $operate.animate({ "left": 0 }, 200);
        } else {
            $operate.animate({ "left": '-100.1%' }, 200);
        }
        $operate.find('.close').click(function () {
            $operate.animate({ "left": '-100.1%' }, 200);
        })
    };
    $element.jqGrid(options);
};

$.fn.GetWebControls = function (keyValue) {
    var reVal = "";
    $(this).find('input,select,textarea,.ui-select').each(function (r) {
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
    //×èÖ¹Î±ÔìÇëÇó
    if ($('[name=__RequestVerificationToken]').length > 0) {
        postdata["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
    }
    return postdata;
};
$.fn.SetWebControls = function (data) {
    var $id = $(this)
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
                    //case "select":
                    //  id.ComboBoxSetValue(value);
                    //    break;
                    // case "selectTree":
                    //     id.ComboBoxTreeSetValue(value);
                    //     break;
                case "datepicker":
                    id.val(formatDate(value, 'yyyy-MM-dd'));
                    break;
                default:
                    id.val(value);
                    break;
            }
        }
    }
}

//$('body').ajaxComplete(function (e, xhr, settings) {
//    if (xhr.status == 200) {
//        var redirect = null;
//        try {
//            redirect = $.parseJSON(xhr.responseText).redirect;
//            if (redirect) {
//                window.location.href = redirect.replace(/\?.*$/, "?ReturnUrl=" + window.location.pathname);
//            }
//        }
//        catch (e) {
//            return;
//        }
//    }
//});
