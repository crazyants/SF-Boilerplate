using SF.Web.Control.JqGrid.Infrastructure.Enums;
using SF.Web.Control.JqGrid.Infrastructure.Constants;

namespace SF.Web.Control.JqGrid.Infrastructure.Options.Navigator
{
    /// <summary>
    /// Class which represents options for jqGrid Navigator form editing add/edit or delete action.
    /// </summary>
    public abstract class JqGridNavigatorModifyActionOptions : JqGridNavigatorFormActionOptions
    {
        #region Properties
        /// <summary>
        /// Gets or sets the url for action requests.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the type of request to make when data is sent to the server.
        /// </summary>
        public JqGridMethodTypes MethodType { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if grid should be reloaded after submiting.
        /// </summary>
        public bool ReloadAfterSubmit { get; set; }

        /// <summary>
        /// Gets or sets an object used to add content to the data posted to the server.
        /// </summary>
        public object ExtraData { get; set; }

        /// <summary>
        /// Gets or sets the JavaScript which will dynamically generate the object which will be used to add content to the data posted to the server (this property takes precedence over ExtraData).
        /// </summary>
        public string ExtraDataScript { get; set; }

        /// <summary>
        /// Gets or sets ajax settings for the request.
        /// </summary>
        public object AjaxOptions { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised after showing the form.
        /// </summary>
        public string AfterShowForm { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised after response has been received from server.
        /// </summary>
        public string AfterSubmit { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised before the data is submitted to the server.
        /// </summary>
        public string BeforeSubmit { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised after the submit button is clicked and the postdata is constructed.
        /// </summary>
        public string OnClickSubmit { get; set; }

        /// <summary>
        /// Gets or sets the function for event which can serialize the data passed to the ajax request from the form.
        /// </summary>
        public string SerializeData { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridNavigatorModifyActionOptions class.
        /// </summary>
        protected JqGridNavigatorModifyActionOptions()
        {
            Url = null;
            MethodType = JqGridOptionsDefaults.Navigator.MethodType;
            ReloadAfterSubmit = JqGridOptionsDefaults.Navigator.ReloadAfterSubmit;
            ExtraData = null;
            ExtraDataScript = null;
            AjaxOptions = null;

            AfterShowForm = null;
            AfterSubmit = null;
            BeforeSubmit = null;
            OnClickSubmit = null;
            SerializeData = null;
        }
        #endregion
    }
}
