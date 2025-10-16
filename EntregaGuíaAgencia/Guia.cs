using CAIGrupoG.Playero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.EntregaGuíaAgencia
{
    public class Guia
    {
        public string NumeroGuia { get; set; }
        public EstadoGuia Estado { get; set; }
        public TipoPaquete TipoPaquete { get; set; }
        public string DniDestinatario { get; set; }
    }
    // Enumeración para los tipos de paquete
    public enum TipoPaquete
    {
        S,
        M,
        L,
        XL
    }

    // Enumeración para los estados de la guía
    public enum EstadoGuia
    {
        PendienteDeRetiroEnAgencia,
        Retirado,
        EnTransito // Otros estados posibles
    }
}
