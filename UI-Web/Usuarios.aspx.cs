using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Entities;
using Business.Logic;
using System.Globalization;
//using System.Runtime.Serialization.Json;
using System.IO;

namespace UI_Web
{
    public partial class Usuarios : System.Web.UI.Page
    {
        #region Variables de los Managers
        private PersonaLogic _logicPersona;
        private PlanLogic _logicPlan;
        private EspecialidadLogic _logicEspecialidad;
        #endregion

        #region Getters de Managers
        private EspecialidadLogic LogicEspecialidad
        {
            get
            {
                if (_logicEspecialidad == null)
                {
                    _logicEspecialidad = new EspecialidadLogic();
                }

                return _logicEspecialidad;
            }
        }
        private PlanLogic LogicPlan
        {
            get
            {
                if (_logicPlan == null)
                {
                    _logicPlan = new PlanLogic();
                }

                return _logicPlan;
            }
        }
        private PersonaLogic LogicPersona
        {
            get
            {
                if (_logicPersona == null)
                {
                    _logicPersona = new PersonaLogic();
                }

                return _logicPersona;
            }
        }
        #endregion

        //Metodo que inicia la pagina
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if ((Persona.TipoPersona)Session["RolSesion"] == Persona.TipoPersona.Administrativo)
                {
                    LoadGrid();
                    //if (!IsPostBack)
                    //{
                    //    Util.Logger.LogHabilitado = false;

                    //}
                }
                else
                {
                    Response.Redirect("~/Default.aspx?mensaje=" + Server.UrlEncode("No tenes permisos para acceder a ese recurso"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect(@"~/Login.aspx");
                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            }

        }

        //Metodo que carga la grilla
        private void LoadGrid()
        {
            try
            {
                gridView.DataSource = LogicPersona.GetAll();
                gridView.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            }
        }

        #region variables de entidades
        private Persona PersonaActual { get; set; }
        private Persona PersonaLog { get; set; }
        private Plan PlanUsuario { get; set; }
        private Especialidad EspecialidadUsuario { get; set; }
        #endregion

        private int? SelectedID
        {
            get
            {
                if (ViewState["SelectedID"] != null)
                {
                    return (int?)ViewState["SelectedID"];
                }
                else
                {
                    return -1;
                }
            }
            set
            {
                ViewState["SelectedID"] = value;
            }
        }

        private bool IsEntitySelected
        {
            get
            {
                return (SelectedID.Value != -1);
            }
        }



        public enum FormModes
        {
            Alta,
            Baja,
            Modificacion
        }

        public FormModes FormMode
        {
            get
            {
                return (FormModes)this.ViewState["FormMode"];
            }
            set
            {
                this.ViewState["FormMode"] = value;
            }
        }

        protected void gridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedID = (int?)gridView.SelectedValue;
            
            formPanel.Visible = false;
            formActionsPanel.Visible = false;
            gridActionsPanel.Visible = true;
        }

        private void CargarTiposPersonas()
        {
            Dictionary<int, string> tipoPersona = new Dictionary<int, string>();
            tipoPersona.Add(0, "Alumno");
            tipoPersona.Add(1, "Docente");
            tipoPersona.Add(2, "Administrativo");
            ddlTipoPersona.DataSource = tipoPersona;
            ddlTipoPersona.DataTextField = "Value";
            ddlTipoPersona.DataValueField = "Key";
            ddlTipoPersona.DataBind();
        }

        private void LimpiarForm()
        {
            txtClave.Enabled = true;
            txtRepetirClave.Enabled = true;
            txtNombre.Enabled = true;
            txtApellido.Enabled = true;
            txtEmail.Enabled = true;
            chkHabilitado.Enabled = true;
            txtNombreUsuario.Enabled = true;
            txtDireccion.Enabled = true;
            txtLegajo.Enabled = true;
            txtTelefono.Enabled = true;
            
            FechaControl.Enabled = true;
            ddlEspecialidad.Enabled = true;
            ddlIdPlan.Enabled = true;
            ddlTipoPersona.Enabled = true;
        }

