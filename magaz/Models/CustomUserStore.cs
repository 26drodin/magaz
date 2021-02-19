using CodeFirst;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace magaz.Models
{
    public class CustomUserStore :
     IUserStore<ApplicationUser, int>,
     IUserPasswordStore<ApplicationUser, int>,
     IQueryableUserStore<ApplicationUser, int>
    {
        static readonly List<ApplicationUser> Users = new List<ApplicationUser>();
        DataContext db;

        IQueryable<ApplicationUser> IQueryableUserStore<ApplicationUser, int>.Users => this.db.ApplicationUser;

        public CustomUserStore()
        {
            this.db = new DataContext();
        }
        public void Dispose()
        {

        }

        public Task CreateAsync(ApplicationUser user)
        {
            return Task.Factory.StartNew(() => { db.ApplicationUser.Add(user); db.SaveChanges(); });
        }

        public Task UpdateAsync(ApplicationUser user)
        {
            return Task.Factory.StartNew(() => { db.Entry(user).State = EntityState.Modified;  db.SaveChanges(); });
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            return Task.Factory.StartNew(() => {
                Cart cart = user.Cart;
                db.ApplicationUser.Remove(user);
                db.Carts.Remove(cart);
                db.Comments.RemoveRange(db.Comments.Where(i=>i.Account.Id==user.Id));
                db.Reviews.RemoveRange(db.Reviews.Where(i => i.Account.Id == user.Id));
                db.Orders.RemoveRange(db.Orders.Where(i => i.Account.Id == user.Id));
                db.SaveChanges();

            });
        }

        public Task<ApplicationUser> FindByIdAsync(int userId)
        {
            return Task<ApplicationUser>.Factory.StartNew(() => db.ApplicationUser.Find(userId));
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return Task<ApplicationUser>.Factory.StartNew(() => db.ApplicationUser.FirstOrDefault(u => u.UserName == userName));
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            return Task.FromResult(user.PasswordHash != String.Empty);
        }
    }
}