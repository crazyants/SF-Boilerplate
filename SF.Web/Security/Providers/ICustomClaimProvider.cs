/*******************************************************************************
* 命名空间: SF.Core.Abstraction.Providers
*
* 功 能： N/A
* 类 名： ICustomClaimProvider
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/15 9:52:06 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SF.Web.Security.Providers
{
    public interface ICustomClaimProvider
    {
        Task AddClaims(UserEntity user, ClaimsIdentity identity);
    }
}
