using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAIGrupoG.Almacenes;

namespace CAIGrupoG.Playero
{
    public class PlayeroModelo
    {
        private const string NuestroCD = "CD01";

        public PlayeroModelo()
        {
        }

        public (List<GuiaEntidad> Cargas, List<GuiaEntidad> Descargas) BuscarGuiasPorPatente(string patente)
        {
            var cargas = new List<GuiaEntidad>();
            var descargas = new List<GuiaEntidad>();

            if (string.IsNullOrWhiteSpace(patente))
                return (cargas, descargas);

            // Usar el almacen ya cargado en memoria
            var servicios = ServicioAlmacen.servicios
                .Where(s => string.Equals(s.Patente?.Trim(), patente.Trim(), StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!servicios.Any())
                return (cargas, descargas);

            var nuestroCentro = CentroDistribucionAlmacen.CentrosDistribucion.FirstOrDefault(c => c.Nombre == NuestroCD);
            if (nuestroCentro == null)
                return (cargas, descargas);

            int nuestroCDId = nuestroCentro.CD_ID;

            DateTime now = DateTime.Now;
            ServicioEntidad servicioSeleccionado = null;
            double mejorDiferenciaSegundos = double.MaxValue;

            foreach (var servicio in servicios)
            {
                DateTime referencia;
                if (servicio.CDOrigen == nuestroCDId)
                    referencia = servicio.FechaHoraSalida;
                else if (servicio.CDDestingo == nuestroCDId)
                    referencia = servicio.FechaHoraLlegada;
                else
                    continue;

                double diff = Math.Abs((referencia - now).TotalSeconds);
                if (diff < mejorDiferenciaSegundos)
                {
                    mejorDiferenciaSegundos = diff;
                    servicioSeleccionado = servicio;
                }
            }

            if (servicioSeleccionado == null)
                return (cargas, descargas);

            var hojas = HojaDeRutaAlmacen.HojasDeRuta.Where(h => h.ServicioID == servicioSeleccionado.ServicioID);
            if (hojas == null || !hojas.Any())
                return (cargas, descargas);

            foreach (var hoja in hojas)
            {
                var guiasHDR = hoja.Guias;
                if (guiasHDR == null) continue;

                foreach (var guiaInHDR in guiasHDR)
                {
                    if (string.IsNullOrWhiteSpace(guiaInHDR.NumeroGuia)) continue;

                    var guiaOriginal = GuiaAlmacen.Guias.FirstOrDefault(g => string.Equals(g.NumeroGuia?.Trim(), guiaInHDR.NumeroGuia.Trim(), StringComparison.OrdinalIgnoreCase));
                    if (guiaOriginal == null) continue;

                    if (guiaOriginal.Estado == EstadoEncomiendaEnum.AdmitidoCDOrigen)
                        cargas.Add(guiaOriginal);
                    else if (guiaOriginal.Estado == EstadoEncomiendaEnum.EnTransito)
                        descargas.Add(guiaOriginal);
                }
            }

            return (cargas, descargas);
        }

        public void ConfirmarOperacion(List<GuiaEntidad> cargas, List<GuiaEntidad> descargas)
        {
            // 1) Actualizar estados según regla:
            //    AdmitidoCDOrigen -> EnTransito
            //    EnTransito -> AdmitidoCDDestino
            if (cargas != null)
            {
                foreach (var guia in cargas)
                {
                    var guiaOriginal = GuiaAlmacen.Guias.FirstOrDefault(g => g.NumeroGuia == guia.NumeroGuia);
                    if (guiaOriginal != null && guiaOriginal.Estado == EstadoEncomiendaEnum.AdmitidoCDOrigen)
                    {
                        guiaOriginal.Estado = EstadoEncomiendaEnum.EnTransito;
                    }
                }
            }

            if (descargas != null)
            {
                foreach (var guia in descargas)
                {
                    var guiaOriginal = GuiaAlmacen.Guias.FirstOrDefault(g => g.NumeroGuia == guia.NumeroGuia);
                    if (guiaOriginal != null && guiaOriginal.Estado == EstadoEncomiendaEnum.EnTransito)
                    {
                        guiaOriginal.Estado = EstadoEncomiendaEnum.AdmitidoCDDestino;
                    }
                }
            }

            // 2) Crear hojas de ruta (HDR) para entregas agrupadas por destino:
            // Recolectar todas las guías procesadas (de cargas y descargas) y filtrar las que requieran entrega
            var procesadas = new List<GuiaEntidad>();
            if (cargas != null) procesadas.AddRange(cargas);
            if (descargas != null) procesadas.AddRange(descargas);

            // Calcular siguiente HDR_ID
            int siguienteHDRID = 1;
            if (HojaDeRutaAlmacen.HojasDeRuta != null && HojaDeRutaAlmacen.HojasDeRuta.Any())
            {
                try
                {
                    siguienteHDRID = HojaDeRutaAlmacen.HojasDeRuta.Max(h => h.HDR_ID) + 1;
                }
                catch
                {
                    siguienteHDRID = 1;
                }
            }

            // Agrupar por AgenciaDestinoID para entregas en agencia
            var entregasAgencia = procesadas
                .Where(g => g != null && g.EntregaAgencia)
                .GroupBy(g => g.AgenciaDestinoID);

            foreach (var grupo in entregasAgencia)
            {
                var hoja = new HojaDeRutaEntidad
                {
                    HDR_ID = siguienteHDRID++,
                    ServicioID = 0, // no hay contexto de servicio en este método
                    FechaCreacion = DateTime.Now,
                    FleteroDNI = string.Empty,
                    Tipo = TipoHDREnum.Distribucion,
                    Completada = false,
                    Guias = grupo.Select(g => GuiaAlmacen.Guias.FirstOrDefault(x => x.NumeroGuia == g.NumeroGuia)).Where(x => x != null).ToList()
                };

                HojaDeRutaAlmacen.Nuevo(hoja);
            }

            // Agrupar por DomicilioDestino para entregas a domicilio
            var entregasDomicilio = procesadas
                .Where(g => g != null && g.EntregaDomicilio && !string.IsNullOrWhiteSpace(g.DomicilioDestino))
                .GroupBy(g => g.DomicilioDestino.Trim(), StringComparer.OrdinalIgnoreCase);

            foreach (var grupo in entregasDomicilio)
            {
                var hoja = new HojaDeRutaEntidad
                {
                    HDR_ID = siguienteHDRID++,
                    ServicioID = 0,
                    FechaCreacion = DateTime.Now,
                    FleteroDNI = string.Empty,
                    Tipo = TipoHDREnum.Distribucion,
                    Completada = false,
                    Guias = grupo.Select(g => GuiaAlmacen.Guias.FirstOrDefault(x => x.NumeroGuia == g.NumeroGuia)).Where(x => x != null).ToList()
                };

                HojaDeRutaAlmacen.Nuevo(hoja);
            }

            // 3) Persistir cambios
            GuiaAlmacen.Grabar();
            HojaDeRutaAlmacen.Grabar();
        }
    }
}
