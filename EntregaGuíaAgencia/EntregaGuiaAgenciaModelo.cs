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
        public EntregaGuiaAgenciaModelo() { }

        public List<Guia> BuscarGuiasPorDNI(string dni)
        {
            if (AgenciaAlmacen.AgenciaActual == null)
                throw new InvalidOperationException("No se ha seleccionado una Agencia en el Menú Principal.");

            int agenciaActualID = AgenciaAlmacen.AgenciaActual.AgenciaID;
            EstadoEncomiendaEnum estadoRequerido = EstadoEncomiendaEnum.AgenciaDestino;

            var guiasEntidad = GuiaAlmacen.Guias
                .Where(g =>
                    g.Estado == estadoRequerido &&
                    g.AgenciaDestinoID == agenciaActualID &&
                    g.DNIAutorizadoRetirar == dni
                )
                .ToList();

            var guiasViewModel = guiasEntidad.Select(g => new Guia
            {
                NumeroGuia = g.NumeroGuia,
                TipoPaquete = (TipoPaquete)g.TipoPaquete,
                Estado = (EstadoGuia)g.Estado,
                TipoPaqueteTexto = Enum.GetName(typeof(TipoPaquete), (TipoPaquete)g.TipoPaquete)
            }).ToList();

            return guiasViewModel;
        }

        public void ConfirmarRetiro(List<Guia> guiasARetirar)
        {
            EstadoEncomiendaEnum nuevoEstado = EstadoEncomiendaEnum.Entregado;

            foreach (var guiaVM in guiasARetirar)
            {
                var guiaEntidad = GuiaAlmacen.Guias
                    .FirstOrDefault(g => g.NumeroGuia == guiaVM.NumeroGuia);

                if (guiaEntidad != null)
                    guiaEntidad.Estado = nuevoEstado;
            }

            GuiaAlmacen.Grabar();
        }
    }
}

