namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class simplerole : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AplicationUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.AplicationUserRoles", "Roles_Id", "dbo.Roles");
            DropIndex("dbo.AplicationUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AplicationUserRoles", new[] { "Roles_Id" });
            CreateTable(
                "dbo.RolesApplicationUsers",
                c => new
                    {
                        Roles_ID = c.Int(nullable: false),
                        ApplicationUser_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Roles_ID, t.ApplicationUser_Id })
                .ForeignKey("dbo.Roles", t => t.Roles_ID, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Roles_ID)
                .Index(t => t.ApplicationUser_Id);
            
            AddColumn("dbo.Roles", "role", c => c.String());
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
            DropColumn("dbo.AplicationUserRoles", "ApplicationUser_Id");
            DropColumn("dbo.AplicationUserRoles", "Roles_Id");
            DropColumn("dbo.Roles", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Roles", "Name", c => c.String());
            AddColumn("dbo.AplicationUserRoles", "Roles_Id", c => c.Int());
            AddColumn("dbo.AplicationUserRoles", "ApplicationUser_Id", c => c.Int());
            DropForeignKey("dbo.RolesApplicationUsers", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.RolesApplicationUsers", "Roles_ID", "dbo.Roles");
            DropIndex("dbo.RolesApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.RolesApplicationUsers", new[] { "Roles_ID" });
            AlterColumn("dbo.Products", "Name", c => c.String());
            DropColumn("dbo.Roles", "role");
            DropTable("dbo.RolesApplicationUsers");
            CreateIndex("dbo.AplicationUserRoles", "Roles_Id");
            CreateIndex("dbo.AplicationUserRoles", "ApplicationUser_Id");
            AddForeignKey("dbo.AplicationUserRoles", "Roles_Id", "dbo.Roles", "Id");
            AddForeignKey("dbo.AplicationUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers", "Id");
        }
    }
}
