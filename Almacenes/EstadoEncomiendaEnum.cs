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
        AdmitidoCDDestino = 8,
        DistribucionUltimaMillaDomicilio = 9,
        DistribucionUltimaMillaAgencia = 10,
        PrimerIntentoDeEntrega = 11,
        Rechazado = 12,
        Entregado = 13

    }
}
