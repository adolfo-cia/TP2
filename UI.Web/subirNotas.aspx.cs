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
    public partial class subirNotas : System.Web.UI.Page
    {
        private DocenteCursoLogic _docenteCursoLogic;
        private CursoLogic _cursoLogic;
        private MateriaLogic _materiaLogic;
        private ComisionLogic _comisionLogic;
        private AlumnoInscripcionLogic _inscripcionLogic;
        private PersonaLogic _personaLogic;

        private PersonaLogic PersonaLogic
        {
            get
            {
                if (_personaLogic == null)
                {
                    _personaLogic = new PersonaLogic();
                }

                return _personaLogic;
            }
        }

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
        private DocenteCursoLogic DocenteCursoLogic
        {
            get
            {
                if (_docenteCursoLogic == null)
                {
                    _docenteCursoLogic = new DocenteCursoLogic();
                }

                return _docenteCursoLogic;
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
                if ((Persona.TipoPersonas)Session["RolSesion"] == Persona.TipoPersonas.Docente)
                {
                    CargarGridDocenteCurso();
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

        private void CargarGridDocenteCurso()
        {
            try
            {
                var docentecursos = DocenteCursoLogic.GetAll(Convert.ToInt32(Session["IdAlumno"]));

                GridViewDocenteCurso.DataSource = docentecursos.Select(docur => new
                {
                    ID = docur.ID,
                                                                                MAteria = (LogicMateria.GetOne((LogicCurso.GetOne(docur.id_curso).IdMateria))).Descripcion,
                                                                                COmision = (LogicComision.GetOne((LogicCurso.GetOne(docur.id_curso).IdComision))).Descripcion,
                                                                                
                                                                              });
                GridViewDocenteCurso.DataBind();
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

        private int? SelectedIDIncripcion
        {
            get
            {
                if (ViewState["SelectedIDIncripcion"] == null)
                {
                    return -1;
                }
                else
                {
                    return (int)ViewState["SelectedIDIncripcion"];
                }
            }
            set
            {
                ViewState["SelectedIDIncripcion"] = value;
            }
        }

        private bool HaySeleccion()
        {
            return (SelectedID != -1);
        }

        private bool HaySeleccionInscripcion()
        {
            return (SelectedIDIncripcion != -1);
        }
        protected void GridViewDocenteCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedID = (int?)GridViewDocenteCurso.SelectedValue;

            formPanelNotas.Visible = false;
            formActionsPanel.Visible = false;
            gridActionsPanel.Visible = true;
        }



        private void CargargridInscripcionesCurso()
        {
            try
            {
                // Usamos solo los cursos del plan en el que esta inscripto el alumno

                var inscripciones = InscripcionLogic.GetAllCur(DocenteCursoLogic.GetOne(SelectedID.Value).id_curso);
                
                /*List<int> materiasInscriptas;
                materiasInscriptas = inscripciones.Select(ins => LogicMateria.GetOne((LogicCurso.GetOne(ins.IdCurso).IdMateria)).ID).ToList();

                var idPlan = Session["IdPlan"];

                var cursos = LogicCurso.GetAll().Where(curso => LogicMateria.GetOne(curso.IdMateria).IdPlan == (int)idPlan);

                cursos = cursos.Where(c => !(materiasInscriptas.Contains(c.IdMateria))); */

                gdvInscripcionesCurso.DataSource = inscripciones.Select(ins => new
                {         
                     ID = ins.ID,
                      ALumno = PersonaLogic.GetOne(ins.IdAlumno).Apellido      
                } );


                if (inscripciones.Count() == 0)
                {
                    lblNoInscriptos.Visible = true;
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

     
        protected void lnkCargarNota_Click(object sender, EventArgs e)
        {
            if (HaySeleccion())
            {
                formPanelNotas.Visible = true;
                gridActionsPanel.Visible = false;
                formActionsPanel.Visible = true;

                CargargridInscripcionesCurso();
                lblNota.Visible = true;
                txtNota.Visible = true;
            }
        }

        protected void lnkAceptar_Click(object sender, EventArgs e)
        {
            formPanelNotas.Visible = false;
            gridActionsPanel.Visible = true;
            formActionsPanel.Visible = false;
            lblNoInscriptos.Visible = false;
            GridViewDocenteCurso.SelectedIndex = -1;
            GridViewDocenteCurso_SelectedIndexChanged(null, null);
        }

        protected void lnkCancelar_Click(object sender, EventArgs e)
        {
            formPanelNotas.Visible = false;
            gridActionsPanel.Visible = true;
            formActionsPanel.Visible = false;
            lblNoInscriptos.Visible = false;
            GridViewDocenteCurso.SelectedIndex = -1;
            GridViewDocenteCurso_SelectedIndexChanged(null, null);
        }

        private void CargarInscripcion()
        {
            AlumnoInscripcion inscripcion = new AlumnoInscripcion();
            inscripcion = InscripcionLogic.GetOne(SelectedIDIncripcion.Value);
            inscripcion.State = BusinessEntity.States.Modified;
            inscripcion.Nota = Convert.ToInt32(txtNota.Text);
            GuardarInscripcion(inscripcion);
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

        protected void lnkSubir_Click(object sender, EventArgs e)
        {
            SelectedIDIncripcion = (int?)gdvInscripcionesCurso.SelectedValue;
            if (HaySeleccionInscripcion())
            {
                CargarInscripcion();
                txtNota.Text = "";
                gdvInscripcionesCurso.SelectedIndex = -1;
                CargargridInscripcionesCurso();
             }
        }

       

        
    }
}