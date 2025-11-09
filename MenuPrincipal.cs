using CAIGrupoG.Admisión;
using CAIGrupoG.Almacenes;
using CAIGrupoG.ConsultaTracking;
using CAIGrupoG.EmitirFactura;
using CAIGrupoG.EntregaGuíaAgencia;
using CAIGrupoG.EntregaGuíaCD;
using CAIGrupoG.Imposicion.ImpAgencia;
using CAIGrupoG.Imposicion.ImpCallCenter;
using CAIGrupoG.Imposicion.ImpCentroDistribucion;
using CAIGrupoG.Playero;
using CAIGrupoG.ResultadoCostosVSVentas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAIGrupoG;

public partial class MenuPrincipal : Form
{
    // Constructor sin cambios
    public MenuPrincipal()
    {
        InitializeComponent();
    }
    private void ConsultaTrackingBttn_Click(object sender, EventArgs e)
    {
        (new ConsultarTrackingForm()).ShowDialog();
    }

    // =================================================================
    // CORRECCIÓN 1: Usar CiudadAlmacen para cargar el Combo CD
    // (Asumo que CentroDistribucionEntidad tiene propiedades Nombre y CDID)
    private void MenuPrincipal_Load(object sender, EventArgs e)
    {
        // Usamos CiudadAlmacen que tiene la relación Ciudad/CDID
        CdCombo.DisplayMember = "Nombre";
        CdCombo.ValueMember = "CDID"; // Usar el ID del CD para pasarlo al Playero

        // Filtra los ítems de CiudadAlmacen que tienen un CDID para cargar
        var cdsDisponibles = CiudadAlmacen.Ciudades
                                         .Where(c => c.CDID > 0)
                                         .ToList();

        CdCombo.DataSource = cdsDisponibles;
        CdCombo.SelectedIndex = -1;

        // Carga de Agencias (se mantiene)
        AgenciaCombo.DisplayMember = "Nombre";
        // NOTA: Es posible que AgenciaCombo.Items.AddRange deba ser AgenciaCombo.DataSource
        // para un manejo correcto de objetos, pero mantengo tu estructura original si funciona:
        AgenciaCombo.Items.AddRange(AgenciaAlmacen.Agencias.ToArray());
    }

    // =================================================================
    // CORRECCIÓN 2: Lógica PlayeroButton_Click para obtener NuestroCD
    private void PlayeroButton_Click(object sender, EventArgs e)
    {
        // 1. Verificar si hay un CD seleccionado
        if (CdCombo.SelectedValue == null)
        {
            MessageBox.Show("Debe seleccionar un Centro de Distribución (CD) actual.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // 2. Obtener el CDID seleccionado (NuestroCD)
        // SelectedValue devuelve el objeto asociado a ValueMember (CDID)
        int cdSeleccionado = (int)CdCombo.SelectedValue;

        // 3. Crear y mostrar el PlayeroForm, pasando el CDID
        try
        {
            // Ahora llama al constructor PlayeroForm(int cdSeleccionado)
            var playeroForm = new PlayeroForm(cdSeleccionado);
            playeroForm.ShowDialog();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al iniciar el Formulario Playero: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }


    private void CdCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
        // NOTA: Si usas ValueMember/DataSource, el SelectedItem será una CiudadEntidad.
        // Asumo que CentroDistribucionAlmacen.CentroDistribucionActual necesita 
        // una entidad que contenga el CDID. La CiudadEntidad contiene ese dato.
        // Si CentroDistribucionEntidad es una clase separada, esto podría fallar.
        // Lo mantengo, asumiendo que el Almacén lo manejará:
        // CentroDistribucionAlmacen.CentroDistribucionActual = CdCombo.SelectedItem as CentroDistribucionEntidad;

        // Mejorar: Si el combo se carga con CiudadEntidad, es más seguro extraer el CDID:
        if (CdCombo.SelectedItem is CiudadEntidad ciudad)
        {
            // Aquí puedes buscar la entidad completa de CD si es necesario, 
            // o simplemente guardar el ID en una variable estática para referencia.
            // Para la funcionalidad Playero, solo necesitamos que el PlayeroForm reciba el ID.
        }
    }

    private void AgenciaCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
        AgenciaAlmacen.AgenciaActual = AgenciaCombo.SelectedItem as AgenciaEntidad;
    }

    // El resto de los métodos se mantienen iguales
    private void ImposicionAgenciaBttn_Click(object sender, EventArgs e)
    {
        (new ImposicionDeEncomiendaAgenciaForm()).ShowDialog();
    }

    private void rendicionFleteroButton_Click(object sender, EventArgs e)
    {
        (new RendicionFleteroForm()).ShowDialog();
    }

    private void ImpCallCenterButton_Click(object sender, EventArgs e)
    {
        (new ImposicionDeEncomiendaCallCenterForm()).ShowDialog();
    }

    private void ImpCDButton_Click(object sender, EventArgs e)
    {
        (new ImposicionDeEncomiendaCDForm()).ShowDialog();
    }

    private void EntregaCDButton_Click(object sender, EventArgs e)
    {
        (new EntregaGuíaCDForm()).ShowDialog();
    }

    private void EmitirFacturaButton_Click(object sender, EventArgs e)
    {
        (new EmitirFacturaForm()).ShowDialog();
    }

    private void EntregaAgenciaButton_Click(object sender, EventArgs e)
    {
        (new EntregaGuíaAgenciaForm()).ShowDialog();
    }

    private void CostosVentasButton_Click(object sender, EventArgs e)
    {
        (new CostosVsVentasForm()).ShowDialog();
    }
}
