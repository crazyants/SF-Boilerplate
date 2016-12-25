/*******************************************************************************
* 命名空间: SF.Module.Backend.ViewModels.Validations
*
* 功 能： N/A
* 类 名： ScheduleViewModelValidator
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/23 14:24:28 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using FluentValidation;
using SF.Module.Backend.Domain.DataItemDetail.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.DataItemDetail.Validation
{
    /// <summary>
    /// 字典项验证
    /// </summary>
    public class DataItemDetailViewModelValidator : AbstractValidator<DataItemDetailViewModel>
    {
        public DataItemDetailViewModelValidator()
        {
            RuleFor(user => user.ItemName).NotEmpty().WithMessage("ItemName cannot be empty");
            RuleFor(user => user.ItemValue).NotEmpty().WithMessage("ItemValue cannot be empty");
            RuleFor(user => user.SortIndex).NotEmpty().WithMessage("Sortindex cannot be empty");

        }
    }
}
