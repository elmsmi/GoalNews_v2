namespace GoalNewsV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class news2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.News", name: "Cliente_ClientID", newName: "Client_ClientID");
            RenameIndex(table: "dbo.News", name: "IX_Cliente_ClientID", newName: "IX_Client_ClientID");
            AddColumn("dbo.News", "ClientName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "ClientName");
            RenameIndex(table: "dbo.News", name: "IX_Client_ClientID", newName: "IX_Cliente_ClientID");
            RenameColumn(table: "dbo.News", name: "Client_ClientID", newName: "Cliente_ClientID");
        }
    }
}
