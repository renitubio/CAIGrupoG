using CAIGrupoG.Almacenes;
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

namespace CAIGrupoG.EntregaGuíaCD
{
    public partial class EntregaGuíaCDForm : Form
    {
        private readonly EntregaGuiaCDModelo modelo = new();
        private List<GuiaEntidad> guiasEncontradas = new List<GuiaEntidad>();

        public EntregaGuíaCDForm()
        {
            InitializeComponent();
            this.BuscarBttn.Click += new System.EventHandler(this.BuscarBttn_Click);
            this.RetirarBttn.Click += new System.EventHandler(this.RetirarBttn_Click);
            this.CancelarBttn.Click += new System.EventHandler(this.CancelarBttn_Click);
        }

        private void BuscarBttn_Click(object sender, EventArgs e)
        {
            string dni = DNIText.Text.Trim();
            if (!ValidarDNI(dni))
            {
                MessageBox.Show("El DNI ingresado no es válido. Debe contener 7 u 8 dígitos.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            guiasEncontradas = modelo.BuscarGuiasPorDNI(dni);
            PoblarListView(guiasEncontradas);

            if (guiasEncontradas.Count == 0)
            {
                MessageBox.Show("No se encontraron guías pendientes de retiro para el DNI ingresado.", "Sin Resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void RetirarBttn_Click(object sender, EventArgs e)
        {
            if (guiasEncontradas.Count == 0)
            {
                MessageBox.Show("No hay guías para retirar. Por favor, realice una búsqueda primero.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            modelo.ConfirmarRetiro(guiasEncontradas);
            MessageBox.Show("Operación Exitosa. Las guías han sido marcadas como retiradas.", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarFormulario();
        }

        private void CancelarBttn_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private bool ValidarDNI(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                return false;
            return Regex.IsMatch(dni, @"^\d{7,8}$");
        }

        private void PoblarListView(List<GuiaEntidad> guias)
        {
            GuiasListView.Items.Clear();
            if (guias == null) return;
            foreach (var guia in guias)
            {
                var row = new string[]
                {
                    guia.NumeroGuia,
                    "Pendiente de retiro en CD",
                    guia.TipoPaquete.ToString()
                };
                var item = new ListViewItem(row);
                GuiasListView.Items.Add(item);
            }
        }

        private void LimpiarFormulario()
        {
            DNIText.Clear();
            GuiasListView.Items.Clear();
            guiasEncontradas.Clear();
            DNIText.Focus();
        }
    }
}
