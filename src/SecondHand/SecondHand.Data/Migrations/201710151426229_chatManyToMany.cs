namespace SecondHand.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chatManyToMany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Chat_Id", "dbo.Chats");
            DropIndex("dbo.AspNetUsers", new[] { "Chat_Id" });
            CreateTable(
                "dbo.ChatApplicationUsers",
                c => new
                    {
                        Chat_Id = c.Guid(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Chat_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.Chats", t => t.Chat_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Chat_Id)
                .Index(t => t.ApplicationUser_Id);
            
            DropColumn("dbo.AspNetUsers", "Chat_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Chat_Id", c => c.Guid());
            DropForeignKey("dbo.ChatApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ChatApplicationUsers", "Chat_Id", "dbo.Chats");
            DropIndex("dbo.ChatApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ChatApplicationUsers", new[] { "Chat_Id" });
            DropTable("dbo.ChatApplicationUsers");
            CreateIndex("dbo.AspNetUsers", "Chat_Id");
            AddForeignKey("dbo.AspNetUsers", "Chat_Id", "dbo.Chats", "Id");
        }
    }
}
