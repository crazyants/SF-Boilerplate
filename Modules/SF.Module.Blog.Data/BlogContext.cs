using Microsoft.EntityFrameworkCore;
using SF.Data;
using SF.Module.Blog.Data.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Blog.Data
{
    /// <summary>
    /// Blog业务库上下文
    /// </summary>
    public class BlogContext : ObjectDbContextBase<Guid, BlogContext>
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
       
    }

}
