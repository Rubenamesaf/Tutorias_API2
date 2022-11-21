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
    public class AsignaturasController : ApiController
    {
        private TutoriasDBEntities db = new TutoriasDBEntities();

        // GET: api/Asignaturas
        public IQueryable<Asignatura> GetAsignaturas()
        {
            return db.Asignaturas;
        }

        // GET: api/Asignaturas/5
        [ResponseType(typeof(Asignatura))]
        public IHttpActionResult GetAsignatura(int id)
        {
            Asignatura asignatura = db.Asignaturas.Find(id);
            if (asignatura == null)
            {
                return NotFound();
            }

            return Ok(asignatura);
        }

        // PUT: api/Asignaturas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAsignatura(int id, Asignatura asignatura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != asignatura.Asignatura_ID)
            {
                return BadRequest();
            }

            db.Entry(asignatura).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsignaturaExists(id))
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

        // POST: api/Asignaturas
        [ResponseType(typeof(Asignatura))]
        public IHttpActionResult PostAsignatura(Asignatura asignatura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Asignaturas.Add(asignatura);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AsignaturaExists(asignatura.Asignatura_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = asignatura.Asignatura_ID }, asignatura);
        }

        // DELETE: api/Asignaturas/5
        [ResponseType(typeof(Asignatura))]
        public IHttpActionResult DeleteAsignatura(int id)
        {
            Asignatura asignatura = db.Asignaturas.Find(id);
            if (asignatura == null)
            {
                return NotFound();
            }

            db.Asignaturas.Remove(asignatura);
            db.SaveChanges();

            return Ok(asignatura);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AsignaturaExists(int id)
        {
            return db.Asignaturas.Count(e => e.Asignatura_ID == id) > 0;
        }
    }
}