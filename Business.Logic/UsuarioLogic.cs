using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Data.Database;

namespace Business.Logic
{
    public class UsuarioLogic : BusinessLogic
    {
        private Data.Database.UsuarioAdapter _UsuarioData;
        
        public UsuarioLogic()
        {
            UsuarioData = new UsuarioAdapter();    
        }
        public Data.Database.UsuarioAdapter UsuarioData
        {
            get{ return _UsuarioData; }
            set { _UsuarioData = value; }
        }

        public List<Usuario> GetAll()
        {
            return UsuarioData.GetAll();
        }

        public Business.Entities.Usuario GetOne(int id)
        {
            return UsuarioData.GetOne(id);
        }

        public void Delete(int id)
        {
            UsuarioData.Delete(id);
        }

        public void Save(Business.Entities.Usuario usuario)
        {
            UsuarioData.Save(usuario);
        }




}
}
