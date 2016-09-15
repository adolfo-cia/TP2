using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Data.Database;

namespace Business.Logic
{
    //HOLA asdasd
    public class UsuarioLogic : BusinessLogic
    {
        private UsuarioAdapter _UsuarioData;  //private Data.Database.UsuarioAdapter _UsuarioData;
        public UsuarioAdapter UsuarioData
        {
            get{ return _UsuarioData; }
            set { _UsuarioData = value; }
        } //public Data.Database.UsuarioAdapter UsuarioData
        public UsuarioLogic()
        {
            UsuarioData = new UsuarioAdapter();
        }





        public List<Usuario> GetAll()
        {
            try
            {
                return UsuarioData.GetAll();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }
        public Usuario GetOne(int id)  //al tener el using Business.Entites no hace falta anteponerlo, quedaria asi--> public Usuario GetOne(int id){}
        {
            try
            {
                return UsuarioData.GetOne(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public void Save(Usuario usuario)
        {
            UsuarioData.Save(usuario);
        }
        public void Delete(int id)
        {
            try
            {
                UsuarioData.Delete(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        



    }
}
