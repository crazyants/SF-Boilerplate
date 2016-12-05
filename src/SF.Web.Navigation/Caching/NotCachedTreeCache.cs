using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Web.Navigation.Caching
{
    public class NotCachedTreeCache : ITreeCache
    {
        public Task<TreeNode<NavigationNode>> GetTree(string cacheKey)
        {
            TreeNode<NavigationNode> tree = null;
            
            return Task.FromResult(tree);
        }

        public void AddToCache(TreeNode<NavigationNode> tree, string cacheKey)
        {
            //do nothing
        }

        
    }
}
