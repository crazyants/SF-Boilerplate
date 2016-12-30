using SF.Web.Control.JqGrid.Infrastructure.Constants;

namespace SF.Web.Control.JqGrid.Infrastructure.Options.Navigator
{
    /// <summary>
    /// Class which represents options for jqGrid Navigator action.
    /// </summary>
    public abstract class JqGridNavigatorActionOptions
    {
        #region Properties
        /// <summary>
        /// Gets or sets the initial top position of modal dialog.
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// Gets or sets the initial left position of modal dialog.
        /// </summary>
        public int Left { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if dialog is modal (requires jqModal plugin).
        /// </summary>
        public bool Modal { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if dialog is dragable (requires jqDnR plugin or dragable widget from jQuery UI).
        /// </summary>
        public bool Dragable { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if dialog is resizable (requires jqDnR plugin or resizable widget from jQuery UI).
        /// </summary>
        public bool Resizable { get; set; }

        /// <summary>
        /// Gets or sets the width of confirmation dialog in pixels (default 'auto').
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the width of the scrolling content.
        /// </summary>
        public int? DataWidth { get; set; }

        /// <summary>
        /// Gets or sets the entry height of confirmation dialog.
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// Gets or sets the height of the scrolling content (i.e between the modal header and modal footer).
        /// </summary>
        public int? DataHeight { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if jqModel plugin should be used for creating dialogs otherwise jqGrid uses its internal function.
        /// </summary>
        public bool UseJqModal { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if modal window can be closed with ESC key.
        /// </summary>
        public bool CloseOnEscape { get; set; }

        /// <summary>
        /// Gets or sets the function for event which is raised just before closing the form.
        /// </summary>
        public string OnClose { get; set; }

        /// <summary>
        /// Gets or sets the value controling overlay in the grid.
        /// </summary>
        public int Overlay { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridNavigatorActionOptions class.
        /// </summary>
        protected JqGridNavigatorActionOptions()
        {
            Top = JqGridOptionsDefaults.Navigator.Top;
            Left = JqGridOptionsDefaults.Navigator.Left;
            Modal = JqGridOptionsDefaults.Navigator.Modal;
            Dragable = JqGridOptionsDefaults.Navigator.Dragable;
            Resizable = JqGridOptionsDefaults.Navigator.Resizable;
            UseJqModal = JqGridOptionsDefaults.Navigator.UseJqModal;
            CloseOnEscape = JqGridOptionsDefaults.Navigator.CloseOnEscape;
            Overlay = JqGridOptionsDefaults.Navigator.Overlay;

            OnClose = null;
        }
        #endregion
    }
}
