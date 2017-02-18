using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Data.Database;

namespace Business.Logic
{
    public class CursoLogic : BusinessLogic
    {
        private CursoAdapter CursoData { get; set; }
        public CursoLogic()
        {
            CursoData = new CursoAdapter();
        }





        public List<Curso> GetAll()
        {
            try
            {
                return CursoData.GetAll();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }

        public List<CursoComplete> GetAllComplete()
        {
            try
            {
                return CursoData.GetAllComplete();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public Curso GetOne(int id) 
        {
            try
            {
                return CursoData.GetOne(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Curso GetOne(int mate, int comi)
        {
            try
            {
                return CursoData.GetOne(mate, comi);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Save(Curso c)
        {
            try
            {
                CursoData.Save(c);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public void Delete(int id)
        {
            try
            {
                CursoData.Delete(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        



    }
}
