using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAIGrupoG.Almacenes;

namespace CAIGrupoG.ResultadoCostosVSVentas
{
    public class CostosVsVentasModelo
    {
        public List<ResultadoEconomico> BuscarResultados(DateTime fechaDesde, DateTime fechaHasta)
        {
           
            DateTime inicioDia = fechaDesde.Date;
            
            DateTime finDia = fechaHasta.Date.AddDays(1).AddTicks(-1);

            // Estado 14 corresponde a "Facturado"
            var estadoFacturado = 14;

            var guiasFiltradas = GuiaAlmacen.Guias
                .Where(g => g.FechaAdmision >= inicioDia && g.FechaAdmision <= finDia)
                .Where(g => (int)g.Estado == estadoFacturado)
                .ToList();

            var numeroGuiasEntregadas = guiasFiltradas.Select(g => g.NumeroGuia).ToList();

            decimal ventasTotales = guiasFiltradas
                .Sum(g => g.Importe);

            decimal costosTotales = EgresosAlmacen.Egresos
                .Where(e => numeroGuiasEntregadas.Contains(e.NumeroGuia))
                .Sum(e => e.MontoPago);

            var resultadoGlobal = new ResultadoEconomico
            {
                Fecha = inicioDia,
                Ventas = ventasTotales,
                Costos = costosTotales
            };

            return new List<ResultadoEconomico> { resultadoGlobal };
        }
    }
}


