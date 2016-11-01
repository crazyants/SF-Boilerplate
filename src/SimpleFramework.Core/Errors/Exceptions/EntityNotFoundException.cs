/*******************************************************************************
* 命名空间: SimpleFramework.Core.Errors.Exceptions
*
* 功 能： N/A
* 类 名： EntityNotFoundException
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/1 15:59:34 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using SimpleFramework.Core.Errors.Internal;

namespace SimpleFramework.Core.Errors.Exceptions
{
    public class EntityNotFoundException : BaseException
    {
        public EntityNotFoundException(string message = Defaults.EntityNotFoundException.Title, Exception exception = null, Dictionary<string, IEnumerable<string>> messages = null)
            : base(message, exception, messages)
        { }

        public EntityNotFoundException(string entityName, int entityKey) : base(Defaults.EntityNotFoundException.Title, null, null)
        {
            base.AddMessage(String.Format("Entity of type '{0}' and key {1} not found in the current context.", entityName, entityKey));

        }
    }
}
