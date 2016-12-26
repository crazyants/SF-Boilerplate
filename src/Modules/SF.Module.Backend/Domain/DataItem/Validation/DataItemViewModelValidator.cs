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
using SF.Module.Backend.Domain.DataItem.Rule;
using SF.Module.Backend.Domain.DataItem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.DataItem.Validation
{
    /// <summary>
    /// 字典验证
    /// </summary>
    public class DataItemViewModelValidator : AbstractValidator<DataItemViewModel>
    {
        private readonly IDataItemRules _dataItemRules;

        public DataItemViewModelValidator(IDataItemRules dataItemRules)
        {
            _dataItemRules = dataItemRules;

            RuleFor(item => item.ItemName)
             .NotEmpty().WithMessage("字典分类名称不能为空")
             .Length(1, 100).WithMessage("字典分类名称长度必须介于1和100个字符之间..")
             .Must(HaveUniqueName).WithMessage("已存在相同名称的字典分类.");

            RuleFor(item => item.ItemCode)
             .NotEmpty().WithMessage("字典分类编码不能为空")
             .Length(1, 100).WithMessage("字典分类编码长度必须介于1和100个字符之间.")
             .Must(HaveUniqueCode).WithMessage("已存在相同编码的字典分类.");

            RuleFor(item => item.SortIndex).NotEmpty()
              .WithMessage("排序不能为空");

            RuleFor(item => item.Id)
              .Must(IsParenByOwnId).WithMessage("不能选择自身为上级!");
        }
        /// <summary>
        /// 判断唯一名字
        /// </summary>
        /// <param name="model">模型数据</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        private bool HaveUniqueName(DataItemViewModel model, string name)
        {
            return !_dataItemRules.IsDataItemNameUnique(name, model.Id);
        }
        /// <summary>
        ///  判断唯一编码
        /// </summary>
        /// <param name="model">模型数据</param>
        /// <param name="code">编码</param>
        /// <returns></returns>
        private bool HaveUniqueCode(DataItemViewModel model, string code)
        {
            return !_dataItemRules.IsDataItemCodeUnique(code, model.Id);
        }
        /// <summary>
        /// 判断父级Id是否自身
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool IsParenByOwnId(DataItemViewModel model, long id)
        {
            return !(model.Id == 0 ? false : model.Id == (model.ParentId ?? -1));
        }
    }
}
