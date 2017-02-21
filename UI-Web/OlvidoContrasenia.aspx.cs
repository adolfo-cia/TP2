using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using Business.Logic;
using Business.Entities;
using System.Text;

namespace UI_Web
{
    public partial class OlvidoContrasenia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void lnkCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(@"~/Login.aspx");
        }

        protected void lnkEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                PersonaLogic personaManager = new PersonaLogic();

                Persona per = personaManager.GetOneByNick(txtUsuario.Text);
                per.State = BusinessEntity.States.Modified;
                if (per.NombreUsuario == txtUsuario.Text)
                {
                    Random randomPass = new Random();
                    string posibles = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                    int longitud = posibles.Length;
                    char letra;
                    string nuevacadena = "";
                    for (int i = 0; i < 9; i++)
                    {
                        letra = posibles[randomPass.Next(longitud)];
                        nuevacadena += letra.ToString();
                    }
                    EnviarMail(per.Email, nuevacadena);
                    Page.ClientScript.RegisterStartupScript(this.GetType(),"Scripts","<script>alert('Correo enviado correctamente');</script>");
                    per.Clave = nuevacadena;
                    personaManager.Save(per);
                    Response.Redirect(@"~/Login.aspx");
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "mensajeError", "mensajeError('" + ex.Message + "');", true);
            }
        }
        private void EnviarMail(string mail, string cuerpo)
        {
            string de = "aplicaciontp2@gmail.com";
            string asunto = "Mensaje de recuperacion de contraseña";
            MailMessage mensaje = new MailMessage(de, mail, asunto, "Su nueva contraseña es: \n\n" + cuerpo);
            SmtpClient cliente = new SmtpClient("smtp.gmail.com");
            cliente.Port = Convert.ToInt32("587");
            cliente.EnableSsl = true;
            cliente.UseDefaultCredentials = false;
            cliente.DeliveryMethod = SmtpDeliveryMethod.Network;
            cliente.Credentials = new NetworkCredential(de, "zaq12wsxCDE3");
            cliente.Send(mensaje);
            mensaje.Dispose();
            cliente.Dispose();
        }
    }
}