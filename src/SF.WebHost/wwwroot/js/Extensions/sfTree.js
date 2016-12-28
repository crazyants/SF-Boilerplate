(function ($) {
    'use strict';

    // Private SFTree "class" that will represent instances of the node tree in memory
    // and provide all functionality needed by instances of a treeview control.
    var SFTree = function (element, options) {
        this.$el = $(element);
        this.options = options;
        this.selectedNodes = [];

        // Create an object-based event aggregator (not DOM-based)
        // for internal eventing
        this.events = $({});
    },

        // Generic recursive utility function to find a node in the tree by its id
		_findNodeById = function (id, array) {
		    var currentNode,
				node;

		    if (!array || typeof array.length !== 'number') {
		        return null;
		    }

		    for (var i = 0; i < array.length; i++) {
		        currentNode = array[i];

		        // remove surrounding single quotes from id if they exist
		        var idCompare = id.toString().replace(/(^')|('$)/g, '');

		        if (currentNode.id.toString() === idCompare) {
		            return currentNode;
		        } else if (currentNode.hasChildren) {
		            node = _findNodeById(id, currentNode.children || []);

		            if (node) {
		                return node;
		            }
		        }
		    }

		    return null;
		},

        // Default utility function to attempt to map a SF.Web.UI.Controls.Pickers.TreeViewItem
        // to a more standard JS object.
		_mapArrayDefault = function (arr) {
		    return $.map(arr, function (item) {
		        var node = {
		            id: item.Guid || item.id,
		            name: item.name || item.title,
		            iconCssClass: item.iconCssClass,
		            parentId: item.parentId,
		            hasChildren: item.hasChildren,
		            isActive: item.isActive,
		            countInfo: item.countInfo
		        };

		        if (item.children && typeof item.children.length === 'number') {
		            node.children = _mapArrayDefault(item.children);
		        }

		        return node;
		    });
		},

        // Utility function that attempts to derive a node tree structure given an HTML element
        _mapFromHtml = function ($el, attrs) {
            var nodes = [],
                $ul = $el.children('ul');

            $ul.children('li').each(function () {
                var $li = $(this),
                    node = {
                        id: $li.attr('data-id'),
                        name: $li.children('span').first().html(),
                        hasChildren: $li.children('ul').length > 0,
                        isOpen: $li.attr('data-expanded') === 'true'
                    };

                if (attrs && typeof attrs.length === 'number') {
                    for (var i = 0; i < attrs.length; i++) {
                        node[attrs[i]] = $li.attr('data-' + attrs[i]);
                    }
                }

                if (node.hasChildren) {
                    node.children = _mapFromHtml($li, attrs);
                }

                nodes.push(node);
            });

            return nodes;
        };

    // Prototype declaration for SFTree, holds all new functionality of the tree
    SFTree.prototype = {
        constructor: SFTree,
        init: function () {
            // Load data into tree asynchronously
            var promise = this.fetch(this.options.id),
				self = this;

            this.showLoading(this.$el);

            // If Selected Ids is set, pre-select those nodes
            promise.done(function () {
                if (self.options.selectedIds && typeof self.options.selectedIds.length === 'number') {
                    self.clear();
                    self.setSelected(self.options.selectedIds);
                }

                self.render();
                self.discardLoading(self.$el);
                self.initTreeEvents();
            });

            // If attempt to load data fails, display error message
            promise.fail(function (msg) {
                self.renderError(msg);
                self.discardLoading(self.$el);
            });
        },
        fetch: function (id) {
            var self = this,
                startingNode = _findNodeById(id, this.nodes),

                // Using a jQuery Deferred to control when this operation will get returned to the caller.
                // Since the fetch operation may span multiple AJAX requests, we need a good way to control
                // how the caller will be notified of completion.
                dfd = $.Deferred(),

                // Create a queue of Ids to expand the corresponding nodes
                toExpand = [],

                // Create a "queue" or hash of AJAX calls that are currently in progress
                inProgress = {},

                // Handler function to determine whether or not the fetch operation is complete.
                onProgressNotification = function () {
                    var numberInQueue = Object.keys(inProgress).length;

                    // If we've drained the queue of all items to prefetch,
                    // and there are no requests in queue currentling being fetched,
                    // and we have not already resolved the deferred, return
                    // control to the caller.
                    if (toExpand.length === 0 && numberInQueue === 0 && dfd.state() !== 'resolved') {
                        dfd.resolve();
                    }
                },

                // Wrapper function around jQuery.ajax. Appends a handler to databind the
                // resulting JSON from the server and returns the promise
                getNodes = function (parentId, parentNode) {
                    var restUrl = self.options.restUrl;

                    // If the Tree Node we are loading has an EntityId attribute, use it to identify the associated Data Entity key - otherwise use the Tree Node identifier itself.
                    // The Data Entity key identifies the node data we are requesting from the REST source to load into the tree.
                    if (parentNode && parentNode.entityId) {
                        restUrl += parentNode.entityId;
                    } else {
                        restUrl += parentId;
                    }

                    if (self.options.restParams) {
                        restUrl += self.options.restParams;
                    }

                    self.clearError();

                    return $.ajax({
                        url: restUrl,
                        dataType: 'json',
                        contentType: 'application/json'
                    })
                        .done(function (data) {
                            try {
                                self.dataBind(data, parentNode);
                                if (self.options.success)
                                    self.options.success(data);
                            } catch (e) {
                                dfd.reject(e);
                            }
                        })
                        .fail(function (jqXHR, textStatus, errorThrown) {
                            self.renderError(jqXHR.responseJSON ? jqXHR.responseJSON.ExceptionMessage : errorThrown);
                        });
                };

            if (this.options.restUrl) {
                if (this.options.expandedIds && typeof this.options.expandedIds.length === 'number') {
                    toExpand = this.options.expandedIds;

                    // Listen for progress on the Deferred and pass it the handler to
                    // check if we're "done"
                    dfd.progress(onProgressNotification);

                    // Listen to internal databound event
                    this.events.on('nodes:dataBound', function () {
                        // Pop the top item off the "stack" to de-queue it...
                        var currentId = toExpand.shift(),
                            currentNode;

                        if (!currentId) {
                            return;
                        }

                        currentNode = _findNodeById(currentId, self.nodes);
                        while (currentNode == null && toExpand.length > 0) {
                            // if we can't find it, try the next one until we find one or run out of expanded ids
                            currentId = toExpand.shift();
                            currentNode = _findNodeById(currentId, self.nodes);
                        }

                        if (!currentNode) {
                            return;
                        }

                        // If we find the node, make sure it's expanded, and fetch its children
                        currentNode.isOpen = true;

                        // Queue up current node
                        inProgress[currentId] = currentId;
                        getNodes(currentId, currentNode).done(function () {
                            // Dequeue on completion
                            delete inProgress[currentId];
                            // And notify the Deferred of progress
                            dfd.notify();
                        });
                    });
                }

                // When databound, check to see if fetching is complete
                this.events.on('nodes:dataBound', onProgressNotification);

                // Get initial node's data
                getNodes(id, startingNode);
            } else if (this.options.local) {
                // Assuming there is local data defined, attempt to databind it
                try {
                    this.dataBind(this.options.local);
                    if (this.options.success)
                        this.options.success(this.options.local);
                    dfd.resolve();
                } catch (e) {
                    dfd.reject(e);
                }
            } else {
                // Otherwise attempt to databind on HTML of the current element
                this.nodes = _mapFromHtml(this.$el, this.options.mapping.include);;
                dfd.resolve();
            }

            return dfd.promise();
        },

        // Attempt to load data returned by `fetch` into the current sfTree's
        // node data structure
        dataBind: function (data, parentNode) {
            var nodeArray,
                i;

            if (!data || typeof this.options.mapping.mapData !== 'function') {
                throw 'Unable to load data!';
            }

            // Call configured `mapData` function. If it wasn't overridden by the user,
            // `_mapArrayDefault` will be called.
            nodeArray = this.options.mapping.mapData(data);

            for (i = 0; i < nodeArray.length; i++) {
                nodeArray[i].isOpen = false;
            }

            // If a parent node is supplied, append the result set to the parent node.
            if (parentNode) {
                parentNode.children = nodeArray;
                // Otherwise the result set would be the root array.
            } else {
                this.nodes = nodeArray;
            }

            // Trigger "internal" databound event and trigger "public" databound event
            // via the $el to notify the DOM
            this.events.trigger('nodes:dataBound');
            this.$el.trigger('sfTree:dataBound');
            return nodeArray;
        },

        // Recursively render out each node in the DOM via the `$el` property
        render: function () {
            var self = this,
				$ul = $('<ul/>'),
				renderNode = function ($list, node) {
				    var $li = $('<li/>'),
						$childUl,
						includeAttrs = self.options.mapping.include,
				        folderCssClass = node.isOpen ? self.options.iconClasses.branchOpen : self.options.iconClasses.branchClosed,
				        leafCssClass = node.iconCssClass || self.options.iconClasses.leaf;

				    $li.addClass('sftree-item')
						.addClass(node.hasChildren ? 'sftree-folder' : 'sftree-leaf')
                        .addClass((!node.hasOwnProperty('isActive') || node.isActive) ? '' : 'is-inactive')
						.attr('data-id', node.id)
						.attr('data-parent-id', node.parentId);

				    // Include any configured custom data-* attributes to be decorated on the <li>
				    for (var i = 0; i < includeAttrs.length; i++) {
				        $li.attr('data-' + includeAttrs[i], node[includeAttrs[i]]);
				    }

				    // ensure we only get Text for the tooltip
				    var tmp = document.createElement("DIV");
				    tmp.innerHTML = node.name;
				    var nodeText = tmp.textContent || tmp.innerText || "";

				    var countInfoHtml = '';
				    if (typeof (node.countInfo) != 'undefined' && node.countInfo != null) {
				        countInfoHtml = '<span class="label label-tree margin-l-sm">' + node.countInfo + '</span>';
				    }

				    $li.append('<span class="sftree-name" title="' + nodeText.trim() + '"> ' + node.name + countInfoHtml + '</span>');

				    for (var i = 0; i < self.selectedNodes.length; i++) {
				        if (self.selectedNodes[i].id == node.id) {
				            $li.find('.sftree-name').addClass('selected');
				            break;
				        }
				    }

				    if (node.hasChildren) {
				        $li.prepend('<i class="sftree-icon icon-fw ' + folderCssClass + '"></i>');

				        if (node.iconCssClass) {
				            $li.find('.sftree-name').prepend('<i class="icon-fw ' + node.iconCssClass + '"></i>');
				        }
				    } else {
				        if (leafCssClass) {
				            $li.find('.sftree-name').prepend('<i class="icon-fw ' + leafCssClass + '"></i>');
				        }
				    }

				    $list.append($li);

				    if (node.hasChildren && node.children) {
				        $childUl = $('<ul/>');
				        $childUl.addClass('sftree-children');

				        if (!node.isOpen) {
				            $childUl.hide();
				        }

				        $li.append($childUl);

				        var l = node.children.length;
				        for (var i = 0; i < l; i++) {
				            renderNode($childUl, node.children[i]);
				        }
				    }
				};

            // Clear tree and prepare to re-render
            this.$el.empty();
            $ul.addClass('sftree');
            this.$el.append($ul);

            $.each(this.nodes, function (index, node) {
                renderNode($ul, node);
            });

            this.$el.trigger('sfTree:rendered');
        },

        // clear the error message
        clearError: function () {
            this.$el.siblings('.js-sftree-alert').remove();
        },

        // Render Bootstrap alert displaying the error message.
        renderError: function (msg) {
            this.clearError();
            this.discardLoading(this.$el);
            var $warning = $('<div class="alert alert-danger alert-dismissable js-sftree-alert"/>');
            $warning.append('<button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>')
                .append('<strong><i class="fa fa-exclamation-triangle"></i> Error </strong>')
                .append(msg);
            $warning.insertBefore(this.$el);
        },

        // Show loading spinner
        showLoading: function ($element) {
            $element.append(this.options.loadingHtml);
        },

        // Remove loading spinner
        discardLoading: function ($element) {
            $element.find('.sftree-loading').remove();
        },

        // Clears all selected nodes
        clear: function () {
            this.selectedNodes = [];
            this.render();
        },

        // Sets selected nodes given an array of ids
        setSelected: function (array) {
            this.selectedNodes = [];

            var currentNode,
                i;

            for (i = 0; i < array.length; i++) {
                currentNode = _findNodeById(array[i], this.nodes);

                if (currentNode) {
                    this.selectedNodes.push(currentNode);

                    // trigger the node as selected
                    this.$el.trigger('sfTree:selected', currentNode.id);
                }
            }
        },
        // Wire up DOM events for sfTree instance
        initTreeEvents: function () {
            var self = this;

            // remove event to make sure it doesn't get attached multiple times
            this.$el.off('click');

            // Expanding or collapsing a node...
            this.$el.on('click', '.sftree-folder > .sftree-icon', function (e) {
                e.preventDefault();
                e.stopPropagation();

                var $icon = $(this),
					$ul = $icon.siblings('ul'),
					id = $icon.parent('li').attr('data-id'),
					node = _findNodeById(id, self.nodes),
					openClass = self.options.iconClasses.branchOpen,
					closedClass = self.options.iconClasses.branchClosed;

                if (node.isOpen) {
                    $ul.hide();
                    node.isOpen = false;
                    $icon.removeClass(openClass).addClass(closedClass);
                    self.$el.trigger('sfTree:collapse');
                } else {
                    node.isOpen = true;
                    $icon.removeClass(closedClass).addClass(openClass);

                    // If the node has children, but they haven't been loaded yet,
                    // attempt to load them first, then re-render
                    if (node.hasChildren && !node.children) {
                        self.showLoading($icon.parent('li'));
                        self.fetch(node.id).done(function () {
                            self.render();
                            self.$el.trigger('sfTree:expand');
                        });
                    } else {
                        $ul.show();
                        self.$el.trigger('sfTree:expand');
                    }
                }
            });

            // Selecting a node...
            this.$el.on('click', '.sftree-item > span', function (e) {
                e.preventDefault();
                e.stopPropagation();

                var $sfTree = $(this).parents('.sftree'),
                    $item = $(this),
                    id = $item.parent('li').attr('data-id'),
                    node = _findNodeById(id, self.nodes),
                    selectedNodes = [],
                    onSelected = self.options.onSelected,
                    i;

                // If multi-select is disabled, clear all previous selections
                if (!self.options.multiselect) {
                    $sfTree.find('.selected').removeClass('selected');
                }

                $item.toggleClass('selected');
                $sfTree.find('.selected').parent('li').each(function (idx, li) {
                    var $li = $(li);
                    selectedNodes.push({
                        id: $li.attr('data-id'),
                        // get the li text excluding child text
                        name: $li.contents(':not(ul)').text()
                    });
                });

                self.selectedNodes = selectedNodes;
                self.$el.trigger('sfTree:selected', id);
                self.$el.trigger('sfTree:itemClicked', id);

                // If there is an array of other events to trigger on select,
                // loop through them and trigger each, passing along the
                // currently selected node's id
                if (!onSelected || typeof onSelected.length !== 'number') {
                    return;
                }

                for (i = 0; i < onSelected.length; i++) {
                    $(document).trigger(onSelected[i], id);
                }
            });
        }
    };

    // jQuery plugin definition
    $.fn.sfTree = function (options) {
        // Make a deep copy of all configuration settings passed in 
        // and merge it with a deep copy of the defaults defined below
        var settings = $.extend(true, {}, $.fn.sfTree.defaults, options);

        // For each element matching the selector, attempt to get an instance
        // of SFTree from $el.data, if not present, create a new instance
        // of SFTree and stash it there, then initialize the tree.
        return this.each(function () {
            var $el = $(this);
            var sfTree = $el.data('sfTree');

            if (!sfTree) {
                // create a new sftree
                sfTree = new SFTree(this, settings);
            }
            else {
                // use the existing sftree but update the settings and clean up selectedNodes
                sfTree.options = settings;
                sfTree.selectedNodes = [];
            }

            $el.data('sfTree', sfTree);
            sfTree.init();
        });
    };

    // Default values to be merged upon initialization of the jQuery plugin
    $.fn.sfTree.defaults = {
        id: 0,
        selectedIds: null,
        expandedIds: null,
        restUrl: null,
        restParams: null,
        local: null,
        multiselect: false,
        success: null,
        loadingHtml: '<span class="sftree-loading"><i class="fa fa-refresh fa-spin"></i></span>',
        iconClasses: {
            branchOpen: 'fa fa-fw fa-caret-down',
            branchClosed: 'fa fa-fw fa-caret-right',
            leaf: ''
        },
        mapping: {
            include: [],
            mapData: _mapArrayDefault
        },
        onSelected: []
    };
}(jQuery));