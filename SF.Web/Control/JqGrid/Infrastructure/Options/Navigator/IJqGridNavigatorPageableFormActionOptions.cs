namespace SF.Web.Control.JqGrid.Infrastructure.Options.Navigator
{
    /// <summary>
    /// Defines the properties for jqGrid Navigator pageable form editing action.
    /// </summary>
    public interface IJqGridNavigatorPageableFormActionOptions
    {
        #region Properties
        /// <summary>
        /// Gets or sets the options for keyboard navigation.
        /// </summary>
        JqGridFormKeyboardNavigation NavigationKeys { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the pager buttons should appear on the form.
        /// </summary>
        bool ViewPagerButtons { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the form should be recreated every time the dialog is activeted with the new options from colModel (if they are changed).
        /// </summary>
        bool RecreateForm { get; set; }
        #endregion
    }
}
