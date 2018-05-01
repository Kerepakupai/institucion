using System;
using Institucion.Models;

namespace Institucion.Misc
{
    public class TransmisorDeDatos
    {
        //  Definicion de Delegado Firma de Metodo
        public delegate string Formatter(string input); 

        public void FormatearYEnviar(IEnteInstituto ente, Formatter localFormatter, string nombre)
        {
            var rawString = $"{ente.CodigoInterno}:{ente.ConstruirLlaveSecreta(nombre)}";

            rawString = localFormatter(rawString);

            Enviar(rawString);
        }

        private void Enviar(string rawString)
        {
            Console.WriteLine("Transmision de Datos: " + rawString);

            if(InformacionEnviada != null)
            {
                InformacionEnviada(this, new EventArgs());
            }
        }

        public event EventHandler InformacionEnviada;

        /*
        public string MyFormatter(string input) 
        {
            
        }
        */
        public TransmisorDeDatos()
        {
        }
    }
}
