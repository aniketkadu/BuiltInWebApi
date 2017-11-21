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
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TempsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Temps
        public IQueryable<Temp> GetTemps()
        {
            return db.Temps;
        }

        // GET: api/Temps/5
        [ResponseType(typeof(Temp))]
        public IHttpActionResult GetTemp(int id)
        {
            Temp temp = db.Temps.Find(id);
            if (temp == null)
            {
                return NotFound();
            }

            return Ok(temp);
        }

        // PUT: api/Temps/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTemp(int id, Temp temp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != temp.Id)
            {
                return BadRequest();
            }

            db.Entry(temp).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TempExists(id))
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

        // POST: api/Temps
        [ResponseType(typeof(Temp))]
        public IHttpActionResult PostTemp(Temp temp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Temps.Add(temp);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = temp.Id }, temp);
        }

        // DELETE: api/Temps/5
        [ResponseType(typeof(Temp))]
        public IHttpActionResult DeleteTemp(int id)
        {
            Temp temp = db.Temps.Find(id);
            if (temp == null)
            {
                return NotFound();
            }

            db.Temps.Remove(temp);
            db.SaveChanges();

            return Ok(temp);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TempExists(int id)
        {
            return db.Temps.Count(e => e.Id == id) > 0;
        }
    }
}