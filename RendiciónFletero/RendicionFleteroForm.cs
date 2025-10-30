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


            // Obtener las guías seleccionadas en ambos ListView
            var guiasAdmSeleccion = ObtenerGuiasSeleccionadas(AdmisionListView, guíasAdmisionActuales);
            var guiasRetSeleccion = ObtenerGuiasSeleccionadas(RetiroListView, guiasRetiroActuales);

            if ((guiasAdmSeleccion == null || guiasAdmSeleccion.Count == 0) &&
                (guiasRetSeleccion == null || guiasRetSeleccion.Count == 0))
            {
                MessageBox.Show("No ha seleccionado guías. Seleccione al menos una guía para continuar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Pasar las selecciones al modelo para que actualice los estados.
            if ((guiasAdmSeleccion != null && guiasAdmSeleccion.Count > 0) && (guiasRetSeleccion != null && guiasRetSeleccion.Count > 0))
            {
                modelo.CambioEstadoGuiasSelecc(guiasAdmSeleccion, guiasRetSeleccion);
            }


            // Refrescar la vista volviendo a consultar las guías y poblando los ListViews
            var resultado = modelo.BuscarGuiasPorDNI(dni);
            PoblarListView(AdmisionListView, resultado.Admision);
            PoblarListView(RetiroListView, resultado.Retiro);

            MessageBox.Show("Operación Exitosa.", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Limpiar la pantalla para la siguiente operación
            LimpiarFormulario();
        }

        /// <summary>
        /// Recupera las guías seleccionadas en un ListView comparando por Número de Guía con la lista fuente.
        /// </summary>
        private List<Guia> ObtenerGuiasSeleccionadas(ListView listView, List<Guia> fuenteGuias)
        {
            var seleccion = new List<Guia>();
            if (listView == null || fuenteGuias == null) return seleccion;

            // Evitar duplicados por número de guía
            var agregados = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // 1) Elementos seleccionados (SelectedItems)
            foreach (ListViewItem item in listView.SelectedItems)
            {
                if (item.Tag is Guia guiaFromTag)
                {
                    if (!string.IsNullOrWhiteSpace(guiaFromTag.NumeroGuia) && agregados.Add(guiaFromTag.NumeroGuia.Trim()))
                        seleccion.Add(guiaFromTag);
                    continue;
                }

                var numeroGuia = item.Text?.Trim();
                if (string.IsNullOrEmpty(numeroGuia)) continue;

                var guia = fuenteGuias.FirstOrDefault(g => string.Equals(g.NumeroGuia?.Trim(), numeroGuia, StringComparison.OrdinalIgnoreCase));
                if (guia != null && agregados.Add(guia.NumeroGuia?.Trim() ?? string.Empty))
                    seleccion.Add(guia);
            }

            // 2) Elementos marcados (CheckedItems) — útil si el ListView tiene CheckBoxes = true
            foreach (ListViewItem item in listView.CheckedItems)
            {
                // Si ya fue añadido en selectedItems, el HashSet lo evita
                if (item.Tag is Guia guiaFromTag)
                {
                    if (!string.IsNullOrWhiteSpace(guiaFromTag.NumeroGuia) && agregados.Add(guiaFromTag.NumeroGuia.Trim()))
                        seleccion.Add(guiaFromTag);
                    continue;
                }

                var numeroGuia = item.Text?.Trim();
                if (string.IsNullOrEmpty(numeroGuia)) continue;

                var guia = fuenteGuias.FirstOrDefault(g => string.Equals(g.NumeroGuia?.Trim(), numeroGuia, StringComparison.OrdinalIgnoreCase));
                if (guia != null && agregados.Add(guia.NumeroGuia?.Trim() ?? string.Empty))
                    seleccion.Add(guia);
            }

            return seleccion;
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
                var item = new ListViewItem(row)
                {
                    Tag = guia,       // <-- asociamos el objeto Guia para recuperarlo directamente
                    Checked = false   // opcional: inicializar como no marcado
                };
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
                case EstadoEncomienda.AdmitidoEnCDDestino:
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
            DNIText.Focus();
        }
    }
}
