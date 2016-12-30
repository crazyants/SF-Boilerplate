using System;
using SF.Web.Control.JqGrid.Infrastructure.Constants;
using SF.Web.Control.JqGrid.Infrastructure.Options.Navigator;

namespace SF.Web.Control.JqGrid.Infrastructure.Options.ColumnModel
{
    /// <summary>
    /// Class which represents options for predefined formatter.
    /// </summary>
    public class JqGridColumnFormatterOptions
    {
        #region Properties
        /// <summary>
        /// Gets or sets the decimal places.
        /// </summary>
        public int DecimalPlaces { get; set; }

        /// <summary>
        /// Gets or sets the decimal separator.
        /// </summary>
        public string DecimalSeparator { get; set; }

        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if checkbox is disabled.
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// Gets or sets the currency prefix.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Gets or sets the currency suffix.
        /// </summary>
        public string Suffix { get; set; }

        /// <summary>
        /// Gets or sets the date source format.
        /// </summary>
        public string SourceFormat { get; set; }

        /// <summary>
        /// Gets or sets the date output format.
        /// </summary>
        public string OutputFormat { get; set; }

        /// <summary>
        /// Gets or sets the link for showlink formatter.
        /// </summary>
        public string BaseLinkUrl { get; set; }

        /// <summary>
        /// Gets or sets the additional value which is added after the BaseLinkUrl.
        /// </summary>
        public string ShowAction { get; set; }

        /// <summary>
        /// Gets or sets the additional parameter which can be added after the IdName property.
        /// </summary>
        public string AddParam { get; set; }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Gets or sets the first parameter that is added after the ShowAction.
        /// </summary>
        public string IdName { get; set; }

        /// <summary>
        /// Gets or sets the thousands separator.
        /// </summary>
        public string ThousandsSeparator { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if edit button is enabled for actions formatter.
        /// </summary>
        public bool EditButton { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if delete button is enabled for actions formatter.
        /// </summary>
        public bool DeleteButton { get; set; }

        /// <summary>
        /// Gets or sets value indicating if form editing should be used instead of inline editing for actions formatter.
        /// </summary>
        public bool UseFormEditing { get; set; }

        /// <summary>
        /// Gets or sets the primary icon class (form UI theme icons) for jQuery UI Button widget.
        /// </summary>
        public string PrimaryIcon { get; set; }

        /// <summary>
        /// Gets or sets the secondary icon class (form UI theme icons) for jQuery UI Button widget.
        /// </summary>
        public string SecondaryIcon { get; set; }

        /// <summary>
        /// Gets or sets the text to show in the button for jQuery UI Button widget.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the value whether to show the label in jQuery UI Button widget.
        /// </summary>
        public bool Text { get; set; }

        /// <summary>
        /// Gets or sets the click handler (JavaScript) for jQuery UI Button widget.
        /// </summary>
        public string OnClick { get; set; }

        /// <summary>
        /// Gets or sets options for inline editing (RestoreAfterError and MethodType options are ignored in this context) for actions formatter.
        /// </summary>
        public JqGridInlineNavigatorActionOptions InlineEditingOptions { get; set; }

        /// <summary>
        /// Gets or sets options for form editing for actions formatter.
        /// </summary>
        public JqGridNavigatorEditActionOptions FormEditingOptions { get; set; }

        /// <summary>
        /// Gets or sets options for deleting for actions formatter.
        /// </summary>
        public JqGridNavigatorDeleteActionOptions DeleteOptions { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes new instance of JqGridColumnFormatterOptions class.
        /// </summary>
        public JqGridColumnFormatterOptions()
        {
            DecimalPlaces = 0;
            DecimalSeparator = String.Empty;
            DefaultValue = String.Empty;
            Disabled = JqGridOptionsDefaults.ColumnModel.Formatter.Disabled;
            ThousandsSeparator = String.Empty;
            Prefix = String.Empty;
            Suffix = String.Empty;
            SourceFormat = JqGridOptionsDefaults.ColumnModel.Formatter.SourceFormat;
            OutputFormat = JqGridOptionsDefaults.ColumnModel.Formatter.OutputFormat;
            BaseLinkUrl = String.Empty;
            ShowAction = String.Empty;
            AddParam = String.Empty;
            Target = String.Empty;
            IdName = JqGridOptionsDefaults.ColumnModel.Formatter.IdName;
            EditButton = JqGridOptionsDefaults.ColumnModel.Formatter.EditButton;
            DeleteButton = JqGridOptionsDefaults.ColumnModel.Formatter.DeleteButton;
            UseFormEditing = JqGridOptionsDefaults.ColumnModel.Formatter.UseFormEditing;
            PrimaryIcon = String.Empty;
            SecondaryIcon = String.Empty;
            Label = String.Empty;
            Text = JqGridOptionsDefaults.ColumnModel.Formatter.Text;
            InlineEditingOptions = null;
            FormEditingOptions = null;
            DeleteOptions = null;
        }

        /// <summary>
        /// Initializes new instance of JqGridColumnFormatterOptions class.
        /// </summary>
        /// <param name="formatter">Predefined formatter</param>
        public JqGridColumnFormatterOptions(string formatter)
            : this()
        {
            switch (formatter)
            {
                case JqGridPredefinedFormatters.Integer:
                    DefaultValue = JqGridOptionsDefaults.ColumnModel.Formatter.IntegerDefaultValue;
                    ThousandsSeparator = JqGridOptionsDefaults.ColumnModel.Formatter.ThousandsSeparator;
                    break;
                case JqGridPredefinedFormatters.Number:
                    DecimalPlaces = JqGridOptionsDefaults.ColumnModel.Formatter.DecimalPlaces;
                    DecimalSeparator = JqGridOptionsDefaults.ColumnModel.Formatter.DecimalSeparator;
                    DefaultValue = JqGridOptionsDefaults.ColumnModel.Formatter.NumberDefaultValue;
                    ThousandsSeparator = JqGridOptionsDefaults.ColumnModel.Formatter.ThousandsSeparator;
                    break;
                case JqGridPredefinedFormatters.Currency:
                    DecimalPlaces = JqGridOptionsDefaults.ColumnModel.Formatter.DecimalPlaces;
                    DecimalSeparator = JqGridOptionsDefaults.ColumnModel.Formatter.DecimalSeparator;
                    DefaultValue = JqGridOptionsDefaults.ColumnModel.Formatter.CurrencyDefaultValue;
                    ThousandsSeparator = JqGridOptionsDefaults.ColumnModel.Formatter.ThousandsSeparator;
                    break;
                case JqGridPredefinedFormatters.Actions:
                    InlineEditingOptions = new JqGridInlineNavigatorActionOptions();
                    DeleteOptions = new JqGridNavigatorDeleteActionOptions();
                    break;
            }
        }
        #endregion
    }
}
