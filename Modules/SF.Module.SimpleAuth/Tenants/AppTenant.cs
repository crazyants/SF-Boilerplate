/*******************************************************************************
* 命名空间: SF.Core.Tenants
*
* 功 能： N/A
* 类 名： SiteSettings
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/10/28 15:28:47 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/

using SF.Module.SimpleAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.SimpleAuth.Tenants
{
    public class AppTenant
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string[] Hostnames { get; set; }
        public string Theme { get; set; }
        public string ConnectionString { get; set; }
        public List<SimpleAuthUser> Users { get; set; }

        // overrides for default simpleauthsettings
        public bool EnablePasswordHasherUi { get; set; } = false;
        public string RecaptchaPublicKey { get; set; }
        public string RecaptchaPrivateKey { get; set; }
        public string AuthenticationScheme { get; set; } = "application";

        public bool Equals(AppTenant other)
        {
            if (other == null)
            {
                return false;
            }

            return other.Name.Equals(Id);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as AppTenant);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
