using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.Logic;

namespace UI.Desktop
{
    public partial class Especialidad : Form
    {
        public Especialidad()
        {
            InitializeComponent();
            this.dgvEspecialidad.AutoGenerateColumns = false;
        }

        private void Especialidad_Load(object sender, EventArgs e)
        {
            this.Listar();
        }

        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        public void Listar()

        {
            EspecialidadLogic el = new EspecialidadLogic();
            this.dgvEspecialidad.DataSource = el.GetAll();

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            this.Listar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvEspecialidad_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
