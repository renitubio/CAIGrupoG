using CAIGrupoG.Admisión;
using CAIGrupoG.EntregaGuíaAgencia;
using CAIGrupoG.EntregaGuíaCD;
using CAIGrupoG.Imposicion.ImpCentroDistribucion;
using CAIGrupoG.Playero;
using CAIGrupoG.EmitirFactura;
using CAIGrupoG.ConsultaTracking;
using CAIGrupoG.ResultadoCostosVSVentas;
using CAIGrupoG.Imposicion.ImpCallCenter;
using CAIGrupoG.Imposicion.ImpAgencia;
using CAIGrupoG.Almacenes;

namespace CAIGrupoG
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new RendicionFleteroForm());

            //GUARDO POR CADA ALMACEN

            ClienteAlmacen.Grabar();
            GuiaAlmacen.Grabar();
            HojaDeRutaAlmacen.Grabar();


        }
    }
}