using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Concurrent;
using System.Reflection;
using Omu.ValueInjecter.Injections;
using Omu.ValueInjecter.Utils;


namespace SF.Core.Security.Converters
{
    /// <summary>
    /// Allows to inject only specified properties not contains null value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PatchInjection<T> : ValueInjection
    {
        private bool _injectNullValue = false;
        private List<string> _propertyNames = new List<string>();
        private class Path
        {
            public IDictionary<string, string> MatchingProps { get; set; }
        }

        private static readonly ConcurrentDictionary<Type, ConcurrentDictionary<Tuple<Type, Type, string>, Path>> WasLearned = new ConcurrentDictionary<Type, ConcurrentDictionary<Tuple<Type, Type, string>, Path>>();

        protected string[] ignoredProps;

        public PatchInjection(params Expression<Func<T, object>>[] propertyNames)
            : this(injectNullValues: false, propertyNames: propertyNames)
        {
        }
        public PatchInjection(bool injectNullValues = false, params Expression<Func<T, object>>[] propertyNames)
        {
            _injectNullValue = injectNullValues;
            if (propertyNames != null)
            {
                foreach (var lambda in propertyNames.Select(x => (LambdaExpression)x))
                {
                    MemberExpression memberExpression;
                    if (lambda.Body is UnaryExpression)
                    {
                        var unaryExpression = (UnaryExpression)lambda.Body;
                        memberExpression = (MemberExpression)unaryExpression.Operand;
                    }
                    else
                    {
                        memberExpression = (MemberExpression)lambda.Body;
                    }
                    _propertyNames.Add(memberExpression.Member.Name);
                }
            }
        }

        protected virtual bool Match(PropertyInfo sourceProp, PropertyInfo targetProp)
        {
            var retVal = sourceProp.Name == targetProp.Name &&
                        sourceProp.PropertyType == targetProp.PropertyType && (_injectNullValue ? true : sourceProp.GetConstantValue() != null)
                        && _propertyNames.Contains(sourceProp.Name);
            return retVal;
        }
        protected virtual void SetValue(PropertyInfo prop, object component, object value)
        {
            prop.SetValue(component, value);
        }

        protected virtual object GetValue(PropertyInfo prop, object component)
        {
            return prop.GetValue(component);
        }

        protected virtual void ExecuteMatch(PropertyInfo sourceProp, PropertyInfo targetProp, object source, object target)
        {
            SetValue(targetProp, target, GetValue(sourceProp, source));
        }

        private Path Learn(IEnumerable<PropertyInfo> sourceProps, IEnumerable<PropertyInfo> targetProps)
        {
            Path path = null;

            foreach (var sourceProp in sourceProps)
            {
                foreach (var targetProp in targetProps)
                {
                    if (!Match(sourceProp, targetProp)) continue;

                    if (path == null)
                        path = new Path
                        {
                            MatchingProps = new Dictionary<string, string> { { sourceProp.Name, targetProp.Name } }
                        };
                    else path.MatchingProps.Add(sourceProp.Name, targetProp.Name);
                }
            }

            return path;
        }
        protected override void Inject(object source, object target)
        {
            var sourceType = source.GetType();
            var targetType = target.GetType();

            var sourceProps = sourceType.GetProps();
            var targetProps = targetType.GetProps();

            var cacheEntry = WasLearned.GetOrAdd(GetType(), new ConcurrentDictionary<Tuple<Type, Type, string>, Path>());

            var ignoreds = ignoredProps == null ? null : string.Join(",", ignoredProps);

            var path = cacheEntry.GetOrAdd(new Tuple<Type, Type, string>(sourceType, targetType, ignoreds), pair => Learn(sourceProps, targetProps));

            if (path == null) return;

            foreach (var pair in path.MatchingProps)
            {
                ExecuteMatch(sourceType.GetProperty(pair.Key), targetType.GetProperty(pair.Value), source, target);
            }
        }

    }
}
