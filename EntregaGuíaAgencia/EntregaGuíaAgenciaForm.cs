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

namespace CAIGrupoG.EntregaGuíaAgencia
{
    public partial class EntregaGuíaAgenciaForm : Form
    {
        private readonly EntregaGuiaAgenciaModelo modelo = new();
        private List<Guia> guiasEncontradas;

        public void EntregaGuiaAgenciaForm()
        {
            InitializeComponent();
            // Asociar los eventos a los manejadores
            this.BuscarBttn.Click += new System.EventHandler(this.BuscarBttn_Click);
            this.RetirarBttn.Click += new System.EventHandler(this.RetirarBttn_Click);
            this.CancelarBttn.Click += new System.EventHandler(this.CancelarBttn_Click);
        }

        private void BuscarBttn_Click(object sender, EventArgs e)
        {
            string dni = DNIText.Text.Trim();

            // 1. Validar el formato del DNI
            if (!ValidarDNI(dni))
            {
                MessageBox.Show("El DNI ingresado no es válido. Debe contener 7 u 8 dígitos.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Buscar las guías en el modelo
            guiasEncontradas = modelo.BuscarGuiasPorDNI(dni);

            // 3. Mostrar las guías en el ListView
            PoblarListView(guiasEncontradas);

            if (guiasEncontradas.Count == 0)
            {
                MessageBox.Show("No se encontraron guías pendientes de retiro para el DNI ingresado.", "Sin Resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void RetirarBttn_Click(object sender, EventArgs e)
        {
            // Validar si se encontraron guías previamente
            if (guiasEncontradas == null || guiasEncontradas.Count == 0)
            {
                MessageBox.Show("No hay guías para retirar. Por favor, realice una búsqueda primero.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirmar la operación en el modelo
            modelo.ConfirmarRetiro(guiasEncontradas);

            MessageBox.Show("Operación Exitosa. Las guías han sido marcadas como retiradas.", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LimpiarFormulario();
        }

        private void CancelarBttn_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        /// <summary>
        /// Valida que el string DNI tenga 7 u 8 dígitos numéricos.
        /// </summary>
        private bool ValidarDNI(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                return false;

            return Regex.IsMatch(dni, @"^\d{7,8}$");
        }

        /// <summary>
        /// Limpia y rellena el ListView con una lista de guías.
        /// </summary>
        private void PoblarListView(List<Guia> guias)
        {
            GuiasListView.Items.Clear();
            if (guias == null) return;

            foreach (var guia in guias)
            {
                var row = new string[]
                {
                    guia.NumeroGuia,
                    "Pendiente de retiro en agencia", // El estado es siempre este según la lógica de búsqueda
                    guia.TipoPaquete.ToString()
                };
                var item = new ListViewItem(row);
                GuiasListView.Items.Add(item);
            }
        }

        /// <summary>
        /// Restablece el formulario a su estado inicial.
        /// </summary>
        private void LimpiarFormulario()
        {
            DNIText.Clear();
            GuiasListView.Items.Clear();
            guiasEncontradas = null;
            DNIText.Focus();
        }
    }
}
