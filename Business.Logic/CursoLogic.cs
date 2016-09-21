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
        private CursoAdapter _CursoData;

        public CursoAdapter CursoData
        {
            get { return _CursoData; }
            set { _CursoData = value; }

        }

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
        public Curso GetOne(int mate, int comi)  //al tener el using Business.Entites no hace falta anteponerlo, quedaria asi--> public Curso GetOne(int id){}
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
        public void Save(Curso curso)
        {
            CursoData.Save(curso);
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
