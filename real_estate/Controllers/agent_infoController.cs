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

    public class agent_infoController : ApiController
    {
        private real_estateEntities1 db = new real_estateEntities1();

        // GET: api/agent_info
        public IQueryable<VT_agent_info> Getagent_info()
        {
            return db.VT_agent_info;
        }

        // GET: api/agent_info/5
        [ResponseType(typeof(VT_agent_info))]
        public IHttpActionResult Getagent_info(int id)
        {
            VT_agent_info agent_info = db.VT_agent_info.SingleOrDefault(i => i.agent_id == id);
            if (agent_info == null)
            {
                return NotFound();
            }

            return Ok(agent_info);
        }

        // PUT: api/agent_info/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putagent_info(int id, agent_info agent_info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != agent_info.agent_id)
            {
                return BadRequest();
            }

            db.Entry(agent_info).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!agent_infoExists(id))
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

        // POST: api/agent_info
        [ResponseType(typeof(agent_info))]
        public IHttpActionResult Postagent_info(agent_info agent_info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.agent_info.Add(agent_info);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = agent_info.agent_id }, agent_info);
        }

        // DELETE: api/agent_info/5
        [ResponseType(typeof(agent_info))]
        public IHttpActionResult Deleteagent_info(int id)
        {
            agent_info agent_info = db.agent_info.Find(id);
            if (agent_info == null)
            {
                return NotFound();
            }

            db.agent_info.Remove(agent_info);
            db.SaveChanges();

            return Ok(agent_info);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool agent_infoExists(int id)
        {
            return db.agent_info.Count(e => e.agent_id == id) > 0;
        }
    }
}