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
    
    public class RegistrosController : ApiController
    {
        private TutoriasDBEntities1 db = new TutoriasDBEntities1();

        // POST: api/Usuarios
        [ResponseType(typeof(Usuarios))]
        public IHttpActionResult PostUsuario(Usuarios usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Usuarios.Add(usuario);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UsuarioExists(usuario.Usuario_Matricula))
                {
                    return Conflict();
                }
                if (UsuarioExists(usuario.Usuario_Correo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = usuario.Usuario_Matricula }, usuario);
        }

        private bool UsuarioExists(string id)
        {
            return db.Usuarios.Count(e => e.Usuario_Matricula == id) > 0;
        }
    }
}