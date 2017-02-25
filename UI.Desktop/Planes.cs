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
    public partial class Planes : Form
    {
        public Planes()
        {
            InitializeComponent();
            dgvPlanes.AutoGenerateColumns = false;
            dgvPlanes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            
        }

        public void Listar()
        {
            PlanLogic plLogic = new PlanLogic();
            this.dgvPlanes.DataSource = plLogic.GetAll();
        }

        private void Planes_Load(object sender, EventArgs e)
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

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            PlanDesktop formPlanDesktop = new PlanDesktop(ApplicationForm.ModoForm.Alta);
            formPlanDesktop.Text = "Agregar Plan";
            formPlanDesktop.ShowDialog();
            this.Listar();
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvPlanes.SelectedRows.Count == 1)
            {
                int id = ((Business.Entities.Plan)this.dgvPlanes.SelectedRows[0].DataBoundItem).ID;

                PlanDesktop formPlanDesktop = new PlanDesktop(id, ApplicationForm.ModoForm.Modificacion);
                formPlanDesktop.Text = "Editar Plan";
                formPlanDesktop.ShowDialog();
                Listar();
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPlanes.SelectedRows.Count == 1)
            {
                int id = ((Business.Entities.Plan)this.dgvPlanes.SelectedRows[0].DataBoundItem).ID;

                PlanDesktop formplanDesktop = new PlanDesktop(id, ApplicationForm.ModoForm.Baja);
                formplanDesktop.Text = "Eliminar Plan";
                formplanDesktop.ShowDialog();
                Listar();
            }
        }
    }
}
