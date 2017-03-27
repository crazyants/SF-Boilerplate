/*******************************************************************************
* 命名空间: SF.Module.Backend.Events
*
* 功 能： N/A
* 类 名： DataItemEventsHandler
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/16 17:09:34 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using CacheManager.Core;
using MediatR;
using SF.Core.Abstraction.Events;
using SF.Entitys;
using SF.Core.Extensions;
using SF.Module.Backend.Common;
using SF.Module.Backend.Data.Entitys;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.DMOS.Events
{
    /// <summary>
    ///  1-岗位2-职位3-工作组
    /// </summary>
    public class DMOSEventsHandler :
        INotificationHandler<EntityCreatedEventData<DMOSEntity>>,
        INotificationHandler<EntityUpdatedEventData<DMOSEntity>>,
        INotificationHandler<EntityDeletedEventData<DMOSEntity>>
    {
        private readonly ICacheManager<object> _cacheManager;

        public DMOSEventsHandler(ICacheManager<object> cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public void Handle(EntityDeletedEventData<DMOSEntity> notification)
        {
            Debug.WriteLine("Pong EntityDeleted");
            _cacheManager.Remove(ConstHelper.ORGANIZE_ALL, ConstHelper.Region);
        }

        public void Handle(EntityUpdatedEventData<DMOSEntity> notification)
        {
            Debug.WriteLine("Pong EntityUpdated");
            _cacheManager.Remove(ConstHelper.ORGANIZE_ALL, ConstHelper.Region);
        }

        public void Handle(EntityCreatedEventData<DMOSEntity> notification)
        {
            Debug.WriteLine("Pong EntityInserted");
            _cacheManager.Remove(ConstHelper.ORGANIZE_ALL, ConstHelper.Region);
        }
    }
}
