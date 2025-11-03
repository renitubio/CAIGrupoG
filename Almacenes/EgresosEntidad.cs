using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Almacenes
{
    internal class EgresosEntidad
    {
        public decimal MontoPago { get; set; }

        public string NumeroGuia { get; set; }

        public DateTime FechaPago { get; set; }

        public int NumeroFactura { get; set; }

        public TipoEgresoEnum TipoEgreso { get; set; }
        public int AgenciaID { get; set; }

        public string FleteroDNI { get; set; }

        public string CUITEmpresaTransporte { get; set; }

    }
}
