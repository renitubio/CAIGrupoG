using CAIGrupoG.Imposicion.ImpAgencia;
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

namespace CAIGrupoG.Imposicion.ImpCallCenter
{
    public partial class ImposicionDeEncomiendaCallCenterForm : Form
    {
        public ImposicionDeEncomiendaCallCenterModelo modelo = new();

        public ImposicionDeEncomiendaCallCenterForm()
        {
            InitializeComponent();
        }

        private void ImposicionCallCenterForm_Load(object sender, EventArgs e)
        {
            // 1. OCULTAR/INICIALIZAR
            GuiasGeneradasGpBox.Visible = false;
            DomicilioText.Enabled = false;
            EntregaCmb.Enabled = false;
            DatosEncomiendaGpBox.Enabled = false;

            CargarDatosIniciales();

            // 2. ENLACE DE EVENTOS
            this.BuscarBttn.Click += new EventHandler(this.BuscarBttn_Click);
            this.CiudadCmb.SelectedIndexChanged += new EventHandler(this.CiudadCmb_SelectedIndexChanged);
            this.AgenciaRdBttn.CheckedChanged += new EventHandler(this.TipoEntrega_CheckedChanged);
            this.DomicilioRdBttn.CheckedChanged += new EventHandler(this.TipoEntrega_CheckedChanged);
            this.AñadirBttn.Click += new EventHandler(this.AñadirBttn_Click);
            this.QuitarBttn.Click += new EventHandler(this.QuitarBttn_Click);
            this.ConfirmarBttn.Click += new EventHandler(this.ConfirmarBttn_Click);
            this.CancelarBttn.Click += new EventHandler(this.CancelarBttn_Click);
            this.button3.Click += new EventHandler(this.Finalizar_Click);

            this.GuiasGeneradasGpBox.Location = new Point(12, 11);
        }

        private void CargarDatosIniciales()
        {
            CiudadCmb.DataSource = modelo.ObtenerCiudades();
            CiudadCmb.DisplayMember = "Nombre";
            CiudadCmb.ValueMember = "Id";
            CiudadCmb.SelectedIndex = -1;

            TipoCajaCmb.DataSource = modelo.ObtenerTiposCaja();
            TipoCajaCmb.DisplayMember = "Nombre";
            TipoCajaCmb.SelectedIndex = -1;
        }

        private void BuscarBttn_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(CuitText.Text, @"^\d{11}$"))
            {
                MessageBox.Show("El CUIT debe ser numérico y contener exactamente 11 dígitos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var cliente = modelo.BuscarCliente(CuitText.Text.Trim());
            if (cliente != null)
            {
                MessageBox.Show($"Cliente encontrado: {cliente.RazonSocial}. Proceda a ingresar la encomienda.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DatosEncomiendaGpBox.Enabled = true;
                CuitText.Enabled = false;
                BuscarBttn.Enabled = false;
            }
            else
            {
                MessageBox.Show("CUIT no registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DatosEncomiendaGpBox.Enabled = false;
            }
        }

        private void CiudadCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AgenciaRdBttn.Checked && CiudadCmb.SelectedItem is Ciudad ciudadSeleccionada)
            {
                EntregaCmb.DataSource = modelo.ObtenerAgenciasPorCiudad(ciudadSeleccionada.Id);
                EntregaCmb.DisplayMember = "Nombre";
                EntregaCmb.ValueMember = "Id";
                EntregaCmb.SelectedIndex = -1;
            }
        }

        private void TipoEntrega_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.Checked)
            {
                if (rb == DomicilioRdBttn)
                {
                    DomicilioText.Enabled = true;
                    EntregaCmb.Enabled = false;
                    EntregaCmb.DataSource = null;
                }
                else // AgenciaRdBttn
                {
                    DomicilioText.Enabled = false;
                    DomicilioText.Clear();
                    EntregaCmb.Enabled = true;
                    if (CiudadCmb.SelectedItem == null)
                    {
                        MessageBox.Show("Primero debe seleccionar una Ciudad.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        AgenciaRdBttn.Checked = false;
                    }
                    else
                    {
                        CiudadCmb_SelectedIndexChanged(sender, e);
                    }
                }
            }
        }

