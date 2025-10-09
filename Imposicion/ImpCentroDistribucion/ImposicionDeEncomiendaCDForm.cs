using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAIGrupoG.Imposicion.ImpCentroDistribucion
{
    public partial class ImposicionDeEncomiendaCDForm : Form
    {
        public ImposicionDeEncomiendaCDForm()
        {
            InitializeComponent();

            // 1. OCULTAR: Esconde el GroupBox de guías generadas al inicio.
            GuiasAsignadasGroupBox.Visible = false;

            // 2. INICIALIZACIÓN: Deshabilita los campos de destino específicos (se habilitan con los RadioButtons).
            DomicilioTxtBox.Enabled = false;
            OpcionesDeEntregaCmb.Enabled = false;

            // 3. ENLACE DE EVENTOS: Asegura que los eventos de los botones de radio estén asignados
            this.DomicilioRadioBttn.CheckedChanged += new EventHandler(this.DomicilioRadioBttn_CheckedChanged);
            this.AgenciaRadioBttn.CheckedChanged += new EventHandler(this.AgenciaRadioBttn_CheckedChanged);
            this.BuscarClienteBttn.Click += new EventHandler(this.BuscarClienteBttn_Click);
            this.AñadirBttn.Click += new EventHandler(this.AñadirBttn_Click);
            this.ConfirmarBttn.Click += new EventHandler(this.ConfirmarBttn_Click);
            this.FinalizaBttn.Click += new EventHandler(this.FinalizaBttn_Click);
        }

        // -------------------------------------------------------------------------
        // LÓGICA DE RADIO BUTTONS
        // -------------------------------------------------------------------------

        // Maneja la selección de Domicilio.
        private void DomicilioRadioBttn_CheckedChanged(object sender, EventArgs e)
        {
            if (DomicilioRadioBttn.Checked)
            {
                DomicilioTxtBox.Enabled = true;
                OpcionesDeEntregaCmb.Enabled = false;
                OpcionesDeEntregaCmb.SelectedIndex = -1; // Limpia la selección de agencia.
            }
        }

        // Maneja la selección de Agencia/CD.
        private void AgenciaRadioBttn_CheckedChanged(object sender, EventArgs e)
        {
            if (AgenciaRadioBttn.Checked)
            {
                DomicilioTxtBox.Enabled = false;
                DomicilioTxtBox.Clear(); // Limpia el domicilio.
                OpcionesDeEntregaCmb.Enabled = true;
                // Lógica pendiente: Cargar OpcionesDeEntregaCmb con agencias/CD de la CiudadCmb.
            }
        }

        // -------------------------------------------------------------------------
        // LÓGICA DE BOTONES (Validaciones)
        // -------------------------------------------------------------------------

        // Valida y busca el cliente por CUIT.
        private void BuscarClienteBttn_Click(object sender, EventArgs e)
        {
            // VALIDACIÓN: Campo no vacío
            if (string.IsNullOrEmpty(CuitTxtBox.Text))
            {
                MessageBox.Show("El CUIT no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string cuit = CuitTxtBox.Text.Trim();

            // VALIDACIÓN: Tipo y rango (debe ser numérico de 11 dígitos)
            if (!long.TryParse(cuit, out _) || cuit.Length != 11)
            {
                MessageBox.Show("El CUIT debe ser numérico y contener exactamente 11 dígitos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // *** SIMULACIÓN DE LÓGICA DE NEGOCIO (PASOS 3, 4) ***
            // Aquí iría la llamada a la capa de negocio para buscar el cliente.
            bool clienteEncontrado = true;

            if (clienteEncontrado)
            {
                MessageBox.Show("Cliente encontrado. Procede a ingresar la encomienda.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Habilitar la sección de encomienda después de la búsqueda exitosa.
                groupBox2.Enabled = true;
            }
            else
            {
                MessageBox.Show("CUIT no registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                groupBox2.Enabled = false;
            }
        }

        // Valida los datos de la encomienda y los añade al ListView.
        private void AñadirBttn_Click(object sender, EventArgs e)
        {
            // VALIDACIÓN: Ciudad y Tipo de Caja no seleccionados
            if (CiudadCmb.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una Ciudad de destino.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (TipoDeCajaCmb.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar el Tipo de Caja.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // VALIDACIÓN: Opciones de Destino (Radio Buttons)
            if (!AgenciaRadioBttn.Checked && !DomicilioRadioBttn.Checked)
            {
                MessageBox.Show("Debe seleccionar si la entrega es a Agencia/CD o a Domicilio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // VALIDACIÓN: Domicilio o Agencia específico (Rango y no vacío)
            if (AgenciaRadioBttn.Checked && OpcionesDeEntregaCmb.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar la Agencia/CD de destino.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (DomicilioRadioBttn.Checked && string.IsNullOrEmpty(DomicilioTxtBox.Text))
            {
                MessageBox.Show("Debe ingresar la dirección del Domicilio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // VALIDACIÓN: Cantidad (Rango)
            if (CantidadNum.Value < 1)
            {
                MessageBox.Show("La cantidad de cajas debe ser al menos 1.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // VALIDACIÓN: DNI autorizado (No vacío, Tipo y Rango)
            if (string.IsNullOrEmpty(DNITxtBox.Text))
            {
                MessageBox.Show("El DNI autorizado no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string dni = DNITxtBox.Text.Trim();
            // Asume DNI de 7 a 8 dígitos.
            if (!int.TryParse(dni, out _) || dni.Length < 7 || dni.Length > 8)
            {
                MessageBox.Show("El DNI debe ser numérico y tener entre 7 y 8 dígitos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // *** LÓGICA DE NEGOCIO (PASO 10 del CU) ***
            // Lógica para añadir el item al EncomiendasListView
            // EncomiendasListView.Items.Add(new ListViewItem(new[] { TipoDeCajaCmb.Text, CantidadNum.Value.ToString(), "CostoCalculado" }));

            MessageBox.Show("Encomienda(s) añadida(s) a la lista.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Confirma la imposición, genera guías y muestra el GroupBox de resultados.
        private void ConfirmarBttn_Click(object sender, EventArgs e)
        {
            // VALIDACIÓN FINAL: La lista de encomiendas no puede estar vacía.
            if (EncomiendasListView.Items.Count == 0)
            {
                MessageBox.Show("Debe añadir al menos una encomienda a la lista antes de confirmar/admitir.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // *** LÓGICA DE NEGOCIO (PASOS 16 y 17) ***
            // Lógica para generar las guías, registrar la Admisión en la DB y llenar GuiasAsignadasListView.
            // Ejemplo:
            // GuiasAsignadasListView.Items.Add("TRK-123456789");
            // GuiasAsignadasListView.Items.Add("TRK-987654321");

            // OCULTAR/MOSTRAR: Oculta la interfaz principal.
            groupBox1.Visible = false;
            groupBox2.Visible = false;

            // OCULTAR/MOSTRAR: Muestra el GroupBox con las guías generadas.
            GuiasAsignadasGroupBox.Visible = true;
        }

        // Finaliza el proceso y limpia el formulario.
        private void FinalizaBttn_Click(object sender, EventArgs e)
        {
            // Lógica para limpiar todos los campos del formulario
            CuitTxtBox.Clear();
            CiudadCmb.SelectedIndex = -1;
            DomicilioRadioBttn.Checked = false;
            AgenciaRadioBttn.Checked = false;
            DomicilioTxtBox.Clear();
            OpcionesDeEntregaCmb.SelectedIndex = -1;
            TipoDeCajaCmb.SelectedIndex = -1;
            CantidadNum.Value = 1;
            DNITxtBox.Clear();
            EncomiendasListView.Items.Clear();
            GuiasAsignadasListView.Items.Clear();

            // Restaura la visibilidad de los GroupBox
            GuiasAsignadasGroupBox.Visible = false;
            groupBox1.Visible = true;
            groupBox2.Visible = true;
            groupBox2.Enabled = false; // Deshabilita hasta una nueva búsqueda de cliente
        }
    }
}