using SF.Web.Control.JqGrid.Infrastructure.Constants;

namespace SF.Web.Control.JqGrid.Infrastructure.Options.Navigator
{
    /// <summary>
    /// Class which represents options for jqGrid Navigator delete action.
    /// </summary>
    public class JqGridNavigatorDeleteActionOptions : JqGridNavigatorModifyActionOptions
    {
        #region Properties
        /// <summary>
        /// Gets or sets the icon for the submit button.
        /// </summary>
        public JqGridFormButtonIcon DeleteButtonIcon { get; set; }

        /// <summary>
        /// Gets or sets the icon for the cancel button.
        /// </summary>
        public JqGridFormButtonIcon CancelButtonIcon { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridNavigatorDeleteActionOptions class.
        /// </summary>
        public JqGridNavigatorDeleteActionOptions()
        {
            Width = JqGridOptionsDefaults.Navigator.DeleteActionWidth;
            DeleteButtonIcon = JqGridFormButtonIcon.DeleteIcon;
            CancelButtonIcon = JqGridFormButtonIcon.CancelIcon;
        }
        #endregion
    }
}
