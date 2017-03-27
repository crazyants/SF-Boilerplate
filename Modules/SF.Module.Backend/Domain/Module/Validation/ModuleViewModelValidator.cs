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
using SF.Module.Backend.Domain.Module.Rule;
using SF.Module.Backend.Domain.Module.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.Module.Validation
{
    /// <summary>
    /// 字典项验证
    /// </summary>
    public class ModuleViewModelValidator : AbstractValidator<ModuleViewModel>
    {

        private readonly IModuleRules _dataItemDetailRules;

        public ModuleViewModelValidator(IModuleRules dataItemDetailRules)
        {
            _dataItemDetailRules = dataItemDetailRules;

            RuleFor(item => item.EnCode)
             .NotEmpty().WithMessage("功能编号不能为空")
             .Length(1, 100).WithMessage("功能编号长度必须介于1和100个字符之间..")
             .Must(HaveUniqueCode).WithMessage("已存在相同名称的功能编号.");

            RuleFor(item => item.FullName)
             .NotEmpty().WithMessage("功能名称不能为空")
             .Length(1, 100).WithMessage("功能名称长度必须介于1和100个字符之间.")
             .Must(HaveUniqueName).WithMessage("已存在相同值的功能名称.");

            RuleFor(item => item.SortIndex).NotEmpty()
              .WithMessage("排序不能为空");

        }

        /// <summary>
        /// 判断唯一名字
        /// </summary>
        /// <param name="model">模型数据</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        private bool HaveUniqueName(ModuleViewModel model, string name)
        {
            return !_dataItemDetailRules.IsModuleNameUnique(name, model.Id);
        }
        /// <summary>
        ///  判断唯一值
        /// </summary>
        /// <param name="model">模型数据</param>
        /// <param name="code">值</param>
        /// <returns></returns>
        private bool HaveUniqueCode(ModuleViewModel model, string code)
        {
            return !_dataItemDetailRules.IsModuleCodeUnique(code, model.Id);
        }
    }
}
