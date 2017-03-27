
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SF.Core.Abstraction.Domain;
using SF.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace SF.Module.Backend.Domain.Role.ViewModel
{
    public class RoleViewModel : EntityModelBase
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string NormalizedName { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        public string Description { get; set; }
        public int Enabled { get; set; }
        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset UpdatedOn { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

    }
}
