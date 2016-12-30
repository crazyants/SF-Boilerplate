using System;
using SF.Web.Control.JqGrid.Infrastructure.Enums;
using SF.Web.Control.JqGrid.Infrastructure.Constants;

namespace SF.Web.Control.JqGrid.DataAnnotations
{
    /// <summary>
    /// Specifies the grouping summary for column.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class JqGridColumnSummaryAttribute : Attribute
    {
        #region Properties
        /// <summary>
        /// Gets the grouping summary type.
        /// </summary>
        public JqGridColumnSummaryTypes Type { get; private set; }

        /// <summary>
        /// Gets or sets the grouping summary template.
        /// </summary>
        public string Template { get; set; }

        /// <summary>
        /// Gets or sets the grouping summary function for custom type.
        /// </summary>
        public string Function { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridColumnGroupingSummaryAttribute class.
        /// </summary>
        /// <param name="type">Type of summary</param>
        public JqGridColumnSummaryAttribute(JqGridColumnSummaryTypes type)
        {
            Type = type;
            Template = JqGridOptionsDefaults.ColumnModel.SummaryTemplate;
            Function = null;
        }
        #endregion
    }
}
