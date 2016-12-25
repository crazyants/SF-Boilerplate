using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using SF.Web.Control.JqGrid.Infrastructure.Enums;
using SF.Web.Control.JqGrid.Infrastructure.Constants;
using SF.Web.Control.JqGrid.Infrastructure.Options;
using SF.Web.Control.JqGrid.Infrastructure.Options.ColumnModel;
using SF.Web.Control.JqGrid.Infrastructure.Options.Navigator;
using SF.Web.Control.JqGrid.Core.Request;
using SF.Web.Control.JqGrid.Core.Response;
using SF.Web.Control.JqGrid.Helper.Constants;
using SF.Web.Control.JqGrid.Helper.InternalHelpers;

namespace SF.Web.Control.JqGrid.Helper
{
    /// <summary>
    /// Provides support for generating jqGrid HMTL and JavaScript.
    /// </summary>
    public static class HtmlHelperJqGridExtensions
    {
        #region Fields
        private const string JQUERY_UI_BUTTON_FORMATTER_START = "function(cellValue,options,rowObject){setTimeout(function(){$('#' + options.rowId + '_JQueryUIButton').attr('data-cell-value',cellValue).button(";
        private const string JQUERY_UI_BUTTON_FORMATTER_ON_CLICK = ").click({0}";
        private const string JQUERY_UI_BUTTON_FORMATTER_END = ");},0);return '<button id=\"' + options.rowId + '_JQueryUIButton\" />';}";
        #endregion

        #region IHtmlHelper Extensions Methods
        /// <summary>
        /// Returns the HTML that is used to render the table placeholder for the grid. 
        /// </summary>
        /// <returns>The HTML that represents the table placeholder for jqGrid</returns>
        public static IHtmlContent JqGridTableHtml(this IHtmlHelper htmlHelper, JqGridOptions options)
        {
            return new HtmlString(String.Format("<table id='{0}'></table>", options.Id));
        }

        /// <summary>
        /// Returns the HTML that is used to render the pager (div) placeholder for the grid. 
        /// </summary>
        /// <returns>The HTML that represents the pager (div) placeholder for jqGrid</returns>
        public static IHtmlContent JqGridPagerHtml(this IHtmlHelper htmlHelper, JqGridOptions options)
        {
            return new HtmlString(String.Format("<div id='{0}'></div>", GetJqGridPagerId(options)));
        }

        /// <summary>
        /// Returns the HTML that is used to render the table placeholder for the grid with pager placeholder below it and filter grid (if enabled) placeholder above it.
        /// </summary>
        /// <returns>The HTML that represents the table placeholder for jqGrid with pager placeholder below i</returns>
        public static IHtmlContent JqGridHtml(this IHtmlHelper htmlHelper, JqGridOptions options)
        {
            if (options.Pager)
                return new HtmlString(htmlHelper.JqGridTableHtml(options).ToString() + htmlHelper.JqGridPagerHtml(options).ToString());
            else
                return htmlHelper.JqGridTableHtml(options);
        }

        /// <summary>
        /// Return the JavaScript that is used to initialize jqGrid with given options.
        /// </summary>
        /// <returns>The JavaScript that initializes jqGrid with give options</returns>
        /// <exception cref="System.InvalidOperationException">
        /// <list type="bullet">
        /// <item><description>TreeGrid and data grouping are both enabled.</description></item>
        /// <item><description>Rows numbers and data grouping are both enabled.</description></item>
        /// <item><description>Dynamic scrolling and data grouping are both enabled.</description></item>
        /// <item><description>TreeGrid and GridView are both enabled.</description></item>
        /// <item><description>SubGrid and GridView are both enabled.</description></item>
        /// <item><description>AfterInsertRow event and GridView are both enabled.</description></item>
        /// </list> 
        /// </exception>
        public static IHtmlContent JqGridJavaScript(this IHtmlHelper htmlHelper, JqGridOptions options)
        {
            ValidateJqGridConstraints(options);

            options.ApplyModelMetadata(htmlHelper.MetadataProvider);

            StringBuilder javaScriptBuilder = new StringBuilder();

            javaScriptBuilder.AppendFormat("$({0}).jqGrid({{", GetJqGridGridSelector(options, false)).AppendLine()
                .AppendJavaScriptObjectStringArrayField(JqGridOptionsNames.COLUMNS_NAMES_FIELD, options.ColumnsNames)
                .AppendColumnsModels(options)
                .AppendOptions(options)
                .AppendJavaScriptObjectClosing()
                .AppendLine(");");

            return new HtmlString(javaScriptBuilder.ToString());
        }
        #endregion

        #region Private Methods
        private static void ValidateJqGridConstraints(JqGridOptions options)
        {
            if (options.TreeGridEnabled && options.GroupingEnabled)
            {
                throw new InvalidOperationException("TreeGrid and data grouping can not be enabled at the same time.");
            }

            if ((options.DynamicScrollingMode != JqGridDynamicScrollingModes.Disabled) && options.GroupingEnabled)
            {
                throw new InvalidOperationException("Dynamic scrolling and data grouping can not be enabled at the same time.");
            }
        }

        private static string GetJqGridGridSelector(JqGridOptions options, bool asSubgrid)
        {
            return asSubgrid ? "'#' + subgridTableId" : String.Format("'#{0}'", options.Id);
        }

        private static string GetJqGridPagerId(JqGridOptions options)
        {
            return options.Id + "Pager";
        }

        private static string GetJqGridPagerSelector(JqGridOptions options, bool asSubgrid)
        {
            return asSubgrid ? "'#' + subgridPagerId" : String.Format("'#{0}'", GetJqGridPagerId(options));
        }

