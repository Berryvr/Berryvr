namespace Whatsup_Her.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        MobileNumber = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        EmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        ChatId = c.Int(nullable: false, identity: true),
                        FirstAccountId = c.Int(nullable: false),
                        SecondAccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ChatId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MobileNumber = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        OwnerAccountId = c.Int(nullable: false),
                        ContactAccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChatId = c.Int(nullable: false),
                        SenderAccountId = c.Int(nullable: false),
                        Text = c.String(),
                        TimeSent = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chats", t => t.ChatId, cascadeDelete: true)
                .Index(t => t.ChatId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "ChatId", "dbo.Chats");
            DropIndex("dbo.Messages", new[] { "ChatId" });
            DropTable("dbo.Messages");
            DropTable("dbo.Contacts");
            DropTable("dbo.Chats");
            DropTable("dbo.Accounts");
        }
    }
}
