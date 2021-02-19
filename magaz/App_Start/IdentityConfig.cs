using CodeFirst;
using magaz.IdentityFolder;
using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using magaz.Models;

namespace magaz.App_Start
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<DataContext>(DataContext.Create);
            app.CreatePerOwinContext<CustomUserManager>(CustomUserManager.Create);
      //      app.CreatePerOwinContext<AppRoleManager>(AppRoleManager.Create);


            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Admin/Login"),
            });
        }
    }
}