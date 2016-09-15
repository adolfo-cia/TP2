using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Data.Database;

namespace Business.Logic
{
    public class ModuloLogic : BusinessLogic
    {
        private ModuloAdapter _ModuloData;  
        public ModuloAdapter ModuloData
        {
            get{ return _ModuloData; }
            set { _ModuloData = value; }
        } 
        public ModuloLogic()
        {
            ModuloData = new ModuloAdapter();
        }





        public List<Modulo> GetAll()
        {
            try
            {
                return ModuloData.GetAll();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }
        public Modulo GetOne(int id) 
        {
            try
            {
                return ModuloData.GetOne(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public void Save(Modulo modulo)
        {
            ModuloData.Save(modulo);
        }
        public void Delete(int id)
        {
            try
            {
                ModuloData.Delete(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        



    }
}
