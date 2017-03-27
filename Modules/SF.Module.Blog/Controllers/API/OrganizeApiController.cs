using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SF.Core.Abstraction.Events;
using SF.Core.Common;
using SF.Data;
using SF.Entitys;
using SF.Core.Extensions;
using SF.Core.QueryExtensions.Extensions;
using SF.Web.Base.Args;
using SF.Web.Base.Controllers;
using SF.Web.Base.DataContractMapper;
using SF.Web.Control.JqGrid.Core.Json;
using SF.Web.Control.JqGrid.Core.Request;
using SF.Web.Control.JqGrid.Core.Response;
using SF.Web.Models.GridTree;
using SF.Web.Models.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using System.Threading.Tasks;
using SF.Module.Blog.Domain.Post.ViewModel;
using SF.Module.Blog.Data.Entitys;
using SF.Module.Blog.Domain.Post.Service;
using SF.Module.Blog.Data.Uow;

namespace SF.Module.Blog.Controllers
{
    /// <summary>
    /// 文章管理API  这就是一个增删改查的基础管理类，可作为API使用
    /// </summary>
    [Authorize]
    [Route("Api/Post/")]
    public class PostApiController : CrudControllerBase<PostEntity, PostViewModel,Guid>
    {
        private readonly IMediator _mediator;
        private readonly IPostService _PostService;
        private readonly IBlogUnitOfWork _BlogUnitOfWork;
        public PostApiController(IServiceCollection collection, ILogger<PostApiController> logger,
             IBlogUnitOfWork BlogUnitOfWork,
             IMediator mediator,
             IPostService PostService)
            : base(BlogUnitOfWork, collection, logger)
        {
            this._BlogUnitOfWork = BlogUnitOfWork;
            this._mediator = mediator;
            this._PostService = PostService;
        }
        #region 事件 主要用户增删改后的处理事件，如清除缓存

        /// <summary>
        /// 新增后
        /// </summary>
        /// <param name="arg"></param>
        protected override void OnAfterAdd(CrudEventArgs<PostEntity, PostViewModel, Guid> arg)
        {
            this._mediator.Publish(new EntityCreatedEventData<PostEntity>(arg.Entity));
        }
        /// <summary>
        /// 编辑后
        /// </summary>
        /// <param name="arg"></param>
        protected override void OnAfterEdit(CrudEventArgs<PostEntity, PostViewModel, Guid> arg)
        {
            this._mediator.Publish(new EntityUpdatedEventData<PostEntity>(arg.Entity));
        }
        /// <summary>
        /// 删除后
        /// </summary>
        /// <param name="arg"></param>
        protected override void OnAfterDeletet(CrudEventArgs<PostEntity, PostViewModel, Guid> arg)
        {
            this._mediator.Publish(new EntityDeletedEventData<PostEntity>(arg.Entity));
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 普通分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        [Route("GetPageList")]
        public ActionResult GetPageListJson(JqGridRequest request, string condition, string keyword)
        {

            var query = _PostService.GetByWhere(keyword, condition,request.PageIndex, request.RecordsCount);
            var dtos = CrudDtoMapper.MapEntityToDtos(query);
            JqGridResponse response = new JqGridResponse()
            {
                TotalPagesCount = query.TotalPages,
                PageIndex = request.PageIndex,
                TotalRecordsCount = query.TotalCount,
            };
            foreach (var userInput in dtos)
            {
                response.Records.Add(new JqGridRecord(Convert.ToString(userInput.Id), userInput));
            }

            response.Reader.RepeatItems = false;
            return new JqGridJsonResult(response);
        }
        #endregion

        #region 验证数据 验证一些数据是否唯一
      
        #endregion

    }


}
