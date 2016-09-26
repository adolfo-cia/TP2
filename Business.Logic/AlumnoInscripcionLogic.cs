using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Data.Database;

namespace Business.Logic
{
    public class AlumnoInscripcionLogic : BusinessLogic
    {
        private AlumnoInscripcionAdapter AlumnoInscripcionData { get; set; }
        public AlumnoInscripcionLogic()
        {
            AlumnoInscripcionData = new AlumnoInscripcionAdapter();
        }





        public List<AlumnoInscripcion> GetAll(int ID) //se manda el id dl alumno para recuperar las inscripciones del mismo
        {
            try
            {
                return AlumnoInscripcionData.GetAll(ID);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }
        public List<AlumnoInscripcion> GetAll()
        {
            try
            {
                return AlumnoInscripcionData.GetAll();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public AlumnoInscripcion GetOne(int id) 
        {
            try
            {
                return AlumnoInscripcionData.GetOne(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public void Save(AlumnoInscripcion plan)
        {
            AlumnoInscripcionData.Save(plan);
        }
        public void Delete(int id)
        {
            try
            {
                AlumnoInscripcionData.Delete(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        



    }
}