        private static StringBuilder AppendColumnsModels(this StringBuilder javaScriptBuilder, JqGridOptions options)
        {
            javaScriptBuilder.AppendJavaScriptArrayFieldOpening(JqGridOptionsNames.COLUMNS_MODEL_FIELD);

            foreach (JqGridColumnModel columnModel in options.ColumnsModels)
            {
                javaScriptBuilder.AppendJavaScriptObjectOpening()
                    .AppendJavaScriptObjectStringField(JqGridOptionsNames.ColumnModel.NAME_FIELD, columnModel.Name)
                    .AppendJavaScriptObjectEnumField(JqGridOptionsNames.ColumnModel.ALIGNMENT, columnModel.Alignment, JqGridOptionsDefaults.ColumnModel.Alignment)
                    .AppendJavaScriptObjectFunctionField(JqGridOptionsNames.ColumnModel.CELL_ATTRIBUTES, columnModel.CellAttributes)
                    .AppendJavaScriptObjectStringField(JqGridOptionsNames.ColumnModel.CLASSES, columnModel.Classes)
                    .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.ColumnModel.FIXED, columnModel.Fixed, JqGridOptionsDefaults.ColumnModel.Fixed)
                    .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.ColumnModel.FROZEN, columnModel.Frozen, JqGridOptionsDefaults.ColumnModel.Frozen)
                    .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.ColumnModel.HIDE_IN_DIALOG, columnModel.HideInDialog, JqGridOptionsDefaults.ColumnModel.HideInDialog)
                    .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.ColumnModel.RESIZABLE, columnModel.Resizable, JqGridOptionsDefaults.ColumnModel.Resizable)
                    .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.ColumnModel.TITLE, columnModel.Title, JqGridOptionsDefaults.ColumnModel.Title)
                    .AppendJavaScriptObjectIntegerField(JqGridOptionsNames.ColumnModel.WIDTH, columnModel.Width, JqGridOptionsDefaults.ColumnModel.Width)
                    .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.ColumnModel.VIEWABLE, columnModel.Viewable, JqGridOptionsDefaults.ColumnModel.Viewable)
                    .AppendColumnModelSortOptions(columnModel)
                    .AppendColumnModelSummaryOptions(columnModel, options)
                    .AppendColumnModelFormatter(columnModel);

                javaScriptBuilder.AppendJavaScriptObjectFieldClosing();
            }

            javaScriptBuilder.AppendJavaScriptArrayFieldClosing()
                .AppendLine();

            return javaScriptBuilder;
        }

        private static StringBuilder AppendColumnModelSortOptions(this StringBuilder javaScriptBuilder, JqGridColumnModel columnModel)
        {
            javaScriptBuilder.AppendJavaScriptObjectStringField(JqGridOptionsNames.ColumnModel.INDEX_FIELD, columnModel.Index)
                .AppendJavaScriptObjectEnumField(JqGridOptionsNames.ColumnModel.INITIAL_SORTING_ORDER_FIELD, columnModel.InitialSortingOrder, JqGridOptionsDefaults.ColumnModel.Sorting.InitialOrder);

            if (columnModel.Sortable != JqGridOptionsDefaults.ColumnModel.Sorting.Sortable)
            {
                javaScriptBuilder.AppendJavaScriptObjectBooleanField(JqGridOptionsNames.ColumnModel.SORTABLE_FIELD, columnModel.Sortable);
            }
            else
            {
                if (columnModel.SortType == JqGridSortTypes.Function)
                {
                    javaScriptBuilder.AppendJavaScriptObjectFunctionField(JqGridOptionsNames.ColumnModel.SORT_TYPE_FIELD, columnModel.SortFunction);
                }
                else
                {
                    javaScriptBuilder.AppendJavaScriptObjectEnumField(JqGridOptionsNames.ColumnModel.SORT_TYPE_FIELD, columnModel.SortType, JqGridOptionsDefaults.ColumnModel.Sorting.Type);
                }
            }

            return javaScriptBuilder;
        }

        private static StringBuilder AppendColumnModelSummaryOptions(this StringBuilder javaScriptBuilder, JqGridColumnModel columnModel, JqGridOptions options)
        {
            if (options.GroupingEnabled)
            {
                if (columnModel.SummaryType.HasValue)
                {
                    if (columnModel.SummaryType.Value != JqGridColumnSummaryTypes.Custom)
                    {
                        javaScriptBuilder.AppendJavaScriptObjectEnumField(JqGridOptionsNames.ColumnModel.SUMMARY_TYPE, columnModel.SummaryType.Value);
                    }
                    else
                    {
                        javaScriptBuilder.AppendJavaScriptObjectFunctionField(JqGridOptionsNames.ColumnModel.SUMMARY_TYPE, columnModel.SummaryFunction);
                    }
                }

                javaScriptBuilder.AppendJavaScriptObjectStringField(JqGridOptionsNames.ColumnModel.SUMMARY_TEMPLATE, columnModel.SummaryTemplate, JqGridOptionsDefaults.ColumnModel.SummaryTemplate);
            }

            return javaScriptBuilder;
        }

        private static StringBuilder AppendColumnModelFormatter(this StringBuilder javaScriptBuilder, JqGridColumnModel columnModel)
        {
            if (!String.IsNullOrWhiteSpace(columnModel.Formatter))
            {
                if (columnModel.Formatter == JqGridPredefinedFormatters.JQueryUIButton)
                {
                    javaScriptBuilder.AppendColumnModelJQueryUIButtonFormatter(columnModel.FormatterOptions);
                }
                else
                {
                    javaScriptBuilder.AppendJavaScriptObjectFunctionField(JqGridOptionsNames.ColumnModel.FORMATTER_FIELD, columnModel.Formatter)
                        .AppendColumnModelFormatterOptions(columnModel.Formatter, columnModel.FormatterOptions)
                        .AppendJavaScriptObjectFunctionField(JqGridOptionsNames.ColumnModel.UNFORMATTER_FIELD, columnModel.UnFormatter);
                }
            }

            return javaScriptBuilder;
        }

        private static StringBuilder AppendColumnModelJQueryUIButtonFormatter(this StringBuilder javaScriptBuilder, JqGridColumnFormatterOptions formatterOptions)
        {
            StringBuilder jQueryUIButtonFormatterBuilder = new StringBuilder(80);
            jQueryUIButtonFormatterBuilder.Append(JQUERY_UI_BUTTON_FORMATTER_START);

            if (!formatterOptions.AreDefault(JqGridPredefinedFormatters.JQueryUIButton))
            {
                jQueryUIButtonFormatterBuilder.AppendJavaScriptObjectOpening()
                    .AppendJavaScriptObjectStringField(JqGridOptionsNames.ColumnModel.Formatter.LABEL, formatterOptions.Label)
                    .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.ColumnModel.Formatter.TEXT, formatterOptions.Text, JqGridOptionsDefaults.ColumnModel.Formatter.Text);

                if (!String.IsNullOrEmpty(formatterOptions.PrimaryIcon) || !String.IsNullOrEmpty(formatterOptions.SecondaryIcon))
                {
                    jQueryUIButtonFormatterBuilder.AppendJavaScriptObjectFieldOpening(JqGridOptionsNames.ColumnModel.Formatter.ICONS)
                        .AppendJavaScriptObjectStringField(JqGridOptionsNames.ColumnModel.Formatter.PRIMARY, formatterOptions.PrimaryIcon)
                        .AppendJavaScriptObjectStringField(JqGridOptionsNames.ColumnModel.Formatter.SECONDARY, formatterOptions.SecondaryIcon)
                        .AppendJavaScriptObjectFieldClosing();
                }

                jQueryUIButtonFormatterBuilder.AppendJavaScriptObjectClosing();
            }

            if (!String.IsNullOrWhiteSpace(formatterOptions.OnClick))
            {
                jQueryUIButtonFormatterBuilder.AppendFormat(JQUERY_UI_BUTTON_FORMATTER_ON_CLICK, formatterOptions.OnClick);
            }
            jQueryUIButtonFormatterBuilder.Append(JQUERY_UI_BUTTON_FORMATTER_END);

            return javaScriptBuilder.AppendJavaScriptObjectFunctionField(JqGridOptionsNames.ColumnModel.FORMATTER_FIELD, jQueryUIButtonFormatterBuilder.ToString());
        }

        private static StringBuilder AppendColumnModelFormatterOptions(this StringBuilder javaScriptBuilder, string formatter, JqGridColumnFormatterOptions formatterOptions)
        {
            if ((formatterOptions) != null && !formatterOptions.AreDefault(formatter))
            {
                javaScriptBuilder.AppendJavaScriptObjectFieldOpening(JqGridOptionsNames.ColumnModel.FORMATTER_OPTIONS_FIELD);

                switch (formatter)
                {
                    case JqGridPredefinedFormatters.Integer:
                        javaScriptBuilder.AppendColumnModelIntegerFormatterOptions(formatterOptions);
                        break;
                    case JqGridPredefinedFormatters.Number:
                        javaScriptBuilder.AppendColumnModelNumberFormatterOptions(formatterOptions);
                        break;
                    case JqGridPredefinedFormatters.Currency:
                        javaScriptBuilder.AppendColumnModelCurrencyFormatterOptions(formatterOptions);
                        break;
                    case JqGridPredefinedFormatters.Date:
                        javaScriptBuilder.AppendColumnModelDateFormatterOptions(formatterOptions);
                        break;
                    case JqGridPredefinedFormatters.Link:
                        javaScriptBuilder.AppendColumnModelLinkFormatterOptions(formatterOptions);
                        break;
                    case JqGridPredefinedFormatters.ShowLink:
                        javaScriptBuilder.AppendColumnModelShowLinkFormatterOptions(formatterOptions);
                        break;
                    case JqGridPredefinedFormatters.CheckBox:
                        javaScriptBuilder.AppendColumnModelCheckBoxFormatterOptions(formatterOptions);
                        break;
                    case JqGridPredefinedFormatters.Actions:
                        javaScriptBuilder.AppendColumnModelActionsFormatterOptions(formatterOptions);
                        break;
                }

                javaScriptBuilder.AppendJavaScriptObjectFieldClosing();
            }

            return javaScriptBuilder;
        }

        private static StringBuilder AppendColumnModelIntegerFormatterOptions(this StringBuilder javaScriptBuilder, JqGridColumnFormatterOptions formatterOptions)
        {
            return javaScriptBuilder.AppendJavaScriptObjectStringField(JqGridOptionsNames.ColumnModel.Formatter.DEFAULT_VALUE, formatterOptions.DefaultValue, JqGridOptionsDefaults.ColumnModel.Formatter.IntegerDefaultValue)
                .AppendJavaScriptObjectStringField(JqGridOptionsNames.ColumnModel.Formatter.THOUSANDS_SEPARATOR, formatterOptions.ThousandsSeparator, JqGridOptionsDefaults.ColumnModel.Formatter.ThousandsSeparator);
        }

        private static StringBuilder AppendColumnModelNumberFormatterOptions(this StringBuilder javaScriptBuilder, JqGridColumnFormatterOptions formatterOptions)
        {
            return javaScriptBuilder.AppendJavaScriptObjectIntegerField(JqGridOptionsNames.ColumnModel.Formatter.DECIMAL_PLACES, formatterOptions.DecimalPlaces, JqGridOptionsDefaults.ColumnModel.Formatter.DecimalPlaces)
                .AppendJavaScriptObjectStringField(JqGridOptionsNames.ColumnModel.Formatter.DECIMAL_SEPARATOR, formatterOptions.DecimalSeparator, JqGridOptionsDefaults.ColumnModel.Formatter.DecimalSeparator)
                .AppendJavaScriptObjectStringField(JqGridOptionsNames.ColumnModel.Formatter.DEFAULT_VALUE, formatterOptions.DefaultValue, JqGridOptionsDefaults.ColumnModel.Formatter.NumberDefaultValue)
                .AppendJavaScriptObjectStringField(JqGridOptionsNames.ColumnModel.Formatter.THOUSANDS_SEPARATOR, formatterOptions.ThousandsSeparator, JqGridOptionsDefaults.ColumnModel.Formatter.ThousandsSeparator);
        }

        private static StringBuilder AppendColumnModelCurrencyFormatterOptions(this StringBuilder javaScriptBuilder, JqGridColumnFormatterOptions formatterOptions)
        {
            return javaScriptBuilder.AppendJavaScriptObjectIntegerField(JqGridOptionsNames.ColumnModel.Formatter.DECIMAL_PLACES, formatterOptions.DecimalPlaces, JqGridOptionsDefaults.ColumnModel.Formatter.DecimalPlaces)
                .AppendJavaScriptObjectStringField(JqGridOptionsNames.ColumnModel.Formatter.DECIMAL_SEPARATOR, formatterOptions.DecimalSeparator, JqGridOptionsDefaults.ColumnModel.Formatter.DecimalSeparator)
                .AppendJavaScriptObjectStringField(JqGridOptionsNames.ColumnModel.Formatter.DEFAULT_VALUE, formatterOptions.DefaultValue, JqGridOptionsDefaults.ColumnModel.Formatter.NumberDefaultValue)
                .AppendJavaScriptObjectStringField(JqGridOptionsNames.ColumnModel.Formatter.PREFIX, formatterOptions.Prefix)
                .AppendJavaScriptObjectStringField(JqGridOptionsNames.ColumnModel.Formatter.SUFFIX, formatterOptions.Suffix)
                .AppendJavaScriptObjectStringField(JqGridOptionsNames.ColumnModel.Formatter.THOUSANDS_SEPARATOR, formatterOptions.ThousandsSeparator, JqGridOptionsDefaults.ColumnModel.Formatter.ThousandsSeparator);
        }

        private static StringBuilder AppendColumnModelDateFormatterOptions(this StringBuilder javaScriptBuilder, JqGridColumnFormatterOptions formatterOptions)
        {
            return javaScriptBuilder.AppendJavaScriptObjectStringField(JqGridOptionsNames.ColumnModel.Formatter.SOURCE_FORMAT, formatterOptions.SourceFormat, JqGridOptionsDefaults.ColumnModel.Formatter.SourceFormat)
                .AppendJavaScriptObjectStringField(JqGridOptionsNames.ColumnModel.Formatter.OUTPUT_FORMAT, formatterOptions.OutputFormat, JqGridOptionsDefaults.ColumnModel.Formatter.OutputFormat);
        }

        private static StringBuilder AppendColumnModelLinkFormatterOptions(this StringBuilder javaScriptBuilder, JqGridColumnFormatterOptions formatterOptions)
        {
            return javaScriptBuilder.AppendJavaScriptObjectStringField(JqGridOptionsNames.ColumnModel.Formatter.TARGET, formatterOptions.Target);
        }

        private static StringBuilder AppendColumnModelShowLinkFormatterOptions(this StringBuilder javaScriptBuilder, JqGridColumnFormatterOptions formatterOptions)
        {
            return javaScriptBuilder.AppendJavaScriptObjectStringField(JqGridOptionsNames.ColumnModel.Formatter.BASE_LINK_URL, formatterOptions.BaseLinkUrl)
                .AppendJavaScriptObjectStringField(JqGridOptionsNames.ColumnModel.Formatter.SHOW_ACTION, formatterOptions.ShowAction)
                .AppendJavaScriptObjectStringField(JqGridOptionsNames.ColumnModel.Formatter.ADD_PARAM, formatterOptions.AddParam)
                .AppendJavaScriptObjectStringField(JqGridOptionsNames.ColumnModel.Formatter.TARGET, formatterOptions.Target)
                .AppendJavaScriptObjectStringField(JqGridOptionsNames.ColumnModel.Formatter.ID_NAME, formatterOptions.IdName, JqGridOptionsDefaults.ColumnModel.Formatter.IdName);
        }

        private static StringBuilder AppendColumnModelCheckBoxFormatterOptions(this StringBuilder javaScriptBuilder, JqGridColumnFormatterOptions formatterOptions)
        {
            return javaScriptBuilder.AppendJavaScriptObjectBooleanField(JqGridOptionsNames.ColumnModel.Formatter.DISABLED, formatterOptions.Disabled);
        }

        private static StringBuilder AppendColumnModelActionsFormatterOptions(this StringBuilder javaScriptBuilder, JqGridColumnFormatterOptions formatterOptions)
        {
            javaScriptBuilder.AppendJavaScriptObjectBooleanField(JqGridOptionsNames.ColumnModel.Formatter.EDIT_BUTTON, formatterOptions.EditButton, JqGridOptionsDefaults.ColumnModel.Formatter.EditButton)
                .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.ColumnModel.Formatter.DELETE_BUTTON, formatterOptions.DeleteButton, JqGridOptionsDefaults.ColumnModel.Formatter.DeleteButton)
                .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.ColumnModel.Formatter.USE_FORM_EDITING, formatterOptions.UseFormEditing, JqGridOptionsDefaults.ColumnModel.Formatter.UseFormEditing);

            if (formatterOptions.EditButton)
            {
                if (formatterOptions.UseFormEditing)
                {
                    javaScriptBuilder.AppendNavigatorEditActionOptions(JqGridOptionsNames.ColumnModel.Formatter.EDIT_OPTIONS, formatterOptions.FormEditingOptions);
                }
                else
                {
                    javaScriptBuilder.AppendInlineNavigatorActionOptions(formatterOptions.InlineEditingOptions);
                }
            }

            if (formatterOptions.DeleteButton)
            {
                javaScriptBuilder.AppendNavigatorDeleteActionOptions(JqGridOptionsNames.ColumnModel.Formatter.DELETE_OPTIONS, formatterOptions.DeleteOptions);
            }

            return javaScriptBuilder;
        }

        private static StringBuilder AppendOptions(this StringBuilder javaScriptBuilder, JqGridOptions options)
        {
            javaScriptBuilder.AppendJavaScriptObjectStringField(JqGridOptionsNames.CAPTION, options.Caption)
                .AppendJavaScriptObjectEnumField(JqGridOptionsNames.DATA_TYPE, options.DataType, JqGridOptionsDefaults.DataType)
                .AppendDataSource(options)
                .AppendGrouping(options)
                .AppendParametersNames(options.ParametersNames)
                .AppendJsonReader(options.JsonReader)
                .AppendPager(options)
                .AppendSubgrid(options)
                .AppendTreeGrid(options)
                .AppendDynamicScrolling(options)
                .AppendJavaScriptObjectStringField(JqGridOptionsNames.SORTING_NAME, options.SortingName)
                .AppendJavaScriptObjectEnumField(JqGridOptionsNames.SORTING_ORDER, options.SortingOrder, JqGridOptionsDefaults.SortingOrder)
                .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.FOOTER_ENABLED, options.FooterEnabled, JqGridOptionsDefaults.FooterEnabled)
                .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.USER_DATA_ON_FOOTER, options.UserDataOnFooter, JqGridOptionsDefaults.UserDataOnFooter)
                .AppendJavaScriptObjectIntegerField(JqGridOptionsNames.HEIGHT, options.Height);

            if (!options.Height.HasValue)
            {
                javaScriptBuilder.AppendJavaScriptObjectStringField(JqGridOptionsNames.HEIGHT, JqGridOptionsDefaults.Height);
            }

            return javaScriptBuilder;
        }

        private static StringBuilder AppendDataSource(this StringBuilder javaScriptBuilder, JqGridOptions options)
        {
            if (options.DataType.IsDataStringDataType())
            {
                javaScriptBuilder.AppendJavaScriptObjectStringField(JqGridOptionsNames.DATA_STRING, options.DataString);
            }
            else
            {
                javaScriptBuilder.AppendJavaScriptObjectStringField(JqGridOptionsNames.URL, options.Url)
                    .AppendJavaScriptObjectEnumField(JqGridOptionsNames.METHOD_TYPE, options.MethodType, JqGridOptionsDefaults.MethodType);
            }

            return javaScriptBuilder;
        }

        private static StringBuilder AppendDynamicScrolling(this StringBuilder javaScriptBuilder, JqGridOptions options)
        {
            if (options.DynamicScrollingMode == JqGridDynamicScrollingModes.HoldAllRows)
            {
                javaScriptBuilder.AppendJavaScriptObjectBooleanField(JqGridOptionsNames.DYNAMIC_SCROLLING_MODE, true);
            }
            else if (options.DynamicScrollingMode == JqGridDynamicScrollingModes.HoldVisibleRows)
            {
                javaScriptBuilder.AppendJavaScriptObjectIntegerField(JqGridOptionsNames.DYNAMIC_SCROLLING_MODE, 10)
                    .AppendJavaScriptObjectIntegerField(JqGridOptionsNames.DYNAMIC_SCROLLING_TIMEOUT, options.DynamicScrollingTimeout, JqGridOptionsDefaults.DynamicScrollingTimeout);
            }

            return javaScriptBuilder;
        }

        private static StringBuilder AppendGrouping(this StringBuilder javaScriptBuilder, JqGridOptions options)
        {
            if (options.GroupingEnabled)
            {
                javaScriptBuilder.AppendJavaScriptObjectBooleanField(JqGridOptionsNames.GROUPING_ENABLED, options.GroupingEnabled);
                if (options.GroupingView != null)
                {
                    javaScriptBuilder.AppendJavaScriptObjectFieldOpening(JqGridOptionsNames.GROUPING_VIEW)
                        .AppendJavaScriptObjectStringArrayField(JqGridOptionsNames.GroupingView.FIELDS, options.GroupingView.Fields)
                        .AppendJavaScriptObjectEnumArrayField(JqGridOptionsNames.GroupingView.ORDERS, options.GroupingView.Orders)
                        .AppendJavaScriptObjectStringArrayField(JqGridOptionsNames.GroupingView.TEXTS, options.GroupingView.Texts)
                        .AppendJavaScriptObjectBooleanArrayField(JqGridOptionsNames.GroupingView.SUMMARY, options.GroupingView.Summary)
                        .AppendJavaScriptObjectBooleanArrayField(JqGridOptionsNames.GroupingView.COLUMN_SHOW, options.GroupingView.ColumnShow)
                        .AppendJavaScriptObjectFunctionArrayField(JqGridOptionsNames.GroupingView.IS_IN_THE_SAME_GROUP_CALLBACKS, options.GroupingView.IsInTheSameGroupCallbacks)
                        .AppendJavaScriptObjectFunctionArrayField(JqGridOptionsNames.GroupingView.FORMAT_DISPLAY_FIELD_CALLBACKS, options.GroupingView.FormatDisplayFieldCallbacks)
                        .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.GroupingView.SUMMARY_ON_HIDE, options.GroupingView.SummaryOnHide, JqGridOptionsDefaults.GroupingView.SummaryOnHide)
                        .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.GroupingView.DATA_SORTED, options.GroupingView.DataSorted, JqGridOptionsDefaults.GroupingView.DataSorted)
                        .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.GroupingView.COLLAPSE, options.GroupingView.Collapse, JqGridOptionsDefaults.GroupingView.Collapse)
                        .AppendJavaScriptObjectStringField(JqGridOptionsNames.GroupingView.PLUS_ICON, options.GroupingView.PlusIcon, JqGridOptionsDefaults.GroupingView.PlusIcon)
                        .AppendJavaScriptObjectStringField(JqGridOptionsNames.GroupingView.MINUS_ICON, options.GroupingView.MinusIcon, JqGridOptionsDefaults.GroupingView.MinusIcon)
                        .AppendJavaScriptObjectFieldClosing();
                }
            }

            return javaScriptBuilder;
        }

        private static StringBuilder AppendSubgrid(this StringBuilder javaScriptBuilder, JqGridOptions options)
        {
            if (options.SubgridEnabled && !String.IsNullOrWhiteSpace(options.SubgridUrl) && (options.SubgridModel != null))
            {
                javaScriptBuilder.AppendJavaScriptObjectBooleanField(JqGridOptionsNames.SUBGRID_ENABLED, true)
                    .AppendJavaScriptObjectStringField(JqGridOptionsNames.SUBGRID_ULR, options.SubgridUrl)
                    .AppendJavaScriptObjectIntegerField(JqGridOptionsNames.SUBGRID_WIDTH, options.SubgridColumnWidth, JqGridOptionsDefaults.SubgridColumnWidth)
                    .AppendJavaScriptObjectFunctionField(JqGridOptionsNames.SUBGRID_BEFORE_EXPAND, options.SubGridBeforeExpand)
                    .AppendJavaScriptObjectFunctionField(JqGridOptionsNames.SUBGRID_ROW_EXPANDED, options.SubGridRowExpanded)
                    .AppendJavaScriptObjectFunctionField(JqGridOptionsNames.SUBGRID_ROW_COLAPSED, options.SubGridRowColapsed)
                    .AppendJavaScriptArrayFieldOpening(JqGridOptionsNames.SUBGRID_MODEL)
                    .AppendJavaScriptObjectOpening()
                    .AppendJavaScriptObjectStringArrayField(JqGridOptionsNames.SubgridModel.NAMES, options.SubgridModel.ColumnsModels.Select(c => c.Name))
                    .AppendJavaScriptObjectIntegerArrayField(JqGridOptionsNames.SubgridModel.WIDTHS, options.SubgridModel.ColumnsModels.Select(c => c.Width))
                    .AppendJavaScriptObjectEnumArrayField(JqGridOptionsNames.SubgridModel.ALIGNMENTS, options.SubgridModel.ColumnsModels.Select(c => c.Alignment))
                    .AppendJavaScriptObjectStringArrayField(JqGridOptionsNames.SubgridModel.MAPPINGS, options.SubgridModel.ColumnsModels.Select(c => c.Mapping))
                    .AppendJavaScriptObjectStringArrayField(JqGridOptionsNames.SubgridModel.PARAMETERS, options.SubgridModel.Parameters)
                    .AppendJavaScriptObjectFieldClosing()
                    .AppendJavaScriptArrayFieldClosing();
            }

            return javaScriptBuilder;
        }

        private static StringBuilder AppendTreeGrid(this StringBuilder javaScriptBuilder, JqGridOptions options)
        {
            if (options.TreeGridEnabled)
            {
                javaScriptBuilder.AppendJavaScriptObjectBooleanField(JqGridOptionsNames.TREE_GRID_ENABLED, options.TreeGridEnabled)
                    .AppendJavaScriptObjectEnumField(JqGridOptionsNames.TREE_GRID_MODEL, options.TreeGridModel, JqGridOptionsDefaults.TreeGridModel)
                    .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.EXPAND_COLUMN_CLICK, options.ExpandColumnClick, JqGridOptionsDefaults.ExpandColumnClick)
                    .AppendJavaScriptObjectStringField(JqGridOptionsNames.EXPAND_COLUMN, options.ExpandColumn);
            }

            return javaScriptBuilder;
        }

        private static StringBuilder AppendPager(this StringBuilder javaScriptBuilder, JqGridOptions options)
        {
            if (options.Pager)
            {
                javaScriptBuilder.AppendJavaScriptObjectFunctionField(JqGridOptionsNames.PAGER, GetJqGridPagerSelector(options, false))
                    .AppendJavaScriptObjectIntegerArrayField(JqGridOptionsNames.ROWS_LIST, options.RowsList)
                    .AppendJavaScriptObjectIntegerField(JqGridOptionsNames.ROWS_NUMBER, options.RowsNumber, JqGridOptionsDefaults.RowsNumber)
                    .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.VIEW_RECORDS, options.ViewRecords, JqGridOptionsDefaults.ViewRecords);
            }

            return javaScriptBuilder;
        }

        private static StringBuilder AppendJsonReader(this StringBuilder javaScriptBuilder, JqGridJsonReader jsonReader)
        {
            jsonReader = jsonReader ?? JqGridResponse.JsonReader;

            if ((jsonReader != null) && !jsonReader.IsDefault())
            {
                javaScriptBuilder.AppendJavaScriptObjectFieldOpening(JqGridOptionsNames.JSON_READER)
                    .AppendJavaScriptObjectStringField(JqGridOptionsNames.JsonReader.PAGE_INDEX, jsonReader.PageIndex, JqGridOptionsDefaults.Response.PageIndex)
                    .AppendJavaScriptObjectStringField(JqGridOptionsNames.JsonReader.RECORD_ID, jsonReader.RecordId, JqGridOptionsDefaults.Response.RecordId)
                    .AppendJavaScriptObjectStringField(JqGridOptionsNames.JsonReader.TOTAL_PAGES_COUNT, jsonReader.TotalPagesCount, JqGridOptionsDefaults.Response.TotalPagesCount)
                    .AppendJavaScriptObjectStringField(JqGridOptionsNames.JsonReader.TOTAL_RECORDS_COUNT, jsonReader.TotalRecordsCount, JqGridOptionsDefaults.Response.TotalRecordsCount)
                    .AppendJavaScriptObjectStringField(JqGridOptionsNames.JsonReader.USER_DATA, jsonReader.UserData, JqGridOptionsDefaults.Response.UserData)
                    .AppendJsonRecordsReaderProperties(jsonReader)
                    .AppendJsonRecordsReader(jsonReader.SubgridReader)
                    .AppendJavaScriptObjectFieldClosing();
            }

            return javaScriptBuilder;
        }

        private static StringBuilder AppendJsonRecordsReader(this StringBuilder javaScriptBuilder, JqGridJsonRecordsReader jsonRecordsReader)
        {
            if ((jsonRecordsReader != null) && !jsonRecordsReader.IsDefault())
            {
                javaScriptBuilder.AppendJavaScriptObjectFieldOpening(JqGridOptionsNames.JsonReader.SUBGRID)
                    .AppendJsonRecordsReaderProperties(jsonRecordsReader)
                    .AppendJavaScriptObjectFieldClosing();
            }

            return javaScriptBuilder;
        }

        private static StringBuilder AppendJsonRecordsReaderProperties(this StringBuilder javaScriptBuilder, JqGridJsonRecordsReader jsonRecordsReader)
        {
            return javaScriptBuilder.AppendJavaScriptObjectStringField(JqGridOptionsNames.JsonReader.RECORDS, jsonRecordsReader.Records, JqGridOptionsDefaults.Response.Records)
                .AppendJavaScriptObjectStringField(JqGridOptionsNames.JsonReader.RECORD_VALUES, jsonRecordsReader.RecordValues, JqGridOptionsDefaults.Response.RecordValues)
                .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.JsonReader.REPEAT_ITEMS, jsonRecordsReader.RepeatItems, JqGridOptionsDefaults.Response.RepeatItems);
        }

        private static StringBuilder AppendParametersNames(this StringBuilder javaScriptBuilder, JqGridParametersNames parametersNames)
        {
            parametersNames = parametersNames ?? JqGridRequest.ParametersNames;

            if ((parametersNames != null) && !parametersNames.AreDefault())
            {
                javaScriptBuilder.AppendJavaScriptObjectFieldOpening(JqGridOptionsNames.PARAMETERS_NAMES)
                    .AppendJavaScriptObjectStringField(JqGridOptionsNames.ParametersNames.PAGE_INDEX, parametersNames.PageIndex, JqGridOptionsDefaults.Request.PageIndex)
                    .AppendJavaScriptObjectStringField(JqGridOptionsNames.ParametersNames.RECORDS_COUNT, parametersNames.RecordsCount, JqGridOptionsDefaults.Request.RecordsCount)
                    .AppendJavaScriptObjectStringField(JqGridOptionsNames.ParametersNames.SORTING_NAME, parametersNames.SortingName, JqGridOptionsDefaults.Request.SortingName)
                    .AppendJavaScriptObjectStringField(JqGridOptionsNames.ParametersNames.SORTING_ORDER, parametersNames.SortingOrder, JqGridOptionsDefaults.Request.SortingOrder)
                    .AppendJavaScriptObjectStringField(JqGridOptionsNames.ParametersNames.SEARCHING, parametersNames.Searching, JqGridOptionsDefaults.Request.Searching)
                    .AppendJavaScriptObjectStringField(JqGridOptionsNames.ParametersNames.ID, parametersNames.Id, JqGridOptionsDefaults.Request.Id)
                    .AppendJavaScriptObjectStringField(JqGridOptionsNames.ParametersNames.OPERATOR, parametersNames.Operator, JqGridOptionsDefaults.Request.Operator)
                    .AppendJavaScriptObjectStringField(JqGridOptionsNames.ParametersNames.EDIT_OPERATOR, parametersNames.EditOperator, JqGridOptionsDefaults.Request.EditOperator)
                    .AppendJavaScriptObjectStringField(JqGridOptionsNames.ParametersNames.ADD_OPERATOR, parametersNames.AddOperator, JqGridOptionsDefaults.Request.AddOperator)
                    .AppendJavaScriptObjectStringField(JqGridOptionsNames.ParametersNames.DELETE_OPERATOR, parametersNames.DeleteOperator, JqGridOptionsDefaults.Request.DeleteOperator)
                    .AppendJavaScriptObjectStringField(JqGridOptionsNames.ParametersNames.SUBGRID_ID, parametersNames.SubgridId, JqGridOptionsDefaults.Request.SubgridId)
                    .AppendJavaScriptObjectStringField(JqGridOptionsNames.ParametersNames.PAGES_COUNT, parametersNames.PagesCount)
                    .AppendJavaScriptObjectStringField(JqGridOptionsNames.ParametersNames.TOTAL_ROWS, parametersNames.TotalRows, JqGridOptionsDefaults.Request.TotalRows)
                    .AppendJavaScriptObjectFieldClosing();
            }

            return javaScriptBuilder;
        }

        private static StringBuilder AppendInlineNavigatorActionOptions(this StringBuilder javaScriptBuilder, JqGridInlineNavigatorActionOptions inlineNavigatorActionOptions)
        {
            if ((inlineNavigatorActionOptions != null) && !inlineNavigatorActionOptions.AreDefault())
            {
                javaScriptBuilder.AppendJavaScriptObjectScriptOrObjectField(JqGridOptionsNames.InlineNavigator.EXTRA_PARAM, inlineNavigatorActionOptions.ExtraParamScript, inlineNavigatorActionOptions.ExtraParam)
                    .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.InlineNavigator.KEYS, inlineNavigatorActionOptions.Keys, JqGridOptionsDefaults.Navigator.InlineActionKeys)
                    .AppendJavaScriptObjectFunctionField(JqGridOptionsNames.InlineNavigator.ON_EDIT_FUNCTION, inlineNavigatorActionOptions.OnEditFunction)
                    .AppendJavaScriptObjectFunctionField(JqGridOptionsNames.InlineNavigator.SUCCESS_FUNCTION, inlineNavigatorActionOptions.SuccessFunction)
                    .AppendJavaScriptObjectStringField(JqGridOptionsNames.InlineNavigator.URL, inlineNavigatorActionOptions.Url)
                    .AppendJavaScriptObjectFunctionField(JqGridOptionsNames.InlineNavigator.AFTER_SAVE_FUNCTION, inlineNavigatorActionOptions.AfterSaveFunction)
                    .AppendJavaScriptObjectFunctionField(JqGridOptionsNames.InlineNavigator.ERROR_FUNCTION, inlineNavigatorActionOptions.ErrorFunction)
                    .AppendJavaScriptObjectFunctionField(JqGridOptionsNames.InlineNavigator.AFTER_RESTORE_FUNCTION, inlineNavigatorActionOptions.AfterRestoreFunction)
                    .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.InlineNavigator.RESTORE_AFTER_ERROR, inlineNavigatorActionOptions.RestoreAfterError, JqGridOptionsDefaults.Navigator.InlineActionRestoreAfterError)
                    .AppendJavaScriptObjectEnumField(JqGridOptionsNames.InlineNavigator.METHOD_TYPE, inlineNavigatorActionOptions.MethodType, JqGridOptionsDefaults.Navigator.InlineActionMethodType);
            }

            return javaScriptBuilder;
        }

        private static StringBuilder AppendNavigatorEditActionOptions(this StringBuilder javaScriptBuilder, string fieldName, JqGridNavigatorEditActionOptions navigatorEditActionOptions)
        {
            if ((navigatorEditActionOptions != null) && !navigatorEditActionOptions.AreDefault())
            {
                if (String.IsNullOrWhiteSpace(fieldName))
                {
                    javaScriptBuilder.AppendJavaScriptObjectOpening();
                }
                else
                {
                    javaScriptBuilder.AppendJavaScriptObjectFieldOpening(fieldName);
                }

                if ((navigatorEditActionOptions.SaveKeyEnabled != JqGridOptionsDefaults.Navigator.SaveKeyEnabled) || (navigatorEditActionOptions.SaveKey != JqGridOptionsDefaults.Navigator.SaveKey))
                {
                    javaScriptBuilder.AppendJavaScriptArrayFieldOpening(JqGridOptionsNames.Navigator.SAVE_KEY)
                    .AppendJavaScriptArrayBooleanValue(navigatorEditActionOptions.SaveKeyEnabled)
                    .AppendJavaScriptArrayIntegerValue(navigatorEditActionOptions.SaveKey)
                    .AppendJavaScriptArrayFieldClosing();
                }

                javaScriptBuilder.AppendNavigatorModifyActionOptions(navigatorEditActionOptions)
                    .AppendJavaScriptObjectIntegerField(JqGridOptionsNames.Navigator.WIDTH, navigatorEditActionOptions.Width, JqGridOptionsDefaults.Navigator.EditActionWidth)
                    .AppendJavaScriptObjectObjectField(JqGridOptionsNames.Navigator.AJAX_EDIT_OPTIONS, navigatorEditActionOptions.AjaxOptions)
                    .AppendJavaScriptObjectScriptOrObjectField(JqGridOptionsNames.Navigator.EDIT_EXTRA_DATA, navigatorEditActionOptions.ExtraDataScript, navigatorEditActionOptions.ExtraData)
                    .AppendJavaScriptObjectFunctionField(JqGridOptionsNames.Navigator.SERIALIZE_EDIT_DATA, navigatorEditActionOptions.SerializeData)
                    .AppendJavaScriptObjectEnumField(JqGridOptionsNames.Navigator.ADDED_ROW_POSITION, navigatorEditActionOptions.AddedRowPosition, JqGridOptionsDefaults.Navigator.AddedRowPosition)
                    .AppendJavaScriptObjectFunctionField(JqGridOptionsNames.Navigator.AFTER_CLICK_PG_BUTTONS, navigatorEditActionOptions.AfterClickPgButtons)
                    .AppendJavaScriptObjectFunctionField(JqGridOptionsNames.Navigator.AFTER_COMPLETE, navigatorEditActionOptions.AfterComplete)
                    .AppendJavaScriptObjectFunctionField(JqGridOptionsNames.Navigator.BEFORE_CHECK_VALUES, navigatorEditActionOptions.BeforeCheckValues)
                    .AppendJavaScriptObjectStringField(JqGridOptionsNames.Navigator.BOTTOM_INFO, navigatorEditActionOptions.BottomInfo)
                    .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.Navigator.CHECK_ON_SUBMIT, navigatorEditActionOptions.CheckOnSubmit, JqGridOptionsDefaults.Navigator.CheckOnSubmit)
                    .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.Navigator.CHECK_ON_UPDATE, navigatorEditActionOptions.CheckOnUpdate, JqGridOptionsDefaults.Navigator.CheckOnUpdate)
                    .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.Navigator.CLEAR_AFTER_ADD, navigatorEditActionOptions.ClearAfterAdd, JqGridOptionsDefaults.Navigator.ClearAfterAdd)
                    .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.Navigator.CLOSE_AFTER_ADD, navigatorEditActionOptions.CloseAfterAdd, JqGridOptionsDefaults.Navigator.CloseAfterAdd)
                    .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.Navigator.CLOSE_AFTER_EDIT, navigatorEditActionOptions.CloseAfterEdit, JqGridOptionsDefaults.Navigator.CloseAfterEdit)
                    .AppendFormButtonIcon(JqGridOptionsNames.Navigator.CLOSE_ICON, navigatorEditActionOptions.CloseButtonIcon, JqGridFormButtonIcon.CloseIcon)
                    .AppendJavaScriptObjectFunctionField(JqGridOptionsNames.Navigator.ERROR_TEXT_FORMAT, navigatorEditActionOptions.ErrorTextFormat)
                    .AppendNavigatorPageableFormActionOptions(navigatorEditActionOptions)
                    .AppendJavaScriptObjectFunctionField(JqGridOptionsNames.Navigator.ON_CLICK_PG_BUTTONS, navigatorEditActionOptions.OnClickPgButtons)
                    .AppendJavaScriptObjectFunctionField(JqGridOptionsNames.Navigator.ON_INITIALIZE_FORM, navigatorEditActionOptions.OnInitializeForm)
                    .AppendFormButtonIcon(JqGridOptionsNames.Navigator.SAVE_ICON, navigatorEditActionOptions.SaveButtonIcon, JqGridFormButtonIcon.SaveIcon)
                    .AppendJavaScriptObjectStringField(JqGridOptionsNames.Navigator.TOP_INFO, navigatorEditActionOptions.TopInfo)
                    .AppendJavaScriptObjectFieldClosing();
            }

            return javaScriptBuilder;
        }

        private static StringBuilder AppendNavigatorDeleteActionOptions(this StringBuilder javaScriptBuilder, string fieldName, JqGridNavigatorDeleteActionOptions navigatorDeleteActionOptions)
        {
            if ((navigatorDeleteActionOptions != null) && !navigatorDeleteActionOptions.AreDefault())
            {
                if (String.IsNullOrWhiteSpace(fieldName))
                {
                    javaScriptBuilder.AppendJavaScriptObjectOpening();
                }
                else
                {
                    javaScriptBuilder.AppendJavaScriptObjectFieldOpening(fieldName);
                }

                javaScriptBuilder.AppendNavigatorModifyActionOptions(navigatorDeleteActionOptions)
                    .AppendJavaScriptObjectIntegerField(JqGridOptionsNames.Navigator.WIDTH, navigatorDeleteActionOptions.Width, JqGridOptionsDefaults.Navigator.DeleteActionWidth)
                    .AppendJavaScriptObjectObjectField(JqGridOptionsNames.Navigator.AJAX_DELETE_OPTIONS, navigatorDeleteActionOptions.AjaxOptions)
                    .AppendJavaScriptObjectScriptOrObjectField(JqGridOptionsNames.Navigator.DELETE_EXTRA_DATA, navigatorDeleteActionOptions.ExtraDataScript, navigatorDeleteActionOptions.ExtraData)
                    .AppendJavaScriptObjectFunctionField(JqGridOptionsNames.Navigator.SERIALIZE_DELETE_DATA, navigatorDeleteActionOptions.SerializeData)
                    .AppendFormButtonIcon(JqGridOptionsNames.Navigator.DELETE_ICON, navigatorDeleteActionOptions.DeleteButtonIcon, JqGridFormButtonIcon.DeleteIcon)
                    .AppendFormButtonIcon(JqGridOptionsNames.Navigator.CANCEL_ICON, navigatorDeleteActionOptions.CancelButtonIcon, JqGridFormButtonIcon.CancelIcon)
                    .AppendJavaScriptObjectFieldClosing();
            }

            return javaScriptBuilder;
        }

        private static StringBuilder AppendFormButtonIcon(this StringBuilder javaScriptBuilder, string formButtonIconName, JqGridFormButtonIcon formButtonIcon, JqGridFormButtonIcon defaultFormButtonIcon)
        {
            if ((formButtonIcon != null) && !formButtonIcon.Equals(defaultFormButtonIcon))
            {
                javaScriptBuilder.AppendJavaScriptArrayFieldOpening(formButtonIconName)
                    .AppendJavaScriptArrayBooleanValue(formButtonIcon.Enabled)
                    .AppendJavaScriptArrayEnumValue(formButtonIcon.Position)
                    .AppendJavaScriptArrayStringValue(formButtonIcon.Class)
                    .AppendJavaScriptArrayFieldClosing();
            }

            return javaScriptBuilder;
        }

        private static StringBuilder AppendNavigatorModifyActionOptions(this StringBuilder javaScriptBuilder, JqGridNavigatorModifyActionOptions navigatorModifyActionOptions)
        {
            javaScriptBuilder.AppendNavigatorFormActionOptions(navigatorModifyActionOptions);

            return javaScriptBuilder.AppendJavaScriptObjectFunctionField(JqGridOptionsNames.Navigator.AFTER_SHOW_FORM, navigatorModifyActionOptions.AfterShowForm)
                .AppendJavaScriptObjectFunctionField(JqGridOptionsNames.Navigator.AFTER_SUBMIT, navigatorModifyActionOptions.AfterSubmit)
                .AppendJavaScriptObjectFunctionField(JqGridOptionsNames.Navigator.BEFORE_SUBMIT, navigatorModifyActionOptions.BeforeSubmit)
                .AppendJavaScriptObjectEnumField(JqGridOptionsNames.Navigator.METHOD_TYPE, navigatorModifyActionOptions.MethodType, JqGridOptionsDefaults.Navigator.MethodType)
                .AppendJavaScriptObjectFunctionField(JqGridOptionsNames.Navigator.ON_CLICK_SUBMIT, navigatorModifyActionOptions.OnClickSubmit)
                .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.Navigator.RELOAD_AFTER_SUBMIT, navigatorModifyActionOptions.ReloadAfterSubmit, JqGridOptionsDefaults.Navigator.ReloadAfterSubmit)
                .AppendJavaScriptObjectStringField(JqGridOptionsNames.Navigator.URL, navigatorModifyActionOptions.Url);
        }

        private static StringBuilder AppendNavigatorFormActionOptions(this StringBuilder javaScriptBuilder, JqGridNavigatorFormActionOptions navigatorFormActionOptions)
        {
            javaScriptBuilder.AppendNavigatorActionOptions(navigatorFormActionOptions);

            return javaScriptBuilder.AppendJavaScriptObjectFunctionField(JqGridOptionsNames.Navigator.BEFORE_INIT_DATA, navigatorFormActionOptions.BeforeInitData)
                .AppendJavaScriptObjectFunctionField(JqGridOptionsNames.Navigator.BEFORE_SHOW_FORM, navigatorFormActionOptions.BeforeShowForm);
        }

        private static StringBuilder AppendNavigatorPageableFormActionOptions(this StringBuilder javaScriptBuilder, IJqGridNavigatorPageableFormActionOptions navigatorPageableFormActionOptions)
        {
            if ((navigatorPageableFormActionOptions.NavigationKeys != null) && navigatorPageableFormActionOptions.NavigationKeys.IsDefault())
            {
                javaScriptBuilder.AppendJavaScriptArrayFieldOpening(JqGridOptionsNames.Navigator.NAVIGATION_KEYS)
                    .AppendJavaScriptArrayBooleanValue(navigatorPageableFormActionOptions.NavigationKeys.Enabled)
                    .AppendJavaScriptArrayIntegerValue(navigatorPageableFormActionOptions.NavigationKeys.RecordDown)
                    .AppendJavaScriptArrayIntegerValue(navigatorPageableFormActionOptions.NavigationKeys.RecordUp)
                    .AppendJavaScriptArrayFieldClosing();
            }

            return javaScriptBuilder.AppendJavaScriptObjectBooleanField(JqGridOptionsNames.Navigator.VIEW_PAGER_BUTTONS, navigatorPageableFormActionOptions.ViewPagerButtons, JqGridOptionsDefaults.Navigator.ViewPagerButtons)
                .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.Navigator.RECREATE_FORM, navigatorPageableFormActionOptions.RecreateForm, JqGridOptionsDefaults.Navigator.RecreateForm);
        }

        private static StringBuilder AppendNavigatorActionOptions(this StringBuilder javaScriptBuilder, JqGridNavigatorActionOptions navigatorActionOptions)
        {
            return javaScriptBuilder.AppendJavaScriptObjectBooleanField(JqGridOptionsNames.Navigator.CLOSE_ON_ESCAPE, navigatorActionOptions.CloseOnEscape, JqGridOptionsDefaults.Navigator.CloseOnEscape)
                .AppendJavaScriptObjectIntegerField(JqGridOptionsNames.Navigator.DATA_HEIGHT, navigatorActionOptions.DataHeight)
                .AppendJavaScriptObjectIntegerField(JqGridOptionsNames.Navigator.DATA_WIDTH, navigatorActionOptions.DataWidth)
                .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.Navigator.DRAGABLE, navigatorActionOptions.Dragable, JqGridOptionsDefaults.Navigator.Dragable)
                .AppendJavaScriptObjectIntegerField(JqGridOptionsNames.Navigator.HEIGHT, navigatorActionOptions.Height)
                .AppendJavaScriptObjectIntegerField(JqGridOptionsNames.Navigator.LEFT, navigatorActionOptions.Left, JqGridOptionsDefaults.Navigator.Left)
                .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.Navigator.MODAL, navigatorActionOptions.Modal, JqGridOptionsDefaults.Navigator.Modal)
                .AppendJavaScriptObjectFunctionField(JqGridOptionsNames.Navigator.ON_CLOSE, navigatorActionOptions.OnClose)
                .AppendJavaScriptObjectIntegerField(JqGridOptionsNames.Navigator.OVERLAY, navigatorActionOptions.Overlay, JqGridOptionsDefaults.Navigator.Overlay)
                .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.Navigator.RESIZE, navigatorActionOptions.Resizable, JqGridOptionsDefaults.Navigator.Resizable)
                .AppendJavaScriptObjectIntegerField(JqGridOptionsNames.Navigator.TOP, navigatorActionOptions.Top, JqGridOptionsDefaults.Navigator.Top)
                .AppendJavaScriptObjectBooleanField(JqGridOptionsNames.Navigator.USE_JQ_MODAL, navigatorActionOptions.UseJqModal, JqGridOptionsDefaults.Navigator.UseJqModal);
        }
        #endregion
    }
}
