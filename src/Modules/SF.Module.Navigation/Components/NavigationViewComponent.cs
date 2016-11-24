
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using SimpleFramework.Web.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleFramework.Module.Navigation.Components
{
    public class NavigationViewComponent : ViewComponent
    {
        public NavigationViewComponent(
            NavigationTreeBuilderService siteMapTreeBuilder,
            IEnumerable<INavigationNodePermissionResolver> permissionResolvers,
            IEnumerable<IFindCurrentNode> nodeFinders,
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccesor,
            INodeUrlPrefixProvider prefixProvider,
            ILogger<NavigationViewComponent> logger)
        {
            builder = siteMapTreeBuilder;
            this.permissionResolvers = permissionResolvers;
            this.nodeFinders = nodeFinders;
            this.urlHelperFactory = urlHelperFactory;
            this.actionContextAccesor = actionContextAccesor;
            if (prefixProvider == null)
            {
                this.prefixProvider = new DefaultNodeUrlPrefixProvider();
            }
            else
            {
                this.prefixProvider = prefixProvider;
            }
            log = logger;
        }

        private ILogger log;
        private NavigationTreeBuilderService builder;
        private IEnumerable<INavigationNodePermissionResolver> permissionResolvers;
        private IEnumerable<IFindCurrentNode> nodeFinders;
        private IUrlHelperFactory urlHelperFactory;
        private IActionContextAccessor actionContextAccesor;
        private INodeUrlPrefixProvider prefixProvider;

        

        public async Task<IViewComponentResult> InvokeAsync(string viewName, string filterName, string startingNodeKey)
        {
            var rootNode = await builder.GetTree();
            var urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccesor.ActionContext);
            NavigationViewModel model = new NavigationViewModel(
                startingNodeKey,
                filterName,
                Request.HttpContext,
                urlHelper,
                rootNode,
                permissionResolvers,
                nodeFinders,
                prefixProvider.GetPrefix(),
                log);

          //  return View(viewName, model);

            return View($"/Modules/SimpleFramework.Module.Navigation/Views/Components/Navigation/{viewName}.cshtml", model);
        }



    }
}
