using System;
namespace Institucion.Models
{
    public class EmptyClass
    {
        public string DummyId
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }
    }
}
