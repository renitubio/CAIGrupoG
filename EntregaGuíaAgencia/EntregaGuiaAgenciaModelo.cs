using CAIGrupoG.Almacenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.EntregaGuíaAgencia
{
    public class EntregaGuiaAgenciaModelo
    {
        public EntregaGuiaAgenciaModelo()
        {
        }


        /// Busca guías en el GuiaAlmacen que coincidan con la regla de negocio.

        public List<GuiaEntidad> BuscarGuiasPorDNI(string dni)
        {
            // 1. Obtener el ID de la Agencia "logueada" (como vimos en MenuPrincipal.cs)
            if (AgenciaAlmacen.AgenciaActual == null)
            {
                // Si alguien abre este form sin seleccionar una Agencia en el menú
                throw new InvalidOperationException("No se ha seleccionado una Agencia en el Menú Principal.");
            }
            // ASUMO que AgenciaEntidad tiene la propiedad 'AgenciaID'
            int agenciaActualID = AgenciaAlmacen.AgenciaActual.AgenciaID;

            // 2. Definir el estado que buscamos
            // (Estado 5)
            EstadoEncomiendaEnum estadoRequerido = EstadoEncomiendaEnum.EnCaminoARetirarAgencia;

            // 3. Buscar en el Almacén (la fuente real 'GuiaEntidad')
            var guiasEntidad = GuiaAlmacen.Guias
                .Where(g =>
                    g.AgenciaDestinoID ==10 &&
                    g.DNIAutorizadoRetirar == dni
                )
                .ToList();

            // 4. Mapear de la lista de 'GuiaEntidad' (Datos) 
            //    a la lista de 'Guia' (el View Model que espera el Form)
            var guiasViewModel = guiasEntidad.Select(g => new GuiaEntidad
            {
                NumeroGuia = g.NumeroGuia,
                TipoPaquete = g.TipoPaquete,
                Estado = g.Estado,
                CDDestinoID = g.CDDestinoID,
                ClienteCUIT = g.ClienteCUIT,
                CDOrigenID = g.CDOrigenID,
                Importe = g.Importe,
                DNIAutorizadoRetirar = g.DNIAutorizadoRetirar,
                DomicilioDestino = g.DomicilioDestino,
                EntregaDomicilio = g.EntregaDomicilio,
                FechaAdmision = g.FechaAdmision,
                RetiroDomicilio = g.RetiroDomicilio,
                EntregaAgencia = g.EntregaAgencia,
                NumeroFactura = g.NumeroFactura,
                AgenciaDestinoID = g.AgenciaDestinoID
            }).ToList();

            return guiasViewModel;
        }

        /// Confirma el retiro, actualizando las entidades reales en el Almacén.

        public void ConfirmarRetiro(List<GuiaEntidad> guiasARetirar)
        {
            // 'guiasARetirar' es la lista de View Models (Guia)
            // Debemos encontrar las entidades reales (GuiaEntidad) y modificarlas.

            // (Estado 13)
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

            // Guardar TODOS los cambios hechos en el Almacén en el JSON
            GuiaAlmacen.Grabar();
        }
    }
}
