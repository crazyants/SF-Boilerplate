using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SF.Module.SimpleData;

namespace SF.Module.SimpleData.Migrations
{
    [DbContext(typeof(UnicornsContext))]
    [Migration("20161202064451_Modify_Multi")]
    partial class Modify_Multi
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SF.Module.SimpleData.Destination", b =>
                {
                    b.Property<int>("DestinationId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Country");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<byte[]>("Photo");

                    b.HasKey("DestinationId");

                    b.ToTable("Destinations");
                });

            modelBuilder.Entity("SF.Module.SimpleData.Lodging", b =>
                {
                    b.Property<int>("LodgingId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DestinationId");

                    b.Property<bool>("IsResort");

                    b.Property<string>("Name");

                    b.Property<string>("Owner");

                    b.HasKey("LodgingId");

                    b.HasIndex("DestinationId");

                    b.ToTable("Lodgings");
                });

            modelBuilder.Entity("SF.Module.SimpleData.Lodging", b =>
                {
                    b.HasOne("SF.Module.SimpleData.Destination", "Destination")
                        .WithMany("Lodgings")
                        .HasForeignKey("DestinationId");
                });
        }
    }
}
