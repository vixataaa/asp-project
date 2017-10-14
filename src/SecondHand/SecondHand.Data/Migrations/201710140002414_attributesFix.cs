namespace SecondHand.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attributesFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Advertisements", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Advertisements", new[] { "Category_Id" });
            AlterColumn("dbo.Advertisements", "Category_Id", c => c.Guid());
            CreateIndex("dbo.Advertisements", "Category_Id");
            AddForeignKey("dbo.Advertisements", "Category_Id", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Advertisements", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Advertisements", new[] { "Category_Id" });
            AlterColumn("dbo.Advertisements", "Category_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.Advertisements", "Category_Id");
            AddForeignKey("dbo.Advertisements", "Category_Id", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
