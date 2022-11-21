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
    public class UsuarioRolesController : ApiController
    {
        private TutoriasDBEntities db = new TutoriasDBEntities();

        // GET: api/UsuarioRoles
        public IQueryable<UsuarioRole> GetUsuarioRoles()
        {
            return db.UsuarioRoles;
        }

        // GET: api/UsuarioRoles/5
        [ResponseType(typeof(UsuarioRole))]
        public IHttpActionResult GetUsuarioRole(int id)
        {
            UsuarioRole usuarioRole = db.UsuarioRoles.Find(id);
            if (usuarioRole == null)
            {
                return NotFound();
            }

            return Ok(usuarioRole);
        }

        // PUT: api/UsuarioRoles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsuarioRole(int id, UsuarioRole usuarioRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuarioRole.Us_Rol_ID)
            {
                return BadRequest();
            }

            db.Entry(usuarioRole).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioRoleExists(id))
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

        // POST: api/UsuarioRoles
        [ResponseType(typeof(UsuarioRole))]
        public IHttpActionResult PostUsuarioRole(UsuarioRole usuarioRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UsuarioRoles.Add(usuarioRole);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = usuarioRole.Us_Rol_ID }, usuarioRole);
        }

        // DELETE: api/UsuarioRoles/5
        [ResponseType(typeof(UsuarioRole))]
        public IHttpActionResult DeleteUsuarioRole(int id)
        {
            UsuarioRole usuarioRole = db.UsuarioRoles.Find(id);
            if (usuarioRole == null)
            {
                return NotFound();
            }

            db.UsuarioRoles.Remove(usuarioRole);
            db.SaveChanges();

            return Ok(usuarioRole);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsuarioRoleExists(int id)
        {
            return db.UsuarioRoles.Count(e => e.Us_Rol_ID == id) > 0;
        }
    }
}