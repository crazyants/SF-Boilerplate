/*******************************************************************************
* 命名空间: SF.Core.Errors.Exceptions
*
* 功 能： N/A
* 类 名： EntityNotFoundException
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/1 15:59:34 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using SF.Core.Errors.Internal;

namespace SF.Core.Errors.Exceptions
{
    public class EntityNotFoundException : BaseException
    {
        public EntityNotFoundException(string message = Defaults.EntityNotFoundException.Title, Exception exception = null, Dictionary<string, IEnumerable<string>> messages = null)
            : base(message, exception, messages)
        { }

        public EntityNotFoundException(string entityName, object entityKey) : base(Defaults.EntityNotFoundException.Title, null, null)
        {
            base.AddMessage(String.Format("Entity of type '{0}' and key {1} not found in the current context.", entityName, entityKey));

        }
    }
}
