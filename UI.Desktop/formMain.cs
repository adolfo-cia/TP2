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


namespace UI.Desktop
{
    public partial class formMain : ApplicationForm
    {
        public Persona.TipoPersona TIPO { get; set; }
        public formMain()
        {
            InitializeComponent();
        }

       

        private void formMain_Shown(object sender, EventArgs e)
        {
            formLogin appLogin = new formLogin();
            if (appLogin.ShowDialog() != DialogResult.OK)
            {
                this.Dispose();
            }
            else
            {
                TIPO = appLogin.TIPO;
            }
            
        }

        private void mnuUsuarios_Click(object sender, EventArgs e)
        {
            if (TIPO != Persona.TipoPersona.Administrativo)
            {
                Notificar("Menu Usuarios","No tiene permiso para acceder", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                Usuarios appUsuarios = new Usuarios();
                appUsuarios.ShowDialog();
            }
        }

        private void mnuMaterias_Click(object sender, EventArgs e)
        {

        }

        private void mnuComisiones_Click(object sender, EventArgs e)
        {

        }

        private void mnuPlanes_Click(object sender, EventArgs e)
        {

        }

        private void mnuEspecialidades_Click(object sender, EventArgs e)
        {
            if (TIPO != Persona.TipoPersona.Administrativo)
            {
                Notificar("Menu Especialidades", "No tiene permiso para acceder", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Especialidades appEspecialidades = new Especialidades();
                appEspecialidades.ShowDialog();
            }
        }









        private void mnuSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
