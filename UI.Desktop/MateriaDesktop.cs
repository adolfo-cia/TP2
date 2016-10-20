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
    public partial class MateriaDesktop : ApplicationForm
    {
        private List<Plan> _planes;
        private List<Especialidad> _especialidades;

        public MateriaDesktop()
        {
            InitializeComponent();
            PlanLogic planLogic = new PlanLogic();
            _planes = planLogic.GetAll();
            this.CargarEspecialidades();
            this.CargarPlanes();
        }

        private void CargarEspecialidades()
        {
            EspecialidadLogic especialidadLogic = new EspecialidadLogic();
            _especialidades = especialidadLogic.GetAll();

            cbEspecialidad.ValueMember = "ID";
            cbEspecialidad.DisplayMember = "Descripcion";
            cbEspecialidad.DataSource = _especialidades;

        }

        private void CargarPlanes()
        {
            List<Plan> planesCombo = _planes.FindAll(x => x.IDEspecialidad == (int)cbEspecialidad.SelectedValue);

            cbPlan.DataSource = planesCombo;
            cbPlan.ValueMember = "ID";
            cbPlan.DisplayMember = "Descripcion";
        }

        public Materia materiaActual { get; set; }

        public MateriaDesktop(ModoForm modo) : this()
        {
            Modo = modo;
        }

        public MateriaDesktop(int ID, ModoForm modo) : this()
        {
            Modo = modo;
            MateriaLogic mat = new MateriaLogic();
            try
            {
                materiaActual = mat.GetOne(ID);
                MapearDeDatos();
            }
            catch (Exception e)
            {
                Notificar(e);
            }
        }



        public override void MapearDeDatos()
        {
            txtID.Text = materiaActual.ID.ToString();
            txtDescripcion.Text = materiaActual.Descripcion;
            txtHSSemanales.Text = materiaActual.HSSemanales.ToString();
            txtHSTotales.Text = materiaActual.HSSTotales.ToString();
            Plan p = _planes.Find(x => x.ID == materiaActual.IDPlan);
            cbEspecialidad.SelectedValue = _especialidades.Find(y => y.ID == p.IDEspecialidad).ID;
            cbPlan.SelectedValue = materiaActual.IDPlan;

            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                btnAceptar.Text = "Guardar";
            }
            else if (Modo == ModoForm.Baja)
            {

                txtID.Enabled = false;
                txtHSSemanales.Enabled = false;
                txtDescripcion.Enabled = false;
                txtHSTotales.Enabled = false;
                cbPlan.Enabled = false;
                cbEspecialidad.Enabled = false;
                btnAceptar.Text = "Eliminar";
            }

        }

        public override void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                Materia mat = new Materia();
                materiaActual = mat;
                materiaActual.State = BusinessEntity.States.New;
            }

            if (Modo == ModoForm.Modificacion)
            {
                materiaActual.ID = int.Parse(txtID.Text);
                materiaActual.State = BusinessEntity.States.Modified;
            }
            if (Modo == ModoForm.Baja)
            {
                materiaActual.ID = int.Parse(txtID.Text);
                materiaActual.State = BusinessEntity.States.Modified;
                //  materiaActual.Baja = true;
            }

            materiaActual.HSSemanales = int.Parse(txtHSSemanales.Text);
            materiaActual.HSSTotales = int.Parse(txtHSTotales.Text);
            materiaActual.Descripcion = txtDescripcion.Text;
            materiaActual.IDPlan = Convert.ToInt32(cbPlan.SelectedValue);

        }

        public override void GuardarCambios()
        {
            MapearADatos();


            MateriaLogic mat = new MateriaLogic();
            try
            {
                mat.Save(materiaActual);
            }
            catch (Exception e)
            {
                Notificar(e);
            }
        }

        public override bool Validar()
        {
            string error = "";
            int temp;
            bool retorno = true;

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                error += "El campo \"Descripcion\" no puede estar vacio\n";
                retorno = false;
            }

            if (string.IsNullOrWhiteSpace(txtHSSemanales.Text))
            {
                error += "El campo \"Horas semanales\" no puede estar vacio\n";
                retorno = false;
            }
            else if (int.TryParse(txtHSSemanales.Text, out temp) == false || temp < 0)
            {
                error += "El campo \"Horas semanales\" debe ser un entero mayor que 0\n";
                retorno = false;
            }

            if (string.IsNullOrWhiteSpace(txtHSTotales.Text))
            {
                error += "El campo \"Horas totales\" no puede estar vacio\n";
                retorno = false;
            }
            else if (int.TryParse(txtHSTotales.Text, out temp) == false || temp < 0)
            {
                error += "El campo \"Horas totales\" debe ser un entero mayor que 0\n";
                retorno = false;
            }

            if (cbPlan.SelectedItem == null)
            {
                error += "Debe seleccionar un plan, el campo no puede estar vacío";
                retorno = false;
            }

            if (retorno == false)
            {
                Notificar(error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return retorno;
        }

        public void Notificar(string titulo, string mensaje, MessageBoxButtons

        botones, MessageBoxIcon icono)

        {

            MessageBox.Show(mensaje, titulo, botones, icono);

        }

        public void Notificar(string mensaje, MessageBoxButtons botones,

        MessageBoxIcon icono)

        {

            this.Notificar(this.Text, mensaje, botones, icono);

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
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cbEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarPlanes();
        }
    }
}