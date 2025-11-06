using CAIGrupoG.Almacenes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CAIGrupoG.Playero
{
    public class PlayeroModelo
    {
        // Cambia const por propiedad
        public int NuestroCD { get; set; }

        public PlayeroModelo(int cdSeleccionado)
        {
            NuestroCD = cdSeleccionado;
        }

        public PlayeroModelo()
        {
        }

        public (List<GuiaEntidad> Cargas, List<GuiaEntidad> Descargas) BuscarGuiasPorPatente(string patente)
        {
            var todas = GuiaAlmacen.Guias;

            var cargas = todas.Where(g =>
                g.Estado == EstadoEncomiendaEnum.AdmitidoCDOrigen &&
                g.CDOrigenID == NuestroCD &&
                g.DNIAutorizadoRetirar == patente
            ).ToList();

            var descargas = todas.Where(g =>
                g.Estado == EstadoEncomiendaEnum.AdmitidoCDDestino &&
                g.CDDestinoID == NuestroCD &&
                g.DNIAutorizadoRetirar == patente
            ).ToList();

            return (cargas, descargas);
        }

        public void ConfirmarOperacion(List<GuiaEntidad> cargas, List<GuiaEntidad> descargas)
        {
            if (cargas != null)
            {
                foreach (var guia in cargas)
                {
                    guia.Estado = EstadoEncomiendaEnum.EnTransito;
                }
            }

            if (descargas != null)
            {
                foreach (var guia in descargas)
                {
                    guia.Estado = EstadoEncomiendaEnum.Entregado;
                }
            }
        }
    }
}
