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
    public partial class Cursos : System.Web.UI.Page
    {
        private CursoLogic _logicCurso;
        private MateriaLogic _logicMateria;
        private ComisionLogic _logicComision;

        private Curso CursoActual {get; set;}

        private MateriaLogic LogicMateria
        {
            get
            {
                if(_logicMateria == null)
                {
                    _logicMateria = new MateriaLogic();
                }
                return _logicMateria;
            }
        }

        private CursoLogic LogicCurso
        {
            get 
            {
                if(_logicCurso==null)
                {
                    _logicCurso = new CursoLogic();
                }
                return _logicCurso;
            }
        }
        private ComisionLogic LogicComision
        {
            get
            {
                if (_logicComision == null)
                {
                    _logicComision = new ComisionLogic();
                }
                return _logicComision;
            }
        }

        private void LoadGrid()
        {
            CargarGridCursos();
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try {
                if ((Persona.TipoPersonas)Session["RolSesion"] == Persona.TipoPersonas.Administrativo)
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

        private void CargarGridCursos()
        {
            try
            {
                var cursos = LogicCurso.GetAll();
                gdvCursos.DataSource = cursos.Select(curso => new
                {
                    ID = curso.ID,
                    AnioCalendario = curso.AnioCalendario,
                    Cupo = curso.Cupo,
                    Materia = LogicMateria.GetOne(curso.IdMateria).Descripcion,
                    Comision = LogicComision.GetOne(curso.IdComision).Descripcion
                });
                gdvCursos.DataBind();
            }
            catch (Exception ex)
            {
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

        protected void gdvCursos_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIDCurso = (int?)gdvCursos.SelectedValue;

            formPanelCurso.Visible = false;
            formActionsPanel.Visible = false;
            gridActionsPanel.Visible = true;
        }

        enum FormModes
        {
            Alta,
            Modificacion,
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

        private void CargarMaterias()
        {
            try
            {
                ddlMaterias.DataSource = LogicMateria.GetAll();
                ddlMaterias.DataValueField = "ID";
                ddlMaterias.DataTextField = "Descripcion";
                ddlMaterias.DataBind();

                ddlMaterias_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            }
        }

        private void CargarAnios()
        {
            List<int> anios = new List<int>(5);

            for (int i = 0; i < 5; i++)
            {
                anios.Add(DateTime.Now.Year + i);
            }

            ddlAnioCalendario.DataSource = anios;
            ddlAnioCalendario.DataBind();
        }
        private void CargarCupos()
        {
            List<int> cupos = new List<int>();

            for (int i = 20; i < 36; i++)
            {
                cupos.Add(i);
            }

            ddlCupo.DataSource = cupos;
            ddlCupo.DataBind();
        }

              
        private bool HaySeleccion()
        {
            return (SelectedIDCurso != -1);
        }

        void CargarForm(int id)
        {
            // No hace falta cargar comisiones ya que estas se cargan en selectedindexchanged de materias
            CargarMaterias();
            CargarAnios();
            CargarCupos();

            if (FormMode == FormModes.Baja)
            {
                ddlMaterias.Enabled = false;
                ddlComisiones.Enabled = false;
                ddlAnioCalendario.Enabled = false;
                ddlCupo.Enabled = false;
            }
            else
            {
                ddlMaterias.Enabled = true;
                ddlComisiones.Enabled = true;
                ddlAnioCalendario.Enabled = true;
                ddlCupo.Enabled = true;
            }
            if (FormMode != FormModes.Alta)
            {
                CursoActual = LogicCurso.GetOne(id);
                                               
                ddlMaterias.SelectedValue = CursoActual.IdMateria.ToString();
                ddlMaterias_SelectedIndexChanged(null, null);
                ddlComisiones.SelectedValue = CursoActual.IdComision.ToString();
                ddlAnioCalendario.SelectedValue = CursoActual.AnioCalendario.ToString();
                ddlCupo.SelectedValue = CursoActual.Cupo.ToString();
            }
        }

        private void CargarCurso()
        {
            CursoActual = new Curso();

            if (FormMode == FormModes.Alta)
            {
                CursoActual.Baja = false;
                CursoActual.State = BusinessEntity.States.New;
            }
            if (FormMode == FormModes.Baja || FormMode == FormModes.Modificacion)
            {
                CursoActual.State = BusinessEntity.States.Modified;
                CursoActual.ID = SelectedIDCurso.Value;
                if (FormMode == FormModes.Baja)
                {
                    CursoActual.Baja = true;
                }
                else
                {
                    CursoActual.Baja = false;
                }
            }
          
            CursoActual.IdMateria = int.Parse(ddlMaterias.SelectedValue);
            CursoActual.IdComision = int.Parse(ddlComisiones.SelectedValue);
            CursoActual.AnioCalendario = int.Parse(ddlAnioCalendario.SelectedValue);
            CursoActual.Cupo = int.Parse(ddlCupo.SelectedValue);
        }

        private void GuardarCurso(Curso cur)
        {
            LogicCurso.Save(cur);
        }

        protected void lnkNuevo_Click(object sender, EventArgs e)
        {
            FormMode = FormModes.Alta;
            formPanelCurso.Visible = true;
            formActionsPanel.Visible = true;
            gridActionsPanel.Visible = false;
           
            CargarForm(SelectedIDCurso.Value);
        }

        protected void lnkEditar_Click(object sender, EventArgs e)
        {
            if (HaySeleccion())
            {
               
                FormMode = FormModes.Modificacion;
                formPanelCurso.Visible = true;
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
                formPanelCurso.Visible = true;
                formActionsPanel.Visible = true;
                gridActionsPanel.Visible = false;

                CargarForm(SelectedIDCurso.Value);
            }
        }

        protected void lnkAceptar_Click(object sender, EventArgs e)
        {
            formPanelCurso.Visible = false;
            formActionsPanel.Visible = false;
            gridActionsPanel.Visible = true;

            CargarCurso();
            GuardarCurso(CursoActual);
            CargarGridCursos();

            gdvCursos.SelectedIndex = -1;
            gdvCursos_SelectedIndexChanged(null, null);            
        }

        protected void lnkCancelar_Click(object sender, EventArgs e)
        {
            formPanelCurso.Visible = false;
            formActionsPanel.Visible = false;
            gridActionsPanel.Visible = true;

            gdvCursos.SelectedIndex = -1;
            gdvCursos_SelectedIndexChanged(null, null); 
        }

        protected void ddlMaterias_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idplan = LogicMateria.GetOne(int.Parse(ddlMaterias.SelectedValue)).IdPlan;
                ddlComisiones.DataSource = LogicComision.GetAll().Where(comi => comi.IdPlan == idplan);

                ddlComisiones.DataValueField = "ID";
                ddlComisiones.DataTextField = "Descripcion";

                ddlComisiones.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            }
        }

        protected void lnkReporte_Click(object sender, EventArgs e)
        {
            try
            {
                CursosReport rpt = new CursosReport(LogicCurso.GetAll());

                rpt.Run(false);
                // Specify the appropriate viewer.
                // If the report has been exported in a different format, the content-type will 
                // need to be changed as noted in the following table:
                //    ExportType  ContentType
                //    PDF       "application/pdf"  (needs to be in lowercase)
                //    RTF       "application/rtf"
                //    TIFF      "image/tiff"       (will open in separate viewer instead of browser)
                //    HTML      "message/rfc822"   (only applies to compressed HTML pages that includes images)
                //    Excel     "application/vnd.ms-excel"
                //    Excel     "application/excel" (either of these types should work) 
                //    Text      "text/plain"  
                Response.ContentType = "application/pdf";
                Response.Clear();
                Response.AddHeader("content-disposition", "inline;filename=MyPDF.PDF");
                // Create the PDF export object.
                PdfExport pdf = new PdfExport();
                // Create a new memory stream that will hold the pdf output
                System.IO.MemoryStream memStream = new System.IO.MemoryStream();
                // Export the report to PDF.
                pdf.Export(rpt.Document, memStream);
                // Write the PDF stream to the output stream.
                Response.BinaryWrite(memStream.ToArray());
                // Send all buffered content to the client
                Response.End();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            }
        }
    }
}