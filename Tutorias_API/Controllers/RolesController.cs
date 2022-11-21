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
    public class RolesController : ApiController
    {
        private TutoriasDBEntities1 db = new TutoriasDBEntities1();

        // GET: api/Roles
        public IQueryable<Roles> GetRoles()
        {
            return db.Roles;
        }

        // GET: api/Roles/5
        [ResponseType(typeof(Roles))]
        public IHttpActionResult GetRole(int id)
        {
            Roles role = db.Roles.Find(id);
            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        // PUT: api/Roles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRole(int id, Roles role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != role.Rol_ID)
            {
                return BadRequest();
            }

            db.Entry(role).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
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

        // POST: api/Roles
        [ResponseType(typeof(Roles))]
        public IHttpActionResult PostRole(Roles role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Roles.Add(role);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = role.Rol_ID }, role);
        }

        // DELETE: api/Roles/5
        [ResponseType(typeof(Roles))]
        public IHttpActionResult DeleteRole(int id)
        {
            Roles role = db.Roles.Find(id);
            if (role == null)
            {
                return NotFound();
            }

            db.Roles.Remove(role);
            db.SaveChanges();

            return Ok(role);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoleExists(int id)
        {
            return db.Roles.Count(e => e.Rol_ID == id) > 0;
        }
    }
}