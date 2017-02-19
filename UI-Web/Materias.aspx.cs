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
    public partial class Materias : System.Web.UI.Page
    {
        #region variables y Getters/Setters
        private PlanLogic _planManager;
        private EspecialidadLogic _especialidadManager;
        private MateriaLogic _materiaManager;
        private CursoLogic _cursoManager;
        enum FormModes
        {
            Alta,
            Modificacion,
            Baja,
        }

        private PlanLogic PlanManager
        {
            get
            {
                if (_planManager == null)
                {
                    _planManager = new PlanLogic();
                }

                return _planManager;
            }
        }
        private EspecialidadLogic EspecialidadManager
        {
            get
            {
                if (_especialidadManager == null)
                {
                    _especialidadManager = new EspecialidadLogic();
                }

                return _especialidadManager;
            }
        }
        private MateriaLogic MateriaManager
        {
            get
            {
                if (_materiaManager == null)
                {
                    _materiaManager = new MateriaLogic(); 
                }

                return _materiaManager;
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
        private Materia MateriaActual { get; set; }
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
        private void CargarGrilla()
        {
            try
            {
                //var materias = MateriaManager.GetAll();
                var materias = MateriaManager.GetAllComplete();
                //Esto es una negrada, habria q pedir la entidad completa al Manager, y este al DAO.
                //TODO: hacer el el GetAllComplete()
                //gridMaterias.DataSource = materias.Select(mat => new
                //{
                //    ID = mat.ID,
                //    Descripcion = mat.Descripcion,
                //    HorasSemanales = mat.HSSemanales,
                //    HorasTotales = mat.HSSTotales,
                //    DPlan = PlanManager.GetOne(mat.IDPlan).Descripcion,
                //    DEspecialidad = EspecialidadManager.GetOne(PlanManager.GetOne(mat.IDPlan).IDEspecialidad).Descripcion
                //});
                gridMaterias.DataSource = materias;

                gridMaterias.DataBind();
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
                ddlPlan.DataSource = PlanManager.GetAll();
                ddlPlan.DataValueField = "ID";
                ddlPlan.DataTextField = "Descripcion";
                ddlPlan.DataBind();

                ddlEspecialidad_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            }
        }
        private void CargarEspecialidades()
        {
            try
            {
                ddlEspecialidad.DataSource = EspecialidadManager.GetAll();
                ddlEspecialidad.DataValueField = "ID";
                ddlEspecialidad.DataTextField = "Descripcion";
                ddlEspecialidad.DataBind();
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
                if (ViewState["SelectedID"] == null)
                {
                    return -1;
                }
                else
                {
                    return (int)ViewState["SelectedID"];
                }
            }
            set
            {
                ViewState["SelectedID"] = value;
            }
        }
        private bool HaySeleccion()
        {
            return (SelectedID != -1);
        }
        
       

        private void CargarMateria()
        {
          

            if (FormMode == FormModes.Baja)
            {
                txtDescripcion.Enabled = false;
                txtHorasSemanales.Enabled = false;
                txtHorasTotales.Enabled = false;
                ddlEspecialidad.Enabled = false;
                ddlPlan.Enabled = false;
                MateriaActual = (Materia)Session["MateriaActual"];
                MateriaActual.State = BusinessEntity.States.Deleted;
            }
            if (FormMode == FormModes.Modificacion)
            {
                txtDescripcion.Enabled = true;
                txtHorasSemanales.Enabled = true;
                txtHorasTotales.Enabled = true;
                ddlEspecialidad.Enabled = true;
                ddlPlan.Enabled = true;
                MateriaActual = (Materia)Session["MateriaActual"];
                MateriaActual.State = BusinessEntity.States.Modified;
            }
            if (FormMode == FormModes.Alta)
            {
                MateriaActual = new Materia();
                MateriaActual.State = BusinessEntity.States.New;
            }

            MateriaActual.Descripcion = txtDescripcion.Text;
            MateriaActual.HSSemanales = int.Parse(txtHorasSemanales.Text);
            MateriaActual.HSSTotales = int.Parse(txtHorasTotales.Text);
            MateriaActual.IDPlan = int.Parse(ddlPlan.SelectedValue);
        }

        private void SaveMateria(Materia mat)
        {
            try
            {
                MateriaManager.Save(mat);
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
                txtDescripcion.Enabled = false;
                txtHorasSemanales.Enabled = false;
                txtHorasTotales.Enabled = false;
                ddlEspecialidad.Enabled = false;
                ddlPlan.Enabled = false;
            }
            else
            {
                txtDescripcion.Enabled = true;
                txtHorasSemanales.Enabled = true;
                txtHorasTotales.Enabled = true;
                ddlEspecialidad.Enabled = true;
                ddlPlan.Enabled = true;

                txtDescripcion.Text = "";
                txtHorasTotales.Text = "";
                txtHorasSemanales.Text = "";
            }

            if (FormMode != FormModes.Alta)
            {
                try
                {
                    MateriaActual = MateriaManager.GetOne(id);
                    Session["MateriaActual"] = MateriaActual;
                    txtDescripcion.Text = MateriaActual.Descripcion;
                    txtHorasSemanales.Text = MateriaActual.HSSemanales.ToString();
                    txtHorasTotales.Text = MateriaActual.HSSTotales.ToString();

                    ddlEspecialidad.SelectedValue = EspecialidadManager.GetAll().Find(esp => esp.ID == PlanManager.GetOne(MateriaActual.IDPlan).IDEspecialidad).ID.ToString();
                    ddlEspecialidad_SelectedIndexChanged(null, null);

                    ddlPlan.SelectedValue = MateriaActual.IDPlan.ToString();
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
            materiasPanel.Visible = true;
            formActionPanel.Visible = true;
            gridMateriasActionPanel.Visible = false;

            CargarForm(SelectedID.Value);
        }

        protected void lnkEditar_Click(object sender, EventArgs e)
        {
            if (HaySeleccion())
            {
                // Verificamos que la materia seleccionada no este referenciada por ningun curso
                // si lo esta, no permitimos la edicion de la materia
                try
                {
                    List<Curso> cursos = CursoManager.GetAll().Where(curso => curso.IDMateria == SelectedID).ToList();

                    if (cursos.Count == 0)
                    {
                        FormMode = FormModes.Modificacion;
                        materiasPanel.Visible = true;
                        formActionPanel.Visible = true;
                        gridMateriasActionPanel.Visible = false;

                        CargarForm(SelectedID.Value);
                    }
                    else
                    {
                        lnkCancelar_Click(null, null);
                        Response.Write("No se puede modificar la materia seleccionada porque la materia esta referenciada por un curso");
                    }
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
                }
            }
        }

        protected void lnkEliminar_Click(object sender, EventArgs e)
        {
            if (HaySeleccion())
            {
                // Verificamos que la materia seleccionada no este referenciada por ningun curso
                // si lo esta, no permitimos la eliminacion de la materia
                try
                {
                    List<Curso> cursos = CursoManager.GetAll().Where(curso => curso.IDMateria == SelectedID).ToList();

                    if (cursos.Count == 0)
                    {
                        FormMode = FormModes.Baja;
                        materiasPanel.Visible = true;
                        formActionPanel.Visible = true;
                        gridMateriasActionPanel.Visible = false;

                        CargarForm(SelectedID.Value);
                    }
                    else
                    {
                        lnkCancelar_Click(null, null);
                        Response.Write("No se puede eliminar la materia seleccionada porque la materia esta referenciada por un curso");
                    }
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
                }

            }
        }

        protected void lnkAceptar_Click(object sender, EventArgs e)
        {
            materiasPanel.Visible = false;
            formActionPanel.Visible = false;
            gridMateriasActionPanel.Visible = true;

            CargarMateria();
            SaveMateria(MateriaActual);
            CargarGrilla();

            gridMaterias.SelectedIndex = -1;
            gridMaterias_SelectedIndexChanged(null, null);
        }

        protected void lnkCancelar_Click(object sender, EventArgs e)
        {
            materiasPanel.Visible = false;
            formActionPanel.Visible = false;
            gridMateriasActionPanel.Visible = true;

            gridMaterias.SelectedIndex = -1;
            gridMaterias_SelectedIndexChanged(null, null);
        }

        protected void gridMaterias_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedID = (int?)gridMaterias.SelectedValue;
            materiasPanel.Visible = false;
            formActionPanel.Visible = false;
            gridMateriasActionPanel.Visible = true;
        }

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlPlan.DataSource = PlanManager.GetAll().Where(plan => plan.IDEspecialidad == int.Parse(ddlEspecialidad.SelectedValue));
                ddlPlan.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            }
        }
    }
}