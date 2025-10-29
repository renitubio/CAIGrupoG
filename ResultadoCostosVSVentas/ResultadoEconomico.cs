using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.ResultadoCostosVSVentas
{
    /// Representa los resultados económicos de una empresa en una fecha específica.
    public class ResultadoEconomico
    {
        public DateTime Fecha { get; set; }
        public decimal Ventas { get; set; }
        public decimal Costos { get; set; }

            public decimal ResultadoEmpresa => Ventas - Costos;

            // Rentabilidad: beneficio total sobre ventas
            public decimal Rentabilidad => (Ventas > 0) ? (ResultadoEmpresa / Ventas) * 100 : 0;
    }
}
