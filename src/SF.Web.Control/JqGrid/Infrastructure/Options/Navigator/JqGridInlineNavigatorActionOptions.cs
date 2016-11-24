using SF.Web.Control.JqGrid.Infrastructure.Constants;
using SF.Web.Control.JqGrid.Infrastructure.Enums;

namespace SF.Web.Control.JqGrid.Infrastructure.Options.Navigator
{
    /// <summary>
    /// Class which represents options for jqGrid Inline Navigator action.
    /// </summary>
    public class JqGridInlineNavigatorActionOptions
    {
        #region Properties
        /// <summary>
        /// Gets or sets the value indicating if user can use [Enter] key to save and [Esc] key to cancel.
        /// </summary>
        public bool Keys { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised after successfully accessing the row for editing.
        /// </summary>
        public string OnEditFunction { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised immediately after the successful request to the server.
        /// </summary>
        public string SuccessFunction { get; set; }

        /// <summary>
        /// Gets or sets URL for the request (replaces the EditingUrl from JqGridOptions).
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the extra values that will be posted with row values to the server.
        /// </summary>
        public object ExtraParam { get; set; }

        /// <summary>
        /// Gets or sets the JavaScript which will dynamically generate the extra values that will be posted with row values to the server (this property takes precedence over ExtraParam).
        /// </summary>
        public string ExtraParamScript { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised after the data is saved to the server.
        /// </summary>
        public string AfterSaveFunction { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised when error occurs during saving to the server.
        /// </summary>
        public string ErrorFunction { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised after restoring the row.
        /// </summary>
        public string AfterRestoreFunction { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the row should be restored in case of error while saving to the server.
        /// </summary>
        public bool RestoreAfterError { get; set; }

        /// <summary>
        /// Gets or sets the type or request to make when data is sent to the server.
        /// </summary>
        public JqGridMethodTypes MethodType { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridInlineNavigatorActionOptions class.
        /// </summary>
        public JqGridInlineNavigatorActionOptions()
        {
            Keys = JqGridOptionsDefaults.Navigator.InlineActionKeys;
            RestoreAfterError = JqGridOptionsDefaults.Navigator.InlineActionRestoreAfterError;
            MethodType = JqGridOptionsDefaults.Navigator.InlineActionMethodType;
        }
        #endregion
    }
}
