//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tutorias_API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Usuario
    {
        public Usuario()
        {
            this.ProfesorAsignaturas = new HashSet<ProfesorAsignatura>();
            this.Tutorias = new HashSet<Tutoria>();
            this.Tutorias1 = new HashSet<Tutoria>();
            this.UsuarioRoles = new HashSet<UsuarioRole>();
        }
    
        public int Usuario_Matricula { get; set; }
        public string Usuario_Nombre { get; set; }
        public string Usuario_Apellido1 { get; set; }
        public string Usuario_Correo { get; set; }
        public string Usuario_Contrasena { get; set; }
        public Nullable<int> Usuario_Telefono { get; set; }
        public string Usuario_Descripcion { get; set; }
        public Nullable<bool> Usuario_Estado { get; set; }
    
        public virtual ICollection<ProfesorAsignatura> ProfesorAsignaturas { get; set; }
        public virtual ICollection<Tutoria> Tutorias { get; set; }
        public virtual ICollection<Tutoria> Tutorias1 { get; set; }
        public virtual ICollection<UsuarioRole> UsuarioRoles { get; set; }
    }
}