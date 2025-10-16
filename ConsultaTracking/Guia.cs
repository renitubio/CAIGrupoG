using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.ConsultaTracking
{
    public enum EstadoGuia
    {
        ImpuestoCallCenter,
        ImpuestoAgencia,
        EnCaminoARetirarDomicilio,
        EnCaminoARetirarAgencia,
        AdmitidoEnCDOrigen,
        EnTransito,
        EnCDDestino,
        DistribucionUltimaMillaDomicilio,
        PrimerIntentoEntrega,
        Rechazado,
        DistribucionUltimaMillaAgencia,
        PendienteDeRetiroEnAgencia,
        Entregado
    }
    public class Guia
    {
        public string NumeroGuia { get; set; }
        public EstadoGuia Estado { get; set; }
    }
}
