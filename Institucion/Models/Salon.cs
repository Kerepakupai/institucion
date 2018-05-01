using System;
namespace Institucion.Models
{
    public class Salon : IEnteInstituto
    {
		public int Id { get; set; }
		
		public string Nombre { get; set; }

        public string CodigoInterno { get; set; }

        public string ConstruirLlaveSecreta(string nombreEnte)
        {
            return "SAL ON";
        }
    }
}
