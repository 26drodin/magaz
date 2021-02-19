using CodeFirst;
using magaz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace magaz.Controllers
{
    public class ProductController : Controller
    { 
        DataManager man = new DataManager();
        public ActionResult List(int id)
        {
            ViewBag.roles = man.CustomRole.GetRoles(Convert.ToInt32(HttpContext.User.Identity.GetUserId()));
            ViewBag.Names = man.ProductModel.GetPropsNames(id);
            ViewBag.Values = man.ProductModel.GetPropsValues(man.ProductModel.GetPropsNames(id),id); 
            ViewBag.Category = man.CategoryModel.GetCat(id);
            return View();
        }

        public ActionResult ListAll(string search)
        {
            ViewBag.serach = search;
            ViewBag.roles = man.CustomRole.GetRoles(Convert.ToInt32(HttpContext.User.Identity.GetUserId()));
            return View("ListAll");
        }
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            if (man.CustomRole.IsInRole(Convert.ToInt32(HttpContext.User.Identity.GetUserId()), "admin") || man.CustomRole.IsInRole(Convert.ToInt32(HttpContext.User.Identity.GetUserId()), "employee"))
            {
                Product a = new Product();
                ViewBag.Categories = man.CategoryModel.GetListCat();
                return View(a);
            }
            else
                return RedirectToAction("Error", "Home", new { });
        }
        [Authorize]
        [HttpPost]
        public ActionResult Create(Product product,HttpPostedFileBase image1, int ID)
        {   
            man.ProductModel.AddProd(product,image1,ID);
            man.ProductModel.CreateProps(product.ID);
            return RedirectToAction("Create2",new {id=product.ID });
        }
        [Authorize]
        [HttpGet]
        public ActionResult Create2(int id)
        {
            if (man.CustomRole.IsInRole(Convert.ToInt32(HttpContext.User.Identity.GetUserId()), "admin") || man.CustomRole.IsInRole(Convert.ToInt32(HttpContext.User.Identity.GetUserId()), "employee"))
            {
                return View(man.ProductModel.GetProduct(id).Props);
            }
            else
                return RedirectToAction("Error", "Home", new { });

        }

        [Authorize]
        [HttpPost]
        public ActionResult Create2(List<Prop_Prod> props, int product_id)
        {
            man.ProductModel.EditProps(props, product_id);
            return RedirectToAction("List",new {id=props.First().Product.Category.ID });
        }


        [Authorize]
        [HttpGet]
        public ActionResult Edit(int prod_id)
        {
            if (man.CustomRole.IsInRole(Convert.ToInt32(HttpContext.User.Identity.GetUserId()), "employee"))
            {               
                return View(man.ProductModel.GetProduct(prod_id));
            }
            else
                return RedirectToAction("Error", "Home", new { });
        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image1)
        {

            man.ProductModel.Edit(product, image1);
            return RedirectToAction("Create2", new { id = product.ID });
        }
        [Authorize]
        [HttpGet]
        public ActionResult Delete(int prod_id)
        {
            int category = man.ProductModel.GetProduct(prod_id).Category.ID;
            man.ProductModel.DeleteProd(man.ProductModel.GetProduct(prod_id));
            return RedirectToAction("List", new { id = category });
        }


        public ActionResult Details(int id)
        {
            ViewBag.roles = man.CustomRole.GetRoles(Convert.ToInt32(HttpContext.User.Identity.GetUserId()));
            ViewBag.Shops = man.ProductModel.GetShops(id);
            return View(man.ProductModel.GetProduct(id));
            
        }

        [Authorize]
        public ActionResult AddToCart(int idd)
        { 

            man.CartModel.Addprod(Convert.ToInt32(HttpContext.User.Identity.GetUserId()), idd);
            return RedirectToAction("Cart", "Account", new { });
        }

        public ActionResult AjaxList(string search,int cat_id)
        {
            ViewBag.roles = man.CustomRole.GetRoles(Convert.ToInt32(HttpContext.User.Identity.GetUserId()));
            return PartialView(man.ProductModel.Search(search,cat_id));
        }
        public ActionResult AjaxListFiltered(Dictionary<string,List<string>> MyDictionary, int cat_id)
        {
            ViewBag.roles = man.CustomRole.GetRoles(Convert.ToInt32(HttpContext.User.Identity.GetUserId()));
            return PartialView(man.ProductModel.Filter(MyDictionary, cat_id));
        }
        [HttpPost]
        [Authorize]
        public ActionResult AddReview(Product model,string text,string purl,short rate)
        {
            man.ProductModel.AddReview(text,Convert.ToInt32(HttpContext.User.Identity.GetUserId()),model.ID, rate);
            return RedirectToAction("Details",new { id=model.ID});
        }



    }
}