using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using magaz.IdentityFolder;
using CodeFirst;
using System.Threading.Tasks;
using magaz.Models;
using Microsoft.Owin.Security;
using System.Security.Claims;

namespace magaz.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        DataManager man = new DataManager();
        public ActionResult Index()
        {

            if (man.CustomRole.IsInRole(Convert.ToInt32(HttpContext.User.Identity.GetUserId()), "admin"))
            {
                return View(UserManager.Users);
            }
            else
                return RedirectToAction("Error","Home",new { });


          
        }


        private CustomUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<CustomUserManager>();
            }
        }
        public ActionResult Create()
        {
            return View(new RegisterModel());
        }
        [HttpPost]
        public async Task<ActionResult> Create(RegisterModel model)
        {
      

            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.UserName, Email = model.Email,FIO=model.FIO,PhoneNumber=model.PhoneNumber };
                
                IdentityResult result =
                    await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    man.CartModel.AddCart(user.Id);
                    man.CustomRole.AddToRole("user", user.Id);
                    return RedirectToAction("Index","Home",new { });
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(model);
        }
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel details, string returnUrl)
        {
            ApplicationUser user = await UserManager.FindAsync(details.Name, details.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Некорректное имя или пароль.");
            }
            else
            {
                ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);

                AuthManager.SignOut();
                AuthManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = false
                }, ident);
                return Redirect(returnUrl);
            }

            return View(details);
        }
        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return Redirect("~/Home/Index");
        }

        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        public ActionResult Edit()
        {
            ApplicationUser user = UserManager.FindById(Convert.ToInt32(HttpContext.User.Identity.GetUserId()));
            return View(user);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ApplicationUser user, HttpPostedFileBase image1)
        {
            if (image1 != null)
            {
                user.Picture = new byte[image1.ContentLength];
                image1.InputStream.Read(user.Picture, 0, image1.ContentLength);
            }
            IdentityResult result = await UserManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Cab","Account",new { });
            }
            else
            {
                AddErrorsFromResult(result);
            }
            return View(user);
        }

        public ActionResult AdminEdit(int user_id)
        {

            ApplicationUser user = UserManager.FindById(user_id);
            if (man.CustomRole.IsInRole(user.Id, "admin"))
            {
                return View(user);
            }
            else
                return RedirectToAction("Error", "Home", new { });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AdminEdit(ApplicationUser user, HttpPostedFileBase image1)
        {
            if (image1 != null)
            {
                user.Picture = new byte[image1.ContentLength];
                image1.InputStream.Read(user.Picture, 0, image1.ContentLength);
            }
            IdentityResult result = await UserManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Cab", "Account", new { });
            }
            else
            {
                AddErrorsFromResult(result);
            }
            return View(user);
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditRoles(int user_id)
        {
           
            if (man.CustomRole.IsInRole(Convert.ToInt32(HttpContext.User.Identity.GetUserId()), "admin"))
            {

                ViewBag.Roles = man.CustomRole.AllRoles();

                return View(UserManager.FindById(user_id));
            }
            else
                return RedirectToAction("Error", "Home", new { });

        }

        [Authorize]
        [HttpPost]
        public ActionResult EditRoles(List<string> roles,ApplicationUser user)
        {

            man.CustomRole.RemoveRoles(user.Id);
            foreach (var item in roles)
                man.CustomRole.AddToRole(item, user.Id);
            return RedirectToAction("Index");


        }

        public ActionResult Delete(int user_id)
        {

            ApplicationUser user = UserManager.FindById(user_id);
            if (man.CustomRole.IsInRole(Convert.ToInt32(HttpContext.User.Identity.GetUserId()), "admin"))
            {

                UserManager.DeleteAsync(user);


                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Error", "Home", new { });
        }



    }
}