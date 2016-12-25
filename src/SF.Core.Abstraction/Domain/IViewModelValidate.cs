/*******************************************************************************
* 命名空间: SF.Core.Abstraction.Domain
*
* 功 能： N/A
* 类 名： IValidate
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/23 17:06:04 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.Abstraction.Domain
{
    public interface IViewModelValidate<T> where T : class
    {
        IEnumerable<ValidationResult> Validate(IValidator<T> validator);
    }
}
