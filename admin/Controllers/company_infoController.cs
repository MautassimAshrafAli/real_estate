using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using admin.Models;

namespace admin.Controllers
{
    public class company_infoController : Controller
    {
        private real_estateEntities db = new real_estateEntities();

        // GET: company_info
        public ActionResult Index()
        {
            TempData["login"] = "done";
            return View(db.company_info.ToList());
        }

        // GET: company_info/Details/5
        public ActionResult Details(int? id)
        {
            TempData["login"] = "done";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            company_info company_info = db.company_info.Find(id);
            if (company_info == null)
            {
                return HttpNotFound();
            }
            return View(company_info);
        }

        // GET: company_info/Create
        public ActionResult Create()
        {
            TempData["login"] = "done";
            return View();
        }

        // POST: company_info/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,ceo_name,company_name,comapny_location_link,address,email,phone_number,facebook_link,instagram_link,twitter_link,linkedin_link,youtube_link,comapny_video_link")] company_info company_info)
        {
            TempData["login"] = "done";
            if (ModelState.IsValid)
            {
                db.company_info.Add(company_info);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(company_info);
        }

        // GET: company_info/Edit/5
        public ActionResult Edit(int? id)
        {
            TempData["login"] = "done";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            company_info company_info = db.company_info.Find(id);
            if (company_info == null)
            {
                return HttpNotFound();
            }
            return View(company_info);
        }

        // POST: company_info/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,ceo_name,company_name,comapny_location_link,address,email,phone_number,facebook_link,instagram_link,twitter_link,linkedin_link,youtube_link,comapny_video_link")] company_info company_info)
        {
            TempData["login"] = "done";
            if (ModelState.IsValid)
            {
                db.Entry(company_info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company_info);
        }

        // GET: company_info/Delete/5
        public ActionResult Delete(int? id)
        {
            TempData["login"] = "done";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            company_info company_info = db.company_info.Find(id);
            if (company_info == null)
            {
                return HttpNotFound();
            }
            return View(company_info);
        }

        // POST: company_info/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TempData["login"] = "done";
            company_info company_info = db.company_info.Find(id);
            db.company_info.Remove(company_info);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            TempData["login"] = "done";
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
