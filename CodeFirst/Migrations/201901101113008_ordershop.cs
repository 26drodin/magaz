namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ordershop : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Shop_ID", "dbo.Shops");
            DropIndex("dbo.Orders", new[] { "Shop_ID" });
            AlterColumn("dbo.Orders", "Shop_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "Shop_ID");
            AddForeignKey("dbo.Orders", "Shop_ID", "dbo.Shops", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Shop_ID", "dbo.Shops");
            DropIndex("dbo.Orders", new[] { "Shop_ID" });
            AlterColumn("dbo.Orders", "Shop_ID", c => c.Int());
            CreateIndex("dbo.Orders", "Shop_ID");
            AddForeignKey("dbo.Orders", "Shop_ID", "dbo.Shops", "ID");
        }
    }
}
