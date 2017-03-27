using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SF.Web.Base.Mappers.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SF.Web.Base.Mappers.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class MapResultAttribute : Attribute, IFilterFactory
    {
        private readonly Type _sourceType;
        private readonly Type _destinationType;

        public MapResultAttribute()
        {
            _sourceType = null;
            _destinationType = null;
        }

        public MapResultAttribute(Type to)
        {
            _sourceType = null;
            _destinationType = to;
        }

        public MapResultAttribute(Type from, Type to)
        {
            _sourceType = from;
            _destinationType = to;
        }

        public bool IsReusable
        {
            get { return false; }
        }

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return new MapResultFilter(serviceProvider.GetService<IOptions<MapResultOptions>>(), serviceProvider.GetService<IMapper>(), _destinationType, _sourceType);
        }

        private class MapResultFilter : IResultFilter
        {
            public MapResultFilter(IOptions<MapResultOptions> options, IMapper mapper, Type destinationType, Type sourceType = null)
            {
                Options = options;
                Mapper = mapper;
                SourceType = sourceType;
                DestinationType = destinationType;
            }

            public IOptions<MapResultOptions> Options { get; private set; }
            public IMapper Mapper { get; private set; }
            public Type SourceType { get; private set; }
            public Type DestinationType { get; private set; }

            public void OnResultExecuting(ResultExecutingContext context)
            {
                var result = context.Result as ObjectResult;
                if (result == null || !result.StatusCode.HasValue || (result.StatusCode != (int)HttpStatusCode.Created && result.StatusCode != (int)HttpStatusCode.OK)) return;

                if (SourceType == null)
                {
                    SourceType = result.Value.GetType();
                }

                if (DestinationType == null)
                {
                    DestinationType = MapType(SourceType, false);
                }

                result.Value = Mapper.Map(result.Value, SourceType, DestinationType);
            }

            private Type MapType(Type source, bool useSourceIfMappingNotFound)
            {
                Type destination = null;

                if (Options?.Value == null) throw new Exception($"No mapping configuration defined for MapResult filter.");

                if (Options.Value.IsMappingDefined(source))
                {
                    destination = Options.Value.GetMapping(source);
                }
                else if (source.IsConstructedGenericType && Options.Value.IsGenericMappingDefined(source.GetGenericTypeDefinition()))
                {
                    var temp = Options.Value.GetGenericMapping(source.GetGenericTypeDefinition());
                    var genericTypeArguments = new List<Type>();

                    foreach (var type in source.GenericTypeArguments)
                    {
                        genericTypeArguments.Add(MapType(type, true));
                    }

                    destination = temp.MakeGenericType(genericTypeArguments.ToArray());
                }
                else
                {
                    if (useSourceIfMappingNotFound)
                        destination = source;
                    else
                        throw new Exception($"No mapping found for type '{source.FullName}'.");
                }

                return destination;
            }

            public void OnResultExecuted(ResultExecutedContext context)
            {
            }
        }
    }
}
