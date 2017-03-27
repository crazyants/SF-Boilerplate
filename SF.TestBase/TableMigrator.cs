using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace SF.TestBase
{
    [Migration(1)]
    public class TestCreateAndDropTableMigration : Migration
    {
        public override void Up()
        {
            Create.Table("TestTable")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString(255).NotNullable().WithDefaultValue("Anonymous");

            Create.Table("TestTable2")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString(255).Nullable()
                .WithColumn("TestTableId").AsInt32().NotNullable();

            Create.Index("ix_Name").OnTable("TestTable2").OnColumn("Name").Ascending()
                .WithOptions().NonClustered();

            Create.Column("Name2").OnTable("TestTable2").AsBoolean().Nullable();

            Create.ForeignKey("fk_TestTable2_TestTableId_TestTable_Id")
                .FromTable("TestTable2").ForeignColumn("TestTableId")
                .ToTable("TestTable").PrimaryColumn("Id");

            Insert.IntoTable("TestTable").Row(new { Name = "Test" });
        }

        public override void Down()
        {
            Delete.Table("TestTable2");
            Delete.Table("TestTable");
        }
    }

    [Migration(2)]
    public class PostMigration : Migration
    {
        public override void Up()
        {
            Create.Table("Blog_Post")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Title").AsString(255).NotNullable().WithDefaultValue("Anonymous")
                .WithColumn("Content").AsString()
                .WithColumn("View").AsString(255)
                .WithColumn("UserId").AsInt32().NotNullable(); ;
 
 
        }

        public override void Down()
        {
            Delete.Table("Blog_Post");
          
        }
    }
}
