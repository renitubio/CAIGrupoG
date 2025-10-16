using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAIGrupoG.ConsultaTracking
{
    public partial class ConsultarTrackingForm : Form
    {
        private readonly ConsultarTrackingModelo modelo = new();

        public ConsultarTrackingForm()
        {
            InitializeComponent();
            // Asociamos los eventos a los botones
            this.BuscarBttn.Click += new System.EventHandler(this.BuscarBttn_Click);
            this.CancelarBttn.Click += new System.EventHandler(this.CancelarBttn_Click);
        }

        private void BuscarBttn_Click(object sender, EventArgs e)
        {
            string numeroGuia = GuiaText.Text.Trim();

            // Validación de campo en el formulario
            if (string.IsNullOrWhiteSpace(numeroGuia))
            {
                MessageBox.Show("Por favor, ingrese un número de guía.", "Campo Vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Llamada al modelo para buscar la guía
            var guiaEncontrada = modelo.BuscarGuia(numeroGuia);

            if (guiaEncontrada != null)
            {
                // Si se encontró, mostramos el estado formateado
                EstadoText.Text = FormatearEstado(guiaEncontrada.Estado);
            }
            else
            {
                // Si no se encontró, informamos al usuario
                MessageBox.Show("El número de guía ingresado no existe.", "Guía no Encontrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                EstadoText.Clear();
            }
        }

        private void CancelarBttn_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }


        /// Borra el contenido de los campos del formulario.
        private void LimpiarCampos()
        {
            GuiaText.Clear();
            EstadoText.Clear();
            GuiaText.Focus();
        }

        /// Convierte un valor del enum EstadoGuia a un string más legible para el usuario.
        private string FormatearEstado(EstadoGuia estado)
        {
            switch (estado)
            {
                case EstadoGuia.ImpuestoCallCenter: return "Impuesto Call Center";
                case EstadoGuia.ImpuestoAgencia: return "Impuesto Agencia";
                case EstadoGuia.EnCaminoARetirarDomicilio: return "En camino a retirar (Domicilio)";
                case EstadoGuia.EnCaminoARetirarAgencia: return "En camino a retirar (Agencia)";
                case EstadoGuia.AdmitidoEnCDOrigen: return "Admitido en CD Origen";
                case EstadoGuia.EnTransito: return "En Tránsito";
                case EstadoGuia.EnCDDestino: return "En CD Destino";
                case EstadoGuia.DistribucionUltimaMillaDomicilio: return "En distribución a domicilio";
                case EstadoGuia.PrimerIntentoEntrega: return "Primer intento de entrega";
                case EstadoGuia.Rechazado: return "Rechazado";
                case EstadoGuia.DistribucionUltimaMillaAgencia: return "En distribución a agencia";
                case EstadoGuia.PendienteDeRetiroEnAgencia: return "Pendiente de retiro en agencia";
                case EstadoGuia.Entregado: return "Entregado";
                default: return estado.ToString();
            }
        }
    }
}
