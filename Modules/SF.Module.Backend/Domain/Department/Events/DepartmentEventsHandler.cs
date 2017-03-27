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

namespace SF.Module.Backend.Domain.Department.Events
{
    /// <summary>
    /// 字典事件处理类
    /// </summary>
    public class DepartmentEventsHandler :
        INotificationHandler<EntityCreatedEventData<DepartmentEntity>>,
        INotificationHandler<EntityUpdatedEventData<DepartmentEntity>>,
        INotificationHandler<EntityDeletedEventData<DepartmentEntity>>
    {
        private readonly ICacheManager<object> _cacheManager;

        public DepartmentEventsHandler(ICacheManager<object> cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public void Handle(EntityDeletedEventData<DepartmentEntity> notification)
        {
            Debug.WriteLine("Pong EntityDeleted");
            _cacheManager.Remove(ConstHelper.ORGANIZE_ALL, ConstHelper.Region);
        }

        public void Handle(EntityUpdatedEventData<DepartmentEntity> notification)
        {
            Debug.WriteLine("Pong EntityUpdated");
            _cacheManager.Remove(ConstHelper.ORGANIZE_ALL, ConstHelper.Region);
        }

        public void Handle(EntityCreatedEventData<DepartmentEntity> notification)
        {
            Debug.WriteLine("Pong EntityInserted");
            _cacheManager.Remove(ConstHelper.ORGANIZE_ALL, ConstHelper.Region);
        }
    }
}
