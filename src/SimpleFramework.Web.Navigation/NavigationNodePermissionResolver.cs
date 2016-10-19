
using SimpleFramework.Web.Navigation.Helpers;
using Microsoft.AspNetCore.Http;

namespace SimpleFramework.Web.Navigation
{
    public class NavigationNodePermissionResolver : INavigationNodePermissionResolver
    {
        public NavigationNodePermissionResolver(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        private IHttpContextAccessor httpContextAccessor;

        public virtual bool ShouldAllowView(TreeNode<NavigationNode> menuNode)
        {
            if (string.IsNullOrEmpty(menuNode.Value.ViewRoles)) { return true; }
            if (menuNode.Value.ViewRoles == "All Users;") { return true; }

            if (httpContextAccessor.HttpContext.User.IsInRoles(menuNode.Value.ViewRoles))
            {
                return true;
            }

            return false;
        }
    }
}
