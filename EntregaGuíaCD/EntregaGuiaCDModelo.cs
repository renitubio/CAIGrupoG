using CAIGrupoG.Almacenes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAIGrupoG.EntregaGuíaCD
{
    public class EntregaGuiaCDModelo
    {
        public EntregaGuiaCDModelo()
        {
        }

        public List<Guia> BuscarGuiasPorDNI(string dni)
        {
            foreach (var g in GuiaAlmacen.Guias)
            {
                Debug.WriteLine($"Guía: {g.NumeroGuia}, Estado: {g.Estado}, CDDestinoID: {g.CDDestinoID}, DNI: {g.DNIAutorizadoRetirar}");
            }

            if (CentroDistribucionAlmacen.CentroDistribucionActual == null)
            {
                throw new InvalidOperationException("No se ha seleccionado un Centro de Distribución en el Menú Principal.");
            }
            int cdActualID = CentroDistribucionAlmacen.CentroDistribucionActual.CD_ID;
            EstadoEncomiendaEnum estadoRequerido = EstadoEncomiendaEnum.AdmitidoCDDestino;

            var guiasEntidad = GuiaAlmacen.Guias
                .Where(g =>
                    g.DNIAutorizadoRetirar == dni && // Usar propiedad de GuiaEntidad
                    g.Estado == estadoRequerido &&
                    g.CDDestinoID == cdActualID
                ).ToList();

            var guiasEncontradas = guiasEntidad.Select(g => new Guia
            {
                NumeroGuia = g.NumeroGuia,
                TipoPaquete = (TipoPaquete)g.TipoPaquete,
                Estado = (EstadoGuia)g.Estado,
                TipoPaqueteTexto = Enum.GetName(typeof(TipoPaquete), (TipoPaquete)g.TipoPaquete)
            }).ToList();

            return guiasEncontradas;
        }

        public void ConfirmarRetiro(List<Guia> guiasARetirar)
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


