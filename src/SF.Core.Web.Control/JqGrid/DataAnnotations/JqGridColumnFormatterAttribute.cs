using System;
using SF.Web.Control.JqGrid.Infrastructure.Options.ColumnModel;

namespace SF.Web.Control.JqGrid.DataAnnotations
{
    /// <summary>
    /// Specifies the custom formatter for column
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class JqGridColumnFormatterAttribute : Attribute
    {
        #region Properties
        /// <summary>
        /// Gets the predefined formatter type ('' delimited string) or custom JavaScript formatting function name.
        /// </summary>
        public string Formatter { get; private set; }

        /// <summary>
        /// Gets the options for predefined formatter (every predefined formatter uses only a subset of all options), which are overwriting the defaults from the language file.
        /// </summary>
        public JqGridColumnFormatterOptions FormatterOptions { get; private set; }

        /// <summary>
        /// Gets or sets the custom function to "unformat" a value of the cell when used in editing or client-side sorting
        /// </summary>
        public string UnFormatter { get; set; }

        /// <summary>
        /// Gets or sets the decimal places.
        /// </summary>
        public int DecimalPlaces
        {
            get { return FormatterOptions.DecimalPlaces; }
            set { FormatterOptions.DecimalPlaces = value; }
        }

        /// <summary>
        /// Gets or sets the decimal separator.
        /// </summary>
        public string DecimalSeparator
        {
            get { return FormatterOptions.DecimalSeparator; }
            set { FormatterOptions.DecimalSeparator = value; }
        }

        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        public string DefaultValue
        {
            get { return FormatterOptions.DefaultValue; }
            set { FormatterOptions.DefaultValue = value; }
        }

        /// <summary>
        /// Gets or sets the value indicating if checkbox is disabled.
        /// </summary>
        public bool Disabled
        {
            get { return FormatterOptions.Disabled; }
            set { FormatterOptions.Disabled = value; }
        }

        /// <summary>
        /// Gets or sets the currency prefix.
        /// </summary>
        public string Prefix
        {
            get { return FormatterOptions.Prefix; }
            set { FormatterOptions.Prefix = value; }
        }

        /// <summary>
        /// Gets or sets the currency suffix.
        /// </summary>
        public string Suffix
        {
            get { return FormatterOptions.Suffix; }
            set { FormatterOptions.Suffix = value; }
        }

        /// <summary>
        /// Gets or sets the date source format.
        /// </summary>
        public string SourceFormat
        {
            get { return FormatterOptions.SourceFormat; }
            set { FormatterOptions.SourceFormat = value; }
        }

        /// <summary>
        /// Gets or sets the date output format.
        /// </summary>
        public string OutputFormat
        {
            get { return FormatterOptions.OutputFormat; }
            set { FormatterOptions.OutputFormat = value; }
        }

        /// <summary>
        /// Gets or sets the link for showlink formatter.
        /// </summary>
        public string BaseLinkUrl
        {
            get { return FormatterOptions.BaseLinkUrl; }
            set { FormatterOptions.BaseLinkUrl = value; }
        }

        /// <summary>
        /// Gets or sets the additional value which is added after the BaseLinkUrl.
        /// </summary>
        public string ShowAction
        {
            get { return FormatterOptions.ShowAction; }
            set { FormatterOptions.ShowAction = value; }
        }

        /// <summary>
        /// Gets or sets the additional parameter which can be added after the IdName property.
        /// </summary>
        public string AddParam
        {
            get { return FormatterOptions.AddParam; }
            set { FormatterOptions.AddParam = value; }
        }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        public string Target
        {
            get { return FormatterOptions.Target; }
            set { FormatterOptions.Target = value; }
        }

        /// <summary>
        /// Gets or sets the first parameter that is added after the ShowAction.
        /// </summary>
        public string IdName
        {
            get { return FormatterOptions.IdName; }
            set { FormatterOptions.IdName = value; }
        }

        /// <summary>
        /// Gets or sets the thousands separator.
        /// </summary>
        public string ThousandsSeparator
        {
            get { return FormatterOptions.ThousandsSeparator; }
            set { FormatterOptions.ThousandsSeparator = value; }
        }

        /// <summary>
        /// Gets or sets the primary icon class (form UI theme icons) for jQuery UI Button widget.
        /// </summary>
        public string PrimaryIcon
        {
            get { return FormatterOptions.PrimaryIcon; }
            set { FormatterOptions.PrimaryIcon = value; }
        }

        /// <summary>
        /// Gets or sets the secondary icon class (form UI theme icons) for jQuery UI Button widget.
        /// </summary>
        public string SecondaryIcon
        {
            get { return FormatterOptions.SecondaryIcon; }
            set { FormatterOptions.SecondaryIcon = value; }
        }

        /// <summary>
        /// Gets or sets the text to show in the button for jQuery UI Button widget.
        /// </summary>
        public string Label
        {
            get { return FormatterOptions.Label; }
            set { FormatterOptions.Label = value; }
        }

        /// <summary>
        /// Gets or sets the value whether to show the label in jQuery UI Button widget.
        /// </summary>
        public bool Text
        {
            get { return FormatterOptions.Text; }
            set { FormatterOptions.Text = value; }
        }

        /// <summary>
        /// Gets or sets the click handler (JavaScript) for jQuery UI Button widget.
        /// </summary>
        public string OnClick
        {
            get { return FormatterOptions.OnClick; }
            set { FormatterOptions.OnClick = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridColumnFormatterAttribute class.
        /// </summary>
        /// <param name="formatter">The predefined formatter type ('' delimited string) or custom JavaScript formatting function name.</param>
        public JqGridColumnFormatterAttribute(string formatter)
        {
            if (String.IsNullOrWhiteSpace(formatter))
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            Formatter = formatter;
            FormatterOptions = new JqGridColumnFormatterOptions(formatter);
        }
        #endregion
    }
}
