
using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace SF.Module.Blog.Migrations
{
    [Migration(1)]
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
