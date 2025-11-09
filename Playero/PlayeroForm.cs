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
        // 1. Declarar el modelo sin inicializarlo aquí.
        private readonly PlayeroModelo modelo;

        private List<GuiaEntidad> _guiasCargaDisponibles;
        private List<GuiaEntidad> _guiasDescargaDisponibles;

        // 2. Constructor principal: Acepta el ID del CD
        public PlayeroForm(int cdSeleccionado)
        {
            InitializeComponent();
            this.modelo = new PlayeroModelo(cdSeleccionado);

            CargaListView.MultiSelect = true;
            DescargarListView.MultiSelect = true;
            CargaListView.FullRowSelect = true;
            DescargarListView.FullRowSelect = true;

            CargaListView.CheckBoxes = true;
            DescargarListView.CheckBoxes = true;
            // Asociar los eventos aquí si el diseñador no lo hace
            this.BuscarGuiasAsociadasBttn.Click += new System.EventHandler(this.BuscarGuiasAsociadasBttn_Click);
            this.AceptarBttn.Click += new System.EventHandler(this.AceptarBttn_Click);
        }

        // 4. Constructor para el diseñador (sin parámetros): 
        // ¡Único constructor sin parámetros! Llama al constructor principal con un ID seguro (0).
        public PlayeroForm() : this(0)
        {
            // La lógica de InitializeComponent() y asociación de eventos
            // ahora ocurre en el constructor de arriba que recibe el CDID.
        }

        // El resto de los métodos se mantiene igual:
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

            _guiasCargaDisponibles = resultado.Cargas;
            _guiasDescargaDisponibles = resultado.Descargas;

            // 3. Limpiar y poblar los ListViews
            PoblarListView(CargaListView, _guiasCargaDisponibles);
            PoblarListView(DescargarListView, _guiasDescargaDisponibles);

            if (!resultado.Cargas.Any() && !resultado.Descargas.Any())
            {
                MessageBox.Show("No se encontraron guías pendientes para la patente ingresada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AceptarBttn_Click(object sender, EventArgs e)
        {
            // 1. Obtener los NÚMEROS DE GUÍA MARCADOS (Checked) de cada ListView
            var numerosCargaSeleccionados = CargaListView.Items.Cast<ListViewItem>()
                // 🚨 CAMBIO CLAVE: Usar .Checked en lugar de .SelectedItems
                .Where(item => item.Checked)
                .Select(item => item.SubItems[0].Text) // El N° Guía está en la columna 0
                .ToList();

            var numerosDescargaSeleccionados = DescargarListView.Items.Cast<ListViewItem>()
                // 🚨 CAMBIO CLAVE: Usar .Checked en lugar de .SelectedItems
                .Where(item => item.Checked)
                .Select(item => item.SubItems[0].Text)
                .ToList();

            // 2. Verificar si se marcó algo
            if (!numerosCargaSeleccionados.Any() && !numerosDescargaSeleccionados.Any())
            {
                MessageBox.Show("Debe MARCAR al menos una guía para confirmar la operación.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Obtener las ENTIDADES COMPLETAS a partir de los números marcados
            var cargasAProcesar = _guiasCargaDisponibles
                .Where(g => numerosCargaSeleccionados.Contains(g.NumeroGuia))
                .ToList();

            var descargasAProcesar = _guiasDescargaDisponibles
                .Where(g => numerosDescargaSeleccionados.Contains(g.NumeroGuia))
                .ToList();

            // 4. Llamar al modelo para confirmar la operación
            try
            {
                var resultadoDistribucion = modelo.ConfirmarOperacion(cargasAProcesar, descargasAProcesar);

                MessageBox.Show("Operación Exitosa. Estados de guías actualizados y Hoja(s) de Ruta de Distribución creada(s).", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 5. Limpiar y refrescar la pantalla
                LimpiarFormulario();

            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error de Operación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
                    guia.ClienteCUIT,
                    guia.CDOrigenID.ToString(),
                    guia.CDDestinoID.ToString()
                };
                var item = new ListViewItem(row);
                listView.Items.Add(item);
            }
        }

        private void LimpiarFormulario()
        {
            PatenteTxt.Clear();
            CargaListView.Items.Clear();
            DescargarListView.Items.Clear();
            PatenteTxt.Focus();
        }
    }
}
