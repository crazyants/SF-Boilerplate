using SF.Web.Control.JqGrid.Infrastructure.Constants;

namespace SF.Web.Control.JqGrid.Infrastructure.Options.Navigator
{
    /// <summary>
    /// Class which represents options for form editing navigation keys.
    /// </summary>
    public class JqGridFormKeyboardNavigation
    {
        #region Properties
        /// <summary>
        /// Gets or sets the value indicating if keyboard navigation is enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the key for "record up".
        /// </summary>
        public char RecordUp { get; set; }

        /// <summary>
        /// Gets or sets the key for "record down".
        /// </summary>
        public char RecordDown { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridFormKeyboardNavigation class.
        /// </summary>
        public JqGridFormKeyboardNavigation()
        {
            Enabled = JqGridOptionsDefaults.Navigator.KeyboardNavigation.Enabled;
            RecordUp = JqGridOptionsDefaults.Navigator.KeyboardNavigation.RecordUp;
            RecordDown = JqGridOptionsDefaults.Navigator.KeyboardNavigation.RecordDown;
        }
        #endregion

        #region Methods
        internal bool IsDefault()
        {
            return ((Enabled == JqGridOptionsDefaults.Navigator.KeyboardNavigation.Enabled) && (RecordUp == JqGridOptionsDefaults.Navigator.KeyboardNavigation.RecordUp) && (RecordDown == JqGridOptionsDefaults.Navigator.KeyboardNavigation.RecordDown));
        }
        #endregion
    }
}
