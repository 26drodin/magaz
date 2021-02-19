using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using CodeFirst;
using magaz.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using magaz.IdentityFolder;

namespace magaz.Controllers
{
    public class HomeController : Controller
    {
        DataManager man = new DataManager();

      
        public ActionResult Index()
        {

            ViewBag.Best = man.OrderModel.GetBest();
            ViewBag.News = man.NewsModel.GetList().Take(8).ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.roles = man.CustomRole.GetRoles(Convert.ToInt32(HttpContext.User.Identity.GetUserId()));
          
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        public ActionResult List()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
        /*    [HttpGet]
            public ActionResult Create()
            {

                return View(new Account());
            }
            [HttpPost]
            public ActionResult Create(Account account)
            {

                man.AccountModel.AddAccount(account);   
                return RedirectToAction("List");
            }
            */

    }
}