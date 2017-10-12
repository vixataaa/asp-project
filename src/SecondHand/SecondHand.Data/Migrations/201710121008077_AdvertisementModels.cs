namespace SecondHand.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdvertisementModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Advertisements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrencyType = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        AddedBy_Id = c.String(nullable: false, maxLength: 128),
                        Category_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AddedBy_Id, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.IsDeleted)
                .Index(t => t.AddedBy_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Url = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        Advertisement_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Advertisements", t => t.Advertisement_Id, cascadeDelete: true)
                .Index(t => t.IsDeleted)
                .Index(t => t.Advertisement_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photos", "Advertisement_Id", "dbo.Advertisements");
            DropForeignKey("dbo.Advertisements", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Advertisements", "AddedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Photos", new[] { "Advertisement_Id" });
            DropIndex("dbo.Photos", new[] { "IsDeleted" });
            DropIndex("dbo.Categories", new[] { "IsDeleted" });
            DropIndex("dbo.Advertisements", new[] { "Category_Id" });
            DropIndex("dbo.Advertisements", new[] { "AddedBy_Id" });
            DropIndex("dbo.Advertisements", new[] { "IsDeleted" });
            DropTable("dbo.Photos");
            DropTable("dbo.Categories");
            DropTable("dbo.Advertisements");
        }
    }
}
