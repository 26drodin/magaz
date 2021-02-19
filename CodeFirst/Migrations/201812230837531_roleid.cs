namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class roleid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AplicationUserRoles", "User", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AplicationUserRoles", "User");
        }
    }
}
