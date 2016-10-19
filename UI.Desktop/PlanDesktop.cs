using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.Logic;
using Business.Entities;

namespace UI.Desktop
{
    public partial class PlanDesktop : ApplicationForm
    {
        public PlanDesktop()
        {
            InitializeComponent();
            this.cargarEspecialidades();
        }

        private void cargarEspecialidades()
        {
            EspecialidadLogic especialidadLogic = new EspecialidadLogic();
            List<Especialidad> _especialidades = especialidadLogic.GetAll();

            cbEspecialidad.DataSource = _especialidades;
            cbEspecialidad.ValueMember = "ID";
            cbEspecialidad.DisplayMember = "Descripcion";
        }


        public Plan planActual { get; set; }

        public PlanDesktop(ModoForm modo) : this()
        {
            Modo = modo;
        }

        public PlanDesktop(int ID, ModoForm modo) : this()
        {
            Modo = modo;

            PlanLogic plan = new PlanLogic();
            try
            {
                planActual = plan.GetOne(ID);
                MapearDeDatos();
            }
            catch (Exception e)
            {
                Notificar(e);
            }
        }

        public override void MapearDeDatos()
        {
            cbEspecialidad.SelectedValue = planActual.IDEspecialidad;
            txtID.Text = planActual.ID.ToString();
            txtDescripcion.Text = planActual.Descripcion;

            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                btnAceptar.Text = "Guardar";
                txtID.Enabled = false;
            }
            else if (Modo == ModoForm.Baja)
            {
                txtID.Enabled = false;
                txtDescripcion.Enabled = false;
                cbEspecialidad.Enabled = false;
                btnAceptar.Text = "Eliminar";
            }
        }


        public override void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                Plan plan = new Plan();
                planActual = plan;
                planActual.State = BusinessEntity.States.New;
            }
            if (Modo == ModoForm.Modificacion)
            {
                planActual.ID = int.Parse(txtID.Text);
                planActual.State = BusinessEntity.States.Modified;
            }
            if (Modo == ModoForm.Baja)
            {
                planActual.ID = int.Parse(txtID.Text);
                planActual.State = BusinessEntity.States.Modified;
               // planActual.Baja = true; ver porque no se puede acceder al campo
            }

            planActual.Descripcion = txtDescripcion.Text;
            planActual.IDEspecialidad = Convert.ToInt32(cbEspecialidad.SelectedValue);
        }


        public override void GuardarCambios()
        {
            MapearADatos();

            PlanLogic plan = new PlanLogic();

            try
            {
                plan.Save(planActual);
            }
            catch (Exception e)
            {
                Notificar(e);
            }
        }


        public override bool Validar()
        {
            string msgError = "";
            bool retorno = true;

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                msgError += "El campo \"Descripcion\" no puede estar vacio\n";
                retorno = false;
            }

            if (cbEspecialidad.SelectedValue == null)
            {
                msgError += "Debe seleccionar una Especialidad para el plan\n";
                retorno = false;
            }

            if (retorno == false)
            {
                Notificar(msgError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return retorno;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                if (Validar() == true)
                {
                    GuardarCambios();
                    Close();
                }
            }
           /* else if (Modo == ModoForm.Baja) ver porque da error, esta buena la idea.
            {
                // TODO: Agregar una columna a las tablas en base de datos para dar de baja los registros
                // UNDONE: Modificar codigo para que elimine los registros de manera logica
                DialogResult resultado = Notificar("Al eliminar un plan se eliminaran todas las personas "
                    + "que pertenezcan al plan! \nDesea continuar?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {
                    GuardarCambios();
                }   

                Close();
            }  */
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
