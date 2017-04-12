namespace GoalNewsV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "Cliente_ClientID", c => c.Int(nullable: false));
            CreateIndex("dbo.News", "Cliente_ClientID");
            AddForeignKey("dbo.News", "Cliente_ClientID", "dbo.Clients", "ClientID", cascadeDelete: true);
            DropColumn("dbo.News", "Cliente");
        }
        
        public override void Down()
        {
            AddColumn("dbo.News", "Cliente", c => c.String(nullable: false));
            DropForeignKey("dbo.News", "Cliente_ClientID", "dbo.Clients");
            DropIndex("dbo.News", new[] { "Cliente_ClientID" });
            DropColumn("dbo.News", "Cliente_ClientID");
        }
    }
}
