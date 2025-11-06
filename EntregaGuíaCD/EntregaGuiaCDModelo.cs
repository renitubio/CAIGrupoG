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
            // El constructor está vacío.
            // Los datos se leen directamente de los Almacenes estáticos.
        }

        /// <summary>
        /// Busca guías en el GuiaAlmacen que coincidan con la regla de negocio.
        /// </summary>
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
            // ASUMO que tu enum real (EstadoEncomiendaEnum) tiene este valor
            EstadoEncomiendaEnum estadoRequerido = EstadoEncomiendaEnum.AdmitidoCDDestino;

            // 3. Buscar en el Almacén (la fuente real 'GuiaEntidad')
            var guiasEntidad = GuiaAlmacen.Guias
                .Where(g =>
                    g.DNIAutorizadoRetirar == dni &&
                    g.Estado == estadoRequerido &&
                    g.CDDestinoID == cdActualID
                )
                .ToList();

            // 4. Mapear de la lista de 'GuiaEntidad' (Datos) 
            //    a la lista de 'Guia' (el View Model que espera el Form)
            var guiasViewModel = guiasEntidad.Select(g => new Guia
            {
                NumeroGuia = g.NumeroGuia,
                DniDestinatario = g.DNIAutorizadoRetirar,
                // Hacemos un "cast" directo de los enums
                TipoPaquete = (TipoPaquete)g.TipoPaquete,
                // El estado es el que espera el Form para mostrar
                Estado = EstadoGuia.PendienteDeRetiroEnCD
            }).ToList();

            return guiasViewModel;
        }

        /// <summary>
        /// Confirma el retiro, actualizando las entidades reales en el Almacén.
        /// </summary>
        public void ConfirmarRetiro(List<Guia> guiasARetirar)
        {
            // 'guiasARetirar' es la lista de View Models (Guia)
            // Debemos encontrar las entidades reales (GuiaEntidad) y modificarlas.

            // ASUMO que tu enum real tiene el estado "Entregado"
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
