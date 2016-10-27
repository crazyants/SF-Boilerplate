var SFMergeField = function (context) {
    var ui = $.summernote.ui;

    // create button
    var button = ui.button({
        contents: '{ }',
        tooltip: 'Merge Field',
        click: function () {
            context.invoke('editor.saveRange');
            var iframeUrl = SF.settings.get('baseUrl') + "htmleditorplugins/SFMergeField?mergeFields=" + encodeURIComponent(context.options.sfMergeFieldOptions.mergeFields);
            iframeUrl += "&theme=" + context.options.sfTheme;
            iframeUrl += "&modalMode=1";

            SF.controls.modal.show(context.layoutInfo.editor, iframeUrl);

            $modalPopupIFrame = SF.controls.modal.getModalPopupIFrame();

            $modalPopupIFrame.load(function () {

                $modalPopupIFrame.contents().off('click');

                $modalPopupIFrame.contents().on('click', '.js-select-mergefield-button', function () {
                    SF.controls.modal.close();

                    var mergeFields = $('body iframe').contents().find('.js-mergefieldpicker-result input[type=hidden]').val();
                    var url = SF.settings.get('baseUrl') + 'api/MergeFields/' + encodeURIComponent(mergeFields);
                    $.get(url, function (data) {
                        {
                            context.invoke('editor.restoreRange');
                            context.invoke('editor.pasteHTML', data);
                        }
                    });
                });

                $modalPopupIFrame.contents().on('click', '.js-cancel-mergefield-button', function () {
                    SF.controls.modal.close();
                });
            });

            
        }
    });
    
    if (context.options.sfMergeFieldOptions.enabled) {
        return button.render();   // return button as jquery object 
    }
    else {
        return null;
    }
}