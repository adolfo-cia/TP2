using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Business.Entities;

namespace UI_Web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try {
                //si se autentica mostramos mensaje y nombre de usuario
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (Request.QueryString["mensaje"] != null)
                    {
                        Response.Write(Server.UrlDecode(Request.QueryString["mensaje"]));
                    }

                    menuPanel.GroupingText = User.Identity.Name;

                    if ((Persona.TipoPersona)Session["RolSesion"] == Persona.TipoPersona.Administrativo)
                    {
                        hlkCargarNotas.Visible = false;
                        hlkInscripcionCurso.Visible = false;
                    }
                    if ((Persona.TipoPersona)Session["RolSesion"] == Persona.TipoPersona.Alumno)
                    {
                        hlkCargarNotas.Visible = false;
                        hlkComisiones.Visible = false;
                        hlkCursos.Visible = false;
                        hlkEspecialidades.Visible = false;
                        hlkInscripcionCurso.Visible = true;
                        hlkMaterias.Visible = false;
                        hlkPlanes.Visible = false;
                        hlkUsuarios.Visible = false;
                        hlkDocenteCurso.Visible = false;
                    }
                    if ((Persona.TipoPersona)Session["RolSesion"] == Persona.TipoPersona.Docente)
                    {
                        hlkCargarNotas.Visible = true;
                        hlkComisiones.Visible = false;
                        hlkCursos.Visible = false;
                        hlkEspecialidades.Visible = false;
                        hlkInscripcionCurso.Visible = false;
                        hlkMaterias.Visible = false;
                        hlkPlanes.Visible = false;
                        hlkUsuarios.Visible = false;
                        hlkDocenteCurso.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Redirect(@"~/Login.aspx");
                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            }

        }
    }
}