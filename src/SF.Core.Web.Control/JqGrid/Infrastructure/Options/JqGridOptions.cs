using System;
using System.Collections.Generic;
using SF.Web.Control.JqGrid.Infrastructure.Enums;
using SF.Web.Control.JqGrid.Infrastructure.Options.ColumnModel;
using SF.Web.Control.JqGrid.Infrastructure.Constants;
using SF.Web.Control.JqGrid.Infrastructure.Options.Subgrid;

namespace SF.Web.Control.JqGrid.Infrastructure.Options
{
    /// <summary>
    /// jqGrid options
    /// </summary>
    public class JqGridOptions
    {
        #region Fields
        private readonly IList<JqGridColumnModel> _columnsModels = new List<JqGridColumnModel>();
        private readonly IList<string> _columnsNames = new List<string>();
        #endregion

        #region Properties
        /// <summary>
        /// Gets the grid identifier which will be used for table (id='{0}'), pager div (id='{0}Pager') and in JavaScript.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets or sets the caption for the grid.
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Gets the list of columns parameters descriptions.
        /// </summary>
        public IReadOnlyList<JqGridColumnModel> ColumnsModels { get { return (IReadOnlyList<JqGridColumnModel>)_columnsModels; } }

        /// <summary>
        /// Gets the list of columns names.
        /// </summary>
        public IReadOnlyList<string> ColumnsNames { get { return (IReadOnlyList<string>)_columnsNames; } }

        /// <summary>
        /// Gets or sets the string of data which will be used when DataType is set to JqGridDataTypes.XmlString or JqGridDataTypes.JsonString (default null).
        /// </summary>
        public string DataString { get; set; }

