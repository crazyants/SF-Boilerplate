using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace SimpleFramework.Web.Control.Lookup
{
    public abstract class MvcLookup<T> : MvcLookup where T : class
    {
        public Func<T, String> Id { get; set; }
        public Func<T, String> Autocomplete { get; set; }
        public virtual IEnumerable<PropertyInfo> AttributedProperties
        {
            get
            {
                return typeof(T)
                    .GetProperties()
                    .Where(property => property.IsDefined(typeof(LookupColumnAttribute), false))
                    .OrderBy(property => property.GetCustomAttribute<LookupColumnAttribute>(false).Position);
            }
        }

        protected MvcLookup()
        {
            Id = (model) => GetValue(model, "Id");
            Autocomplete = (model) => GetValue(model, Columns.Where(col => !col.Hidden).Select(col => col.Key).FirstOrDefault() ?? "");

            foreach (PropertyInfo property in AttributedProperties)
                Columns.Add(new LookupColumn(GetColumnKey(property), GetColumnHeader(property))
                {
                    Hidden = property.GetCustomAttribute<LookupColumnAttribute>(false).Hidden,
                    CssClass = GetColumnCssClass(property)
                });
        }
        public virtual String GetColumnKey(PropertyInfo property)
        {
            if (property == null)
                throw new ArgumentNullException(nameof(property));

            return property.Name;
        }
        public virtual String GetColumnHeader(PropertyInfo property)
        {
            return property?.GetCustomAttribute<DisplayAttribute>(false)?.GetShortName();
        }
        public virtual String GetColumnCssClass(PropertyInfo property)
        {
            return null;
        }

        public override LookupData GetData()
        {
            IQueryable<T> models = GetModels();
            models = FilterByRequest(models);
            models = Sort(models);

            return FormLookupData(models);
        }
        public abstract IQueryable<T> GetModels();

        private IQueryable<T> FilterByRequest(IQueryable<T> models)
        {
            if (Filter.Id != null)
                return FilterById(models);

            if (Filter.AdditionalFilters.Count > 0)
                models = FilterByAdditionalFilters(models);

            return FilterBySearch(models);
        }
        public virtual IQueryable<T> FilterById(IQueryable<T> models)
        {
            PropertyInfo key = typeof(T).GetProperties()
                .FirstOrDefault(prop => prop.IsDefined(typeof(KeyAttribute))) ?? typeof(T).GetProperty("Id");

            if (key == null)
                throw new LookupException($"'{typeof(T).Name}' type does not have key or property named 'Id', required for automatic id filtering.");

            if (key.PropertyType == typeof(String))
                return models.Where($"{key.Name} = @0", Filter.Id);

            Decimal id;
            if (IsNumeric(key.PropertyType) && Decimal.TryParse(Filter.Id, out id))
                return models.Where($"{key.Name} = @0", id);

            throw new LookupException($"'{typeof(T).Name}.{key.Name}' property type has to be a string or a number.");
        }
        public virtual IQueryable<T> FilterBySearch(IQueryable<T> models)
        {
            if (String.IsNullOrEmpty(Filter.Search))
                return models;

            List<String> queries = new List<String>();
            foreach (String property in Columns.Where(column => !column.Hidden).Select(column => column.Key))
                if (typeof(T).GetProperty(property)?.PropertyType == typeof(String))
                    queries.Add($"({property} != null && {property}.ToLower().Contains(@0))");

            if (queries.Count == 0) return models;

            return models.Where(String.Join(" || ", queries), Filter.Search.ToLower());
        }
        public virtual IQueryable<T> FilterByAdditionalFilters(IQueryable<T> models)
        {
            foreach (KeyValuePair<String, Object> filter in Filter.AdditionalFilters.Where(item => item.Value != null))
                if (filter.Value is IEnumerable)
                    models = models.Where($"({filter.Key} != null && @0.Contains({filter.Key}))", filter.Value);
                else
                    models = models.Where($"({filter.Key} != null && {filter.Key} == @0)", filter.Value);

            return models;
        }

        public virtual IQueryable<T> Sort(IQueryable<T> models)
        {
            String column = Filter.SortColumn ?? Columns.Where(col => !col.Hidden).Select(col => col.Key).FirstOrDefault();

            if (String.IsNullOrWhiteSpace(column))
                return models;

            return models.OrderBy(column + " " + Filter.SortOrder);
        }

        public virtual LookupData FormLookupData(IQueryable<T> models)
        {
            LookupData data = new LookupData();
            data.FilteredRows = models.Count();
            data.Columns = Columns;

            IQueryable<T> pagedModels = models
                .Skip(Filter.Page * Filter.Rows)
                .Take(Math.Min(Filter.Rows, 99));

            foreach (T model in pagedModels)
            {
                Dictionary<String, String> row = new Dictionary<String, String>();
                AddId(row, model);
                AddAutocomplete(row, model);
                AddData(row, model);

                data.Rows.Add(row);
            }

            return data;
        }
        public virtual void AddId(Dictionary<String, String> row, T model)
        {
            row.Add(IdKey, Id(model));
        }
        public virtual void AddAutocomplete(Dictionary<String, String> row, T model)
        {
            row.Add(AcKey, Autocomplete(model));
        }
        public virtual void AddData(Dictionary<String, String> row, T model)
        {
            foreach (LookupColumn column in Columns)
                row[column.Key] = GetValue(model, column.Key);
        }

        private String GetValue(T model, String propertyName)
        {
            PropertyInfo property = typeof(T).GetProperty(propertyName);
            if (property == null) return null;

            LookupColumnAttribute column = property.GetCustomAttribute<LookupColumnAttribute>(false);
            if (column?.Format != null) return String.Format(column.Format, property.GetValue(model));

            return property.GetValue(model)?.ToString();
        }
        private Boolean IsNumeric(Type type)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    return true;
                default:
                    return false;
            }
        }
    }
}
