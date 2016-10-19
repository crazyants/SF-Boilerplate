
namespace SimpleFramework.Web.Navigation
{
    public interface INavigationNodePermissionResolver
    {
        bool ShouldAllowView(TreeNode<NavigationNode> menuNode);
    }
}
