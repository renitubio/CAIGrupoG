using CAIGrupoG.Playero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.RendiciónFletero
{
    public class Guia
    {
        public string NumeroGuia { get; set; }
        public EstadoEncomienda Estado { get; set; }
        public TipoPaquete TipoPaquete { get; set; }
        public string CUIT { get; set; }
        public string DniAutorizadoRetirar { get; set; }
        public string Destino { get; set; } // Puede ser una dirección o el nombre de una agencia
    }

    // Enumeración para los estados específicos de esta pantalla
    public enum EstadoEncomienda
    {
        DistribucionUltimaMillaAgencia,
        DistribucionUltimaMillaDomicilio,
        PrimerIntentoDeEntrega,
        EnCaminoARetirarDomicilio,
        EnCaminoARetirarAgencia,
        Entregado,
        Rechazado,
        EnCDDestino,
        EnCDOrigen
    }
    public enum TipoPaquete
    {
        S,
        M,
        L,
        XL
    }

}
