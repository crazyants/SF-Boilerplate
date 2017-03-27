using System;
using System.Collections.Generic;
using System.Text;
using SF.Entitys.Abstraction;
using SF.Entitys;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SF.Module.Blog.Data.Entitys
{
    /// <summary>
    /// 创建 更新 删除 等信息都基础base
    /// </summary>
    [DbContextAttribute(typeof(BlogContext))]
    public class PostEntity : BaseEntity<Guid>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string View { get; set; }

        public long UserId { get; set; }

    }
}
