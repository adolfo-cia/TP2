using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Data.Database;

namespace Business.Logic
{
    public class ComisionLogic : BusinessLogic
    {
        private ComisionAdapter ComisionData { get; set; }
        public ComisionLogic()
        {
            ComisionData = new ComisionAdapter();
        }





        public List<Comision> GetAll()
        {
            try
            {
                return ComisionData.GetAll();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }

        public List<ComisionComplete> GetAllComplete()
        {
            try
            {
                return ComisionData.GetAllComplete();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public Comision GetOne(int id) 
        {
            try
            {
                return ComisionData.GetOne(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public void Save(Comision com)
        {
            try
            {
                ComisionData.Save(com);
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
                ComisionData.Delete(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        



    }
}
