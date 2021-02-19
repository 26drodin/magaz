namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AplicationUserClaims",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Id = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.AplicationUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        ApplicationUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Password = c.String(),
                        FIO = c.String(),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                        Cart_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carts", t => t.Cart_ID)
                .Index(t => t.Cart_ID);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        Picture = c.Binary(storeType: "image"),
                        Category_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.Category_ID)
                .Index(t => t.Category_ID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Prop_Cat",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Category_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.Category_ID)
                .Index(t => t.Category_ID);
            
            CreateTable(
                "dbo.Prop_Prod",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.String(),
                        Product_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.Product_ID)
                .Index(t => t.Product_ID);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Rate = c.Short(nullable: false),
                        Text = c.String(),
                        Date = c.DateTime(nullable: false),
                        Account_Id = c.Int(),
                        Product_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ApplicationUsers", t => t.Account_Id)
                .ForeignKey("dbo.Products", t => t.Product_ID)
                .Index(t => t.Account_Id)
                .Index(t => t.Product_ID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Date = c.DateTime(nullable: false),
                        Account_Id = c.Int(),
                        News_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ApplicationUsers", t => t.Account_Id)
                .ForeignKey("dbo.News", t => t.News_ID)
                .Index(t => t.Account_Id)
                .Index(t => t.News_ID);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Picture = c.Binary(storeType: "image"),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Discounts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Bonus = c.Double(nullable: false),
                        Card_num = c.String(),
                        Date = c.DateTime(nullable: false),
                        Account_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ApplicationUsers", t => t.Account_Id)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.ApplicationUserUserLogins",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Status = c.String(),
                        Date = c.DateTime(nullable: false),
                        Account_Id = c.Int(),
                        Product_ID = c.Int(),
                        Shop_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ApplicationUsers", t => t.Account_Id)
                .ForeignKey("dbo.Products", t => t.Product_ID)
                .ForeignKey("dbo.Shops", t => t.Shop_ID)
                .Index(t => t.Account_Id)
                .Index(t => t.Product_ID)
                .Index(t => t.Shop_ID);
            
            CreateTable(
                "dbo.Shops",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Coord_N = c.String(),
                        Coord_E = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Shop_Prod",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Product_ID = c.Int(),
                        Shop_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.Product_ID)
                .ForeignKey("dbo.Shops", t => t.Shop_ID)
                .Index(t => t.Product_ID)
                .Index(t => t.Shop_ID);
            
            CreateTable(
                "dbo.ProductCarts",
                c => new
                    {
                        Product_ID = c.Int(nullable: false),
                        Cart_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_ID, t.Cart_ID })
                .ForeignKey("dbo.Products", t => t.Product_ID, cascadeDelete: true)
                .ForeignKey("dbo.Carts", t => t.Cart_ID, cascadeDelete: true)
                .Index(t => t.Product_ID)
                .Index(t => t.Cart_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AplicationUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Orders", "Shop_ID", "dbo.Shops");
            DropForeignKey("dbo.Shop_Prod", "Shop_ID", "dbo.Shops");
            DropForeignKey("dbo.Shop_Prod", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.Orders", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.Orders", "Account_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Discounts", "Account_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Comments", "News_ID", "dbo.News");
            DropForeignKey("dbo.Comments", "Account_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.AplicationUserClaims", "Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUsers", "Cart_ID", "dbo.Carts");
            DropForeignKey("dbo.Reviews", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.Reviews", "Account_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Prop_Prod", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.Products", "Category_ID", "dbo.Categories");
            DropForeignKey("dbo.Prop_Cat", "Category_ID", "dbo.Categories");
            DropForeignKey("dbo.ProductCarts", "Cart_ID", "dbo.Carts");
            DropForeignKey("dbo.ProductCarts", "Product_ID", "dbo.Products");
            DropIndex("dbo.ProductCarts", new[] { "Cart_ID" });
            DropIndex("dbo.ProductCarts", new[] { "Product_ID" });
            DropIndex("dbo.Shop_Prod", new[] { "Shop_ID" });
            DropIndex("dbo.Shop_Prod", new[] { "Product_ID" });
            DropIndex("dbo.Orders", new[] { "Shop_ID" });
            DropIndex("dbo.Orders", new[] { "Product_ID" });
            DropIndex("dbo.Orders", new[] { "Account_Id" });
            DropIndex("dbo.ApplicationUserUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Discounts", new[] { "Account_Id" });
            DropIndex("dbo.Comments", new[] { "News_ID" });
            DropIndex("dbo.Comments", new[] { "Account_Id" });
            DropIndex("dbo.Reviews", new[] { "Product_ID" });
            DropIndex("dbo.Reviews", new[] { "Account_Id" });
            DropIndex("dbo.Prop_Prod", new[] { "Product_ID" });
            DropIndex("dbo.Prop_Cat", new[] { "Category_ID" });
            DropIndex("dbo.Products", new[] { "Category_ID" });
            DropIndex("dbo.ApplicationUsers", new[] { "Cart_ID" });
            DropIndex("dbo.AplicationUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AplicationUserClaims", new[] { "Id" });
            DropTable("dbo.ProductCarts");
            DropTable("dbo.Shop_Prod");
            DropTable("dbo.Shops");
            DropTable("dbo.Orders");
            DropTable("dbo.ApplicationUserUserLogins");
            DropTable("dbo.Discounts");
            DropTable("dbo.News");
            DropTable("dbo.Comments");
            DropTable("dbo.Reviews");
            DropTable("dbo.Prop_Prod");
            DropTable("dbo.Prop_Cat");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.Carts");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.AplicationUserRoles");
            DropTable("dbo.AplicationUserClaims");
        }
    }
}
