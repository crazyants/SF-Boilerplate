using SF.Web.Control.JqGrid.Core.Json.Converters;
using SF.Web.Control.JqGrid.Infrastructure.Enums;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace SF.Web.Control.JqGrid.Core.Request.ModelBinders
{
    /// <summary>
    /// An IModelBinder which binds JqGridRequest
    /// </summary>
    internal sealed class JqGridRequestModelBinder : IModelBinder
    {
        #region Fields
        private const string SEARCH_FILTERS_BINDING_KEY = "filters";
        private const string SEARCH_NAME_BINDING_KEY = "searchField";
        private const string SEARCH_VALUE_BINDING_KEY = "searchString";
        private const string SEARCH_OPERATOR_BINDING_KEY = "searchOper";
        #endregion

        #region Methods
        /// <summary>
        /// Attempts to bind a model.
        /// </summary>
        /// <param name="bindingContext">The binding context.</param>
        /// <returns>The result of the model binding process.</returns>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            string key = (bindingContext.IsTopLevelObject) ? (bindingContext.BinderModelName ?? String.Empty) : bindingContext.ModelName;

            if (bindingContext.ModelType == typeof(JqGridRequest))
            {
                JqGridRequest model = new JqGridRequest();
                model = BindPagingProperties(model, bindingContext);
                model = BindSortingProperties(model, bindingContext);
                model = BindSearchingProperties(model, bindingContext);

                bindingContext.Result = ModelBindingResult.Success(model);
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }

            return TaskCache.CompletedTask;
        }

        private JqGridRequest BindPagingProperties(JqGridRequest model, ModelBindingContext bindingContext)
        {
            if (!String.IsNullOrWhiteSpace(JqGridRequest.ParametersNames.PageIndex))
            {
                model.PageIndex = bindingContext.ValueProvider.GetValue(JqGridRequest.ParametersNames.PageIndex).ConvertTo<int>() - 1;
            }
            else
            {
                model.PageIndex = 0;
            }

            model.PagesCount = 1;
            if (!String.IsNullOrWhiteSpace(JqGridRequest.ParametersNames.PagesCount))
            {
                ValueProviderResult pagesCountValueResult = bindingContext.ValueProvider.GetValue(JqGridRequest.ParametersNames.PagesCount);
                if (pagesCountValueResult != ValueProviderResult.None)
                {
                    model.PagesCount = pagesCountValueResult.ConvertTo<int>();
                }
            }

            if (!String.IsNullOrWhiteSpace(JqGridRequest.ParametersNames.RecordsCount))
            {
                model.RecordsCount = bindingContext.ValueProvider.GetValue(JqGridRequest.ParametersNames.RecordsCount).ConvertTo<int>();
            }

            return model;
        }

        private JqGridRequest BindSortingProperties(JqGridRequest model, ModelBindingContext bindingContext)
        {
            if (!String.IsNullOrWhiteSpace(JqGridRequest.ParametersNames.SortingName))
            {
                string sortingName = bindingContext.ValueProvider.GetValue(JqGridRequest.ParametersNames.SortingName).ConvertTo<string>();

                if (!String.IsNullOrWhiteSpace(sortingName))
                {
                    model.SortingName = sortingName;
                }
            }

            if (!String.IsNullOrWhiteSpace(JqGridRequest.ParametersNames.SortingOrder))
            {
                JqGridSortingOrders sortingOrder = JqGridSortingOrders.Asc;
                string sortingOrderAsString = bindingContext.ValueProvider.GetValue(JqGridRequest.ParametersNames.SortingOrder).ConvertTo<string>();

                if (!String.IsNullOrWhiteSpace(sortingOrderAsString) && Enum.TryParse(sortingOrderAsString, true, out sortingOrder))
                {
                    model.SortingOrder = sortingOrder;
                }
            }

            return model;
        }

        private JqGridRequest BindSearchingProperties(JqGridRequest model, ModelBindingContext bindingContext)
        {
            model.Searching = false;
            if (!String.IsNullOrWhiteSpace(JqGridRequest.ParametersNames.Searching))
            {
                ValueProviderResult searchingValueResult = bindingContext.ValueProvider.GetValue(JqGridRequest.ParametersNames.Searching);
                if (searchingValueResult != ValueProviderResult.None)
                {
                    model.Searching = searchingValueResult.ConvertTo<bool>();
                }
            }

            model.SearchingFilter = null;
            model.SearchingFilters = null;

            if (model.Searching)
            {
                ValueProviderResult searchingFiltersValueResult = bindingContext.ValueProvider.GetValue(SEARCH_FILTERS_BINDING_KEY);
                if ((searchingFiltersValueResult != ValueProviderResult.None) && !String.IsNullOrWhiteSpace(searchingFiltersValueResult.FirstValue))
                {
                    JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
                    jsonSerializerSettings.Converters.Add(new JqGridRequestSearchingFiltersJsonConverter());

                    model.SearchingFilters = JsonConvert.DeserializeObject<JqGridRequestSearchingFilters>(searchingFiltersValueResult.ConvertTo<string>(), jsonSerializerSettings);
                }
                else
                {
                    string searchingName = bindingContext.ValueProvider.GetValue(SEARCH_NAME_BINDING_KEY).ConvertTo<string>();
                    string searchingValue = bindingContext.ValueProvider.GetValue(SEARCH_VALUE_BINDING_KEY).ConvertTo<string>();

                    JqGridSearchOperators searchingOperator = JqGridSearchOperators.Eq;
                    Enum.TryParse(bindingContext.ValueProvider.GetValue(SEARCH_OPERATOR_BINDING_KEY).ConvertTo<string>(), true, out searchingOperator);

                    if (!String.IsNullOrWhiteSpace(searchingName) && (!String.IsNullOrWhiteSpace(searchingValue) || ((searchingOperator & JqGridSearchOperators.NullOperators) != 0)))
                    {
                        model.SearchingFilter = new JqGridRequestSearchingFilter() { SearchingName = searchingName, SearchingOperator = searchingOperator, SearchingValue = searchingValue };
                    }
                }
            }

            return model;
        }
        #endregion
    }
}
