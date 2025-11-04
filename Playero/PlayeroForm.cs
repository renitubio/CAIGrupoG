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
using CAIGrupoG.Almacenes;

namespace CAIGrupoG.Playero
{
    public partial class PlayeroForm : Form
    {
        private readonly PlayeroModelo modelo = new();

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

            // Suponiendo que BuscarGuiasPorPatente ahora retorna una tupla (List<Guia> Cargas, List<Guia> Descargas)
            var resultado = modelo.BuscarGuiasPorPatente(patente);


            // 3. Limpiar y poblar los ListViews
            PoblarListView(CargaListView, resultado.Cargas);
            PoblarListView(DescargarListView, resultado.Descargas);

            if (!resultado.Cargas.Any() && !resultado.Descargas.Any())
            {
                MessageBox.Show("No se encontraron guías pendientes para la patente ingresada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AceptarBttn_Click(object sender, EventArgs e)
        {
            string patente = PatenteTxt.Text.Trim();
            var resultado = modelo.BuscarGuiasPorPatente(patente);

            // Validar si hay guías para procesar
            if ((resultado.Cargas == null || !resultado.Cargas.Any()) &&
                (resultado.Descargas == null || !resultado.Descargas.Any()))
            {
                MessageBox.Show("No hay guías para procesar. Por favor, busque una patente primero.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Llamar al modelo para confirmar la operación
            modelo.ConfirmarOperacion(resultado.Cargas, resultado.Descargas);

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
        private void PoblarListView(ListView listView, List<GuiaEntidad> guias)
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
            PatenteTxt.Focus();
        }
    }
}
