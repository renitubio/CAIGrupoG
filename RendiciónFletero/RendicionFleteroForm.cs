using CAIGrupoG.Modelos;
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
            var guiasAdmisionPresentacion = modelo.ObtenerGuiasPresentacion(resultado.Admision);
            var guiasRetiroPresentacion = modelo.ObtenerGuiasPresentacion(resultado.Retiro);

            // 3. Poblar los ListViews
            PoblarListView(AdmisionListView, guiasAdmisionPresentacion);
            PoblarListView(RetiroListView, guiasRetiroPresentacion);

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

            // Aquí iría la lógica para procesar las guías seleccionadas (opcional según requerimiento)
            List<string> admisionesSeleccionadas = this.AdmisionListView.CheckedItems
                                                       .Cast<ListViewItem>()
                                                       .Select(l => l.Text)
                                                       .ToList(); //Talvez funciona.

            List<string> retirosSeleccionados = this.RetiroListView.CheckedItems
                                                    .Cast<ListViewItem>()
                                                    .Select(l => l.Text)
                                                    .ToList(); //Talvez funciona.

            modelo.Rendir(admisionesSeleccionadas, retirosSeleccionados);


            MessageBox.Show("Operación Exitosa.", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // 4. Limpiar y refrescar la interfaz
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
        private void PoblarListView(ListView listView, List<GuiaPresentacionDTO> guias)
        {
            listView.Items.Clear();
            if (guias == null) return;

            foreach (var guia in guias)
            {
                var row = new string[]
                {
                    guia.NumeroGuia,
                    guia.EstadoDescripcion,
                    guia.TipoPaquete,
                    guia.CUIT,
                    guia.DniAutorizadoRetirar,
                    guia.Destino
                };
                var item = new ListViewItem(row);
                listView.Items.Add(item);
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
