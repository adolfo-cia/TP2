using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.Entities;
using Business.Logic;

namespace UI.Desktop
{
    public partial class formLogin : ApplicationForm
    {
        //La entity q se va a obtener desp del logueo
        private Persona UsuarioActual { get; set; }
        public Persona.TipoPersona TIPO { get { return UsuarioActual.Tipo; } }
        public formLogin()
        {
            InitializeComponent();
        }

        
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                try
                {
                    UsuarioActual = new PersonaLogic().GetOneByNick(this.txtUsuario.Text);
                    if (UsuarioActual != null && UsuarioActual.Clave == txtPassword.Text)
                    {
                        Notificar("Login", "Bienvenido " + UsuarioActual.NombreUsuario + @"   ༼ つ ◕_◕ ༽つ ", MessageBoxButtons.OK, MessageBoxIcon.None);
                        this.DialogResult = DialogResult.OK;
                    }
                    else if (UsuarioActual != null || UsuarioActual.Habilitado == false)
                    {
                        Notificar("Login", "Usuario no habilitado para uso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        Notificar("Login", "Usuario y/o password incorrectos!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {

                    Notificar(ex);
                }
            }
            
        }

        private void lnkOlvidaPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Es usted un usuario muy descuidado, haga memoria", "Olvide mi password!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public override bool Validar()
        {
            if (this.txtUsuario.Text == "")
            {
                Notificar("Login", "El campo Usuario no debe estar vacio!",  MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (this.txtPassword.Text == "")
            {
                Notificar("Login", "El campo Password no debe estar vacio!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            return true;
        }
    }
}
