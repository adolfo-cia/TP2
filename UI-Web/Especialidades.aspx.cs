using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Entities;
using Business.Logic;

namespace UI_Web
{
    public partial class Especialidades : System.Web.UI.Page
    {
        EspecialidadLogic _especialidadLogic;

        private EspecialidadLogic LogicEspecialidad
        {
            get
            {
                if (_especialidadLogic == null)
                    _especialidadLogic = new EspecialidadLogic();

                return _especialidadLogic;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if ((Persona.TipoPersona)Session["RolSesion"] == Persona.TipoPersona.Administrativo)
                {
                    LoadGrid();
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

        private void LoadGrid()
        {
            try
            {
                gridEspecialidades.DataSource = LogicEspecialidad.GetAll();
                gridEspecialidades.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
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
            get { return (FormModes)ViewState["FormMode"]; }
            set { ViewState["FormMode"] = value; }
        }

        private Especialidad EspActual { get; set; }

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
                return (SelectedID != -1);
            }
        }

        protected void gridEspecialidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedID = (int?)gridEspecialidades.SelectedValue;

            EspecialidadesPanel.Visible = false;
            formEspecialidadesActionPanel.Visible = false;
            gridEspecialidadesActionPanel.Visible = true;
        }

        private void LoadForm(int id)
        {
            if(FormMode == FormModes.Baja)
            {
                txtDescEsp.Enabled = false;
            }
            else
            {
                txtDescEsp.Enabled = true;
                txtDescEsp.Text = "";
            }


            if (FormMode != FormModes.Alta)
            {
                try
                {
                    EspActual = LogicEspecialidad.GetOne(id);
                    Session["Especialidad"] = EspActual;
                    txtDescEsp.Text = EspActual.Descripcion;
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
                } 
            }
        }

        protected void lnkEditar_Click(object sender, EventArgs e)
        {
            
            if (IsEntitySelected)
            {
                FormMode = FormModes.Modificacion;

                gridEspecialidadesActionPanel.Visible = false;
                formEspecialidadesActionPanel.Visible = true;
                EspecialidadesPanel.Visible = true;

                LoadForm(SelectedID.Value);
            }
        }

        protected void lnkNuevo_Click(object sender, EventArgs e)
        {
            FormMode = FormModes.Alta;
            gridEspecialidadesActionPanel.Visible = false;
            formEspecialidadesActionPanel.Visible = true;
            EspecialidadesPanel.Visible = true;

            LoadForm(SelectedID.Value);
            
        }

        protected void lnkEliminar_Click(object sender, EventArgs e)
        {
            if (IsEntitySelected)
            {
                FormMode = FormModes.Baja;

                EspecialidadesPanel.Visible = true;
                gridEspecialidadesActionPanel.Visible = false;
                formEspecialidadesActionPanel.Visible = true;

                LoadForm(SelectedID.Value);
            }
        }

        void GuardarEspecialidad(Especialidad esp)
        {
            try
            {
                LogicEspecialidad.Save(esp);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            }
        }

        private void CargarEspecialidad()
        {
            if (FormMode == FormModes.Alta)
            {
                EspActual = new Especialidad();
                EspActual.State = BusinessEntity.States.New;
            }
            if (FormMode == FormModes.Baja || FormMode == FormModes.Modificacion)
            {
                EspActual = (Especialidad)Session["Especialidad"];
                EspActual.ID = SelectedID.Value;
                if (FormMode == FormModes.Baja)
                {
                    EspActual.State = BusinessEntity.States.Deleted;
                }
                else
                {
                    EspActual.State = BusinessEntity.States.Modified;
                }

            }
          

            EspActual.Descripcion = txtDescEsp.Text;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            EspecialidadesPanel.Visible = false;
            formEspecialidadesActionPanel.Visible = false;
            gridEspecialidadesActionPanel.Visible = true;

            CargarEspecialidad();
            GuardarEspecialidad(EspActual);
            LoadGrid();

            gridEspecialidades.SelectedIndex = -1;
            gridEspecialidades_SelectedIndexChanged(null, null);            
        }

        protected void btCancelar_Click(object sender, EventArgs e)
        {
            EspecialidadesPanel.Visible = false;
            formEspecialidadesActionPanel.Visible = false;
            gridEspecialidadesActionPanel.Visible = true;

            gridEspecialidades.SelectedIndex = -1;
            gridEspecialidades_SelectedIndexChanged(null, null);
        }
    }
}