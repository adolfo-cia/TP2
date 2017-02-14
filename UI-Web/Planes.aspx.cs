using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Logic;
using Business.Entities;

namespace UI.Web
{
    public partial class PlanesMaterias : System.Web.UI.Page
    {
        private PlanLogic _planLogic;
        private EspecialidadLogic _especialidadLogic;

        private Plan PlanActual
        {
            get; set;
        }

        private PlanLogic LogicPlan
        {
            get
            {
                if (_planLogic == null)
                {
                    _planLogic = new PlanLogic();
                }

                return _planLogic;
            }
        }
        private EspecialidadLogic LogicEspecialidad
        {
            get
            {
                if (_especialidadLogic == null)
                {
                    _especialidadLogic = new EspecialidadLogic();
                }

                return _especialidadLogic;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try {
                if ((Persona.TipoPersona)Session["RolSesion"] == Persona.TipoPersona.Administrativo)
                {
                    CargarGridPlanes();


                    if (!Page.IsPostBack)
                    {
                        SelectedID = -1;
                    }
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

        private void CargarGridPlanes()
        {
            try
            {
                var planes = LogicPlan.GetAll();
                // se reemplaza cada plan en planes con un objeto anonimo de la forma {ID, Descripcion, DEspecialidad}
                // donde ID es la id del plan, Descripcion es la descripcion del plan y DEspecialidad es la descripcion de la especialidad del plan
                gridPlanes.DataSource = planes.Select(plan => new { ID = plan.ID, Descripcion = plan.Descripcion, DEspecialidad = LogicEspecialidad.GetOne(plan.IDEspecialidad).Descripcion });

                gridPlanes.DataBind();
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

        protected void gridPlanes_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedID = (int?)gridPlanes.SelectedValue;
            planesPanel.Visible = false;
            formActionPanel.Visible = false;
            gridPlanesActionPanel.Visible = true;
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

        private void CargarEspecialidades()
        {
            try
            {
                ddlEspecialidades.DataSource = LogicEspecialidad.GetAll();
                ddlEspecialidades.DataValueField = "ID";
                ddlEspecialidades.DataTextField = "Descripcion";
                ddlEspecialidades.DataBind();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            }
        }

        private bool HaySeleccion()
        {
            return (SelectedID != -1);
        }

        void CargarForm(int id)
        {
            if (FormMode == FormModes.Baja)
            {
                txtDescripcion.Enabled = false;
                ddlEspecialidades.Enabled = false;
            }
            if (FormMode == FormModes.Modificacion)
            {
                txtDescripcion.Enabled = true;
                ddlEspecialidades.Enabled = true;
            }

            CargarEspecialidades();

            try
            {
                PlanActual = LogicPlan.GetOne(id);
                Session["Plan"] = PlanActual;
                txtDescripcion.Text = PlanActual.Descripcion;
                ddlEspecialidades.SelectedValue = PlanActual.IDEspecialidad.ToString();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            }


        }

        private void  CargarPlan()
        { 
            if (FormMode == FormModes.Alta)
            {
                PlanActual = new Plan();
                PlanActual.State = BusinessEntity.States.New;
            }
            if (FormMode == FormModes.Baja ||FormMode == FormModes.Modificacion)
            {
                PlanActual = (Plan)Session["Plan"];
                PlanActual.State = BusinessEntity.States.Modified;
            }

            if (FormMode == FormModes.Baja)
            {
                // PlanActual.Baja = true;
                PlanActual.State = BusinessEntity.States.Deleted;
            }
            else
            {
                //  PlanActual.Baja = false;
                PlanActual.State = BusinessEntity.States.Modified;
            }

            PlanActual.Descripcion = txtDescripcion.Text;
            PlanActual.IDEspecialidad = int.Parse(ddlEspecialidades.SelectedValue);
        }

        private void GuardarPlan(Plan pa)
        {
            try
            {
                LogicPlan.Save(pa);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            }
        }

        protected void lnkNuevo_Click(object sender, EventArgs e)
        {
            FormMode = FormModes.Alta;
            planesPanel.Visible = true;
            formActionPanel.Visible = true;
            gridPlanesActionPanel.Visible = false;

            CargarEspecialidades();
            txtDescripcion.Enabled = true;
            ddlEspecialidades.Enabled = true;

            txtDescripcion.Text = "";
            ddlEspecialidades.SelectedIndex = 0;
        }

        protected void lnkEditar_Click(object sender, EventArgs e)
        {
            if (HaySeleccion())
            {
                FormMode = FormModes.Modificacion;
                planesPanel.Visible = true;
                formActionPanel.Visible = true;
                gridPlanesActionPanel.Visible = false;

                CargarForm(SelectedID.Value);
            }
        }

        protected void lnkEliminar_Click(object sender, EventArgs e)
        {
            if (HaySeleccion())
            {
                planesPanel.Visible = true;
                formActionPanel.Visible = true;
                gridPlanesActionPanel.Visible = false;
                FormMode = FormModes.Baja;
                
                CargarForm(SelectedID.Value);
            }
        }

        protected void lnkAceptar_Click(object sender, EventArgs e)
        {
            CargarPlan();
            GuardarPlan(PlanActual);
            CargarGridPlanes();
            formActionPanel.Visible = false;
            planesPanel.Visible = false;
            gridPlanesActionPanel.Visible = true;
                

            gridPlanes.SelectedIndex = -1;
            gridPlanes_SelectedIndexChanged(null, null);
        }

        protected void lnkCancelar_Click(object sender, EventArgs e)
        {
            planesPanel.Visible = false;
            formActionPanel.Visible = false;
            gridPlanesActionPanel.Visible = true;

            gridPlanes.SelectedIndex = -1;
            gridPlanes_SelectedIndexChanged(null, null);
            
        }

        protected void lnkReporte_Click(object sender, EventArgs e)
        {
         //   try
           // {
             //   PlanesReport rpt = new PlanesReport(LogicPlan.GetAll());

               // rpt.Run(false);
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
               // Response.ContentType = "application/pdf";
               // Response.Clear();
               // Response.AddHeader("content-disposition", "inline;filename=MyPDF.PDF");
                // Create the PDF export object.
               // PdfExport pdf = new PdfExport();
                // Create a new memory stream that will hold the pdf output
               // System.IO.MemoryStream memStream = new System.IO.MemoryStream();
                // Export the report to PDF.
               // pdf.Export(rpt.Document, memStream);
                // Write the PDF stream to the output stream.
               // Response.BinaryWrite(memStream.ToArray());
                // Send all buffered content to the client
               // Response.End();
           // }
           // catch (Exception ex)
           // {
           //     Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
           // }
        }
    }
}