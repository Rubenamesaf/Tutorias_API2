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
    public class ProfesorAsignaturasController : ApiController
    {
        private TutoriasDBEntities db = new TutoriasDBEntities();

        // GET: api/ProfesorAsignaturas
        public IQueryable<ProfesorAsignatura> GetProfesorAsignaturas()
        {

            return db.ProfesorAsignaturas;
        }

        // GET: api/ProfesorAsignaturas/5
        [ResponseType(typeof(ProfesorAsignatura))]
        public IHttpActionResult GetProfesorAsignatura(int id)
        {
            ProfesorAsignatura profesorAsignatura = db.ProfesorAsignaturas.Find(id);
            if (profesorAsignatura == null)
            {
                return NotFound();
            }

            return Ok(profesorAsignatura);
        }

        // PUT: api/ProfesorAsignaturas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProfesorAsignatura(int id, ProfesorAsignatura profesorAsignatura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != profesorAsignatura.Pro_asig_ID)
            {
                return BadRequest();
            }

            db.Entry(profesorAsignatura).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesorAsignaturaExists(id))
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

        // POST: api/ProfesorAsignaturas
        [ResponseType(typeof(ProfesorAsignatura))]
        public IHttpActionResult PostProfesorAsignatura(ProfesorAsignatura profesorAsignatura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProfesorAsignaturas.Add(profesorAsignatura);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = profesorAsignatura.Pro_asig_ID }, profesorAsignatura);
        }

        // DELETE: api/ProfesorAsignaturas/5
        [ResponseType(typeof(ProfesorAsignatura))]
        public IHttpActionResult DeleteProfesorAsignatura(int id)
        {
            ProfesorAsignatura profesorAsignatura = db.ProfesorAsignaturas.Find(id);
            if (profesorAsignatura == null)
            {
                return NotFound();
            }

            db.ProfesorAsignaturas.Remove(profesorAsignatura);
            db.SaveChanges();

            return Ok(profesorAsignatura);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProfesorAsignaturaExists(int id)
        {
            return db.ProfesorAsignaturas.Count(e => e.Pro_asig_ID == id) > 0;
        }
    }
}