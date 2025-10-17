using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAIGrupoG.Playero
{
    public partial class PlayeroForm : Form
    {
        private readonly PlayeroModelo modelo = new();

        //TODO: Estas dos variables se tienen que ir. Pedirle los datos al modelo en donde se necesiten, las veces q haga falta.
        private List<Guia> guiasCargaActuales;
        private List<Guia> guiasDescargaActuales;

        public PlayeroForm()
        {
            InitializeComponent();
            // Asociar los eventos a los manejadores
            this.BuscarGuiasAsociadasBttn.Click += new System.EventHandler(this.BuscarGuiasAsociadasBttn_Click);
            this.AceptarBttn.Click += new System.EventHandler(this.AceptarBttn_Click);
        }

        private void BuscarGuiasAsociadasBttn_Click(object sender, EventArgs e)
        {
            string patente = PatenteTxt.Text.Trim();

            // 1. Validación de la patente
            if (!ValidarPatente(patente))
            {
                MessageBox.Show("El formato de la patente no es válido. Formatos aceptados: AA123BC o ABC123.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Llamada al modelo para buscar las guías
            var resultado = modelo.BuscarGuiasPorPatente(patente);
            guiasCargaActuales = resultado.Cargas;
            guiasDescargaActuales = resultado.Descargas;

            // 3. Limpiar y poblar los ListViews
            PoblarListView(CargaListView, guiasCargaActuales);
            PoblarListView(DescargarListView, guiasDescargaActuales);

            if (!guiasCargaActuales.Any() && !guiasDescargaActuales.Any())
            {
                MessageBox.Show("No se encontraron guías pendientes para la patente ingresada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AceptarBttn_Click(object sender, EventArgs e)
        {
            // Validar si hay guías para procesar
            if ((guiasCargaActuales == null || !guiasCargaActuales.Any()) &&
                (guiasDescargaActuales == null || !guiasDescargaActuales.Any()))
            {
                MessageBox.Show("No hay guías para procesar. Por favor, busque una patente primero.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Llamar al modelo para confirmar la operación
            modelo.ConfirmarOperacion(guiasCargaActuales, guiasDescargaActuales);

            MessageBox.Show("Operación Exitosa.", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Limpiar la pantalla para la siguiente operación
            LimpiarFormulario();
        }

        /// <summary>
        /// Valida el formato de una patente argentina (Mercosur y anterior).
        /// </summary>
        private bool ValidarPatente(string patente)
        {
            if (string.IsNullOrWhiteSpace(patente))
            {
                return false;
            }
            // Expresión regular para patentes Mercosur (AA123BC) y antiguas (ABC123)
            var regex = new Regex(@"^(?:[A-Z]{2}\d{3}[A-Z]{2})|(?:[A-Z]{3}\d{3})$");
            return regex.IsMatch(patente.ToUpper());
        }

        /// <summary>
        /// Rellena un control ListView con una lista de guías.
        /// </summary>
        private void PoblarListView(ListView listView, List<Guia> guias)
        {
            listView.Items.Clear(); // Limpiar items previos

            if (guias == null) return;

            foreach (var guia in guias)
            {
                var row = new string[]
                {
                    guia.NumeroGuia,
                    guia.TipoPaquete.ToString(),
                    guia.CUIT,
                    guia.CDOrigen,
                    guia.CDDestino
                };
                var item = new ListViewItem(row);
                listView.Items.Add(item);
            }
        }

        /// <summary>
        /// Limpia todos los controles del formulario a su estado inicial.
        /// </summary>
        private void LimpiarFormulario()
        {
            PatenteTxt.Clear();
            CargaListView.Items.Clear();
            DescargarListView.Items.Clear();
            guiasCargaActuales = null;
            guiasDescargaActuales = null;
            PatenteTxt.Focus();
        }
    }
}
