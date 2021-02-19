using CodeFirst;
using magaz.Controllers;
using magaz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ecampus3._0.IdentityFolder
{
    public class AuthCustom: AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.Controller is ICustomController cnt)
            {
                DataManager dataManager = cnt.DataManager;
                CustomUserStore a= new CustomUserStore();
                Task<ApplicationUser> userTask= a.FindByNameAsync(filterContext.HttpContext.User.Identity.Name);
                ApplicationUser user = userTask.Result;
            }
            base.OnAuthorization(filterContext);
        }
        
    }

}