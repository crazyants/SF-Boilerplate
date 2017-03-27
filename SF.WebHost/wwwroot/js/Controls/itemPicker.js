(function ($) {
    'use strict';
    window.SF = window.SF || {};
    SF.controls = SF.controls || {};

    SF.controls.itemPicker = (function () {
        var ItemPicker = function (options) {
            this.options = options;

            // set a flag so that the picker only auto-scrolls to a selected item once. This prevents it from scrolling at unwanted times
            this.alreadyScrolledToSelected = false;
        },
            exports;

        ItemPicker.prototype = {
            constructor: ItemPicker,
            initialize: function () {
                var $control = $('#' + this.options.controlId),
                    $tree = $control.find('.treeview'),
                    treeOptions = {
                        multiselect: this.options.allowMultiSelect,
                        restUrl: this.options.restUrl,
                        restParams: this.options.restParams,
                        expandedIds: this.options.expandedIds,
                        success: this.options.success,
                        id: this.options.startingId
                    },
                    $hfItemIds = $control.find('.js-item-id-value'),
                    $hfExpandedIds = $control.find('.js-initial-item-parent-ids-value');

                if (typeof this.options.mapItems === 'function') {
                    treeOptions.mapping = {
                        mapData: this.options.mapItems
                    };
                }

                // clean up the tree (in case it was initialized already, but we are rebuilding it)
                var sfTree = $tree.data('sfTree');
                if (sfTree) {
                    sfTree.nodes = [];
                }
                $tree.empty();

                $control.find('.scroll-container').tinyscrollbar({ size: 120, sizethumb: 20 });
                // Since some hanlers are "live" events, they need to be bound before tree is initialized
                this.initializeEventHandlers();

                if ($hfItemIds.val() && $hfItemIds !== '0') {
                    treeOptions.selectedIds = $hfItemIds.val().split(',');
                }

                if ($hfExpandedIds.val()) {
                    treeOptions.expandedIds = $hfExpandedIds.val().split(',');
                }

                $tree.sfTree(treeOptions);
                this.updateScrollbar();
            },
            initializeEventHandlers: function () {
                var self = this,
                    $control = $('#' + this.options.controlId),
                    $spanNames = $control.find('.selected-names'),
                    $hfItemIds = $control.find('.js-item-id-value'),
                    $hfItemNames = $control.find('.js-item-name-value');

                // Bind tree events
                $control.find('.treeview')
                    .on('sfTree:selected', function () {
                        if ($hfItemNames.val() == '') {
                            if (!self.options.allowMultiSelect) {
                                $control.find('.picker-btn').trigger('click');
                            }
                        }
                    })
                    .on('sfTree:itemClicked', function (e) {
                        // make sure it doesn't autoscroll after something has been manually clicked
                        self.alreadyScrolledToSelected = true;
                        if (!self.options.allowMultiSelect) {
                            $control.find('.picker-btn').trigger('click');
                        }
                    })
                    .on('sfTree:expand sfTree:collapse sfTree:dataBound', function (evt) {
                        self.updateScrollbar();
                    })
                    .on('sfTree:rendered', function (evt) {
                        self.scrollToSelectedItem();
                    });

                $control.find('a.picker-label').click(function (e) {
                    e.preventDefault();
                    $control.find('.picker-menu').first().toggle(function () {
                        self.scrollToSelectedItem();
                    });
                });

                $control.find('.picker-cancel').click(function () {
                    $(this).closest('.picker-menu').slideUp(function () {
                        self.updateScrollbar();
                    });
                });

                // have the X appear on hover if something is selected
                if ($hfItemIds.val() && $hfItemIds.val() !== '0') {
                    $control.find('.picker-select-none').addClass('rollover-item');
                    $control.find('.picker-select-none').show();
                }

                $control.find('.picker-btn').click(function (el) {

                    var sfTree = $control.find('.treeview').data('sfTree'),
                            selectedNodes = sfTree.selectedNodes,
                            selectedIds = [],
                            selectedNames = [];

                    $.each(selectedNodes, function (index, node) {
                        selectedIds.push(node.id);
                        selectedNames.push(node.name);
                    });

                    $hfItemIds.val(selectedIds.join(','));
                    $hfItemNames.val(selectedNames.join(','));

                    // have the X appear on hover. something is selected
                    $control.find('.picker-select-none').addClass('rollover-item');
                    $control.find('.picker-select-none').show();

                    $spanNames.text(selectedNames.join(', '));

                    $(this).closest('.picker-menu').slideUp(function () {
                        self.updateScrollbar();
                    });

                    if (!(el && el.originalEvent && el.originalEvent.srcElement == this)) {
                        // if this event was called by something other than the button itself, make sure the execute the href (which is probably javascript)
                        var jsPostback = $(this).attr('href');
                        if (jsPostback) {
                            window.location = jsPostback;
                        }
                    }
                });

                $control.find('.picker-select-none').click(function (e) {
                    e.stopImmediatePropagation();
                    var sfTree = $control.find('.treeview').data('sfTree');
                    sfTree.clear();
                    $hfItemIds.val('0');
                    $hfItemNames.val('');

                    // don't have the X appear on hover. nothing is selected
                    $control.find('.picker-select-none').removeClass('rollover-item');
                    $control.find('.picker-select-none').hide();

                    $control.siblings('.js-hide-on-select-none').hide();

                    $spanNames.text(self.options.defaultText);
                });
            },
            updateScrollbar: function (sPosition) {
                // first, update this control's scrollbar, then the modal's
                var $container = $('#' + this.options.controlId).find('.scroll-container')

                if ($container.is(':visible')) {
                    if (!sPosition) {
                        sPosition = 'relative'
                    }
                    $container.tinyscrollbar_update(sPosition);
                }

                // update the outer modal  
                SF.dialogs.updateModalScrollBar(this.options.controlId);
            },
            scrollToSelectedItem: function () {
                var $selectedItem = $('#' + this.options.controlId).find('.picker-menu').find('.selected').first()
                if ($selectedItem.length && (!this.alreadyScrolledToSelected)) {
                    var $scrollContainer = $selectedItem.closest('.scroll-container');
                    var itemTop = $selectedItem.offset().top
                    var itemBottom = $selectedItem.offset().top + $selectedItem.height();
                    var viewportTop = $scrollContainer.offset().top;
                    var viewportBottom = $scrollContainer.offset().top + $scrollContainer.height();

                    // scroll so the item is at top if it isn't already showing within the viewport
                    if (itemTop < viewportTop || itemBottom > viewportBottom) {
                        var treeview = $selectedItem.closest('.treeview');
                        var pPosition = $selectedItem.offset().top - treeview.offset().top;
                        // initialize/update the scrollbar and set to a specific position
                        this.updateScrollbar(pPosition);
                        this.alreadyScrolledToSelected = true;
                    }
                    else {
                        // initialize/update the scrollbar 
                        this.updateScrollbar();
                    }
                } else {
                    // initialize/update the scrollbar 
                    this.updateScrollbar();
                }
            }
        };

        exports = {
            defaults: {
                id: 0,
                controlId: null,
                restUrl: null,
                restParams: null,
                allowMultiSelect: false,
                defaultText: '',
                selectedIds: null,
                expandedIds: null,
                selected: null,
            },
            controls: {},
            initialize: function (options) {
                var settings,
                    itemPicker;

                if (!options.controlId) throw 'controlId must be set';
                if (!options.restUrl) throw 'restUrl must be set';

                settings = $.extend({}, exports.defaults, options);

                if (!settings.defaultText) {
                    settings.defaultText = exports.defaults.defaultText;
                }

                itemPicker = new ItemPicker(settings);
                exports.controls[settings.controlId] = itemPicker;
                itemPicker.initialize();
            }
        };

        return exports;
    }());
}(jQuery));