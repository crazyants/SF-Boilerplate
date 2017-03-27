using System;
using SF.Web.Control.JqGrid.Infrastructure.Constants;
using SF.Web.Control.JqGrid.Infrastructure.Options;
using SF.Web.Control.JqGrid.Infrastructure.Options.ColumnModel;
using SF.Web.Control.JqGrid.Infrastructure.Options.Navigator;

namespace SF.Web.Control.JqGrid.Helper.InternalHelpers
{
    internal static class JqGridOptionsHelper
    {
        #region Extension Methods
        internal static bool IsDefault(this JqGridJsonReader jsonReader)
        {
            return (jsonReader.PageIndex == JqGridOptionsDefaults.Response.PageIndex)
                && (jsonReader.RecordId == JqGridOptionsDefaults.Response.RecordId)
                && (jsonReader.TotalPagesCount == JqGridOptionsDefaults.Response.TotalPagesCount)
                && (jsonReader.TotalRecordsCount == JqGridOptionsDefaults.Response.TotalRecordsCount)
                && (jsonReader.UserData == JqGridOptionsDefaults.Response.UserData)
                && ((jsonReader.SubgridReader == null) || jsonReader.SubgridReader.IsDefault())
                && (jsonReader as JqGridJsonRecordsReader).IsDefault();
        }

        internal static bool IsDefault(this JqGridJsonRecordsReader jsonRecordsReader)
        {
            return (jsonRecordsReader.Records == JqGridOptionsDefaults.Response.Records)
                && (jsonRecordsReader.RecordValues == JqGridOptionsDefaults.Response.RecordValues)
                && (jsonRecordsReader.RepeatItems == JqGridOptionsDefaults.Response.RepeatItems);
        }

        internal static bool AreDefault(this JqGridParametersNames parametersNames)
        {
            return (parametersNames.PageIndex == JqGridOptionsDefaults.Request.PageIndex)
                && (parametersNames.RecordsCount == JqGridOptionsDefaults.Request.RecordsCount)
                && (parametersNames.SortingName == JqGridOptionsDefaults.Request.SortingName)
                && (parametersNames.SortingOrder == JqGridOptionsDefaults.Request.SortingOrder)
                && (parametersNames.Searching == JqGridOptionsDefaults.Request.Searching)
                && (parametersNames.Id == JqGridOptionsDefaults.Request.Id)
                && (parametersNames.Operator == JqGridOptionsDefaults.Request.Operator)
                && (parametersNames.EditOperator == JqGridOptionsDefaults.Request.EditOperator)
                && (parametersNames.AddOperator == JqGridOptionsDefaults.Request.AddOperator)
                && (parametersNames.DeleteOperator == JqGridOptionsDefaults.Request.DeleteOperator)
                && (parametersNames.SubgridId == JqGridOptionsDefaults.Request.SubgridId)
                && String.IsNullOrEmpty(parametersNames.PagesCount)
                && (parametersNames.TotalRows == JqGridOptionsDefaults.Request.TotalRows);
        }

