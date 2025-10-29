using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAIGrupoG.ResultadoCostosVSVentas
{
    public partial class CostosVsVentasForm : Form
    {
        private readonly CostosVsVentasModelo modelo = new();

        public CostosVsVentasForm()
        {
            InitializeComponent();
            // Asociamos los eventos a los botones
            this.BuscarBttn.Click += new System.EventHandler(this.BuscarBttn_Click);
            this.CancelarBttn.Click += new System.EventHandler(this.CancelarBttn_Click);
        }

        private void BuscarBttn_Click(object sender, EventArgs e)
        {
            DateTime fechaDesde = DesdeTimePicker.Value;
            DateTime fechaHasta = HastaTimePicker.Value;

            // Validación en el formulario: la fecha "Desde" no puede ser posterior a "Hasta".
            if (fechaDesde.Date > fechaHasta.Date)
            {
                MessageBox.Show("La fecha 'Desde' no puede ser posterior a la fecha 'Hasta'.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Pedimos los datos al modelo
            var resultados = modelo.BuscarResultados(fechaDesde, fechaHasta);
            PoblarListView(resultados);

            if (resultados.Count == 0)
            {
                MessageBox.Show("No se encontraron resultados para el período seleccionado.", "Sin Resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CancelarBttn_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        /// Rellena el ListView con los resultados económicos.
        private void PoblarListView(List<ResultadoEconomico> resultados)
        {
            ResultadoListView.Items.Clear();
            var culturaARS = new CultureInfo("es-AR");

            foreach (var res in resultados)
            {
                var row = new string[]
                {
                    // Formateamos los valores a moneda argentina y porcentaje
                    res.ResultadoEmpresa.ToString("C", culturaARS),
                    res.Ventas.ToString("C", culturaARS),
                    res.Costos.ToString("C", culturaARS),
                    res.Rentabilidad.ToString("N2") + " %" // Formato Numérico con 2 decimales
                };
                ResultadoListView.Items.Add(new ListViewItem(row));
            }
        }

        /// Limpia los controles del formulario a su estado inicial.
        private void LimpiarFormulario()
        {
            ResultadoListView.Items.Clear();
            // Reseteamos los calendarios a la fecha de hoy
            DesdeTimePicker.Value = DateTime.Now;
            HastaTimePicker.Value = DateTime.Now;
        }
    }
}
