using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.EmitirFactura
{
    public enum EstadoGuia
    {
        Entregada,
        Devuelta,
        Facturada // Estado final después de emitir
    }

    public class Guia
    {
        public string NumeroGuia { get; set; }
        public EstadoGuia Estado { get; set; }
        public string RazonSocial { get; set; }
        public decimal Importe { get; set; }
        public string CUIT { get; set; }
    }
}
