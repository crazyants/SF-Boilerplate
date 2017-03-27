/*******************************************************************************
* 命名空间: SF.Data
*
* 功 能： N/A
* 类 名： ICoreTableNames
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/13 12:20:49 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Data
{
    public interface ICoreTableNames
    {
        string CurrencyTableName { get; }
        string GeoCountryTableName { get; }
        string GeoZoneTableName { get; }
        string LanguageTableName { get; }
        string RoleTableName { get; }
        string SiteHostTableName { get; }
        string SiteTableName { get; }
        string TablePrefix { get; }
        string UserClaimTableName { get; }
        string UserLocationTableName { get; }
        string UserLoginTableName { get; }
        string UserRoleTableName { get; }
        string UserTableName { get; }
    }
}
