using SF.Web.Control.Pagination;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCloudscribePagination(this IServiceCollection services)
        {
            
            services.TryAddTransient<IBuildPaginationLinks, PaginationLinkBuilder>();

            return services;
        }
    }
}
