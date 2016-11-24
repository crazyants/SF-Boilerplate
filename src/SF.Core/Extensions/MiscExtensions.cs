/*******************************************************************************
* 命名空间: SF.Core.Extensions
*
* 功 能： N/A
* 类 名： MiscExtensions
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/24 10:40:28 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.Extensions
{
    public static class MiscExtensions
    {
        public static void Dump(this Exception exception)
        {
            try
            {
                exception.StackTrace.Dump();
                exception.Message.Dump();
            }
            catch { }
        }
    }
}
