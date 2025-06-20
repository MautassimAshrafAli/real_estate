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
    public class agent_infoController : Controller
    {
        private real_estateEntities db = new real_estateEntities();

        // GET: agent_info
        public ActionResult Index()
        {
            var agent_info = db.agent_info.Include(a => a.property);
            return View(agent_info.ToList());
        }

        // GET: agent_info/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            agent_info agent_info = db.agent_info.Find(id);
            if (agent_info == null)
            {
                return HttpNotFound();
            }
            return View(agent_info);
        }

        // GET: agent_info/Create
        public ActionResult Create()
        {
            ViewBag.id_properties = new SelectList(db.properties, "id_properties", "type");
            return View();
        }

        // POST: agent_info/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_properties,agent,national_number,address,email,phone_number,whatsapp_ink")] agent_info agent_info)
        {
            if (ModelState.IsValid)
            {
                db.agent_info.Add(agent_info);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_properties = new SelectList(db.properties, "id_properties", "type", agent_info.id_properties);
            return View(agent_info);
        }

        // GET: agent_info/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            agent_info agent_info = db.agent_info.Find(id);
            if (agent_info == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_properties = new SelectList(db.properties, "id_properties", "type", agent_info.id_properties);
            return View(agent_info);
        }

        // POST: agent_info/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_properties,agent,national_number,address,email,phone_number,whatsapp_ink")] agent_info agent_info)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agent_info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_properties = new SelectList(db.properties, "id_properties", "type", agent_info.id_properties);
            return View(agent_info);
        }

        // GET: agent_info/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            agent_info agent_info = db.agent_info.Find(id);
            if (agent_info == null)
            {
                return HttpNotFound();
            }
            return View(agent_info);
        }

        // POST: agent_info/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            agent_info agent_info = db.agent_info.Find(id);
            db.agent_info.Remove(agent_info);
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
