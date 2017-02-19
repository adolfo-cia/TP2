 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class Materia : BusinessEntity
    {
        private string _Descripcion;
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        private int _HSSemanales;
        public int HSSemanales
        {
            get { return _HSSemanales; }
            set { _HSSemanales = value; }
        }

        private int _HSTotales;
        public int HSSTotales
        {
            get { return _HSTotales; }
            set { _HSTotales = value; }
        }

        private int _IDPlan;
        public int IDPlan
        {
            get { return _IDPlan; }
            set { _IDPlan = value; }
        }


      
    }



    public class MateriaComplete : Materia
    {
        public string DPlan { get; set; }
        public int IDEspecialidad { get; set; }
        public string DEspecialidad { get; set; }

        public string DescripcionCompleta
        {
            get { return this.Descripcion + " - " + this.DPlan + " - " + this.DEspecialidad;  }
        }

    }


}
