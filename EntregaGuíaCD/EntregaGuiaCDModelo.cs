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
        public EntregaGuiaCDModelo()
        {
        }
        public List<Guia> BuscarGuiasPorDNI(string dni)
        {
            // 1. Obtener el ID del CD "logueado" (como vimos en MenuPrincipal.cs)
            if (CentroDistribucionAlmacen.CentroDistribucionActual == null)
            {
                // Si alguien abre este form sin seleccionar un CD en el menú
                throw new InvalidOperationException("No se ha seleccionado un Centro de Distribución en el Menú Principal.");
            }
            int cdActualID = CentroDistribucionAlmacen.CentroDistribucionActual.CD_ID;

            // 2. Definir el estado que buscamos
            EstadoEncomiendaEnum estadoRequerido = EstadoEncomiendaEnum.AdmitidoCDDestino;

            // 3. Buscar en el Almacén
            var guiasEntidad = GuiaAlmacen.Guias
                .Where(g =>
                    g.DNIAutorizadoRetirar == dni &&
                    g.Estado == estadoRequerido &&
                    g.CDDestinoID == cdActualID
                )
                .ToList();

            // 4. Mapear de la lista de 'GuiaEntidad' (Datos) 
            var guiasViewModel = guiasEntidad.Select(g => new Guia
            {
                NumeroGuia = g.NumeroGuia,
                DniDestinatario = g.DNIAutorizadoRetirar,
                TipoPaquete = (TipoPaquete)g.TipoPaquete,
                Estado = EstadoGuia.PendienteDeRetiroEnCD
            }).ToList();

            return guiasViewModel;
        }

        public void ConfirmarRetiro(List<Guia> guiasARetirar)
        {
            // 1. Definís el nuevo estado
            EstadoEncomiendaEnum nuevoEstado = EstadoEncomiendaEnum.Entregado; // (Estado 13)

            foreach (var guiaVM in guiasARetirar)
            {
                var guiaEntidad = GuiaAlmacen.Guias
                  .FirstOrDefault(g => g.NumeroGuia == guiaVM.NumeroGuia);

                if (guiaEntidad != null)
                {
                    // 2. Cambiás el estado de la guía en el Almacén
                    guiaEntidad.Estado = nuevoEstado;
                }
            }

            // 3. Grabás los cambios permanentemente en el Guias.json
            GuiaAlmacen.Grabar();
        }
    }
}
