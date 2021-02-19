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
    public class NewsController : Controller
    {
        DataManager man = new DataManager();
        // GET: News
        public ActionResult List()
        {
            ViewBag.roles = man.CustomRole.GetRoles(Convert.ToInt32(HttpContext.User.Identity.GetUserId()));
            return View(man.NewsModel.GetList());
        }
        public ActionResult Details(int id)
        {
            ViewBag.roles = man.CustomRole.GetRoles(Convert.ToInt32(HttpContext.User.Identity.GetUserId()));
            return View(man.NewsModel.GetNews(id));
        }
        [Authorize]
        public ActionResult Create()
        {


            if (man.CustomRole.IsInRole(Convert.ToInt32(HttpContext.User.Identity.GetUserId()), "employee"))
            {
                return View(new News());
            }
            else
                return RedirectToAction("Error", "Home", new { });
        }
        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(News news, HttpPostedFileBase image)
        {

            man.NewsModel.AddNews(news,image);
            return RedirectToAction("List");
        }
        [HttpGet]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Edit(int id)
        {

            if (man.CustomRole.IsInRole(Convert.ToInt32(HttpContext.User.Identity.GetUserId()), "employee"))
            {
                return View(man.NewsModel.GetNews(id));
            }
            else
                return RedirectToAction("Error", "Home", new { });
        }

       

        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Edit( News news, HttpPostedFileBase image)
        {
            man.NewsModel.Edit(news, image);
            return RedirectToAction("List");
        }


        [Authorize]
        public ActionResult Delete(int id)
        {
            if (man.CustomRole.IsInRole(Convert.ToInt32(HttpContext.User.Identity.GetUserId()), "employee"))
            {
                man.NewsModel.Remove(id);
                return View(new News());
            }
            else
                return RedirectToAction("Error", "Home", new { });
        }




        [HttpGet]
        public ActionResult Comment()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Comment(string text, News model)
        {
            if ((text != "")&&(HttpContext.User.Identity.IsAuthenticated))
            {
                man.NewsModel.AddComment(model.ID, text, Convert.ToInt32(HttpContext.User.Identity.GetUserId()));
            }
            var comments = man.NewsModel.GetComments(model.ID);
            if (comments.Count <= 0)
            {
                return HttpNotFound();
            }
            return PartialView(comments);
        }


    }
}