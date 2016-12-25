using SF.Web.Control.JqGrid.Infrastructure.Enums;

namespace SF.Web.Control.JqGrid.Infrastructure.Constants
{
    /// <summary>
    /// Contains default values for jqGrid options
    /// </summary>
    public static class JqGridOptionsDefaults
    {
        /// <summary>
        /// Response options.
        /// </summary>
        public static class Response
        {
            /// <summary>
            /// The name of an array that contains the actual data.
            /// </summary>
            public const string Records = "rows";

            /// <summary>
            /// The name for page index parametr.
            /// </summary>
            public const string PageIndex = "page";

            /// <summary>
            /// The name of a field that contains total pages count.
            /// </summary>
            public const string TotalPagesCount = "total";

            /// <summary>
            /// The name of a field that contains total records count.
            /// </summary>
            public const string TotalRecordsCount = "records";

            /// <summary>
            /// The name of an array that contains custom data.
            /// </summary>
            public const string UserData = "userdata";

            /// <summary>
            /// The name of a field that contains record identifier.
            /// </summary>
            public const string RecordId = "id";

            /// <summary>
            /// The name of an array that contains record values.
            /// </summary>
            public const string RecordValues = "cell";

            /// <summary>
            /// The value indicating if the information for the data in the row is repeatable.
            /// </summary>
            public const bool RepeatItems = true;
        }

        /// <summary>
        /// Request options.
        /// </summary>
        public static class Request
        {
            /// <summary>
            /// The name for page index parametr.
            /// </summary>
            public const string PageIndex = "page";

            /// <summary>
            /// The name for records count parametr.
            /// </summary>
            public const string RecordsCount = "rows";

            /// <summary>
            /// The name for sorting name parametr.
            /// </summary>
            public const string SortingName = "sidx";

            /// <summary>
            /// The name for sorting order parametr.
            /// </summary>
            public const string SortingOrder = "sord";

            /// <summary>
            /// The name for searching parametr.
            /// </summary>
            public const string Searching = "_search";

            /// <summary>
            /// The name for id parametr.
            /// </summary>
            public const string Id = "id";

            /// <summary>
            /// The name for operator parametr.
            /// </summary>
            public const string Operator = "oper";

            /// <summary>
            /// The name for edit operator parametr.
            /// </summary>
            public const string EditOperator = "edit";

            /// <summary>
            /// The name for add operator parametr.
            /// </summary>
            public const string AddOperator = "add";

            /// <summary>
            /// The name for delete operator parametr.
            /// </summary>
            public const string DeleteOperator = "del";

            /// <summary>
            /// The name for subgrid id parametr.
            /// </summary>
            public const string SubgridId = "id";

            /// <summary>
            /// The name for total rows parametr.
            /// </summary>
            public const string TotalRows = "totalrows";
        }

        /// <summary>
        /// Column model options.
        /// </summary>
        public static class ColumnModel
        {
            /// <summary>
            /// Formatter options.
            /// </summary>
            public static class Formatter
            {
                /// <summary>
                /// The decimal places for formatter.
                /// </summary>
                public const int DecimalPlaces = 2;

                /// <summary>
                /// The decimal separator for formatter.
                /// </summary>
                public const string DecimalSeparator = ".";

                /// <summary>
                /// The date source format for formatter.
                /// </summary>
                public const string SourceFormat = "Y-m-d";

                /// <summary>
                /// The date output format for formatter.
                /// </summary>
                public const string OutputFormat = "n/j/Y";

                /// <summary>
                /// The thousands separator for formatter.
                /// </summary>
                public const string ThousandsSeparator = " ";

                /// <summary>
                /// The checkbox disabled property for formatter.
                /// </summary>
                public const bool Disabled = true;

                /// <summary>
                /// The first parameter that is added after the ShowAction.
                /// </summary>
                public const string IdName = "id";

                /// <summary>
                /// The default value for integer formatter.
                /// </summary>
                public const string IntegerDefaultValue = "0";

                /// <summary>
                /// The default value for number formatter.
                /// </summary>
                public const string NumberDefaultValue = "0.00";

                /// <summary>
                /// The default value for currency formatter.
                /// </summary>
                public const string CurrencyDefaultValue = "0.00";

                /// <summary>
                /// The value indicating if edit button is enabled for actions formatter.
                /// </summary>
                public const bool EditButton = true;

                /// <summary>
                /// The value indicating if delete button is enabled for actions formatter.
                /// </summary>
                public const bool DeleteButton = true;

                /// <summary>
                /// The value indicating if form editing should be used instead of inline editing for actions formatter
                /// </summary>
                public const bool UseFormEditing = false;

                /// <summary>
                /// The value indicating whether to show the label in jQuery UI Button widget.
                /// </summary>
                public const bool Text = true;
            }

            /// <summary>
            /// Sorting options.
            /// </summary>
            public static class Sorting
            {
                /// <summary>
                /// The flag indicating if column is sortable.
                /// </summary>
                public const bool Sortable = true;

