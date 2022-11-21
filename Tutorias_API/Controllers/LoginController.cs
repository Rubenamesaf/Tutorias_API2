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
using System.Threading;
using WebApiSegura.Controllers;

namespace Tutorias_API.Controllers
{
    /// <summary>
    /// login controller class for authenticate users
    /// </summary>
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        private TutoriasDBEntities1 db = new TutoriasDBEntities1();

        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }

        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }

        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(Usuarios login)
        {
            if (login == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var Contra = (from x in db.Usuarios
                          where x.Usuario_Matricula == login.Usuario_Matricula
                          select x.Usuario_Contraseña ).FirstOrDefault();

            //TODO: Validate credentials Correctly, this code is only for demo !!
            bool isCredentialValid = (login.Usuario_Contraseña == Contra);
            if (isCredentialValid)
            {
                var token = TokenGenerator.GenerateTokenJwt(login.Usuario_Matricula);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }
    }

}