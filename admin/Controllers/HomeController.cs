using admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        real_estateEntities db = new real_estateEntities();
        public ActionResult Index()
        {
            TempData["login"] = "done";

            counter_page();
            return View();
        }

        public void counter_page()
        {
            ViewBag.users_count = db.users.Count().ToString();
            ViewBag.authentications_count = db.portal_users.Count().ToString();

        }

        public ActionResult Delete(int id)
        {
            return RedirectToAction("Index","Home");

        }
      
    }
}