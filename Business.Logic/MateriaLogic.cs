using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Data.Database;

namespace Business.Logic
{
    public class MateriaLogic : BusinessLogic
    {
        private MateriaAdapter MateriaData { get; set; } 
        public MateriaLogic()
        {
            MateriaData = new MateriaAdapter();
        }





        public List<Materia> GetAll()
        {
            try
            {
                return MateriaData.GetAll();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }

        public List<MateriaComplete> GetAllComplete()
        {
            try
            {
                return MateriaData.GetAllComplete();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public Materia GetOne(int id) 
        {
            try
            {
                return MateriaData.GetOne(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public void Save(Materia Materia)
        {
            MateriaData.Save(Materia);
        }
        public void Delete(int id)
        {
            try
            {
                MateriaData.Delete(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        



    }
}
