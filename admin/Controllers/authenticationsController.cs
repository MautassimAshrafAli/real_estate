using admin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace admin.Controllers
{
    public class authenticationsController : Controller
    {
        private real_estateEntities db = new real_estateEntities();

        // GET: authentications
        public ActionResult Index()
        {
            TempData["login"] = "done";
            TempData["msg"] = "";
            return View(db.portal_users.ToList());
        }


        public JsonResult checkEmailAvailability(string email)
        {
            try
            {
                System.Threading.Thread.Sleep(100);

                var data = db.portal_users.Where(x => x.login == email).FirstOrDefault();
                if (data != null)
                {
                    return Json(1);
                }
                else
                {
                    return Json(0);
                }
            }

            catch (Exception)
            { return Json(0); }
        }
        // GET: authentications/Details/5
        public ActionResult Details(int? id)
        {
            TempData["login"] = "done";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            portal_users portal_users = db.portal_users.Find(id);
            if (portal_users == null)
            {
                return HttpNotFound();
            }
            return View(portal_users);
        }

        // GET: authentications/Create
        public ActionResult Create()
        {
            TempData["login"] = "done";
            return View();
        }

        // POST: authentications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,company_id,user_id,login,password")] portal_users portal_users)
        {
            TempData["login"] = "done";

            if (ModelState.IsValid)
            {
                db.portal_users.Add(portal_users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(portal_users);
        }

        // GET: authentications/Edit/5
        public ActionResult Edit(int? id)
        {
            TempData["login"] = "done";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            portal_users portal_users = db.portal_users.Find(id);
            Session["email"] = portal_users.login;
            if (portal_users == null)
            {
                return HttpNotFound();
            }
            return View(portal_users);
        }

        // POST: authentications/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,company_id,user_id,login,password")] portal_users portal_users)
        {
            TempData["login"] = "done";

            if (ModelState.IsValid)
            {
                db.Entry(portal_users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(portal_users);
        }

        // GET: authentications/Delete/5
        public ActionResult Delete(int? id)
        {
            TempData["login"] = "done";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            portal_users portal_users = db.portal_users.Find(id);
            if (portal_users == null)
            {
                return HttpNotFound();
            }
            return View(portal_users);
        }

        // POST: authentications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TempData["login"] = "done";

            portal_users portal_users = db.portal_users.Find(id);
            db.portal_users.Remove(portal_users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();

            return View();
        }

        [HttpPost]
        public ActionResult Auth(portal_users Users)
        {

            var user = db.portal_users.Where(x => x.login == Users.login  && x.password == Users.password).FirstOrDefault();
            if (user == null)
            {
                if (Users.login == null && Users.password == null)
                {
                    TempData["msg"] = "User name and Password field is empty";
                    
                }
                else if (Users.password == null)
                {
                    TempData["msg"] = "Password field is empty";
                }
                else if (Users.login == null)
                {
                    TempData["msg"] = "User Name field is empty";
           
                }
                else
                {
                    TempData["msg"] = "Wrong User Name or Password";
                }
                return RedirectToAction("Login", Users);
            }
            else
            {
                Session["UserID"] = user.id;
                Session["login"] = user.login;
                return RedirectToAction("Index", "Home");
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
