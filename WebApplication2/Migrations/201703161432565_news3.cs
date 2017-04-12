namespace GoalNewsV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class news3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.News", "ClientName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.News", "ClientName", c => c.String(nullable: false));
        }
    }
}
