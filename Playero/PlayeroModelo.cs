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
                g.Estado == EstadoEncomiendaEnum.EnTransito &&
                g.CDDestinoID == NuestroCD &&
                g.DNIAutorizadoRetirar == patente
            ).ToList();

            return (cargas, descargas);
        }

        public Dictionary<string, List<GuiaEntidad>> ConfirmarOperacion(List<GuiaEntidad> cargas, List<GuiaEntidad> descargas)
        {
            if (cargas != null)
            {
                foreach (var guia in cargas)
                {
                    guia.Estado = EstadoEncomiendaEnum.EnTransito;
                }
            }

            var agrupadas = new Dictionary<string, List<GuiaEntidad>>();

            if (descargas != null)
            {
                foreach (var guia in descargas)
                {
                    guia.Estado = EstadoEncomiendaEnum.AdmitidoCDDestino;
                }

                // Agrupar por DomicilioDestino si EntregaDomicilio es true
                var porDomicilio = descargas.Where(g => g.EntregaDomicilio).GroupBy(g => g.DomicilioDestino)
                    .ToDictionary(g => $"Domicilio: {g.Key}", g => g.ToList());
                foreach (var kvp in porDomicilio)
                    agrupadas[kvp.Key] = kvp.Value;

                // Agrupar por AgenciaDestinoID si EntregaAgencia es true
                var porAgencia = descargas.Where(g => g.EntregaAgencia).GroupBy(g => g.AgenciaDestinoID)
                    .ToDictionary(g => $"Agencia: {g.Key}", g => g.ToList());
                foreach (var kvp in porAgencia)
                    agrupadas[kvp.Key] = kvp.Value;

                // Las restantes como entrega en CD
                var enCD = descargas.Where(g => !g.EntregaDomicilio && !g.EntregaAgencia).ToList();
                if (enCD.Any())
                    agrupadas["Entrega en CD"] = enCD;
            }

            return agrupadas;
        }

        public List<HojaDeRutaEntidad> CrearHojasDeRutaDistribucion(Dictionary<string, List<GuiaEntidad>> agrupadas)
        {
            var hojas = new List<HojaDeRutaEntidad>();
            int ultimoId = HojaDeRutaAlmacen.HojasDeRuta.Any() ? HojaDeRutaAlmacen.HojasDeRuta.Max(h => h.HDR_ID) :0;
            var fletero = FleteroAlmacen.Fleteros.FirstOrDefault(f => f.CD_ID == NuestroCD);
            string fleteroDNI = fletero?.FleteroDNI ?? string.Empty;

            foreach (var grupo in agrupadas)
            {
                var hoja = new HojaDeRutaEntidad
                {
                    HDR_ID = ++ultimoId,
                    ServicioID =0,
                    FechaCreacion = DateTime.Now,
                    FleteroDNI = fleteroDNI,
                    Tipo = TipoHDREnum.Distribucion, // Asumiendo que existe y es =3
                    Completada = false,
                    Guias = grupo.Value
                };
                HojaDeRutaAlmacen.Nuevo(hoja);
                hojas.Add(hoja);
            }
            HojaDeRutaAlmacen.Grabar();
            return hojas;
        }
    }
}
