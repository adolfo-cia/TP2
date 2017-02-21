using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Logic;
using Business.Entities;
using Microsoft.Reporting.WebForms;

namespace UI_Web
{
    public partial class PlanesMaterias : System.Web.UI.Page
    {
        #region variables y Getters/Setters
        private PlanLogic _planManager;
        private EspecialidadLogic _especialidadManager;
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
        private Plan PlanActual
        {
            get; set;
        }
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            try {
                if ((Persona.TipoPersona)Session["RolSesion"] == Persona.TipoPersona.Administrativo)
                {
                    CargarGrilla();


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
        private void CargarGrilla()
        {
            try
            {
                //var planes = PlanManager.GetAll();
                //// se reemplaza cada plan en planes con un objeto anonimo de la forma {ID, Descripcion, DEspecialidad}
                //// donde ID es la id del plan, Descripcion es la descripcion del plan y DEspecialidad es la descripcion de la especialidad del plan
                //gridPlanes.DataSource = planes.Select(plan => new { ID = plan.ID, Descripcion = plan.Descripcion, DEspecialidad = EspecialidadManager.GetOne(plan.IDEspecialidad).Descripcion });

                gridPlanes.DataSource = PlanManager.GetAllComplete();

                gridPlanes.DataBind();
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
                PlanActual = PlanManager.GetOne(id);
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

                if (FormMode == FormModes.Baja)
                {
                    PlanActual.State = BusinessEntity.States.Deleted;
                }
                else
                {
                    PlanActual.State = BusinessEntity.States.Modified;
                }

            }

            

            PlanActual.Descripcion = txtDescripcion.Text;
            PlanActual.IDEspecialidad = int.Parse(ddlEspecialidades.SelectedValue);
        }

        private void SavePlan(Plan pa)
        {
            try
            {
                PlanManager.Save(pa);
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
            SavePlan(PlanActual);
            CargarGrilla();
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
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = "Reportes/planesRDLC.rdlc";
            ReportDataSource ds = new ReportDataSource("DataSetPlanes",  PlanManager.GetAllComplete());
    
           
            viewer.LocalReport.DataSources.Add( ds);


            byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);


            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=" + "Reporte Planes" + "." + extension);
            Response.BinaryWrite(bytes); // create the file
            Response.Flush(); // send it to the client to download

         
        }

        protected void gridPlanes_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedID = (int?)gridPlanes.SelectedValue;
            planesPanel.Visible = false;
            formActionPanel.Visible = false;
            gridPlanesActionPanel.Visible = true;
        }
       
    }
}