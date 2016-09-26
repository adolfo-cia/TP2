using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Data.Database;

namespace Business.Logic
{
    public class PersonaLogic : BusinessLogic
    {
        public PersonaAdapter PersonaData { get; set; }
        public PersonaLogic()
        {
            PersonaData = new PersonaAdapter();
        }





        public List<Persona> GetAll(Persona.TipoPersona tipo)
        {
            try
            {
                return PersonaData.GetAll(tipo);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }
        public Persona GetOneByID(int ID)  
        {
            try
            {
                return PersonaData.GetOne(ID, 0, "");
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public Persona GetOneByLeg(int legajo)
        {
            try
            {
                return PersonaData.GetOne(0, legajo, "");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public Persona GetOneByNick(string nick)
        {
            try
            {
                return PersonaData.GetOne(0,0,nick);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

      

        public void Save(Persona persona)
        {
            bool validated = true;
            string error = "";
            try
            {
                if (persona.State == BusinessEntity.States.New || persona.State == BusinessEntity.States.Modified)
                {
                    if (_validateNick(persona) == false)
                    {
                        validated = false;
                        error = "Nick/NombreUsuario ya ha sido usado\n";
                    }
                    if (_validateLegajo(persona) == false)
                    {
                        validated = false;
                        error += "Legajo ya ha sido usado\n";
                    }
                    if (validated == true)
                    {
                        PersonaData.Save(persona);
                    }
                    else
                    {
                        throw new Exception(error);
                    }
                }
            }
            catch (Exception e)
            {

                throw;
            }
            
        }
        public void Delete(int id)
        {
            try
            {
                PersonaData.Delete(id);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        private bool _validateNick(Persona p)
        {
            bool validated = true;
            //valido Nick
            Persona existePer = new PersonaLogic().GetOneByNick(p.NombreUsuario);
            if ( existePer != null && p.NombreUsuario == existePer.NombreUsuario && p.ID != existePer.ID)
            {
                validated = false;
            }
            return validated;
        }
        private bool _validateLegajo(Persona p)
        {
            bool validated = true;
            //valido legajo
            Persona existePer = new PersonaLogic().GetOneByLeg(p.Legajo);
            if (existePer != null && p.Legajo == existePer.Legajo && p.ID != existePer.ID)
            {
                validated = false;
            }
            return validated;
        }




    }
}
