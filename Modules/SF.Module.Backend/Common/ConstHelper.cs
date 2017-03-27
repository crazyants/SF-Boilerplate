/*******************************************************************************
* 命名空间: SF.Module.Backend.Common
*
* 功 能： N/A
* 类 名： ConstHelper
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/21 17:26:36 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Common
{
    public static class ConstHelper
    {
        public const string Region = "sf.base";

        /// <summary>
        /// 字典分类
        /// </summary>
        public const string DATAITEM_PATTERN_KEY = "sf.pres.dataitem-{0}";
        public const string DATAITEM_ALL = "sf.pres.dataitem-all";

        /// <summary>
        /// 字典项
        /// </summary>
        public const string DATAITEMDETAIL_ITEMID_PATTERN_KEY = "sf.pres.dataitemdetail-{0}-{1}";
        public const string DATAITEMDETAIL_ITEMID_ALL = "sf.pres.dataitemdetail-all-{0}";
        public const string DATAITEMDETAIL_ITEMCODE_ALL = "sf.pres.dataitemdetail.code-all-{0}";
        public const string DATAITEMDETAIL_ALL = "sf.pres.dataitemdetail-all";

        /// <summary>
        /// 区域
        /// </summary>
        public const string AREA_PATTERN_KEY = "sf.pres.area-{0}";
        public const string AREA_PATTERN_KEY_PARENT = "sf.pres.area-{0}-{1}";
        public const string AREA_ALL = "sf.pres.area-all";


        /// <summary>
        /// 菜单
        /// </summary>
        public const string MODULE_PATTERN_KEY = "sf.pres.module-{0}";
        public const string MODULE_ALL = "sf.pres.module-all";

        /// <summary>
        ///  机构
        /// </summary>
        public const string ORGANIZE_PATTERN_KEY = "sf.pres.organize-{0}";
        public const string ORGANIZE_ALL = "sf.pres.organize-all";

        /// <summary>
        ///  部门
        /// </summary>
        public const string DEPARTMENT_PATTERN_KEY = "sf.pres.department-{0}";
        public const string DEPARTMENT_ALL = "sf.pres.department-all";

        /// <summary>
        ///  部门
        /// </summary>
        public const string DMOS_PATTERN_KEY = "sf.pres.dmos-{0}";
        public const string DMOS_CATEGORYTYPE_ALL = "sf.pres.dmos-all-{0}";

        /// <summary>
        /// 角色
        /// </summary>
        public const string ROLE_PATTERN_KEY = "sf.pres.role-{0}";
        public const string ROLE_ALL = "sf.pres.role-all";
    }
}
