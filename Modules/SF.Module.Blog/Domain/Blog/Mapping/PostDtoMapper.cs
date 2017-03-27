using AutoMapper;
using SF.Entitys;
using SF.Module.Blog.Data.Entitys;
using SF.Module.Blog.Domain.Post.ViewModel;
using SF.Web.Base.DataContractMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Blog.Domain.Post.Mapping
{
    /// <summary>
    /// Post映射处理类，用户业务调用
    /// </summary>
    public class PostDtoMapper : BaseCrudDtoMapper<PostEntity, PostViewModel,Guid>
    {

    }
}
