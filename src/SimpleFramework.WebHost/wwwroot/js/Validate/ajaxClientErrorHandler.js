(function ($) {
    'use strict';
    $(function () {
        $.ajaxSetup({

            timeout: 3000,

            dataType: 'html',

            //请求成功后触发

            success: function (data) { show.append('success invoke!' + data + '<br/>'); },

            //请求失败遇到异常触发

            error: function (xhr, status, e) {
                if (xhr.get_error() != undefined && xhr.get_error().httpStatusCode == '500') {
                    var errorName = xhr.geonse();
                    if (response) {

                        // if we got responseData (probably from Error.aspx.cs), use that as the error output
                        var responseData = response.get_responseData();
                        errorMessage = responseData;
                    }

                    $(".ajax-error-message").html(errorMessage);
                    $(".ajax-error").show();
                }
                else if (xhr.get_response() != undefined && xhr.get_response().get_timedOut() == true) {
                    $(".ajax-error-message").text("Request timed out.  Please try again later.");
                    $(".ajax-error").show();
                }
            },

            //完成请求后触发。即在success或error触发后触发

            complete: function (xhr, status) { show.append('complete invoke! status:' + status + '<br/>'); },

            //发送请求前触发

            beforeSend: function (xhr) {

                //可以设置自定义标头
                $(".ajax-error").hide();

            },

        })
    });
}(jQuery));