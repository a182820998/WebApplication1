namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201712142257 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Grade", "Math", c => c.String());
            AlterColumn("dbo.Grade", "Chinese", c => c.String());
            AlterColumn("dbo.Grade", "English", c => c.String());
            Sql("DBCC CHECKIDENT ('Grade', RESEED, 0);");
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Grade", "English", c => c.Int(nullable: false));
            AlterColumn("dbo.Grade", "Chinese", c => c.Int(nullable: false));
            AlterColumn("dbo.Grade", "Math", c => c.Int(nullable: false));
        }
    }
}
