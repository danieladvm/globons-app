using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Newtonsoft.Json;

namespace globons_app
{
    public partial class Personas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            http://1000hz.github.io/bootstrap-validator/
            http://jqueryui.com/datepicker/
            https://getbootstrap.com/docs/3.3/javascript/#modals
            */
        }

        [WebMethod(EnableSession = true)]
        public static string GetPersonas()
        {
            Repositories.PersonaRepository personaRepository = new Repositories.PersonaRepository();
            string tabla = "<table id='tabla-listado-personas' class='table table-striped'>";

            tabla += "<tr>";
            tabla += "<td><strong>Nombre</strong></td>";
            tabla += "<td><strong>Apellido</strong></td>";
            tabla += "<td><strong>Nro documento</strong></td>";
            tabla += "<td><strong>Fecha nacimiento</strong></td>";
            tabla += "<td></td>";
            tabla += "</tr>";

            foreach (Persona persona in personaRepository.GetPersonas())
            {
                tabla += "<tr>";
                tabla += "<td>" + persona.nombre + "</td>";
                tabla += "<td>" + persona.apellido + "</td>";
                tabla += "<td>" + persona.numeroDocumento + "</td>";
                tabla += "<td>" + persona.fechaNacimiento + "</td>";
                tabla += "<td>";
                tabla += "<button class='editar-persona btn btn-default' name='action' data-id='" + persona.idPersona + "'>";
                tabla += "<span class='glyphicon glyphicon-pencil'></span>";
                tabla += "</button>";
                tabla += "<button class='borrar-persona btn btn-default' name='action' data-id='" + persona.idPersona + "'>";
                tabla += "<span class='glyphicon glyphicon-remove'></span>";
                tabla += "</button>";
                tabla += "</td>";
                tabla += "</tr>";
            }

            tabla += "</table>";

            return tabla;
        }

        [WebMethod(EnableSession = true)]
        public static string GetPersona(int idPersona)
        {
            Repositories.PersonaRepository personaRepository = new Repositories.PersonaRepository();
            Modelo.PersonaModel persona = personaRepository.GetPersona(idPersona);
            return JsonConvert.SerializeObject(persona);
        }

        [WebMethod(EnableSession = true)]
        public static string SetPersona(Modelo.PersonaModel persona)
        {
            Repositories.PersonaRepository personaRepository = new Repositories.PersonaRepository();
            string mensaje = string.Empty;
            personaRepository.SetPersona(persona, ref mensaje);
            return mensaje;
        }

        [WebMethod(EnableSession = true)]
        public static string DeletePersona(int idPersona)
        {
            Repositories.PersonaRepository personaRepository = new Repositories.PersonaRepository();
            string mensaje = string.Empty;
            personaRepository.DeletePersona(idPersona, ref mensaje);
            return mensaje;
        }
    }
}
