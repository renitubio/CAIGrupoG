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
            // Utiliza GuiaEntidad y CentroDistribucionEntidad del Almacenes
        }

        public List<GuiaEntidad> BuscarGuiasPorDNI(string dni)
        {
            if (CentroDistribucionAlmacen.CentroDistribucionActual == null)
            {
                throw new InvalidOperationException("No se ha seleccionado un Centro de Distribución en el Menú Principal.");
            }
            int cdActualID = CentroDistribucionAlmacen.CentroDistribucionActual.CD_ID;
            EstadoEncomiendaEnum estadoRequerido = EstadoEncomiendaEnum.AdmitidoCDDestino;

            return GuiaAlmacen.Guias
                .Where(g =>
                    g.DNIAutorizadoRetirar == dni &&
                    g.Estado == estadoRequerido &&
                    g.CDDestinoID == cdActualID
                )
                .ToList();
        }

        public void ConfirmarRetiro(List<GuiaEntidad> guiasARetirar)
        {
            EstadoEncomiendaEnum nuevoEstado = EstadoEncomiendaEnum.Entregado;

            foreach (var guiaEntidad in guiasARetirar)
            {
                var guia = GuiaAlmacen.Guias
                  .FirstOrDefault(g => g.NumeroGuia == guiaEntidad.NumeroGuia);

                if (guia != null)
                {
                    guia.Estado = nuevoEstado;
                }
            }
            GuiaAlmacen.Grabar();
        }
    }
}
