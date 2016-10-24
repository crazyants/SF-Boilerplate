using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SimpleFramework.Core.Abstraction.Data;
using SimpleFramework.Module.EFLogging.Models;

namespace SimpleFramework.Module.EFLogging.Data
{
    public class CatalogCustomModelBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<LogItem>();
            entity.ToTable("cs_SystemLog");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
            .ForSqlServerHasColumnType("uniqueidentifier")
            .ForSqlServerHasDefaultValueSql("newid()")
               .IsRequired();

            entity.Property(p => p.LogDateUtc)
            .HasColumnName("LogDate")
            .ForSqlServerHasColumnType("datetime")
            .ForSqlServerHasDefaultValueSql("getutcdate()")
            ;

            entity.Property(p => p.IpAddress)
            .HasMaxLength(50)
            ;

            entity.Property(p => p.Culture)
            .HasMaxLength(10)
            ;

            entity.Property(p => p.ShortUrl)
            .HasMaxLength(255)
            ;

            entity.Property(p => p.Thread)
            .HasMaxLength(255)
            ;

            entity.Property(p => p.LogLevel)
            .HasMaxLength(20)
            ;

            entity.Property(p => p.Logger)
            .HasMaxLength(255)
            ;
        }
    }
}
