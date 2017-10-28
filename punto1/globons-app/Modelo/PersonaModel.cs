using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace globons_app.Modelo
{
    public class PersonaModel
    {
        public int idPersona { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public long numeroDocumento { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public DireccionModel Direccion { get; set; }
    }
}