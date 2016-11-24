using SF.Web.Control.JqGrid.Infrastructure.Constants;
using SF.Web.Control.JqGrid.Infrastructure.Enums;

namespace SF.Web.Control.JqGrid.Infrastructure.Options
{
    /// <summary>
    /// Class which represents options for jqGrid grouping view.
    /// </summary>
    public class JqGridGroupingView
    {
        #region Properties
        /// <summary>
        /// Gets or sets the array of names from ColumnsModels on which jqGrid will group. Each value represents separate level (jqGrid supports only one level at this time).
        /// </summary>
        public string[] Fields { get; set; }

        /// <summary>
        /// Gets or sets the array of initial sort orders for every level.
        /// </summary>
        public JqGridSortingOrders[] Orders { get; set; }

        /// <summary>
        /// Gets or sets the array of grouping headers texts for every level.
        /// </summary>
        public string[] Texts { get; set; }

        /// <summary>
        /// Gets or sets the array of values for every level indicating if the column on which we group is visible.
        /// </summary>
        public bool[] ColumnShow { get; set; }

        /// <summary>
        /// Gets or sets the values for every level indicating if the summary (footer) row for that level is enabled.
        /// </summary>
        public bool[] Summary { get; set; }

        /// <summary>
        /// Gets or sets the array of callbacks which allow "not exact" grouping.
        /// </summary>
        public string[] IsInTheSameGroupCallbacks { get; set; }

        /// <summary>
        /// Gets or sets the array of callbacks for formatting group display values.
        /// </summary>
        public string[] FormatDisplayFieldCallbacks { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if summary row is visible when the group is collapsed.
        /// </summary>
        public bool SummaryOnHide { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the group names should be added to request SortingName
        /// </summary>
        public bool DataSorted { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the groups should be initially collapsed.
        /// </summary>
        public bool Collapse { get; set; }

        /// <summary>
        /// Gets or sets the icon (form UI theme images) that will be used if the group is collapsed.
        /// </summary>
        public string PlusIcon { get; set; }

        /// <summary>
        /// Gets or sets the icon (form UI theme images) that will be used if the group is expanded.
        /// </summary>
        public string MinusIcon { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridGroupingView class.
        /// </summary>
        public JqGridGroupingView()
        {
            Fields = null;
            Orders = null;
            Texts = null;
            ColumnShow = null;
            Summary = null;
            IsInTheSameGroupCallbacks = null;
            FormatDisplayFieldCallbacks = null;
            SummaryOnHide = JqGridOptionsDefaults.GroupingView.SummaryOnHide;
            DataSorted = JqGridOptionsDefaults.GroupingView.DataSorted;
            Collapse = JqGridOptionsDefaults.GroupingView.Collapse;
            PlusIcon = JqGridOptionsDefaults.GroupingView.PlusIcon;
            MinusIcon = JqGridOptionsDefaults.GroupingView.MinusIcon;
        }
        #endregion
    }
}
