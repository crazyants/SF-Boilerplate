/*******************************************************************************
* 命名空间: SF.Data
*
* 功 能： N/A
* 类 名： CoreTableNames
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/13 12:20:59 疯狂蚂蚁 初版
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
    public class CoreTableNames : ICoreTableNames
    {
        public CoreTableNames()
        {

        }
        public string TablePrefix { get; set; } = "code_";
        public string SiteTableName { get; set; } = "Site";
        public string SiteHostTableName { get; set; } = "SiteHost";
        public string UserTableName { get; set; } = "User";
        public string RoleTableName { get; set; } = "Role";
        public string UserClaimTableName { get; set; } = "UserClaim";
        public string UserLoginTableName { get; set; } = "UserLogin";
        public string UserLocationTableName { get; set; } = "UserLocation";
        public string UserRoleTableName { get; set; } = "UserRole";

        public string GeoCountryTableName { get; set; } = "GeoCountry";
        public string GeoZoneTableName { get; set; } = "GeoZone";
        public string CurrencyTableName { get; set; } = "Currency";
        public string LanguageTableName { get; set; } = "Language";

    }
}
