using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.SimpleData
{
    public class UnicornsContext : DbContext
    {
        public UnicornsContext(DbContextOptions<UnicornsContext> options) : base(options)
        {
            //ChangeTracker.AutoDetectChangesEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // should this be called before or after we do our thing?

            base.OnModelCreating(modelBuilder);
            #region Logging
            modelBuilder.Entity<Lodging>(cfg =>
            {
                cfg.HasKey(e => e.LodgingId);
                cfg.Property(u => u.Name).HasMaxLength(1000);
                cfg.ToTable("Lodgings");
            });
            
           
            #endregion
        }

    }

}
