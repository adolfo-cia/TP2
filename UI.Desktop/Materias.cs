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
    public partial class Materias : Form
    {
        public Materias()
        {
            InitializeComponent();
            dgvMaterias.AutoGenerateColumns = false;
            dgvMaterias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public void Listar()

        {
            MateriaLogic matLogic = new MateriaLogic();
            this.dgvMaterias.DataSource = matLogic.GetAll();
        }

        private void Materias_Load(object sender, EventArgs e)
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
            MateriaDesktop formMateriaDesktop = new MateriaDesktop(ApplicationForm.ModoForm.Alta);
            formMateriaDesktop.Text = "Agregar Materia";
            formMateriaDesktop.ShowDialog();
            Listar();
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvMaterias.SelectedRows.Count == 1)
            {
                int id = ((Materia)(dgvMaterias.SelectedRows[0].DataBoundItem)).ID;
                MateriaDesktop formMateriaDesktop = new MateriaDesktop(id, ApplicationForm.ModoForm.Modificacion);
                formMateriaDesktop.Text = "Editar Materia";
                formMateriaDesktop.ShowDialog();
                Listar();
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (dgvMaterias.SelectedRows.Count == 1)
            {
                int id = ((Materia)(dgvMaterias.SelectedRows[0].DataBoundItem)).ID;
                MateriaDesktop formMateriaDesktop = new MateriaDesktop(id, ApplicationForm.ModoForm.Baja);
                formMateriaDesktop.Text = "Eliminar Materia";
                formMateriaDesktop.ShowDialog();
                Listar();
            }
        }
    }
}
