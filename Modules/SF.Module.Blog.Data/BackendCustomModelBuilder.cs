using SF.Core.Abstraction.Data;
using System;
using Microsoft.EntityFrameworkCore;
using SF.Module.Blog.Data.Entitys;
using SF.Core.EFCore.Maps.Fluent;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SF.Module.Blog.Data;

namespace SF.Module.Blog
{
    [DbContextAttribute(typeof(BlogContext))]
    public class BlogCustomModelBuilder : ICustomModelBuilder
    {

        public void Build(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<PostEntity>(b =>
            //{
            //    b.HasKey(x => x.Id);
            //    //b.Property(u => u.Title).HasMaxLength(127);
            //    //b.Property(u => u.Content);
            //    //b.Property(u => u.View).HasMaxLength(127);
            //    //以下的是创建更新删除的映射
            //    b.MapCreatedMeta().MapUpdatedMeta().MapDeletedMeta();
            //});
        }
    }
}
