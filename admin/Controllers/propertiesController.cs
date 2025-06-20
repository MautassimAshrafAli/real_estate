using admin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace admin.Controllers
{
    public class propertiesController : Controller
    {
        private real_estateEntities db = new real_estateEntities();

        // GET: properties
        public ActionResult Index()
        {
            TempData["login"] = "done";
            TempData["msg"] = "";
            var properties = db.properties;
            return View(properties.ToList());
        }

        // GET: properties/Details/5
        public ActionResult Details(int? id)
        {
            TempData["login"] = "done";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            property properties = db.properties.Find(id);
            if (properties == null)
            {
                return HttpNotFound();
            }
            return View(properties);
        }

        // GET: properties/Create
        public ActionResult Create()
        {
            TempData["login"] = "done";
            return View();
        }

        // POST: properties/Create
        [HttpPost]
        public ActionResult Create(property property)
        {
            TempData["login"] = "done";
            if (ModelState.IsValid)
            {
                db.properties.Add(property);
                db.SaveChanges();
                TempData["msg"] = "Department \'" + property.id_properties + "\' is Create";
                return RedirectToAction("Index");
            }
            return View(property);
            
        }

        // GET: properties/Edit/5
        public ActionResult Edit(int? id)
        {
            TempData["login"] = "done";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            property property = db.properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // POST: properties/Edit/5
        [HttpPost]
        public ActionResult Edit(property property)
        {
            TempData["login"] = "done";

            if (ModelState.IsValid)
            {
                db.Entry(property).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Department \'" + property.id_properties + "\' is update";
                return RedirectToAction("Index");
            }

            return View(property);
            
        }

        // GET: properties/Delete/5
        public ActionResult Delete(int? id)
        {
            TempData["login"] = "done";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            property property = db.properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }
        // POST: departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TempData["login"] = "done";

            property property = db.properties.Find(id);
            db.properties.Remove(property);
            db.SaveChanges();
            TempData["msg"] = "Department \'" + property.id_properties + "\' is Delete";
            return RedirectToAction("Index");
        }

       
    }
}