        private void AñadirBttn_Click(object sender, EventArgs e)
        {
            string dni = DNIText.Text.Trim();

            // Validaciones (Ajustadas desde el código que funciona)
            if (CiudadCmb.SelectedIndex == -1 || TipoCajaCmb.SelectedIndex == -1 ||
                (!AgenciaRdBttn.Checked && !DomicilioRdBttn.Checked) || CantidadNum.Value < 1)
            {
                MessageBox.Show("Debe completar todos los campos de destino y encomienda.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Regex.IsMatch(dni, @"^\d{7,8}$"))
            {
                MessageBox.Show("El DNI debe ser numérico y tener entre 7 y 8 dígitos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (AgenciaRdBttn.Checked && EntregaCmb.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar la Agencia/CD de destino.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (DomicilioRdBttn.Checked && string.IsNullOrEmpty(DomicilioText.Text))
            {
                MessageBox.Show("Debe ingresar la dirección del Domicilio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Se crea el ítem para la lista (solo con Tipo y Cantidad como en el diseñador)
            var item = new ListViewItem(new[] { TipoCajaCmb.Text, CantidadNum.Value.ToString() });
            EncomiendasListView.Items.Add(item);

            MessageBox.Show($"Se añadió una caja tipo {TipoCajaCmb.Text} a la lista.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Limpieza (opcional, mantiene los datos de destino para añadir más cajas iguales)
            TipoCajaCmb.SelectedIndex = -1;
            CantidadNum.Value = 0;
        }

        private void QuitarBttn_Click(object sender, EventArgs e)
        {
            if (EncomiendasListView.SelectedItems.Count > 0)
            {
                EncomiendasListView.Items.Remove(EncomiendasListView.SelectedItems[0]);
            }
            else
            {
                MessageBox.Show("Debe seleccionar una línea para quitar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ConfirmarBttn_Click(object sender, EventArgs e)
        {
            string dni = DNIText.Text.Trim();
            bool esDomicilio = DomicilioRdBttn.Checked;
            string domicilio = DomicilioText.Text.Trim();
            int agenciaDestinoID = 0;
            var ciudadDestino = (Ciudad)CiudadCmb.SelectedItem;
            int cdDestinoID = ciudadDestino.CD_ID;

            if (!esDomicilio)
            {
                var destinoSeleccionado = (AgenciaCD)EntregaCmb.SelectedItem;
                agenciaDestinoID = destinoSeleccionado.Id;
            }

            if (EncomiendasListView.Items.Count == 0)
            {
                MessageBox.Show("Debe añadir al menos una encomienda a la lista antes de confirmar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Las validaciones de DNI y destino ya se hicieron en el botón Añadir,pero se mantienen aca como una última capa de seguridad.

            if (!Regex.IsMatch(DNIText.Text, @"^\d{7,8}$"))
            {
                MessageBox.Show("El DNI debe ser numérico y tener entre 7 y 8 dígitos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            var itemsAgrupados = new Dictionary<string, int>();
            foreach (ListViewItem item in EncomiendasListView.Items)
            {
                string tipoCaja = item.SubItems[0].Text; // "S", "M", etc.
                int cantidad = int.Parse(item.SubItems[1].Text);

                if (itemsAgrupados.ContainsKey(tipoCaja))
                {
                    itemsAgrupados[tipoCaja] += cantidad;
                }
                else
                {
                    itemsAgrupados.Add(tipoCaja, cantidad);
                }
            }

            string codigoDestino = AgenciaRdBttn.Checked ? ((AgenciaCD)EntregaCmb.SelectedItem).Id.ToString() : "DOM";

            var datosImposicion = new DatosImposicion
            {
                Items = itemsAgrupados,
                DNIAutorizadoRetirar = DNIText.Text,
                EntregaDomicilio = esDomicilio,
                DomicilioDestino = domicilio,
                AgenciaDestinoID = agenciaDestinoID,
                CDDestinoID = cdDestinoID
            };

            List<string> guiasGeneradas = modelo.ConfirmarImposicion(datosImposicion);

            Guías.DataSource = guiasGeneradas;
            DatosClienteGpBox.Visible = false;
            DatosEncomiendaGpBox.Visible = false;
            GuiasGeneradasGpBox.Visible = true;
        }

        private void Finalizar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void CancelarBttn_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            CuitText.Clear();
            DomicilioRdBttn.Checked = false;
            AgenciaRdBttn.Checked = false;
            CiudadCmb.SelectedIndex = -1;
            DomicilioText.Clear();
            EntregaCmb.DataSource = null;
            TipoCajaCmb.SelectedIndex = -1;
            CantidadNum.Value = 0;
            DNIText.Clear();
            EncomiendasListView.Items.Clear();
            Guías.DataSource = null;

            GuiasGeneradasGpBox.Visible = false;
            DatosClienteGpBox.Visible = true;
            DatosEncomiendaGpBox.Visible = true;
            DatosEncomiendaGpBox.Enabled = false;

            CuitText.Enabled = true;
            BuscarBttn.Enabled = true;
        }
    }
}
