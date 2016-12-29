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
using SF.Core.Entitys;
using SF.Core.Extensions;
using SF.Module.Backend.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.DataItem.Events
{
    /// <summary>
    /// 字典事件处理类
    /// </summary>
    public class DataItemEventsHandler :
        INotificationHandler<EntityInserted<DataItemEntity>>,
        INotificationHandler<EntityUpdated<DataItemEntity>>,
        INotificationHandler<EntityDeleted<DataItemEntity>>
    {
        private readonly ICacheManager<object> _cacheManager;

        public DataItemEventsHandler(ICacheManager<object> cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public void Handle(EntityDeleted<DataItemEntity> notification)
        {
            Debug.WriteLine("Pong EntityDeleted");
            _cacheManager.Remove(ConstHelper.DATAITEM_PATTERN_KEY.FormatCurrent(notification.Entity.Id));
            _cacheManager.Remove(ConstHelper.DATAITEM_ALL);
        }

        public void Handle(EntityUpdated<DataItemEntity> notification)
        {
            Debug.WriteLine("Pong EntityUpdated");
            _cacheManager.Remove(ConstHelper.DATAITEM_PATTERN_KEY.FormatCurrent(notification.Entity.Id));
            _cacheManager.Remove(ConstHelper.DATAITEM_ALL);
        }

        public void Handle(EntityInserted<DataItemEntity> notification)
        {
            Debug.WriteLine("Pong EntityInserted");
            _cacheManager.Remove(ConstHelper.DATAITEM_PATTERN_KEY.FormatCurrent(notification.Entity.Id));
            _cacheManager.Remove(ConstHelper.DATAITEM_ALL);
        }
    }
}
