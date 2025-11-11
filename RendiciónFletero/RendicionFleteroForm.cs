using CAIGrupoG.Modelos;
using CAIGrupoG.RendiciónFletero;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CAIGrupoG.Admisión
{
    public partial class RendicionFleteroForm : Form
    {
        private readonly RendicionFleteroModelo modelo = new();

        public RendicionFleteroForm()
        {
            InitializeComponent();
            // Asociar los eventos a los manejadores de eventos correctos
            this.BuscarGuiasButton.Click += new System.EventHandler(this.BuscarGuiasButton_Click);
            this.AceptarButton.Click += new System.EventHandler(this.AceptarButton_Click);
        }

        private void BuscarGuiasButton_Click(object sender, EventArgs e)
        {
            string dni = DNIText.Text.Trim();

            // 1. Validación del DNI
            if (!ValidarDNI(dni))
            {
                MessageBox.Show("El formato del DNI no es válido. Debe contener entre 7 y 8 dígitos numéricos.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // acá tengo que llamar al modelo para buscar las guías de admsión y retiro

            var guíasAdmisionActuales = modelo.BuscarGuiasPorDNI(dni).Admision;
            var guiasRetiroActuales = modelo.BuscarGuiasPorDNI(dni).Retiro;

            // 2. Llamada al modelo para buscar las guías
            var resultado = modelo.BuscarGuiasPorDNI(dni);

            guíasAdmisionActuales = resultado.Admision;
            guiasRetiroActuales = resultado.Retiro;

            // 3. Poblar los ListViews
            PoblarListView(AdmisionListView, guíasAdmisionActuales);
            PoblarListView(RetiroListView, guiasRetiroActuales);

            if (guíasAdmisionActuales.Count == 0 && guiasRetiroActuales.Count == 0)
            {
                MessageBox.Show("No se encontraron guías asociadas al DNI del fletero.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AceptarButton_Click(object sender, EventArgs e)
        {
            string dni = DNIText.Text.Trim();

            var guíasAdmisionActuales = modelo.BuscarGuiasPorDNI(dni).Admision;
            var guiasRetiroActuales = modelo.BuscarGuiasPorDNI(dni).Retiro;

            if ((guíasAdmisionActuales == null || guíasAdmisionActuales.Count == 0) &&
                (guiasRetiroActuales == null || guiasRetiroActuales.Count == 0))
            {
                MessageBox.Show("No hay guías para procesar. Realice una búsqueda primero.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<Guia> admisiconesSeleccionadas = modelo.ObtenerGuiasSeleccionadas(AdmisionListView, guíasAdmisionActuales);
            List<Guia> retirosSeleccionados = modelo.ObtenerGuiasSeleccionadas(RetiroListView, guiasRetiroActuales);

            modelo.Rendir(admisiconesSeleccionadas, retirosSeleccionados);

            MessageBox.Show("Operación Exitosa.", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Limpiar la pantalla para la siguiente operación
            LimpiarFormulario();
        }

        /// Valida que el DNI ingresado tenga un formato correcto (7 u 8 dígitos).
        private bool ValidarDNI(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                return false;

            // Expresión regular para DNI argentino (7 u 8 dígitos)
            return Regex.IsMatch(dni, @"^\d{7,8}$");
        }

        /// Rellena un control ListView con la lista de guías correspondiente.
        private void PoblarListView(System.Windows.Forms.ListView listView, List<Guia> guias)
        {
            listView.Items.Clear();
            if (guias == null) return;

            foreach (var guia in guias)
            {
                var row = new string[]
                {
                    guia.NumeroGuia,
                    FormatearEstado(guia.Estado),
                    guia.TipoPaquete.ToString(),
                    guia.CUIT,
                    guia.DniAutorizadoRetirar,
                    guia.Destino
                };
                var item = new System.Windows.Forms.ListViewItem(row);
                listView.Items.Add(item);
            }
        }

        /// Convierte un valor del enum EstadoEncomienda a un string legible.
        private string FormatearEstado(EstadoEncomienda estado)
        {
            switch (estado)
            {
                case EstadoEncomienda.DistribucionUltimaMillaAgencia:
                    return "Distribución última milla - Agencia";
                case EstadoEncomienda.DistribucionUltimaMillaDomicilio:
                    return "Distribución última milla - Domicilio";
                case EstadoEncomienda.PrimerIntentoDeEntrega:
                    return "Primer Intento de Entrega";
                case EstadoEncomienda.EnCaminoARetirarDomicilio:
                    return "En camino a retirar (Dom)";
                case EstadoEncomienda.EnCaminoARetirarAgencia:
                    return "En camino a retirar (Age)";
                case EstadoEncomienda.AdmitidoCDDestino:
                    return "En CD destino";
                default:
                    return estado.ToString();
            }
        }

        /// Limpia todos los controles del formulario.
        private void LimpiarFormulario()
        {
            DNIText.Clear();
            AdmisionListView.Items.Clear();
            RetiroListView.Items.Clear();
            DNIText.Focus();
        }
    }
}