        internal static bool AreDefault(this JqGridColumnFormatterOptions formatterOptions, string formatter)
        {
            switch (formatter)
            {
                case JqGridPredefinedFormatters.Integer:
                    return (formatterOptions.DefaultValue == JqGridOptionsDefaults.ColumnModel.Formatter.IntegerDefaultValue)
                        && (formatterOptions.ThousandsSeparator == JqGridOptionsDefaults.ColumnModel.Formatter.ThousandsSeparator);
                case JqGridPredefinedFormatters.Number:
                    return (formatterOptions.DecimalPlaces == JqGridOptionsDefaults.ColumnModel.Formatter.DecimalPlaces)
                        && (formatterOptions.DecimalSeparator == JqGridOptionsDefaults.ColumnModel.Formatter.DecimalSeparator)
                        && (formatterOptions.DefaultValue == JqGridOptionsDefaults.ColumnModel.Formatter.NumberDefaultValue)
                        && (formatterOptions.ThousandsSeparator == JqGridOptionsDefaults.ColumnModel.Formatter.ThousandsSeparator);
                case JqGridPredefinedFormatters.Currency:
                    return (formatterOptions.DecimalPlaces == JqGridOptionsDefaults.ColumnModel.Formatter.DecimalPlaces)
                        && (formatterOptions.DecimalSeparator == JqGridOptionsDefaults.ColumnModel.Formatter.DecimalSeparator)
                        && (formatterOptions.DefaultValue == JqGridOptionsDefaults.ColumnModel.Formatter.CurrencyDefaultValue)
                        && String.IsNullOrWhiteSpace(formatterOptions.Prefix)
                        && String.IsNullOrWhiteSpace(formatterOptions.Suffix)
                        && (formatterOptions.ThousandsSeparator == JqGridOptionsDefaults.ColumnModel.Formatter.ThousandsSeparator);
                case JqGridPredefinedFormatters.Date:
                    return (formatterOptions.SourceFormat == JqGridOptionsDefaults.ColumnModel.Formatter.SourceFormat)
                        && (formatterOptions.OutputFormat == JqGridOptionsDefaults.ColumnModel.Formatter.OutputFormat);
                case JqGridPredefinedFormatters.Link:
                    return String.IsNullOrWhiteSpace(formatterOptions.Target);
                case JqGridPredefinedFormatters.ShowLink:
                    return String.IsNullOrWhiteSpace(formatterOptions.BaseLinkUrl)
                        && String.IsNullOrWhiteSpace(formatterOptions.ShowAction)
                        && String.IsNullOrWhiteSpace(formatterOptions.AddParam)
                        && String.IsNullOrWhiteSpace(formatterOptions.Target)
                        && (formatterOptions.IdName == JqGridOptionsDefaults.ColumnModel.Formatter.IdName);
                case JqGridPredefinedFormatters.CheckBox:
                    return (formatterOptions.Disabled == JqGridOptionsDefaults.ColumnModel.Formatter.Disabled);
                case JqGridPredefinedFormatters.Actions:
                    return (formatterOptions.EditButton == JqGridOptionsDefaults.ColumnModel.Formatter.EditButton)
                        && (formatterOptions.UseFormEditing == JqGridOptionsDefaults.ColumnModel.Formatter.UseFormEditing)
                        && ((formatterOptions.InlineEditingOptions == null) || formatterOptions.InlineEditingOptions.AreDefault())
                        && (formatterOptions.DeleteButton == JqGridOptionsDefaults.ColumnModel.Formatter.DeleteButton)
                        && ((formatterOptions.DeleteOptions == null) || formatterOptions.DeleteOptions.AreDefault());
                case JqGridPredefinedFormatters.JQueryUIButton:
                    return String.IsNullOrEmpty(formatterOptions.Label)
                        && (formatterOptions.Text == JqGridOptionsDefaults.ColumnModel.Formatter.Text)
                        && String.IsNullOrEmpty(formatterOptions.PrimaryIcon)
                        && String.IsNullOrEmpty(formatterOptions.SecondaryIcon);
                default:
                    return true;
            }
        }

