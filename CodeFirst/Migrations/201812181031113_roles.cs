namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class roles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AplicationUserRoles", "Roles_Id", c => c.Int());
            CreateIndex("dbo.AplicationUserRoles", "Roles_Id");
            AddForeignKey("dbo.AplicationUserRoles", "Roles_Id", "dbo.Roles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AplicationUserRoles", "Roles_Id", "dbo.Roles");
            DropIndex("dbo.AplicationUserRoles", new[] { "Roles_Id" });
            DropColumn("dbo.AplicationUserRoles", "Roles_Id");
            DropTable("dbo.Roles");
        }
    }
}
