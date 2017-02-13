using Business.Entities;
using Business.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Web
{
    public partial class Login : System.Web.UI.Page
    {

        private Persona usrActual { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            PersonaLogic personaManager = new PersonaLogic();

            try
            {
                usrActual = personaManager.GetOneByNick(loginAcademia.UserName);
                // UNDONE: Restringir el acceso a ciertos forms segun el tipo de persona
                if (usrActual.Clave != null &&
                    Util.Hash.VerificarHash(Encoding.ASCII.GetBytes(usrActual.Clave), loginAcademia.Password) &&
                    usrActual.Habilitado == true)
                { 
                    Session["RolSesion"] = usrActual.Tipo;
                    Session["IdAlumno"] = usrActual.ID;
                    Session["IdPlan"] = usrActual.IDPlan;
                    FormsAuthentication.RedirectFromLoginPage(loginAcademia.UserName, loginAcademia.RememberMeSet);
                }
                else
                {
                    if (usrActual.Habilitado == false)
                    {
                        Response.Write("El usuario " + User.Identity.Name + " no esta habilitado a usar el sistema.");
                    }
                    else
                    {
                        Response.Write("Usuario y/o contraseña incorrectos");
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('"+ex.Message+"');", true);
            }
        }


    }
}