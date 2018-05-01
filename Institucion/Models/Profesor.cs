using System;
namespace Institucion.Models
{
    public class Profesor : Persona
    {
        public string Materia { get; set; }

		public override string Resumen()
		{
            return $"{NombreCompleto} {Materia}";
		}

	}
}
