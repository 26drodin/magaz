namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccPic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "Picture", c => c.Binary(storeType: "image"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUsers", "Picture");
        }
    }
}
