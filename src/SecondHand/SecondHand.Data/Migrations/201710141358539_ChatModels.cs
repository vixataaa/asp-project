namespace SecondHand.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChatModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        Advertisement_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Advertisements", t => t.Advertisement_Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.Advertisement_Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Text = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        Author_Id = c.String(maxLength: 128),
                        Chat_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .ForeignKey("dbo.Chats", t => t.Chat_Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.Author_Id)
                .Index(t => t.Chat_Id);
            
            AddColumn("dbo.AspNetUsers", "Chat_Id", c => c.Guid());
            CreateIndex("dbo.AspNetUsers", "Chat_Id");
            AddForeignKey("dbo.AspNetUsers", "Chat_Id", "dbo.Chats", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Chat_Id", "dbo.Chats");
            DropForeignKey("dbo.Messages", "Chat_Id", "dbo.Chats");
            DropForeignKey("dbo.Messages", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Chats", "Advertisement_Id", "dbo.Advertisements");
            DropIndex("dbo.Messages", new[] { "Chat_Id" });
            DropIndex("dbo.Messages", new[] { "Author_Id" });
            DropIndex("dbo.Messages", new[] { "IsDeleted" });
            DropIndex("dbo.Chats", new[] { "Advertisement_Id" });
            DropIndex("dbo.Chats", new[] { "IsDeleted" });
            DropIndex("dbo.AspNetUsers", new[] { "Chat_Id" });
            DropColumn("dbo.AspNetUsers", "Chat_Id");
            DropTable("dbo.Messages");
            DropTable("dbo.Chats");
        }
    }
}
