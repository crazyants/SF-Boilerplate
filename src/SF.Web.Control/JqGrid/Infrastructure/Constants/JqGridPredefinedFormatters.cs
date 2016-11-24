namespace SF.Web.Control.JqGrid.Infrastructure.Constants
{
    /// <summary>
    /// Contains predefined jqGrid formatters names, delimited with ''
    /// </summary>
    public static class JqGridPredefinedFormatters
    {
        /// <summary>
        /// Predefined integer formatter.
        /// </summary>
        public const string Integer = "'integer'";

        /// <summary>
        /// Predefined number formatter.
        /// </summary>
        public const string Number = "'number'";

        /// <summary>
        /// Predefined currency formatter.
        /// </summary>
        public const string Currency = "'currency'";

        /// <summary>
        /// Predefined date formatter.
        /// </summary>
        public const string Date = "'date'";

        /// <summary>
        /// Predefined email formatter.
        /// </summary>
        public const string Email = "'email'";

        /// <summary>
        /// Predefined link formatter.
        /// </summary>
        public const string Link = "'link'";

        /// <summary>
        /// Predefined showlink formatter.
        /// </summary>
        public const string ShowLink = "'showlink'";

        /// <summary>
        /// Predefined checkbox formatter.
        /// </summary>
        public const string CheckBox = "'checkbox'";

        /// <summary>
        /// Predefined select formatter.
        /// </summary>
        public const string Select = "'select'";

        /// <summary>
        /// Predefined jQuery UI Button formatter.
        /// </summary>
        /// <remarks>The column value can be accessed in function passed to OnClick property from custom attribute: $(this).attr('data-cell-value');.</remarks>
        public const string JQueryUIButton = "'JQueryUIButton'";

        /// <summary>
        /// Predefined actions formatter.
        /// </summary>
        public const string Actions = "'actions'";
    }
}
