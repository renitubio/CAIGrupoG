using CAIGrupoG.Almacenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.ConsultaTracking
{
    public class ConsultarTrackingModelo
    {
        public ConsultarTrackingModelo()
        {

        }

        /// Busca el estado de una guía por su número de seguimiento.
        public Guia BuscarGuia(string numeroGuia)
        {
            numeroGuia = numeroGuia.ToUpper();
            var guiaEntidad = GuiaAlmacen.Guias.FirstOrDefault(g => g.NumeroGuia == numeroGuia);
            if (guiaEntidad == null)
                return null;
            return new Guia
            {
                NumeroGuia = guiaEntidad.NumeroGuia,
                Estado = ConvertirEstado(guiaEntidad.Estado)
            };
        }

        private EstadoGuia ConvertirEstado(EstadoEncomiendaEnum estado)
        {
            switch (estado)
            {
                case EstadoEncomiendaEnum.ImpuestoCallCenter:
                    return EstadoGuia.ImpuestoCallCenter;
                case EstadoEncomiendaEnum.ImpuestoAgencia:
                    return EstadoGuia.ImpuestoAgencia;
                case EstadoEncomiendaEnum.EnCaminoARetirarDomicilio:
                    return EstadoGuia.EnCaminoARetirarDomicilio;
                case EstadoEncomiendaEnum.EnCaminoARetirarAgencia:
                    return EstadoGuia.EnCaminoARetirarAgencia;
                case EstadoEncomiendaEnum.AdmitidoCDOrigen:
                    return EstadoGuia.AdmitidoCDOrigen;
                case EstadoEncomiendaEnum.EnTransito:
                    return EstadoGuia.EnTransito;
                case EstadoEncomiendaEnum.AdmitidoCDDestino:
                    return EstadoGuia.AdmitidoCDDestino;
                case EstadoEncomiendaEnum.DistribucionUltimaMillaDomicilio:
                    return EstadoGuia.DistribucionUltimaMillaDomicilio;
                case EstadoEncomiendaEnum.DistribucionUltimaMillaAgencia:
                    return EstadoGuia.DistribucionUltimaMillaAgencia;
                case EstadoEncomiendaEnum.AgenciaDestino:
                    return EstadoGuia.AgenciaDestino;
                case EstadoEncomiendaEnum.PrimerIntentoDeEntrega:
                    return EstadoGuia.PrimerIntentoDeEntrega;
                case EstadoEncomiendaEnum.Rechazado:
                    return EstadoGuia.Rechazado;
                case EstadoEncomiendaEnum.Entregado:
                    return EstadoGuia.Entregado;
                default:
                    throw new ArgumentOutOfRangeException(nameof(estado), $"Estado no soportado: {estado}");
            }
        }
    }
}
