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
        public ImposicionDeEncomiendaCDModelo modelo = new();

        public ImposicionDeEncomiendaCDForm()
        {
            InitializeComponent();

            // 1. OCULTAR/INICIALIZAR 
            GuiasAsignadasGroupBox.Visible = false;
            DomicilioTxtBox.Enabled = false;
            OpcionesDeEntregaCmb.Enabled = false;
            //Deshabilitar el groupbox de encomienda al iniciar
            groupBox2.Enabled = false;

            CargarDatosIniciales();

            // 3. ENLACE DE EVENTOS
            this.DomicilioRadioBttn.CheckedChanged += new EventHandler(this.DomicilioRadioBttn_CheckedChanged);
            this.AgenciaRadioBttn.CheckedChanged += new EventHandler(this.AgenciaRadioBttn_CheckedChanged);
            this.CiudadCmb.SelectedIndexChanged += new EventHandler(this.CiudadCmb_SelectedIndexChanged);
            this.BuscarClienteBttn.Click += new EventHandler(this.BuscarClienteBttn_Click);
            this.AñadirBttn.Click += new EventHandler(this.AñadirBttn_Click);
            this.QuitarBttn.Click += new EventHandler(this.QuitarBttn_Click);
            this.ConfirmarBttn.Click += new EventHandler(this.ConfirmarBttn_Click);
            this.FinalizaBttn.Click += new EventHandler(this.FinalizaBttn_Click);
            this.CancelarBttn.Click += new EventHandler(this.CancelarBttn_Click);


            this.GuiasAsignadasGroupBox.Location = new Point(79, 58);
        }

        // -------------------------------------------------------------------------
        // MÉTODOS DE CARGA DE DATOS Y FLUJO
        // -------------------------------------------------------------------------

        private void CargarDatosIniciales()
        {
            CiudadCmb.DataSource = modelo.ObtenerCiudades();
            CiudadCmb.DisplayMember = "Nombre";
            CiudadCmb.ValueMember = "Id";
            CiudadCmb.SelectedIndex = -1;

            TipoDeCajaCmb.DataSource = modelo.ObtenerTiposCaja();
            TipoDeCajaCmb.DisplayMember = "Nombre";
            TipoDeCajaCmb.ValueMember = "Nombre";
            TipoDeCajaCmb.SelectedIndex = -1;
        }

        private void CiudadCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            OpcionesDeEntregaCmb.DataSource = null;
            OpcionesDeEntregaCmb.SelectedIndex = -1;

            if (AgenciaRadioBttn.Checked)
            {
                AgenciaRadioBttn_CheckedChanged(sender, e);
            }
        }

        private void DomicilioRadioBttn_CheckedChanged(object sender, EventArgs e)
        {
            if (DomicilioRadioBttn.Checked)
            {
                DomicilioTxtBox.Enabled = true;
                OpcionesDeEntregaCmb.Enabled = false;
                OpcionesDeEntregaCmb.DataSource = null;
            }
        }

        private void AgenciaRadioBttn_CheckedChanged(object sender, EventArgs e)
        {
            if (AgenciaRadioBttn.Checked)
            {
                // Solo ejecutamos la lógica de carga de agencias si está MARCADO
                if (CiudadCmb.SelectedItem is Ciudad ciudadSeleccionada)
                {
                    DomicilioTxtBox.Enabled = false;
                    DomicilioTxtBox.Clear();
                    OpcionesDeEntregaCmb.Enabled = true;

                    //Asumiendo que Ciudad es una clase con propiedad 'Id' (Int32)
                    var agenciasFiltradas = modelo.ObtenerAgenciasPorCiudad(ciudadSeleccionada.Id);

                    OpcionesDeEntregaCmb.DataSource = agenciasFiltradas;
                    OpcionesDeEntregaCmb.DisplayMember = "Nombre";
                    OpcionesDeEntregaCmb.ValueMember = "Id";
                    OpcionesDeEntregaCmb.SelectedIndex = -1;
                }
                else
                {
                    // Si el usuario MARCA Agencia y no hay Ciudad, mostramos la advertencia y desmarcamos.
                    OpcionesDeEntregaCmb.DataSource = null;
                    OpcionesDeEntregaCmb.Enabled = false;
                    MessageBox.Show("Primero debe seleccionar una Ciudad.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    AgenciaRadioBttn.Checked = false; // Desmarcar para forzar al usuario a seleccionar Ciudad primero.
                }
            }
            else
            {
                // Cuando se DESMARCA (ya sea porque el usuario marcó Domicilio o por limpieza en Finalizar):
                // Simplemente limpiamos los controles sin mostrar advertencias.
                OpcionesDeEntregaCmb.DataSource = null;
                OpcionesDeEntregaCmb.Enabled = false;

                // NOTA: No hacemos nada más para evitar que se ejecute en Finalizar.
            }
        }


        // -------------------------------------------------------------------------
        // LÓGICA DE BOTONES (CRUD Encomiendas)
        // -------------------------------------------------------------------------

        // Logica del Cliente
        private void BuscarClienteBttn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CuitTxtBox.Text) || !long.TryParse(CuitTxtBox.Text, out _) || CuitTxtBox.Text.Length != 11)
            {
                MessageBox.Show("El CUIT debe ser numérico y contener exactamente 11 dígitos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (modelo.BuscarCliente(CuitTxtBox.Text.Trim()))
            {
                MessageBox.Show("Cliente encontrado. Procede a ingresar la encomienda.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Encontrar cliente: Habilita el siguiente paso y deshabilita la búsqueda
                groupBox2.Enabled = true;
                CuitTxtBox.Enabled = false; // Deshabilita el CUIT
                BuscarClienteBttn.Enabled = false; // Deshabilita el botón de búsqueda
            }
            else
            {
                // No encuentra cliente: Deshabilita el siguiente paso
                MessageBox.Show("CUIT no registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                groupBox2.Enabled = false;
                CuitTxtBox.Enabled = true; // Asegura que pueda reintentar
                BuscarClienteBttn.Enabled = true;
            }
        }

        // Añade una encomienda al ListView
        private void AñadirBttn_Click(object sender, EventArgs e)
        {
            string dni = DNITxtBox.Text.Trim();

            // Validaciones (Ajustadas a lo esencial)
            if (CiudadCmb.SelectedIndex == -1 || TipoDeCajaCmb.SelectedIndex == -1 ||
                (!AgenciaRadioBttn.Checked && !DomicilioRadioBttn.Checked) || CantidadNum.Value < 1)
            {
                MessageBox.Show("Debe completar todos los campos de destino y encomienda.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(dni, out _) || dni.Length < 7 || dni.Length > 8)
            {
                MessageBox.Show("El DNI debe ser numérico y tener entre 7 y 8 dígitos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
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

            // Determinación del destino
            string tipoDestino;
            string destino;
            if (DomicilioRadioBttn.Checked)
            {
                tipoDestino = "Domicilio";
                destino = DomicilioTxtBox.Text.Trim();
            }
            else
            {
                tipoDestino = "Agencia/CD";
                destino = OpcionesDeEntregaCmb.Text;
            }

            // Se crea el ítem (asumiendo que hay suficientes columnas definidas en EncomiendasListView)
            var item = new ListViewItem(new[]
            {
                TipoDeCajaCmb.Text,          // Col 1: Tipo de Caja
                CantidadNum.Value.ToString(),    // Col 2: Cantidad 
                CiudadCmb.Text,                  // Col 3: Ciudad
                tipoDestino,                    // Col 4: Tipo de Destino
                destino,                        // Col 5: Detalle del Destino
                dni                              // Col 6: DNI Autorizado
            });

            EncomiendasListView.Items.Add(item);

            MessageBox.Show($"Se añadió una caja tipo {TipoDeCajaCmb.Text} a la lista.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Limpieza (opcional)
            TipoDeCajaCmb.SelectedIndex = -1;
            CantidadNum.Value = 1;
            DNITxtBox.Clear();
        }


        //Remueve la línea seleccionada del ListView
        private void QuitarBttn_Click(object sender, EventArgs e)
        {
            if (EncomiendasListView.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in EncomiendasListView.SelectedItems)
                {
                    EncomiendasListView.Items.Remove(item);
                }
                MessageBox.Show("Línea(s) de encomienda eliminada(s).", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Debe seleccionar una línea para quitar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ConfirmarBttn_Click(object sender, EventArgs e)
        {
            if (EncomiendasListView.Items.Count == 0)
            {
                MessageBox.Show("Debe añadir al menos una encomienda a la lista antes de confirmar/admitir.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Calcular la cantidad total de encomiendas sumando la columna 'Cantidad'
            int totalEncomiendas = 0;

            // Recorremos todos los ítems del ListView de Encomiendas
            foreach (ListViewItem item in EncomiendasListView.Items)
            {
                // La cantidad está en el SubItem[1] (el SubItem[0] es el Tipo de Caja)
                if (int.TryParse(item.SubItems[1].Text, out int cantidadPorLinea))
                {
                    totalEncomiendas += cantidadPorLinea;
                }
                else
                {
                    // Esto es una validación extra por si los datos fueran inconsistentes
                    MessageBox.Show("Error de formato en la columna Cantidad del listado. Por favor, revise la información.", "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Si la suma total es 0, no continuamos.
            if (totalEncomiendas == 0)
            {
                MessageBox.Show("La cantidad total de encomiendas debe ser mayor a cero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // Llama al modelo para obtener una guía ficticia por el TOTAL de encomiendas
            List<string> guiasGeneradas = modelo.ConfirmarAdmision(totalEncomiendas);

            // 1. Llenar el ListView de guías generadas 
            GuiasGeneradasListView.Items.Clear();

            foreach (string guia in guiasGeneradas)
            {
                // Aseguramos que se añade correctamente como un elemento para el ListView
                GuiasGeneradasListView.Items.Add(new ListViewItem(guia));
            }

            // 2. Control de visibilidad para mostrar SOLO los resultados

            // Oculta los GroupBox principales
            groupBox1.Visible = false; // Datos del Cliente
            groupBox2.Visible = false; // Datos de la encomienda

            // Muestra el GroupBox de resultados
            GuiasAsignadasGroupBox.Visible = true;

            // Opcional: Asegúrate de que el formulario redibuje correctamente
            this.Refresh();
        }

        //Vuelve al formulario para seguir operando (Botón 'Finalizar' en la pantalla de resultados).
        private void FinalizaBttn_Click(object sender, EventArgs e)
        {
            // Lógica para limpiar todos los campos del formulario
            CuitTxtBox.Clear();
            DomicilioRadioBttn.Checked = false;
            AgenciaRadioBttn.Checked = false;
            CiudadCmb.SelectedIndex = -1;
            DomicilioTxtBox.Clear();
            OpcionesDeEntregaCmb.DataSource = null;
            TipoDeCajaCmb.SelectedIndex = -1;
            CantidadNum.Value = 1;
            DNITxtBox.Clear();
            EncomiendasListView.Items.Clear();
            GuiasGeneradasListView.Items.Clear();

            //Restaura la visibilidad
            GuiasAsignadasGroupBox.Visible = false;
            groupBox1.Visible = true;
            groupBox2.Visible = true;
            groupBox2.Enabled = false; // Deshabilita hasta una nueva búsqueda de cliente

            //Restaurar la habilidad de buscar cliente
            CuitTxtBox.Enabled = true;
            BuscarClienteBttn.Enabled = true;
        }

        //Cierra el formulario (Botón 'Cancelar' en la pantalla principal).
        private void CancelarBttn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}