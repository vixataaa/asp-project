namespace SecondHand.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbAttributes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Advertisements", "AddedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Photos", "Advertisement_Id", "dbo.Advertisements");
            DropIndex("dbo.Advertisements", new[] { "AddedBy_Id" });
            DropIndex("dbo.Photos", new[] { "Advertisement_Id" });
            AlterColumn("dbo.Advertisements", "Title", c => c.String());
            AlterColumn("dbo.Advertisements", "Description", c => c.String());
            AlterColumn("dbo.Advertisements", "AddedBy_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Messages", "Text", c => c.String());
            AlterColumn("dbo.Categories", "Name", c => c.String());
            AlterColumn("dbo.Photos", "Url", c => c.String());
            AlterColumn("dbo.Photos", "Advertisement_Id", c => c.Guid());
            CreateIndex("dbo.Advertisements", "AddedBy_Id");
            CreateIndex("dbo.Photos", "Advertisement_Id");
            AddForeignKey("dbo.Advertisements", "AddedBy_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Photos", "Advertisement_Id", "dbo.Advertisements", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photos", "Advertisement_Id", "dbo.Advertisements");
            DropForeignKey("dbo.Advertisements", "AddedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Photos", new[] { "Advertisement_Id" });
            DropIndex("dbo.Advertisements", new[] { "AddedBy_Id" });
            AlterColumn("dbo.Photos", "Advertisement_Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Photos", "Url", c => c.String(nullable: false));
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Messages", "Text", c => c.String(nullable: false));
            AlterColumn("dbo.Advertisements", "AddedBy_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Advertisements", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Advertisements", "Title", c => c.String(nullable: false));
            CreateIndex("dbo.Photos", "Advertisement_Id");
            CreateIndex("dbo.Advertisements", "AddedBy_Id");
            AddForeignKey("dbo.Photos", "Advertisement_Id", "dbo.Advertisements", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Advertisements", "AddedBy_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
