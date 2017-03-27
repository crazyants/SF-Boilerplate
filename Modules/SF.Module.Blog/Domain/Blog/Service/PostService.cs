using AutoMapper;
using CacheManager.Core;
using SF.Data;
using SF.Web.Base.DataContractMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SF.Core.Extensions;
using System.Linq.Expressions;
using LinqKit;
using SF.Entitys.Abstraction.Pages;
using Microsoft.EntityFrameworkCore;
using SF.Module.Blog.Data.Entitys;
using SF.Module.Blog.Data.Uow;
using SF.Core.Abstraction.GenericServices;

namespace SF.Module.Blog.Domain.Post.Service
{
    public class PostService : ServiceBase, IPostService
    {
        #region Fields
        private readonly ICacheManager<object> _cacheManager;
        private readonly IBlogUnitOfWork _BlogUnitOfWork;
        #endregion

        #region Constructors
        public PostService(IBlogUnitOfWork BlogUnitOfWork,
            ICacheManager<object> cacheManager)
        {
            _BlogUnitOfWork = BlogUnitOfWork;
            _cacheManager = cacheManager;

        }
        #endregion


        #region Method  
        /// <summary>
        /// 获取所有机构（缓存）
        /// </summary>
        /// <returns></returns>
        public async Task<List<PostEntity>> GetAlls()
        {
            
                return await _BlogUnitOfWork.Post.Query().ToListAsync();
            

        }

        /// <summary>
        /// 查询某个用户的文章
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="keyword"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<IPagedList<PostEntity>> GetPageListBykeyword(long userId, string keyword, int pageIndex = 0, int pageSize = 100)
        {
            var predicate = PredicateBuilder.New<PostEntity>();
            predicate.And(d => d.UserId == userId);
            if (!string.IsNullOrEmpty(keyword))
            {
                predicate.And(d => d.Title.Contains(keyword));
            }
            return await _BlogUnitOfWork.Post.QueryPageAsync(predicate, page: pageIndex, pageSize: pageSize);

            //这个就是简单的关联查询 ，使用Linqy
           // _BlogUnitOfWork.Post.Query().Include(x => x.User).Where(x => x.User.Id == userId).ToList();
            
        }

        /// <summary>
        /// 这里只要是根据下拉的条件判断使用哪个查询条件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rootDataItemId"></param>
        /// <returns></returns>
        public IPagedList<PostEntity> GetByWhere(string keyWord, string condition, int pageIndex = 0, int recordsCount = 100)
        {
            var predicate = PredicateBuilder.New<PostEntity>(true);

            #region 多条件查询
            if (!keyWord.IsEmpty())
            {
                switch (condition)
                {
                    case "Title":        //公司名称
                        predicate.And(d => d.Title == keyWord);
                        break;
                    default:
                        break;

                }
            }
            #endregion

            return _BlogUnitOfWork.Post.QueryPage(predicate, page: pageIndex, pageSize: recordsCount);

        }
        #endregion
    }
}
