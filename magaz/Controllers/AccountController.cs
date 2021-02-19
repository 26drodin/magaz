using magaz.IdentityFolder;
using magaz.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeFirst;

namespace magaz.Controllers
{
    public class AccountController : Controller
    {
        DataManager man = new DataManager();
        // GET: Account
        [Authorize]
        public ActionResult Cab()
        {

            ViewBag.Roles = man.CustomRole.GetRoles(Convert.ToInt32(HttpContext.User.Identity.GetUserId()));
            // ViewBag.Account = Convert.ToInt32(HttpContext.User.Identity.GetUserId());

            return View(UserManager.FindById(Convert.ToInt32(HttpContext.User.Identity.GetUserId())));
        }
        private CustomUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<CustomUserManager>();
            }
        }
        [Authorize]
        public ActionResult Orders()
        {

            return View(UserManager.FindById(Convert.ToInt32(HttpContext.User.Identity.GetUserId())).Orders);
        }
        [Authorize]
        public ActionResult Cart()
        {

            return View(UserManager.FindById(Convert.ToInt32(HttpContext.User.Identity.GetUserId())).Cart.Products);
        }
        [Authorize]
        public ActionResult ClearCart()
        {
            man.CartModel.ClearCart(Convert.ToInt32(HttpContext.User.Identity.GetUserId()));
            return RedirectToAction("Cart", "Account");
        }



        [Authorize]

        public ActionResult MakeOrder(int prod_id)
        {
            ViewBag.Shops = man.ProductModel.GetShopsNames(prod_id);
            return View(new Order() { Product = man.ProductModel.GetProduct(prod_id) });
        }
        [Authorize]
        [HttpPost]
        public ActionResult MakeOrder(Order order, int ID,int prod)
        {

            if (man.OrderModel.MakeOrder(order, Convert.ToInt32(HttpContext.User.Identity.GetUserId()), ID,prod))
            {
            
                return RedirectToAction( "Cart");
            }
            else return RedirectToAction("Error", "Account");
        }
     


        public ActionResult Error() { return View(); }
    }
}