using System;
using System.Data.Entity;
using Institucion.Models;

namespace Institucion.DataAccess
{
    public class InstitucionDB: DbContext
    {
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }

    }
}
