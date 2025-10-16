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

        // Propiedad calculada: Resultado = Ventas - Costos
        public decimal ResultadoEmpresa => Ventas - Costos;

        // Propiedad calculada: Margen Bruto = (Resultado / Ventas)
        public decimal MargenBruto => (Ventas > 0) ? (ResultadoEmpresa / Ventas) : 0;

        // Propiedad calculada: Rentabilidad en porcentaje
        public decimal Rentabilidad => MargenBruto * 100;
    }
}
