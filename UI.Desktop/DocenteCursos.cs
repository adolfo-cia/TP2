using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.Entities;
using Business.Logic;

namespace UI.Desktop
{
    public partial class DocenteCursos : Form
    {
        public DocenteCursos()
        {
            InitializeComponent();
        }

        private void tcDocenteCursos_ContentPanel_Load(object sender, EventArgs e)
        {

        }

        public void Listar()
        {
            DocenteCursoLogic docCur = new DocenteCursoLogic();
            //this.dgvDocenteCursos = docCur.GetAll(); 
        }

        private void DocenteCursos_Load(object sender, EventArgs e)
        {
            this.Listar();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            this.Listar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