                /// <summary>
                /// The sorting order for first column sorting
                /// </summary>
                public const JqGridSortingOrders InitialOrder = JqGridSortingOrders.Asc;

                /// <summary>
                /// The the type of the column for appropriate sorting when datatype is local.
                /// </summary>
                public const JqGridSortTypes Type = JqGridSortTypes.Text;
            }

            /// <summary>
            /// The alignment of the cell in the grid body layer.
            /// </summary>
            public const JqGridAlignments Alignment = JqGridAlignments.Left;

            /// <summary>
            /// The value which defines if internal recalculation of the width of the column is disabled.
            /// </summary>
            public const bool Fixed = false;

            /// <summary>
            /// The value indicating if column shouldn't scroll out of view when user is moving horizontally across the grid.
            /// </summary>
            public const bool Frozen = false;

            /// <summary>
            /// The value which defines if column will appear in the modal dialog where users can choose which columns to show or hide.
            /// </summary>
            public const bool HideInDialog = false;

            /// <summary>
            /// The value which defines if column can be resized.
            /// </summary>
            public const bool Resizable = true;

            /// <summary>
            /// The grouping summary template.
            /// </summary>
            public const string SummaryTemplate = "{0}";

            /// <summary>
            /// The value which defines if the title should be displayed in the column when user hovers a cell with the mouse.
            /// </summary>
            public const bool Title = true;

            /// <summary>
            /// The initial width in pixels of the column.
            /// </summary>
            public const int Width = 150;

            /// <summary>
            /// The value which defines if the column should appear in view form.
            /// </summary>
            public const bool Viewable = true;
        }

        /// <summary>
        /// The options for jqGrid grouping view.
        /// </summary>
        public static class GroupingView
        {
            /// <summary>
            /// The value indicating if summary row is visible when the group is collapsed.
            /// </summary>
            public const bool SummaryOnHide = false;

            /// <summary>
            /// The value indicating if the group names should be added to request SortingName.
            /// </summary>
            public const bool DataSorted = false;

            /// <summary>
            /// The value indicating if the groups should be initially collapsed.
            /// </summary>
            public const bool Collapse = false;

            /// <summary>
            /// The icon (form UI theme images) that will be used if the group is collapsed.
            /// </summary>
            public const string PlusIcon = "ui-icon-circlesmall-plus";

            /// <summary>
            /// The icon (form UI theme images) that will be used if the group is expanded.
            /// </summary>
            public const string MinusIcon = "ui-icon-circlesmall-minus";
        }

        /// <summary>
        /// Navigator options.
        /// </summary>
        public static class Navigator
        {
            /// <summary>
            /// The initial top position of modal dialog.
            /// </summary>
            public const int Top = 0;

            /// <summary>
            /// The initial left position of modal dialog.
            /// </summary>
            public const int Left = 0;

            /// <summary>
            /// The value indicating if dialog is modal.
            /// </summary>
            public const bool Modal = false;

            /// <summary>
            /// The value indicating if dialog is dragable.
            /// </summary>
            public const bool Dragable = true;

            /// <summary>
            /// The value indicating if dialog is resizable.
            /// </summary>
            public const bool Resizable = true;

            /// <summary>
            /// The value indicating if jqModel plugin should be used for creating dialogs.
            /// </summary>
            public const bool UseJqModal = true;

            /// <summary>
            /// The value indicating if modal window can be closed with ESC key.
            /// </summary>
            public const bool CloseOnEscape = false;

            /// <summary>
            /// The value controling overlay in the grid.
            /// </summary>
            public const int Overlay = 30;

            /// <summary>
            /// The request method.
            /// </summary>
            public const JqGridMethodTypes MethodType = JqGridMethodTypes.Post;

            /// <summary>
            /// The value indicating if grid should be reloaded after submiting.
            /// </summary>
            public const bool ReloadAfterSubmit = true;

            /// <summary>
            /// The width of delete action confirmation dialog.
            /// </summary>
            public const int DeleteActionWidth = 240;

            /// <summary>
            /// The width of edit action confirmation dialog.
            /// </summary>
            public const int EditActionWidth = 300;

            /// <summary>
            /// The value indicating if the pager buttons should appear on the form.
            /// </summary>
            public const bool ViewPagerButtons = true;

            /// <summary>
            /// The value indicating if the form should be recreated every time the dialog is activeted with the new options from colModel.
            /// </summary>
            public const bool RecreateForm = false;

            /// <summary>
            /// The value indicating where the row just added should be placed.
            /// </summary>
            public const JqGridNewRowPositions AddedRowPosition = JqGridNewRowPositions.First;

            /// <summary>
            /// The value indicating if add action dialog should be cleared after submiting.
            /// </summary>
            public const bool ClearAfterAdd = true;

            /// <summary>
            /// The value indicating if add action dialog should be closed after submiting.
            /// </summary>
            public const bool CloseAfterAdd = false;

