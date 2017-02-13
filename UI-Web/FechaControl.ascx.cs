using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI_Web
{
    public partial class FechaControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDias();

                CargarMeses();

                CargarAnios();

                ddlMes.Attributes["onchange"] = "onCambiaFecha();";
                ddlAnio.Attributes["onchange"] = "onCambiaFecha();";
            }
            
        }

        private void CargarAnios()
        {
            List<int> anios = new List<int>(100);

            for (int i = 0; i < 100; i++)
            {
                anios.Add(DateTime.Now.Year - i);
            }

            ddlAnio.DataSource = anios;
            ddlAnio.DataBind();
        }

        private void CargarMeses()
        {
            Dictionary<int, string> meses = new Dictionary<int, string>();

            for (int i = 1; i < 13; i++)
            {
                meses.Add(i, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
            }

            ddlMes.DataSource = meses;
            ddlMes.DataValueField = "Key";
            ddlMes.DataTextField = "Value";
            ddlMes.DataBind();
        }

        private void CargarDias()
        {
            List<int> dias = new List<int>();

            for (int i = 1; i < 32; i++)
            {
                dias.Add(i);
            }

            ddlDia.DataSource = dias;
            ddlDia.DataBind();
        }

        public void SeleccionarFecha(string dia, string mes, string anio)
        {
            ddlAnio.SelectedValue = anio;
            ddlMes.SelectedValue = mes;
            ddlDia.SelectedValue = dia;
        }

        public DateTime ObtenerFecha()
        {
            var anio = int.Parse(ddlAnio.SelectedValue);
            var mes = ddlMes.SelectedIndex + 1;
            var dia = int.Parse(ddlDia.SelectedValue);

            var dt = new DateTime(anio, mes, dia);


            return dt;
        }

        public bool Enabled
        {
            set
            {
                ddlAnio.Enabled = value;
                ddlDia.Enabled = value;
                ddlMes.Enabled = value;
            }
        }
    }
}