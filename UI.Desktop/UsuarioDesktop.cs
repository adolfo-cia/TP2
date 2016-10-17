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
using System.Text.RegularExpressions;

namespace UI.Desktop
{
    public partial class UsuarioDesktop : ApplicationForm
    {
        private List<Plan> _planes;    
        public UsuarioDesktop()
        {
            InitializeComponent();

            CargarTiposPersonas();
            //CargarPlanes();
        }
        private void CargarTiposPersonas()
        {
            cbTipo.DataSource = Enum.GetValues(typeof(Persona.TipoPersona));
        }
        private void CargarPlanes()
        {
            _planes = new PlanLogic().GetAll();
            cbPlan.DataSource = _planes;
            cbPlan.DisplayMember = "Descripcion";
            cbPlan.ValueMember = "ID";
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

            if (Modo == ModoForm.Baja)
            {
                UsuarioActual.ID = int.Parse(txtID.Text);
                UsuarioActual.State = BusinessEntity.States.Modified;
            }
        }

        public override void GuardarCambios()
        {
            this.MapearADatos();
            UsuarioLogic usr = new UsuarioLogic();
            usr.Save(UsuarioActual);
        }



        public override bool Validar()
        {

            string mensaje = "";
            bool bandera = true;

            if (this.txtID.Text == null)
            {
                bandera = false;
                mensaje += "No ingresó ningun ID\n";
            }

            if (this.txtNombre.Text == null)
            {
                bandera = false;
                mensaje += "No ingresó ningun Nombre\n";
            }

            if (this.txtApellido.Text == null)
            {
                bandera = false;
                mensaje += "No ingresó ningun Apellido\n";
            }

            if (this.txtEmail.Text == null)
            {
                bandera = false;
                mensaje += "No ingresó ningun Email\n";
            }

            if (this.txtUsuario.Text == null)
            {
                bandera = false;
                mensaje += "No ingresó ningun Usuario\n";
            }


            // las contraseñas se validan solo si estamos en modo alta ya que estas
            // no van a ser modificadas en una modificación o baja 

            if (Modo == ModoForm.Alta)
            {
                if (txtClave.Text != txtConfirmarClave.Text)
                {
                    mensaje += "Las claves no coinciden\n";
                    bandera = false;
                }
                if (txtClave.TextLength < 8)
                {
                    mensaje += "La clave no puede tener menos de 8 caracteres\n";
                    bandera = false;
                }

                if (this.txtClave.Text == null)
                {
                    bandera = false;
                    mensaje += "No ingresó ninguna Clave\n";
                }

                if (this.txtConfirmarClave.Text == null)
                {
                    bandera = false;
                    mensaje += "No ingresó la confirmación de la clave\n";
                }
            }


            if (!Regex.IsMatch(this.txtEmail.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
            {
                mensaje += "Debe ingresar un email valido\n";
                bandera = false;
            }


            if (bandera)
                return bandera;
            else
            {
                return bandera;
                Notificar(mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    this.Close();
                }
            }
            else if (Modo == ModoForm.Baja)
            {
                GuardarCambios();
                this.Close();
            }
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
 }

