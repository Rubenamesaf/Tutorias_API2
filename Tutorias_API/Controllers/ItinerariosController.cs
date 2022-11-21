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
using Tutorias_API.Models;

namespace Tutorias_API.Controllers
{
    public class ItinerariosController : ApiController
    {
        private TutoriasDBEntities1 db = new TutoriasDBEntities1();

        // GET: api/Itinerarios
        public IQueryable<Itinerario> GetItinerarios()
        {
            return db.Itinerario;
        }

        // GET: api/Itinerarios/5
        [ResponseType(typeof(Itinerario))]
        public IHttpActionResult GetItinerario(int id)
        {
            Itinerario itinerario = db.Itinerario.Find(id);
            if (itinerario == null)
            {
                return NotFound();
            }

            return Ok(itinerario);
        }

        // PUT: api/Itinerarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutItinerario(int id, Itinerario itinerario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != itinerario.Itinerario_ID)
            {
                return BadRequest();
            }

            db.Entry(itinerario).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItinerarioExists(id))
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

        // POST: api/Itinerarios
        [ResponseType(typeof(Itinerario))]
        public IHttpActionResult PostItinerario(Itinerario itinerario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Itinerario.Add(itinerario);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = itinerario.Itinerario_ID }, itinerario);
        }

        // DELETE: api/Itinerarios/5
        [ResponseType(typeof(Itinerario))]
        public IHttpActionResult DeleteItinerario(int id)
        {
            Itinerario itinerario = db.Itinerario.Find(id);
            if (itinerario == null)
            {
                return NotFound();
            }

            db.Itinerario.Remove(itinerario);
            db.SaveChanges();

            return Ok(itinerario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItinerarioExists(int id)
        {
            return db.Itinerario.Count(e => e.Itinerario_ID == id) > 0;
        }
    }
}