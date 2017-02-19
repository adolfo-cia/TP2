using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Data.Database;

namespace Business.Logic
{
    public class DocenteCursoLogic : BusinessLogic
    {
        private DocenteCursoAdapter DocenteCursoData { get; set; }
        public DocenteCursoLogic()
        {
            DocenteCursoData = new DocenteCursoAdapter();
        }





        public List<DocenteCurso> GetAll(int ID)
        {
            try
            {
                return DocenteCursoData.GetAll(ID);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }

        public List<DocenteCursoComplete> GetAllComplete()
        {
            try
            {
                return DocenteCursoData.GetAllComplete();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public DocenteCurso GetOne(int id) 
        {
            try
            {
                return DocenteCursoData.GetOne(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public void Save(DocenteCurso plan)
        {
            try
            {
                DocenteCursoData.Save(plan);
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
                DocenteCursoData.Delete(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        



    }
}