        /// <summary>
        /// Gets or sets the type of information to expect to represent data in the grid (default JqGridDataTypes.Xml).
        /// </summary>
        public JqGridDataTypes DataType { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if dynamic scrolling is enabled.
        /// </summary>
        public JqGridDynamicScrollingModes DynamicScrollingMode { get; set; }

        /// <summary>
        /// Gets or sets the timeout (in miliseconds) if DynamicScrollingMode is set to JqGridDynamicScrollingModes.HoldVisibleRows
        /// </summary>
        public int DynamicScrollingTimeout { get; set; }

        /// <summary>
        /// Gets or sets the value which defines whether the tree is expanded and/or collapsed when user clicks on the text of the expanded column, not only on the image.
        /// </summary>
        public bool ExpandColumnClick { get; set; }

        /// <summary>
        /// Gets or sets the name of column which should be used to expand the tree grid.
        /// </summary>
        public string ExpandColumn { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the footer table (with one row) will be placed below the grid records and above the pager. The number of columns equal of these from ColumnsModels.
        /// </summary>
        public bool FooterEnabled { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the grouping is enabled.
        /// </summary>
        public bool GroupingEnabled { get; set; }

        /// <summary>
        /// Gets or sets the grouping view options.
        /// </summary>
        public JqGridGroupingView GroupingView { get; set; }

        /// <summary>
        /// Gets or sets the height of the grid in pixels (default '100%').
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// Gets or sets the JSON reader for the grid.
        /// </summary>
        public JqGridJsonReader JsonReader { get; set; }

        /// <summary>
        /// Gets or sets the type of request to make (default JqGridMethodTypes.Get).
        /// </summary>
        public JqGridMethodTypes MethodType { get; set; }

        /// <summary>
        /// Gets or sets if grid should use a pager bar to navigate through the records (default: false).
        /// </summary>
        public bool Pager { get; set; }

        /// <summary>
        /// Gets or sets customized names for jqGrid request parameters.
        /// </summary>
        public JqGridParametersNames ParametersNames { get; set; }

        /// <summary>
        /// Gets or sets an array to construct a select box element in the pager in which user can change the number of the visible rows.
        /// </summary>
        public IList<int> RowsList { get; set; }

        /// <summary>
        /// Gets or sets how many records should be displayed in the grid (default 20).
        /// </summary>
        public int RowsNumber { get; set; }

        /// <summary>
        /// Gets or sets the initial sorting column index, when  using data returned from server
        /// </summary>
        public string SortingName { get; set; }

        /// <summary>
        /// Gets or sets the initial sorting order, when  using data returned from server (default JqGridSortingOrders.Asc)
        /// </summary>
        public JqGridSortingOrders SortingOrder { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if subgrid is enabled.
        /// </summary>
        public bool SubgridEnabled { get; set; }

        /// <summary>
        /// Gets or sets the subgrid model.
        /// </summary>
        public JqGridSubgridModel SubgridModel { get; set; }

        /// <summary>
        /// Gets or sets the url for subgrid data requests.
        /// </summary>
        public string SubgridUrl { get; set; }

        /// <summary>
        /// Gets or sets the width of subgrid expand/colapse column.
        /// </summary>
        public int SubgridColumnWidth { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised just before expanding the subgrid.
        /// </summary>
        public string SubGridBeforeExpand { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised when the user clicks on the plus icon of the grid.
        /// </summary>
        public string SubGridRowExpanded { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised when the user clicks on the minus icon of the grid.
        /// </summary>
        public string SubGridRowColapsed { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if TreeGrid is enabled.
        /// </summary>
        public bool TreeGridEnabled { get; set; }

        /// <summary>
        /// Gets or sets the model for TreeGrid.
        /// </summary>
        public JqGridTreeGridModels TreeGridModel { get; set; }

        /// <summary>
        /// Gets or sets the url for data requests
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the values from user data should be placed on footer.
        /// </summary>
        public bool UserDataOnFooter { get; set; }

        /// <summary>
        /// Gets or sets if grid should display the beginning and ending record number out of the total number of records in the query (default: false)
        /// </summary>
        public bool ViewRecords { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridOptions class.
        /// </summary>
        /// <param name="id">Identifier, which will be used for table (id='{0}'), pager div (id='{0}Pager'), filter grid div (id='{0}Search') and in JavaScript.</param>
        public JqGridOptions(string id)
        {
            Id = id;
            Caption = null;
            DataString = null;
            DataType = JqGridOptionsDefaults.DataType;
            DynamicScrollingMode = JqGridOptionsDefaults.DynamicScrollingMode;
            DynamicScrollingTimeout = JqGridOptionsDefaults.DynamicScrollingTimeout;
            ExpandColumn = null;
            ExpandColumnClick = JqGridOptionsDefaults.ExpandColumnClick;
            FooterEnabled = JqGridOptionsDefaults.FooterEnabled;
            GroupingEnabled = JqGridOptionsDefaults.GroupingEnabled;
            GroupingView = null;
            Height = null;
            JsonReader = null;
            MethodType = JqGridOptionsDefaults.MethodType;
            Pager = JqGridOptionsDefaults.Pager;
            ParametersNames = null;
            RowsList = null;
            RowsNumber = JqGridOptionsDefaults.RowsNumber;
            SortingName = null;
            SortingOrder = JqGridOptionsDefaults.SortingOrder;
            SubgridColumnWidth = JqGridOptionsDefaults.SubgridColumnWidth;
            SubgridEnabled = JqGridOptionsDefaults.SubgridEnabled;
            SubgridModel = null;
            SubgridUrl = null;
            SubGridBeforeExpand = null;
            SubGridRowColapsed = null;
            SubGridRowExpanded = null;
            TreeGridEnabled = JqGridOptionsDefaults.TreeGridEnabled;
            TreeGridModel = JqGridOptionsDefaults.TreeGridModel;
            Url = null;
            UserDataOnFooter = JqGridOptionsDefaults.UserDataOnFooter;
            ViewRecords = JqGridOptionsDefaults.ViewRecords;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds column to options.
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <param name="columnModel">The column model.</param>
        /// <exception cref="System.ArgumentNullException">
        /// The column name or model haven't been provided. 
        /// </exception>
        public void AddColumn(string columnName, JqGridColumnModel columnModel)
        {
            if (String.IsNullOrWhiteSpace(columnName))
            {
                throw new ArgumentNullException(nameof(columnName));
            }

            if (columnModel == null)
            {
                throw new ArgumentNullException(nameof(columnModel));
            }

            _columnsModels.Add(columnModel);
            _columnsNames.Add(columnName);
        }
        #endregion
    }
}
