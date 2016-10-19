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
    public partial class EspecialidadDesktop : ApplicationForm
    {
        public EspecialidadDesktop()
        {
            InitializeComponent();
        }

       public Especialidad EspecialidadActual { get; set; }

       public EspecialidadDesktop(ModoForm modo) : this()
        {
            Modo = modo;
        }

        public EspecialidadDesktop(int ID, ModoForm modo) : this()
        {
            Modo = modo;
            EspecialidadLogic espl = new EspecialidadLogic();
            EspecialidadActual = espl.GetOne(ID);
            this.MapearDeDatos();
        }






        public override void MapearDeDatos()
        {
            this.txtID.Text = this.EspecialidadActual.ID.ToString();
            this.txtDescripcion.Text = this.EspecialidadActual.Descripcion;
           
            //cambiamos el texto del boton aceptar según corresponda
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                btnAceptar.Text = "Guardar";

            }
            else if (Modo == ModoForm.Baja)
            {

                btnAceptar.Text = "Eliminar";
            }
            else if (Modo == ModoForm.Consulta)
            {
                btnAceptar.Text = "Aceptar";
            }
        }

        public override void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                Especialidad esp = new Especialidad();
                EspecialidadActual = esp;
                EspecialidadActual.State = BusinessEntity.States.New;

            }
            if (Modo == ModoForm.Modificacion)
            {
                EspecialidadActual.ID = int.Parse(txtID.Text);
                EspecialidadActual.State = BusinessEntity.States.Modified;
            }

            if (txtID.TextLength > 0)
                EspecialidadActual.ID = int.Parse(txtID.Text);

            if (Modo == ModoForm.Baja)
            {
                EspecialidadActual.ID = int.Parse(txtID.Text);
                EspecialidadActual.State = BusinessEntity.States.Modified;

            }

            EspecialidadActual.Descripcion = txtDescripcion.Text;

        }
    

        public override void GuardarCambios()
        {
            this.MapearADatos();
            EspecialidadLogic esp = new EspecialidadLogic();
            esp.Save(EspecialidadActual);

        }

        public override bool Validar()
        {
            string mensaje = "";
            bool bandera = true;

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                mensaje += "El campo \"Descripcion\" no puede estar vacio\n";
                bandera = false;
            }

            if (bandera == false)
            {
                Notificar(mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return bandera;

        }


        private void ApplicationForm_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                if (Validar() == true)
                {
                    GuardarCambios();
                    this.Close();
                }
            }
            else if (Modo == ModoForm.Baja)
            {
                GuardarCambios();
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
