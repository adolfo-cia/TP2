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
    public partial class CursoDesktop : ApplicationForm
    {
        public CursoDesktop()
        {
            InitializeComponent();
        }

        public override void MapearDeDatos()
        {
            this.txtID.Text = this.CursoActual.ID.ToString();
            this.txtAnioCalendario.Text = this.CursoActual.AnioCalendario.ToString();
            this.txtCupo.Text = this.CursoActual.Cupo.ToString();
            this.txtDescripcion.Text = this.CursoActual.Descripcion;
            this.txtIDComision.Text = this.CursoActual.IDComision.ToString();
            this.txtIDMateria.Text = this.CursoActual.IDMateria.ToString();

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
                Curso cur = new Curso();
                CursoActual = cur;
                CursoActual.State = BusinessEntity.States.New;

            }
            if (Modo == ModoForm.Modificacion)
            {
                CursoActual.ID = int.Parse(txtID.Text);
                CursoActual.State = BusinessEntity.States.Modified;
            }

            if (txtID.TextLength > 0)
                CursoActual.ID = int.Parse(txtID.Text);

            this.txtAnioCalendario.Text = this.CursoActual.AnioCalendario.ToString();
            this.txtCupo.Text = this.CursoActual.Cupo.ToString();
            this.txtDescripcion.Text = this.CursoActual.Descripcion;
            this.txtIDComision.Text = this.CursoActual.IDComision.ToString();
            this.txtIDMateria.Text = this.CursoActual.IDMateria.ToString();


            if (Modo == ModoForm.Baja)
            {
                CursoActual.ID = int.Parse(txtID.Text);
                CursoActual.State = BusinessEntity.States.Modified;
            }
        }

        public override void GuardarCambios()
        {
            this.MapearADatos();
            CursoLogic cur = new CursoLogic();
            cur.Save(CursoActual);
        }

        public override bool Validar()
        {
            //vincular los controles con los datos de la tabla de comisiones y materias
            return false;
        }

        public Curso CursoActual { get; set; }

        public CursoDesktop(ModoForm modo) : this()
        {
            Modo = modo;
        }

        public CursoDesktop(int ID, ModoForm modo) : this()
        {
            Modo = modo;
            CursoLogic Curso = new CursoLogic();
           // CursoActual = Curso.GetOne(); hay que hacer los parámetros del método, dan error
            this.MapearDeDatos();
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
