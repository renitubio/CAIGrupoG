using CAIGrupoG.Almacenes;
using CAIGrupoG.ConsultaTracking;
using CAIGrupoG.Imposicion.ImpAgencia;
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

    private void ImposicionAgenciaBttn_Click(object sender, EventArgs e)
    {
        (new ImposicionDeEncomiendaAgenciaForm()).ShowDialog();
    }
}
