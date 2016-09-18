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
            Modo = modo;
        }

        public UsuarioDesktop(int ID, ModoForm modo) : this()
        {
            Modo = modo;
            UsuarioLogic Usuario = new UsuarioLogic();
            UsuarioActual = Usuario.GetOne(ID);
            this.MapearDeDatos();           
        }

        public override void MapearDeDatos()
        {

            this.txtID.Text = this.UsuarioActual.ID.ToString();
            this.chkHabilitado.Checked = this.UsuarioActual.Habilitado;
            this.txtNombre.Text = this.UsuarioActual.Nombre;
            this.txtApellido.Text = this.UsuarioActual.Apellido;
            this.txtEmail.Text = this.UsuarioActual.Email;
            this.txtUsuario.Text = this.UsuarioActual.NombreUsuario;
            this.txtClave.Text = this.UsuarioActual.Clave;

            //cambiamos el texto del boton aceptar según corresponda
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                btnAceptar.Text = "Guardar";

            }
            else if (Modo == ModoForm.Baja)
            {
                            
                btnAceptar.Text = "Eliminar";
            }
            else if (Modo == ModoForm.Consulta)
            {
                btnAceptar.Text = "Aceptar";
            }

        }

        public override void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                Usuario us = new Usuario();
                UsuarioActual = us;
                UsuarioActual.State = BusinessEntity.States.New;
               
            }
            if (Modo == ModoForm.Modificacion)
            {
                UsuarioActual.ID = int.Parse(txtID.Text);
                UsuarioActual.State = BusinessEntity.States.Modified;
            }

            if (txtID.TextLength > 0)
                UsuarioActual.ID = int.Parse(txtID.Text);

            this.chkHabilitado.Checked = this.UsuarioActual.Habilitado;
            this.txtNombre.Text = this.UsuarioActual.Nombre;
            this.txtApellido.Text = this.UsuarioActual.Apellido;
            this.txtEmail.Text = this.UsuarioActual.Email;
            this.txtUsuario.Text = this.UsuarioActual.NombreUsuario;
            this.txtClave.Text = this.UsuarioActual.Clave;

        }

        public override void GuardarCambios()
        {
            this.MapearADatos();
            UsuarioLogic usr = new UsuarioLogic();
            usr.Save(UsuarioActual);
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                if (Validar() == true)
                {
                    GuardarCambios();
                }
            }
            else if (Modo == ModoForm.Baja)
            {
                GuardarCambios();
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
 }

