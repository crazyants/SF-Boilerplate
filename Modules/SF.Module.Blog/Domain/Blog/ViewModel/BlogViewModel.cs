
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SF.Core.Abstraction.Domain;
using SF.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace SF.Module.Blog.Domain.Post.ViewModel
{
    /// <summary>
    /// 这是一个DTO 也可以理解为MVC的Model
    /// </summary>
    public class PostViewModel : EntityModelBase<Guid>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string View { get; set; }


        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset UpdatedOn { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }


    }
}