        internal static bool AreDefault(this JqGridInlineNavigatorActionOptions inlineNavigatorActionOptions)
        {
            return (inlineNavigatorActionOptions.Keys == JqGridOptionsDefaults.Navigator.InlineActionKeys)
                && String.IsNullOrWhiteSpace(inlineNavigatorActionOptions.OnEditFunction)
                && String.IsNullOrWhiteSpace(inlineNavigatorActionOptions.SuccessFunction)
                && String.IsNullOrEmpty(inlineNavigatorActionOptions.Url)
                && (inlineNavigatorActionOptions.ExtraParam == null)
                && String.IsNullOrWhiteSpace(inlineNavigatorActionOptions.ExtraParamScript)
                && String.IsNullOrWhiteSpace(inlineNavigatorActionOptions.AfterSaveFunction)
                && String.IsNullOrWhiteSpace(inlineNavigatorActionOptions.ErrorFunction)
                && String.IsNullOrWhiteSpace(inlineNavigatorActionOptions.AfterRestoreFunction)
                && (inlineNavigatorActionOptions.RestoreAfterError == JqGridOptionsDefaults.Navigator.InlineActionRestoreAfterError)
                && (inlineNavigatorActionOptions.MethodType == JqGridOptionsDefaults.Navigator.InlineActionMethodType);
        }

        internal static bool AreDefault(this JqGridNavigatorEditActionOptions navigatorEditActionOptions)
        {
            return (navigatorEditActionOptions.AddedRowPosition == JqGridOptionsDefaults.Navigator.AddedRowPosition)
                && String.IsNullOrWhiteSpace(navigatorEditActionOptions.AfterClickPgButtons)
                && String.IsNullOrWhiteSpace(navigatorEditActionOptions.AfterComplete)
                && String.IsNullOrWhiteSpace(navigatorEditActionOptions.BeforeCheckValues)
                && String.IsNullOrEmpty(navigatorEditActionOptions.BottomInfo)
                && (navigatorEditActionOptions.CheckOnSubmit == JqGridOptionsDefaults.Navigator.CheckOnSubmit)
                && (navigatorEditActionOptions.CheckOnUpdate == JqGridOptionsDefaults.Navigator.CheckOnUpdate)
                && (navigatorEditActionOptions.ClearAfterAdd == JqGridOptionsDefaults.Navigator.ClearAfterAdd)
                && (navigatorEditActionOptions.CloseAfterAdd == JqGridOptionsDefaults.Navigator.CloseAfterAdd)
                && (navigatorEditActionOptions.CloseAfterEdit == JqGridOptionsDefaults.Navigator.CloseAfterEdit)
                && (navigatorEditActionOptions.CloseButtonIcon == JqGridFormButtonIcon.CloseIcon)
                && String.IsNullOrWhiteSpace(navigatorEditActionOptions.ErrorTextFormat)
                && ((navigatorEditActionOptions.NavigationKeys == null) || navigatorEditActionOptions.NavigationKeys.IsDefault())
                && String.IsNullOrWhiteSpace(navigatorEditActionOptions.OnClickPgButtons)
                && String.IsNullOrWhiteSpace(navigatorEditActionOptions.OnInitializeForm)
                && (navigatorEditActionOptions.RecreateForm == JqGridOptionsDefaults.Navigator.RecreateForm)
                && (navigatorEditActionOptions.SaveButtonIcon == JqGridFormButtonIcon.SaveIcon)
                && (navigatorEditActionOptions.SaveKey == JqGridOptionsDefaults.Navigator.SaveKey)
                && (navigatorEditActionOptions.SaveKeyEnabled == JqGridOptionsDefaults.Navigator.SaveKeyEnabled)
                && String.IsNullOrEmpty(navigatorEditActionOptions.TopInfo)
                && (navigatorEditActionOptions.ViewPagerButtons == JqGridOptionsDefaults.Navigator.ViewPagerButtons)
                && (navigatorEditActionOptions.Width == JqGridOptionsDefaults.Navigator.EditActionWidth)
                && (navigatorEditActionOptions as JqGridNavigatorModifyActionOptions).AreDefault();
        }

        internal static bool AreDefault(this JqGridNavigatorDeleteActionOptions navigatorDeleteActionOptions)
        {
            return ((navigatorDeleteActionOptions.CancelButtonIcon == null) || navigatorDeleteActionOptions.CancelButtonIcon.Equals(JqGridFormButtonIcon.CancelIcon))
                && ((navigatorDeleteActionOptions.DeleteButtonIcon == null) || navigatorDeleteActionOptions.DeleteButtonIcon.Equals(JqGridFormButtonIcon.DeleteIcon))
                && (navigatorDeleteActionOptions.Width == JqGridOptionsDefaults.Navigator.DeleteActionWidth)
                && (navigatorDeleteActionOptions as JqGridNavigatorModifyActionOptions).AreDefault();
        }

