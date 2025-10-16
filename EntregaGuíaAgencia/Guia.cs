using CAIGrupoG.Playero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.EntregaGuíaAgencia
{
    public enum EstadoGuia
    {
        PendienteDeRetiroEnAgencia,
        Retirado,
        EnTransito
    }
    public enum TipoPaquete
    {
        S,
        M,
        L,
        XL
    }
    public class Guia
    {
        public string NumeroGuia { get; set; }
        public EstadoGuia Estado { get; set; }
        public TipoPaquete TipoPaquete { get; set; }
        public string DniDestinatario { get; set; }
    }
}
