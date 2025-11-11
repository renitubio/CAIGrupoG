using CAIGrupoG.Almacenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CAIGrupoG.EntregaGuíaAgencia
{
    public class EntregaGuiaAgenciaModelo
    {
        public EntregaGuiaAgenciaModelo()
        {
        }


        /// Busca guías en el GuiaAlmacen que coincidan con la regla de negocio.

        public List<Guia> BuscarGuiasPorDNI(string dni)
        {

            if (AgenciaAlmacen.AgenciaActual == null)
            {
                // Si alguien abre este form sin seleccionar una Agencia en el menú
                throw new InvalidOperationException("No se ha seleccionado una Agencia en el Menú Principal.");
            }
            
            int agenciaActualID = AgenciaAlmacen.AgenciaActual.AgenciaID;

            EstadoEncomiendaEnum estadoRequerido = EstadoEncomiendaEnum.AgenciaDestino;

            var guiasEntidad = GuiaAlmacen.Guias
                .Where(g =>
                    g.Estado == estadoRequerido &&
                    g.AgenciaDestinoID == agenciaActualID &&
                    g.DNIAutorizadoRetirar == dni
                )
                .ToList();

            // 4. Mapear de la lista de 'GuiaEntidad' (Datos) 
            //    a la lista de 'Guia' (el View Model que espera el Form)
            var guiasViewModel = guiasEntidad.Select(g => new Guia
            {
                NumeroGuia = g.NumeroGuia,
                TipoPaquete = (TipoPaquete)g.TipoPaquete,
                Estado = (EstadoGuia)g.Estado
            }).ToList();

            return guiasViewModel;
        }

        /// Confirma el retiro, actualizando las entidades reales en el Almacén.

        public void ConfirmarRetiro(List<Guia> guiasARetirar)
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

