using System;
namespace Institucion.Models
{
    public interface IEnteInstituto
    {
        string CodigoInterno { get; set; }

        string ConstruirLlaveSecreta(string nombreEnte);


    }
}
