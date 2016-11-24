
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using SF.Core.Web.Base.Mapper.Extensions;
using SF.Core.Web.Base.Mapper.Filters;
using SF.Core.Web.Base.Mapper.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.Web.Base.Mapper.DescriptionProviders
{
    public class MapBodyApiDescriptionProvider : IApiDescriptionProvider
    {
        public MapBodyApiDescriptionProvider(IOptions<DescriptionProviderOptions<MapBodyApiDescriptionProvider>> options)
        {
            Order = options?.Value?.Order ?? 0;
        }

        public int Order { get; private set; }

        public void OnProvidersExecuting(ApiDescriptionProviderContext context)
        {
            // Update the api description
            foreach (var apiDescription in context.Results)
            {
                var mapBodyAttribute = apiDescription.ActionDescriptor.GetCustomControllerActionAttribute<MapBodyAttribute>();

                if (mapBodyAttribute != null)
                {
                    var bodyParam = apiDescription.ParameterDescriptions.FirstOrDefault(x => x.Source.Id.Equals("Body"));
                    if (bodyParam != null)
                    {
                        bodyParam.Type = mapBodyAttribute.SourceType;
                    }
                }
            }
        }

        public void OnProvidersExecuted(ApiDescriptionProviderContext context)
        {
        }
    }
}
