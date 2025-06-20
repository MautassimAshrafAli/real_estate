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
    public class propertiesController : ApiController
    {
        private real_estateEntities1 db = new real_estateEntities1();

        // GET: api/properties
        public IQueryable<VT_Properties> Getproperties()
        {
            return db.VT_Properties;
        }

        // GET: api/properties/5
        [ResponseType(typeof(VT_Properties))]
        public IHttpActionResult Getproperty(int id)
        {
            VT_Properties property = db.VT_Properties.SingleOrDefault(i => i.id_properties == id);
            if (property == null)
            {
                return NotFound();
            }

            return Ok(property);
        }

        // PUT: api/properties/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putproperty(int id, property property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != property.id_properties)
            {
                return BadRequest();
            }

            db.Entry(property).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!propertyExists(id))
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

        // POST: api/properties
        [ResponseType(typeof(property))]
        public IHttpActionResult Postproperty(property property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.properties.Add(property);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = property.id_properties }, property);
        }

        // DELETE: api/properties/5
        [ResponseType(typeof(property))]
        public IHttpActionResult Deleteproperty(int id)
        {
            property property = db.properties.Find(id);
            if (property == null)
            {
                return NotFound();
            }

            db.properties.Remove(property);
            db.SaveChanges();

            return Ok(property);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool propertyExists(int id)
        {
            return db.properties.Count(e => e.id_properties == id) > 0;
        }
    }
}