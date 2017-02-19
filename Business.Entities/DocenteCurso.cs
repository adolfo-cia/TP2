using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
            
                // VER _CARGO : TIPOCARGOS


namespace Business.Entities
{
    public class DocenteCurso : BusinessEntity
    {
        public enum TipoCargo { Titular, Auxiliar }

        public TipoCargo Cargo { get; set; }

        private int _IDCurso;
        public int IDCurso
        {
            get { return _IDCurso; }
            set { _IDCurso = value; }
        }

        private int _IDDocente;
        public int IDDocente
        {
            get { return _IDDocente; }
            set { _IDDocente = value; }
        }
    }

    public class DocenteCursoComplete : DocenteCurso
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreCompleto
        {
            get { return Apellido + " " + Nombre; }
            set { this.NombreCompleto = value; }
        }

        public string DComision { get; set; }
        public string DMateria { get; set; }
        public int AnioCalendario { get; set; }
        public int AnioEspecialidad { get; set; }



    }



}
