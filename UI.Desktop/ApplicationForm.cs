using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace UI.Desktop
{
    public partial class ApplicationForm : Form
    {
        public ApplicationForm()
        {
            InitializeComponent();
        }

        public enum ModoForm { Alta, Baja, Modificacion, Consulta }
        public ModoForm Modo { get; set; }

        public virtual void MapearDeDatos()
        {
            this.txtID.Text = this.UsuarioActual.ID.ToString();
            this.chkHabilitado.Checked = this.UsuarioActual.Habilitado;      
            this.txtNombre.Text = this.UsuarioActual.Nombre;
            this.txtApellido.Text = this.UsuarioActual.Apellido;
            this.txtEmail.Text = this.UsuarioActual.Email;
            this.txtUsuario.Text = this.UsuarioActual.Usuario;
            this.txtClave.Text = this.UsuarioActual.Clave;
            this.txtConfirmarClave.Text = this.UsuarioActual.COnfirmarClave;
            
            //14.Dentro del mismo método setearemos el texto del botón Aceptar
            // falta hacerlo

        }

        public virtual void MapearADatos()
        {

        }

        public virtual void GuardarCambios()
        {

        }

        public virtual bool Validar()
        {
            string mensaje = "";
            bool bandera = true;

            if (this.txtID.Text = null)
            {
                bandera = false;
                mensaje += "No ingresó ningun ID\n";
            }

            if (this.txtNombre.Text = null)
            {
                bandera = false;
                mensaje += "No ingresó ningun Nombre\n";
            }

            if (this.txtApellido.Text = null)
            {
                bandera = false;
                mensaje += "No ingresó ningun Apellido\n";
            }

            if (this.txtEmail.Text = null)
            {
                bandera = false;
                mensaje += "No ingresó ningun Email\n";
            }

            if (this.txtUsuario.Text = null)
            {
                bandera = false;
                mensaje += "No ingresó ningun Usuario\n";
            }

            if (this.txtClave.Text = null)
            {
                bandera = false;
                mensaje += "No ingresó ninguna Clave\n";
            }

            if (this.txtConfirmarClave.Text = null)
            {
                bandera = false;
                mensaje += "No ingresó la confirmación de la clave\n";
            }
            
            if (this.txtClave.Text!=this.txtConfirmarClave.Text)
            {
                bandera = false;
                mensaje += "La clave no se corresponde con su confirmación\n";
            }

            if (this.txtClave.Text.Length < 8)
            {
                bandera = false;
                mensaje += "La contraseña es menor a 8 caracteres";
            }
            //falta validar email


            if (bandera)
                return bandera;
            else
            {
                return bandera;
                this.Notificar("Error", mensaje, , );
            }
            

            
        }

        public void Notificar(string titulo, string mensaje, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            MessageBox.Show(mensaje, titulo, botones, icono);
        }

        public void Notificar(string mensaje, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            this.Notificar(this.Text, mensaje, botones, icono);
        }

        private void ApplicationForm_Load(object sender, EventArgs e)
        {

        }
    }
}
