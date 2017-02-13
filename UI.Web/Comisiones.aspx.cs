using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Entities;
using Business.Logic;

namespace UI.Web
{
    public partial class Comisiones : System.Web.UI.Page
    {
        private ComisionLogic _logicComision;
        private EspecialidadLogic _logicEspecialidades;
        private PlanLogic _logicPlanes;
        private CursoLogic _cursologic;  

        private Comision ComisionActual { get; set; }

        private CursoLogic LogicCurso
        {
            get
            {
                if (_cursologic == null)
                {
                    _cursologic = new CursoLogic();
                }

                return _cursologic;
            }
        }
        public EspecialidadLogic LogicEspecialidades
        {
            get
            {
                if (_logicEspecialidades == null)
                    _logicEspecialidades = new EspecialidadLogic();
                return _logicEspecialidades;
            }
        }
        public ComisionLogic LogicComision
        {
            get
            {
                if (_logicComision == null)
                    _logicComision = new ComisionLogic();
                return _logicComision;
            }
        }

        public PlanLogic LogicPlanes
        {
            get
            {
                if (_logicPlanes == null)
                    _logicPlanes = new PlanLogic();
                return _logicPlanes;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try {
                if ((Persona.TipoPersonas)Session["RolSesion"] == Persona.TipoPersonas.Administrativo)
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
                gridComisiones.DataSource = LogicComision.GetAll();
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


        enum FormModes
        {
            Alta,
            Baja,
            Modificacion
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

        private void CargarEspecialidades()
        {
            try
            {
                ddlEspecialidades.DataSource = LogicEspecialidades.GetAll();
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
                ddlPlanes.DataSource = LogicPlanes.GetAll();
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

        //private void CargarPlanes()
        //{
        //    ddlPlanes.DataSource = LogicPlanes.GetAll();
        //    ddlPlanes.DataValueField = "ID";
        //    ddlPlanes.DataTextField = "Descripcion";
        //    ddlPlanes.DataBind();
        //}

        //private void CargarEspecialidades()
        //{
        //    ddlEspecialidades.DataSource = LogicEspecialidades.GetAll();
        //    ddlEspecialidades.DataValueField = "ID";
        //    ddlEspecialidades.DataTextField = "Descripcion";
        //    ddlEspecialidades.DataBind();
        //}

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
                    ComisionActual = LogicComision.GetOne(id);

                    txtAnioEsp.Text = ComisionActual.AnioEspecialidad.ToString();
                    txtDescCom.Text = ComisionActual.Descripcion;

                    Plan p = LogicPlanes.GetOne(ComisionActual.IdPlan);
                    ddlEspecialidades.SelectedValue = LogicEspecialidades.GetOne(p.IdEspecialidad).ID.ToString();

                    ddlEspecialidades_SelectedIndexChanged(null, null);
                    ddlPlanes.SelectedValue = ComisionActual.IdPlan.ToString();
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
                    List<Curso> cursos = LogicCurso.GetAll().Where(curso => curso.IdComision == SelectedID).ToList();

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
                    List<Curso> cursos = LogicCurso.GetAll().Where(curso => curso.IdComision == SelectedID).ToList();

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
                ComisionActual.Baja = false;
                ComisionActual.State = BusinessEntity.States.New;
            }
            if (FormMode == FormModes.Baja || FormMode == FormModes.Modificacion)
            {
                ComisionActual.ID = SelectedID.Value;
                ComisionActual.State = BusinessEntity.States.Modified;

                if (FormMode == FormModes.Baja)
                {
                    ComisionActual.Baja = true;
                }
                else
                {
                    ComisionActual.Baja = false;
                }
            }
            

            ComisionActual.AnioEspecialidad = Convert.ToInt32(txtAnioEsp.Text);
            ComisionActual.Descripcion = txtDescCom.Text;
            ComisionActual.IdPlan= Convert.ToInt32(ddlPlanes.SelectedValue);
        }

        private void GuardarComision(Comision com)
        {
            LogicComision.Save(com);
        }

        protected void lnkAceptar_Click(object sender, EventArgs e)
        {
            panelFormComisiones.Visible = false;
            formActionPanel.Visible = false;
            gridActionPanel.Visible = true;

            CargarComision();
            GuardarComision(ComisionActual);
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
                ddlPlanes.DataSource = LogicPlanes.GetAll().Where(plan => plan.IdEspecialidad == int.Parse(ddlEspecialidades.SelectedValue));
                ddlPlanes.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            }
        }
    }
}