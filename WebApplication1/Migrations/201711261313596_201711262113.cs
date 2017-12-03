namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201711262113 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customer", "PasswordConfirmation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customer", "PasswordConfirmation", c => c.String(nullable: false));
        }
    }
}
