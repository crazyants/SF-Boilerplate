var SFImageBrowser = function (context) {
    var ui = $.summernote.ui;

    // create button
    var button = ui.button({
        contents: '<i class="fa fa-picture-o"/>',
        tooltip: 'Image Browser',
        click: function () {

            context.invoke('editor.saveRange');
            var iframeUrl = SF.settings.get('baseUrl') + "htmleditorplugins/sffilebrowser";
            iframeUrl += "?rootFolder=" + encodeURIComponent(context.options.sfFileBrowserOptions.imageFolderRoot);
            iframeUrl += "&browserMode=image";
            iframeUrl += "&fileTypeBlackList=" + encodeURIComponent(context.options.sfFileBrowserOptions.fileTypeBlackList);
            iframeUrl += "&imageFileTypeWhiteList=" + encodeURIComponent(context.options.sfFileBrowserOptions.imageFileTypeWhiteList);
            iframeUrl += "&theme=" + context.options.sfTheme;
            iframeUrl += "&modalMode=1";
            iframeUrl += "&title=Select%20Image";

            SF.controls.modal.show(context.layoutInfo.editor, iframeUrl);

            $modalPopupIFrame = SF.controls.modal.getModalPopupIFrame();

            $modalPopupIFrame.load(function () {

                $modalPopupIFrame.contents().off('click');

                $modalPopupIFrame.contents().on('click', '.js-select-file-button', function () {
                    SF.controls.modal.close();
                    var fileResult = $('body iframe').contents().find('.js-filebrowser-result input[type=hidden]').val();
                    if (fileResult) {
                        // iframe returns the result in the format "imageSrcUrl|imageAltText"
                        var resultParts = fileResult.split('|');
                        var imageElement = document.createElement('img');
                        var url = SF.settings.get('baseUrl') + resultParts[0];
                        var altText = resultParts[1];
                        
                        var imgTarget = context.invoke('editor.restoreTarget');
                        // if they already have an img selected, just change the src of the image
                        if (imgTarget) {
                            imgTarget.src = url;
                            imgTarget.alt = altText;
                        }
                        else {
                            // insert the image at 25% to get them started
                            context.invoke('editor.restoreRange');
                            context.invoke('editor.insertImage', url, function ($image) {
                                $image.css('width', '25%');
                                $image.attr('alt', altText);
                            });
                        }
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