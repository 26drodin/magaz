using magaz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeFirst;
using Microsoft.AspNet.Identity;

namespace magaz.Controllers
{
    public class ShopController : Controller
    {
        DataManager man = new DataManager();

        // GET: Shop
        public ActionResult List()
        {

            ViewBag.roles = man.CustomRole.GetRoles(Convert.ToInt32(HttpContext.User.Identity.GetUserId()));

            return View(man.ShopModel.GetListShop());
        }
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {

            if ( man.CustomRole.IsInRole(Convert.ToInt32(HttpContext.User.Identity.GetUserId()), "employee"))
                return View(new Shop());
            else
                return RedirectToAction("Error", "Home", new { });
        }
        [HttpPost]
        [Authorize]
        public ActionResult Create(Shop shop)
        {
            man.ShopModel.AddShop(shop);
            return RedirectToAction("List");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Delete(int id)
        {
            if (man.CustomRole.IsInRole(Convert.ToInt32(HttpContext.User.Identity.GetUserId()), "employee")) {
            man.ShopModel.Remove(id);
            return RedirectToAction("List");
            }
        
           else
                return RedirectToAction("Error", "Home", new { });
        }


        public ActionResult Details(int id)
        {
            return View(man.ShopModel.GetShopByID(id));
        }

        public ActionResult Products(int id)
        {

            if (man.CustomRole.IsInRole(Convert.ToInt32(HttpContext.User.Identity.GetUserId()), "employee"))
            {
                ViewBag.Shop = man.ShopModel.GetShopByID(id);
            return View(man.ShopModel.GetProds(id));
            }
            else
                return RedirectToAction("Error", "Home", new { });


        }

        [Authorize]
        public ActionResult ChooseShop(int prod_id)
        {

            if ( man.CustomRole.IsInRole(Convert.ToInt32(HttpContext.User.Identity.GetUserId()), "employee"))
            {
                ViewBag.ProdId = prod_id;
                return View(man.ShopModel.GetListShop());
            }
            else
                return RedirectToAction("Error", "Home", new { });



        }




        [HttpGet]
        [Authorize]
        public ActionResult AddProduct(int shop_id, int prod_id)
        {


            if (man.CustomRole.IsInRole(Convert.ToInt32(HttpContext.User.Identity.GetUserId()), "admin") || man.CustomRole.IsInRole(Convert.ToInt32(HttpContext.User.Identity.GetUserId()), "employee"))
            {
                var a = new Shop_Prod();
                a.Product = man.ProductModel.GetProduct(prod_id);
                a.Shop = man.ShopModel.GetShopByID(shop_id);

                // ViewBag.ShopID = shop_id;
                //  ViewBag.ProdID = prod_id;
                if (man.ShopModel.CheckProd(prod_id, shop_id) != null)
                    return RedirectToAction("Edit_Prod", new { shop_prod_id = man.ShopModel.CheckProd(prod_id, shop_id).ID });
                return View(a);
            }
            else
                return RedirectToAction("Error", "Shared", new { });



        }
        [HttpPost]
        [Authorize]
        public ActionResult AddProduct(Shop_Prod prod, int product, int shop)
        {

            man.ShopModel.AddProd(prod, shop, product);
            return RedirectToAction("Products", new { id = prod.Shop.ID });
        }
        [HttpGet]
        public ActionResult Edit_Prod(int shop_prod_id)
        {


            if (man.CustomRole.IsInRole(Convert.ToInt32(HttpContext.User.Identity.GetUserId()), "admin") || man.CustomRole.IsInRole(Convert.ToInt32(HttpContext.User.Identity.GetUserId()), "employee"))
            {
                return View(man.ShopModel.GetProd(shop_prod_id));
            }
            else
                return RedirectToAction("Error", "Shared", new { });



        }
        [HttpPost]
        public ActionResult Edit_prod(Shop_Prod prod, int product, int shop)
        {
            man.ShopModel.EditProds(prod, shop, product);
            return RedirectToAction("Products", new { id = shop });
        }
    }
}