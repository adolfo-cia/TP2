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
    public partial class Especialidades : Form
    {
        public Especialidades()
        {
            InitializeComponent();
            this.dgvEspecialidades.AutoGenerateColumns = false;
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
            this.dgvEspecialidades.DataSource = el.GetAll();

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

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            EspecialidadDesktop espd = new EspecialidadDesktop(ApplicationForm.ModoForm.Alta);
            espd.ShowDialog();
            Listar();
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvEspecialidades.SelectedRows.Count == 1)
            {

                int ID = ((Business.Entities.Especialidad)this.dgvEspecialidades.SelectedRows[0].DataBoundItem).ID;
                EspecialidadDesktop espd = new EspecialidadDesktop(ID, ApplicationForm.ModoForm.Modificacion);
                espd.ShowDialog();
                this.Listar();
            }



        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (dgvEspecialidades.SelectedRows.Count == 1)
            {

                int ID = ((Business.Entities.Especialidad)this.dgvEspecialidades.SelectedRows[0].DataBoundItem).ID;
                EspecialidadDesktop espd = new EspecialidadDesktop(ID, ApplicationForm.ModoForm.Modificacion);
                espd.ShowDialog();
                this.Listar();
            }
        }
    }
}
