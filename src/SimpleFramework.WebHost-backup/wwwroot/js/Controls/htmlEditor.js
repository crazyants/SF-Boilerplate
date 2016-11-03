(function ($) {
    'use strict';
    window.SF= window.SF|| {};

    SF.htmlEditor = (function () {
        var exports = {
            // full list of available items is at http://summernote.org/deep-dive/#custom-toolbar-popover

            // the toolbar items to include in the HtmlEditor when Toolbar is set to Light 
            toolbar_SFCustomConfigLight:
	            [
                    // [groupName, [list of button]]
                    ['source_group',['sfcodeeditor']],
                    ['style_group1', ['bold', 'italic', 'underline', 'strikethrough', 'ol', 'ul', 'link']],
                    ['style_group2', ['undo', 'redo']],
                    ['style_group3', ['clear']],
                    ['style_group4', ['style']],
                    ['para', ['paragraph']],
                    ['plugins1', ['sfmergefield']],
                    ['plugins2', ['sfimagebrowser', 'sffilebrowser']],
                    ['plugins3', ['sfpastetext', 'sfpastefromword']],
                    ['help_group1', ['help']]
	            ],

            // the toolbar items to include in the HtmlEditor when Toolbar is set to Full 
            toolbar_SFCustomConfigFull:
                [
                    // [groupName, [list of button]]
                    ['source_group', ['sfcodeeditor']],
                    ['style_group1', ['bold', 'italic', 'underline', 'strikethrough', 'ol', 'ul', 'link']],
                    ['style_group2', ['undo', 'redo']],
                    ['style_group3', ['clear']],
                    ['style_group4', ['style']],
                    ['full_toolbar_only', ['fontname', 'fontsize', 'color', 'superscript', 'subscript', 'table', 'hr']],
                    ['para', ['paragraph']],
                    ['plugins1', ['sfmergefield']],
                    ['plugins2', ['sfimagebrowser', 'sffilebrowser']],
                    ['plugins3', ['sfpastetext', 'sfpastefromword']],
                    ['help_group1', ['help']]

                ]
        }

        return exports;

    }());
}(jQuery));