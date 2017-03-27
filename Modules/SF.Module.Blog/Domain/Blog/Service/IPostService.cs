using SF.Core.Abstraction.GenericServices;
using SF.Entitys;
using SF.Entitys.Abstraction.Pages;
using SF.Module.Blog.Data.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Blog.Domain.Post.Service
{
    /// <summary>
    /// 扩展的服务处理类,主要用户查询及复杂的业务处理
    /// </summary>
    public interface IPostService : IServiceBase
    {
        Task<List<PostEntity>> GetAlls();
        
        Task<IPagedList<PostEntity>> GetPageListBykeyword(long userId, string keyword, int pageIndex = 0, int pageSize = 100);
        IPagedList<PostEntity> GetByWhere(string keyWord, string condition, int pageIndex = 0, int recordsCount = 100);
    }
}
