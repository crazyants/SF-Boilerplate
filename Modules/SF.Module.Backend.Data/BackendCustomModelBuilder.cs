/*******************************************************************************
* 命名空间: SF.Module.Backend
*
* 功 能： N/A
* 类 名： ICustomModelBuilder
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2017/1/6 17:39:14 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using SF.Core.Abstraction.Data;
using SF.Core.EFCore.Maps.Fluent;
using SF.Module.Backend.Data.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend
{
    public class BackendCustomModelBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            #region 字典

            modelBuilder.Entity<DataItemEntity>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(u => u.ItemName).HasMaxLength(1000);
                b.Property(u => u.ItemCode).HasMaxLength(1000);
                b.Property(u => u.Description).HasMaxLength(1000);
                b.MapCreatedMeta().MapUpdatedMeta().MapDeletedMeta();
                b.ToTable("Core_DataItem");
            });

            modelBuilder.Entity<DataItemDetailEntity>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(u => u.Description).HasMaxLength(1000);
                b.MapCreatedMeta().MapUpdatedMeta().MapDeletedMeta();
                b.ToTable("Core_DataItemDetail");
            });

            modelBuilder.Entity<DataItemDetailEntity>()
            .HasOne(x => x.DataItem)
            .WithMany(x => x.DataItemDetailEntitys)
            .HasForeignKey(x => x.ItemId);


            #endregion
            //区域
            modelBuilder.Entity<AreaEntity>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(u => u.AreaCode).HasMaxLength(127);
                b.Property(u => u.AreaName).HasMaxLength(127);
                b.Property(u => u.QuickQuery).HasMaxLength(127);
                b.Property(u => u.SimpleSpelling).HasMaxLength(127);
                b.Property(u => u.Description).HasMaxLength(280);
                b.MapCreatedMeta().MapUpdatedMeta().MapDeletedMeta();
            });
         
            //岗位职位
            modelBuilder.Entity<DMOSEntity>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(u => u.EnCode).HasMaxLength(127);
                b.Property(u => u.FullName).HasMaxLength(127);
                b.Property(u => u.Description).HasMaxLength(280);
                b.MapCreatedMeta().MapUpdatedMeta().MapDeletedMeta();
            });
            //机构
            modelBuilder.Entity<OrganizeEntity>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(u => u.EnCode).HasMaxLength(127);
                b.Property(u => u.FullName).HasMaxLength(127);
                b.Property(u => u.Description).HasMaxLength(280);
                b.MapCreatedMeta().MapUpdatedMeta().MapDeletedMeta();
            });
            //部门
            modelBuilder.Entity<DepartmentEntity>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(u => u.EnCode).HasMaxLength(127);
                b.Property(u => u.FullName).HasMaxLength(127);
                b.Property(u => u.Description).HasMaxLength(280);
                b.MapCreatedMeta().MapUpdatedMeta().MapDeletedMeta();
            });
        }
    }
}
