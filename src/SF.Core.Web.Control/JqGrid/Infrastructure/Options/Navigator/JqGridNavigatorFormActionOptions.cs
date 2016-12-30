namespace SF.Web.Control.JqGrid.Infrastructure.Options.Navigator
{
    /// <summary>
    /// Class which represents options for jqGrid Navigator form editing action.
    /// </summary>
    public abstract class JqGridNavigatorFormActionOptions : JqGridNavigatorActionOptions
    {
        #region Properties
        /// <summary>
        /// Gets or sets the function for event which is raised before initializing the new form data.
        /// </summary>
        public string BeforeInitData { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised before showing the form with the new data.
        /// </summary>
        public string BeforeShowForm { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridNavigatorFormActionOptions class.
        /// </summary>
        protected JqGridNavigatorFormActionOptions()
        {
            BeforeInitData = null;
            BeforeShowForm = null;
        }
        #endregion
    }
}
