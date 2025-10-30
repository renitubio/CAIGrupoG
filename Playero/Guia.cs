using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Playero
{
    public class Guia
    {
        public string NumeroGuia { get; set; }
        public TipoPaquete TipoPaquete { get; set; }
        public string CUIT { get; set; }
        public string DireccionDestinatario { get; set; }
        public string CDOrigen { get; set; }
        public string CDDestino { get; set; }
        public EstadoGuia Estado { get; set; }
    }
    // Enumeración para los estados de la guía
    public enum EstadoGuia
    {
        AdmitidoCDOrigen,
        EnTransito,
        AdmitidoCDDestino
    }

    // Enumeración para los tipos de paquete
    public enum TipoPaquete
    {
        S,
        M,
        L,
        XL
    }
}
