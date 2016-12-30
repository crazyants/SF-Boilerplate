using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SF.Core.Abstraction.Data;
using SF.Core.EFCore.Maps.Fluent;
using SF.Module.ActivityLog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.ActivityLog.Data
{
    public class ActivityCustomModelBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActivityEntity>(cfg =>
            {
                cfg.MapCreatedMeta().MapUpdatedMeta().MapDeletedMeta().MapDeletedMeta();
                cfg.ToTable("Core_Activity");
            });

            modelBuilder.Entity<ActivityTypeEntity>(cfg =>
            {
                cfg.MapCreatedMeta().MapUpdatedMeta().MapDeletedMeta().MapDeletedMeta();
                cfg.ToTable("Core_ActivityType");
            });

            modelBuilder.Entity<ActivityEntity>()
             .HasOne(x => x.ActivityType)
             .WithMany(x => x.ActivityEntitys)
             .HasForeignKey(x => x.ActivityTypeId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
