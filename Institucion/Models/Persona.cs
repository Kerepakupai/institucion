using System;
namespace Institucion.Models
{
    public abstract class Persona: IEnteInstituto
    {
        public static int inasistencias = 0;

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string Telefono { get; set; }
        public int Inasistencias { get; set; }

        public virtual string NombreCompleto 
        {
            get 
            {
                return $"{Nombre} {Apellido}";
            }   
        }

        public string CodigoInterno 
        { 
            get; 
            set; 
        }


        public Persona()
        {
            inasistencias++;
        }

        public abstract string Resumen();

        public string ConstruirLlaveSecreta(string nombreEnte)
        {
            var rnd = new Random();

            return rnd.Next(1, 9999999).ToString();
        }
    }
}
