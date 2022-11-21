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
    public class TutoriasController : ApiController
    {
        private TutoriasDBEntities db = new TutoriasDBEntities();

        // GET: api/Tutorias
        public IQueryable<Tutoria> GetTutorias()
        {
            return db.Tutorias;
        }

        // GET: api/Tutorias/5
        [ResponseType(typeof(Tutoria))]
        public IHttpActionResult GetTutoria(int id)
        {
            Tutoria tutoria = db.Tutorias.Find(id);
            if (tutoria == null)
            {
                return NotFound();
            }

            return Ok(tutoria);
        }

        // PUT: api/Tutorias/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTutoria(int id, Tutoria tutoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tutoria.Tutoria_ID)
            {
                return BadRequest();
            }

            db.Entry(tutoria).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TutoriaExists(id))
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

        // POST: api/Tutorias
        [ResponseType(typeof(Tutoria))]
        public IHttpActionResult PostTutoria(Tutoria tutoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tutorias.Add(tutoria);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tutoria.Tutoria_ID }, tutoria);
        }

        // DELETE: api/Tutorias/5
        [ResponseType(typeof(Tutoria))]
        public IHttpActionResult DeleteTutoria(int id)
        {
            Tutoria tutoria = db.Tutorias.Find(id);
            if (tutoria == null)
            {
                return NotFound();
            }

            db.Tutorias.Remove(tutoria);
            db.SaveChanges();

            return Ok(tutoria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TutoriaExists(int id)
        {
            return db.Tutorias.Count(e => e.Tutoria_ID == id) > 0;
        }
    }
}