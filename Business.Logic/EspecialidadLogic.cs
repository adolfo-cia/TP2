using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Data.Database;

namespace Business.Logic
{
    public class EspecialidadLogic : BusinessLogic
    {
        private EspecialidadAdapter _EspecialidadData;  
        public EspecialidadAdapter EspecialidadData
        {
            get{ return _EspecialidadData; }
            set { _EspecialidadData = value; }
        } 
        public EspecialidadLogic()
        {
            EspecialidadData = new EspecialidadAdapter();
        }





        public List<Especialidad> GetAll()
        {
            try
            {
                return EspecialidadData.GetAll();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }
        public Especialidad GetOne(int id) 
        {
            try
            {
                return EspecialidadData.GetOne(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public void Save(Especialidad especialidad)
        {
            try
            {
                EspecialidadData.Save(especialidad);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public void Delete(int id)
        {
            try
            {
                EspecialidadData.Delete(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        



    }
}
