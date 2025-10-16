using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAIGrupoG.EmitirFactura
{
    public partial class EmitirFacturaForm : Form
    {
        private readonly EmitirFacturaModelo modelo = new();
        private List<Guia> guiasEncontradas = new List<Guia>();

        public EmitirFacturaForm()
        {
            InitializeComponent();
            // Limpiamos la lista de cualquier ítem de prueba que venga del diseñador
            GuiasListView.Items.Clear();

            // Asociamos los eventos a los botones
            this.BuscarBttn.Click += new System.EventHandler(this.BuscarBttn_Click);
            this.EmitirBttn.Click += new System.EventHandler(this.EmitirBttn_Click);
            this.CancelarBttn.Click += new System.EventHandler(this.CancelarBttn_Click);
        }

        private void BuscarBttn_Click(object sender, EventArgs e)
        {
            string cuit = CuitText.Text;

            if (!ValidarCUIT(cuit))
            {
                MessageBox.Show("El CUIT ingresado no es válido.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            guiasEncontradas = modelo.BuscarGuiasPorCUIT(cuit);
            PoblarListView(guiasEncontradas);

            if (guiasEncontradas.Count == 0)
            {
                MessageBox.Show("No se encontraron guías pendientes de facturar para el CUIT ingresado.", "Sin Resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void EmitirBttn_Click(object sender, EventArgs e)
        {
            if (guiasEncontradas.Count == 0)
            {
                MessageBox.Show("No hay guías para facturar. Por favor, realice una búsqueda primero.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            modelo.EmitirFacturas(guiasEncontradas);
            MessageBox.Show("Facturas emitidas exitosamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarFormulario();
        }

        private void CancelarBttn_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void PoblarListView(List<Guia> guias)
        {
            GuiasListView.Items.Clear();
            foreach (var guia in guias)
            {
                var row = new string[]
                {
                    guia.NumeroGuia,
                    guia.Estado.ToString(),
                    guia.RazonSocial,
                    // Le damos formato de moneda argentina
                    guia.Importe.ToString("C", new System.Globalization.CultureInfo("es-AR"))
                };
                GuiasListView.Items.Add(new ListViewItem(row));
            }
        }

        private void LimpiarFormulario()
        {
            CuitText.Clear();
            GuiasListView.Items.Clear();
            guiasEncontradas.Clear();
            CuitText.Focus();
        }

        /// Valida un CUIT argentino, incluyendo el dígito verificador.
        public bool ValidarCUIT(string cuit)
        {
            if (string.IsNullOrWhiteSpace(cuit)) return false;

            // Quitamos guiones y espacios
            cuit = cuit.Replace("-", "").Replace(" ", "");

            if (cuit.Length != 11 || !cuit.All(char.IsDigit)) return false;

            var digitos = cuit.Select(c => int.Parse(c.ToString())).ToArray();
            var secuencia = new int[] { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };

            int suma = 0;
            for (int i = 0; i < 10; i++)
            {
                suma += digitos[i] * secuencia[i];
            }

            int resto = suma % 11;
            int digitoVerificador = 11 - resto;
            if (digitoVerificador == 11)
            {
                digitoVerificador = 0;
            }
            else if (digitoVerificador == 10)
            {
                // Un CUIT válido no puede terminar en 10, pero la lógica de validación
                // a veces incluye casos especiales con CUITs de 23, 24, etc.
                // Para simplificar, lo consideramos inválido.
                return false;
            }

            return digitos[10] == digitoVerificador;
        }
    }
}
