namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newsname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "Name");
        }
    }
}
