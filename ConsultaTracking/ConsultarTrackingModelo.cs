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
        private readonly List<GuiaEntidad> GuiaAlmacen;

        public ConsultarTrackingModelo()
        {
            GuiaAlmacen = new List<GuiaEntidad>();
            // Aquí puedes cargar datos ficticios si lo necesitas
        }


        public GuiaEntidad BuscarGuia(string numeroGuia)
        {
            return GuiaAlmacen.FirstOrDefault(g => g.NumeroGuia == numeroGuia);
        }

        /// Devuelve la descripción en texto del estado de la guía identificada por su número en el almacén.
        public string BuscarEstadoGuia(string numeroGuia)
        {
            var guia = GuiaAlmacen.FirstOrDefault(g => g.NumeroGuia.Equals(numeroGuia, StringComparison.OrdinalIgnoreCase));
            return ObtenerDescripcionEstado(guia);
        }

        public string ObtenerDescripcionEstado(GuiaEntidad guia)
        {
            if (guia == null) return string.Empty;

            switch (guia.Estado)
            {
                case EstadoEncomiendaEnum.ImpuestoCallCenter: return "Impuesto Call Center";
                case EstadoEncomiendaEnum.ImpuestoAgencia: return "Impuesto Agencia";
                case EstadoEncomiendaEnum.EnCaminoARetirarDomicilio: return "En camino a retirar (Domicilio)";
                case EstadoEncomiendaEnum.EnCaminoARetirarAgencia: return "En camino a retirar (Agencia)";
                case EstadoEncomiendaEnum.AdmitidoCDOrigen: return "Admitido en CD Origen";
                case EstadoEncomiendaEnum.EnTransito: return "En Tránsito";
                case EstadoEncomiendaEnum.AdmitidoCDDestino: return "En CD Destino";
                case EstadoEncomiendaEnum.DistribucionUltimaMillaDomicilio: return "En distribución a domicilio";
                case EstadoEncomiendaEnum.PrimerIntentoDeEntrega: return "Primer intento de entrega";
                case EstadoEncomiendaEnum.Rechazado: return "Rechazado";
                case EstadoEncomiendaEnum.DistribucionUltimaMillaAgencia: return "En distribución a agencia";
                case EstadoEncomiendaEnum.PendienteDeRetiroEnAgencia: return "Pendiente de retiro en agencia";
                case EstadoEncomiendaEnum.Entregado: return "Entregado";
                default: return guia.Estado.ToString();
            }
        }
    }
}