        private void LoadForm(int id)
        {
            CargarTiposPersonas();
            CargarEspecialidades();

            if (FormMode == FormModes.Baja)
            {
                txtClave.Enabled = false;
                txtRepetirClave.Enabled = false;
                txtNombre.Enabled = false;
                txtApellido.Enabled = false;
                txtEmail.Enabled = false;
                chkHabilitado.Enabled = false;
                txtNombreUsuario.Enabled = false;
                txtDireccion.Enabled = false;
                txtLegajo.Enabled = false;
                txtTelefono.Enabled = false;

                FechaControl.Enabled = false;
                ddlEspecialidad.Enabled = false;
                ddlIdPlan.Enabled = false;
                ddlTipoPersona.Enabled = false;
            }
            else
            {
                txtClave.Text = "";
                txtRepetirClave.Text = "";
                txtNombre.Text = "";
                txtApellido.Text = "";
                txtEmail.Text = "";
                chkHabilitado.Text = "";
                txtNombreUsuario.Text = "";
                txtDireccion.Text = "";
                txtLegajo.Text = "";
                txtTelefono.Text = "";

                txtClave.Enabled = false;
                txtRepetirClave.Enabled = false;
                txtNombre.Enabled = true;
                txtApellido.Enabled = true;
                txtEmail.Enabled = true;
                chkHabilitado.Enabled = true;
                txtNombreUsuario.Enabled = true;
                txtDireccion.Enabled = true;
                txtLegajo.Enabled = true;
                txtTelefono.Enabled = true;

                FechaControl.Enabled = true;
                ddlEspecialidad.Enabled = true;
                ddlIdPlan.Enabled = true;
                ddlTipoPersona.Enabled = true;

                cvCoinciden.Enabled = true;
                rfvClave.Enabled = true;
                rfvRepiteClave.Enabled = true;

                LimpiarForm();
            }
            if (FormMode != FormModes.Alta)
            {
                try
                {
                    txtClave.Enabled = false;
                    txtRepetirClave.Enabled = false;

                    cvCoinciden.Enabled = false;
                    rfvClave.Enabled = false;
                    rfvRepiteClave.Enabled = false;

                    PersonaActual = LogicPersona.GetOneByID(id);
                    Session["Persona"] = PersonaActual;
                    PlanUsuario = LogicPlan.GetOne((int)PersonaActual.IDPlan);
                    EspecialidadUsuario = LogicEspecialidad.GetOne(PlanUsuario.IDEspecialidad);

                    txtNombre.Text = PersonaActual.Nombre;
                    txtApellido.Text = PersonaActual.Apellido;
                    txtEmail.Text = PersonaActual.Email;
                    chkHabilitado.Checked = PersonaActual.Habilitado;
                    txtNombreUsuario.Text = PersonaActual.NombreUsuario;
                    txtDireccion.Text = PersonaActual.Direccion;
                    txtLegajo.Text = PersonaActual.Legajo.ToString();
                    txtTelefono.Text = PersonaActual.Telefono;


                    FechaControl.SeleccionarFecha(PersonaActual.FechaNacimiento.Day.ToString(),
                                                  PersonaActual.FechaNacimiento.Month.ToString(),
                                                  PersonaActual.FechaNacimiento.Year.ToString());

                    ddlEspecialidad.SelectedValue = PlanUsuario.IDEspecialidad.ToString();
                    ddlIdPlan.SelectedValue = PersonaActual.IDPlan.ToString();
                    ddlTipoPersona.SelectedIndex = (int)PersonaActual.Tipo;
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
                }
            }
        }

        private void CargarEspecialidades()
        {
            try
            {
                ddlEspecialidad.DataSource = LogicEspecialidad.GetAll();
                ddlEspecialidad.DataValueField = "ID";
                ddlEspecialidad.DataTextField = "Descripcion";
                ddlEspecialidad.DataBind();

                ddlEspecialidad_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            }
        }

        private void LoadEntity()
        {
            if (FormMode == FormModes.Alta)
            {
                PersonaActual = new Persona();
                PersonaActual.ID = SelectedID.Value;
                PersonaActual.State = BusinessEntity.States.New;
            }
            if (FormMode == FormModes.Modificacion || FormMode == FormModes.Baja)
            {
                PersonaActual = (Persona)Session["Persona"];
                //PersonaActual.State = BusinessEntity.States.Modified;
                if (FormMode == FormModes.Baja)
                {
                    //PersonaActual.Baja = true;
                    PersonaActual.State = BusinessEntity.States.Deleted;
                }
                else
                {
                    PersonaActual.State = BusinessEntity.States.Modified;
                    //PersonaActual.Baja = false;
                }
            }
            if (FormMode == FormModes.Alta)
            {
                PersonaActual.Clave = txtClave.Text; // Util.Hash.SHA256ConSal(txtClave.Text, null);
            }

            PersonaActual.Nombre = txtNombre.Text;
            PersonaActual.Apellido = txtApellido.Text;
            PersonaActual.Email = txtEmail.Text;
            PersonaActual.NombreUsuario = txtNombreUsuario.Text;
            PersonaActual.Habilitado = chkHabilitado.Checked;

            PersonaActual.Direccion = txtDireccion.Text;
            PersonaActual.FechaNacimiento = FechaControl.ObtenerFecha();
            PersonaActual.IDPlan = int.Parse(ddlIdPlan.SelectedValue);
            PersonaActual.Legajo = int.Parse(txtLegajo.Text);
            //PersonaActual.CambiaClave = PersonaActual.CambiaClave;
            PersonaActual.Telefono = txtTelefono.Text;
            PersonaActual.Tipo = (Persona.TipoPersona)ddlTipoPersona.SelectedIndex;
        }

