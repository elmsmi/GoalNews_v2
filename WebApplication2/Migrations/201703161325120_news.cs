namespace GoalNewsV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class news : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.News", "Cliente_ClientID", "dbo.Clients");
            DropIndex("dbo.News", new[] { "Cliente_ClientID" });
            AlterColumn("dbo.News", "Cliente_ClientID", c => c.Int());
            CreateIndex("dbo.News", "Cliente_ClientID");
            AddForeignKey("dbo.News", "Cliente_ClientID", "dbo.Clients", "ClientID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "Cliente_ClientID", "dbo.Clients");
            DropIndex("dbo.News", new[] { "Cliente_ClientID" });
            AlterColumn("dbo.News", "Cliente_ClientID", c => c.Int(nullable: false));
            CreateIndex("dbo.News", "Cliente_ClientID");
            AddForeignKey("dbo.News", "Cliente_ClientID", "dbo.Clients", "ClientID", cascadeDelete: true);
        }
    }
}
