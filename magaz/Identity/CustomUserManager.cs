using CodeFirst;
using magaz.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace magaz.IdentityFolder
{
    public class CustomUserManager : UserManager<ApplicationUser, int>, IUserPasswordStore<ApplicationUser, int>
    {
        public CustomUserManager(IUserStore<ApplicationUser, int> store)
            : base(store)
        { }

        public static CustomUserManager Create(IdentityFactoryOptions<CustomUserManager> options,
            IOwinContext context)
        {
            DataContext db = context.Get<DataContext>();
            CustomUserManager manager = new CustomUserManager(new CustomUserStore())
            {
                PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 6,
                    RequireNonLetterOrDigit = false,
                    RequireDigit = false,
                    RequireLowercase = true,
                    RequireUppercase = true
                },
            };
            manager.UserValidator = new CustomUserValidator();
            return manager;
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            throw new NotImplementedException();
        }

        Task IUserStore<ApplicationUser, int>.CreateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        Task IUserStore<ApplicationUser, int>.DeleteAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        Task IUserStore<ApplicationUser, int>.UpdateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }

}