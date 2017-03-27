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
using SF.Module.Backend.Domain.DataItemDetail.Rule;
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

        private readonly IDataItemDetailRules _dataItemDetailRules;

        public DataItemDetailViewModelValidator(IDataItemDetailRules dataItemDetailRules)
        {
            _dataItemDetailRules = dataItemDetailRules;

            RuleFor(item => item.ItemName)
             .NotEmpty().WithMessage("字典项名称不能为空")
             .Length(1, 100).WithMessage("字典项名称长度必须介于1和100个字符之间..")
             .Must(HaveUniqueName).WithMessage("已存在相同名称的字典项.");

            RuleFor(item => item.ItemValue)
             .NotEmpty().WithMessage("字典项值不能为空")
             .Length(1, 100).WithMessage("字典项值长度必须介于1和100个字符之间.")
             .Must(HaveUniqueValue).WithMessage("已存在相同值的字典项.");

            RuleFor(item => item.SortIndex).NotEmpty()
              .WithMessage("排序不能为空");

        }

        /// <summary>
        /// 判断唯一名字
        /// </summary>
        /// <param name="model">模型数据</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        private bool HaveUniqueName(DataItemDetailViewModel model, string name)
        {
            return !_dataItemDetailRules.IsDataItemDetailNameUnique(name, model.ItemId, model.Id);
        }
        /// <summary>
        ///  判断唯一值
        /// </summary>
        /// <param name="model">模型数据</param>
        /// <param name="code">值</param>
        /// <returns></returns>
        private bool HaveUniqueValue(DataItemDetailViewModel model, string value)
        {
            return !_dataItemDetailRules.IsDataItemDetailValueUnique(value, model.ItemId, model.Id);
        }
    }
}
