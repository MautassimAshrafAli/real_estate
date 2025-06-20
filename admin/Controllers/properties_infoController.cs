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
    public class properties_infoController : Controller
    {
        private real_estateEntities db = new real_estateEntities();

        // GET: properties_info
        public ActionResult Index()
        {
            var properties_info = db.properties_info.Include(p => p.property);
            return View(properties_info.ToList());
        }

        // GET: properties_info/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            properties_info properties_info = db.properties_info.Find(id);
            if (properties_info == null)
            {
                return HttpNotFound();
            }
            return View(properties_info);
        }

        // GET: properties_info/Create
        public ActionResult Create()
        {
            ViewBag.id_properties = new SelectList(db.properties, "id_properties", "type");
            return View();
        }

        // POST: properties_info/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_properties,images,city,address,info_propertie,year_built,Bedrooms,Bathrooms,Area,Floor,Parking,price,currency,Payment_method,long_address,building_type,Display_type,Furnishing,publich_date,map_link,youtube_video_link")] properties_info properties_info)
        {
            if (ModelState.IsValid)
            {
                db.properties_info.Add(properties_info);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_properties = new SelectList(db.properties, "id_properties", "type", properties_info.id_properties);
            return View(properties_info);
        }

        // GET: properties_info/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            properties_info properties_info = db.properties_info.Find(id);
            if (properties_info == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_properties = new SelectList(db.properties, "id_properties", "type", properties_info.id_properties);
            return View(properties_info);
        }

        // POST: properties_info/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_properties,images,city,address,info_propertie,year_built,Bedrooms,Bathrooms,Area,Floor,Parking,price,currency,Payment_method,long_address,building_type,Display_type,Furnishing,publich_date,map_link,youtube_video_link")] properties_info properties_info)
        {
            if (ModelState.IsValid)
            {
                db.Entry(properties_info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_properties = new SelectList(db.properties, "id_properties", "type", properties_info.id_properties);
            return View(properties_info);
        }

        // GET: properties_info/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            properties_info properties_info = db.properties_info.Find(id);
            if (properties_info == null)
            {
                return HttpNotFound();
            }
            return View(properties_info);
        }

        // POST: properties_info/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            properties_info properties_info = db.properties_info.Find(id);
            db.properties_info.Remove(properties_info);
            db.SaveChanges();
            return RedirectToAction("Index");
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
