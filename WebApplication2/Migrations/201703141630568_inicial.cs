namespace GoalNewsV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.EmpClis", "IX_FirstAndSecond");
            DropTable("dbo.EmpClis");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EmpClis",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDEmp = c.Int(nullable: false),
                        IDCli = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.EmpClis", new[] { "IDEmp", "IDCli" }, unique: true, name: "IX_FirstAndSecond");
        }
    }
}
