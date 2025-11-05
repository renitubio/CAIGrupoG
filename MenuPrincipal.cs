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
    public MenuPrincipal()
    {
        InitializeComponent();
    }


    //PRUEBA
    private void ConsultaTrackingBttn_Click(object sender, EventArgs e)
    {
        (new ConsultarTrackingForm()).ShowDialog();
    }
    private void EmitirFacturaButton_Click(object sender, EventArgs e)
    {

        (new EmitirFacturaForm()).ShowDialog();
    }

    private void EntregaGuiaAgenciaButton_Click(object sender, EventArgs e)
    {

        (new EntregaGuíaAgenciaForm()).ShowDialog();
    }

    private void ImpCallCenterButton_Click(object sender, EventArgs e)
    {

        (new ImposicionDeEncomiendaCallCenterForm()).ShowDialog();
    }
    private void ImpAgenciaButton_Click(object sender, EventArgs e)
    {
        (new ImposicionDeEncomiendaAgenciaForm()).ShowDialog();
    }
    private void ImpCentroDistribucionButton_Click(object sender, EventArgs e)
    {
        (new ImposicionDeEncomiendaCDForm()).ShowDialog();
    }
    private void EntregaGuiaCDButton_Click(object sender, EventArgs e)
    {
        (new EntregaGuíaCDForm()).ShowDialog();
    }

    private void PlayeroButton_Click(object sender, EventArgs e)
    {
        (new PlayeroForm()).ShowDialog();
    }

    private void RendicionFleteroButton_Click(object sender, EventArgs e)
    {

        (new RendicionFleteroForm()).ShowDialog();
    }

    private void CostosVentasButton_Click(object sender, EventArgs e)
    {
        (new CostosVsVentasForm()).ShowDialog();
    }


    private void MenuPrincipal_Load(object sender, EventArgs e)
    {
        CdCombo.DisplayMember = "Nombre";
        CdCombo.Items.AddRange(CentroDistribucionAlmacen.CentrosDistribucion.ToArray());

        AgenciaCombo.DisplayMember = "Nombre";
        AgenciaCombo.Items.AddRange(AgenciaAlmacen.Agencias.ToArray());
    }

    private void CdCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
        CentroDistribucionAlmacen.CentroDistribucionActual = CdCombo.SelectedItem as CentroDistribucionEntidad;
    }

    private void AgenciaCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
        AgenciaAlmacen.AgenciaActual = AgenciaCombo.SelectedItem as AgenciaEntidad;
    }



   
}
