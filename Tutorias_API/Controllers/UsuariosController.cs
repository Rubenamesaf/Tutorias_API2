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
    [Authorize]
    public class UsuariosController : ApiController
    {
        private TutoriasDBEntities1 db = new TutoriasDBEntities1();

        // GET: api/Usuarios
        public IQueryable<Usuarios> GetUsuarios()
        {
            return db.Usuarios;
        }

        // GET: api/Usuarios/5
        [ResponseType(typeof(Usuarios))]
        public IHttpActionResult GetUsuario(string id)
        {
            Usuarios usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }
            var usuarios = (from p in db.Usuarios
                            where p.Usuario_Matricula == id
                            select new
                            {
                                Matricula = p.Usuario_Matricula,
                                Nombre = p.Usuario_Nombre,
                                Apellido = p.Usuario_Apellido1,
                                Rol = ((from x in db.UsuarioRoles
                                        where x.Usuario_ID == id
                                        select new { x.Roles.Rol_Nombre }).FirstOrDefault()).Rol_Nombre,
                                Telefono = p.Usuario_Telefono,
                                Correo = p.Usuario_Correo,
                                Estado = p.Usuario_Estado,
                                Tutorias = (from x in db.Itinerario
                                            where x.Tutorias.Profesor_ID == id
                                            select new { x.Tutorias.Asignaturas.Asignatura_Nombre })



                            }).FirstOrDefault();

            return Ok(usuario);
        }

        // PUT: api/Usuarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsuario(string id, Usuarios usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id.ToString() != usuario.Usuario_Matricula)
            {
                return BadRequest();
            }

            db.Entry(usuario).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        private bool UsuarioExiste(string id)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Usuarios/5
        [ResponseType(typeof(Usuarios))]
        public IHttpActionResult DeleteUsuario(int id)
        {
            Usuarios usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            db.Usuarios.Remove(usuario);
            db.SaveChanges();

            return Ok(usuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsuarioExists(string id)
        {
            return db.Usuarios.Count(e => e.Usuario_Matricula == id) > 0;
        }
    }
}