using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using real_estate.Models;

namespace real_estate.Controllers
{
    public class properties_infoController : ApiController
    {
        private real_estateEntities1 db = new real_estateEntities1();

        // GET: api/properties_info
        public IQueryable<VT_properties_info> Getproperties_info()
        {
            return db.VT_properties_info;
        }

        // GET: api/properties_info/5
        [ResponseType(typeof(VT_properties_info))]
        public IHttpActionResult Getproperties_info(int id)
        {
            VT_properties_info properties_info = db.VT_properties_info.SingleOrDefault(i => i.id == id);
            if (properties_info == null)
            {
                return NotFound();
            }

            return Ok(properties_info);
        }

        // PUT: api/properties_info/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putproperties_info(int id, properties_info properties_info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != properties_info.id)
            {
                return BadRequest();
            }

            db.Entry(properties_info).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!properties_infoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/properties_info
        [ResponseType(typeof(properties_info))]
        public IHttpActionResult Postproperties_info(properties_info properties_info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.properties_info.Add(properties_info);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = properties_info.id }, properties_info);
        }

        // DELETE: api/properties_info/5
        [ResponseType(typeof(properties_info))]
        public IHttpActionResult Deleteproperties_info(int id)
        {
            properties_info properties_info = db.properties_info.Find(id);
            if (properties_info == null)
            {
                return NotFound();
            }

            db.properties_info.Remove(properties_info);
            db.SaveChanges();

            return Ok(properties_info);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool properties_infoExists(int id)
        {
            return db.properties_info.Count(e => e.id == id) > 0;
        }
    }
}