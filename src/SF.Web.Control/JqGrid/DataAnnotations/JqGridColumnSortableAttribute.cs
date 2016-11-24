using System;
using SF.Web.Control.JqGrid.Infrastructure.Constants;
using SF.Web.Control.JqGrid.Infrastructure.Enums;

namespace SF.Web.Control.JqGrid.DataAnnotations
{
    /// <summary>
    /// Specifies the sorting options for column
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class JqGridColumnSortableAttribute : Attribute
    {
        #region Properties
        /// <summary>
        /// Gets the value defining if this column can be sorted.
        /// </summary>
        public bool Sortable { get; private set; }

        /// <summary>
        /// Gets or sets the type of the column for appropriate sorting when datatype is local.
        /// </summary>
        public JqGridSortTypes SortType { get; set; }

        /// <summary>
        /// Gets or sets the custom sorting function when the SortType is set to JqGridColumnSortTypes.Function.
        /// </summary>
        public string SortFunction { get; set; }

        /// <summary>
        /// Gets or sets the index name for sorting and searching.
        /// </summary>
        public string Index { get; set; }

        /// <summary>
        /// Gets or sets the sorting order for first column sorting.
        /// </summary>
        public JqGridSortingOrders InitialSortingOrder { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridColumnSortingNameAttribute class.
        /// </summary>
        /// <param name="sortable">If this column can be sorted</param>
        public JqGridColumnSortableAttribute(bool sortable)
        {
            Sortable = sortable;
            SortType = JqGridOptionsDefaults.ColumnModel.Sorting.Type;
            SortFunction = String.Empty;
            InitialSortingOrder = JqGridOptionsDefaults.ColumnModel.Sorting.InitialOrder;
        }
        #endregion
    }
}
