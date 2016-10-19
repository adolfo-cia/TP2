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
        private Persona PersonaActual { get; set; }
        private List<Plan> _planes;    

        public UsuarioDesktop()
        {
            InitializeComponent();

            CargarTiposPersonas();
            CargarPlanes();
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

        public UsuarioDesktop(ModoForm modo) : this()
        {
            Modo = modo;
        }

        public UsuarioDesktop(int ID, ModoForm modo) : this()
        {
            Modo = modo;
            try
            {
                PersonaActual = new PersonaLogic().GetOneByID(ID);
                this.MapearDeDatos();
            }
            catch (Exception ex)
            {

                Notificar(ex);
            }
                       
        }

        public override void MapearDeDatos()
        {

            this.txtID.Text = this.PersonaActual.ID.ToString();
            this.txtNombre.Text = this.PersonaActual.Nombre;
            this.txtApellido.Text = this.PersonaActual.Apellido;
            this.txtEmail.Text = this.PersonaActual.Email;
            this.txtLegajo.Text = this.PersonaActual.Legajo.ToString();
            this.dtpFeNac.Value = (DateTime)this.PersonaActual.FechaNacimiento;
            //this.dtpFeNac.Enabled = false;
            this.txtDire.Text = this.PersonaActual.Direccion;
            if (this.PersonaActual.IDPlan != null)
            {
                this.cbPlan.SelectedValue = this.PersonaActual.IDPlan;
            }
            else
            {
                this.cbPlan.Enabled = false;
            }
            
            this.cbTipo.SelectedIndex = (int)this.PersonaActual.Tipo;

            this.txtUsuario.Text = this.PersonaActual.NombreUsuario;
            this.txtClave.Text = this.PersonaActual.Clave;
            this.chkHabilitado.Checked = this.PersonaActual.Habilitado;

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
                Persona per = new Persona();
                PersonaActual = per;
                PersonaActual.State = BusinessEntity.States.New;
               
            }
            if (Modo == ModoForm.Modificacion)
            {
                PersonaActual.ID = int.Parse(txtID.Text);
                PersonaActual.State = BusinessEntity.States.Modified;
            }
            if (Modo == ModoForm.Baja)
            {
                PersonaActual.ID = int.Parse(txtID.Text);
                PersonaActual.State = BusinessEntity.States.Deleted;
            }

            
            this.PersonaActual.Nombre = this.txtNombre.Text;
            this.PersonaActual.Apellido = this.txtApellido.Text;
            this.PersonaActual.Email = this.txtEmail.Text;
            if (txtLegajo.Text == "")
            {
                this.PersonaActual.Legajo = null;
            }
            else
            {
                this.PersonaActual.Legajo = int.Parse(txtLegajo.Text);
            }
            
            this.PersonaActual.FechaNacimiento = dtpFeNac.Value;
            this.PersonaActual.Direccion = txtDire.Text;
            this.PersonaActual.Telefono = txtTel.Text;
            if (this.cbPlan.Enabled == true)
            {
                this.PersonaActual.IDPlan = (int)cbPlan.SelectedValue;
            }
            else
            {
                this.PersonaActual.IDPlan = null;
            }
            
            this.PersonaActual.Tipo = (Persona.TipoPersona)cbTipo.SelectedValue;
            
            this.PersonaActual.NombreUsuario = this.txtUsuario.Text;
            this.PersonaActual.Clave = this.txtClave.Text;
            this.PersonaActual.Habilitado = this.chkHabilitado.Checked;
        }

        public override void GuardarCambios()
        {
            this.MapearADatos();

            try
            {
                new PersonaLogic().Save(PersonaActual);
            }
            catch (Exception ex)
            {

                Notificar(ex);
            }
        }



        public override bool Validar()
        {

            string mensaje = "";
            bool bandera = true;
            int intLegajo;

            if ((Persona.TipoPersona)this.cbTipo.SelectedValue != Persona.TipoPersona.Administrativo)
            {
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
                if (txtLegajo.TextLength == 0)
                {
                    mensaje += "No ingresó ningun Legajo\n";
                    bandera = false;
                }
                if (txtLegajo.TextLength > 0 && int.TryParse(txtLegajo.Text, out intLegajo) == false)
                {
                    mensaje += "El Legajo debe ser un entero\n";
                    bandera = false;
                }
                if (!Regex.IsMatch(this.txtEmail.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                {
                    mensaje += "Debe ingresar un email valido\n";
                    bandera = false;
                }
                if (this.txtDire.Text == null)
                {
                    bandera = false;
                    mensaje += "No ingresó ninguna Direccion\n";
                }
                if (this.txtTel.Text == null)
                {
                    bandera = false;
                    mensaje += "No ingresó ninguna Télefono\n";
                }

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


           


            if (bandera == false)
            {
                Notificar(mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return bandera;
        }
        
            
        
     
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                if (Validar())
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

        private void cbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTipo.SelectedIndex == 2)
            {
                this.cbPlan.Enabled = false;
            }
            else
            {
                this.cbPlan.Enabled = true;
            }
        }
    }
 }

