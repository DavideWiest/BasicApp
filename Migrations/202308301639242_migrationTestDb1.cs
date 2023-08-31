namespace BasicApp.Migrations;

using System;
using System.Data.Entity.Migrations;

public partial class migrationTestDb1 : DbMigration
{
    public override void Up()
    {
        CreateTable(
            "dbo.Item1Table",
            c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                    Job = c.String(),
                    Age = c.Short(nullable: false),
                })
            .PrimaryKey(t => t.Id);
        
    }
    
    public override void Down()
    {
        DropTable("dbo.Item1Table");
    }
}
