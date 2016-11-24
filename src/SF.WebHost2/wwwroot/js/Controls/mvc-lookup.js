/*!
 * Mvc.Lookup 1.1.0
 * https://github.com/NonFactors/MVC6.Lookup
 *
 * Copyright © NonFactors
 *
 * Licensed under the terms of the MIT License
 * http://www.opensource.org/licenses/mit-license.php
 */
(function ($) {
    $.widget('mvc.mvclookup', {
        _create: function () {
            if (!this.element.hasClass('mvc-lookup-input')) {
                return;
            }

            this._initOptions();
            this._initFilters();
            this._initAutocomplete();
            this._initLookupOpenSpan();

            this._cleanUp();

            this.reload(false);
        },
        _initOptions: function () {
            var e = this.element;
            var o = this.options;

            o.hiddenElement = $('#' + e.attr('data-mvc-lookup-for'))[0];
            o.sortColumn = e.attr('data-mvc-lookup-sort-column');
            o.sortOrder = e.attr('data-mvc-lookup-sort-order');
            o.page = parseInt(e.attr('data-mvc-lookup-page'));
            var filters = e.attr('data-mvc-lookup-filters');
            o.filters = filters ? filters.split(',') : [];
            o.search = e.attr('data-mvc-lookup-search');
            o.title = e.attr('data-mvc-lookup-title');
            o.rows = e.attr('data-mvc-lookup-rows');
            o.url = e.attr('data-mvc-lookup-url');
        },
        _initFilters: function () {
            for (var i = 0; i < this.options.filters.length; i++) {
                this._initFilter($('[name="' + this.options.filters[i] + '"]'));
            }
        },
        _initFilter: function (filters) {
            var that = this;
            that._on(filters, {
                change: function (e) {
                    if (that.options.filterChange) {
                        that.options.filterChange(e, that.element[0], that.options.hiddenElement);
                    }

                    if (!e.isDefaultPrevented()) {
                        that._select(null, true);
                    }
                }
            });
        },
        _initAutocomplete: function () {
            var that = this;
            this.element.autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: that._formAutocompleteUrl(request.term),
                        success: function (data) {
                            response($.map(data.rows, function (item) {
                                return {
                                    label: item.LookupAcKey,
                                    value: item.LookupAcKey,
                                    item: item
                                };
                            }));
                        }
                    });
                },
                select: function (e, selection) {
                    that._select(selection.item.item, true);
                    e.preventDefault();
                },
                minLength: 1,
                delay: 500
            });

            this.element.on('keyup.mvclookup', function (e) {
                if (e.which != 9 && this.value.length == 0 && $(that.options.hiddenElement).val()) {
                    that._select(null, true);
                }
            });
            this.element.prevAll('.ui-helper-hidden-accessible').remove();
        },
        _initLookupOpenSpan: function () {
            var browse = this.element.nextAll('.mvc-lookup-browse:first');
            if (browse.length != 0) {
                var that = this;

                this._on(browse, {
                    click: function () {
                        if (that.element.is('[readonly]') || that.element.is('[disabled]')) {
                            return;
                        }

                        var timeout;
                        lookup
                            .find('.mvc-lookup-search')
                            .off('keyup.mvclookup')
                            .on('keyup.mvclookup', function (e) {
                                if (e.keyCode < 112 || e.keyCode > 126) {
                                    var input = this;
                                    clearTimeout(timeout);
                                    timeout = setTimeout(function () {
                                        that.options.search = input.value;
                                        that.options.page = 0;
                                        that._update(lookup);
                                    }, 500);
                                }
                            })
                            .val(that.options.search);
                        lookup
                            .find('.mvc-lookup-rows input')
                            .spinner({
                                change: function () {
                                    this.value = that._limitTo(this.value, 1, 99);
                                    that.options.rows = this.value;
                                    that.options.page = 0;
                                    that._update(lookup);
                                }
                            })
                            .val(that._limitTo(that.options.rows, 1, 99));

                        lookup.find('.mvc-lookup-search').attr('placeholder', $.fn.mvclookup.lang.search);
                        lookup.find('.mvc-lookup-error').html($.fn.mvclookup.lang.error);
                        lookup.dialog('option', 'title', that.options.title);
                        lookup.find('thead').empty();
                        lookup.find('tbody').empty();
                        that._update(lookup);

                        setTimeout(function () {
                            var dialog = lookup.dialog('open').parent();
                            dialog.position({
                                my: "center",
                                at: "center",
                                of: window
                            });

                            if (parseInt(dialog.css('left')) < 0) {
                                dialog.css('left', 0);
                            }
                            if (parseInt(dialog.css('top')) < 0) {
                                dialog.css('top', 0);
                            }
                        }, 100);
                    }
                });
            }
        },

        _formAutocompleteUrl: function (search) {
            return this.options.url +
                '?search=' + search +
                '&sortOrder=Asc' +
                '&rows=20' +
                '&page=0' +
                this._formFiltersQuery();
        },
        _formLookupUrl: function (search) {
            return this.options.url +
                '?search=' + search +
                '&sortColumn=' + this.options.sortColumn +
                '&sortOrder=' + this.options.sortOrder +
                '&rows=' + this.options.rows +
                '&page=' + this.options.page +
                this._formFiltersQuery();
        },
        _formFiltersQuery: function () {
            var query = '';
            for (var i = 0; i < this.options.filters.length; i++) {
                var filters = $('[name="' + this.options.filters[i] + '"]');
                for (var j = 0; j < filters.length; j++) {
                    query += '&' + this.options.filters[i] + '=' + filters[j].value;
                }
            }

            return query;
        },

        _defaultSelect: function (data, triggerChanges) {
            if (data) {
                $(this.options.hiddenElement).val(data.LookupIdKey);
                $(this.element).val(data.LookupAcKey);
            } else {
                $(this.options.hiddenElement).val(null);
                $(this.element).val(null);
            }

            if (triggerChanges) {
                $(this.options.hiddenElement).change();
                $(this.element).change();
            }
        },
        _select: function (data, triggerChanges) {
            var event = $.Event(this._defaultSelect);
            if (this.options.select) {
                this.options.select(event, this.element[0], this.options.hiddenElement, data, triggerChanges);
            }

            if (!event.isDefaultPrevented()) {
                this._defaultSelect(data, triggerChanges);
            }
        },

        _limitTo: function (value, min, max) {
            value = parseInt(value);
            if (isNaN(value)) {
                return 20;
            }

            if (value < min) {
                return min;
            }

            if (value > max) {
                return max;
            }

            return value;
        },
        _cleanUp: function () {
            this.element.removeAttr('data-mvc-lookup-sort-column');
            this.element.removeAttr('data-mvc-lookup-sort-order');
            this.element.removeAttr('data-mvc-lookup-filters');
            this.element.removeAttr('data-mvc-lookup-search');
            this.element.removeAttr('data-mvc-lookup-title');
            this.element.removeAttr('data-mvc-lookup-rows');
            this.element.removeAttr('data-mvc-lookup-page');
            this.element.removeAttr('data-mvc-lookup-url');
        },

        _update: function (lookup) {
            var that = this;
            var search = lookup.find('.mvc-lookup-search').val();
            lookup.find('.mvc-lookup-error').fadeOut(300);

            var timeout = setTimeout(function () {
                lookup.find('.mvc-lookup-loading').fadeIn(300);
                lookup.find('table').fadeOut(300);
                lookup.find('ul').fadeOut(300);
            }, 500);

            $.ajax({
                url: that._formLookupUrl(search),
                cache: false,
                success: function (data) {
                    that._updateHeader(lookup, data.columns);
                    that._updateData(lookup, data);
                    that._updateNavbar(lookup, data.filteredRows);

                    clearTimeout(timeout);
                    lookup.find('.mvc-lookup-error').hide();
                    lookup.find('.mvc-lookup-loading').fadeOut(300);
                    lookup.find('table').fadeIn(300);
                    lookup.find('ul').fadeIn(300);
                },
                error: function () {
                    clearTimeout(timeout);
                    lookup.find('.mvc-lookup-error').fadeIn(300);
                    lookup.find('.mvc-lookup-loading').hide();
                    lookup.find('table').hide();
                    lookup.find('ul').hide();
                }
            });
        },
        _updateHeader: function (lookup, columns) {
            var that = this;
            var header = '';

            for (var i = 0; i < columns.length; i++) {
                var column = columns[i];
                if (column.hidden) {
                    continue;
                }

                header += '<th class="' + (column.cssClass || '');
                if (that.options.sortColumn == column.key || (that.options.sortColumn == '' && i == 0)) {
                    header += ' mvc-lookup-' + (that.options.sortOrder == 'Asc' ? 'asc' : 'desc');
                    that.options.sortColumn = column.key;
                }

                header += '" data-column="' + column.key + '">' + (column.header || '') + '</th>';
            }

            lookup.find('thead').html('<tr>' + header + '<th></th></tr>');
            lookup.find('th').click(function () {
                var header = $(this);
                if (!header.attr('data-column')) {
                    return false;
                }

                if (that.options.sortColumn == header.attr('data-column')) {
                    that.options.sortOrder = that.options.sortOrder == 'Asc' ? 'Desc' : 'Asc';
                } else {
                    that.options.sortOrder = 'Asc';
                }

                that.options.sortColumn = header.attr('data-column');
                that._update(lookup);
            });
        },
        _updateData: function (lookup, data) {
            if (data.rows.length == 0) {
                var columns = (data.columns) ? data.columns.length + 1 : 1;
                lookup.find('tbody').html('<tr><td colspan="' + columns + '" style="text-align: center">' + $.fn.mvclookup.lang.noData + '</td></tr>');

                return;
            }

            var tableData = '';
            for (var i = 0; i < data.rows.length; i++) {
                var tableRow = '<tr>';
                var row = data.rows[i];

                for (var j = 0; j < data.columns.length; j++) {
                    var column = data.columns[j];
                    if (column.hidden) {
                        continue;
                    }

                    tableRow += '<td' + (column.cssClass ? ' class="' + column.cssClass + '">' : '>') + (row[column.key] || '') + '</td>';
                }

                tableRow += '<td></td></tr>';
                tableData += tableRow;
            }

            lookup.find('tbody').html(tableData);
            var selectRows = lookup.find('tbody tr');
            for (var k = 0; k < selectRows.length; k++) {
                this._bindSelect(lookup, selectRows[k], data.rows[k]);
            }
        },
        _updateNavbar: function (lookup, filteredRows) {
            var pageLength = lookup.find('.mvc-lookup-rows input').val();
            var totalPages = parseInt(filteredRows / pageLength) + 1;
            if (filteredRows % pageLength == 0) {
                totalPages--;
            }

            if (totalPages == 0) {
                lookup.find('ul').empty();
            } else {
                this._paginate(totalPages);
            }
        },
        _paginate: function (totalPages) {
            var startingPage = Math.floor(this.options.page / 5) * 5;
            var currentPage = this.options.page;
            var page = startingPage;
            var pagination = '';
            var that = this;

            if (totalPages > 5 && currentPage > 0) {
                pagination = '<li><span data-page="0">&laquo;</span></li><li><span data-page="' + (currentPage - 1) + '">&lsaquo;</span></li>';
            }

            while (page < totalPages && page < startingPage + 5) {
                pagination += '<li' + (page == this.options.page ? ' class="active"' : '') + '><span data-page="' + page + '">' + (++page) + '</span></li>';
            }

            if (totalPages > 5 && currentPage < (totalPages - 1)) {
                pagination += '<li><span data-page="' + (currentPage + 1) + '">&rsaquo;</span></li><li><span data-page="' + (totalPages - 1) + '">&raquo;</span></li>';
            }

            lookup.find('ul').html(pagination).find('li:not(.active) > span').click(function (e) {
                that.options.page = parseInt($(this).data('page'));
                that._update(lookup);
            });
        },
        _bindSelect: function (lookup, selectRow, data) {
            var that = this;
            that._on(selectRow, {
                click: function () {
                    lookup.dialog('close');
                    that._select(data, false);
                }
            });
        },

        reload: function (triggerChanges) {
            var that = this;
            triggerChanges = triggerChanges == null ? true : triggerChanges;

            var id = $(that.options.hiddenElement).val();
            if (id) {
                $.ajax({
                    url: that.options.url + '?id=' + id + '&rows=1' + this._formFiltersQuery(),
                    cache: false,
                    success: function (data) {
                        if (data.rows.length > 0) {
                            that._select(data.rows[0], triggerChanges);
                        }
                    }
                });
            } else {
                that._select(null, triggerChanges);
            }
        },

        _destroy: function () {
            var e = this.element;
            var o = this.options;

            e.attr('data-mvc-lookup-filters', o.filters.join());
            e.attr('data-mvc-lookup-sort-column', o.sortColumn);
            e.attr('data-mvc-lookup-sort-order', o.sortOrder);
            e.attr('data-mvc-lookup-search', o.search);
            e.attr('data-mvc-lookup-title', o.title);
            e.attr('data-mvc-lookup-rows', o.rows);
            e.attr('data-mvc-lookup-page', o.page);
            e.attr('data-mvc-lookup-url', o.url);
            e.autocomplete('destroy');

            return this._super();
        }
    });

    $.fn.mvclookup.lang = {
        error: 'Error while retrieving records',
        noData: 'No data found',
        search: 'Search...'
    };

    var lookup = $('#MvcLookup');

    $(function () {
        lookup.find('.mvc-lookup-rows input').spinner({ min: 1, max: 99 });
        lookup.dialog({
            classes: { 'ui-dialog': 'mvc-lookup-dialog' },
            dialogClass: 'mvc-lookup-dialog',
            autoOpen: false,
            minHeight: 210,
            height: 'auto',
            minWidth: 455,
            width: 'auto',
            modal: true
        });

        $('.mvc-lookup-dialog').resizable({
            handles: 'w,e',
            stop: function (event, ui) {
                $(this).css('height', 'auto');
            }
        });

        $('.mvc-lookup-input').mvclookup();
    });
})(jQuery);
