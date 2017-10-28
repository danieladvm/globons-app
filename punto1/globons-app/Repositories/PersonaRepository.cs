using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace globons_app.Repositories
{
    public class PersonaRepository
    {
        private Entities Entities = new Entities();

        public List<Persona> GetPersonas()
        {
            return (from p in Entities.Persona select p).ToList();
        }

        public Modelo.PersonaModel GetPersona(int idPersona)
        {
            Persona persona = (from p in Entities.Persona where p.idPersona == idPersona select p).FirstOrDefault();
            Modelo.PersonaModel personaModel = new Modelo.PersonaModel();
            Modelo.DireccionModel direccionModel = new Modelo.DireccionModel();
            personaModel.idPersona = persona.idPersona;
            personaModel.nombre = persona.nombre;
            personaModel.apellido = persona.apellido;
            personaModel.numeroDocumento = persona.numeroDocumento;
            personaModel.fechaNacimiento = persona.fechaNacimiento;
            direccionModel.idDireccion = persona.Direccion1.idDireccion;
            direccionModel.calle = persona.Direccion1.calle;
            direccionModel.numero = persona.Direccion1.numero;
            personaModel.Direccion = direccionModel;
            return personaModel;
        }

        public bool SetPersona(Modelo.PersonaModel persona, ref string mensaje)
        {
            Persona per;

            try
            {
                if (persona.idPersona == 0)
                {
                    if ((from p in Entities.Persona where p.numeroDocumento == persona.numeroDocumento select p).FirstOrDefault() != null)
                    {
                        mensaje = "Ya existe el número de documento ingresado";
                        return false;
                    }
                    else
                    {
                        per = new Persona();
                        per.Direccion1 = new Direccion();
                        per.nombre = persona.nombre;
                        per.apellido = persona.apellido;
                        per.numeroDocumento = persona.numeroDocumento;
                        per.fechaNacimiento = persona.fechaNacimiento;
                        per.Direccion1.calle = persona.Direccion.calle;
                        per.Direccion1.numero = persona.Direccion.numero;

                        Entities.Persona.Add(per);
                        Entities.SaveChanges();

                        persona.idPersona = per.idPersona;
                    }
                }
                else
                {
                    per = (from p in Entities.Persona where p.idPersona == persona.idPersona select p).FirstOrDefault();
                    per.Direccion1 = new Direccion();
                    per.nombre = persona.nombre;
                    per.apellido = persona.apellido;
                    per.numeroDocumento = persona.numeroDocumento;
                    per.fechaNacimiento = persona.fechaNacimiento;
                    per.direccion = persona.Direccion.idDireccion;
                    per.Direccion1.idDireccion = persona.Direccion.idDireccion;
                    per.Direccion1.calle = persona.Direccion.calle;
                    per.Direccion1.numero = persona.Direccion.numero;

                    Entities.Entry(per);
                    Entities.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
        }

        public bool DeletePersona(int idPersona, ref string mensaje)
        {
            try
            {
                Persona persona = (from p in Entities.Persona where p.idPersona == idPersona select p).FirstOrDefault();
                Entities.Persona.Remove(persona);
                Entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
        }
    }
}