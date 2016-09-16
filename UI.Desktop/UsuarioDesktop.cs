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
using Business.Entities;

namespace UI.Desktop
{
    public partial class UsuarioDesktop : ApplicationForm
    {
        public UsuarioDesktop()
        {
            InitializeComponent();
        }

        private void lblNombre_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblHabilitado_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void UsuarioDesktop_Load(object sender, EventArgs e)
        {

        }


        public Usuario UsuarioActual { get; set; }

        public UsuarioDesktop(ModoForm modo) : this()
        {
            //falta setear a modo form en el modo enviadp
            modo = ModoForm.Alta;
        }

        public UsuarioDesktop(int ID, ModoForm modo) : this()
        {
            UsuarioLogic Usuario = new UsuarioLogic();
            UsuarioActual = Usuario.GetOne(ID);
            this.MapearDeDatos();
            //14 Falta setear el texto del botón Aceptar en
            // función del Modo del formulario 

           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
