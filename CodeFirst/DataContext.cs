using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst
{
  public  class DataContext : DbContext
    {
        public DataContext() : base ("Comp_Shop")
            {}
        public static DataContext Create(IdentityFactoryOptions<DataContext> options,
    IOwinContext context)
        {
            DataContext db = new DataContext();
            return db;
        }
     //   public DbSet<ApplicationUser> Accounts { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shop> Shops  { get; set; }
        public DbSet<Review> Reviews  { get; set; }
        public DbSet<Category> Categories  { get; set; }
        public DbSet<Prop_Cat> Props_Cat  { get; set; }
        public DbSet<Prop_Prod> Props_Prod { get; set; }
        public DbSet<Shop_Prod> Shop_Prods { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Cart> Carts { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<AplicationUserRole> AplicationUserRole { get; set; }
        public DbSet<AplicationUserClaim> AplicationUserClaim { get; set; }
        public DbSet<Roles> Roles { get; set; }

        public System.Data.Entity.DbSet<CodeFirst.Comment> Comments { get; set; }
    }
}
