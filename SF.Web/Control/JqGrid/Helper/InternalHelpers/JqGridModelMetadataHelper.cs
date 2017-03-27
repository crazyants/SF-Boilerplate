using System;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SF.Web.Control.JqGrid.Helper.Options;
using SF.Web.Control.JqGrid.DataAnnotations;
using SF.Web.Control.JqGrid.Infrastructure.Options;
using SF.Web.Control.JqGrid.Infrastructure.Options.ColumnModel;
using SF.Web.Control.JqGrid.Helper.Options.Subgrid;
using SF.Web.Control.JqGrid.Infrastructure.Enums;
using SF.Web.Control.JqGrid.Infrastructure.Constants;
using SF.Web.Control.JqGrid.Infrastructure.Options.Subgrid;

namespace SF.Web.Control.JqGrid.Helper.InternalHelpers
{
    internal static class JqGridModelMetadataHelper
    {
        #region Extension Methods
        internal static void ApplyModelMetadata(this JqGridOptions options, IModelMetadataProvider metadataProvider)
        {
            Type jqGridOptionsType = options.GetType();
            if (jqGridOptionsType.IsConstructedGenericType && jqGridOptionsType.GetGenericTypeDefinition() == typeof(JqGridOptions<>))
            {
                foreach (ModelMetadata columnMetadata in metadataProvider.GetMetadataForProperties(jqGridOptionsType.GenericTypeArguments[0]))
                {
                    if (IsValidForColumn(columnMetadata))
                    {
                        options.AddColumn(columnMetadata.GetDisplayName(), CreateJqGridColumnModel(columnMetadata));
                    }
                }
            }

            if (options.SubgridEnabled && (options.SubgridModel != null))
            {
                options.SubgridModel.ApplyModelMetadata(metadataProvider);
            }
        }
        #endregion

        #region Private Methods
        private static bool IsValidForColumn(ModelMetadata columnMetadata)
        {
            return columnMetadata.ShowForDisplay && (!columnMetadata.IsComplexType || columnMetadata.ModelType == typeof(byte[]));
        }

        private static JqGridColumnModel CreateJqGridColumnModel(ModelMetadata columnMetadata)
        {
            JqGridColumnModel columnModel = new JqGridColumnModel(columnMetadata.PropertyName);

            TimestampAttribute timeStampAttribute = null;
            JqGridColumnLayoutAttribute jqGridColumnLayoutAttribute = null;
            JqGridColumnSortableAttribute jqGridColumnSortableAttribute = null;
            JqGridColumnFormatterAttribute jqGridColumnFormatterAttribute = null;
            JqGridColumnSummaryAttribute jqGridColumnSummaryAttribute = null;

            foreach (Attribute customAttribute in columnMetadata.ContainerType.GetProperty(columnMetadata.PropertyName).GetCustomAttributes(true))
            {
                timeStampAttribute = (customAttribute as TimestampAttribute) ?? timeStampAttribute;
                jqGridColumnLayoutAttribute = (customAttribute as JqGridColumnLayoutAttribute) ?? jqGridColumnLayoutAttribute;
                jqGridColumnSortableAttribute = (customAttribute as JqGridColumnSortableAttribute) ?? jqGridColumnSortableAttribute;
                jqGridColumnFormatterAttribute = (customAttribute as JqGridColumnFormatterAttribute) ?? jqGridColumnFormatterAttribute;
                jqGridColumnSummaryAttribute = (customAttribute as JqGridColumnSummaryAttribute) ?? jqGridColumnSummaryAttribute;
            }

            if (timeStampAttribute != null)
            { }
            else
            {
                columnModel = SetLayoutOptions(columnModel, jqGridColumnLayoutAttribute);
                columnModel = SetSortOptions(columnModel, jqGridColumnSortableAttribute);
                columnModel = SetFormatterOptions(columnModel, jqGridColumnFormatterAttribute);
                columnModel = SetSummaryOptions(columnModel, jqGridColumnSummaryAttribute);
            }

            return columnModel;
        }