            /// <summary>
            /// The value indicating if edit action dialog should be closed after submiting.
            /// </summary>
            public const bool CloseAfterEdit = false;

            /// <summary>
            /// The value indicating if the user should confirm the changes uppon saving or cancel them.
            /// </summary>
            public const bool CheckOnSubmit = false;

            /// <summary>
            /// The value indicating if the user should be prompted when leaving unsaved changes.
            /// </summary>
            public const bool CheckOnUpdate = false;

            /// <summary>
            /// The value indicating if the form can be saved by pressing a key.
            /// </summary>
            public const bool SaveKeyEnabled = false;

            /// <summary>
            /// The key for saving.
            /// </summary>
            public const char SaveKey = (char)13;

            /// <summary>
            /// The value indicating if user can use [Enter] key to save and [Esc] key to cancel in Inline Navigator.
            /// </summary>
            public const bool InlineActionKeys = false;

            /// <summary>
            /// The value indicating if the row should be restored in case of error while saving to the server in Inline Navigator.
            /// </summary>
            public const bool InlineActionRestoreAfterError = true;

            /// <summary>
            /// The request method for Inline Navigator.
            /// </summary>
            public const JqGridMethodTypes InlineActionMethodType = JqGridMethodTypes.Post;

            /// <summary>
            /// Keyboard navigation options.
            /// </summary>
            public static class KeyboardNavigation
            {
                /// <summary>
                /// The value indicating if keyboard navigation is enabled.
                /// </summary>
                public const bool Enabled = false;

                /// <summary>
                /// The key for "record up".
                /// </summary>
                public const char RecordUp = (char)38;

                /// <summary>
                /// The key for "record down".
                /// </summary>
                public const char RecordDown = (char)40;
            }
        }

        /// <summary>
        /// The value indicating if the grouping is enabled.
        /// </summary>
        public const bool GroupingEnabled = false;

        /// <summary>
        /// The class that is used for alternate rows.
        /// </summary>
        public const string AltClass = "ui-priority-secondary";

        /// <summary>
        /// The padding plus border width of the cell.
        /// </summary>
        public const int CellLayout = 5;

        /// <summary>
        /// The type of information to expect to represent data in the grid.
        /// </summary>
        public const JqGridDataTypes DataType = JqGridDataTypes.Xml;

        /// <summary>
        /// The ISO date format.
        /// </summary>
        public const string DateFormat = "Y-m-d";

        /// <summary>
        /// The value which defines if dynamic scrolling is enabled.
        /// </summary>
        public const JqGridDynamicScrollingModes DynamicScrollingMode = JqGridDynamicScrollingModes.Disabled;

        /// <summary>
        /// The timeout (in miliseconds) if DynamicScrollingMode is set to JqGridDynamicScrollingModes.HoldVisibleRows.
        /// </summary>
        public const int DynamicScrollingTimeout = 200;

        /// <summary>
        /// The information to be displayed when the returned (or the current) number of records is zero.
        /// </summary>
        public const string EmptyRecords = "No records to view";

        /// <summary>
        /// The value which defines whether the tree is expanded and/or collapsed when user clicks on the text of the expanded column, not only on the image.
        /// </summary>
        public const bool ExpandColumnClick = true;

        /// <summary>
        /// The value indicating if the footer table (with one row) will be placed below the grid records and above the pager.
        /// </summary>
        public const bool FooterEnabled = false;

        /// <summary>
        /// The default height of the grid (100%).
        /// </summary>
        public const string Height = "100%";

        /// <summary>
        /// The type of request to make.
        /// </summary>
        public const JqGridMethodTypes MethodType = JqGridMethodTypes.Get;

        /// <summary>
        /// The value indicating if pager bar should be used to navigate through the records.
        /// </summary>
        public const bool Pager = false;

        /// <summary>
        /// The number of records which should be displayed in the grid.
        /// </summary>
        public const int RowsNumber = 20;

        /// <summary>
        /// The initial sorting order.
        /// </summary>
        public const JqGridSortingOrders SortingOrder = JqGridSortingOrders.Asc;

        /// <summary>
        /// The width of subgrid expand/colapse column.
        /// </summary>
        public const int SubgridColumnWidth = 20;

        /// <summary>
        /// The value which defines if subgrid is enabled.
        /// </summary>
        public const bool SubgridEnabled = false;

        /// <summary>
        /// The value which defines if TreeGrid is enabled.
        /// </summary>
        public const bool TreeGridEnabled = false;

        /// <summary>
        /// The model for TreeGrid.
        /// </summary>
        public const JqGridTreeGridModels TreeGridModel = JqGridTreeGridModels.Nested;

        /// <summary>
        /// The value indicating if the values from user data should be placed on footer.
        /// </summary>
        public const bool UserDataOnFooter = false;

        /// <summary>
        /// The value indicating if grid should display the beginning and ending record number out of the total number of records in the query
        /// </summary>
        public const bool ViewRecords = false;
    }
}
