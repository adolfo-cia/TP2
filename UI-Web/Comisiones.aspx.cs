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
    public partial class Comisiones : System.Web.UI.Page
    {
        #region variables y Getters/Setters
        private ComisionLogic _comisionManager;
        private CursoLogic _cursoManager;
        private EspecialidadLogic _especialidadManager;
        private PlanLogic _planManager;
        enum FormModes
        {
            Alta,
            Baja,
            Modificacion
        }

       
        public ComisionLogic ComisionManager
        {
            get
            {
                if (_comisionManager == null)
                    _comisionManager = new ComisionLogic();
                return _comisionManager;
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
        public EspecialidadLogic EspecialidadManager
        {
            get
            {
                if (_especialidadManager == null)
                    _especialidadManager = new EspecialidadLogic();
                return _especialidadManager;
            }
        }
        public PlanLogic PlanManager
        {
            get
            {
                if (_planManager == null)
                    _planManager = new PlanLogic();
                return _planManager;
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
        private Comision ComisionActual { get; set; }
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            try {
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

        private void CargarGrilla()
        {
            try
            {
                gridComisiones.DataSource = ComisionManager.GetAllComplete();
                gridComisiones.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            }
        }

        private int? SelectedID
        {
            get
            {
                if (ViewState["SelectedID"] != null)
                {
                    return (int)ViewState["SelectedID"];
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

        protected void gridComisiones_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedID = (int?)gridComisiones.SelectedValue;
            panelFormComisiones.Visible = false;
            formActionPanel.Visible = false;
            gridActionPanel.Visible = true;
        }

        private void CargarEspecialidades()
        {
            try
            {
                ddlEspecialidades.DataSource = EspecialidadManager.GetAll();
                ddlEspecialidades.DataValueField = "ID";
                ddlEspecialidades.DataTextField = "Descripcion";
                ddlEspecialidades.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            }

        }

        private void CargarPlanes()
        {
            try
            {
                ddlPlanes.DataSource = PlanManager.GetAll();
                ddlPlanes.DataValueField = "ID";
                ddlPlanes.DataTextField = "Descripcion";
                ddlPlanes.DataBind();

                ddlEspecialidades_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            }
        }
            
        private void CargarForm(int id)
        {
            CargarEspecialidades();
            CargarPlanes();

            if (FormMode == FormModes.Baja)
            {
                txtAnioEsp.Enabled = false;
                txtDescCom.Enabled = false;
                ddlEspecialidades.Enabled = false;
                ddlPlanes.Enabled = false;
            }
            else
            {
                txtAnioEsp.Enabled = true;
                txtDescCom.Enabled = true;
                ddlEspecialidades.Enabled = true;
                ddlPlanes.Enabled = true;

                txtAnioEsp.Text = "";
                txtDescCom.Text = "";
            }
            

            if (FormMode != FormModes.Alta)
            {
                try
                {
                    ComisionActual = ComisionManager.GetOne(id);

                    txtAnioEsp.Text = ComisionActual.AnioEspecialidad.ToString();
                    txtDescCom.Text = ComisionActual.Descripcion;

                    Plan p = PlanManager.GetOne(ComisionActual.IDPlan);
                    ddlEspecialidades.SelectedValue = EspecialidadManager.GetOne(p.IDEspecialidad).ID.ToString();

                    ddlEspecialidades_SelectedIndexChanged(null, null);
                    ddlPlanes.SelectedValue = ComisionActual.IDPlan.ToString();
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
                }


            }
        }

        protected void lnkNuevo_Click(object sender, EventArgs e)
        {
            FormMode = FormModes.Alta;
            panelFormComisiones.Visible = true;
            formActionPanel.Visible = true;
            gridActionPanel.Visible = false;

            CargarForm(SelectedID.Value);
        }

        protected void lnkEditar_Click(object sender, EventArgs e)
        {
            if(IsEntitySelected)
            {
                try
                {
                    // Verificamos que la comision seleccionada no este referenciada por ningun curso
                    // si lo esta, no permitimos la edicion de la materia
                    List<Curso> cursos = CursoManager.GetAll().Where(curso => curso.IDComision == SelectedID).ToList();

                    if (cursos.Count == 0)
                    {
                        FormMode = FormModes.Modificacion;
                        panelFormComisiones.Visible = true;
                        formActionPanel.Visible = true;
                        gridActionPanel.Visible = false;

                        CargarForm(SelectedID.Value);
                    }
                    else
                    {
                        lnkCancelar_Click(null, null);
                        Response.Write("No se puede editar la comision seleccionada porque la comision esta referenciada por un curso");
                    }
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
                }

            }
        }

        protected void lnkBorrar_Click(object sender, EventArgs e)
        {
            if (IsEntitySelected)
            {
                try
                {
                    // Verificamos que la comision seleccionada no este referenciada por ningun curso
                    // si lo esta, no permitimos la eliminacion de la materia
                    List<Curso> cursos = CursoManager.GetAll().Where(curso => curso.IDComision == SelectedID).ToList();

                    if (cursos.Count == 0)
                    {
                        FormMode = FormModes.Baja;
                        panelFormComisiones.Visible = true;
                        formActionPanel.Visible = true;
                        gridActionPanel.Visible = false;

                        CargarForm(SelectedID.Value);
                    }
                    else
                    {
                        lnkCancelar_Click(null, null);
                        Response.Write("No se puede eliminar la comision seleccionada porque la comision esta referenciada por un curso");
                    }
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
                }

            }
        }

        private void CargarComision()
        {
            ComisionActual = new Comision();

            if (FormMode == FormModes.Alta)
            {
                ComisionActual.State = BusinessEntity.States.New;
            }
            if (FormMode == FormModes.Baja || FormMode == FormModes.Modificacion)
            {
                ComisionActual.ID = SelectedID.Value;
                ComisionActual.State = BusinessEntity.States.Modified;

                if (FormMode == FormModes.Baja)
                {
                    ComisionActual.State = BusinessEntity.States.Deleted;
                }
                else
                {
                    ComisionActual.State = BusinessEntity.States.Modified;
                }
            }
            

            ComisionActual.AnioEspecialidad = Convert.ToInt32(txtAnioEsp.Text);
            ComisionActual.Descripcion = txtDescCom.Text;
            ComisionActual.IDPlan= Convert.ToInt32(ddlPlanes.SelectedValue);
        }

        private void SaveComision(Comision com)
        {
            ComisionManager.Save(com);
        }

        protected void lnkAceptar_Click(object sender, EventArgs e)
        {
            panelFormComisiones.Visible = false;
            formActionPanel.Visible = false;
            gridActionPanel.Visible = true;

            CargarComision();
            SaveComision(ComisionActual);
            CargarGrilla();

            gridComisiones.SelectedIndex = -1;
            gridComisiones_SelectedIndexChanged(null, null);
        }

        protected void lnkCancelar_Click(object sender, EventArgs e)
        {
            panelFormComisiones.Visible = false;
            formActionPanel.Visible = false;
            gridActionPanel.Visible = true;

            gridComisiones.SelectedIndex = -1;
            gridComisiones_SelectedIndexChanged(null, null);
        }

        protected void ddlEspecialidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlPlanes.DataSource = PlanManager.GetAll().Where(plan => plan.IDEspecialidad == int.Parse(ddlEspecialidades.SelectedValue));
                ddlPlanes.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            }
        }
    }
}