
using SimpleFramework.Web.Navigation.Caching;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleFramework.Web.Navigation
{
    public class NavigationTreeBuilderService
    {
        public NavigationTreeBuilderService(
            IEnumerable<INavigationTreeBuilder> treeBuilders,
            ITreeCacheKeyResolver cacheKeyResolver,
            IOptions<NavigationOptions> navigationOptionsAccessor,
            ITreeCache treeCache = null
            )
        {
            if (treeBuilders == null) { throw new ArgumentNullException(nameof(treeBuilders)); }
            if (navigationOptionsAccessor == null) { throw new ArgumentNullException(nameof(navigationOptionsAccessor)); }

            this.cacheKeyResolver = cacheKeyResolver;
            this.treeCache = treeCache ?? new NotCachedTreeCache();
            builders = treeBuilders;
            navOptions = navigationOptionsAccessor.Value;

        }

        private ITreeCache treeCache;
        private ITreeCacheKeyResolver cacheKeyResolver;
        private NavigationOptions navOptions;

        private IEnumerable<INavigationTreeBuilder> builders;

        public INavigationTreeBuilder GetRootTreeBuilder()
        {
            return GetTreeBuilder(navOptions.RootTreeBuilderName);
        }

        public INavigationTreeBuilder GetTreeBuilder(string name)
        {
            foreach(var t in builders)
            {
                if(t.Name == name) { return t; }
            }

            return null;
        }

        public async Task<TreeNode<NavigationNode>> GetTree()
        {
            var builder = GetRootTreeBuilder();
            var cacheKey = cacheKeyResolver.GetCacheKey(builder);
            var tree = await treeCache.GetTree(cacheKey).ConfigureAwait(false);
            if(tree != null) { return tree; }
            tree = await builder.BuildTree(this).ConfigureAwait(false);
            treeCache.AddToCache(tree, cacheKey);

            return tree;
        }

        public async Task<TreeNode<NavigationNode>> GetTree(string builderName)
        {
            //this one is only called if using nested builders so
            // we should not cache here, the result will be cached in the main tree
            // after all the builders have built it up

            //var cacheKey = builderName;
            //var tree = await treeCache.GetTree(cacheKey).ConfigureAwait(false);
            //if (tree != null) { return tree; }
            var builder = GetTreeBuilder(builderName);
            var tree = await builder.BuildTree(this).ConfigureAwait(false);

            return tree;
        }


    }
}
