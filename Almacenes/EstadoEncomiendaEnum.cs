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
        AdmitidoCDOrigen = 5,
        EnTransito = 6,
        AdmitidoCDDestino = 7,
        DistribucionUltimaMillaDomicilio = 8,
        DistribucionUltimaMillaAgencia = 9,
        AgenciaDestino = 10,
        PrimerIntentoDeEntrega = 11,
        Rechazado = 12,
        Entregado = 13,
        Facturada = 14
    }
}
