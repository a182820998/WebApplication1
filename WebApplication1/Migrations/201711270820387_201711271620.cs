namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201711271620 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customer", "FacebookId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customer", "FacebookId");
        }
    }
}
