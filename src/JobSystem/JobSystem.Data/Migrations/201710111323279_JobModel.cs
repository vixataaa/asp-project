namespace JobSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        AddedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AddedBy_Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.AddedBy_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "AddedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Jobs", new[] { "AddedBy_Id" });
            DropIndex("dbo.Jobs", new[] { "IsDeleted" });
            DropTable("dbo.Jobs");
        }
    }
}
