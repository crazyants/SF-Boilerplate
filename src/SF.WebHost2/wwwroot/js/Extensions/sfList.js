(function ($) {
    'use strict';
    
    // Private SFList "class" that will represent instances of the item list in memory
    // and provide all functionality needed by instances of a list control.
    var SFList = function (element) {
        this.$el = $(element);
    };

    // Prototype declaration for SFList, holds all new functionality of the list
    SFList.prototype = {
        constructor: SFList,
        init: function () {
            this.initListEvents();
        },

        // Wire up DOM events for sfList instance
        initListEvents: function () {
            var self = this;

            // remove event to make sure it doesn't get attached multiple times
            this.$el.off('click');

            // Selecting an item...
            this.$el.on('click', '.sflist-item', function (e) {
                e.preventDefault();
                e.stopPropagation();

                var $sfList = $(this).parents('.sflist'),
                    $item = $(this),
                    id = $item.closest('li').attr('data-id');

                // clear all previous selections
                $sfList.find('.selected').removeClass('selected');

                // mark the selected as selected
                $item.toggleClass('selected');

                self.$el.trigger('sfList:selected', id);
            });
        }
    };

    // jQuery plugin definition
    $.fn.sfList = function () {

        // For each element matching the selector, attempt to get an instance
        // of SFList from $el.data, if not present, create a new instance
        // of SFList and stash it there, then initialize the list.
        return this.each(function () {
            var $el = $(this);
            var sfList = $el.data('sfList');
            
            if (!sfList) {
                // create a new sflist
                sfList = new SFList(this);
            }

            $el.data('sfList', sfList);
            sfList.init();
        });
    };
}(jQuery));