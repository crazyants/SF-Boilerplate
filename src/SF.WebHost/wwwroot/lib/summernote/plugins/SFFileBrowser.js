var SFFileBrowser = function (context) {
    var ui = $.summernote.ui;

    // create button
    var button = ui.button({
        contents: '<i class="fa fa-file-text-o"/>',
        tooltip: 'File Browser',
        click: function () {
            context.invoke('editor.saveRange');
            var iframeUrl = SF.settings.get('baseUrl') + "htmleditorplugins/sffilebrowser";
            iframeUrl += "?rootFolder=" + encodeURIComponent(context.options.sfFileBrowserOptions.documentFolderRoot);
            iframeUrl += "&browserMode=doc";
            iframeUrl += "&fileTypeBlackList=" + encodeURIComponent(context.options.sfFileBrowserOptions.fileTypeBlackList);
            iframeUrl += "&theme=" + context.options.sfTheme;
            iframeUrl += "&modalMode=1";
            iframeUrl += "&title=Select%20File";

            SF.controls.modal.show(context.layoutInfo.editor, iframeUrl);

            $modalPopupIFrame = SF.controls.modal.getModalPopupIFrame();

            $modalPopupIFrame.load(function () {

                $modalPopupIFrame.contents().off('click');

                $modalPopupIFrame.contents().on('click', '.js-select-file-button', function () {
                    SF.controls.modal.close();
                    var fileResult = $('body iframe').contents().find('.js-filebrowser-result input[type=hidden]').val();
                    if (fileResult) {

                        // iframe returns the result in the format "href|text"
                        var resultParts = fileResult.split('|');

                        context.invoke('editor.restoreRange');
                        context.invoke('editor.createLink', {
                            text: resultParts[1],
                            url: SF.settings.get('baseUrl') + resultParts[0],
                            newWindow: false
                        });
                    }
                });

                $modalPopupIFrame.contents().on('click', '.js-cancel-file-button', function () {
                    SF.controls.modal.close();
                });
            });

            
        }
    });

    if (context.options.sfFileBrowserOptions.enabled) {
        return button.render();   // return button as jquery object 
    }
    else {
        return null;
    }
}