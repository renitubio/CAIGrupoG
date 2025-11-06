using CAIGrupoG.Almacenes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CAIGrupoG.Playero
{
    public class PlayeroModelo
    {
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
            var ahora = DateTime.Now;

            // Buscar el servicio de carga (salida desde nuestro CD)
            var servicioCarga = ServicioAlmacen.servicios
                .Where(s => s.Patente == patente && s.CDOrigen == NuestroCD && s.FechaHoraSalida > ahora)
                .OrderBy(s => s.FechaHoraSalida)
                .FirstOrDefault();

            // Buscar el servicio de descarga (llegada a nuestro CD)
            var servicioDescarga = ServicioAlmacen.servicios
                .Where(s => s.Patente == patente && s.CDDestino == NuestroCD && s.FechaHoraLlegada > ahora)
                .OrderBy(s => s.FechaHoraLlegada)
                .FirstOrDefault();

            var cargas = new List<GuiaEntidad>();
            var descargas = new List<GuiaEntidad>();

            if (servicioCarga != null)
            {
                var hojaCarga = HojaDeRutaAlmacen.HojasDeRuta.FirstOrDefault(h => h.ServicioID == servicioCarga.ServicioID);
                if (hojaCarga != null && hojaCarga.Guias != null)
                {
                    cargas = hojaCarga.Guias
                        .Where(g => g.Estado == EstadoEncomiendaEnum.AdmitidoCDOrigen && g.CDOrigenID == NuestroCD)
                        .ToList();
                }
            }

            if (servicioDescarga != null)
            {
                var hojaDescarga = HojaDeRutaAlmacen.HojasDeRuta.FirstOrDefault(h => h.ServicioID == servicioDescarga.ServicioID);
                if (hojaDescarga != null && hojaDescarga.Guias != null)
                {
                    descargas = hojaDescarga.Guias
                        .Where(g => g.Estado == EstadoEncomiendaEnum.EnTransito && g.CDDestinoID == NuestroCD)
                        .ToList();
                }
            }

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