        private static JqGridColumnModel SetLayoutOptions(JqGridColumnModel columnModel, JqGridColumnLayoutAttribute jqGridColumnLayoutAttribute)
        {
            if (jqGridColumnLayoutAttribute != null)
            {
                columnModel.Alignment = jqGridColumnLayoutAttribute.Alignment;
                columnModel.CellAttributes = jqGridColumnLayoutAttribute.CellAttributes;
                columnModel.Classes = jqGridColumnLayoutAttribute.Classes;
                columnModel.Fixed = jqGridColumnLayoutAttribute.Fixed;
                columnModel.Frozen = jqGridColumnLayoutAttribute.Frozen;
                columnModel.HideInDialog = jqGridColumnLayoutAttribute.HideInDialog;
                columnModel.Resizable = jqGridColumnLayoutAttribute.Resizable;
                columnModel.Title = jqGridColumnLayoutAttribute.Title;
                columnModel.Width = jqGridColumnLayoutAttribute.Width;
                columnModel.Viewable = jqGridColumnLayoutAttribute.Viewable;
            }

            return columnModel;
        }

        private static JqGridColumnModel SetSortOptions(JqGridColumnModel columnModel, JqGridColumnSortableAttribute jqGridColumnSortableAttribute)
        {
            if (jqGridColumnSortableAttribute != null)
            {
                columnModel.Index = jqGridColumnSortableAttribute.Index;
                columnModel.InitialSortingOrder = jqGridColumnSortableAttribute.InitialSortingOrder;
                columnModel.Sortable = jqGridColumnSortableAttribute.Sortable;
                columnModel.SortType = jqGridColumnSortableAttribute.SortType;
                columnModel.SortFunction = jqGridColumnSortableAttribute.SortFunction;
            }

            return columnModel;
        }

        private static JqGridColumnModel SetFormatterOptions(JqGridColumnModel columnModel, JqGridColumnFormatterAttribute jqGridColumnFormatterAttribute)
        {
            if (jqGridColumnFormatterAttribute != null)
            {
                columnModel.Formatter = jqGridColumnFormatterAttribute.Formatter;
                columnModel.FormatterOptions = jqGridColumnFormatterAttribute.FormatterOptions;
                columnModel.UnFormatter = jqGridColumnFormatterAttribute.UnFormatter;
            }

            return columnModel;
        }

        private static JqGridColumnModel SetSummaryOptions(JqGridColumnModel columnModel, JqGridColumnSummaryAttribute jqGridColumnSummaryAttribute)
        {
            if (jqGridColumnSummaryAttribute != null)
            {
                columnModel.SummaryType = jqGridColumnSummaryAttribute.Type;
                columnModel.SummaryTemplate = jqGridColumnSummaryAttribute.Template;
                columnModel.SummaryFunction = jqGridColumnSummaryAttribute.Function;
            }

            return columnModel;
        }

        private static void ApplyModelMetadata(this JqGridSubgridModel subgridModel, IModelMetadataProvider metadataProvider)
        {
            Type jqGridsubgridModelType = subgridModel.GetType();
            if (jqGridsubgridModelType.IsConstructedGenericType && jqGridsubgridModelType.GetGenericTypeDefinition() == typeof(JqGridSubgridModel<>))
            {
                foreach (ModelMetadata columnMetadata in metadataProvider.GetMetadataForProperties(jqGridsubgridModelType.GenericTypeArguments[0]))
                {
                    if (IsValidForColumn(columnMetadata))
                    {
                        subgridModel.AddColumn(CreateJqGridSubgridColumnModel(columnMetadata));
                    }
                }
            }
        }

        private static JqGridSubgridColumnModel CreateJqGridSubgridColumnModel(ModelMetadata columnMetadata)
        {
            JqGridAlignments alignment = JqGridOptionsDefaults.ColumnModel.Alignment;
            int width = JqGridOptionsDefaults.ColumnModel.Width;

            foreach (Attribute customAttribute in columnMetadata.ContainerType.GetProperty(columnMetadata.PropertyName).GetCustomAttributes(true))
            {
                JqGridColumnLayoutAttribute jqGridColumnLayoutAttribute = (customAttribute as JqGridColumnLayoutAttribute);

                if (jqGridColumnLayoutAttribute != null)
                {
                    alignment = jqGridColumnLayoutAttribute.Alignment;
                    width = jqGridColumnLayoutAttribute.Width;
                    break;
                }
            }

            return new JqGridSubgridColumnModel(columnMetadata.GetDisplayName(), alignment, width, columnMetadata.PropertyName);
        }
        #endregion
    }
}
