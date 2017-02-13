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
    public partial class AlumnoInscripciones : System.Web.UI.Page
    {
        private AlumnoInscripcionLogic _inscripcionLogic;
        private CursoLogic _cursoLogic;
        private MateriaLogic _materiaLogic;
        private ComisionLogic _comisionLogic;

        private AlumnoInscripcion inscripcionActual { get; set; }
        
        private AlumnoInscripcionLogic InscripcionLogic
        {
            get
            {
                if (_inscripcionLogic == null)
                {
                    _inscripcionLogic = new AlumnoInscripcionLogic();
                }

                return _inscripcionLogic;
            }
        }

        private MateriaLogic LogicMateria
        {
            get
            {
                if (_materiaLogic == null)
                {
                    _materiaLogic = new MateriaLogic();
                }

                return _materiaLogic;
            }
        }

        private ComisionLogic LogicComision
        {
            get
            {
                if (_comisionLogic == null)
                {
                    _comisionLogic = new ComisionLogic();
                }

                return _comisionLogic;
            }
        }
        private CursoLogic LogicCurso
        {
            get
            {
                if (_cursoLogic == null)
                {
                    _cursoLogic = new CursoLogic();
                }

                return _cursoLogic;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try {
                if ((Persona.TipoPersonas)Session["RolSesion"] == Persona.TipoPersonas.Alumno)
                {
                    CargarGridInscripciones();
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
      
        private void CargarGridInscripciones()
        {
            try
            {
                var inscripciones = InscripcionLogic.GetAll(Convert.ToInt32(Session["IdAlumno"]));
            
                gdvAlumno_Incripcion.DataSource = inscripciones.Select(ins => new
                {
                    ID = ins.ID,
                    MAteria = (LogicMateria.GetOne((LogicCurso.GetOne(ins.IdCurso).IdMateria))).Descripcion,
                    COmision = (LogicComision.GetOne((LogicCurso.GetOne(ins.IdCurso).IdComision))).Descripcion,
                    Condicion = ins.Condicion,
                });

            gdvAlumno_Incripcion.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            }
        }

        private int? SelectedIDInscripcion
        {
            get
            {
                if (ViewState["SelectedIDInscripcion"] == null)
                {
                    return -1;
                }
                else
                {
                    return (int)ViewState["SelectedIDInscripcion"];
                }
            }
            set
            {
                ViewState["SelectedIDInscripcion"] = value;
            }
        }

        private bool HaySeleccion()
        {
            return(SelectedIDInscripcion != -1);
        }
         protected void gdvIncripcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIDInscripcion = (int?)gdvAlumno_Incripcion.SelectedValue;

            formPanelInscripcion.Visible = false;
            formActionsPanel.Visible = false;
            gridActionsPanel.Visible = true;
        }
        enum FormModes
        {
            Alta,
            Baja,
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

        private void CargarGridCursos()
        {
            try
            {
                // Usamos solo los cursos del plan en el que esta inscripto el alumno

                var inscripciones = InscripcionLogic.GetAll(Convert.ToInt32(Session["IdAlumno"]));
                List<int> materiasInscriptas;
                materiasInscriptas = inscripciones.Select(ins => LogicMateria.GetOne((LogicCurso.GetOne(ins.IdCurso).IdMateria)).ID).ToList();

                var idPlan = Session["IdPlan"];

                var cursos = LogicCurso.GetAll().Where(curso => LogicMateria.GetOne(curso.IdMateria).IdPlan == (int)idPlan);

                cursos = cursos.Where(c => !(materiasInscriptas.Contains(c.IdMateria)));
            
            

                gdvInscripcionesCurso.DataSource = cursos.Select(cur => new
                {   
                    ID = cur.ID,
                    IdCurso = cur.ID,
                    MAte = (LogicMateria.GetOne((cur.IdMateria))).Descripcion,
                    COmi = (LogicComision.GetOne((cur.IdComision))).Descripcion,
                });

                if (cursos.Count() == 0)
                {
                    lblNoMateria.Visible = true;
                    lnkAceptar.Visible = false;
                }
                else
                    lnkAceptar.Visible = true;

                gdvInscripcionesCurso.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            }
        }

        private void CargarInscripcion()
        {
            inscripcionActual = new AlumnoInscripcion();
            
            if (FormMode == FormModes.Alta)
            {
                inscripcionActual.Baja = false;
                inscripcionActual.State = BusinessEntity.States.New;
                inscripcionActual.IdCurso = (int)gdvInscripcionesCurso.SelectedValue;
            }
            if (FormMode == FormModes.Baja)
            {
                inscripcionActual.State = BusinessEntity.States.Modified;
                inscripcionActual.ID = SelectedIDInscripcion.Value;
                inscripcionActual.IdCurso = InscripcionLogic.GetOne(SelectedIDInscripcion.Value).IdCurso;
                inscripcionActual.Baja = true;             
            }

            inscripcionActual.IdAlumno = Convert.ToInt32(Session["IdAlumno"]);
            inscripcionActual.Condicion = "Inscripto";
        }

        private void GuardarInscripcion(AlumnoInscripcion alumIns)
        {
            try
            {
                InscripcionLogic.Save(alumIns);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            }
        }

        protected void lnkCancelar_Click(object sender, EventArgs e)
        {
            formPanelInscripcion.Visible = false;
            gridActionsPanel.Visible = true;
            formActionsPanel.Visible = false;
            lblNoMateria.Visible = false;
            gdvAlumno_Incripcion.SelectedIndex = -1;
            gdvIncripcion_SelectedIndexChanged(null, null);

        }

        protected void lnkAceptar_Click(object sender, EventArgs e)
        {
            formPanelInscripcion.Visible = false;
            formActionsPanel.Visible = false;
            gridActionsPanel.Visible = true;

            CargarInscripcion();
            GuardarInscripcion(inscripcionActual);
            CargarGridInscripciones();

            gdvAlumno_Incripcion.SelectedIndex = -1;
            gdvIncripcion_SelectedIndexChanged(null, null);

        }

        protected void lnkNuevo_Click(object sender, EventArgs e)
        {
            FormMode = FormModes.Alta;
            formPanelInscripcion.Visible = true;
            formActionsPanel.Visible = true;
            gridActionsPanel.Visible = false;

            CargarGridCursos();
        }

        protected void lnkEliminar_Click(object sender, EventArgs e)
        {
            if (HaySeleccion())
            {
                FormMode = FormModes.Baja;
                formPanelInscripcion.Visible = false;
                formActionsPanel.Visible = false;
                gridActionsPanel.Visible = true;

                CargarInscripcion();
                GuardarInscripcion(inscripcionActual);
                CargarGridInscripciones();

                gdvAlumno_Incripcion.SelectedIndex = -1;
                gdvIncripcion_SelectedIndexChanged(null, null);
             }
        }
    }
}