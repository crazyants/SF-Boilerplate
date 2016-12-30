using SF.Web.Control.JqGrid.Infrastructure.Enums;
using System.Collections.Generic;

namespace SF.Web.Control.JqGrid.Core.Request
{
    /// <summary>
    /// Class which represents filters in request from jqGrid.
    /// </summary>
    public class JqGridRequestSearchingFilters
    {
        #region Properties
        /// <summary>
        /// Gets the operator grouping the filters.
        /// </summary>
        public JqGridSearchGroupingOperators GroupingOperator { get; set; }

        /// <summary>
        /// Gets the list of filters.
        /// </summary>
        public List<JqGridRequestSearchingFilter> Filters { get; set; }

        /// <summary>
        /// Gets the list of filters sub groups.
        /// </summary>
        public List<JqGridRequestSearchingFilters> Groups { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridRequestSearchingFilters class.
        /// </summary>
        public JqGridRequestSearchingFilters()
        {
            Filters = new List<JqGridRequestSearchingFilter>();
            Groups = new List<JqGridRequestSearchingFilters>();
        }
        #endregion
    }
}
