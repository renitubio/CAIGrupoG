using CAIGrupoG.Almacenes;
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
        private string FormatearEstado(EstadoEncomiendaEnum estado)
        {
            switch (estado)
            {
                case EstadoEncomiendaEnum.ImpuestoCallCenter: return "Impuesto Call Center";
                case EstadoEncomiendaEnum.ImpuestoAgencia: return "Impuesto Agencia";
                case EstadoEncomiendaEnum.EnCaminoARetirarDomicilio: return "En camino a retirar (Domicilio)";
                case EstadoEncomiendaEnum.EnCaminoARetirarAgencia: return "En camino a retirar (Agencia)";
                case EstadoEncomiendaEnum.PendienteDeRetiroEnAgencia: return "Pendiente de retiro en agencia";
                case EstadoEncomiendaEnum.AdmitidoCDOrigen: return "Admitido en CD Origen";
                case EstadoEncomiendaEnum.EnTransito: return "En tránsito";
                case EstadoEncomiendaEnum.AdmitidoCDDestino: return "Admitido en CD Destino";
                case EstadoEncomiendaEnum.DistribucionUltimaMillaDomicilio: return "Distribución última milla (domicilio)";
                case EstadoEncomiendaEnum.DistribucionUltimaMillaAgencia: return "Distribución última milla (agencia)";
                case EstadoEncomiendaEnum.PrimerIntentoDeEntrega: return "Primer intento de entrega";
                case EstadoEncomiendaEnum.Rechazado: return "Rechazado";
                case EstadoEncomiendaEnum.Entregado: return "Entregado";
                default: return estado.ToString();
            }
        }
    }
}
