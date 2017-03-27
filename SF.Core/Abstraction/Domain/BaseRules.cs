/*******************************************************************************
* 命名空间: SF.Core.Abstraction.Domain
*
* 功 能： N/A
* 类 名： BaseRules
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2017/1/10 14:26:09 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Entitys.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.Abstraction.Domain
{
    public abstract class BaseRules<T> : IRules<T> where T : IEntity
    {
        public virtual bool AllowDelete(T entity)
        {
            return true;
        }
    }
}
