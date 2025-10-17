using CAIGrupoG.RendiciónFletero;
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

namespace CAIGrupoG.Admisión
{
    public partial class RendicionFleteroForm : Form
    {
        private readonly RendicionFleteroModelo modelo = new();

        //TODO: Estas dos variables se tienen que ir. Pedirle los datos al modelo en donde se necesiten, las veces q haga falta.
        private List<Guia> guiasAdmisionActuales;
        private List<Guia> guiasRetiroActuales;

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

            // 2. Llamada al modelo para buscar las guías
            var resultado = modelo.BuscarGuiasPorDNI(dni);
            guiasAdmisionActuales = resultado.Admision;
            guiasRetiroActuales = resultado.Retiro;

            // 3. Poblar los ListViews
            PoblarListView(AdmisionListView, guiasAdmisionActuales);
            PoblarListView(RetiroListView, guiasRetiroActuales);

            if (guiasAdmisionActuales.Count == 0 && guiasRetiroActuales.Count == 0)
            {
                MessageBox.Show("No se encontraron guías asociadas al DNI del fletero.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AceptarButton_Click(object sender, EventArgs e)
        {
            if ((guiasAdmisionActuales == null || guiasAdmisionActuales.Count == 0) &&
                (guiasRetiroActuales == null || guiasRetiroActuales.Count == 0))
            {
                MessageBox.Show("No hay guías para procesar. Realice una búsqueda primero.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Aquí iría la lógica para procesar las guías seleccionadas (opcional según requerimiento)

            MessageBox.Show("Operación Exitosa.", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Limpiar la pantalla para la siguiente operación
            LimpiarFormulario();
        }

        /// <summary>
        /// Valida que el DNI ingresado tenga un formato correcto (7 u 8 dígitos).
        /// </summary>
        private bool ValidarDNI(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                return false;

            // Expresión regular para DNI argentino (7 u 8 dígitos)
            return Regex.IsMatch(dni, @"^\d{7,8}$");
        }

        /// <summary>
        /// Rellena un control ListView con la lista de guías correspondiente.
        /// </summary>
        private void PoblarListView(ListView listView, List<Guia> guias)
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
                var item = new ListViewItem(row);
                listView.Items.Add(item);
            }
        }

        /// <summary>
        /// Convierte un valor del enum EstadoEncomienda a un string legible.
        /// </summary>
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
                case EstadoEncomienda.EnCDDestino:
                    return "En CD destino";
                default:
                    return estado.ToString();
            }
        }

        /// <summary>
        /// Limpia todos los controles del formulario.
        /// </summary>
        private void LimpiarFormulario()
        {
            DNIText.Clear();
            AdmisionListView.Items.Clear();
            RetiroListView.Items.Clear();
            guiasAdmisionActuales = null;
            guiasRetiroActuales = null;
            DNIText.Focus();
        }
    }
}