        private void SaveEntity(Persona per)
        {
            try
            {
                //string validacion=_logicPersona.Save(per);
                _logicPersona.Save(per);
                //if (validacion.Length > 1)               
                //    Response.Write(validacion);
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            }
        }

        protected void lnkEditar_Click(object sender, EventArgs e)
        {
            if (IsEntitySelected)
            {
                formPanel.Visible = true;
                formActionsPanel.Visible = true;
                gridActionsPanel.Visible = false;
                FormMode = FormModes.Modificacion;
                LoadForm(SelectedID.Value);
            }
        }

        protected void lnkAceptar_Click(object sender, EventArgs e)
        {
            formPanel.Visible = false;
            formActionsPanel.Visible = false;
            gridActionsPanel.Visible = true;

            // Se cargan los datos del form en PersonaActual y luego se persisten en base de datos
            LoadEntity();
            SaveEntity(PersonaActual);
            LoadGrid();
            // LoadGrid no dispara SelectedIndexChanged de la gridview por lo que cambiamos el
            // selected ID manualmente
            gridView.SelectedIndex = -1;
            gridView_SelectedIndexChanged(null, null);
        }

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlIdPlan.DataValueField = "ID";
                ddlIdPlan.DataTextField = "Descripcion";
                ddlIdPlan.DataSource = LogicPlan.GetAll().Where(plan => plan.IDEspecialidad == int.Parse(ddlEspecialidad.SelectedValue));
                ddlIdPlan.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            }
        }
        
        protected void lnkEliminar_Click(object sender, EventArgs e)
        {
            if (IsEntitySelected)
            {
                formPanel.Visible = true;
                formActionsPanel.Visible = true;
                gridActionsPanel.Visible = false;

                FormMode = FormModes.Baja;
                LoadForm(SelectedID.Value);
            }
        }

        protected void lnkNuevo_Click(object sender, EventArgs e)
        {
            FormMode = FormModes.Alta;

            formPanel.Visible = true;
            formActionsPanel.Visible = true;
            gridActionsPanel.Visible = false;

            LoadForm(SelectedID.Value);
            
        }

        protected void lnkCancelar_Click(object sender, EventArgs e)
        {
            formPanel.Visible = false;
            formActionsPanel.Visible = false;
            gridActionsPanel.Visible = false;

            gridView.SelectedIndex = -1;
            gridView_SelectedIndexChanged(null, null);
        }

        protected void cvValidadorLongitudClave_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (args.Value.Length >= 8);
        }

        protected void lnkSerializar_Click(object sender, EventArgs e)
        {
            //if (IsEntitySelected)
            //{
            //    try
            //    {
            //        Persona p = LogicPersona.GetOne(SelectedID.Value);

            //        if (p.TipoPersona == Persona.TipoPersona.Alumno)
            //        {
            //            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Persona));
            //            using (FileStream archivo = new FileStream(@"C:\Users\ANDRES\Desktop\alumno.json", FileMode.Create))
            //            {
            //                ser.WriteObject(archivo, p);
            //            }
            //        }
            //        else
            //        {
            //            Response.Write("Solo se pueden serializar alumnos");
            //        }
            //    }
            //    catch (Exception ex)
            //    {        
            //        Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            //    }
                
            //}
            
        }

        protected void lnkDeserializar_Click(object sender, EventArgs e)
        {   
            //try
            //{
            //    using (FileStream archivo = new FileStream(@"C:\alumno.json", FileMode.Open))
            //    {
            //        DataContractJsonSerializer serializadorJSON = new DataContractJsonSerializer(typeof(Persona));
            //        Persona p = (Persona)serializadorJSON.ReadObject(archivo);
            //        p.State = BusinessEntity.States.New;
            //        FormMode = FormModes.Alta;
            //        SaveEntity(p);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            //}
        }
        
    }
}