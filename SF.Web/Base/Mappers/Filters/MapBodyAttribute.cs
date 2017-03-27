using AutoMapper;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Web.Base.Mappers.Filters
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class MapBodyAttribute : Attribute, IFilterFactory
    {
        public MapBodyAttribute(Type from, Type to)
        {
            SourceType = from;
            DestinationType = to;
        }

        public Type SourceType { get; private set; }
        public Type DestinationType { get; private set; }

        public bool IsReusable
        {
            get { return false; }
        }

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return new MapBodyFilter(serviceProvider.GetService<IMapper>(), SourceType, DestinationType);
        }

        private class MapBodyFilter : IActionFilter, IResourceFilter
        {
            public MapBodyFilter(IMapper mapper, Type sourceType, Type destinationType)
            {
                Mapper = mapper;
                SourceType = sourceType;
                DestinationType = destinationType;
            }

            public IMapper Mapper { get; private set; }
            public Type SourceType { get; private set; }
            public Type DestinationType { get; private set; }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                // Get the body parameter
                var bodyParam = context.ActionDescriptor.Parameters.FirstOrDefault(x => x.BindingInfo.BindingSource.Id.Equals("Body"));
                if (bodyParam == null) return;

                // Revert the type of the body parameter
                bodyParam.ParameterType = DestinationType;

                // Get the correct name of the body parameter on the controller action
                var bodyParamName = bodyParam.Name;
                if (string.IsNullOrEmpty(bodyParamName)) return;

                // Get the body parameter value
                if (!context.ActionArguments.ContainsKey(bodyParamName)) return;
                var bodyParamValue = context.ActionArguments[bodyParamName];

                // Map the body parameter value
                var mappedParamValue = Mapper.Map(bodyParamValue, SourceType, DestinationType);

                // Replace the original value with the mapped value
                context.ActionArguments[bodyParamName] = mappedParamValue;
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
            }

            public void OnResourceExecuting(ResourceExecutingContext context)
            {
                // Get the body parameter
                var bodyParam = context.ActionDescriptor.Parameters.FirstOrDefault(x => x.BindingInfo.BindingSource.Id.Equals("Body"));
                if (bodyParam == null) return;

                // Mislead model binders and other dependencies that the controller action requests the mapped model when the MapBody attribute is used.
                bodyParam.ParameterType = SourceType;
            }

            public void OnResourceExecuted(ResourceExecutedContext context)
            {
            }
        }
    }
}
