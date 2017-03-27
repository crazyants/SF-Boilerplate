

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Web.Navigation.Caching
{
    public interface ITreeCache
    {
        Task<TreeNode<NavigationNode>> GetTree(string cacheKey);
        void AddToCache(TreeNode<NavigationNode> tree, string cacheKey);
        
    }
}