        internal static bool IsDefault(this JqGridFormKeyboardNavigation formKeyboardNavigation)
        {
            return (formKeyboardNavigation.Enabled == JqGridOptionsDefaults.Navigator.KeyboardNavigation.Enabled)
                && (formKeyboardNavigation.RecordDown == JqGridOptionsDefaults.Navigator.KeyboardNavigation.RecordDown)
                && (formKeyboardNavigation.RecordUp == JqGridOptionsDefaults.Navigator.KeyboardNavigation.RecordUp);
        }

        private static bool AreDefault(this JqGridNavigatorModifyActionOptions navigatorModifyActionOptions)
        {
            return String.IsNullOrWhiteSpace(navigatorModifyActionOptions.AfterShowForm)
                && String.IsNullOrWhiteSpace(navigatorModifyActionOptions.AfterSubmit)
                && (navigatorModifyActionOptions.AjaxOptions == null)
                && String.IsNullOrWhiteSpace(navigatorModifyActionOptions.BeforeSubmit)
                && (navigatorModifyActionOptions.ExtraData == null)
                && String.IsNullOrWhiteSpace(navigatorModifyActionOptions.ExtraDataScript)
                && (navigatorModifyActionOptions.MethodType == JqGridOptionsDefaults.Navigator.MethodType)
                && String.IsNullOrWhiteSpace(navigatorModifyActionOptions.OnClickSubmit)
                && (navigatorModifyActionOptions.ReloadAfterSubmit == JqGridOptionsDefaults.Navigator.ReloadAfterSubmit)
                && String.IsNullOrWhiteSpace(navigatorModifyActionOptions.SerializeData)
                && String.IsNullOrEmpty(navigatorModifyActionOptions.Url)
                && (navigatorModifyActionOptions as JqGridNavigatorFormActionOptions).AreDefault();
        }

        private static bool AreDefault(this JqGridNavigatorFormActionOptions navigatorFormActionOptions)
        {
            return String.IsNullOrWhiteSpace(navigatorFormActionOptions.BeforeInitData)
                && String.IsNullOrWhiteSpace(navigatorFormActionOptions.BeforeShowForm)
                && (navigatorFormActionOptions as JqGridNavigatorActionOptions).AreDefault();
        }

        private static bool AreDefault(this JqGridNavigatorActionOptions navigatorActionOptions)
        {
            return (navigatorActionOptions.CloseOnEscape == JqGridOptionsDefaults.Navigator.CloseOnEscape)
                && (!navigatorActionOptions.DataHeight.HasValue)
                && (!navigatorActionOptions.DataWidth.HasValue)
                && (navigatorActionOptions.Dragable == JqGridOptionsDefaults.Navigator.Dragable)
                && (!navigatorActionOptions.Height.HasValue)
                && (navigatorActionOptions.Left == JqGridOptionsDefaults.Navigator.Left)
                && (navigatorActionOptions.Modal == JqGridOptionsDefaults.Navigator.Modal)
                && String.IsNullOrWhiteSpace(navigatorActionOptions.OnClose)
                && (navigatorActionOptions.Overlay == JqGridOptionsDefaults.Navigator.Overlay)
                && (navigatorActionOptions.Resizable == JqGridOptionsDefaults.Navigator.Resizable)
                && (navigatorActionOptions.Top == JqGridOptionsDefaults.Navigator.Top)
                && (navigatorActionOptions.UseJqModal == JqGridOptionsDefaults.Navigator.UseJqModal);
        } 
        #endregion
    }
}
