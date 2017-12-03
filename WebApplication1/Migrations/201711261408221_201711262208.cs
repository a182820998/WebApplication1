namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201711262208 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customer", "PasswordConfirmation", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customer", "PasswordConfirmation");
        }
    }
}
