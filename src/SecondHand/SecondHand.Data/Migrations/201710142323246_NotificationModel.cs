namespace SecondHand.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatNotifications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        Chat_Id = c.Guid(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chats", t => t.Chat_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.Chat_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChatNotifications", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ChatNotifications", "Chat_Id", "dbo.Chats");
            DropIndex("dbo.ChatNotifications", new[] { "User_Id" });
            DropIndex("dbo.ChatNotifications", new[] { "Chat_Id" });
            DropIndex("dbo.ChatNotifications", new[] { "IsDeleted" });
            DropTable("dbo.ChatNotifications");
        }
    }
}
