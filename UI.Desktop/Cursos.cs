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
    public partial class Cursos : Form
    {
        public Cursos()
        {
            dgvCursos.AutoGenerateColumns = false;
            dgvCursos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            InitializeComponent();
        }

        private void Cursos_Load(object sender, EventArgs e)
        {
            this.Listar();
        }

        public void Listar()
        {
            CursoLogic cl = new CursoLogic();
            this.dgvCursos.DataSource = cl.GetAll();
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
            CursoDesktop formCurso = new CursoDesktop(ApplicationForm.ModoForm.Alta);
            formCurso.ShowDialog();
            this.Listar();
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            int ID = ((Business.Entities.Curso)this.dgvCursos.SelectedRows[0].DataBoundItem).ID;
            CursoDesktop curd = new CursoDesktop(ID, ApplicationForm.ModoForm.Baja);
            curd.ShowDialog();
            this.Listar();
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvCursos.SelectedRows.Count == 1)
            {

                int ID = ((Business.Entities.Curso)this.dgvCursos.SelectedRows[0].DataBoundItem).ID;
                CursoDesktop curd = new CursoDesktop(ID, ApplicationForm.ModoForm.Modificacion);
                curd.ShowDialog();
                this.Listar();


            }
        }
    }
}
