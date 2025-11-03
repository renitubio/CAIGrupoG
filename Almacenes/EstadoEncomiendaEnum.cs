using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Almacenes
{
    public enum EstadoEncomiendaEnum
    {
        ImpuestoCallCenter = 1,
        ImpuestoAgencia = 2,
        EnCaminoARetirarDomicilio = 3,
        EnCaminoARetirarAgencia = 4,
        PendienteDeRetiroEnAgencia = 5,
        AdmitidoCDOrigen = 6,
        EnTransito = 7,
        DistribucionUltimaMillaDomicilio = 8,
        DistribucionUltimaMillaAgencia = 9,
        PrimerIntentoDeEntrega = 10,
        Rechazado = 11,
        Entregado = 12,
    }
}
