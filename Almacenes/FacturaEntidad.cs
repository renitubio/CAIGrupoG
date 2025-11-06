using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Almacenes
{
    internal class FacturaEntidad
    {
        public int NumeroFactura { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaEmision { get; set; }

        public string ClienteCUIT { get; set; }

    }
}
