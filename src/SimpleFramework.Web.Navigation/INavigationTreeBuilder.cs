
using System.Threading.Tasks;

namespace SimpleFramework.Web.Navigation
{
    public interface INavigationTreeBuilder
    {
        string Name { get; }
        Task<TreeNode<NavigationNode>> BuildTree(NavigationTreeBuilderService service);
    }
}
