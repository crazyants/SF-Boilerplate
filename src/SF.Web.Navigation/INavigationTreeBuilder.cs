
using System.Threading.Tasks;

namespace SF.Web.Navigation
{
    public interface INavigationTreeBuilder
    {
        string Name { get; }
        Task<TreeNode<NavigationNode>> BuildTree(NavigationTreeBuilderService service);
    }
}
