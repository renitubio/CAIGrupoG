using CAIGrupoG.Almacenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.EntregaGuíaCD
{
    public class EntregaGuiaCDModelo
    {
        private readonly List<Guia> _guias;

        public EntregaGuiaCDModelo()
        {
       
        }
        public List<Guia> BuscarGuiasPorDNI(string dni)
        {
            int cdActualID = CentroDistribucionAlmacen.CentroDistribucionActual.CD_ID;

            //Definir el estado que buscamos
            EstadoEncomiendaEnum estadoRequerido = EstadoEncomiendaEnum.AdmitidoCDDestino;

            // Buscar en el Almacén
            var guiasEntidad = GuiaAlmacen.Guias
                .Where(g =>
                    g.DNIAutorizadoRetirar == dni &&
                    g.Estado == estadoRequerido &&
                    g.CDDestinoID == cdActualID
                )
                .ToList(); // Traemos las GuiaEntidad que coinciden

            // 4. Mapear de 'GuiaEntidad' (Datos) a 'Guia' (View Model que espera el Form)
            var guiasViewModel = guiasEntidad.Select(g => new Guia
            {
                NumeroGuia = g.NumeroGuia,
                DniDestinatario = g.DNIAutorizadoRetirar,
                // El Form hardcodea el texto "Pendiente de retiro...",
                // así que solo necesitamos mapear el TipoPaquete.
                TipoPaquete = (TipoPaquete)g.TipoPaquete, // Mapeo directo de Enums
                Estado = EstadoGuia.PendienteDeRetiroEnCD // Estado que espera el mock
            }).ToList();

            return guiasViewModel;
        }

        public void ConfirmarRetiro(List<Guia> guiasARetirar)
        {
            // 'guiasARetirar' es la lista de View Models (Guia)
            // Debemos encontrar las entidades reales (GuiaEntidad) y modificarlas.

            // ASUMO que tu enum real tiene este valor
            EstadoEncomiendaEnum nuevoEstado = EstadoEncomiendaEnum.Entregado;

            foreach (var guiaVM in guiasARetirar)
            {
                // Buscar la entidad original en el Almacén
                var guiaEntidad = GuiaAlmacen.Guias
                    .FirstOrDefault(g => g.NumeroGuia == guiaVM.NumeroGuia);

                if (guiaEntidad != null)
                {
                    // Cambiar el estado en la entidad real
                    guiaEntidad.Estado = nuevoEstado;
                }
            }

            // Guardar TODOS los cambios hechos en el Almacén
            GuiaAlmacen.Grabar();
        }
    }
}
