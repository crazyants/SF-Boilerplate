using SF.Web.Control.JqGrid.Infrastructure.Enums;
using SF.Web.Control.JqGrid.Infrastructure.Options;
using SF.Web.Control.JqGrid.Core.Request.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using System;

namespace SF.Web.Control.JqGrid.Core.Request
{
    /// <summary>
    /// Class which represents request from jqGrid.
    /// </summary>
    [ModelBinder(BinderType = typeof(JqGridRequestModelBinder))]
    public class JqGridRequest
    {
        #region Fields
        private static JqGridParametersNames _parametersNames;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the customized names for jqGrid request parameters (will be also use as defaults for JqGridHelper).
        /// </summary>
        public static JqGridParametersNames ParametersNames
        {
            get { return _parametersNames; }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _parametersNames = value;
            }
        }

        /// <summary>
        /// Gets the value indicating searching.
        /// </summary>
        public bool Searching { get; set; }

        /// <summary>
        /// Gets the searching filter (single searching). 
        /// </summary>
        public JqGridRequestSearchingFilter SearchingFilter { get; set; }

        /// <summary>
        /// Gets the searching filters (advanced searching or toolbar searching with JqGridFilterToolbarOptions.StringResult = true). 
        /// </summary>
        public JqGridRequestSearchingFilters SearchingFilters { get; set; }

        /// <summary>
        /// Gets the sorting column name.
        /// </summary>
        public string SortingName { get; set; }

        /// <summary>
        /// Gets the sorting order
        /// </summary>
        public JqGridSortingOrders SortingOrder { get; set; }

        /// <summary>
        /// Gets the index (zero based) of page to return
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Gets the number of pages to return
        /// </summary>
        public int PagesCount { get; set; }

        /// <summary>
        /// Gets the number of rows to return
        /// </summary>
        public int RecordsCount { get; set; }
        #endregion

        #region Constructors
        static JqGridRequest()
        {
            ParametersNames = new JqGridParametersNames();
        }
        #endregion
    }
}
