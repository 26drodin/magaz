using CodeFirst;
using magaz.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace magaz.Controllers
{
 
    public class CategoryController : Controller
    {
        int ID;
        DataManager man = new DataManager();
        // GET: Category
        [Authorize]
        public ActionResult List()
        {


            if ( man.CustomRole.IsInRole(Convert.ToInt32(HttpContext.User.Identity.GetUserId()), "employee"))
            {
                return View(man.CategoryModel.GetListCat());
            }
            else
                return RedirectToAction("Error", "Home", new { });
        }


        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {


            if ( man.CustomRole.IsInRole(Convert.ToInt32(HttpContext.User.Identity.GetUserId()), "employee"))
            {
                return View(new Category());
            }
            else
                return RedirectToAction("Error", "Home", new { });
        }
        [Authorize]
        [HttpPost]
        public ActionResult Create(Category category)
        {
      
            man.CategoryModel.AddCategory(category);           
            return RedirectToAction("Edit",new { id=category.ID});
        }
        [Authorize]
        [HttpGet]
        public ActionResult Edit(int id)
        {


            if ( man.CustomRole.IsInRole(Convert.ToInt32(HttpContext.User.Identity.GetUserId()), "employee"))
            {

                ID = id;
            return View(man.CategoryModel.GetCat(id));
            }
            else
                return RedirectToAction("Error", "Home", new { });
        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            man.CategoryModel.Edit(category);
            return RedirectToAction("List");
        }
        [Authorize]
        [HttpGet]
        public ActionResult Prop()
        {


            if (man.CustomRole.IsInRole(Convert.ToInt32(HttpContext.User.Identity.GetUserId()), "employee"))
            {
                return PartialView();
            }
            else
                return RedirectToAction("Error", "Home", new { });
        }
        [Authorize]
        [HttpPost]
        public ActionResult Prop(string name,int id)
        {
            if (name != "") { 
            man.CategoryModel.AddProp(id, name);
            }
            var allprops = man.CategoryModel.GetProps(id);
            if (allprops.Count <= 0)
            {
                return HttpNotFound();
            }
            return PartialView(allprops);
        }


        [Authorize]
        [HttpGet]
        public ActionResult DeleteProp(int id,int cat_id)
        {
            man.CategoryModel.DeleteProp(id);
          return  RedirectToAction("Edit", new { id = cat_id });
        }

    }
}