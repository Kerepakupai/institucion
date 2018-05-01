using System;
namespace Institucion.Models
{
    public class Alumno : Persona
    {
        public string Correo { get; set; }

        public Alumno(string nombre, string apellido)
        {
            Nombre = nombre;
            Apellido = apellido;
        }

		public override string Resumen()
		{
            return $"{NombreCompleto} {Telefono}";
		}

        public override string NombreCompleto
        {
            get
            {
                return base.NombreCompleto.ToUpper();
            }
        }


	}
}
