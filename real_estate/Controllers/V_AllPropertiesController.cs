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

    public class V_AllPropertiesController : ApiController
    {
        //sql
         private real_estateEntities1 db = new real_estateEntities1();

        //mysql
       //private RealEstateModel_mysql.RealEstateEntities_mysql db_ = new RealEstateModel_mysql.RealEstateEntities_mysql();

        // GET: api/V_AllProperties
        //sql
        public IQueryable<V_AllProperties> GetV_AllProperties()
        {
            return db.V_AllProperties;
        }

        //mysql
        //public List<RealEstateModel_mysql.VAllproperty> GetV_AllProperties()
        //{
        //    List<RealEstateModel_mysql.VAllproperty> result = db.VAllproperties.ToList();
        //    return result;
        //}

        // GET: api/V_AllProperties/5
        //sql
        [ResponseType(typeof(V_AllProperties))]
        public IHttpActionResult GetV_AllProperties(int id)
        {
            V_AllProperties v_AllProperties = db.V_AllProperties.SingleOrDefault(i => i.id == id);
            if (v_AllProperties == null)
            {
                return NotFound();
            }

            return Ok(v_AllProperties);
           
        }

        //mysql
        //[ResponseType(typeof(RealEstateModel_mysql.VAllproperty))]
        //public IHttpActionResult GetV_AllProperties(int id)
        //{
        //    RealEstateModel_mysql.VAllproperty v_AllProperties = db.VAllproperties.SingleOrDefault(i => i.Id == id);
        //    if (v_AllProperties == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(v_AllProperties);
        //}
    }
}