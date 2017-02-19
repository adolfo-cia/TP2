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
    public partial class Docentes_cursos : System.Web.UI.Page
    {
        #region variables y Getters/Setters

        private DocenteCursoLogic _docentecursoManager;
        private CursoLogic _cursoManager;
        private PersonaLogic _personaManager;

        enum FormModes
        {
            Alta,
            Modificacion,
            Baja,
        }

        private DocenteCursoLogic DocenteCursoManager
        {
            get
            {
                if (_docentecursoManager == null)
                {
                    _docentecursoManager = new DocenteCursoLogic();
                }
                return _docentecursoManager;
            }
        }
        private CursoLogic CursoManager
        {
            get
            {
                if (_cursoManager == null)
                {
                    _cursoManager = new CursoLogic();
                }
                return _cursoManager;
            }
        }
        private PersonaLogic PersonaManager
        {
            get
            {
                if (_personaManager == null)
                {
                    _personaManager = new PersonaLogic();
                }
                return _personaManager;
            }
        }
        FormModes FormMode
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

        private DocenteCurso DocenteCursoActual { get; set; }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if ((Persona.TipoPersona)Session["RolSesion"] == Persona.TipoPersona.Administrativo)
                {
                    CargarGrilla();
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


        private int? SelectedIDCurso
        {
            get
            {
                if (ViewState["SelectedIDCurso"] == null)
                {
                    return -1;
                }
                else
                {
                    return (int)ViewState["SelectedIDCurso"];
                }
            }
            set
            {
                ViewState["SelectedIDCurso"] = value;
            }
        }
        private bool HaySeleccion()
        {
            return (SelectedIDCurso != -1);
        }

        private void CargarGrilla()
        {
            try
            {
               
                gridDocenteCurso.DataSource = DocenteCursoManager.GetAllComplete();
                gridDocenteCurso.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            }
        }

        private void CargarCargo()
        {
            Dictionary<int, string> cargo = new Dictionary<int, string>();
            cargo.Add(0, "Titular");
            cargo.Add(1, "Auxiliar");
            ddlCargo.DataSource = cargo;
            ddlCargo.DataTextField = "Value";
            ddlCargo.DataValueField = "Key";
            ddlCargo.DataBind();
        }

        private void CargarCursos()
        {
            try
            {
                ddlCurso.DataSource = CursoManager.GetAllComplete();
                ddlCurso.DataValueField = "ID";
                ddlCurso.DataTextField = "EnString";
                ddlCurso.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            }
        }

        private void CargarDocentes()
        {
            try
            {
                ddlDocente.DataSource = PersonaManager.GetAll(Persona.TipoPersona.Docente);
                ddlDocente.DataValueField = "ID";
                ddlDocente.DataTextField = "NombreCompleto";
                ddlDocente.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            }
        }

        private void CargarDocenteCurso()
        {
            DocenteCursoActual = new DocenteCurso();

            if (FormMode == FormModes.Alta)
            {
                DocenteCursoActual.State = BusinessEntity.States.New;
            }
            if (FormMode == FormModes.Baja || FormMode == FormModes.Modificacion)
            {
                DocenteCursoActual.State = BusinessEntity.States.Modified;
                DocenteCursoActual.ID = SelectedIDCurso.Value;
                if (FormMode == FormModes.Baja)
                {
                   
                    DocenteCursoActual.State = BusinessEntity.States.Deleted;
                }
                else
                {
                    
                    DocenteCursoActual.State = BusinessEntity.States.Modified;
                }
            }

            DocenteCursoActual.IDCurso = int.Parse(ddlCurso.SelectedValue);
            DocenteCursoActual.IDDocente = int.Parse(ddlDocente.SelectedValue);
            DocenteCursoActual.Cargo = (DocenteCurso.TipoCargo)(ddlCargo.SelectedIndex);
            
        }

        void CargarForm(int id)
        {
            // No hace falta cargar comisiones ya que estas se cargan en selectedindexchanged de materias
            CargarDocentes();
            CargarCursos();
            CargarCargo();

            if (FormMode == FormModes.Baja)
            {
                ddlDocente.Enabled = false;
                ddlCurso.Enabled = false;
                ddlCargo.Enabled = false;
            }
            else
            {
                ddlDocente.Enabled = true;
                ddlCurso.Enabled = true;
                ddlCargo.Enabled = true;
            }
            if (FormMode != FormModes.Alta)
            {
                DocenteCursoActual = DocenteCursoManager.GetOne(id);

                ddlDocente.SelectedValue = DocenteCursoActual.IDDocente.ToString();
                ddlCurso.SelectedValue = DocenteCursoActual.IDCurso.ToString(); 
                ddlCargo.SelectedIndex = (int)DocenteCursoActual.Cargo;
            }
        }

        private void SaveDocenteCurso(DocenteCurso doccur)
        {
            DocenteCursoManager.Save(doccur);
        }

        protected void lnkNuevo_Click(object sender, EventArgs e)
        {
            FormMode = FormModes.Alta;
            formPanelDocenteCurso.Visible = true;
            formActionsPanel.Visible = true;
            gridActionsPanel.Visible = false;

            CargarForm(SelectedIDCurso.Value);
        }

        protected void lnkEditar_Click(object sender, EventArgs e)
        {
            if (HaySeleccion())
            {

                FormMode = FormModes.Modificacion;
                formPanelDocenteCurso.Visible = true;
                formActionsPanel.Visible = true;
                gridActionsPanel.Visible = false;

                CargarForm(SelectedIDCurso.Value);
            }
        }

        protected void lnkEliminar_Click(object sender, EventArgs e)
        {
            if (HaySeleccion())
            {
                FormMode = FormModes.Baja;
                formPanelDocenteCurso.Visible = true;
                formActionsPanel.Visible = true;
                gridActionsPanel.Visible = false;

                CargarForm(SelectedIDCurso.Value);
            }
        }

        protected void lnkAceptar_Click(object sender, EventArgs e)
        {
            formPanelDocenteCurso.Visible = false;
            formActionsPanel.Visible = false;
            gridActionsPanel.Visible = true;

            CargarDocenteCurso();
            SaveDocenteCurso(DocenteCursoActual);
            CargarGrilla();

            gridDocenteCurso.SelectedIndex = -1;
            gridDocenteCurso_SelectedIndexChanged(null, null);
        }

        protected void lnkCancelar_Click(object sender, EventArgs e)
        {
            formPanelDocenteCurso.Visible = false;
            formActionsPanel.Visible = false;
            gridActionsPanel.Visible = true;

            gridDocenteCurso.SelectedIndex = -1;
            gridDocenteCurso_SelectedIndexChanged(null, null);
        }

        protected void gridDocenteCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIDCurso = (int?)gridDocenteCurso.SelectedValue;

            formPanelDocenteCurso.Visible = false;
            formActionsPanel.Visible = false;
            gridActionsPanel.Visible = true;
        }
    }
}