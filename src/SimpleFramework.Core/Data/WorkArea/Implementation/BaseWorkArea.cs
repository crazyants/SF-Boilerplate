/*******************************************************************************
* 命名空间: SimpleFramework.Core.Data.WorkArea.Implementation
*
* 功 能： N/A
* 类 名： BaseWorkArea
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 16:00:03 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SimpleFramework.Core.Data.Repository;
using SimpleFramework.Core.Data.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Data.WorkArea
{
    public class BaseWorkArea : EFCoreWorkArea<CoreDbContext>, IBaseWorkArea
    {
        public BaseWorkArea(CoreDbContext context) : base(context)
        {
            User = new UserRepository(context);
            Media = new MediaRespository(context);
            DataItem = new DataItemRepository(context);
            Setting = new SettingRepository(context);
            UrlSlug= new UrlSlugRepository(context); 
        }
        public IUserRepository User { get; }

        public IMediaRespository Media { get; }

        public IDataItemRepository DataItem { get; }

        public ISettingRepository Setting { get; }

        public IUrlSlugRepository UrlSlug { get; }
        
    }
}
