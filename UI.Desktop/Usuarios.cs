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
    public partial class Usuarios : ApplicationForm
    {
        public Usuarios()
        {
            InitializeComponent();
            this.dgvUsuarios.AutoGenerateColumns = false;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public void Listar()
        {
            try
            {
                this.dgvUsuarios.DataSource = new PersonaLogic().GetAll();
            }
            catch (Exception ex)
            {

                Notificar(ex);
            }
           
        }

        private void Usuarios_Load(object sender, EventArgs e)
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
            UsuarioDesktop usrd = new UsuarioDesktop(ApplicationForm.ModoForm.Alta);
            usrd.Text = "Agregar Usuario";
            usrd.ShowDialog();
            this.Listar();
        }
        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count == 1)
            {
                int ID = ((Persona)this.dgvUsuarios.SelectedRows[0].DataBoundItem).ID;
                UsuarioDesktop usrd = new UsuarioDesktop(ID, ApplicationForm.ModoForm.Modificacion);
                usrd.Text = "Modificar Usuario";
                usrd.ShowDialog();
                
            }
            this.Listar();
        }
        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count == 1)
            {
                int ID = ((Persona)this.dgvUsuarios.SelectedRows[0].DataBoundItem).ID;
                UsuarioDesktop usrd = new UsuarioDesktop(ID, ApplicationForm.ModoForm.Baja);
                usrd.Text = "Eliminar Usuario";
                usrd.ShowDialog();
                
            }
            this.Listar();

        }

    }
}
