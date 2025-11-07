using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.ResultadoCostosVSVentas
{
    public class CostosVsVentasModelo
    {
        private readonly List<ResultadoEconomico> _resultados;

        public CostosVsVentasModelo()
        {
            _resultados = new List<ResultadoEconomico>();
            CargarDatosFicticios();
        }

        /// Busca y devuelve una lista de resultados económicos dentro de un rango de fechas.
        public List<ResultadoEconomico> BuscarResultados(DateTime fechaDesde, DateTime fechaHasta)
        {
            // Nos aseguramos de incluir el día completo en la fecha "Hasta"
            return _resultados
                .Where(r => r.Fecha.Date >= fechaDesde.Date && r.Fecha.Date <= fechaHasta.Date)
                .OrderBy(r => r.Fecha) // Opcional: ordenamos los resultados por fecha
                .ToList();
        }

        /// Genera datos de prueba en ARS para diferentes fechas.
      
        
        //               ¡ CORRESPONDE A PROTOTIPO; MODELO AUN NO TERMINADO !
        private void CargarDatosFicticios()
        {
            // Datos de Septiembre 2025
            _resultados.Add(new ResultadoEconomico { Fecha = new DateTime(2025, 9, 15), Ventas = 1200000.50m, Costos = 750000.20m });
            _resultados.Add(new ResultadoEconomico { Fecha = new DateTime(2025, 9, 30), Ventas = 980000.00m, Costos = 620000.75m });

            // Datos de Octubre 2025
            _resultados.Add(new ResultadoEconomico { Fecha = new DateTime(2025, 10, 1), Ventas = 1500000.00m, Costos = 900000.00m });
            _resultados.Add(new ResultadoEconomico { Fecha = new DateTime(2025, 10, 15), Ventas = 2100000.30m, Costos = 1300000.10m });

            // Datos de Noviembre 2025
            _resultados.Add(new ResultadoEconomico { Fecha = new DateTime(2025, 11, 5), Ventas = 1850000.00m, Costos = 1100000.00m });
        }
    }
}
