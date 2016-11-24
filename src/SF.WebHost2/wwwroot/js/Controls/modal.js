(function ($) {
    'use strict';
    window.SF = window.SF || {};
    SF.controls = SF.controls || {};

    SF.controls.modal = (function () {
        // shows the IFrame #modal-popup modal
        var _showModalPopup = function (options) {
            var $modalPopup = $('#modal-popup');
            var $modalPopupIFrame = $modalPopup.find('iframe');

            // Use the anchor tag's title attribute as the title of the dialog box
            if (options.sender.attr('title') != undefined) {
                $('#modal-popup_panel h3').html(options.sender.attr('title') + ' <small></small>');
            }
            if (options.hideFooter)
                $('.modal-footer').hide();
            //$(window).resize(function () {
            //    _resize($modalPopupIFrame, options);
            //});

            _resize($modalPopupIFrame, options);
            $modalPopupIFrame.one('load', function () {

                // now that the iframe is loaded, show it, set it's initial height and do a modal layout
                $('#modal-popup').fadeTo(0, 1);

                // $(this).height(options.height);
               // _resize($modalPopupIFrame, options);

                $('body').addClass('modal-open');
                $('#modal-popup').modal('layout');

                if (options.title != "")
                    $modalPopupIFrame.contents().find(".modal-title").html(options.title);

            });
     
            // Use the anchor tag's href attribute as the source for the iframe
            // this will trigger the load event (above) which will show the popup
            $('#modal-popup').fadeTo(0, 0);
            $modalPopupIFrame.attr('src', options.popupUrl);
            $('#modal-popup').modal({
                show: true,
                backdrop: 'static',
                keyboard: false,
                attentionAnimation: '',
                modalOverflow: true
            });
        },

         _resize = function (iframe, opts) {
             var $cur = $(iframe);
             //设置高度  
             if (opts.height != 0) {
                 var height = $(window).height();
                 if (opts.height < 0) {
                     height = height + opts.height;
                     if ($(window).height() - height + opts.height < 4) {
                         //解决IE的4像素问题，IE的应用可能有一些限制  
                         $("html").css("overflow-y", "hidden");
                     }
                 } else {
                     height = (height > opts.height) ? opts.height : $(window).height();
                     if ($(window).height() - height < 4) {
                         //解决IE的4像素问题，IE的应用可能有一些限制  
                         $("html").css("overflow-y", "hidden");
                     }
                 }
                 $cur.height(height);
             }
         },

        // shows a non-IFrame modal dialog control
        _showModalControl = function ($modalDialog, managerId) {
            $('body').addClass('modal-open');
            $modalDialog.modal({
                show: true,
                manager: managerId,
                backdrop: 'static',
                keyboard: false,
                attentionAnimation: '',
                modalOverflow: true,
                replace: true
            });
        },

        exports = {
            // updates the side of the modal that the control is in.  
            // this function works for both the IFrame modal and ModalDialog control
            updateSize: function (controlId) {
                var $control = typeof (controlId) == 'string' ? $('#' + controlId) : $(controlId);
                if ($control && $control.length) {
                    var $modalBody = $control.closest('.modal-body');
                    if ($modalBody.is(':visible')) {
                        $modalBody[0].style.minHeight = "0px";
                        var scrollHeight = $modalBody.prop('scrollHeight');
                        if ($modalBody.outerHeight() != scrollHeight) {
                            // if modalbody didn't already grow to fit (maybe because of a bootstrap dropdown) make modal-body big enough to fit.
                            $modalBody[0].style.minHeight = scrollHeight + "px";

                            // force the resizeDetector to fire
                            if ($('#dialog').length && $('#dialog')[0].resizedAttached) {
                                $('#dialog')[0].resizedAttached.call();
                            }
                        }
                    }
                }

            },
            // closes the #modal-popup modal (IFrame Modal)
            close: function (msg) {
                // do a setTimeout so this fires after the postback
                $('#modal-popup').hide();
                setTimeout(function () {
                    $('#modal-popup iframe').attr('src', '');
                    $('#modal-popup').modal('hide');

                }, 0);

                $('body').removeClass('modal-open');

                if (msg && msg != '') {

                    if (msg == 'PAGE_UPDATED') {
                        location.reload(true);
                    }
                    else {
                        $('#sf-config-trigger-data').val(msg);
                        $('#sf-config-trigger').click();
                    }
                }
            },
            // closes a ModalDialog control (non-IFrame Modal) 
            closeModalDialog: function ($modalDialog) {
                if ($modalDialog && $modalDialog.length && $modalDialog.modal) {
                    $modalDialog.modal('hide');
                }

                // if all modals are closed, remove all the modal-open class 
                if (!$('.modal').is(':visible')) {
                    {
                        $('.modal-open').removeClass('modal-open');
                    }
                }
                $('body').removeClass('modal-open');
            },
            // shows the #modal-popup modal (IFrame Modal)
            show: function (options) {
                var defaults = {
                    sender: null,
                    popupUrl: '',
                    width: "100",
                    height: "100",
                    detailsId: '',
                    postbackUrl: '',
                    title: '系统窗口',
                    hideFooter:true,
                };
                var options = $.extend(defaults, options);
                //var _width = SF.utility.windowWidth() > parseInt(options.width.replace('px', '')) ? options.width : SF.utility.windowWidth() + 'px';
                //var _height = SF.utility.windowHeight() > parseInt(options.height.replace('px', '')) ? options.height : SF.utility.windowHeight() + 'px';

                _showModalPopup(options);
            },
            // shows a ModalDialog control (non-IFrame Modal) 
            showModalDialog: function ($modalDialog, managerId) {
                _showModalControl($modalDialog, managerId);
            },

            // gets the IFrame element of the global the Modal Popup (for the IFrame Modal)
            getModalPopupIFrame: function () {
                var $modalPopup = $('#modal-popup');
                var $modalPopupIFrame = $modalPopup.find('iframe');

                return $modalPopupIFrame;
            },

            resize: function (height) {
                var $modalPopup = top.$('#modal-popup');
                var $modalPopupIFrame = $modalPopup.find('iframe');
                $modalPopupIFrame.height(height);
            },


        };

        return exports;
    }());
}(jQuery));