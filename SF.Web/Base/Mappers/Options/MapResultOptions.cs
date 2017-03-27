using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SF.Web.Base.Mappers.Options
{
    public class MapResultOptions
    {
        private Dictionary<Type, Type> _mappings;
        private Dictionary<Type, Type> _genericMappings;

        public void AddResultMapping<TSource, TDestination>()
        {
            AddMapping(typeof(TSource), typeof(TDestination));
        }

        public void AddMapping(Type source, Type destination)
        {
            if (source == null) throw new NullReferenceException(nameof(source));
            if (destination == null) throw new NullReferenceException(nameof(destination));

            if (_mappings != null && _mappings.ContainsKey(source)) throw new ArgumentException("There is already a mapping defined for this source type.", nameof(source));

            if (source.IsConstructedGenericType && source.GenericTypeArguments.Length == 0) throw new ArgumentException("If the source or destination is a generic type, the generic type arguments should be specified. Try to add a generic mapping instead.", nameof(source));
            if (destination.IsConstructedGenericType && destination.GenericTypeArguments.Length == 0) throw new ArgumentException("If the source or destination is a generic type, the generic type arguments should be specified. Try to add a generic mapping instead.", nameof(destination));

            if (_mappings == null) _mappings = new Dictionary<Type, Type>();

            _mappings.Add(source, destination);
        }

        public bool IsMappingDefined(Type source)
        {
            return _mappings.ContainsKey(source);
        }

        public Type GetMapping(Type source)
        {
            return _mappings[source];
        }

        public void AddGenericResultMapping(Type source, Type destination)
        {
            if (source == null) throw new NullReferenceException(nameof(source));
            if (destination == null) throw new NullReferenceException(nameof(destination));

            var sourceTypeInfo = source.GetTypeInfo();
            var destinationTypeInfo = source.GetTypeInfo();

            if (!sourceTypeInfo.IsGenericType) throw new ArgumentException("Both the source as destination type should be Generic types.", nameof(source));
            if (!destinationTypeInfo.IsGenericType) throw new ArgumentException("Both the source as destination type should be Generic types.", nameof(destination));

            if (source.GenericTypeArguments.Length != 0) throw new ArgumentException("No generic type arguments should be given to both the source as destination type.", nameof(source));
            if (destination.GenericTypeArguments.Length != 0) throw new ArgumentException("No generic type arguments should be given to both the source as destination type.", nameof(destination));

            if (_genericMappings != null && _genericMappings.ContainsKey(source)) throw new ArgumentException("There is already a generic mapping defined for this source type.", nameof(source));

            if (_genericMappings == null) _genericMappings = new Dictionary<Type, Type>();

            _genericMappings.Add(source, destination);
        }

        public bool IsGenericMappingDefined(Type source)
        {
            return _genericMappings.ContainsKey(source);
        }

        public Type GetGenericMapping(Type source)
        {
            return _genericMappings[source];
        }
    }
}
