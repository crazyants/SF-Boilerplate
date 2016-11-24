using SF.Web.Control.JqGrid.Infrastructure.Constants;

namespace SF.Web.Control.JqGrid.Infrastructure.Options
{
    /// <summary>
    /// Class which represents names for jqGrid request parameters.
    /// </summary>
    public sealed class JqGridParametersNames
    {
        #region Properties
        /// <summary>
        /// Gets or sets the name for page index parametr.
        /// </summary>
        public string PageIndex { get; set; }

        /// <summary>
        /// Gets or sets the name for records count parametr.
        /// </summary>
        public string RecordsCount { get; set; }

        /// <summary>
        /// Gets or sets the name for sorting name parametr.
        /// </summary>
        public string SortingName { get; set; }

        /// <summary>
        /// Gets or sets the name for sorting order parametr.
        /// </summary>
        public string SortingOrder { get; set; }

        /// <summary>
        /// Gets or sets the name for searching parametr.
        /// </summary>
        public string Searching { get; set; }

        /// <summary>
        /// Gets or sets the name for id parametr.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name for operator parametr.
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// Gets or sets the name for edit operator parametr.
        /// </summary>
        public string EditOperator { get; set; }

        /// <summary>
        /// Gets or sets the name for add operator parametr.
        /// </summary>
        public string AddOperator { get; set; }

        /// <summary>
        /// Gets or sets the name for delete operator parametr.
        /// </summary>
        public string DeleteOperator { get; set; }

        /// <summary>
        /// Gets or sets the name for subgrid id parametr.
        /// </summary>
        public string SubgridId { get; set; }

        /// <summary>
        /// Gets or sets the name for pages count parametr.
        /// </summary>
        public string PagesCount { get; set; }

        /// <summary>
        /// Gets or sets the name for total rows parametr.
        /// </summary>
        public string TotalRows { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridParametersNames class.
        /// </summary>
        public JqGridParametersNames()
        {
            PageIndex = JqGridOptionsDefaults.Request.PageIndex;
            RecordsCount = JqGridOptionsDefaults.Request.RecordsCount;
            SortingName = JqGridOptionsDefaults.Request.SortingName;
            SortingOrder = JqGridOptionsDefaults.Request.SortingOrder;
            Searching = JqGridOptionsDefaults.Request.Searching;
            Id = JqGridOptionsDefaults.Request.Id;
            Operator = JqGridOptionsDefaults.Request.Operator;
            EditOperator = JqGridOptionsDefaults.Request.EditOperator;
            AddOperator = JqGridOptionsDefaults.Request.AddOperator;
            DeleteOperator = JqGridOptionsDefaults.Request.DeleteOperator;
            SubgridId = JqGridOptionsDefaults.Request.SubgridId;
            PagesCount = null;
            TotalRows = JqGridOptionsDefaults.Request.TotalRows;
        }
        #endregion
    }
}
