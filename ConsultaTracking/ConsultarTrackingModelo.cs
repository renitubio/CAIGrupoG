using CAIGrupoG.Almacenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.ConsultaTracking
{
    public class ConsultarTrackingModelo
    {


        public ConsultarTrackingModelo()
        {

        }

        public GuiaEntidad BuscarGuia(string numeroGuia)
        { 
        
            // Solución: Buscar en la colección Guias de GuiaAlmacen
            return CAIGrupoG.Almacenes.GuiaAlmacen.Guias.FirstOrDefault(g => g.NumeroGuia == numeroGuia);
        }

        public EstadoEncomiendaEnum? ObtenerEstadoGuia(string numeroGuia)
        {
            var guia = BuscarGuia(numeroGuia);
            return guia?.Estado;
        }

        public string ObtenerDescripcionEstado(EstadoEncomiendaEnum estado)
        {
            switch (estado)
            {
                case EstadoEncomiendaEnum.ImpuestoCallCenter: return "Impuesto Call Center";
                case EstadoEncomiendaEnum.ImpuestoAgencia: return "Impuesto Agencia";
                case EstadoEncomiendaEnum.EnCaminoARetirarDomicilio: return "En camino a retirar (Domicilio)";
                case EstadoEncomiendaEnum.EnCaminoARetirarAgencia: return "En camino a retirar (Agencia)";
                case EstadoEncomiendaEnum.AdmitidoCDOrigen: return "Admitido en CD Origen";
                case EstadoEncomiendaEnum.EnTransito: return "En tránsito";
                case EstadoEncomiendaEnum.AdmitidoCDDestino: return "Admitido en CD Destino";
                case EstadoEncomiendaEnum.DistribucionUltimaMillaDomicilio: return "Distribución última milla (domicilio)";
                case EstadoEncomiendaEnum.DistribucionUltimaMillaAgencia: return "Distribución última milla (agencia)";
                case EstadoEncomiendaEnum.PrimerIntentoDeEntrega: return "Primer intento de entrega";
                case EstadoEncomiendaEnum.Rechazado: return "Rechazado";
                case EstadoEncomiendaEnum.Entregado: return "Entregado";
                default: return estado.ToString();
            }
        }
    }
}
