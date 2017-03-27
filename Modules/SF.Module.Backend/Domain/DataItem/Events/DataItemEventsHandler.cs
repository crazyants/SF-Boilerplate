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

namespace SF.Module.Backend.Domain.DataItem.Events
{
    /// <summary>
    /// 字典事件处理类
    /// </summary>
    public class DataItemEventsHandler :
        INotificationHandler<EntityCreatedEventData<DataItemEntity>>,
        INotificationHandler<EntityUpdatedEventData<DataItemEntity>>,
        INotificationHandler<EntityDeletedEventData<DataItemEntity>>
    {
        private readonly ICacheManager<object> _cacheManager;

        public DataItemEventsHandler(ICacheManager<object> cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public void Handle(EntityDeletedEventData<DataItemEntity> notification)
        {
            Debug.WriteLine("Pong EntityDeleted");
            _cacheManager.Remove(ConstHelper.DATAITEM_PATTERN_KEY.FormatCurrent(notification.Entity.Id), ConstHelper.Region);
            _cacheManager.Remove(ConstHelper.DATAITEMDETAIL_ITEMCODE_ALL.FormatCurrent(notification.Entity.ItemCode), ConstHelper.Region);
            _cacheManager.Remove(ConstHelper.DATAITEM_ALL, ConstHelper.Region);
        }

        public void Handle(EntityUpdatedEventData<DataItemEntity> notification)
        {
            Debug.WriteLine("Pong EntityUpdated");
            _cacheManager.Remove(ConstHelper.DATAITEM_PATTERN_KEY.FormatCurrent(notification.Entity.Id), ConstHelper.Region);
            _cacheManager.Remove(ConstHelper.DATAITEMDETAIL_ITEMCODE_ALL.FormatCurrent(notification.Entity.ItemCode), ConstHelper.Region);
            _cacheManager.Remove(ConstHelper.DATAITEM_ALL, ConstHelper.Region);
        }

        public void Handle(EntityCreatedEventData<DataItemEntity> notification)
        {
            Debug.WriteLine("Pong EntityInserted");
            _cacheManager.Remove(ConstHelper.DATAITEM_PATTERN_KEY.FormatCurrent(notification.Entity.Id), ConstHelper.Region);
            _cacheManager.Remove(ConstHelper.DATAITEMDETAIL_ITEMCODE_ALL.FormatCurrent(notification.Entity.ItemCode), ConstHelper.Region);
            _cacheManager.Remove(ConstHelper.DATAITEM_ALL, ConstHelper.Region);
        }
    }
}
