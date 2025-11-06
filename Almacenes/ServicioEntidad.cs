using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Almacenes
{
    internal class ServicioEntidad
    {
        public int ServicioID { get; set; }

        public string Patente { get; set; }

        public string CUITEmpresaTransporte { get; set; }

        public int CDOrigen { get; set; }

        public int CDDestino { get; set; }

        public DateTime FechaHoraSalida { get; set; }

        public DateTime FechaHoraLlegada { get; set; }

    }
}
